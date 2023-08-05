using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmStoreInAdd : Form
    {
        #region Fields and Properties

        public List<int> onroadcarInList;
        public List<int> onroadcarOutList;
        public bool modified = false;

        #endregion

        #region Constructors

        public FrmStoreInAdd()
        {
            InitializeComponent();

            onroadcarInList = new List<int>();
            onroadcarOutList = new List<int>();

            this.FormClosing += new FormClosingEventHandler(FrmStoreInAdd_FormClosing);

            navStorein.PositionChanged += new EventHandler(navStorein_PositionChanged);
            navStorein.SaveData += new EventHandler(navStorein_SaveData);
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
                    row["changedate"] = DateTime.Now;
                    row["inprice"] = 0.0;
                    row["passno"] = "";
                    row["companyno"] = 100;
                    row["inpath"] = ((DataRowView)(InpathBindingSource[0])).Row["value"];
                    row["intype"] = ((DataRowView)(IntypeBindingSource[0])).Row["value"];
                    row["incarpricekind"] = @"融资车";
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

        #endregion

        #region Event Methods

        private void FrmStoreInAdd_Load(object sender, EventArgs e)
        {
            try
            {
                tblBasedataTableAdapter.Fill(cmsDB.tbl_basedata);
                tblStorechangeTableAdapter.Fill(cmsDB.tbl_storechange);
                //tblStoreinTableAdapter.Fill(cmsDB.tbl_storein);

                MakeStoreinTable();
                
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void FrmStoreInAdd_FormClosing(object sender, FormClosingEventArgs e)
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

        void navStorein_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                tblOnroadTableAdapter.FillByUid(cmsDB.tbl_onroad, onroadcarInList[navStorein.Position]);
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