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
    public partial class FrmReportSaleCountTotal : Form
    {
        #region Fields and Properties

        public static FrmReportSaleCountTotal frmReportSaleCountTotal;

        public DateTime startDate, endDate;

        #endregion

        #region Constructors

        public FrmReportSaleCountTotal()
        {
            InitializeComponent();

            startDate = DateTime.Now;
            endDate = DateTime.Now;

            this.Activated += new EventHandler(FrmReportSaleCountTotal_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmReportSaleCountTotal_FormClosing);
        }

        
        #endregion

        #region Public Methods

        public static FrmReportSaleCountTotal GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmReportSaleCountTotal == null) //if not created yet, Create an instance
                {
                    frmReportSaleCountTotal = new FrmReportSaleCountTotal();
                    frmReportSaleCountTotal.MdiParent = parent;
                }
            }
            return frmReportSaleCountTotal;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void RenderDocument()
        {
            try
            {
                prntDoc.Body.Children.Clear();

                storReportStoreoutcountTableAdapter.Fill(cmsDB.stor_report_storeoutcount, startDate, endDate);

                // title
                RenderField rTitle = new RenderField();

                rTitle.Text = "销售数量汇总报表";

                rTitle.Style.FontName = "Simsun";
                rTitle.Style.FontSize = 18;
                rTitle.Style.TextAlignHorz = AlignHorzEnum.Center;
                rTitle.Style.TextAlignVert = AlignVertEnum.Center;
                rTitle.Style.Spacing.Bottom = "5mm";
                this.prntDoc.Body.Children.Add(rTitle);

                RenderField rDate = new RenderField();

                rDate.Text = "从：" + startDate.ToShortDateString() + " - 到：" + endDate.ToShortDateString();
                rDate.Style.FontName = "Simsun";
                rDate.Style.TextAlignHorz = AlignHorzEnum.Right;
                rDate.Style.TextAlignVert = AlignVertEnum.Center;
                rDate.Style.Spacing.Bottom = "1mm";
                prntDoc.Body.Children.Add(rDate);

                RenderTable rTable = new RenderTable();
                rTable.Style.GridLines.All = new LineDef(Color.Black);
                rTable.SplitHorzBehavior = SplitBehaviorEnum.SplitIfNeeded;
                rTable.Style.FontSize = 9;
                rTable.Style.TextAlignHorz = AlignHorzEnum.Center;
                rTable.Style.TextAlignVert = AlignVertEnum.Center;
                rTable.CellStyle.Padding.All = C1.C1Preview.Unit.BoldLineWidth;

                rTable.Cells[0, 0].Text = "车辆类别";
                rTable.Cells[0, 1].Text = "车辆代码";
                rTable.Cells[0, 2].Text = "选装包";
                rTable.Cells[0, 3].Text = "内饰";
                rTable.Cells[0, 4].Text = "数量";
                rTable.Cells[0, 5].Text = "本月销售";
                rTable.Cells[0, 6].Text = "累计销售";
                rTable.Cells[0, 7].Text = "销售额";
                rTable.Cells[0, 8].Text = "销售利润";

                CommonMisc.AutoSizeSpecificCols(ref rTable);

                int totalCount = 0;
                int groupIdx = 1;
                int rowIdx = 1, rowCount = 0, nSaleCount = 0;
                String seriesText = "";
                decimal[] arrSmallSum = new decimal[4] { 0, 0, 0, 0 };
                decimal[] arrTotalSum = new decimal[4] { 0, 0, 0, 0 };

                foreach (DataRow row in cmsDB.stor_report_storeoutcount.Rows)
                {
                    // table cell
                    if (!row["carseries"].ToString().Equals(seriesText) && seriesText.Length > 0)
                    {
                        rTable.Cells[groupIdx, 0].SpanRows = rowCount;

                        rTable.Cells[rowIdx, 0].Text = "小计";
                        rTable.Cells[rowIdx, 1].SpanCols = 4;
                        rTable.Cells[rowIdx, 1].Text = "数量 : " + nSaleCount;

                        for (int i = 0; i < arrSmallSum.Length; i++)
                        {
                            rTable.Cells[rowIdx, 5 + i].Text = String.Format("{0:########0}", arrSmallSum[i]);
                        }

                        rowIdx++;
                        totalCount += nSaleCount;
                        rowCount = 0;
                        nSaleCount = 0;
                        groupIdx = rowIdx;

                        for (int i = 0; i < arrTotalSum.Length; i++)
                        {
                            arrTotalSum[i] += arrSmallSum[i];
                            arrSmallSum[i] = 0;
                        }

                        seriesText = row["carseries"].ToString();
                    }

                    if (seriesText.Length == 0)
                    {
                        seriesText = row["carseries"].ToString();
                    }

                    rTable.Cells[rowIdx, 0].Text = row["carseries"].ToString();
                    rTable.Cells[rowIdx, 1].Text = row["cartype"].ToString();
                    rTable.Cells[rowIdx, 2].Text = row["subsets"].ToString();
                    rTable.Cells[rowIdx, 3].Text = row["insidesetcode"].ToString();
                    double d1, d2;
                    bool b1 = double.TryParse(row["count1"].ToString(), out d1);
                    bool b2 = double.TryParse(row["count2"].ToString(), out d2);
                    rTable.Cells[rowIdx, 4].Text = (d1 + d2).ToString();
                    rTable.Cells[rowIdx, 5].Text = row["curmonthcount"].ToString();
                    arrSmallSum[0] += Convert.ToDecimal(row["curmonthcount"]);
                    rTable.Cells[rowIdx, 6].Text = row["allcount"].ToString();
                    arrSmallSum[1] += Convert.ToDecimal(row["allcount"]);
                    if (row["allcount"] != null)
                        nSaleCount += Convert.ToInt32(row["allcount"]);
                    rTable.Cells[rowIdx, 7].Text = String.Format("{0:########0}", row["outprice"]);
                    arrSmallSum[2] += Convert.ToDecimal(row["outprice"]);
                    rTable.Cells[rowIdx, 7].Text = String.Format("{0:########0}", row["diffprice"]);
                    arrSmallSum[3] += Convert.ToDecimal(row["diffprice"]);

                    rowIdx++;
                    rowCount++;
                }

                if (rowCount > 0)
                {
                    rTable.Cells[rowIdx, 0].Text = "小计";
                    rTable.Cells[rowIdx, 1].SpanCols = 4;
                    rTable.Cells[rowIdx, 1].Text = "数量 : " + nSaleCount;

                    for (int i = 0; i < arrSmallSum.Length; i++)
                    {
                        rTable.Cells[rowIdx, 5 + i].Text = String.Format("{0:########0}", arrSmallSum[i]);
                    }

                    totalCount += nSaleCount;

                    rowIdx++;

                    rTable.Cells[rowIdx, 0].Text = "总计";
                    rTable.Cells[rowIdx, 1].SpanCols = 4;
                    rTable.Cells[rowIdx, 1].Text = "总数量 : " + totalCount;

                    for (int i = 0; i < arrTotalSum.Length; i++)
                    {
                        arrTotalSum[i] += arrSmallSum[i];
                        rTable.Cells[rowIdx, 5 + i].Text = String.Format("{0:########0}", arrTotalSum[i]);
                    }
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

        void FrmReportSaleCountTotal_Activated(object sender, EventArgs e)
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

        void FrmReportSaleCountTotal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmReportSaleCountTotal = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion
    }
}
