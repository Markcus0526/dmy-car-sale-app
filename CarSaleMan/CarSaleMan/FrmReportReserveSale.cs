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
    public partial class FrmReportReserveSale : Form
    {
        #region Fields and Properties

        public static FrmReportReserveSale frmReportReserveSale;

        #endregion

        #region Constructors

        public FrmReportReserveSale()
        {
            InitializeComponent();

            this.Activated += new EventHandler(FrmReportReserveSale_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmReportReserveSale_FormClosing);
        }

        
        #endregion

        #region Public Methods

        public static FrmReportReserveSale GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmReportReserveSale == null) //if not created yet, Create an instance
                {
                    frmReportReserveSale = new FrmReportReserveSale();
                    frmReportReserveSale.MdiParent = parent;
                }
            }
            return frmReportReserveSale;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void RenderDocument()
        {
            try
            {
                prntDoc.Body.Children.Clear();

                storReportStoreoutreserveTableAdapter.Fill(cmsDB.stor_report_storeoutreserve);
                
                // title
                RenderField rTitle = new RenderField();

                rTitle.Text = "预售明细报表";

                rTitle.Style.FontName = "Simsun";
                rTitle.Style.FontSize = 18;
                rTitle.Style.TextAlignHorz = AlignHorzEnum.Center;
                rTitle.Style.TextAlignVert = AlignVertEnum.Center;
                rTitle.Style.Spacing.Bottom = "5mm";
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
                rTable.Cells[0, 5].Text = "提车日期";
                rTable.Cells[0, 6].Text = "单位名称";
                rTable.Cells[0, 7].Text = "VIN码";
                rTable.Cells[0, 8].Text = "销售站点";
                rTable.Cells[0, 9].Text = "零售价格";
                rTable.Cells[0, 10].Text = "其它1";
                rTable.Cells[0, 11].Text = "其它2";
                rTable.Cells[0, 12].Text = "其它3";
                rTable.Cells[0, 13].Text = "其它4";
                rTable.Cells[0, 14].Text = "累计利息";
                rTable.Cells[0, 15].Text = "差价";
                rTable.Cells[0, 16].Text = "备注";

                CommonMisc.AutoSizeSpecificCols(ref rTable, new int[] { 1, 5, 7 });

                int totalCount = 0;
                int groupIdx = 1;
                int rowIdx = 1, rowCount = 0;
                String seriesText = "";
                decimal[] arrSmallSum = new decimal[7] { 0, 0, 0, 0, 0, 0, 0 };
                decimal[] arrTotalSum = new decimal[7] { 0, 0, 0, 0, 0, 0, 0 };

                foreach (DataRow row in cmsDB.stor_report_storeoutreserve.Rows)
                {
                    // table cell
                    if (!row["carseries"].ToString().Equals(seriesText) && seriesText.Length > 0) 
                    {
                        rTable.Cells[groupIdx, 0].SpanRows = rowCount;

                        rTable.Cells[rowIdx, 0].Text = "小计";
                        rTable.Cells[rowIdx, 1].SpanCols = 8;
                        rTable.Cells[rowIdx, 1].Text = "数量 : " + rowCount;

                        for (int i = 0; i < arrSmallSum.Length; i++)
                        {
                            rTable.Cells[rowIdx, i + 9].Text = String.Format("{0:########0}", arrSmallSum[i]);
                        }

                        rowIdx++;
                        totalCount += rowCount;
                        rowCount = 0;
                        groupIdx = rowIdx;
                        
                        for (int i = 0; i < arrSmallSum.Length; i++)
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

                    DateTime d = DateTime.Now;
                    rTable.Cells[rowIdx, 0].Text = row["carseries"].ToString();
                    rTable.Cells[rowIdx, 1].Text = row["cartype"].ToString();
                    rTable.Cells[rowIdx, 2].Text = row["subsets"].ToString();
                    rTable.Cells[rowIdx, 3].Text = row["insidesetcode"].ToString();
                    rTable.Cells[rowIdx, 4].Text = row["colorcode"].ToString();
                    bool b = DateTime.TryParse(row["outdate"].ToString(), out d);
                    if (b == true)
                        rTable.Cells[rowIdx, 5].Text = d.ToShortDateString();
                    rTable.Cells[rowIdx, 6].Text = row["salecompany"].ToString();
                    rTable.Cells[rowIdx, 7].Text = row["vin"].ToString();
                    rTable.Cells[rowIdx, 8].Text = row["saleplace"].ToString();

                    rTable.Cells[rowIdx, 9].Text = String.Format("{0:########0}", row["votecost"]);
                    arrSmallSum[0] += Convert.ToDecimal(row["votecost"]);
                    rTable.Cells[rowIdx, 10].Text = row["otherprice1"].ToString();
                    arrSmallSum[1] += Convert.ToDecimal(row["otherprice1"]);
                    rTable.Cells[rowIdx, 11].Text = row["otherprice2"].ToString();
                    arrSmallSum[2] += Convert.ToDecimal(row["otherprice2"]);
                    rTable.Cells[rowIdx, 12].Text = row["otherprice3"].ToString();
                    arrSmallSum[3] += Convert.ToDecimal(row["otherprice3"]);
                    rTable.Cells[rowIdx, 13].Text = row["otherprice4"].ToString();
                    arrSmallSum[4] += Convert.ToDecimal(row["otherprice4"]);
                    rTable.Cells[rowIdx, 14].Text = row["interestprice"].ToString();
                    arrSmallSum[5] += Convert.ToDecimal(row["interestprice"]);
                    rTable.Cells[rowIdx, 15].Text = String.Format("{0:########0}", row["pricediff"]);
                    arrSmallSum[6] += Convert.ToDecimal(row["pricediff"]);
                    rTable.Cells[rowIdx, 16].Text = "";

                    rowIdx++;
                    rowCount++;
                }
                
                if (rowCount > 0)
                {
                    rTable.Cells[rowIdx, 0].Text = "小计";
                    rTable.Cells[rowIdx, 1].SpanCols = 8;
                    rTable.Cells[rowIdx, 1].Text = "数量 : " + rowCount;

                    for (int i = 0; i < arrSmallSum.Length; i++)
                    {
                        rTable.Cells[rowIdx, i + 9].Text = String.Format("{0:########0}", arrSmallSum[i]);
                    }

                    totalCount += rowCount;

                    rowIdx++;

                    rTable.Cells[rowIdx, 0].Text = "总计";
                    rTable.Cells[rowIdx, 1].SpanCols = 8;
                    rTable.Cells[rowIdx, 1].Text = "总数量 : " + totalCount;

                    for (int i = 0; i < arrTotalSum.Length; i++)
                    {
                        arrTotalSum[i] += arrSmallSum[i];
                        rTable.Cells[rowIdx, i + 9].Text = String.Format("{0:########0}", arrTotalSum[i]);
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

        void FrmReportReserveSale_Activated(object sender, EventArgs e)
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

        void FrmReportReserveSale_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmReportReserveSale = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion
    }
}
