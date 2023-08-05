using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmEnvSet : Form
    {
        #region Fields and Properties

        public String serverName = "";
        public String dbPassword = "";
        public bool autoLogon = true;

        private Point pointMouseOffset = new Point(0, 0);

        #endregion

        #region Constructors

        public FrmEnvSet()
        {
            InitializeComponent();

            this.MouseDown += new MouseEventHandler(frmEnvSet_MouseDown);
            this.MouseMove += new MouseEventHandler(frmEnvSet_MouseMove);
        }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Methods

        private void FrmEnvSet_Load(object sender, EventArgs e)
        {
            try
            {
                tbServerName.Text = serverName;
                tbPassword.Text = dbPassword;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void frmEnvSet_MouseMove(object sender, MouseEventArgs e)
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

        void frmEnvSet_MouseDown(object sender, MouseEventArgs e)
        {
            pointMouseOffset = new Point(-e.X, -e.Y);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                DBProvider.SetServerAddress(tbServerName.Text);
                DBProvider.SetDbPassword(tbPassword.Text);
                DBProvider.SetAutoLogon(chkAutoLogon.Checked);

                if (DBProvider.ConnectServer() == false)
                {
                    MessageBox.Show(this, "服务器连接失败。", "", MessageBoxButtons.OK);
                    this.DialogResult = DialogResult.None;
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