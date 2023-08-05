using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmSearch : Form
    {
        #region Fields and Properties

        public int selkey = 0;
        public String keyField1 = "";
        public String keyField2 = "";
        public String keyField3 = "";
        public String keyField4 = "";
        public bool isEnable = true;
        public int searchKind = Global.SEARCH_NONE;

        private Form parent = null;

        #endregion

        #region Constructors

        public FrmSearch()
        {
            InitializeComponent();

            this.FormClosed += new FormClosedEventHandler(FrmSearch_FormClosed);
            this.KeyPress += new KeyPressEventHandler(SearchKey_KeyPress);
            this.cbKey1.KeyPress += new KeyPressEventHandler(SearchKey_KeyPress);
            this.cbKey2.KeyPress += new KeyPressEventHandler(SearchKey_KeyPress);
            this.cbKey3.KeyPress += new KeyPressEventHandler(SearchKey_KeyPress);
        }

        public FrmSearch(Form frm): this()
        {
            parent = frm;
            //this.parent = frm;
        }

        #endregion        

        #region Public Methods

        public void InitSearch()
        {
            try
            {
                chkKey1.Text = "×Ö¶Î";
                chkKey1.Checked = false;
                cbKey1.Text = "";

                chkKey2.Text = "×Ö¶Î";
                chkKey2.Checked = false;
                cbKey2.Text = "";

                chkKey3.Text = "×Ö¶Î";
                chkKey3.Checked = false;
                cbKey3.Text = "";

                chkDate.Text = "ÈÕÆÚ×Ö¶Î";
                chkDate.Checked = false;
                dtpStart.Value = DateTime.Now;
                dtpEnd.Value = DateTime.Now;

                selkey = 0;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion

        #region Private Methods
        #endregion

        #region Event Methods

        void FrmSearch_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (parent is FrmOnRoadCar)
                {
                    FrmOnRoadCar frm = (FrmOnRoadCar)parent;
                    frm.frmSearch = null;
                }

                if (parent is FrmStoreIn)
                {
                    FrmStoreIn frm = (FrmStoreIn)parent;
                    frm.frmSearch = null;
                }

                if (parent is FrmStoreChange)
                {
                    FrmStoreChange frm = (FrmStoreChange)parent;
                    frm.frmSearch = null;
                }

                if (parent is FrmStoreOut)
                {
                    FrmStoreOut frm = (FrmStoreOut)parent;
                    frm.frmSearch = null;
                }

                if (parent is FrmSpecCar)
                {
                    FrmSpecCar frm = (FrmSpecCar)parent;
                    frm.frmSearch = null;
                }

                if (parent is FrmSearchOnroadCar)
                {
                    FrmSearchOnroadCar frm = (FrmSearchOnroadCar)parent;
                    frm.frmSearch = null;
                }

                if (parent is FrmSearchStorein)
                {
                    FrmSearchStorein frm = (FrmSearchStorein)parent;
                    frm.frmSearch = null;
                }

                if (parent is FrmSearchStoreout)
                {
                    FrmSearchStoreout frm = (FrmSearchStoreout)parent;
                    frm.frmSearch = null;
                }

                if (parent is FrmFinanceStore)
                {
                    FrmFinanceStore frm = (FrmFinanceStore)parent;
                    frm.frmSearch = null;
                }

                if (parent is FrmCarType)
                {
                    FrmCarType frm = (FrmCarType)parent;
                    frm.frmSearch = null;
                }

                if (parent is FrmCarCompany)
                {
                    FrmCarCompany frm = (FrmCarCompany)parent;
                    frm.frmSearch = null;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void btnErase_Click(object sender, EventArgs e)
        {
            try
            {
                chkKey1.Text = "×Ö¶Î";
                chkKey1.Checked = false;
                cbKey1.Text = "";

                chkKey2.Text = "×Ö¶Î";
                chkKey2.Checked = false;
                cbKey2.Text = "";

                chkKey3.Text = "×Ö¶Î";
                chkKey3.Checked = false;
                cbKey3.Text = "";

                //chkDate.Text = "ÈÕÆÚ×Ö¶Î";
                //chkDate.Checked = false;
                //dtpStart.Value = DateTime.Now;

                selkey = 0;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (parent is FrmOnRoadCar)
                {                    
                    FrmOnRoadCar frm = (FrmOnRoadCar)parent;

                    String filterText = "";

                    if (chkKey1.Checked)
                        filterText = keyField1 + " LIKE '%" + cbKey1.Text + "%'";
                    if (chkKey2.Checked)
                        filterText += " AND " + keyField2 + " LIKE '%" + cbKey2.Text + "%'";
                    if (chkKey3.Checked)
                        filterText += " AND " + keyField3 + " LIKE '%" + cbKey3.Text + "%'";

                    if (chkDate.Checked)
                    {
                        if (filterText.Length > 0)
                            filterText += " AND ";
                        filterText += keyField4 + " >= '" + dtpStart.Value.ToShortDateString() + "' AND " + keyField4 + " <= '" + dtpEnd.Value.ToShortDateString() + "'";
                    }

                    isEnable = false;
                    frm.SearchByCondition(filterText);
                    isEnable = true;
                }

                if (parent is FrmStoreIn)
                {
                    FrmStoreIn frm = (FrmStoreIn)parent;

                    String filterText = "";

                    if (chkKey1.Checked)
                        filterText = keyField1 + " LIKE '%" + cbKey1.Text + "%'";
                    if (chkKey2.Checked)
                        filterText += " AND " + keyField2 + " LIKE '%" + cbKey2.Text + "%'";
                    if (chkKey3.Checked)
                        filterText += " AND " + keyField3 + " LIKE '%" + cbKey3.Text + "%'";

                    if (chkDate.Checked)
                    {
                        if (filterText.Length > 0)
                            filterText += " AND ";
                        filterText += keyField4 + " >= '" + dtpStart.Value.ToShortDateString() + "' AND " + keyField4 + " <= '" + dtpEnd.Value.ToShortDateString() + "'";
                    }

                    isEnable = false;
                    frm.SearchByCondition(searchKind, filterText);
                    isEnable = true;
                }

                if (parent is FrmStoreChange)
                {
                    FrmStoreChange frm = (FrmStoreChange)parent;

                    String filterText = "";

                    if (chkKey1.Checked)
                        filterText = keyField1 + " LIKE '%" + cbKey1.Text + "%'";
                    if (chkKey2.Checked)
                        filterText += " AND " + keyField2 + " LIKE '%" + cbKey2.Text + "%'";
                    if (chkKey3.Checked)
                        filterText += " AND " + keyField3 + " LIKE '%" + cbKey3.Text + "%'";

                    if (chkDate.Checked)
                    {
                        if (filterText.Length > 0)
                            filterText += " AND ";
                        filterText += keyField4 + " >= '" + dtpStart.Value.ToShortDateString() + "' AND " + keyField4 + " <= '" + dtpEnd.Value.ToShortDateString() + "'";
                    }

                    isEnable = false;
                    frm.SearchByCondition(filterText);
                    isEnable = true;
                }

                if (parent is FrmStoreOut)
                {
                    FrmStoreOut frm = (FrmStoreOut)parent;

                    String filterText = "";

                    if (chkKey1.Checked)
                        filterText = keyField1 + " LIKE '%" + cbKey1.Text + "%'";
                    if (chkKey2.Checked)
                        filterText += " AND " + keyField2 + " LIKE '%" + cbKey2.Text + "%'";
                    if (chkKey3.Checked)
                        filterText += " AND " + keyField3 + " LIKE '%" + cbKey3.Text + "%'";

                    if (chkDate.Checked)
                    {
                        if (filterText.Length > 0)
                            filterText += " AND ";
                        filterText += keyField4 + " >= '" + dtpStart.Value.ToShortDateString() + "' AND " + keyField4 + " <= '" + dtpEnd.Value.ToShortDateString() + "'";
                    }

                    isEnable = false;
                    frm.SearchByCondition(searchKind, filterText);
                    isEnable = true;
                }

                if (parent is FrmSpecCar)
                {
                    FrmSpecCar frm = (FrmSpecCar)parent;

                    String filterText = "";

                    if (chkKey1.Checked)
                        filterText = keyField1 + " LIKE '%" + cbKey1.Text + "%'";
                    if (chkKey2.Checked)
                        filterText += " AND " + keyField2 + " LIKE '%" + cbKey2.Text + "%'";
                    if (chkKey3.Checked)
                        filterText += " AND " + keyField3 + " LIKE '%" + cbKey3.Text + "%'";

                    if (chkDate.Checked)
                    {
                        if (filterText.Length > 0)
                            filterText += " AND ";
                        filterText += keyField4 + " >= '" + dtpStart.Value.ToShortDateString() + "' AND " + keyField4 + " <= '" + dtpEnd.Value.ToShortDateString() + "'";
                    }

                    isEnable = false;
                    frm.SearchByCondition(filterText);
                    isEnable = true;
                }

                if (parent is FrmSearchOnroadCar)
                {
                    FrmSearchOnroadCar frm = (FrmSearchOnroadCar)parent;

                    String filterText = "";

                    if (chkKey1.Checked)
                        filterText = keyField1 + " LIKE '%" + cbKey1.Text + "%'";
                    if (chkKey2.Checked)
                        filterText += " AND " + keyField2 + " LIKE '%" + cbKey2.Text + "%'";
                    if (chkKey3.Checked)
                        filterText += " AND " + keyField3 + " LIKE '%" + cbKey3.Text + "%'";

                    if (chkDate.Checked)
                    {
                        if (filterText.Length > 0)
                            filterText += " AND ";
                        filterText += keyField4 + " >= '" + dtpStart.Value.ToShortDateString() + "' AND " + keyField4 + " <= '" + dtpEnd.Value.ToShortDateString() + "'";
                    }

                    isEnable = false;
                    frm.SearchByCondition(filterText);
                    isEnable = true;
                }

                if (parent is FrmSearchStorein)
                {
                    FrmSearchStorein frm = (FrmSearchStorein)parent;

                    String filterText = "";

                    if (chkKey1.Checked)
                        filterText = keyField1 + " LIKE '%" + cbKey1.Text + "%'";
                    if (chkKey2.Checked)
                        filterText += " AND " + keyField2 + " LIKE '%" + cbKey2.Text + "%'";
                    if (chkKey3.Checked)
                        filterText += " AND " + keyField3 + " LIKE '%" + cbKey3.Text + "%'";

                    if (chkDate.Checked)
                    {
                        if (filterText.Length > 0)
                            filterText += " AND ";
                        filterText += keyField4 + " >= '" + dtpStart.Value.ToShortDateString() + "' AND " + keyField4 + " <= '" + dtpEnd.Value.ToShortDateString() + "'";
                    }

                    isEnable = false;
                    frm.SearchByCondition(filterText);
                    isEnable = true;
                }

                if (parent is FrmSearchStoreout)
                {
                    FrmSearchStoreout frm = (FrmSearchStoreout)parent;

                    String filterText = "";

                    if (chkKey1.Checked)
                        filterText = keyField1 + " LIKE '%" + cbKey1.Text + "%'";
                    if (chkKey2.Checked)
                        filterText += " AND " + keyField2 + " LIKE '%" + cbKey2.Text + "%'";
                    if (chkKey3.Checked)
                        filterText += " AND " + keyField3 + " LIKE '%" + cbKey3.Text + "%'";

                    if (chkDate.Checked)
                    {
                        if (filterText.Length > 0)
                            filterText += " AND ";
                        filterText += keyField4 + " >= '" + dtpStart.Value.ToShortDateString() + "' AND " + keyField4 + " <= '" + dtpEnd.Value.ToShortDateString() + "'";
                    }

                    isEnable = false;
                    frm.SearchByCondition(filterText);
                    isEnable = true;
                }

                if (parent is FrmFinanceStore)
                {
                    FrmFinanceStore frm = (FrmFinanceStore)parent;

                    String filterText = "";

                    if (chkKey1.Checked)
                        filterText = keyField1 + " LIKE '%" + cbKey1.Text + "%'";
                    if (chkKey2.Checked)
                        filterText += " AND " + keyField2 + " LIKE '%" + cbKey2.Text + "%'";
                    if (chkKey3.Checked)
                        filterText += " AND " + keyField3 + " LIKE '%" + cbKey3.Text + "%'";

                    if (chkDate.Checked)
                    {
                        if (filterText.Length > 0)
                            filterText += " AND ";
                        filterText += keyField4 + " >= '" + dtpStart.Value.ToShortDateString() + "' AND " + keyField4 + " <= '" + dtpEnd.Value.ToShortDateString() + "'";
                    }

                    isEnable = false;
                    frm.SearchByCondition(filterText);
                    isEnable = true;
                }

                if (parent is FrmCarType)
                {
                    FrmCarType frm = (FrmCarType)parent;

                    String filterText = "";

                    if (chkKey1.Checked)
                        filterText = keyField1 + " LIKE '%" + cbKey1.Text + "%'";
                    if (chkKey2.Checked)
                        filterText += " AND " + keyField2 + " LIKE '%" + cbKey2.Text + "%'";
                    if (chkKey3.Checked)
                        filterText += " AND " + keyField3 + " LIKE '%" + cbKey3.Text + "%'";

                    if (chkDate.Checked)
                    {
                        if (filterText.Length > 0)
                            filterText += " AND ";
                        filterText += keyField4 + " >= '" + dtpStart.Value.ToShortDateString() + "' AND " + keyField4 + " <= '" + dtpEnd.Value.ToShortDateString() + "'";
                    }

                    isEnable = false;
                    frm.SearchByCondition(filterText);
                    isEnable = true;
                }

                if (parent is FrmCarCompany)
                {
                    FrmCarCompany frm = (FrmCarCompany)parent;

                    String filterText = "";

                    if (chkKey1.Checked)
                        filterText = keyField1 + " LIKE '%" + cbKey1.Text + "%'";
                    if (chkKey2.Checked)
                        filterText += " AND " + keyField2 + " LIKE '%" + cbKey2.Text + "%'";
                    if (chkKey3.Checked)
                        filterText += " AND " + keyField3 + " LIKE '%" + cbKey3.Text + "%'";

                    if (chkDate.Checked)
                    {
                        if (filterText.Length > 0)
                            filterText += " AND ";
                        filterText += keyField4 + " >= '" + dtpStart.Value.ToShortDateString() + "' AND " + keyField4 + " <= '" + dtpEnd.Value.ToShortDateString() + "'";
                    }

                    isEnable = false;
                    frm.SearchByCondition(filterText);
                    isEnable = true;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void SearchKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Return))
                {
                    btnSearch_Click(this, e);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion
    }
}