namespace CarSaleMan
{
    partial class FrmCarTypeEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCarTypeEdit));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelCartype = new C1.Win.C1InputPanel.C1InputPanel();
            this.tblCartypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.navCartype = new C1.Win.C1InputPanel.InputDataNavigator();
            this.sepLine = new C1.Win.C1InputPanel.InputSeparator();
            this.lblcarseries = new C1.Win.C1InputPanel.InputLabel();
            this.txtcarseries = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcarcode = new C1.Win.C1InputPanel.InputLabel();
            this.txtcarcode = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcarname = new C1.Win.C1InputPanel.InputLabel();
            this.txtcarname = new C1.Win.C1InputPanel.InputTextBox();
            this.lbleop = new C1.Win.C1InputPanel.InputLabel();
            this.txteop = new C1.Win.C1InputPanel.InputTextBox();
            this.lblsubsets = new C1.Win.C1InputPanel.InputLabel();
            this.txtsubsets = new C1.Win.C1InputPanel.InputTextBox();
            this.lblinsidesetcode = new C1.Win.C1InputPanel.InputLabel();
            this.txtinsidesetcode = new C1.Win.C1InputPanel.InputTextBox();
            this.lblinsidesetname = new C1.Win.C1InputPanel.InputLabel();
            this.txtinsidesetname = new C1.Win.C1InputPanel.InputTextBox();
            this.lblinprice = new C1.Win.C1InputPanel.InputLabel();
            this.numinprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.lbloutprice = new C1.Win.C1InputPanel.InputLabel();
            this.numoutprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblotherprice1 = new C1.Win.C1InputPanel.InputLabel();
            this.numotherprice1 = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblotherprice2 = new C1.Win.C1InputPanel.InputLabel();
            this.numotherprice2 = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblotherprice3 = new C1.Win.C1InputPanel.InputLabel();
            this.numotherprice3 = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblotherprice4 = new C1.Win.C1InputPanel.InputLabel();
            this.numotherprice4 = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblpropval = new C1.Win.C1InputPanel.InputLabel();
            this.numpropval = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblprofitval = new C1.Win.C1InputPanel.InputLabel();
            this.numprofitval = new C1.Win.C1InputPanel.InputNumericBox();
            this.lbloutstoreprice = new C1.Win.C1InputPanel.InputLabel();
            this.numoutstoreprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblvinprefix = new C1.Win.C1InputPanel.InputLabel();
            this.txtvinprefix = new C1.Win.C1InputPanel.InputTextBox();
            this.lblenginenoprefix = new C1.Win.C1InputPanel.InputLabel();
            this.txtenginenoprefix = new C1.Win.C1InputPanel.InputTextBox();
            this.lbluid = new C1.Win.C1InputPanel.InputLabel();
            this.numuid = new C1.Win.C1InputPanel.InputNumericBox();
            this.lbldeleted = new C1.Win.C1InputPanel.InputLabel();
            this.numdeleted = new C1.Win.C1InputPanel.InputNumericBox();
            this.tblCartypeTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_cartypeTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.panelCartype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCartypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(160, 422);
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
            this.btnCancel.Location = new System.Drawing.Point(329, 422);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "返  回";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panelCartype
            // 
            this.panelCartype.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panelCartype.DataSource = this.tblCartypeBindingSource;
            this.panelCartype.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCartype.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.panelCartype.Items.Add(this.navCartype);
            this.panelCartype.Items.Add(this.sepLine);
            this.panelCartype.Items.Add(this.lblcarseries);
            this.panelCartype.Items.Add(this.txtcarseries);
            this.panelCartype.Items.Add(this.lblcarcode);
            this.panelCartype.Items.Add(this.txtcarcode);
            this.panelCartype.Items.Add(this.lblcarname);
            this.panelCartype.Items.Add(this.txtcarname);
            this.panelCartype.Items.Add(this.lbleop);
            this.panelCartype.Items.Add(this.txteop);
            this.panelCartype.Items.Add(this.lblsubsets);
            this.panelCartype.Items.Add(this.txtsubsets);
            this.panelCartype.Items.Add(this.lblinsidesetcode);
            this.panelCartype.Items.Add(this.txtinsidesetcode);
            this.panelCartype.Items.Add(this.lblinsidesetname);
            this.panelCartype.Items.Add(this.txtinsidesetname);
            this.panelCartype.Items.Add(this.lblinprice);
            this.panelCartype.Items.Add(this.numinprice);
            this.panelCartype.Items.Add(this.lbloutprice);
            this.panelCartype.Items.Add(this.numoutprice);
            this.panelCartype.Items.Add(this.lblotherprice1);
            this.panelCartype.Items.Add(this.numotherprice1);
            this.panelCartype.Items.Add(this.lblotherprice2);
            this.panelCartype.Items.Add(this.numotherprice2);
            this.panelCartype.Items.Add(this.lblotherprice3);
            this.panelCartype.Items.Add(this.numotherprice3);
            this.panelCartype.Items.Add(this.lblotherprice4);
            this.panelCartype.Items.Add(this.numotherprice4);
            this.panelCartype.Items.Add(this.lblpropval);
            this.panelCartype.Items.Add(this.numpropval);
            this.panelCartype.Items.Add(this.lblprofitval);
            this.panelCartype.Items.Add(this.numprofitval);
            this.panelCartype.Items.Add(this.lbloutstoreprice);
            this.panelCartype.Items.Add(this.numoutstoreprice);
            this.panelCartype.Items.Add(this.lblvinprefix);
            this.panelCartype.Items.Add(this.txtvinprefix);
            this.panelCartype.Items.Add(this.lblenginenoprefix);
            this.panelCartype.Items.Add(this.txtenginenoprefix);
            this.panelCartype.Items.Add(this.lbluid);
            this.panelCartype.Items.Add(this.numuid);
            this.panelCartype.Items.Add(this.lbldeleted);
            this.panelCartype.Items.Add(this.numdeleted);
            this.panelCartype.Location = new System.Drawing.Point(0, 0);
            this.panelCartype.Name = "panelCartype";
            this.panelCartype.Size = new System.Drawing.Size(559, 402);
            this.panelCartype.TabIndex = 3;
            this.panelCartype.VisualStyle = C1.Win.C1InputPanel.VisualStyle.Office2010Blue;
            // 
            // tblCartypeBindingSource
            // 
            this.tblCartypeBindingSource.DataMember = "tbl_cartype";
            this.tblCartypeBindingSource.DataSource = this.cmsDB;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // navCartype
            // 
            this.navCartype.AddNewImage = global::CarSaleMan.Properties.Resources.btnPlus2;
            this.navCartype.AddNewToolTip = "Add New";
            this.navCartype.ApplyImage = global::CarSaleMan.Properties.Resources.btnCheck;
            this.navCartype.ApplyToolTip = "Apply Changes";
            this.navCartype.CancelImage = global::CarSaleMan.Properties.Resources.btnStop;
            this.navCartype.CancelToolTip = "Cancel Changes";
            this.navCartype.CountLabelFormat = "/ {0}";
            this.navCartype.DataSource = this.tblCartypeBindingSource;
            this.navCartype.DeleteImage = global::CarSaleMan.Properties.Resources.btnMinus2;
            this.navCartype.DeleteToolTip = "Delete";
            this.navCartype.EditImage = ((System.Drawing.Image)(resources.GetObject("navCartype.EditImage")));
            this.navCartype.EditToolTip = "Edit";
            this.navCartype.MoveFirstImage = global::CarSaleMan.Properties.Resources.btnFirst;
            this.navCartype.MoveFirstToolTip = "Move First";
            this.navCartype.MoveLastImage = global::CarSaleMan.Properties.Resources.btnLast;
            this.navCartype.MoveLastToolTip = "Move Last";
            this.navCartype.MoveNextImage = global::CarSaleMan.Properties.Resources.btnRight;
            this.navCartype.MoveNextToolTip = "Move Next";
            this.navCartype.MovePreviousImage = global::CarSaleMan.Properties.Resources.btnLeft;
            this.navCartype.MovePreviousToolTip = "Move Previous";
            this.navCartype.Name = "navCartype";
            this.navCartype.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.navCartype.ReloadImage = ((System.Drawing.Image)(resources.GetObject("navCartype.ReloadImage")));
            this.navCartype.ReloadToolTip = "Reload Data";
            this.navCartype.SaveImage = global::CarSaleMan.Properties.Resources.btnSave;
            this.navCartype.SaveToolTip = "Save Data";
            this.navCartype.ShowSaveButton = true;
            // 
            // sepLine
            // 
            this.sepLine.Height = 11;
            this.sepLine.Name = "sepLine";
            this.sepLine.Width = 528;
            // 
            // lblcarseries
            // 
            this.lblcarseries.Name = "lblcarseries";
            this.lblcarseries.Padding = new System.Windows.Forms.Padding(5);
            this.lblcarseries.Text = "大类型:";
            this.lblcarseries.Width = 80;
            // 
            // txtcarseries
            // 
            this.txtcarseries.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "carseries", true));
            this.txtcarseries.Name = "txtcarseries";
            this.txtcarseries.Padding = new System.Windows.Forms.Padding(5);
            this.txtcarseries.Width = 160;
            // 
            // lblcarcode
            // 
            this.lblcarcode.Name = "lblcarcode";
            this.lblcarcode.Padding = new System.Windows.Forms.Padding(5);
            this.lblcarcode.Text = "车辆代码:";
            this.lblcarcode.Width = 80;
            // 
            // txtcarcode
            // 
            this.txtcarcode.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtcarcode.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "carcode", true));
            this.txtcarcode.Name = "txtcarcode";
            this.txtcarcode.Padding = new System.Windows.Forms.Padding(5);
            this.txtcarcode.Width = 160;
            // 
            // lblcarname
            // 
            this.lblcarname.Name = "lblcarname";
            this.lblcarname.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblcarname.Text = "车辆名称:";
            this.lblcarname.Width = 110;
            // 
            // txtcarname
            // 
            this.txtcarname.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "carname", true));
            this.txtcarname.Name = "txtcarname";
            this.txtcarname.Padding = new System.Windows.Forms.Padding(5);
            this.txtcarname.Width = 160;
            // 
            // lbleop
            // 
            this.lbleop.Name = "lbleop";
            this.lbleop.Padding = new System.Windows.Forms.Padding(5);
            this.lbleop.Text = "是否EOP:";
            this.lbleop.Width = 80;
            // 
            // txteop
            // 
            this.txteop.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txteop.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "eop", true));
            this.txteop.Name = "txteop";
            this.txteop.Padding = new System.Windows.Forms.Padding(5);
            this.txteop.Width = 160;
            // 
            // lblsubsets
            // 
            this.lblsubsets.Name = "lblsubsets";
            this.lblsubsets.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblsubsets.Text = "选装包:";
            this.lblsubsets.Width = 110;
            // 
            // txtsubsets
            // 
            this.txtsubsets.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "subsets", true));
            this.txtsubsets.Name = "txtsubsets";
            this.txtsubsets.Padding = new System.Windows.Forms.Padding(5);
            this.txtsubsets.Width = 160;
            // 
            // lblinsidesetcode
            // 
            this.lblinsidesetcode.Name = "lblinsidesetcode";
            this.lblinsidesetcode.Padding = new System.Windows.Forms.Padding(5);
            this.lblinsidesetcode.Text = "内饰代码:";
            this.lblinsidesetcode.Width = 80;
            // 
            // txtinsidesetcode
            // 
            this.txtinsidesetcode.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtinsidesetcode.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "insidesetcode", true));
            this.txtinsidesetcode.Name = "txtinsidesetcode";
            this.txtinsidesetcode.Padding = new System.Windows.Forms.Padding(5);
            this.txtinsidesetcode.Width = 160;
            // 
            // lblinsidesetname
            // 
            this.lblinsidesetname.Name = "lblinsidesetname";
            this.lblinsidesetname.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblinsidesetname.Text = "内饰名称:";
            this.lblinsidesetname.Width = 110;
            // 
            // txtinsidesetname
            // 
            this.txtinsidesetname.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "insidesetname", true));
            this.txtinsidesetname.Name = "txtinsidesetname";
            this.txtinsidesetname.Padding = new System.Windows.Forms.Padding(5);
            this.txtinsidesetname.Width = 160;
            // 
            // lblinprice
            // 
            this.lblinprice.Name = "lblinprice";
            this.lblinprice.Padding = new System.Windows.Forms.Padding(5);
            this.lblinprice.Text = "进价:";
            this.lblinprice.Width = 80;
            // 
            // numinprice
            // 
            this.numinprice.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numinprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "inprice", true));
            this.numinprice.Format = "N2";
            this.numinprice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numinprice.Name = "numinprice";
            this.numinprice.Padding = new System.Windows.Forms.Padding(5);
            this.numinprice.Width = 160;
            // 
            // lbloutprice
            // 
            this.lbloutprice.Name = "lbloutprice";
            this.lbloutprice.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lbloutprice.Text = "售价:";
            this.lbloutprice.Width = 110;
            // 
            // numoutprice
            // 
            this.numoutprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "outprice", true));
            this.numoutprice.Format = "N2";
            this.numoutprice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numoutprice.Name = "numoutprice";
            this.numoutprice.Padding = new System.Windows.Forms.Padding(5);
            this.numoutprice.Width = 160;
            // 
            // lblotherprice1
            // 
            this.lblotherprice1.Name = "lblotherprice1";
            this.lblotherprice1.Padding = new System.Windows.Forms.Padding(5);
            this.lblotherprice1.Text = "其它1:";
            this.lblotherprice1.Width = 45;
            // 
            // numotherprice1
            // 
            this.numotherprice1.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numotherprice1.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "otherprice1", true));
            this.numotherprice1.Format = "N2";
            this.numotherprice1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numotherprice1.Name = "numotherprice1";
            this.numotherprice1.Padding = new System.Windows.Forms.Padding(5);
            this.numotherprice1.Width = 79;
            // 
            // lblotherprice2
            // 
            this.lblotherprice2.Name = "lblotherprice2";
            this.lblotherprice2.Padding = new System.Windows.Forms.Padding(5);
            this.lblotherprice2.Text = "其它2:";
            this.lblotherprice2.Width = 45;
            // 
            // numotherprice2
            // 
            this.numotherprice2.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numotherprice2.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "otherprice2", true));
            this.numotherprice2.Format = "N2";
            this.numotherprice2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numotherprice2.Name = "numotherprice2";
            this.numotherprice2.Padding = new System.Windows.Forms.Padding(5);
            this.numotherprice2.Width = 79;
            // 
            // lblotherprice3
            // 
            this.lblotherprice3.Name = "lblotherprice3";
            this.lblotherprice3.Padding = new System.Windows.Forms.Padding(5);
            this.lblotherprice3.Text = "其它3:";
            this.lblotherprice3.Width = 45;
            // 
            // numotherprice3
            // 
            this.numotherprice3.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numotherprice3.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "otherprice3", true));
            this.numotherprice3.Format = "N2";
            this.numotherprice3.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numotherprice3.Name = "numotherprice3";
            this.numotherprice3.Padding = new System.Windows.Forms.Padding(5);
            this.numotherprice3.Width = 79;
            // 
            // lblotherprice4
            // 
            this.lblotherprice4.Name = "lblotherprice4";
            this.lblotherprice4.Padding = new System.Windows.Forms.Padding(5);
            this.lblotherprice4.Text = "其它4:";
            this.lblotherprice4.Width = 45;
            // 
            // numotherprice4
            // 
            this.numotherprice4.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "otherprice4", true));
            this.numotherprice4.Format = "N2";
            this.numotherprice4.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numotherprice4.Name = "numotherprice4";
            this.numotherprice4.Padding = new System.Windows.Forms.Padding(5);
            this.numotherprice4.Width = 79;
            // 
            // lblpropval
            // 
            this.lblpropval.Name = "lblpropval";
            this.lblpropval.Padding = new System.Windows.Forms.Padding(5);
            this.lblpropval.Text = "比列系数:";
            this.lblpropval.Width = 80;
            // 
            // numpropval
            // 
            this.numpropval.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numpropval.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "propval", true));
            this.numpropval.Format = "N2";
            this.numpropval.Name = "numpropval";
            this.numpropval.Padding = new System.Windows.Forms.Padding(5);
            this.numpropval.Width = 160;
            // 
            // lblprofitval
            // 
            this.lblprofitval.Name = "lblprofitval";
            this.lblprofitval.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblprofitval.Text = "返利系数:";
            this.lblprofitval.Width = 110;
            // 
            // numprofitval
            // 
            this.numprofitval.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "profitval", true));
            this.numprofitval.Format = "N2";
            this.numprofitval.Name = "numprofitval";
            this.numprofitval.Padding = new System.Windows.Forms.Padding(5);
            this.numprofitval.Width = 160;
            // 
            // lbloutstoreprice
            // 
            this.lbloutstoreprice.Name = "lbloutstoreprice";
            this.lbloutstoreprice.Padding = new System.Windows.Forms.Padding(5);
            this.lbloutstoreprice.Text = "出库费:";
            this.lbloutstoreprice.Width = 80;
            // 
            // numoutstoreprice
            // 
            this.numoutstoreprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "outstoreprice", true));
            this.numoutstoreprice.Format = "N2";
            this.numoutstoreprice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numoutstoreprice.Name = "numoutstoreprice";
            this.numoutstoreprice.Padding = new System.Windows.Forms.Padding(5);
            this.numoutstoreprice.Width = 160;
            // 
            // lblvinprefix
            // 
            this.lblvinprefix.Name = "lblvinprefix";
            this.lblvinprefix.Padding = new System.Windows.Forms.Padding(5);
            this.lblvinprefix.Text = "VIN前吗:";
            this.lblvinprefix.Width = 80;
            // 
            // txtvinprefix
            // 
            this.txtvinprefix.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtvinprefix.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "vinprefix", true));
            this.txtvinprefix.Name = "txtvinprefix";
            this.txtvinprefix.Padding = new System.Windows.Forms.Padding(5);
            this.txtvinprefix.Width = 160;
            // 
            // lblenginenoprefix
            // 
            this.lblenginenoprefix.Name = "lblenginenoprefix";
            this.lblenginenoprefix.Padding = new System.Windows.Forms.Padding(40, 5, 5, 5);
            this.lblenginenoprefix.Text = "发动机前吗:";
            this.lblenginenoprefix.Width = 110;
            // 
            // txtenginenoprefix
            // 
            this.txtenginenoprefix.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "enginenoprefix", true));
            this.txtenginenoprefix.Name = "txtenginenoprefix";
            this.txtenginenoprefix.Padding = new System.Windows.Forms.Padding(5);
            this.txtenginenoprefix.Width = 160;
            // 
            // lbluid
            // 
            this.lbluid.Name = "lbluid";
            this.lbluid.Text = "&uid:";
            this.lbluid.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lbluid.Width = 28;
            // 
            // numuid
            // 
            this.numuid.Break = C1.Win.C1InputPanel.BreakType.None;
            this.numuid.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "uid", true));
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
            // lbldeleted
            // 
            this.lbldeleted.Name = "lbldeleted";
            this.lbldeleted.Text = "&deleted:";
            this.lbldeleted.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lbldeleted.Width = 47;
            // 
            // numdeleted
            // 
            this.numdeleted.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "deleted", true));
            this.numdeleted.Format = "0";
            this.numdeleted.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numdeleted.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numdeleted.Name = "numdeleted";
            this.numdeleted.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.numdeleted.Width = 24;
            // 
            // tblCartypeTableAdapter
            // 
            this.tblCartypeTableAdapter.ClearBeforeFill = true;
            // 
            // FrmCarTypeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(559, 465);
            this.Controls.Add(this.panelCartype);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmCarTypeEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "车辆价格修改";
            this.Load += new System.EventHandler(this.FrmCarTypeEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelCartype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCartypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private C1.Win.C1InputPanel.C1InputPanel panelCartype;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource tblCartypeBindingSource;
        private CmsDBTableAdapters.tbl_cartypeTableAdapter tblCartypeTableAdapter;
        private C1.Win.C1InputPanel.InputDataNavigator navCartype;
        private C1.Win.C1InputPanel.InputSeparator sepLine;
        private C1.Win.C1InputPanel.InputLabel lbluid;
        private C1.Win.C1InputPanel.InputNumericBox numuid;
        private C1.Win.C1InputPanel.InputLabel lblcarseries;
        private C1.Win.C1InputPanel.InputTextBox txtcarseries;
        private C1.Win.C1InputPanel.InputLabel lblcarcode;
        private C1.Win.C1InputPanel.InputTextBox txtcarcode;
        private C1.Win.C1InputPanel.InputLabel lblcarname;
        private C1.Win.C1InputPanel.InputTextBox txtcarname;
        private C1.Win.C1InputPanel.InputLabel lbleop;
        private C1.Win.C1InputPanel.InputTextBox txteop;
        private C1.Win.C1InputPanel.InputLabel lblsubsets;
        private C1.Win.C1InputPanel.InputTextBox txtsubsets;
        private C1.Win.C1InputPanel.InputLabel lblinsidesetcode;
        private C1.Win.C1InputPanel.InputTextBox txtinsidesetcode;
        private C1.Win.C1InputPanel.InputLabel lblinsidesetname;
        private C1.Win.C1InputPanel.InputTextBox txtinsidesetname;
        private C1.Win.C1InputPanel.InputLabel lblinprice;
        private C1.Win.C1InputPanel.InputNumericBox numinprice;
        private C1.Win.C1InputPanel.InputLabel lbloutprice;
        private C1.Win.C1InputPanel.InputNumericBox numoutprice;
        private C1.Win.C1InputPanel.InputLabel lblotherprice1;
        private C1.Win.C1InputPanel.InputNumericBox numotherprice1;
        private C1.Win.C1InputPanel.InputLabel lblotherprice2;
        private C1.Win.C1InputPanel.InputNumericBox numotherprice2;
        private C1.Win.C1InputPanel.InputLabel lblotherprice3;
        private C1.Win.C1InputPanel.InputNumericBox numotherprice3;
        private C1.Win.C1InputPanel.InputLabel lblotherprice4;
        private C1.Win.C1InputPanel.InputNumericBox numotherprice4;
        private C1.Win.C1InputPanel.InputLabel lblpropval;
        private C1.Win.C1InputPanel.InputNumericBox numpropval;
        private C1.Win.C1InputPanel.InputLabel lblprofitval;
        private C1.Win.C1InputPanel.InputNumericBox numprofitval;
        private C1.Win.C1InputPanel.InputLabel lbloutstoreprice;
        private C1.Win.C1InputPanel.InputNumericBox numoutstoreprice;
        private C1.Win.C1InputPanel.InputLabel lblvinprefix;
        private C1.Win.C1InputPanel.InputTextBox txtvinprefix;
        private C1.Win.C1InputPanel.InputLabel lblenginenoprefix;
        private C1.Win.C1InputPanel.InputTextBox txtenginenoprefix;
        private C1.Win.C1InputPanel.InputLabel lbldeleted;
        private C1.Win.C1InputPanel.InputNumericBox numdeleted;
    }
}