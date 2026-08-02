package mysql

import (
	"context"
	"database/sql"
	"testing"

	"github.com/Markcus0526/carsaleman/internal/domain/quarterstats"
)

func freshQuarters(t *testing.T, db *sql.DB) *QuarterStatsRepo {
	t.Helper()
	if _, err := db.ExecContext(context.Background(), "DELETE FROM tbl_quarterstats"); err != nil {
		t.Fatalf("clearing tbl_quarterstats: %v", err)
	}
	return NewQuarterStatsRepo(db)
}

func TestQuarterStatsRoundTrip(t *testing.T) {
	db := testDB(t)
	repo := freshQuarters(t, db)
	ctx := context.Background()

	general := []quarterstats.Row{
		{CarSeries: "轿车系列", Target: 100, Month1: 10, Month2: 20, Month3: 30, Remain: 5},
	}
	special := []quarterstats.Row{
		{CarSeries: "特种车", Target: 10, Month1: 1, Month2: 2, Month3: 3, Remain: 1},
	}
	if err := repo.Save(ctx, 2026, 3, general, special); err != nil {
		t.Fatalf("save: %v", err)
	}

	grid, err := repo.Load(ctx, 2026, 3)
	if err != nil {
		t.Fatalf("load: %v", err)
	}
	if len(grid.General.Rows) != 1 || len(grid.Special.Rows) != 1 {
		t.Fatalf("blocks = %d general / %d special, want 1 / 1",
			len(grid.General.Rows), len(grid.Special.Rows))
	}
	g := grid.General.Rows[0]
	if g.Target != 100 || g.Month1 != 10 || g.Month3 != 30 || g.Remain != 5 {
		t.Errorf("general row = %+v", g)
	}
	// Derived cells are computed on load, so the client never reimplements the
	// percentage rounding.
	if g.Achieved != 60 || g.Percent != "60.00%" {
		t.Errorf("achieved/percent = %d/%q, want 60/60.00%%", g.Achieved, g.Percent)
	}
	if grid.Special.Rows[0].CarSeries != "特种车" {
		t.Errorf("special row landed in the wrong block: %+v", grid.Special.Rows[0])
	}
}

// THE test for this repository. One tbl_quarterstats row carries all twelve
// months and all four quarters, so saving Q3 must not disturb Q1, Q2 or Q4 --
// someone editing one quarter must not silently blank the rest of the year.
func TestSavingOneQuarterLeavesTheOthersAlone(t *testing.T) {
	db := testDB(t)
	repo := freshQuarters(t, db)
	ctx := context.Background()

	// A full year, written directly.
	if _, err := db.ExecContext(ctx, `
		INSERT INTO tbl_quarterstats
			(year, carseries, type, total1, total2, total3, total4,
			 m1,m2,m3,m4,m5,m6,m7,m8,m9,m10,m11,m12,
			 remain1, remain2, remain3, remain4)
		VALUES (2026,'轿车系列',0, 11,22,33,44,
		        1,2,3,4,5,6,7,8,9,10,11,12,
		        91,92,93,94)`); err != nil {
		t.Fatal(err)
	}

	// Overwrite Q3 only.
	if err := repo.Save(ctx, 2026, 3, []quarterstats.Row{
		{CarSeries: "轿车系列", Target: 999, Month1: 71, Month2: 81, Month3: 91, Remain: 993},
	}, nil); err != nil {
		t.Fatalf("save: %v", err)
	}

	var t1, t2, t3, t4, m1, m6, m7, m8, m9, m12, r1, r3, r4 int
	if err := db.QueryRowContext(ctx, `
		SELECT total1,total2,total3,total4, m1,m6,m7,m8,m9,m12, remain1,remain3,remain4
		FROM tbl_quarterstats WHERE year = 2026 AND carseries = '轿车系列'`).
		Scan(&t1, &t2, &t3, &t4, &m1, &m6, &m7, &m8, &m9, &m12, &r1, &r3, &r4); err != nil {
		t.Fatal(err)
	}

	// Q3's own columns changed...
	if t3 != 999 || m7 != 71 || m8 != 81 || m9 != 91 || r3 != 993 {
		t.Errorf("Q3 not written: total3=%d m7=%d m8=%d m9=%d remain3=%d", t3, m7, m8, m9, r3)
	}
	// ...and nothing else did.
	if t1 != 11 || t2 != 22 || t4 != 44 {
		t.Errorf("other quarters' targets changed: %d %d %d, want 11 22 44", t1, t2, t4)
	}
	if m1 != 1 || m6 != 6 || m12 != 12 {
		t.Errorf("other months changed: m1=%d m6=%d m12=%d, want 1 6 12", m1, m6, m12)
	}
	if r1 != 91 || r4 != 94 {
		t.Errorf("other remains changed: %d %d, want 91 94", r1, r4)
	}
}

