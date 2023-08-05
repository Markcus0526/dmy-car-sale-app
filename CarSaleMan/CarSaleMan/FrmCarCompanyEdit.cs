using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmCarCompanyEdit : Form
    {
        #region Fields and Properties

        public int selRowIndex;
        public String filterText;
        public bool modified = false;

        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        public FrmCarCompanyEdit()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(FrmCarCompanyEdit_FormClosing);

            cmsDB.tbl_carcompany.TableNewRow += new DataTableNewRowEventHandler(tbl_carcompany_TableNewRow);
            navCarcompany.SaveData += new EventHandler(navCarcompany_SaveData);
            navCarcompany.ConfirmDelete += new CancelEventHandler(navCarcompany_ConfirmDelete);
        }

              
        #endregion

        #region Private Methods

        #endregion

        #region Event Methods

        private void FrmCarCompanyEdit_Load(object sender, EventArgs e)
        {
            try
            {
                tblCarcompanyTableAdapter.Fill(this.cmsDB.tbl_carcompany);

                tblCarcompanyBindingSource.Filter = filterText;
                navCarcompany.Position = selRowIndex;

            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmCarCompanyEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                navCarcompany.CurrencyManager.EndCurrentEdit();
                DataTable tblChange = cmsDB.tbl_carcompany.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        navCarcompany_SaveData(this, e);
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
                navCarcompany.Position = 0;
                DataTable tblChange = cmsDB.tbl_carcompany.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        navCarcompany_SaveData(this, e);
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void navCarcompany_SaveData(object sender, EventArgs e)
        {
            try
            {
                FrmMessage msg = new FrmMessage();
                String errText = "";
                bool deleted = false;

                for (int i = 0; i < cmsDB.tbl_carcompany.Rows.Count; i++)
                {
                    DataRow row = cmsDB.tbl_carcompany.Rows[i];

                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        if (row["companyno"].ToString().Length == 0)
                        {
                            errText = String.Format("{0:0}的进货单位代码空了。", (i + 1).ToString());
                            msg.ShowCloseMsg(errText, "", @"警告");
                            break;
                        }

                        if (row["companyname"].ToString().Length == 0)
                        {
                            errText = String.Format("{0:0}的进货单位名称空了。", (i + 1).ToString());
                            msg.ShowCloseMsg(errText, "", @"警告");
                            break;
                        }
                        DataRow[] row1 = cmsDB.tbl_carcompany.Select("companyno = " + row["companyno"].ToString());
                        if (row1.Length > 1)
                        {
                            errText = String.Format("{0:0}的进货单位代码已经在了。", (i + 1).ToString());
                            msg.ShowCloseMsg(errText, "", @"警告");
                            break;
                        }

                        tblCarcompanyTableAdapter.Update(row);
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
                    tblCarcompanyTableAdapter.Update(cmsDB.tbl_carcompany);
                    cmsDB.tbl_cartype.AcceptChanges();
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void tbl_carcompany_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            try
            {
                e.Row["companyno"] = 100;
                e.Row["companyname"] = "";
                e.Row["address"] = "";
                e.Row["postno"] = "";
                e.Row["telno"] = "";
                e.Row["cartypes"] = "";
                e.Row["email"] = "";
                e.Row["managername"] = "";
                e.Row["linkmanname"] = "";
                e.Row["property"] = "";
                e.Row["remark"] = "";
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void navCarcompany_ConfirmDelete(object sender, CancelEventArgs e)
        {
            try
            {
                String warningText = String.Format("你删除第{0:0}项？", navCarcompany.Position + 1);

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