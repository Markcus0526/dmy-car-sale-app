package movement

import (
	"context"
	"errors"
	"testing"
)

// storeInFixture seeds a vehicle and stores it in, returning its on-road id.
func storeInFixture(t *testing.T, svc *Service, uid int64) {
	t.Helper()
	if _, err := svc.StoreIn(context.Background(), sampleStoreIn(uid)); err != nil {
		t.Fatalf("fixture store-in: %v", err)
	}
}

func TestTransferMovesStockRecordAndHistoryTogether(t *testing.T) {
	db := testDB(t)
	uid := seedVehicle(t, db)
	svc := newTestService(t, db, nil)
	storeInFixture(t, svc, uid)

	if err := svc.Transfer(context.Background(), TransferInput{
		OnRoadID: uid, StorePlace: "二号库", ActionPay: "500.00",
		Settlement: "批复甲", Handler: "经手乙", Remark: "转库",
	}); err != nil {
		t.Fatalf("transfer: %v", err)
	}

	// The stock record must move too. The legacy screens recorded the movement
	// and left tbl_storein.storeplace pointing at the old bay on some paths --
	// a car the history says has moved and the stock list says has not.
	var place string
	if err := db.QueryRow(`SELECT storeplace FROM tbl_storein WHERE onroadid = ?`, uid).Scan(&place); err != nil {
		t.Fatal(err)
	}
	if place != "二号库" {
		t.Errorf("tbl_storein.storeplace = %q, want 二号库", place)
	}
	if got := count(t, db, "tbl_storechange"); got != 2 {
		t.Errorf("%d movement rows, want 2 (store-in + transfer)", got)
	}
}

// A transfer carries the store-in's batchno rather than minting a new one:
// the movement belongs to the same intake, and a fresh number would break the
// thread through the history.
func TestTransferKeepsTheOriginalBatchNo(t *testing.T) {
	db := testDB(t)
	uid := seedVehicle(t, db)
	svc := newTestService(t, db, fixedClock("2026-08-02 10:00:00"))
	storeInFixture(t, svc, uid)

	if err := svc.Transfer(context.Background(), TransferInput{
		OnRoadID: uid, StorePlace: "二号库", Handler: "经手乙",
	}); err != nil {
		t.Fatal(err)
	}

	rows, err := db.Query(`SELECT batchno FROM tbl_storechange ORDER BY uid`)
	if err != nil {
		t.Fatal(err)
	}
	defer rows.Close()
	var seen []string
	for rows.Next() {
		var b string
		if err := rows.Scan(&b); err != nil {
			t.Fatal(err)
		}
		seen = append(seen, b)
	}
	if len(seen) != 2 || seen[0] != seen[1] {
		t.Errorf("batchnos = %v, want both movements on the same batch", seen)
	}
}

func TestTransferRequiresAStoredVehicle(t *testing.T) {
	db := testDB(t)
	uid := seedVehicle(t, db)
	svc := newTestService(t, db, nil)

	err := svc.Transfer(context.Background(), TransferInput{OnRoadID: uid, StorePlace: "二号库"})
	if !errors.Is(err, ErrNotStoredIn) {
		t.Fatalf("err = %v, want ErrNotStoredIn", err)
	}
}

func TestStoreOutDispatchesOnce(t *testing.T) {
	db := testDB(t)
	uid := seedVehicle(t, db)
	svc := newTestService(t, db, nil)
	storeInFixture(t, svc, uid)
	ctx := context.Background()

	out := StoreOutInput{
		OnRoadID: uid, OutBillNo: "O1", SaleCompany: "某公司", SaleKind: "零售",
		Settlement: "批复甲", Handler: "经手乙", SalePlace: "沈阳", InCarKind: "现车",
		OutDate: "2026-08-05", CustomerName: "客户丙", CustomerJob: "个体",
		SaleRegion: "辽宁", CarNo: "辽A12345", VoteCost: "100.00", OutPrice: "150000.00",
	}
	if _, err := svc.StoreOut(ctx, out); err != nil {
		t.Fatalf("store-out: %v", err)
	}

	var outflag int
	if err := db.QueryRow(`SELECT outflag FROM tbl_storein WHERE onroadid = ?`, uid).Scan(&outflag); err != nil {
		t.Fatal(err)
	}
	if outflag != 1 {
		t.Errorf("outflag = %d, want 1", outflag)
	}
	if got := count(t, db, "tbl_storeout"); got != 1 {
		t.Errorf("%d tbl_storeout rows, want 1", got)
	}
	if got := count(t, db, "tbl_storechange"); got != 2 {
		t.Errorf("%d movement rows, want 2 (store-in + store-out)", got)
	}

	// Second dispatch refused, and nothing extra written.
	if _, err := svc.StoreOut(ctx, out); !errors.Is(err, ErrAlreadyStoredOut) {
		t.Fatalf("second store-out: err = %v, want ErrAlreadyStoredOut", err)
	}
	if got := count(t, db, "tbl_storeout"); got != 1 {
		t.Errorf("%d tbl_storeout rows after a rejected double dispatch, want 1", got)
	}
}

