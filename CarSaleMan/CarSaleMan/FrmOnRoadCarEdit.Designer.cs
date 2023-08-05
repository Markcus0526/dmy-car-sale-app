namespace CarSaleMan
{
    partial class FrmOnRoadCarEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOnRoadCarEdit));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelOnroad = new C1.Win.C1InputPanel.C1InputPanel();
            this.tblOnroadBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.navOnroad = new C1.Win.C1InputPanel.InputDataNavigator();
            this.sepLine = new C1.Win.C1InputPanel.InputSeparator();
            this.lblbillno = new C1.Win.C1InputPanel.InputLabel();
            this.txtbillno = new C1.Win.C1InputPanel.InputTextBox();
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
            this.lblinprice = new C1.Win.C1InputPanel.InputLabel();
            this.numinprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.txtcartypeid = new C1.Win.C1InputPanel.InputTextBox();
            this.tblOnroadTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_onroadTableAdapter();
            this.tblBasedataTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_basedataTableAdapter();
            this.tblCartypeTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_cartypeTableAdapter();
            this.tblStorechangeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblStorechangeTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storechangeTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.panelOnroad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOnroadBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCartypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarstateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStorechangeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(159, 353);
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
            this.btnCancel.Location = new System.Drawing.Point(328, 353);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "返  回";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panelOnroad
            // 
            this.panelOnroad.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panelOnroad.DataSource = this.tblOnroadBindingSource;
            this.panelOnroad.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOnroad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.panelOnroad.Items.Add(this.navOnroad);
            this.panelOnroad.Items.Add(this.sepLine);
            this.panelOnroad.Items.Add(this.lblbillno);
            this.panelOnroad.Items.Add(this.txtbillno);
            this.panelOnroad.Items.Add(this.lblbilldate);
            this.panelOnroad.Items.Add(this.dtpbilldate);
            this.panelOnroad.Items.Add(this.lblvin);
            this.panelOnroad.Items.Add(this.txtvin);
            this.panelOnroad.Items.Add(this.lblengineno);
            this.panelOnroad.Items.Add(this.txtengineno);
            this.panelOnroad.Items.Add(this.lblcartype);
            this.panelOnroad.Items.Add(this.cbcartype);
            this.panelOnroad.Items.Add(this.lblcarname);
            this.panelOnroad.Items.Add(this.txtcartypename);
            this.panelOnroad.Items.Add(this.lblcolorcode);
            this.panelOnroad.Items.Add(this.txtcolorcode);
            this.panelOnroad.Items.Add(this.lblcolorname);
            this.panelOnroad.Items.Add(this.txtcolorname);
            this.panelOnroad.Items.Add(this.lblinsidesetcode);
            this.panelOnroad.Items.Add(this.txtinsidesetcode);
            this.panelOnroad.Items.Add(this.lblinsidesetname);
            this.panelOnroad.Items.Add(this.txtinsidesetname);
            this.panelOnroad.Items.Add(this.lblsubsets);
            this.panelOnroad.Items.Add(this.txtsubsets);
            this.panelOnroad.Items.Add(this.lblcarstate);
            this.panelOnroad.Items.Add(this.cbcarstate);
            this.panelOnroad.Items.Add(this.lblproperty);
            this.panelOnroad.Items.Add(this.txtproperty);
            this.panelOnroad.Items.Add(this.lblinprice);
            this.panelOnroad.Items.Add(this.numinprice);
            this.panelOnroad.Items.Add(this.txtcartypeid);
            this.panelOnroad.Location = new System.Drawing.Point(0, 0);
            this.panelOnroad.Name = "panelOnroad";
            this.panelOnroad.Size = new System.Drawing.Size(557, 334);
            this.panelOnroad.TabIndex = 3;
            this.panelOnroad.VisualStyle = C1.Win.C1InputPanel.VisualStyle.Office2010Blue;
            // 
            // tblOnroadBindingSource
            // 
            this.tblOnroadBindingSource.AllowNew = true;
            this.tblOnroadBindingSource.DataMember = "tbl_onroad";
            this.tblOnroadBindingSource.DataSource = this.cmsDB;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // navOnroad
            // 
            this.navOnroad.AddNewImage = global::CarSaleMan.Properties.Resources.btnPlus2;
            this.navOnroad.AddNewToolTip = "Add New";
            this.navOnroad.ApplyImage = global::CarSaleMan.Properties.Resources.btnCheck;
            this.navOnroad.ApplyToolTip = "Apply Changes";
            this.navOnroad.CancelImage = global::CarSaleMan.Properties.Resources.btnStop;
            this.navOnroad.CancelToolTip = "Cancel Changes";
            this.navOnroad.CountLabelFormat = "/ {0}";
            this.navOnroad.DataSource = this.tblOnroadBindingSource;
            this.navOnroad.DeleteImage = global::CarSaleMan.Properties.Resources.btnMinus2;
            this.navOnroad.DeleteToolTip = "Delete";
            this.navOnroad.EditImage = ((System.Drawing.Image)(resources.GetObject("navOnroad.EditImage")));
            this.navOnroad.EditToolTip = "Edit";
            this.navOnroad.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Center;
            this.navOnroad.MoveFirstImage = global::CarSaleMan.Properties.Resources.btnFirst;
            this.navOnroad.MoveFirstToolTip = "Move First";
            this.navOnroad.MoveLastImage = global::CarSaleMan.Properties.Resources.btnLast;
            this.navOnroad.MoveLastToolTip = "Move Last";
            this.navOnroad.MoveNextImage = global::CarSaleMan.Properties.Resources.btnRight;
            this.navOnroad.MoveNextToolTip = "Move Next";
            this.navOnroad.MovePreviousImage = global::CarSaleMan.Properties.Resources.btnLeft;
            this.navOnroad.MovePreviousToolTip = "Move Previous";
            this.navOnroad.Name = "navOnroad";
            this.navOnroad.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.navOnroad.ReloadImage = ((System.Drawing.Image)(resources.GetObject("navOnroad.ReloadImage")));
            this.navOnroad.ReloadToolTip = "Reload Data";
            this.navOnroad.SaveImage = global::CarSaleMan.Properties.Resources.btnSave;
            this.navOnroad.SaveToolTip = "Save Data";
            this.navOnroad.ShowSaveButton = true;
            // 
            // sepLine
            // 
            this.sepLine.Height = 11;
            this.sepLine.Name = "sepLine";
            this.sepLine.Width = 525;
            // 
            // lblbillno
            // 
            this.lblbillno.Name = "lblbillno";
            this.lblbillno.Padding = new System.Windows.Forms.Padding(5);
            this.lblbillno.Text = "原始提单号:";
            this.lblbillno.Width = 80;
            // 
            // txtbillno
            // 
            this.txtbillno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtbillno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "billno", true));
            this.txtbillno.Name = "txtbillno";
            this.txtbillno.Padding = new System.Windows.Forms.Padding(5);
            this.txtbillno.Width = 160;
            // 
            // lblbilldate
            // 
            this.lblbilldate.Name = "lblbilldate";
            this.lblbilldate.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblbilldate.Text = "开单日期:";
            this.lblbilldate.Width = 110;
            // 
            // dtpbilldate
            // 
            this.dtpbilldate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "billdate", true));
            this.dtpbilldate.Name = "dtpbilldate";
            this.dtpbilldate.Padding = new System.Windows.Forms.Padding(5);
            this.dtpbilldate.Width = 160;
            // 
            // lblvin
            // 
            this.lblvin.Name = "lblvin";
            this.lblvin.Padding = new System.Windows.Forms.Padding(5);
            this.lblvin.Text = "VIN码:";
            this.lblvin.Width = 80;
            // 
            // txtvin
            // 
            this.txtvin.AsciiOnly = true;
            this.txtvin.BeepOnError = true;
            this.txtvin.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtvin.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "vin", true));
            this.txtvin.Mask = "AAAAAAAAAAAAAAAAA";
            this.txtvin.Name = "txtvin";
            this.txtvin.Padding = new System.Windows.Forms.Padding(5);
            this.txtvin.PromptChar = '#';
            this.txtvin.Width = 160;
            // 
            // lblengineno
            // 
            this.lblengineno.Name = "lblengineno";
            this.lblengineno.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblengineno.Text = "发动机号:";
            this.lblengineno.Width = 110;
            // 
            // txtengineno
            // 
            this.txtengineno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "engineno", true));
            this.txtengineno.Name = "txtengineno";
            this.txtengineno.Padding = new System.Windows.Forms.Padding(5);
            this.txtengineno.Width = 160;
            // 
            // lblcartype
            // 
            this.lblcartype.Name = "lblcartype";
            this.lblcartype.Padding = new System.Windows.Forms.Padding(5);
            this.lblcartype.Text = "车辆代码:";
            this.lblcartype.Width = 80;
            // 
            // cbcartype
            // 
            this.cbcartype.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbcartype.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblOnroadBindingSource, "cartype", true));
            this.cbcartype.DataSource = this.tblCartypeBindingSource;
            this.cbcartype.DisplayMember = "carcode";
            this.cbcartype.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbcartype.Name = "cbcartype";
            this.cbcartype.Padding = new System.Windows.Forms.Padding(5);
            this.cbcartype.ValueMember = "carcode";
            this.cbcartype.Width = 160;
            // 
            // tblCartypeBindingSource
            // 
            this.tblCartypeBindingSource.DataMember = "tbl_cartype";
            this.tblCartypeBindingSource.DataSource = this.cmsDB;
            // 
            // lblcarname
            // 
            this.lblcarname.Name = "lblcarname";
            this.lblcarname.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblcarname.Text = "车辆名称:";
            this.lblcarname.Width = 110;
            // 
            // txtcartypename
            // 
            this.txtcartypename.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "carname", true));
            this.txtcartypename.Enabled = false;
            this.txtcartypename.Name = "txtcartypename";
            this.txtcartypename.Padding = new System.Windows.Forms.Padding(5);
            this.txtcartypename.Width = 160;
            // 
            // lblcolorcode
            // 
            this.lblcolorcode.Name = "lblcolorcode";
            this.lblcolorcode.Padding = new System.Windows.Forms.Padding(5);
            this.lblcolorcode.Text = "颜色代码:";
            this.lblcolorcode.Width = 80;
            // 
            // txtcolorcode
            // 
            this.txtcolorcode.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtcolorcode.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "colorcode", true));
            this.txtcolorcode.Name = "txtcolorcode";
            this.txtcolorcode.Padding = new System.Windows.Forms.Padding(5);
            this.txtcolorcode.ReadOnly = true;
            this.txtcolorcode.Width = 160;
            // 
            // lblcolorname
            // 
            this.lblcolorname.Name = "lblcolorname";
            this.lblcolorname.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblcolorname.Text = "颜色名称:";
            this.lblcolorname.Width = 110;
            // 
            // txtcolorname
            // 
            this.txtcolorname.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "colorname", true));
            this.txtcolorname.Name = "txtcolorname";
            this.txtcolorname.Padding = new System.Windows.Forms.Padding(5);
            this.txtcolorname.ReadOnly = true;
            this.txtcolorname.Width = 160;
            // 
            // lblinsidesetcode
            // 
            this.lblinsidesetcode.Name = "lblinsidesetcode";
            this.lblinsidesetcode.Padding = new System.Windows.Forms.Padding(5);
            this.lblinsidesetcode.Text = "内饰:";
            this.lblinsidesetcode.Width = 80;
            // 
            // txtinsidesetcode
            // 
            this.txtinsidesetcode.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtinsidesetcode.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "insidesetcode", true));
            this.txtinsidesetcode.Enabled = false;
            this.txtinsidesetcode.Name = "txtinsidesetcode";
            this.txtinsidesetcode.Padding = new System.Windows.Forms.Padding(5);
            this.txtinsidesetcode.Width = 160;
            // 
            // lblinsidesetname
            // 
            this.lblinsidesetname.Name = "lblinsidesetname";
            this.lblinsidesetname.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblinsidesetname.Text = "内饰名:";
            this.lblinsidesetname.Width = 110;
            // 
            // txtinsidesetname
            // 
            this.txtinsidesetname.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "insidesetname", true));
            this.txtinsidesetname.Enabled = false;
            this.txtinsidesetname.Name = "txtinsidesetname";
            this.txtinsidesetname.Padding = new System.Windows.Forms.Padding(5);
            this.txtinsidesetname.Width = 160;
            // 
            // lblsubsets
            // 
            this.lblsubsets.Name = "lblsubsets";
            this.lblsubsets.Padding = new System.Windows.Forms.Padding(5);
            this.lblsubsets.Text = "选装包:";
            this.lblsubsets.Width = 80;
            // 
            // txtsubsets
            // 
            this.txtsubsets.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtsubsets.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "subsets", true));
            this.txtsubsets.Enabled = false;
            this.txtsubsets.Name = "txtsubsets";
            this.txtsubsets.Padding = new System.Windows.Forms.Padding(5);
            this.txtsubsets.Width = 160;
            // 
            // lblcarstate
            // 
            this.lblcarstate.Name = "lblcarstate";
            this.lblcarstate.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblcarstate.Text = "状态名称:";
            this.lblcarstate.Width = 110;
            // 
            // cbcarstate
            // 
            this.cbcarstate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblOnroadBindingSource, "carstate", true));
            this.cbcarstate.DataSource = this.CarstateBindingSource;
            this.cbcarstate.DisplayMember = "value";
            this.cbcarstate.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbcarstate.Name = "cbcarstate";
            this.cbcarstate.Padding = new System.Windows.Forms.Padding(5);
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
            this.lblproperty.Name = "lblproperty";
            this.lblproperty.Padding = new System.Windows.Forms.Padding(5);
            this.lblproperty.Text = "属性名称:";
            this.lblproperty.Width = 80;
            // 
            // txtproperty
            // 
            this.txtproperty.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtproperty.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "property", true));
            this.txtproperty.Name = "txtproperty";
            this.txtproperty.Padding = new System.Windows.Forms.Padding(5);
            this.txtproperty.Width = 160;
            // 
            // lblinprice
            // 
            this.lblinprice.Name = "lblinprice";
            this.lblinprice.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblinprice.Text = "进价:";
            this.lblinprice.Width = 110;
            // 
            // numinprice
            // 
            this.numinprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCartypeBindingSource, "inprice", true));
            this.numinprice.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tblOnroadBindingSource, "inprice", true));
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
            // txtcartypeid
            // 
            this.txtcartypeid.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtcartypeid.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblOnroadBindingSource, "cartypeid", true));
            this.txtcartypeid.Name = "txtcartypeid";
            this.txtcartypeid.TabStop = false;
            this.txtcartypeid.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.txtcartypeid.Width = 22;
            // 
            // tblOnroadTableAdapter
            // 
            this.tblOnroadTableAdapter.ClearBeforeFill = true;
            // 
            // tblBasedataTableAdapter
            // 
            this.tblBasedataTableAdapter.ClearBeforeFill = true;
            // 
            // tblCartypeTableAdapter
            // 
            this.tblCartypeTableAdapter.ClearBeforeFill = true;
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
            // FrmOnRoadCarEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(557, 397);
            this.Controls.Add(this.panelOnroad);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmOnRoadCarEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "车辆修改";
            this.Load += new System.EventHandler(this.FrmOnRoadCarEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelOnroad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOnroadBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCartypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarstateBindingSource)).EndInit();
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
        private C1.Win.C1InputPanel.InputDataNavigator navOnroad;
        private C1.Win.C1InputPanel.InputSeparator sepLine;
        private C1.Win.C1InputPanel.InputLabel lblbillno;
        private C1.Win.C1InputPanel.InputTextBox txtbillno;
        private C1.Win.C1InputPanel.InputLabel lblbilldate;
        private C1.Win.C1InputPanel.InputDatePicker dtpbilldate;
        private C1.Win.C1InputPanel.InputLabel lblvin;
        private C1.Win.C1InputPanel.InputLabel lblengineno;
        private C1.Win.C1InputPanel.InputTextBox txtengineno;
        private C1.Win.C1InputPanel.InputLabel lblcartype;
        private C1.Win.C1InputPanel.InputLabel lblcarname;
        private C1.Win.C1InputPanel.InputLabel lblcolorcode;
        private C1.Win.C1InputPanel.InputLabel lblcolorname;
        private C1.Win.C1InputPanel.InputLabel lblinsidesetcode;
        private C1.Win.C1InputPanel.InputLabel lblinsidesetname;
        private C1.Win.C1InputPanel.InputLabel lblsubsets;
        private C1.Win.C1InputPanel.InputLabel lblcarstate;
        private C1.Win.C1InputPanel.InputLabel lblproperty;
        private C1.Win.C1InputPanel.InputTextBox txtproperty;
        private C1.Win.C1InputPanel.InputLabel lblinprice;
        private C1.Win.C1InputPanel.InputNumericBox numinprice;
        private C1.Win.C1InputPanel.InputComboBox cbcarstate;
        private System.Windows.Forms.BindingSource CarstateBindingSource;
        private CarSaleMan.CmsDBTableAdapters.tbl_basedataTableAdapter tblBasedataTableAdapter;
        private C1.Win.C1InputPanel.InputComboBox cbcartype;
        private System.Windows.Forms.BindingSource tblCartypeBindingSource;
        private CarSaleMan.CmsDBTableAdapters.tbl_cartypeTableAdapter tblCartypeTableAdapter;
        private C1.Win.C1InputPanel.InputTextBox txtcartypeid;
        private C1.Win.C1InputPanel.InputTextBox txtcolorcode;
        private C1.Win.C1InputPanel.InputTextBox txtcolorname;
        private C1.Win.C1InputPanel.InputMaskedTextBox txtvin;
        private C1.Win.C1InputPanel.InputTextBox txtcartypename;
        private C1.Win.C1InputPanel.InputTextBox txtinsidesetcode;
        private C1.Win.C1InputPanel.InputTextBox txtinsidesetname;
        private C1.Win.C1InputPanel.InputTextBox txtsubsets;
        private System.Windows.Forms.BindingSource tblStorechangeBindingSource;
        private CmsDBTableAdapters.tbl_storechangeTableAdapter tblStorechangeTableAdapter;
    }
}