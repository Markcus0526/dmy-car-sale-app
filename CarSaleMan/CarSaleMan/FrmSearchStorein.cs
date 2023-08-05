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
    public partial class FrmSearchStorein : Form
    {
        #region Fields and Properties

        public static FrmSearchStorein frmSearchStorein;
        public FrmSearch frmSearch;

        private C1XLBook xlbook;
        private Hashtable styles;

        private bool writable;

        #endregion

        #region Constructors

        public FrmSearchStorein()
        {
            InitializeComponent();

            // init private variables
            xlbook = new C1XLBook();
            writable = true;

            this.Activated += new EventHandler(FrmSearchStorein_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmSearchStorein_FormClosing);

            // flexgrid events
            this.gridStorein.SelChange += new EventHandler(gridStorein_SelChange);
            this.gridStorein.KeyPressEdit += new KeyPressEditEventHandler(gridStorein_KeyPressEdit);
            this.gridStorein.KeyDownEdit += new KeyEditEventHandler(gridStorein_KeyDownEdit);
        }
                
        #endregion

        #region Public Methods

        public static FrmSearchStorein GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmSearchStorein == null) //if not created yet, Create an instance
                {
                    frmSearchStorein = new FrmSearchStorein();
                    frmSearchStorein.MdiParent = parent;
                }
            }
            return frmSearchStorein;  //just created or created earlier.Return it
        }

        public void SearchByCondition(String cond)
        {
            try
            {
                storStatisStoreinBindingSource.Filter = cond;

                if (cmsDB.stor_statis_storein.Count > 0)
                {
                    for (int i = 0; i < storStatisStoreinBindingSource.Count; i++)
                    {
                        gridStorein.Cols[0][i + 1] = i + 1;
                    }
                }
                gridStorein.AutoSizeCols();

                frmSearch.lblCount.Text = storStatisStoreinBindingSource.Count.ToString();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion

        #region Private Methods

        private void LoadTableFromDB()
        {
            try
            {
                storStatisStoreinBindingSource.Filter = "";

                storStatisStoreinTableAdapter.Fill(cmsDB.stor_statis_storein);

                tblEnvTableAdapter.Fill(cmsDB.tbl_env);

                DataRow[] rows = cmsDB.tbl_env.Select("name = 'nointerestdates'");
                int _nointerestdates = Convert.ToInt32(rows[0]["value"].ToString());

                rows = cmsDB.tbl_env.Select("name = 'extenddates'");
                int _extenddates = Convert.ToInt32(rows[0]["value"].ToString());

                rows = cmsDB.tbl_env.Select("name = 'interestrate'");
                double _interestrate = Convert.ToDouble(rows[0]["value"].ToString());

                rows = cmsDB.tbl_env.Select("name = 'extendrate'");
                double _extendrate = Convert.ToDouble(rows[0]["value"].ToString());

                int nDatesPerYear = 360;

                if (cmsDB.stor_statis_storein.Count > 0)
                {
                    for (int i = 0; i < cmsDB.stor_statis_storein.Count; i++)
                    {
                        DataRow row = cmsDB.stor_statis_storein.Rows[i];

                        DateTime billdate = Convert.ToDateTime(row["billdate"].ToString());

                        DateTime nointerestdate = billdate.AddDays(_nointerestdates);
                        row["nointerestdate"] = nointerestdate;

                        DateTime noextenddate = nointerestdate.AddDays(_extenddates);
                        row["noextenddate"] = noextenddate;

                        DateTime noallmoneydate = noextenddate.AddDays(_extenddates);
                        row["noallmoneydate"] = noallmoneydate;

                        decimal inprice = Convert.ToDecimal(row["inprice"].ToString());
                        decimal goodprice = (decimal)((double)inprice * 0.9);
                        decimal goodremainprice = (decimal)((double)inprice * 0.68);

                        int distnointerestdates = CommonMisc.BetweenDates(nointerestdate, DateTime.Today);
                        int interestdates = CommonMisc.BetweenDates(DateTime.Today, nointerestdate);
                        int distextenddates = CommonMisc.BetweenDates(DateTime.Today, noextenddate);
                        int distallmoney = CommonMisc.BetweenDates(DateTime.Today, noallmoneydate);
                        double totalinterest = 0.0f;
                        double interestrate = 0.0f;

                        if (interestdates > 0)
                        {
                            if (interestdates <= _nointerestdates)
                            {
                                totalinterest = (((double)goodprice * (_interestrate / 100.0f)) / nDatesPerYear) * interestdates;
                                interestrate = _interestrate;
                            }
                            else
                            {
                                if (distextenddates > 0)
                                {
                                    totalinterest = (((double)goodprice * (_interestrate / 100.0f)) / nDatesPerYear) * _nointerestdates
                                            + (((double)goodremainprice * (_extendrate / 100.0f)) / nDatesPerYear) * distextenddates;
                                    interestrate = _extendrate;
                                }
                            }
                        }
                        else
                        {
                            totalinterest = 0.0f;
                        }
                        if (totalinterest < 0)
                            totalinterest = 0;

                        int stockindates = CommonMisc.BetweenDates(DateTime.Today, billdate);

                        row["interestdates"] = -distnointerestdates;
                        row["distnointerestdates"] = -interestdates;
                        row["distextenddates"] = -distextenddates;
                        row["distallmoney"] = -distallmoney;
                        row["interestrate"] = interestrate;
                        row["totalinterest"] = totalinterest;

                        gridStorein.Cols[0][i + 1] = i + 1;
                    }
                }

                gridStorein.AutoSizeCols();
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

        private void FrmSearchStorein_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmSearchStorein_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmSearchStorein = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmSearchStorein_Activated(object sender, EventArgs e)
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

        // toolbar event
        void tsbFindAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                LoadTableFromDB();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void tsbFindCondition_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch == null)
                    frmSearch = new FrmSearch(this);

                frmSearch.searchKind = Global.SEARCH_SEARCH_STOREIN;
                frmSearch.Show(this);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }
        
        void tsbExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridStorein.Rows.Count < 2)
                {
                    FrmMessage msg = new FrmMessage();
                    msg.ShowCloseMsg("要导出Excel数据没有！", "", "通知");
                    return;
                }

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Excel files (*.xls)|*.xls";
                dlg.Title = "导出Excel";
                dlg.FileName = "库存查询模块.xls";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                // clear book
                xlbook.Clear();
                xlbook.Sheets.Clear();

                XLSheet sheet = xlbook.Sheets.Add(this.Text);
                SaveSheet(gridStorein, sheet, false);

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

        // flexgrid events
        void gridStorein_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null && frmSearch.Visible == true && frmSearch.isEnable == true)
                {
                    int r = gridStorein.RowSel;
                    int c = gridStorein.ColSel;

                    if (gridStorein[0, c].ToString().Equals("开单日期"))
                    {
                        frmSearch.chkDate.Text = gridStorein[0, c].ToString();
                        frmSearch.chkDate.Checked = true;
                        frmSearch.keyField4 = gridStorein.Cols[c].Name;

                        DateTime date = new DateTime();
                        if (DateTime.TryParse(gridStorein[r, c].ToString(), out date))
                            frmSearch.dtpStart.Value = date;
                        else
                            frmSearch.dtpStart.Value = DateTime.MinValue;
                    }
                    else
                    {
                        if (frmSearch.selkey == 0)
                        {
                            frmSearch.chkKey1.Text = gridStorein[0, c].ToString();
                            frmSearch.chkKey1.Checked = true;
                            frmSearch.keyField1 = gridStorein.Cols[c].Name;

                            frmSearch.cbKey1.Text = gridStorein[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 1)
                        {
                            frmSearch.chkKey2.Text = gridStorein[0, c].ToString();
                            frmSearch.chkKey2.Checked = true;
                            frmSearch.keyField2 = gridStorein.Cols[c].Name;

                            frmSearch.cbKey2.Text = gridStorein[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 2)
                        {
                            frmSearch.chkKey3.Text = gridStorein[0, c].ToString();
                            frmSearch.chkKey3.Checked = true;
                            frmSearch.keyField3 = gridStorein.Cols[c].Name;

                            frmSearch.cbKey3.Text = gridStorein[r, c].ToString();
                        }

                        frmSearch.selkey++;
                        if (frmSearch.selkey >= 3) frmSearch.selkey = 0;
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void gridStorein_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            try
            {
                if (frmSearch != null && e.KeyChar == 3)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void gridStorein_KeyDownEdit(object sender, KeyEditEventArgs e)
        {
            try
            {
                if (frmSearch != null && (e.KeyCode == Keys.C && e.Control == true))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion
        
    }
}