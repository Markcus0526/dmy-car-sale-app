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
    public partial class FrmReportOnroadDetail : Form
    {
        #region Fields and Properties

        public static FrmReportOnroadDetail frmReportOnroadDetail;

        public DateTime startDate, endDate;

        #endregion

        #region Constructors

        public FrmReportOnroadDetail()
        {
            InitializeComponent();

            startDate = DateTime.Now;
            endDate = DateTime.Now;

            this.Activated += new EventHandler(FrmReportOnroadDetail_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmReportOnroadDetail_FormClosing);
        }

        
        #endregion

        #region Public Methods

        public static FrmReportOnroadDetail GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmReportOnroadDetail == null) //if not created yet, Create an instance
                {
                    frmReportOnroadDetail = new FrmReportOnroadDetail();
                    frmReportOnroadDetail.MdiParent = parent;
                }
            }
            return frmReportOnroadDetail;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void RenderDocument()
        {
            try
            {
                prntDoc.Body.Children.Clear();

                storReportOnroaddetailTableAdapter.Fill(cmsDB.stor_report_onroaddetail, startDate, endDate);
                
                // title
                RenderField rTitle = new RenderField();

                rTitle.Text = "在途/未提车辆明细报表";

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
                rTable.Cells[0, 1].Text = "原始提单号";
                rTable.Cells[0, 2].Text = "开单日期";
                rTable.Cells[0, 3].Text = "车辆代码";
                rTable.Cells[0, 4].Text = "选装包";
                rTable.Cells[0, 5].Text = "内饰";
                rTable.Cells[0, 6].Text = "颜色";
                rTable.Cells[0, 7].Text = "VIN码";
                rTable.Cells[0, 8].Text = "进货价格";
                rTable.Cells[0, 9].Text = "状态";

                CommonMisc.AutoSizeSpecificCols(ref rTable, new int[] { 1, 2, 3, 4, 5, 7, 8 });

                double subTotalPrice = 0.0, totalPrice = 0.0;
                int totalCount = 0;
                int groupIdx = 1;
                int rowIdx = 1, rowCount = 0;
                String cartypeText = "";

                foreach (DataRow row in cmsDB.stor_report_onroaddetail.Rows)
                {
                    // table cell
                    if (!row["cartype"].ToString().Equals(cartypeText) && cartypeText.Length > 0) 
                    {
                        rTable.Cells[groupIdx, 0].SpanRows = rowCount;

                        rTable.Cells[rowIdx, 0].Text = "";
                        rTable.Cells[rowIdx, 1].SpanCols = 7;
                        rTable.Cells[rowIdx, 1].Text = "数量 : " + rowCount;
                        rTable.Cells[rowIdx, 8].SpanCols = 2;
                        rTable.Cells[rowIdx, 8].Text = "小计 : " + String.Format("{0:########0}", subTotalPrice);

                        rowIdx++;
                        totalCount += rowCount;
                        rowCount = 0;
                        groupIdx = rowIdx;
                        subTotalPrice = 0;

                        cartypeText = row["cartype"].ToString();
                    }

                    if (cartypeText.Length == 0)
                    {
                        cartypeText = row["cartype"].ToString();
                    }

                    rTable.Cells[rowIdx, 0].Text = row["carseries"].ToString();
                    rTable.Cells[rowIdx, 1].Text = row["billno"].ToString();
                    DateTime d = DateTime.Parse(row["billdate"].ToString());
                    rTable.Cells[rowIdx, 2].Text = d.ToShortDateString();
                    rTable.Cells[rowIdx, 3].Text = row["cartype"].ToString();
                    rTable.Cells[rowIdx, 4].Text = row["subsets"].ToString();
                    rTable.Cells[rowIdx, 5].Text = row["insidesetcode"].ToString();
                    rTable.Cells[rowIdx, 6].Text = row["colorcode"].ToString();
                    rTable.Cells[rowIdx, 7].Text = row["vin"].ToString();
                    rTable.Cells[rowIdx, 8].Text = String.Format("{0:########0}", row["inprice"]);
                    if (int.Parse(row["inflag"].ToString()) != 0)
                        rTable.Cells[rowIdx, 9].Text = "入库";
                    else
                        rTable.Cells[rowIdx, 9].Text = row["carstate"].ToString();

                    subTotalPrice += double.Parse(row["inprice"].ToString());
                    totalPrice += double.Parse(row["inprice"].ToString());
                    rowIdx++;
                    rowCount++;
                }
                
                if (rowCount > 0)
                {
                    rTable.Cells[groupIdx, 0].SpanRows = rowCount;

                    rTable.Cells[rowIdx, 0].Text = "";
                    rTable.Cells[rowIdx, 1].SpanCols = 7;
                    rTable.Cells[rowIdx, 1].Text = "数量 : " + rowCount;
                    rTable.Cells[rowIdx, 8].SpanCols = 2;
                    rTable.Cells[rowIdx, 8].Text = "小计 : " + String.Format("{0:########0}", subTotalPrice);

                    totalCount += rowCount;

                    rowIdx++;

                    rTable.Cells[rowIdx, 0].Text = "总计";
                    rTable.Cells[rowIdx, 1].SpanCols = 7;
                    rTable.Cells[rowIdx, 1].Text = "总数量 : " + totalCount;
                    rTable.Cells[rowIdx, 8].SpanCols = 2;
                    rTable.Cells[rowIdx, 8].Text = "总计 ：" + String.Format("{0:########0}", totalPrice);
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

        void FrmReportOnroadDetail_Activated(object sender, EventArgs e)
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

        void FrmReportOnroadDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmReportOnroadDetail = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

    }
}
