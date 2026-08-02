package http

import (
	"errors"
	"net/http"

	"github.com/Markcus0526/carsaleman/internal/auth"
	"github.com/Markcus0526/carsaleman/internal/http/apierr"
	storemysql "github.com/Markcus0526/carsaleman/internal/store/mysql"
)

// 特殊日记 -- the notes table behind FrmSpecJournal. See journal_repo.go on why
// this is NOT the audit trail the plan described until day 23.

const (
	maxJournalTitleLen   = 50  // tbl_log.title VARCHAR(50)
	maxJournalContentLen = 200 // tbl_log.cont  VARCHAR(200)
)

type journalListResponse struct {
	Entries   []storemysql.JournalEntry `json:"entries"`
	Truncated bool                      `json:"truncated"`
	Limit     int                       `json:"limit"`
}

func (s *Server) handleJournalList(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	if s.Journal == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	entries, truncated, err := s.Journal.List(r.Context())
	if err != nil {
		s.log.Error("listing journal failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusOK, journalListResponse{
		Entries: entries, Truncated: truncated, Limit: storemysql.MaxJournalRows,
	})
}

type journalForm struct {
	Date       string `json:"logdate"`
	Title      string `json:"title"`
	Content    string `json:"cont"`
	RowVersion int    `json:"rowVersion"`
}

// validate. title and cont are NULL-able columns whose legacy default is "",
// so neither is required -- but a note with no content at all is not a note,
// so at least one of the two must carry something.
func (f journalForm) validate() (date, title, content string, fields map[string]string) {
	v := newValidator()
	date = v.requiredDate("logdate", f.Date)
	title = v.optional("title", f.Title, maxJournalTitleLen)
	content = v.optional("cont", f.Content, maxJournalContentLen)
	if title == "" && content == "" {
		v.set("cont", fieldRequired)
	}
	if v.ok() {
		return date, title, content, nil
	}
	return date, title, content, v.fields
}

func (s *Server) handleJournalCreate(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())

	var form journalForm
	if err := decodeJSON(r, &form); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	date, title, content, fields := form.validate()
	if fields != nil {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, fields)
		return
	}
	if s.Journal == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	uid, err := s.Journal.Create(r.Context(), date, title, content)
	if err != nil {
		s.log.Error("creating journal entry failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusCreated, map[string]any{"uid": uid})
}

func (s *Server) handleJournalUpdate(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	uid, ok := pathInt64(r, "id")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	var form journalForm
	if err := decodeJSON(r, &form); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if form.RowVersion <= 0 {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	date, title, content, fields := form.validate()
	if fields != nil {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, fields)
		return
	}
	if s.Journal == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	err := s.Journal.Update(r.Context(), uid, form.RowVersion, date, title, content)
	switch {
	case errors.Is(err, storemysql.ErrNotFound):
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
	case errors.Is(err, storemysql.ErrVersionConflict):
		apierr.Write(w, apierr.CodeConflict, reqID, nil)
	case err != nil:
		s.log.Error("updating journal entry failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
	default:
		w.WriteHeader(http.StatusNoContent)
	}
}

func (s *Server) handleJournalDelete(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	uid, ok := pathInt64(r, "id")
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if s.Journal == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	err := s.Journal.Delete(r.Context(), uid)
	switch {
	case errors.Is(err, storemysql.ErrNotFound):
		apierr.Write(w, apierr.CodeNotFound, reqID, nil)
	case err != nil:
		s.log.Error("deleting journal entry failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
	default:
		w.WriteHeader(http.StatusNoContent)
	}
}

func (s *Server) registerJournal(mux *http.ServeMux) {
	mux.Handle("GET /api/journal",
		s.requireAuth(s.requirePermission(auth.PermSpecJournal, false,
			http.HandlerFunc(s.handleJournalList))))
	mux.Handle("POST /api/journal",
		s.requireAuth(s.requirePermission(auth.PermSpecJournal, true,
			http.HandlerFunc(s.handleJournalCreate))))
	mux.Handle("PATCH /api/journal/{id}",
		s.requireAuth(s.requirePermission(auth.PermSpecJournal, true,
			http.HandlerFunc(s.handleJournalUpdate))))
	mux.Handle("DELETE /api/journal/{id}",
		s.requireAuth(s.requirePermission(auth.PermSpecJournal, true,
			http.HandlerFunc(s.handleJournalDelete))))
}
