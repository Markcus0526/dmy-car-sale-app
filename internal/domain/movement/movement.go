// Package movement implements the vehicle lifecycle state machine (§6.2).
//
// The lifecycle spans three tables and two flags:
//
//	车辆采购 purchase  → tbl_onroad (inflag = 0)
//	车辆入库 store-in  → tbl_storein  + tbl_onroad.inflag  = 1
//	车辆转库 transfer  → tbl_storechange
//	车辆出库 store-out → tbl_storeout + tbl_storein.outflag = 1
//
// # WHY THIS PACKAGE EXISTS
//
// Today each transition is two or three independent adapter Update() calls with
// NO transaction (FrmStoreInAddMan.cs:318-395). A crash between them leaves a
// vehicle half-moved: stored in with no movement record, or flagged as stored
// while the tbl_storein row was never written.
//
// Every transition here is ONE MySQL transaction, and every flag update is
// guarded (`WHERE uid = ? AND inflag = 0`) so a double submit fails loudly
// instead of storing the same vehicle twice. Per §6.2 this is the one place the
// port deliberately does not reproduce the original's behaviour.
package movement

import (
	"context"
	"database/sql"
	"errors"
	"fmt"
	"time"
)

// Action values, exactly as stored in tbl_storechange.actionkind
// (CommonMisc.cs:438-443). Chinese because the DATA is Chinese: these are
// compared by value and written to the database. Never translate them; the UI
// resolves a display label separately.
const (
	ActionPurchase    = "车辆采购"
	ActionStoreIn     = "车辆入库"
	ActionStoreChange = "车辆转库"
	ActionStoreOut    = "车辆出库"
)

var (
	// ErrNotFound: no such vehicle, or no store-in record for it.
	ErrNotFound = errors.New("movement: not found")

	// ErrAlreadyStoredIn is what a double submit produces. The guarded UPDATE
	// affects zero rows, and that is reported rather than silently ignored --
	// the legacy code would happily write a second tbl_storein row.
	ErrAlreadyStoredIn = errors.New("movement: vehicle is already stored in")

	// ErrNotStoredIn: cannot transfer or dispatch a vehicle still on the road.
	ErrNotStoredIn = errors.New("movement: vehicle is not stored in")

	// ErrAlreadyStoredOut: the store-in record is already dispatched.
	ErrAlreadyStoredOut = errors.New("movement: vehicle is already dispatched")
)

// Clock is injected so batch numbers are testable. Production passes time.Now.
type Clock func() time.Time

type Service struct {
	db  *sql.DB
	now Clock
	loc *time.Location
}

// New builds the service. loc must be the business timezone -- Asia/Shanghai --
// because batch numbers embed a wall-clock timestamp that staff read and quote.
// Generating them in UTC would put yesterday's date on an evening delivery.
func New(db *sql.DB, now Clock, loc *time.Location) *Service {
	if now == nil {
		now = time.Now
	}
	return &Service{db: db, now: now, loc: loc}
}

// NewBatchNo generates a batch number.
//
// Reproduces the legacy shape "R" + yyyyMMddHHmmss (FrmStoreInAddMan.cs:425-437)
// with two fixes §6.6 calls for:
//
//   - HH, not hh. .NET's "hh" is 12-HOUR, so 09:00 and 21:00 produced the
//     identical string. That is not a formatting nit -- it silently merged two
//     deliveries twelve hours apart.
//   - Generated on the server, not per client, so two users cannot both mint
//     the same number in the same second.
//
// WHAT IS DELIBERATELY *NOT* DONE: §6.6 also recommends a UNIQUE index on
// batchno. That is not safe to apply blind. batchno is per-row (txtbatchno
// binds to the BindingSource's CURRENT row), the legacy generator has
// one-second resolution, and an operator clicking through ten vehicles in a
// batch can easily produce ten identical numbers -- so duplicates are likely to
// exist in the real data. Adding the constraint before the Phase 0 dump is
// examined would fail the migration on live rows. Same reasoning as the
// deliberately non-unique username index.
//
// TODO(phase0): count duplicate batchno values in the dump. If clean, add the
// UNIQUE index in a follow-up migration. If not, decide explicitly whether to
// de-duplicate or to leave batchno non-unique.
func (s *Service) NewBatchNo() string {
	return "R" + s.now().In(s.loc).Format("20060102150405")
}

// StoreInInput is one vehicle entering stock.
type StoreInInput struct {
	OnRoadID   int64
	BatchNo    string // generated if empty
	StorePlace string
	InDate     string // yyyy-mm-dd
	InPrice    string // decimal string, never a float
	PassNo     string
	CompanyNo  int
	InPath     string
	InType     string
	PriceKind  string
	FactoryOut string // yyyy-mm-dd
	Settlement string
	Handler    string
	Remark     string
}

