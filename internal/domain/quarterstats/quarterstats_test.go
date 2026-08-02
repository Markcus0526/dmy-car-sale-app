package quarterstats

import (
	"fmt"
	"testing"
)

// TestPercentRoundsHalfAwayFromZeroLikeDotNet is the load-bearing test in this
// package.
//
// .NET formats a decimal with "0.00" by rounding HALF AWAY FROM ZERO. Go's
// fmt "%.2f" rounds HALF TO EVEN. On a report that compares old system against
// new, that is a last-digit disagreement nobody can explain without knowing
// this — so the disagreement is asserted here, in both directions, rather than
// left as a comment.
func TestPercentRoundsHalfAwayFromZeroLikeDotNet(t *testing.T) {
	// 1/160 = 0.625%, an exact binary value sitting exactly on the midpoint.
	got := FormatPercent(1, 160)
	if got != "0.63%" {
		t.Errorf("FormatPercent(1, 160) = %q, want 0.63%% (half away from zero, as .NET)", got)
	}

	// Demonstrate the trap: Go's own formatter disagrees on this input. If this
	// ever starts matching, Go changed its rounding and this package's reason
	// for existing needs rechecking.
	if naive := fmt.Sprintf("%.2f%%", 1.0/160.0*100); naive == got {
		t.Errorf("fmt %%.2f produced %q too — the half-to-even difference this "+
			"function exists to avoid may no longer hold", naive)
	} else if naive != "0.62%" {
		t.Errorf("sanity: fmt %%.2f gave %q, expected 0.62%% (half to even)", naive)
	}

	// 3/160 = 1.875%, midpoint again; away-from-zero gives 1.88, to-even 1.88
	// as well (8 is even) -- included so the test does not accidentally assert
	// that the two ALWAYS differ.
	if got := FormatPercent(3, 160); got != "1.88%" {
		t.Errorf("FormatPercent(3, 160) = %q, want 1.88%%", got)
	}
}

func TestPercentZeroTargetIsNotAnError(t *testing.T) {
	// The original guards this explicitly and the reports depend on the literal
	// "0.00%" -- not a blank, not "NaN", not a division panic.
	if got := FormatPercent(50, 0); got != "0.00%" {
		t.Errorf("FormatPercent(50, 0) = %q, want 0.00%%", got)
	}
	if got := FormatPercent(0, 0); got != "0.00%" {
		t.Errorf("FormatPercent(0, 0) = %q, want 0.00%%", got)
	}
}

func TestPercentOrdinaryCases(t *testing.T) {
	cases := []struct {
		achieved, target int
		want             string
	}{
		{0, 100, "0.00%"},
		{50, 100, "50.00%"},
		{100, 100, "100.00%"},
		{150, 100, "150.00%"}, // over-achievement is not capped
		{1, 3, "33.33%"},
		{2, 3, "66.67%"},
		{1, 8, "12.50%"},
		{1, 7, "14.29%"},
	}
	for _, c := range cases {
		if got := FormatPercent(c.achieved, c.target); got != c.want {
			t.Errorf("FormatPercent(%d, %d) = %q, want %q", c.achieved, c.target, got, c.want)
		}
	}
}

// Targets and counts should never be negative, but the grid is free-entry and
// nothing stops an operator typing one. It must format, not panic or produce
// "0.-50%".
func TestPercentHandlesNegatives(t *testing.T) {
	cases := []struct {
		achieved, target int
		want             string
	}{
		{-50, 100, "-50.00%"},
		{50, -100, "-50.00%"},
		{-1, 200, "-0.50%"}, // between -1 and 0: the sign must survive
	}
	for _, c := range cases {
		if got := FormatPercent(c.achieved, c.target); got != c.want {
			t.Errorf("FormatPercent(%d, %d) = %q, want %q", c.achieved, c.target, got, c.want)
		}
	}
}

func TestCalculateFillsAchievedAndSubtotals(t *testing.T) {
	g := &Grid{
		Year: 2026, Quarter: 3,
		General: Block{Rows: []Row{
			{CarSeries: "轿车系列", Target: 100, Month1: 10, Month2: 20, Month3: 30, Remain: 5},
			{CarSeries: "越野系列", Target: 50, Month1: 5, Month2: 5, Month3: 5, Remain: 2},
		}},
		Special: Block{Rows: []Row{
			{CarSeries: "特种车", Target: 10, Month1: 1, Month2: 2, Month3: 3, Remain: 1},
		}},
	}
	Calculate(g)

	if got := g.General.Rows[0].Achieved; got != 60 {
		t.Errorf("row achieved = %d, want 60", got)
	}
	if got := g.General.Rows[0].Percent; got != "60.00%" {
		t.Errorf("row percent = %q, want 60.00%%", got)
	}

	sub := g.General.Subtotal
	if sub.Target != 150 || sub.Achieved != 75 || sub.Remain != 7 {
		t.Errorf("subtotal = %+v, want target 150 achieved 75 remain 7", sub)
	}
	// 75/150 = 50%
	if sub.Percent != "50.00%" {
		t.Errorf("subtotal percent = %q, want 50.00%%", sub.Percent)
	}

	// The two blocks are computed independently -- the legacy form runs the
	// same code twice with a shifted colbase, and a subtotal that leaked
	// between them would be a silent cross-contamination.
	if g.Special.Subtotal.Target != 10 || g.Special.Subtotal.Achieved != 6 {
		t.Errorf("special subtotal = %+v, want target 10 achieved 6", g.Special.Subtotal)
	}
}

// The subtotal percentage divides (m1+m2+m3) by the target, not the sum of the
// per-row Achieved values. Those are equal by construction; the test pins that
// they stay equal, because the original relies on it.
func TestSubtotalPercentMatchesSumOfAchieved(t *testing.T) {
	g := &Grid{General: Block{Rows: []Row{
		{Target: 7, Month1: 1, Month2: 1, Month3: 1},
		{Target: 9, Month1: 2, Month2: 2, Month3: 2},
	}}}
	Calculate(g)

	sub := g.General.Subtotal
	if sub.Achieved != sub.Month1+sub.Month2+sub.Month3 {
		t.Fatalf("achieved %d != m1+m2+m3 %d", sub.Achieved, sub.Month1+sub.Month2+sub.Month3)
	}
	if want := FormatPercent(sub.Achieved, sub.Target); sub.Percent != want {
		t.Errorf("subtotal percent = %q, want %q", sub.Percent, want)
	}
}

func TestCalculateOnEmptyBlocks(t *testing.T) {
	g := &Grid{}
	Calculate(g) // must not panic on a year with no configured series
	if g.General.Subtotal.Percent != "0.00%" {
		t.Errorf("empty subtotal percent = %q, want 0.00%%", g.General.Subtotal.Percent)
	}
}

func TestMonthsOf(t *testing.T) {
	cases := map[int][3]int{
		1: {1, 2, 3}, 2: {4, 5, 6}, 3: {7, 8, 9}, 4: {10, 11, 12},
	}
	for q, want := range cases {
		a, b, c := MonthsOf(q)
		if [3]int{a, b, c} != want {
			t.Errorf("MonthsOf(%d) = %v, want %v", q, [3]int{a, b, c}, want)
		}
	}
}
