using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmLogon : Form
    {
        #region Fields and Properties

        private Point pointMouseOffset = new Point(0, 0);

        private Bitmap bmpLogon, bmpExit;

        CarSaleMan.CmsDBTableAdapters.tbl_userinfoTableAdapter query = new CarSaleMan.CmsDBTableAdapters.tbl_userinfoTableAdapter();

        #endregion

        #region Constructors

        public FrmLogon()
        {
            InitializeComponent();

            bmpLogon = Properties.Resources.btnLogon;
            bmpExit = Properties.Resources.btnExit;
            //BitmapRegion.CreateControlRegion(btnLogon, bmpLogon);
            //BitmapRegion.CreateControlRegion(btnExit, bmpExit);

            cbDepartCode.SelectedIndexChanged += new EventHandler(cbDepartCode_SelectedIndexChanged);
            this.MouseDown += new MouseEventHandler(frmLogon_MouseDown);
            this.MouseMove += new MouseEventHandler(FrmLogon_MouseMove);
        }

        

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        public void SetPermission(int userid)
        {
            try
            {
                DataRow[] rows = cmsDB.tbl_permission.Select("userinfoid = " + userid);
                if (rows != null && rows.Length > 0)
                {
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

        private void Logon()
        {
            try
            {
                String cryptpass = CsmEncrypt.DESEncode(tbPassword.Text, Global.STR_DES_KEY);

                DataRow[] rows = cmsDB.tbl_userinfo.Select("username = '" + cbUsername.Text + "'");
                if (rows != null && rows.Length > 0)
                {
                    if (rows[0]["password"].ToString().Equals(cryptpass) == false)
                    {
                        FrmMessage msg = new FrmMessage();
                        msg.ShowCloseMsg("√‹¬Î”–ŒÛ£¨«Î∫À µ£°", "", @"æØ∏Ê");

                        tbPassword.Focus();
                        tbPassword.SelectAll();

                        return;
                    }

                    Global.LOGIN_USERID = Convert.ToInt32(rows[0]["uid"].ToString());
                    Global.LOGIN_DEPARTMENTCODE = cbDepartCode.Text;
                    Global.LOGIN_USERNAME = cbUsername.Text;
                    Global.LOGIN_PASS = tbPassword.Text;

                    SetPermission(Convert.ToInt32(rows[0]["uid"].ToString()));

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

        #region Event Methods

        private void FrmLogon_Load(object sender, EventArgs e)
        {
            try
            {
                vwDepartmentTableAdapter.Fill(cmsDB.vw_department);
                tblUserinfoTableAdapter.Fill(cmsDB.tbl_userinfo);
                tblPermissionTableAdapter.Fill(cmsDB.tbl_permission);

                tblUserinfoBindingSource.Filter = "departmentcode = '" + cbDepartCode.Text + "'";
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void cbDepartCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tblUserinfoBindingSource.Filter = "departmentcode = '" + cbDepartCode.Text + "'";
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void FrmLogon_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!(sender is System.Windows.Forms.Control)) return;

                if (e.Button == MouseButtons.Left)
                {
                    Control objControl = (Control)sender;
                    ScrollableControl objScrollControl = (ScrollableControl)objControl.Parent;
                    Point pointMouse = Control.MousePosition;

                    if (sender.Equals(this))
                    {
                        pointMouse.Offset(pointMouseOffset.X, pointMouseOffset.Y);
                    }
                    else
                    {
                        pointMouse.Offset(pointMouseOffset.X - objScrollControl.DockPadding.Left - objControl.Left
                          , pointMouseOffset.Y - objScrollControl.DockPadding.Top - objControl.Top);
                    }

                    this.Location = pointMouse;
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void frmLogon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                pointMouseOffset = new Point(-e.X, -e.Y);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            try
            {
                Logon();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Logon();
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