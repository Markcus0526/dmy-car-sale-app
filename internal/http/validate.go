package http

import (
	"regexp"
	"strings"
	"unicode/utf8"
)

// Field-level validation codes. Like every other user-visible string in this
// backend these are CODES, resolved to prose by the client's `error.field.*`
// catalogue -- never English or Chinese text.
const (
	fieldRequired      = "REQUIRED"
	fieldTooLong       = "TOO_LONG"
	fieldInvalidFormat = "INVALID_FORMAT"
)

// validator accumulates per-field codes.
//
// It collects every problem rather than returning the first: a form that
// reports one error per submit makes the user play twenty questions.
type validator struct{ fields map[string]string }

func newValidator() *validator { return &validator{fields: map[string]string{}} }

func (v *validator) ok() bool { return len(v.fields) == 0 }

func (v *validator) set(name, code string) {
	if _, exists := v.fields[name]; !exists {
		v.fields[name] = code // first problem per field wins; REQUIRED beats TOO_LONG
	}
}

// required checks presence and length in one call.
//
// Length is counted in runes, not bytes. VARCHAR(50) in MySQL means 50
// characters, so a byte count would reject a perfectly legal 20-character
// Chinese name (60 bytes in UTF-8) -- the exact failure this system would hit
// constantly.
func (v *validator) required(name, value string, max int) string {
	value = strings.TrimSpace(value)
	if value == "" {
		v.set(name, fieldRequired)
		return value
	}
	if utf8.RuneCountInString(value) > max {
		v.set(name, fieldTooLong)
	}
	return value
}

// optional is `required` without the presence check.
func (v *validator) optional(name, value string, max int) string {
	value = strings.TrimSpace(value)
	if utf8.RuneCountInString(value) > max {
		v.set(name, fieldTooLong)
	}
	return value
}

var dateRe = regexp.MustCompile(`^\d{4}-\d{2}-\d{2}$`)

func (v *validator) requiredDate(name, value string) string {
	value = strings.TrimSpace(value)
	if value == "" {
		v.set(name, fieldRequired)
		return value
	}
	if !dateRe.MatchString(value) {
		v.set(name, fieldInvalidFormat)
	}
	return value
}

// decimalRe matches an optionally-signed fixed-point number. No exponent: this
// is a money field, and 1e5 in a price box is far more likely to be a mistake
// than an intention.
var decimalRe = regexp.MustCompile(`^-?\d+(\.\d+)?$`)

// decimal validates a money string against a DECIMAL(precision, scale) column.
//
// Validated here rather than left to MySQL because the alternative is bad
// either way: in strict mode an over-scale value is a driver error the handler
// can only report as INTERNAL, and in non-strict mode it is silently rounded.
// A user typing one digit too many deserves 422 pointing at the field.
//
// The check is done on the digit string. Parsing to float64 to measure it
// would reintroduce the precision loss the string representation exists to
// avoid (plan 11.2).
func (v *validator) decimal(name, value string, precision, scale int) *string {
	value = strings.TrimSpace(value)
	if value == "" {
		return nil // NULL, which the column permits
	}
	if !decimalRe.MatchString(value) {
		v.set(name, fieldInvalidFormat)
		return nil
	}

	digits := strings.TrimPrefix(value, "-")
	intPart, fracPart, _ := strings.Cut(digits, ".")
	intPart = strings.TrimLeft(intPart, "0")
	fracPart = strings.TrimRight(fracPart, "0")

	if len(fracPart) > scale || len(intPart) > precision-scale {
		v.set(name, fieldInvalidFormat)
		return nil
	}
	return &value
}
