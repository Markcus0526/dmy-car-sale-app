package http

import (
	"net/http"
	"strconv"

	"github.com/Markcus0526/carsaleman/internal/auth"
	"github.com/Markcus0526/carsaleman/internal/domain/quarterstats"
	"github.com/Markcus0526/carsaleman/internal/http/apierr"
)

// 销售季度任务统计 (§6.3).

// yearQuarter reads and bounds the two query parameters.
//
// The year bound is not arbitrary: `year` is an INT column and the value is
// interpolated into no SQL, but an unbounded year makes it trivial to ask for
// a grid that can never exist and forces the client to render one.
func yearQuarter(r *http.Request) (year, quarter int, ok bool) {
	year, err := strconv.Atoi(r.URL.Query().Get("year"))
	if err != nil || year < 1990 || year > 2999 {
		return 0, 0, false
	}
	quarter, err = strconv.Atoi(r.URL.Query().Get("quarter"))
	if err != nil || !quarterstats.ValidQuarter(quarter) {
		return 0, 0, false
	}
	return year, quarter, true
}

func (s *Server) handleQuarterStatsGet(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	year, quarter, ok := yearQuarter(r)
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}
	if s.QuarterStats == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	grid, err := s.QuarterStats.Load(r.Context(), year, quarter)
	if err != nil {
		s.log.Error("loading quarter stats failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusOK, grid)
}

type quarterSaveRequest struct {
	General []quarterstats.Row `json:"general"`
	Special []quarterstats.Row `json:"special"`
}

// handleQuarterStatsPut saves a WHOLE quarter.
//
// PUT of the entire grid, per §6.3, precisely so partial-save races disappear.
// The legacy form saved on year/quarter switch and is the place the plan
// identifies as most likely to lose edits.
func (s *Server) handleQuarterStatsPut(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())
	year, quarter, ok := yearQuarter(r)
	if !ok {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	var req quarterSaveRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	v := newValidator()
	for i, row := range append(append([]quarterstats.Row{}, req.General...), req.Special...) {
		if row.CarSeries == "" {
			v.set("carseries."+strconv.Itoa(i), fieldRequired)
		}
		// These are vehicle counts. A negative one is a typo, not a business
		// case, and it would corrupt every subtotal and percentage on the grid.
		if row.Target < 0 || row.Month1 < 0 || row.Month2 < 0 || row.Month3 < 0 || row.Remain < 0 {
			v.set("counts."+strconv.Itoa(i), fieldInvalidFormat)
		}
	}
	if !v.ok() {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, v.fields)
		return
	}
	if s.QuarterStats == nil {
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	if err := s.QuarterStats.Save(r.Context(), year, quarter, req.General, req.Special); err != nil {
		s.log.Error("saving quarter stats failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}

	// Return the recomputed grid rather than 204: the derived cells are the
	// server's to compute, and echoing them back means the client never has to
	// reimplement the percentage rounding.
	grid, err := s.QuarterStats.Load(r.Context(), year, quarter)
	if err != nil {
		s.log.Error("reloading quarter stats failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	writeJSON(w, http.StatusOK, grid)
}

func (s *Server) registerQuarterStats(mux *http.ServeMux) {
	mux.Handle("GET /api/quarterstats",
		s.requireAuth(s.requirePermission(auth.PermQuarterTarget, false,
			http.HandlerFunc(s.handleQuarterStatsGet))))
	mux.Handle("PUT /api/quarterstats",
		s.requireAuth(s.requirePermission(auth.PermQuarterTarget, true,
			http.HandlerFunc(s.handleQuarterStatsPut))))
}
