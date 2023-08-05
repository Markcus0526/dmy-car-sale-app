using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.Globalization;

namespace CarSaleMan
{
    public partial class FrmOnRoadCar : Form
    {
        #region Fields and Properties

        public static FrmOnRoadCar frmOnRoadCar;
        public FrmSearch frmSearch;

        private bool writable;

        #endregion

        #region Constructors

        public FrmOnRoadCar()
        {
            InitializeComponent();

            // init private variables
            writable = true;

            this.Activated += new EventHandler(FrmOnRoadCar_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmOnRoadCar_FormClosing);

            // flexgrid events
            this.gridRoadCar.SelChange += new EventHandler(gridRoadCar_SelChange);
            this.gridRoadCar.MouseDoubleClick += new MouseEventHandler(gridRoadCar_MouseDoubleClick);
            this.gridRoadCar.KeyPressEdit += new KeyPressEditEventHandler(gridRoadCar_KeyPressEdit);
            this.gridRoadCar.KeyDownEdit += new KeyEditEventHandler(gridRoadCar_KeyDownEdit);
        }
                
        #endregion

        #region Public Methods

        public static FrmOnRoadCar GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmOnRoadCar == null) //if not created yet, Create an instance
                {
                    frmOnRoadCar = new FrmOnRoadCar();
                    frmOnRoadCar.MdiParent = parent;
                }
            }
            return frmOnRoadCar;  //just created or created earlier.Return it
        }

        public void SearchByCondition(String cond)
        {
            try
            {
                tblOnroadBindingSource.Filter = cond;

                if (cmsDB.tbl_onroad.Count > 0)
                {
                    for (int i = 0; i < tblOnroadBindingSource.Count; i++)
                    {
                        gridRoadCar.Cols[0][i + 1] = i + 1;
                    }
                }
                gridRoadCar.AutoSizeCols();

                frmSearch.lblCount.Text = tblOnroadBindingSource.Count.ToString();
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
                tblOnroadBindingSource.Filter = "";

                tblOnroadTableAdapter.FillByOnroad(cmsDB.tbl_onroad);
                tblStorechangeTableAdapter.Fill(cmsDB.tbl_storechange);
                tblCartypeTableAdapter.Fill(cmsDB.tbl_cartype);

                if (cmsDB.tbl_onroad.Count > 0)
                {
                    for (int i = 0; i < cmsDB.tbl_onroad.Count; i++)
                    {
                        gridRoadCar.Cols[0][i + 1] = i + 1;
                    }
                }

                gridRoadCar.AutoSizeCols();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void ImportFromExcel(C1FlexGrid grid)
        {
            try
            {
                tblCartypeTableAdapter.Fill(cmsDB.tbl_cartype);

                for (int i = 1; i < grid.Rows.Count; i++)
                {
                    DataRow row = cmsDB.tbl_onroad.NewRow();
                    row["billno"] = grid[i, 3].ToString();
                    DateTime date = new DateTime();
                    row["billdate"] = DateTime.TryParseExact(grid[i, 4].ToString(), "yyyyMMdd", new CultureInfo("en-US"), DateTimeStyles.None, out date) ? date : DateTime.MinValue;
                    row["vin"] = grid[i, 7].ToString();

                    String carCode = grid[i, 9].ToString();
                    String subsets = grid[i, 17].ToString();
                    String insidesetcode = grid[i, 15].ToString();

                    row["engineno"] = grid[i, 8].ToString();
                    row["cartypeid"] = GetCartypeIdFromImportCar(carCode, subsets, insidesetcode);
                    row["cartype"] = grid[i, 9].ToString();
                    row["carname"] = grid[i, 10].ToString();
                    row["colorcode"] = grid[i, 11].ToString();
                    row["colorname"] = grid[i, 12].ToString();
                    row["insidesetcode"] = grid[i, 15].ToString();
                    row["insidesetname"] = grid[i, 16].ToString();
                    row["subsets"] = grid[i, 17].ToString();
                    row["carstate"] = grid[i, 18].ToString();
                    row["property"] = grid[i, 19].ToString();
                    row["inprice"] = 0;
                    row["inflag"] = 0;
                    row["inkind"] = 0;

                    cmsDB.tbl_onroad.Rows.Add(row);
                }

                tblOnroadTableAdapter.Update(cmsDB.tbl_onroad);

                // save to storechange
                for (int i = 1; i < grid.Rows.Count; i++)
                {
                    DataRow[] rows = cmsDB.tbl_onroad.Select("vin = '" + grid[i, 7].ToString() + "'");
                    if (rows != null && rows.Length > 0)
                    {
                        DataRow row = cmsDB.tbl_storechange.NewRow();
                        row["changeid"] = cmsDB.tbl_storechange.Rows.Count;
                        row["batchno"] = rows[0]["billno"];
                        row["onroadid"] = rows[0]["uid"];
                        row["storeplace"] = "";
                        row["actionkind"] = Global.CAR_PURCHASE;
                        row["actiondate"] = DateTime.Now;
                        row["actionpay"] = 0;
                        row["settlementname"] = "";
                        row["handlername"] = Global.LOGIN_USERNAME;
                        row["repairstate"] = "";
                        row["reservestate"] = "";
                        row["remark"] = "";

                        cmsDB.tbl_storechange.Rows.Add(row);
                    }
                }

                tblStorechangeTableAdapter.Update(cmsDB.tbl_storechange);

                if (cmsDB.tbl_onroad.Count > 0)
                {
                    for (int i = 0; i < cmsDB.tbl_onroad.Count; i++)
                    {
                        gridRoadCar.Cols[0][i + 1] = i + 1;
                    }
                }

                gridRoadCar.AutoSizeCols();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private int GetCartypeIdFromImportCar(String carcode, String subsets, string insidesetcode)
        {
            int uid = 0;
            try
            {
                String selectQuery = "carcode = '" + carcode + "' AND subsets = '" + subsets + "' AND insidesetcode = '" + insidesetcode + "'";
                DataRow[] rows = cmsDB.tbl_cartype.Select(selectQuery);

                if (rows.Length > 0)
                {
                    uid = int.Parse(rows[0]["uid"].ToString());
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }

            return uid;
        }

        private void SetReadWriteProperty(bool writable)
        {
            try
            {
                if (writable)
                {
                    tsbImportExcel.Enabled = true;
                    tsbChange.Enabled = true;
                }
                else
                {
                    tsbImportExcel.Enabled = false;
                    tsbChange.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }
        
        #endregion

        #region Event Methods

        private void FrmOnRoadCar_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmOnRoadCar_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmOnRoadCar = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmOnRoadCar_Activated(object sender, EventArgs e)
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

        // flexgrid events
        void gridRoadCar_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null && frmSearch.Visible == true && frmSearch.isEnable == true)
                {
                    int r = gridRoadCar.RowSel;
                    int c = gridRoadCar.ColSel;

                    if (gridRoadCar[0, c].ToString().Equals("开单日期"))
                    {
                        frmSearch.chkDate.Text = gridRoadCar[0, c].ToString();
                        frmSearch.chkDate.Checked = true;
                        frmSearch.keyField4 = gridRoadCar.Cols[c].Name;

                        DateTime date = new DateTime();
                        if (DateTime.TryParse(gridRoadCar[r, c].ToString(), out date))
                            frmSearch.dtpStart.Value = date;
                        else
                            frmSearch.dtpStart.Value = DateTime.MinValue;
                    }
                    else
                    {
                        if (frmSearch.selkey == 0)
                        {
                            frmSearch.chkKey1.Text = gridRoadCar[0, c].ToString();
                            frmSearch.chkKey1.Checked = true;
                            frmSearch.keyField1 = gridRoadCar.Cols[c].Name;

                            frmSearch.cbKey1.Text = gridRoadCar[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 1)
                        {
                            frmSearch.chkKey2.Text = gridRoadCar[0, c].ToString();
                            frmSearch.chkKey2.Checked = true;
                            frmSearch.keyField2 = gridRoadCar.Cols[c].Name;

                            frmSearch.cbKey2.Text = gridRoadCar[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 2)
                        {
                            frmSearch.chkKey3.Text = gridRoadCar[0, c].ToString();
                            frmSearch.chkKey3.Checked = true;
                            frmSearch.keyField3 = gridRoadCar.Cols[c].Name;

                            frmSearch.cbKey3.Text = gridRoadCar[r, c].ToString();
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

        void gridRoadCar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (writable == false)
                    return;

                if (frmSearch != null)
                    return;

                tsbChange_Click(this, e);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void gridRoadCar_KeyPressEdit(object sender, KeyPressEditEventArgs e)
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

        void gridRoadCar_KeyDownEdit(object sender, KeyEditEventArgs e)
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

                frmSearch.searchKind = Global.SEARCH_ONROADCAR;
                frmSearch.Show(this);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }
        
        void tsbImportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                FrmImportExcel frm = new FrmImportExcel();
                DialogResult ret = frm.ShowDialog();

                if (ret == DialogResult.Yes)
                {
                    if (frmSearch != null)
                    {
                        frmSearch.Close();
                        frmSearch = null;
                    }

                    C1FlexGrid grid = frm.gridImportCar;

                    ImportFromExcel(grid);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                int oldPos = gridRoadCar.Row;

                FrmOnRoadCarEdit frm = new FrmOnRoadCarEdit();
                frm.selRowIndex = gridRoadCar.Row - 1;
                frm.filterText = tblOnroadBindingSource.Filter;
                frm.ShowDialog();
                
                if (frm.modified)
                {
                    LoadTableFromDB();
                    gridRoadCar.Row = oldPos;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                FrmActionHis frm = new FrmActionHis();

                int onroadid = Convert.ToInt32(gridRoadCar.Rows[gridRoadCar.Row]["uid"].ToString());
                String vin = gridRoadCar.Rows[gridRoadCar.Row]["vin"].ToString();
                DataRow[] rows = cmsDB.tbl_storechange.Select("onroadid = " + onroadid);
                if (rows != null && rows.Length > 0)
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        StoreChange his = new StoreChange();
                        his.vin = vin;
                        his.chagneid = Convert.ToInt32(rows[i]["changeid"].ToString());
                        his.batchno = rows[i]["batchno"].ToString();
                        his.storeplace = rows[i]["storeplace"].ToString();
                        his.actionkind = rows[i]["actionkind"].ToString();
                        his.actiondate = rows[i]["actiondate"].ToString();
                        his.actionpay = rows[i]["actionpay"].ToString();
                        his.settlementname = rows[i]["settlementname"].ToString();
                        his.handlername = rows[i]["handlername"].ToString();
                        his.repairstate = rows[i]["repairstate"].ToString();
                        his.reservestate = rows[i]["reservestate"].ToString();
                        his.remark = rows[i]["remark"].ToString();

                        frm.actionlist.Add(his);
                    }
                    
                    frm.ShowDialog();
                }                
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