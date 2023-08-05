using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using C1.Win.C1FlexGrid;

namespace CarSaleMan
{
    public partial class FrmUserPermission : Form
    {
        #region Fields and Properties

        public static FrmUserPermission frmUserPermission;

        private String fieldcombolist = "";
        private String permissioncombolist = "";
        private int userinfoid = 0;

        #endregion

        #region Constructors

        public FrmUserPermission()
        {
            InitializeComponent();

            // init private variables

            // initialize event handler
            this.Activated += new EventHandler(FrmUserPermission_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmUserPermission_FormClosing);

            gridUser.SelChange += new EventHandler(gridUser_SelChange);
            gridValue.BeforeEdit += new RowColEventHandler(gridValue_BeforeEdit);
            cmsDB.tbl_permission.TableNewRow += new DataTableNewRowEventHandler(tbl_permission_TableNewRow);
        }

        
        #endregion

        #region Public Methods

        public static FrmUserPermission GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmUserPermission == null) //if not created yet, Create an instance
                {
                    frmUserPermission = new FrmUserPermission();
                    frmUserPermission.MdiParent = parent;
                }
            }
            return frmUserPermission;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void LoadTableFromDB()
        {
            try
            {
                String menulist = "";

                foreach (String item in Global.MENU_LIST)
                {
                    menulist += item + "|";
                }
                menulist = menulist.Substring(0, menulist.Length - 1);
                gridValue.Cols["fieldname"].ComboList = menulist;

                fieldcombolist = gridValue.Cols["fieldname"].ComboList.ToString();
                permissioncombolist = gridValue.Cols["permission"].ComboList.ToString();

                // load user and department name
                tblUserinfoTableAdapter.FillByDepartment(cmsDB.tbl_userinfo);

                gridUser.Tree.Column = 0;
                gridUser.Tree.Style = TreeStyleFlags.Simple;
                gridUser.AllowMerging = AllowMergingEnum.Nodes;
                gridUser.Rows.Count = 1;

                String departcode1 = cmsDB.tbl_userinfo.Rows[0]["departmentcode"].ToString() + " - " + cmsDB.tbl_userinfo.Rows[0]["departmentname"].ToString();
                gridUser.Rows.Add();
                gridUser.Rows[1].IsNode = true;
                gridUser.Rows[1].Node.Level = 0;
                gridUser[1, 0] = departcode1;

                String username = cmsDB.tbl_userinfo.Rows[0]["username"].ToString();

                int row = gridUser.Rows.Count;

                gridUser.Rows.Add();
                gridUser.Rows[row].IsNode = true;
                gridUser.Rows[row].Node.Level = 1;
                gridUser[row, 0] = username;

                for (int i = 1; i < cmsDB.tbl_userinfo.Rows.Count; i++)
                {
                    String departcode2 = cmsDB.tbl_userinfo.Rows[i]["departmentcode"].ToString() + " - " + cmsDB.tbl_userinfo.Rows[i]["departmentname"].ToString();
                    if (departcode1.Equals(departcode2) == true)
                    {
                        username = cmsDB.tbl_userinfo.Rows[i]["username"].ToString();

                        row = gridUser.Rows.Count;
                        gridUser.Rows.Add();
                        gridUser.Rows[row].IsNode = true;
                        gridUser.Rows[row].Node.Level = 1;
                        gridUser[row, 0] = username;
                    }
                    else
                    {
                        username = cmsDB.tbl_userinfo.Rows[i]["username"].ToString();

                        row = gridUser.Rows.Count;
                        gridUser.Rows.Add();
                        gridUser.Rows[row].IsNode = true;
                        gridUser.Rows[row].Node.Level = 0;
                        gridUser[row, 0] = departcode2;

                        row = gridUser.Rows.Count;
                        gridUser.Rows.Add();
                        gridUser.Rows[row].IsNode = true;
                        gridUser.Rows[row].Node.Level = 1;
                        gridUser[row, 0] = username;

                        departcode1 = departcode2;
                    }
                }

            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        public void SetCurrentPermission()
        {
            try
            {
                DataRow[] rows = cmsDB.tbl_permission.Select("userinfoid = " + Global.LOGIN_USERID);
                if (rows != null && rows.Length > 0)
                {
                    Global.PERMISSION_LIST.Clear();

                    for (int i = 0; i < rows.Length; i++)
                    {
                        Global.PERMISSION_LIST.Add(rows[i]["fieldname"].ToString(), rows[i]["permission"].ToString());
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

        private void FrmUserPermission_Load(object sender, EventArgs e)
        {
            try
            {                
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void FrmUserPermission_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmUserPermission = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmUserPermission_Activated(object sender, EventArgs e)
        {
            try
            {
                LoadTableFromDB();
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
                FrmUserPermissionEdit frm = new FrmUserPermissionEdit();
                frm.editOption = 1;

                frm.ShowDialog();
                if (frm.modified == true)
                {
                    LoadTableFromDB();
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
                FrmUserPermissionEdit frm = new FrmUserPermissionEdit();
                frm.editOption = 3;

                DataRow[] rows = cmsDB.tbl_userinfo.Select("username = '" + gridUser.Rows[gridUser.Row][0].ToString() + "'");

                if (rows != null && rows.Length > 0)
                {
                    frm.oldDepartCode = rows[0]["departmentcode"].ToString();
                    frm.oldDepartName = rows[0]["departmentname"].ToString();
                    frm.oldName = rows[0]["username"].ToString();

                    frm.ShowDialog();
                    if (frm.modified == true)
                    {
                        LoadTableFromDB();
                    }
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
                FrmUserPermissionEdit frm = new FrmUserPermissionEdit();
                frm.editOption = 2;

                DataRow[] rows = cmsDB.tbl_userinfo.Select("username = '" + gridUser.Rows[gridUser.Row][0].ToString() + "'");

                if (rows != null && rows.Length > 0)
                {
                    frm.oldDepartCode = rows[0]["departmentcode"].ToString();
                    frm.oldDepartName = rows[0]["departmentname"].ToString();
                    frm.oldName = rows[0]["username"].ToString();

                    frm.ShowDialog();
                    if (frm.modified == true)
                    {
                        LoadTableFromDB();
                    }
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
                tblPermissionTableAdapter.Update(cmsDB.tbl_permission);
                cmsDB.tbl_permission.AcceptChanges();

                SetCurrentPermission();

                FrmMDIMain parent = (FrmMDIMain)this.MdiParent;
                parent.SetMenuPermission();
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
                cmsDB.tbl_permission.RejectChanges();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void tsbPassword_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = cmsDB.tbl_userinfo.Select("username = '" + gridUser.Rows[gridUser.Row][0].ToString() + "'");

                if (rows != null && rows.Length > 0)
                {
                    FrmPassChange frm = new FrmPassChange();
                    frm.username = rows[0]["username"].ToString();
                    frm.userpass = rows[0]["password"].ToString();
                    frm.currentuser = false;

                    frm.ShowDialog();
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
                DataTable tblChange = cmsDB.tbl_permission.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg(@"你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        tsbSaveValue_Click(sender, e);
                    }
                    else
                    {
                        this.cmsDB.tbl_permission.RejectChanges();
                    }
                }

                this.Close();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void gridUser_SelChange(object sender, EventArgs e)
        {
            try
            {
                DataTable tblChange = cmsDB.tbl_permission.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        tblPermissionTableAdapter.Update(cmsDB.tbl_permission);
                        cmsDB.tbl_permission.AcceptChanges();
                    }
                    else
                    {
                        cmsDB.tbl_permission.RejectChanges();
                    }
                }

                if (gridUser.Rows[gridUser.Row].Node != null && gridUser.Rows[gridUser.Row].Node.Level == 1)
                {
                    DataRow[] rows = cmsDB.tbl_userinfo.Select("username = '" + gridUser.Rows[gridUser.Row][0].ToString() + "'");

                    if (rows != null && rows.Length > 0)
                    {
                        gridValue.AllowAddNew = true;
                        gridValue.AllowDelete = true;
                        gridValue.AllowEditing = true;

                        userinfoid = Convert.ToInt32(rows[0]["uid"].ToString());
                        tblPermissionTableAdapter.FillByUserinfo(cmsDB.tbl_permission, userinfoid);
                    }
                }
                else
                {
                    gridValue.AllowAddNew = false;
                    gridValue.AllowDelete = false;
                    gridValue.AllowEditing = false;

                    tblPermissionTableAdapter.FillByUserinfo(cmsDB.tbl_permission, 0);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        // gridValue event handler
        void gridValue_BeforeEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (e.Col == 2) // fieldname
                {
                    String[] contents = fieldcombolist.Split(new string[] { "|" }, StringSplitOptions.None);
                    String newcontents = "";

                    foreach (String item in contents)
                    {
                        if (item.Length > 0)
                        {
                            DataRow[] rows = cmsDB.tbl_permission.Select("fieldname = '" + item + "'");
                            if (rows != null && rows.Length == 0)
                            {
                                newcontents += item + "|";
                            }
                        }
                    }

                    gridValue.Cols["fieldname"].ComboList = newcontents;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void tbl_permission_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            try
            {
                String[] contents = permissioncombolist.Split(new string[] { "|" }, StringSplitOptions.None);

                e.Row["userinfoid"] = userinfoid;
                e.Row["fieldname"] = "";
                e.Row["permission"] = contents[0];
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion
    }
}