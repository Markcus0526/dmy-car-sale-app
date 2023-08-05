using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmAppendRepairEdit : Form
    {
        #region Fields and Properties

        public int selRowIndex;
        public String filterText;
        public bool modified = false;

        #endregion

        #region Constructors
        
        public FrmAppendRepairEdit()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(FrmAppendRepairEdit_FormClosing);

            cmsDB.tbl_fit.TableNewRow += new DataTableNewRowEventHandler(tbl_fit_TableNewRow);
            navFit.SaveData += new EventHandler(navFit_SaveData);
            navFit.ConfirmDelete += new CancelEventHandler(navFit_ConfirmDelete);
        }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        private int GetDefaultIdFromCarTypeTable()
        {
            try
            {
                if (cmsDB.tbl_cartype.Rows.Count > 0)
                {
                    return int.Parse(cmsDB.tbl_cartype.Rows[0]["uid"].ToString());
                }

                return 0;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
                return 0;
            }
        }

        private int GetDefaultCarStateFromBasedataTable()
        {
            try
            {
                if (cmsDB.tbl_basedata.Rows.Count > 0)
                {
                    return int.Parse(cmsDB.tbl_basedata.Rows[0]["uid"].ToString());
                }

                return 0;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
                return 0;
            }
        }

        #endregion

        #region Event Methods
                
        private void FrmAppendRepairEdit_Load(object sender, EventArgs e)
        {
            try
            {
                tblFitTableAdapter.Fill(cmsDB.tbl_fit);

                navFit.Position = selRowIndex;

            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmAppendRepairEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                navFit.CurrencyManager.EndCurrentEdit();
                DataTable tblChange = cmsDB.tbl_fit.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        navFit_SaveData(this, e);
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                navFit.Position = 0;
                DataTable tblChange = cmsDB.tbl_onroad.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        navFit_SaveData(this, e);
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void navFit_SaveData(object sender, EventArgs e)
        {
            try
            {
                DataTable tblChange = cmsDB.tbl_fit.GetChanges();

                if (tblChange != null)
                {
                    tblFitTableAdapter.Update(cmsDB.tbl_fit);

                    modified = true;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void tbl_fit_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            try
            {
                e.Row["consumer"] = "";
                e.Row["seller"] = "";
                e.Row["fitdate"] = DateTime.Now;
                e.Row["fitprice"] = 0.0;
                e.Row["remark"] = "";
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void navFit_ConfirmDelete(object sender, CancelEventArgs e)
        {
            try
            {
                String warningText = String.Format("你删除第{0:0}项？", navFit.Position + 1);

                FrmMessage msg = new FrmMessage();
                if (msg.ShowYesNoMsg(warningText, "", @"警告") == DialogResult.No)
                {
                    e.Cancel = true;
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