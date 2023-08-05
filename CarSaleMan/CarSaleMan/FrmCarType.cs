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
    public partial class FrmCarType : Form
    {
        #region Fields and Properties

        public static FrmCarType frmCarType;
        public FrmSearch frmSearch;

        #endregion

        #region Constructors

        public FrmCarType()
        {
            InitializeComponent();

            // init private variables

            this.Activated += new EventHandler(FrmCarType_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmCarType_FormClosing);

            // flexgrid events
            this.gridCarType.SelChange += new EventHandler(gridCarType_SelChange);
            this.gridCarType.MouseDoubleClick += new MouseEventHandler(gridCarType_MouseDoubleClick);
        }
                
        #endregion

        #region Public Methods

        public static FrmCarType GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmCarType == null) //if not created yet, Create an instance
                {
                    frmCarType = new FrmCarType();
                    frmCarType.MdiParent = parent;
                }
            }
            return frmCarType;  //just created or created earlier.Return it
        }

        public void SearchByCondition(String cond)
        {
            try
            {
                tblCartypeBindingSource.Filter = cond;

                if (cmsDB.tbl_cartype.Count > 0)
                {
                    for (int i = 0; i < tblCartypeBindingSource.Count; i++)
                    {
                        gridCarType.Cols[0][i + 1] = i + 1;
                    }
                }
                gridCarType.AutoSizeCols();

                frmSearch.lblCount.Text = tblCartypeBindingSource.Count.ToString();
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
                tblCartypeBindingSource.Filter = "";

                tblCartypeTableAdapter.FillByCarseries(cmsDB.tbl_cartype);

                if (cmsDB.tbl_cartype.Count > 0)
                {
                    for (int i = 0; i < cmsDB.tbl_cartype.Count; i++)
                    {
                        gridCarType.Cols[0][i + 1] = i + 1;
                    }
                }

                gridCarType.AutoSizeCols();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion

        #region Event Methods

        private void FrmCarType_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmCarType_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmCarType = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmCarType_Activated(object sender, EventArgs e)
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
        void gridCarType_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null && frmSearch.Visible == true && frmSearch.isEnable == true)
                {
                    int r = gridCarType.RowSel;
                    int c = gridCarType.ColSel;

                    if (gridCarType[0, c].ToString().Equals("开单日期"))
                    {
                        frmSearch.chkDate.Text = gridCarType[0, c].ToString();
                        frmSearch.chkDate.Checked = true;
                        frmSearch.keyField4 = gridCarType.Cols[c].Name;

                        DateTime date = new DateTime();
                        if (DateTime.TryParse(gridCarType[r, c].ToString(), out date))
                            frmSearch.dtpStart.Value = date;
                        else
                            frmSearch.dtpStart.Value = DateTime.MinValue;
                    }
                    else
                    {
                        if (frmSearch.selkey == 0)
                        {
                            frmSearch.chkKey1.Text = gridCarType[0, c].ToString();
                            frmSearch.chkKey1.Checked = true;
                            frmSearch.keyField1 = gridCarType.Cols[c].Name;

                            frmSearch.cbKey1.Text = gridCarType[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 1)
                        {
                            frmSearch.chkKey2.Text = gridCarType[0, c].ToString();
                            frmSearch.chkKey2.Checked = true;
                            frmSearch.keyField2 = gridCarType.Cols[c].Name;

                            frmSearch.cbKey2.Text = gridCarType[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 2)
                        {
                            frmSearch.chkKey3.Text = gridCarType[0, c].ToString();
                            frmSearch.chkKey3.Checked = true;
                            frmSearch.keyField3 = gridCarType.Cols[c].Name;

                            frmSearch.cbKey3.Text = gridCarType[r, c].ToString();
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

        void gridCarType_MouseDoubleClick(object sender, MouseEventArgs e)
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

                frmSearch.searchKind = Global.SEARCH_SETTING_CARTYPE;
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
                if (gridCarType.Row < 1)
                    return;
                if (gridCarType.Rows.Count < 2)
                    return;

                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                int oldPos = gridCarType.Row;

                FrmCarTypeEdit frm = new FrmCarTypeEdit();
                frm.selRowIndex = gridCarType.Row - 1;
                frm.filterText = tblCartypeBindingSource.Filter;
                frm.ShowDialog();
                
                if (frm.modified)
                {
                    LoadTableFromDB();
                    gridCarType.Row = oldPos;
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