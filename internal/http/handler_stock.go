package http

import (
	"errors"
	"net/http"

	"github.com/Markcus0526/carsaleman/internal/auth"
	"github.com/Markcus0526/carsaleman/internal/http/apierr"
	"github.com/Markcus0526/carsaleman/internal/query"
	storemysql "github.com/Markcus0526/carsaleman/internal/store/mysql"
)

// The two stock list screens. POST, not GET, for the same reason as
// /api/onroad/list: the filter is a structured object, and encoding it into a
// query string means inventing a serialisation and parsing it back.

// writeFilterError is the shared translation for a rejected filter. Every list
// screen needs it, and three copies would drift.
func (s *Server) writeFilterError(w http.ResponseWriter, reqID string, err error, op string) {
	var invalid *query.ErrInvalidFilter
	if errors.As(err, &invalid) {
		fields := map[string]string{}
		if invalid.Field != "" {
			fields[invalid.Field] = "INVALID_FORMAT"
		}
		apierr.Write(w, apierr.CodeValidationFailed, reqID, fields)
		return
	}
	s.log.Error(op+" failed", "err", err, "request_id", reqID)
	apierr.Write(w, apierr.CodeInternal, reqID, nil)
}

func (s *Server) handleStoreInList(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	if s.StoreInList == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	var req listRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	rows, truncated, err := s.StoreInList.List(r.Context(), req.Filter)
	if err != nil {
		s.writeFilterError(w, reqID, err, "listing stock")
		return
	}
	writeJSON(w, http.StatusOK, map[string]any{
		"rows": rows, "truncated": truncated, "limit": storemysql.MaxListRows,
	})
}

func (s *Server) handleStoreOutList(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	if s.StoreOutList == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	var req listRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	rows, truncated, err := s.StoreOutList.List(r.Context(), req.Filter)
	if err != nil {
		s.writeFilterError(w, reqID, err, "listing dispatches")
		return
	}
	writeJSON(w, http.StatusOK, map[string]any{
		"rows": rows, "truncated": truncated, "limit": storemysql.MaxListRows,
	})
}

func (s *Server) registerStock(mux *http.ServeMux) {
	mux.Handle("POST /api/storein/list",
		s.requireAuth(s.requirePermission(auth.PermStoreIn, false,
			http.HandlerFunc(s.handleStoreInList))))
	mux.Handle("POST /api/storeout/list",
		s.requireAuth(s.requirePermission(auth.PermStoreOut, false,
			http.HandlerFunc(s.handleStoreOutList))))
}
