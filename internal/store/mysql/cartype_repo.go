package mysql

import (
	"context"
	"database/sql"
	"fmt"
)

// CarType is one row of tbl_cartype, as needed to pick one.
//
// This is the lookup half of the 车型价格设置 screen, not the whole thing. The
// pricing columns (outprice, otherprice1-4, propval, profitval, outstoreprice)
// are deliberately absent: they feed the finance engine (§6.1), which is
// blocked on Phase 0, and exposing them through a picker endpoint would put
// cost and margin data in front of anyone who can open a vehicle form.
type CarType struct {
	UID       int64  `json:"uid"`
	CarSeries string `json:"carseries"`
	CarCode   string `json:"carcode"`
	CarName   string `json:"carname"`
	Subsets   string `json:"subsets"`
	// InPrice is the default cost price, used to prefill a new vehicle. A
	// decimal string, never a number.
	InPrice       string `json:"inprice"`
	InsideSetCode string `json:"insidesetcode"`
	InsideSetName string `json:"insidesetname"`
	VINPrefix     string `json:"vinprefix"`
	EngineNoPfx   string `json:"enginenoprefix"`
}

type CarTypeRepo struct{ db *sql.DB }

func NewCarTypeRepo(db *sql.DB) *CarTypeRepo { return &CarTypeRepo{db: db} }

// MaxCarTypeRows bounds the picker feed.
//
// Generous, because the picker filters client-side and a truncated list would
// silently hide a car type someone needs. If a real dealership ever exceeds
// this the truncation is reported, not swallowed.
const MaxCarTypeRows = 2000

// List returns selectable car types.
//
// `deleted = 0` is a soft-delete flag, so this is not merely a filter for
// tidiness: a discontinued model must stop being offered for NEW vehicles
// while remaining resolvable for the thousands of existing rows that reference
// it. That is exactly why Get below does NOT filter on it.
func (r *CarTypeRepo) List(ctx context.Context) (types []CarType, truncated bool, err error) {
	const q = `
		SELECT uid, carseries, carcode, carname, COALESCE(subsets,''), inprice,
		       COALESCE(insidesetcode,''), COALESCE(insidesetname,''),
		       vinprefix, enginenoprefix
		FROM tbl_cartype
		WHERE deleted = 0
		ORDER BY uid
		LIMIT ?`

	rows, err := r.db.QueryContext(ctx, q, MaxCarTypeRows+1)
	if err != nil {
		return nil, false, fmt.Errorf("mysql: list car types: %w", err)
	}
	defer rows.Close()

	types = []CarType{}
	for rows.Next() {
		var c CarType
		if err := rows.Scan(&c.UID, &c.CarSeries, &c.CarCode, &c.CarName, &c.Subsets,
			&c.InPrice, &c.InsideSetCode, &c.InsideSetName, &c.VINPrefix, &c.EngineNoPfx); err != nil {
			return nil, false, fmt.Errorf("mysql: list car types: %w", err)
		}
		types = append(types, c)
	}
	if err := rows.Err(); err != nil {
		return nil, false, fmt.Errorf("mysql: list car types: %w", err)
	}

	if len(types) > MaxCarTypeRows {
		return types[:MaxCarTypeRows], true, nil
	}
	return types, false, nil
}

// Get resolves one car type by id, INCLUDING soft-deleted ones.
//
// Deliberately different from List. Editing a vehicle bought three years ago
// must still show what it is; filtering on `deleted` here would blank the field
// on exactly the historic records people look up most.
func (r *CarTypeRepo) Get(ctx context.Context, uid int64) (*CarType, error) {
	const q = `
		SELECT uid, carseries, carcode, carname, COALESCE(subsets,''), inprice,
		       COALESCE(insidesetcode,''), COALESCE(insidesetname,''),
		       vinprefix, enginenoprefix
		FROM tbl_cartype WHERE uid = ?`

	var c CarType
	err := r.db.QueryRowContext(ctx, q, uid).Scan(
		&c.UID, &c.CarSeries, &c.CarCode, &c.CarName, &c.Subsets, &c.InPrice,
		&c.InsideSetCode, &c.InsideSetName, &c.VINPrefix, &c.EngineNoPfx)
	if err == sql.ErrNoRows {
		return nil, ErrNotFound
	}
	if err != nil {
		return nil, fmt.Errorf("mysql: get car type: %w", err)
	}
	return &c, nil
}
