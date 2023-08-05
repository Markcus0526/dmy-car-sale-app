namespace CarSaleMan
{
    partial class FrmStoreInAddMan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStoreInAddMan));
            this.c1InputPanel1 = new C1.Win.C1InputPanel.C1InputPanel();
            this.tblStoreinBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.navStorein = new C1.Win.C1InputPanel.InputDataNavigator();
            this.sepLine = new C1.Win.C1InputPanel.InputSeparator();
            this.hdrOnroadcar = new C1.Win.C1InputPanel.InputGroupHeader();
            this.lblbillno = new C1.Win.C1InputPanel.InputLabel();
            this.txtbillno = new C1.Win.C1InputPanel.InputTextBox();
            this.tblOnroadBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblbilldate = new C1.Win.C1InputPanel.InputLabel();
            this.dtpbilldate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lblvin = new C1.Win.C1InputPanel.InputLabel();
            this.txtvin = new C1.Win.C1InputPanel.InputMaskedTextBox();
            this.lblengineno = new C1.Win.C1InputPanel.InputLabel();
            this.txtengineno = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcartype = new C1.Win.C1InputPanel.InputLabel();
            this.cbcartype = new C1.Win.C1InputPanel.InputComboBox();
            this.tblCartypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblcarname = new C1.Win.C1InputPanel.InputLabel();
            this.txtcartypename = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcolorcode = new C1.Win.C1InputPanel.InputLabel();
            this.txtcolorcode = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcolorname = new C1.Win.C1InputPanel.InputLabel();
            this.txtcolorname = new C1.Win.C1InputPanel.InputTextBox();
            this.lblinsidesetcode = new C1.Win.C1InputPanel.InputLabel();
            this.txtinsidesetcode = new C1.Win.C1InputPanel.InputTextBox();
            this.lblinsidesetname = new C1.Win.C1InputPanel.InputLabel();
            this.txtinsidesetname = new C1.Win.C1InputPanel.InputTextBox();
            this.lblsubsets = new C1.Win.C1InputPanel.InputLabel();
            this.txtsubsets = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcarstate = new C1.Win.C1InputPanel.InputLabel();
            this.cbcarstate = new C1.Win.C1InputPanel.InputComboBox();
            this.CarstateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblproperty = new C1.Win.C1InputPanel.InputLabel();
            this.txtproperty = new C1.Win.C1InputPanel.InputTextBox();
            this.hdrStorein = new C1.Win.C1InputPanel.InputGroupHeader();
            this.lblbatchno = new C1.Win.C1InputPanel.InputLabel();
            this.txtbatchno = new C1.Win.C1InputPanel.InputTextBox();
            this.btnbatchno = new C1.Win.C1InputPanel.InputButton();
            this.lblinprice = new C1.Win.C1InputPanel.InputLabel();
            this.numinprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblfactoryoutdate = new C1.Win.C1InputPanel.InputLabel();
            this.dtpfactoryoutdate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lbltransdate = new C1.Win.C1InputPanel.InputLabel();
            this.dtptransdate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lblindate = new C1.Win.C1InputPanel.InputLabel();
            this.dtpindate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lblpropval = new C1.Win.C1InputPanel.InputLabel();
            this.numpropval = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblprofitprop = new C1.Win.C1InputPanel.InputLabel();
            this.numprofitprop = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblprofitval = new C1.Win.C1InputPanel.InputLabel();
            this.numprofitval = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblprofitstate = new C1.Win.C1InputPanel.InputLabel();
            this.cbprofitstate = new C1.Win.C1InputPanel.InputComboBox();
            this.ProfitstateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblspecprofitval = new C1.Win.C1InputPanel.InputLabel();
            this.numspecprofitval = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblcompanyno = new C1.Win.C1InputPanel.InputLabel();
            this.numcompanyno = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblstoreplace = new C1.Win.C1InputPanel.InputLabel();
            this.cbstoreplace = new C1.Win.C1InputPanel.InputComboBox();
            this.StoreplaceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblpassno = new C1.Win.C1InputPanel.InputLabel();
            this.txtpassno = new C1.Win.C1InputPanel.InputTextBox();
            this.lbloutstoreprice = new C1.Win.C1InputPanel.InputLabel();
            this.numoutstoreprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblreservestate = new C1.Win.C1InputPanel.InputLabel();
            this.cbreservestate = new C1.Win.C1InputPanel.InputComboBox();
            this.ReservestateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblrepairstate = new C1.Win.C1InputPanel.InputLabel();
            this.txtrepairstate = new C1.Win.C1InputPanel.InputTextBox();
            this.lblinpath = new C1.Win.C1InputPanel.InputLabel();
            this.cbinpath = new C1.Win.C1InputPanel.InputComboBox();
            this.InpathBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblintype = new C1.Win.C1InputPanel.InputLabel();
            this.cbintype = new C1.Win.C1InputPanel.InputComboBox();
            this.IntypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblsettlementname = new C1.Win.C1InputPanel.InputLabel();
            this.cbsettlementname = new C1.Win.C1InputPanel.InputComboBox();
            this.SettlementnameBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblhandlername = new C1.Win.C1InputPanel.InputLabel();
            this.cbhandlername = new C1.Win.C1InputPanel.InputComboBox();
            this.HandlernameBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblremark = new C1.Win.C1InputPanel.InputLabel();
            this.txtremark = new C1.Win.C1InputPanel.InputTextBox();
            this.lbluid = new C1.Win.C1InputPanel.InputLabel();
            this.numuid = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblonroadid = new C1.Win.C1InputPanel.InputLabel();
            this.numonroadid = new C1.Win.C1InputPanel.InputNumericBox();
            this.lbloutflag = new C1.Win.C1InputPanel.InputLabel();
            this.numoutflag = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblincarpricekind = new C1.Win.C1InputPanel.InputLabel();
            this.txtincarpricekind = new C1.Win.C1InputPanel.InputTextBox();
            this.tblStoreinTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storeinTableAdapter();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tblOnroadTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_onroadTableAdapter();
            this.label25 = new System.Windows.Forms.Label();
            this.tblBasedataTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_basedataTableAdapter();
            this.tblCartypeTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_cartypeTableAdapter();
            this.tblStorechagneBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblStorechangeTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storechangeTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.c1InputPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreinBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOnroadBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCartypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarstateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfitstateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StoreplaceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReservestateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InpathBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettlementnameBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HandlernameBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStorechagneBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // c1InputPanel1
            // 
            this.c1InputPanel1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.c1InputPanel1.DataSource = this.tblStoreinBindingSource;
            this.c1InputPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.c1InputPanel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.c1InputPanel1.Items.Add(this.navStorein);
            this.c1InputPanel1.Items.Add(this.sepLine);
            this.c1InputPanel1.Items.Add(this.hdrOnroadcar);
            this.c1InputPanel1.Items.Add(this.lblbillno);
            this.c1InputPanel1.Items.Add(this.txtbillno);
            this.c1InputPanel1.Items.Add(this.lblbilldate);
            this.c1InputPanel1.Items.Add(this.dtpbilldate);
            this.c1InputPanel1.Items.Add(this.lblvin);
            this.c1InputPanel1.Items.Add(this.txtvin);
            this.c1InputPanel1.Items.Add(this.lblengineno);
            this.c1InputPanel1.Items.Add(this.txtengineno);
            this.c1InputPanel1.Items.Add(this.lblcartype);
            this.c1InputPanel1.Items.Add(this.cbcartype);
            this.c1InputPanel1.Items.Add(this.lblcarname);
            this.c1InputPanel1.Items.Add(this.txtcartypename);
            this.c1InputPanel1.Items.Add(this.lblcolorcode);
            this.c1InputPanel1.Items.Add(this.txtcolorcode);
            this.c1InputPanel1.Items.Add(this.lblcolorname);
            this.c1InputPanel1.Items.Add(this.txtcolorname);
            this.c1InputPanel1.Items.Add(this.lblinsidesetcode);
            this.c1InputPanel1.Items.Add(this.txtinsidesetcode);
            this.c1InputPanel1.Items.Add(this.lblinsidesetname);
            this.c1InputPanel1.Items.Add(this.txtinsidesetname);
            this.c1InputPanel1.Items.Add(this.lblsubsets);
            this.c1InputPanel1.Items.Add(this.txtsubsets);
            this.c1InputPanel1.Items.Add(this.lblcarstate);
            this.c1InputPanel1.Items.Add(this.cbcarstate);
            this.c1InputPanel1.Items.Add(this.lblproperty);
            this.c1InputPanel1.Items.Add(this.txtproperty);
            this.c1InputPanel1.Items.Add(this.hdrStorein);
            this.c1InputPanel1.Items.Add(this.lblbatchno);
            this.c1InputPanel1.Items.Add(this.txtbatchno);
            this.c1InputPanel1.Items.Add(this.btnbatchno);
            this.c1InputPanel1.Items.Add(this.lblinprice);
            this.c1InputPanel1.Items.Add(this.numinprice);
            this.c1InputPanel1.Items.Add(this.lblfactoryoutdate);
            this.c1InputPanel1.Items.Add(this.dtpfactoryoutdate);
            this.c1InputPanel1.Items.Add(this.lbltransdate);
            this.c1InputPanel1.Items.Add(this.dtptransdate);
            this.c1InputPanel1.Items.Add(this.lblindate);
            this.c1InputPanel1.Items.Add(this.dtpindate);
            this.c1InputPanel1.Items.Add(this.lblpropval);
            this.c1InputPanel1.Items.Add(this.numpropval);
            this.c1InputPanel1.Items.Add(this.lblprofitprop);
            this.c1InputPanel1.Items.Add(this.numprofitprop);
            this.c1InputPanel1.Items.Add(this.lblprofitval);
            this.c1InputPanel1.Items.Add(this.numprofitval);
            this.c1InputPanel1.Items.Add(this.lblprofitstate);
            this.c1InputPanel1.Items.Add(this.cbprofitstate);
            this.c1InputPanel1.Items.Add(this.lblspecprofitval);
            this.c1InputPanel1.Items.Add(this.numspecprofitval);
            this.c1InputPanel1.Items.Add(this.lblcompanyno);
            this.c1InputPanel1.Items.Add(this.numcompanyno);
            this.c1InputPanel1.Items.Add(this.lblstoreplace);
            this.c1InputPanel1.Items.Add(this.cbstoreplace);
            this.c1InputPanel1.Items.Add(this.lblpassno);
            this.c1InputPanel1.Items.Add(this.txtpassno);
            this.c1InputPanel1.Items.Add(this.lbloutstoreprice);
            this.c1InputPanel1.Items.Add(this.numoutstoreprice);
            this.c1InputPanel1.Items.Add(this.lblreservestate);
            this.c1InputPanel1.Items.Add(this.cbreservestate);
            this.c1InputPanel1.Items.Add(this.lblrepairstate);
            this.c1InputPanel1.Items.Add(this.txtrepairstate);
            this.c1InputPanel1.Items.Add(this.lblinpath);
            this.c1InputPanel1.Items.Add(this.cbinpath);
            this.c1InputPanel1.Items.Add(this.lblintype);
            this.c1InputPanel1.Items.Add(this.cbintype);
            this.c1InputPanel1.Items.Add(this.lblsettlementname);
            this.c1InputPanel1.Items.Add(this.cbsettlementname);
            this.c1InputPanel1.Items.Add(this.lblhandlername);
            this.c1InputPanel1.Items.Add(this.cbhandlername);
            this.c1InputPanel1.Items.Add(this.lblremark);
            this.c1InputPanel1.Items.Add(this.txtremark);
            this.c1InputPanel1.Items.Add(this.lbluid);
            this.c1InputPanel1.Items.Add(this.numuid);
            this.c1InputPanel1.Items.Add(this.lblonroadid);
            this.c1InputPanel1.Items.Add(this.numonroadid);
            this.c1InputPanel1.Items.Add(this.lbloutflag);
            this.c1InputPanel1.Items.Add(this.numoutflag);
            this.c1InputPanel1.Items.Add(this.lblincarpricekind);
            this.c1InputPanel1.Items.Add(this.txtincarpricekind);
            this.c1InputPanel1.Location = new System.Drawing.Point(0, 0);
            this.c1InputPanel1.Name = "c1InputPanel1";
            this.c1InputPanel1.Size = new System.Drawing.Size(769, 482);
            this.c1InputPanel1.TabIndex = 0;
            this.c1InputPanel1.VisualStyle = C1.Win.C1InputPanel.VisualStyle.Office2010Blue;
            // 
            // tblStoreinBindingSource
            // 
            this.tblStoreinBindingSource.DataMember = "tbl_storein";
            this.tblStoreinBindingSource.DataSource = this.cmsDB;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // navStorein
            // 
            this.navStorein.AddNewImage = global::CarSaleMan.Properties.Resources.btnPlus2;
            this.navStorein.AddNewToolTip = "Add New";
            this.navStorein.ApplyImage = global::CarSaleMan.Properties.Resources.btnCheck;
            this.navStorein.ApplyToolTip = "Apply Changes";
            this.navStorein.CancelImage = global::CarSaleMan.Properties.Resources.btnStop;
            this.navStorein.CancelToolTip = "Cancel Changes";
            this.navStorein.CountLabelFormat = "/ {0}";
            this.navStorein.DataSource = this.tblStoreinBindingSource;
            this.navStorein.DeleteImage = global::CarSaleMan.Properties.Resources.btnMinus2;
            this.navStorein.DeleteToolTip = "Delete";
            this.navStorein.EditImage = ((System.Drawing.Image)(resources.GetObject("navStorein.EditImage")));
            this.navStorein.EditToolTip = "Edit";
            this.navStorein.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Center;
            this.navStorein.MoveFirstImage = global::CarSaleMan.Properties.Resources.btnFirst;
            this.navStorein.MoveFirstToolTip = "Move First";
            this.navStorein.MoveLastImage = global::CarSaleMan.Properties.Resources.btnLast;
            this.navStorein.MoveLastToolTip = "Move Last";
            this.navStorein.MoveNextImage = global::CarSaleMan.Properties.Resources.btnRight;
            this.navStorein.MoveNextToolTip = "Move Next";
            this.navStorein.MovePreviousImage = global::CarSaleMan.Properties.Resources.btnLeft;
            this.navStorein.MovePreviousToolTip = "Move Previous";
            this.navStorein.Name = "navStorein";
            this.navStorein.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.navStorein.ReloadImage = ((System.Drawing.Image)(resources.GetObject("navStorein.ReloadImage")));
            this.navStorein.ReloadToolTip = "Reload Data";
            this.navStorein.SaveImage = global::CarSaleMan.Properties.Resources.btnSave;
            this.navStorein.SaveToolTip = "Save Data";
            this.navStorein.ShowSaveButton = true;
            this.navStorein.Width = 732;
            // 
            // sepLine
            // 
            this.sepLine.Height = 11;
            this.sepLine.Name = "sepLine";
            this.sepLine.Width = 732;
            // 
            // hdrOnroadcar
            // 
            this.hdrOnroadcar.ElementHeight = 10;
            this.hdrOnroadcar.Name = "hdrOnroadcar";
            // 
            // lblbillno
            // 
            this.lblbillno.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblbillno.Name = "lblbillno";
            this.lblbillno.Text = "原始提单号:";
            this.lblbillno.Width = 75;
            // 
            // txtbillno
            // 
            this.txtbillno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtbillno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "billno", true));
            this.txtbillno.Name = "txtbillno";
            this.txtbillno.Width = 160;
            // 
            // tblOnroadBindingSource
            // 
            this.tblOnroadBindingSource.DataMember = "tbl_onroad";
            this.tblOnroadBindingSource.DataSource = this.cmsDB;
            this.tblOnroadBindingSource.Filter = "";
            // 
            // lblbilldate
            // 
            this.lblbilldate.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblbilldate.Name = "lblbilldate";
            this.lblbilldate.Text = "开单日期:";
            this.lblbilldate.Width = 75;
            // 
            // dtpbilldate
            // 
            this.dtpbilldate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.dtpbilldate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "billdate", true));
            this.dtpbilldate.Name = "dtpbilldate";
            this.dtpbilldate.Width = 160;
            // 
            // lblvin
            // 
            this.lblvin.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblvin.Name = "lblvin";
            this.lblvin.Text = "VIN码:";
            this.lblvin.Width = 75;
            // 
            // txtvin
            // 
            this.txtvin.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "vin", true));
            this.txtvin.Mask = "AAAAAAAAAAAAAAAAA";
            this.txtvin.Name = "txtvin";
            this.txtvin.PromptChar = '#';
            this.txtvin.Width = 160;
            // 
            // lblengineno
            // 
            this.lblengineno.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblengineno.Name = "lblengineno";
            this.lblengineno.Text = "发动机号:";
            this.lblengineno.Width = 75;
            // 
            // txtengineno
            // 
            this.txtengineno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtengineno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "engineno", true));
            this.txtengineno.Name = "txtengineno";
            this.txtengineno.Width = 160;
            // 
            // lblcartype
            // 
            this.lblcartype.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblcartype.Name = "lblcartype";
            this.lblcartype.Text = "车辆代码:";
            this.lblcartype.Width = 75;
            // 
            // cbcartype
            // 
            this.cbcartype.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbcartype.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblOnroadBindingSource, "cartype", true));
            this.cbcartype.DataSource = this.tblCartypeBindingSource;
            this.cbcartype.DisplayMember = "carcode";
            this.cbcartype.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbcartype.Name = "cbcartype";
            this.cbcartype.Width = 160;
            // 
            // tblCartypeBindingSource
            // 
            this.tblCartypeBindingSource.DataMember = "tbl_cartype";
            this.tblCartypeBindingSource.DataSource = this.cmsDB;
            // 
            // lblcarname
            // 
            this.lblcarname.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblcarname.Name = "lblcarname";
            this.lblcarname.Text = "车辆名称:";
            this.lblcarname.Width = 75;
            // 
            // txtcartypename
            // 
            this.txtcartypename.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "carname", true));
            this.txtcartypename.Enabled = false;
            this.txtcartypename.Name = "txtcartypename";
            this.txtcartypename.Width = 160;
            // 
            // lblcolorcode
            // 
            this.lblcolorcode.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblcolorcode.Name = "lblcolorcode";
            this.lblcolorcode.Text = "颜色代码:";
            this.lblcolorcode.Width = 75;
            // 
            // txtcolorcode
            // 
            this.txtcolorcode.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtcolorcode.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "colorcode", true));
            this.txtcolorcode.Name = "txtcolorcode";
            this.txtcolorcode.Width = 160;
            // 
            // lblcolorname
            // 
            this.lblcolorname.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblcolorname.Name = "lblcolorname";
            this.lblcolorname.Text = "颜色名称:";
            this.lblcolorname.Width = 75;
            // 
            // txtcolorname
            // 
            this.txtcolorname.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtcolorname.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "colorname", true));
            this.txtcolorname.Name = "txtcolorname";
            this.txtcolorname.Width = 160;
            // 
            // lblinsidesetcode
            // 
            this.lblinsidesetcode.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblinsidesetcode.Name = "lblinsidesetcode";
            this.lblinsidesetcode.Text = "内  饰:";
            this.lblinsidesetcode.Width = 75;
            // 
            // txtinsidesetcode
            // 
            this.txtinsidesetcode.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "insidesetcode", true));
            this.txtinsidesetcode.Enabled = false;
            this.txtinsidesetcode.Name = "txtinsidesetcode";
            this.txtinsidesetcode.Width = 160;
            // 
            // lblinsidesetname
            // 
            this.lblinsidesetname.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblinsidesetname.Name = "lblinsidesetname";
            this.lblinsidesetname.Text = "内饰名:";
            this.lblinsidesetname.Width = 75;
            // 
            // txtinsidesetname
            // 
            this.txtinsidesetname.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtinsidesetname.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "insidesetname", true));
            this.txtinsidesetname.Enabled = false;
            this.txtinsidesetname.Name = "txtinsidesetname";
            this.txtinsidesetname.Width = 160;
            // 
            // lblsubsets
            // 
            this.lblsubsets.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblsubsets.Name = "lblsubsets";
            this.lblsubsets.Text = "选装包:";
            this.lblsubsets.Width = 75;
            // 
            // txtsubsets
            // 
            this.txtsubsets.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtsubsets.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "subsets", true));
            this.txtsubsets.Enabled = false;
            this.txtsubsets.Name = "txtsubsets";
            this.txtsubsets.Width = 160;
            // 
            // lblcarstate
            // 
            this.lblcarstate.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblcarstate.Name = "lblcarstate";
            this.lblcarstate.Text = "状态名称:";
            this.lblcarstate.Width = 75;
            // 
            // cbcarstate
            // 
            this.cbcarstate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblOnroadBindingSource, "carstate", true));
            this.cbcarstate.DataSource = this.CarstateBindingSource;
            this.cbcarstate.DisplayMember = "value";
            this.cbcarstate.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbcarstate.Name = "cbcarstate";
            this.cbcarstate.ValueMember = "value";
            this.cbcarstate.Width = 160;
            // 
            // CarstateBindingSource
            // 
            this.CarstateBindingSource.DataMember = "tbl_basedata";
            this.CarstateBindingSource.DataSource = this.cmsDB;
            this.CarstateBindingSource.Filter = "name = \'状态名称\'";
            // 
            // lblproperty
            // 
            this.lblproperty.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblproperty.Name = "lblproperty";
            this.lblproperty.Text = "属性名称:";
            this.lblproperty.Width = 75;
            // 
            // txtproperty
            // 
            this.txtproperty.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "property", true));
            this.txtproperty.Name = "txtproperty";
            this.txtproperty.Width = 646;
            // 
            // hdrStorein
            // 
            this.hdrStorein.ElementHeight = 10;
            this.hdrStorein.Name = "hdrStorein";
            // 
            // lblbatchno
            // 
            this.lblbatchno.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblbatchno.Name = "lblbatchno";
            this.lblbatchno.Text = "入库单号:";
            this.lblbatchno.Width = 75;
            // 
            // txtbatchno
            // 
            this.txtbatchno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtbatchno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "batchno", true));
            this.txtbatchno.Name = "txtbatchno";
            this.txtbatchno.Width = 160;
            // 
            // btnbatchno
            // 
            this.btnbatchno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnbatchno.Name = "btnbatchno";
            this.btnbatchno.Text = "...";
            this.btnbatchno.Width = 25;
            this.btnbatchno.Click += new System.EventHandler(this.btnbatchno_Click);
            // 
            // lblinprice
            // 
            this.lblinprice.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblinprice.Name = "lblinprice";
            this.lblinprice.Text = "进价(元):";
            this.lblinprice.Width = 46;
            // 
            // numinprice
            // 
            this.numinprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "inprice", true));
            this.numinprice.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tblStoreinBindingSource, "inprice", true));
            this.numinprice.Format = "N2";
            this.numinprice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numinprice.Name = "numinprice";
            this.numinprice.Width = 160;
            // 
            // lblfactoryoutdate
            // 
            this.lblfactoryoutdate.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblfactoryoutdate.Name = "lblfactoryoutdate";
            this.lblfactoryoutdate.Text = "出厂日期:";
            this.lblfactoryoutdate.Width = 75;
            // 
            // dtpfactoryoutdate
            // 
            this.dtpfactoryoutdate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.dtpfactoryoutdate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "factoryoutdate", true));
            this.dtpfactoryoutdate.Name = "dtpfactoryoutdate";
            this.dtpfactoryoutdate.Width = 160;
            // 
            // lbltransdate
            // 
            this.lbltransdate.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lbltransdate.Name = "lbltransdate";
            this.lbltransdate.Text = "到货日期:";
            this.lbltransdate.Width = 75;
            // 
            // dtptransdate
            // 
            this.dtptransdate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.dtptransdate.Name = "dtptransdate";
            this.dtptransdate.Width = 160;
            // 
            // lblindate
            // 
            this.lblindate.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblindate.Name = "lblindate";
            this.lblindate.Text = "入库日期:";
            this.lblindate.Width = 75;
            // 
            // dtpindate
            // 
            this.dtpindate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "indate", true));
            this.dtpindate.Name = "dtpindate";
            this.dtpindate.Width = 160;
            // 
            // lblpropval
            // 
            this.lblpropval.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblpropval.Name = "lblpropval";
            this.lblpropval.Text = "比例系数:";
            this.lblpropval.Width = 75;
            // 
            // numpropval
            // 
            this.numpropval.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numpropval.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "propval", true));
            this.numpropval.Format = "N2";
            this.numpropval.Name = "numpropval";
            this.numpropval.Width = 160;
            // 
            // lblprofitprop
            // 
            this.lblprofitprop.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblprofitprop.Name = "lblprofitprop";
            this.lblprofitprop.Text = "返利率:";
            this.lblprofitprop.Width = 75;
            // 
            // numprofitprop
            // 
            this.numprofitprop.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numprofitprop.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "profitprop", true));
            this.numprofitprop.Format = "N3";
            this.numprofitprop.Name = "numprofitprop";
            this.numprofitprop.Width = 160;
            // 
            // lblprofitval
            // 
            this.lblprofitval.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblprofitval.Name = "lblprofitval";
            this.lblprofitval.Text = "返  利:";
            this.lblprofitval.Width = 75;
            // 
            // numprofitval
            // 
            this.numprofitval.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "profitval", true));
            this.numprofitval.Format = "N2";
            this.numprofitval.Name = "numprofitval";
            this.numprofitval.Width = 160;
            // 
            // lblprofitstate
            // 
            this.lblprofitstate.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblprofitstate.Name = "lblprofitstate";
            this.lblprofitstate.Text = "返利状态:";
            this.lblprofitstate.Width = 75;
            // 
            // cbprofitstate
            // 
            this.cbprofitstate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbprofitstate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreinBindingSource, "profitstate", true));
            this.cbprofitstate.DataSource = this.ProfitstateBindingSource;
            this.cbprofitstate.DisplayMember = "value";
            this.cbprofitstate.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbprofitstate.Name = "cbprofitstate";
            this.cbprofitstate.ValueMember = "value";
            this.cbprofitstate.Width = 160;
            // 
            // ProfitstateBindingSource
            // 
            this.ProfitstateBindingSource.DataMember = "tbl_basedata";
            this.ProfitstateBindingSource.DataSource = this.cmsDB;
            this.ProfitstateBindingSource.Filter = "name = \'返利状态\'";
            // 
            // lblspecprofitval
            // 
            this.lblspecprofitval.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblspecprofitval.Name = "lblspecprofitval";
            this.lblspecprofitval.Text = "特殊返利:";
            this.lblspecprofitval.Width = 75;
            // 
            // numspecprofitval
            // 
            this.numspecprofitval.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "specprofitval", true));
            this.numspecprofitval.Format = "N2";
            this.numspecprofitval.Name = "numspecprofitval";
            this.numspecprofitval.Width = 160;
            // 
            // lblcompanyno
            // 
            this.lblcompanyno.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblcompanyno.Name = "lblcompanyno";
            this.lblcompanyno.Text = "进货单位编号:";
            this.lblcompanyno.Width = 75;
            // 
            // numcompanyno
            // 
            this.numcompanyno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numcompanyno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "companyno", true));
            this.numcompanyno.Format = "0";
            this.numcompanyno.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numcompanyno.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numcompanyno.Name = "numcompanyno";
            this.numcompanyno.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numcompanyno.Width = 160;
            // 
            // lblstoreplace
            // 
            this.lblstoreplace.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblstoreplace.Name = "lblstoreplace";
            this.lblstoreplace.Text = "库  位:";
            this.lblstoreplace.Width = 75;
            // 
            // cbstoreplace
            // 
            this.cbstoreplace.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbstoreplace.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreinBindingSource, "storeplace", true));
            this.cbstoreplace.DataSource = this.StoreplaceBindingSource;
            this.cbstoreplace.DisplayMember = "value";
            this.cbstoreplace.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbstoreplace.Name = "cbstoreplace";
            this.cbstoreplace.ValueMember = "value";
            this.cbstoreplace.Width = 160;
            // 
            // StoreplaceBindingSource
            // 
            this.StoreplaceBindingSource.DataMember = "tbl_basedata";
            this.StoreplaceBindingSource.DataSource = this.cmsDB;
            this.StoreplaceBindingSource.Filter = "name = \'库位\'";
            // 
            // lblpassno
            // 
            this.lblpassno.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblpassno.Name = "lblpassno";
            this.lblpassno.Text = "合格证:";
            this.lblpassno.Width = 75;
            // 
            // txtpassno
            // 
            this.txtpassno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "passno", true));
            this.txtpassno.Name = "txtpassno";
            this.txtpassno.Width = 160;
            // 
            // lbloutstoreprice
            // 
            this.lbloutstoreprice.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lbloutstoreprice.Name = "lbloutstoreprice";
            this.lbloutstoreprice.Text = "出库费:";
            this.lbloutstoreprice.Width = 75;
            // 
            // numoutstoreprice
            // 
            this.numoutstoreprice.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numoutstoreprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "outstoreprice", true));
            this.numoutstoreprice.Format = "N2";
            this.numoutstoreprice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numoutstoreprice.Name = "numoutstoreprice";
            this.numoutstoreprice.Width = 160;
            // 
            // lblreservestate
            // 
            this.lblreservestate.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblreservestate.Name = "lblreservestate";
            this.lblreservestate.Text = "销售顾问:";
            this.lblreservestate.Width = 75;
            // 
            // cbreservestate
            // 
            this.cbreservestate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbreservestate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreinBindingSource, "reservestate", true));
            this.cbreservestate.DataSource = this.ReservestateBindingSource;
            this.cbreservestate.DisplayMember = "value";
            this.cbreservestate.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbreservestate.Name = "cbreservestate";
            this.cbreservestate.ValueMember = "value";
            this.cbreservestate.Width = 160;
            // 
            // ReservestateBindingSource
            // 
            this.ReservestateBindingSource.DataMember = "tbl_basedata";
            this.ReservestateBindingSource.DataSource = this.cmsDB;
            this.ReservestateBindingSource.Filter = "name = \'销售顾问\'";
            // 
            // lblrepairstate
            // 
            this.lblrepairstate.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblrepairstate.Name = "lblrepairstate";
            this.lblrepairstate.Text = "维修记录:";
            this.lblrepairstate.Width = 75;
            // 
            // txtrepairstate
            // 
            this.txtrepairstate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "repairstate", true));
            this.txtrepairstate.Name = "txtrepairstate";
            this.txtrepairstate.Width = 160;
            // 
            // lblinpath
            // 
            this.lblinpath.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblinpath.Name = "lblinpath";
            this.lblinpath.Text = "进货途径:";
            this.lblinpath.Width = 75;
            // 
            // cbinpath
            // 
            this.cbinpath.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbinpath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreinBindingSource, "inpath", true));
            this.cbinpath.DataSource = this.InpathBindingSource;
            this.cbinpath.DisplayMember = "value";
            this.cbinpath.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbinpath.Name = "cbinpath";
            this.cbinpath.ValueMember = "value";
            this.cbinpath.Width = 160;
            // 
            // InpathBindingSource
            // 
            this.InpathBindingSource.DataMember = "tbl_basedata";
            this.InpathBindingSource.DataSource = this.cmsDB;
            this.InpathBindingSource.Filter = "name = \'进货途径\'";
            // 
            // lblintype
            // 
            this.lblintype.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblintype.Name = "lblintype";
            this.lblintype.Text = "进车状态:";
            this.lblintype.Width = 75;
            // 
            // cbintype
            // 
            this.cbintype.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreinBindingSource, "intype", true));
            this.cbintype.DataSource = this.IntypeBindingSource;
            this.cbintype.DisplayMember = "value";
            this.cbintype.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbintype.Name = "cbintype";
            this.cbintype.ValueMember = "value";
            this.cbintype.Width = 160;
            // 
            // IntypeBindingSource
            // 
            this.IntypeBindingSource.DataMember = "tbl_basedata";
            this.IntypeBindingSource.DataSource = this.cmsDB;
            this.IntypeBindingSource.Filter = "name = \'进车状态\'";
            // 
            // lblsettlementname
            // 
            this.lblsettlementname.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblsettlementname.Name = "lblsettlementname";
            this.lblsettlementname.Text = "批复人:";
            this.lblsettlementname.Width = 75;
            // 
            // cbsettlementname
            // 
            this.cbsettlementname.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbsettlementname.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreinBindingSource, "settlementname", true));
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
            // lblhandlername
            // 
            this.lblhandlername.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblhandlername.Name = "lblhandlername";
            this.lblhandlername.Text = "经手人:";
            this.lblhandlername.Width = 75;
            // 
            // cbhandlername
            // 
            this.cbhandlername.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreinBindingSource, "handlername", true));
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
            // lblremark
            // 
            this.lblremark.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblremark.Name = "lblremark";
            this.lblremark.Text = "备  注:";
            this.lblremark.Width = 75;
            // 
            // txtremark
            // 
            this.txtremark.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "remark", true));
            this.txtremark.Name = "txtremark";
            this.txtremark.Width = 646;
            // 
            // lbluid
            // 
            this.lbluid.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lbluid.Name = "lbluid";
            this.lbluid.Text = "&uid:";
            this.lbluid.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lbluid.Width = 75;
            // 
            // numuid
            // 
            this.numuid.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numuid.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "uid", true));
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
            this.numuid.Width = 39;
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
            this.numonroadid.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "onroadid", true));
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
            this.numonroadid.Width = 25;
            // 
            // lbloutflag
            // 
            this.lbloutflag.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lbloutflag.Name = "lbloutflag";
            this.lbloutflag.Text = "outfla&g:";
            this.lbloutflag.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lbloutflag.Width = 75;
            // 
            // numoutflag
            // 
            this.numoutflag.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numoutflag.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "outflag", true));
            this.numoutflag.Format = "0";
            this.numoutflag.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numoutflag.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numoutflag.Name = "numoutflag";
            this.numoutflag.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numoutflag.Width = 32;
            // 
            // lblincarpricekind
            // 
            this.lblincarpricekind.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lblincarpricekind.Name = "lblincarpricekind";
            this.lblincarpricekind.Text = "inca&rpricekind:";
            this.lblincarpricekind.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblincarpricekind.Width = 75;
            // 
            // txtincarpricekind
            // 
            this.txtincarpricekind.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "incarpricekind", true));
            this.txtincarpricekind.Name = "txtincarpricekind";
            this.txtincarpricekind.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtincarpricekind.Width = 26;
            // 
            // tblStoreinTableAdapter
            // 
            this.tblStoreinTableAdapter.ClearBeforeFill = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(257, 499);
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
            this.btnCancel.Location = new System.Drawing.Point(438, 499);
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
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(198, 530);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(403, 13);
            this.label25.TabIndex = 17;
            this.label25.Text = "底盘号17位，前６位字母，发动机号<=10位，前3位字母，合格证前7位数字";
            // 
            // tblBasedataTableAdapter
            // 
            this.tblBasedataTableAdapter.ClearBeforeFill = true;
            // 
            // tblCartypeTableAdapter
            // 
            this.tblCartypeTableAdapter.ClearBeforeFill = true;
            // 
            // tblStorechagneBindingSource
            // 
            this.tblStorechagneBindingSource.DataMember = "tbl_storechange";
            this.tblStorechagneBindingSource.DataSource = this.cmsDB;
            // 
            // tblStorechangeTableAdapter
            // 
            this.tblStorechangeTableAdapter.ClearBeforeFill = true;
            // 
            // FrmStoreInAddMan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(769, 551);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.c1InputPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmStoreInAddMan";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "单车入库";
            this.Load += new System.EventHandler(this.FrmStoreInAddMan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1InputPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreinBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOnroadBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCartypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarstateBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfitstateBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StoreplaceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReservestateBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InpathBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettlementnameBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HandlernameBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStorechagneBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1InputPanel.C1InputPanel c1InputPanel1;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource tblStoreinBindingSource;
        private CmsDBTableAdapters.tbl_storeinTableAdapter tblStoreinTableAdapter;
        private C1.Win.C1InputPanel.InputGroupHeader hdrStorein;
        private C1.Win.C1InputPanel.InputDataNavigator navStorein;
        private C1.Win.C1InputPanel.InputSeparator sepLine;
        private C1.Win.C1InputPanel.InputLabel lbluid;
        private C1.Win.C1InputPanel.InputNumericBox numuid;
        private C1.Win.C1InputPanel.InputLabel lblbatchno;
        private C1.Win.C1InputPanel.InputTextBox txtbatchno;
        private C1.Win.C1InputPanel.InputLabel lblstoreplace;
        private C1.Win.C1InputPanel.InputLabel lblonroadid;
        private C1.Win.C1InputPanel.InputNumericBox numonroadid;
        private C1.Win.C1InputPanel.InputLabel lblindate;
        private C1.Win.C1InputPanel.InputDatePicker dtpindate;
        private C1.Win.C1InputPanel.InputLabel lblinprice;
        private C1.Win.C1InputPanel.InputNumericBox numinprice;
        private C1.Win.C1InputPanel.InputLabel lblpassno;
        private C1.Win.C1InputPanel.InputTextBox txtpassno;
        private C1.Win.C1InputPanel.InputLabel lblcompanyno;
        private C1.Win.C1InputPanel.InputNumericBox numcompanyno;
        private C1.Win.C1InputPanel.InputLabel lblinpath;
        private C1.Win.C1InputPanel.InputLabel lblintype;
        private C1.Win.C1InputPanel.InputLabel lblincarpricekind;
        private C1.Win.C1InputPanel.InputTextBox txtincarpricekind;
        private C1.Win.C1InputPanel.InputLabel lblfactoryoutdate;
        private C1.Win.C1InputPanel.InputDatePicker dtpfactoryoutdate;
        private C1.Win.C1InputPanel.InputLabel lblrepairstate;
        private C1.Win.C1InputPanel.InputTextBox txtrepairstate;
        private C1.Win.C1InputPanel.InputLabel lblreservestate;
        private C1.Win.C1InputPanel.InputLabel lblpropval;
        private C1.Win.C1InputPanel.InputNumericBox numpropval;
        private C1.Win.C1InputPanel.InputLabel lblprofitval;
        private C1.Win.C1InputPanel.InputNumericBox numprofitval;
        private C1.Win.C1InputPanel.InputLabel lblspecprofitval;
        private C1.Win.C1InputPanel.InputNumericBox numspecprofitval;
        private C1.Win.C1InputPanel.InputLabel lblprofitstate;
        private C1.Win.C1InputPanel.InputLabel lbloutstoreprice;
        private C1.Win.C1InputPanel.InputNumericBox numoutstoreprice;
        private C1.Win.C1InputPanel.InputLabel lblsettlementname;
        private C1.Win.C1InputPanel.InputLabel lblhandlername;
        private C1.Win.C1InputPanel.InputLabel lblremark;
        private C1.Win.C1InputPanel.InputTextBox txtremark;
        private C1.Win.C1InputPanel.InputLabel lbloutflag;
        private C1.Win.C1InputPanel.InputNumericBox numoutflag;
        private C1.Win.C1InputPanel.InputGroupHeader hdrOnroadcar;
        private C1.Win.C1InputPanel.InputLabel lblbillno;
        private C1.Win.C1InputPanel.InputLabel lblbilldate;
        private C1.Win.C1InputPanel.InputLabel lblvin;
        private C1.Win.C1InputPanel.InputLabel lblengineno;
        private C1.Win.C1InputPanel.InputLabel lblcartype;
        private C1.Win.C1InputPanel.InputLabel lblcarname;
        private C1.Win.C1InputPanel.InputLabel lblcolorcode;
        private C1.Win.C1InputPanel.InputLabel lblcolorname;
        private C1.Win.C1InputPanel.InputLabel lblinsidesetcode;
        private C1.Win.C1InputPanel.InputLabel lblinsidesetname;
        private C1.Win.C1InputPanel.InputLabel lblsubsets;
        private C1.Win.C1InputPanel.InputLabel lblcarstate;
        private C1.Win.C1InputPanel.InputLabel lblproperty;
        private C1.Win.C1InputPanel.InputTextBox txtbillno;
        private C1.Win.C1InputPanel.InputDatePicker dtpbilldate;
        private C1.Win.C1InputPanel.InputMaskedTextBox txtvin;
        private C1.Win.C1InputPanel.InputTextBox txtengineno;
        private C1.Win.C1InputPanel.InputTextBox txtcolorcode;
        private C1.Win.C1InputPanel.InputTextBox txtcolorname;
        private C1.Win.C1InputPanel.InputTextBox txtproperty;
        private C1.Win.C1InputPanel.InputButton btnbatchno;
        private C1.Win.C1InputPanel.InputLabel lbltransdate;
        private C1.Win.C1InputPanel.InputDatePicker dtptransdate;
        private C1.Win.C1InputPanel.InputComboBox cbstoreplace;
        private C1.Win.C1InputPanel.InputComboBox cbinpath;
        private C1.Win.C1InputPanel.InputComboBox cbsettlementname;
        private C1.Win.C1InputPanel.InputComboBox cbhandlername;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.BindingSource tblOnroadBindingSource;
        private CmsDBTableAdapters.tbl_onroadTableAdapter tblOnroadTableAdapter;
        private C1.Win.C1InputPanel.InputTextBox txtcartypename;
        private C1.Win.C1InputPanel.InputTextBox txtinsidesetcode;
        private C1.Win.C1InputPanel.InputTextBox txtinsidesetname;
        private C1.Win.C1InputPanel.InputTextBox txtsubsets;
        private C1.Win.C1InputPanel.InputLabel lblprofitprop;
        private C1.Win.C1InputPanel.InputNumericBox numprofitprop;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.BindingSource StoreplaceBindingSource;
        private CmsDBTableAdapters.tbl_basedataTableAdapter tblBasedataTableAdapter;
        private System.Windows.Forms.BindingSource SettlementnameBindingSource;
        private System.Windows.Forms.BindingSource HandlernameBindingSource;
        private System.Windows.Forms.BindingSource ProfitstateBindingSource;
        private System.Windows.Forms.BindingSource InpathBindingSource;
        private C1.Win.C1InputPanel.InputComboBox cbprofitstate;
        private C1.Win.C1InputPanel.InputComboBox cbreservestate;
        private System.Windows.Forms.BindingSource ReservestateBindingSource;
        private C1.Win.C1InputPanel.InputComboBox cbintype;
        private System.Windows.Forms.BindingSource IntypeBindingSource;
        private C1.Win.C1InputPanel.InputComboBox cbcartype;
        private C1.Win.C1InputPanel.InputComboBox cbcarstate;
        private System.Windows.Forms.BindingSource tblCartypeBindingSource;
        private CmsDBTableAdapters.tbl_cartypeTableAdapter tblCartypeTableAdapter;
        private System.Windows.Forms.BindingSource CarstateBindingSource;
        private System.Windows.Forms.BindingSource tblStorechagneBindingSource;
        private CmsDBTableAdapters.tbl_storechangeTableAdapter tblStorechangeTableAdapter;

    }
}