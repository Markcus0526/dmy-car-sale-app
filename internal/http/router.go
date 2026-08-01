// Package http wires routes and HTTP concerns.
//
// Routing uses the Go 1.22 stdlib ServeMux (method+path patterns) so the
// skeleton builds with zero third-party dependencies. Swap in go-chi/chi at
// Phase 1 day 3 when sub-routers and richer middleware are needed
// (GO_MIGRATION_PLAN.md 3.1) -- the handler signatures do not change.
package http

import (
	"encoding/json"
	"log/slog"
	"net/http"

	"github.com/Markcus0526/carsaleman/internal/auth"
	"github.com/Markcus0526/carsaleman/internal/http/apierr"
	"github.com/Markcus0526/carsaleman/internal/platform/config"
)

type Server struct {
	cfg      config.Config
	log      *slog.Logger
	auth     *auth.Service
	sessions *auth.SessionService
}

// NewServer wires the HTTP layer.
//
// auth and sessions may be nil in tests that only exercise unauthenticated
// routes; every handler that needs them sits behind requireAuth.
func NewServer(cfg config.Config, log *slog.Logger, authSvc *auth.Service, sessions *auth.SessionService) *Server {
	return &Server{cfg: cfg, log: log, auth: authSvc, sessions: sessions}
}

// Handler returns the fully wired root handler.
func (s *Server) Handler() http.Handler {
	mux := http.NewServeMux()

	mux.HandleFunc("GET /api/health", s.handleHealth)

	// Unauthenticated.
	mux.HandleFunc("POST /api/auth/login", s.handleLogin)
	mux.HandleFunc("POST /api/auth/logout", s.handleLogout)

	// Authenticated. Every route beyond this point carries a real session;
	// nav gating on the client is convenience, this is the boundary (§10.8).
	mux.Handle("GET /api/auth/me", s.requireAuth(http.HandlerFunc(s.handleMe)))

	// Catch-all. Without it, unmatched routes fall through to the stdlib's
	// "404 page not found" plaintext, the client's JSON parse fails, and every
	// wrong URL surfaces as error.UNKNOWN instead of error.NOT_FOUND. The
	// code-not-prose contract has to hold for misses too, not just for hits.
	mux.HandleFunc("/", s.handleNotFound)

	return chain(mux,
		requestID,
		recoverPanic(s.log),
		accessLog(s.log),
		cors(s.cfg.CORSOrigins),
	)
}

func writeJSON(w http.ResponseWriter, status int, v any) {
	w.Header().Set("Content-Type", "application/json; charset=utf-8")
	w.WriteHeader(status)
	_ = json.NewEncoder(w).Encode(v)
}

func (s *Server) handleHealth(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, map[string]string{"status": "ok"})
}

func (s *Server) handleNotFound(w http.ResponseWriter, r *http.Request) {
	apierr.Write(w, apierr.CodeNotFound, RequestIDFrom(r.Context()), nil)
}

// meResponse drives client-side nav gating.
//
// Nav gating here is convenience only. The real check is server-side, per
// route -- 10.8 records that the legacy app greyed out menu items and enforced
// nothing, which was not a security boundary.
type meResponse struct {
	Username    string            `json:"username"`
	DisplayName string            `json:"displayName"`
	Permissions map[string]string `json:"permissions"`
	Menu        []MenuNode        `json:"menu"`
}

// handleMe returns the current user, permissions and filtered menu.
//
// Sits behind requireAuth, so the context always carries a caller.
func (s *Server) handleMe(w http.ResponseWriter, r *http.Request) {
	a, ok := UserFrom(r.Context())
	if !ok {
		apierr.Write(w, apierr.CodeUnauthorized, RequestIDFrom(r.Context()), nil)
		return
	}

	user, err := s.auth.UserByID(r.Context(), a.UserID)
	if err != nil {
		s.log.Error("loading user failed", "err", err, "request_id", RequestIDFrom(r.Context()))
		apierr.Write(w, apierr.CodeInternal, RequestIDFrom(r.Context()), nil)
		return
	}
	writeJSON(w, http.StatusOK, s.meBody(user, a.Permissions))
}

// allPermissionKeys walks the menu tree collecting every permission key.
func allPermissionKeys() []string {
	var keys []string
	var walk func([]MenuNode)
	walk = func(nodes []MenuNode) {
		for _, n := range nodes {
			keys = append(keys, n.PermissionKey)
			walk(n.Children)
		}
	}
	walk(menuTree)
	return keys
}
