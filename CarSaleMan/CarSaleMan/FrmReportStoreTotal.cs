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
    public partial class FrmReportStoreTotal : Form
    {
        #region Fields and Properties

        public static FrmReportStoreTotal frmReportStoreTotal;

        #endregion

        #region Constructors

        public FrmReportStoreTotal()
        {
            InitializeComponent();

            this.Activated += new EventHandler(FrmReportStoreTotal_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmReportStoreTotal_FormClosing);
        }

        
        #endregion

        #region Public Methods

        public static FrmReportStoreTotal GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmReportStoreTotal == null) //if not created yet, Create an instance
                {
                    frmReportStoreTotal = new FrmReportStoreTotal();
                    frmReportStoreTotal.MdiParent = parent;
                }
            }
            return frmReportStoreTotal;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void RenderDocument()
        {
            try
            {
                prntDoc.Body.Children.Clear();

                storReportStoreintotalTableAdapter.Fill(cmsDB.stor_report_storeintotal);
                
                // title
                RenderField rTitle = new RenderField();

                rTitle.Text = "总库存明细报表";

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

                rTable.Cells[0, 0].Text = "当前库位";
                rTable.Cells[0, 1].Text = "车辆类别";
                rTable.Cells[0, 2].Text = "开单日期";
                rTable.Cells[0, 3].Text = "入库日期";
                rTable.Cells[0, 4].Text = "车辆代码";
                rTable.Cells[0, 5].Text = "选装包";
                rTable.Cells[0, 6].Text = "内饰";
                rTable.Cells[0, 7].Text = "颜色";
                rTable.Cells[0, 8].Text = "VIN码";
                rTable.Cells[0, 9].Text = "进货价格";

                CommonMisc.AutoSizeSpecificCols(ref rTable, new int[] { 2, 3, 4, 5, 8, 9 });

                double subTotalPrice = 0.0, totalPrice = 0.0;
                int totalCount = 0;
                int groupIdx = 1;
                int rowIdx = 1, rowCount = 0;
                String cartypeText = "";

                foreach (DataRow row in cmsDB.stor_report_storeintotal.Rows)
                {
                    // table cell
                    if (!row["cartype"].ToString().Equals(cartypeText) && cartypeText.Length > 0) 
                    {
                        rTable.Cells[groupIdx, 0].SpanRows = rowCount;

                        rTable.Cells[rowIdx, 0].Text = "小计";
                        rTable.Cells[rowIdx, 1].SpanCols = 8;
                        rTable.Cells[rowIdx, 1].Text = "数量 : " + rowCount;
                        rTable.Cells[rowIdx, 9].Text = String.Format("{0:########0}", subTotalPrice);

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

                    DateTime d = DateTime.Now;
                    rTable.Cells[rowIdx, 0].Text = row["storeplace"].ToString();
                    rTable.Cells[rowIdx, 1].Text = row["carseries"].ToString();
                    bool b = DateTime.TryParse(row["billdate"].ToString(), out d);
                    if (b == true)
                        rTable.Cells[rowIdx, 2].Text = d.ToShortDateString();
                    b = DateTime.TryParse(row["indate"].ToString(), out d);
                    if (b == true)
                        rTable.Cells[rowIdx, 3].Text = d.ToShortDateString();
                    rTable.Cells[rowIdx, 4].Text = row["cartype"].ToString();
                    rTable.Cells[rowIdx, 5].Text = row["subsets"].ToString();
                    rTable.Cells[rowIdx, 6].Text = row["insidesetcode"].ToString();
                    rTable.Cells[rowIdx, 7].Text = row["colorcode"].ToString();
                    rTable.Cells[rowIdx, 8].Text = row["vin"].ToString();
                    rTable.Cells[rowIdx, 9].Text = String.Format("{0:########0}", row["inprice"]);

                    subTotalPrice += double.Parse(row["inprice"].ToString());
                    totalPrice += double.Parse(row["inprice"].ToString());
                    rowIdx++;
                    rowCount++;
                }
                
                if (rowCount > 0)
                {
                    rTable.Cells[rowIdx, 0].Text = "小计";
                    rTable.Cells[rowIdx, 1].SpanCols = 8;
                    rTable.Cells[rowIdx, 1].Text = "数量 : " + rowCount;
                    rTable.Cells[rowIdx, 9].Text = String.Format("{0:########0}", subTotalPrice);

                    totalCount += rowCount;

                    rowIdx++;

                    rTable.Cells[rowIdx, 0].Text = "总计";
                    rTable.Cells[rowIdx, 1].SpanCols = 8;
                    rTable.Cells[rowIdx, 1].Text = "总数量 : " + totalCount;
                    rTable.Cells[rowIdx, 9].Text = totalPrice.ToString();
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

        void FrmReportStoreTotal_Activated(object sender, EventArgs e)
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

        void FrmReportStoreTotal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmReportStoreTotal = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

    }
}
