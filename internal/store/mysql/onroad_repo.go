package mysql

import (
	"context"
	"database/sql"
	"fmt"
	"strings"

	"github.com/Markcus0526/carsaleman/internal/query"
)

// OnRoad is one row of vw_onroad.
//
// Money is a string, not a float: 148 decimal columns carry prices across this
// system and float64 silently corrupts totals (plan 11.2). It stays a string
// end to end -- Go, JSON, and the branded Decimal type in the client.
type OnRoad struct {
	UID           int64   `json:"uid"`
	BillNo        string  `json:"billno"`
	BillDate      *string `json:"billdate"`
	VIN           string  `json:"vin"`
	EngineNo      string  `json:"engineno"`
	CarTypeID     int64   `json:"cartypeid"`
	CarType       string  `json:"cartype"`
	CarName       string  `json:"carname"`
	ColorCode     string  `json:"colorcode"`
	ColorName     string  `json:"colorname"`
	Subsets       string  `json:"subsets"`
	InsideSetCode string  `json:"insidesetcode"`
	InsideSetName string  `json:"insidesetname"`
	CarState      string  `json:"carstate"`
	Property      string  `json:"property"`
	InPrice       *string `json:"inprice"`
	InFlag        int     `json:"inflag"`
	InKind        int     `json:"inkind"`
	CarSeries     string  `json:"carseries"`
}

// OnRoadFilterFields is the allowlist for GET /api/onroad.
//
// This is the security boundary for filtering: a column name cannot be a bind
// parameter, so the only safe design is to accept an API name and map it here.
// Columns absent from this map cannot be filtered on at all -- which is also
// how `inflag`/`inkind` stay internal rather than becoming query surface.
var OnRoadFilterFields = query.Fields{
	"vin":           {SQL: "vin", Kind: query.KindText},
	"engineno":      {SQL: "engineno", Kind: query.KindText},
	"billno":        {SQL: "billno", Kind: query.KindText},
	"cartype":       {SQL: "cartype", Kind: query.KindText},
	"carname":       {SQL: "carname", Kind: query.KindText},
	"carseries":     {SQL: "carseries", Kind: query.KindText},
	"colorname":     {SQL: "colorname", Kind: query.KindText},
	"insidesetname": {SQL: "insidesetname", Kind: query.KindText},
	"carstate":      {SQL: "carstate", Kind: query.KindText},
	"property":      {SQL: "property", Kind: query.KindText},
	"billdate":      {SQL: "billdate", Kind: query.KindDate},
	"inprice":       {SQL: "inprice", Kind: query.KindNumber},
}

// MaxListRows bounds one response.
//
// The legacy app pulled entire tables into a client-side DataSet and filtered
// in memory (plan 2.4). The port must not reproduce that shape: an unbounded
// list is a way to turn one careless request into an outage.
const MaxListRows = 500

type OnRoadRepo struct{ db *sql.DB }

func NewOnRoadRepo(db *sql.DB) *OnRoadRepo { return &OnRoadRepo{db: db} }

// List returns rows matching the filter, and whether the result was truncated.
//
// truncated is reported rather than hidden: a silently capped list looks like
// "there are only 500 of these", which is how people draw wrong conclusions
// from a screen.
func (r *OnRoadRepo) List(ctx context.Context, f query.Filter) (rows []OnRoad, truncated bool, err error) {
	where, args, err := query.Build(f, OnRoadFilterFields)
	if err != nil {
		return nil, false, err // an ErrInvalidFilter; the handler turns it into 422
	}

	var b strings.Builder
	b.WriteString(`
		SELECT uid, billno, billdate, vin, engineno, cartypeid, cartype,
		       COALESCE(carname,''), COALESCE(colorcode,''), COALESCE(colorname,''),
		       COALESCE(subsets,''), COALESCE(insidesetcode,''), COALESCE(insidesetname,''),
		       COALESCE(carstate,''), COALESCE(property,''), inprice, inflag, inkind,
		       COALESCE(carseries,'')
		FROM vw_onroad`)
	if where != "" {
		b.WriteString(" WHERE " + where)
	}
	// Ordering is by uid, not carseries: Chinese display order is applied in Go
	// with x/text/collate (D16), because utf8mb4_unicode_ci does not reproduce
	// the legacy Chinese_PRC_CI_AS sequence.
	b.WriteString(" ORDER BY uid LIMIT ?")
	args = append(args, MaxListRows+1) // one extra row reveals truncation

	res, err := r.db.QueryContext(ctx, b.String(), args...)
	if err != nil {
		return nil, false, fmt.Errorf("mysql: list onroad: %w", err)
	}
	defer res.Close()

	for res.Next() {
		var o OnRoad
		var billDate sql.NullTime
		var inPrice sql.NullString
		if err := res.Scan(
			&o.UID, &o.BillNo, &billDate, &o.VIN, &o.EngineNo, &o.CarTypeID, &o.CarType,
			&o.CarName, &o.ColorCode, &o.ColorName, &o.Subsets, &o.InsideSetCode,
			&o.InsideSetName, &o.CarState, &o.Property, &inPrice, &o.InFlag, &o.InKind,
			&o.CarSeries,
		); err != nil {
			return nil, false, fmt.Errorf("mysql: list onroad: %w", err)
		}
		if billDate.Valid {
			s := billDate.Time.Format("2006-01-02")
			o.BillDate = &s
		}
		if inPrice.Valid {
			// Already a decimal string from MySQL. Never parsed into a float.
			o.InPrice = &inPrice.String
		}
		rows = append(rows, o)
	}
	if err := res.Err(); err != nil {
		return nil, false, fmt.Errorf("mysql: list onroad: %w", err)
	}

	if len(rows) > MaxListRows {
		return rows[:MaxListRows], true, nil
	}
	return rows, false, nil
}
