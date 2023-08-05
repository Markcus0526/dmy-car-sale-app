namespace CarSaleMan
{
    partial class FrmStoreInEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStoreInEdit));
            this.c1InputPanel1 = new C1.Win.C1InputPanel.C1InputPanel();
            this.tblStoreinBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.navStorein = new C1.Win.C1InputPanel.InputDataNavigator();
            this.sepLine = new C1.Win.C1InputPanel.InputSeparator();
            this.lblbatchno = new C1.Win.C1InputPanel.InputLabel();
            this.txtbatchno = new C1.Win.C1InputPanel.InputTextBox();
            this.btnbatchno = new C1.Win.C1InputPanel.InputButton();
            this.lblinprice = new C1.Win.C1InputPanel.InputLabel();
            this.numinprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.tblOnroadBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
            this.tblStorechangeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblStorechangeTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storechangeTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.c1InputPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreinBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOnroadBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfitstateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StoreplaceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReservestateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InpathBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettlementnameBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HandlernameBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStorechangeBindingSource)).BeginInit();
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
            this.c1InputPanel1.Size = new System.Drawing.Size(769, 323);
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
            this.navStorein.NavigatorItems = ((C1.Win.C1InputPanel.InputNavigatorItems)((((((((C1.Win.C1InputPanel.InputNavigatorItems.MoveFirstButton | C1.Win.C1InputPanel.InputNavigatorItems.MovePreviousButton)
                        | C1.Win.C1InputPanel.InputNavigatorItems.PositionInputBox)
                        | C1.Win.C1InputPanel.InputNavigatorItems.CountLabel)
                        | C1.Win.C1InputPanel.InputNavigatorItems.MoveNextButton)
                        | C1.Win.C1InputPanel.InputNavigatorItems.MoveLastButton)
                        | C1.Win.C1InputPanel.InputNavigatorItems.ApplyButton)
                        | C1.Win.C1InputPanel.InputNavigatorItems.CancelButton)));
            this.navStorein.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.navStorein.ReloadImage = ((System.Drawing.Image)(resources.GetObject("navStorein.ReloadImage")));
            this.navStorein.ReloadToolTip = "Reload Data";
            this.navStorein.SaveImage = global::CarSaleMan.Properties.Resources.btnSave;
            this.navStorein.SaveToolTip = "Save Data";
            this.navStorein.ShowSaveButton = true;
            this.navStorein.Width = 727;
            // 
            // sepLine
            // 
            this.sepLine.Height = 11;
            this.sepLine.Name = "sepLine";
            this.sepLine.Width = 732;
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
            this.lblinprice.Width = 45;
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
            // tblOnroadBindingSource
            // 
            this.tblOnroadBindingSource.DataMember = "tbl_onroad";
            this.tblOnroadBindingSource.DataSource = this.cmsDB;
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
            this.btnOk.Location = new System.Drawing.Point(253, 339);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确  定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(434, 339);
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
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(194, 367);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(403, 13);
            this.label25.TabIndex = 17;
            this.label25.Text = "底盘号17位，前６位字母，发动机号<=10位，前3位字母，合格证前7位数字";
            // 
            // tblBasedataTableAdapter
            // 
            this.tblBasedataTableAdapter.ClearBeforeFill = true;
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
            // FrmStoreInEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(769, 391);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.c1InputPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmStoreInEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "单车入库";
            this.Load += new System.EventHandler(this.FrmStoreInEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1InputPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreinBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOnroadBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfitstateBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StoreplaceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReservestateBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InpathBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettlementnameBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HandlernameBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStorechangeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1InputPanel.C1InputPanel c1InputPanel1;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource tblStoreinBindingSource;
        private CmsDBTableAdapters.tbl_storeinTableAdapter tblStoreinTableAdapter;
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
        private System.Windows.Forms.BindingSource tblStorechangeBindingSource;
        private CmsDBTableAdapters.tbl_storechangeTableAdapter tblStorechangeTableAdapter;

    }
}