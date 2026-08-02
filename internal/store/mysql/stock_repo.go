package mysql

import (
	"context"
	"database/sql"
	"fmt"
	"strings"

	"github.com/Markcus0526/carsaleman/internal/query"
)

// The stock list screens: 入库处理 over vw_storein and 出库处理 over vw_storeout.
//
// Both views are provisional until the Phase 0 dump (see migration 0006). The
// column contracts are exact, so these repositories are safe to write against;
// only the JOIN semantics are unconfirmed.

// StoreIn is one row of vw_storein, narrowed to what a list screen shows.
//
// Deliberately not all 42 columns. The finance columns (propval, profitprop,
// profitval, specprofitval, outstoreprice) belong to the finance engine, which
// is blocked on Phase 0 — pulling them into a stock list would put margin data
// on a screen that does not need it and would have to be unpicked later.
type StoreIn struct {
	UID        int64   `json:"uid"`
	BatchNo    string  `json:"batchno"`
	StorePlace string  `json:"storeplace"`
	OnRoadID   int64   `json:"onroadid"`
	InDate     *string `json:"indate"`
	InPrice    string  `json:"inprice"`
	PassNo     string  `json:"passno"`
	InPath     string  `json:"inpath"`
	InType     string  `json:"intype"`
	Settlement string  `json:"settlementname"`
	Handler    string  `json:"handlername"`
	Remark     string  `json:"remark"`
	OutFlag    int     `json:"outflag"`
	VIN        string  `json:"vin"`
	EngineNo   string  `json:"engineno"`
	CarType    string  `json:"cartype"`
	CarName    string  `json:"carname"`
	ColorName  string  `json:"colorname"`
	CarSeries  string  `json:"carseries"`
}

// StoreInFilterFields is the allowlist. A column name cannot be a bind
// parameter, so an API name mapped here is the only safe design.
//
// `outflag` is present and `inflag` is not: which vehicles are still in stock
// is the single most useful filter on this screen, whereas inflag is internal
// lifecycle state that a filter has no business reaching.
var StoreInFilterFields = query.Fields{
	"vin":            {SQL: "vin", Kind: query.KindText},
	"engineno":       {SQL: "engineno", Kind: query.KindText},
	"batchno":        {SQL: "batchno", Kind: query.KindText},
	"storeplace":     {SQL: "storeplace", Kind: query.KindText},
	"cartype":        {SQL: "cartype", Kind: query.KindText},
	"carname":        {SQL: "carname", Kind: query.KindText},
	"carseries":      {SQL: "carseries", Kind: query.KindText},
	"colorname":      {SQL: "colorname", Kind: query.KindText},
	"inpath":         {SQL: "inpath", Kind: query.KindText},
	"intype":         {SQL: "intype", Kind: query.KindText},
	"settlementname": {SQL: "settlementname", Kind: query.KindText},
	"handlername":    {SQL: "handlername", Kind: query.KindText},
	"indate":         {SQL: "indate", Kind: query.KindDate},
	"inprice":        {SQL: "inprice", Kind: query.KindNumber},
	"outflag":        {SQL: "outflag", Kind: query.KindNumber},
}

type StoreInRepo struct{ db *sql.DB }

func NewStoreInRepo(db *sql.DB) *StoreInRepo { return &StoreInRepo{db: db} }

func (r *StoreInRepo) List(ctx context.Context, f query.Filter) (rows []StoreIn, truncated bool, err error) {
	where, args, err := query.Build(f, StoreInFilterFields)
	if err != nil {
		return nil, false, err
	}

	var b strings.Builder
	b.WriteString(`
		SELECT uid, batchno, storeplace, onroadid, indate, inprice,
		       COALESCE(passno,''), COALESCE(inpath,''), COALESCE(intype,''),
		       COALESCE(settlementname,''), COALESCE(handlername,''),
		       COALESCE(remark,''), COALESCE(outflag,0),
		       COALESCE(vin,''), COALESCE(engineno,''), COALESCE(cartype,''),
		       COALESCE(carname,''), COALESCE(colorname,''), COALESCE(carseries,'')
		FROM vw_storein`)
	if where != "" {
		b.WriteString(" WHERE " + where)
	}
	b.WriteString(" ORDER BY uid LIMIT ?")
	args = append(args, MaxListRows+1)

	res, err := r.db.QueryContext(ctx, b.String(), args...)
	if err != nil {
		return nil, false, fmt.Errorf("mysql: list storein: %w", err)
	}
	defer res.Close()

	rows = []StoreIn{}
	for res.Next() {
		var s StoreIn
		var inDate sql.NullTime
		if err := res.Scan(&s.UID, &s.BatchNo, &s.StorePlace, &s.OnRoadID, &inDate,
			&s.InPrice, &s.PassNo, &s.InPath, &s.InType, &s.Settlement, &s.Handler,
			&s.Remark, &s.OutFlag, &s.VIN, &s.EngineNo, &s.CarType, &s.CarName,
			&s.ColorName, &s.CarSeries); err != nil {
			return nil, false, fmt.Errorf("mysql: list storein: %w", err)
		}
		if inDate.Valid {
			d := inDate.Time.Format("2006-01-02")
			s.InDate = &d
		}
		rows = append(rows, s)
	}
	if err := res.Err(); err != nil {
		return nil, false, fmt.Errorf("mysql: list storein: %w", err)
	}
	if len(rows) > MaxListRows {
		return rows[:MaxListRows], true, nil
	}
	return rows, false, nil
}

