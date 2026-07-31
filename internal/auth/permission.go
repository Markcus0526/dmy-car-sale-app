// Package auth holds authorization primitives.
//
// The permission keys below are the Chinese menu labels already stored in
// tbl_permission.fieldname. They are AUTHORIZATION KEYS, not display text --
// keeping them verbatim means existing permission rows migrate with no data
// transformation (GO_MIGRATION_PLAN.md 6.4).
//
// Never translate these. The UI label travels separately as an i18n key; see
// MenuNode.LabelKey. An English-locale user whose permission lookup misses is
// silently denied every screen, which is why this file exists instead of 63
// inline string literals.
//
// Legacy note: Permission.SetMenuPermission stripped a trailing "(...)" suffix
// from each MenuStrip label before using it as a key, so "车辆流转(&1)" became
// "车辆流转". The constants below are already stripped.
package auth

// Permission values as stored in tbl_permission.permission.
type Level string

const (
	LevelReadWrite   Level = "读写"   // read-write  -- the only value granting write access
	LevelReadOnly    Level = "只读"   // read-only
	LevelUnavailable Level = "不可用" // unavailable -- menu entry disabled
)

// Permission keys, grouped by menu section.
const (
	// 车辆流转 -- vehicle movement
	PermMovement       = "车辆流转"
	PermOnRoad         = "在途/未提车辆管理"
	PermStoreIn        = "入库处理"
	PermStoreChange    = "库位变化"
	PermStoreOut       = "出库处理"
	PermSpecCar        = "特种车统计表"
	PermAppendRepair   = "赠送装修"
	PermSpecJournal    = "特殊日记"

	// 查询统计 -- query and statistics
	PermStatistics      = "查询统计"
	PermSearchOnRoad    = "在途查询"
	PermSearchStoreIn   = "库存查询"
	PermSearchStoreOut  = "出车查询"
	PermQuarterTarget   = "销售季度任务统计"
	PermRegionStats     = "零售区域统计"
	PermCustJobStats    = "零售行业统计"
	PermCarTypeColor    = "销售车型颜色情况分析"
	PermHandlerStats    = "销售顾问销量排名统计"
	PermRemainAmount    = "各车型占用数量及资金比例统计"
	PermAnnualBonus     = "年度销售奖励"
	PermSaleCarType     = "各车型销售情况统计表"

	// 报表管理 -- reports
	PermReports              = "报表管理"
	PermRptStoreDetail       = "到货明细报表"
	PermRptStoreTypeDetail   = "分销明细报表"
	PermRptOnRoadDetail      = "在途/未提车辆明细报表"
	PermRptProfitDetail      = "进货返利报表"
	PermRptStoreTotal        = "总库存明细报表"
	PermRptStoreTypeTotal    = "库存车型颜色统计报表"
	PermRptReserveSale       = "预售明细报表"
	PermRptSaleTotal         = "总销售明细报表"
	PermRptSaleCountTotal    = "销售数量汇总报表"
	PermRptWholeSaleTotal    = "销售批发零售汇总报表"
	PermRptCarSeriesTotal    = "进销存汇总报表"

	// 图表分析 -- charts
	PermCharts          = "图表分析"
	PermChartBuyTotal   = "总体的进销售存情况"
	PermChartSaleKind   = "销售方式的数量情况"
	PermChartSaleCount  = "销售顾问的销量排名统计"
	PermChartSaleRegion = "车辆的销售区域统计"
	PermChartCustJob    = "销售的行业统计"

	// 财务分析 -- finance
	PermFinance      = "财务分析"
	PermFinanceStore = "财务综合统计"

	// 系统设置 -- settings
	PermSettings       = "系统设置"
	PermCarType        = "车型价格设置"
	PermBaseData       = "基础信息"
	PermCarCompany     = "进货单位模块"
	PermUserPermission = "权限设定"
	PermDBBackup       = "数据备份"
	PermFinanceParam   = "财务方面参数"

	// standalone
	PermPassChange = "密码修改"
	PermExit       = "程序退出"
)

// Set is a user's resolved permission map, keyed by the constants above.
type Set map[string]Level

// CanWrite reports whether the user may perform write operations on key.
//
// Deny-by-default: an absent key is NOT permitted. Legacy
// Permission.GetFuncPermission returned true only for 读写, and an absent key
// left the menu item disabled -- both behaviours are preserved here.
func (s Set) CanWrite(key string) bool {
	return s[key] == LevelReadWrite
}

// CanRead reports whether the user may view the screen behind key.
func (s Set) CanRead(key string) bool {
	switch s[key] {
	case LevelReadWrite, LevelReadOnly:
		return true
	default:
		return false
	}
}
