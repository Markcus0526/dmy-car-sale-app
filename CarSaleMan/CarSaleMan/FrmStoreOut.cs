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
    public partial class FrmStoreOut : Form
    {
        #region Fields and Properties

        public static FrmStoreOut frmStoreOut;
        public FrmSearch frmSearch;

        private bool selectMode = false;
        private bool selectAll = false;

        private bool writable;

        #endregion

        #region Constructors

        public FrmStoreOut()
        {
            InitializeComponent();

            writable = true;

            this.Activated += new EventHandler(FrmStoreOut_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmStoreOut_FormClosing);

            // flexgrid events
            this.gridStoreIn.SelChange += new EventHandler(gridStoreIn_SelChange);
            this.gridStoreIn.MouseClick += new MouseEventHandler(gridStoreIn_MouseClick);
            this.gridStoreIn.MouseDoubleClick += new MouseEventHandler(gridStoreIn_MouseDoubleClick);
            this.gridStoreIn.KeyPressEdit += new KeyPressEditEventHandler(gridStoreIn_KeyPressEdit);
            this.gridStoreIn.KeyDownEdit += new KeyEditEventHandler(gridStoreIn_KeyDownEdit);

            this.gridStoreOut.SelChange += new EventHandler(gridStoreOut_SelChange);
            this.gridStoreOut.MouseDoubleClick += new MouseEventHandler(gridStoreOut_MouseDoubleClick);
            this.gridStoreOut.KeyPressEdit += new KeyPressEditEventHandler(gridStoreOut_KeyPressEdit);
            this.gridStoreOut.KeyDownEdit += new KeyEditEventHandler(gridStoreOut_KeyDownEdit);
        }

        

        #endregion

        #region Public Methods

        public static FrmStoreOut GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmStoreOut == null) //if not created yet, Create an instance
                {
                    frmStoreOut = new FrmStoreOut();
                    frmStoreOut.MdiParent = parent;
                }
            }
            return frmStoreOut;  //just created or created earlier.Return it
        }

        public void SearchByCondition(int searchKind, String cond)
        {
            try
            {
                if (searchKind == Global.SEARCH_STOREOUT_STOREIN)
                {
                    vwStoreinBindingSource.Filter = cond;

                    if (cmsDB.tbl_onroad.Count > 0)
                    {
                        for (int i = 0; i < vwStoreinBindingSource.Count; i++)
                        {
                            gridStoreIn.Cols[0][i + 1] = i + 1;
                        }
                    }
                    gridStoreIn.AutoSizeCols();

                    frmSearch.lblCount.Text = vwStoreinBindingSource.Count.ToString();
                }

                if (searchKind == Global.SEARCH_STOREOUT_STOREOUT)
                {
                    vwStoreoutBindingSource.Filter = cond;

                    if (cmsDB.vw_storein.Count > 0)
                    {
                        for (int i = 0; i < vwStoreoutBindingSource.Count; i++)
                        {
                            gridStoreOut.Cols[0][i + 1] = i + 1;
                        }
                    }
                    gridStoreOut.AutoSizeCols();

                    frmSearch.lblCount.Text = vwStoreoutBindingSource.Count.ToString();
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion

        #region Private Methods

        private void LoadTableFromDB(bool storein, bool storeout)
        {
            try
            {
                tblStorechangeTableAdapter.Fill(cmsDB.tbl_storechange);
                
                // load storein view
                if (storein == true)
                {
                    tblStoreinBindingSource.Filter = "";
                    tblStoreinTableAdapter.FillByStorein(cmsDB.tbl_storein);

                    vwStoreinBindingSource.Filter = "";
                    vwStoreinTableAdapter.Fill(cmsDB.vw_storein);

                    if (cmsDB.tbl_storein.Count > 0)
                    {
                        for (int i = 0; i < cmsDB.tbl_storein.Count; i++)
                        {
                            gridStoreIn.Cols[0][i + 1] = i + 1;
                        }
                    }

                    gridStoreIn.AutoSizeCols();
                }

                // load storeout view
                if (storeout == true)
                {
                    tblStoreoutBindingSource.Filter = "";
                    tblStoreoutTableAdapter.Fill(cmsDB.tbl_storeout);

                    vwStoreoutBindingSource.Filter = "";
                    vwStoreoutTableAdapter.Fill(cmsDB.vw_storeout);

                    if (cmsDB.vw_storeout.Count > 0)
                    {
                        for (int i = 0; i < cmsDB.vw_storeout.Count; i++)
                        {
                            gridStoreOut.Cols[0][i + 1] = i + 1;
                        }
                    }

                    gridStoreOut.AutoSizeCols();
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
                    tsbChange2.Enabled = true;
                    tsbDelete2.Enabled = true;
                }
                else
                {
                    tsbCheck.Enabled = false;
                    tsbSelect.Enabled = false;
                    tsbAdd2.Enabled = false;
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

        private void FrmStoreOut_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }

        }

        void FrmStoreOut_Activated(object sender, EventArgs e)
        {
            try
            {
                LoadTableFromDB(true, true);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmStoreOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmStoreOut = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        // flexgrid events
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

        void gridStoreIn_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Rectangle rc = gridStoreIn.GetCellRect(gridStoreIn.RowSel, gridStoreIn.ColSel);
                Point pt = new Point(e.X, e.Y);

                if (rc.Contains(pt) && selectMode == true)
                {
                    if (gridStoreIn[gridStoreIn.RowSel, 1] == null || (bool)gridStoreIn[gridStoreIn.RowSel, 1] == false)
                        gridStoreIn[gridStoreIn.RowSel, 1] = true;
                    else
                        gridStoreIn[gridStoreIn.RowSel, 1] = false;
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

        void gridStoreOut_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null && frmSearch.Visible == true && frmSearch.isEnable == true)
                {
                    int r = gridStoreOut.Row;
                    int c = gridStoreOut.Col;

                    if (gridStoreOut[0, c].ToString().Equals("开单日期"))
                    {
                        frmSearch.chkDate.Text = gridStoreOut[0, c].ToString();
                        frmSearch.chkDate.Checked = true;
                        frmSearch.keyField4 = gridStoreOut.Cols[c].Name;

                        DateTime date = new DateTime();
                        if (DateTime.TryParse(gridStoreOut[r, c].ToString(), out date))
                            frmSearch.dtpStart.Value = date;
                        else
                            frmSearch.dtpStart.Value = DateTime.MinValue;
                    }
                    else
                    {
                        if (frmSearch.selkey == 0)
                        {
                            frmSearch.chkKey1.Text = gridStoreOut[0, c].ToString();
                            frmSearch.chkKey1.Checked = true;
                            frmSearch.keyField1 = gridStoreOut.Cols[c].Name;

                            frmSearch.cbKey1.Text = gridStoreOut[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 1)
                        {
                            frmSearch.chkKey2.Text = gridStoreOut[0, c].ToString();
                            frmSearch.chkKey2.Checked = true;
                            frmSearch.keyField2 = gridStoreOut.Cols[c].Name;

                            frmSearch.cbKey2.Text = gridStoreOut[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 2)
                        {
                            frmSearch.chkKey3.Text = gridStoreOut[0, c].ToString();
                            frmSearch.chkKey3.Checked = true;
                            frmSearch.keyField3 = gridStoreOut.Cols[c].Name;

                            frmSearch.cbKey3.Text = gridStoreOut[r, c].ToString();
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

        void gridStoreOut_MouseDoubleClick(object sender, MouseEventArgs e)
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

        void gridStoreOut_KeyPressEdit(object sender, KeyPressEditEventArgs e)
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

        void gridStoreOut_KeyDownEdit(object sender, KeyEditEventArgs e)
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

                if (frmSearch.searchKind != Global.SEARCH_STOREOUT_STOREIN)
                {
                    frmSearch.InitSearch();
                }

                frmSearch.searchKind = Global.SEARCH_STOREOUT_STOREIN;
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
                    gridStoreIn.Cols[1].Visible = true;
                    tsbSelect.Visible = true;
                }
                else
                {
                    tsbCheck.BackColor = Color.Transparent;
                    gridStoreIn.Cols[1].Visible = false;
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
                        for (int i = 1; i < gridStoreIn.Rows.Count; i++)
                        {
                            gridStoreIn[i, 1] = false;
                        }

                        selectAll = false;
                        tsbSelect.Text = @"全部选择";
                    }
                    else
                    {
                        for (int i = 1; i < gridStoreIn.Rows.Count; i++)
                        {
                            gridStoreIn[i, 1] = true;
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

                if (frmSearch.searchKind != Global.SEARCH_STOREOUT_STOREOUT)
                {
                    frmSearch.InitSearch();
                }

                frmSearch.searchKind = Global.SEARCH_STOREOUT_STOREOUT;
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

                int onroadid = Convert.ToInt32(gridStoreOut.Rows[gridStoreOut.Row]["onroadid"].ToString());
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
                if (gridStoreOut.Row < 1)
                    return;
                if (gridStoreOut.Rows.Count < 2)
                    return;

                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                int oldPos = gridStoreIn.Row;

                FrmStoreOutEdit frm = new FrmStoreOutEdit();
                frm.selRowIndex = gridStoreOut.Row - 1;
                frm.filterText = tblStoreoutBindingSource.Filter;
                frm.ShowDialog();

                if (frm.modified)
                {
                    LoadTableFromDB(false, true);
                    gridStoreOut.Row = oldPos;
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
                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                FrmStoreOutAdd frm = new FrmStoreOutAdd();

                if (selectMode == true)
                {
                    for (int i = 1; i < gridStoreIn.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(gridStoreIn[i, 1]) == true)
                        {
                            frm.onroadcarInList.Add(Convert.ToInt32(gridStoreIn[i, "onroadid"]));
                        }
                    }
                }
                else
                {
                    frm.onroadcarInList.Add(Convert.ToInt32(gridStoreIn[gridStoreIn.RowSel, "onroadid"]));
                }

                if (frm.onroadcarInList.Count > 0)
                {
                    frm.ShowDialog();
                    if (frm.modified == true)
                    {
                        for (int i = 0; i < frm.onroadcarOutList.Count; i++)
                        {
                            DataRow[] rows = cmsDB.tbl_storein.Select("onroadid = " + frm.onroadcarOutList[i]);
                            rows[0]["outflag"] = 1;
                            tblStoreinTableAdapter.Update(rows[0]);
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

        private void tsbDelete2_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                int onroadid = Convert.ToInt32(gridStoreOut.Rows[gridStoreOut.Row]["onroadid"].ToString());

                tblStoreoutTableAdapter.Fill(cmsDB.tbl_storeout);
                DataRow[] rows = cmsDB.tbl_onroad.Select("uid = " + onroadid);
                rows[0]["inflag"] = 0;
                tblStoreoutTableAdapter.Update(rows[0]);
                cmsDB.tbl_onroad.AcceptChanges();

                int uid = Convert.ToInt32(gridStoreOut.Rows[gridStoreOut.Row]["uid"].ToString());
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