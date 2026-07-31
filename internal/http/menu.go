package http

import "github.com/Markcus0526/carsaleman/internal/auth"

// MenuNode is one entry in the navigation tree.
//
// Note the deliberate separation of the two string fields:
//
//	PermissionKey -- the Chinese label stored in tbl_permission.fieldname.
//	                 An opaque authorization key. NEVER translated, never shown.
//	LabelKey      -- an i18n key the client resolves against its catalogue.
//	                 Display only, carries no authorization meaning.
//
// Conflating them is the failure mode described in DEVELOPMENT_PLAN.md 1.1:
// switch the UI to English, the permission lookup misses, and the user is
// silently denied every screen.
type MenuNode struct {
	ID            string     `json:"id"`
	PermissionKey string     `json:"permissionKey"`
	LabelKey      string     `json:"labelKey"`
	Path          string     `json:"path,omitempty"`
	Children      []MenuNode `json:"children,omitempty"`
}

// menuTree mirrors FrmMDIMain's MenuStrip. Permission keys are the exact
// menu labels with the trailing "(&N)" accelerator stripped, matching what
// Permission.SetMenuPermission wrote to Global.MENU_LIST.
var menuTree = []MenuNode{
	{ID: "movement", PermissionKey: auth.PermMovement, LabelKey: "menu.movement", Children: []MenuNode{
		{ID: "onroad", PermissionKey: auth.PermOnRoad, LabelKey: "menu.movement.onroad", Path: "/onroad"},
		{ID: "storein", PermissionKey: auth.PermStoreIn, LabelKey: "menu.movement.storein", Path: "/storein"},
		{ID: "storechange", PermissionKey: auth.PermStoreChange, LabelKey: "menu.movement.storechange", Path: "/storechange"},
		{ID: "storeout", PermissionKey: auth.PermStoreOut, LabelKey: "menu.movement.storeout", Path: "/storeout"},
		{ID: "speccar", PermissionKey: auth.PermSpecCar, LabelKey: "menu.movement.speccar", Path: "/speccar"},
		{ID: "repair", PermissionKey: auth.PermAppendRepair, LabelKey: "menu.movement.repair", Path: "/repairs"},
		{ID: "journal", PermissionKey: auth.PermSpecJournal, LabelKey: "menu.movement.journal", Path: "/journal"},
	}},
	{ID: "statistics", PermissionKey: auth.PermStatistics, LabelKey: "menu.statistics", Children: []MenuNode{
		{ID: "search-onroad", PermissionKey: auth.PermSearchOnRoad, LabelKey: "menu.statistics.searchOnroad", Path: "/search/onroad"},
		{ID: "search-storein", PermissionKey: auth.PermSearchStoreIn, LabelKey: "menu.statistics.searchStorein", Path: "/search/storein"},
		{ID: "search-storeout", PermissionKey: auth.PermSearchStoreOut, LabelKey: "menu.statistics.searchStoreout", Path: "/search/storeout"},
		{ID: "quarter-target", PermissionKey: auth.PermQuarterTarget, LabelKey: "menu.statistics.quarterTarget", Path: "/stats/quarter"},
		{ID: "region", PermissionKey: auth.PermRegionStats, LabelKey: "menu.statistics.region", Path: "/stats/region"},
		{ID: "custjob", PermissionKey: auth.PermCustJobStats, LabelKey: "menu.statistics.custJob", Path: "/stats/custjob"},
		{ID: "cartype-color", PermissionKey: auth.PermCarTypeColor, LabelKey: "menu.statistics.carTypeColor", Path: "/stats/cartype-color"},
		{ID: "handler", PermissionKey: auth.PermHandlerStats, LabelKey: "menu.statistics.handler", Path: "/stats/handler"},
		{ID: "remain-amount", PermissionKey: auth.PermRemainAmount, LabelKey: "menu.statistics.remainAmount", Path: "/stats/remain"},
		{ID: "annual-bonus", PermissionKey: auth.PermAnnualBonus, LabelKey: "menu.statistics.annualBonus", Path: "/stats/bonus"},
		{ID: "sale-cartype", PermissionKey: auth.PermSaleCarType, LabelKey: "menu.statistics.saleCarType", Path: "/stats/sale-cartype"},
	}},
	{ID: "reports", PermissionKey: auth.PermReports, LabelKey: "menu.reports", Children: []MenuNode{
		{ID: "rpt-store-detail", PermissionKey: auth.PermRptStoreDetail, LabelKey: "menu.reports.storeDetail", Path: "/reports/store-detail"},
		{ID: "rpt-store-type-detail", PermissionKey: auth.PermRptStoreTypeDetail, LabelKey: "menu.reports.storeTypeDetail", Path: "/reports/store-type-detail"},
		{ID: "rpt-onroad-detail", PermissionKey: auth.PermRptOnRoadDetail, LabelKey: "menu.reports.onroadDetail", Path: "/reports/onroad-detail"},
		{ID: "rpt-profit-detail", PermissionKey: auth.PermRptProfitDetail, LabelKey: "menu.reports.profitDetail", Path: "/reports/profit-detail"},
		{ID: "rpt-store-total", PermissionKey: auth.PermRptStoreTotal, LabelKey: "menu.reports.storeTotal", Path: "/reports/store-total"},
		{ID: "rpt-store-type-total", PermissionKey: auth.PermRptStoreTypeTotal, LabelKey: "menu.reports.storeTypeTotal", Path: "/reports/store-type-total"},
		{ID: "rpt-reserve-sale", PermissionKey: auth.PermRptReserveSale, LabelKey: "menu.reports.reserveSale", Path: "/reports/reserve-sale"},
		{ID: "rpt-sale-total", PermissionKey: auth.PermRptSaleTotal, LabelKey: "menu.reports.saleTotal", Path: "/reports/sale-total"},
		{ID: "rpt-sale-count", PermissionKey: auth.PermRptSaleCountTotal, LabelKey: "menu.reports.saleCountTotal", Path: "/reports/sale-count"},
		{ID: "rpt-wholesale", PermissionKey: auth.PermRptWholeSaleTotal, LabelKey: "menu.reports.wholeSaleTotal", Path: "/reports/wholesale"},
		{ID: "rpt-carseries", PermissionKey: auth.PermRptCarSeriesTotal, LabelKey: "menu.reports.carSeriesTotal", Path: "/reports/carseries"},
	}},
	{ID: "charts", PermissionKey: auth.PermCharts, LabelKey: "menu.charts", Children: []MenuNode{
		{ID: "chart-buy-total", PermissionKey: auth.PermChartBuyTotal, LabelKey: "menu.charts.buyTotal", Path: "/charts/buy-total"},
		{ID: "chart-sale-kind", PermissionKey: auth.PermChartSaleKind, LabelKey: "menu.charts.saleKind", Path: "/charts/sale-kind"},
		{ID: "chart-sale-count", PermissionKey: auth.PermChartSaleCount, LabelKey: "menu.charts.saleCount", Path: "/charts/sale-count"},
		{ID: "chart-sale-region", PermissionKey: auth.PermChartSaleRegion, LabelKey: "menu.charts.saleRegion", Path: "/charts/sale-region"},
		{ID: "chart-cust-job", PermissionKey: auth.PermChartCustJob, LabelKey: "menu.charts.custJob", Path: "/charts/cust-job"},
	}},
	{ID: "finance", PermissionKey: auth.PermFinance, LabelKey: "menu.finance", Children: []MenuNode{
		{ID: "finance-store", PermissionKey: auth.PermFinanceStore, LabelKey: "menu.finance.store", Path: "/finance/store"},
	}},
	{ID: "settings", PermissionKey: auth.PermSettings, LabelKey: "menu.settings", Children: []MenuNode{
		{ID: "cartype", PermissionKey: auth.PermCarType, LabelKey: "menu.settings.carType", Path: "/settings/cartypes"},
		{ID: "basedata", PermissionKey: auth.PermBaseData, LabelKey: "menu.settings.baseData", Path: "/settings/basedata"},
		{ID: "carcompany", PermissionKey: auth.PermCarCompany, LabelKey: "menu.settings.carCompany", Path: "/settings/carcompanies"},
		{ID: "user-permission", PermissionKey: auth.PermUserPermission, LabelKey: "menu.settings.userPermission", Path: "/settings/permissions"},
		{ID: "db-backup", PermissionKey: auth.PermDBBackup, LabelKey: "menu.settings.dbBackup", Path: "/settings/backup"},
		{ID: "finance-param", PermissionKey: auth.PermFinanceParam, LabelKey: "menu.settings.financeParam", Path: "/settings/finance-params"},
	}},
	{ID: "passchange", PermissionKey: auth.PermPassChange, LabelKey: "menu.passChange", Path: "/password"},
}

// filterMenu returns only the nodes the permission set grants read access to.
//
// Deny-by-default: an absent key is dropped, matching the legacy behaviour
// where Global.PERMISSION_LIST lookups that missed left the item disabled.
// A parent with no surviving children is dropped too.
func filterMenu(nodes []MenuNode, perms auth.Set) []MenuNode {
	out := make([]MenuNode, 0, len(nodes))
	for _, n := range nodes {
		if !perms.CanRead(n.PermissionKey) {
			continue
		}
		if len(n.Children) > 0 {
			kids := filterMenu(n.Children, perms)
			if len(kids) == 0 {
				continue
			}
			n.Children = kids
		}
		out = append(out, n)
	}
	return out
}
