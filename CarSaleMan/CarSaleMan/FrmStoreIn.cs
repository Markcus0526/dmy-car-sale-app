using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace CarSaleMan
{
    public partial class FrmStoreIn : Form
    {
        #region Fields and Properties

        public static FrmStoreIn frmStoreIn;
        public FrmSearch frmSearch;

        private bool selectMode = false;
        private bool selectAll = false;

        private bool writable;

        #endregion

        #region Constructors

        public FrmStoreIn()
        {
            InitializeComponent();

            writable = true;

            this.Activated += new EventHandler(FrmStoreIn_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmStoreIn_FormClosing);

            // flexgrid events
            this.gridRoadCar.SelChange += new EventHandler(gridRoadCar_SelChange);
            this.gridRoadCar.MouseClick += new MouseEventHandler(gridRoadCar_MouseClick);
            this.gridRoadCar.MouseDoubleClick += new MouseEventHandler(gridRoadCar_MouseDoubleClick);
            this.gridRoadCar.KeyPressEdit += new KeyPressEditEventHandler(gridRoadCar_KeyPressEdit);
            this.gridRoadCar.KeyDownEdit += new KeyEditEventHandler(gridRoadCar_KeyDownEdit);

            this.gridStoreIn.SelChange += new EventHandler(gridStoreIn_SelChange);
            this.gridStoreIn.MouseDoubleClick += new MouseEventHandler(gridStoreIn_MouseDoubleClick);
            this.gridStoreIn.KeyPressEdit += new KeyPressEditEventHandler(gridStoreIn_KeyPressEdit);
            this.gridStoreIn.KeyDownEdit += new KeyEditEventHandler(gridStoreIn_KeyDownEdit);
        }

        

        #endregion

        #region Public Methods

        public static FrmStoreIn GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmStoreIn == null) //if not created yet, Create an instance
                {
                    frmStoreIn = new FrmStoreIn();
                    frmStoreIn.MdiParent = parent;
                }
            }
            return frmStoreIn;  //just created or created earlier.Return it
        }

        public void SearchByCondition(int searchKind, String cond)
        {
            try
            {
                if (searchKind == Global.SEARCH_STOREIN_ONROADCAR)
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

                if (searchKind == Global.SEARCH_STOREIN_STOREIN)
                {
                    vwStoreinBindingSource.Filter = cond;

                    if (cmsDB.vw_storein.Count > 0)
                    {
                        for (int i = 0; i < vwStoreinBindingSource.Count; i++)
                        {
                            gridStoreIn.Cols[0][i + 1] = i + 1;
                        }
                    }
                    gridStoreIn.AutoSizeCols();

                    frmSearch.lblCount.Text = vwStoreinBindingSource.Count.ToString();
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion

        #region Private Methods

        private void LoadTableFromDB(bool onroad, bool storein)
        {
            try
            {
                // load onroad table
                if (onroad == true)
                {
                    tblOnroadBindingSource.Filter = "";

                    tblOnroadTableAdapter.FillByOnroad(cmsDB.tbl_onroad);

                    if (cmsDB.tbl_onroad.Count > 0)
                    {
                        for (int i = 0; i < cmsDB.tbl_onroad.Count; i++)
                        {
                            gridRoadCar.Cols[0][i + 1] = i + 1;
                        }
                    }

                    gridRoadCar.AutoSizeCols();
                }

                // load storein table
                if (storein == true)
                {
                    tblStoreinBindingSource.Filter = "";
                    tblStoreinTableAdapter.FillByStorein(cmsDB.tbl_storein);

                    vwStoreinBindingSource.Filter = "";
                    vwStoreinTableAdapter.Fill(cmsDB.vw_storein);

                    if (cmsDB.vw_storein.Count > 0)
                    {
                        for (int i = 0; i < cmsDB.vw_storein.Count; i++)
                        {
                            gridStoreIn.Cols[0][i + 1] = i + 1;
                        }
                    }

                    gridStoreIn.AutoSizeCols();
                }

                if (onroad == true || storein == true)
                {
                    tblStorechangeTableAdapter.Fill(cmsDB.tbl_storechange);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void SetReadWriteProperty(bool writable)
        {
            try
            {
                if (writable)
                {
                    tsbCheck.Enabled = true;
                    tsbSelect.Enabled = true;
                    tsbAdd2.Enabled = true;
                    tsbAddMan2.Enabled = true;
                    tsbChange2.Enabled = true;
                    tsbDelete2.Enabled = true;
                }
                else
                {
                    tsbCheck.Enabled = false;
                    tsbSelect.Enabled = false;
                    tsbAdd2.Enabled = false;
                    tsbAddMan2.Enabled = false;
                    tsbChange2.Enabled = false;
                    tsbDelete2.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

        #region Event Methods

        private void FrmStoreIn_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }

        }

        void FrmStoreIn_Activated(object sender, EventArgs e)
        {
            try
            {
                LoadTableFromDB(true, true);

                writable = Permission.GetFuncPermission(this.Text);
                SetReadWriteProperty(writable);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmStoreIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmStoreIn = null;
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

        void gridRoadCar_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {                
                Rectangle rc = gridRoadCar.GetCellRect(gridRoadCar.RowSel, gridRoadCar.ColSel);
                Point pt = new Point(e.X, e.Y);

                if (rc.Contains(pt) && selectMode == true)
                {
                    if (gridRoadCar[gridRoadCar.RowSel, 1] == null || (bool)gridRoadCar[gridRoadCar.RowSel, 1] == false)
                        gridRoadCar[gridRoadCar.RowSel, 1] = true;
                    else
                        gridRoadCar[gridRoadCar.RowSel, 1] = false;
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

                if (selectMode == false)
                {
                    tsbAdd2_Click(this, e);
                }
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

        void gridStoreIn_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null && frmSearch.Visible == true && frmSearch.isEnable == true)
                {
                    int r = gridStoreIn.Row;
                    int c = gridStoreIn.Col;

                    if (gridStoreIn[0, c].ToString().Equals("开单日期"))
                    {
                        frmSearch.chkDate.Text = gridStoreIn[0, c].ToString();
                        frmSearch.chkDate.Checked = true;
                        frmSearch.keyField4 = gridStoreIn.Cols[c].Name;

                        DateTime date = new DateTime();
                        if (DateTime.TryParse(gridStoreIn[r, c].ToString(), out date))
                            frmSearch.dtpStart.Value = date;
                        else
                            frmSearch.dtpStart.Value = DateTime.MinValue;
                    }
                    else
                    {
                        if (frmSearch.selkey == 0)
                        {
                            frmSearch.chkKey1.Text = gridStoreIn[0, c].ToString();
                            frmSearch.chkKey1.Checked = true;
                            frmSearch.keyField1 = gridStoreIn.Cols[c].Name;

                            frmSearch.cbKey1.Text = gridStoreIn[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 1)
                        {
                            frmSearch.chkKey2.Text = gridStoreIn[0, c].ToString();
                            frmSearch.chkKey2.Checked = true;
                            frmSearch.keyField2 = gridStoreIn.Cols[c].Name;

                            frmSearch.cbKey2.Text = gridStoreIn[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 2)
                        {
                            frmSearch.chkKey3.Text = gridStoreIn[0, c].ToString();
                            frmSearch.chkKey3.Checked = true;
                            frmSearch.keyField3 = gridStoreIn.Cols[c].Name;

                            frmSearch.cbKey3.Text = gridStoreIn[r, c].ToString();
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

        void gridStoreIn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (writable == false)
                    return;

                if (frmSearch != null)
                    return;

                tsbChange2_Click(this, e);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void gridStoreIn_KeyPressEdit(object sender, KeyPressEditEventArgs e)
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

        void gridStoreIn_KeyDownEdit(object sender, KeyEditEventArgs e)
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
        private void tsbFindAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                LoadTableFromDB(true, false);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbFindCondition_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch == null)
                    frmSearch = new FrmSearch(this);

                if (frmSearch.searchKind != Global.SEARCH_STOREIN_ONROADCAR)
                {
                    frmSearch.InitSearch();
                }

                frmSearch.searchKind = Global.SEARCH_STOREIN_ONROADCAR;
                frmSearch.Show(this);
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

        private void tsbCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                selectMode = !selectMode;

                if (selectMode == true)
                {
                    tsbCheck.BackColor = Color.MediumAquamarine;
                    gridRoadCar.Cols[1].Visible = true;
                    tsbSelect.Visible = true;
                }
                else
                {
                    tsbCheck.BackColor = Color.Transparent;
                    gridRoadCar.Cols[1].Visible = false;
                    tsbSelect.Visible = false;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                if (selectMode == true)
                {
                    if (selectAll == true)
                    {
                        for (int i = 1; i < gridRoadCar.Rows.Count; i++)
                        {
                            gridRoadCar[i, 1] = false;
                        }

                        selectAll = false;
                        tsbSelect.Text = @"全部选择";
                    }
                    else
                    {
                        for (int i = 1; i < gridRoadCar.Rows.Count; i++)
                        {
                            gridRoadCar[i, 1] = true;
                        } 

                        selectAll = true;
                        tsbSelect.Text = @"全部取消";
                    }
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

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbFindAll2_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                LoadTableFromDB(false, true);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbFindCondition2_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch == null)
                    frmSearch = new FrmSearch(this);

                if (frmSearch.searchKind != Global.SEARCH_STOREIN_STOREIN)
                {
                    frmSearch.InitSearch();
                }

                frmSearch.searchKind = Global.SEARCH_STOREIN_STOREIN;
                frmSearch.Show(this);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbHistory2_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                FrmActionHis frm = new FrmActionHis();

                int onroadid = Convert.ToInt32(gridStoreIn.Rows[gridStoreIn.Row]["onroadid"].ToString());
                DataRow[] rows = cmsDB.tbl_storechange.Select("onroadid = " + onroadid);
                if (rows != null && rows.Length > 0)
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        StoreChange his = new StoreChange();
                        his.vin = "";
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

        private void tsbChange2_Click(object sender, EventArgs e)
        {
            try
            {
                if (writable == false)
                    return;

                if (gridStoreIn.Row < 1)
                    return;
                if (gridStoreIn.Rows.Count < 2)
                    return;

                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                int oldPos = gridStoreIn.Row;

                FrmStoreInEdit frm = new FrmStoreInEdit();
                frm.selRowIndex = gridStoreIn.Row - 1;
                frm.filterText = tblStoreinBindingSource.Filter;
                frm.ShowDialog();

                if (frm.modified)
                {
                    LoadTableFromDB(false, true);
                    gridStoreIn.Row = oldPos;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }
        
        private void tsbAdd2_Click(object sender, EventArgs e)
        {
            try
            {
                if (writable == false)
                    return;

                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                FrmStoreInAdd frm = new FrmStoreInAdd();

                if (selectMode == true)
                {
                    for (int i = 1; i < gridRoadCar.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(gridRoadCar[i, 1]) == true)
                        {
                            frm.onroadcarInList.Add(Convert.ToInt32(gridRoadCar[i, "uid"]));
                        }
                    }
                }
                else
                {
                    frm.onroadcarInList.Add(Convert.ToInt32(gridRoadCar[gridRoadCar.RowSel, "uid"]));
                }

                if (frm.onroadcarInList.Count > 0)
                {
                    frm.ShowDialog();
                    if (frm.modified == true)
                    {
                        for (int i = 0; i < frm.onroadcarOutList.Count; i++)
                        {
                            DataRow[] rows = cmsDB.tbl_onroad.Select("uid = " + frm.onroadcarOutList[i]);
                            rows[0]["inflag"] = 1;
                            tblOnroadTableAdapter.Update(rows[0]);
                        }
                        cmsDB.tbl_onroad.AcceptChanges();

                        LoadTableFromDB(true, true);
                    }
                }
                else
                {
                    FrmMessage msg = new FrmMessage();
                    msg.ShowCloseMsg("You must select item in onroad car list.", "", @"警告");
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbAddMan2_Click(object sender, EventArgs e)
        {
            try
            {
                if (writable == false)
                    return;

                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                FrmStoreInAddMan frm = new FrmStoreInAddMan();

                frm.ShowDialog();
                if (frm.modified == true)
                {
                    LoadTableFromDB(true, true);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbDelete2_Click(object sender, EventArgs e)
        {
            try
            {
                if (writable == false)
                    return;

                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                int onroadid = Convert.ToInt32(gridStoreIn.Rows[gridStoreIn.Row]["onroadid"].ToString());

                tblOnroadTableAdapter.Fill(cmsDB.tbl_onroad);
                DataRow[] rows = cmsDB.tbl_onroad.Select("uid = " + onroadid);
                rows[0]["inflag"] = 0;
                tblOnroadTableAdapter.Update(rows[0]);
                cmsDB.tbl_onroad.AcceptChanges();

                int uid = Convert.ToInt32(gridStoreIn.Rows[gridStoreIn.Row]["uid"].ToString());
                tblStoreinTableAdapter.DeleteByUid(uid);
                //tblStoreinTableAdapter.Update(cmsDB.tbl_storein);
                cmsDB.tbl_storein.AcceptChanges();

                LoadTableFromDB(true, true);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion
    }
}