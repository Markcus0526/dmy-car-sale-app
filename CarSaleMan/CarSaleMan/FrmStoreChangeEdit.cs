using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmStoreChangeEdit : Form
    {
        #region Fields and Properties

        public int selRowIndex;
        public String filterText;
        public bool modified = false;

        private List<String> oldStoreplaceList;

        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        public FrmStoreChangeEdit()
        {
            InitializeComponent();

            oldStoreplaceList = new List<String>();

            this.FormClosing += new FormClosingEventHandler(FrmStoreChangeEdit_FormClosing);

            navStorein.PositionChanged += new EventHandler(navStorein_PositionChanged);
            navStorein.SaveData += new EventHandler(navStorein_SaveData);
            navStorein.ConfirmDelete += new CancelEventHandler(navStorein_ConfirmDelete);

        }

        
              
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

        private void FrmStoreChangeEdit_Load(object sender, EventArgs e)
        {
            try
            {
                tblBasedataTableAdapter.Fill(cmsDB.tbl_basedata);
                tblOnroadTableAdapter.Fill(cmsDB.tbl_onroad);
                tblStoreinTableAdapter.FillByStorein(cmsDB.tbl_storein);
                tblStorechangeTableAdapter.Fill(cmsDB.tbl_storechange);

                tblStoreinBindingSource.Filter = filterText;

                for (int i = 0; i < tblStoreinBindingSource.Count; i++)
                {
                    oldStoreplaceList.Add((((DataRowView)(tblStoreinBindingSource[i])).Row["storeplace"]).ToString());
                }
                 
                navStorein.Position = selRowIndex;

            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmStoreChangeEdit_FormClosing(object sender, FormClosingEventArgs e)
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
                CommonMisc.LogErrors(ex.ToString());
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
                navStorein.Position = 0;
                DataTable tblChange = cmsDB.tbl_storein.GetChanges();

                if (tblChange != null)
                {
                    for (int i = 0; i < cmsDB.tbl_storein.Count; i++)
                    {
                        DataRow row = cmsDB.tbl_storein.Rows[i];

                        if (row.RowState == DataRowState.Modified && row["batchno"].ToString().Substring(0, 1).Equals("Z") == false)
                        {
                            String id;
                            id = "Z" + DateTime.Now.ToString("yyyyMMddhhmmss");
                            row["batchno"] = id;
                        }
                    }

                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        navStorein_SaveData(this, e);
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void navStorein_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                tblOnroadBindingSource.Filter = "uid = " + Convert.ToInt32((((DataRowView)(tblStoreinBindingSource[navStorein.Position])).Row["onroadid"]).ToString());

                txtcartype.Text = (((DataRowView)(tblOnroadBindingSource[0])).Row["cartype"]).ToString();
                txtcolorname.Text = (((DataRowView)(tblOnroadBindingSource[0])).Row["colorname"]).ToString();
                txtengineno.Text = (((DataRowView)(tblOnroadBindingSource[0])).Row["engineno"]).ToString();
                txtvin.Text = (((DataRowView)(tblOnroadBindingSource[0])).Row["vin"]).ToString();
                txtoldstoreplace.Text = oldStoreplaceList[navStorein.Position];
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
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
                        DataRow row2 = cmsDB.tbl_storechange.NewRow();
                        row2["changeid"] = cmsDB.tbl_storechange.Rows.Count;
                        row2["batchno"] = row["batchno"].ToString();
                        row2["onroadid"] = Convert.ToInt32(row["onroadid"].ToString());
                        row2["storeplace"] = row["storeplace"].ToString();
                        row2["actionkind"] = Global.CAR_STORECHANGE;
                        row2["actiondate"] = DateTime.Now;
                        row2["actionpay"] = 0;
                        row2["settlementname"] = "";
                        row2["handlername"] = Global.LOGIN_USERNAME;
                        row2["repairstate"] = "";
                        row2["reservestate"] = "";
                        row2["remark"] = row["remark"].ToString();

                        cmsDB.tbl_storechange.Rows.Add(row2);

                        tblStorechangeTableAdapter.Update(row2);

                        modified = true;
                    }
                }

                oldStoreplaceList.Clear();
                for (int i = 0; i < tblStoreinBindingSource.Count; i++)
                {
                    oldStoreplaceList.Add((((DataRowView)(tblStoreinBindingSource[i])).Row["storeplace"]).ToString());
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void navStorein_ConfirmDelete(object sender, CancelEventArgs e)
        {
            try
            {
                String warningText = String.Format("你删除第{0:0}项？", navStorein.Position + 1);

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