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

// baseDataServer signs a user in holding `level` on 基础信息 and nothing else.
// The repository is nil, so a caller that clears the permission gate lands on
// 503 -- which is what distinguishes "permitted" from "forbidden" here.
func baseDataServer(t *testing.T, level auth.Level) (http.Handler, *http.Cookie) {
	t.Helper()

	u := &auth.User{
		ID: 7, Username: "张三", DepartmentCode: "D1", DepartmentName: "销售部",
		Credential: auth.Credential{Bcrypt: testBcryptS3cret},
	}
	users := &memUserRepo{
		byID:   map[int64]*auth.User{7: u},
		byName: map[string]*auth.User{"张三": u},
		perms:  map[int64]auth.Set{7: {auth.PermBaseData: level}},
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

func TestBaseDataReadOnlyUserCannotWrite(t *testing.T) {
	h, c := baseDataServer(t, auth.LevelReadOnly)

	writes := []struct{ method, path, body string }{
		{"POST", "/api/basedata/domains", `{"name":"地区","type":1}`},
		{"PATCH", "/api/basedata/domains/%E5%9C%B0%E5%8C%BA", `{"name":"区域"}`},
		{"DELETE", "/api/basedata/domains/%E5%9C%B0%E5%8C%BA", ""},
		{"POST", "/api/basedata/domains/%E5%9C%B0%E5%8C%BA/values", `{"value":"沈阳"}`},
		{"PATCH", "/api/basedata/values/1", `{"value":"沈阳","rowVersion":1}`},
		{"DELETE", "/api/basedata/values/1", ""},
		{"POST", "/api/basedata/domains/%E5%9C%B0%E5%8C%BA/import", `{"content":"沈阳"}`},
	}
	for _, w := range writes {
		if rec := do(h, w.method, w.path, w.body, c); rec.Code != http.StatusForbidden {
			t.Errorf("%s %s: status = %d, want 403", w.method, w.path, rec.Code)
		}
	}

	// Reads are permitted: 503 means the request reached a handler with no
	// database behind it, not that it was refused.
	for _, p := range []string{
		"/api/basedata/domains",
		"/api/basedata/domains/%E5%9C%B0%E5%8C%BA/values",
	} {
		if rec := do(h, "GET", p, "", c); rec.Code != http.StatusServiceUnavailable {
			t.Errorf("GET %s: status = %d, want 503 (i.e. permitted)", p, rec.Code)
		}
	}
}

// The lookup endpoint is gated on being signed in, NOT on 基础信息. Filling in
// a sale needs the 地区 list; it does not need the right to administer
// reference data. Gating it on the admin permission would force every clerk to
// hold one, which is how over-broad grants happen.
func TestLookupNeedsOnlyASession(t *testing.T) {
	// 不可用 on 基础信息 — no reference-data rights whatsoever.
	h, c := baseDataServer(t, auth.LevelUnavailable)

	if rec := do(h, "GET", "/api/basedata/domains", "", c); rec.Code != http.StatusForbidden {
		t.Fatalf("admin list: status = %d, want 403", rec.Code)
	}
	if rec := do(h, "GET", "/api/lookup/%E5%9C%B0%E5%8C%BA", "", c); rec.Code != http.StatusServiceUnavailable {
		t.Errorf("lookup: status = %d, want 503 (i.e. permitted)", rec.Code)
	}
}

func TestLookupStillRequiresASession(t *testing.T) {
	h, _ := baseDataServer(t, auth.LevelReadWrite)
	if rec := do(h, "GET", "/api/lookup/%E5%9C%B0%E5%8C%BA", "", nil); rec.Code != http.StatusUnauthorized {
		t.Errorf("status = %d, want 401", rec.Code)
	}
}

// `type` is a per-domain shape flag with exactly two legal values (Q4). A
// third would create a domain no screen knows how to render.
func TestCreateDomainRejectsUnknownType(t *testing.T) {
	h, c := baseDataServer(t, auth.LevelReadWrite)

	for _, body := range []string{
		`{"name":"地区","type":0}`,
		`{"name":"地区","type":3}`,
		`{"name":"地区"}`,
	} {
		rec := do(h, "POST", "/api/basedata/domains", body, c)
		if rec.Code != http.StatusUnprocessableEntity {
			t.Errorf("%s: status = %d, want 422", body, rec.Code)
		}
		if !strings.Contains(rec.Body.String(), `"type":"INVALID_FORMAT"`) {
			t.Errorf("%s: body %s does not mark the type field", body, rec.Body)
		}
	}
}

func TestCreateDomainRequiresName(t *testing.T) {
	h, c := baseDataServer(t, auth.LevelReadWrite)

	rec := do(h, "POST", "/api/basedata/domains", `{"name":"   ","type":1}`, c)
	if rec.Code != http.StatusUnprocessableEntity {
		t.Fatalf("status = %d, want 422", rec.Code)
	}
	if !strings.Contains(rec.Body.String(), `"name":"REQUIRED"`) {
		t.Errorf("body %s, want name REQUIRED", rec.Body)
	}
}

// Domain names are Chinese and travel percent-encoded. A name that decodes to
// something longer than VARCHAR(50) would fail as a driver error on write and
// silently match nothing on read, so it is rejected at the edge.
func TestDomainNameLengthIsCheckedAfterDecoding(t *testing.T) {
	h, c := baseDataServer(t, auth.LevelReadWrite)

	long := strings.Repeat("%E5%9C%B0", 51) // 51 Chinese characters
	if rec := do(h, "DELETE", "/api/basedata/domains/"+long, "", c); rec.Code != http.StatusBadRequest {
		t.Errorf("status = %d, want 400", rec.Code)
	}

	// A 50-character name is legal, so it must reach the handler (503).
	ok := strings.Repeat("%E5%9C%B0", 50)
	if rec := do(h, "DELETE", "/api/basedata/domains/"+ok, "", c); rec.Code != http.StatusServiceUnavailable {
		t.Errorf("50-char name: status = %d, want 503 (i.e. accepted)", rec.Code)
	}
}

func TestBaseDataUpdateValueRequiresRowVersion(t *testing.T) {
	h, c := baseDataServer(t, auth.LevelReadWrite)

	if rec := do(h, "PATCH", "/api/basedata/values/1", `{"value":"沈阳"}`, c); rec.Code != http.StatusBadRequest {
		t.Errorf("status = %d, want 400", rec.Code)
	}
}
