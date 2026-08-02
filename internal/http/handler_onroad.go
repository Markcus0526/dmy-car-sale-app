package http

import (
	"errors"
	"net/http"

	"github.com/Markcus0526/carsaleman/internal/auth"
	"github.com/Markcus0526/carsaleman/internal/http/apierr"
	"github.com/Markcus0526/carsaleman/internal/query"
	storemysql "github.com/Markcus0526/carsaleman/internal/store/mysql"
)

type listRequest struct {
	Filter query.Filter `json:"filter"`
}

type onRoadListResponse struct {
	Rows []storemysql.OnRoad `json:"rows"`
	// Truncated tells the client the server capped the result. Surfaced rather
	// than hidden: a silently capped list reads as "there are only this many",
	// which is how people draw wrong conclusions from a screen.
	Truncated bool `json:"truncated"`
	Limit     int  `json:"limit"`
}

// handleOnRoadList is POST, not GET.
//
// The filter is a structured object; encoding it into a query string would
// mean inventing a serialisation and parsing it back, which is exactly the
// string-munging this replaces (§2.6, §10.6). It is a read, so it stays
// side-effect free and is gated on read permission.
func (s *Server) handleOnRoadList(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())

	if s.OnRoad == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	var req listRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	rows, truncated, err := s.OnRoad.List(r.Context(), req.Filter)
	if err != nil {
		var invalid *query.ErrInvalidFilter
		if errors.As(err, &invalid) {
			// A bad filter is the client's mistake, not a server fault. The
			// field name goes back so the UI can mark the offending row.
			fields := map[string]string{}
			if invalid.Field != "" {
				fields[invalid.Field] = "INVALID_FORMAT"
			}
			apierr.Write(w, apierr.CodeValidationFailed, reqID, fields)
			return
		}
		s.log.Error("listing on-road vehicles failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}

	if rows == nil {
		rows = []storemysql.OnRoad{} // [] not null, so the client never branches on nil
	}
	writeJSON(w, http.StatusOK, onRoadListResponse{
		Rows: rows, Truncated: truncated, Limit: storemysql.MaxListRows,
	})
}

// onRoadRoutes registers the slice-4 endpoints.
//
// requirePermission is the §10.8 fix: the legacy app only greyed out menu
// items, so anyone who could reach the network could issue the operation.
func (s *Server) registerOnRoad(mux *http.ServeMux) {
	// Reads need 只读 or better.
	mux.Handle("POST /api/onroad/list",
		s.requireAuth(s.requirePermission(auth.PermOnRoad, false,
			http.HandlerFunc(s.handleOnRoadList))))
	mux.Handle("GET /api/onroad/{id}",
		s.requireAuth(s.requirePermission(auth.PermOnRoad, false,
			http.HandlerFunc(s.handleOnRoadGet))))

	// Writes need 读写. The distinction is enforced here, not in the UI: a
	// read-only user's browser can still issue a POST.
	mux.Handle("POST /api/onroad",
		s.requireAuth(s.requirePermission(auth.PermOnRoad, true,
			http.HandlerFunc(s.handleOnRoadCreate))))
	mux.Handle("PATCH /api/onroad/{id}",
		s.requireAuth(s.requirePermission(auth.PermOnRoad, true,
			http.HandlerFunc(s.handleOnRoadUpdate))))
}
