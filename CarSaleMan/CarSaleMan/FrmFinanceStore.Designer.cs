namespace CarSaleMan
{
    partial class FrmFinanceStore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFinanceStore));
            this.gridStore = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.storFinanceStoreBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbFindAll = new System.Windows.Forms.ToolStripButton();
            this.tsbFindCondition = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExportExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbReturn = new System.Windows.Forms.ToolStripButton();
            this.storFinanceStoreTableAdapter = new CarSaleMan.CmsDBTableAdapters.stor_finance_storeTableAdapter();
            this.tblEnvBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblEnvTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_envTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gridStore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.storFinanceStoreBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblEnvBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridStore
            // 
            this.gridStore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridStore.BackColor = System.Drawing.Color.White;
            this.gridStore.ColumnInfo = resources.GetString("gridStore.ColumnInfo");
            this.gridStore.DataSource = this.storFinanceStoreBindingSource;
            this.gridStore.Location = new System.Drawing.Point(1, 64);
            this.gridStore.Name = "gridStore";
            this.gridStore.Rows.Count = 1;
            this.gridStore.Rows.DefaultSize = 19;
            this.gridStore.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridStore.Size = new System.Drawing.Size(918, 393);
            this.gridStore.StyleInfo = resources.GetString("gridStore.StyleInfo");
            this.gridStore.TabIndex = 11;
            // 
            // storFinanceStoreBindingSource
            // 
            this.storFinanceStoreBindingSource.DataMember = "stor_finance_store";
            this.storFinanceStoreBindingSource.DataSource = this.cmsDB;
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
            this.toolStripSeparator1,
            this.tsbExportExcel,
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 60);
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
            // storFinanceStoreTableAdapter
            // 
            this.storFinanceStoreTableAdapter.ClearBeforeFill = true;
            // 
            // tblEnvBindingSource
            // 
            this.tblEnvBindingSource.DataMember = "tbl_env";
            this.tblEnvBindingSource.DataSource = this.cmsDB;
            // 
            // tblEnvTableAdapter
            // 
            this.tblEnvTableAdapter.ClearBeforeFill = true;
            // 
            // FrmFinanceStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(921, 469);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.gridStore);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFinanceStore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "财务综合统计";
            this.Load += new System.EventHandler(this.FrmFinanceStore_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridStore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.storFinanceStoreBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblEnvBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbFindAll;
        private System.Windows.Forms.ToolStripButton tsbFindCondition;
        private System.Windows.Forms.ToolStripButton tsbExportExcel;
        private System.Windows.Forms.ToolStripButton tsbReturn;
        private C1.Win.C1FlexGrid.C1FlexGrid gridStore;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource storFinanceStoreBindingSource;
        private CmsDBTableAdapters.stor_finance_storeTableAdapter storFinanceStoreTableAdapter;
        private System.Windows.Forms.BindingSource tblEnvBindingSource;
        private CmsDBTableAdapters.tbl_envTableAdapter tblEnvTableAdapter;
    }
}