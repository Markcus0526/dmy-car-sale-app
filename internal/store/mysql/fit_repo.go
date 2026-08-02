package mysql

import (
	"context"
	"database/sql"
	"fmt"
	"strings"

	"github.com/Markcus0526/carsaleman/internal/query"
)

// 赠送装修 — complimentary fit-out (tbl_fit), FrmAppendRepair. Slice 8.
//
// A plain base table with no view and no lifecycle: consumer, seller, date,
// price, remark. Its only relationship to the rest of the system is by name,
// not by key — tbl_fit has no foreign key to a vehicle or a sale, so a fit-out
// is recorded against a customer name rather than against a car.
//
// That is worth stating rather than quietly "fixing": adding an onroadid here
// would be a schema change to a table the reports read, and the correct link
// (if any) is not recoverable from this repo.

// FitRecord is one row of tbl_fit.
type FitRecord struct {
	UID int64 `json:"uid"`
	// Consumer is NOT NULL; seller, date, price and remark are all nullable.
	Consumer   string  `json:"consumer"`
	Seller     string  `json:"seller"`
	FitDate    *string `json:"fitdate"`
	FitPrice   *string `json:"fitprice"`
	Remark     string  `json:"remark"`
	RowVersion int     `json:"rowVersion"`
}

var FitFilterFields = query.Fields{
	"consumer": {SQL: "consumer", Kind: query.KindText},
	"seller":   {SQL: "seller", Kind: query.KindText},
	"remark":   {SQL: "remark", Kind: query.KindText},
	"fitdate":  {SQL: "fitdate", Kind: query.KindDate},
	"fitprice": {SQL: "fitprice", Kind: query.KindNumber},
}

type FitRepo struct{ db *sql.DB }

func NewFitRepo(db *sql.DB) *FitRepo { return &FitRepo{db: db} }

func (r *FitRepo) List(ctx context.Context, f query.Filter) (rows []FitRecord, truncated bool, err error) {
	where, args, err := query.Build(f, FitFilterFields)
	if err != nil {
		return nil, false, err
	}

	var b strings.Builder
	b.WriteString(`
		SELECT uid, consumer, COALESCE(seller,''), fitdate, fitprice,
		       COALESCE(remark,''), row_version
		FROM tbl_fit`)
	if where != "" {
		b.WriteString(" WHERE " + where)
	}
	b.WriteString(" ORDER BY uid LIMIT ?")
	args = append(args, MaxListRows+1)

	res, err := r.db.QueryContext(ctx, b.String(), args...)
	if err != nil {
		return nil, false, fmt.Errorf("mysql: list fit: %w", err)
	}
	defer res.Close()

	rows = []FitRecord{}
	for res.Next() {
		var f FitRecord
		var date sql.NullTime
		var price sql.NullString
		if err := res.Scan(&f.UID, &f.Consumer, &f.Seller, &date, &price,
			&f.Remark, &f.RowVersion); err != nil {
			return nil, false, fmt.Errorf("mysql: list fit: %w", err)
		}
		if date.Valid {
			d := date.Time.Format("2006-01-02")
			f.FitDate = &d
		}
		if price.Valid {
			// NULL means "no charge recorded", not "free". Kept distinct.
			p := price.String
			f.FitPrice = &p
		}
		rows = append(rows, f)
	}
	if err := res.Err(); err != nil {
		return nil, false, fmt.Errorf("mysql: list fit: %w", err)
	}
	if len(rows) > MaxListRows {
		return rows[:MaxListRows], true, nil
	}
	return rows, false, nil
}

func (r *FitRepo) Create(ctx context.Context, consumer, seller string, date, price *string, remark string) (int64, error) {
	res, err := r.db.ExecContext(ctx,
		`INSERT INTO tbl_fit (consumer, seller, fitdate, fitprice, remark) VALUES (?,?,?,?,?)`,
		consumer, seller, date, price, remark)
	if err != nil {
		return 0, fmt.Errorf("mysql: create fit: %w", err)
	}
	return res.LastInsertId()
}

func (r *FitRepo) Update(ctx context.Context, uid int64, version int, consumer, seller string, date, price *string, remark string) error {
	res, err := r.db.ExecContext(ctx, `
		UPDATE tbl_fit
		SET consumer = ?, seller = ?, fitdate = ?, fitprice = ?, remark = ?,
		    row_version = row_version + 1
		WHERE uid = ? AND row_version = ?`,
		consumer, seller, date, price, remark, uid, version)
	if err != nil {
		return fmt.Errorf("mysql: update fit: %w", err)
	}
	if n, _ := res.RowsAffected(); n == 1 {
		return nil
	}

	var exists bool
	if err := r.db.QueryRowContext(ctx,
		`SELECT EXISTS(SELECT 1 FROM tbl_fit WHERE uid = ?)`, uid).Scan(&exists); err != nil {
		return fmt.Errorf("mysql: update fit: %w", err)
	}
	if !exists {
		return ErrNotFound
	}
	return ErrVersionConflict
}

func (r *FitRepo) Delete(ctx context.Context, uid int64) error {
	res, err := r.db.ExecContext(ctx, `DELETE FROM tbl_fit WHERE uid = ?`, uid)
	if err != nil {
		return fmt.Errorf("mysql: delete fit: %w", err)
	}
	if n, _ := res.RowsAffected(); n == 0 {
		return ErrNotFound
	}
	return nil
}
