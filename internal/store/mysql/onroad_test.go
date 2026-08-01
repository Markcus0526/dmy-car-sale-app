package mysql

import (
	"context"
	"database/sql"
	"testing"

	"github.com/Markcus0526/carsaleman/internal/query"
)

func seedOnRoad(t *testing.T, db *sql.DB) {
	t.Helper()
	ctx := context.Background()

	for _, tbl := range []string{"tbl_onroad", "tbl_cartype"} {
		if _, err := db.ExecContext(ctx, "DELETE FROM "+tbl); err != nil {
			t.Fatalf("clearing %s: %v", tbl, err)
		}
	}

	// Every NOT NULL column must be supplied: the inferred schema has 107
	// columns that are NOT NULL with no DEFAULT, because CmsDB.xsd cannot
	// express defaults (export step 4 recovers the real ones).
	res, err := db.ExecContext(ctx, `
		INSERT INTO tbl_cartype
			(carseries, carcode, carname, eop, inprice, outprice,
			 otherprice1, otherprice2, otherprice3, otherprice4,
			 propval, profitval, outstoreprice, vinprefix, enginenoprefix, deleted)
		VALUES ('轿车系列','C1','测试车型','否',100000,120000,0,0,0,0,
		        0,0,0,'LSGH','E',0)`)
	if err != nil {
		t.Fatalf("seed cartype: %v", err)
	}
	cartypeID, _ := res.LastInsertId()

	rows := []struct {
		billno, vin, engineno, cartype, carname, colorname, billdate string
		inprice                                                      string
		orphan                                                       bool
	}{
		{"B001", "LSGH100001", "E001", "C1", "轿车 A", "红色", "2026-07-01", "100000.00", false},
		{"B002", "LSGH100002", "E002", "C1", "轿车 B", "白色", "2026-08-01", "110000.00", false},
		{"B003", "WDD200003", "E003", "C1", "越野车", "黑色", "2026-08-15", "250000.00", false},
		// 100% in a field, to prove LIKE metacharacters are treated literally.
		{"B004", "PCT100%", "E004", "C1", "特殊", "银色", "2026-08-20", "90000.00", false},
		// Orphaned cartypeid: only visible because vw_onroad uses LEFT JOIN.
		{"B005", "ORPH00005", "E005", "C9", "孤儿", "灰色", "2026-08-25", "80000.00", true},
	}
	for _, r := range rows {
		id := cartypeID
		if r.orphan {
			id = 999999
			// The FK from 0002 refuses this, correctly. Orphans can only exist
			// in data loaded BEFORE the constraint -- which is the whole reason
			// foreign keys are a separate migration applied after
			// cmd/migrate-data (plan §5.3: load, verify, then constrain).
			//
			// Disabling the check here reproduces that window rather than
			// pretending it cannot happen.
			if _, err := db.ExecContext(ctx, "SET FOREIGN_KEY_CHECKS = 0"); err != nil {
				t.Fatal(err)
			}
			defer db.ExecContext(ctx, "SET FOREIGN_KEY_CHECKS = 1")
		}
		_, err := db.ExecContext(ctx, `
			INSERT INTO tbl_onroad (billno, billdate, vin, engineno, cartypeid, cartype,
			                        carname, colorname, inprice, inflag, inkind)
			VALUES (?,?,?,?,?,?,?,?,?,0,0)`,
			r.billno, r.billdate, r.vin, r.engineno, id, r.cartype, r.carname, r.colorname, r.inprice)
		if err != nil {
			t.Fatalf("seed onroad %s: %v", r.billno, err)
		}
	}
}

func TestOnRoadListNoFilterReturnsAll(t *testing.T) {
	db := testDB(t)
	seedOnRoad(t, db)

	rows, truncated, err := NewOnRoadRepo(db).List(context.Background(), query.Filter{})
	if err != nil {
		t.Fatalf("List: %v", err)
	}
	if len(rows) != 5 {
		t.Errorf("rows = %d, want 5", len(rows))
	}
	if truncated {
		t.Error("5 rows should not be truncated")
	}
}

// The LEFT JOIN decision, pinned.
//
// Note the scope: once 0002's foreign key is in place an orphan cannot be
// created, so INNER and LEFT become equivalent going forward. The choice only
// matters for legacy data loaded before the constraint -- but that is exactly
// the data this port migrates, and with INNER those vehicles would vanish from
// every screen and report with no error anywhere.
func TestOnRoadOrphanedCarTypeStillAppears(t *testing.T) {
	db := testDB(t)
	seedOnRoad(t, db)

	rows, _, err := NewOnRoadRepo(db).List(context.Background(), query.Filter{
		Conditions: []query.Condition{{Field: "vin", Op: query.OpContains, Value: "ORPH"}},
	})
	if err != nil {
		t.Fatal(err)
	}
	if len(rows) != 1 {
		t.Fatalf("rows = %d, want 1 — an orphaned cartypeid must not hide the vehicle", len(rows))
	}
	if rows[0].CarSeries != "" {
		t.Errorf("carseries = %q, want empty for an orphan", rows[0].CarSeries)
	}
}

