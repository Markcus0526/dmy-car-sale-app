using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmStoreInEdit : Form
    {
        #region Fields and Properties

        public int selRowIndex;
        public String filterText;
        public bool modified = false;

        #endregion

        #region Constructors

        public FrmStoreInEdit()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(FrmStoreInEdit_FormClosing);

            navStorein.SaveData += new EventHandler(navStorein_SaveData);
        }
        
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Methods

        private void FrmStoreInEdit_Load(object sender, EventArgs e)
        {
            try
            {
                tblBasedataTableAdapter.Fill(cmsDB.tbl_basedata);
                tblStoreinTableAdapter.FillByStorein(cmsDB.tbl_storein);
                tblStorechangeTableAdapter.Fill(cmsDB.tbl_storechange);

                tblStoreinBindingSource.Filter = filterText;
                navStorein.Position = selRowIndex;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void FrmStoreInEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                navStorein.CurrencyManager.EndCurrentEdit();
                DataTable tblChange = cmsDB.tbl_storein.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        navStorein_SaveData(this, e);
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void navStorein_SaveData(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < cmsDB.tbl_storein.Rows.Count; i++)
                {
                    DataRow row = cmsDB.tbl_storein.Rows[i];

                    if (row.RowState == DataRowState.Modified)
                    {
                        tblStoreinTableAdapter.Update(row);
                        row.AcceptChanges();

                        // save to storechange
                        DataRow[] rows = cmsDB.tbl_storechange.Select("onroadid = " + row["onroadid"] + " AND actionkind = '" + Global.CAR_STOREIN + "'");
                        if (rows != null && rows.Length > 0)
                        {
                            rows[0]["batchno"] = row["batchno"].ToString();
                            rows[0]["storeplace"] = row["storeplace"].ToString();
                            rows[0]["actiondate"] = DateTime.Now;
                            rows[0]["actionpay"] = 0;
                            rows[0]["settlementname"] = "";
                            rows[0]["handlername"] = Global.LOGIN_USERNAME;
                            rows[0]["repairstate"] = "";
                            rows[0]["reservestate"] = "";
                            rows[0]["remark"] = row["remark"].ToString();

                            tblStorechangeTableAdapter.Update(rows[0]);
                        }

                        modified = true;
                    }
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
                id = "R" + DateTime.Now.ToString("yyyyMMddhhmmss");
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
                navStorein.Position = 0;
                DataTable tblChange = cmsDB.tbl_storein.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        navStorein_SaveData(this, e);
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