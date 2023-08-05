using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Runtime.InteropServices;

namespace CarSaleMan
{
    static class CarSaleMain
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN", true);

            CommonMisc.ReadEnvironment();
            
            if (DBProvider.GetAutoLogon() == true)
            {
                if (DBProvider.ConnectServer() == false)
                {
                    FrmEnvSet frm = new FrmEnvSet();
                    frm.serverName = DBProvider.GetServerAddress();
                    frm.dbPassword = DBProvider.GetDbPassword();

                    if (frm.ShowDialog() == DialogResult.Cancel)
                    {
                        Application.Exit();
                        return;
                    }
                }
            }
            else
            {
                DBProvider.ParseConnectionStringFromConfig();

                FrmEnvSet frm = new FrmEnvSet();
                frm.serverName = DBProvider.GetServerAddress();
                frm.dbPassword = DBProvider.GetDbPassword();

                if (frm.ShowDialog() == DialogResult.Cancel)
                {
                    Application.Exit();
                    return;
                }
                
            }

            FrmLogon frmLogon = new FrmLogon();

            if (frmLogon.ShowDialog() == DialogResult.Cancel)
            {
                CommonMisc.WriteEnvironment();
                Application.Exit();
                return;
            }

            /*FrmSplash frmSplash = new FrmSplash();
            frmSplash.ShowDialog();*/

            Application.Run(new FrmMDIMain());

            CommonMisc.WriteEnvironment();
        }
    }
}