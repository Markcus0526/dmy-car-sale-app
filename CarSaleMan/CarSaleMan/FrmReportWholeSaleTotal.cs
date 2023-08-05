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
    public partial class FrmReportWholeSaleTotal : Form
    {
        #region Fields and Properties

        public static FrmReportWholeSaleTotal frmReportWholeSaleTotal;

        public DateTime startDate, endDate;

        #endregion

        #region Constructors

        public FrmReportWholeSaleTotal()
        {
            InitializeComponent();

            startDate = DateTime.Now;
            endDate = DateTime.Now;

            this.Activated += new EventHandler(FrmReportWholeSaleTotal_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmReportWholeSaleTotal_FormClosing);
        }

        
        #endregion

        #region Public Methods

        public static FrmReportWholeSaleTotal GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmReportWholeSaleTotal == null) //if not created yet, Create an instance
                {
                    frmReportWholeSaleTotal = new FrmReportWholeSaleTotal();
                    frmReportWholeSaleTotal.MdiParent = parent;
                }
            }
            return frmReportWholeSaleTotal;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void RenderDocument()
        {
            try
            {
                prntDoc.Body.Children.Clear();

                storReportWholesaletotalTableAdapter.Fill(cmsDB.stor_report_wholesaletotal, startDate, endDate);

                // title
                RenderField rTitle = new RenderField();

                rTitle.Text = "销售批发零售汇总报表";

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
                rTable.Cells[0, 7].Text = "合计";
                rTable.Cells[1, 4].Text = "零售";
                rTable.Cells[1, 5].Text = "批发";
                rTable.Cells[1, 6].Text = "大客户";

                rTable.Cells[0, 0].SpanRows = 2;
                rTable.Cells[0, 1].SpanRows = 2;
                rTable.Cells[0, 2].SpanRows = 2;
                rTable.Cells[0, 3].SpanRows = 2;
                rTable.Cells[0, 4].SpanCols = 3;
                rTable.Cells[0, 7].SpanRows = 2;

                CommonMisc.AutoSizeSpecificCols(ref rTable);

                int subTotalPrice = 0, totalPrice = 0;
                int totalCount = 0;
                int nTotalSum1 = 0, nTotalSum2 = 0, nTotalSum3 = 0;
                int nSubSum1 = 0, nSubSum2 = 0, nSubSum3 = 0;
                int groupIdx = 2;
                int rowIdx = 2, rowCount = 0;
                String seriesText = "";

                foreach (DataRow row in cmsDB.stor_report_wholesaletotal.Rows)
                {
                    // table cell
                    if (!row["carseries"].ToString().Equals(seriesText) && seriesText.Length > 0)
                    {
                        rTable.Cells[rowIdx, 0].Text = "小计";
                        rTable.Cells[rowIdx, 0].SpanCols = 4;
                        rTable.Cells[rowIdx, 4].Text = Convert.ToString(nSubSum1);
                        rTable.Cells[rowIdx, 5].Text = Convert.ToString(nSubSum2);
                        rTable.Cells[rowIdx, 6].Text = Convert.ToString(nSubSum3);
                        rTable.Cells[rowIdx, 7].Text = Convert.ToString(subTotalPrice);

                        rowIdx++;
                        totalCount += rowCount;
                        nTotalSum1 += nSubSum1;
                        nTotalSum2 += nSubSum2;
                        nTotalSum3 += nSubSum3;
                        rowCount = 0;
                        nSubSum1 = 0;
                        nSubSum2 = 0;
                        nSubSum3 = 0;
                        groupIdx = rowIdx;
                        subTotalPrice = 0;

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
                    rTable.Cells[rowIdx, 4].Text = row["retailcount"].ToString();
                    rTable.Cells[rowIdx, 5].Text = row["salecount"].ToString();
                    rTable.Cells[rowIdx, 6].Text = row["customcount"].ToString();
                    rTable.Cells[rowIdx, 7].Text = row["allcount"].ToString();

                    subTotalPrice += int.Parse(row["allcount"].ToString());
                    totalPrice += int.Parse(row["allcount"].ToString());

                    rowIdx++;
                    rowCount++;
                    nSubSum1 += int.Parse(row["retailcount"].ToString());
                    nSubSum2 += int.Parse(row["salecount"].ToString());
                    nSubSum3 += int.Parse(row["allcount"].ToString());
                }

                if (rowCount > 0)
                {
                    rTable.Cells[rowIdx, 0].Text = "小计";
                    rTable.Cells[rowIdx, 0].SpanCols = 4;
                    rTable.Cells[rowIdx, 4].Text = Convert.ToString(nSubSum1);
                    rTable.Cells[rowIdx, 5].Text = Convert.ToString(nSubSum2);
                    rTable.Cells[rowIdx, 6].Text = Convert.ToString(nSubSum3);
                    rTable.Cells[rowIdx, 7].Text = Convert.ToString(subTotalPrice);

                    totalCount += rowCount;
                    nTotalSum1 += nSubSum1;
                    nTotalSum2 += nSubSum2;
                    nTotalSum3 += nSubSum3;

                    rowIdx++;

                    rTable.Cells[rowIdx, 0].Text = "总计";
                    //rTable.Cells[groupIdx, 0].SpanRows = totalCount;
                    rTable.Cells[rowIdx, 0].SpanCols = 4;
                    rTable.Cells[rowIdx, 4].Text = Convert.ToString(nTotalSum1);
                    rTable.Cells[rowIdx, 5].Text = Convert.ToString(nTotalSum2);
                    rTable.Cells[rowIdx, 6].Text = Convert.ToString(nTotalSum3);
                    rTable.Cells[rowIdx, 7].Text = /*"总数量 : " + */Convert.ToString(totalPrice);
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

        void FrmReportWholeSaleTotal_Activated(object sender, EventArgs e)
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

        void FrmReportWholeSaleTotal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmReportWholeSaleTotal = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion
    }
}
