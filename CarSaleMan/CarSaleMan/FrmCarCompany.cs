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
    public partial class FrmCarCompany : Form
    {
        #region Fields and Properties

        public static FrmCarCompany frmCarCompany;
        public FrmSearch frmSearch;

        #endregion

        #region Constructors

        public FrmCarCompany()
        {
            InitializeComponent();

            // init private variables

            this.Activated += new EventHandler(FrmCarCompany_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmCarCompany_FormClosing);

            // flexgrid events
            this.gridCarCompany.SelChange += new EventHandler(gridCarCompany_SelChange);
            this.gridCarCompany.MouseDoubleClick += new MouseEventHandler(gridCarCompany_MouseDoubleClick);
        }
                
        #endregion

        #region Public Methods

        public static FrmCarCompany GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmCarCompany == null) //if not created yet, Create an instance
                {
                    frmCarCompany = new FrmCarCompany();
                    frmCarCompany.MdiParent = parent;
                }
            }
            return frmCarCompany;  //just created or created earlier.Return it
        }

        public void SearchByCondition(String cond)
        {
            try
            {
                tblCarcompanyBindingSource.Filter = cond;

                if (cmsDB.tbl_carcompany.Count > 0)
                {
                    for (int i = 0; i < tblCarcompanyBindingSource.Count; i++)
                    {
                        gridCarCompany.Cols[0][i + 1] = i + 1;
                    }
                }
                gridCarCompany.AutoSizeCols();

                frmSearch.lblCount.Text = tblCarcompanyBindingSource.Count.ToString();
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
                tblCarcompanyBindingSource.Filter = "";

                tblCarcompanyTableAdapter.FillByCompanyno(cmsDB.tbl_carcompany);

                if (cmsDB.tbl_carcompany.Count > 0)
                {
                    for (int i = 0; i < cmsDB.tbl_carcompany.Count; i++)
                    {
                        gridCarCompany.Cols[0][i + 1] = i + 1;
                    }
                }

                gridCarCompany.AutoSizeCols();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion

        #region Event Methods

        private void FrmCarCompany_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmCarCompany_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmCarCompany = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmCarCompany_Activated(object sender, EventArgs e)
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

        // flexgrid events
        void gridCarCompany_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null && frmSearch.Visible == true && frmSearch.isEnable == true)
                {
                    int r = gridCarCompany.RowSel;
                    int c = gridCarCompany.ColSel;

                    if (gridCarCompany[0, c].ToString().Equals("开单日期"))
                    {
                        frmSearch.chkDate.Text = gridCarCompany[0, c].ToString();
                        frmSearch.chkDate.Checked = true;
                        frmSearch.keyField4 = gridCarCompany.Cols[c].Name;

                        DateTime date = new DateTime();
                        if (DateTime.TryParse(gridCarCompany[r, c].ToString(), out date))
                            frmSearch.dtpStart.Value = date;
                        else
                            frmSearch.dtpStart.Value = DateTime.MinValue;
                    }
                    else
                    {
                        if (frmSearch.selkey == 0)
                        {
                            frmSearch.chkKey1.Text = gridCarCompany[0, c].ToString();
                            frmSearch.chkKey1.Checked = true;
                            frmSearch.keyField1 = gridCarCompany.Cols[c].Name;

                            frmSearch.cbKey1.Text = gridCarCompany[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 1)
                        {
                            frmSearch.chkKey2.Text = gridCarCompany[0, c].ToString();
                            frmSearch.chkKey2.Checked = true;
                            frmSearch.keyField2 = gridCarCompany.Cols[c].Name;

                            frmSearch.cbKey2.Text = gridCarCompany[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 2)
                        {
                            frmSearch.chkKey3.Text = gridCarCompany[0, c].ToString();
                            frmSearch.chkKey3.Checked = true;
                            frmSearch.keyField3 = gridCarCompany.Cols[c].Name;

                            frmSearch.cbKey3.Text = gridCarCompany[r, c].ToString();
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

        void gridCarCompany_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                tsbChange_Click(this, e);
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

                frmSearch.searchKind = Global.SEARCH_SETTING_COMPANY;
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
                /*if (gridCarCompany.Row < 1)
                    return;
                if (gridCarCompany.Rows.Count < 2)
                    return;*/

                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                int oldPos = gridCarCompany.Row;

                FrmCarCompanyEdit frm = new FrmCarCompanyEdit();
                frm.selRowIndex = gridCarCompany.Row - 1;
                frm.filterText = tblCarcompanyBindingSource.Filter;
                frm.ShowDialog();
                
                if (frm.modified)
                {
                    LoadTableFromDB();
                    gridCarCompany.Row = oldPos;
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
                if (frm.ShowDialog() == DialogResult.OK)
                {
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