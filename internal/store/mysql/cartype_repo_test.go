package mysql

import (
	"context"
	"database/sql"
	"errors"
	"testing"
)

func seedCarTypes(t *testing.T, db *sql.DB) (live, deleted int64) {
	t.Helper()
	ctx := context.Background()
	for _, tbl := range []string{"tbl_onroad", "tbl_cartype"} {
		if _, err := db.ExecContext(ctx, "DELETE FROM "+tbl); err != nil {
			t.Fatalf("clearing %s: %v", tbl, err)
		}
	}

	insert := func(code, name string, del int) int64 {
		res, err := db.ExecContext(ctx, `
			INSERT INTO tbl_cartype
				(carseries, carcode, carname, eop, inprice, outprice,
				 otherprice1, otherprice2, otherprice3, otherprice4,
				 propval, profitval, outstoreprice, vinprefix, enginenoprefix, deleted)
			VALUES ('轿车系列',?,?,'否',123456.78,150000,0,0,0,0,0,0,0,'LSGH','E',?)`,
			code, name, del)
		if err != nil {
			t.Fatalf("seed cartype %s: %v", code, err)
		}
		id, _ := res.LastInsertId()
		return id
	}
	return insert("C1", "在售车型", 0), insert("C9", "停产车型", 1)
}

// The picker offers only current models...
func TestCarTypeListExcludesSoftDeleted(t *testing.T) {
	db := testDB(t)
	repo := NewCarTypeRepo(db)
	live, deleted := seedCarTypes(t, db)

	types, truncated, err := repo.List(context.Background())
	if err != nil {
		t.Fatalf("list: %v", err)
	}
	if truncated {
		t.Error("two rows should not truncate")
	}
	if len(types) != 1 || types[0].UID != live {
		t.Fatalf("got %+v, want only the live type %d", types, live)
	}
	for _, c := range types {
		if c.UID == deleted {
			t.Error("a discontinued model must not be offered for new vehicles")
		}
	}
}

// ...but Get must still resolve them. A vehicle bought three years ago
// references a type that may since have been discontinued; filtering here too
// would blank the field on exactly the historic records people look up most.
func TestCarTypeGetIncludesSoftDeleted(t *testing.T) {
	db := testDB(t)
	repo := NewCarTypeRepo(db)
	_, deleted := seedCarTypes(t, db)

	ct, err := repo.Get(context.Background(), deleted)
	if err != nil {
		t.Fatalf("get: %v", err)
	}
	if ct.CarName != "停产车型" {
		t.Errorf("carname = %q, want 停产车型", ct.CarName)
	}
}

func TestCarTypeGetMissing(t *testing.T) {
	db := testDB(t)
	repo := NewCarTypeRepo(db)
	seedCarTypes(t, db)

	if _, err := repo.Get(context.Background(), 999999); !errors.Is(err, ErrNotFound) {
		t.Fatalf("err = %v, want ErrNotFound", err)
	}
}

// inprice prefills a new vehicle's cost price, so it must survive as an exact
// decimal string. 123456.78 is not representable in binary floating point, so
// a float round-trip anywhere in the path would show up here.
func TestCarTypeInPriceIsAnExactString(t *testing.T) {
	db := testDB(t)
	repo := NewCarTypeRepo(db)
	live, _ := seedCarTypes(t, db)

	ct, err := repo.Get(context.Background(), live)
	if err != nil {
		t.Fatal(err)
	}
	if ct.InPrice != "123456.78" {
		t.Errorf("inprice = %q, want 123456.78", ct.InPrice)
	}
}
