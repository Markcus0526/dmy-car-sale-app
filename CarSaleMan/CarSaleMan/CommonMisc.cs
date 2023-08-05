using CarSaleMan;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using C1.C1Preview;

//#region Fields and Properties
//#endregion

//#region Constructors
//#endregion

//#region Public Methods
//#endregion

//#region Private Methods
//#endregion

//#region Event Methods
//#endregion

public static class CommonMisc
{
    #region Fields and Properties

    public static String appTitle = "";

    private static String envFileName = "CSMEnv.ini";
    private static String errFileName = "CSMEnv.log";

    #endregion

    #region Public Methods

    public static void ReadEnvironment()
    {
        try
        {
            String strFilePath = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "\\" + envFileName;

            XmlDocument xmlDoc = new XmlDocument();
            if (File.Exists(strFilePath) == false)
                return;

            xmlDoc.Load(strFilePath);

            XmlNode nodeItems = xmlDoc.FirstChild;

            foreach (XmlNode nodeItem in nodeItems)
            {
                if (nodeItem.Name == @"Environment")
                {
                    for (int i = 0; i < nodeItem.ChildNodes.Count; i++)
                    {
                        if (nodeItem.ChildNodes[i].Name == @"Server")
                            DBProvider.SetServerAddress(nodeItem.ChildNodes[i].InnerText);
                        else if (nodeItem.ChildNodes[i].Name == @"Password")
                            DBProvider.SetDbPassword(CsmEncrypt.DESDecode(nodeItem.ChildNodes[i].InnerText, Global.STR_DES_KEY));
                        else if (nodeItem.ChildNodes[i].Name == @"User")
                            DBProvider.SetUserName(nodeItem.ChildNodes[i].InnerText);
                        else if (nodeItem.ChildNodes[i].Name == @"Auto")
                        {
                            if (nodeItem.ChildNodes[i].InnerText == @"1")
                                DBProvider.SetAutoLogon(true);
                            else
                                DBProvider.SetAutoLogon(false);
                        }
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            LogErrors(ex.ToString());
            throw;
        }
    }

    public static void WriteEnvironment()
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
            XmlNode nodeRoot = xmlDoc.CreateNode("element", "CarSaleMan", "");

            XmlNode nodeApp = xmlDoc.CreateNode("element", "Application", "");

            XmlNode nodeName = xmlDoc.CreateNode("element", "name", "");
            nodeName.InnerText = "辽宁众志诚汽车销售进销存系统";

            XmlNode nodeVersion = xmlDoc.CreateNode("element", "version", "");
            nodeVersion.InnerText = "2.0";

            nodeApp.AppendChild(nodeName);
            nodeApp.AppendChild(nodeVersion);
            nodeRoot.AppendChild(nodeApp);

            XmlNode nodeEnv = xmlDoc.CreateNode("element", "Environment", "");

            XmlNode nodeServer = xmlDoc.CreateNode("element", "Server", "");
            nodeServer.InnerText = DBProvider.GetServerAddress();

            XmlNode nodePassword = xmlDoc.CreateNode("element", "Password", "");
            nodePassword.InnerText = CsmEncrypt.DESEncode(DBProvider.GetDbPassword(), Global.STR_DES_KEY);

            XmlNode nodeUser = xmlDoc.CreateNode("element", "User", "");
            nodeUser.InnerText = DBProvider.GetUserName();

            XmlNode nodeAuto = xmlDoc.CreateNode("element", "Auto", "");
            nodeAuto.InnerText = (DBProvider.GetAutoLogon() == true ? @"1" : @"0");

            nodeEnv.AppendChild(nodeServer);
            nodeEnv.AppendChild(nodePassword);
            nodeEnv.AppendChild(nodeUser);
            nodeEnv.AppendChild(nodeAuto);
            nodeRoot.AppendChild(nodeEnv);

            xmlDoc.AppendChild(nodeRoot);

            xmlDoc.Save(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "\\" + envFileName);
        }
        catch (System.Exception ex)
        {
            LogErrors(ex.ToString());
            throw;
        }
    }

    public static void LogErrors(String pMessage)
    {
        try
        {
            StreamWriter w = File.AppendText(System.Environment.CurrentDirectory + "\\" + errFileName);
            w.WriteLine("{0}--{1}--{2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), pMessage);
            w.Flush();
            w.Close();
        }
        catch (System.Exception ex)
        {
            LogErrors(ex.ToString());
            throw;
        }
    }

    public static DataTable GroupBy(String groupByColumn, String aggregateColumn, DataTable dataTable)
    {
        try
        {
            DataView dv = new DataView(dataTable);

            //getting distinct values for group column
            DataTable dtGroup = dv.ToTable(true, new string[] { groupByColumn });

            //adding column for the row count
            dtGroup.Columns.Add("Count", typeof(int));

            //looping thru distinct values for the group, counting
            foreach (DataRow dr in dtGroup.Rows)
            {
                dr["Count"] = dataTable.Compute("Count(" + aggregateColumn + ")", groupByColumn + " = '" + dr[groupByColumn] + "'");
            }

            //returning grouped/counted result
            return dtGroup;
        }
        catch (System.Exception ex)
        {
            LogErrors(ex.ToString());
            return null;
        }
    }

    public static void AutoSizeSpecificCols(ref RenderTable ioTable, int[] inArrIndexes)
    {
        try
        {
            for (int i = 0; i < inArrIndexes.Length; i++)
            {
                ioTable.Cols[inArrIndexes[i]].Stretch = StretchColumnEnum.Yes;
                ioTable.Cols[inArrIndexes[i]].SizingMode = TableSizingModeEnum.Auto;
            }
        }
        catch (System.Exception ex)
        {
            LogErrors(ex.ToString());
        }
    }

    public static void AutoSizeSpecificCols(ref RenderTable ioTable)
    {
        try
        {
            for (int i = 1; i < ioTable.Cols.Count; i++)
            {
                ioTable.Cols[i].Stretch = StretchColumnEnum.Yes;
                ioTable.Cols[i].SizingMode = TableSizingModeEnum.Auto;
            }
        }
        catch (System.Exception ex)
        {
            LogErrors(ex.ToString());
        }
    }

    public static void DecideChartClusterWidth(ref C1.Win.C1Chart.C1Chart ioChart)
    {
        try
        {
            int nWidth = 0, nSeriesCount = 0, nPointCount = 0, nLimitWidth = 0;

            nWidth = ioChart.ChartArea.Size.Width;
            if (ioChart.ChartGroups.Group0.ChartType != C1.Win.C1Chart.Chart2DTypeEnum.Bar)
                return;

            nSeriesCount = ioChart.ChartGroups.Group0.ChartData.SeriesList.Count;
            if (nSeriesCount == 0)
                return;

            nPointCount = ioChart.ChartGroups.Group0.ChartData.SeriesList[0].Length;
            nLimitWidth = nWidth / 15;                   // pixel
            if (nLimitWidth == 0)
                nLimitWidth = 1;

            if (nWidth / nPointCount * ioChart.ChartGroups.Group0.Bar.ClusterWidth / 100 / nSeriesCount > nLimitWidth)
                ioChart.ChartGroups.Group0.Bar.ClusterWidth = nLimitWidth * 100 * nPointCount * nSeriesCount / nWidth;
        }
        catch (System.Exception ex)
        {
            CommonMisc.LogErrors(ex.Message);
        }
    }

    public static void SetXLabelsVert(ref C1.Win.C1Chart.C1Chart ioChart)
    {
        try
        {
            if (ioChart.ChartGroups.Group0.ChartType == C1.Win.C1Chart.Chart2DTypeEnum.Pie)
                return;

            ioChart.ChartArea.AxisX.AnnoVerticalText = true;
        }
        catch (System.Exception ex)
        {
            CommonMisc.LogErrors(ex.Message);
        }
    }

    public static int BetweenDates(DateTime a, DateTime b)
    {
        try
        {
            TimeSpan span = a - b;
            return (int)span.TotalDays;
        }
        catch (System.Exception ex)
        {
            LogErrors(ex.ToString());
            return 0;
        }
    }
    #endregion
}

public static class DBProvider
{
    #region Fields and Properties

    private static SqlConnection dbConn;

    private static String serverAddress = "";
    private static String dbPassword = "";
    private static String logonUserName = "";
    private static bool autoLogon = true;

    #endregion

    #region Constructors
    #endregion

    #region Public Methods

    public static bool ConnectServer()
    {
        try
        {
            if (serverAddress.Length == 0)
            {
                ParseConnectionStringFromConfig();
            }

            dbConn = GetDBConnection();
            if (dbConn == null)
                return false;

            dbConn.Open();

            dbConn.Close();
            return true;
        }
        catch (System.Exception ex)
        {
            CommonMisc.LogErrors(ex.ToString());
            return false;
        }
    }

    public static bool ParseConnectionStringFromConfig()
    {
        try
        {
            String connString = CarSaleMan.Properties.Settings.Default.csmConnectionString;

            String[] items = Regex.Split(connString, @";");

            foreach (String item in items)
            {
                String[] values = Regex.Split(item, @"=");

                if (values.Length > 1)
                {
                    if (values[0].Equals("Data Source"))
                        serverAddress = values[1];
                    if (values[0].Equals("Password"))
                        dbPassword = values[1];
                }
            }
            return true;
        }
        catch (System.Exception ex)
        {
            CommonMisc.LogErrors(ex.ToString());
            return false;
        }
    }

    public static String GetServerAddress()
    {
        return serverAddress;
    }

    public static void SetServerAddress(String name)
    {
        serverAddress = name;
    }

    public static String GetDbPassword()
    {
        return dbPassword;
    }

    public static void SetDbPassword(String pass)
    {
        dbPassword = pass;
    }

    public static String GetUserName()
    {
        return logonUserName;
    }

    public static void SetUserName(String name)
    {
        logonUserName = name;
    }

    public static bool GetAutoLogon()
    {
        return autoLogon;
    }

    public static void SetAutoLogon(bool auto)
    {
        autoLogon = auto;
    }

    #endregion

    #region Private Methods

    
    private static String SetConnectionString()
    {
        String ConString = "Data Source=" + serverAddress + ";" +
            "Initial Catalog=csm;Persist Security Info=True;" +
            "User ID=sa;" +
            "Password=" + dbPassword;

        CarSaleMan.Properties.Settings.Default.csmConnectionString = ConString;
        return ConString;
    }

    private static SqlConnection GetDBConnection()
    {
        SqlConnection conn = null;

        try
        {
            if (string.IsNullOrEmpty(serverAddress))
                return null;

            conn = new SqlConnection(SetConnectionString());
            return conn;
        }
        catch (System.Exception ex)
        {
            CommonMisc.LogErrors(ex.ToString());
            throw;
        }
    }

    #endregion

}

public static class Global
{
    #region FILE_REFFERENCE_KEY
    public const String STR_CARSERIES = "车型大类";
    #endregion

    #region PASS_ENCRYPT_KEY
    public const String STR_DES_KEY = "123456ABCDEF";
    #endregion

    #region CAR_OPERATION
    public const String CAR_PURCHASE = "车辆采购";
    public const String CAR_STOREIN = "车辆入库";
    public const String CAR_STORECHANGE = "车辆转库";
    public const String CAR_STOREOUT = "车辆出库";
    #endregion

    #region SEARCH_KIND
    public const int SEARCH_NONE = 0;
    public const int SEARCH_ONROADCAR = 1;
    public const int SEARCH_STOREIN_ONROADCAR = 2;
    public const int SEARCH_STOREIN_STOREIN = 3;
    public const int SEARCH_STORECHANGE = 4;
    public const int SEARCH_STOREOUT_STOREIN = 5;
    public const int SEARCH_STOREOUT_STOREOUT = 6;
    public const int SEARCH_SPECCAR = 7;
    public const int SEARCH_SEARCH_ONROADCAR = 11;
    public const int SEARCH_SEARCH_STOREIN = 12;
    public const int SEARCH_SEARCH_STOREOUT = 13;
    public const int SEARCH_FINANCE_STORE = 21;
    public const int SEARCH_SETTING_CARTYPE = 31;
    public const int SEARCH_SETTING_COMPANY = 32;
    #endregion

    #region USER_LOGIN_MANAGEMENT
    public static int LOGIN_USERID = 0;
    public static String LOGIN_USERNAME = "";
    public static String LOGIN_PASS = "";
    public static String LOGIN_DEPARTMENTCODE = "";

    public static Dictionary<String, String> PERMISSION_LIST = new Dictionary<String, String>();
    public static List<String> MENU_LIST = new List<String>();

    #endregion
}

public static class CsmEncrypt
{
    #region Fields and Properties
    private static byte[] AESKeys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };
    private static byte[] DESKeys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
    #endregion

    #region Constructors
    #endregion

    #region Public Methods
    //         internal static String AESEncode(String encryptString, String encryptKey)
    //         {
    //             try
    //             {
    //                 encryptKey = encryptKey.Substring(0, 32);
    //                 encryptKey = encryptKey.PadRight(32, ' ');
    //                 RijndaelManaged rijndaelProvider = new RijndaelManaged();
    //                 rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
    //                 rijndaelProvider.IV = AESKeys;
    //                 ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();
    //                 byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
    //                 byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);
    //                 return Convert.ToBase64String(encryptedData);
    //             }
    //             catch (System.Exception ex)
    //             {
    //                 ComMisc.LogErrors(ex.ToString());
    //                 throw;
    //             }            
    //         }
    //         internal static String AESDecode(String decryptString, String decryptKey)
    //         {
    //             try
    //             {
    //                 decryptKey = decryptKey.Substring(0, 32);
    //                 decryptKey = decryptKey.PadRight(32, ' ');
    //                 RijndaelManaged rijndaelProvider = new RijndaelManaged();
    //                 rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
    //                 rijndaelProvider.IV = AESKeys;
    //                 ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();
    //                 byte[] inputData = Convert.FromBase64String(decryptString);
    //                 byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);
    //                 return Encoding.UTF8.GetString(decryptedData);
    //             }
    //             catch (Exception ex)
    //             {
    //                 ComMisc.LogErrors(ex.ToString());
    //                 throw;
    //             }
    //         }

    internal static String DESEncode(String encryptString, String encryptKey)
    {
        encryptKey = encryptKey.Substring(0, 8);
        encryptKey = encryptKey.PadRight(8, ' ');
        byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
        byte[] rgbIV = DESKeys;
        byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
        DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
        MemoryStream mStream = new MemoryStream();
        CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        cStream.FlushFinalBlock();
        cStream.Dispose();
        return Convert.ToBase64String(mStream.ToArray());
    }

    internal static String DESDecode(String decryptString, String decryptKey)
    {
        try
        {
            decryptKey = decryptKey.Substring(0, 8);
            decryptKey = decryptKey.PadRight(8, ' ');
            byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
            byte[] rgbIV = DESKeys;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
        catch (System.Exception ex)
        {
            CommonMisc.LogErrors(ex.ToString());
        }

        return null;
    }

    //         internal static String Base64Encode(String encryptString)
    //         {
    //             byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(encryptString);
    //             return Convert.ToBase64String(encbuff);
    //         }
    // 
    //         internal static String Base64Decode(String decryptString)
    //         {
    //             byte[] decbuff = Convert.FromBase64String(decryptString);
    //             return System.Text.Encoding.UTF8.GetString(decbuff);
    //         }


    //         internal static String RSADecrypt(String s, String key)
    //         {
    //             String result = null;
    //             if (string.IsNullOrEmpty(s)) throw new ArgumentException("An empty String value cannot be encrypted.");
    //             if (string.IsNullOrEmpty(key)) throw new ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");
    //             CspParameters cspp = new CspParameters();
    //             cspp.KeyContainerName = key;
    //             RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
    //             rsa.PersistKeyInCsp = true;
    //             string[] decryptArray = s.Split(new string[] { "-" }, StringSplitOptions.None);
    //             byte[] decryptByteArray = Array.ConvertAll<string, byte>(decryptArray, StringToByte);
    //             byte[] bytes = rsa.Decrypt(decryptByteArray, true);
    //             result = System.Text.UTF8Encoding.UTF8.GetString(bytes);
    //             return result;
    //         }
    // 
    //         internal static String RSAEncrypt(String s, String key)
    //         {
    //             if (string.IsNullOrEmpty(s)) throw new ArgumentException("An empty String value cannot be encrypted.");
    //             if (string.IsNullOrEmpty(key)) throw new ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");
    //             CspParameters cspp = new CspParameters();
    //             cspp.KeyContainerName = key;
    //             RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
    //             rsa.PersistKeyInCsp = true;
    //             byte[] bytes = rsa.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(s), true);
    //             return BitConverter.ToString(bytes);
    //         }

    //         internal static String MD5(String str)
    //         {
    //             String cl1 = str;
    //             String pwd = "";
    //             System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
    //             byte[] s = md5.ComputeHash(Encoding.Unicode.GetBytes(cl1));
    //             for (int i = 0; i < s.Length; i++)
    //             {
    //                 pwd += s[i].ToString("x");
    //             }
    //             return pwd;
    //         }
    // 
    //         internal static String MD5CSP(String encypStr)
    //         {
    //             String retStr;
    //             MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();
    //             byte[] inputBye;
    //             byte[] outputBye;
    //             inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
    //             outputBye = m5.ComputeHash(inputBye);
    //             retStr = System.BitConverter.ToString(outputBye);
    //             retStr = retStr.Replace("-", "").ToLower();
    //             return retStr;
    //         }

    //         internal static String SHA256(String str)
    //         {
    //             byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
    //             SHA256Managed Sha256 = new SHA256Managed();
    //             byte[] Result = Sha256.ComputeHash(SHA256Data);
    //             return Convert.ToBase64String(Result);
    //         }
    #endregion

    #region Private Methods
    //         internal static byte StringToByte(String s)
    //         {
    //             return Convert.ToByte(byte.Parse(s, System.Globalization.NumberStyles.HexNumber));
    //         }

    #endregion

    #region Event Methods
    #endregion
}

public static class BitmapRegion
{
    #region Fields and Properties
    #endregion

    #region Constructors
    #endregion

    #region Public Methods
    /// <summary>
    /// Create and apply the region on the supplied control
    /// </summary>
    /// <param name="control">The Control object to apply the region to</param>
    /// <param name="bitmap">The Bitmap object to create the region from</param>
    internal static void CreateControlRegion(Control ctrl, Bitmap bitmap)
    {
        // Return if control and bitmap are null
        if (ctrl == null || bitmap == null)
            return;

        // Set our control's size to be the same as the bitmap
        ctrl.Width = bitmap.Width;
        ctrl.Height = bitmap.Height;

        // Check if we are dealing with Form here
        Form form = ctrl as Form;
        if (form != null)
        {
            form.Width += 15;
            form.Height += 35;

            form.FormBorderStyle = FormBorderStyle.None;
            form.BackgroundImage = bitmap;

            // Calculate the graphics path based on the bitmap supplied
            GraphicsPath graphicsPath = CalculateControlGraphicsPath(bitmap);

            // Apply new region
            form.Region = new Region(graphicsPath);
        }

        // Check if we are dealing with Button here
        Button button = ctrl as Button;
        if (button != null)
        {
            // Do not show button text
            button.Text = String.Empty;

            // Change cursor to hand when over button
            button.Cursor = Cursors.Hand;

            // Set background image of button
            button.BackgroundImage = bitmap;

            // Calculate the graphics path based on the bitmap supplied
            GraphicsPath graphicsPath = CalculateControlGraphicsPath(bitmap);

            // Apply new region
            button.Region = new Region(graphicsPath);
        }

        Label label = ctrl as Label;
        if (label != null)
        {
            // Do not show button text
            label.Text = String.Empty;

            // Change cursor to hand when over button
            label.Cursor = Cursors.Hand;

            // Set background image of button
            label.BackgroundImage = bitmap;

            // Calculate the graphics path based on the bitmap supplied
            GraphicsPath graphicsPath = CalculateControlGraphicsPath(bitmap);

            // Apply new region
            label.Region = new Region(graphicsPath);
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Calculate the graphics path that representing the figure in the bitmap 
    /// excluding the transparent color which is the top left pixel.
    /// </summary>
    /// <param name="bitmap">The Bitmap object to calculate our graphics path from</param>
    /// <returns>Calculated graphics path</returns>
    private static GraphicsPath CalculateControlGraphicsPath(Bitmap bitmap)
    {
        // Create GraphicsPath for our bitmap calculation
        GraphicsPath graphicsPath = new GraphicsPath();

        // Use the top left pixel as our transparent color
        Color colorTransparent = bitmap.GetPixel(0, 0);

        // This is to store the column value where an opaque pixel is first found.
        // This value will determine where we start scanning for trailing opaque pixels.
        int colOpaquePixel = 0;

        // Go through all rows (Y axis)
        for (int row = 0; row < bitmap.Height; row++)
        {
            // Reset value
            colOpaquePixel = 0;

            // Go through all columns (X axis)
            for (int col = 0; col < bitmap.Width; col++)
            {
                // If this is an opaque pixel, mark it and search for anymore trailing behind
                if (bitmap.GetPixel(col, row) != colorTransparent)
                {
                    // Opaque pixel found, mark current position
                    colOpaquePixel = col;

                    // Create another variable to set the current pixel position
                    int colNext = col;

                    // Starting from current found opaque pixel, search for anymore opaque pixels 
                    // trailing behind, until a transparent pixel is found or minimum width is reached
                    for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                        if (bitmap.GetPixel(colNext, row) == colorTransparent)
                            break;

                    // Form a rectangle for line of opaque pixels found and add it to our graphics path
                    graphicsPath.AddRectangle(new Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1));

                    // No need to scan the line of opaque pixels just found
                    col = colNext;
                }
            }
        }

        // Return calculated graphics path
        return graphicsPath;
    }
    #endregion

    #region Event Methods
    #endregion
}

public static class Permission
{
    #region Fields and Properties

    public static Dictionary<String, String> menuList = new Dictionary<String, String>();

    #endregion

    #region Public Methods

    public static void SetMenuPermission(MenuStrip menu)
    {
        try
        {
            foreach (ToolStripItem item in menu.Items)
            {
                ToolStripMenuItem mainmenu = (ToolStripMenuItem)item;

                foreach (ToolStripItem item2 in mainmenu.DropDownItems)
                {
                    String name = item2.Text;

                    if (name.Length == 0)
                        continue;

                    String realname = name.Trim();

                    if (name.IndexOf("(") >= 0)
                        realname = name.Substring(0, name.IndexOf("("));
                    realname = realname.Trim();

                    Global.MENU_LIST.Add(realname);

                    if (Global.PERMISSION_LIST.ContainsKey(realname) == true)
                    {
                        if (Global.PERMISSION_LIST[realname].Equals("不可能") == true)
                        {
                            item2.Enabled = false;
                        }
                        else
                        {
                            item2.Enabled = true;
                        }
                    }
                    else
                    {
                        item2.Enabled = false;
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            CommonMisc.LogErrors(ex.Message);
        }
    }

    public static bool GetFuncPermission(String func)
    {
        try
        {
            if (Global.PERMISSION_LIST.ContainsKey(func) == true)
            {
                if (Global.PERMISSION_LIST[func].Equals("读写") == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch (System.Exception ex)
        {
            CommonMisc.LogErrors(ex.Message);
            return false;
        }
    }
    #endregion

    #region Private Methods
    #endregion
}

public class StoreChange
{
    public int chagneid;
    public String batchno;
    public String vin;
    public String storeplace;
    public String actionkind;
    public String actiondate;
    public String actionpay;
    public String settlementname;
    public String handlername;
    public String repairstate;
    public String reservestate;
    public String remark;
}