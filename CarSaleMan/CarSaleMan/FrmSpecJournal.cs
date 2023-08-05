using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CarSaleMan
{
    public partial class FrmSpecJournal : Form
    {
        #region Fields and Properties

        public static FrmSpecJournal frmSpecJournal;

        private bool writable;

        #endregion

        #region Constructors

        public FrmSpecJournal()
        {
            InitializeComponent();

            // init private variables
            writable = false;

            // initialize event handler
            this.FormClosing += new FormClosingEventHandler(FrmSpecJournal_FormClosing);

            cmsDB.tbl_log.TableNewRow += new DataTableNewRowEventHandler(tbl_log_TableNewRow);
        }

        #endregion

        #region Public Methods

        public static FrmSpecJournal GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmSpecJournal == null) //if not created yet, Create an instance
                {
                    frmSpecJournal = new FrmSpecJournal();
                    frmSpecJournal.MdiParent = parent;
                }
            }
            return frmSpecJournal;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void SetReadWriteProperty(bool writable)
        {
            try
            {
                if (writable)
                {
                    tsbSaveValue.Enabled = true;
                    tsbRejectValue.Enabled = true;
                    gridValue.Enabled = true;
                }
                else
                {
                    tsbSaveValue.Enabled = false;
                    tsbRejectValue.Enabled = false;
                    gridValue.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

        #region Event Methods

        private void FrmSpecJournal_Load(object sender, EventArgs e)
        {
            try
            {
                tblLogTableAdapter.Fill(cmsDB.tbl_log);

                writable = Permission.GetFuncPermission(this.Text);
                SetReadWriteProperty(writable);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void FrmSpecJournal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmSpecJournal = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbSaveValue_Click(object sender, EventArgs e)
        {
            try
            {
                tblLogTableAdapter.Update(cmsDB.tbl_log);
                cmsDB.tbl_log.AcceptChanges();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbRejectValue_Click(object sender, EventArgs e)
        {
            try
            {
                cmsDB.tbl_log.RejectChanges();
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
                DataTable tblChange = cmsDB.tbl_log.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg(@"你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        tblLogTableAdapter.Update(cmsDB.tbl_log);
                        cmsDB.tbl_log.AcceptChanges();
                    }
                    else
                    {
                        cmsDB.tbl_log.RejectChanges();
                    }
                }
                this.Close();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        // tbl_log event handler
        void tbl_log_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            try
            {
                e.Row["logdate"] = DateTime.Now;
                e.Row["title"] = "";
                e.Row["cont"] = "";
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
            
        }

        #endregion
    }
}