namespace CarSaleMan
{
    partial class FrmAppendRepairEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAppendRepairEdit));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelFit = new C1.Win.C1InputPanel.C1InputPanel();
            this.cmsDB = new CarSaleMan.CmsDB();
            this.tblFitBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblFitTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_fitTableAdapter();
            this.navFit = new C1.Win.C1InputPanel.InputDataNavigator();
            this.sepLine = new C1.Win.C1InputPanel.InputSeparator();
            this.lbluid = new C1.Win.C1InputPanel.InputLabel();
            this.numuid = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblconsumer = new C1.Win.C1InputPanel.InputLabel();
            this.txtconsumer = new C1.Win.C1InputPanel.InputTextBox();
            this.lblseller = new C1.Win.C1InputPanel.InputLabel();
            this.txtseller = new C1.Win.C1InputPanel.InputTextBox();
            this.lblfitdate = new C1.Win.C1InputPanel.InputLabel();
            this.dtpfitdate = new C1.Win.C1InputPanel.InputDatePicker();
            this.lblfitprice = new C1.Win.C1InputPanel.InputLabel();
            this.numfitprice = new C1.Win.C1InputPanel.InputNumericBox();
            this.lblremark = new C1.Win.C1InputPanel.InputLabel();
            this.txtremark = new C1.Win.C1InputPanel.InputTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelFit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblFitBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(152, 224);
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
            this.btnCancel.Location = new System.Drawing.Point(321, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "返  回";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panelFit
            // 
            this.panelFit.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panelFit.DataSource = this.tblFitBindingSource;
            this.panelFit.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.panelFit.Items.Add(this.navFit);
            this.panelFit.Items.Add(this.sepLine);
            this.panelFit.Items.Add(this.lblconsumer);
            this.panelFit.Items.Add(this.txtconsumer);
            this.panelFit.Items.Add(this.lblseller);
            this.panelFit.Items.Add(this.txtseller);
            this.panelFit.Items.Add(this.lblfitdate);
            this.panelFit.Items.Add(this.dtpfitdate);
            this.panelFit.Items.Add(this.lblfitprice);
            this.panelFit.Items.Add(this.numfitprice);
            this.panelFit.Items.Add(this.lblremark);
            this.panelFit.Items.Add(this.txtremark);
            this.panelFit.Items.Add(this.lbluid);
            this.panelFit.Items.Add(this.numuid);
            this.panelFit.Location = new System.Drawing.Point(0, 0);
            this.panelFit.Name = "panelFit";
            this.panelFit.Size = new System.Drawing.Size(557, 199);
            this.panelFit.TabIndex = 3;
            this.panelFit.VisualStyle = C1.Win.C1InputPanel.VisualStyle.Office2010Blue;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblFitBindingSource
            // 
            this.tblFitBindingSource.DataMember = "tbl_fit";
            this.tblFitBindingSource.DataSource = this.cmsDB;
            // 
            // tblFitTableAdapter
            // 
            this.tblFitTableAdapter.ClearBeforeFill = true;
            // 
            // navFit
            // 
            this.navFit.AddNewImage = global::CarSaleMan.Properties.Resources.btnPlus2;
            this.navFit.AddNewToolTip = "Add New";
            this.navFit.ApplyImage = global::CarSaleMan.Properties.Resources.btnCheck;
            this.navFit.ApplyToolTip = "Apply Changes";
            this.navFit.CancelImage = global::CarSaleMan.Properties.Resources.btnStop;
            this.navFit.CancelToolTip = "Cancel Changes";
            this.navFit.DataSource = this.tblFitBindingSource;
            this.navFit.DeleteImage = global::CarSaleMan.Properties.Resources.btnMinus2;
            this.navFit.DeleteToolTip = "Delete";
            this.navFit.EditImage = ((System.Drawing.Image)(resources.GetObject("navFit.EditImage")));
            this.navFit.EditToolTip = "Edit";
            this.navFit.MoveFirstImage = global::CarSaleMan.Properties.Resources.btnFirst;
            this.navFit.MoveFirstToolTip = "Move First";
            this.navFit.MoveLastImage = global::CarSaleMan.Properties.Resources.btnLast;
            this.navFit.MoveLastToolTip = "Move Last";
            this.navFit.MoveNextImage = global::CarSaleMan.Properties.Resources.btnRight;
            this.navFit.MoveNextToolTip = "Move Next";
            this.navFit.MovePreviousImage = global::CarSaleMan.Properties.Resources.btnLeft;
            this.navFit.MovePreviousToolTip = "Move Previous";
            this.navFit.Name = "navFit";
            this.navFit.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.navFit.ReloadImage = ((System.Drawing.Image)(resources.GetObject("navFit.ReloadImage")));
            this.navFit.ReloadToolTip = "Reload Data";
            this.navFit.SaveImage = global::CarSaleMan.Properties.Resources.btnSave;
            this.navFit.SaveToolTip = "Save Data";
            this.navFit.ShowSaveButton = true;
            // 
            // sepLine
            // 
            this.sepLine.Height = 11;
            this.sepLine.Name = "sepLine";
            this.sepLine.Width = 531;
            // 
            // lbluid
            // 
            this.lbluid.Name = "lbluid";
            this.lbluid.Text = "&uid:";
            this.lbluid.Visibility = C1.Win.C1InputPanel.Visibility.Hidden;
            this.lbluid.Width = 24;
            // 
            // numuid
            // 
            this.numuid.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblFitBindingSource, "uid", true));
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
            this.numuid.Width = 40;
            // 
            // lblconsumer
            // 
            this.lblconsumer.Name = "lblconsumer";
            this.lblconsumer.Padding = new System.Windows.Forms.Padding(5);
            this.lblconsumer.Text = "&consumer:";
            this.lblconsumer.Width = 80;
            // 
            // txtconsumer
            // 
            this.txtconsumer.Break = C1.Win.C1InputPanel.BreakType.None;
            this.txtconsumer.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblFitBindingSource, "consumer", true));
            this.txtconsumer.Name = "txtconsumer";
            this.txtconsumer.Padding = new System.Windows.Forms.Padding(5);
            this.txtconsumer.Width = 160;
            // 
            // lblseller
            // 
            this.lblseller.Name = "lblseller";
            this.lblseller.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblseller.Text = "&seller:";
            this.lblseller.Width = 110;
            // 
            // txtseller
            // 
            this.txtseller.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblFitBindingSource, "seller", true));
            this.txtseller.Name = "txtseller";
            this.txtseller.Padding = new System.Windows.Forms.Padding(5);
            this.txtseller.Width = 160;
            // 
            // lblfitdate
            // 
            this.lblfitdate.Name = "lblfitdate";
            this.lblfitdate.Padding = new System.Windows.Forms.Padding(5);
            this.lblfitdate.Text = "&fitdate:";
            this.lblfitdate.Width = 80;
            // 
            // dtpfitdate
            // 
            this.dtpfitdate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.dtpfitdate.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblFitBindingSource, "fitdate", true));
            this.dtpfitdate.Name = "dtpfitdate";
            this.dtpfitdate.Padding = new System.Windows.Forms.Padding(5);
            this.dtpfitdate.Width = 160;
            // 
            // lblfitprice
            // 
            this.lblfitprice.Name = "lblfitprice";
            this.lblfitprice.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
            this.lblfitprice.Text = "f&itprice:";
            this.lblfitprice.Width = 110;
            // 
            // numfitprice
            // 
            this.numfitprice.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblFitBindingSource, "fitprice", true));
            this.numfitprice.Format = "N2";
            this.numfitprice.Name = "numfitprice";
            this.numfitprice.Padding = new System.Windows.Forms.Padding(5);
            this.numfitprice.Width = 160;
            // 
            // lblremark
            // 
            this.lblremark.Name = "lblremark";
            this.lblremark.Padding = new System.Windows.Forms.Padding(5);
            this.lblremark.Text = "&remark:";
            this.lblremark.Width = 80;
            // 
            // txtremark
            // 
            this.txtremark.DataBindings.Add(new System.Windows.Forms.Binding("BoundValue", this.tblFitBindingSource, "remark", true));
            this.txtremark.Name = "txtremark";
            this.txtremark.Padding = new System.Windows.Forms.Padding(5);
            this.txtremark.Width = 438;
            // 
            // FrmAppendRepairEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(557, 274);
            this.Controls.Add(this.panelFit);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmAppendRepairEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "车辆修改";
            this.Load += new System.EventHandler(this.FrmAppendRepairEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelFit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblFitBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private C1.Win.C1InputPanel.C1InputPanel panelFit;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource tblFitBindingSource;
        private CmsDBTableAdapters.tbl_fitTableAdapter tblFitTableAdapter;
        private C1.Win.C1InputPanel.InputDataNavigator navFit;
        private C1.Win.C1InputPanel.InputSeparator sepLine;
        private C1.Win.C1InputPanel.InputLabel lblconsumer;
        private C1.Win.C1InputPanel.InputTextBox txtconsumer;
        private C1.Win.C1InputPanel.InputLabel lblseller;
        private C1.Win.C1InputPanel.InputTextBox txtseller;
        private C1.Win.C1InputPanel.InputLabel lblfitdate;
        private C1.Win.C1InputPanel.InputDatePicker dtpfitdate;
        private C1.Win.C1InputPanel.InputLabel lblfitprice;
        private C1.Win.C1InputPanel.InputNumericBox numfitprice;
        private C1.Win.C1InputPanel.InputLabel lblremark;
        private C1.Win.C1InputPanel.InputTextBox txtremark;
        private C1.Win.C1InputPanel.InputLabel lbluid;
        private C1.Win.C1InputPanel.InputNumericBox numuid;
    }
}