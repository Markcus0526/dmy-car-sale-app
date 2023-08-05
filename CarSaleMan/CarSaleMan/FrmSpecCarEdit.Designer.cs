namespace CarSaleMan
{
    partial class FrmSpecCarEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSpecCarEdit));
            this.c1InputPanel1 = new C1.Win.C1InputPanel.C1InputPanel();
            this.tblStoreoutBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.navStoreout = new C1.Win.C1InputPanel.InputDataNavigator();
            this.sepLine = new C1.Win.C1InputPanel.InputSeparator();
            this.lblbatchno = new C1.Win.C1InputPanel.InputLabel();
            this.txtbatchno = new C1.Win.C1InputPanel.InputTextBox();
            this.lblsalekind = new C1.Win.C1InputPanel.InputLabel();
            this.txtsalekind = new C1.Win.C1InputPanel.InputTextBox();
            this.lbloutdate = new C1.Win.C1InputPanel.InputLabel();
            this.dtpoutdate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lblcustomername = new C1.Win.C1InputPanel.InputLabel();
            this.txtcustomername = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcustomerphoneno = new C1.Win.C1InputPanel.InputLabel();
            this.txtcustomerphoneno = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcustomerjobkind = new C1.Win.C1InputPanel.InputLabel();
            this.txtcustomerjobkind = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcarno = new C1.Win.C1InputPanel.InputLabel();
            this.txtcarno = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcarspeckind = new C1.Win.C1InputPanel.InputLabel();
            this.txtcarspeckind = new C1.Win.C1InputPanel.InputTextBox();
            this.lblremark = new C1.Win.C1InputPanel.InputLabel();
            this.txtremark = new C1.Win.C1InputPanel.InputTextBox();
            this.lblonroadid = new C1.Win.C1InputPanel.InputLabel();
            this.numonroadid = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblsalecompany = new C1.Win.C1InputPanel.InputLabel();
            this.txtsalecompany = new C1.Win.C1InputPanel.InputTextBox();
            this.lbloutbillno = new C1.Win.C1InputPanel.InputLabel();
            this.txtoutbillno = new C1.Win.C1InputPanel.InputTextBox();
            this.lblsettlementname = new C1.Win.C1InputPanel.InputLabel();
            this.txtsettlementname = new C1.Win.C1InputPanel.InputTextBox();
            this.lblhandlername = new C1.Win.C1InputPanel.InputLabel();
            this.txthandlername = new C1.Win.C1InputPanel.InputTextBox();
            this.lblsaleplace = new C1.Win.C1InputPanel.InputLabel();
            this.txtsaleplace = new C1.Win.C1InputPanel.InputTextBox();
            this.lblincarkind = new C1.Win.C1InputPanel.InputLabel();
            this.txtincarkind = new C1.Win.C1InputPanel.InputTextBox();
            this.lblsaleregion = new C1.Win.C1InputPanel.InputLabel();
            this.txtsaleregion = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcustomeraddress = new C1.Win.C1InputPanel.InputLabel();
            this.txtcustomeraddress = new C1.Win.C1InputPanel.InputTextBox();
            this.lblisreport = new C1.Win.C1InputPanel.InputLabel();
            this.txtisreport = new C1.Win.C1InputPanel.InputTextBox();
            this.lblvotecost = new C1.Win.C1InputPanel.InputLabel();
            this.numvotecost = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblinprice = new C1.Win.C1InputPanel.InputLabel();
            this.numinprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblotherprice1 = new C1.Win.C1InputPanel.InputLabel();
            this.numotherprice1 = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblotherprice2 = new C1.Win.C1InputPanel.InputLabel();
            this.numotherprice2 = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblotherprice3 = new C1.Win.C1InputPanel.InputLabel();
            this.numotherprice3 = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblotherprice4 = new C1.Win.C1InputPanel.InputLabel();
            this.numotherprice4 = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblinterestprice = new C1.Win.C1InputPanel.InputLabel();
            this.numinterestprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblprofitval = new C1.Win.C1InputPanel.InputLabel();
            this.numprofitval = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblspecprofitval = new C1.Win.C1InputPanel.InputLabel();
            this.numspecprofitval = new C1.Win.C1InputPanel.InputNumericBox();
            this.lbloutprice = new C1.Win.C1InputPanel.InputLabel();
            this.numoutprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblpricediff = new C1.Win.C1InputPanel.InputLabel();
            this.numpricediff = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblisbill = new C1.Win.C1InputPanel.InputLabel();
            this.txtisbill = new C1.Win.C1InputPanel.InputTextBox();
            this.lblbilloutdate = new C1.Win.C1InputPanel.InputLabel();
            this.dtpbilloutdate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lblispayment = new C1.Win.C1InputPanel.InputLabel();
            this.txtispayment = new C1.Win.C1InputPanel.InputTextBox();
            this.lblpaymentdate = new C1.Win.C1InputPanel.InputLabel();
            this.dtppaymentdate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lblissend = new C1.Win.C1InputPanel.InputLabel();
            this.txtissend = new C1.Win.C1InputPanel.InputTextBox();
            this.lblsenddate = new C1.Win.C1InputPanel.InputLabel();
            this.dtpsenddate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lbluid = new C1.Win.C1InputPanel.InputLabel();
            this.numuid = new C1.Win.C1InputPanel.InputNumericBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tblStoreoutTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storeoutTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.c1InputPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreoutBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            this.SuspendLayout();
            // 
            // c1InputPanel1
            // 
            this.c1InputPanel1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.c1InputPanel1.DataSource = this.tblStoreoutBindingSource;
            this.c1InputPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.c1InputPanel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.c1InputPanel1.Items.Add(this.navStoreout);
            this.c1InputPanel1.Items.Add(this.sepLine);
            this.c1InputPanel1.Items.Add(this.lblbatchno);
            this.c1InputPanel1.Items.Add(this.txtbatchno);
            this.c1InputPanel1.Items.Add(this.lblsalekind);
            this.c1InputPanel1.Items.Add(this.txtsalekind);
            this.c1InputPanel1.Items.Add(this.lbloutdate);
            this.c1InputPanel1.Items.Add(this.dtpoutdate);
            this.c1InputPanel1.Items.Add(this.lblcustomername);
            this.c1InputPanel1.Items.Add(this.txtcustomername);
            this.c1InputPanel1.Items.Add(this.lblcustomerphoneno);
            this.c1InputPanel1.Items.Add(this.txtcustomerphoneno);
            this.c1InputPanel1.Items.Add(this.lblcustomerjobkind);
            this.c1InputPanel1.Items.Add(this.txtcustomerjobkind);
            this.c1InputPanel1.Items.Add(this.lblcarno);
            this.c1InputPanel1.Items.Add(this.txtcarno);
            this.c1InputPanel1.Items.Add(this.lblcarspeckind);
            this.c1InputPanel1.Items.Add(this.txtcarspeckind);
            this.c1InputPanel1.Items.Add(this.lblremark);
            this.c1InputPanel1.Items.Add(this.txtremark);
            this.c1InputPanel1.Items.Add(this.lblonroadid);
            this.c1InputPanel1.Items.Add(this.numonroadid);
            this.c1InputPanel1.Items.Add(this.lblsalecompany);
            this.c1InputPanel1.Items.Add(this.txtsalecompany);
            this.c1InputPanel1.Items.Add(this.lbloutbillno);
            this.c1InputPanel1.Items.Add(this.txtoutbillno);
            this.c1InputPanel1.Items.Add(this.lblsettlementname);
            this.c1InputPanel1.Items.Add(this.txtsettlementname);
            this.c1InputPanel1.Items.Add(this.lblhandlername);
            this.c1InputPanel1.Items.Add(this.txthandlername);
            this.c1InputPanel1.Items.Add(this.lblsaleplace);
            this.c1InputPanel1.Items.Add(this.txtsaleplace);
            this.c1InputPanel1.Items.Add(this.lblincarkind);
            this.c1InputPanel1.Items.Add(this.txtincarkind);
            this.c1InputPanel1.Items.Add(this.lblsaleregion);
            this.c1InputPanel1.Items.Add(this.txtsaleregion);
            this.c1InputPanel1.Items.Add(this.lblcustomeraddress);
            this.c1InputPanel1.Items.Add(this.txtcustomeraddress);
            this.c1InputPanel1.Items.Add(this.lblisreport);
            this.c1InputPanel1.Items.Add(this.txtisreport);
            this.c1InputPanel1.Items.Add(this.lblvotecost);
            this.c1InputPanel1.Items.Add(this.numvotecost);
            this.c1InputPanel1.Items.Add(this.lblinprice);
            this.c1InputPanel1.Items.Add(this.numinprice);
            this.c1InputPanel1.Items.Add(this.lblotherprice1);
            this.c1InputPanel1.Items.Add(this.numotherprice1);
            this.c1InputPanel1.Items.Add(this.lblotherprice2);
            this.c1InputPanel1.Items.Add(this.numotherprice2);
            this.c1InputPanel1.Items.Add(this.lblotherprice3);
            this.c1InputPanel1.Items.Add(this.numotherprice3);
            this.c1InputPanel1.Items.Add(this.lblotherprice4);
            this.c1InputPanel1.Items.Add(this.numotherprice4);
            this.c1InputPanel1.Items.Add(this.lblinterestprice);
            this.c1InputPanel1.Items.Add(this.numinterestprice);
            this.c1InputPanel1.Items.Add(this.lblprofitval);
            this.c1InputPanel1.Items.Add(this.numprofitval);
            this.c1InputPanel1.Items.Add(this.lblspecprofitval);
            this.c1InputPanel1.Items.Add(this.numspecprofitval);
            this.c1InputPanel1.Items.Add(this.lbloutprice);
            this.c1InputPanel1.Items.Add(this.numoutprice);
            this.c1InputPanel1.Items.Add(this.lblpricediff);
            this.c1InputPanel1.Items.Add(this.numpricediff);
            this.c1InputPanel1.Items.Add(this.lblisbill);
            this.c1InputPanel1.Items.Add(this.txtisbill);
            this.c1InputPanel1.Items.Add(this.lblbilloutdate);
            this.c1InputPanel1.Items.Add(this.dtpbilloutdate);
            this.c1InputPanel1.Items.Add(this.lblispayment);
            this.c1InputPanel1.Items.Add(this.txtispayment);
            this.c1InputPanel1.Items.Add(this.lblpaymentdate);
            this.c1InputPanel1.Items.Add(this.dtppaymentdate);
            this.c1InputPanel1.Items.Add(this.lblissend);
            this.c1InputPanel1.Items.Add(this.txtissend);
            this.c1InputPanel1.Items.Add(this.lblsenddate);
            this.c1InputPanel1.Items.Add(this.dtpsenddate);
            this.c1InputPanel1.Items.Add(this.lbluid);
            this.c1InputPanel1.Items.Add(this.numuid);
            this.c1InputPanel1.Location = new System.Drawing.Point(0, 0);
            this.c1InputPanel1.Name = "c1InputPanel1";
            this.c1InputPanel1.Size = new System.Drawing.Size(541, 277);
            this.c1InputPanel1.TabIndex = 0;
            this.c1InputPanel1.VisualStyle = C1.Win.C1InputPanel.VisualStyle.Office2010Blue;
            // 
            // tblStoreoutBindingSource
            // 
            this.tblStoreoutBindingSource.DataMember = "tbl_storeout";
            this.tblStoreoutBindingSource.DataSource = this.cmsDB;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // navStoreout
            // 
            this.navStoreout.AddNewImage = ((System.Drawing.Image)(resources.GetObject("navStoreout.AddNewImage")));
            this.navStoreout.AddNewToolTip = "Add New";
            this.navStoreout.ApplyImage = global::CarSaleMan.Properties.Resources.btnCheck;
            this.navStoreout.ApplyToolTip = "Apply Changes";
            this.navStoreout.CancelImage = global::CarSaleMan.Properties.Resources.btnStop;
            this.navStoreout.CancelToolTip = "Cancel Changes";
            this.navStoreout.DataSource = this.tblStoreoutBindingSource;
            this.navStoreout.DeleteImage = ((System.Drawing.Image)(resources.GetObject("navStoreout.DeleteImage")));
            this.navStoreout.DeleteToolTip = "Delete";
            this.navStoreout.EditImage = ((System.Drawing.Image)(resources.GetObject("navStoreout.EditImage")));
            this.navStoreout.EditToolTip = "Edit";
            this.navStoreout.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Center;
            this.navStoreout.MoveFirstImage = global::CarSaleMan.Properties.Resources.btnFirst;
            this.navStoreout.MoveFirstToolTip = "Move First";
            this.navStoreout.MoveLastImage = global::CarSaleMan.Properties.Resources.btnLast;
            this.navStoreout.MoveLastToolTip = "Move Last";
            this.navStoreout.MoveNextImage = global::CarSaleMan.Properties.Resources.btnRight;
            this.navStoreout.MoveNextToolTip = "Move Next";
            this.navStoreout.MovePreviousImage = global::CarSaleMan.Properties.Resources.btnLeft;
            this.navStoreout.MovePreviousToolTip = "Move Previous";
            this.navStoreout.Name = "navStoreout";
            this.navStoreout.NavigatorItems = ((C1.Win.C1InputPanel.InputNavigatorItems)((((((((C1.Win.C1InputPanel.InputNavigatorItems.MoveFirstButton | C1.Win.C1InputPanel.InputNavigatorItems.MovePreviousButton)
                        | C1.Win.C1InputPanel.InputNavigatorItems.PositionInputBox)
                        | C1.Win.C1InputPanel.InputNavigatorItems.CountLabel)
                        | C1.Win.C1InputPanel.InputNavigatorItems.MoveNextButton)
                        | C1.Win.C1InputPanel.InputNavigatorItems.MoveLastButton)
                        | C1.Win.C1InputPanel.InputNavigatorItems.ApplyButton)
                        | C1.Win.C1InputPanel.InputNavigatorItems.CancelButton)));
            this.navStoreout.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.navStoreout.ReloadImage = ((System.Drawing.Image)(resources.GetObject("navStoreout.ReloadImage")));
            this.navStoreout.ReloadToolTip = "Reload Data";
            this.navStoreout.SaveImage = global::CarSaleMan.Properties.Resources.btnSave;
            this.navStoreout.SaveToolTip = "Save Data";
            this.navStoreout.ShowSaveButton = true;
            this.navStoreout.Width = 522;
            // 
            // sepLine
            // 
            this.sepLine.Height = 11;
            this.sepLine.Name = "sepLine";
            this.sepLine.Width = 520;
            // 
            // lblbatchno
            // 
            this.lblbatchno.Name = "lblbatchno";
            this.lblbatchno.Padding = new System.Windows.Forms.Padding(5);
            this.lblbatchno.Text = "出库单号:";
            this.lblbatchno.Width = 80;
            // 
            // txtbatchno
            // 
            this.txtbatchno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtbatchno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "batchno", true));
            this.txtbatchno.Enabled = false;
            this.txtbatchno.Name = "txtbatchno";
            this.txtbatchno.Padding = new System.Windows.Forms.Padding(5);
            this.txtbatchno.Width = 160;
            // 
            // lblsalekind
            // 
            this.lblsalekind.Name = "lblsalekind";
            this.lblsalekind.Padding = new System.Windows.Forms.Padding(40, 5, 5, 5);
            this.lblsalekind.Text = "销售方式:";
            this.lblsalekind.Width = 100;
            // 
            // txtsalekind
            // 
            this.txtsalekind.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "salekind", true));
            this.txtsalekind.Enabled = false;
            this.txtsalekind.Name = "txtsalekind";
            this.txtsalekind.Padding = new System.Windows.Forms.Padding(5);
            this.txtsalekind.Width = 160;
            // 
            // lbloutdate
            // 
            this.lbloutdate.Name = "lbloutdate";
            this.lbloutdate.Padding = new System.Windows.Forms.Padding(5);
            this.lbloutdate.Text = "出库日期:";
            this.lbloutdate.Width = 80;
            // 
            // dtpoutdate
            // 
            this.dtpoutdate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.dtpoutdate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "outdate", true));
            this.dtpoutdate.Enabled = false;
            this.dtpoutdate.Name = "dtpoutdate";
            this.dtpoutdate.Padding = new System.Windows.Forms.Padding(5);
            this.dtpoutdate.Width = 160;
            // 
            // lblcustomername
            // 
            this.lblcustomername.Name = "lblcustomername";
            this.lblcustomername.Padding = new System.Windows.Forms.Padding(40, 5, 5, 5);
            this.lblcustomername.Text = "客户姓名:";
            this.lblcustomername.Width = 100;
            // 
            // txtcustomername
            // 
            this.txtcustomername.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "customername", true));
            this.txtcustomername.Enabled = false;
            this.txtcustomername.Name = "txtcustomername";
            this.txtcustomername.Padding = new System.Windows.Forms.Padding(5);
            this.txtcustomername.Width = 160;
            // 
            // lblcustomerphoneno
            // 
            this.lblcustomerphoneno.Name = "lblcustomerphoneno";
            this.lblcustomerphoneno.Padding = new System.Windows.Forms.Padding(5);
            this.lblcustomerphoneno.Text = "电  话:";
            this.lblcustomerphoneno.Width = 80;
            // 
            // txtcustomerphoneno
            // 
            this.txtcustomerphoneno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtcustomerphoneno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "customerphoneno", true));
            this.txtcustomerphoneno.Enabled = false;
            this.txtcustomerphoneno.Name = "txtcustomerphoneno";
            this.txtcustomerphoneno.Padding = new System.Windows.Forms.Padding(5);
            this.txtcustomerphoneno.Width = 160;
            // 
            // lblcustomerjobkind
            // 
            this.lblcustomerjobkind.Name = "lblcustomerjobkind";
            this.lblcustomerjobkind.Padding = new System.Windows.Forms.Padding(40, 5, 5, 5);
            this.lblcustomerjobkind.Text = "行  业:";
            this.lblcustomerjobkind.Width = 100;
            // 
            // txtcustomerjobkind
            // 
            this.txtcustomerjobkind.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "customerjobkind", true));
            this.txtcustomerjobkind.Enabled = false;
            this.txtcustomerjobkind.Name = "txtcustomerjobkind";
            this.txtcustomerjobkind.Padding = new System.Windows.Forms.Padding(5);
            this.txtcustomerjobkind.Width = 160;
            // 
            // lblcarno
            // 
            this.lblcarno.Name = "lblcarno";
            this.lblcarno.Padding = new System.Windows.Forms.Padding(5);
            this.lblcarno.Text = "车牌号:";
            this.lblcarno.Width = 80;
            // 
            // txtcarno
            // 
            this.txtcarno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtcarno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "carno", true));
            this.txtcarno.Name = "txtcarno";
            this.txtcarno.Padding = new System.Windows.Forms.Padding(5);
            this.txtcarno.Width = 160;
            // 
            // lblcarspeckind
            // 
            this.lblcarspeckind.Name = "lblcarspeckind";
            this.lblcarspeckind.Padding = new System.Windows.Forms.Padding(40, 5, 5, 5);
            this.lblcarspeckind.Text = "特种车类型:";
            this.lblcarspeckind.Width = 100;
            // 
            // txtcarspeckind
            // 
            this.txtcarspeckind.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "carspeckind", true));
            this.txtcarspeckind.Enabled = false;
            this.txtcarspeckind.Name = "txtcarspeckind";
            this.txtcarspeckind.Padding = new System.Windows.Forms.Padding(5);
            this.txtcarspeckind.Width = 160;
            // 
            // lblremark
            // 
            this.lblremark.Name = "lblremark";
            this.lblremark.Padding = new System.Windows.Forms.Padding(5);
            this.lblremark.Text = "备  注:";
            this.lblremark.Width = 80;
            // 
            // txtremark
            // 
            this.txtremark.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "remark", true));
            this.txtremark.Name = "txtremark";
            this.txtremark.Padding = new System.Windows.Forms.Padding(5);
            this.txtremark.Width = 427;
            // 
            // lblonroadid
            // 
            this.lblonroadid.Name = "lblonroadid";
            this.lblonroadid.Text = "&onroadid:";
            this.lblonroadid.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblonroadid.Width = 26;
            // 
            // numonroadid
            // 
            this.numonroadid.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numonroadid.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "onroadid", true));
            this.numonroadid.Format = "0";
            this.numonroadid.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numonroadid.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.numonroadid.Name = "numonroadid";
            this.numonroadid.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numonroadid.Width = 19;
            // 
            // lblsalecompany
            // 
            this.lblsalecompany.Name = "lblsalecompany";
            this.lblsalecompany.Text = "&salecompany:";
            this.lblsalecompany.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblsalecompany.Width = 27;
            // 
            // txtsalecompany
            // 
            this.txtsalecompany.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtsalecompany.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "salecompany", true));
            this.txtsalecompany.Name = "txtsalecompany";
            this.txtsalecompany.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtsalecompany.Width = 18;
            // 
            // lbloutbillno
            // 
            this.lbloutbillno.Name = "lbloutbillno";
            this.lbloutbillno.Text = "outb&illno:";
            this.lbloutbillno.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lbloutbillno.Width = 29;
            // 
            // txtoutbillno
            // 
            this.txtoutbillno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtoutbillno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "outbillno", true));
            this.txtoutbillno.Name = "txtoutbillno";
            this.txtoutbillno.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtoutbillno.Width = 19;
            // 
            // lblsettlementname
            // 
            this.lblsettlementname.Name = "lblsettlementname";
            this.lblsettlementname.Text = "s&ettlementname:";
            this.lblsettlementname.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblsettlementname.Width = 28;
            // 
            // txtsettlementname
            // 
            this.txtsettlementname.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtsettlementname.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "settlementname", true));
            this.txtsettlementname.Name = "txtsettlementname";
            this.txtsettlementname.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtsettlementname.Width = 23;
            // 
            // lblhandlername
            // 
            this.lblhandlername.Name = "lblhandlername";
            this.lblhandlername.Text = "&handlername:";
            this.lblhandlername.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblhandlername.Width = 31;
            // 
            // txthandlername
            // 
            this.txthandlername.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txthandlername.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "handlername", true));
            this.txthandlername.Name = "txthandlername";
            this.txthandlername.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txthandlername.Width = 16;
            // 
            // lblsaleplace
            // 
            this.lblsaleplace.Name = "lblsaleplace";
            this.lblsaleplace.Text = "sa&leplace:";
            this.lblsaleplace.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblsaleplace.Width = 28;
            // 
            // txtsaleplace
            // 
            this.txtsaleplace.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtsaleplace.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "saleplace", true));
            this.txtsaleplace.Name = "txtsaleplace";
            this.txtsaleplace.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtsaleplace.Width = 29;
            // 
            // lblincarkind
            // 
            this.lblincarkind.Name = "lblincarkind";
            this.lblincarkind.Text = "i&ncarkind:";
            this.lblincarkind.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblincarkind.Width = 31;
            // 
            // txtincarkind
            // 
            this.txtincarkind.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtincarkind.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "incarkind", true));
            this.txtincarkind.Name = "txtincarkind";
            this.txtincarkind.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtincarkind.Width = 24;
            // 
            // lblsaleregion
            // 
            this.lblsaleregion.Name = "lblsaleregion";
            this.lblsaleregion.Text = "salere&gion:";
            this.lblsaleregion.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblsaleregion.Width = 23;
            // 
            // txtsaleregion
            // 
            this.txtsaleregion.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtsaleregion.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "saleregion", true));
            this.txtsaleregion.Name = "txtsaleregion";
            this.txtsaleregion.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtsaleregion.Width = 29;
            // 
            // lblcustomeraddress
            // 
            this.lblcustomeraddress.Name = "lblcustomeraddress";
            this.lblcustomeraddress.Text = "customeraddress:";
            this.lblcustomeraddress.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblcustomeraddress.Width = 29;
            // 
            // txtcustomeraddress
            // 
            this.txtcustomeraddress.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "customeraddress", true));
            this.txtcustomeraddress.Name = "txtcustomeraddress";
            this.txtcustomeraddress.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtcustomeraddress.Width = 18;
            // 
            // lblisreport
            // 
            this.lblisreport.Name = "lblisreport";
            this.lblisreport.Text = "isreport:";
            this.lblisreport.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblisreport.Width = 22;
            // 
            // txtisreport
            // 
            this.txtisreport.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtisreport.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "isreport", true));
            this.txtisreport.Name = "txtisreport";
            this.txtisreport.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtisreport.Width = 24;
            // 
            // lblvotecost
            // 
            this.lblvotecost.Name = "lblvotecost";
            this.lblvotecost.Text = "&votecost:";
            this.lblvotecost.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblvotecost.Width = 27;
            // 
            // numvotecost
            // 
            this.numvotecost.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numvotecost.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "votecost", true));
            this.numvotecost.Format = "N2";
            this.numvotecost.Name = "numvotecost";
            this.numvotecost.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numvotecost.Width = 24;
            // 
            // lblinprice
            // 
            this.lblinprice.Name = "lblinprice";
            this.lblinprice.Text = "inprice:";
            this.lblinprice.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblinprice.Width = 25;
            // 
            // numinprice
            // 
            this.numinprice.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numinprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "inprice", true));
            this.numinprice.Format = "N2";
            this.numinprice.Name = "numinprice";
            this.numinprice.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numinprice.Width = 25;
            // 
            // lblotherprice1
            // 
            this.lblotherprice1.Name = "lblotherprice1";
            this.lblotherprice1.Text = "otherprice1:";
            this.lblotherprice1.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblotherprice1.Width = 22;
            // 
            // numotherprice1
            // 
            this.numotherprice1.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numotherprice1.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "otherprice1", true));
            this.numotherprice1.Format = "N2";
            this.numotherprice1.Name = "numotherprice1";
            this.numotherprice1.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numotherprice1.Width = 25;
            // 
            // lblotherprice2
            // 
            this.lblotherprice2.Name = "lblotherprice2";
            this.lblotherprice2.Text = "otherprice2:";
            this.lblotherprice2.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblotherprice2.Width = 26;
            // 
            // numotherprice2
            // 
            this.numotherprice2.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numotherprice2.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "otherprice2", true));
            this.numotherprice2.Format = "N2";
            this.numotherprice2.Name = "numotherprice2";
            this.numotherprice2.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numotherprice2.Width = 21;
            // 
            // lblotherprice3
            // 
            this.lblotherprice3.Name = "lblotherprice3";
            this.lblotherprice3.Text = "otherprice3:";
            this.lblotherprice3.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblotherprice3.Width = 26;
            // 
            // numotherprice3
            // 
            this.numotherprice3.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numotherprice3.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "otherprice3", true));
            this.numotherprice3.Format = "N2";
            this.numotherprice3.Name = "numotherprice3";
            this.numotherprice3.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numotherprice3.Width = 22;
            // 
            // lblotherprice4
            // 
            this.lblotherprice4.Name = "lblotherprice4";
            this.lblotherprice4.Text = "otherprice4:";
            this.lblotherprice4.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblotherprice4.Width = 27;
            // 
            // numotherprice4
            // 
            this.numotherprice4.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numotherprice4.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "otherprice4", true));
            this.numotherprice4.Format = "N2";
            this.numotherprice4.Name = "numotherprice4";
            this.numotherprice4.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numotherprice4.Width = 19;
            // 
            // lblinterestprice
            // 
            this.lblinterestprice.Name = "lblinterestprice";
            this.lblinterestprice.Text = "interestprice:";
            this.lblinterestprice.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblinterestprice.Width = 26;
            // 
            // numinterestprice
            // 
            this.numinterestprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "interestprice", true));
            this.numinterestprice.Format = "N2";
            this.numinterestprice.Name = "numinterestprice";
            this.numinterestprice.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numinterestprice.Width = 22;
            // 
            // lblprofitval
            // 
            this.lblprofitval.Name = "lblprofitval";
            this.lblprofitval.Text = "pro&fitval:";
            this.lblprofitval.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblprofitval.Width = 28;
            // 
            // numprofitval
            // 
            this.numprofitval.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numprofitval.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "profitval", true));
            this.numprofitval.Format = "N2";
            this.numprofitval.Name = "numprofitval";
            this.numprofitval.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numprofitval.Width = 25;
            // 
            // lblspecprofitval
            // 
            this.lblspecprofitval.Name = "lblspecprofitval";
            this.lblspecprofitval.Text = "specprofitval:";
            this.lblspecprofitval.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblspecprofitval.Width = 21;
            // 
            // numspecprofitval
            // 
            this.numspecprofitval.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numspecprofitval.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "specprofitval", true));
            this.numspecprofitval.Format = "N2";
            this.numspecprofitval.Name = "numspecprofitval";
            this.numspecprofitval.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numspecprofitval.Width = 26;
            // 
            // lbloutprice
            // 
            this.lbloutprice.Name = "lbloutprice";
            this.lbloutprice.Text = "outprice:";
            this.lbloutprice.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lbloutprice.Width = 27;
            // 
            // numoutprice
            // 
            this.numoutprice.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numoutprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "outprice", true));
            this.numoutprice.Format = "N2";
            this.numoutprice.Name = "numoutprice";
            this.numoutprice.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numoutprice.Width = 17;
            // 
            // lblpricediff
            // 
            this.lblpricediff.Name = "lblpricediff";
            this.lblpricediff.Text = "pricediff:";
            this.lblpricediff.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblpricediff.Width = 26;
            // 
            // numpricediff
            // 
            this.numpricediff.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numpricediff.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "pricediff", true));
            this.numpricediff.Format = "N2";
            this.numpricediff.Name = "numpricediff";
            this.numpricediff.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numpricediff.Width = 22;
            // 
            // lblisbill
            // 
            this.lblisbill.Name = "lblisbill";
            this.lblisbill.Text = "isbill:";
            this.lblisbill.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblisbill.Width = 26;
            // 
            // txtisbill
            // 
            this.txtisbill.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtisbill.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "isbill", true));
            this.txtisbill.Name = "txtisbill";
            this.txtisbill.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtisbill.Width = 26;
            // 
            // lblbilloutdate
            // 
            this.lblbilloutdate.Name = "lblbilloutdate";
            this.lblbilloutdate.Text = "billoutdate:";
            this.lblbilloutdate.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblbilloutdate.Width = 34;
            // 
            // dtpbilloutdate
            // 
            this.dtpbilloutdate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.dtpbilloutdate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "billoutdate", true));
            this.dtpbilloutdate.Name = "dtpbilloutdate";
            this.dtpbilloutdate.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.dtpbilloutdate.Width = 23;
            // 
            // lblispayment
            // 
            this.lblispayment.Name = "lblispayment";
            this.lblispayment.Text = "ispa&yment:";
            this.lblispayment.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblispayment.Width = 26;
            // 
            // txtispayment
            // 
            this.txtispayment.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtispayment.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "ispayment", true));
            this.txtispayment.Name = "txtispayment";
            this.txtispayment.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtispayment.Width = 32;
            // 
            // lblpaymentdate
            // 
            this.lblpaymentdate.Name = "lblpaymentdate";
            this.lblpaymentdate.Text = "paymentdate:";
            this.lblpaymentdate.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblpaymentdate.Width = 26;
            // 
            // dtppaymentdate
            // 
            this.dtppaymentdate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.dtppaymentdate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "paymentdate", true));
            this.dtppaymentdate.Name = "dtppaymentdate";
            this.dtppaymentdate.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.dtppaymentdate.Width = 20;
            // 
            // lblissend
            // 
            this.lblissend.Name = "lblissend";
            this.lblissend.Text = "issend:";
            this.lblissend.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblissend.Width = 26;
            // 
            // txtissend
            // 
            this.txtissend.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtissend.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "issend", true));
            this.txtissend.Name = "txtissend";
            this.txtissend.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtissend.Width = 25;
            // 
            // lblsenddate
            // 
            this.lblsenddate.Name = "lblsenddate";
            this.lblsenddate.Text = "senddate:";
            this.lblsenddate.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblsenddate.Width = 26;
            // 
            // dtpsenddate
            // 
            this.dtpsenddate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.dtpsenddate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "senddate", true));
            this.dtpsenddate.Name = "dtpsenddate";
            this.dtpsenddate.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.dtpsenddate.Width = 20;
            // 
            // lbluid
            // 
            this.lbluid.Name = "lbluid";
            this.lbluid.Text = "&uid:";
            this.lbluid.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lbluid.Width = 29;
            // 
            // numuid
            // 
            this.numuid.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "uid", true));
            this.numuid.Format = "0";
            this.numuid.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numuid.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.numuid.Name = "numuid";
            this.numuid.ReadOnly = true;
            this.numuid.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numuid.Width = 21;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(144, 295);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "追  加";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(325, 295);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "返  回";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tblStoreoutTableAdapter
            // 
            this.tblStoreoutTableAdapter.ClearBeforeFill = true;
            // 
            // FrmSpecCarEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(541, 342);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.c1InputPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSpecCarEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "特种车修改";
            this.Load += new System.EventHandler(this.FrmSpecCarEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1InputPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreoutBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1InputPanel.C1InputPanel c1InputPanel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource tblStoreoutBindingSource;
        private CmsDBTableAdapters.tbl_storeoutTableAdapter tblStoreoutTableAdapter;
        private C1.Win.C1InputPanel.InputDataNavigator navStoreout;
        private C1.Win.C1InputPanel.InputSeparator sepLine;
        private C1.Win.C1InputPanel.InputLabel lbluid;
        private C1.Win.C1InputPanel.InputNumericBox numuid;
        private C1.Win.C1InputPanel.InputLabel lblbatchno;
        private C1.Win.C1InputPanel.InputTextBox txtbatchno;
        private C1.Win.C1InputPanel.InputLabel lblonroadid;
        private C1.Win.C1InputPanel.InputNumericBox numonroadid;
        private C1.Win.C1InputPanel.InputLabel lblsalecompany;
        private C1.Win.C1InputPanel.InputTextBox txtsalecompany;
        private C1.Win.C1InputPanel.InputLabel lbloutbillno;
        private C1.Win.C1InputPanel.InputTextBox txtoutbillno;
        private C1.Win.C1InputPanel.InputLabel lblsalekind;
        private C1.Win.C1InputPanel.InputTextBox txtsalekind;
        private C1.Win.C1InputPanel.InputLabel lblsettlementname;
        private C1.Win.C1InputPanel.InputTextBox txtsettlementname;
        private C1.Win.C1InputPanel.InputLabel lblhandlername;
        private C1.Win.C1InputPanel.InputTextBox txthandlername;
        private C1.Win.C1InputPanel.InputLabel lblsaleplace;
        private C1.Win.C1InputPanel.InputTextBox txtsaleplace;
        private C1.Win.C1InputPanel.InputLabel lblincarkind;
        private C1.Win.C1InputPanel.InputTextBox txtincarkind;
        private C1.Win.C1InputPanel.InputLabel lbloutdate;
        private C1.Win.C1InputPanel.InputDatePicker dtpoutdate;
        private C1.Win.C1InputPanel.InputLabel lblcustomername;
        private C1.Win.C1InputPanel.InputTextBox txtcustomername;
        private C1.Win.C1InputPanel.InputLabel lblcustomerphoneno;
        private C1.Win.C1InputPanel.InputTextBox txtcustomerphoneno;
        private C1.Win.C1InputPanel.InputLabel lblcustomerjobkind;
        private C1.Win.C1InputPanel.InputTextBox txtcustomerjobkind;
        private C1.Win.C1InputPanel.InputLabel lblsaleregion;
        private C1.Win.C1InputPanel.InputTextBox txtsaleregion;
        private C1.Win.C1InputPanel.InputLabel lblcustomeraddress;
        private C1.Win.C1InputPanel.InputTextBox txtcustomeraddress;
        private C1.Win.C1InputPanel.InputLabel lblcarno;
        private C1.Win.C1InputPanel.InputTextBox txtcarno;
        private C1.Win.C1InputPanel.InputLabel lblcarspeckind;
        private C1.Win.C1InputPanel.InputTextBox txtcarspeckind;
        private C1.Win.C1InputPanel.InputLabel lblisreport;
        private C1.Win.C1InputPanel.InputTextBox txtisreport;
        private C1.Win.C1InputPanel.InputLabel lblvotecost;
        private C1.Win.C1InputPanel.InputNumericBox numvotecost;
        private C1.Win.C1InputPanel.InputLabel lblinprice;
        private C1.Win.C1InputPanel.InputNumericBox numinprice;
        private C1.Win.C1InputPanel.InputLabel lblotherprice1;
        private C1.Win.C1InputPanel.InputNumericBox numotherprice1;
        private C1.Win.C1InputPanel.InputLabel lblotherprice2;
        private C1.Win.C1InputPanel.InputNumericBox numotherprice2;
        private C1.Win.C1InputPanel.InputLabel lblotherprice3;
        private C1.Win.C1InputPanel.InputNumericBox numotherprice3;
        private C1.Win.C1InputPanel.InputLabel lblotherprice4;
        private C1.Win.C1InputPanel.InputNumericBox numotherprice4;
        private C1.Win.C1InputPanel.InputLabel lblinterestprice;
        private C1.Win.C1InputPanel.InputNumericBox numinterestprice;
        private C1.Win.C1InputPanel.InputLabel lblprofitval;
        private C1.Win.C1InputPanel.InputNumericBox numprofitval;
        private C1.Win.C1InputPanel.InputLabel lblspecprofitval;
        private C1.Win.C1InputPanel.InputNumericBox numspecprofitval;
        private C1.Win.C1InputPanel.InputLabel lbloutprice;
        private C1.Win.C1InputPanel.InputNumericBox numoutprice;
        private C1.Win.C1InputPanel.InputLabel lblpricediff;
        private C1.Win.C1InputPanel.InputNumericBox numpricediff;
        private C1.Win.C1InputPanel.InputLabel lblisbill;
        private C1.Win.C1InputPanel.InputTextBox txtisbill;
        private C1.Win.C1InputPanel.InputLabel lblbilloutdate;
        private C1.Win.C1InputPanel.InputDatePicker dtpbilloutdate;
        private C1.Win.C1InputPanel.InputLabel lblispayment;
        private C1.Win.C1InputPanel.InputTextBox txtispayment;
        private C1.Win.C1InputPanel.InputLabel lblpaymentdate;
        private C1.Win.C1InputPanel.InputDatePicker dtppaymentdate;
        private C1.Win.C1InputPanel.InputLabel lblissend;
        private C1.Win.C1InputPanel.InputTextBox txtissend;
        private C1.Win.C1InputPanel.InputLabel lblsenddate;
        private C1.Win.C1InputPanel.InputDatePicker dtpsenddate;
        private C1.Win.C1InputPanel.InputLabel lblremark;
        private C1.Win.C1InputPanel.InputTextBox txtremark;

    }
}