// Package query builds parameterised SQL WHERE clauses from a structured
// filter, replacing the legacy FrmSearch dialog (plan 2.6).
//
// # WHAT THE LEGACY DOES
//
// FrmSearch lets the user pick up to three text columns and one date column by
// clicking grid headers; the column's underlying name lands in keyField1..4
// (52 assignments across 11 forms, all of the shape
// `frmSearch.keyField1 = grid.Cols[c].Name`). It then concatenates:
//
//	keyField1 LIKE '%typed%' AND keyField2 LIKE '%typed%' AND …
//	AND keyField4 >= 'start' AND keyField4 <= 'end'
//
// and assigns it to a BindingSource.Filter. That same block is copy-pasted
// eleven times, once per searchKind -- which is why this exists once instead.
//
// TWO THINGS THIS FIXES
//
//  1. Values are parameterised. The legacy builds SQL-ish text by string
//     concatenation (plan 10.6). It targets DataTable.Select rather than the
//     database, so it was expression injection rather than SQL injection --
//     but the port issues real SQL, where the same shape is not survivable.
//
//  2. Field names are validated against an allowlist. A column name cannot be
//     a bind parameter, so it is the one part of a filter that must be checked
//     rather than escaped. The allowlist is per resource, declared next to the
//     repository that owns it.
package query

import (
	"fmt"
	"slices"
	"strings"
	"time"
)

// Op is a comparison. Deliberately a closed set: every operator must map to a
// known SQL fragment, so no caller can introduce one.
type Op string

const (
	// OpContains is the legacy default -- LIKE '%value%'.
	OpContains Op = "contains"
	OpEquals   Op = "eq"
	OpGTE      Op = "gte"
	OpLTE      Op = "lte"
)

var validOps = []Op{OpContains, OpEquals, OpGTE, OpLTE}

// Condition is one clause. Value is always bound, never interpolated.
type Condition struct {
	Field string `json:"field"`
	Op    Op     `json:"op"`
	Value string `json:"value"`
}

// Filter is a set of conditions.
//
// There is no OR: the legacy dialog only ever produced AND, and every screen
// that uses it expects that. Adding disjunction later means adding it here
// once, not in eleven places.
type Filter struct {
	Conditions []Condition `json:"conditions"`
}

// MaxConditions bounds what one request can ask the database to do. The legacy
// UI allowed four (three text + one date range, which is two conditions), so
// eight is generous while still refusing a pathological request.
const MaxConditions = 8

// Fields is the allowlist for one resource: API field name -> SQL column.
//
// The indirection matters. It means a rename in the database does not change
// the API, and -- more importantly -- that a client can never name a column
// directly. `vw_storeout.Expr1` (plan 8.1) is the sort of thing that must
// stay internal.
type Fields map[string]Column

// Column is an allowlisted column and what it may be compared with.
type Column struct {
	// SQL is the column expression. It comes from this map, never from input.
	SQL string
	// Kind restricts the operators that make sense. A date column with
	// `contains` would produce a LIKE against a DATETIME, which MySQL will
	// happily do and which will confuse everyone.
	Kind Kind
}

type Kind int

const (
	KindText Kind = iota
	KindDate
	KindNumber
)

// ErrInvalidFilter is returned for anything a client got wrong. It carries a
// field name where one is relevant so the handler can build a 422 body.
type ErrInvalidFilter struct {
	Field  string
	Reason string
}

func (e *ErrInvalidFilter) Error() string {
	if e.Field != "" {
		return fmt.Sprintf("query: %s: %s", e.Field, e.Reason)
	}
	return "query: " + e.Reason
}

