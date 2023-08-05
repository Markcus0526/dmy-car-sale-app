using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmStoreInAddMan : Form
    {
        #region Fields and Properties

        public List<int> onroadcarInList;
        public List<int> onroadcarOutList;
        public bool modified = false;

        private int oldPosition = -1;

        #endregion

        #region Constructors

        public FrmStoreInAddMan()
        {
            InitializeComponent();

            onroadcarInList = new List<int>();
            onroadcarOutList = new List<int>();

            this.FormClosing += new FormClosingEventHandler(FrmStoreInAddMan_FormClosing);

            cmsDB.tbl_storein.TableNewRow += new DataTableNewRowEventHandler(tbl_storein_TableNewRow);
            navStorein.PositionChanged += new EventHandler(navStorein_PositionChanged);
            navStorein.SaveData += new EventHandler(navStorein_SaveData);
            navStorein.BeforeButtonClick += new C1.Win.C1InputPanel.InputNavigatorClickEventHandler(navStorein_BeforeButtonClick);

            cbcartype.SelectedIndexChanged += new EventHandler(cbcartype_SelectedIndexChanged);
        }

        void navStorein_BeforeButtonClick(object sender, C1.Win.C1InputPanel.InputNavigatorClickEventArgs e)
        {
            try
            {
                oldPosition = navStorein.Position;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        private void MakeStoreinTable()
        {
            try
            {
                for (int i = 0; i < onroadcarInList.Count; i++)
                {
                    DataRow row = cmsDB.tbl_storein.NewRow();

                    row["batchno"] = "";
                    row["storeplace"] = ((DataRowView)(StoreplaceBindingSource[0])).Row["value"];
                    row["onroadid"] = onroadcarInList[i];
                    row["indate"] = DateTime.Now;
                    row["inprice"] = 0.0;
                    row["passno"] = "";
                    row["companyno"] = 0;
                    row["inpath"] = ((DataRowView)(InpathBindingSource[0])).Row["value"];
                    row["intype"] = ((DataRowView)(IntypeBindingSource[0])).Row["value"];
                    row["incarpricekind"] = "";
                    row["factoryoutdate"] = DateTime.Now;
                    row["repairstate"] = "";
                    row["reservestate"] = ((DataRowView)(ReservestateBindingSource[0])).Row["value"];
                    row["propval"] = 0.0;
                    row["profitprop"] = 0.0;
                    row["profitval"] = 0.0;
                    row["specprofitval"] = 0.0;
                    row["profitstate"] = ((DataRowView)(ProfitstateBindingSource[0])).Row["value"];
                    row["outstoreprice"] = 0.0;
                    row["settlementname"] = ((DataRowView)(SettlementnameBindingSource[0])).Row["value"];
                    row["handlername"] = ((DataRowView)(HandlernameBindingSource[0])).Row["value"];
                    row["remark"] = "";
                    row["outflag"] = 0;

                    cmsDB.tbl_storein.Rows.Add(row);
                }

                dtptransdate.Value = DateTime.Now;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

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

        private void FrmStoreInAddMan_Load(object sender, EventArgs e)
        {
            try
            {
                tblBasedataTableAdapter.Fill(cmsDB.tbl_basedata);
                tblCartypeTableAdapter.Fill(cmsDB.tbl_cartype);
                tblStorechangeTableAdapter.Fill(cmsDB.tbl_storechange);

                MakeStoreinTable();
                
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void FrmStoreInAddMan_FormClosing(object sender, FormClosingEventArgs e)
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

        void tbl_storein_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            try
            {
                // add new row of onroad table, but don't determine uid of onroad table
                DataRow row = cmsDB.tbl_onroad.NewRow();

                row["billno"] = "000000";
                row["billdate"] = DateTime.Now;
                row["vin"] = "00000000000000000";

                int typeid = GetDefaultIdFromCarTypeTable();
                if (typeid > 0)
                {
                    DataRow row1 = cmsDB.tbl_cartype.Rows[0];
                    row["cartypeid"] = row1["uid"];
                    row["cartype"] = row1["carcode"];
                    row["carname"] = row1["carname"];
                    row["insidesetcode"] = row1["insidesetcode"];
                    row["insidesetname"] = row1["insidesetname"];
                    row["subsets"] = row1["subsets"];
                    row["inprice"] = row1["inprice"];
                }

                int baseid = GetDefaultCarStateFromBasedataTable();
                if (baseid > 0)
                {
                    DataRow row2 = cmsDB.tbl_basedata.Rows[0];
                    row["carstate"] = row2["value"];
                }

                row["engineno"] = "";
                row["inflag"] = 1;
                row["inkind"] = 1;

                cmsDB.tbl_onroad.Rows.Add(row);
                tblOnroadTableAdapter.Update(cmsDB.tbl_onroad);
                tblOnroadTableAdapter.FillByLastest(cmsDB.tbl_onroad);
                int onroadid  = Convert.ToInt32(cmsDB.tbl_onroad[0]["uid"]);

                // add new row of storein table
                e.Row["batchno"] = "";
                e.Row["storeplace"] = ((DataRowView)(StoreplaceBindingSource[0])).Row["value"];
                e.Row["onroadid"] = onroadid;
                e.Row["indate"] = DateTime.Now;
                e.Row["changedate"] = DateTime.Now;
                e.Row["inprice"] = 0.0;
                e.Row["passno"] = "";
                e.Row["companyno"] = 100;
                e.Row["inpath"] = ((DataRowView)(InpathBindingSource[0])).Row["value"];
                e.Row["intype"] = ((DataRowView)(IntypeBindingSource[0])).Row["value"];
                e.Row["incarpricekind"] = @"融资车";
                e.Row["factoryoutdate"] = DateTime.Now;
                e.Row["repairstate"] = "";
                e.Row["reservestate"] = ((DataRowView)(ReservestateBindingSource[0])).Row["value"];
                e.Row["propval"] = 0.0;
                e.Row["profitprop"] = 0.0;
                e.Row["profitval"] = 0.0;
                e.Row["specprofitval"] = 0.0;
                e.Row["profitstate"] = ((DataRowView)(ProfitstateBindingSource[0])).Row["value"];
                e.Row["outstoreprice"] = 0.0;
                e.Row["settlementname"] = ((DataRowView)(SettlementnameBindingSource[0])).Row["value"];
                e.Row["handlername"] = ((DataRowView)(HandlernameBindingSource[0])).Row["value"];
                e.Row["remark"] = "";
                e.Row["outflag"] = 0;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void navStorein_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                if (oldPosition > -1 && navStorein.Position != oldPosition && cmsDB.tbl_storein.Rows.Count > oldPosition)
                {
                    DataRow[] rows = cmsDB.tbl_onroad.Select("uid = " + cmsDB.tbl_storein.Rows[oldPosition]["onroadid"].ToString());

                    rows[0]["billno"] = txtbillno.Text;
                    rows[0]["billdate"] = dtpbilldate.Value;
                    rows[0]["vin"] = txtvin.Text;
                    rows[0]["engineno"] = txtengineno.Text;
                    rows[0]["cartype"] = cbcartype.Text;
                    rows[0]["carname"] = txtcartypename.Text;
                    rows[0]["colorcode"] = txtcolorcode.Text;
                    rows[0]["colorname"] = txtcolorname.Text;
                    rows[0]["insidesetcode"] = txtinsidesetcode.Text;
                    rows[0]["insidesetname"] = txtinsidesetname.Text;
                    rows[0]["subsets"] = txtsubsets.Text;
                    rows[0]["carstate"] = cbcarstate.Text;
                    rows[0]["property"] = txtproperty.Text;

                    tblOnroadTableAdapter.Update(rows);
                }

                if (navStorein.Position >= 0 && cmsDB.tbl_storein.Rows.Count > navStorein.Position)
                {
                    DataRow[] rows = cmsDB.tbl_onroad.Select("uid = " + cmsDB.tbl_storein.Rows[navStorein.Position]["onroadid"].ToString());

                    if (rows != null && rows.Length > 0)
                    {
                        txtbillno.Text = rows[0]["billno"].ToString();
                        dtpbilldate.Value = Convert.ToDateTime(rows[0]["billdate"].ToString());
                        txtvin.Text = rows[0]["vin"].ToString();
                        txtengineno.Text = rows[0]["engineno"].ToString();
                        cbcartype.Text = rows[0]["cartype"].ToString();
                        txtcartypename.Text = rows[0]["carname"].ToString();
                        txtcolorcode.Text = rows[0]["colorcode"].ToString();
                        txtcolorname.Text = rows[0]["colorname"].ToString();
                        txtinsidesetcode.Text = rows[0]["insidesetcode"].ToString();
                        txtinsidesetname.Text = rows[0]["insidesetname"].ToString();
                        txtsubsets.Text = rows[0]["subsets"].ToString();
                        cbcarstate.Text = rows[0]["carstate"].ToString();
                        txtproperty.Text = rows[0]["property"].ToString();
                    }
                }

                oldPosition = navStorein.Position;
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
                FrmMessage msg = new FrmMessage();
                String errText = "";
                bool deleted = false;

                tblOnroadTableAdapter.Update(cmsDB.tbl_onroad);

                for (int i = 0; i < cmsDB.tbl_storein.Rows.Count; i++)
                {
                    DataRow row = cmsDB.tbl_storein.Rows[i];

                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        DataRowState rowState = row.RowState;

                        if (row["batchno"].ToString().Length == 0)
                        {
                            errText = String.Format("{0:0}的入库单号价空了!.", (i + 1).ToString());
                            msg.ShowCloseMsg(errText, "", @"警告");
                            break;
                        }

                        tblStoreinTableAdapter.Update(row);
                        row.AcceptChanges();

                        // save to storechange
                        if (rowState == DataRowState.Modified)
                        {
                            DataRow[] rows = cmsDB.tbl_storechange.Select("onroadid = " + row["onroadid"].ToString() + " AND actionkind = '" + Global.CAR_STOREIN + "'");
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
                        }
                        else
                        {
                            DataRow row2 = cmsDB.tbl_storechange.NewRow();
                            row2["changeid"] = cmsDB.tbl_storechange.Rows.Count;
                            row2["batchno"] = row["batchno"].ToString();
                            row2["onroadid"] = Convert.ToInt32(row["onroadid"].ToString());
                            row2["storeplace"] = row["storeplace"].ToString();
                            row2["actionkind"] = Global.CAR_STOREIN;
                            row2["actiondate"] = DateTime.Now;
                            row2["actionpay"] = 0;
                            row2["settlementname"] = "";
                            row2["handlername"] = Global.LOGIN_USERNAME;
                            row2["repairstate"] = "";
                            row2["reservestate"] = "";
                            row2["remark"] = row["remark"].ToString();

                            cmsDB.tbl_storechange.Rows.Add(row2);

                            tblStorechangeTableAdapter.Update(row2);
                        }

                        int onroadid = Convert.ToInt32(row["onroadid"].ToString());
                        if (onroadcarOutList.Contains(onroadid) == false)
                        {
                            onroadcarOutList.Add(onroadid);
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
                    tblStoreinTableAdapter.Update(cmsDB.tbl_storein);
                    cmsDB.tbl_storein.AcceptChanges();
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void cbcartype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (navStorein.Position >= 0)
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