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
    public partial class FrmFinanceParam : Form
    {
        #region Fields and Properties
        #endregion

        #region Constructors

        public FrmFinanceParam()
        {
            InitializeComponent();

            // events
            this.FormClosing += new FormClosingEventHandler(FrmFinanceParam_FormClosing);

            // button's events
            btnNoInterest10Color.BackColorChanged += new EventHandler(btnNoInterest10Color_BackColorChanged);
            btnNoInterest5Color.BackColorChanged += new EventHandler(btnNoInterest5Color_BackColorChanged);
            btnExtend10Color.BackColorChanged += new EventHandler(btnExtend10Color_BackColorChanged);
            btnExtend5Color.BackColorChanged += new EventHandler(btnExtend5Color_BackColorChanged);
        }

        
        

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        private void SelectColor(Button btnSender)
        {
            try
            {
                ColorDialog dlg = new ColorDialog();
                // Keeps the user from selecting a custom color.
                dlg.AllowFullOpen = false;
                // Allows the user to get help. (The default is false.)
                dlg.ShowHelp = true;
                // Sets the initial color select to the current text color.
                dlg.Color = btnSender.BackColor;

                // Update the text box color if the user clicks OK  
                if (dlg.ShowDialog() == DialogResult.OK)
                    btnSender.BackColor = dlg.Color;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void LoadTableFromDB()
        {
            try
            {
                this.tblEnvTableAdapter.Fill(cmsDB.tbl_env);

                DataRow[] rows = cmsDB.tbl_env.Select("name = 'nointerestdates'");
                numNoInterestDates.Value = Convert.ToInt32(rows[0]["value"].ToString());

                txtNoInterestRate.Text = "0";

                rows = cmsDB.tbl_env.Select("name = 'nointerest10color'");
                btnNoInterest10Color.BackColor = Color.FromArgb(Convert.ToInt32(rows[0]["value"].ToString()));

                rows = cmsDB.tbl_env.Select("name = 'nointerest5color'");
                btnNoInterest5Color.BackColor = Color.FromArgb(Convert.ToInt32(rows[0]["value"].ToString()));

                rows = cmsDB.tbl_env.Select("name = 'extenddates'");
                numExtendDates.Value = Convert.ToInt32(rows[0]["value"].ToString());

                rows = cmsDB.tbl_env.Select("name = 'interestrate'");
                txtInterestRate.Text = rows[0]["value"].ToString();

                rows = cmsDB.tbl_env.Select("name = 'extend10color'");
                btnExtend10Color.BackColor = Color.FromArgb(Convert.ToInt32(rows[0]["value"].ToString()));

                rows = cmsDB.tbl_env.Select("name = 'extend5color'");
                btnExtend5Color.BackColor = Color.FromArgb(Convert.ToInt32(rows[0]["value"].ToString()));

                rows = cmsDB.tbl_env.Select("name = 'extendrate'");
                txtExtendRate.Text = rows[0]["value"].ToString();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void SaveData()
        {
            try
            {
                tblEnvTableAdapter.Update(cmsDB.tbl_env);
                cmsDB.tbl_env.AcceptChanges();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }
        #endregion

        #region Event Methods

        private void FrmFinanceParam_Load(object sender, EventArgs e)
        {
            try
            {                
                LoadTableFromDB();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void FrmFinanceParam_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DataTable tblChange = cmsDB.tbl_env.GetChanges();

                if (tblChange != null)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你是否保存更改", "", @"警告") == DialogResult.Yes)
                    {
                        SaveData();
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void btnNoInterest10Color_Click(object sender, EventArgs e)
        {
            try
            {
                SelectColor(btnNoInterest10Color);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void btnNoInterest5Color_Click(object sender, EventArgs e)
        {
            try
            {
                SelectColor(btnNoInterest5Color);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void btnExtend10Color_Click(object sender, EventArgs e)
        {
            try
            {
                SelectColor(btnExtend10Color);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void btnExtend5Color_Click(object sender, EventArgs e)
        {
            try
            {
                SelectColor(btnExtend5Color);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void btnExtend5Color_BackColorChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = cmsDB.tbl_env.Select("name = 'extend5color'");

                if (btnExtend5Color.BackColor != Color.FromArgb(Convert.ToInt32(rows[0]["value"].ToString())))
                    rows[0]["value"] = btnExtend5Color.BackColor.ToArgb().ToString();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void btnExtend10Color_BackColorChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = cmsDB.tbl_env.Select("name = 'extend10color'");

                if (btnExtend10Color.BackColor != Color.FromArgb(Convert.ToInt32(rows[0]["value"].ToString())))
                    rows[0]["value"] = btnExtend10Color.BackColor.ToArgb().ToString();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void btnNoInterest5Color_BackColorChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = cmsDB.tbl_env.Select("name = 'nointerest5color'");

                if (btnNoInterest5Color.BackColor != Color.FromArgb(Convert.ToInt32(rows[0]["value"].ToString())))
                    rows[0]["value"] = btnNoInterest5Color.BackColor.ToArgb().ToString();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void btnNoInterest10Color_BackColorChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = cmsDB.tbl_env.Select("name = 'nointerest10color'");

                if (btnNoInterest10Color.BackColor != Color.FromArgb(Convert.ToInt32(rows[0]["value"].ToString())))
                    rows[0]["value"] = btnNoInterest10Color.BackColor.ToArgb().ToString();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void numNoInterestDates_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = cmsDB.tbl_env.Select("name = 'nointerestdates'");

                if (numNoInterestDates.Value != Convert.ToDecimal(rows[0]["value"].ToString()))
                    rows[0]["value"] = numNoInterestDates.Value.ToString();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void numExtendDates_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = cmsDB.tbl_env.Select("name = 'extenddates'");

                if (numExtendDates.Value != Convert.ToDecimal(rows[0]["value"].ToString()))
                    rows[0]["value"] = numExtendDates.Value.ToString();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void txtNoInterestRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void txtInterestRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = cmsDB.tbl_env.Select("name = 'interestrate'");

                if (txtInterestRate.Text.Equals(rows[0]["value"].ToString()) == false)
                    rows[0]["value"] = txtInterestRate.Text;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void txtExtendRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = cmsDB.tbl_env.Select("name = 'extendrate'");

                if (txtExtendRate.Text.Equals(rows[0]["value"].ToString()) == false)
                    rows[0]["value"] = txtExtendRate.Text;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion

    }
}
