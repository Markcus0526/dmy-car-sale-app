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
    public partial class FrmStoreChange : Form
    {
        #region Fields and Properties

        public static FrmStoreChange frmStoreChange;
        public FrmSearch frmSearch;

        private bool writable;

        #endregion

        #region Constructors

        public FrmStoreChange()
        {
            InitializeComponent();

            writable = true;

            this.Activated += new EventHandler(FrmStoreChange_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmStoreChange_FormClosing);

            // flexgrid events
            this.gridStoreChange.SelChange += new EventHandler(gridStoreChange_SelChange);
            this.gridStoreChange.MouseDoubleClick += new MouseEventHandler(gridStoreChange_MouseDoubleClick);
            this.gridStoreChange.KeyPressEdit += new KeyPressEditEventHandler(gridStoreChange_KeyPressEdit);
            this.gridStoreChange.KeyDownEdit += new KeyEditEventHandler(gridStoreChange_KeyDownEdit);
        }
                
        #endregion

        #region Public Methods

        public static FrmStoreChange GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmStoreChange == null) //if not created yet, Create an instance
                {
                    frmStoreChange = new FrmStoreChange();
                    frmStoreChange.MdiParent = parent;
                }
            }
            return frmStoreChange;  //just created or created earlier.Return it
        }

        public void SearchByCondition(String cond)
        {
            try
            {
                vwStoreinBindingSource.Filter = cond;

                if (cmsDB.vw_storein.Count > 0)
                {
                    for (int i = 0; i < vwStoreinBindingSource.Count; i++)
                    {
                        gridStoreChange.Cols[0][i + 1] = i + 1;
                    }
                }
                gridStoreChange.AutoSizeCols();

                frmSearch.lblCount.Text = vwStoreinBindingSource.Count.ToString();
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
                tblStoreinBindingSource.Filter = "";
                tblStoreinTableAdapter.FillByStorein(cmsDB.tbl_storein);

                vwStoreinBindingSource.Filter = "";
                vwStoreinTableAdapter.Fill(cmsDB.vw_storein);

                tblStorechangeTableAdapter.Fill(cmsDB.tbl_storechange);

                if (cmsDB.vw_storein.Count > 0)
                {
                    for (int i = 0; i < cmsDB.vw_storein.Count; i++)
                    {
                        gridStoreChange.Cols[0][i + 1] = i + 1;
                    }
                }

                gridStoreChange.AutoSizeCols();
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
                    tsbChange.Enabled = true;
                }
                else
                {
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

        private void FrmStoreChange_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmStoreChange_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmStoreChange = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmStoreChange_Activated(object sender, EventArgs e)
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
        void gridStoreChange_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null && frmSearch.Visible == true && frmSearch.isEnable == true)
                {
                    int r = gridStoreChange.RowSel;
                    int c = gridStoreChange.ColSel;

                    if (gridStoreChange[0, c].ToString().Equals("开单日期"))
                    {
                        frmSearch.chkDate.Text = gridStoreChange[0, c].ToString();
                        frmSearch.chkDate.Checked = true;
                        frmSearch.keyField4 = gridStoreChange.Cols[c].Name;

                        DateTime date = new DateTime();
                        if (DateTime.TryParse(gridStoreChange[r, c].ToString(), out date))
                            frmSearch.dtpStart.Value = date;
                        else
                            frmSearch.dtpStart.Value = DateTime.MinValue;
                    }
                    else
                    {
                        if (frmSearch.selkey == 0)
                        {
                            frmSearch.chkKey1.Text = gridStoreChange[0, c].ToString();
                            frmSearch.chkKey1.Checked = true;
                            frmSearch.keyField1 = gridStoreChange.Cols[c].Name;

                            frmSearch.cbKey1.Text = gridStoreChange[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 1)
                        {
                            frmSearch.chkKey2.Text = gridStoreChange[0, c].ToString();
                            frmSearch.chkKey2.Checked = true;
                            frmSearch.keyField2 = gridStoreChange.Cols[c].Name;

                            frmSearch.cbKey2.Text = gridStoreChange[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 2)
                        {
                            frmSearch.chkKey3.Text = gridStoreChange[0, c].ToString();
                            frmSearch.chkKey3.Checked = true;
                            frmSearch.keyField3 = gridStoreChange.Cols[c].Name;

                            frmSearch.cbKey3.Text = gridStoreChange[r, c].ToString();
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

        void gridStoreChange_MouseDoubleClick(object sender, MouseEventArgs e)
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

        void gridStoreChange_KeyPressEdit(object sender, KeyPressEditEventArgs e)
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

        void gridStoreChange_KeyDownEdit(object sender, KeyEditEventArgs e)
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

                frmSearch.searchKind = Global.SEARCH_STORECHANGE;
                frmSearch.Show(this);
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
                if (gridStoreChange.Row < 1)
                    return;
                if (gridStoreChange.Rows.Count < 2)
                    return;

                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                int oldPos = gridStoreChange.Row;

                FrmStoreChangeEdit frm = new FrmStoreChangeEdit();
                frm.selRowIndex = gridStoreChange.Row - 1;
                frm.filterText = vwStoreinBindingSource.Filter;
                frm.ShowDialog();
                
                if (frm.modified)
                {
                    LoadTableFromDB();
                    gridStoreChange.Row = oldPos;
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

                int onroadid = Convert.ToInt32(gridStoreChange.Rows[gridStoreChange.Row]["onroadid"].ToString());
                String vin = gridStoreChange.Rows[gridStoreChange.Row]["vin"].ToString();
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