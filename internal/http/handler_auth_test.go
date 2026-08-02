package http

import (
	"context"
	"encoding/json"
	"io"
	"log/slog"
	"net/http"
	"net/http/httptest"
	"strings"
	"testing"
	"time"

	"github.com/Markcus0526/carsaleman/internal/auth"
	"github.com/Markcus0526/carsaleman/internal/platform/config"
)

// bcrypt hash of "s3cret" at cost 4 (bcrypt.MinCost). Verification is
// cost-agnostic, so this is purely a test-speed measure;
// auth.TestHashPasswordUsesProductionCost guards the real setting.
const testBcryptS3cret = "$2a$04$kLr3LXjZLyXKoTmmcgPF7eehBArbC2bB0ahB3aQhVwMH1ET7w5SXK"

// In-memory repositories. The MySQL implementations are exercised separately
// in internal/store/mysql; here the point is the HTTP behaviour around them.

type memUserRepo struct {
	byID   map[int64]*auth.User
	byName map[string]*auth.User
	perms  map[int64]auth.Set
}

func (m *memUserRepo) FindByUsername(_ context.Context, n string) (*auth.User, error) {
	return m.byName[n], nil
}
func (m *memUserRepo) FindByID(_ context.Context, id int64) (*auth.User, error) {
	return m.byID[id], nil
}
func (m *memUserRepo) UpgradePassword(_ context.Context, id int64, hash string) error {
	if u := m.byID[id]; u != nil {
		u.Credential = auth.Credential{Bcrypt: hash}
	}
	return nil
}
func (m *memUserRepo) LoadPermissions(_ context.Context, id int64) (auth.Set, error) {
	if p, ok := m.perms[id]; ok {
		return p, nil
	}
	return auth.Set{}, nil
}

type memSessionRepo struct {
	byHash map[string]*auth.Session
	nextID int64
}

func (m *memSessionRepo) Create(_ context.Context, s auth.Session) (int64, error) {
	m.nextID++
	s.ID = m.nextID
	m.byHash[s.TokenHash] = &s
	return s.ID, nil
}
func (m *memSessionRepo) FindByTokenHash(_ context.Context, h string) (*auth.Session, error) {
	return m.byHash[h], nil
}
func (m *memSessionRepo) Touch(_ context.Context, id int64, t time.Time) error {
	for _, s := range m.byHash {
		if s.ID == id {
			s.LastSeenAt = t
		}
	}
	return nil
}
func (m *memSessionRepo) Delete(_ context.Context, id int64) error {
	for h, s := range m.byHash {
		if s.ID == id {
			delete(m.byHash, h)
		}
	}
	return nil
}
func (m *memSessionRepo) DeleteForUser(_ context.Context, uid int64) error {
	for h, s := range m.byHash {
		if s.UserID == uid {
			delete(m.byHash, h)
		}
	}
	return nil
}
func (m *memSessionRepo) DeleteExpired(context.Context, time.Time) (int64, error) { return 0, nil }

// authedServer wires a server with one user who can read/write store-in and
// read store-out, and nothing else.
func authedServer(t *testing.T, password string) (http.Handler, *memUserRepo) {
	t.Helper()

	// A precomputed cost-4 hash for "s3cret". Hashing at the production cost
	// (12) here costs seconds per server and this package builds one per test;
	// cost does not affect any behaviour under test.
	hash := testBcryptS3cret
	if password != "s3cret" {
		var err error
		if hash, err = auth.HashPassword(password); err != nil {
			t.Fatal(err)
		}
	}
	u := &auth.User{
		ID: 7, Username: "张三", DepartmentCode: "D1", DepartmentName: "销售部",
		Credential: auth.Credential{Bcrypt: hash},
	}
	users := &memUserRepo{
		byID:   map[int64]*auth.User{7: u},
		byName: map[string]*auth.User{"张三": u},
		perms: map[int64]auth.Set{7: {
			auth.PermMovement: auth.LevelReadWrite,
			auth.PermStoreIn:  auth.LevelReadWrite,
			auth.PermStoreOut: auth.LevelReadOnly,
		}},
	}
	log := slog.New(slog.NewTextHandler(io.Discard, nil))
	srv := NewServer(
		config.Config{Env: "dev", CORSOrigins: []string{"http://localhost:5173"}},
		log,
		auth.NewService(users, log),
		Deps{Sessions: auth.NewSessionService(&memSessionRepo{byHash: map[string]*auth.Session{}})},
	)
	return srv.Handler(), users
}

