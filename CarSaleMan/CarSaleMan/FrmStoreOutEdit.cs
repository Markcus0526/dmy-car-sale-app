using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmStoreOutEdit : Form
    {
        #region Fields and Properties

        public int selRowIndex;
        public String filterText;
        public bool modified = false;

        #endregion

        #region Constructors

        public FrmStoreOutEdit()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(FrmStoreOutEdit_FormClosing);

            navStoreout.SaveData += new EventHandler(navStoreout_SaveData);

            cbsalekind.SelectedIndexChanged += new EventHandler(cbsalekind_SelectedIndexChanged);
        }

        
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Methods

        private void FrmStoreOutEdit_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cmsDB.tbl_storechange' table. You can move, or remove it, as needed.
            this.tblStorechangeTableAdapter.Fill(this.cmsDB.tbl_storechange);
            try
            {
                tblBasedataTableAdapter.Fill(cmsDB.tbl_basedata);
                tblStoreoutTableAdapter.Fill(cmsDB.tbl_storeout);

                tblStoreoutBindingSource.Filter = filterText;
                navStoreout.Position = selRowIndex;
                
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void FrmStoreOutEdit_FormClosing(object sender, FormClosingEventArgs e)
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
                for (int i = 0; i < cmsDB.tbl_storeout.Rows.Count; i++)
                {
                    DataRow row = cmsDB.tbl_storeout.Rows[i];

                    if (row.RowState == DataRowState.Modified)
                    {
                        tblStoreoutTableAdapter.Update(row);
                        row.AcceptChanges();

                        // save to storechange
                        DataRow[] rows = cmsDB.tbl_storechange.Select("onroadid = " + row["onroadid"] + " AND actionkind = '" + Global.CAR_STOREOUT + "'");
                        if (rows != null && rows.Length > 0)
                        {
                            rows[0]["batchno"] = row["batchno"].ToString();
                            rows[0]["storeplace"] = "";
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
                id = "Z" + DateTime.Now.ToString("yyyyMMddhhmmss");
                txtbatchno.Text = id;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void cbsalekind_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbsalekind.SelectedValue.ToString().Equals("大客户") == true)
                    cbcarspeckind.Enabled = true;
                else
                    cbcarspeckind.Enabled = false;
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