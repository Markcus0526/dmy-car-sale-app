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

func quarterServer(t *testing.T, level auth.Level) (http.Handler, *http.Cookie) {
	t.Helper()

	u := &auth.User{
		ID: 7, Username: "张三", DepartmentCode: "D1", DepartmentName: "销售部",
		Credential: auth.Credential{Bcrypt: testBcryptS3cret},
	}
	users := &memUserRepo{
		byID:   map[int64]*auth.User{7: u},
		byName: map[string]*auth.User{"张三": u},
		perms:  map[int64]auth.Set{7: {auth.PermQuarterTarget: level}},
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

// A read-only user sees the grid and cannot write it. The grid is a planning
// document -- targets are set by management, and reading them is not the same
// right as changing them.
func TestQuarterStatsReadOnlyCannotSave(t *testing.T) {
	h, c := quarterServer(t, auth.LevelReadOnly)

	if rec := do(h, "GET", "/api/quarterstats?year=2026&quarter=3", "", c); rec.Code != http.StatusServiceUnavailable {
		t.Errorf("read: status = %d, want 503 (i.e. permitted)", rec.Code)
	}
	body := `{"general":[{"carseries":"轿车系列","target":10}],"special":[]}`
	if rec := do(h, "PUT", "/api/quarterstats?year=2026&quarter=3", body, c); rec.Code != http.StatusForbidden {
		t.Errorf("write: status = %d, want 403", rec.Code)
	}
}

// year and quarter are bounded at the edge. An out-of-range quarter would
// otherwise reach the repository and build a column name like `total7`, which
// fails as a driver error -- a 500 where the answer is 400.
func TestQuarterStatsRejectsBadYearOrQuarter(t *testing.T) {
	h, c := quarterServer(t, auth.LevelReadWrite)

	for _, qs := range []string{
		"?year=2026&quarter=0",
		"?year=2026&quarter=5",
		"?year=2026",
		"?quarter=3",
		"?year=1800&quarter=1",
		"?year=abc&quarter=1",
	} {
		if rec := do(h, "GET", "/api/quarterstats"+qs, "", c); rec.Code != http.StatusBadRequest {
			t.Errorf("GET %s: status = %d, want 400", qs, rec.Code)
		}
	}
	// A legal pair must get through to the handler (503, no database here).
	if rec := do(h, "GET", "/api/quarterstats?year=2026&quarter=4", "", c); rec.Code != http.StatusServiceUnavailable {
		t.Errorf("valid pair: status = %d, want 503 (i.e. accepted)", rec.Code)
	}
}

// These are vehicle counts. A negative one is a typo, not a business case, and
// it would corrupt every subtotal and percentage on the grid.
func TestQuarterStatsRejectsNegativeCounts(t *testing.T) {
	h, c := quarterServer(t, auth.LevelReadWrite)

	body := `{"general":[{"carseries":"轿车系列","target":10,"month1":-1}],"special":[]}`
	rec := do(h, "PUT", "/api/quarterstats?year=2026&quarter=3", body, c)
	if rec.Code != http.StatusUnprocessableEntity {
		t.Fatalf("status = %d, want 422 (body %s)", rec.Code, rec.Body)
	}
	if !strings.Contains(rec.Body.String(), "INVALID_FORMAT") {
		t.Errorf("body %s, want an INVALID_FORMAT field code", rec.Body)
	}
}

func TestQuarterStatsRequiresCarSeries(t *testing.T) {
	h, c := quarterServer(t, auth.LevelReadWrite)

	body := `{"general":[{"carseries":"","target":10}],"special":[]}`
	rec := do(h, "PUT", "/api/quarterstats?year=2026&quarter=3", body, c)
	if rec.Code != http.StatusUnprocessableEntity {
		t.Fatalf("status = %d, want 422", rec.Code)
	}
	if !strings.Contains(rec.Body.String(), "REQUIRED") {
		t.Errorf("body %s, want a REQUIRED field code", rec.Body)
	}
}

// Validation runs before the availability check, so a malformed request is not
// answered with 503 -- which would invite a retry that can never succeed.
func TestQuarterStatsValidatesBeforeCheckingAvailability(t *testing.T) {
	h, c := quarterServer(t, auth.LevelReadWrite)

	body := `{"general":[{"carseries":"","target":-5}],"special":[]}`
	if rec := do(h, "PUT", "/api/quarterstats?year=2026&quarter=3", body, c); rec.Code != http.StatusUnprocessableEntity {
		t.Errorf("status = %d, want 422 rather than 503", rec.Code)
	}
}
