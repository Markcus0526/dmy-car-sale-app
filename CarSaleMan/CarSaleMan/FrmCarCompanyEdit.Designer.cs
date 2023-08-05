namespace CarSaleMan
{
    partial class FrmCarCompanyEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCarCompanyEdit));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelCarCompany = new C1.Win.C1InputPanel.C1InputPanel();
            this.tblCarcompanyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.navCarcompany = new C1.Win.C1InputPanel.InputDataNavigator();
            this.sepLine = new C1.Win.C1InputPanel.InputSeparator();
            this.lblcompanyno = new C1.Win.C1InputPanel.InputLabel();
            this.numcompanyno = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblcompanyname = new C1.Win.C1InputPanel.InputLabel();
            this.txtcompanyname = new C1.Win.C1InputPanel.InputTextBox();
            this.lbladdress = new C1.Win.C1InputPanel.InputLabel();
            this.txtaddress = new C1.Win.C1InputPanel.InputTextBox();
            this.lblmanagername = new C1.Win.C1InputPanel.InputLabel();
            this.txtmanagername = new C1.Win.C1InputPanel.InputTextBox();
            this.lbllinkmanname = new C1.Win.C1InputPanel.InputLabel();
            this.txtlinkmanname = new C1.Win.C1InputPanel.InputTextBox();
            this.lblpostno = new C1.Win.C1InputPanel.InputLabel();
            this.txtpostno = new C1.Win.C1InputPanel.InputTextBox();
            this.lbltelno = new C1.Win.C1InputPanel.InputLabel();
            this.txttelno = new C1.Win.C1InputPanel.InputTextBox();
            this.lblemail = new C1.Win.C1InputPanel.InputLabel();
            this.txtemail = new C1.Win.C1InputPanel.InputTextBox();
            this.lblcartypes = new C1.Win.C1InputPanel.InputLabel();
            this.txtcartypes = new C1.Win.C1InputPanel.InputTextBox();
            this.lblproperty = new C1.Win.C1InputPanel.InputLabel();
            this.txtproperty = new C1.Win.C1InputPanel.InputTextBox();
            this.lblremark = new C1.Win.C1InputPanel.InputLabel();
            this.txtremark = new C1.Win.C1InputPanel.InputTextBox();
            this.lbluid = new C1.Win.C1InputPanel.InputLabel();
            this.numuid = new C1.Win.C1InputPanel.InputNumericBox();
            this.tblCarcompanyTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_carcompanyTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.panelCarCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCarcompanyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(159, 402);
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
            this.btnCancel.Location = new System.Drawing.Point(328, 402);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "返  回";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panelCarCompany
            // 
            this.panelCarCompany.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panelCarCompany.DataSource = this.tblCarcompanyBindingSource;
            this.panelCarCompany.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCarCompany.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.panelCarCompany.Items.Add(this.navCarcompany);
            this.panelCarCompany.Items.Add(this.sepLine);
            this.panelCarCompany.Items.Add(this.lblcompanyno);
            this.panelCarCompany.Items.Add(this.numcompanyno);
            this.panelCarCompany.Items.Add(this.lblcompanyname);
            this.panelCarCompany.Items.Add(this.txtcompanyname);
            this.panelCarCompany.Items.Add(this.lbladdress);
            this.panelCarCompany.Items.Add(this.txtaddress);
            this.panelCarCompany.Items.Add(this.lblmanagername);
            this.panelCarCompany.Items.Add(this.txtmanagername);
            this.panelCarCompany.Items.Add(this.lbllinkmanname);
            this.panelCarCompany.Items.Add(this.txtlinkmanname);
            this.panelCarCompany.Items.Add(this.lblpostno);
            this.panelCarCompany.Items.Add(this.txtpostno);
            this.panelCarCompany.Items.Add(this.lbltelno);
            this.panelCarCompany.Items.Add(this.txttelno);
            this.panelCarCompany.Items.Add(this.lblemail);
            this.panelCarCompany.Items.Add(this.txtemail);
            this.panelCarCompany.Items.Add(this.lblcartypes);
            this.panelCarCompany.Items.Add(this.txtcartypes);
            this.panelCarCompany.Items.Add(this.lblproperty);
            this.panelCarCompany.Items.Add(this.txtproperty);
            this.panelCarCompany.Items.Add(this.lblremark);
            this.panelCarCompany.Items.Add(this.txtremark);
            this.panelCarCompany.Items.Add(this.lbluid);
            this.panelCarCompany.Items.Add(this.numuid);
            this.panelCarCompany.Location = new System.Drawing.Point(0, 0);
            this.panelCarCompany.Name = "panelCarCompany";
            this.panelCarCompany.Size = new System.Drawing.Size(559, 380);
            this.panelCarCompany.TabIndex = 3;
            this.panelCarCompany.VisualStyle = C1.Win.C1InputPanel.VisualStyle.Office2010Blue;
            // 
            // tblCarcompanyBindingSource
            // 
            this.tblCarcompanyBindingSource.DataMember = "tbl_carcompany";
            this.tblCarcompanyBindingSource.DataSource = this.cmsDB;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // navCarcompany
            // 
            this.navCarcompany.AddNewImage = global::CarSaleMan.Properties.Resources.btnPlus2;
            this.navCarcompany.AddNewToolTip = "Add New";
            this.navCarcompany.ApplyImage = global::CarSaleMan.Properties.Resources.btnCheck;
            this.navCarcompany.ApplyToolTip = "Apply Changes";
            this.navCarcompany.CancelImage = global::CarSaleMan.Properties.Resources.btnStop;
            this.navCarcompany.CancelToolTip = "Cancel Changes";
            this.navCarcompany.CountLabelFormat = "/ {0}";
            this.navCarcompany.DataSource = this.tblCarcompanyBindingSource;
            this.navCarcompany.DeleteImage = global::CarSaleMan.Properties.Resources.btnMinus2;
            this.navCarcompany.DeleteToolTip = "Delete";
            this.navCarcompany.EditImage = ((System.Drawing.Image)(resources.GetObject("navCarcompany.EditImage")));
            this.navCarcompany.EditToolTip = "Edit";
            this.navCarcompany.MoveFirstImage = global::CarSaleMan.Properties.Resources.btnFirst;
            this.navCarcompany.MoveFirstToolTip = "Move First";
            this.navCarcompany.MoveLastImage = global::CarSaleMan.Properties.Resources.btnLast;
            this.navCarcompany.MoveLastToolTip = "Move Last";
            this.navCarcompany.MoveNextImage = global::CarSaleMan.Properties.Resources.btnRight;
            this.navCarcompany.MoveNextToolTip = "Move Next";
            this.navCarcompany.MovePreviousImage = global::CarSaleMan.Properties.Resources.btnLeft;
            this.navCarcompany.MovePreviousToolTip = "Move Previous";
            this.navCarcompany.Name = "navCarcompany";
            this.navCarcompany.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.navCarcompany.ReloadImage = ((System.Drawing.Image)(resources.GetObject("navCarcompany.ReloadImage")));
            this.navCarcompany.ReloadToolTip = "Reload Data";
            this.navCarcompany.SaveImage = global::CarSaleMan.Properties.Resources.btnSave;
            this.navCarcompany.SaveToolTip = "Save Data";
            this.navCarcompany.ShowSaveButton = true;
            // 
            // sepLine
            // 
            this.sepLine.Height = 11;
            this.sepLine.Name = "sepLine";
            this.sepLine.Width = 533;
            // 
            // lblcompanyno
            // 
            this.lblcompanyno.Name = "lblcompanyno";
            this.lblcompanyno.Padding = new System.Windows.Forms.Padding(5);
            this.lblcompanyno.Text = "进货单位代码:";
            this.lblcompanyno.Width = 90;
            // 
            // numcompanyno
            // 
            this.numcompanyno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCarcompanyBindingSource, "companyno", true));
            this.numcompanyno.Format = "0";
            this.numcompanyno.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numcompanyno.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numcompanyno.Name = "numcompanyno";
            this.numcompanyno.Padding = new System.Windows.Forms.Padding(5);
            this.numcompanyno.Width = 160;
            // 
            // lblcompanyname
            // 
            this.lblcompanyname.Name = "lblcompanyname";
            this.lblcompanyname.Padding = new System.Windows.Forms.Padding(5);
            this.lblcompanyname.Text = "进货单位名称:";
            this.lblcompanyname.Width = 90;
            // 
            // txtcompanyname
            // 
            this.txtcompanyname.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCarcompanyBindingSource, "companyname", true));
            this.txtcompanyname.Name = "txtcompanyname";
            this.txtcompanyname.Padding = new System.Windows.Forms.Padding(5);
            this.txtcompanyname.Width = 428;
            // 
            // lbladdress
            // 
            this.lbladdress.Name = "lbladdress";
            this.lbladdress.Padding = new System.Windows.Forms.Padding(5);
            this.lbladdress.Text = "进货单位地址:";
            this.lbladdress.Width = 90;
            // 
            // txtaddress
            // 
            this.txtaddress.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCarcompanyBindingSource, "address", true));
            this.txtaddress.Name = "txtaddress";
            this.txtaddress.Padding = new System.Windows.Forms.Padding(5);
            this.txtaddress.Width = 428;
            // 
            // lblmanagername
            // 
            this.lblmanagername.Name = "lblmanagername";
            this.lblmanagername.Padding = new System.Windows.Forms.Padding(5);
            this.lblmanagername.Text = "负责人:";
            this.lblmanagername.Width = 90;
            // 
            // txtmanagername
            // 
            this.txtmanagername.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtmanagername.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCarcompanyBindingSource, "managername", true));
            this.txtmanagername.Name = "txtmanagername";
            this.txtmanagername.Padding = new System.Windows.Forms.Padding(5);
            this.txtmanagername.Width = 160;
            // 
            // lbllinkmanname
            // 
            this.lbllinkmanname.Name = "lbllinkmanname";
            this.lbllinkmanname.Padding = new System.Windows.Forms.Padding(30, 5, 5, 5);
            this.lbllinkmanname.Text = "联系人:";
            this.lbllinkmanname.Width = 100;
            // 
            // txtlinkmanname
            // 
            this.txtlinkmanname.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCarcompanyBindingSource, "linkmanname", true));
            this.txtlinkmanname.Name = "txtlinkmanname";
            this.txtlinkmanname.Padding = new System.Windows.Forms.Padding(5);
            this.txtlinkmanname.Width = 160;
            // 
            // lblpostno
            // 
            this.lblpostno.Name = "lblpostno";
            this.lblpostno.Padding = new System.Windows.Forms.Padding(5);
            this.lblpostno.Text = "邮货编码:";
            this.lblpostno.Width = 90;
            // 
            // txtpostno
            // 
            this.txtpostno.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtpostno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCarcompanyBindingSource, "postno", true));
            this.txtpostno.Name = "txtpostno";
            this.txtpostno.Padding = new System.Windows.Forms.Padding(5);
            this.txtpostno.Width = 160;
            // 
            // lbltelno
            // 
            this.lbltelno.Name = "lbltelno";
            this.lbltelno.Padding = new System.Windows.Forms.Padding(30, 5, 5, 5);
            this.lbltelno.Text = "联系电话:";
            this.lbltelno.Width = 100;
            // 
            // txttelno
            // 
            this.txttelno.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCarcompanyBindingSource, "telno", true));
            this.txttelno.Name = "txttelno";
            this.txttelno.Padding = new System.Windows.Forms.Padding(5);
            this.txttelno.Width = 160;
            // 
            // lblemail
            // 
            this.lblemail.Name = "lblemail";
            this.lblemail.Padding = new System.Windows.Forms.Padding(5);
            this.lblemail.Text = "电子邮箱:";
            this.lblemail.Width = 90;
            // 
            // txtemail
            // 
            this.txtemail.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtemail.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCarcompanyBindingSource, "email", true));
            this.txtemail.Name = "txtemail";
            this.txtemail.Padding = new System.Windows.Forms.Padding(5);
            this.txtemail.Width = 160;
            // 
            // lblcartypes
            // 
            this.lblcartypes.Name = "lblcartypes";
            this.lblcartypes.Padding = new System.Windows.Forms.Padding(30, 5, 5, 5);
            this.lblcartypes.Text = "大类型:";
            this.lblcartypes.Width = 100;
            // 
            // txtcartypes
            // 
            this.txtcartypes.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCarcompanyBindingSource, "cartypes", true));
            this.txtcartypes.Name = "txtcartypes";
            this.txtcartypes.Padding = new System.Windows.Forms.Padding(5);
            this.txtcartypes.Width = 160;
            // 
            // lblproperty
            // 
            this.lblproperty.Name = "lblproperty";
            this.lblproperty.Padding = new System.Windows.Forms.Padding(5);
            this.lblproperty.Text = "性质:";
            this.lblproperty.Width = 90;
            // 
            // txtproperty
            // 
            this.txtproperty.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCarcompanyBindingSource, "property", true));
            this.txtproperty.Name = "txtproperty";
            this.txtproperty.Padding = new System.Windows.Forms.Padding(5);
            this.txtproperty.Width = 160;
            // 
            // lblremark
            // 
            this.lblremark.Name = "lblremark";
            this.lblremark.Padding = new System.Windows.Forms.Padding(5);
            this.lblremark.Text = "备注:";
            this.lblremark.Width = 90;
            // 
            // txtremark
            // 
            this.txtremark.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCarcompanyBindingSource, "remark", true));
            this.txtremark.Name = "txtremark";
            this.txtremark.Padding = new System.Windows.Forms.Padding(5);
            this.txtremark.Width = 428;
            // 
            // lbluid
            // 
            this.lbluid.Name = "lbluid";
            this.lbluid.Text = "&uid:";
            this.lbluid.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lbluid.Width = 27;
            // 
            // numuid
            // 
            this.numuid.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblCarcompanyBindingSource, "uid", true));
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
            this.numuid.Width = 27;
            // 
            // tblCarcompanyTableAdapter
            // 
            this.tblCarcompanyTableAdapter.ClearBeforeFill = true;
            // 
            // FrmCarCompanyEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(559, 448);
            this.Controls.Add(this.panelCarCompany);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmCarCompanyEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "进货单位模块修改";
            this.Load += new System.EventHandler(this.FrmCarCompanyEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelCarCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCarcompanyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private C1.Win.C1InputPanel.C1InputPanel panelCarCompany;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource tblCarcompanyBindingSource;
        private CmsDBTableAdapters.tbl_carcompanyTableAdapter tblCarcompanyTableAdapter;
        private C1.Win.C1InputPanel.InputDataNavigator navCarcompany;
        private C1.Win.C1InputPanel.InputSeparator sepLine;
        private C1.Win.C1InputPanel.InputLabel lbluid;
        private C1.Win.C1InputPanel.InputNumericBox numuid;
        private C1.Win.C1InputPanel.InputLabel lblcompanyno;
        private C1.Win.C1InputPanel.InputNumericBox numcompanyno;
        private C1.Win.C1InputPanel.InputLabel lblcompanyname;
        private C1.Win.C1InputPanel.InputTextBox txtcompanyname;
        private C1.Win.C1InputPanel.InputLabel lbladdress;
        private C1.Win.C1InputPanel.InputTextBox txtaddress;
        private C1.Win.C1InputPanel.InputLabel lblpostno;
        private C1.Win.C1InputPanel.InputTextBox txtpostno;
        private C1.Win.C1InputPanel.InputLabel lbltelno;
        private C1.Win.C1InputPanel.InputTextBox txttelno;
        private C1.Win.C1InputPanel.InputLabel lblcartypes;
        private C1.Win.C1InputPanel.InputTextBox txtcartypes;
        private C1.Win.C1InputPanel.InputLabel lblemail;
        private C1.Win.C1InputPanel.InputTextBox txtemail;
        private C1.Win.C1InputPanel.InputLabel lblmanagername;
        private C1.Win.C1InputPanel.InputTextBox txtmanagername;
        private C1.Win.C1InputPanel.InputLabel lbllinkmanname;
        private C1.Win.C1InputPanel.InputTextBox txtlinkmanname;
        private C1.Win.C1InputPanel.InputLabel lblproperty;
        private C1.Win.C1InputPanel.InputTextBox txtproperty;
        private C1.Win.C1InputPanel.InputLabel lblremark;
        private C1.Win.C1InputPanel.InputTextBox txtremark;
    }
}