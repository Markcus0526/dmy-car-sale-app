using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmSpecCarEdit : Form
    {
        #region Fields and Properties

        public int selRowIndex;
        public String filterText;
        public bool modified = false;

        #endregion

        #region Constructors

        public FrmSpecCarEdit()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(FrmSpecCarEdit_FormClosing);

            navStoreout.SaveData += new EventHandler(navStoreout_SaveData);

        }
        
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Methods

        private void FrmSpecCarEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.tblStoreoutTableAdapter.Fill(this.cmsDB.tbl_storeout);

                tblStoreoutBindingSource.Filter = filterText;
                navStoreout.Position = selRowIndex;
                
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void FrmSpecCarEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                navStoreout.CurrencyManager.EndCurrentEdit();
                DataTable tblChange = cmsDB.tbl_storeout.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        navStoreout_SaveData(this, e);
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void navStoreout_SaveData(object sender, EventArgs e)
        {
            try
            {
                DataTable tblChange = cmsDB.tbl_storeout.GetChanges();

                if (tblChange != null)
                {
                    tblStoreoutTableAdapter.Update(cmsDB.tbl_storeout);
                    cmsDB.tbl_storeout.AcceptChanges();
                    modified = true;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void btnbatchno_Click(object sender, EventArgs e)
        {
            try
            {
                String id;
                id = "Z" + DateTime.Now.ToString("yyyyMMddhhmmss");
                txtbatchno.Text = id;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                navStoreout.Position = 0;
                DataTable tblChange = cmsDB.tbl_storeout.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        navStoreout_SaveData(this, e);
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

    }
}