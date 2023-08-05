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
    public partial class FrmStatisSaleCartype : Form
    {
        #region Fields and Properties

        public static FrmStatisSaleCartype frmStatisSaleCartype;

        private C1XLBook xlbook;
        private Hashtable styles;

        private bool writable;

        #endregion

        #region Constructors

        public FrmStatisSaleCartype()
        {
            InitializeComponent();

            xlbook = new C1XLBook();
            writable = true;

            this.Activated += new EventHandler(FrmStatisSaleCartype_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmStatisSaleCartype_FormClosing);

            // flexgrid events

        }
                
        #endregion

        #region Public Methods

        public static FrmStatisSaleCartype GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmStatisSaleCartype == null) //if not created yet, Create an instance
                {
                    frmStatisSaleCartype = new FrmStatisSaleCartype();
                    frmStatisSaleCartype.MdiParent = parent;
                }
            }
            return frmStatisSaleCartype;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void LoadTableFromDB()
        {
            try
            {
                storStatisSalecartypeTableAdapter.Fill(cmsDB.stor_statis_salecartype, dtpStart.Value, dtpEnd.Value);

                gridStatis.Rows.Count = 2;
                gridStatis.Rows.Fixed = 1;

                CellStyle cs = gridStatis.Styles.Add("summy");
                cs.BackColor = Color.Gainsboro;
                cs.ForeColor = Color.BlueViolet;
                cs.DataType = typeof(int);

                int[] totals = new int[13];

                DataRow[] rows = cmsDB.stor_statis_salecartype.Select("");
                for (int i = 0; i < cmsDB.stor_statis_salecartype.Count; i++)
                {
                    gridStatis.Rows.Insert(i + 1);

                    gridStatis[i + 1, 0] = rows[i]["carseries"].ToString();
                    totals[0] += Convert.ToInt32(rows[i]["cnt1"].ToString());
                    gridStatis[i + 1, 1] = Convert.ToInt32(rows[i]["cnt1"].ToString());
                    totals[1] += Convert.ToInt32(rows[i]["cnt2"].ToString());
                    gridStatis[i + 1, 2] = Convert.ToInt32(rows[i]["cnt2"].ToString());
                    totals[2] += Convert.ToInt32(rows[i]["cnt3"].ToString());
                    gridStatis[i + 1, 3] = Convert.ToInt32(rows[i]["cnt3"].ToString());
                    totals[3] += Convert.ToInt32(rows[i]["cnt4"].ToString());
                    gridStatis[i + 1, 4] = Convert.ToInt32(rows[i]["cnt4"].ToString());
                    totals[4] += Convert.ToInt32(rows[i]["cnt5"].ToString());
                    gridStatis[i + 1, 5] = Convert.ToInt32(rows[i]["cnt5"].ToString());
                    totals[5] += Convert.ToInt32(rows[i]["cnt6"].ToString());
                    gridStatis[i + 1, 6] = Convert.ToInt32(rows[i]["cnt6"].ToString());
                    totals[6] += Convert.ToInt32(rows[i]["cnt7"].ToString());
                    gridStatis[i + 1, 7] = Convert.ToInt32(rows[i]["cnt7"].ToString());
                    totals[7] += Convert.ToInt32(rows[i]["cnt8"].ToString());
                    gridStatis[i + 1, 8] = Convert.ToInt32(rows[i]["cnt8"].ToString());
                    totals[8] += Convert.ToInt32(rows[i]["cnt9"].ToString());
                    gridStatis[i + 1, 9] = Convert.ToInt32(rows[i]["cnt9"].ToString());
                    totals[9] += Convert.ToInt32(rows[i]["cnt10"].ToString());
                    gridStatis[i + 1, 10] = Convert.ToInt32(rows[i]["cnt10"].ToString());
                    totals[10] += Convert.ToInt32(rows[i]["cnt11"].ToString());
                    gridStatis[i + 1, 11] = Convert.ToInt32(rows[i]["cnt11"].ToString());
                    totals[11] += Convert.ToInt32(rows[i]["cnt12"].ToString());
                    gridStatis[i + 1, 12] = Convert.ToInt32(rows[i]["cnt12"].ToString());
                    totals[12] += Convert.ToInt32(rows[i]["cnttotal"].ToString());
                    gridStatis[i + 1, 13] = Convert.ToInt32(rows[i]["cnttotal"].ToString());
                    
                }

                gridStatis[gridStatis.Rows.Count - 1, 0] = "总计";
                for (int i = 1; i < gridStatis.Cols.Count; i++)
                {
                    gridStatis[gridStatis.Rows.Count - 1, i] = totals[i - 1];
                }
                
                CellRange cr = gridStatis.GetCellRange(gridStatis.Rows.Count - 1, 0, gridStatis.Rows.Count - 1, gridStatis.Cols.Count - 1);
                cr.Style = gridStatis.Styles["summy"];

                //gridStatis.AutoSizeCols();
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

        private void FrmStatisSaleCartype_Load(object sender, EventArgs e)
        {            
            try
            {
                dtpStart.Value = DateTime.Now.AddYears(-1).AddDays(1);
                dtpEnd.Value = DateTime.Now;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmStatisSaleCartype_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmStatisSaleCartype = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmStatisSaleCartype_Activated(object sender, EventArgs e)
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
                dlg.FileName = "各车型销售情况统计表.xls";
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