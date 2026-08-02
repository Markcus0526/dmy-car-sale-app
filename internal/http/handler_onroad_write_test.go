package http

import (
	"io"
	"log/slog"
	"net/http"
	"net/http/httptest"
	"strings"
	"testing"

	"github.com/Markcus0526/carsaleman/internal/auth"
	"github.com/Markcus0526/carsaleman/internal/platform/config"
)

// onRoadServer builds a signed-in session holding the given level on the
// on-road screen. The repository is nil on purpose: this exercises the
// permission boundary, which sits in middleware ahead of any handler, so a
// caller that clears the gate lands on SERVICE_UNAVAILABLE rather than 403.
// That difference is exactly what makes the assertion meaningful.
func onRoadServer(t *testing.T, level auth.Level) (http.Handler, *http.Cookie) {
	t.Helper()

	u := &auth.User{
		ID: 7, Username: "张三", DepartmentCode: "D1", DepartmentName: "销售部",
		Credential: auth.Credential{Bcrypt: testBcryptS3cret},
	}
	users := &memUserRepo{
		byID:   map[int64]*auth.User{7: u},
		byName: map[string]*auth.User{"张三": u},
		perms:  map[int64]auth.Set{7: {auth.PermOnRoad: level}},
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

func do(h http.Handler, method, path, body string, c *http.Cookie) *httptest.ResponseRecorder {
	req := httptest.NewRequest(method, path, strings.NewReader(body))
	req.Header.Set("Content-Type", "application/json")
	if c != nil {
		req.AddCookie(c)
	}
	rec := httptest.NewRecorder()
	h.ServeHTTP(rec, req)
	return rec
}

const validForm = `{"billno":"B1","billdate":"2026-08-01","vin":"V1",
	"engineno":"E1","cartypeid":3,"cartype":"C1","rowVersion":1}`

// A read-only user can list and read, and cannot write. The legacy app made
// this distinction in the menu only (§10.8); here it is a route-level check,
// because a browser can issue a POST whatever the menu looks like.
func TestOnRoadReadOnlyUserCannotWrite(t *testing.T) {
	h, c := onRoadServer(t, auth.LevelReadOnly)

	for _, tc := range []struct{ method, path, body string }{
		{"POST", "/api/onroad", validForm},
		{"PATCH", "/api/onroad/1", validForm},
	} {
		rec := do(h, tc.method, tc.path, tc.body, c)
		if rec.Code != http.StatusForbidden {
			t.Errorf("%s %s: status = %d, want 403", tc.method, tc.path, rec.Code)
		}
	}

	// The same user's reads still work — 503 means they passed the gate and
	// reached the handler, which has no database in this test.
	if rec := do(h, "GET", "/api/onroad/1", "", c); rec.Code != http.StatusServiceUnavailable {
		t.Errorf("read: status = %d, want 503 (i.e. permitted)", rec.Code)
	}
}

func TestOnRoadWriteUserPassesTheGate(t *testing.T) {
	h, c := onRoadServer(t, auth.LevelReadWrite)

	for _, tc := range []struct{ method, path string }{
		{"POST", "/api/onroad"},
		{"PATCH", "/api/onroad/1"},
	} {
		rec := do(h, tc.method, tc.path, validForm, c)
		if rec.Code != http.StatusServiceUnavailable {
			t.Errorf("%s %s: status = %d, want 503 (i.e. permitted)", tc.method, tc.path, rec.Code)
		}
	}
}

func TestOnRoadWriteRequiresAuth(t *testing.T) {
	h, _ := onRoadServer(t, auth.LevelReadWrite)
	if rec := do(h, "POST", "/api/onroad", validForm, nil); rec.Code != http.StatusUnauthorized {
		t.Errorf("status = %d, want 401", rec.Code)
	}
}

// A form failing validation must be rejected as 422 with field codes, not
// waved through to the database. This is the one write path where the nil
// repository is not reached, so it can be asserted without one.
func TestOnRoadCreateRejectsInvalidForm(t *testing.T) {
	h, c := onRoadServer(t, auth.LevelReadWrite)

	rec := do(h, "POST", "/api/onroad", `{"billno":"","vin":"","cartypeid":0}`, c)
	if rec.Code != http.StatusUnprocessableEntity {
		t.Fatalf("status = %d, want 422 (body %s)", rec.Code, rec.Body)
	}
	body := rec.Body.String()
	for _, want := range []string{`"VALIDATION_FAILED"`, `"billno":"REQUIRED"`, `"cartypeid":"REQUIRED"`} {
		if !strings.Contains(body, want) {
			t.Errorf("body %s does not contain %s", body, want)
		}
	}
	// Codes, not prose. If a translated message ever appears here the backend
	// has taken on a locale, which is the thing the code contract prevents.
	if strings.Contains(body, "required") || strings.Contains(body, "必填") {
		t.Errorf("body carries human prose, not codes: %s", body)
	}
}

// rowVersion is mandatory on update. Treating a missing value as "no opinion"
// and writing anyway would make the lost-update guard opt-in.
func TestOnRoadUpdateRejectsMissingRowVersion(t *testing.T) {
	h, c := onRoadServer(t, auth.LevelReadWrite)

	body := `{"billno":"B1","billdate":"2026-08-01","vin":"V1",
		"engineno":"E1","cartypeid":3,"cartype":"C1"}` // no rowVersion
	if rec := do(h, "PATCH", "/api/onroad/1", body, c); rec.Code != http.StatusBadRequest {
		t.Errorf("status = %d, want 400", rec.Code)
	}
}

func TestOnRoadRejectsNonNumericID(t *testing.T) {
	h, c := onRoadServer(t, auth.LevelReadWrite)
	if rec := do(h, "GET", "/api/onroad/abc", "", c); rec.Code != http.StatusBadRequest {
		t.Errorf("status = %d, want 400", rec.Code)
	}
}
