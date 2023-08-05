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
    public partial class FrmSpecCar : Form
    {
        #region Fields and Properties

        public static FrmSpecCar frmSpecCar;
        public FrmSearch frmSearch;

        private bool writable;

        #endregion

        #region Constructors

        public FrmSpecCar()
        {
            InitializeComponent();

            // init private variables
            writable = true;

            this.Activated += new EventHandler(FrmSpecCar_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmSpecCar_FormClosing);

            // flexgrid events
            this.gridSpecCar.SelChange += new EventHandler(gridSpecCar_SelChange);
            this.gridSpecCar.MouseDoubleClick += new MouseEventHandler(gridSpecCar_MouseDoubleClick);
        }
                
        #endregion

        #region Public Methods

        public static FrmSpecCar GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmSpecCar == null) //if not created yet, Create an instance
                {
                    frmSpecCar = new FrmSpecCar();
                    frmSpecCar.MdiParent = parent;
                }
            }
            return frmSpecCar;  //just created or created earlier.Return it
        }

        public void SearchByCondition(String cond)
        {
            try
            {
                vwSpeccarBindingSource.Filter = cond;

                if (cmsDB.vw_speccar.Count > 0)
                {
                    for (int i = 0; i < vwSpeccarBindingSource.Count; i++)
                    {
                        gridSpecCar.Cols[0][i + 1] = i + 1;
                    }
                }
                gridSpecCar.AutoSizeCols();

                frmSearch.lblCount.Text = vwSpeccarBindingSource.Count.ToString();
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
                vwSpeccarBindingSource.Filter = "";

                vwSpeccarTableAdapter.Fill(cmsDB.vw_speccar);

                if (cmsDB.vw_speccar.Count > 0)
                {
                    for (int i = 0; i < cmsDB.vw_speccar.Count; i++)
                    {
                        gridSpecCar.Cols[0][i + 1] = i + 1;
                    }
                }

                gridSpecCar.AutoSizeCols();
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
                    tsbExportExcel.Enabled = true;
                    tsbChange.Enabled = true;
                }
                else
                {
                    tsbExportExcel.Enabled = false;
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

        private void FrmSpecCar_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmSpecCar_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmSpecCar = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmSpecCar_Activated(object sender, EventArgs e)
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
        void gridSpecCar_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null && frmSearch.Visible == true && frmSearch.isEnable == true)
                {
                    int r = gridSpecCar.RowSel;
                    int c = gridSpecCar.ColSel;

                    if (gridSpecCar[0, c].ToString().Equals("开单日期"))
                    {
                        frmSearch.chkDate.Text = gridSpecCar[0, c].ToString();
                        frmSearch.chkDate.Checked = true;
                        frmSearch.keyField4 = gridSpecCar.Cols[c].Name;

                        DateTime date = new DateTime();
                        if (DateTime.TryParse(gridSpecCar[r, c].ToString(), out date))
                            frmSearch.dtpStart.Value = date;
                        else
                            frmSearch.dtpStart.Value = DateTime.MinValue;
                    }
                    else
                    {
                        if (frmSearch.selkey == 0)
                        {
                            frmSearch.chkKey1.Text = gridSpecCar[0, c].ToString();
                            frmSearch.chkKey1.Checked = true;
                            frmSearch.keyField1 = gridSpecCar.Cols[c].Name;

                            frmSearch.cbKey1.Text = gridSpecCar[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 1)
                        {
                            frmSearch.chkKey2.Text = gridSpecCar[0, c].ToString();
                            frmSearch.chkKey2.Checked = true;
                            frmSearch.keyField2 = gridSpecCar.Cols[c].Name;

                            frmSearch.cbKey2.Text = gridSpecCar[r, c].ToString();
                        }
                        else if (frmSearch.selkey == 2)
                        {
                            frmSearch.chkKey3.Text = gridSpecCar[0, c].ToString();
                            frmSearch.chkKey3.Checked = true;
                            frmSearch.keyField3 = gridSpecCar.Cols[c].Name;

                            frmSearch.cbKey3.Text = gridSpecCar[r, c].ToString();
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

        void gridSpecCar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (writable == false)
                    return;

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

                frmSearch.searchKind = Global.SEARCH_SPECCAR;
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
                if (gridSpecCar.Row < 1)
                    return;
                if (gridSpecCar.Rows.Count < 2)
                    return;

                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                int oldPos = gridSpecCar.Row;

                FrmSpecCarEdit frm = new FrmSpecCarEdit();
                frm.selRowIndex = gridSpecCar.Row - 1;
                if (vwSpeccarBindingSource.Filter.Length > 0)
                {
                    frm.filterText = vwSpeccarBindingSource.Filter + "AND salekind = '大客户'";
                }
                else
                {
                    frm.filterText = "salekind = '大客户'";
                }
                frm.ShowDialog();
                
                if (frm.modified)
                {
                    LoadTableFromDB();
                    gridSpecCar.Row = oldPos;
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