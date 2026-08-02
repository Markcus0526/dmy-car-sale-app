package mysql

import (
	"context"
	"database/sql"
	"fmt"
	"strings"

	"github.com/Markcus0526/carsaleman/internal/query"
)

// 特种车统计表 over vw_speccar (slice 8).
//
// WHAT MAKES A CAR "SPECIAL" HERE
//
// Not `carspeckind`, which the name suggests. FrmSpecCar identifies them by
// `salekind = '大客户'` (key-account sales) -- see FrmSpecCar.cs:327 and :331.
// carspeckind is carried as data but is not the selector.
//
// SpecialSaleKind is exposed rather than hardcoded into the query because the
// view itself is unfiltered here (see migration 0007): the predicate lives in
// the allowlist and is defaulted by the UI, where it is visible and adjustable
// rather than buried in SQL nobody reads.
const SpecialSaleKind = "大客户"

// SpecCar is one row of vw_speccar, narrowed to what the screen shows.
//
// The price breakdown (otherprice1-4, interestprice, pricediff) is present
// because this screen IS the margin view for key-account sales -- unlike the
// stock list, where those columns would be gratuitous.
type SpecCar struct {
	UID          int64   `json:"uid"`
	OnRoadID     int64   `json:"onroadid"`
	BatchNo      string  `json:"batchno"`
	OutBillNo    string  `json:"outbillno"`
	SaleCompany  string  `json:"salecompany"`
	SaleKind     string  `json:"salekind"`
	CarSpecKind  string  `json:"carspeckind"`
	Settlement   string  `json:"settlementname"`
	Handler      string  `json:"handlername"`
	OutDate      *string `json:"outdate"`
	CustomerName string  `json:"customername"`
	SaleRegion   string  `json:"saleregion"`
	CarNo        string  `json:"carno"`
	InPrice      *string `json:"inprice"`
	OutPrice     string  `json:"outprice"`
	ProfitVal    *string `json:"profitval"`
	SpecProfit   *string `json:"specprofitval"`
	PriceDiff    *string `json:"pricediff"`
	IsReport     string  `json:"isreport"`
	Remark       string  `json:"remark"`
	VIN          string  `json:"vin"`
	EngineNo     string  `json:"engineno"`
	CarType      string  `json:"cartype"`
	CarName      string  `json:"carname"`
	ColorName    string  `json:"colorname"`
}

var SpecCarFilterFields = query.Fields{
	"vin":             {SQL: "vin", Kind: query.KindText},
	"engineno":        {SQL: "engineno", Kind: query.KindText},
	"outbillno":       {SQL: "outbillno", Kind: query.KindText},
	"salekind":        {SQL: "salekind", Kind: query.KindText},
	"carspeckind":     {SQL: "carspeckind", Kind: query.KindText},
	"salecompany":     {SQL: "salecompany", Kind: query.KindText},
	"customername":    {SQL: "customername", Kind: query.KindText},
	"customerjobkind": {SQL: "customerjobkind", Kind: query.KindText},
	"saleregion":      {SQL: "saleregion", Kind: query.KindText},
	"carno":           {SQL: "carno", Kind: query.KindText},
	"cartype":         {SQL: "cartype", Kind: query.KindText},
	"carname":         {SQL: "carname", Kind: query.KindText},
	"colorname":       {SQL: "colorname", Kind: query.KindText},
	"handlername":     {SQL: "handlername", Kind: query.KindText},
	"isreport":        {SQL: "isreport", Kind: query.KindText},
	"outdate":         {SQL: "outdate", Kind: query.KindDate},
	"outprice":        {SQL: "outprice", Kind: query.KindNumber},
}

type SpecCarRepo struct{ db *sql.DB }

func NewSpecCarRepo(db *sql.DB) *SpecCarRepo { return &SpecCarRepo{db: db} }

func (r *SpecCarRepo) List(ctx context.Context, f query.Filter) (rows []SpecCar, truncated bool, err error) {
	where, args, err := query.Build(f, SpecCarFilterFields)
	if err != nil {
		return nil, false, err
	}

	var b strings.Builder
	b.WriteString(`
		SELECT uid, onroadid, batchno, outbillno, salecompany, salekind,
		       COALESCE(carspeckind,''), COALESCE(settlementname,''),
		       COALESCE(handlername,''), outdate, COALESCE(customername,''),
		       COALESCE(saleregion,''), COALESCE(carno,''),
		       inprice, outprice, profitval, specprofitval, pricediff,
		       COALESCE(isreport,''), COALESCE(remark,''),
		       COALESCE(vin,''), COALESCE(engineno,''), COALESCE(cartype,''),
		       COALESCE(carname,''), COALESCE(colorname,'')
		FROM vw_speccar`)
	if where != "" {
		b.WriteString(" WHERE " + where)
	}
	b.WriteString(" ORDER BY uid LIMIT ?")
	args = append(args, MaxListRows+1)

	res, err := r.db.QueryContext(ctx, b.String(), args...)
	if err != nil {
		return nil, false, fmt.Errorf("mysql: list speccar: %w", err)
	}
	defer res.Close()

	rows = []SpecCar{}
	for res.Next() {
		var s SpecCar
		var outDate sql.NullTime
		var inPrice, profit, specProfit, diff sql.NullString
		if err := res.Scan(&s.UID, &s.OnRoadID, &s.BatchNo, &s.OutBillNo, &s.SaleCompany,
			&s.SaleKind, &s.CarSpecKind, &s.Settlement, &s.Handler, &outDate,
			&s.CustomerName, &s.SaleRegion, &s.CarNo, &inPrice, &s.OutPrice,
			&profit, &specProfit, &diff, &s.IsReport, &s.Remark,
			&s.VIN, &s.EngineNo, &s.CarType, &s.CarName, &s.ColorName); err != nil {
			return nil, false, fmt.Errorf("mysql: list speccar: %w", err)
		}
		if outDate.Valid {
			d := outDate.Time.Format("2006-01-02")
			s.OutDate = &d
		}
		// NULL money stays NULL, never "0.00". "not recorded" and "zero" are
		// different facts, and the second one lands in the margin totals.
		for _, m := range []struct {
			src sql.NullString
			dst **string
		}{
			{inPrice, &s.InPrice}, {profit, &s.ProfitVal},
			{specProfit, &s.SpecProfit}, {diff, &s.PriceDiff},
		} {
			if m.src.Valid {
				v := m.src.String
				*m.dst = &v
			}
		}
		rows = append(rows, s)
	}
	if err := res.Err(); err != nil {
		return nil, false, fmt.Errorf("mysql: list speccar: %w", err)
	}
	if len(rows) > MaxListRows {
		return rows[:MaxListRows], true, nil
	}
	return rows, false, nil
}
