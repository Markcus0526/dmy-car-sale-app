using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmMDIMain : Form
    {
        #region Fields and Properties
        #endregion

        #region Constructors

        public FrmMDIMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Methods

        public void SetMenuPermission()
        {
            try
            {
                Permission.SetMenuPermission(this.menuMainStrip);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

        #region Private Methods

        private void ActivateMDIChild(Form frm)
        {
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        #endregion

        #region Event Methods

        private void FrmMDIMain_Load(object sender, EventArgs e)
        {
            try
            {
                SetMenuPermission();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdOnRoadCar_Click(object sender, EventArgs e)
        {
            try
            {
                FrmOnRoadCar frm = FrmOnRoadCar.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdStoreIn_Click(object sender, EventArgs e)
        {
            try
            {
                FrmStoreIn frm = FrmStoreIn.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdStoreChange_Click(object sender, EventArgs e)
        {
            try
            {
                FrmStoreChange frm = FrmStoreChange.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdStoreOut_Click(object sender, EventArgs e)
        {
            try
            {
                FrmStoreOut frm = FrmStoreOut.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdSpecCar_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSpecCar frm = FrmSpecCar.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdAppendRepair_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAppendRepair frm = FrmAppendRepair.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdSpecJournal_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSpecJournal frm = FrmSpecJournal.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        // statis
        private void cmdSearchOnroadCar_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchOnroadCar frm = FrmSearchOnroadCar.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdSearchStorein_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchStorein frm = FrmSearchStorein.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdSearchStoreout_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchStoreout frm = FrmSearchStoreout.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdStatisCarSaleQuarter_Click(object sender, EventArgs e)
        {
            try
            {
                FrmStatisCarSaleQuarter frm = FrmStatisCarSaleQuarter.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdStatisSaleRegion_Click(object sender, EventArgs e)
        {
            try
            {
                FrmStatisCarSaleRegion frm = FrmStatisCarSaleRegion.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdStatisCustsJob_Click(object sender, EventArgs e)
        {
            try
            {
                FrmStatisCustsJob frm = FrmStatisCustsJob.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdStatisCartypeColor_Click(object sender, EventArgs e)
        {
            try
            {
                FrmStatisCartypeColor frm = FrmStatisCartypeColor.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdStatisHandler_Click(object sender, EventArgs e)
        {
            try
            {
                FrmStatisHandler frm = FrmStatisHandler.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdStatisRemainAmount_Click(object sender, EventArgs e)
        {
            try
            {
                FrmStatisRemainAmount frm = FrmStatisRemainAmount.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdStatisSaleHonor_Click(object sender, EventArgs e)
        {
            try
            {
                FrmStatisRemainAmount frm = FrmStatisRemainAmount.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdStatisSaleCartype_Click(object sender, EventArgs e)
        {
            try
            {
                FrmStatisSaleCartype frm = FrmStatisSaleCartype.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        // report group
        private void cmdReportStoreDetail_Click(object sender, EventArgs e)
        {
            try
            {
                FrmDateSetting msg = new FrmDateSetting();
                if (msg.ShowDialog() == DialogResult.OK)
                {
                    FrmReportStoreDetail frm = FrmReportStoreDetail.GetFrmInstance(this, true);
                    frm.startDate = msg.startDate;
                    frm.endDate = msg.endDate;

                    ActivateMDIChild(frm);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdReportStoreCarTypeDetail_Click(object sender, EventArgs e)
        {
            try
            {
                FrmDateSetting msg = new FrmDateSetting();
                if (msg.ShowDialog() == DialogResult.OK)
                {
                    FrmReportStoreCarTypeDetail frm = FrmReportStoreCarTypeDetail.GetFrmInstance(this, true);
                    frm.startDate = msg.startDate;
                    frm.endDate = msg.endDate;

                    ActivateMDIChild(frm);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdReportOnroadDetail_Click(object sender, EventArgs e)
        {
            try
            {
                FrmDateSetting msg = new FrmDateSetting();
                if (msg.ShowDialog() == DialogResult.OK)
                {
                    FrmReportOnroadDetail frm = FrmReportOnroadDetail.GetFrmInstance(this, true);
                    frm.startDate = msg.startDate;
                    frm.endDate = msg.endDate;

                    ActivateMDIChild(frm);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdReportProfitDetail_Click(object sender, EventArgs e)
        {
            try
            {
                FrmDateSetting msg = new FrmDateSetting();
                if (msg.ShowDialog() == DialogResult.OK)
                {
                    FrmReportProfitDetail frm = FrmReportProfitDetail.GetFrmInstance(this, true);
                    frm.startDate = msg.startDate;
                    frm.endDate = msg.endDate;

                    ActivateMDIChild(frm);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdReportStoreTotal_Click(object sender, EventArgs e)
        {
            try
            {
                FrmReportStoreTotal frm = FrmReportStoreTotal.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdReportStoreCartypeTotal_Click(object sender, EventArgs e)
        {
            try
            {
                FrmReportStoreCartypeTotal frm = FrmReportStoreCartypeTotal.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdReportReserveSale_Click(object sender, EventArgs e)
        {
            try
            {
                FrmReportReserveSale frm = FrmReportReserveSale.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdReportSaleTotal_Click(object sender, EventArgs e)
        {
            try
            {
                FrmReportSaleTotal frm = FrmReportSaleTotal.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdReportSaleCountTotal_Click(object sender, EventArgs e)
        {
            try
            {
                FrmDateSetting msg = new FrmDateSetting();
                if (msg.ShowDialog() == DialogResult.OK)
                {
                    FrmReportSaleCountTotal frm = FrmReportSaleCountTotal.GetFrmInstance(this, true);
                    frm.startDate = msg.startDate;
                    frm.endDate = msg.endDate;

                    ActivateMDIChild(frm);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdReportWholeSaleTotal_Click(object sender, EventArgs e)
        {
            try
            {
                FrmDateSetting msg = new FrmDateSetting();
                if (msg.ShowDialog() == DialogResult.OK)
                {
                    FrmReportWholeSaleTotal frm = FrmReportWholeSaleTotal.GetFrmInstance(this, true);
                    frm.startDate = msg.startDate;
                    frm.endDate = msg.endDate;

                    ActivateMDIChild(frm);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdReportCarseriesTotal_Click(object sender, EventArgs e)
        {
            try
            {
                FrmDateSetting msg = new FrmDateSetting();
                if (msg.ShowDialog() == DialogResult.OK)
                {
                    FrmReportCarseriesTotal frm = FrmReportCarseriesTotal.GetFrmInstance(this, true);
                    frm.startDate = msg.startDate;
                    frm.endDate = msg.endDate;

                    ActivateMDIChild(frm);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        // chart group
        private void cmdChartBuyTotal_Click(object sender, EventArgs e)
        {
            try
            {
                FrmChartBuyTotal frm = FrmChartBuyTotal.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdChartSaleKindCount_Click(object sender, EventArgs e)
        {
            try
            {
                FrmChartSaleKindCount frm = FrmChartSaleKindCount.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdChartSaleCount_Click(object sender, EventArgs e)
        {
            try
            {
                FrmChartSaleCount frm = FrmChartSaleCount.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdChartSaleRegion_Click(object sender, EventArgs e)
        {
            try
            {
                FrmChartSaleRegion frm = FrmChartSaleRegion.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdChartCustsJob_Click(object sender, EventArgs e)
        {
            try
            {
                FrmChartCustsJob frm = FrmChartCustsJob.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        // finance group
        private void cmdFinanceSummary_Click(object sender, EventArgs e)
        {
            try
            {
                FrmFinanceStore frm = FrmFinanceStore.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        // setting group
        private void cmdCarType_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCarType frm = FrmCarType.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdBaseData_Click(object sender, EventArgs e)
        {
            try
            {
                FrmBaseData frm = FrmBaseData.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdCarCompany_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCarCompany frm = FrmCarCompany.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdFinanceParam_Click(object sender, EventArgs e)
        {
            try
            {
                FrmFinanceParam frm = new FrmFinanceParam();
                frm.ShowDialog();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdUserPermission_Click(object sender, EventArgs e)
        {
            try
            {
                FrmUserPermission frm = FrmUserPermission.GetFrmInstance(this, true);
                ActivateMDIChild(frm);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        // password group
        private void cmdPassword_Click(object sender, EventArgs e)
        {
            try
            {
                FrmPassChange frm = new FrmPassChange();
                frm.username = Global.LOGIN_USERNAME;
                frm.ShowDialog();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

        
        
    }
}