// StoreIn moves a vehicle from on-road into stock, atomically.
//
// Three writes in one transaction:
//  1. tbl_onroad.inflag 0 -> 1, GUARDED. Zero rows affected means either the
//     vehicle does not exist or it is already stored in, and those are told
//     apart rather than collapsed.
//  2. tbl_storein insert.
//  3. tbl_storechange insert, actionkind = 车辆入库.
//
// The order matters: the guarded flag update runs FIRST, so a concurrent
// duplicate submit is rejected before either insert happens. Doing the inserts
// first and the guard last would leave orphan rows to clean up on rollback --
// which works, but only because the transaction is there. Failing early is
// cheaper and clearer.
func (s *Service) StoreIn(ctx context.Context, in StoreInInput) (int64, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return 0, fmt.Errorf("movement: store-in: %w", err)
	}
	defer tx.Rollback() //nolint:errcheck // no-op after Commit

	res, err := tx.ExecContext(ctx, `
		UPDATE tbl_onroad SET inflag = 1, row_version = row_version + 1
		WHERE uid = ? AND inflag = 0`, in.OnRoadID)
	if err != nil {
		return 0, fmt.Errorf("movement: store-in flag: %w", err)
	}
	if n, _ := res.RowsAffected(); n != 1 {
		return 0, s.explainOnRoadGuard(ctx, tx, in.OnRoadID)
	}

	batchNo := in.BatchNo
	if batchNo == "" {
		batchNo = s.NewBatchNo()
	}

	// Every NOT NULL column must be supplied: the inferred schema has 107 that
	// are NOT NULL with no DEFAULT, because CmsDB.xsd cannot express defaults.
	// The financial columns start at zero, exactly as MakeStoreinTable does
	// (FrmStoreInAddMan.cs:61-90) -- they are filled in later by the finance
	// screens, not at store-in time.
	res, err = tx.ExecContext(ctx, `
		INSERT INTO tbl_storein
			(batchno, storeplace, onroadid, indate, inprice, passno, companyno,
			 inpath, intype, incarpricekind, factoryoutdate, repairstate,
			 reservestate, propval, profitprop, profitval, specprofitval,
			 profitstate, outstoreprice, settlementname, handlername, remark,
			 outflag, changedate)
		VALUES (?,?,?,?,?,?,?,?,?,?,?,'','',0,0,0,0,'',0,?,?,?,0,?)`,
		batchNo, in.StorePlace, in.OnRoadID, in.InDate, in.InPrice, in.PassNo,
		in.CompanyNo, in.InPath, in.InType, in.PriceKind, in.FactoryOut,
		in.Settlement, in.Handler, in.Remark, in.InDate)
	if err != nil {
		return 0, fmt.Errorf("movement: store-in insert: %w", err)
	}
	storeInID, err := res.LastInsertId()
	if err != nil {
		return 0, fmt.Errorf("movement: store-in insert: %w", err)
	}

	if err := s.recordChange(ctx, tx, changeInput{
		BatchNo:    batchNo,
		OnRoadID:   in.OnRoadID,
		StorePlace: in.StorePlace,
		ActionKind: ActionStoreIn,
		Handler:    in.Handler,
		Remark:     in.Remark,
	}); err != nil {
		return 0, err
	}

	if err := tx.Commit(); err != nil {
		return 0, fmt.Errorf("movement: store-in commit: %w", err)
	}
	return storeInID, nil
}

// explainOnRoadGuard turns "zero rows affected" into a specific error.
//
// Worth the extra query: "already stored in" tells the user their colleague got
// there first, while "not found" means they are looking at a deleted record.
// Collapsing them into one message makes a routine race look like a bug.
func (s *Service) explainOnRoadGuard(ctx context.Context, tx *sql.Tx, onRoadID int64) error {
	var inflag int
	err := tx.QueryRowContext(ctx, `SELECT inflag FROM tbl_onroad WHERE uid = ?`, onRoadID).Scan(&inflag)
	if errors.Is(err, sql.ErrNoRows) {
		return ErrNotFound
	}
	if err != nil {
		return fmt.Errorf("movement: store-in guard: %w", err)
	}
	return ErrAlreadyStoredIn
}

type changeInput struct {
	BatchNo    string
	OnRoadID   int64
	StorePlace string
	ActionKind string
	ActionPay  string // decimal string; "" means 0
	Settlement string
	Handler    string
	Repair     string
	Reserve    string
	Remark     string
}

// recordChange appends one movement-history row.
//
// changeid is allocated as MAX(changeid)+1 WITHIN the transaction. The legacy
// code used `cmsDB.tbl_storechange.Rows.Count` -- the row count of the CLIENT's
// cached DataTable (FrmStoreInAddMan.cs:360 and five other sites). That is
// wrong three ways: two clients mint the same id, it moves backwards when rows
// are deleted, and it depends on what the client happened to have loaded.
//
// Nothing queries on changeid -- every read site just displays it
// (FrmActionHis) -- so a server-side sequence is a faithful replacement for
// what it was meant to be rather than what it was.
func (s *Service) recordChange(ctx context.Context, tx *sql.Tx, c changeInput) error {
	var next int64
	if err := tx.QueryRowContext(ctx,
		`SELECT COALESCE(MAX(changeid), 0) + 1 FROM tbl_storechange FOR UPDATE`).Scan(&next); err != nil {
		return fmt.Errorf("movement: allocate changeid: %w", err)
	}

	pay := c.ActionPay
	if pay == "" {
		pay = "0"
	}

	_, err := tx.ExecContext(ctx, `
		INSERT INTO tbl_storechange
			(changeid, batchno, onroadid, storeplace, actionkind, actiondate,
			 actionpay, settlementname, handlername, repairstate, reservestate, remark)
		VALUES (?,?,?,?,?,?,?,?,?,?,?,?)`,
		next, c.BatchNo, c.OnRoadID, c.StorePlace, c.ActionKind,
		s.now().In(s.loc), pay, c.Settlement, c.Handler, c.Repair, c.Reserve, c.Remark)
	if err != nil {
		return fmt.Errorf("movement: record change: %w", err)
	}
	return nil
}