// A series with no row for the year yet must be created. The legacy form
// showed zeroes for it and silently discarded any edit, because Select returned
// nothing and there was no insert path at all.
func TestSavingCreatesRowsForNewSeries(t *testing.T) {
	db := testDB(t)
	repo := freshQuarters(t, db)
	ctx := context.Background()

	if err := repo.Save(ctx, 2026, 1, []quarterstats.Row{
		{CarSeries: "新系列", Target: 40, Month1: 1, Month2: 2, Month3: 3, Remain: 4},
	}, nil); err != nil {
		t.Fatalf("save: %v", err)
	}

	grid, err := repo.Load(ctx, 2026, 1)
	if err != nil {
		t.Fatal(err)
	}
	if len(grid.General.Rows) != 1 || grid.General.Rows[0].Target != 40 {
		t.Fatalf("new series not persisted: %+v", grid.General.Rows)
	}
}

// Saving must not create a duplicate on the second call. There is no unique
// key on (year, carseries, type) to lean on, so the update-then-insert order
// is what prevents it.
func TestSavingTwiceDoesNotDuplicate(t *testing.T) {
	db := testDB(t)
	repo := freshQuarters(t, db)
	ctx := context.Background()

	rows := []quarterstats.Row{{CarSeries: "轿车系列", Target: 10, Month1: 1}}
	for i := 0; i < 3; i++ {
		if err := repo.Save(ctx, 2026, 2, rows, nil); err != nil {
			t.Fatalf("save %d: %v", i, err)
		}
	}

	var n int
	if err := db.QueryRowContext(ctx,
		`SELECT COUNT(*) FROM tbl_quarterstats WHERE year = 2026 AND carseries = '轿车系列'`).
		Scan(&n); err != nil {
		t.Fatal(err)
	}
	if n != 1 {
		t.Errorf("%d rows after three saves, want 1", n)
	}
}

// A general and a special row can share a carseries name; they are distinct
// records and must not overwrite each other.
func TestGeneralAndSpecialRowsAreDistinct(t *testing.T) {
	db := testDB(t)
	repo := freshQuarters(t, db)
	ctx := context.Background()

	if err := repo.Save(ctx, 2026, 1,
		[]quarterstats.Row{{CarSeries: "同名", Target: 10}},
		[]quarterstats.Row{{CarSeries: "同名", Target: 99}},
	); err != nil {
		t.Fatalf("save: %v", err)
	}

	grid, err := repo.Load(ctx, 2026, 1)
	if err != nil {
		t.Fatal(err)
	}
	if len(grid.General.Rows) != 1 || grid.General.Rows[0].Target != 10 {
		t.Errorf("general = %+v, want target 10", grid.General.Rows)
	}
	if len(grid.Special.Rows) != 1 || grid.Special.Rows[0].Target != 99 {
		t.Errorf("special = %+v, want target 99", grid.Special.Rows)
	}
}

// NULL columns are the norm here: tbl_quarterstats is almost entirely nullable
// and the legacy loader maps "" to "0" cell by cell. They must read as zero,
// not fail the scan.
func TestNullColumnsLoadAsZero(t *testing.T) {
	db := testDB(t)
	repo := freshQuarters(t, db)
	ctx := context.Background()

	if _, err := db.ExecContext(ctx,
		`INSERT INTO tbl_quarterstats (year, carseries, type) VALUES (2026,'空行',NULL)`); err != nil {
		t.Fatal(err)
	}
	grid, err := repo.Load(ctx, 2026, 4)
	if err != nil {
		t.Fatalf("load: %v", err)
	}
	if len(grid.General.Rows) != 1 {
		t.Fatalf("got %d rows, want 1", len(grid.General.Rows))
	}
	r := grid.General.Rows[0]
	if r.Target != 0 || r.Month1 != 0 || r.Remain != 0 {
		t.Errorf("NULL columns did not read as zero: %+v", r)
	}
	// A zero target must give the literal "0.00%", which the reports depend on.
	if r.Percent != "0.00%" {
		t.Errorf("percent = %q, want 0.00%%", r.Percent)
	}
}
