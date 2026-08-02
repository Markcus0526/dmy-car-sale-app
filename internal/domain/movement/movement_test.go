package movement

import (
	"context"
	"database/sql"
	"errors"
	"os"
	"strings"
	"sync"
	"testing"
	"time"

	_ "github.com/go-sql-driver/mysql"
)

// These tests need a real MySQL: the whole package is about transactions, row
// guards and locking, none of which a fake reproduces. A fake here would test
// that the code calls the methods it calls, which is worth nothing.
func testDB(t *testing.T) *sql.DB {
	t.Helper()
	dsn := os.Getenv("CARSALEMAN_TEST_DSN")
	if dsn == "" {
		t.Skip("CARSALEMAN_TEST_DSN not set; skipping movement integration tests")
	}
	db, err := sql.Open("mysql", dsn+"?parseTime=true&charset=utf8mb4&loc=Asia%2FShanghai")
	if err != nil {
		t.Fatalf("opening test database: %v", err)
	}
	t.Cleanup(func() { db.Close() })
	if err := db.Ping(); err != nil {
		t.Fatalf("pinging test database: %v", err)
	}
	return db
}

var shanghai = mustLoad("Asia/Shanghai")

func mustLoad(name string) *time.Location {
	loc, err := time.LoadLocation(name)
	if err != nil {
		panic(err)
	}
	return loc
}

// fixedClock makes batch numbers deterministic.
func fixedClock(s string) Clock {
	ts, err := time.ParseInLocation("2006-01-02 15:04:05", s, shanghai)
	if err != nil {
		panic(err)
	}
	return func() time.Time { return ts }
}

func newTestService(t *testing.T, db *sql.DB, clock Clock) *Service {
	t.Helper()
	if clock == nil {
		clock = time.Now
	}
	return New(db, clock, shanghai)
}

