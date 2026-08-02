package mysql

import (
	"context"
	"database/sql"
	"testing"

	"github.com/Markcus0526/carsaleman/internal/query"
)

// seedStock builds a vehicle, stores it in, and dispatches it, so both views
// have something to return.
func seedStock(t *testing.T, db *sql.DB) (onRoadID int64) {
	t.Helper()
	ctx := context.Background()
	for _, tbl := range []string{"tbl_storeout", "tbl_storechange", "tbl_storein", "tbl_onroad", "tbl_cartype"} {
		if _, err := db.ExecContext(ctx, "DELETE FROM "+tbl); err != nil {
			t.Fatalf("clearing %s: %v", tbl, err)
		}
	}

	res, err := db.ExecContext(ctx, `
		INSERT INTO tbl_cartype
			(carseries, carcode, carname, eop, inprice, outprice, otherprice1,
			 otherprice2, otherprice3, otherprice4, propval, profitval,
			 outstoreprice, vinprefix, enginenoprefix, deleted)
		VALUES ('轿车系列','C1','测试车型','否',100000,120000,0,0,0,0,0,0,0,'LSGH','E',0)`)
	if err != nil {
		t.Fatalf("seed cartype: %v", err)
	}
	cartypeID, _ := res.LastInsertId()

	res, err = db.ExecContext(ctx, `
		INSERT INTO tbl_onroad (billno, billdate, vin, engineno, cartypeid, cartype,
		                        carname, colorname, inflag, inkind)
		VALUES ('B001','2026-07-01','LSGH000001','E001',?,'C1','轿车 甲','红色',1,0)`, cartypeID)
	if err != nil {
		t.Fatalf("seed onroad: %v", err)
	}
	onRoadID, _ = res.LastInsertId()

	if _, err := db.ExecContext(ctx, `
		INSERT INTO tbl_storein
			(batchno, storeplace, onroadid, indate, inprice, passno, companyno,
			 inpath, intype, incarpricekind, factoryoutdate, propval, profitprop,
			 profitval, specprofitval, profitstate, outstoreprice, settlementname,
			 handlername, outflag, changedate)
		VALUES ('R20260801120000','一号库',?,'2026-08-01',123456.78,'P1',1,
		        '厂家直发','现车','标准','2026-07-20',0,0,0,0,'',0,'批复甲','经手乙',0,'2026-08-01')`,
		onRoadID); err != nil {
		t.Fatalf("seed storein: %v", err)
	}

	if _, err := db.ExecContext(ctx, `
		INSERT INTO tbl_storeout
			(batchno, onroadid, salecompany, outbillno, salekind, settlementname,
			 handlername, saleplace, incarkind, outdate, customername,
			 customerjobkind, saleregion, carno, votecost, outprice)
		VALUES ('R20260801120000',?,'某公司','O1','零售','批复甲','经手乙','沈阳',
		        '现车','2026-08-05','客户丙','个体','辽宁','辽A12345',100,150000.00)`,
		onRoadID); err != nil {
		t.Fatalf("seed storeout: %v", err)
	}
	return onRoadID
}

// The view must carry the vehicle's own columns through the join, or the stock
// list shows batch numbers with no car attached to them.
func TestStoreInViewJoinsVehicleAndSeries(t *testing.T) {
	db := testDB(t)
	seedStock(t, db)
	repo := NewStoreInRepo(db)

	rows, truncated, err := repo.List(context.Background(), query.Filter{})
	if err != nil {
		t.Fatalf("list: %v", err)
	}
	if truncated {
		t.Error("one row should not truncate")
	}
	if len(rows) != 1 {
		t.Fatalf("got %d rows, want 1", len(rows))
	}
	r := rows[0]
	for _, c := range []struct{ name, got, want string }{
		{"vin", r.VIN, "LSGH000001"},
		{"carname", r.CarName, "轿车 甲"},
		{"colorname", r.ColorName, "红色"},
		{"carseries", r.CarSeries, "轿车系列"},
		{"storeplace", r.StorePlace, "一号库"},
	} {
		if c.got != c.want {
			t.Errorf("%s = %q, want %q", c.name, c.got, c.want)
		}
	}
	// Decimal stays exact. 123456.78 is not representable in binary floating
	// point, so any float round-trip shows here.
	if r.InPrice != "123456.78" {
		t.Errorf("inprice = %q, want 123456.78", r.InPrice)
	}
}

