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
    public partial class FrmDateSetting : Form
    {
        #region Fields and Properties

        public DateTime startDate;
        public DateTime endDate;

        #endregion

        #region Constructors

        public FrmDateSetting()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(FrmDateSetting_FormClosing);
        }

        void FrmDateSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                startDate = dtpStart.Value;
                endDate = dtpEnd.Value;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        #endregion
           
        #region Public Methods

        #endregion

        #region Private Methods
        #endregion

        #region Event Methods

        private void FrmDateSetting_Load(object sender, EventArgs e)
        {
            try
            {
                dtpStart.Value = DateTime.Now.AddMonths(-1).AddDays(1);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }
             
        #endregion
        
    }
}
