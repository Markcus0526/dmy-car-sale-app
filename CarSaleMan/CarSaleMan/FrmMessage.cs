using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmMessage : Form
    {
        #region Fiels and Properties
        DialogResult retValue;
        private Point pointMouseOffset = new Point(0, 0);
        #endregion

        #region Constructors
        /// <summary>
        /// FrmMessage
        /// </summary>
        internal FrmMessage()
        {
            InitializeComponent();
            btnYes.Click += new EventHandler(btnYes_Click);
            btnNo.Click += new EventHandler(btnNo_Click);
            btnClose.Click += new EventHandler(btnClose_Click);
            this.MouseDown += new MouseEventHandler(frmMessage_MouseDown);
            this.MouseMove += new MouseEventHandler(frmMessage_MouseMove);
        }
        #endregion

        #region internal Methods
        /// <summary>
        /// ShowCloseMsg
        /// </summary>
        /// <param name="pstrText1"></param>
        /// <param name="pstrText2"></param>
        /// <param name="pstrTitle"></param>
        /// <returns></returns>
        internal DialogResult ShowCloseMsg(string pstrText1, string pstrText2, string pstrTitle)
        {
            lblText1.Text = pstrText1;
            lblText2.Text = pstrText2;
            lblTitle.Text = pstrTitle;
            btnYes.Visible = false;
            btnNo.Visible = false;
            btnClose.Visible = true;

            rdSelect1.Visible = false;
            rdSelect2.Visible = false;

            this.ShowDialog();
            return retValue;
        }

        /// <summary>
        /// ShowYesNoMsg
        /// </summary>
        /// <param name="pstrText1"></param>
        /// <param name="pstrText2"></param>
        /// <param name="pstrTitle"></param>
        /// <returns></returns>
        internal DialogResult ShowYesNoMsg(string pstrText1, string pstrText2, string pstrTitle)
        {
            lblText1.Text = pstrText1;
            lblText2.Text = pstrText2;
            lblTitle.Text = pstrTitle;
            btnYes.Visible = true;
            btnNo.Visible = true;
            btnClose.Visible = false;

            rdSelect1.Visible = false;
            rdSelect2.Visible = false;

            this.ShowDialog();
            return retValue;
        }

        /// <summary>
        /// ShowSelectMsg
        /// </summary>
        /// <param name="pstrText1"></param>
        /// <param name="pstrText2"></param>
        /// <param name="pstrTitle"></param>
        /// <returns></returns>
        internal DialogResult ShowSelectMsg(string pstrText1, string pstrText2, string pstrTitle)
        {
            rdSelect1.Text = pstrText1;
            rdSelect2.Text = pstrText2;
            lblTitle.Text = pstrTitle;
            btnYes.Visible = true;
            btnNo.Visible = true;
            btnClose.Visible = false;

            lblText1.Visible = false;
            lblText2.Visible = false;

            this.ShowDialog();
            return retValue;
        }
        #endregion

        #region Private Method
        #endregion

        #region Event Method
        /// <summary>
        /// btnClose_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnClose_Click(object sender, EventArgs e)
        {
            retValue = DialogResult.None;
            this.Close();
        }

        /// <summary>
        /// btnNo_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnNo_Click(object sender, EventArgs e)
        {
            if (rdSelect1.Visible == true)
            {
                retValue = DialogResult.Cancel;
                this.Close();
            }
            else
            {
                retValue = DialogResult.No;
                this.Close();
            }
        }

        /// <summary>
        /// btnYes_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnYes_Click(object sender, EventArgs e)
        {
            if (rdSelect1.Visible == true)
            {
                if (rdSelect1.Checked == true)
                    retValue = DialogResult.Yes;
                else
                    retValue = DialogResult.No;
                this.Close();
            }
            else
            {
                retValue = DialogResult.Yes;
                this.Close();
            }
        }

        /// <summary>
        /// frmMessage_MouseMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmMessage_MouseMove(object sender, MouseEventArgs e)
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

        /// <summary>
        /// frmMessage_MouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmMessage_MouseDown(object sender, MouseEventArgs e)
        {
            pointMouseOffset = new Point(-e.X, -e.Y);
        }

        #endregion    
    }
}