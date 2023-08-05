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
    public partial class FrmReportStoreCartypeTotal : Form
    {
        #region Fields and Properties

        public static FrmReportStoreCartypeTotal frmReportStoreCartypeTotal;

        #endregion

        #region Constructors

        public FrmReportStoreCartypeTotal()
        {
            InitializeComponent();

            this.Activated += new EventHandler(FrmReportStoreCartypeTotal_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmReportStoreCartypeTotal_FormClosing);
        }

        
        #endregion

        #region Public Methods

        public static FrmReportStoreCartypeTotal GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmReportStoreCartypeTotal == null) //if not created yet, Create an instance
                {
                    frmReportStoreCartypeTotal = new FrmReportStoreCartypeTotal();
                    frmReportStoreCartypeTotal.MdiParent = parent;
                }
            }
            return frmReportStoreCartypeTotal;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void RenderDocument()
        {
            try
            {
                prntDoc.Body.Children.Clear();

                storReportStoreintypetotalTableAdapter.Fill(cmsDB.stor_report_storeintypetotal);
                
                // title
                RenderField rTitle = new RenderField();

                rTitle.Text = "库存车型颜色统计报表";

                rTitle.Style.FontName = "Simsun";
                rTitle.Style.FontSize = 18;
                rTitle.Style.TextAlignHorz = AlignHorzEnum.Center;
                rTitle.Style.TextAlignVert = AlignVertEnum.Center;
                this.prntDoc.Body.Children.Add(rTitle);

                RenderField rDate = new RenderField();

                rDate.Text = "打印日期：" + DateTime.Now.ToShortDateString();
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
                rTable.Cells[0, 5].Text = "数量";

                CommonMisc.AutoSizeSpecificCols(ref rTable, new int[] { 0, 1 });

                int subTotalPrice = 0, totalPrice = 0;
                int totalCount = 0;
                int groupIdx = 1;
                int rowIdx = 1, rowCount = 0;
                String seriesText = "";

                foreach (DataRow row in cmsDB.stor_report_storeintypetotal.Rows)
                {
                    // table cell
                    if (!row["carseries"].ToString().Equals(seriesText) && seriesText.Length > 0) 
                    {
                        rTable.Cells[groupIdx, 0].SpanRows = rowCount;

                        rTable.Cells[rowIdx, 0].Text = "小计";
                        rTable.Cells[rowIdx, 1].SpanCols = 5;
                        rTable.Cells[rowIdx, 1].Text = "数量 : " + String.Format("{0:########0}", subTotalPrice);

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
                    rTable.Cells[rowIdx, 1].Text = row["cartype"].ToString();
                    rTable.Cells[rowIdx, 2].Text = row["subsets"].ToString();
                    rTable.Cells[rowIdx, 3].Text = row["insidesetcode"].ToString();
                    rTable.Cells[rowIdx, 4].Text = row["colorcode"].ToString() + "(" + row["colorname"].ToString() + ")";
                    rTable.Cells[rowIdx, 5].Text = row["colorcount"].ToString();

                    subTotalPrice += int.Parse(row["colorcount"].ToString());
                    totalPrice += int.Parse(row["colorcount"].ToString());
                    rowIdx++;
                    rowCount++;
                }
                
                if (rowCount > 0)
                {
                    rTable.Cells[rowIdx, 0].Text = "小计";
                    rTable.Cells[rowIdx, 1].SpanCols = 5;
                    rTable.Cells[rowIdx, 1].Text = "数量 : " + subTotalPrice;

                    totalCount += rowCount;

                    rowIdx++;

                    rTable.Cells[rowIdx, 0].Text = "总计";
                    rTable.Cells[rowIdx, 1].SpanCols = 5;
                    rTable.Cells[rowIdx, 1].Text = "总数量 : " + totalCount;
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

        void FrmReportStoreCartypeTotal_Activated(object sender, EventArgs e)
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

        void FrmReportStoreCartypeTotal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmReportStoreCartypeTotal = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion
    }
}