func TestOnRoadFilterContains(t *testing.T) {
	db := testDB(t)
	seedOnRoad(t, db)

	rows, _, err := NewOnRoadRepo(db).List(context.Background(), query.Filter{
		Conditions: []query.Condition{{Field: "vin", Op: query.OpContains, Value: "LSGH"}},
	})
	if err != nil {
		t.Fatal(err)
	}
	if len(rows) != 2 {
		t.Errorf("rows = %d, want 2", len(rows))
	}
}

// End-to-end proof that LIKE escaping survives into real SQL: "100%" must
// match only the row containing that literal text, not every row.
func TestOnRoadPercentIsLiteral(t *testing.T) {
	db := testDB(t)
	seedOnRoad(t, db)

	rows, _, err := NewOnRoadRepo(db).List(context.Background(), query.Filter{
		Conditions: []query.Condition{{Field: "vin", Op: query.OpContains, Value: "100%"}},
	})
	if err != nil {
		t.Fatal(err)
	}
	if len(rows) != 1 {
		t.Fatalf("rows = %d, want 1 — '%%' must be literal, not a wildcard", len(rows))
	}
	if rows[0].VIN != "PCT100%" {
		t.Errorf("vin = %q", rows[0].VIN)
	}
}

// Chinese filtering must work through the whole chain: utf8mb4 connection,
// bound parameter, LIKE.
func TestOnRoadFilterChinese(t *testing.T) {
	db := testDB(t)
	seedOnRoad(t, db)

	rows, _, err := NewOnRoadRepo(db).List(context.Background(), query.Filter{
		Conditions: []query.Condition{{Field: "carname", Op: query.OpContains, Value: "轿车"}},
	})
	if err != nil {
		t.Fatal(err)
	}
	if len(rows) != 2 {
		t.Errorf("rows = %d, want 2", len(rows))
	}
}

// The inclusive upper bound must include rows dated on the boundary day.
// Without the end-of-day widening this returns 1 instead of 2.
func TestOnRoadDateRangeIsInclusive(t *testing.T) {
	db := testDB(t)
	seedOnRoad(t, db)

	rows, _, err := NewOnRoadRepo(db).List(context.Background(), query.Filter{
		Conditions: []query.Condition{
			{Field: "billdate", Op: query.OpGTE, Value: "2026-08-01"},
			{Field: "billdate", Op: query.OpLTE, Value: "2026-08-15"},
		},
	})
	if err != nil {
		t.Fatal(err)
	}
	if len(rows) != 2 {
		t.Errorf("rows = %d, want 2 (2026-08-01 and 2026-08-15 both inclusive)", len(rows))
	}
}

func TestOnRoadConditionsAreAnded(t *testing.T) {
	db := testDB(t)
	seedOnRoad(t, db)

	rows, _, err := NewOnRoadRepo(db).List(context.Background(), query.Filter{
		Conditions: []query.Condition{
			{Field: "vin", Op: query.OpContains, Value: "LSGH"},
			{Field: "colorname", Op: query.OpContains, Value: "白"},
		},
	})
	if err != nil {
		t.Fatal(err)
	}
	if len(rows) != 1 {
		t.Errorf("rows = %d, want 1", len(rows))
	}
}

// A column that exists but is not in the allowlist must be refused, not
// quietly ignored -- ignoring it would return unfiltered data to a caller who
// believes it was filtered.
func TestOnRoadRejectsNonAllowlistedField(t *testing.T) {
	db := testDB(t)
	seedOnRoad(t, db)

	for _, field := range []string{"inflag", "cartypeid", "row_version"} {
		_, _, err := NewOnRoadRepo(db).List(context.Background(), query.Filter{
			Conditions: []query.Condition{{Field: field, Op: query.OpEquals, Value: "0"}},
		})
		if err == nil {
			t.Errorf("%s: a non-allowlisted column must be rejected", field)
		}
	}
}

// Prices must survive as exact decimal strings; a float round-trip would show
// up here as 100000 or 99999.999999.
func TestOnRoadPriceStaysADecimalString(t *testing.T) {
	db := testDB(t)
	seedOnRoad(t, db)

	rows, _, err := NewOnRoadRepo(db).List(context.Background(), query.Filter{
		Conditions: []query.Condition{{Field: "vin", Op: query.OpContains, Value: "LSGH100001"}},
	})
	if err != nil {
		t.Fatal(err)
	}
	if rows[0].InPrice == nil || *rows[0].InPrice != "100000.00" {
		t.Errorf("inprice = %v, want the exact string 100000.00", rows[0].InPrice)
	}
}