// StoreOut is one row of vw_storeout, narrowed the same way.
type StoreOut struct {
	UID          int64   `json:"uid"`
	BatchNo      string  `json:"batchno"`
	OnRoadID     int64   `json:"onroadid"`
	OutBillNo    string  `json:"outbillno"`
	SaleCompany  string  `json:"salecompany"`
	SaleKind     string  `json:"salekind"`
	Settlement   string  `json:"settlementname"`
	Handler      string  `json:"handlername"`
	SalePlace    string  `json:"saleplace"`
	OutDate      *string `json:"outdate"`
	CustomerName string  `json:"customername"`
	CustomerJob  string  `json:"customerjobkind"`
	SaleRegion   string  `json:"saleregion"`
	CarNo        string  `json:"carno"`
	OutPrice     string  `json:"outprice"`
	Remark       string  `json:"remark"`
	VIN          string  `json:"vin"`
	EngineNo     string  `json:"engineno"`
	CarType      string  `json:"cartype"`
	CarName      string  `json:"carname"`
	ColorName    string  `json:"colorname"`
	CarSeries    string  `json:"carseries"`
}

var StoreOutFilterFields = query.Fields{
	"vin":             {SQL: "vin", Kind: query.KindText},
	"engineno":        {SQL: "engineno", Kind: query.KindText},
	"batchno":         {SQL: "batchno", Kind: query.KindText},
	"outbillno":       {SQL: "outbillno", Kind: query.KindText},
	"salecompany":     {SQL: "salecompany", Kind: query.KindText},
	"salekind":        {SQL: "salekind", Kind: query.KindText},
	"saleplace":       {SQL: "saleplace", Kind: query.KindText},
	"saleregion":      {SQL: "saleregion", Kind: query.KindText},
	"customername":    {SQL: "customername", Kind: query.KindText},
	"customerjobkind": {SQL: "customerjobkind", Kind: query.KindText},
	"carno":           {SQL: "carno", Kind: query.KindText},
	"cartype":         {SQL: "cartype", Kind: query.KindText},
	"carname":         {SQL: "carname", Kind: query.KindText},
	"carseries":       {SQL: "carseries", Kind: query.KindText},
	"colorname":       {SQL: "colorname", Kind: query.KindText},
	"settlementname":  {SQL: "settlementname", Kind: query.KindText},
	"handlername":     {SQL: "handlername", Kind: query.KindText},
	"outdate":         {SQL: "outdate", Kind: query.KindDate},
	"outprice":        {SQL: "outprice", Kind: query.KindNumber},
}

type StoreOutRepo struct{ db *sql.DB }

func NewStoreOutRepo(db *sql.DB) *StoreOutRepo { return &StoreOutRepo{db: db} }

func (r *StoreOutRepo) List(ctx context.Context, f query.Filter) (rows []StoreOut, truncated bool, err error) {
	where, args, err := query.Build(f, StoreOutFilterFields)
	if err != nil {
		return nil, false, err
	}

	var b strings.Builder
	b.WriteString(`
		SELECT uid, batchno, onroadid, outbillno, salecompany, salekind,
		       COALESCE(settlementname,''), COALESCE(handlername,''),
		       COALESCE(saleplace,''), outdate, COALESCE(customername,''),
		       COALESCE(customerjobkind,''), COALESCE(saleregion,''),
		       COALESCE(carno,''), outprice, COALESCE(remark,''),
		       COALESCE(vin,''), COALESCE(engineno,''), COALESCE(cartype,''),
		       COALESCE(carname,''), COALESCE(colorname,''), COALESCE(carseries,'')
		FROM vw_storeout`)
	if where != "" {
		b.WriteString(" WHERE " + where)
	}
	b.WriteString(" ORDER BY uid LIMIT ?")
	args = append(args, MaxListRows+1)

	res, err := r.db.QueryContext(ctx, b.String(), args...)
	if err != nil {
		return nil, false, fmt.Errorf("mysql: list storeout: %w", err)
	}
	defer res.Close()

	rows = []StoreOut{}
	for res.Next() {
		var s StoreOut
		var outDate sql.NullTime
		if err := res.Scan(&s.UID, &s.BatchNo, &s.OnRoadID, &s.OutBillNo, &s.SaleCompany,
			&s.SaleKind, &s.Settlement, &s.Handler, &s.SalePlace, &outDate,
			&s.CustomerName, &s.CustomerJob, &s.SaleRegion, &s.CarNo, &s.OutPrice,
			&s.Remark, &s.VIN, &s.EngineNo, &s.CarType, &s.CarName, &s.ColorName,
			&s.CarSeries); err != nil {
			return nil, false, fmt.Errorf("mysql: list storeout: %w", err)
		}
		if outDate.Valid {
			d := outDate.Time.Format("2006-01-02")
			s.OutDate = &d
		}
		rows = append(rows, s)
	}
	if err := res.Err(); err != nil {
		return nil, false, fmt.Errorf("mysql: list storeout: %w", err)
	}
	if len(rows) > MaxListRows {
		return rows[:MaxListRows], true, nil
	}
	return rows, false, nil
}
