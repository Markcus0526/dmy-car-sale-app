using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.Globalization;
using C1.C1Excel;
using System.Collections;
using System.Diagnostics;

namespace CarSaleMan
{
    public partial class FrmStatisRemainAmount : Form
    {
        #region Fields and Properties

        public static FrmStatisRemainAmount frmStatisRemainAmount;

        private C1XLBook xlbook;
        private Hashtable styles;

        private bool writable;

        #endregion

        #region Constructors

        public FrmStatisRemainAmount()
        {
            InitializeComponent();

            xlbook = new C1XLBook();
            writable = true;

            this.Activated += new EventHandler(FrmStatisRemainAmount_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmStatisRemainAmount_FormClosing);

            // flexgrid events

        }
                
        #endregion

        #region Public Methods

        public static FrmStatisRemainAmount GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmStatisRemainAmount == null) //if not created yet, Create an instance
                {
                    frmStatisRemainAmount = new FrmStatisRemainAmount();
                    frmStatisRemainAmount.MdiParent = parent;
                }
            }
            return frmStatisRemainAmount;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void LoadTableFromDB()
        {
            try
            {
                storStatisRemainAmountTableAdapter.Fill(cmsDB.stor_statis_remainamount);

                gridStatis.Rows.Count = 1;
                gridStatis.Rows.Fixed = 1;

                CellStyle cs = gridStatis.Styles.Add("summy");
                cs.BackColor = Color.Gainsboro;
                cs.ForeColor = Color.BlueViolet;
                cs.DataType = typeof(int);

                List<String> carserieslist = new List<String>();
                List<int> totalstorecountlist = new List<int>();
                List<int> substorecountlist = new List<int>();
                List<double> totalstoreamountlist = new List<double>();
                List<double> substoreamountlist = new List<double>();
                List<int> totalonroadcountlist = new List<int>();
                List<int> subonroadcountlist = new List<int>();
                List<double> totalonroadamountlist = new List<double>();
                List<double> subonroadamountlist = new List<double>();

                DataRow[] rows = cmsDB.stor_statis_remainamount.Select("");
                int k = 0;
                for (int i = 0; i < cmsDB.stor_statis_remainamount.Count; i++)
                {
                    String carseries = rows[i]["carseries"].ToString();
                    if (carserieslist.Count > 0 && carserieslist.Contains(carseries) == true)
                    {
                        int storecount = 0, onroadcount = 0;
                        double storeamount = 0.0, onroadamount = 0.0;
                        double storecountpercent = 0.0, storeamountpercent = 0.0;

                        gridStatis.Rows.Insert(k + 1);

                        gridStatis[k + 1, 0] = rows[i]["carseries"].ToString();
                        gridStatis[k + 1, 1] = rows[i]["cartype"].ToString();
                        gridStatis[k + 1, 2] = rows[i]["subsets"].ToString();
                        gridStatis[k + 1, 3] = rows[i]["insidesetcode"].ToString();
                        storecount = Convert.ToInt32(rows[i]["storecount"].ToString());
                        gridStatis[k + 1, 4] = storecount;
                        storeamount = Convert.ToDouble(rows[i]["storeamount"].ToString());
                        gridStatis[k + 1, 5] = storeamount;
                        onroadcount = Convert.ToInt32(rows[i]["onroadcount"].ToString());
                        gridStatis[k + 1, 6] = onroadcount;
                        onroadamount = Convert.ToDouble(rows[i]["onroadamount"].ToString());
                        gridStatis[k + 1, 7] = onroadamount;
                        if (storecount == 0 && onroadcount == 0)
                            storecountpercent = 0.0;
                        else
                            storecountpercent = (double)storecount / (storecount + onroadcount) * 100;
                        gridStatis[k + 1, 8] = storecountpercent;
                        if (storeamount == 0 && onroadamount == 0)
                            storeamountpercent = 0.0;
                        else
                            storeamountpercent = storeamount / (storeamount + onroadamount) * 100;
                        gridStatis[k + 1, 9] = storeamountpercent;

                        substorecountlist.Add(storecount);
                        substoreamountlist.Add(storeamount);
                        subonroadcountlist.Add(onroadcount);
                        subonroadamountlist.Add(onroadamount);
                        k++;
                    }
                    else
                    {
                        if (carserieslist.Count > 0)
                        {
                            gridStatis.Rows.Insert(k + 1);

                            int substorecount = 0, subonroadcount = 0;
                            double substoreamount = 0.0, subonroadamount = 0.0;
                            double substorecountpercent = 0.0, substoreamountpercent = 0.0;

                            for (int j = 0; j < substorecountlist.Count; j++)
                            {
                                substorecount += substorecountlist[j];
                                subonroadcount += subonroadcountlist[j];
                                substoreamount += substoreamountlist[j];
                                subonroadamount += subonroadamountlist[j];
                            }

                            if (substorecount == 0 && subonroadcount == 0)
                                substorecountpercent = 0.0;
                            else
                                substorecountpercent = (double)substorecount / (substorecount + subonroadcount) * 100;

                            if (substoreamount == 0 && subonroadamount == 0)
                                substoreamountpercent = 0.0;
                            else
                                substoreamountpercent = (double)substoreamount / (substorecount + subonroadamount) * 100;

                            totalstorecountlist.Add(substorecount);
                            totalonroadcountlist.Add(subonroadcount);
                            totalstoreamountlist.Add(substoreamount);
                            totalonroadamountlist.Add(subonroadamount);

                            gridStatis[k + 1, 0] = "小计";
                            gridStatis[k + 1, 4] = substorecount;
                            gridStatis[k + 1, 5] = substoreamount;
                            gridStatis[k + 1, 6] = subonroadcount;
                            gridStatis[k + 1, 7] = subonroadamount;
                            gridStatis[k + 1, 8] = substorecountpercent;
                            gridStatis[k + 1, 9] = substoreamountpercent;
                            
                            CellRange cr = gridStatis.GetCellRange(k + 1, 0, k + 1, gridStatis.Cols.Count - 1);
                            cr.Style = gridStatis.Styles["summy"];

                            substorecountlist.Clear();
                            subonroadcountlist.Clear();
                            substoreamountlist.Clear();
                            subonroadamountlist.Clear();
                            k++;
                        }

                        carserieslist.Add(carseries);

                        int storecount = 0, onroadcount = 0;
                        double storeamount = 0.0, onroadamount = 0.0;
                        double storecountpercent = 0.0, storeamountpercent = 0.0;

                        gridStatis.Rows.Insert(k + 1);

                        gridStatis[k + 1, 0] = rows[i]["carseries"].ToString();
                        gridStatis[k + 1, 1] = rows[i]["cartype"].ToString();
                        gridStatis[k + 1, 2] = rows[i]["subsets"].ToString();
                        gridStatis[k + 1, 3] = rows[i]["insidesetcode"].ToString();
                        storecount = Convert.ToInt32(rows[i]["storecount"].ToString());
                        gridStatis[k + 1, 4] = storecount;
                        storeamount = Convert.ToDouble(rows[i]["storeamount"].ToString());
                        gridStatis[k + 1, 5] = storeamount;
                        onroadcount = Convert.ToInt32(rows[i]["onroadcount"].ToString());
                        gridStatis[k + 1, 6] = onroadcount;
                        onroadamount = Convert.ToDouble(rows[i]["onroadamount"].ToString());
                        gridStatis[k + 1, 7] = onroadamount;
                        if (storecount == 0 && onroadcount == 0)
                            storecountpercent = 0.0;
                        else
                            storecountpercent = (double)storecount / (storecount + onroadcount) * 100;
                        gridStatis[k + 1, 8] = storecountpercent;
                        if (storeamount == 0 && onroadamount == 0)
                            storeamountpercent = 0.0;
                        else
                            storeamountpercent = storeamount / (storeamount + onroadamount) * 100;
                        gridStatis[k + 1, 9] = storeamountpercent;

                        substorecountlist.Add(storecount);
                        substoreamountlist.Add(storeamount);
                        subonroadcountlist.Add(onroadcount);
                        subonroadamountlist.Add(onroadamount);
                        k++;
                    }
                }

                if (carserieslist.Count > 0)
                {
                    gridStatis.Rows.Insert(k + 1);

                    int substorecount = 0, subonroadcount = 0;
                    double substoreamount = 0.0, subonroadamount = 0.0;
                    double substorecountpercent = 0.0, substoreamountpercent = 0.0;

                    for (int j = 0; j < substorecountlist.Count; j++)
                    {
                        substorecount += substorecountlist[j];
                        subonroadcount += subonroadcountlist[j];
                        substoreamount += substoreamountlist[j];
                        subonroadamount += subonroadamountlist[j];
                    }

                    if (substorecount == 0 && subonroadcount == 0)
                        substorecountpercent = 0.0;
                    else
                        substorecountpercent = (double)substorecount / (substorecount + subonroadcount) * 100;

                    if (substoreamount == 0 && subonroadamount == 0)
                        substoreamountpercent = 0.0;
                    else
                        substoreamountpercent = (double)substoreamount / (substorecount + subonroadamount) * 100;

                    totalstorecountlist.Add(substorecount);
                    totalonroadcountlist.Add(subonroadcount);
                    totalstoreamountlist.Add(substoreamount);
                    totalonroadamountlist.Add(subonroadamount);

                    gridStatis[k + 1, 0] = "小计";
                    gridStatis[k + 1, 4] = substorecount;
                    gridStatis[k + 1, 5] = substoreamount;
                    gridStatis[k + 1, 6] = subonroadcount;
                    gridStatis[k + 1, 7] = subonroadamount;
                    gridStatis[k + 1, 8] = substorecountpercent;
                    gridStatis[k + 1, 9] = substoreamountpercent;

                    CellRange cr = gridStatis.GetCellRange(k + 1, 0, k + 1, gridStatis.Cols.Count - 1);
                    cr.Style = gridStatis.Styles["summy"];

                    substorecountlist.Clear();
                    subonroadcountlist.Clear();
                    substoreamountlist.Clear();
                    subonroadamountlist.Clear();
                    k++;

                    gridStatis.Rows.Insert(k + 1);

                    int totalstorecount = 0, totalonroadcount = 0;
                    double totalstoreamount = 0.0, totalonroadamount = 0.0;
                    double totalstorecountpercent = 0.0, totalstoreamountpercent = 0.0;

                    for (int j = 0; j < totalstorecountlist.Count; j++)
                    {
                        totalstorecount += totalstorecountlist[j];
                        totalonroadcount += totalonroadcountlist[j];
                        totalstoreamount += totalstoreamountlist[j];
                        totalonroadamount += totalonroadamountlist[j];
                    }

                    if (totalstorecount == 0 && totalonroadcount == 0)
                        totalstorecountpercent = 0.0;
                    else
                        totalstorecountpercent = (double)totalstorecount / (totalstorecount + totalonroadcount) * 100;

                    if (substoreamount == 0 && subonroadamount == 0)
                        totalstoreamountpercent = 0.0;
                    else
                        totalstoreamountpercent = (double)totalstoreamount / (totalstorecount + totalonroadamount) * 100;

                    gridStatis[k + 1, 0] = "总计";
                    gridStatis[k + 1, 4] = totalstorecount;
                    gridStatis[k + 1, 5] = totalstoreamount;
                    gridStatis[k + 1, 6] = totalonroadcount;
                    gridStatis[k + 1, 7] = totalonroadamount;
                    gridStatis[k + 1, 8] = totalstorecountpercent;
                    gridStatis[k + 1, 9] = totalstoreamountpercent;

                    cr = gridStatis.GetCellRange(k + 1, 0, k + 1, gridStatis.Cols.Count - 1);
                    cr.Style = gridStatis.Styles["summy"];
                }

                gridStatis.AutoSizeCols();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void SaveSheet(C1FlexGrid flex, XLSheet sheet, bool titlevisible)
        {
            try
            {
                int frow0 = 0;
                int fcol0 = 0;
                // account for fixed cells
                if (!titlevisible) frow0 = 2;

                // copy dimensions
                int lastRow = flex.Rows.Count + frow0;
                int lastCol = flex.Cols.Count + fcol0;
                if (lastRow < 0 || lastCol < 0) return;
                XLCell cell = sheet[lastRow, lastCol];

                // set default properties
                sheet.Book.DefaultFont = flex.Font;
                sheet.DefaultRowHeight = C1XLBook.PixelsToTwips(flex.Rows.DefaultSize);
                sheet.DefaultColumnWidth = C1XLBook.PixelsToTwips(flex.Cols.DefaultSize);

                // prepare to convert styles
                styles = new Hashtable();

                // set title style

                // set fixed row style
                for (int r = 0; r < flex.Rows.Fixed; r++)
                {
                    XLRow xr = sheet.Rows[r + 2];

                    XLStyle xs = new XLStyle(xlbook);
                    xs.BackColor = Color.FromArgb(128, 255, 255);
                    xs.AlignHorz = XLAlignHorzEnum.Center;
                    xs.AlignVert = XLAlignVertEnum.Center;
                    xr.Style = xs;
                }

                // set row/column properties
                for (int r = flex.Rows.Fixed; r < flex.Rows.Count; r++)
                {
                    // size/visibility
                    Row fr = flex.Rows[r];
                    XLRow xr = sheet.Rows[r + frow0];
                    if (fr.Height >= 0)
                        xr.Height = C1XLBook.PixelsToTwips(fr.Height);
                    xr.Visible = fr.Visible;

                    // style
                    XLStyle xs = StyleFromFlex(fr.Style);
                    if (xs != null)
                        xr.Style = xs;
                }

                for (int c = flex.Cols.Fixed; c < flex.Cols.Count; c++)
                {
                    // size/visibility
                    Column fc = flex.Cols[c];
                    XLColumn xc = sheet.Columns[c + fcol0];
                    if (fc.Width >= 0)
                        xc.Width = C1XLBook.PixelsToTwips(fc.Width);
                    xc.Visible = fc.Visible;

                    // style
                    XLStyle xs = StyleFromFlex(fc.Style);
                    if (xs != null)
                        xc.Style = xs;
                }

                // load cells
                for (int r = 0; r < flex.Rows.Count; r++)
                {
                    for (int c = 0; c < flex.Cols.Count; c++)
                    {
                        // get cell
                        cell = sheet[r + frow0, c + fcol0];

                        // apply content
                        cell.Value = flex[r, c];

                        // apply style
                        /*XLStyle xs = StyleFromFlex(flex.GetCellStyle(r, c));
                        if (xs != null)
                            cell.Style = xs;*/
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        // convert FlexGrid style into Excel style
        private XLStyle StyleFromFlex(CellStyle style)
        {
            try
            {
                // sanity
                if (style == null)
                    return null;

                // look it up on list
                if (styles.Contains(style))
                    return styles[style] as XLStyle;

                // create new Excel style
                XLStyle xs = new XLStyle(xlbook);

                // set up new style
                xs.Font = style.Font;
                /*if (style.BackColor.ToArgb() != SystemColors.Window.ToArgb())
                {
                    xs.BackColor = style.BackColor;
                }*/
                xs.WordWrap = style.WordWrap;
                xs.Format = XLStyle.FormatDotNetToXL(style.Format);
                switch (style.TextDirection)
                {
                    case TextDirectionEnum.Up:
                        xs.Rotation = 90;
                        break;
                    case TextDirectionEnum.Down:
                        xs.Rotation = 180;
                        break;
                }
                switch (style.TextAlign)
                {
                    case TextAlignEnum.CenterBottom:
                        xs.AlignHorz = XLAlignHorzEnum.Center;
                        xs.AlignVert = XLAlignVertEnum.Bottom;
                        break;
                    case TextAlignEnum.CenterCenter:
                        xs.AlignHorz = XLAlignHorzEnum.Center;
                        xs.AlignVert = XLAlignVertEnum.Center;
                        break;
                    case TextAlignEnum.CenterTop:
                        xs.AlignHorz = XLAlignHorzEnum.Center;
                        xs.AlignVert = XLAlignVertEnum.Top;
                        break;
                    case TextAlignEnum.GeneralBottom:
                        xs.AlignHorz = XLAlignHorzEnum.General;
                        xs.AlignVert = XLAlignVertEnum.Bottom;
                        break;
                    case TextAlignEnum.GeneralCenter:
                        xs.AlignHorz = XLAlignHorzEnum.General;
                        xs.AlignVert = XLAlignVertEnum.Center;
                        break;
                    case TextAlignEnum.GeneralTop:
                        xs.AlignHorz = XLAlignHorzEnum.General;
                        xs.AlignVert = XLAlignVertEnum.Top;
                        break;
                    case TextAlignEnum.LeftBottom:
                        xs.AlignHorz = XLAlignHorzEnum.Left;
                        xs.AlignVert = XLAlignVertEnum.Bottom;
                        break;
                    case TextAlignEnum.LeftCenter:
                        xs.AlignHorz = XLAlignHorzEnum.Left;
                        xs.AlignVert = XLAlignVertEnum.Center;
                        break;
                    case TextAlignEnum.LeftTop:
                        xs.AlignHorz = XLAlignHorzEnum.Left;
                        xs.AlignVert = XLAlignVertEnum.Top;
                        break;
                    case TextAlignEnum.RightBottom:
                        xs.AlignHorz = XLAlignHorzEnum.Right;
                        xs.AlignVert = XLAlignVertEnum.Bottom;
                        break;
                    case TextAlignEnum.RightCenter:
                        xs.AlignHorz = XLAlignHorzEnum.Right;
                        xs.AlignVert = XLAlignVertEnum.Center;
                        break;
                    case TextAlignEnum.RightTop:
                        xs.AlignHorz = XLAlignHorzEnum.Right;
                        xs.AlignVert = XLAlignVertEnum.Top;
                        break;
                    default:
                        break;
                }

                // save it
                styles.Add(style, xs);

                // return it
                return xs;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
                return null;
            }
        }

        private void SetReadWriteProperty(bool writable)
        {
            try
            {
                if (writable)
                {
                    tsbExportExcel.Enabled = true;
                }
                else
                {
                    tsbExportExcel.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

        #region Event Methods

        private void FrmStatisRemainAmount_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmStatisRemainAmount_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmStatisRemainAmount = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmStatisRemainAmount_Activated(object sender, EventArgs e)
        {
            try
            {
                LoadTableFromDB();

                writable = Permission.GetFuncPermission(this.Text);
                SetReadWriteProperty(writable);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadTableFromDB();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Excel files (*.xls)|*.xls";
                dlg.Title = "导出Excel";
                dlg.FileName = "各车型占用数量及资金比例统计.xls";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                // clear book
                xlbook.Clear();
                xlbook.Sheets.Clear();

                XLSheet sheet = xlbook.Sheets.Add(this.Text);
                SaveSheet(gridStatis, sheet, false);

                xlbook.Save(dlg.FileName);
                Process.Start(dlg.FileName);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbReturn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion
                
    }
}