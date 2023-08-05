using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CarSaleMan
{
    public partial class FrmBaseData : Form
    {
        #region Fields and Properties

        public static FrmBaseData frmBaseData;

        private DataTable tblGroupTable;

        private String selectedName;
        private int selectedType;

        #endregion

        #region Constructors

        public FrmBaseData()
        {
            InitializeComponent();

            // init private variables
            tblGroupTable = new DataTable();               

            // initialize event handler
            this.Activated += new EventHandler(FrmBaseData_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmBaseData_FormClosing);

            this.lstName.SelChange += new CancelEventHandler(lstName_SelChange);

            cmsDB.tbl_basedata.TableNewRow += new DataTableNewRowEventHandler(tbl_basedata_TableNewRow);
        }

        #endregion

        #region Public Methods

        public static FrmBaseData GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmBaseData == null) //if not created yet, Create an instance
                {
                    frmBaseData = new FrmBaseData();
                    frmBaseData.MdiParent = parent;
                }
            }
            return frmBaseData;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void LoadTableFromDB(bool isReload, int grpIdx)
        {
            try
            {               
                if (isReload)
                {
                    lstName.ClearItems();

                    tblBasedataTableAdapter.Fill(cmsDB.tbl_basedata);

                    tblGroupTable = CommonMisc.GroupBy("name", "name", cmsDB.tbl_basedata);

                    if (tblGroupTable == null)
                        return;

                    for (int i = 0; i < tblGroupTable.Rows.Count; i++)
                    {
                        lstName.AddItem(tblGroupTable.Rows[i]["name"].ToString());
                    }

                    grpIdx = 0;
                    lstName.SelectedIndex = 0;
                }

                if (tblGroupTable.Rows.Count > 0)
                {
                    selectedName = lstName.GetItemText(grpIdx, 0);    
                    tblBasedataBindingSource.Filter = "name = '" + selectedName + "'";

                    selectedType = int.Parse(gridValue.Cols["type"][1].ToString());
                    if (selectedType == 1)
                    {
                        gridValue.Cols["keyname"].Visible = false;
                    }
                    else if (selectedType == 2)
                    {
                        gridValue.Cols["keyname"].Visible = true;
                    }

                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion

        #region Event Methods

        private void FrmBaseData_Load(object sender, EventArgs e)
        {
            try
            {                
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void FrmBaseData_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmBaseData = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmBaseData_Activated(object sender, EventArgs e)
        {
            try
            {
                LoadTableFromDB(true, 0);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbAddName_Click(object sender, EventArgs e)
        {
            try
            {
                FrmBaseDataEdit frm = new FrmBaseDataEdit();
                frm.editOption = 1;

                frm.ShowDialog();
                if (frm.modified == true)
                {
                    DataRow[] rows = cmsDB.tbl_basedata.Select("name = '" + frm.newName + "'");

                    if (rows != null && rows.Length > 0)
                    {
                        FrmMessage msg = new FrmMessage();
                        msg.ShowCloseMsg("你输入的名称已经在了。", "", @"警告");

                        return;
                    }

                    DataRow row = cmsDB.tbl_basedata.NewRow();
                    row["type"] = frm.valOption;
                    row["name"] = frm.newName;
                    row["keyname"] = "";
                    row["value"] = "";

                    cmsDB.tbl_basedata.Rows.Add(row);

                    lstName.AddItem(frm.newName);

                    tblBasedataTableAdapter.Update(row);
                    cmsDB.tbl_basedata.AcceptChanges();

                    lstName.Row = lstName.ListCount - 1;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbDeleteName_Click(object sender, EventArgs e)
        {
            try
            {
                FrmBaseDataEdit frm = new FrmBaseDataEdit();
                frm.editOption = 3;
                frm.oldName = selectedName;

                frm.ShowDialog();
                if (frm.modified == true)
                {
                    DataRow[] rows = cmsDB.tbl_basedata.Select("name = '" + selectedName + "'");
                    for (int i = 0; i < rows.Length; i++)
                    {
                        rows[i].BeginEdit();
                        rows[i].Delete();
                        rows[i].EndEdit();
                    }

                    tblBasedataTableAdapter.Update(rows);
                    cmsDB.tbl_basedata.AcceptChanges();

                    lstName.RemoveItem(lstName.SelectedIndex);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbChangeName_Click(object sender, EventArgs e)
        {
            try
            {
                FrmBaseDataEdit frm = new FrmBaseDataEdit();
                frm.editOption = 2;
                frm.oldName = selectedName;

                frm.ShowDialog();
                if (frm.modified == true)
                {
                    DataRow[] rows = cmsDB.tbl_basedata.Select("name = '" + frm.oldName + "'");
                    for (int i = 0; i < rows.Length; i++)
                    {
                        rows[i].BeginEdit();
                        rows[i]["name"] = frm.newName;
                        rows[i].EndEdit();
                    }

                    tblBasedataTableAdapter.Update(rows);
                    cmsDB.tbl_basedata.AcceptChanges();

                    int sel = lstName.SelectedIndex;
                    lstName.RemoveItem(sel);
                    lstName.InsertItem(frm.newName, sel);

                    lstName.Row = sel;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbSaveValue_Click(object sender, EventArgs e)
        {
            try
            {
                tblBasedataTableAdapter.Update(cmsDB.tbl_basedata);
                cmsDB.tbl_basedata.AcceptChanges();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbRejectValue_Click(object sender, EventArgs e)
        {
            try
            {
                cmsDB.tbl_basedata.RejectChanges();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbImportValue_Click(object sender, EventArgs e)
        {
            try
            {
                FrmBaseDataImport frm = new FrmBaseDataImport();

                if (frm.ShowDialog() == DialogResult.OK && frm.valContent.Length > 0)
                {
                    String[] contents = Regex.Split(frm.valContent, @"\r?\n|\r");

                    foreach (String item in contents)
                    {
                        if (item.Length > 0)
                        {
                            DataRow row = cmsDB.tbl_basedata.NewRow();
                            row["type"] = selectedType;
                            row["name"] = selectedName;
                            if (selectedType == 1)
                            {
                                row["keyname"] = "";
                                row["value"] = item;
                            }
                            else if (selectedType == 2)
                            {
                                String[] item2 = Regex.Split(item, @":");
                                if (item2.Length >= 2)
                                {
                                    row["keyname"] = item2[0];
                                    row["value"] = item2[1];
                                }
                                else
                                {
                                    row["keyname"] = "";
                                    row["value"] = item;
                                }
                            }

                            cmsDB.tbl_basedata.Rows.Add(row);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbReturn_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tblChange = cmsDB.tbl_basedata.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg(@"你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        tsbSaveValue_Click(sender, e);
                    }
                    else
                    {
                        cmsDB.tbl_basedata.RejectChanges();
                    }
                }
                this.Close();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        // list box event handler
        void lstName_SelChange(object sender, CancelEventArgs e)
        {
            try
            {
                if (lstName.WillChangeToIndex < 0)
                    return;

                DataTable tblChange = cmsDB.tbl_basedata.GetChanges();

                if (tblChange != null && lstName.SelectedIndex >= 0)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        tblBasedataTableAdapter.Update(cmsDB.tbl_basedata);
                        cmsDB.tbl_basedata.AcceptChanges();
                    }
                    else
                    {
                        cmsDB.tbl_basedata.RejectChanges();
                    }
                }

                LoadTableFromDB(false, lstName.WillChangeToIndex);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        // tbl_basedata event handler
        void tbl_basedata_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            try
            {
                e.Row["type"] = selectedType;
                e.Row["name"] = selectedName;
                e.Row["keyname"] = "";
                e.Row["value"] = "";
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
            
        }

        #endregion

        
    }
}