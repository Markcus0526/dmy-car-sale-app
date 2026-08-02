package http

import (
	"errors"
	"net/http"

	"github.com/Markcus0526/carsaleman/internal/auth"
	"github.com/Markcus0526/carsaleman/internal/http/apierr"
	storemysql "github.com/Markcus0526/carsaleman/internal/store/mysql"
)

// Slice 8's remaining screens: 特种车统计表 and 赠送装修.

func (s *Server) handleSpecCarList(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	if s.SpecCar == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	var req listRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	rows, truncated, err := s.SpecCar.List(r.Context(), req.Filter)
	if err != nil {
		s.writeFilterError(w, reqID, err, "listing special vehicles")
		return
	}
	writeJSON(w, http.StatusOK, map[string]any{
		"rows": rows, "truncated": truncated, "limit": storemysql.MaxListRows,
		// The selector is sent to the client rather than hardcoded there:
		// FrmSpecCar identifies special vehicles by salekind = '大客户', and
		// that is a stored data value, not a display string. If the Phase 0
		// dump shows the view already filters, this is where it changes.
		"specialSaleKind": storemysql.SpecialSaleKind,
	})
}

// ---------------------------------------------------------------------------
// 赠送装修 (tbl_fit)

func (s *Server) handleFitList(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	if s.Fit == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	var req listRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	rows, truncated, err := s.Fit.List(r.Context(), req.Filter)
	if err != nil {
		s.writeFilterError(w, reqID, err, "listing fit-outs")
		return
	}
	writeJSON(w, http.StatusOK, map[string]any{
		"rows": rows, "truncated": truncated, "limit": storemysql.MaxListRows,
	})
}

type fitForm struct {
	Consumer   string `json:"consumer"`
	Seller     string `json:"seller"`
	FitDate    string `json:"fitdate"`
	FitPrice   string `json:"fitprice"`
	Remark     string `json:"remark"`
	RowVersion int    `json:"rowVersion"`
}

// validate. Only `consumer` is NOT NULL in tbl_fit; the rest are nullable, and
// a blank date or price must become NULL rather than a zero — "no charge
// recorded" and "free" are different facts.
func (f fitForm) validate() (consumer, seller, remark string, date, price *string, fields map[string]string) {
	v := newValidator()
	consumer = v.required("consumer", f.Consumer, 50)
	seller = v.optional("seller", f.Seller, 50)
	remark = v.optional("remark", f.Remark, 100)

	if d := f.FitDate; d != "" {
		v.requiredDate("fitdate", d)
		date = &d
	}
	price = v.decimal("fitprice", f.FitPrice, 10, 2)

	if v.ok() {
		return consumer, seller, remark, date, price, nil
	}
	return consumer, seller, remark, date, price, v.fields
}

func (s *Server) handleFitCreate(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())

	var form fitForm
	if err := decodeJSON(r, &form); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	consumer, seller, remark, date, price, fields := form.validate()
	if fields != nil {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, fields)
		return
	}
	if s.Fit == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	uid, err := s.Fit.Create(r.Context(), consumer, seller, date, price, remark)
	if err != nil {
		s.log.Error("creating fit-out failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusCreated, map[string]any{"uid": uid})
}

func (s *Server) handleFitUpdate(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	uid, ok := pathInt64(r, "id")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	var form fitForm
	if err := decodeJSON(r, &form); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if form.RowVersion <= 0 {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	consumer, seller, remark, date, price, fields := form.validate()
	if fields != nil {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, fields)
		return
	}
	if s.Fit == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	err := s.Fit.Update(r.Context(), uid, form.RowVersion, consumer, seller, date, price, remark)
	switch {
	case errors.Is(err, storemysql.ErrNotFound):
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
	case errors.Is(err, storemysql.ErrVersionConflict):
		apierr.Write(w, apierr.CodeConflict, reqID, nil)
	case err != nil:
		s.log.Error("updating fit-out failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
	default:
		w.WriteHeader(http.StatusNoContent)
	}
}

func (s *Server) handleFitDelete(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	uid, ok := pathInt64(r, "id")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if s.Fit == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	err := s.Fit.Delete(r.Context(), uid)
	switch {
	case errors.Is(err, storemysql.ErrNotFound):
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
	case err != nil:
		s.log.Error("deleting fit-out failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
	default:
		w.WriteHeader(http.StatusNoContent)
	}
}

func (s *Server) registerSlice8(mux *http.ServeMux) {
	mux.Handle("POST /api/speccar/list",
		s.requireAuth(s.requirePermission(auth.PermSpecCar, false,
			http.HandlerFunc(s.handleSpecCarList))))

	mux.Handle("POST /api/fit/list",
		s.requireAuth(s.requirePermission(auth.PermAppendRepair, false,
			http.HandlerFunc(s.handleFitList))))
	mux.Handle("POST /api/fit",
		s.requireAuth(s.requirePermission(auth.PermAppendRepair, true,
			http.HandlerFunc(s.handleFitCreate))))
	mux.Handle("PATCH /api/fit/{id}",
		s.requireAuth(s.requirePermission(auth.PermAppendRepair, true,
			http.HandlerFunc(s.handleFitUpdate))))
	mux.Handle("DELETE /api/fit/{id}",
		s.requireAuth(s.requirePermission(auth.PermAppendRepair, true,
			http.HandlerFunc(s.handleFitDelete))))
}
