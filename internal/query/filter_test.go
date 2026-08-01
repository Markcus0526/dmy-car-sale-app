package query

import (
	"errors"
	"strings"
	"testing"
	"time"
)

var testFields = Fields{
	"vin":       {SQL: "vin", Kind: KindText},
	"carname":   {SQL: "carname", Kind: KindText},
	"indate":    {SQL: "indate", Kind: KindDate},
	"inprice":   {SQL: "inprice", Kind: KindNumber},
	"colorname": {SQL: "t.colorname", Kind: KindText}, // qualified, to prove the mapping is used
}

func build(t *testing.T, conds ...Condition) (string, []any) {
	t.Helper()
	where, args, err := Build(Filter{Conditions: conds}, testFields)
	if err != nil {
		t.Fatalf("Build: %v", err)
	}
	return where, args
}

// ---------------------------------------------------------------------------
// The security property: a field name cannot be a bind parameter, so it is the
// one part of a filter that must be allowlisted rather than escaped.

func TestUnknownFieldIsRejected(t *testing.T) {
	for _, field := range []string{
		"password",                      // real column, not filterable
		"vin; DROP TABLE tbl_onroad --", // injection through the field name
		"1=1",
		"",
		"VIN", // case matters; the map is the contract
	} {
		_, _, err := Build(Filter{Conditions: []Condition{
			{Field: field, Op: OpContains, Value: "x"},
		}}, testFields)

		var invalid *ErrInvalidFilter
		if !errors.As(err, &invalid) {
			t.Errorf("field %q: err = %v, want ErrInvalidFilter", field, err)
		}
	}
}

// Every user-supplied value must arrive as a bind argument, never inside the
// SQL text.
func TestValuesAreAlwaysBound(t *testing.T) {
	nasty := "'; DROP TABLE tbl_onroad; --"

	where, args := build(t, Condition{Field: "vin", Op: OpContains, Value: nasty})

	// The property is that no part of the user's value reaches the SQL text.
	// The clause does contain quotes -- ESCAPE '\\' is ours, not theirs --
	// so checking for quotes would be testing the wrong thing.
	for _, fragment := range []string{"DROP", "TABLE", "--", "tbl_onroad"} {
		if strings.Contains(where, fragment) {
			t.Fatalf("value fragment %q leaked into SQL: %q", fragment, where)
		}
	}
	if strings.Count(where, "?") != 1 {
		t.Errorf("where = %q, want exactly one placeholder", where)
	}
	if len(args) != 1 {
		t.Fatalf("args = %v, want one bound value", args)
	}
	// The bound value is the input with LIKE metacharacters escaped -- the
	// underscore in tbl_onroad becomes tbl\_onroad. It is still a bind
	// parameter, so nothing in it is ever interpreted as SQL; the escaping
	// only governs how LIKE reads it. Verified against MySQL, where
	// `LIKE '%a\_b%' ESCAPE '\\'` matches "a_b" but not "axb".
	if want := "%" + escapeLike(nasty) + "%"; args[0] != want {
		t.Errorf("arg = %q, want %q", args[0], want)
	}
}

func TestUnsupportedOperatorIsRejected(t *testing.T) {
	_, _, err := Build(Filter{Conditions: []Condition{
		{Field: "vin", Op: Op("; DELETE FROM tbl_onroad"), Value: "x"},
	}}, testFields)

	var invalid *ErrInvalidFilter
	if !errors.As(err, &invalid) {
		t.Errorf("err = %v, want ErrInvalidFilter", err)
	}
}

// ---------------------------------------------------------------------------

func TestContainsWrapsWildcardsAroundABoundValue(t *testing.T) {
	where, args := build(t, Condition{Field: "vin", Op: OpContains, Value: "LSGH"})

	if !strings.Contains(where, "vin LIKE ?") {
		t.Errorf("where = %q", where)
	}
	if args[0] != "%LSGH%" {
		t.Errorf("arg = %q, want %%LSGH%%", args[0])
	}
}

// The legacy interpolated raw input into '%value%', so "%" matched everything
// and "_" matched any character. Input is now literal, which is what a search
// box is understood to do.
func TestLikeMetacharactersAreEscaped(t *testing.T) {
	for _, tc := range []struct{ in, want string }{
		{"100%", `%100\%%`},
		{"a_b", `%a\_b%`},
		{`back\slash`, `%back\\slash%`},
		{"%", `%\%%`},
	} {
		_, args := build(t, Condition{Field: "vin", Op: OpContains, Value: tc.in})
		if args[0] != tc.want {
			t.Errorf("input %q -> %q, want %q", tc.in, args[0], tc.want)
		}
	}
}

func TestFieldMapsToItsColumnExpression(t *testing.T) {
	where, _ := build(t, Condition{Field: "colorname", Op: OpEquals, Value: "红"})

	if !strings.Contains(where, "t.colorname = ?") {
		t.Errorf("where = %q, want the mapped column expression", where)
	}
}

