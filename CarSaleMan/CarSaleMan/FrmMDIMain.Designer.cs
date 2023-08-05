namespace CarSaleMan
{
    partial class FrmMDIMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMDIMain));
            this.menuMainStrip = new System.Windows.Forms.MenuStrip();
            this.cmdCarInout = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdOnRoadCar = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdStoreIn = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdStoreChange = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdStoreOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdSpecCar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdAppendRepair = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSpecJournal = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdStats = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSearchOnroadCar = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSearchStorein = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSearchStoreout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdStatisCarSaleQuarter = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdStatisSaleRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdStatisCustsJob = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdStatisCartypeColor = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdStatisHandler = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdStatisRemainAmount = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdStatisSaleHonor = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdStatisSaleCartype = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdReport = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdReportStoreDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdReportStoreTypeDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdReportOnroadDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdReportProfitDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdReportStoreTotal = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdReportStoreCartypeTotal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdReportReserveSale = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdReportSaleTotal = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdReportSaleCountTotal = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdReportWholeSaleTotal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdReportCarseriesTotal = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdChart = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdChartBuyTotal = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdChartSaleKindCount = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdChartSaleCount = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdChartSaleRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdChartCustsJob = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdFinance = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdFinanceSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdEnvironment = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdCarType = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdBaseData = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdCarCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdUserPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdDatabaseBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdFinanceParam = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdPassword = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMainStrip
            // 
            this.menuMainStrip.BackgroundImage = global::CarSaleMan.Properties.Resources.bkMenuBar;
            this.menuMainStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuMainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdCarInout,
            this.cmdStats,
            this.cmdReport,
            this.cmdChart,
            this.cmdFinance,
            this.cmdEnvironment,
            this.cmdPassword,
            this.cmdClose});
            this.menuMainStrip.Location = new System.Drawing.Point(0, 0);
            this.menuMainStrip.Name = "menuMainStrip";
            this.menuMainStrip.Size = new System.Drawing.Size(918, 24);
            this.menuMainStrip.TabIndex = 1;
            this.menuMainStrip.Text = "menuMainStrip";
            // 
            // cmdCarInout
            // 
            this.cmdCarInout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdOnRoadCar,
            this.cmdStoreIn,
            this.cmdStoreChange,
            this.cmdStoreOut,
            this.toolStripSeparator8,
            this.cmdSpecCar,
            this.toolStripSeparator1,
            this.cmdAppendRepair,
            this.cmdSpecJournal});
            this.cmdCarInout.Name = "cmdCarInout";
            this.cmdCarInout.Size = new System.Drawing.Size(84, 20);
            this.cmdCarInout.Text = "车辆流转(&1)";
            // 
            // cmdOnRoadCar
            // 
            this.cmdOnRoadCar.Name = "cmdOnRoadCar";
            this.cmdOnRoadCar.Size = new System.Drawing.Size(197, 22);
            this.cmdOnRoadCar.Text = "在途/未提车辆管理 (&A)";
            this.cmdOnRoadCar.Click += new System.EventHandler(this.cmdOnRoadCar_Click);
            // 
            // cmdStoreIn
            // 
            this.cmdStoreIn.Name = "cmdStoreIn";
            this.cmdStoreIn.Size = new System.Drawing.Size(197, 22);
            this.cmdStoreIn.Text = "入库处理 (&B)";
            this.cmdStoreIn.Click += new System.EventHandler(this.cmdStoreIn_Click);
            // 
            // cmdStoreChange
            // 
            this.cmdStoreChange.Name = "cmdStoreChange";
            this.cmdStoreChange.Size = new System.Drawing.Size(197, 22);
            this.cmdStoreChange.Text = "库位变化 (&C)";
            this.cmdStoreChange.Click += new System.EventHandler(this.cmdStoreChange_Click);
            // 
            // cmdStoreOut
            // 
            this.cmdStoreOut.Name = "cmdStoreOut";
            this.cmdStoreOut.Size = new System.Drawing.Size(197, 22);
            this.cmdStoreOut.Text = "出库处理 (&D)";
            this.cmdStoreOut.Click += new System.EventHandler(this.cmdStoreOut_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(194, 6);
            // 
            // cmdSpecCar
            // 
            this.cmdSpecCar.Name = "cmdSpecCar";
            this.cmdSpecCar.Size = new System.Drawing.Size(197, 22);
            this.cmdSpecCar.Text = "特种车统计表 (&E)";
            this.cmdSpecCar.Click += new System.EventHandler(this.cmdSpecCar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(194, 6);
            // 
            // cmdAppendRepair
            // 
            this.cmdAppendRepair.Name = "cmdAppendRepair";
            this.cmdAppendRepair.Size = new System.Drawing.Size(197, 22);
            this.cmdAppendRepair.Text = "赠送装修 (&F)";
            this.cmdAppendRepair.Click += new System.EventHandler(this.cmdAppendRepair_Click);
            // 
            // cmdSpecJournal
            // 
            this.cmdSpecJournal.Name = "cmdSpecJournal";
            this.cmdSpecJournal.Size = new System.Drawing.Size(197, 22);
            this.cmdSpecJournal.Text = "特殊日记 (&G)";
            this.cmdSpecJournal.Click += new System.EventHandler(this.cmdSpecJournal_Click);
            // 
            // cmdStats
            // 
            this.cmdStats.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdSearchOnroadCar,
            this.cmdSearchStorein,
            this.cmdSearchStoreout,
            this.toolStripSeparator2,
            this.cmdStatisCarSaleQuarter,
            this.cmdStatisSaleRegion,
            this.cmdStatisCustsJob,
            this.cmdStatisCartypeColor,
            this.cmdStatisHandler,
            this.cmdStatisRemainAmount,
            this.cmdStatisSaleHonor,
            this.cmdStatisSaleCartype});
            this.cmdStats.Name = "cmdStats";
            this.cmdStats.Size = new System.Drawing.Size(84, 20);
            this.cmdStats.Text = "查询统计(&2)";
            // 
            // cmdSearchOnroadCar
            // 
            this.cmdSearchOnroadCar.Name = "cmdSearchOnroadCar";
            this.cmdSearchOnroadCar.Size = new System.Drawing.Size(242, 22);
            this.cmdSearchOnroadCar.Text = "在途查询";
            this.cmdSearchOnroadCar.Click += new System.EventHandler(this.cmdSearchOnroadCar_Click);
            // 
            // cmdSearchStorein
            // 
            this.cmdSearchStorein.Name = "cmdSearchStorein";
            this.cmdSearchStorein.Size = new System.Drawing.Size(242, 22);
            this.cmdSearchStorein.Text = "库存查询";
            this.cmdSearchStorein.Click += new System.EventHandler(this.cmdSearchStorein_Click);
            // 
            // cmdSearchStoreout
            // 
            this.cmdSearchStoreout.Name = "cmdSearchStoreout";
            this.cmdSearchStoreout.Size = new System.Drawing.Size(242, 22);
            this.cmdSearchStoreout.Text = "出车查询";
            this.cmdSearchStoreout.Click += new System.EventHandler(this.cmdSearchStoreout_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(239, 6);
            // 
            // cmdStatisCarSaleQuarter
            // 
            this.cmdStatisCarSaleQuarter.Name = "cmdStatisCarSaleQuarter";
            this.cmdStatisCarSaleQuarter.Size = new System.Drawing.Size(242, 22);
            this.cmdStatisCarSaleQuarter.Text = "销售季度任务统计";
            this.cmdStatisCarSaleQuarter.Click += new System.EventHandler(this.cmdStatisCarSaleQuarter_Click);
            // 
            // cmdStatisSaleRegion
            // 
            this.cmdStatisSaleRegion.Name = "cmdStatisSaleRegion";
            this.cmdStatisSaleRegion.Size = new System.Drawing.Size(242, 22);
            this.cmdStatisSaleRegion.Text = "零售区域统计";
            this.cmdStatisSaleRegion.Click += new System.EventHandler(this.cmdStatisSaleRegion_Click);
            // 
            // cmdStatisCustsJob
            // 
            this.cmdStatisCustsJob.Name = "cmdStatisCustsJob";
            this.cmdStatisCustsJob.Size = new System.Drawing.Size(242, 22);
            this.cmdStatisCustsJob.Text = "零售行业统计";
            this.cmdStatisCustsJob.Click += new System.EventHandler(this.cmdStatisCustsJob_Click);
            // 
            // cmdStatisCartypeColor
            // 
            this.cmdStatisCartypeColor.Name = "cmdStatisCartypeColor";
            this.cmdStatisCartypeColor.Size = new System.Drawing.Size(242, 22);
            this.cmdStatisCartypeColor.Text = "销售车型颜色情况分析";
            this.cmdStatisCartypeColor.Click += new System.EventHandler(this.cmdStatisCartypeColor_Click);
            // 
            // cmdStatisHandler
            // 
            this.cmdStatisHandler.Name = "cmdStatisHandler";
            this.cmdStatisHandler.Size = new System.Drawing.Size(242, 22);
            this.cmdStatisHandler.Text = "销售顾问销量排名统计";
            this.cmdStatisHandler.Click += new System.EventHandler(this.cmdStatisHandler_Click);
            // 
            // cmdStatisRemainAmount
            // 
            this.cmdStatisRemainAmount.Name = "cmdStatisRemainAmount";
            this.cmdStatisRemainAmount.Size = new System.Drawing.Size(242, 22);
            this.cmdStatisRemainAmount.Text = "各车型占用数量及资金比例统计";
            this.cmdStatisRemainAmount.Click += new System.EventHandler(this.cmdStatisRemainAmount_Click);
            // 
            // cmdStatisSaleHonor
            // 
            this.cmdStatisSaleHonor.Name = "cmdStatisSaleHonor";
            this.cmdStatisSaleHonor.Size = new System.Drawing.Size(242, 22);
            this.cmdStatisSaleHonor.Text = "年度销售奖励";
            this.cmdStatisSaleHonor.Click += new System.EventHandler(this.cmdStatisSaleHonor_Click);
            // 
            // cmdStatisSaleCartype
            // 
            this.cmdStatisSaleCartype.Name = "cmdStatisSaleCartype";
            this.cmdStatisSaleCartype.Size = new System.Drawing.Size(242, 22);
            this.cmdStatisSaleCartype.Text = "各车型销售情况统计表";
            this.cmdStatisSaleCartype.Click += new System.EventHandler(this.cmdStatisSaleCartype_Click);
            // 
            // cmdReport
            // 
            this.cmdReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdReportStoreDetail,
            this.cmdReportStoreTypeDetail,
            this.cmdReportOnroadDetail,
            this.cmdReportProfitDetail,
            this.toolStripSeparator3,
            this.cmdReportStoreTotal,
            this.cmdReportStoreCartypeTotal,
            this.toolStripSeparator4,
            this.cmdReportReserveSale,
            this.toolStripSeparator5,
            this.cmdReportSaleTotal,
            this.cmdReportSaleCountTotal,
            this.cmdReportWholeSaleTotal,
            this.toolStripSeparator6,
            this.cmdReportCarseriesTotal});
            this.cmdReport.Name = "cmdReport";
            this.cmdReport.Size = new System.Drawing.Size(84, 20);
            this.cmdReport.Text = "报表管理(&3)";
            // 
            // cmdReportStoreDetail
            // 
            this.cmdReportStoreDetail.Name = "cmdReportStoreDetail";
            this.cmdReportStoreDetail.Size = new System.Drawing.Size(199, 22);
            this.cmdReportStoreDetail.Text = "到货明细报表";
            this.cmdReportStoreDetail.Click += new System.EventHandler(this.cmdReportStoreDetail_Click);
            // 
            // cmdReportStoreTypeDetail
            // 
            this.cmdReportStoreTypeDetail.Name = "cmdReportStoreTypeDetail";
            this.cmdReportStoreTypeDetail.Size = new System.Drawing.Size(199, 22);
            this.cmdReportStoreTypeDetail.Text = "分销明细报表";
            this.cmdReportStoreTypeDetail.Click += new System.EventHandler(this.cmdReportStoreCarTypeDetail_Click);
            // 
            // cmdReportOnroadDetail
            // 
            this.cmdReportOnroadDetail.Name = "cmdReportOnroadDetail";
            this.cmdReportOnroadDetail.Size = new System.Drawing.Size(199, 22);
            this.cmdReportOnroadDetail.Text = "在途/未提车辆明细报表";
            this.cmdReportOnroadDetail.Click += new System.EventHandler(this.cmdReportOnroadDetail_Click);
            // 
            // cmdReportProfitDetail
            // 
            this.cmdReportProfitDetail.Name = "cmdReportProfitDetail";
            this.cmdReportProfitDetail.Size = new System.Drawing.Size(199, 22);
            this.cmdReportProfitDetail.Text = "进货返利报表";
            this.cmdReportProfitDetail.Click += new System.EventHandler(this.cmdReportProfitDetail_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(196, 6);
            // 
            // cmdReportStoreTotal
            // 
            this.cmdReportStoreTotal.Name = "cmdReportStoreTotal";
            this.cmdReportStoreTotal.Size = new System.Drawing.Size(199, 22);
            this.cmdReportStoreTotal.Text = "总库存明细报表";
            this.cmdReportStoreTotal.Click += new System.EventHandler(this.cmdReportStoreTotal_Click);
            // 
            // cmdReportStoreCartypeTotal
            // 
            this.cmdReportStoreCartypeTotal.Name = "cmdReportStoreCartypeTotal";
            this.cmdReportStoreCartypeTotal.Size = new System.Drawing.Size(199, 22);
            this.cmdReportStoreCartypeTotal.Text = "库存车型颜色统计报表";
            this.cmdReportStoreCartypeTotal.Click += new System.EventHandler(this.cmdReportStoreCartypeTotal_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(196, 6);
            // 
            // cmdReportReserveSale
            // 
            this.cmdReportReserveSale.Name = "cmdReportReserveSale";
            this.cmdReportReserveSale.Size = new System.Drawing.Size(199, 22);
            this.cmdReportReserveSale.Text = "预售明细报表";
            this.cmdReportReserveSale.Click += new System.EventHandler(this.cmdReportReserveSale_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(196, 6);
            // 
            // cmdReportSaleTotal
            // 
            this.cmdReportSaleTotal.Name = "cmdReportSaleTotal";
            this.cmdReportSaleTotal.Size = new System.Drawing.Size(199, 22);
            this.cmdReportSaleTotal.Text = "总销售明细报表";
            this.cmdReportSaleTotal.Click += new System.EventHandler(this.cmdReportSaleTotal_Click);
            // 
            // cmdReportSaleCountTotal
            // 
            this.cmdReportSaleCountTotal.Name = "cmdReportSaleCountTotal";
            this.cmdReportSaleCountTotal.Size = new System.Drawing.Size(199, 22);
            this.cmdReportSaleCountTotal.Text = "销售数量汇总报表";
            this.cmdReportSaleCountTotal.Click += new System.EventHandler(this.cmdReportSaleCountTotal_Click);
            // 
            // cmdReportWholeSaleTotal
            // 
            this.cmdReportWholeSaleTotal.Name = "cmdReportWholeSaleTotal";
            this.cmdReportWholeSaleTotal.Size = new System.Drawing.Size(199, 22);
            this.cmdReportWholeSaleTotal.Text = "销售批发零售汇总报表";
            this.cmdReportWholeSaleTotal.Click += new System.EventHandler(this.cmdReportWholeSaleTotal_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(196, 6);
            // 
            // cmdReportCarseriesTotal
            // 
            this.cmdReportCarseriesTotal.Name = "cmdReportCarseriesTotal";
            this.cmdReportCarseriesTotal.Size = new System.Drawing.Size(199, 22);
            this.cmdReportCarseriesTotal.Text = "进销存汇总报表";
            this.cmdReportCarseriesTotal.Click += new System.EventHandler(this.cmdReportCarseriesTotal_Click);
            // 
            // cmdChart
            // 
            this.cmdChart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdChartBuyTotal,
            this.cmdChartSaleKindCount,
            this.cmdChartSaleCount,
            this.cmdChartSaleRegion,
            this.cmdChartCustsJob});
            this.cmdChart.Name = "cmdChart";
            this.cmdChart.Size = new System.Drawing.Size(84, 20);
            this.cmdChart.Text = "图表分析(&4)";
            // 
            // cmdChartBuyTotal
            // 
            this.cmdChartBuyTotal.Name = "cmdChartBuyTotal";
            this.cmdChartBuyTotal.Size = new System.Drawing.Size(206, 22);
            this.cmdChartBuyTotal.Text = "总体的进销售存情况";
            this.cmdChartBuyTotal.Click += new System.EventHandler(this.cmdChartBuyTotal_Click);
            // 
            // cmdChartSaleKindCount
            // 
            this.cmdChartSaleKindCount.Name = "cmdChartSaleKindCount";
            this.cmdChartSaleKindCount.Size = new System.Drawing.Size(206, 22);
            this.cmdChartSaleKindCount.Text = "销售方式的数量情况";
            this.cmdChartSaleKindCount.Click += new System.EventHandler(this.cmdChartSaleKindCount_Click);
            // 
            // cmdChartSaleCount
            // 
            this.cmdChartSaleCount.Name = "cmdChartSaleCount";
            this.cmdChartSaleCount.Size = new System.Drawing.Size(206, 22);
            this.cmdChartSaleCount.Text = "销售顾问的销量排名统计";
            this.cmdChartSaleCount.Click += new System.EventHandler(this.cmdChartSaleCount_Click);
            // 
            // cmdChartSaleRegion
            // 
            this.cmdChartSaleRegion.Name = "cmdChartSaleRegion";
            this.cmdChartSaleRegion.Size = new System.Drawing.Size(206, 22);
            this.cmdChartSaleRegion.Text = "车辆的销售区域统计";
            this.cmdChartSaleRegion.Click += new System.EventHandler(this.cmdChartSaleRegion_Click);
            // 
            // cmdChartCustsJob
            // 
            this.cmdChartCustsJob.Name = "cmdChartCustsJob";
            this.cmdChartCustsJob.Size = new System.Drawing.Size(206, 22);
            this.cmdChartCustsJob.Text = "销售的行业统计";
            this.cmdChartCustsJob.Click += new System.EventHandler(this.cmdChartCustsJob_Click);
            // 
            // cmdFinance
            // 
            this.cmdFinance.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdFinanceSummary});
            this.cmdFinance.Name = "cmdFinance";
            this.cmdFinance.Size = new System.Drawing.Size(84, 20);
            this.cmdFinance.Text = "财务分析(&5)";
            // 
            // cmdFinanceSummary
            // 
            this.cmdFinanceSummary.Name = "cmdFinanceSummary";
            this.cmdFinanceSummary.Size = new System.Drawing.Size(146, 22);
            this.cmdFinanceSummary.Text = "财务综合统计";
            this.cmdFinanceSummary.Click += new System.EventHandler(this.cmdFinanceSummary_Click);
            // 
            // cmdEnvironment
            // 
            this.cmdEnvironment.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdCarType,
            this.cmdBaseData,
            this.cmdCarCompany,
            this.cmdUserPermission,
            this.toolStripSeparator7,
            this.cmdDatabaseBackup,
            this.cmdFinanceParam});
            this.cmdEnvironment.Name = "cmdEnvironment";
            this.cmdEnvironment.Size = new System.Drawing.Size(84, 20);
            this.cmdEnvironment.Text = "系统设置(&6)";
            // 
            // cmdCarType
            // 
            this.cmdCarType.Name = "cmdCarType";
            this.cmdCarType.Size = new System.Drawing.Size(146, 22);
            this.cmdCarType.Text = "车型价格设置";
            this.cmdCarType.Click += new System.EventHandler(this.cmdCarType_Click);
            // 
            // cmdBaseData
            // 
            this.cmdBaseData.Name = "cmdBaseData";
            this.cmdBaseData.Size = new System.Drawing.Size(146, 22);
            this.cmdBaseData.Text = "基础信息";
            this.cmdBaseData.Click += new System.EventHandler(this.cmdBaseData_Click);
            // 
            // cmdCarCompany
            // 
            this.cmdCarCompany.Name = "cmdCarCompany";
            this.cmdCarCompany.Size = new System.Drawing.Size(146, 22);
            this.cmdCarCompany.Text = "进货单位模块";
            this.cmdCarCompany.Click += new System.EventHandler(this.cmdCarCompany_Click);
            // 
            // cmdUserPermission
            // 
            this.cmdUserPermission.Name = "cmdUserPermission";
            this.cmdUserPermission.Size = new System.Drawing.Size(146, 22);
            this.cmdUserPermission.Text = "权限设定";
            this.cmdUserPermission.Click += new System.EventHandler(this.cmdUserPermission_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(143, 6);
            // 
            // cmdDatabaseBackup
            // 
            this.cmdDatabaseBackup.Name = "cmdDatabaseBackup";
            this.cmdDatabaseBackup.Size = new System.Drawing.Size(146, 22);
            this.cmdDatabaseBackup.Text = "数据备份";
            // 
            // cmdFinanceParam
            // 
            this.cmdFinanceParam.Name = "cmdFinanceParam";
            this.cmdFinanceParam.Size = new System.Drawing.Size(146, 22);
            this.cmdFinanceParam.Text = "财务方面参数";
            this.cmdFinanceParam.Click += new System.EventHandler(this.cmdFinanceParam_Click);
            // 
            // cmdPassword
            // 
            this.cmdPassword.Name = "cmdPassword";
            this.cmdPassword.Size = new System.Drawing.Size(84, 20);
            this.cmdPassword.Text = "密码修改(&7)";
            this.cmdPassword.Click += new System.EventHandler(this.cmdPassword_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(84, 20);
            this.cmdClose.Text = "程序退出(&8)";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // FrmMDIMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(918, 640);
            this.Controls.Add(this.menuMainStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuMainStrip;
            this.Name = "FrmMDIMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "辽宁众志诚汽车销售进销存系统";
            this.Load += new System.EventHandler(this.FrmMDIMain_Load);
            this.menuMainStrip.ResumeLayout(false);
            this.menuMainStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem cmdCarInout;
        private System.Windows.Forms.ToolStripMenuItem cmdOnRoadCar;
        private System.Windows.Forms.ToolStripMenuItem cmdStoreIn;
        private System.Windows.Forms.ToolStripMenuItem cmdStoreChange;
        private System.Windows.Forms.ToolStripMenuItem cmdStoreOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cmdAppendRepair;
        private System.Windows.Forms.ToolStripMenuItem cmdSpecJournal;
        private System.Windows.Forms.ToolStripMenuItem cmdStats;
        private System.Windows.Forms.ToolStripMenuItem cmdSearchOnroadCar;
        private System.Windows.Forms.ToolStripMenuItem cmdSearchStorein;
        private System.Windows.Forms.ToolStripMenuItem cmdSearchStoreout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cmdStatisCarSaleQuarter;
        private System.Windows.Forms.ToolStripMenuItem cmdStatisSaleRegion;
        private System.Windows.Forms.ToolStripMenuItem cmdStatisCustsJob;
        private System.Windows.Forms.ToolStripMenuItem cmdStatisCartypeColor;
        private System.Windows.Forms.ToolStripMenuItem cmdStatisHandler;
        private System.Windows.Forms.ToolStripMenuItem cmdStatisSaleHonor;
        private System.Windows.Forms.ToolStripMenuItem cmdStatisSaleCartype;
        private System.Windows.Forms.ToolStripMenuItem cmdReport;
        private System.Windows.Forms.ToolStripMenuItem cmdReportStoreDetail;
        private System.Windows.Forms.ToolStripMenuItem cmdReportStoreTypeDetail;
        private System.Windows.Forms.ToolStripMenuItem cmdReportOnroadDetail;
        private System.Windows.Forms.ToolStripMenuItem cmdReportProfitDetail;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cmdReportStoreTotal;
        private System.Windows.Forms.ToolStripMenuItem cmdReportStoreCartypeTotal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem cmdReportReserveSale;
        private System.Windows.Forms.ToolStripMenuItem cmdReportSaleTotal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem cmdReportSaleCountTotal;
        private System.Windows.Forms.ToolStripMenuItem cmdReportWholeSaleTotal;
        private System.Windows.Forms.ToolStripMenuItem cmdChart;
        private System.Windows.Forms.ToolStripMenuItem cmdChartBuyTotal;
        private System.Windows.Forms.ToolStripMenuItem cmdChartSaleKindCount;
        private System.Windows.Forms.ToolStripMenuItem cmdFinance;
        private System.Windows.Forms.ToolStripMenuItem cmdFinanceSummary;
        private System.Windows.Forms.ToolStripMenuItem cmdEnvironment;
        private System.Windows.Forms.ToolStripMenuItem cmdCarType;
        private System.Windows.Forms.ToolStripMenuItem cmdBaseData;
        private System.Windows.Forms.ToolStripMenuItem cmdCarCompany;
        private System.Windows.Forms.ToolStripMenuItem cmdUserPermission;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem cmdDatabaseBackup;
        private System.Windows.Forms.ToolStripMenuItem cmdFinanceParam;
        private System.Windows.Forms.ToolStripMenuItem cmdPassword;
        private System.Windows.Forms.ToolStripMenuItem cmdClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem cmdSpecCar;
        private System.Windows.Forms.ToolStripMenuItem cmdChartSaleCount;
        private System.Windows.Forms.ToolStripMenuItem cmdChartSaleRegion;
        private System.Windows.Forms.ToolStripMenuItem cmdChartCustsJob;
        public System.Windows.Forms.MenuStrip menuMainStrip;
        private System.Windows.Forms.ToolStripMenuItem cmdStatisRemainAmount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem cmdReportCarseriesTotal;
    }
}