func postJSON(h http.Handler, path, body string, cookies ...*http.Cookie) *httptest.ResponseRecorder {
	req := httptest.NewRequest(http.MethodPost, path, strings.NewReader(body))
	req.Header.Set("Content-Type", "application/json")
	for _, c := range cookies {
		req.AddCookie(c)
	}
	rec := httptest.NewRecorder()
	h.ServeHTTP(rec, req)
	return rec
}

func sessionCookie(t *testing.T, rec *httptest.ResponseRecorder) *http.Cookie {
	t.Helper()
	for _, c := range rec.Result().Cookies() {
		if c.Name == SessionCookieNameDev {
			return c
		}
	}
	t.Fatal("no session cookie was set")
	return nil
}

// ---------------------------------------------------------------------------

func TestLoginSetsHardenedCookieAndReturnsMenu(t *testing.T) {
	h, _ := authedServer(t, "s3cret")

	rec := postJSON(h, "/api/auth/login", `{"username":"张三","password":"s3cret"}`)
	if rec.Code != http.StatusOK {
		t.Fatalf("status = %d, want 200 (body %s)", rec.Code, rec.Body)
	}

	c := sessionCookie(t, rec)
	if !c.HttpOnly {
		t.Error("the session cookie must be HttpOnly — that is why it is a cookie and not a header (D17)")
	}
	if c.SameSite != http.SameSiteLaxMode {
		t.Errorf("SameSite = %v, want Lax", c.SameSite)
	}
	if c.Path != "/" {
		t.Errorf("Path = %q, want /", c.Path)
	}
	if c.Value == "" {
		t.Error("empty session token")
	}

	var body meResponse
	if err := json.Unmarshal(rec.Body.Bytes(), &body); err != nil {
		t.Fatalf("body is not JSON: %v", err)
	}
	if body.Username != "张三" {
		t.Errorf("username = %q", body.Username)
	}
	// Only the permitted branch should appear.
	if len(body.Menu) != 1 || body.Menu[0].ID != "movement" {
		t.Fatalf("menu = %+v, want only the movement section", body.Menu)
	}
	if len(body.Menu[0].Children) != 2 {
		t.Errorf("children = %d, want 2 (storein rw, storeout ro)", len(body.Menu[0].Children))
	}
}

// The response must never leak which half of the credential was wrong.
func TestLoginRejectsBadCredentialsUniformly(t *testing.T) {
	h, _ := authedServer(t, "s3cret")

	for _, body := range []string{
		`{"username":"张三","password":"wrong"}`,
		`{"username":"nobody","password":"s3cret"}`,
	} {
		rec := postJSON(h, "/api/auth/login", body)
		if rec.Code != http.StatusUnauthorized {
			t.Errorf("%s -> status %d, want 401", body, rec.Code)
		}
		var e struct {
			Code string `json:"code"`
		}
		_ = json.Unmarshal(rec.Body.Bytes(), &e)
		if e.Code != "AUTH_INVALID_CREDENTIALS" {
			t.Errorf("%s -> code %q", body, e.Code)
		}
		if len(rec.Result().Cookies()) != 0 {
			t.Error("a failed login must not set a session cookie")
		}
	}
}

func TestLoginValidatesInput(t *testing.T) {
	h, _ := authedServer(t, "s3cret")

	rec := postJSON(h, "/api/auth/login", `{"username":"  ","password":""}`)
	if rec.Code != http.StatusUnprocessableEntity {
		t.Fatalf("status = %d, want 422", rec.Code)
	}
	var e struct {
		Code   string            `json:"code"`
		Fields map[string]string `json:"fields"`
	}
	_ = json.Unmarshal(rec.Body.Bytes(), &e)
	if e.Fields["username"] != "REQUIRED" || e.Fields["password"] != "REQUIRED" {
		t.Errorf("fields = %v, want both REQUIRED", e.Fields)
	}
}

// Unknown fields are rejected rather than ignored, so a client sending
// {"user":...} finds out immediately instead of getting a confusing 401.
func TestLoginRejectsUnknownFields(t *testing.T) {
	h, _ := authedServer(t, "s3cret")

	rec := postJSON(h, "/api/auth/login", `{"username":"张三","password":"s3cret","admin":true}`)
	if rec.Code != http.StatusBadRequest {
		t.Errorf("status = %d, want 400", rec.Code)
	}
}

