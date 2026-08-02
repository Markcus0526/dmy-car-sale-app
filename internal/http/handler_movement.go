package http

import (
	"errors"
	"net/http"

	"github.com/Markcus0526/carsaleman/internal/auth"
	"github.com/Markcus0526/carsaleman/internal/domain/movement"
	"github.com/Markcus0526/carsaleman/internal/http/apierr"
)

// Slices 5, 6, 7 and 10 -- the vehicle lifecycle (§6.2).
//
// The transitions themselves live in internal/domain/movement, which owns the
// transaction and the flag guards. These handlers only validate and translate
// errors into codes.

const (
	maxPlaceLen  = 50
	maxNameLen   = 50
	maxRemarkLen = 500
	maxPathLen   = 100
)

// writeMovementError maps the domain's states onto HTTP.
//
// The three "wrong state" errors are 409, not 422: the request is well formed
// and the user is allowed to make it -- the vehicle simply is not where they
// think it is, usually because a colleague moved it first. That is a conflict,
// and the client's answer is to reload rather than to correct a field.
func (s *Server) writeMovementError(w http.ResponseWriter, reqID string, err error, op string) {
	switch {
	case errors.Is(err, movement.ErrNotFound):
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
	case errors.Is(err, movement.ErrAlreadyStoredIn):
		apierr.Write(w, apierr.CodeConflict, reqID, map[string]string{"state": "ALREADY_STORED_IN"})
	case errors.Is(err, movement.ErrNotStoredIn):
		apierr.Write(w, apierr.CodeConflict, reqID, map[string]string{"state": "NOT_STORED_IN"})
	case errors.Is(err, movement.ErrAlreadyStoredOut):
		apierr.Write(w, apierr.CodeConflict, reqID, map[string]string{"state": "ALREADY_STORED_OUT"})
	default:
		s.log.Error(op+" failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
	}
}

type storeInRequest struct {
	OnRoadID   int64  `json:"onroadid"`
	BatchNo    string `json:"batchno"`
	StorePlace string `json:"storeplace"`
	InDate     string `json:"indate"`
	InPrice    string `json:"inprice"`
	PassNo     string `json:"passno"`
	CompanyNo  int    `json:"companyno"`
	InPath     string `json:"inpath"`
	InType     string `json:"intype"`
	PriceKind  string `json:"incarpricekind"`
	FactoryOut string `json:"factoryoutdate"`
	Settlement string `json:"settlementname"`
	Handler    string `json:"handlername"`
	Remark     string `json:"remark"`
}

func (s *Server) handleStoreIn(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())

	var req storeInRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	v := newValidator()
	in := movement.StoreInInput{
		OnRoadID:   req.OnRoadID,
		BatchNo:    v.optional("batchno", req.BatchNo, maxNameLen),
		StorePlace: v.required("storeplace", req.StorePlace, maxPlaceLen),
		InDate:     v.requiredDate("indate", req.InDate),
		PassNo:     v.optional("passno", req.PassNo, maxNameLen),
		CompanyNo:  req.CompanyNo,
		InPath:     v.required("inpath", req.InPath, maxPathLen),
		InType:     v.required("intype", req.InType, maxPathLen),
		PriceKind:  v.optional("incarpricekind", req.PriceKind, maxNameLen),
		FactoryOut: v.requiredDate("factoryoutdate", req.FactoryOut),
		Settlement: v.required("settlementname", req.Settlement, maxNameLen),
		Handler:    v.required("handlername", req.Handler, maxNameLen),
		Remark:     v.optional("remark", req.Remark, maxRemarkLen),
	}
	// inprice is NOT NULL on tbl_storein, unlike tbl_onroad's. Required here.
	if p := v.decimal("inprice", req.InPrice, 10, 2); p != nil {
		in.InPrice = *p
	} else if v.ok() {
		v.set("inprice", fieldRequired)
	}
	if req.OnRoadID <= 0 {
		v.set("onroadid", fieldRequired)
	}
	if !v.ok() {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, v.fields)
		return
	}
	if s.Movement == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	id, err := s.Movement.StoreIn(r.Context(), in)
	if err != nil {
		s.writeMovementError(w, reqID, err, "store-in")
		return
	}
	writeJSON(w, http.StatusCreated, map[string]any{"uid": id})
}

type transferRequest struct {
	OnRoadID   int64  `json:"onroadid"`
	StorePlace string `json:"storeplace"`
	ActionPay  string `json:"actionpay"`
	Settlement string `json:"settlementname"`
	Handler    string `json:"handlername"`
	Repair     string `json:"repairstate"`
	Reserve    string `json:"reservestate"`
	Remark     string `json:"remark"`
}

func (s *Server) handleTransfer(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())

	var req transferRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	v := newValidator()
	in := movement.TransferInput{
		OnRoadID:   req.OnRoadID,
		StorePlace: v.required("storeplace", req.StorePlace, maxPathLen),
		Settlement: v.required("settlementname", req.Settlement, maxNameLen),
		Handler:    v.required("handlername", req.Handler, maxNameLen),
		Repair:     v.optional("repairstate", req.Repair, maxPathLen),
		Reserve:    v.optional("reservestate", req.Reserve, maxPathLen),
		Remark:     v.optional("remark", req.Remark, maxRemarkLen),
	}
	if p := v.decimal("actionpay", req.ActionPay, 10, 2); p != nil {
		in.ActionPay = *p
	}
	if req.OnRoadID <= 0 {
		v.set("onroadid", fieldRequired)
	}
	if !v.ok() {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, v.fields)
		return
	}
	if s.Movement == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	if err := s.Movement.Transfer(r.Context(), in); err != nil {
		s.writeMovementError(w, reqID, err, "transfer")
		return
	}
	w.WriteHeader(http.StatusNoContent)
}

