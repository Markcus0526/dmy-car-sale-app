package http

import (
	"io"
	"log/slog"
	"net/http"
	"strings"
	"testing"

	"github.com/Markcus0526/carsaleman/internal/auth"
	"github.com/Markcus0526/carsaleman/internal/platform/config"
)

// movementServer signs a user in with the given permission set.
func movementServer(t *testing.T, perms auth.Set) (http.Handler, *http.Cookie) {
	t.Helper()

	u := &auth.User{
		ID: 7, Username: "张三", DepartmentCode: "D1", DepartmentName: "销售部",
		Credential: auth.Credential{Bcrypt: testBcryptS3cret},
	}
	users := &memUserRepo{
		byID:   map[int64]*auth.User{7: u},
		byName: map[string]*auth.User{"张三": u},
		perms:  map[int64]auth.Set{7: perms},
	}
	log := slog.New(slog.NewTextHandler(io.Discard, nil))
	srv := NewServer(
		config.Config{Env: "dev"},
		log,
		auth.NewService(users, log),
		Deps{Sessions: auth.NewSessionService(&memSessionRepo{byHash: map[string]*auth.Session{}})},
	)
	h := srv.Handler()

	rec := postJSON(h, "/api/auth/login", `{"username":"张三","password":"s3cret"}`)
	if rec.Code != http.StatusOK {
		t.Fatalf("login failed: %d %s", rec.Code, rec.Body)
	}
	return h, sessionCookie(t, rec)
}

const validStoreIn = `{"onroadid":1,"storeplace":"一号库","indate":"2026-08-01",
	"inprice":"100000.00","inpath":"厂家直发","intype":"现车",
	"factoryoutdate":"2026-07-20","settlementname":"甲","handlername":"乙"}`

// Each transition is gated on its OWN permission. A user who may store vehicles
// in has no business dispatching them -- that separation is the point of having
// three permissions rather than one "movement" one.
func TestMovementTransitionsAreSeparatelyGated(t *testing.T) {
	h, c := movementServer(t, auth.Set{
		auth.PermStoreIn:     auth.LevelReadWrite,
		auth.PermStoreChange: auth.LevelUnavailable,
		auth.PermStoreOut:    auth.LevelReadOnly, // read-only is not write
	})

	// Permitted: reaches the handler, which has no service (503).
	if rec := do(h, "POST", "/api/movement/storein", validStoreIn, c); rec.Code != http.StatusServiceUnavailable {
		t.Errorf("store-in: status = %d, want 503 (i.e. permitted)", rec.Code)
	}

	// Refused.
	for _, tc := range []struct{ path, body string }{
		{"/api/movement/transfer", `{"onroadid":1,"storeplace":"二号库","settlementname":"甲","handlername":"乙"}`},
		{"/api/movement/storeout", `{"onroadid":1,"outbillno":"O1","salecompany":"某公司","salekind":"零售","settlementname":"甲","handlername":"乙","saleplace":"沈阳","incarkind":"现车","outdate":"2026-08-05","customername":"丙","customerjobkind":"个体","saleregion":"辽宁","carno":"辽A1","outprice":"150000.00"}`},
	} {
		if rec := do(h, "POST", tc.path, tc.body, c); rec.Code != http.StatusForbidden {
			t.Errorf("%s: status = %d, want 403", tc.path, rec.Code)
		}
	}
}

func TestStoreInValidatesRequiredFields(t *testing.T) {
	h, c := movementServer(t, auth.Set{auth.PermStoreIn: auth.LevelReadWrite})

	rec := do(h, "POST", "/api/movement/storein", `{"onroadid":0}`, c)
	if rec.Code != http.StatusUnprocessableEntity {
		t.Fatalf("status = %d, want 422 (body %s)", rec.Code, rec.Body)
	}
	body := rec.Body.String()
	for _, want := range []string{
		`"onroadid":"REQUIRED"`, `"storeplace":"REQUIRED"`, `"indate":"REQUIRED"`,
		`"inpath":"REQUIRED"`, `"settlementname":"REQUIRED"`, `"handlername":"REQUIRED"`,
	} {
		if !strings.Contains(body, want) {
			t.Errorf("body %s missing %s", body, want)
		}
	}
}

// inprice is NOT NULL on tbl_storein, unlike tbl_onroad's. A blank must be a
// field error, not a NULL the driver rejects as a 500.
func TestStoreInRequiresACostPrice(t *testing.T) {
	h, c := movementServer(t, auth.Set{auth.PermStoreIn: auth.LevelReadWrite})

	body := strings.Replace(validStoreIn, `"inprice":"100000.00",`, `"inprice":"",`, 1)
	rec := do(h, "POST", "/api/movement/storein", body, c)
	if rec.Code != http.StatusUnprocessableEntity {
		t.Fatalf("status = %d, want 422", rec.Code)
	}
	if !strings.Contains(rec.Body.String(), `"inprice":"REQUIRED"`) {
		t.Errorf("body %s, want inprice REQUIRED", rec.Body)
	}
}

func TestStoreOutRequiresASalePrice(t *testing.T) {
	h, c := movementServer(t, auth.Set{auth.PermStoreOut: auth.LevelReadWrite})

	body := `{"onroadid":1,"outbillno":"O1","salecompany":"某公司","salekind":"零售",
		"settlementname":"甲","handlername":"乙","saleplace":"沈阳","incarkind":"现车",
		"outdate":"2026-08-05","customername":"丙","customerjobkind":"个体",
		"saleregion":"辽宁","carno":"辽A1"}`
	rec := do(h, "POST", "/api/movement/storeout", body, c)
	if rec.Code != http.StatusUnprocessableEntity {
		t.Fatalf("status = %d, want 422", rec.Code)
	}
	if !strings.Contains(rec.Body.String(), `"outprice":"REQUIRED"`) {
		t.Errorf("body %s, want outprice REQUIRED", rec.Body)
	}
}

// History spans all three transitions, so it is gated on the vehicle screen's
// own read permission rather than on any single movement right. Requiring all
// three would hide a car's record from the people who look after it.
func TestHistoryUsesTheVehiclePermission(t *testing.T) {
	h, c := movementServer(t, auth.Set{auth.PermOnRoad: auth.LevelReadOnly})
	if rec := do(h, "GET", "/api/movement/history/1", "", c); rec.Code != http.StatusServiceUnavailable {
		t.Errorf("status = %d, want 503 (i.e. permitted)", rec.Code)
	}

	h2, c2 := movementServer(t, auth.Set{auth.PermStoreIn: auth.LevelReadWrite})
	if rec := do(h2, "GET", "/api/movement/history/1", "", c2); rec.Code != http.StatusForbidden {
		t.Errorf("without the vehicle permission: status = %d, want 403", rec.Code)
	}
}

// Minting a batch number is a write-side operation: it is only useful when
// filling in a store-in, and a read-only user has no store-in to fill.
func TestBatchNoRequiresWritePermission(t *testing.T) {
	h, c := movementServer(t, auth.Set{auth.PermStoreIn: auth.LevelReadOnly})
	if rec := do(h, "GET", "/api/movement/batchno", "", c); rec.Code != http.StatusForbidden {
		t.Errorf("status = %d, want 403", rec.Code)
	}
}
