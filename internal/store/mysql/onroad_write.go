package mysql

import (
	"context"
	"database/sql"
	"errors"
	"fmt"
)

// ErrVersionConflict means the row changed since the client read it.
//
// This is the replacement for the legacy whole-row comparison (plan 2.5). The
// generated adapters compared every original column plus @IsNull_* flags on
// each UPDATE; a naive `WHERE uid = ?` would turn those violations into silent
// overwrites (11.4). The handler turns this into 409.
var ErrVersionConflict = errors.New("mysql: row was modified by someone else")

// ErrNotFound distinguishes "no such row" from "wrong version" -- 404 and 409
// mean different things to a client.
var ErrNotFound = errors.New("mysql: not found")

// OnRoadInput is the writable subset of an on-road vehicle.
//
// Deliberately not the full row: uid, row_version, and the inflag/inkind
// lifecycle flags are owned by the system. inflag in particular is the
// store-in state machine (§6.2) and must only move through the movement
// service, never through a general edit form.
type OnRoadInput struct {
	BillNo        string  `json:"billno"`
	BillDate      string  `json:"billdate"`
	VIN           string  `json:"vin"`
	EngineNo      string  `json:"engineno"`
	CarTypeID     int64   `json:"cartypeid"`
	CarType       string  `json:"cartype"`
	CarName       string  `json:"carname"`
	ColorCode     string  `json:"colorcode"`
	ColorName     string  `json:"colorname"`
	Subsets       string  `json:"subsets"`
	InsideSetCode string  `json:"insidesetcode"`
	InsideSetName string  `json:"insidesetname"`
	CarState      string  `json:"carstate"`
	Property      string  `json:"property"`
	InPrice       *string `json:"inprice"`
}

// Get returns one vehicle, or ErrNotFound.
func (r *OnRoadRepo) Get(ctx context.Context, uid int64) (*OnRoad, int, error) {
	const q = `
		SELECT uid, billno, billdate, vin, engineno, cartypeid, cartype,
		       COALESCE(carname,''), COALESCE(colorcode,''), COALESCE(colorname,''),
		       COALESCE(subsets,''), COALESCE(insidesetcode,''), COALESCE(insidesetname,''),
		       COALESCE(carstate,''), COALESCE(property,''), inprice, inflag, inkind,
		       row_version
		FROM tbl_onroad WHERE uid = ?`

	var (
		o        OnRoad
		billDate sql.NullTime
		inPrice  sql.NullString
		version  int
	)
	err := r.db.QueryRowContext(ctx, q, uid).Scan(
		&o.UID, &o.BillNo, &billDate, &o.VIN, &o.EngineNo, &o.CarTypeID, &o.CarType,
		&o.CarName, &o.ColorCode, &o.ColorName, &o.Subsets, &o.InsideSetCode,
		&o.InsideSetName, &o.CarState, &o.Property, &inPrice, &o.InFlag, &o.InKind,
		&version,
	)
	if errors.Is(err, sql.ErrNoRows) {
		return nil, 0, ErrNotFound
	}
	if err != nil {
		return nil, 0, fmt.Errorf("mysql: get onroad: %w", err)
	}
	if billDate.Valid {
		s := billDate.Time.Format("2006-01-02")
		o.BillDate = &s
	}
	if inPrice.Valid {
		o.InPrice = &inPrice.String
	}
	return &o, version, nil
}

// Create inserts a vehicle in the initial on-road state.
//
// inflag and inkind are set by this method, not supplied by the caller: a new
// on-road vehicle is by definition not yet stored in (§6.2), and letting a
// form set that flag would let a client skip the movement transaction.
func (r *OnRoadRepo) Create(ctx context.Context, in OnRoadInput) (int64, error) {
	const q = `
		INSERT INTO tbl_onroad
			(billno, billdate, vin, engineno, cartypeid, cartype, carname,
			 colorcode, colorname, subsets, insidesetcode, insidesetname,
			 carstate, property, inprice, inflag, inkind)
		VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,0,0)`

	res, err := r.db.ExecContext(ctx, q,
		in.BillNo, in.BillDate, in.VIN, in.EngineNo, in.CarTypeID, in.CarType,
		in.CarName, in.ColorCode, in.ColorName, in.Subsets, in.InsideSetCode,
		in.InsideSetName, in.CarState, in.Property, in.InPrice)
	if err != nil {
		return 0, fmt.Errorf("mysql: create onroad: %w", err)
	}
	return res.LastInsertId()
}

// Update applies an edit, guarded by row_version.
//
// The guard is in the WHERE clause rather than a read-then-compare, so the
// check and the write are one atomic statement. A read-then-compare has a race
// between them, which is precisely the lost update it is meant to prevent.
func (r *OnRoadRepo) Update(ctx context.Context, uid int64, version int, in OnRoadInput) error {
	const q = `
		UPDATE tbl_onroad SET
			billno=?, billdate=?, vin=?, engineno=?, cartypeid=?, cartype=?,
			carname=?, colorcode=?, colorname=?, subsets=?, insidesetcode=?,
			insidesetname=?, carstate=?, property=?, inprice=?,
			row_version = row_version + 1
		WHERE uid = ? AND row_version = ?`

	res, err := r.db.ExecContext(ctx, q,
		in.BillNo, in.BillDate, in.VIN, in.EngineNo, in.CarTypeID, in.CarType,
		in.CarName, in.ColorCode, in.ColorName, in.Subsets, in.InsideSetCode,
		in.InsideSetName, in.CarState, in.Property, in.InPrice,
		uid, version)
	if err != nil {
		return fmt.Errorf("mysql: update onroad: %w", err)
	}
	n, err := res.RowsAffected()
	if err != nil {
		return fmt.Errorf("mysql: update onroad: %w", err)
	}
	if n == 1 {
		return nil
	}

	// Zero rows means either the row is gone or the version moved. Tell them
	// apart: 404 and 409 lead a client to do different things.
	var exists bool
	if err := r.db.QueryRowContext(ctx,
		`SELECT EXISTS(SELECT 1 FROM tbl_onroad WHERE uid = ?)`, uid).Scan(&exists); err != nil {
		return fmt.Errorf("mysql: update onroad: %w", err)
	}
	if !exists {
		return ErrNotFound
	}
	return ErrVersionConflict
}