type storeOutRequest struct {
	OnRoadID     int64  `json:"onroadid"`
	OutBillNo    string `json:"outbillno"`
	SaleCompany  string `json:"salecompany"`
	SaleKind     string `json:"salekind"`
	Settlement   string `json:"settlementname"`
	Handler      string `json:"handlername"`
	SalePlace    string `json:"saleplace"`
	InCarKind    string `json:"incarkind"`
	OutDate      string `json:"outdate"`
	CustomerName string `json:"customername"`
	CustomerJob  string `json:"customerjobkind"`
	SaleRegion   string `json:"saleregion"`
	CarNo        string `json:"carno"`
	VoteCost     string `json:"votecost"`
	OutPrice     string `json:"outprice"`
	Remark       string `json:"remark"`
}

func (s *Server) handleStoreOut(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())

	var req storeOutRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	v := newValidator()
	in := movement.StoreOutInput{
		OnRoadID:     req.OnRoadID,
		OutBillNo:    v.required("outbillno", req.OutBillNo, maxNameLen),
		SaleCompany:  v.required("salecompany", req.SaleCompany, maxPathLen),
		SaleKind:     v.required("salekind", req.SaleKind, maxNameLen),
		Settlement:   v.required("settlementname", req.Settlement, maxNameLen),
		Handler:      v.required("handlername", req.Handler, maxNameLen),
		SalePlace:    v.required("saleplace", req.SalePlace, maxNameLen),
		InCarKind:    v.required("incarkind", req.InCarKind, maxPathLen),
		OutDate:      v.requiredDate("outdate", req.OutDate),
		CustomerName: v.required("customername", req.CustomerName, maxNameLen),
		CustomerJob:  v.required("customerjobkind", req.CustomerJob, maxNameLen),
		SaleRegion:   v.required("saleregion", req.SaleRegion, maxPathLen),
		CarNo:        v.required("carno", req.CarNo, maxNameLen),
		Remark:       v.optional("remark", req.Remark, maxRemarkLen),
	}
	if p := v.decimal("votecost", req.VoteCost, 10, 2); p != nil {
		in.VoteCost = *p
	}
	// outprice is NOT NULL: a dispatch without a sale price is not a dispatch.
	if p := v.decimal("outprice", req.OutPrice, 10, 2); p != nil {
		in.OutPrice = *p
	} else if v.ok() {
		v.set("outprice", fieldRequired)
	}
	if req.OnRoadID <= 0 {
		v.set("onroadid", fieldRequired)
	}
	if !v.ok() {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, v.fields)
		return
	}
	if s.Movement == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	id, err := s.Movement.StoreOut(r.Context(), in)
	if err != nil {
		s.writeMovementError(w, reqID, err, "store-out")
		return
	}
	writeJSON(w, http.StatusCreated, map[string]any{"uid": id})
}

// handleHistory is slice 10 -- what FrmActionHis actually shows.
//
// Gated on 在途/未提车辆管理 rather than on any single movement permission:
// the history spans store-in, transfer and dispatch, and requiring all three
// would hide a car's own record from the people who look after it.
func (s *Server) handleHistory(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	uid, ok := pathInt64(r, "id")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if s.Movement == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	changes, err := s.Movement.History(r.Context(), uid)
	if err != nil {
		s.log.Error("reading movement history failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusOK, map[string]any{"changes": changes})
}

// handleNewBatchNo mints a batch number for the store-in form.
//
// Server-side, per §6.6: the legacy client generated it with a 12-hour clock,
// so 09:00 and 21:00 collided, and two users in the same second collided too.
func (s *Server) handleNewBatchNo(w http.ResponseWriter, r *http.Request) {
	if s.Movement == nil {
		apierr.Write(w, apierr.CodeUnavailable, RequestIDFrom(r.Context()), nil)
		return
	}
	writeJSON(w, http.StatusOK, map[string]any{"batchno": s.Movement.NewBatchNo()})
}

func (s *Server) registerMovement(mux *http.ServeMux) {
	mux.Handle("POST /api/movement/storein",
		s.requireAuth(s.requirePermission(auth.PermStoreIn, true,
			http.HandlerFunc(s.handleStoreIn))))
	mux.Handle("GET /api/movement/batchno",
		s.requireAuth(s.requirePermission(auth.PermStoreIn, true,
			http.HandlerFunc(s.handleNewBatchNo))))
	mux.Handle("POST /api/movement/transfer",
		s.requireAuth(s.requirePermission(auth.PermStoreChange, true,
			http.HandlerFunc(s.handleTransfer))))
	mux.Handle("POST /api/movement/storeout",
		s.requireAuth(s.requirePermission(auth.PermStoreOut, true,
			http.HandlerFunc(s.handleStoreOut))))
	mux.Handle("GET /api/movement/history/{id}",
		s.requireAuth(s.requirePermission(auth.PermOnRoad, false,
			http.HandlerFunc(s.handleHistory))))
}
