namespace CarSaleMan
{
    partial class FrmImportExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportExcel));
            this.gridImportCar = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.xlsRoadCar = new C1.C1Excel.C1XLBook();
            this.tblCarseriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.tblCarseriesTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_basedataTableAdapter();
            this.tblOnroadBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblOnroadTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_onroadTableAdapter();
            this.tblCartypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblCartypeTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_cartypeTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gridImportCar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCarseriesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOnroadBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCartypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridImportCar
            // 
            this.gridImportCar.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.gridImportCar.AllowEditing = false;
            this.gridImportCar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridImportCar.AutoResize = true;
            this.gridImportCar.BackColor = System.Drawing.Color.White;
            this.gridImportCar.ColumnInfo = "17,1,0,0,0,95,Columns:1{AllowEditing:False;}\t";
            this.gridImportCar.EditOptions = ((C1.Win.C1FlexGrid.EditFlags)(((C1.Win.C1FlexGrid.EditFlags.AutoSearch | C1.Win.C1FlexGrid.EditFlags.ExitOnLeftRightKeys)
                        | C1.Win.C1FlexGrid.EditFlags.EditOnRequest)));
            this.gridImportCar.Location = new System.Drawing.Point(2, 1);
            this.gridImportCar.Name = "gridImportCar";
            this.gridImportCar.Rows.Count = 1;
            this.gridImportCar.Rows.DefaultSize = 19;
            this.gridImportCar.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
            this.gridImportCar.Size = new System.Drawing.Size(791, 517);
            this.gridImportCar.StyleInfo = resources.GetString("gridImportCar.StyleInfo");
            this.gridImportCar.TabIndex = 12;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnSave.Location = new System.Drawing.Point(578, 531);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 531);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "总数：";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnCancel.Location = new System.Drawing.Point(682, 531);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "返回";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCount.Location = new System.Drawing.Point(85, 531);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 16);
            this.lblCount.TabIndex = 16;
            // 
            // tblCarseriesBindingSource
            // 
            this.tblCarseriesBindingSource.DataMember = "tbl_basedata";
            this.tblCarseriesBindingSource.DataSource = this.cmsDB;
            this.tblCarseriesBindingSource.Filter = "name = \'车型大类\'";
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblCarseriesTableAdapter
            // 
            this.tblCarseriesTableAdapter.ClearBeforeFill = true;
            // 
            // tblOnroadBindingSource
            // 
            this.tblOnroadBindingSource.DataMember = "tbl_onroad";
            this.tblOnroadBindingSource.DataSource = this.cmsDB;
            // 
            // tblOnroadTableAdapter
            // 
            this.tblOnroadTableAdapter.ClearBeforeFill = true;
            // 
            // tblCartypeBindingSource
            // 
            this.tblCartypeBindingSource.DataMember = "tbl_cartype";
            this.tblCartypeBindingSource.DataSource = this.cmsDB;
            // 
            // tblCartypeTableAdapter
            // 
            this.tblCartypeTableAdapter.ClearBeforeFill = true;
            // 
            // FrmImportExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gridImportCar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmImportExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "在途/未提车辆";
            this.Load += new System.EventHandler(this.FrmImportExcel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridImportCar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCarseriesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOnroadBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCartypeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCount;
        private C1.C1Excel.C1XLBook xlsRoadCar;
        public C1.Win.C1FlexGrid.C1FlexGrid gridImportCar;
        private System.Windows.Forms.BindingSource tblCarseriesBindingSource;
        private CmsDB cmsDB;
        private CmsDBTableAdapters.tbl_basedataTableAdapter tblCarseriesTableAdapter;
        private System.Windows.Forms.BindingSource tblOnroadBindingSource;
        private CmsDBTableAdapters.tbl_onroadTableAdapter tblOnroadTableAdapter;
        private System.Windows.Forms.BindingSource tblCartypeBindingSource;
        private CmsDBTableAdapters.tbl_cartypeTableAdapter tblCartypeTableAdapter;
    }
}