func TestConditionsAreAnded(t *testing.T) {
	where, args := build(t,
		Condition{Field: "vin", Op: OpContains, Value: "LSGH"},
		Condition{Field: "carname", Op: OpContains, Value: "轿车"},
	)

	if strings.Count(where, " AND ") != 1 {
		t.Errorf("where = %q, want exactly one AND", where)
	}
	if len(args) != 2 {
		t.Errorf("args = %v, want 2", args)
	}
}

// The legacy UI ignored blank inputs. An empty `contains` would match every
// row, which is surprising rather than useful.
func TestEmptyValuesAreSkipped(t *testing.T) {
	where, args := build(t,
		Condition{Field: "vin", Op: OpContains, Value: ""},
		Condition{Field: "carname", Op: OpContains, Value: "轿车"},
	)

	if strings.Contains(where, "vin") {
		t.Errorf("where = %q, should have skipped the empty condition", where)
	}
	if len(args) != 1 {
		t.Errorf("args = %v, want 1", args)
	}
}

func TestNoConditionsYieldsEmptyClause(t *testing.T) {
	where, args, err := Build(Filter{}, testFields)
	if err != nil {
		t.Fatal(err)
	}
	if where != "" || args != nil {
		t.Errorf("where = %q, args = %v; want empty so callers omit WHERE entirely", where, args)
	}

	// All-empty values must behave the same, not produce a dangling WHERE.
	where, _, err = Build(Filter{Conditions: []Condition{
		{Field: "vin", Op: OpContains, Value: ""},
	}}, testFields)
	if err != nil || where != "" {
		t.Errorf("where = %q, err = %v; want empty", where, err)
	}
}

func TestTooManyConditionsRejected(t *testing.T) {
	conds := make([]Condition, MaxConditions+1)
	for i := range conds {
		conds[i] = Condition{Field: "vin", Op: OpContains, Value: "x"}
	}
	if _, _, err := Build(Filter{Conditions: conds}, testFields); err == nil {
		t.Error("a request beyond MaxConditions should be rejected")
	}
}

// ---------------------------------------------------------------------------
// Dates

// The bug this prevents: an inclusive upper bound on a DATETIME that is not
// widened excludes everything after midnight, so "to 1 August" silently drops
// a day of data. In a sales report that is a wrong number, not a wrong screen.
func TestDateUpperBoundCoversTheWholeDay(t *testing.T) {
	_, args := build(t, Condition{Field: "indate", Op: OpLTE, Value: "2026-08-01"})

	got, ok := args[0].(time.Time)
	if !ok {
		t.Fatalf("arg = %T, want time.Time", args[0])
	}
	if got.Hour() != 23 || got.Minute() != 59 || got.Second() != 59 {
		t.Errorf("upper bound = %v, want end of day", got)
	}
	if got.Day() != 1 || got.Month() != time.August {
		t.Errorf("upper bound moved to a different day: %v", got)
	}
}

func TestDateLowerBoundIsStartOfDay(t *testing.T) {
	_, args := build(t, Condition{Field: "indate", Op: OpGTE, Value: "2026-08-01"})

	got := args[0].(time.Time)
	if got.Hour() != 0 || got.Minute() != 0 || got.Second() != 0 {
		t.Errorf("lower bound = %v, want start of day", got)
	}
}

// Legacy DATETIME values are Chinese local time; comparing them against a UTC
// boundary shifts every range by eight hours.
func TestDatesAreParsedInShanghai(t *testing.T) {
	_, args := build(t, Condition{Field: "indate", Op: OpGTE, Value: "2026-08-01"})

	got := args[0].(time.Time)
	_, offset := got.Zone()
	if offset != 8*3600 {
		t.Errorf("offset = %ds, want +8h (Asia/Shanghai)", offset)
	}
}

func TestInvalidDateIsRejected(t *testing.T) {
	_, _, err := Build(Filter{Conditions: []Condition{
		{Field: "indate", Op: OpGTE, Value: "not-a-date"},
	}}, testFields)

	var invalid *ErrInvalidFilter
	if !errors.As(err, &invalid) || invalid.Field != "indate" {
		t.Errorf("err = %v, want ErrInvalidFilter on indate", err)
	}
}

// A LIKE against a DATETIME is valid SQL and never what anyone meant.
func TestContainsIsRejectedForNonTextFields(t *testing.T) {
	for _, field := range []string{"indate", "inprice"} {
		_, _, err := Build(Filter{Conditions: []Condition{
			{Field: field, Op: OpContains, Value: "8"},
		}}, testFields)
		if err == nil {
			t.Errorf("%s: contains should be rejected for a non-text field", field)
		}
	}
}

func TestDateRangeProducesBothBounds(t *testing.T) {
	where, args := build(t,
		Condition{Field: "indate", Op: OpGTE, Value: "2026-08-01"},
		Condition{Field: "indate", Op: OpLTE, Value: "2026-08-31"},
	)

	if !strings.Contains(where, "indate >= ?") || !strings.Contains(where, "indate <= ?") {
		t.Errorf("where = %q, want both bounds", where)
	}
	if len(args) != 2 {
		t.Fatalf("args = %v, want 2", args)
	}
	lo, hi := args[0].(time.Time), args[1].(time.Time)
	if !lo.Before(hi) {
		t.Errorf("lower bound %v is not before upper bound %v", lo, hi)
	}
}
