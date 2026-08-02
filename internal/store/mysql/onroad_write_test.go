package mysql

import (
	"context"
	"database/sql"
	"errors"
	"testing"
)

// seedCarType returns a usable cartypeid for the write tests.
func seedCarType(t *testing.T, db *sql.DB) int64 {
	t.Helper()
	ctx := context.Background()
	for _, tbl := range []string{"tbl_onroad", "tbl_cartype"} {
		if _, err := db.ExecContext(ctx, "DELETE FROM "+tbl); err != nil {
			t.Fatalf("clearing %s: %v", tbl, err)
		}
	}
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
	id, _ := res.LastInsertId()
	return id
}

func sampleInput(cartypeID int64) OnRoadInput {
	price := "123456.78"
	return OnRoadInput{
		BillNo: "B100", BillDate: "2026-08-01", VIN: "LSGH900001", EngineNo: "E900",
		CarTypeID: cartypeID, CarType: "C1", CarName: "轿车 甲", ColorName: "红色",
		CarState: "在途", InPrice: &price,
	}
}

func TestOnRoadCreateAndGet(t *testing.T) {
	db := testDB(t)
	repo := NewOnRoadRepo(db)
	ctx := context.Background()
	cartypeID := seedCarType(t, db)

	uid, err := repo.Create(ctx, sampleInput(cartypeID))
	if err != nil {
		t.Fatalf("create: %v", err)
	}

	row, version, err := repo.Get(ctx, uid)
	if err != nil {
		t.Fatalf("get: %v", err)
	}
	// A fresh row starts at 1, per the DEFAULT in migration 0001. If this ever
	// reads 0 the update guard silently stops working, because the handler
	// rejects rowVersion <= 0.
	if version != 1 {
		t.Errorf("row_version = %d, want 1", version)
	}
	if row.CarName != "轿车 甲" {
		t.Errorf("carname = %q, want 轿车 甲", row.CarName)
	}
	// Decimal survives as an exact string. 123456.78 is not representable in
	// binary floating point, so a float round-trip would show here.
	if row.InPrice == nil || *row.InPrice != "123456.78" {
		t.Errorf("inprice = %v, want 123456.78", row.InPrice)
	}
	// Create owns the lifecycle flags; a caller cannot set them.
	if row.InFlag != 0 || row.InKind != 0 {
		t.Errorf("inflag/inkind = %d/%d, want 0/0", row.InFlag, row.InKind)
	}
}

func TestOnRoadUpdateBumpsVersion(t *testing.T) {
	db := testDB(t)
	repo := NewOnRoadRepo(db)
	ctx := context.Background()
	cartypeID := seedCarType(t, db)

	uid, err := repo.Create(ctx, sampleInput(cartypeID))
	if err != nil {
		t.Fatalf("create: %v", err)
	}

	in := sampleInput(cartypeID)
	in.CarName = "轿车 乙"
	if err := repo.Update(ctx, uid, 1, in); err != nil {
		t.Fatalf("update: %v", err)
	}

	row, version, err := repo.Get(ctx, uid)
	if err != nil {
		t.Fatalf("get: %v", err)
	}
	if version != 2 {
		t.Errorf("row_version = %d, want 2", version)
	}
	if row.CarName != "轿车 乙" {
		t.Errorf("carname = %q, want 轿车 乙", row.CarName)
	}
}

// TestOnRoadUpdateRejectsStaleVersion is the lost-update test.
//
// Two users open the same vehicle. The first saves; the second saves against
// the version they read. The second must be refused, and -- the part that
// matters -- the first user's value must still be there afterwards. Asserting
// only on the error would pass even if the write had gone through.
func TestOnRoadUpdateRejectsStaleVersion(t *testing.T) {
	db := testDB(t)
	repo := NewOnRoadRepo(db)
	ctx := context.Background()
	cartypeID := seedCarType(t, db)

	uid, err := repo.Create(ctx, sampleInput(cartypeID))
	if err != nil {
		t.Fatalf("create: %v", err)
	}

	first := sampleInput(cartypeID)
	first.CarName = "用户甲的修改"
	if err := repo.Update(ctx, uid, 1, first); err != nil {
		t.Fatalf("first update: %v", err)
	}

	second := sampleInput(cartypeID)
	second.CarName = "用户乙的修改"
	err = repo.Update(ctx, uid, 1, second) // still holding version 1
	if !errors.Is(err, ErrVersionConflict) {
		t.Fatalf("stale update: err = %v, want ErrVersionConflict", err)
	}

	row, version, err := repo.Get(ctx, uid)
	if err != nil {
		t.Fatalf("get: %v", err)
	}
	if row.CarName != "用户甲的修改" {
		t.Errorf("carname = %q — the stale write overwrote the newer row", row.CarName)
	}
	if version != 2 {
		t.Errorf("row_version = %d, want 2 (the refused write must not bump it)", version)
	}
}

// A deleted row and a stale version both produce zero affected rows. They must
// not collapse into one error: 404 and 409 tell the client to do different
// things.
func TestOnRoadUpdateMissingRowIsNotFoundNotConflict(t *testing.T) {
	db := testDB(t)
	repo := NewOnRoadRepo(db)
	ctx := context.Background()
	cartypeID := seedCarType(t, db)

	err := repo.Update(ctx, 999999, 1, sampleInput(cartypeID))
	if !errors.Is(err, ErrNotFound) {
		t.Fatalf("err = %v, want ErrNotFound", err)
	}
}

func TestOnRoadGetMissingRow(t *testing.T) {
	db := testDB(t)
	repo := NewOnRoadRepo(db)
	seedCarType(t, db)

	_, _, err := repo.Get(context.Background(), 999999)
	if !errors.Is(err, ErrNotFound) {
		t.Fatalf("err = %v, want ErrNotFound", err)
	}
}

// NULL inprice must survive the round trip as NULL, not as "0.00". The
// difference is "no cost price recorded" versus "this vehicle was free", and
// the second one lands in the finance totals.
func TestOnRoadCreateNullPrice(t *testing.T) {
	db := testDB(t)
	repo := NewOnRoadRepo(db)
	ctx := context.Background()
	cartypeID := seedCarType(t, db)

	in := sampleInput(cartypeID)
	in.InPrice = nil
	uid, err := repo.Create(ctx, in)
	if err != nil {
		t.Fatalf("create: %v", err)
	}
	row, _, err := repo.Get(ctx, uid)
	if err != nil {
		t.Fatalf("get: %v", err)
	}
	if row.InPrice != nil {
		t.Errorf("inprice = %q, want NULL", *row.InPrice)
	}
}
