package http

import (
	"errors"
	"net/http"
	"strconv"

	"github.com/Markcus0526/carsaleman/internal/http/apierr"
	storemysql "github.com/Markcus0526/carsaleman/internal/store/mysql"
)

// onRoadForm is the wire shape of the create/edit modal.
//
// Deliberately not storemysql.OnRoad: that carries uid, inflag, inkind and
// carseries, none of which a form may set. Accepting the read model as the
// write model is how a client ends up able to flip the store-in state machine
// (§6.2) through an edit box.
type onRoadForm struct {
	BillNo        string `json:"billno"`
	BillDate      string `json:"billdate"`
	VIN           string `json:"vin"`
	EngineNo      string `json:"engineno"`
	CarTypeID     int64  `json:"cartypeid"`
	CarType       string `json:"cartype"`
	CarName       string `json:"carname"`
	ColorCode     string `json:"colorcode"`
	ColorName     string `json:"colorname"`
	Subsets       string `json:"subsets"`
	InsideSetCode string `json:"insidesetcode"`
	InsideSetName string `json:"insidesetname"`
	CarState      string `json:"carstate"`
	Property      string `json:"property"`
	InPrice       string `json:"inprice"`
	// RowVersion is what the client last read. Required on update, ignored on
	// create. Its absence is a conflict, not a licence to overwrite.
	RowVersion int `json:"rowVersion"`
}

// validate maps the form onto the column constraints in migration 0001.
//
// The lengths are the VARCHAR widths, kept literal rather than derived: they
// are a contract with the schema, and a wrong one here shows up as a 500 from
// the driver instead of a field marked in the UI.
func (f onRoadForm) validate() (storemysql.OnRoadInput, map[string]string) {
	v := newValidator()
	in := storemysql.OnRoadInput{
		BillNo:        v.required("billno", f.BillNo, 50),
		BillDate:      v.requiredDate("billdate", f.BillDate),
		VIN:           v.required("vin", f.VIN, 50),
		EngineNo:      v.required("engineno", f.EngineNo, 50),
		CarType:       v.required("cartype", f.CarType, 50),
		CarName:       v.optional("carname", f.CarName, 200),
		ColorCode:     v.optional("colorcode", f.ColorCode, 50),
		ColorName:     v.optional("colorname", f.ColorName, 100),
		Subsets:       v.optional("subsets", f.Subsets, 50),
		InsideSetCode: v.optional("insidesetcode", f.InsideSetCode, 50),
		InsideSetName: v.optional("insidesetname", f.InsideSetName, 100),
		CarState:      v.optional("carstate", f.CarState, 50),
		Property:      v.optional("property", f.Property, 50),
		InPrice:       v.decimal("inprice", f.InPrice, 10, 2),
		CarTypeID:     f.CarTypeID,
	}
	if f.CarTypeID <= 0 {
		v.set("cartypeid", fieldRequired)
	}
	if v.ok() {
		return in, nil
	}
	return in, v.fields
}

// onRoadDetailResponse carries the row plus the version to send back on save.
//
// rowVersion sits beside the row rather than inside it because it is not data
// about the vehicle -- it is a concurrency token, and mixing the two invites
// someone to render it.
type onRoadDetailResponse struct {
	Row        *storemysql.OnRoad `json:"row"`
	RowVersion int                `json:"rowVersion"`
}

// handleOnRoadGet reads one vehicle for the edit modal.
//
// Note the ordering shared by all three handlers: parse and validate the
// request BEFORE checking that a database is configured. A malformed request
// is malformed either way, and answering it with 503 invites the client to
// retry something that can never succeed.
//
// The modal always re-reads rather than editing the row cached in the grid.
// A grid row can be minutes old; opening against it would mean the user's
// first save conflicts on data they never saw change.
func (s *Server) handleOnRoadGet(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	uid, ok := pathInt64(r, "id")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if s.onroad == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	row, version, err := s.onroad.Get(r.Context(), uid)
	if errors.Is(err, storemysql.ErrNotFound) {
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
		return
	}
	if err != nil {
		s.log.Error("reading on-road vehicle failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusOK, onRoadDetailResponse{Row: row, RowVersion: version})
}

func (s *Server) handleOnRoadCreate(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())

	var form onRoadForm
	if err := decodeJSON(r, &form); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	in, fields := form.validate()
	if fields != nil {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, fields)
		return
	}
	if s.onroad == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	uid, err := s.onroad.Create(r.Context(), in)
	if err != nil {
		s.log.Error("creating on-road vehicle failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}

	// 201 with the stored row, not the submitted one: the response must show
	// what the database actually holds, including anything it normalised.
	row, version, err := s.onroad.Get(r.Context(), uid)
	if err != nil {
		s.log.Error("re-reading created vehicle failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusCreated, onRoadDetailResponse{Row: row, RowVersion: version})
}

func (s *Server) handleOnRoadUpdate(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	uid, ok := pathInt64(r, "id")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	var form onRoadForm
	if err := decodeJSON(r, &form); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	// A missing rowVersion is rejected outright. Treating 0 as "no opinion"
	// and writing anyway would make the concurrency check opt-in, and an
	// opt-in lost-update guard is not a guard.
	if form.RowVersion <= 0 {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	in, fields := form.validate()
	if fields != nil {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, fields)
		return
	}
	if s.onroad == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	err := s.onroad.Update(r.Context(), uid, form.RowVersion, in)
	switch {
	case errors.Is(err, storemysql.ErrNotFound):
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
		return
	case errors.Is(err, storemysql.ErrVersionConflict):
		// 409. The client reloads and shows the user what changed underneath
		// them -- the legacy app had no equivalent and last-write-won (§11.4).
		apierr.Write(w, apierr.CodeConflict, reqID, nil)
		return
	case err != nil:
		s.log.Error("updating on-road vehicle failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}

	row, version, err := s.onroad.Get(r.Context(), uid)
	if err != nil {
		s.log.Error("re-reading updated vehicle failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusOK, onRoadDetailResponse{Row: row, RowVersion: version})
}

// pathInt64 reads a positive integer path parameter.
func pathInt64(r *http.Request, name string) (int64, bool) {
	n, err := strconv.ParseInt(r.PathValue(name), 10, 64)
	if err != nil || n <= 0 {
		return 0, false
	}
	return n, true
}