func TestMeWithValidSession(t *testing.T) {
	h, _ := authedServer(t, "s3cret")
	login := postJSON(h, "/api/auth/login", `{"username":"张三","password":"s3cret"}`)

	req := httptest.NewRequest(http.MethodGet, "/api/auth/me", nil)
	req.AddCookie(sessionCookie(t, login))
	rec := httptest.NewRecorder()
	h.ServeHTTP(rec, req)

	if rec.Code != http.StatusOK {
		t.Fatalf("status = %d, want 200 (body %s)", rec.Code, rec.Body)
	}
	var body meResponse
	_ = json.Unmarshal(rec.Body.Bytes(), &body)
	if body.Username != "张三" {
		t.Errorf("username = %q", body.Username)
	}
}

func TestLogoutRevokesTheSession(t *testing.T) {
	h, _ := authedServer(t, "s3cret")
	login := postJSON(h, "/api/auth/login", `{"username":"张三","password":"s3cret"}`)
	c := sessionCookie(t, login)

	out := postJSON(h, "/api/auth/logout", "", c)
	if out.Code != http.StatusNoContent {
		t.Fatalf("logout status = %d, want 204", out.Code)
	}

	// The cookie is cleared...
	var cleared bool
	for _, rc := range out.Result().Cookies() {
		if rc.Name == SessionCookieNameDev && rc.MaxAge < 0 {
			cleared = true
		}
	}
	if !cleared {
		t.Error("logout must clear the session cookie")
	}

	// ...and, more importantly, the token is dead server-side. Clearing the
	// cookie alone would leave a stolen token valid.
	req := httptest.NewRequest(http.MethodGet, "/api/auth/me", nil)
	req.AddCookie(c)
	rec := httptest.NewRecorder()
	h.ServeHTTP(rec, req)
	if rec.Code != http.StatusUnauthorized {
		t.Errorf("the revoked token still works: status = %d, want 401", rec.Code)
	}
}

// The property D17 exists for: an administrator's change lands on the very
// next request, with no waiting for a token to expire.
func TestPermissionChangeTakesEffectImmediately(t *testing.T) {
	h, users := authedServer(t, "s3cret")
	login := postJSON(h, "/api/auth/login", `{"username":"张三","password":"s3cret"}`)
	c := sessionCookie(t, login)

	// An administrator revokes everything.
	users.perms[7] = auth.Set{}

	req := httptest.NewRequest(http.MethodGet, "/api/auth/me", nil)
	req.AddCookie(c)
	rec := httptest.NewRecorder()
	h.ServeHTTP(rec, req)

	if rec.Code != http.StatusOK {
		t.Fatalf("status = %d", rec.Code)
	}
	var body meResponse
	_ = json.Unmarshal(rec.Body.Bytes(), &body)
	if len(body.Menu) != 0 {
		t.Errorf("menu = %+v, want empty — permissions must not be cached in the session", body.Menu)
	}
}

// A legacy DES credential is upgraded to bcrypt on first successful login.
//
// The ciphertext is a verified vector from internal/auth/password_test.go,
// cross-checked against python-cryptography and openssl. Using a literal keeps
// this test honest: nothing here can agree with a wrong implementation of the
// legacy scheme, because the value was produced outside this codebase.
const legacyCiphertextForPassword = "3G6E0xYbz/9rcBgwNecrDg==" // DES("password")

func TestLoginUpgradesLegacyCredential(t *testing.T) {
	h, users := authedServer(t, "unused")

	users.byID[7].Credential = auth.Credential{Legacy: legacyCiphertextForPassword}

	rec := postJSON(h, "/api/auth/login", `{"username":"张三","password":"password"}`)
	if rec.Code != http.StatusOK {
		t.Fatalf("status = %d, want 200 (body %s)", rec.Code, rec.Body)
	}

	cred := users.byID[7].Credential
	if cred.Legacy != "" {
		t.Error("the legacy credential should have been cleared")
	}
	if cred.Bcrypt == "" {
		t.Fatal("no bcrypt hash was written")
	}
	if ok, _ := auth.Verify("password", cred); !ok {
		t.Error("the upgraded hash does not verify against the original password")
	}
}