func TestStoreOutViewJoinsVehicleAndSeries(t *testing.T) {
	db := testDB(t)
	seedStock(t, db)
	repo := NewStoreOutRepo(db)

	rows, _, err := repo.List(context.Background(), query.Filter{})
	if err != nil {
		t.Fatalf("list: %v", err)
	}
	if len(rows) != 1 {
		t.Fatalf("got %d rows, want 1", len(rows))
	}
	r := rows[0]
	if r.VIN != "LSGH000001" || r.CarSeries != "轿车系列" {
		t.Errorf("vin/carseries = %q/%q, want LSGH000001/轿车系列", r.VIN, r.CarSeries)
	}
	if r.OutPrice != "150000.00" {
		t.Errorf("outprice = %q, want 150000.00", r.OutPrice)
	}
	if r.OutDate == nil || *r.OutDate != "2026-08-05" {
		t.Errorf("outdate = %v, want 2026-08-05", r.OutDate)
	}
}

// A stock row whose vehicle is missing must still appear. LEFT JOIN, same
// judgement as vw_onroad: an INNER original means this shows extra rows, which
// is visible; a LEFT original reproduced as INNER loses rows silently.
func TestStoreInViewKeepsOrphanedRows(t *testing.T) {
	db := testDB(t)
	seedStock(t, db)
	ctx := context.Background()

	if _, err := db.ExecContext(ctx, "SET FOREIGN_KEY_CHECKS = 0"); err != nil {
		t.Fatal(err)
	}
	defer db.ExecContext(ctx, "SET FOREIGN_KEY_CHECKS = 1")

	if _, err := db.ExecContext(ctx, `
		INSERT INTO tbl_storein
			(batchno, storeplace, onroadid, indate, inprice, passno, companyno,
			 inpath, intype, incarpricekind, factoryoutdate, propval, profitprop,
			 profitval, specprofitval, profitstate, outstoreprice, settlementname,
			 handlername, outflag, changedate)
		VALUES ('ORPHAN','二号库',999999,'2026-08-02',1,'P2',1,'厂家直发','现车',
		        '标准','2026-07-21',0,0,0,0,'',0,'甲','乙',0,'2026-08-02')`); err != nil {
		t.Fatal(err)
	}

	rows, _, err := NewStoreInRepo(db).List(ctx, query.Filter{})
	if err != nil {
		t.Fatalf("list: %v", err)
	}
	found := false
	for _, r := range rows {
		if r.BatchNo == "ORPHAN" {
			found = true
			if r.VIN != "" {
				t.Errorf("orphan vin = %q, want empty", r.VIN)
			}
		}
	}
	if !found {
		t.Error("an orphaned stock row vanished — silent row loss in a financial system")
	}
}

// The allowlist is the security boundary: a column name cannot be a bind
// parameter, so anything not mapped must be refused outright.
func TestStockFiltersRejectUnknownFields(t *testing.T) {
	db := testDB(t)
	seedStock(t, db)

	bad := query.Filter{Conditions: []query.Condition{
		{Field: "inprice; DROP TABLE tbl_storein", Op: query.OpEquals, Value: "1"},
	}}
	if _, _, err := NewStoreInRepo(db).List(context.Background(), bad); err == nil {
		t.Fatal("an unmapped filter field was accepted")
	}
	if _, _, err := NewStoreOutRepo(db).List(context.Background(), bad); err == nil {
		t.Fatal("an unmapped filter field was accepted")
	}
}

// outflag is the one lifecycle flag exposed to filtering, because "what is
// still on the lot" is the question this screen exists to answer.
func TestStoreInFilterByOutFlag(t *testing.T) {
	db := testDB(t)
	seedStock(t, db)
	ctx := context.Background()

	if _, err := db.ExecContext(ctx, `UPDATE tbl_storein SET outflag = 1`); err != nil {
		t.Fatal(err)
	}
	inStock := query.Filter{Conditions: []query.Condition{
		{Field: "outflag", Op: query.OpEquals, Value: "0"},
	}}
	rows, _, err := NewStoreInRepo(db).List(ctx, inStock)
	if err != nil {
		t.Fatalf("list: %v", err)
	}
	if len(rows) != 0 {
		t.Errorf("got %d in-stock rows after dispatching everything, want 0", len(rows))
	}
}
