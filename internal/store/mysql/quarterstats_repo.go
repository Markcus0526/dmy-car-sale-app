package mysql

import (
	"context"
	"database/sql"
	"fmt"

	"github.com/Markcus0526/carsaleman/internal/domain/quarterstats"
)

// tbl_quarterstats holds one row per (year, carseries, type) carrying ALL
// twelve months and all four quarters. A quarter is a projection of that row,
// not a row of its own -- which is why saving a quarter is an UPDATE of six
// named columns rather than a row replacement.

type QuarterStatsRepo struct{ db *sql.DB }

func NewQuarterStatsRepo(db *sql.DB) *QuarterStatsRepo { return &QuarterStatsRepo{db: db} }

// SpecialType is the tbl_quarterstats.type value for the special-car block.
//
// The legacy general-block query has NO type predicate at all
// (FrmStatisCarSaleQuarter.cs:176) while the special block filters `type = 1`
// (line 206). Reproduced here as "not special" rather than "no filter",
// because the literal original would let a special series' row also satisfy the
// general query and appear in both blocks. In practice the two blocks draw
// their series from different lists so they never overlap -- but relying on
// that is relying on a coincidence.
//
// TODO(phase0): confirm from live data that no carseries has both a type=1 row
// and a non-type=1 row for the same year. If any does, the two readings differ
// and the business needs to say which is intended.
const SpecialType = 1

// Load reads one quarter and computes the derived cells.
func (r *QuarterStatsRepo) Load(ctx context.Context, year, quarter int) (*quarterstats.Grid, error) {
	if !quarterstats.ValidQuarter(quarter) {
		return nil, fmt.Errorf("mysql: quarter %d out of range", quarter)
	}
	m1, m2, m3 := quarterstats.MonthsOf(quarter)

	// Column names are chosen from a closed set derived from `quarter`, never
	// from caller input, so this interpolation cannot carry anything a bind
	// parameter would have protected.
	q := fmt.Sprintf(`
		SELECT COALESCE(carseries,''),
		       COALESCE(total%d,0), COALESCE(m%d,0), COALESCE(m%d,0), COALESCE(m%d,0),
		       COALESCE(remain%d,0), COALESCE(type,0)
		FROM tbl_quarterstats
		WHERE year = ?
		ORDER BY uid`, quarter, m1, m2, m3, quarter)

	rows, err := r.db.QueryContext(ctx, q, year)
	if err != nil {
		return nil, fmt.Errorf("mysql: load quarterstats: %w", err)
	}
	defer rows.Close()

	grid := &quarterstats.Grid{Year: year, Quarter: quarter}
	grid.General.Rows = []quarterstats.Row{}
	grid.Special.Rows = []quarterstats.Row{}

	for rows.Next() {
		var row quarterstats.Row
		var typ int
		if err := rows.Scan(&row.CarSeries, &row.Target, &row.Month1, &row.Month2,
			&row.Month3, &row.Remain, &typ); err != nil {
			return nil, fmt.Errorf("mysql: load quarterstats: %w", err)
		}
		if typ == SpecialType {
			grid.Special.Rows = append(grid.Special.Rows, row)
		} else {
			grid.General.Rows = append(grid.General.Rows, row)
		}
	}
	if err := rows.Err(); err != nil {
		return nil, fmt.Errorf("mysql: load quarterstats: %w", err)
	}

	quarterstats.Calculate(grid)
	return grid, nil
}

// Save writes a WHOLE quarter in one transaction.
//
// §6.3 calls for this shape explicitly. The legacy form saved on year/quarter
// switch via SaveChange(oldYear, oldQuarter) and is "where the current code is
// most likely to lose edits": a per-cell save racing a block recalculation can
// leave a quarter half-written, and switching away before the handler fires
// loses the lot.
//
// Only the six columns this quarter owns are touched. The other three quarters
// and the other nine months live in the same row and must not be disturbed by
// someone editing Q3.
func (r *QuarterStatsRepo) Save(ctx context.Context, year, quarter int, general, special []quarterstats.Row) error {
	if !quarterstats.ValidQuarter(quarter) {
		return fmt.Errorf("mysql: quarter %d out of range", quarter)
	}
	m1, m2, m3 := quarterstats.MonthsOf(quarter)

	tx, err := r.db.BeginTx(ctx, nil)
	if err != nil {
		return fmt.Errorf("mysql: save quarterstats: %w", err)
	}
	defer tx.Rollback() //nolint:errcheck

	// INSERT ... ON DUPLICATE KEY UPDATE is deliberately NOT used: there is no
	// unique key on (year, carseries, type) to hang it on, and inventing one
	// before the Phase 0 dump is examined could fail on live data. Update-then-
	// insert inside a transaction achieves the same thing without a constraint.
	upd := fmt.Sprintf(`
		UPDATE tbl_quarterstats
		SET total%d = ?, m%d = ?, m%d = ?, m%d = ?, remain%d = ?,
		    row_version = row_version + 1
		WHERE year = ? AND carseries = ? AND COALESCE(type,0) = ?`,
		quarter, m1, m2, m3, quarter)
	ins := fmt.Sprintf(`
		INSERT INTO tbl_quarterstats (year, carseries, type, total%d, m%d, m%d, m%d, remain%d)
		VALUES (?,?,?,?,?,?,?,?)`, quarter, m1, m2, m3, quarter)

	write := func(rows []quarterstats.Row, typ int) error {
		for _, row := range rows {
			res, err := tx.ExecContext(ctx, upd,
				row.Target, row.Month1, row.Month2, row.Month3, row.Remain,
				year, row.CarSeries, typ)
			if err != nil {
				return fmt.Errorf("mysql: save quarterstats: %w", err)
			}
			if n, _ := res.RowsAffected(); n > 0 {
				continue
			}
			// A series with no row for this year yet. The legacy form showed
			// zeroes and silently discarded any edit to it, because Select
			// returned nothing and there was no insert path at all.
			if _, err := tx.ExecContext(ctx, ins,
				year, row.CarSeries, typ,
				row.Target, row.Month1, row.Month2, row.Month3, row.Remain); err != nil {
				return fmt.Errorf("mysql: save quarterstats: %w", err)
			}
		}
		return nil
	}

	if err := write(general, 0); err != nil {
		return err
	}
	if err := write(special, SpecialType); err != nil {
		return err
	}
	return tx.Commit()
}
