package movement

import (
	"context"
	"database/sql"
	"errors"
	"fmt"
	"time"
)

// TransferInput moves a stored vehicle to a different storage location.
type TransferInput struct {
	OnRoadID   int64
	StorePlace string // the destination
	ActionPay  string // decimal string; transfer costs, "" means 0
	Settlement string
	Handler    string
	Repair     string
	Reserve    string
	Remark     string
}

// Transfer records a 车辆转库 and moves the store-in row's location.
//
// Both in one transaction. The legacy screens wrote the movement row and left
// tbl_storein.storeplace untouched in some paths, so the history said the car
// had moved while the stock record still pointed at the old bay -- which is a
// vehicle nobody can find.
//
// Guarded on outflag = 0: a dispatched vehicle has left, and recording a
// transfer for it would put a car in two places at once.
func (s *Service) Transfer(ctx context.Context, in TransferInput) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return fmt.Errorf("movement: transfer: %w", err)
	}
	defer tx.Rollback() //nolint:errcheck

	res, err := tx.ExecContext(ctx, `
		UPDATE tbl_storein SET storeplace = ?, changedate = ?, row_version = row_version + 1
		WHERE onroadid = ? AND outflag = 0`,
		in.StorePlace, s.now().In(s.loc), in.OnRoadID)
	if err != nil {
		return fmt.Errorf("movement: transfer update: %w", err)
	}
	if n, _ := res.RowsAffected(); n != 1 {
		return s.explainStoreInGuard(ctx, tx, in.OnRoadID)
	}

	// batchno carries over from the store-in record: a transfer belongs to the
	// same intake, and minting a fresh one would break the thread through the
	// history.
	var batchNo string
	if err := tx.QueryRowContext(ctx,
		`SELECT batchno FROM tbl_storein WHERE onroadid = ? AND outflag = 0`,
		in.OnRoadID).Scan(&batchNo); err != nil {
		return fmt.Errorf("movement: transfer batchno: %w", err)
	}

	if err := s.recordChange(ctx, tx, changeInput{
		BatchNo:    batchNo,
		OnRoadID:   in.OnRoadID,
		StorePlace: in.StorePlace,
		ActionKind: ActionStoreChange,
		ActionPay:  in.ActionPay,
		Settlement: in.Settlement,
		Handler:    in.Handler,
		Repair:     in.Repair,
		Reserve:    in.Reserve,
		Remark:     in.Remark,
	}); err != nil {
		return err
	}
	return tx.Commit()
}

// StoreOutInput dispatches a vehicle.
//
// Only the columns a dispatch actually needs. tbl_storeout has 37 of them, most
// of which the finance screens fill in afterwards; requiring all of them here
// would make the caller invent zeroes for fields it has no opinion about.
type StoreOutInput struct {
	OnRoadID     int64
	OutBillNo    string
	SaleCompany  string
	SaleKind     string
	Settlement   string
	Handler      string
	SalePlace    string
	InCarKind    string
	OutDate      string // yyyy-mm-dd
	CustomerName string
	CustomerJob  string
	SaleRegion   string
	CarNo        string
	VoteCost     string // decimal string
	OutPrice     string // decimal string
	Remark       string
}

// StoreOut dispatches a vehicle out of stock, atomically.
//
// The guard is on tbl_storein.outflag, not on tbl_onroad: a vehicle can only be
// dispatched from stock, and the store-in row is what says it is in stock.
func (s *Service) StoreOut(ctx context.Context, in StoreOutInput) (int64, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return 0, fmt.Errorf("movement: store-out: %w", err)
	}
	defer tx.Rollback() //nolint:errcheck

	var batchNo, storePlace string
	err = tx.QueryRowContext(ctx,
		`SELECT batchno, storeplace FROM tbl_storein WHERE onroadid = ? AND outflag = 0 FOR UPDATE`,
		in.OnRoadID).Scan(&batchNo, &storePlace)
	if errors.Is(err, sql.ErrNoRows) {
		return 0, s.explainStoreInGuard(ctx, tx, in.OnRoadID)
	}
	if err != nil {
		return 0, fmt.Errorf("movement: store-out read: %w", err)
	}

	res, err := tx.ExecContext(ctx, `
		UPDATE tbl_storein SET outflag = 1, row_version = row_version + 1
		WHERE onroadid = ? AND outflag = 0`, in.OnRoadID)
	if err != nil {
		return 0, fmt.Errorf("movement: store-out flag: %w", err)
	}
	if n, _ := res.RowsAffected(); n != 1 {
		// FOR UPDATE above held the row, so this should be unreachable. Kept
		// because "should be unreachable" and "is unreachable" differ, and the
		// cost of being wrong here is a double dispatch.
		return 0, ErrAlreadyStoredOut
	}

	res, err = tx.ExecContext(ctx, `
		INSERT INTO tbl_storeout
			(batchno, onroadid, salecompany, outbillno, salekind, settlementname,
			 handlername, saleplace, incarkind, outdate, customername,
			 customerjobkind, saleregion, carno, votecost, outprice, remark)
		VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`,
		batchNo, in.OnRoadID, in.SaleCompany, in.OutBillNo, in.SaleKind,
		in.Settlement, in.Handler, in.SalePlace, in.InCarKind, in.OutDate,
		in.CustomerName, in.CustomerJob, in.SaleRegion, in.CarNo,
		zeroIfEmpty(in.VoteCost), zeroIfEmpty(in.OutPrice), in.Remark)
	if err != nil {
		return 0, fmt.Errorf("movement: store-out insert: %w", err)
	}
	storeOutID, err := res.LastInsertId()
	if err != nil {
		return 0, fmt.Errorf("movement: store-out insert: %w", err)
	}

	if err := s.recordChange(ctx, tx, changeInput{
		BatchNo:    batchNo,
		OnRoadID:   in.OnRoadID,
		StorePlace: storePlace,
		ActionKind: ActionStoreOut,
		Settlement: in.Settlement,
		Handler:    in.Handler,
		Remark:     in.Remark,
	}); err != nil {
		return 0, err
	}

	if err := tx.Commit(); err != nil {
		return 0, fmt.Errorf("movement: store-out commit: %w", err)
	}
	return storeOutID, nil
}

