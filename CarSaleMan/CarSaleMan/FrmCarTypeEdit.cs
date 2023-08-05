using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmCarTypeEdit : Form
    {
        #region Fields and Properties

        public int selRowIndex;
        public String filterText;
        public bool modified = false;

        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        public FrmCarTypeEdit()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(FrmCarTypeEdit_FormClosing);

            cmsDB.tbl_cartype.TableNewRow += new DataTableNewRowEventHandler(tbl_cartype_TableNewRow);
            navCartype.SaveData += new EventHandler(navCartype_SaveData);
            navCartype.ConfirmDelete += new CancelEventHandler(navCartype_ConfirmDelete);
        }

              
        #endregion

        #region Private Methods

        #endregion

        #region Event Methods

        private void FrmCarTypeEdit_Load(object sender, EventArgs e)
        {
            try
            {
                tblCartypeTableAdapter.Fill(this.cmsDB.tbl_cartype);

                tblCartypeBindingSource.Filter = filterText;
                navCartype.Position = selRowIndex;

            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmCarTypeEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                navCartype.CurrencyManager.EndCurrentEdit();
                DataTable tblChange = cmsDB.tbl_cartype.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        navCartype_SaveData(this, e);
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
                navCartype.Position = 0;
                DataTable tblChange = cmsDB.tbl_cartype.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        navCartype_SaveData(this, e);
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void navCartype_SaveData(object sender, EventArgs e)
        {
            try
            {
                FrmMessage msg = new FrmMessage();
                String errText = "";
                bool deleted = false;

                for (int i = 0; i < cmsDB.tbl_cartype.Rows.Count; i++)
                {
                    DataRow row = cmsDB.tbl_cartype.Rows[i];

                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        if (row["carseries"].ToString().Length == 0)
                        {
                            errText = String.Format("{0:0}的大类型空了。", (i + 1).ToString());
                            msg.ShowCloseMsg(errText, "", @"警告");
                            break;
                        }

                        if (row["carcode"].ToString().Length == 0)
                        {
                            errText = String.Format("{0:0}的车辆代码空了。", (i + 1).ToString());
                            msg.ShowCloseMsg(errText, "", @"警告");
                            break;
                        }

                        if (row["carname"].ToString().Length == 0)
                        {
                            errText = String.Format("{0:0}的车辆名称空了。", (i + 1).ToString());
                            msg.ShowCloseMsg(errText, "", @"警告");
                            break;
                        }

                        tblCartypeTableAdapter.Update(row);
                        row.AcceptChanges();

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
                    tblCartypeTableAdapter.Update(cmsDB.tbl_cartype);
                    cmsDB.tbl_cartype.AcceptChanges();
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void tbl_cartype_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            try
            {
                e.Row["carseries"] = "";
                e.Row["carcode"] = "";
                e.Row["carname"] = "";
                e.Row["eop"] = "否";
                e.Row["subsets"] = "";
                e.Row["insidesetcode"] = "";
                e.Row["insidesetname"] = "";
                e.Row["inprice"] = 0.0;
                e.Row["outprice"] = 0.0;
                e.Row["otherprice1"] = 0.0;
                e.Row["otherprice2"] = 0.0;
                e.Row["otherprice3"] = 0.0;
                e.Row["otherprice4"] = 0.0;
                e.Row["propval"] = 0.0;
                e.Row["profitval"] = 1.17;
                e.Row["outstoreprice"] = 0.0;
                e.Row["vinprefix"] = "";
                e.Row["enginenoprefix"] = "";
                e.Row["deleted"] = 0;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void navCartype_ConfirmDelete(object sender, CancelEventArgs e)
        {
            try
            {
                String warningText = String.Format("你删除第{0:0}项？", navCartype.Position + 1);

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