using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmOnRoadCarEdit : Form
    {
        #region Fields and Properties

        public int selRowIndex;
        public String filterText;
        public bool modified = false;

        #endregion

        #region Constructors
        
        public FrmOnRoadCarEdit()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(FrmOnRoadCarEdit_FormClosing);

            cmsDB.tbl_onroad.TableNewRow += new DataTableNewRowEventHandler(tbl_onroad_TableNewRow);
            navOnroad.SaveData += new EventHandler(navOnroad_SaveData);
            navOnroad.ConfirmDelete += new CancelEventHandler(navOnroad_ConfirmDelete);

            cbcartype.SelectedIndexChanged += new EventHandler(cbcartype_SelectedIndexChanged);
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
                
        private void FrmOnRoadCarEdit_Load(object sender, EventArgs e)
        {
            try
            {
                tblBasedataTableAdapter.Fill(cmsDB.tbl_basedata);
                tblCartypeTableAdapter.Fill(cmsDB.tbl_cartype);
                tblStorechangeTableAdapter.Fill(cmsDB.tbl_storechange);

                tblOnroadTableAdapter.FillByOnroad(cmsDB.tbl_onroad);

                tblOnroadBindingSource.Filter = filterText;
                navOnroad.Position = selRowIndex;

            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmOnRoadCarEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                navOnroad.CurrencyManager.EndCurrentEdit();
                DataTable tblChange = cmsDB.tbl_onroad.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        navOnroad_SaveData(this, e);
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
                navOnroad.Position = 0;
                DataTable tblChange = cmsDB.tbl_onroad.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        navOnroad_SaveData(this, e);
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void navOnroad_SaveData(object sender, EventArgs e)
        {
            try
            {
                FrmMessage msg = new FrmMessage();
                String errText = "";
                bool deleted = false;

                for (int i = 0; i < cmsDB.tbl_onroad.Rows.Count; i++)
                {
                    DataRow row = cmsDB.tbl_onroad.Rows[i];

                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        DataRowState rowState = row.RowState;

                        DataRow[] rows = cmsDB.tbl_onroad.Select("vin = '" + row["vin"].ToString() + "'");
                        if (rows != null && rows.Length > 1)
                        {
                            errText = String.Format("{0:0}的VIN码价重复了!.", (i + 1).ToString());
                            msg.ShowCloseMsg(errText, "", @"警告");
                            break;
                        }

                        tblOnroadTableAdapter.Update(row);
                        row.AcceptChanges();

                        // save to storechange
                        if (rowState == DataRowState.Modified)
                        {
                            rows = cmsDB.tbl_storechange.Select("onroadid = " + row["uid"].ToString() + " AND actionkind = '" + Global.CAR_PURCHASE + "'");
                            if (rows != null && rows.Length > 0)
                            {
                                rows[0]["batchno"] = row["billno"].ToString();
                                rows[0]["storeplace"] = "";
                                rows[0]["actiondate"] = DateTime.Now;
                                rows[0]["actionpay"] = 0;
                                rows[0]["settlementname"] = "";
                                rows[0]["handlername"] = Global.LOGIN_USERNAME;
                                rows[0]["repairstate"] = "";
                                rows[0]["reservestate"] = "";
                                rows[0]["remark"] = "";

                                tblStorechangeTableAdapter.Update(rows[0]);
                            }
                        }
                        else
                        {
                            DataRow row2 = cmsDB.tbl_storechange.NewRow();
                            row2["changeid"] = cmsDB.tbl_storechange.Rows.Count;
                            row2["batchno"] = row["billno"].ToString();
                            row2["onroadid"] = Convert.ToInt32(row["uid"].ToString());
                            row2["storeplace"] = "";
                            row2["actionkind"] = Global.CAR_PURCHASE;
                            row2["actiondate"] = DateTime.Now;
                            row2["actionpay"] = 0;
                            row2["settlementname"] = "";
                            row2["handlername"] = Global.LOGIN_USERNAME;
                            row2["repairstate"] = "";
                            row2["reservestate"] = "";
                            row2["remark"] = "";

                            cmsDB.tbl_storechange.Rows.Add(row2);

                            tblStorechangeTableAdapter.Update(row2);
                        }

                        modified = true;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        deleted = true;
                        modified = true;
                    }
                }

                if (deleted == true)
                {
                    tblOnroadTableAdapter.Update(cmsDB.tbl_onroad);
                    cmsDB.tbl_onroad.AcceptChanges();
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void tbl_onroad_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            try
            {
                e.Row["billno"] = "000000";
                e.Row["billdate"] = DateTime.Now;
                e.Row["vin"] = "00000000000000000";

                int typeid = GetDefaultIdFromCarTypeTable();
                if (typeid > 0)
                {
                    DataRow row = cmsDB.tbl_cartype.Rows[0];
                    e.Row["cartypeid"] = row["uid"];
                    e.Row["cartype"] = row["carcode"];
                    e.Row["carname"] = row["carname"];
                    e.Row["insidesetcode"] = row["insidesetcode"];
                    e.Row["insidesetname"] = row["insidesetname"];
                    e.Row["subsets"] = row["subsets"];
                    e.Row["inprice"] = row["inprice"];
                }

                int baseid = GetDefaultCarStateFromBasedataTable();
                if (baseid > 0)
                {
                    DataRow row = cmsDB.tbl_basedata.Rows[0];
                    e.Row["carstate"] = row["value"];
                }
                
                e.Row["engineno"] = "";
                e.Row["inflag"] = 0;
                e.Row["inkind"] = 1;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void navOnroad_ConfirmDelete(object sender, CancelEventArgs e)
        {
            try
            {
                String warningText = String.Format("你删除第{0:0}项？", navOnroad.Position + 1);

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

        void cbcartype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (navOnroad.Position >= 0)
                {
                    DataRow[] rows = cmsDB.tbl_cartype.Select("carcode = '" + cbcartype.Text + "'");
                    txtcartypename.Text = rows[0]["carname"].ToString();
                    txtinsidesetcode.Text = rows[0]["insidesetcode"].ToString();
                    txtinsidesetname.Text = rows[0]["insidesetname"].ToString();
                    txtsubsets.Text = rows[0]["subsets"].ToString();
                    numinprice.Value = Convert.ToDecimal(rows[0]["inprice"].ToString());
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