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
		nil, nil, nil,
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

// A server with no database must say so, not panic into a generic 500 and
// look like a bug.
func TestAuthenticatedRouteWithoutDatabase(t *testing.T) {
	rec := httptest.NewRecorder()
	testServer().ServeHTTP(rec, httptest.NewRequest(http.MethodGet, "/api/auth/me", nil))

	if rec.Code != http.StatusServiceUnavailable {
		t.Fatalf("status = %d, want 503", rec.Code)
	}
	var body struct {
		Code string `json:"code"`
	}
	_ = json.Unmarshal(rec.Body.Bytes(), &body)
	if body.Code != "SERVICE_UNAVAILABLE" {
		t.Errorf("code = %q, want SERVICE_UNAVAILABLE", body.Code)
	}
}

// /me is behind requireAuth: no cookie means 401, never a partially populated
// body. This is the §10.8 boundary -- the client's nav gating is convenience.
func TestMeRequiresAuthentication(t *testing.T) {
	h, _ := authedServer(t, "s3cret")
	rec := httptest.NewRecorder()
	h.ServeHTTP(rec, httptest.NewRequest(http.MethodGet, "/api/auth/me", nil))

	if rec.Code != http.StatusUnauthorized {
		t.Fatalf("status = %d, want 401", rec.Code)
	}

	var body struct {
		Code string `json:"code"`
	}
	if err := json.Unmarshal(rec.Body.Bytes(), &body); err != nil {
		t.Fatalf("body is not JSON: %v", err)
	}
	if body.Code != "UNAUTHORIZED" {
		t.Errorf("code = %q, want UNAUTHORIZED", body.Code)
	}
}

// A garbage cookie must be rejected, not treated as absent-and-harmless.
func TestMeRejectsBogusSessionCookie(t *testing.T) {
	h, _ := authedServer(t, "s3cret")
	req := httptest.NewRequest(http.MethodGet, "/api/auth/me", nil)
	req.AddCookie(&http.Cookie{Name: SessionCookieNameDev, Value: "not-a-real-token"})

	rec := httptest.NewRecorder()
	h.ServeHTTP(rec, req)

	if rec.Code != http.StatusUnauthorized {
		t.Errorf("status = %d, want 401", rec.Code)
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
