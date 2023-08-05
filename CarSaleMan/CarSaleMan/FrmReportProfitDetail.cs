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
    public partial class FrmReportProfitDetail : Form
    {
        #region Fields and Properties

        public static FrmReportProfitDetail frmReportProfitDetail;

        public DateTime startDate, endDate;

        #endregion

        #region Constructors

        public FrmReportProfitDetail()
        {
            InitializeComponent();

            startDate = DateTime.Now;
            endDate = DateTime.Now;

            this.Activated += new EventHandler(FrmReportProfitDetail_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmReportProfitDetail_FormClosing);
        }

        
        #endregion

        #region Public Methods

        public static FrmReportProfitDetail GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmReportProfitDetail == null) //if not created yet, Create an instance
                {
                    frmReportProfitDetail = new FrmReportProfitDetail();
                    frmReportProfitDetail.MdiParent = parent;
                }
            }
            return frmReportProfitDetail;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void RenderDocument()
        {
            try
            {
                prntDoc.Body.Children.Clear();

                storReportProfitdetailTableAdapter.Fill(cmsDB.stor_report_profitdetail, startDate, endDate);
                
                // title
                RenderField rTitle = new RenderField();

                rTitle.Text = "进货返利报表";

                rTitle.Style.FontName = "Simsun";
                rTitle.Style.FontSize = 18;
                rTitle.Style.TextAlignHorz = AlignHorzEnum.Center;
                rTitle.Style.TextAlignVert = AlignVertEnum.Center;
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
                rTable.Cells[0, 4].Text = "颜色";
                rTable.Cells[0, 5].Text = "进货数量";
                rTable.Cells[0, 6].Text = "进货价格";
                rTable.Cells[0, 7].Text = "单车返利";
                rTable.Cells[0, 8].Text = "返利额";
                rTable.Cells[0, 9].Text = "返利率";
                rTable.Cells[0, 10].Text = "特殊返利";

                CommonMisc.AutoSizeSpecificCols(ref rTable, new int[] { 1, 2, 5, 6, 7, 8, 9, 10 });

                int totalCount = 0;
                int groupIdx = 1;
                int rowIdx = 1, rowCount = 0;
                String seriesText = "";
                decimal[] arrSmallSums = new decimal[6] { 0, 0, 0, 0, 0, 0 };
                decimal[] arrTotalSums = new decimal[6] { 0, 0, 0, 0, 0, 0 };

                foreach (DataRow row in cmsDB.stor_report_profitdetail.Rows)
                {
                    // table cell
                    if (!row["carseries"].ToString().Equals(seriesText) && seriesText.Length > 0) 
                    {
                        rTable.Cells[groupIdx, 0].SpanRows = rowCount;

                        rTable.Cells[rowIdx, 0].Text = "小计";
                        rTable.Cells[rowIdx, 1].SpanCols = 4;
                        rTable.Cells[rowIdx, 1].Text = "数量 : " + rowCount;
                        for (int i = 0; i < arrSmallSums.Length; i++)
                            rTable.Cells[rowIdx, 5 + i].Text = String.Format("{0:########0}", arrSmallSums[i]);

                        rowIdx++;
                        totalCount += rowCount;
                        rowCount = 0;
                        groupIdx = rowIdx;

                        for (int i = 0; i < arrSmallSums.Length; i++)
                        {
                            arrTotalSums[i] += arrSmallSums[i];
                            arrSmallSums[i] = 0;
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
                    rTable.Cells[rowIdx, 4].Text = row["colorcode"].ToString();
                    rTable.Cells[rowIdx, 5].Text = row["colorcount"].ToString();
                    arrSmallSums[0] += Convert.ToDecimal(row["colorcount"]);
                    rTable.Cells[rowIdx, 6].Text = String.Format("{0:########0}", row["inpricesum"]);
                    arrSmallSums[1] += Convert.ToDecimal(row["inpricesum"]);
                    rTable.Cells[rowIdx, 7].Text = String.Format("{0:########0.00}", row["profitvalavg"]);
                    arrSmallSums[2] += Convert.ToDecimal(row["profitvalavg"]);
                    rTable.Cells[rowIdx, 8].Text = String.Format("{0:########0.00}", row["profitvalsum"]);
                    arrSmallSums[3] += Convert.ToDecimal(row["profitvalsum"]);
                    rTable.Cells[rowIdx, 9].Text = String.Format("{0:########0.00}", row["outstorepriceavg"]);
                    arrSmallSums[4] += Convert.ToDecimal(row["outstorepriceavg"]);
                    rTable.Cells[rowIdx, 10].Text = String.Format("{0:########0.00}", row["propvalavg"]);
                    arrSmallSums[5] += Convert.ToDecimal(row["propvalavg"]);

                    rowIdx++;
                    rowCount++;
                }
                
                if (rowCount > 0)
                {
                    rTable.Cells[groupIdx, 0].SpanRows = rowCount;

                    rTable.Cells[rowIdx, 0].Text = "小计";
                    rTable.Cells[rowIdx, 1].SpanCols = 4;
                    rTable.Cells[rowIdx, 1].Text = "数量 : " + rowCount;
                    for (int i = 0; i < arrSmallSums.Length; i++)
                    {
                        rTable.Cells[rowIdx, 5 + i].Text = String.Format("{0:########0}", arrSmallSums[i]);
                    }

                    totalCount += rowCount;

                    rowIdx++;

                    rTable.Cells[rowIdx, 0].Text = "总计";
                    rTable.Cells[rowIdx, 1].SpanCols = 4;
                    rTable.Cells[rowIdx, 1].Text = "总数量 : " + totalCount;
                    for (int i = 0; i < arrSmallSums.Length; i++)
                    {
                        arrTotalSums[i] += arrSmallSums[i];
                        rTable.Cells[rowIdx, i + 5].Text = String.Format("{0:########0}", arrTotalSums[i]);
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

        void FrmReportProfitDetail_Activated(object sender, EventArgs e)
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

        void FrmReportProfitDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmReportProfitDetail = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion
    }
}
