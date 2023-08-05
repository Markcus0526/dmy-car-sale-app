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
    public partial class FrmStatisCarSaleQuarter : Form
    {
        #region Fields and Properties

        public static FrmStatisCarSaleQuarter frmStatisCarSaleQuarter;

        private int gencount, speccount;
        private bool cellchanged = false;
        private String oldYear;
        private int oldQuarter;
        private C1XLBook xlbook;
        private Hashtable styles;

        private bool writable;

        #endregion

        #region Constructors

        public FrmStatisCarSaleQuarter()
        {
            InitializeComponent();

            // init private variables
            for (int i = 2002; i <= DateTime.Now.Year; i++)
            {
                tsbYear.Items.Add(i);
            }

            oldYear = tsbYear.Items[tsbYear.Items.Count - 1].ToString();
            if (DateTime.Now.Month < 4)
                oldQuarter = 1;
            else if (DateTime.Now.Month < 7)
                oldQuarter = 2;
            else if (DateTime.Now.Month < 10)
                oldQuarter = 3;
            else
                oldQuarter = 4;

            xlbook = new C1XLBook();
            writable = true;

            this.Activated += new EventHandler(FrmStatisCarSaleQuarter_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmStatisCarSaleQuarter_FormClosing);

            // flexgrid events
            gridQuarter.AfterEdit += new RowColEventHandler(gridQuarter_AfterEdit);

            tsbYear.SelectedIndexChanged += new EventHandler(tsbYear_SelectedIndexChanged);
            tsbQuarter.SelectedIndexChanged += new EventHandler(tsbQuarter_SelectedIndexChanged);
        }

        
                
        #endregion

        #region Public Methods

        public static FrmStatisCarSaleQuarter GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmStatisCarSaleQuarter == null) //if not created yet, Create an instance
                {
                    frmStatisCarSaleQuarter = new FrmStatisCarSaleQuarter();
                    frmStatisCarSaleQuarter.MdiParent = parent;
                }
            }
            return frmStatisCarSaleQuarter;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void LoadTableFromDB(bool initial)
        {
            try
            {
                int colbase;

                if (initial == true)
                {
                    tblBasedataTableAdapter.Fill(cmsDB.tbl_basedata);

                    CellStyle cs = gridQuarter.Styles.Add("summy");
                    cs.BackColor = Color.Gainsboro;
                    cs.ForeColor = Color.BlueViolet;

                    cs = gridQuarter.Styles.Add("count");
                    cs.DataType = typeof(int);

                    cs = gridQuarter.Styles.Add("percent");
                    cs.DataType = typeof(String);
                    cs.BackColor = Color.Gainsboro;
                    cs.ForeColor = Color.BlueViolet;

                    gencount = speccount = 0;
                    colbase = gridQuarter.Rows.Fixed;

                    CellRange cr = gridQuarter.GetCellRange(1, 1, 7, 3);
                    cr.Style = gridQuarter.Styles["summy"];

                    // load datas from base datas
                    DataRow[] rows = cmsDB.tbl_basedata.Select("keyname = '民用'");
                    gencount = rows.Length;
                    if (rows != null && rows.Length > 0)
                    {
                        for (int i = 0; i < rows.Length; i++)
                        {
                            gridQuarter.Cols.Insert(colbase + i);
                            cr = gridQuarter.GetCellRange(1, colbase + i, 7, colbase + i);
                            cr.Style = gridQuarter.Styles["count"];
                            gridQuarter.Cols[colbase + i][0] = rows[i]["value"].ToString();
                        }
                    }
                    cr = gridQuarter.GetCellRange(1, colbase + gencount, 7, colbase + gencount);
                    cr.Style = gridQuarter.Styles["summy"];

                    rows = cmsDB.tbl_basedata.Select("keyname = '特种'");
                    speccount = rows.Length;
                    colbase = gridQuarter.Rows.Fixed + gencount + 1;
                    if (rows != null && rows.Length > 0)
                    {
                        for (int i = 0; i < rows.Length; i++)
                        {
                            gridQuarter.Cols.Insert(colbase + i);
                            cr = gridQuarter.GetCellRange(1, colbase + i, 7, colbase + i);
                            cr.Style = gridQuarter.Styles["count"];
                            gridQuarter.Cols[colbase + i][0] = rows[i]["value"].ToString();
                        }
                    }
                    cr = gridQuarter.GetCellRange(1, colbase + speccount, 7, colbase + speccount);
                    cr.Style = gridQuarter.Styles["summy"];
                    cr = gridQuarter.GetCellRange(1, colbase + speccount + 1, 7, colbase + speccount + 1);
                    cr.Style = gridQuarter.Styles["summy"];

                    gridQuarter.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
                }

                tblQuarterstatsTableAdapter.Fill(cmsDB.tbl_quarterstats);                

                gridQuarter.Rows[1][0] = "任务：(此处可以添加任务总数）";
                gridQuarter.Rows[2][0] = tsbQuarter.SelectedIndex * 3 + 1 + "月";
                gridQuarter.Rows[3][0] = tsbQuarter.SelectedIndex * 3 + 2 + "月";
                gridQuarter.Rows[4][0] = tsbQuarter.SelectedIndex * 3 + 3 + "月";
                gridQuarter.Rows[5][0] = "实际完成";
                gridQuarter.Rows[6][0] = "系统库存";
                gridQuarter.Rows[7][0] = "预配未执行";
                gridQuarter.Rows[8][0] = "完成率";

                gridQuarter.Rows[5].AllowEditing = false;
                gridQuarter.Rows[5].Style = gridQuarter.Styles["summy"];
                gridQuarter.Rows[8].AllowEditing = false;
                gridQuarter.Rows[8].Style = gridQuarter.Styles["percent"];

                // load quarter datas from tbl_quarterstats
                colbase = gridQuarter.Rows.Fixed;
                for (int i = 0; i < gencount; i++)
                {
                    String filter = "carseries = '" + gridQuarter.Cols[colbase + i][0].ToString() + "' AND year = '" + tsbYear.SelectedItem.ToString() + "'";
                    DataRow[] mrows = cmsDB.tbl_quarterstats.Select(filter);
                    if (mrows != null && mrows.Length > 0)
                    {
                        gridQuarter.Cols[colbase + i][1] = mrows[0]["total" + (tsbQuarter.SelectedIndex + 1)].ToString().Equals("") ? "0" : mrows[0]["total" + (tsbQuarter.SelectedIndex + 1)].ToString();

                        String colname = "m" + gridQuarter.Rows[2][0].ToString().Substring(0, gridQuarter.Rows[2][0].ToString().Length - 1);
                        gridQuarter.Cols[colbase + i][2] = mrows[0][colname].ToString().Equals("") ? "0" : mrows[0][colname].ToString();
                        colname = "m" + gridQuarter.Rows[3][0].ToString().Substring(0, gridQuarter.Rows[3][0].ToString().Length - 1);
                        gridQuarter.Cols[colbase + i][3] = mrows[0][colname].ToString().Equals("") ? "0" : mrows[0][colname].ToString();
                        colname = "m" + gridQuarter.Rows[4][0].ToString().Substring(0, gridQuarter.Rows[4][0].ToString().Length - 1);
                        gridQuarter.Cols[colbase + i][4] = mrows[0][colname].ToString().Equals("") ? "0" : mrows[0][colname].ToString();

                        gridQuarter.Cols[colbase + i][6] = mrows[0]["remain" + (tsbQuarter.SelectedIndex + 1)].ToString().Equals("") ? "0" : mrows[0]["remain" + (tsbQuarter.SelectedIndex + 1)].ToString();
                        gridQuarter.Cols[colbase + i][7] = "0";
                    }
                    else
                    {
                        gridQuarter.Cols[colbase + i][1] = "0";
                        gridQuarter.Cols[colbase + i][2] = "0";
                        gridQuarter.Cols[colbase + i][3] = "0";
                        gridQuarter.Cols[colbase + i][4] = "0";
                        gridQuarter.Cols[colbase + i][6] = "0";
                        gridQuarter.Cols[colbase + i][7] = "0";
                    }
                }

                colbase = gridQuarter.Cols.Fixed + gencount + 1;
                for (int i = 0; i < speccount; i++)
                {
                    String filter = "carseries = '" + gridQuarter.Cols[colbase + i][0].ToString() + "' AND year = '" + tsbYear.SelectedItem.ToString() + "' AND type = 1";
                    DataRow[] mrows = cmsDB.tbl_quarterstats.Select(filter);
                    if (mrows != null && mrows.Length > 0)
                    {
                        gridQuarter.Cols[colbase + i][1] = mrows[0]["total" + (tsbQuarter.SelectedIndex + 1)].ToString().Equals("") ? "0" : mrows[0]["total" + (tsbQuarter.SelectedIndex + 1)].ToString();

                        String colname = "m" + gridQuarter.Rows[2][0].ToString().Substring(0, gridQuarter.Rows[2][0].ToString().Length - 1);
                        gridQuarter.Cols[colbase + i][2] = mrows[0][colname].ToString().Equals("") ? "0" : mrows[0][colname].ToString();
                        colname = "m" + gridQuarter.Rows[3][0].ToString().Substring(0, gridQuarter.Rows[3][0].ToString().Length - 1);
                        gridQuarter.Cols[colbase + i][3] = mrows[0][colname].ToString().Equals("") ? "0" : mrows[0][colname].ToString();
                        colname = "m" + gridQuarter.Rows[4][0].ToString().Substring(0, gridQuarter.Rows[4][0].ToString().Length - 1);
                        gridQuarter.Cols[colbase + i][4] = mrows[0][colname].ToString().Equals("") ? "0" : mrows[0][colname].ToString();

                        gridQuarter.Cols[colbase + i][6] = mrows[0]["remain" + (tsbQuarter.SelectedIndex + 1)].ToString().Equals("") ? "0" : mrows[0]["remain" + (tsbQuarter.SelectedIndex + 1)].ToString();
                        gridQuarter.Cols[colbase + i][7] = "0";
                    }
                    else
                    {
                        gridQuarter.Cols[colbase + i][1] = "0";
                        gridQuarter.Cols[colbase + i][2] = "0";
                        gridQuarter.Cols[colbase + i][3] = "0";
                        gridQuarter.Cols[colbase + i][4] = "0";
                        gridQuarter.Cols[colbase + i][6] = "0";
                        gridQuarter.Cols[colbase + i][7] = "0";
                    }
                }

                CalculateAmounts();

                gridQuarter.AutoSizeCols();
                
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void CalculateAmounts()
        {
            try
            {                
                int a1 = 0, a2 = 0, a3 = 0, a4 = 0, a5 = 0, a6 = 0, a7 = 0;
                int colbase = gridQuarter.Rows.Fixed;

                // calculate general car type
                for (int i = 0; i < gencount; i++)
                {
                    int sum = Convert.ToInt32(gridQuarter[2, colbase + i].ToString()) +
                        Convert.ToInt32(gridQuarter[3, colbase + i].ToString()) +
                        Convert.ToInt32(gridQuarter[4, colbase + i].ToString());
                    gridQuarter[5, colbase + i] = sum;

                    a1 += Convert.ToInt32(gridQuarter[1, colbase + i].ToString());
                    a2 += Convert.ToInt32(gridQuarter[2, colbase + i].ToString());
                    a3 += Convert.ToInt32(gridQuarter[3, colbase + i].ToString());
                    a4 += Convert.ToInt32(gridQuarter[4, colbase + i].ToString());
                    a5 += Convert.ToInt32(gridQuarter[5, colbase + i].ToString());
                    a6 += Convert.ToInt32(gridQuarter[6, colbase + i].ToString());
                    a7 += Convert.ToInt32(gridQuarter[7, colbase + i].ToString());

                    if (Convert.ToInt32(gridQuarter[1, colbase + i].ToString()) == 0)
                    {
                        gridQuarter[8, colbase + i] = "0.00%";
                    }
                    else
                    {
                        decimal per = (decimal)sum / (decimal)Convert.ToInt32(gridQuarter[1, colbase + i].ToString()) * 100;
                        gridQuarter[8, colbase + i] = String.Format("{0:0.00}%", per);
                    }
                }

                gridQuarter[1, colbase + gencount] = a1;
                gridQuarter[2, colbase + gencount] = a2;
                gridQuarter[3, colbase + gencount] = a3;
                gridQuarter[4, colbase + gencount] = a4;
                gridQuarter[5, colbase + gencount] = a5;
                gridQuarter[6, colbase + gencount] = a6;
                gridQuarter[7, colbase + gencount] = a7;

                if (a1 == 0)
                {
                    gridQuarter[8, colbase + gencount] = "0.00%";
                }
                else
                {
                    decimal per = (decimal)(a2 + a3 + a4) / (decimal)a1 * 100;
                    gridQuarter[8, colbase + gencount] = String.Format("{0:0.00}%", per);
                }

                // calculate special car type
                a1 = a2 = a3 = a4 = a5 = a6 = a7 = 0;
                colbase = gridQuarter.Rows.Fixed + gencount + 1;
                for (int i = 0; i < speccount; i++)
                {
                    int sum = Convert.ToInt32(gridQuarter[2, colbase + i].ToString()) +
                        Convert.ToInt32(gridQuarter[3, colbase + i].ToString()) +
                        Convert.ToInt32(gridQuarter[4, colbase + i].ToString());
                    gridQuarter[5, colbase + i] = sum;

                    a1 += Convert.ToInt32(gridQuarter[1, colbase + i].ToString());
                    a2 += Convert.ToInt32(gridQuarter[2, colbase + i].ToString());
                    a3 += Convert.ToInt32(gridQuarter[3, colbase + i].ToString());
                    a4 += Convert.ToInt32(gridQuarter[4, colbase + i].ToString());
                    a5 += Convert.ToInt32(gridQuarter[5, colbase + i].ToString());
                    a6 += Convert.ToInt32(gridQuarter[6, colbase + i].ToString());
                    a7 += Convert.ToInt32(gridQuarter[7, colbase + i].ToString());

                    if (Convert.ToInt32(gridQuarter[1, colbase + i].ToString()) == 0)
                    {
                        gridQuarter[8, colbase + i] = "0.00%";
                    }
                    else
                    {
                        decimal per = (decimal)sum / (decimal)Convert.ToInt32(gridQuarter[1, colbase + i].ToString()) * 100;
                        gridQuarter[8, colbase + i] = String.Format("{0:0.00}%", per);
                    }
                }

                gridQuarter[1, colbase + speccount] = a1;
                gridQuarter[2, colbase + speccount] = a2;
                gridQuarter[3, colbase + speccount] = a3;
                gridQuarter[4, colbase + speccount] = a4;
                gridQuarter[5, colbase + speccount] = a5;
                gridQuarter[6, colbase + speccount] = a6;
                gridQuarter[7, colbase + speccount] = a7;

                if (a1 == 0)
                {
                    gridQuarter[8, colbase + speccount] = "0.00%";
                }
                else
                {
                    decimal per = (decimal)(a2 + a3 + a4) / (decimal)a1 * 100;
                    gridQuarter[8, colbase + speccount] = String.Format("{0:0.00}%", per);
                }

                // calculate total amount
                colbase = gridQuarter.Cols.Fixed + gencount;
                gridQuarter[1, colbase + speccount + 2] = Convert.ToInt32(gridQuarter[1, colbase].ToString()) +
                        Convert.ToInt32(gridQuarter[1, colbase + speccount + 1].ToString());
                gridQuarter[2, colbase + speccount + 2] = Convert.ToInt32(gridQuarter[2, colbase].ToString()) +
                        Convert.ToInt32(gridQuarter[2, colbase + speccount + 1].ToString());
                gridQuarter[3, colbase + speccount + 2] = Convert.ToInt32(gridQuarter[3, colbase].ToString()) +
                        Convert.ToInt32(gridQuarter[3, colbase + speccount + 1].ToString());
                gridQuarter[4, colbase + speccount + 2] = Convert.ToInt32(gridQuarter[4, colbase].ToString()) +
                        Convert.ToInt32(gridQuarter[4, colbase + speccount + 1].ToString());
                gridQuarter[5, colbase + speccount + 2] = Convert.ToInt32(gridQuarter[5, colbase].ToString()) +
                        Convert.ToInt32(gridQuarter[5, colbase + speccount + 1].ToString());
                gridQuarter[6, colbase + speccount + 2] = Convert.ToInt32(gridQuarter[6, colbase].ToString()) +
                        Convert.ToInt32(gridQuarter[6, colbase + speccount + 1].ToString());
                gridQuarter[7, colbase + speccount + 2] = Convert.ToInt32(gridQuarter[7, colbase].ToString()) +
                        Convert.ToInt32(gridQuarter[7, colbase + speccount + 1].ToString());

                if (Convert.ToInt32(gridQuarter[1, colbase + speccount + 2].ToString()) == 0)
                {
                    gridQuarter[8, colbase + speccount + 2] = "0.00%";
                }
                else
                {
                    decimal per = (decimal)(Convert.ToInt32(gridQuarter[2, colbase + speccount + 2].ToString()) + Convert.ToInt32(gridQuarter[3, colbase + speccount + 2].ToString()) + Convert.ToInt32(gridQuarter[4, colbase + speccount + 2].ToString())) / 
                        (decimal)Convert.ToInt32(gridQuarter[1, colbase + speccount + 2].ToString()) * 100;
                    gridQuarter[8, colbase + speccount + 2] = String.Format("{0:0.00}%", per);
                }

            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void ExportFromExcel(C1FlexGrid grid)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void SaveChange(String oldYear, int oldQuarter)
        {
            try
            {
                int colbase = gridQuarter.Cols.Fixed;

                for (int i = 0; i < gencount; i++)
                {
                    String filter = "year = '" + oldYear + "' AND carseries = '" + gridQuarter[0, colbase + i] + "' AND type = 0";
                    DataRow[] mrows = cmsDB.tbl_quarterstats.Select(filter);

                    if (mrows != null && mrows.Length > 0)
                    {
                        mrows[0]["total" + oldQuarter] = gridQuarter[1, colbase + i];
                        String colname = "m" + gridQuarter.Rows[2][0].ToString().Substring(0, gridQuarter.Rows[2][0].ToString().Length - 1);
                        mrows[0][colname] = gridQuarter[2, colbase + i];
                        colname = "m" + gridQuarter.Rows[3][0].ToString().Substring(0, gridQuarter.Rows[3][0].ToString().Length - 1);
                        mrows[0][colname] = gridQuarter[3, colbase + i];
                        colname = "m" + gridQuarter.Rows[4][0].ToString().Substring(0, gridQuarter.Rows[4][0].ToString().Length - 1);
                        mrows[0][colname] = gridQuarter[4, colbase + i];
                        mrows[0]["remain" + oldQuarter] = gridQuarter[6, colbase + i];

                        tblQuarterstatsTableAdapter.Update(mrows);
                        cmsDB.tbl_quarterstats.AcceptChanges();
                    }
                    else
                    {
                        CmsDB.tbl_quarterstatsRow row = (CmsDB.tbl_quarterstatsRow)cmsDB.tbl_quarterstats.NewRow();
                        row["carseries"] = gridQuarter.Rows[0][colbase + i];
                        row["type"] = 0;
                        row["year"] = oldYear;
                        row["total" + oldQuarter] = gridQuarter[1, colbase + i];
                        String colname = "m" + gridQuarter.Rows[2][0].ToString().Substring(0, gridQuarter.Rows[2][0].ToString().Length - 1);
                        row[colname] = gridQuarter[2, colbase + i];
                        colname = "m" + gridQuarter.Rows[3][0].ToString().Substring(0, gridQuarter.Rows[3][0].ToString().Length - 1);
                        row[colname] = gridQuarter[3, colbase + i];
                        colname = "m" + gridQuarter.Rows[4][0].ToString().Substring(0, gridQuarter.Rows[4][0].ToString().Length - 1); ;
                        row[colname] = gridQuarter[4, colbase + i];
                        row["remain" + oldQuarter] = gridQuarter[6, colbase + i];

                        cmsDB.tbl_quarterstats.Addtbl_quarterstatsRow(row);
                        tblQuarterstatsTableAdapter.Update(cmsDB.tbl_quarterstats);
                        cmsDB.tbl_quarterstats.AcceptChanges();
                    }
                }

                colbase = gridQuarter.Cols.Fixed + gencount + 1;
                for (int i = 0; i < speccount; i++)
                {
                    String filter = "year = '" + oldYear + "' AND carseries = '" + gridQuarter[0, colbase + i] + "' AND type = 1";
                    DataRow[] mrows = cmsDB.tbl_quarterstats.Select(filter);

                    if (mrows != null && mrows.Length > 0)
                    {
                        mrows[0]["total" + oldQuarter] = gridQuarter[1, colbase + i];
                        String colname = "m" + gridQuarter.Rows[2][0].ToString().Substring(0, gridQuarter.Rows[2][0].ToString().Length - 1);
                        mrows[0][colname] = gridQuarter[2, colbase + i];
                        colname = "m" + gridQuarter.Rows[3][0].ToString().Substring(0, gridQuarter.Rows[3][0].ToString().Length - 1);
                        mrows[0][colname] = gridQuarter[3, colbase + i];
                        colname = "m" + gridQuarter.Rows[4][0].ToString().Substring(0, gridQuarter.Rows[4][0].ToString().Length - 1);
                        mrows[0][colname] = gridQuarter[4, colbase + i];
                        mrows[0]["remain" + oldQuarter] = gridQuarter[6, colbase + i];

                        tblQuarterstatsTableAdapter.Update(mrows);
                        cmsDB.tbl_quarterstats.AcceptChanges();
                    }
                    else
                    {
                        CmsDB.tbl_quarterstatsRow row = (CmsDB.tbl_quarterstatsRow)cmsDB.tbl_quarterstats.NewRow();
                        row["carseries"] = gridQuarter.Rows[0][colbase + i];
                        row["type"] = 1;
                        row["year"] = oldYear;
                        row["total" + oldQuarter] = gridQuarter[1, colbase + i];
                        String colname = "m" + gridQuarter.Rows[2][0].ToString().Substring(0, gridQuarter.Rows[2][0].ToString().Length - 1);
                        row[colname] = gridQuarter[2, colbase + i];
                        colname = "m" + gridQuarter.Rows[3][0].ToString().Substring(0, gridQuarter.Rows[3][0].ToString().Length - 1);
                        row[colname] = gridQuarter[3, colbase + i];
                        colname = "m" + gridQuarter.Rows[4][0].ToString().Substring(0, gridQuarter.Rows[4][0].ToString().Length - 1); ;
                        row[colname] = gridQuarter[4, colbase + i];
                        row["remain" + oldQuarter] = gridQuarter[6, colbase + i];

                        cmsDB.tbl_quarterstats.Addtbl_quarterstatsRow(row);
                        tblQuarterstatsTableAdapter.Update(cmsDB.tbl_quarterstats);
                        cmsDB.tbl_quarterstats.AcceptChanges();
                    }
                }

                if (tsbQuarter.SelectedIndex == 0)
                {
                    
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
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
                    tsbSave.Enabled = true;
                    tsbExportExcel.Enabled = true;
                    gridQuarter.Enabled = true;
                }
                else
                {
                    tsbSave.Enabled = false;
                    tsbExportExcel.Enabled = false;
                    gridQuarter.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

        #region Event Methods

        private void FrmStatisCarSaleQuarter_Load(object sender, EventArgs e)
        {            
            try
            {
                tsbYear.SelectedIndex = tsbYear.Items.Count - 1;

                if (DateTime.Now.Month < 4)
                    tsbQuarter.SelectedIndex = 0;
                else if (DateTime.Now.Month < 7)
                    tsbQuarter.SelectedIndex = 1;
                else if (DateTime.Now.Month < 10)
                    tsbQuarter.SelectedIndex = 2;
                else
                    tsbQuarter.SelectedIndex = 3;

                LoadTableFromDB(true);

                writable = Permission.GetFuncPermission(this.Text);
                SetReadWriteProperty(writable);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmStatisCarSaleQuarter_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (cellchanged == true)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg(@"你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        SaveChange(tsbYear.SelectedItem.ToString(), tsbQuarter.SelectedIndex + 1);
                        cellchanged = false;
                    }
                }

                frmStatisCarSaleQuarter = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmStatisCarSaleQuarter_Activated(object sender, EventArgs e)
        {
            try
            {
                //LoadTableFromDB(false);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveChange(tsbYear.SelectedItem.ToString(), tsbQuarter.SelectedIndex + 1);
                cellchanged = false;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void tsbExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Excel files (*.xls)|*.xls";
                dlg.Title = "导出Excel";
                dlg.FileName = "销售季度任务统计.xls";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                // clear book
                xlbook.Clear();
                xlbook.Sheets.Clear();

                XLSheet sheet = xlbook.Sheets.Add(this.Text);
                SaveSheet(gridQuarter, sheet, false);

                xlbook.Save(dlg.FileName);
                Process.Start(dlg.FileName);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void tsbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tsbTitleText.Text = "     " + tsbYear.SelectedItem.ToString() + "年 " + tsbQuarter.SelectedItem.ToString() + "任务汇总";

                if (cellchanged == true)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg(@"你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        SaveChange(oldYear, oldQuarter);
                        cellchanged = false;
                    }
                }

                LoadTableFromDB(false);

                oldYear = tsbYear.SelectedItem.ToString();
                oldQuarter = tsbQuarter.SelectedIndex + 1;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }
        
        void tsbQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tsbTitleText.Text = "     " + tsbYear.SelectedItem.ToString() + "年 " + tsbQuarter.SelectedItem.ToString() + "任务汇总";

                if (cellchanged == true)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg(@"你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        SaveChange(oldYear, oldQuarter);
                        cellchanged = false;
                    }
                }

                LoadTableFromDB(false);

                oldYear = tsbYear.SelectedItem.ToString();
                oldQuarter = tsbQuarter.SelectedIndex + 1;
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
                if (cellchanged == true)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg(@"你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        SaveChange(tsbYear.SelectedItem.ToString(), tsbQuarter.SelectedIndex + 1);
                        cellchanged = false;
                    }
                }
                this.Close();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void gridQuarter_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                cellchanged = true;
                CalculateAmounts();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

        
        
    }
}