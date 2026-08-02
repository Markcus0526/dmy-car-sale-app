package http

import (
	"errors"
	"net/http"
	"net/url"

	"github.com/Markcus0526/carsaleman/internal/auth"
	"github.com/Markcus0526/carsaleman/internal/http/apierr"
	storemysql "github.com/Markcus0526/carsaleman/internal/store/mysql"
)

// Slice 2 -- reference data. tbl_basedata backs every dropdown in the system,
// so this screen is a prerequisite for most of the ones that follow.

const (
	maxDomainNameLen = 50  // tbl_basedata.name  VARCHAR(50)
	maxKeyNameLen    = 100 // tbl_basedata.keyname VARCHAR(100)
	maxValueLen      = 100 // tbl_basedata.value   VARCHAR(100)
)

type domainsResponse struct {
	Domains []storemysql.BaseDomain `json:"domains"`
	// TypeConflicts names domains whose rows disagree about `type` -- possible
	// in legacy data because the schema cannot express the invariant. Reported
	// rather than quietly normalised: it is a data problem for a human.
	TypeConflicts []string `json:"typeConflicts"`
}

func (s *Server) handleBaseDomains(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	if s.BaseData == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	domains, err := s.BaseData.Domains(r.Context())
	if err != nil {
		s.log.Error("listing basedata domains failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	conflicts, err := s.BaseData.DomainTypeConflicts(r.Context())
	if err != nil {
		s.log.Error("checking basedata type conflicts failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusOK, domainsResponse{Domains: domains, TypeConflicts: conflicts})
}

// pathName reads a domain name from the path.
//
// Domain names are Chinese and travel percent-encoded. Go's ServeMux decodes
// path segments already, so this only guards emptiness and length -- but the
// length check matters: a name longer than the column would fail as a driver
// error on write and simply match nothing on read.
func pathName(r *http.Request, key string) (string, bool) {
	name, err := url.PathUnescape(r.PathValue(key))
	if err != nil || name == "" || len([]rune(name)) > maxDomainNameLen {
		return "", false
	}
	return name, true
}

func (s *Server) handleBaseValues(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	name, ok := pathName(r, "name")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if s.BaseData == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	values, err := s.BaseData.Values(r.Context(), name)
	if err != nil {
		s.log.Error("listing basedata values failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusOK, map[string]any{"values": values})
}

// handleBaseLookup feeds dropdowns on other screens.
//
// Separate from handleBaseValues because it filters out the blank placeholder
// row an empty domain must carry, and because it is gated on the READING
// screen's own permission rather than on 基础信息 -- a sales clerk needs the
// 地区 list to fill in a sale without any right to administer reference data.
func (s *Server) handleBaseLookup(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	name, ok := pathName(r, "name")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if s.BaseData == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	values, err := s.BaseData.Lookup(r.Context(), name)
	if err != nil {
		s.log.Error("basedata lookup failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusOK, map[string]any{"values": values})
}

type createDomainRequest struct {
	Name string `json:"name"`
	Type int    `json:"type"`
}

func (s *Server) handleBaseCreateDomain(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())

	var req createDomainRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	v := newValidator()
	name := v.required("name", req.Name, maxDomainNameLen)
	if req.Type != storemysql.TypeValueOnly && req.Type != storemysql.TypeKeyValue {
		v.set("type", fieldInvalidFormat)
	}
	if !v.ok() {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, v.fields)
		return
	}
	if s.BaseData == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	err := s.BaseData.CreateDomain(r.Context(), name, req.Type)
	if errors.Is(err, storemysql.ErrDuplicateDomain) {
		// 409, and a field code so the UI can mark the name box rather than
		// showing a banner the user has to map back to an input themselves.
		apierr.Write(w, apierr.CodeConflict, reqID, map[string]string{"name": "DUPLICATE"})
		return
	}
	if err != nil {
		s.log.Error("creating basedata domain failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	w.WriteHeader(http.StatusCreated)
}

type renameDomainRequest struct {
	Name string `json:"name"`
}

func (s *Server) handleBaseRenameDomain(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	oldName, ok := pathName(r, "name")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	var req renameDomainRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	v := newValidator()
	newName := v.required("name", req.Name, maxDomainNameLen)
	if !v.ok() {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, v.fields)
		return
	}
	if s.BaseData == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	err := s.BaseData.RenameDomain(r.Context(), oldName, newName)
	switch {
	case errors.Is(err, storemysql.ErrDuplicateDomain):
		apierr.Write(w, apierr.CodeConflict, reqID, map[string]string{"name": "DUPLICATE"})
	case errors.Is(err, storemysql.ErrNotFound):
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
	case err != nil:
		s.log.Error("renaming basedata domain failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
	default:
		w.WriteHeader(http.StatusNoContent)
	}
}

func (s *Server) handleBaseDeleteDomain(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	name, ok := pathName(r, "name")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if s.BaseData == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	err := s.BaseData.DeleteDomain(r.Context(), name)
	switch {
	case errors.Is(err, storemysql.ErrNotFound):
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
	case err != nil:
		s.log.Error("deleting basedata domain failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
	default:
		w.WriteHeader(http.StatusNoContent)
	}
}

type valueRequest struct {
	KeyName    string `json:"keyname"`
	Value      string `json:"value"`
	RowVersion int    `json:"rowVersion"`
}

func (s *Server) handleBaseAddValue(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	name, ok := pathName(r, "name")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	var req valueRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	v := newValidator()
	value := v.required("value", req.Value, maxValueLen)
	keyname := v.optional("keyname", req.KeyName, maxKeyNameLen)
	if !v.ok() {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, v.fields)
		return
	}
	if s.BaseData == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	uid, err := s.BaseData.AddValue(r.Context(), name, keyname, value)
	if errors.Is(err, storemysql.ErrNotFound) {
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
		return
	}
	if err != nil {
		s.log.Error("adding basedata value failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusCreated, map[string]any{"uid": uid})
}

func (s *Server) handleBaseUpdateValue(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	uid, ok := pathInt64(r, "id")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	var req valueRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if req.RowVersion <= 0 {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	v := newValidator()
	value := v.required("value", req.Value, maxValueLen)
	keyname := v.optional("keyname", req.KeyName, maxKeyNameLen)
	if !v.ok() {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, v.fields)
		return
	}
	if s.BaseData == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	err := s.BaseData.UpdateValue(r.Context(), uid, req.RowVersion, keyname, value)
	switch {
	case errors.Is(err, storemysql.ErrNotFound):
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
	case errors.Is(err, storemysql.ErrVersionConflict):
		apierr.Write(w, apierr.CodeConflict, reqID, nil)
	case err != nil:
		s.log.Error("updating basedata value failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
	default:
		w.WriteHeader(http.StatusNoContent)
	}
}

func (s *Server) handleBaseDeleteValue(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	uid, ok := pathInt64(r, "id")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if s.BaseData == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	err := s.BaseData.DeleteValue(r.Context(), uid)
	switch {
	case errors.Is(err, storemysql.ErrNotFound):
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
	case err != nil:
		s.log.Error("deleting basedata value failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
	default:
		w.WriteHeader(http.StatusNoContent)
	}
}

type importRequest struct {
	Content string `json:"content"`
}

func (s *Server) handleBaseImport(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	name, ok := pathName(r, "name")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	var req importRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if s.BaseData == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	n, err := s.BaseData.ImportValues(r.Context(), name, req.Content)
	switch {
	case errors.Is(err, storemysql.ErrNotFound):
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
	case errors.Is(err, storemysql.ErrTooManyRows):
		apierr.Write(w, apierr.CodeValidationFailed, reqID,
			map[string]string{"content": "TOO_LONG"})
	case err != nil:
		s.log.Error("importing basedata failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
	default:
		writeJSON(w, http.StatusOK, map[string]any{"imported": n})
	}
}

func (s *Server) registerBaseData(mux *http.ServeMux) {
	// Administration: gated on 基础信息.
	mux.Handle("GET /api/basedata/domains",
		s.requireAuth(s.requirePermission(auth.PermBaseData, false,
			http.HandlerFunc(s.handleBaseDomains))))
	mux.Handle("GET /api/basedata/domains/{name}/values",
		s.requireAuth(s.requirePermission(auth.PermBaseData, false,
			http.HandlerFunc(s.handleBaseValues))))
	mux.Handle("POST /api/basedata/domains",
		s.requireAuth(s.requirePermission(auth.PermBaseData, true,
			http.HandlerFunc(s.handleBaseCreateDomain))))
	mux.Handle("PATCH /api/basedata/domains/{name}",
		s.requireAuth(s.requirePermission(auth.PermBaseData, true,
			http.HandlerFunc(s.handleBaseRenameDomain))))
	mux.Handle("DELETE /api/basedata/domains/{name}",
		s.requireAuth(s.requirePermission(auth.PermBaseData, true,
			http.HandlerFunc(s.handleBaseDeleteDomain))))
	mux.Handle("POST /api/basedata/domains/{name}/values",
		s.requireAuth(s.requirePermission(auth.PermBaseData, true,
			http.HandlerFunc(s.handleBaseAddValue))))
	mux.Handle("PATCH /api/basedata/values/{id}",
		s.requireAuth(s.requirePermission(auth.PermBaseData, true,
			http.HandlerFunc(s.handleBaseUpdateValue))))
	mux.Handle("DELETE /api/basedata/values/{id}",
		s.requireAuth(s.requirePermission(auth.PermBaseData, true,
			http.HandlerFunc(s.handleBaseDeleteValue))))
	mux.Handle("POST /api/basedata/domains/{name}/import",
		s.requireAuth(s.requirePermission(auth.PermBaseData, true,
			http.HandlerFunc(s.handleBaseImport))))

	// Lookup: any signed-in user. Filling in a sale needs the 地区 list; it
	// does not need the right to administer reference data. Gating this on
	// 基础信息 would force every clerk to hold an admin permission, which is
	// how over-broad grants happen.
	mux.Handle("GET /api/lookup/{name}",
		s.requireAuth(http.HandlerFunc(s.handleBaseLookup)))
}