// explainStoreInGuard distinguishes "never stored in" from "already dispatched".
func (s *Service) explainStoreInGuard(ctx context.Context, tx *sql.Tx, onRoadID int64) error {
	var outflag sql.NullInt64
	err := tx.QueryRowContext(ctx,
		`SELECT MAX(outflag) FROM tbl_storein WHERE onroadid = ?`, onRoadID).Scan(&outflag)
	if err != nil {
		return fmt.Errorf("movement: store-in guard: %w", err)
	}
	// A bare aggregate over zero rows returns one NULL row, not no rows.
	if !outflag.Valid {
		return ErrNotStoredIn
	}
	return ErrAlreadyStoredOut
}

func zeroIfEmpty(decimal string) string {
	if decimal == "" {
		return "0"
	}
	return decimal
}

// Change is one row of a vehicle's movement history.
type Change struct {
	UID        int64  `json:"uid"`
	ChangeID   int64  `json:"changeid"`
	BatchNo    string `json:"batchno"`
	OnRoadID   int64  `json:"onroadid"`
	StorePlace string `json:"storeplace"`
	// ActionKind is one of the four Action constants -- data, not display text.
	// The UI resolves it to a label; it is never translated in storage.
	ActionKind string `json:"actionkind"`
	ActionDate string `json:"actiondate"`
	ActionPay  string `json:"actionpay"`
	Settlement string `json:"settlementname"`
	Handler    string `json:"handlername"`
	Repair     string `json:"repairstate"`
	Reserve    string `json:"reservestate"`
	Remark     string `json:"remark"`
}

// History returns one vehicle's movements, oldest first.
//
// This is what FrmActionHis actually shows -- slice 10. It reads
// tbl_storechange, NOT tbl_log: the plan called FrmActionHis an audit-trail
// viewer over tbl_log until day 23, but that form renders a List<StoreChange>
// its caller passes in and never touches tbl_log.
//
// Oldest first, unlike the journal: a movement history is a sequence, and
// reading a car's life backwards is harder than reading it forwards.
func (s *Service) History(ctx context.Context, onRoadID int64) ([]Change, error) {
	const q = `
		SELECT uid, changeid, batchno, onroadid, storeplace, actionkind,
		       actiondate, actionpay, settlementname, handlername,
		       COALESCE(repairstate,''), COALESCE(reservestate,''), COALESCE(remark,'')
		FROM tbl_storechange
		WHERE onroadid = ?
		ORDER BY actiondate, uid`

	rows, err := s.db.QueryContext(ctx, q, onRoadID)
	if err != nil {
		return nil, fmt.Errorf("movement: history: %w", err)
	}
	defer rows.Close()

	changes := []Change{}
	for rows.Next() {
		var c Change
		var when time.Time
		if err := rows.Scan(&c.UID, &c.ChangeID, &c.BatchNo, &c.OnRoadID, &c.StorePlace,
			&c.ActionKind, &when, &c.ActionPay, &c.Settlement, &c.Handler,
			&c.Repair, &c.Reserve, &c.Remark); err != nil {
			return nil, fmt.Errorf("movement: history: %w", err)
		}
		c.ActionDate = when.Format("2006-01-02 15:04:05")
		changes = append(changes, c)
	}
	return changes, rows.Err()
}
