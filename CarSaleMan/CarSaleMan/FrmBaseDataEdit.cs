using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmBaseDataEdit : Form
    {
        #region Fields and Properties

        public int editOption = 1; //1: add, 2: edit, 3: delete
        public String newName = "";
        public String oldName = "";
        public int valOption = 1;

        public bool modified = false;

        #endregion

        #region Constructors

        public FrmBaseDataEdit()
        {
            InitializeComponent();
        }

        
        #endregion
        
        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Methods

        private void FrmBaseDataEdit_Load(object sender, EventArgs e)
        {
            try
            {
                if (editOption == 1)
                {
                    group2.Enabled = false;
                    group3.Enabled = false;
                }
                if (editOption == 2)
                {
                    group1.Enabled = false;
                    group3.Enabled = false;

                    tbName2.Text = oldName;
                }
                if (editOption == 3)
                {
                    group1.Enabled = false;
                    group2.Enabled = false;

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
                    if (tbName1.Text.Length == 0)
                    {
                        FrmMessage msg = new FrmMessage();
                        msg.ShowCloseMsg("你不输入名称。", "", @"警告");

                        return;
                    }

                    newName = tbName1.Text;
                    valOption = (rdOne.Checked == true ? 1 : 2);
                }
                if (editOption == 2)
                {
                    if (tbName3.Text.Length == 0)
                    {
                        FrmMessage msg = new FrmMessage();
                        msg.ShowCloseMsg("你不输入名称。", "", @"警告");

                        return;
                    }

                    newName = tbName3.Text;
                }
                if (editOption == 3)
                {
                    FrmMessage msg = new FrmMessage();
                    if (msg.ShowYesNoMsg("你想删除名称?", "", @"警告") == DialogResult.Yes)
                    {
                        newName = tbName4.Text;
                        return;
                    }
                    newName = "";                    
                }

                modified = true;
                this.Close();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion        
        
    }
}