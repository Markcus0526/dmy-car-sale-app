package http

import (
	"errors"
	"net/http"

	"github.com/Markcus0526/carsaleman/internal/http/apierr"
	storemysql "github.com/Markcus0526/carsaleman/internal/store/mysql"
)

type carTypeListResponse struct {
	Types     []storemysql.CarType `json:"types"`
	Truncated bool                 `json:"truncated"`
	Limit     int                  `json:"limit"`
}

// handleCarTypeList feeds the car-type picker.
//
// Gated on being signed in rather than on 车型价格设置, for the same reason as
// /api/lookup: creating a vehicle needs to NAME a car type, which is not the
// right to administer car types and their prices. The response carries no
// pricing beyond inprice, which the vehicle form prefills anyway.
func (s *Server) handleCarTypeList(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	if s.CarType == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	types, truncated, err := s.CarType.List(r.Context())
	if err != nil {
		s.log.Error("listing car types failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusOK, carTypeListResponse{
		Types: types, Truncated: truncated, Limit: storemysql.MaxCarTypeRows,
	})
}

// handleCarTypeGet resolves a single car type, including soft-deleted ones, so
// an old vehicle's type still renders when its form is opened.
func (s *Server) handleCarTypeGet(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	uid, ok := pathInt64(r, "id")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if s.CarType == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	ct, err := s.CarType.Get(r.Context(), uid)
	if errors.Is(err, storemysql.ErrNotFound) {
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
		return
	}
	if err != nil {
		s.log.Error("reading car type failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusOK, ct)
}

func (s *Server) registerCarType(mux *http.ServeMux) {
	mux.Handle("GET /api/cartypes", s.requireAuth(http.HandlerFunc(s.handleCarTypeList)))
	mux.Handle("GET /api/cartypes/{id}", s.requireAuth(http.HandlerFunc(s.handleCarTypeGet)))
}
