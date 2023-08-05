using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmUserPermissionEdit : Form
    {
        #region Fields and Properties

        public int editOption = 1; //1: add, 2: edit, 3: delete
        public String oldDepartCode = "";
        public String oldDepartName = "";
        public String oldName = "";

        public int valOption = 1;
        public bool modified = false;

        #endregion

        #region Constructors

        public FrmUserPermissionEdit()
        {
            InitializeComponent();

            // event proc
            cbDepartCode3.SelectedIndexChanged += new EventHandler(cbDepartCode3_SelectedIndexChanged);
        }
        
        #endregion

        
        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Methods

        private void FrmUserPermissionEdit_Load(object sender, EventArgs e)
        {            
            try
            {
                tblUserinfoTableAdapter.Fill(cmsDB.tbl_userinfo);                

                if (editOption == 1)
                {
                    group2.Enabled = false;
                    group3.Enabled = false;
                }
                if (editOption == 2)
                {
                    group1.Enabled = false;
                    group3.Enabled = false;

                    tbDepartCode2.Text = oldDepartCode;
                    tbDepartName2.Text = oldDepartName;
                    tbName2.Text = oldName;

                    vwDepartmentTableAdapter.Fill(cmsDB.vw_department);

                    DataRow[] rows = cmsDB.vw_department.Select("departmentcode = '" + oldDepartCode + "'");
                    if (rows != null && rows.Length > 0)
                    {
                        cbDepartCode3.Text = rows[0]["departmentcode"].ToString();
                        tbDepartName3.Text = rows[0]["departmentname"].ToString();
                    }
                }
                if (editOption == 3)
                {
                    group1.Enabled = false;
                    group2.Enabled = false;

                    tbDepartCode4.Text = oldDepartCode;
                    tbDepartName4.Text = oldDepartName;
                    tbName4.Text = oldName;
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
                if (editOption == 1)
                {
                    if (tbDepartCode1.Text.Length == 0 && tbDepartName1.Text.Length == 0 && tbName1.Text.Length == 0)
                    {
                        FrmMessage msg = new FrmMessage();
                        msg.ShowCloseMsg("你不输入名称。", "", @"警告");

                        return;
                    }

                    DataRow[] rows = cmsDB.tbl_userinfo.Select("departmentcode = '" + tbDepartCode1.Text + "'");
                    if (rows != null && rows.Length > 0)
                    {
                        String departname = rows[0]["departmentname"].ToString();

                        if (departname.Equals(tbDepartName1.Text) == false)
                        {
                            FrmMessage msg = new FrmMessage();
                            String errText = String.Format("{0:0}的名称已经在了。", tbDepartCode1.Text);
                            msg.ShowCloseMsg(errText, "", @"警告");
                            
                            return;
                        }
                    }

                    rows = cmsDB.tbl_userinfo.Select("username = '" + tbName1.Text + "'");
                    if (rows != null && rows.Length > 0)
                    {
                        String departname = rows[0]["departmentname"].ToString();

                        if (departname.Equals(tbDepartName1.Text) == true)
                        {
                            FrmMessage msg = new FrmMessage();
                            msg.ShowCloseMsg("用户名称已经在了", "", @"警告");

                            return;
                        }
                    }

                    DataRow row = cmsDB.tbl_userinfo.NewRow();
                    row["departmentcode"] = tbDepartCode1.Text;
                    row["departmentname"] = tbDepartName1.Text;
                    row["username"] = tbName1.Text;
                    row["password"] = CsmEncrypt.DESEncode("", Global.STR_DES_KEY);

                    cmsDB.tbl_userinfo.Rows.Add(row);
                    tblUserinfoTableAdapter.Update(row);

                    modified = true;
                }
                if (editOption == 2)
                {
                    if (tbName3.Text.Length == 0)
                    {
                        FrmMessage msg = new FrmMessage();
                        msg.ShowCloseMsg("你不输入名称。", "", @"警告");

                        return;
                    }

                    DataRow[] rows = cmsDB.tbl_userinfo.Select("departmentcode = '" + oldDepartCode + "' AND username = '" + oldName + "'");
                    if (rows != null && rows.Length > 0)
                    {
                        rows[0]["departmentcode"] = cbDepartCode3.Text;
                        rows[0]["departmentname"] = tbDepartName3.Text;
                        rows[0]["username"] = tbName3.Text;

                        tblUserinfoTableAdapter.Update(rows);
                    }

                    modified = true;
                }
                if (editOption == 3)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你想删除名称?", "", @"警告") == DialogResult.Yes)
                    {
                        DataRow[] rows = cmsDB.tbl_userinfo.Select("departmentcode = '" + oldDepartCode + "' AND username = '" + oldName + "'");
                        if (rows != null && rows.Length > 0)
                        {
                            rows[0].Delete();

                            tblUserinfoTableAdapter.Update(rows);
                        }

                        modified = true;
                    }
                }

                this.Close();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void cbDepartCode3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = cmsDB.vw_department.Select("departmentcode = '" + cbDepartCode3.Text + "'");
                if (rows != null && rows.Length > 0)
                {
                    tbDepartName3.Text = rows[0]["departmentname"].ToString();
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