namespace CarSaleMan
{
    partial class FrmStoreChangeEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStoreChangeEdit));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelOnroad = new C1.Win.C1InputPanel.C1InputPanel();
            this.tblStoreinBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.navStorein = new C1.Win.C1InputPanel.InputDataNavigator();
            this.sepLine = new C1.Win.C1InputPanel.InputSeparator();
            this.lblinpath = new C1.Win.C1InputPanel.InputLabel();
            this.txtinpath = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcartype = new C1.Win.C1InputPanel.InputLabel();
            this.txtcartype = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcolorname = new C1.Win.C1InputPanel.InputLabel();
            this.txtcolorname = new C1.Win.C1InputPanel.InputTextBox();
            this.lblinprice = new C1.Win.C1InputPanel.InputLabel();
            this.numinprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblengineno = new C1.Win.C1InputPanel.InputLabel();
            this.txtengineno = new C1.Win.C1InputPanel.InputTextBox();
            this.lblvin = new C1.Win.C1InputPanel.InputLabel();
            this.txtvin = new C1.Win.C1InputPanel.InputTextBox();
            this.lblindate = new C1.Win.C1InputPanel.InputLabel();
            this.dtpindate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lblpassno = new C1.Win.C1InputPanel.InputLabel();
            this.txtpassno = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcompanyno = new C1.Win.C1InputPanel.InputLabel();
            this.numcompanyno = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblbatchno = new C1.Win.C1InputPanel.InputLabel();
            this.txtbatchno = new C1.Win.C1InputPanel.InputTextBox();
            this.btnbatchno = new C1.Win.C1InputPanel.InputButton();
            this.lblchangedate = new C1.Win.C1InputPanel.InputLabel();
            this.dtpchangedate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lbloldstoreplace = new C1.Win.C1InputPanel.InputLabel();
            this.txtoldstoreplace = new C1.Win.C1InputPanel.InputTextBox();
            this.lblstoreplace = new C1.Win.C1InputPanel.InputLabel();
            this.cbstoreplace = new C1.Win.C1InputPanel.InputComboBox();
            this.StoreplaceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblsettlementname = new C1.Win.C1InputPanel.InputLabel();
            this.cbsettlementname = new C1.Win.C1InputPanel.InputComboBox();
            this.SettlementnameBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblhandlername = new C1.Win.C1InputPanel.InputLabel();
            this.cbhandlername = new C1.Win.C1InputPanel.InputComboBox();
            this.HandlernameBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblincarpricekind = new C1.Win.C1InputPanel.InputLabel();
            this.cbincarpricekind = new C1.Win.C1InputPanel.InputComboBox();
            this.CarpricekindBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblrepairstate = new C1.Win.C1InputPanel.InputLabel();
            this.txtrepairstate = new C1.Win.C1InputPanel.InputTextBox();
            this.lblremark = new C1.Win.C1InputPanel.InputLabel();
            this.txtremark = new C1.Win.C1InputPanel.InputTextBox();
            this.lbluid = new C1.Win.C1InputPanel.InputLabel();
            this.numuid = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblonroadid = new C1.Win.C1InputPanel.InputLabel();
            this.numonroadid = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblintype = new C1.Win.C1InputPanel.InputLabel();
            this.txtintype = new C1.Win.C1InputPanel.InputTextBox();
            this.lblfactoryoutdate = new C1.Win.C1InputPanel.InputLabel();
            this.dtpfactoryoutdate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lblreservestate = new C1.Win.C1InputPanel.InputLabel();
            this.txtreservestate = new C1.Win.C1InputPanel.InputTextBox();
            this.lblpropval = new C1.Win.C1InputPanel.InputLabel();
            this.numpropval = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblprofitprop = new C1.Win.C1InputPanel.InputLabel();
            this.numprofitprop = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblprofitval = new C1.Win.C1InputPanel.InputLabel();
            this.numprofitval = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblspecprofitval = new C1.Win.C1InputPanel.InputLabel();
            this.numspecprofitval = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblprofitstate = new C1.Win.C1InputPanel.InputLabel();
            this.txtprofitstate = new C1.Win.C1InputPanel.InputTextBox();
            this.lbloutstoreprice = new C1.Win.C1InputPanel.InputLabel();
            this.numoutstoreprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.lbloutflag = new C1.Win.C1InputPanel.InputLabel();
            this.numoutflag = new C1.Win.C1InputPanel.InputNumericBox();
            this.tblOnroadBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblOnroadTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_onroadTableAdapter();
            this.tblBasedataTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_basedataTableAdapter();
            this.tblStoreinTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storeinTableAdapter();
            this.tblStorechangeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblStorechangeTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storechangeTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.panelOnroad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreinBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StoreplaceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettlementnameBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HandlernameBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarpricekindBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOnroadBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStorechangeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(152, 459);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确  定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(321, 459);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "返  回";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panelOnroad
            // 
            this.panelOnroad.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panelOnroad.DataSource = this.tblStoreinBindingSource;
            this.panelOnroad.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOnroad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.panelOnroad.Items.Add(this.navStorein);
            this.panelOnroad.Items.Add(this.sepLine);
            this.panelOnroad.Items.Add(this.lblinpath);
            this.panelOnroad.Items.Add(this.txtinpath);
            this.panelOnroad.Items.Add(this.lblcartype);
            this.panelOnroad.Items.Add(this.txtcartype);
            this.panelOnroad.Items.Add(this.lblcolorname);
            this.panelOnroad.Items.Add(this.txtcolorname);
            this.panelOnroad.Items.Add(this.lblinprice);
            this.panelOnroad.Items.Add(this.numinprice);
            this.panelOnroad.Items.Add(this.lblengineno);
            this.panelOnroad.Items.Add(this.txtengineno);
            this.panelOnroad.Items.Add(this.lblvin);
            this.panelOnroad.Items.Add(this.txtvin);
            this.panelOnroad.Items.Add(this.lblindate);
            this.panelOnroad.Items.Add(this.dtpindate);
            this.panelOnroad.Items.Add(this.lblpassno);
            this.panelOnroad.Items.Add(this.txtpassno);
            this.panelOnroad.Items.Add(this.lblcompanyno);
            this.panelOnroad.Items.Add(this.numcompanyno);
            this.panelOnroad.Items.Add(this.lblbatchno);
            this.panelOnroad.Items.Add(this.txtbatchno);
            this.panelOnroad.Items.Add(this.btnbatchno);
            this.panelOnroad.Items.Add(this.lblchangedate);
            this.panelOnroad.Items.Add(this.dtpchangedate);
            this.panelOnroad.Items.Add(this.lbloldstoreplace);
            this.panelOnroad.Items.Add(this.txtoldstoreplace);
            this.panelOnroad.Items.Add(this.lblstoreplace);
            this.panelOnroad.Items.Add(this.cbstoreplace);
            this.panelOnroad.Items.Add(this.lblsettlementname);
            this.panelOnroad.Items.Add(this.cbsettlementname);
            this.panelOnroad.Items.Add(this.lblhandlername);
            this.panelOnroad.Items.Add(this.cbhandlername);
            this.panelOnroad.Items.Add(this.lblincarpricekind);
            this.panelOnroad.Items.Add(this.cbincarpricekind);
            this.panelOnroad.Items.Add(this.lblrepairstate);
            this.panelOnroad.Items.Add(this.txtrepairstate);
            this.panelOnroad.Items.Add(this.lblremark);
            this.panelOnroad.Items.Add(this.txtremark);
            this.panelOnroad.Items.Add(this.lbluid);
            this.panelOnroad.Items.Add(this.numuid);
            this.panelOnroad.Items.Add(this.lblonroadid);
            this.panelOnroad.Items.Add(this.numonroadid);
            this.panelOnroad.Items.Add(this.lblintype);
            this.panelOnroad.Items.Add(this.txtintype);
            this.panelOnroad.Items.Add(this.lblfactoryoutdate);
            this.panelOnroad.Items.Add(this.dtpfactoryoutdate);
            this.panelOnroad.Items.Add(this.lblreservestate);
            this.panelOnroad.Items.Add(this.txtreservestate);
            this.panelOnroad.Items.Add(this.lblpropval);
            this.panelOnroad.Items.Add(this.numpropval);
            this.panelOnroad.Items.Add(this.lblprofitprop);
            this.panelOnroad.Items.Add(this.numprofitprop);
            this.panelOnroad.Items.Add(this.lblprofitval);
            this.panelOnroad.Items.Add(this.numprofitval);
            this.panelOnroad.Items.Add(this.lblspecprofitval);
            this.panelOnroad.Items.Add(this.numspecprofitval);
            this.panelOnroad.Items.Add(this.lblprofitstate);
            this.panelOnroad.Items.Add(this.txtprofitstate);
            this.panelOnroad.Items.Add(this.lbloutstoreprice);
            this.panelOnroad.Items.Add(this.numoutstoreprice);
            this.panelOnroad.Items.Add(this.lbloutflag);
            this.panelOnroad.Items.Add(this.numoutflag);
            this.panelOnroad.Location = new System.Drawing.Point(0, 0);
            this.panelOnroad.Name = "panelOnroad";
            this.panelOnroad.Size = new System.Drawing.Size(555, 443);
            this.panelOnroad.TabIndex = 3;
            this.panelOnroad.VisualStyle = C1.Win.C1InputPanel.VisualStyle.Office2010Blue;
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
            this.navStorein.AddNewImage = ((System.Drawing.Image)(resources.GetObject("navStorein.AddNewImage")));
            this.navStorein.AddNewToolTip = "Add New";
            this.navStorein.ApplyImage = global::CarSaleMan.Properties.Resources.btnCheck;
            this.navStorein.ApplyToolTip = "Apply Changes";
            this.navStorein.CancelImage = global::CarSaleMan.Properties.Resources.btnStop;
            this.navStorein.CancelToolTip = "Cancel Changes";
            this.navStorein.CountLabelFormat = "/ {0}";
            this.navStorein.DataSource = this.tblStoreinBindingSource;
            this.navStorein.DeleteImage = ((System.Drawing.Image)(resources.GetObject("navStorein.DeleteImage")));
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
            this.navStorein.Width = 525;
            // 
            // sepLine
            // 
            this.sepLine.Height = 11;
            this.sepLine.Name = "sepLine";
            this.sepLine.Width = 519;
            // 
            // lblinpath
            // 
            this.lblinpath.Name = "lblinpath";
            this.lblinpath.Padding = new System.Windows.Forms.Padding(5);
            this.lblinpath.Text = "进货途径:";
            this.lblinpath.Width = 80;
            // 
            // txtinpath
            // 
            this.txtinpath.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtinpath.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "inpath", true));
            this.txtinpath.Enabled = false;
            this.txtinpath.Name = "txtinpath";
            this.txtinpath.Padding = new System.Windows.Forms.Padding(5);
            this.txtinpath.Width = 160;
            // 
            // lblcartype
            // 
            this.lblcartype.Name = "lblcartype";
            this.lblcartype.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblcartype.Text = "车  型:";
            this.lblcartype.Width = 110;
            // 
            // txtcartype
            // 
            this.txtcartype.Enabled = false;
            this.txtcartype.Name = "txtcartype";
            this.txtcartype.Padding = new System.Windows.Forms.Padding(5);
            this.txtcartype.Width = 160;
            // 
            // lblcolorname
            // 
            this.lblcolorname.Name = "lblcolorname";
            this.lblcolorname.Padding = new System.Windows.Forms.Padding(5);
            this.lblcolorname.Text = "颜  色:";
            this.lblcolorname.Width = 80;
            // 
            // txtcolorname
            // 
            this.txtcolorname.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtcolorname.Enabled = false;
            this.txtcolorname.Name = "txtcolorname";
            this.txtcolorname.Padding = new System.Windows.Forms.Padding(5);
            this.txtcolorname.Width = 160;
            // 
            // lblinprice
            // 
            this.lblinprice.Name = "lblinprice";
            this.lblinprice.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblinprice.Text = "进价(元):";
            this.lblinprice.Width = 110;
            // 
            // numinprice
            // 
            this.numinprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "inprice", true));
            this.numinprice.Enabled = false;
            this.numinprice.Format = "N2";
            this.numinprice.Name = "numinprice";
            this.numinprice.Padding = new System.Windows.Forms.Padding(5);
            this.numinprice.Width = 160;
            // 
            // lblengineno
            // 
            this.lblengineno.Name = "lblengineno";
            this.lblengineno.Padding = new System.Windows.Forms.Padding(5);
            this.lblengineno.Text = "发动机号:";
            this.lblengineno.Width = 80;
            // 
            // txtengineno
            // 
            this.txtengineno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtengineno.Enabled = false;
            this.txtengineno.Name = "txtengineno";
            this.txtengineno.Padding = new System.Windows.Forms.Padding(5);
            this.txtengineno.Width = 160;
            // 
            // lblvin
            // 
            this.lblvin.Name = "lblvin";
            this.lblvin.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblvin.Text = "VIN码:";
            this.lblvin.Width = 110;
            // 
            // txtvin
            // 
            this.txtvin.Enabled = false;
            this.txtvin.Name = "txtvin";
            this.txtvin.Padding = new System.Windows.Forms.Padding(5);
            this.txtvin.Width = 160;
            // 
            // lblindate
            // 
            this.lblindate.Name = "lblindate";
            this.lblindate.Padding = new System.Windows.Forms.Padding(5);
            this.lblindate.Text = "进货日期:";
            this.lblindate.Width = 80;
            // 
            // dtpindate
            // 
            this.dtpindate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.dtpindate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "indate", true));
            this.dtpindate.Enabled = false;
            this.dtpindate.Name = "dtpindate";
            this.dtpindate.Padding = new System.Windows.Forms.Padding(5);
            this.dtpindate.Width = 160;
            // 
            // lblpassno
            // 
            this.lblpassno.Name = "lblpassno";
            this.lblpassno.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblpassno.Text = "合格证:";
            this.lblpassno.Width = 110;
            // 
            // txtpassno
            // 
            this.txtpassno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "passno", true));
            this.txtpassno.Enabled = false;
            this.txtpassno.Name = "txtpassno";
            this.txtpassno.Padding = new System.Windows.Forms.Padding(5);
            this.txtpassno.Width = 160;
            // 
            // lblcompanyno
            // 
            this.lblcompanyno.Name = "lblcompanyno";
            this.lblcompanyno.Padding = new System.Windows.Forms.Padding(5);
            this.lblcompanyno.Text = "进货单位:";
            this.lblcompanyno.Width = 80;
            // 
            // numcompanyno
            // 
            this.numcompanyno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "companyno", true));
            this.numcompanyno.Enabled = false;
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
            this.numcompanyno.Padding = new System.Windows.Forms.Padding(5);
            this.numcompanyno.Width = 160;
            // 
            // lblbatchno
            // 
            this.lblbatchno.Name = "lblbatchno";
            this.lblbatchno.Padding = new System.Windows.Forms.Padding(5);
            this.lblbatchno.Text = "转库单号:";
            this.lblbatchno.Width = 80;
            // 
            // txtbatchno
            // 
            this.txtbatchno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtbatchno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "batchno", true));
            this.txtbatchno.Name = "txtbatchno";
            this.txtbatchno.Padding = new System.Windows.Forms.Padding(5);
            this.txtbatchno.Width = 160;
            // 
            // btnbatchno
            // 
            this.btnbatchno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnbatchno.Name = "btnbatchno";
            this.btnbatchno.Text = "...";
            this.btnbatchno.Width = 26;
            this.btnbatchno.Click += new System.EventHandler(this.btnbatchno_Click);
            // 
            // lblchangedate
            // 
            this.lblchangedate.Name = "lblchangedate";
            this.lblchangedate.Padding = new System.Windows.Forms.Padding(20, 5, 5, 5);
            this.lblchangedate.Text = "转库日期:";
            this.lblchangedate.Width = 80;
            // 
            // dtpchangedate
            // 
            this.dtpchangedate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "changedate", true));
            this.dtpchangedate.Name = "dtpchangedate";
            this.dtpchangedate.Padding = new System.Windows.Forms.Padding(5);
            this.dtpchangedate.Width = 160;
            // 
            // lbloldstoreplace
            // 
            this.lbloldstoreplace.Name = "lbloldstoreplace";
            this.lbloldstoreplace.Padding = new System.Windows.Forms.Padding(5);
            this.lbloldstoreplace.Text = "原库位:";
            this.lbloldstoreplace.Width = 80;
            // 
            // txtoldstoreplace
            // 
            this.txtoldstoreplace.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtoldstoreplace.Enabled = false;
            this.txtoldstoreplace.Name = "txtoldstoreplace";
            this.txtoldstoreplace.Padding = new System.Windows.Forms.Padding(5);
            this.txtoldstoreplace.Width = 160;
            // 
            // lblstoreplace
            // 
            this.lblstoreplace.Name = "lblstoreplace";
            this.lblstoreplace.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblstoreplace.Text = "转到库位:";
            this.lblstoreplace.Width = 110;
            // 
            // cbstoreplace
            // 
            this.cbstoreplace.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreinBindingSource, "storeplace", true));
            this.cbstoreplace.DataSource = this.StoreplaceBindingSource;
            this.cbstoreplace.DisplayMember = "value";
            this.cbstoreplace.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbstoreplace.Name = "cbstoreplace";
            this.cbstoreplace.Padding = new System.Windows.Forms.Padding(5);
            this.cbstoreplace.ValueMember = "value";
            this.cbstoreplace.Width = 160;
            // 
            // StoreplaceBindingSource
            // 
            this.StoreplaceBindingSource.DataMember = "tbl_basedata";
            this.StoreplaceBindingSource.DataSource = this.cmsDB;
            this.StoreplaceBindingSource.Filter = "name = \'库位\'";
            // 
            // lblsettlementname
            // 
            this.lblsettlementname.Name = "lblsettlementname";
            this.lblsettlementname.Padding = new System.Windows.Forms.Padding(5);
            this.lblsettlementname.Text = "批复人:";
            this.lblsettlementname.Width = 80;
            // 
            // cbsettlementname
            // 
            this.cbsettlementname.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbsettlementname.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreinBindingSource, "settlementname", true));
            this.cbsettlementname.DataSource = this.SettlementnameBindingSource;
            this.cbsettlementname.DisplayMember = "value";
            this.cbsettlementname.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbsettlementname.Name = "cbsettlementname";
            this.cbsettlementname.Padding = new System.Windows.Forms.Padding(5);
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
            this.lblhandlername.Name = "lblhandlername";
            this.lblhandlername.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblhandlername.Text = "经手人:";
            this.lblhandlername.Width = 110;
            // 
            // cbhandlername
            // 
            this.cbhandlername.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreinBindingSource, "handlername", true));
            this.cbhandlername.DataSource = this.HandlernameBindingSource;
            this.cbhandlername.DisplayMember = "value";
            this.cbhandlername.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbhandlername.Name = "cbhandlername";
            this.cbhandlername.Padding = new System.Windows.Forms.Padding(5);
            this.cbhandlername.ValueMember = "value";
            this.cbhandlername.Width = 160;
            // 
            // HandlernameBindingSource
            // 
            this.HandlernameBindingSource.DataMember = "tbl_basedata";
            this.HandlernameBindingSource.DataSource = this.cmsDB;
            this.HandlernameBindingSource.Filter = "name = \'经手人\'";
            // 
            // lblincarpricekind
            // 
            this.lblincarpricekind.Name = "lblincarpricekind";
            this.lblincarpricekind.Padding = new System.Windows.Forms.Padding(5);
            this.lblincarpricekind.Text = "资金情况:";
            this.lblincarpricekind.Width = 80;
            // 
            // cbincarpricekind
            // 
            this.cbincarpricekind.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbincarpricekind.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblStoreinBindingSource, "incarpricekind", true));
            this.cbincarpricekind.DataSource = this.CarpricekindBindingSource;
            this.cbincarpricekind.DisplayMember = "value";
            this.cbincarpricekind.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbincarpricekind.Name = "cbincarpricekind";
            this.cbincarpricekind.Padding = new System.Windows.Forms.Padding(5);
            this.cbincarpricekind.ValueMember = "value";
            this.cbincarpricekind.Width = 160;
            // 
            // CarpricekindBindingSource
            // 
            this.CarpricekindBindingSource.DataMember = "tbl_basedata";
            this.CarpricekindBindingSource.DataSource = this.cmsDB;
            this.CarpricekindBindingSource.Filter = "name = \'资金情况\'";
            // 
            // lblrepairstate
            // 
            this.lblrepairstate.Name = "lblrepairstate";
            this.lblrepairstate.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblrepairstate.Text = "维修记录:";
            this.lblrepairstate.Width = 110;
            // 
            // txtrepairstate
            // 
            this.txtrepairstate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "repairstate", true));
            this.txtrepairstate.Name = "txtrepairstate";
            this.txtrepairstate.Padding = new System.Windows.Forms.Padding(5);
            this.txtrepairstate.Width = 160;
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
            this.txtremark.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "remark", true));
            this.txtremark.Name = "txtremark";
            this.txtremark.Padding = new System.Windows.Forms.Padding(5);
            this.txtremark.Width = 438;
            // 
            // lbluid
            // 
            this.lbluid.Name = "lbluid";
            this.lbluid.Text = "&uid:";
            this.lbluid.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lbluid.Width = 30;
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
            this.numuid.Width = 28;
            // 
            // lblonroadid
            // 
            this.lblonroadid.Name = "lblonroadid";
            this.lblonroadid.Text = "&onroadid:";
            this.lblonroadid.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblonroadid.Width = 94;
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
            this.numonroadid.Width = 28;
            // 
            // lblintype
            // 
            this.lblintype.Name = "lblintype";
            this.lblintype.Text = "int&ype:";
            this.lblintype.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblintype.Width = 94;
            // 
            // txtintype
            // 
            this.txtintype.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtintype.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "intype", true));
            this.txtintype.Name = "txtintype";
            this.txtintype.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtintype.Width = 30;
            // 
            // lblfactoryoutdate
            // 
            this.lblfactoryoutdate.Name = "lblfactoryoutdate";
            this.lblfactoryoutdate.Text = "&factoryoutdate:";
            this.lblfactoryoutdate.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblfactoryoutdate.Width = 94;
            // 
            // dtpfactoryoutdate
            // 
            this.dtpfactoryoutdate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "factoryoutdate", true));
            this.dtpfactoryoutdate.Name = "dtpfactoryoutdate";
            this.dtpfactoryoutdate.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.dtpfactoryoutdate.Width = 27;
            // 
            // lblreservestate
            // 
            this.lblreservestate.Name = "lblreservestate";
            this.lblreservestate.Text = "reser&vestate:";
            this.lblreservestate.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblreservestate.Width = 94;
            // 
            // txtreservestate
            // 
            this.txtreservestate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtreservestate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "reservestate", true));
            this.txtreservestate.Name = "txtreservestate";
            this.txtreservestate.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtreservestate.Width = 28;
            // 
            // lblpropval
            // 
            this.lblpropval.Name = "lblpropval";
            this.lblpropval.Text = "propva&l:";
            this.lblpropval.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblpropval.Width = 94;
            // 
            // numpropval
            // 
            this.numpropval.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numpropval.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "propval", true));
            this.numpropval.Format = "N2";
            this.numpropval.Name = "numpropval";
            this.numpropval.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numpropval.Width = 28;
            // 
            // lblprofitprop
            // 
            this.lblprofitprop.Name = "lblprofitprop";
            this.lblprofitprop.Text = "profitprop:";
            this.lblprofitprop.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblprofitprop.Width = 94;
            // 
            // numprofitprop
            // 
            this.numprofitprop.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numprofitprop.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "profitprop", true));
            this.numprofitprop.Format = "N2";
            this.numprofitprop.Name = "numprofitprop";
            this.numprofitprop.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numprofitprop.Width = 28;
            // 
            // lblprofitval
            // 
            this.lblprofitval.Name = "lblprofitval";
            this.lblprofitval.Text = "profitval:";
            this.lblprofitval.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblprofitval.Width = 94;
            // 
            // numprofitval
            // 
            this.numprofitval.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "profitval", true));
            this.numprofitval.Format = "N2";
            this.numprofitval.Name = "numprofitval";
            this.numprofitval.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numprofitval.Width = 28;
            // 
            // lblspecprofitval
            // 
            this.lblspecprofitval.Name = "lblspecprofitval";
            this.lblspecprofitval.Text = "specprofitval:";
            this.lblspecprofitval.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblspecprofitval.Width = 94;
            // 
            // numspecprofitval
            // 
            this.numspecprofitval.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numspecprofitval.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "specprofitval", true));
            this.numspecprofitval.Format = "N2";
            this.numspecprofitval.Name = "numspecprofitval";
            this.numspecprofitval.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numspecprofitval.Width = 26;
            // 
            // lblprofitstate
            // 
            this.lblprofitstate.Name = "lblprofitstate";
            this.lblprofitstate.Text = "profitstate:";
            this.lblprofitstate.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lblprofitstate.Width = 94;
            // 
            // txtprofitstate
            // 
            this.txtprofitstate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtprofitstate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "profitstate", true));
            this.txtprofitstate.Name = "txtprofitstate";
            this.txtprofitstate.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtprofitstate.Width = 25;
            // 
            // lbloutstoreprice
            // 
            this.lbloutstoreprice.Name = "lbloutstoreprice";
            this.lbloutstoreprice.Text = "outstoreprice:";
            this.lbloutstoreprice.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lbloutstoreprice.Width = 94;
            // 
            // numoutstoreprice
            // 
            this.numoutstoreprice.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numoutstoreprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblStoreinBindingSource, "outstoreprice", true));
            this.numoutstoreprice.Format = "N2";
            this.numoutstoreprice.Name = "numoutstoreprice";
            this.numoutstoreprice.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numoutstoreprice.Width = 29;
            // 
            // lbloutflag
            // 
            this.lbloutflag.Name = "lbloutflag";
            this.lbloutflag.Text = "outfla&g:";
            this.lbloutflag.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lbloutflag.Width = 94;
            // 
            // numoutflag
            // 
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
            this.numoutflag.Width = 27;
            // 
            // tblOnroadBindingSource
            // 
            this.tblOnroadBindingSource.AllowNew = true;
            this.tblOnroadBindingSource.DataMember = "tbl_onroad";
            this.tblOnroadBindingSource.DataSource = this.cmsDB;
            // 
            // tblOnroadTableAdapter
            // 
            this.tblOnroadTableAdapter.ClearBeforeFill = true;
            // 
            // tblBasedataTableAdapter
            // 
            this.tblBasedataTableAdapter.ClearBeforeFill = true;
            // 
            // tblStoreinTableAdapter
            // 
            this.tblStoreinTableAdapter.ClearBeforeFill = true;
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
            // FrmStoreChangeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(555, 506);
            this.Controls.Add(this.panelOnroad);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmStoreChangeEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "库存转库";
            this.Load += new System.EventHandler(this.FrmStoreChangeEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelOnroad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreinBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StoreplaceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettlementnameBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HandlernameBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarpricekindBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOnroadBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStorechangeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private C1.Win.C1InputPanel.C1InputPanel panelOnroad;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource tblOnroadBindingSource;
        private CarSaleMan.CmsDBTableAdapters.tbl_onroadTableAdapter tblOnroadTableAdapter;
        private CarSaleMan.CmsDBTableAdapters.tbl_basedataTableAdapter tblBasedataTableAdapter;
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
        private C1.Win.C1InputPanel.InputTextBox txtinpath;
        private C1.Win.C1InputPanel.InputLabel lblintype;
        private C1.Win.C1InputPanel.InputTextBox txtintype;
        private C1.Win.C1InputPanel.InputLabel lblincarpricekind;
        private C1.Win.C1InputPanel.InputLabel lblfactoryoutdate;
        private C1.Win.C1InputPanel.InputDatePicker dtpfactoryoutdate;
        private C1.Win.C1InputPanel.InputLabel lblrepairstate;
        private C1.Win.C1InputPanel.InputTextBox txtrepairstate;
        private C1.Win.C1InputPanel.InputLabel lblreservestate;
        private C1.Win.C1InputPanel.InputTextBox txtreservestate;
        private C1.Win.C1InputPanel.InputLabel lblpropval;
        private C1.Win.C1InputPanel.InputNumericBox numpropval;
        private C1.Win.C1InputPanel.InputLabel lblprofitprop;
        private C1.Win.C1InputPanel.InputNumericBox numprofitprop;
        private C1.Win.C1InputPanel.InputLabel lblprofitval;
        private C1.Win.C1InputPanel.InputNumericBox numprofitval;
        private C1.Win.C1InputPanel.InputLabel lblspecprofitval;
        private C1.Win.C1InputPanel.InputNumericBox numspecprofitval;
        private C1.Win.C1InputPanel.InputLabel lblprofitstate;
        private C1.Win.C1InputPanel.InputTextBox txtprofitstate;
        private C1.Win.C1InputPanel.InputLabel lbloutstoreprice;
        private C1.Win.C1InputPanel.InputNumericBox numoutstoreprice;
        private C1.Win.C1InputPanel.InputLabel lblsettlementname;
        private C1.Win.C1InputPanel.InputLabel lblhandlername;
        private C1.Win.C1InputPanel.InputLabel lblremark;
        private C1.Win.C1InputPanel.InputTextBox txtremark;
        private C1.Win.C1InputPanel.InputLabel lbloutflag;
        private C1.Win.C1InputPanel.InputNumericBox numoutflag;
        private C1.Win.C1InputPanel.InputLabel lblchangedate;
        private C1.Win.C1InputPanel.InputDatePicker dtpchangedate;
        private System.Windows.Forms.BindingSource CarpricekindBindingSource;
        private System.Windows.Forms.BindingSource SettlementnameBindingSource;
        private System.Windows.Forms.BindingSource HandlernameBindingSource;
        private System.Windows.Forms.BindingSource StoreplaceBindingSource;
        private C1.Win.C1InputPanel.InputLabel lblcartype;
        private C1.Win.C1InputPanel.InputTextBox txtcartype;
        private C1.Win.C1InputPanel.InputLabel lblcolorname;
        private C1.Win.C1InputPanel.InputTextBox txtcolorname;
        private C1.Win.C1InputPanel.InputLabel lblengineno;
        private C1.Win.C1InputPanel.InputTextBox txtengineno;
        private C1.Win.C1InputPanel.InputLabel lblvin;
        private C1.Win.C1InputPanel.InputTextBox txtvin;
        private C1.Win.C1InputPanel.InputLabel lbloldstoreplace;
        private C1.Win.C1InputPanel.InputTextBox txtoldstoreplace;
        private C1.Win.C1InputPanel.InputComboBox cbstoreplace;
        private C1.Win.C1InputPanel.InputComboBox cbsettlementname;
        private C1.Win.C1InputPanel.InputComboBox cbhandlername;
        private C1.Win.C1InputPanel.InputComboBox cbincarpricekind;
        private C1.Win.C1InputPanel.InputButton btnbatchno;
        private System.Windows.Forms.BindingSource tblStorechangeBindingSource;
        private CmsDBTableAdapters.tbl_storechangeTableAdapter tblStorechangeTableAdapter;
    }
}