func TestStoreOutRequiresAStoredVehicle(t *testing.T) {
	db := testDB(t)
	uid := seedVehicle(t, db)
	svc := newTestService(t, db, nil)

	_, err := svc.StoreOut(context.Background(), StoreOutInput{OnRoadID: uid, OutDate: "2026-08-05"})
	if !errors.Is(err, ErrNotStoredIn) {
		t.Fatalf("err = %v, want ErrNotStoredIn", err)
	}
}

// A dispatched vehicle has left the site; recording a transfer for it would put
// the same car in two places.
func TestTransferAfterDispatchIsRejected(t *testing.T) {
	db := testDB(t)
	uid := seedVehicle(t, db)
	svc := newTestService(t, db, nil)
	ctx := context.Background()
	storeInFixture(t, svc, uid)

	if _, err := svc.StoreOut(ctx, StoreOutInput{
		OnRoadID: uid, OutBillNo: "O1", SaleCompany: "某公司", SaleKind: "零售",
		Settlement: "甲", Handler: "乙", SalePlace: "沈阳", InCarKind: "现车",
		OutDate: "2026-08-05", CustomerName: "丙", CustomerJob: "个体",
		SaleRegion: "辽宁", CarNo: "辽A12345", OutPrice: "150000.00",
	}); err != nil {
		t.Fatal(err)
	}

	err := svc.Transfer(ctx, TransferInput{OnRoadID: uid, StorePlace: "三号库"})
	if !errors.Is(err, ErrAlreadyStoredOut) {
		t.Fatalf("err = %v, want ErrAlreadyStoredOut", err)
	}
}

// The full lifecycle, in order. This is what FrmActionHis shows -- slice 10 --
// and it reads tbl_storechange, not tbl_log.
func TestHistoryReturnsTheWholeLifecycleOldestFirst(t *testing.T) {
	db := testDB(t)
	uid := seedVehicle(t, db)
	svc := newTestService(t, db, nil)
	ctx := context.Background()

	storeInFixture(t, svc, uid)
	if err := svc.Transfer(ctx, TransferInput{OnRoadID: uid, StorePlace: "二号库", Handler: "乙"}); err != nil {
		t.Fatal(err)
	}
	if _, err := svc.StoreOut(ctx, StoreOutInput{
		OnRoadID: uid, OutBillNo: "O1", SaleCompany: "某公司", SaleKind: "零售",
		Settlement: "甲", Handler: "乙", SalePlace: "沈阳", InCarKind: "现车",
		OutDate: "2026-08-05", CustomerName: "丙", CustomerJob: "个体",
		SaleRegion: "辽宁", CarNo: "辽A12345", OutPrice: "150000.00",
	}); err != nil {
		t.Fatal(err)
	}

	history, err := svc.History(ctx, uid)
	if err != nil {
		t.Fatalf("history: %v", err)
	}
	want := []string{ActionStoreIn, ActionStoreChange, ActionStoreOut}
	if len(history) != len(want) {
		t.Fatalf("%d movements, want %d: %+v", len(history), len(want), history)
	}
	for i, kind := range want {
		if history[i].ActionKind != kind {
			t.Errorf("movement %d = %q, want %q", i, history[i].ActionKind, kind)
		}
	}
	// The transfer's destination must be on the transfer row, not the store-in's.
	if history[1].StorePlace != "二号库" {
		t.Errorf("transfer storeplace = %q, want 二号库", history[1].StorePlace)
	}
}

func TestHistoryOfAnUnknownVehicleIsEmptyNotNil(t *testing.T) {
	db := testDB(t)
	seedVehicle(t, db)
	svc := newTestService(t, db, nil)

	h, err := svc.History(context.Background(), 999999)
	if err != nil {
		t.Fatal(err)
	}
	if h == nil {
		t.Error("history must be [] not nil, so the client never branches on null")
	}
	if len(h) != 0 {
		t.Errorf("got %d movements, want 0", len(h))
	}
}
