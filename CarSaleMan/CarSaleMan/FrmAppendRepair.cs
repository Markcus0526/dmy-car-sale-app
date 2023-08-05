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
    public partial class FrmAppendRepair : Form
    {
        #region Fields and Properties

        public static FrmAppendRepair frmAppendRepair;
        public FrmSearch frmSearch;

        private bool writable;

        #endregion

        #region Constructors

        public FrmAppendRepair()
        {
            InitializeComponent();

            // init private variables
            writable = true;

            this.Activated += new EventHandler(FrmAppendRepair_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmAppendRepair_FormClosing);

            // flexgrid events
            this.gridFit.MouseDoubleClick += new MouseEventHandler(gridFit_MouseDoubleClick);
        }
                
        #endregion

        #region Public Methods

        public static FrmAppendRepair GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmAppendRepair == null) //if not created yet, Create an instance
                {
                    frmAppendRepair = new FrmAppendRepair();
                    frmAppendRepair.MdiParent = parent;
                }
            }
            return frmAppendRepair;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void LoadTableFromDB()
        {
            try
            {
                tblFitTableAdapter.Fill(cmsDB.tbl_fit);

                if (cmsDB.tbl_fit.Count > 0)
                {
                    for (int i = 0; i < cmsDB.tbl_fit.Count; i++)
                    {
                        gridFit.Cols[0][i + 1] = i + 1;
                    }
                }

                gridFit.AutoSizeCols();
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

        private void FrmAppendRepair_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmAppendRepair_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmAppendRepair = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmAppendRepair_Activated(object sender, EventArgs e)
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
        void gridFit_MouseDoubleClick(object sender, MouseEventArgs e)
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

        private void tsbChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSearch != null)
                {
                    frmSearch.Close();
                    frmSearch = null;
                }

                int oldPos = gridFit.Row;

                FrmAppendRepairEdit frm = new FrmAppendRepairEdit();
                frm.selRowIndex = gridFit.Row - 1;
                frm.ShowDialog();
                
                if (frm.modified)
                {
                    LoadTableFromDB();
                    gridFit.Row = oldPos;
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