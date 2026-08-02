package mysql

import (
	"context"
	"errors"
	"testing"

	"github.com/Markcus0526/carsaleman/internal/query"
)

// vw_speccar carries the price breakdown, unlike the stock list: this screen
// IS the margin view for key-account sales. NULL money must stay NULL — "not
// recorded" and "zero" are different facts, and only one belongs in a total.
func TestSpecCarKeepsNullMoneyDistinctFromZero(t *testing.T) {
	db := testDB(t)
	onRoadID := seedStock(t, db)
	ctx := context.Background()

	// The seeded dispatch leaves profitval, specprofitval and pricediff NULL.
	if _, err := db.ExecContext(ctx,
		`UPDATE tbl_storeout SET salekind = ?, carspeckind = '工程车', profitval = NULL,
		 specprofitval = 0.00 WHERE onroadid = ?`, SpecialSaleKind, onRoadID); err != nil {
		t.Fatal(err)
	}

	rows, _, err := NewSpecCarRepo(db).List(ctx, query.Filter{})
	if err != nil {
		t.Fatalf("list: %v", err)
	}
	if len(rows) != 1 {
		t.Fatalf("got %d rows, want 1", len(rows))
	}
	r := rows[0]
	if r.ProfitVal != nil {
		t.Errorf("profitval = %q, want nil — a NULL must not become 0.00", *r.ProfitVal)
	}
	if r.SpecProfit == nil || *r.SpecProfit != "0.00" {
		t.Errorf("specprofitval = %v, want the recorded 0.00", r.SpecProfit)
	}
	if r.VIN != "LSGH000001" || r.CarName != "轿车 甲" {
		t.Errorf("vehicle columns did not come through the join: %+v", r)
	}
}

// The view is deliberately unfiltered (migration 0007); the salekind predicate
// is an ordinary, visible filter condition. This asserts it actually selects.
func TestSpecCarSaleKindIsAnOrdinaryFilter(t *testing.T) {
	db := testDB(t)
	onRoadID := seedStock(t, db)
	ctx := context.Background()

	if _, err := db.ExecContext(ctx,
		`UPDATE tbl_storeout SET salekind = '零售' WHERE onroadid = ?`, onRoadID); err != nil {
		t.Fatal(err)
	}

	repo := NewSpecCarRepo(db)
	all, _, err := repo.List(ctx, query.Filter{})
	if err != nil {
		t.Fatal(err)
	}
	if len(all) != 1 {
		t.Fatalf("unfiltered view returned %d rows, want 1 — the view must not filter", len(all))
	}

	special, _, err := repo.List(ctx, query.Filter{Conditions: []query.Condition{
		{Field: "salekind", Op: query.OpEquals, Value: SpecialSaleKind},
	}})
	if err != nil {
		t.Fatal(err)
	}
	if len(special) != 0 {
		t.Errorf("a 零售 sale matched the %s filter", SpecialSaleKind)
	}
}

func TestSpecCarRejectsUnmappedFilterField(t *testing.T) {
	db := testDB(t)
	seedStock(t, db)

	bad := query.Filter{Conditions: []query.Condition{
		{Field: "outprice; DROP TABLE tbl_storeout", Op: query.OpEquals, Value: "1"},
	}}
	if _, _, err := NewSpecCarRepo(db).List(context.Background(), bad); err == nil {
		t.Fatal("an unmapped filter field was accepted")
	}
}

// ---------------------------------------------------------------------------

func TestFitCrudRoundTrip(t *testing.T) {
	db := testDB(t)
	ctx := context.Background()
	if _, err := db.ExecContext(ctx, "DELETE FROM tbl_fit"); err != nil {
		t.Fatal(err)
	}
	repo := NewFitRepo(db)

	date, price := "2026-08-01", "1500.50"
	uid, err := repo.Create(ctx, "客户甲", "销售乙", &date, &price, "全车贴膜")
	if err != nil {
		t.Fatalf("create: %v", err)
	}

	rows, _, err := repo.List(ctx, query.Filter{})
	if err != nil {
		t.Fatal(err)
	}
	if len(rows) != 1 {
		t.Fatalf("got %d rows, want 1", len(rows))
	}
	r := rows[0]
	if r.Consumer != "客户甲" || r.Remark != "全车贴膜" {
		t.Errorf("row = %+v", r)
	}
	// 1500.50 is not representable in binary floating point.
	if r.FitPrice == nil || *r.FitPrice != "1500.50" {
		t.Errorf("fitprice = %v, want 1500.50", r.FitPrice)
	}

	if err := repo.Update(ctx, uid, r.RowVersion, "客户甲", "销售丙", &date, &price, "改装"); err != nil {
		t.Fatalf("update: %v", err)
	}
	// Stale version refused, and the newer value survives.
	if err := repo.Update(ctx, uid, r.RowVersion, "客户甲", "销售丁", &date, &price, "覆盖"); !errors.Is(err, ErrVersionConflict) {
		t.Fatalf("stale update: err = %v, want ErrVersionConflict", err)
	}
	rows, _, _ = repo.List(ctx, query.Filter{})
	if rows[0].Seller != "销售丙" {
		t.Errorf("seller = %q — the stale write overwrote the newer row", rows[0].Seller)
	}

	if err := repo.Delete(ctx, uid); err != nil {
		t.Fatalf("delete: %v", err)
	}
	if err := repo.Delete(ctx, uid); !errors.Is(err, ErrNotFound) {
		t.Errorf("second delete: err = %v, want ErrNotFound", err)
	}
}

// A fit-out with no price recorded must read back as NULL, not as 0.00 — the
// column is nullable and the distinction is the difference between "we have
// not billed this yet" and "it was free".
func TestFitNullPriceAndDateSurvive(t *testing.T) {
	db := testDB(t)
	ctx := context.Background()
	if _, err := db.ExecContext(ctx, "DELETE FROM tbl_fit"); err != nil {
		t.Fatal(err)
	}
	repo := NewFitRepo(db)

	if _, err := repo.Create(ctx, "客户乙", "", nil, nil, ""); err != nil {
		t.Fatalf("create: %v", err)
	}
	rows, _, err := repo.List(ctx, query.Filter{})
	if err != nil {
		t.Fatal(err)
	}
	if len(rows) != 1 {
		t.Fatalf("got %d rows, want 1", len(rows))
	}
	if rows[0].FitPrice != nil {
		t.Errorf("fitprice = %q, want NULL", *rows[0].FitPrice)
	}
	if rows[0].FitDate != nil {
		t.Errorf("fitdate = %q, want NULL", *rows[0].FitDate)
	}
}
