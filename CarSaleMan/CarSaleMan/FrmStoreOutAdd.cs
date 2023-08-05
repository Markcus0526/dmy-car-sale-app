using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmStoreOutAdd : Form
    {
        #region Fields and Properties

        public List<int> onroadcarInList;
        public List<int> onroadcarOutList;
        public bool modified = false;

        #endregion

        #region Constructors

        public FrmStoreOutAdd()
        {
            InitializeComponent();

            onroadcarInList = new List<int>();
            onroadcarOutList = new List<int>();

            this.FormClosing += new FormClosingEventHandler(FrmStoreOutAdd_FormClosing);

            navStoreout.PositionChanged += new EventHandler(navStoreout_PositionChanged);
            navStoreout.SaveData += new EventHandler(navStoreout_SaveData);

            cbsalekind.SelectedIndexChanged += new EventHandler(cbsalekind_SelectedIndexChanged);
        }

        
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        private void MakeStoreoutTable()
        {
            try
            {
                for (int i = 0; i < onroadcarInList.Count; i++)
                {
                    DataRow row = cmsDB.tbl_storeout.NewRow();

                    row["batchno"] = "";
                    row["onroadid"] = onroadcarInList[i];
                    row["salecompany"] = "";
                    row["outbillno"] = "";
                    row["salekind"] = ((DataRowView)(SalekindBindingSource[0])).Row["value"];
                    row["settlementname"] = ((DataRowView)(SettlementnameBindingSource[0])).Row["value"];
                    row["handlername"] = ((DataRowView)(HandlernameBindingSource[0])).Row["value"];
                    row["saleplace"] = ((DataRowView)(StoreplaceBindingSource[0])).Row["value"];
                    row["incarkind"] = "";
                    row["outdate"] = DateTime.Now;
                    row["customername"] = "";
                    row["customerphoneno"] = "";
                    row["customerjobkind"] = ((DataRowView)(JobkindBindingSource[0])).Row["value"];
                    row["saleregion"] = ((DataRowView)(RegionBindingSource[0])).Row["value"];
                    row["customeraddress"] = "";
                    row["carno"] = "";
                    row["carspeckind"] = ((DataRowView)(CarspeckindBindingSource[0])).Row["value"];
                    row["isreport"] = ((DataRowView)(IsreportBindingSource[0])).Row["value"];
                    row["votecost"] = 0.0;
                    row["inprice"] = 0.0;
                    row["otherprice1"] = 0.0;
                    row["otherprice2"] = 0.0;
                    row["otherprice3"] = 0.0;
                    row["otherprice4"] = 0.0;
                    row["interestprice"] = 0.0;
                    row["profitval"] = 0.0;
                    row["specprofitval"] = 0.0;
                    row["outprice"] = 0.0;
                    row["pricediff"] = 0.0;
                    row["isbill"] = ((DataRowView)(IsbillBindingSource[0])).Row["value"];
                    row["billoutdate"] = DateTime.Now;
                    row["ispayment"] = ((DataRowView)(IspayBindingSource[0])).Row["value"];
                    row["paymentdate"] = DateTime.Now;
                    row["issend"] = ((DataRowView)(IssendBindingSource[0])).Row["value"];
                    row["senddate"] = DateTime.Now;
                    row["remark"] = "";

                    cmsDB.tbl_storeout.Rows.Add(row);
                }

            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

        #region Event Methods

        private void FrmStoreOutAdd_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cmsDB.tbl_storechange' table. You can move, or remove it, as needed.
            this.tblStorechangeTableAdapter.Fill(this.cmsDB.tbl_storechange);
            try
            {
                tblBasedataTableAdapter.Fill(cmsDB.tbl_basedata);

                MakeStoreoutTable();
                
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void FrmStoreOutAdd_FormClosing(object sender, FormClosingEventArgs e)
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

        void navStoreout_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                tblOnroadTableAdapter.FillByUid(cmsDB.tbl_onroad, onroadcarInList[navStoreout.Position]);
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
                FrmMessage msg = new FrmMessage();
                String errText = "";
                bool deleted = false;

                for (int i = 0; i < cmsDB.tbl_storeout.Rows.Count; i++)
                {
                    DataRow row = cmsDB.tbl_storeout.Rows[i];

                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        DataRowState rowState = row.RowState;

                        if (row["batchno"].ToString().Length == 0)
                        {
                            errText = String.Format("{0:0}的入库单号价空了!.", (i + 1).ToString());
                            msg.ShowCloseMsg(errText, "", @"警告");
                            break;
                        }

                        tblStoreoutTableAdapter.Update(row);
                        row.AcceptChanges();

                        // save to storechange
                        if (rowState == DataRowState.Modified)
                        {
                            DataRow[] rows = cmsDB.tbl_storechange.Select("onroadid = " + row["onroadid"].ToString() + " AND actionkind = '" + Global.CAR_STOREOUT + "'");
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
                        }
                        else
                        {
                            DataRow row2 = cmsDB.tbl_storechange.NewRow();
                            row2["changeid"] = cmsDB.tbl_storechange.Rows.Count;
                            row2["batchno"] = row["batchno"].ToString();
                            row2["onroadid"] = Convert.ToInt32(row["onroadid"].ToString());
                            row2["storeplace"] = "";
                            row2["actionkind"] = Global.CAR_STOREOUT;
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
                    tblStoreoutTableAdapter.Update(cmsDB.tbl_storeout);
                    cmsDB.tbl_storeout.AcceptChanges();
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