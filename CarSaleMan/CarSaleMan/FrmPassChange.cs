using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmPassChange : Form
    {
        #region Fields and Properties

        public static FrmPassChange frmPassData = null;
        public String username = "";
        public String userpass = "";
        public bool currentuser = true;

        #endregion

        #region Constructors
        public FrmPassChange()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Methods
        public static FrmPassChange GetFrmInstance(Form parent, bool pCreate, bool mdiMode)
        {
            if (pCreate == true)
            {
                if (frmPassData == null) //if not created yet, Create an instance
                {
                    frmPassData = new FrmPassChange();

                    if (mdiMode == true)
                        frmPassData.MdiParent = parent;
                }
            }
            return frmPassData;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void ChangePass()
        {
            try
            {
                if (tbNewPass.Text.Equals(tbNewPassConfirm.Text) == false)
                {
                    FrmMessage msg = new FrmMessage();
                    msg.ShowCloseMsg("新密码有误，请核实！", "", @"警告");

                    tbNewPass.Focus();
                    tbNewPass.SelectAll();

                    return;
                }

                String oldcryptpass = "";
                if (currentuser == true)
                    oldcryptpass = CsmEncrypt.DESEncode(tbOldPass.Text, Global.STR_DES_KEY);
                else
                    oldcryptpass = userpass;

                DataRow[] rows = cmsDB.tbl_userinfo.Select("username = '" + username + "'");
                if (rows != null && rows.Length > 0)
                {
                    if (rows[0]["password"].ToString().Equals(oldcryptpass) == false)
                    {
                        FrmMessage msg = new FrmMessage();
                        msg.ShowCloseMsg("久密码失败！", "", @"警告");

                        tbOldPass.Focus();
                        tbOldPass.SelectAll();

                        return;
                    }

                    String newcryptpass = CsmEncrypt.DESEncode(tbNewPass.Text, Global.STR_DES_KEY);

                    rows[0]["password"] = newcryptpass;
                    tblUserinfoTableAdapter.Update(rows);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }
        #endregion

        #region Event Methods

        private void FrmPassChange_Load(object sender, EventArgs e)
        {
            try
            {
                tblUserinfoTableAdapter.FillByUserName(cmsDB.tbl_userinfo, username);

                tbName.Text = username;
                if (currentuser == true)
                    tbOldPass.Enabled = true;
                else
                    tbOldPass.Enabled = false;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                ChangePass();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void txtOldPass_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    tbNewPass.Focus();
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void txtNewPass_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    tbNewPassConfirm.Focus();
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void txtNewPassConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ChangePass();
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
