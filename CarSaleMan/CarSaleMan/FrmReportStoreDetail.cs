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
    public partial class FrmReportStoreDetail : Form
    {
        #region Fields and Properties

        public static FrmReportStoreDetail frmReportStoreDetail;

        public DateTime startDate, endDate;

        #endregion

        #region Constructors

        public FrmReportStoreDetail()
        {
            InitializeComponent();

            startDate = DateTime.Now;
            endDate = DateTime.Now;

            this.Activated += new EventHandler(FrmReportStoreDetail_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmReportStoreDetail_FormClosing);
        }

        
        #endregion

        #region Public Methods

        public static FrmReportStoreDetail GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmReportStoreDetail == null) //if not created yet, Create an instance
                {
                    frmReportStoreDetail = new FrmReportStoreDetail();
                    frmReportStoreDetail.MdiParent = parent;
                }
            }
            return frmReportStoreDetail;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void RenderDocument()
        {
            try
            {
                prntDoc.Body.Children.Clear();

                storReportStoreindetailTableAdapter.Fill(cmsDB.stor_report_storeindetail, startDate, endDate);
                
                // title
                RenderField rTitle = new RenderField();

                rTitle.Text = "到货明细报表";

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
                rTable.Cells[0, 1].Text = "到车日期";
                rTable.Cells[0, 2].Text = "车辆代码";
                rTable.Cells[0, 3].Text = "选装包";
                rTable.Cells[0, 4].Text = "内饰";
                rTable.Cells[0, 5].Text = "颜色";
                rTable.Cells[0, 6].Text = "VIN码";
                rTable.Cells[0, 7].Text = "发动机号";
                rTable.Cells[0, 8].Text = "进货价";
                rTable.Cells[0, 9].Text = "进车状态";

                CommonMisc.AutoSizeSpecificCols(ref rTable);

                double subTotalPrice = 0.0, totalPrice = 0.0;
                int totalCount = 0;
                int groupIdx = 1;
                int rowIdx = 1, rowCount = 0;
                String seriesText = "";

                foreach (DataRow row in cmsDB.stor_report_storeindetail.Rows)
                {
                    // table cell
                    if (!row["carseries"].ToString().Equals(seriesText) && seriesText.Length > 0) 
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

                        seriesText = row["carseries"].ToString();
                    }

                    if (seriesText.Length == 0)
                    {
                        seriesText = row["carseries"].ToString();
                    }

                    rTable.Cells[rowIdx, 0].Text = row["carseries"].ToString();
                    DateTime d = DateTime.Parse(row["indate"].ToString());
                    rTable.Cells[rowIdx, 1].Text = d.ToShortDateString();
                    rTable.Cells[rowIdx, 2].Text = row["cartype"].ToString();
                    rTable.Cells[rowIdx, 3].Text = row["subsets"].ToString();
                    rTable.Cells[rowIdx, 4].Text = row["insidesetcode"].ToString();
                    rTable.Cells[rowIdx, 5].Text = row["colorcode"].ToString();
                    rTable.Cells[rowIdx, 6].Text = row["vin"].ToString();
                    rTable.Cells[rowIdx, 7].Text = row["engineno"].ToString();
                    rTable.Cells[rowIdx, 8].Text = String.Format("{0:########0}", row["inprice"]);
                    rTable.Cells[rowIdx, 9].Text = row["intype"].ToString();

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

        void FrmReportStoreDetail_Activated(object sender, EventArgs e)
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

        void FrmReportStoreDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmReportStoreDetail = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion
    }
}
