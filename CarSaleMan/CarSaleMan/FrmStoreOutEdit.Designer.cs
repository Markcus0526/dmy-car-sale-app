namespace CarSaleMan
{
    partial class FrmStoreOutEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStoreOutEdit));
            this.c1InputPanel1 = new C1.Win.C1InputPanel.C1InputPanel();
            this.tblStoreoutBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.navStoreout = new C1.Win.C1InputPanel.InputDataNavigator();
            this.sepLine = new C1.Win.C1InputPanel.InputSeparator();
            this.lblbatchno = new C1.Win.C1InputPanel.InputLabel();
            this.txtbatchno = new C1.Win.C1InputPanel.InputTextBox();
            this.btnbatchno = new C1.Win.C1InputPanel.InputButton();
            this.lblspecprofitval = new C1.Win.C1InputPanel.InputLabel();
            this.numspecprofitval = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblsalekind = new C1.Win.C1InputPanel.InputLabel();
            this.cbsalekind = new C1.Win.C1InputPanel.InputComboBox();
            this.SalekindBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblsalecompany = new C1.Win.C1InputPanel.InputLabel();
            this.txtsalecompany = new C1.Win.C1InputPanel.InputTextBox();
            this.lbloutdate = new C1.Win.C1InputPanel.InputLabel();
            this.dtpoutdate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lbloutbillno = new C1.Win.C1InputPanel.InputLabel();
            this.txtoutbillno = new C1.Win.C1InputPanel.InputTextBox();
            this.lblhandlername = new C1.Win.C1InputPanel.InputLabel();
            this.cbhandlername = new C1.Win.C1InputPanel.InputComboBox();
            this.HandlernameBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblsettlementname = new C1.Win.C1InputPanel.InputLabel();
            this.cbsettlementname = new C1.Win.C1InputPanel.InputComboBox();
            this.SettlementnameBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblcustomername = new C1.Win.C1InputPanel.InputLabel();
            this.txtcustomername = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcustomerphoneno = new C1.Win.C1InputPanel.InputLabel();
            this.txtcustomerphoneno = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcustomerjobkind = new C1.Win.C1InputPanel.InputLabel();
            this.cbcustomerjobkind = new C1.Win.C1InputPanel.InputComboBox();
            this.JobkindBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblsaleregion = new C1.Win.C1InputPanel.InputLabel();
            this.cbsaleregion = new C1.Win.C1InputPanel.InputComboBox();
            this.RegionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblcustomeraddress = new C1.Win.C1InputPanel.InputLabel();
            this.txtcustomeraddress = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcarno = new C1.Win.C1InputPanel.InputLabel();
            this.txtcarno = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcarspeckind = new C1.Win.C1InputPanel.InputLabel();
            this.cbcarspeckind = new C1.Win.C1InputPanel.InputComboBox();
            this.CarspeckindBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblisreport = new C1.Win.C1InputPanel.InputLabel();
            this.cbisreport = new C1.Win.C1InputPanel.InputComboBox();
            this.IsreportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblvotecost = new C1.Win.C1InputPanel.InputLabel();
            this.numvotecost = new C1.Win.C1InputPanel.InputNumericBox();
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
            this.lbloutprice = new C1.Win.C1InputPanel.InputLabel();
            this.numoutprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblinprice = new C1.Win.C1InputPanel.InputLabel();
            this.numinprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblprofitval = new C1.Win.C1InputPanel.InputLabel();
            this.numprofitval = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblpricediff = new C1.Win.C1InputPanel.InputLabel();
            this.numpricediff = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblsaleplace = new C1.Win.C1InputPanel.InputLabel();
            this.cbsaleplace = new C1.Win.C1InputPanel.InputComboBox();
            this.StoreplaceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblisbill = new C1.Win.C1InputPanel.InputLabel();
            this.cbisbill = new C1.Win.C1InputPanel.InputComboBox();
            this.IsbillBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblispayment = new C1.Win.C1InputPanel.InputLabel();
            this.cbispayment = new C1.Win.C1InputPanel.InputComboBox();
            this.IspayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblissend = new C1.Win.C1InputPanel.InputLabel();
            this.cbissend = new C1.Win.C1InputPanel.InputComboBox();
            this.IssendBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblbilloutdate = new C1.Win.C1InputPanel.InputLabel();
            this.dtpbilloutdate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lblpaymentdate = new C1.Win.C1InputPanel.InputLabel();
            this.dtppaymentdate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lblsenddate = new C1.Win.C1InputPanel.InputLabel();
            this.dtpsenddate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lblremark = new C1.Win.C1InputPanel.InputLabel();
            this.txtremark = new C1.Win.C1InputPanel.InputTextBox();
            this.lbluid = new C1.Win.C1InputPanel.InputLabel();
            this.numuid = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblonroadid = new C1.Win.C1InputPanel.InputLabel();
            this.numonroadid = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblincarkind = new C1.Win.C1InputPanel.InputLabel();
            this.txtincarkind = new C1.Win.C1InputPanel.InputTextBox();
            this.tblOnroadBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tblOnroadTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_onroadTableAdapter();
            this.tblBasedataTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_basedataTableAdapter();
            this.tblStoreoutTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storeoutTableAdapter();
            this.tblStorechangeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblStorechangeTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storechangeTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.c1InputPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreoutBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalekindBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HandlernameBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettlementnameBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JobkindBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarspeckindBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsreportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StoreplaceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsbillBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IspayBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssendBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOnroadBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStorechangeBindingSource)).BeginInit();
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
            this.c1InputPanel1.Items.Add(this.btnbatchno);
            this.c1InputPanel1.Items.Add(this.lblspecprofitval);
            this.c1InputPanel1.Items.Add(this.numspecprofitval);
            this.c1InputPanel1.Items.Add(this.lblsalekind);
            this.c1InputPanel1.Items.Add(this.cbsalekind);
            this.c1InputPanel1.Items.Add(this.lblsalecompany);
            this.c1InputPanel1.Items.Add(this.txtsalecompany);
            this.c1InputPanel1.Items.Add(this.lbloutdate);
            this.c1InputPanel1.Items.Add(this.dtpoutdate);
            this.c1InputPanel1.Items.Add(this.lbloutbillno);
            this.c1InputPanel1.Items.Add(this.txtoutbillno);
            this.c1InputPanel1.Items.Add(this.lblhandlername);
            this.c1InputPanel1.Items.Add(this.cbhandlername);
            this.c1InputPanel1.Items.Add(this.lblsettlementname);
            this.c1InputPanel1.Items.Add(this.cbsettlementname);
            this.c1InputPanel1.Items.Add(this.lblcustomername);
            this.c1InputPanel1.Items.Add(this.txtcustomername);
            this.c1InputPanel1.Items.Add(this.lblcustomerphoneno);
            this.c1InputPanel1.Items.Add(this.txtcustomerphoneno);
            this.c1InputPanel1.Items.Add(this.lblcustomerjobkind);
            this.c1InputPanel1.Items.Add(this.cbcustomerjobkind);
            this.c1InputPanel1.Items.Add(this.lblsaleregion);
            this.c1InputPanel1.Items.Add(this.cbsaleregion);
            this.c1InputPanel1.Items.Add(this.lblcustomeraddress);
            this.c1InputPanel1.Items.Add(this.txtcustomeraddress);
            this.c1InputPanel1.Items.Add(this.lblcarno);
            this.c1InputPanel1.Items.Add(this.txtcarno);
            this.c1InputPanel1.Items.Add(this.lblcarspeckind);
            this.c1InputPanel1.Items.Add(this.cbcarspeckind);
            this.c1InputPanel1.Items.Add(this.lblisreport);
            this.c1InputPanel1.Items.Add(this.cbisreport);
            this.c1InputPanel1.Items.Add(this.lblvotecost);
            this.c1InputPanel1.Items.Add(this.numvotecost);
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
            this.c1InputPanel1.Items.Add(this.lbloutprice);
            this.c1InputPanel1.Items.Add(this.numoutprice);
            this.c1InputPanel1.Items.Add(this.lblinprice);
            this.c1InputPanel1.Items.Add(this.numinprice);
            this.c1InputPanel1.Items.Add(this.lblprofitval);
            this.c1InputPanel1.Items.Add(this.numprofitval);
            this.c1InputPanel1.Items.Add(this.lblpricediff);
            this.c1InputPanel1.Items.Add(this.numpricediff);
            this.c1InputPanel1.Items.Add(this.lblsaleplace);
            this.c1InputPanel1.Items.Add(this.cbsaleplace);
            this.c1InputPanel1.Items.Add(this.lblisbill);
            this.c1InputPanel1.Items.Add(this.cbisbill);
            this.c1InputPanel1.Items.Add(this.lblispayment);
            this.c1InputPanel1.Items.Add(this.cbispayment);
            this.c1InputPanel1.Items.Add(this.lblissend);
            this.c1InputPanel1.Items.Add(this.cbissend);
            this.c1InputPanel1.Items.Add(this.lblbilloutdate);
            this.c1InputPanel1.Items.Add(this.dtpbilloutdate);
            this.c1InputPanel1.Items.Add(this.lblpaymentdate);
            this.c1InputPanel1.Items.Add(this.dtppaymentdate);
            this.c1InputPanel1.Items.Add(this.lblsenddate);
            this.c1InputPanel1.Items.Add(this.dtpsenddate);
            this.c1InputPanel1.Items.Add(this.lblremark);
            this.c1InputPanel1.Items.Add(this.txtremark);
            this.c1InputPanel1.Items.Add(this.lbluid);
            this.c1InputPanel1.Items.Add(this.numuid);
            this.c1InputPanel1.Items.Add(this.lblonroadid);
            this.c1InputPanel1.Items.Add(this.numonroadid);
            this.c1InputPanel1.Items.Add(this.lblincarkind);
            this.c1InputPanel1.Items.Add(this.txtincarkind);
            this.c1InputPanel1.Location = new System.Drawing.Point(0, 0);
            this.c1InputPanel1.Name = "c1InputPanel1";
            this.c1InputPanel1.Size = new System.Drawing.Size(769, 429);
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
            this.navStoreout.CountLabelFormat = "/ {0}";
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
            this.navStoreout.Width = 733;
            // 
            // sepLine
            // 
            this.sepLine.Height = 11;
            this.sepLine.Name = "sepLine";
            this.sepLine.Width = 730;
            // 
            // lblbatchno
            // 
            this.lblbatchno.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblbatchno.Name = "lblbatchno";
            this.lblbatchno.Text = "出库单号:";
            this.lblbatchno.Width = 75;
            // 
            // txtbatchno
            // 
            this.txtbatchno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtbatchno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "batchno", true));
            this.txtbatchno.Name = "txtbatchno";
            this.txtbatchno.Width = 160;
            // 
            // btnbatchno
            // 
            this.btnbatchno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnbatchno.Name = "btnbatchno";
            this.btnbatchno.Text = "...";
            this.btnbatchno.Width = 21;
            this.btnbatchno.Click += new System.EventHandler(this.btnbatchno_Click);
            // 
            // lblspecprofitval
            // 
            this.lblspecprofitval.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblspecprofitval.Name = "lblspecprofitval";
            this.lblspecprofitval.Text = "特殊返利:";
            this.lblspecprofitval.Width = 50;
            // 
            // numspecprofitval
            // 
            this.numspecprofitval.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numspecprofitval.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "specprofitval", true));
            this.numspecprofitval.Format = "N2";
            this.numspecprofitval.Name = "numspecprofitval";
            this.numspecprofitval.Width = 160;
            // 
            // lblsalekind
            // 
            this.lblsalekind.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblsalekind.Name = "lblsalekind";
            this.lblsalekind.Text = "销售方式:";
            this.lblsalekind.Width = 75;
            // 
            // cbsalekind
            // 
            this.cbsalekind.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreoutBindingSource, "salekind", true));
            this.cbsalekind.DataSource = this.SalekindBindingSource;
            this.cbsalekind.DisplayMember = "value";
            this.cbsalekind.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbsalekind.Name = "cbsalekind";
            this.cbsalekind.ValueMember = "value";
            this.cbsalekind.Width = 160;
            // 
            // SalekindBindingSource
            // 
            this.SalekindBindingSource.DataMember = "tbl_basedata";
            this.SalekindBindingSource.DataSource = this.cmsDB;
            this.SalekindBindingSource.Filter = "name = \'销售方式\'";
            // 
            // lblsalecompany
            // 
            this.lblsalecompany.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblsalecompany.Name = "lblsalecompany";
            this.lblsalecompany.Text = "单位名称:";
            this.lblsalecompany.Width = 75;
            // 
            // txtsalecompany
            // 
            this.txtsalecompany.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtsalecompany.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "salecompany", true));
            this.txtsalecompany.Name = "txtsalecompany";
            this.txtsalecompany.Width = 403;
            // 
            // lbloutdate
            // 
            this.lbloutdate.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lbloutdate.Name = "lbloutdate";
            this.lbloutdate.Text = "出库日期:";
            this.lbloutdate.Width = 75;
            // 
            // dtpoutdate
            // 
            this.dtpoutdate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "outdate", true));
            this.dtpoutdate.Name = "dtpoutdate";
            this.dtpoutdate.Width = 160;
            // 
            // lbloutbillno
            // 
            this.lbloutbillno.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lbloutbillno.Name = "lbloutbillno";
            this.lbloutbillno.Text = "票  号:";
            this.lbloutbillno.Width = 75;
            // 
            // txtoutbillno
            // 
            this.txtoutbillno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtoutbillno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "outbillno", true));
            this.txtoutbillno.Name = "txtoutbillno";
            this.txtoutbillno.Width = 160;
            // 
            // lblhandlername
            // 
            this.lblhandlername.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblhandlername.Name = "lblhandlername";
            this.lblhandlername.Text = "批复人:";
            this.lblhandlername.Width = 75;
            // 
            // cbhandlername
            // 
            this.cbhandlername.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbhandlername.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreoutBindingSource, "handlername", true));
            this.cbhandlername.DataSource = this.HandlernameBindingSource;
            this.cbhandlername.DisplayMember = "value";
            this.cbhandlername.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbhandlername.Name = "cbhandlername";
            this.cbhandlername.ValueMember = "value";
            this.cbhandlername.Width = 160;
            // 
            // HandlernameBindingSource
            // 
            this.HandlernameBindingSource.DataMember = "tbl_basedata";
            this.HandlernameBindingSource.DataSource = this.cmsDB;
            this.HandlernameBindingSource.Filter = "name = \'经手人\'";
            // 
            // lblsettlementname
            // 
            this.lblsettlementname.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblsettlementname.Name = "lblsettlementname";
            this.lblsettlementname.Text = "经手人:";
            this.lblsettlementname.Width = 75;
            // 
            // cbsettlementname
            // 
            this.cbsettlementname.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreoutBindingSource, "settlementname", true));
            this.cbsettlementname.DataSource = this.SettlementnameBindingSource;
            this.cbsettlementname.DisplayMember = "value";
            this.cbsettlementname.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbsettlementname.Name = "cbsettlementname";
            this.cbsettlementname.ValueMember = "value";
            this.cbsettlementname.Width = 160;
            // 
            // SettlementnameBindingSource
            // 
            this.SettlementnameBindingSource.DataMember = "tbl_basedata";
            this.SettlementnameBindingSource.DataSource = this.cmsDB;
            this.SettlementnameBindingSource.Filter = "name = \'批复人\'";
            // 
            // lblcustomername
            // 
            this.lblcustomername.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblcustomername.Name = "lblcustomername";
            this.lblcustomername.Text = "客户姓名:";
            this.lblcustomername.Width = 75;
            // 
            // txtcustomername
            // 
            this.txtcustomername.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtcustomername.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "customername", true));
            this.txtcustomername.Name = "txtcustomername";
            this.txtcustomername.Width = 160;
            // 
            // lblcustomerphoneno
            // 
            this.lblcustomerphoneno.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblcustomerphoneno.Name = "lblcustomerphoneno";
            this.lblcustomerphoneno.Text = "电  话:";
            this.lblcustomerphoneno.Width = 75;
            // 
            // txtcustomerphoneno
            // 
            this.txtcustomerphoneno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtcustomerphoneno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "customerphoneno", true));
            this.txtcustomerphoneno.Name = "txtcustomerphoneno";
            this.txtcustomerphoneno.Width = 160;
            // 
            // lblcustomerjobkind
            // 
            this.lblcustomerjobkind.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblcustomerjobkind.Name = "lblcustomerjobkind";
            this.lblcustomerjobkind.Text = "行  业:";
            this.lblcustomerjobkind.Width = 75;
            // 
            // cbcustomerjobkind
            // 
            this.cbcustomerjobkind.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreoutBindingSource, "customerjobkind", true));
            this.cbcustomerjobkind.DataSource = this.JobkindBindingSource;
            this.cbcustomerjobkind.DisplayMember = "value";
            this.cbcustomerjobkind.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbcustomerjobkind.Name = "cbcustomerjobkind";
            this.cbcustomerjobkind.ValueMember = "value";
            this.cbcustomerjobkind.Width = 160;
            // 
            // JobkindBindingSource
            // 
            this.JobkindBindingSource.DataMember = "tbl_basedata";
            this.JobkindBindingSource.DataSource = this.cmsDB;
            this.JobkindBindingSource.Filter = "name = \'行业\'";
            // 
            // lblsaleregion
            // 
            this.lblsaleregion.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblsaleregion.Name = "lblsaleregion";
            this.lblsaleregion.Text = "地  区:";
            this.lblsaleregion.Width = 75;
            // 
            // cbsaleregion
            // 
            this.cbsaleregion.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbsaleregion.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreoutBindingSource, "saleregion", true));
            this.cbsaleregion.DataSource = this.RegionBindingSource;
            this.cbsaleregion.DisplayMember = "value";
            this.cbsaleregion.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbsaleregion.Name = "cbsaleregion";
            this.cbsaleregion.ValueMember = "value";
            this.cbsaleregion.Width = 160;
            // 
            // RegionBindingSource
            // 
            this.RegionBindingSource.DataMember = "tbl_basedata";
            this.RegionBindingSource.DataSource = this.cmsDB;
            this.RegionBindingSource.Filter = "name = \'地区\'";
            // 
            // lblcustomeraddress
            // 
            this.lblcustomeraddress.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblcustomeraddress.Name = "lblcustomeraddress";
            this.lblcustomeraddress.Text = "地  址:";
            this.lblcustomeraddress.Width = 75;
            // 
            // txtcustomeraddress
            // 
            this.txtcustomeraddress.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "customeraddress", true));
            this.txtcustomeraddress.Name = "txtcustomeraddress";
            this.txtcustomeraddress.Width = 403;
            // 
            // lblcarno
            // 
            this.lblcarno.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblcarno.Name = "lblcarno";
            this.lblcarno.Text = "车牌号:";
            this.lblcarno.Width = 75;
            // 
            // txtcarno
            // 
            this.txtcarno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtcarno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "carno", true));
            this.txtcarno.Name = "txtcarno";
            this.txtcarno.Width = 160;
            // 
            // lblcarspeckind
            // 
            this.lblcarspeckind.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblcarspeckind.Name = "lblcarspeckind";
            this.lblcarspeckind.Text = "特种车类型:";
            this.lblcarspeckind.Width = 75;
            // 
            // cbcarspeckind
            // 
            this.cbcarspeckind.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbcarspeckind.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreoutBindingSource, "carspeckind", true));
            this.cbcarspeckind.DataSource = this.CarspeckindBindingSource;
            this.cbcarspeckind.DisplayMember = "value";
            this.cbcarspeckind.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbcarspeckind.Name = "cbcarspeckind";
            this.cbcarspeckind.ValueMember = "value";
            this.cbcarspeckind.Width = 160;
            // 
            // CarspeckindBindingSource
            // 
            this.CarspeckindBindingSource.DataMember = "tbl_basedata";
            this.CarspeckindBindingSource.DataSource = this.cmsDB;
            this.CarspeckindBindingSource.Filter = "name = \'特种车类型\'";
            // 
            // lblisreport
            // 
            this.lblisreport.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblisreport.Name = "lblisreport";
            this.lblisreport.Text = "是否上报:";
            this.lblisreport.Width = 75;
            // 
            // cbisreport
            // 
            this.cbisreport.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreoutBindingSource, "isreport", true));
            this.cbisreport.DataSource = this.IsreportBindingSource;
            this.cbisreport.DisplayMember = "value";
            this.cbisreport.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbisreport.Name = "cbisreport";
            this.cbisreport.ValueMember = "value";
            this.cbisreport.Width = 160;
            // 
            // IsreportBindingSource
            // 
            this.IsreportBindingSource.DataMember = "tbl_basedata";
            this.IsreportBindingSource.DataSource = this.cmsDB;
            this.IsreportBindingSource.Filter = "name = \'是否上报\'";
            // 
            // lblvotecost
            // 
            this.lblvotecost.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblvotecost.Name = "lblvotecost";
            this.lblvotecost.Text = "开票价(元):";
            this.lblvotecost.Width = 75;
            // 
            // numvotecost
            // 
            this.numvotecost.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "votecost", true));
            this.numvotecost.Format = "N2";
            this.numvotecost.Name = "numvotecost";
            this.numvotecost.Width = 160;
            // 
            // lblotherprice1
            // 
            this.lblotherprice1.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblotherprice1.Name = "lblotherprice1";
            this.lblotherprice1.Text = "其它1:";
            this.lblotherprice1.Width = 75;
            // 
            // numotherprice1
            // 
            this.numotherprice1.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numotherprice1.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "otherprice1", true));
            this.numotherprice1.Format = "N2";
            this.numotherprice1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numotherprice1.Name = "numotherprice1";
            this.numotherprice1.Width = 118;
            // 
            // lblotherprice2
            // 
            this.lblotherprice2.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblotherprice2.Name = "lblotherprice2";
            this.lblotherprice2.Text = "其它2:";
            this.lblotherprice2.Width = 50;
            // 
            // numotherprice2
            // 
            this.numotherprice2.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numotherprice2.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "otherprice2", true));
            this.numotherprice2.Format = "N2";
            this.numotherprice2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numotherprice2.Name = "numotherprice2";
            this.numotherprice2.Width = 118;
            // 
            // lblotherprice3
            // 
            this.lblotherprice3.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblotherprice3.Name = "lblotherprice3";
            this.lblotherprice3.Text = "其它3:";
            this.lblotherprice3.Width = 50;
            // 
            // numotherprice3
            // 
            this.numotherprice3.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numotherprice3.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "otherprice3", true));
            this.numotherprice3.Format = "N2";
            this.numotherprice3.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numotherprice3.Name = "numotherprice3";
            this.numotherprice3.Width = 118;
            // 
            // lblotherprice4
            // 
            this.lblotherprice4.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblotherprice4.Name = "lblotherprice4";
            this.lblotherprice4.Text = "其它4:";
            this.lblotherprice4.Width = 50;
            // 
            // numotherprice4
            // 
            this.numotherprice4.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "otherprice4", true));
            this.numotherprice4.Format = "N2";
            this.numotherprice4.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numotherprice4.Name = "numotherprice4";
            this.numotherprice4.Width = 118;
            // 
            // lblinterestprice
            // 
            this.lblinterestprice.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblinterestprice.Name = "lblinterestprice";
            this.lblinterestprice.Text = "累计利息:";
            this.lblinterestprice.Width = 75;
            // 
            // numinterestprice
            // 
            this.numinterestprice.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numinterestprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "interestprice", true));
            this.numinterestprice.Format = "N2";
            this.numinterestprice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numinterestprice.Name = "numinterestprice";
            this.numinterestprice.Width = 160;
            // 
            // lbloutprice
            // 
            this.lbloutprice.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lbloutprice.Name = "lbloutprice";
            this.lbloutprice.Text = "出库费(元):";
            this.lbloutprice.Width = 75;
            // 
            // numoutprice
            // 
            this.numoutprice.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numoutprice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numoutprice.Name = "numoutprice";
            this.numoutprice.Width = 160;
            // 
            // lblinprice
            // 
            this.lblinprice.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblinprice.Name = "lblinprice";
            this.lblinprice.Text = "成本费(元):";
            this.lblinprice.Width = 75;
            // 
            // numinprice
            // 
            this.numinprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "inprice", true));
            this.numinprice.Format = "N2";
            this.numinprice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numinprice.Name = "numinprice";
            this.numinprice.Width = 160;
            // 
            // lblprofitval
            // 
            this.lblprofitval.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblprofitval.Name = "lblprofitval";
            this.lblprofitval.Text = "返利(元):";
            this.lblprofitval.Width = 75;
            // 
            // numprofitval
            // 
            this.numprofitval.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numprofitval.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "profitval", true));
            this.numprofitval.Format = "N2";
            this.numprofitval.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numprofitval.Name = "numprofitval";
            this.numprofitval.Width = 160;
            // 
            // lblpricediff
            // 
            this.lblpricediff.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblpricediff.Name = "lblpricediff";
            this.lblpricediff.Text = "差价(元):";
            this.lblpricediff.Width = 75;
            // 
            // numpricediff
            // 
            this.numpricediff.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numpricediff.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "pricediff", true));
            this.numpricediff.Format = "N2";
            this.numpricediff.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numpricediff.Name = "numpricediff";
            this.numpricediff.Width = 160;
            // 
            // lblsaleplace
            // 
            this.lblsaleplace.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblsaleplace.Name = "lblsaleplace";
            this.lblsaleplace.Text = "销售站点:";
            this.lblsaleplace.Width = 75;
            // 
            // cbsaleplace
            // 
            this.cbsaleplace.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreoutBindingSource, "saleplace", true));
            this.cbsaleplace.DataSource = this.StoreplaceBindingSource;
            this.cbsaleplace.DisplayMember = "value";
            this.cbsaleplace.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbsaleplace.Name = "cbsaleplace";
            this.cbsaleplace.ValueMember = "value";
            this.cbsaleplace.Width = 160;
            // 
            // StoreplaceBindingSource
            // 
            this.StoreplaceBindingSource.DataMember = "tbl_basedata";
            this.StoreplaceBindingSource.DataSource = this.cmsDB;
            this.StoreplaceBindingSource.Filter = "name = \'库位\'";
            // 
            // lblisbill
            // 
            this.lblisbill.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblisbill.Name = "lblisbill";
            this.lblisbill.Text = "开  票:";
            this.lblisbill.Width = 75;
            // 
            // cbisbill
            // 
            this.cbisbill.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbisbill.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreoutBindingSource, "isbill", true));
            this.cbisbill.DataSource = this.IsbillBindingSource;
            this.cbisbill.DisplayMember = "value";
            this.cbisbill.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbisbill.Name = "cbisbill";
            this.cbisbill.ValueMember = "value";
            this.cbisbill.Width = 60;
            // 
            // IsbillBindingSource
            // 
            this.IsbillBindingSource.DataMember = "tbl_basedata";
            this.IsbillBindingSource.DataSource = this.cmsDB;
            this.IsbillBindingSource.Filter = "name = \'是否开票\'";
            // 
            // lblispayment
            // 
            this.lblispayment.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblispayment.Name = "lblispayment";
            this.lblispayment.Text = "付  款:";
            this.lblispayment.Width = 175;
            // 
            // cbispayment
            // 
            this.cbispayment.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbispayment.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreoutBindingSource, "ispayment", true));
            this.cbispayment.DataSource = this.IspayBindingSource;
            this.cbispayment.DisplayMember = "value";
            this.cbispayment.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbispayment.Name = "cbispayment";
            this.cbispayment.ValueMember = "value";
            this.cbispayment.Width = 60;
            // 
            // IspayBindingSource
            // 
            this.IspayBindingSource.DataMember = "tbl_basedata";
            this.IspayBindingSource.DataSource = this.cmsDB;
            this.IspayBindingSource.Filter = "name = \'是否付款\'";
            // 
            // lblissend
            // 
            this.lblissend.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblissend.Name = "lblissend";
            this.lblissend.Text = "提  车:";
            this.lblissend.Width = 175;
            // 
            // cbissend
            // 
            this.cbissend.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreoutBindingSource, "issend", true));
            this.cbissend.DataSource = this.IssendBindingSource;
            this.cbissend.DisplayMember = "value";
            this.cbissend.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbissend.Name = "cbissend";
            this.cbissend.ValueMember = "value";
            this.cbissend.Width = 60;
            // 
            // IssendBindingSource
            // 
            this.IssendBindingSource.DataMember = "tbl_basedata";
            this.IssendBindingSource.DataSource = this.cmsDB;
            this.IssendBindingSource.Filter = "name = \'是否提车\'";
            // 
            // lblbilloutdate
            // 
            this.lblbilloutdate.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblbilloutdate.Name = "lblbilloutdate";
            this.lblbilloutdate.Text = "开票日期:";
            this.lblbilloutdate.Width = 75;
            // 
            // dtpbilloutdate
            // 
            this.dtpbilloutdate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.dtpbilloutdate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "billoutdate", true));
            this.dtpbilloutdate.Name = "dtpbilloutdate";
            this.dtpbilloutdate.Width = 160;
            // 
            // lblpaymentdate
            // 
            this.lblpaymentdate.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblpaymentdate.Name = "lblpaymentdate";
            this.lblpaymentdate.Text = "付款日期:";
            this.lblpaymentdate.Width = 75;
            // 
            // dtppaymentdate
            // 
            this.dtppaymentdate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.dtppaymentdate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "paymentdate", true));
            this.dtppaymentdate.Name = "dtppaymentdate";
            this.dtppaymentdate.Width = 160;
            // 
            // lblsenddate
            // 
            this.lblsenddate.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblsenddate.Name = "lblsenddate";
            this.lblsenddate.Text = "提车日期:";
            this.lblsenddate.Width = 75;
            // 
            // dtpsenddate
            // 
            this.dtpsenddate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "senddate", true));
            this.dtpsenddate.Name = "dtpsenddate";
            this.dtpsenddate.Width = 160;
            // 
            // lblremark
            // 
            this.lblremark.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblremark.Name = "lblremark";
            this.lblremark.Text = "备  注:";
            this.lblremark.Width = 75;
            // 
            // txtremark
            // 
            this.txtremark.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "remark", true));
            this.txtremark.Name = "txtremark";
            this.txtremark.Width = 646;
            // 
            // lbluid
            // 
            this.lbluid.Name = "lbluid";
            this.lbluid.Text = "&uid:";
            this.lbluid.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lbluid.Width = 34;
            // 
            // numuid
            // 
            this.numuid.Break = C1.Win.C1InputPanel.BreakType.None;
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
            this.numuid.Width = 24;
            // 
            // lblonroadid
            // 
            this.lblonroadid.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblonroadid.Name = "lblonroadid";
            this.lblonroadid.Text = "&onroadid:";
            this.lblonroadid.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblonroadid.Width = 75;
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
            this.numonroadid.Width = 24;
            // 
            // lblincarkind
            // 
            this.lblincarkind.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblincarkind.Name = "lblincarkind";
            this.lblincarkind.Text = "i&ncarkind:";
            this.lblincarkind.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblincarkind.Width = 75;
            // 
            // txtincarkind
            // 
            this.txtincarkind.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreoutBindingSource, "incarkind", true));
            this.txtincarkind.Name = "txtincarkind";
            this.txtincarkind.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtincarkind.Width = 28;
            // 
            // tblOnroadBindingSource
            // 
            this.tblOnroadBindingSource.DataMember = "tbl_onroad";
            this.tblOnroadBindingSource.DataSource = this.cmsDB;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(261, 450);
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
            this.btnCancel.Location = new System.Drawing.Point(442, 450);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "返  回";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tblOnroadTableAdapter
            // 
            this.tblOnroadTableAdapter.ClearBeforeFill = true;
            // 
            // tblBasedataTableAdapter
            // 
            this.tblBasedataTableAdapter.ClearBeforeFill = true;
            // 
            // tblStoreoutTableAdapter
            // 
            this.tblStoreoutTableAdapter.ClearBeforeFill = true;
            // 
            // tblStorechangeBindingSource
            // 
            this.tblStorechangeBindingSource.DataMember = "tbl_storechange";
            this.tblStorechangeBindingSource.DataSource = this.cmsDB;
            // 
            // tblStorechangeTableAdapter
            // 
            this.tblStorechangeTableAdapter.ClearBeforeFill = true;
            // 
            // FrmStoreOutEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(769, 496);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.c1InputPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmStoreOutEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "出库修改";
            this.Load += new System.EventHandler(this.FrmStoreOutEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1InputPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreoutBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalekindBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HandlernameBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettlementnameBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JobkindBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarspeckindBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsreportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StoreplaceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsbillBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IspayBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssendBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOnroadBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStorechangeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1InputPanel.C1InputPanel c1InputPanel1;
        private CmsDB cmsDB;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.BindingSource tblOnroadBindingSource;
        private CmsDBTableAdapters.tbl_onroadTableAdapter tblOnroadTableAdapter;
        private System.Windows.Forms.BindingSource StoreplaceBindingSource;
        private CmsDBTableAdapters.tbl_basedataTableAdapter tblBasedataTableAdapter;
        private System.Windows.Forms.BindingSource SettlementnameBindingSource;
        private System.Windows.Forms.BindingSource HandlernameBindingSource;
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
        private C1.Win.C1InputPanel.InputLabel lblsettlementname;
        private C1.Win.C1InputPanel.InputLabel lblhandlername;
        private C1.Win.C1InputPanel.InputLabel lblsaleplace;
        private C1.Win.C1InputPanel.InputLabel lblincarkind;
        private C1.Win.C1InputPanel.InputTextBox txtincarkind;
        private C1.Win.C1InputPanel.InputLabel lbloutdate;
        private C1.Win.C1InputPanel.InputDatePicker dtpoutdate;
        private C1.Win.C1InputPanel.InputLabel lblcustomername;
        private C1.Win.C1InputPanel.InputTextBox txtcustomername;
        private C1.Win.C1InputPanel.InputLabel lblcustomerphoneno;
        private C1.Win.C1InputPanel.InputTextBox txtcustomerphoneno;
        private C1.Win.C1InputPanel.InputLabel lblcustomerjobkind;
        private C1.Win.C1InputPanel.InputLabel lblsaleregion;
        private C1.Win.C1InputPanel.InputLabel lblcustomeraddress;
        private C1.Win.C1InputPanel.InputTextBox txtcustomeraddress;
        private C1.Win.C1InputPanel.InputLabel lblcarno;
        private C1.Win.C1InputPanel.InputTextBox txtcarno;
        private C1.Win.C1InputPanel.InputLabel lblcarspeckind;
        private C1.Win.C1InputPanel.InputLabel lblisreport;
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
        private C1.Win.C1InputPanel.InputLabel lblpricediff;
        private C1.Win.C1InputPanel.InputNumericBox numpricediff;
        private C1.Win.C1InputPanel.InputLabel lblisbill;
        private C1.Win.C1InputPanel.InputLabel lblbilloutdate;
        private C1.Win.C1InputPanel.InputDatePicker dtpbilloutdate;
        private C1.Win.C1InputPanel.InputLabel lblispayment;
        private C1.Win.C1InputPanel.InputLabel lblpaymentdate;
        private C1.Win.C1InputPanel.InputDatePicker dtppaymentdate;
        private C1.Win.C1InputPanel.InputLabel lblissend;
        private C1.Win.C1InputPanel.InputLabel lblsenddate;
        private C1.Win.C1InputPanel.InputDatePicker dtpsenddate;
        private C1.Win.C1InputPanel.InputLabel lblremark;
        private C1.Win.C1InputPanel.InputTextBox txtremark;
        private C1.Win.C1InputPanel.InputButton btnbatchno;
        private C1.Win.C1InputPanel.InputComboBox cbsalekind;
        private C1.Win.C1InputPanel.InputComboBox cbsettlementname;
        private C1.Win.C1InputPanel.InputComboBox cbhandlername;
        private C1.Win.C1InputPanel.InputComboBox cbsaleregion;
        private C1.Win.C1InputPanel.InputComboBox cbcarspeckind;
        private C1.Win.C1InputPanel.InputComboBox cbisreport;
        private C1.Win.C1InputPanel.InputComboBox cbsaleplace;
        private C1.Win.C1InputPanel.InputComboBox cbcustomerjobkind;
        private C1.Win.C1InputPanel.InputComboBox cbisbill;
        private C1.Win.C1InputPanel.InputComboBox cbispayment;
        private C1.Win.C1InputPanel.InputComboBox cbissend;
        private System.Windows.Forms.BindingSource RegionBindingSource;
        private System.Windows.Forms.BindingSource SalekindBindingSource;
        private C1.Win.C1InputPanel.InputNumericBox numoutprice;
        private System.Windows.Forms.BindingSource JobkindBindingSource;
        private System.Windows.Forms.BindingSource CarspeckindBindingSource;
        private System.Windows.Forms.BindingSource IsreportBindingSource;
        private System.Windows.Forms.BindingSource IsbillBindingSource;
        private System.Windows.Forms.BindingSource IspayBindingSource;
        private System.Windows.Forms.BindingSource IssendBindingSource;
        private System.Windows.Forms.BindingSource tblStorechangeBindingSource;
        private CmsDBTableAdapters.tbl_storechangeTableAdapter tblStorechangeTableAdapter;

    }
}