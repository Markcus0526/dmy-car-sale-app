package http

import (
	"encoding/json"
	"io"
	"log/slog"
	"net/http"
	"net/http/httptest"
	"testing"

	"github.com/Markcus0526/carsaleman/internal/platform/config"
)

func testServer() http.Handler {
	return NewServer(
		config.Config{Env: "dev", CORSOrigins: []string{"http://localhost:5173"}},
		slog.New(slog.NewTextHandler(io.Discard, nil)),
	).Handler()
}

func TestHealth(t *testing.T) {
	rec := httptest.NewRecorder()
	testServer().ServeHTTP(rec, httptest.NewRequest(http.MethodGet, "/api/health", nil))

	if rec.Code != http.StatusOK {
		t.Fatalf("status = %d, want 200", rec.Code)
	}
	var body map[string]string
	if err := json.Unmarshal(rec.Body.Bytes(), &body); err != nil {
		t.Fatalf("body is not JSON: %v", err)
	}
	if body["status"] != "ok" {
		t.Errorf("status = %q, want ok", body["status"])
	}
}

// Regression: an unmatched route used to fall through to the stdlib's
// "404 page not found" plaintext. The client parses JSON, so a plaintext body
// degraded every wrong URL to error.UNKNOWN instead of error.NOT_FOUND.
func TestNotFoundReturnsErrorCodeNotProse(t *testing.T) {
	rec := httptest.NewRecorder()
	testServer().ServeHTTP(rec, httptest.NewRequest(http.MethodGet, "/api/does-not-exist", nil))

	if rec.Code != http.StatusNotFound {
		t.Fatalf("status = %d, want 404", rec.Code)
	}
	if ct := rec.Header().Get("Content-Type"); ct != "application/json; charset=utf-8" {
		t.Errorf("Content-Type = %q, want JSON", ct)
	}

	var body struct {
		Code      string `json:"code"`
		RequestID string `json:"requestId"`
	}
	if err := json.Unmarshal(rec.Body.Bytes(), &body); err != nil {
		t.Fatalf("body is not JSON (%q): %v", rec.Body.String(), err)
	}
	if body.Code != "NOT_FOUND" {
		t.Errorf("code = %q, want NOT_FOUND", body.Code)
	}
	if body.RequestID == "" {
		t.Error("requestId should be populated so a user can quote it")
	}
}

func TestRequestIDHeaderAlwaysSet(t *testing.T) {
	rec := httptest.NewRecorder()
	testServer().ServeHTTP(rec, httptest.NewRequest(http.MethodGet, "/api/health", nil))

	if rec.Header().Get("X-Request-Id") == "" {
		t.Error("X-Request-Id must be set on every response")
	}
}

func TestRequestIDIsPropagatedFromClient(t *testing.T) {
	req := httptest.NewRequest(http.MethodGet, "/api/health", nil)
	req.Header.Set("X-Request-Id", "client-supplied-id")

	rec := httptest.NewRecorder()
	testServer().ServeHTTP(rec, req)

	if got := rec.Header().Get("X-Request-Id"); got != "client-supplied-id" {
		t.Errorf("X-Request-Id = %q, want the client's value echoed back", got)
	}
}

func TestMeReturnsMenuAndPermissions(t *testing.T) {
	rec := httptest.NewRecorder()
	testServer().ServeHTTP(rec, httptest.NewRequest(http.MethodGet, "/api/auth/me", nil))

	if rec.Code != http.StatusOK {
		t.Fatalf("status = %d, want 200", rec.Code)
	}

	var body meResponse
	if err := json.Unmarshal(rec.Body.Bytes(), &body); err != nil {
		t.Fatalf("body is not JSON: %v", err)
	}
	if len(body.Menu) == 0 {
		t.Error("menu is empty")
	}
	if len(body.Permissions) != len(allPermissionKeys()) {
		t.Errorf("permissions = %d, want %d", len(body.Permissions), len(allPermissionKeys()))
	}
	for _, n := range body.Menu {
		if n.PermissionKey == "" || n.LabelKey == "" {
			t.Errorf("node %s is missing a key: %+v", n.ID, n)
		}
	}
}

func TestCORSOnlyEchoesAllowedOrigin(t *testing.T) {
	for _, tc := range []struct{ origin, want string }{
		{"http://localhost:5173", "http://localhost:5173"},
		{"https://evil.example", ""},
	} {
		req := httptest.NewRequest(http.MethodGet, "/api/health", nil)
		req.Header.Set("Origin", tc.origin)

		rec := httptest.NewRecorder()
		testServer().ServeHTTP(rec, req)

		if got := rec.Header().Get("Access-Control-Allow-Origin"); got != tc.want {
			t.Errorf("origin %q -> Allow-Origin %q, want %q", tc.origin, got, tc.want)
		}
	}
}