// Build turns a filter into a WHERE fragment and its bind arguments.
//
// Returns an empty string when there is nothing to filter on -- callers should
// append it only if non-empty rather than emitting a bare WHERE.
func Build(f Filter, allowed Fields) (where string, args []any, err error) {
	if len(f.Conditions) == 0 {
		return "", nil, nil
	}
	if len(f.Conditions) > MaxConditions {
		return "", nil, &ErrInvalidFilter{
			Reason: fmt.Sprintf("at most %d conditions, got %d", MaxConditions, len(f.Conditions)),
		}
	}

	clauses := make([]string, 0, len(f.Conditions))
	for _, c := range f.Conditions {
		col, ok := allowed[c.Field]
		if !ok {
			// Deliberately does not echo the field back into the message: an
			// unknown field is a client bug or a probe, and neither benefits
			// from confirmation of what does exist.
			return "", nil, &ErrInvalidFilter{Field: c.Field, Reason: "not a filterable field"}
		}
		if !slices.Contains(validOps, c.Op) {
			return "", nil, &ErrInvalidFilter{Field: c.Field, Reason: "unsupported operator"}
		}
		if c.Value == "" {
			// The legacy UI skipped empty inputs rather than filtering on
			// them; an empty `contains` would match everything, which is
			// surprising rather than useful.
			continue
		}
		if err := checkKind(col, c); err != nil {
			return "", nil, err
		}

		clause, arg, err := render(col, c)
		if err != nil {
			return "", nil, err
		}
		clauses = append(clauses, clause)
		args = append(args, arg)
	}

	if len(clauses) == 0 {
		return "", nil, nil
	}
	return strings.Join(clauses, " AND "), args, nil
}

func checkKind(col Column, c Condition) error {
	switch col.Kind {
	case KindDate, KindNumber:
		if c.Op == OpContains {
			return &ErrInvalidFilter{
				Field:  c.Field,
				Reason: "contains is only valid for text fields",
			}
		}
	}
	return nil
}

func render(col Column, c Condition) (clause string, arg any, err error) {
	switch c.Op {
	case OpContains:
		// The value is bound; only the wildcards are ours.
		//
		// escapeLike matters: the legacy interpolated raw input into
		// '%value%', so a user typing "%" silently matched everything and "_"
		// matched any character. Nobody was relying on that -- it is not
		// documented or discoverable -- so this treats input as literal text,
		// which is what a search box is understood to do.
		return col.SQL + " LIKE ? ESCAPE '\\\\'", "%" + escapeLike(c.Value) + "%", nil

	case OpEquals:
		return col.SQL + " = ?", c.Value, nil

	case OpGTE, OpLTE:
		op := ">="
		if c.Op == OpLTE {
			op = "<="
		}
		if col.Kind == KindDate {
			t, err := parseDate(c.Value)
			if err != nil {
				return "", nil, &ErrInvalidFilter{Field: c.Field, Reason: "not a valid date"}
			}
			// An inclusive upper bound on a DATETIME must cover the whole day,
			// or "to 1 August" silently excludes everything after midnight --
			// the classic off-by-a-day in date-range reporting.
			if c.Op == OpLTE {
				t = t.Add(24*time.Hour - time.Nanosecond)
			}
			return col.SQL + " " + op + " ?", t, nil
		}
		return col.SQL + " " + op + " ?", c.Value, nil
	}
	return "", nil, &ErrInvalidFilter{Field: c.Field, Reason: "unsupported operator"}
}

// escapeLike neutralises LIKE metacharacters so user input is matched
// literally. Paired with ESCAPE '\' in the clause.
func escapeLike(s string) string {
	r := strings.NewReplacer(`\`, `\\`, `%`, `\%`, `_`, `\_`)
	return r.Replace(s)
}

// parseDate accepts the shapes a browser date input and an API client produce.
//
// Parsed in Asia/Shanghai for the same reason the driver uses it: legacy
// DATETIME values are Chinese local time, and comparing them against a UTC
// boundary shifts every range by eight hours.
func parseDate(s string) (time.Time, error) {
	loc, err := time.LoadLocation("Asia/Shanghai")
	if err != nil {
		loc = time.Local
	}
	for _, layout := range []string{"2006-01-02", time.RFC3339, "2006-01-02 15:04:05"} {
		if t, err := time.ParseInLocation(layout, s, loc); err == nil {
			return t, nil
		}
	}
	return time.Time{}, fmt.Errorf("unrecognised date %q", s)
}
