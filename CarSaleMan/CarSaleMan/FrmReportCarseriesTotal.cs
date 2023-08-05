using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.C1Preview;
using C1.C1Report;

namespace CarSaleMan
{
    public partial class FrmReportCarseriesTotal : Form
    {
        #region Fields and Properties

        public static FrmReportCarseriesTotal frmReportCarseriesTotal;

        public DateTime startDate, endDate;

        #endregion

        #region Constructors

        public FrmReportCarseriesTotal()
        {
            InitializeComponent();

            startDate = DateTime.Now;
            endDate = DateTime.Now;

            this.Activated += new EventHandler(FrmReportCarseriesTotal_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmReportCarseriesTotal_FormClosing);
        }

        
        #endregion

        #region Public Methods

        public static FrmReportCarseriesTotal GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmReportCarseriesTotal == null) //if not created yet, Create an instance
                {
                    frmReportCarseriesTotal = new FrmReportCarseriesTotal();
                    frmReportCarseriesTotal.MdiParent = parent;
                }
            }
            return frmReportCarseriesTotal;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void RenderDocument()
        {
            try
            {
                prntDoc.Body.Children.Clear();

                storReportCarseriestotalTableAdapter.Fill(cmsDB.stor_report_carseriestotal, 100, startDate, endDate);
                storReportCarseriesdetailTableAdapter.Fill(cmsDB.stor_report_carseriesdetail, 100, startDate, endDate);

                // title
                RenderField rTitle = new RenderField();

                rTitle.Text = "进销存汇总报表";

                rTitle.Style.FontName = "Simsun";
                rTitle.Style.FontSize = 18;
                rTitle.Style.TextAlignHorz = AlignHorzEnum.Center;
                rTitle.Style.TextAlignVert = AlignVertEnum.Center;
                rTitle.Style.Spacing.Bottom = "5mm";
                this.prntDoc.Body.Children.Add(rTitle);

                RenderTable rTable = new RenderTable();
                rTable.Style.GridLines.All = new LineDef(Color.Black);
                rTable.SplitHorzBehavior = SplitBehaviorEnum.SplitIfNeeded;
                rTable.ColumnSizingMode = TableSizingModeEnum.Auto;
                rTable.Style.TextAlignHorz = AlignHorzEnum.Center;
                rTable.Style.TextAlignVert = AlignVertEnum.Center;
                rTable.CellStyle.Padding.All = C1.C1Preview.Unit.BoldLineWidth;

                rTable.Cells[0, 0].SpanRows = 2;
                rTable.Cells[0, 0].SpanCols = 2;
                rTable.Cells[0, 0].Text = String.Format("{0}年 {1}月", startDate.Year, startDate.Month);

                rTable.Cells[0, 2].SpanCols = 3;
                rTable.Cells[0, 2].Text = "本期进货";
                rTable.Cells[1, 2].Text = "SVW";
                rTable.Cells[1, 3].Text = "其它进货";
                rTable.Cells[1, 4].Text = "总计";

                rTable.Cells[0, 5].SpanCols = 3;
                rTable.Cells[0, 5].Text = "库存";
                rTable.Cells[1, 5].Text = "在库";
                rTable.Cells[1, 6].Text = "在途";
                rTable.Cells[1, 7].Text = "总计";

                rTable.Cells[0, 8].SpanCols = 3;
                rTable.Cells[0, 8].Text = "零售";
                rTable.Cells[1, 8].Text = "SVW";
                rTable.Cells[1, 9].Text = "其它零售";
                rTable.Cells[1, 10].Text = "总计";
                

                CommonMisc.AutoSizeSpecificCols(ref rTable);

                int rowIdx = 2;
                foreach (DataRow row in cmsDB.stor_report_carseriestotal.Rows)
                {
                    rTable.Cells[rowIdx, 0].SpanCols = 2;
                    rTable.Cells[rowIdx, 0].Text = row["carseries"].ToString();
                    rTable.Cells[rowIdx, 2].Text = row["onroadcount"].ToString();
                    rTable.Cells[rowIdx, 3].Text = "0";
                    rTable.Cells[rowIdx, 4].Text = row["onroadcount"].ToString();
                    rTable.Cells[rowIdx, 5].Text = row["storeincount1"].ToString();
                    rTable.Cells[rowIdx, 6].Text = row["storeincount2"].ToString();
                    rTable.Cells[rowIdx, 7].Text = (Convert.ToInt32(row["storeincount1"].ToString()) + Convert.ToInt32(row["storeincount2"].ToString())).ToString();
                    rTable.Cells[rowIdx, 8].Text = row["storeoutcount"].ToString();
                    rTable.Cells[rowIdx, 9].Text = "0";
                    rTable.Cells[rowIdx, 10].Text = row["storeoutcount"].ToString();

                    rowIdx++;
                }

                int groupIdx = rowIdx, rowCount = 0;
                String seriesText = "";

                foreach (DataRow row in cmsDB.stor_report_carseriesdetail.Rows)
                {
                    if (!row["carseries"].ToString().Equals(seriesText))
                    {
                        rTable.Cells[groupIdx, 0].SpanRows = rowCount;

                        rowIdx++;
                        rowCount = 0;
                        groupIdx = rowIdx;

                        seriesText = row["carseries"].ToString();
                    }

                    if (seriesText.Length == 0)
                    {
                        seriesText = row["carseries"].ToString();
                    }

                    rTable.Cells[rowIdx, 0].Text = row["carseries"].ToString();
                    rTable.Cells[rowIdx, 1].Text = row["cartype"].ToString();
                    rTable.Cells[rowIdx, 2].Text = row["onroadcount"].ToString();
                    rTable.Cells[rowIdx, 3].Text = "0";
                    rTable.Cells[rowIdx, 4].Text = row["onroadcount"].ToString();
                    rTable.Cells[rowIdx, 5].Text = row["storeincount1"].ToString();
                    rTable.Cells[rowIdx, 6].Text = row["storeincount2"].ToString();
                    rTable.Cells[rowIdx, 7].Text = (Convert.ToInt32(row["storeincount1"].ToString()) + Convert.ToInt32(row["storeincount2"].ToString())).ToString();
                    rTable.Cells[rowIdx, 8].Text = row["storeoutcount"].ToString();
                    rTable.Cells[rowIdx, 9].Text = "0";
                    rTable.Cells[rowIdx, 10].Text = row["storeoutcount"].ToString();

                    rowIdx++;
                    rowCount++;
                }

                if (rowCount > 0)
                {
                    rTable.Cells[groupIdx, 0].SpanRows = rowCount;
                }
                prntDoc.Body.Children.Add(rTable);
                prntDoc.Generate();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

        #region Event Methods

        void FrmReportCarseriesTotal_Activated(object sender, EventArgs e)
        {
            try
            {
                RenderDocument();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void FrmReportCarseriesTotal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmReportCarseriesTotal = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion
    }
}
