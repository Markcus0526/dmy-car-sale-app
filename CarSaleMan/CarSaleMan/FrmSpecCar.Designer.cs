namespace CarSaleMan
{
    partial class FrmSpecCar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSpecCar));
            this.gridSpecCar = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.vwSpeccarBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbFindAll = new System.Windows.Forms.ToolStripButton();
            this.tsbFindCondition = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExportExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbChange = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHistory = new System.Windows.Forms.ToolStripButton();
            this.tsbReturn = new System.Windows.Forms.ToolStripButton();
            this.vwSpeccarTableAdapter = new CarSaleMan.CmsDBTableAdapters.vw_speccarTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gridSpecCar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwSpeccarBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridSpecCar
            // 
            this.gridSpecCar.AllowEditing = false;
            this.gridSpecCar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSpecCar.BackColor = System.Drawing.Color.White;
            this.gridSpecCar.ColumnInfo = resources.GetString("gridSpecCar.ColumnInfo");
            this.gridSpecCar.DataSource = this.vwSpeccarBindingSource;
            this.gridSpecCar.Location = new System.Drawing.Point(1, 64);
            this.gridSpecCar.Name = "gridSpecCar";
            this.gridSpecCar.Rows.Count = 1;
            this.gridSpecCar.Rows.DefaultSize = 19;
            this.gridSpecCar.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridSpecCar.Size = new System.Drawing.Size(918, 393);
            this.gridSpecCar.StyleInfo = resources.GetString("gridSpecCar.StyleInfo");
            this.gridSpecCar.TabIndex = 11;
            // 
            // vwSpeccarBindingSource
            // 
            this.vwSpeccarBindingSource.DataMember = "vw_speccar";
            this.vwSpeccarBindingSource.DataSource = this.cmsDB;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(1, 458);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(918, 10);
            this.progressBar1.TabIndex = 12;
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.toolStrip.BackgroundImage = global::CarSaleMan.Properties.Resources.bkToolBar;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFindAll,
            this.tsbFindCondition,
            this.tsbHistory,
            this.toolStripSeparator1,
            this.tsbExportExcel,
            this.tsbChange,
            this.toolStripSeparator2,
            this.tsbReturn});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.Size = new System.Drawing.Size(921, 60);
            this.toolStrip.TabIndex = 10;
            this.toolStrip.Text = "ToolBar";
            // 
            // tsbFindAll
            // 
            this.tsbFindAll.AutoSize = false;
            this.tsbFindAll.Image = global::CarSaleMan.Properties.Resources.btnAllSelect;
            this.tsbFindAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindAll.Name = "tsbFindAll";
            this.tsbFindAll.Size = new System.Drawing.Size(60, 51);
            this.tsbFindAll.Text = "浏览全部";
            this.tsbFindAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbFindAll.Click += new System.EventHandler(this.tsbFindAll_Click);
            // 
            // tsbFindCondition
            // 
            this.tsbFindCondition.AutoSize = false;
            this.tsbFindCondition.Image = global::CarSaleMan.Properties.Resources.btnFind;
            this.tsbFindCondition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindCondition.Name = "tsbFindCondition";
            this.tsbFindCondition.Size = new System.Drawing.Size(60, 51);
            this.tsbFindCondition.Text = "条件查询";
            this.tsbFindCondition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbFindCondition.Click += new System.EventHandler(this.tsbFindCondition_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 60);
            // 
            // tsbExportExcel
            // 
            this.tsbExportExcel.AutoSize = false;
            this.tsbExportExcel.Image = global::CarSaleMan.Properties.Resources.btnExcel;
            this.tsbExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportExcel.Name = "tsbExportExcel";
            this.tsbExportExcel.Size = new System.Drawing.Size(60, 51);
            this.tsbExportExcel.Text = "导出EXCEL";
            this.tsbExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbExportExcel.Click += new System.EventHandler(this.tsbExportExcel_Click);
            // 
            // tsbChange
            // 
            this.tsbChange.AutoSize = false;
            this.tsbChange.Image = global::CarSaleMan.Properties.Resources.btnRepair;
            this.tsbChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChange.Name = "tsbChange";
            this.tsbChange.Size = new System.Drawing.Size(60, 51);
            this.tsbChange.Text = "车辆修改";
            this.tsbChange.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbChange.Click += new System.EventHandler(this.tsbChange_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 60);
            // 
            // tsbHistory
            // 
            this.tsbHistory.AutoSize = false;
            this.tsbHistory.Image = global::CarSaleMan.Properties.Resources.btnHistory;
            this.tsbHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHistory.Name = "tsbHistory";
            this.tsbHistory.Size = new System.Drawing.Size(60, 51);
            this.tsbHistory.Text = "单车流转";
            this.tsbHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbHistory.Click += new System.EventHandler(this.tsbHistory_Click);
            // 
            // tsbReturn
            // 
            this.tsbReturn.AutoSize = false;
            this.tsbReturn.Image = global::CarSaleMan.Properties.Resources.btnBack;
            this.tsbReturn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReturn.Name = "tsbReturn";
            this.tsbReturn.Size = new System.Drawing.Size(60, 51);
            this.tsbReturn.Text = "近回系统";
            this.tsbReturn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbReturn.Click += new System.EventHandler(this.tsbReturn_Click);
            // 
            // vwSpeccarTableAdapter
            // 
            this.vwSpeccarTableAdapter.ClearBeforeFill = true;
            // 
            // FrmSpecCar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(921, 469);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.gridSpecCar);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSpecCar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "特种车统计表";
            this.Load += new System.EventHandler(this.FrmSpecCar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSpecCar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwSpeccarBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbFindAll;
        private System.Windows.Forms.ToolStripButton tsbFindCondition;
        private System.Windows.Forms.ToolStripButton tsbExportExcel;
        private System.Windows.Forms.ToolStripButton tsbChange;
        private System.Windows.Forms.ToolStripButton tsbHistory;
        private System.Windows.Forms.ToolStripButton tsbReturn;
        private C1.Win.C1FlexGrid.C1FlexGrid gridSpecCar;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource vwSpeccarBindingSource;
        private CmsDBTableAdapters.vw_speccarTableAdapter vwSpeccarTableAdapter;
    }
}