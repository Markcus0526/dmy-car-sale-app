using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using C1.Win.C1InputPanel;
using C1.Win.C1List;
using System.Threading;

namespace CarSaleMan
{
    public partial class FrmSplash : Form
    {
        #region Fields and Properties
        #endregion

        #region Constructors

        public FrmSplash()
        {
            InitializeComponent();

            // definition events
            this.workerLoading.DoWork += new DoWorkEventHandler(workerLoading_DoWork);
            this.workerLoading.ProgressChanged += new ProgressChangedEventHandler(workerLoading_ProgressChanged);
            this.workerLoading.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerLoading_RunWorkerCompleted);
        }

        

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Methods

        private void FrmSplash_Load(object sender, EventArgs e)
        {
            try
            {
                workerLoading.RunWorkerAsync();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void workerLoading_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                workerLoading.ReportProgress(40);

                C1FlexGrid grid = new C1FlexGrid();
                workerLoading.ReportProgress(60);
                Thread.Sleep(1000);

                C1InputPanel panel = new C1InputPanel();
                workerLoading.ReportProgress(80);
                Thread.Sleep(1000);

                C1List list = new C1List();
                workerLoading.ReportProgress(100);
                Thread.Sleep(1000);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void workerLoading_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        void workerLoading_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                progLoading.Value = e.ProgressPercentage;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }
        
        #endregion
        
    }
}