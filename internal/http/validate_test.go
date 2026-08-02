package http

import "testing"

// Length limits are in characters, not bytes. A 20-character Chinese name is
// 60 bytes in UTF-8; counting bytes would reject it against a VARCHAR(50)
// column that accepts it happily. This system is almost entirely Chinese text,
// so a byte count would fail constantly.
func TestRequiredCountsRunesNotBytes(t *testing.T) {
	v := newValidator()
	v.required("carname", "轿车名称测试用例文字", 50) // 10 chars, 30 bytes
	if !v.ok() {
		t.Errorf("10-character Chinese name rejected against a 50-char limit: %v", v.fields)
	}

	v = newValidator()
	v.required("carname", "轿车名称测", 4) // 5 chars
	if v.fields["carname"] != fieldTooLong {
		t.Errorf("fields = %v, want carname TOO_LONG", v.fields)
	}
}

func TestRequiredRejectsWhitespaceOnly(t *testing.T) {
	v := newValidator()
	if got := v.required("vin", "   ", 50); got != "" {
		t.Errorf("returned %q, want the trimmed empty string", got)
	}
	if v.fields["vin"] != fieldRequired {
		t.Errorf("fields = %v, want vin REQUIRED", v.fields)
	}
}

func TestValidatorCollectsEveryProblem(t *testing.T) {
	v := newValidator()
	v.required("vin", "", 50)
	v.required("engineno", "", 50)
	v.requiredDate("billdate", "01/08/2026")
	if len(v.fields) != 3 {
		t.Errorf("fields = %v, want all three reported in one pass", v.fields)
	}
}

func TestDecimalAgainstColumnPrecision(t *testing.T) {
	// DECIMAL(10,2): eight integer digits, two fractional.
	cases := []struct {
		in   string
		want bool // valid
	}{
		{"100000.00", true},
		{"0", true},
		{"-500.25", true},
		{"99999999.99", true},   // exactly at the limit
		{"100000000.00", false}, // nine integer digits
		{"1.234", false},        // three decimal places
		{"1.2300", true},        // trailing zeros are not significant digits
		{"0000123.45", true},    // nor are leading ones
		{"1e5", false},          // exponent form: far more likely a typo than intent
		{"12.5.6", false},
		{"abc", false},
		{"", true}, // empty means NULL, which the column permits
	}
	for _, c := range cases {
		v := newValidator()
		v.decimal("inprice", c.in, 10, 2)
		if v.ok() != c.want {
			t.Errorf("decimal(%q): valid = %v, want %v", c.in, v.ok(), c.want)
		}
	}
}

// The value must come back byte-identical. Normalising it -- or worse, routing
// it through float64 to measure its magnitude -- is the precision loss the
// decimal-as-string rule exists to prevent (plan §11.2).
func TestDecimalReturnsInputUnchanged(t *testing.T) {
	v := newValidator()
	got := v.decimal("inprice", " 1234567.89 ", 10, 2)
	if got == nil || *got != "1234567.89" {
		t.Fatalf("got %v, want the trimmed input verbatim", got)
	}
}

func TestDecimalEmptyIsNullNotZero(t *testing.T) {
	v := newValidator()
	if got := v.decimal("inprice", "", 10, 2); got != nil {
		t.Errorf("got %q, want nil — blank means 'not recorded', not 0", *got)
	}
	if !v.ok() {
		t.Errorf("blank flagged as invalid: %v", v.fields)
	}
}

// REQUIRED must survive a later TOO_LONG on the same field, so the message the
// user sees names the actual problem.
func TestFirstProblemPerFieldWins(t *testing.T) {
	v := newValidator()
	v.set("vin", fieldRequired)
	v.set("vin", fieldTooLong)
	if v.fields["vin"] != fieldRequired {
		t.Errorf("vin = %q, want REQUIRED", v.fields["vin"])
	}
}

func TestOnRoadFormValidation(t *testing.T) {
	valid := onRoadForm{
		BillNo: "B1", BillDate: "2026-08-01", VIN: "LSGH1", EngineNo: "E1",
		CarTypeID: 7, CarType: "C1", InPrice: "1000.00",
	}
	if _, fields := valid.validate(); fields != nil {
		t.Errorf("valid form rejected: %v", fields)
	}

	// cartypeid is a foreign key; zero is JSON's missing-number, not a row.
	noType := valid
	noType.CarTypeID = 0
	if _, fields := noType.validate(); fields["cartypeid"] != fieldRequired {
		t.Errorf("fields = %v, want cartypeid REQUIRED", fields)
	}

	empty := onRoadForm{}
	_, fields := empty.validate()
	for _, name := range []string{"billno", "billdate", "vin", "engineno", "cartype", "cartypeid"} {
		if fields[name] != fieldRequired {
			t.Errorf("%s = %q, want REQUIRED", name, fields[name])
		}
	}
}