// seedVehicle clears the movement tables and inserts one on-road vehicle.
func seedVehicle(t *testing.T, db *sql.DB) int64 {
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
		INSERT INTO tbl_onroad (billno, billdate, vin, engineno, cartypeid, cartype, inflag, inkind)
		VALUES ('B001','2026-07-01','LSGH000001','E001',?,'C1',0,0)`, cartypeID)
	if err != nil {
		t.Fatalf("seed onroad: %v", err)
	}
	uid, _ := res.LastInsertId()
	return uid
}

func sampleStoreIn(onRoadID int64) StoreInInput {
	return StoreInInput{
		OnRoadID: onRoadID, StorePlace: "一号库", InDate: "2026-08-01",
		InPrice: "123456.78", PassNo: "P1", CompanyNo: 1, InPath: "厂家直发",
		InType: "现车", PriceKind: "标准", FactoryOut: "2026-07-20",
		Settlement: "批复甲", Handler: "经手乙", Remark: "备注",
	}
}

func inflag(t *testing.T, db *sql.DB, uid int64) int {
	t.Helper()
	var f int
	if err := db.QueryRow(`SELECT inflag FROM tbl_onroad WHERE uid = ?`, uid).Scan(&f); err != nil {
		t.Fatalf("reading inflag: %v", err)
	}
	return f
}

func count(t *testing.T, db *sql.DB, table string) int {
	t.Helper()
	var n int
	if err := db.QueryRow("SELECT COUNT(*) FROM " + table).Scan(&n); err != nil {
		t.Fatalf("counting %s: %v", table, err)
	}
	return n
}

// ---------------------------------------------------------------------------

func TestStoreInWritesAllThreeChangesTogether(t *testing.T) {
	db := testDB(t)
	uid := seedVehicle(t, db)
	svc := newTestService(t, db, nil)

	if _, err := svc.StoreIn(context.Background(), sampleStoreIn(uid)); err != nil {
		t.Fatalf("store-in: %v", err)
	}

	if got := inflag(t, db, uid); got != 1 {
		t.Errorf("inflag = %d, want 1", got)
	}
	if got := count(t, db, "tbl_storein"); got != 1 {
		t.Errorf("%d tbl_storein rows, want 1", got)
	}
	if got := count(t, db, "tbl_storechange"); got != 1 {
		t.Errorf("%d tbl_storechange rows, want 1", got)
	}

	var kind, place string
	if err := db.QueryRow(
		`SELECT actionkind, storeplace FROM tbl_storechange`).Scan(&kind, &place); err != nil {
		t.Fatal(err)
	}
	if kind != ActionStoreIn {
		t.Errorf("actionkind = %q, want %q", kind, ActionStoreIn)
	}
	if place != "一号库" {
		t.Errorf("storeplace = %q, want 一号库", place)
	}
}

// The point of the guard. In the legacy code a double submit wrote a SECOND
// tbl_storein row and the vehicle was in stock twice.
func TestStoreInTwiceIsRejected(t *testing.T) {
	db := testDB(t)
	uid := seedVehicle(t, db)
	svc := newTestService(t, db, nil)
	ctx := context.Background()

	if _, err := svc.StoreIn(ctx, sampleStoreIn(uid)); err != nil {
		t.Fatalf("first store-in: %v", err)
	}
	if _, err := svc.StoreIn(ctx, sampleStoreIn(uid)); !errors.Is(err, ErrAlreadyStoredIn) {
		t.Fatalf("second store-in: err = %v, want ErrAlreadyStoredIn", err)
	}

	if got := count(t, db, "tbl_storein"); got != 1 {
		t.Errorf("%d tbl_storein rows after a rejected double submit, want 1", got)
	}
	if got := count(t, db, "tbl_storechange"); got != 1 {
		t.Errorf("%d movement rows, want 1", got)
	}
}

// Concurrency, not just sequence: two clients pressing save at the same instant
// is the case the legacy code had no defence against at all.
func TestConcurrentStoreInStoresExactlyOnce(t *testing.T) {
	db := testDB(t)
	uid := seedVehicle(t, db)
	svc := newTestService(t, db, nil)

	const racers = 8
	var wg sync.WaitGroup
	errs := make([]error, racers)
	start := make(chan struct{})
	for i := 0; i < racers; i++ {
		wg.Add(1)
		go func(i int) {
			defer wg.Done()
			<-start
			_, errs[i] = svc.StoreIn(context.Background(), sampleStoreIn(uid))
		}(i)
	}
	close(start)
	wg.Wait()

	succeeded := 0
	for i, err := range errs {
		switch {
		case err == nil:
			succeeded++
		case errors.Is(err, ErrAlreadyStoredIn):
			// expected for the losers
		default:
			t.Errorf("racer %d: unexpected error %v", i, err)
		}
	}
	if succeeded != 1 {
		t.Errorf("%d racers succeeded, want exactly 1", succeeded)
	}
	if got := count(t, db, "tbl_storein"); got != 1 {
		t.Errorf("%d tbl_storein rows, want exactly 1", got)
	}
}

func TestStoreInMissingVehicle(t *testing.T) {
	db := testDB(t)
	seedVehicle(t, db)
	svc := newTestService(t, db, nil)

	if _, err := svc.StoreIn(context.Background(), sampleStoreIn(999999)); !errors.Is(err, ErrNotFound) {
		t.Fatalf("err = %v, want ErrNotFound", err)
	}
}

// A failure anywhere in the transition must leave NOTHING behind -- not the
// flag, not the storein row, not the movement record. This is the guarantee the
// original could not make, because it had no transaction at all.
func TestStoreInRollsBackEverythingOnFailure(t *testing.T) {
	db := testDB(t)
	uid := seedVehicle(t, db)
	svc := newTestService(t, db, nil)

	in := sampleStoreIn(uid)
	// storeplace is VARCHAR(50). 60 characters fails under strict mode, and it
	// fails AFTER the flag update has already succeeded inside the transaction
	// -- which is exactly the window the original had no protection for.
	in.StorePlace = strings.Repeat("库", 60)

	if _, err := svc.StoreIn(context.Background(), in); err == nil {
		t.Fatal("expected the over-long storeplace to fail the transition")
	}

	if got := inflag(t, db, uid); got != 0 {
		t.Errorf("inflag = %d after a failed store-in, want 0 — the flag update was not rolled back", got)
	}
	if got := count(t, db, "tbl_storein"); got != 0 {
		t.Errorf("%d tbl_storein rows survived a failed transition", got)
	}
	if got := count(t, db, "tbl_storechange"); got != 0 {
		t.Errorf("%d movement rows survived a failed transition", got)
	}
}

// HH, not hh. .NET's "hh" is 12-hour, so 09:00 and 21:00 produced the same
// batch number -- silently merging two deliveries twelve hours apart.
func TestBatchNoUses24HourClock(t *testing.T) {
	db := testDB(t)

	morning := New(db, fixedClock("2026-08-02 09:30:00"), shanghai).NewBatchNo()
	evening := New(db, fixedClock("2026-08-02 21:30:00"), shanghai).NewBatchNo()

	if morning == evening {
		t.Fatalf("09:30 and 21:30 both produced %q — this is the 12-hour bug", morning)
	}
	if want := "R20260802093000"; morning != want {
		t.Errorf("morning = %q, want %q", morning, want)
	}
	if want := "R20260802213000"; evening != want {
		t.Errorf("evening = %q, want %q", evening, want)
	}
}

// Batch numbers embed a wall-clock date staff read and quote. Generated in UTC,
// an evening delivery in Shanghai would carry the previous day's date.
func TestBatchNoUsesBusinessTimezone(t *testing.T) {
	db := testDB(t)
	// 2026-08-02 23:30 Shanghai is 15:30 UTC the same day; 00:30 Shanghai on
	// the 3rd is 16:30 UTC on the 2nd -- that is where UTC would go wrong.
	svc := New(db, fixedClock("2026-08-03 00:30:00"), shanghai)
	if got := svc.NewBatchNo(); got != "R20260803003000" {
		t.Errorf("batchno = %q, want R20260803003000 (Shanghai date, not UTC)", got)
	}
}

func TestChangeIDIsAllocatedServerSide(t *testing.T) {
	db := testDB(t)
	uid := seedVehicle(t, db)
	svc := newTestService(t, db, nil)
	ctx := context.Background()

	if _, err := svc.StoreIn(ctx, sampleStoreIn(uid)); err != nil {
		t.Fatal(err)
	}
	if err := svc.Transfer(ctx, TransferInput{
		OnRoadID: uid, StorePlace: "二号库", Handler: "经手乙",
	}); err != nil {
		t.Fatalf("transfer: %v", err)
	}

	rows, err := db.Query(`SELECT changeid FROM tbl_storechange ORDER BY uid`)
	if err != nil {
		t.Fatal(err)
	}
	defer rows.Close()
	var ids []int64
	for rows.Next() {
		var id int64
		if err := rows.Scan(&id); err != nil {
			t.Fatal(err)
		}
		ids = append(ids, id)
	}
	// 1 then 2. The legacy code used the CLIENT's cached row count, which
	// starts at 0, collides between clients, and moves backwards on delete.
	if len(ids) != 2 || ids[0] != 1 || ids[1] != 2 {
		t.Errorf("changeids = %v, want [1 2]", ids)
	}
}
