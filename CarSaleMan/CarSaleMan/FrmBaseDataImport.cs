using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CarSaleMan
{
    public partial class FrmBaseDataImport : Form
    {
        #region Fields and Properties

        public String valContent;

        #endregion

        #region Constructors

        public FrmBaseDataImport()
        {
            InitializeComponent();

        }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Methods

        private void FrmBaseDataImport_Load(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "txt";
            dlg.FileName = "*.txt";
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                this.Close();
                return;
            }

            StreamReader streamReader = new StreamReader(dlg.FileName);
            valContent = streamReader.ReadToEnd();

            rtContent.Text = valContent;
            streamReader.Close();
        }

        #endregion
        
    }
}