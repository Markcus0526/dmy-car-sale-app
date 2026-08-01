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
	cfg config.Config
	log *slog.Logger
}

func NewServer(cfg config.Config, log *slog.Logger) *Server {
	return &Server{cfg: cfg, log: log}
}

// Handler returns the fully wired root handler.
func (s *Server) Handler() http.Handler {
	mux := http.NewServeMux()

	mux.HandleFunc("GET /api/health", s.handleHealth)
	mux.HandleFunc("GET /api/auth/me", s.handleMe)

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
// STUB: until slice 1 (auth) lands there is no session and no tbl_userinfo,
// so this grants every permission at 读写 in dev. It exists to prove the
// permission-key/label-key separation end to end.
func (s *Server) handleMe(w http.ResponseWriter, r *http.Request) {
	perms := auth.Set{}
	for _, key := range allPermissionKeys() {
		perms[key] = auth.LevelReadWrite
	}

	out := make(map[string]string, len(perms))
	for k, v := range perms {
		out[k] = string(v)
	}

	writeJSON(w, http.StatusOK, meResponse{
		Username:    "dev",
		DisplayName: "Developer",
		Permissions: out,
		Menu:        filterMenu(menuTree, perms),
	})
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
