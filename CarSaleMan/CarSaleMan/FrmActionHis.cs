using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarSaleMan
{
    public partial class FrmActionHis : Form
    {
        #region Fields and Properties

        public List<StoreChange> actionlist;

        private String logtext;

        #endregion

        #region Constructors

        public FrmActionHis()
        {
            InitializeComponent();

            actionlist = new List<StoreChange>();
        }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Methods

        private void FrmActionHis_Load(object sender, EventArgs e)
        {
            try
            {
                logtext = "";

                if (actionlist.Count > 0)
                {
                    for (int i = 0; i < actionlist.Count; i++)
                    {
                        if (i == 0)
                            logtext += "序    号：" + actionlist[i].vin + "\r\n";

                        logtext += "流 转 号：" + actionlist[i].chagneid + "\r\n";
                        logtext += "操    作：" + actionlist[i].actionkind + "		";
                        logtext += "操作时间：" + actionlist[i].actiondate + "\r\n";
                        logtext += "批 复 人：" + actionlist[i].settlementname + "		";
                        logtext += "经 手 人：" + actionlist[i].handlername + "\r\n";
                        logtext += "当前库位：" + actionlist[i].storeplace + "\r\n";
                        logtext += "-------------------------------------------------------------------------------------------------------------------" + "\r\n";
                        logtext += "备    注：" + actionlist[i].remark + "\r\n";
                        logtext += "-------------------------------------------------------------------------------------------------------------------" + "\r\n";
                    }
                }

                tbActionLog.Text = logtext;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion
        
    }
}