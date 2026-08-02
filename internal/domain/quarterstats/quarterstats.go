// Package quarterstats ports the quarterly sales-target grid (§6.3).
//
// FrmStatisCarSaleQuarter is an editable grid over tbl_quarterstats, split into
// a "general" and a "special" car-type block, each with a subtotal row. The
// grid is TRANSPOSED relative to how it reads on screen: columns are car
// series, rows are the eight metrics below.
//
//	row 1  target    quarterly target        <- tbl_quarterstats.total{Q}
//	row 2  month1    first month of quarter  <- m{3Q-2}
//	row 3  month2                            <- m{3Q-1}
//	row 4  month3                            <- m{3Q}
//	row 5  achieved  month1+month2+month3    computed, not stored
//	row 6  remain                            <- remain{Q}
//	row 7  (unused)  always loaded as "0"    see Row.Extra
//	row 8  percent   achieved/target*100     computed, formatted "0.00%"
//
// The arithmetic is a pure function over a typed grid so it can be unit-tested
// without a database or a UI, which is the whole reason it lives here rather
// than in a handler.
package quarterstats

import (
	"fmt"
	"strconv"
)

// Row is one car series' figures for one quarter.
//
// Counts are ints, not decimals: these are vehicle counts, and the legacy code
// reads every cell through Convert.ToInt32.
type Row struct {
	CarSeries string `json:"carseries"`
	Target    int    `json:"target"`
	Month1    int    `json:"month1"`
	Month2    int    `json:"month2"`
	Month3    int    `json:"month3"`
	Remain    int    `json:"remain"`
	// Extra is grid row 7. The legacy loader writes the literal "0" into it and
	// never reads it from the database (FrmStatisCarSaleQuarter.cs:190, 220),
	// so it is always zero in practice. Carried because the subtotal sums it
	// and the column is visible; reproducing it as a field makes that explicit
	// rather than leaving a mystery column in the UI.
	Extra int `json:"extra"`

	// Computed, never stored.
	Achieved int    `json:"achieved"`
	Percent  string `json:"percent"`
}

// Block is one half of the grid plus its subtotal.
type Block struct {
	Rows     []Row `json:"rows"`
	Subtotal Row   `json:"subtotal"`
}

// Grid is the whole screen: the two blocks the legacy form computes twice with
// a shifted colbase.
type Grid struct {
	Year    int   `json:"year"`
	Quarter int   `json:"quarter"`
	General Block `json:"general"`
	Special Block `json:"special"`
}

// Calculate fills in Achieved, Percent and both subtotals.
//
// Faithful to CalculateAmounts (FrmStatisCarSaleQuarter.cs:244-330), including
// two details worth naming:
//
//   - The subtotal percentage divides (a2+a3+a4) by a1, NOT a5 by a1. Those are
//     equal only because col5 is recomputed in the same loop before a5
//     accumulates it. Reproduced as written so the equivalence is provable
//     rather than assumed.
//   - A zero target yields "0.00%", not a division error and not a blank. The
//     original guards this explicitly and the reports depend on the literal.
func Calculate(g *Grid) {
	calcBlock(&g.General)
	calcBlock(&g.Special)
}

func calcBlock(b *Block) {
	var sub Row
	for i := range b.Rows {
		r := &b.Rows[i]
		r.Achieved = r.Month1 + r.Month2 + r.Month3
		r.Percent = FormatPercent(r.Achieved, r.Target)

		sub.Target += r.Target
		sub.Month1 += r.Month1
		sub.Month2 += r.Month2
		sub.Month3 += r.Month3
		sub.Achieved += r.Achieved
		sub.Remain += r.Remain
		sub.Extra += r.Extra
	}
	sub.Percent = FormatPercent(sub.Month1+sub.Month2+sub.Month3, sub.Target)
	b.Subtotal = sub
}

// FormatPercent renders achieved/target*100 as .NET's "{0:0.00}%" does.
//
// THE ROUNDING IS THE POINT. .NET formats a `decimal` with "0.00" by rounding
// HALF AWAY FROM ZERO. Go's fmt "%.2f" rounds HALF TO EVEN, and it does so on
// a float64 whose binary value may already sit just below the midpoint. Both
// differences produce a last-digit disagreement on exactly the inputs a
// report-equivalence test would flag, so this computes in integers and rounds
// explicitly.
//
//	1/8   -> 12.5%   both agree
//	1/16  -> 6.25%   both agree
//	5/8   -> 62.5%   both agree
//	1/160 -> 0.625%  .NET "0.63", Go %.2f "0.62"  <- the disagreement
//
// A zero target is "0.00%", matching the original's explicit guard.
func FormatPercent(achieved, target int) string {
	if target == 0 {
		return "0.00%"
	}

	// hundredths of a percent = achieved * 100 * 100 / target, rounded half
	// away from zero. Integer arithmetic throughout: no float, no decimal
	// library, no representation error to reason about.
	num := int64(achieved) * 10000
	den := int64(target)
	if den < 0 {
		num, den = -num, -den
	}

	var h int64
	if num >= 0 {
		h = (2*num + den) / (2 * den)
	} else {
		h = -((-2*num + den) / (2 * den))
	}

	whole := h / 100
	frac := h % 100
	if frac < 0 {
		frac = -frac
	}
	// A negative value between -1 and 0 loses its sign to integer division,
	// e.g. -0.5% gives whole=0. Restore it explicitly.
	sign := ""
	if h < 0 && whole == 0 {
		sign = "-"
	}
	return sign + strconv.FormatInt(whole, 10) + "." + fmt.Sprintf("%02d", frac) + "%"
}

// MonthsOf returns the three tbl_quarterstats month column indices for a
// quarter: Q1 -> 1,2,3 ... Q4 -> 10,11,12.
func MonthsOf(quarter int) (int, int, int) {
	base := (quarter-1)*3 + 1
	return base, base + 1, base + 2
}

// ValidQuarter reports whether q is 1-4.
func ValidQuarter(q int) bool { return q >= 1 && q <= 4 }
