namespace CarSaleMan
{
    partial class FrmStoreChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStoreChange));
            this.gridStoreChange = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.vwStoreinBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbFindAll = new System.Windows.Forms.ToolStripButton();
            this.tsbFindCondition = new System.Windows.Forms.ToolStripButton();
            this.tsbHistory = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbChange = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbReturn = new System.Windows.Forms.ToolStripButton();
            this.tblStoreinBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblStoreinTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storeinTableAdapter();
            this.vwStoreinTableAdapter = new CarSaleMan.CmsDBTableAdapters.vw_storeinTableAdapter();
            this.tblStorechangeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblStorechangeTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storechangeTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gridStoreChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwStoreinBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreinBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStorechangeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridStoreChange
            // 
            this.gridStoreChange.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridStoreChange.BackColor = System.Drawing.Color.White;
            this.gridStoreChange.ColumnInfo = resources.GetString("gridStoreChange.ColumnInfo");
            this.gridStoreChange.DataSource = this.vwStoreinBindingSource;
            this.gridStoreChange.Location = new System.Drawing.Point(1, 64);
            this.gridStoreChange.Name = "gridStoreChange";
            this.gridStoreChange.Rows.Count = 1;
            this.gridStoreChange.Rows.DefaultSize = 19;
            this.gridStoreChange.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridStoreChange.Size = new System.Drawing.Size(918, 393);
            this.gridStoreChange.StyleInfo = resources.GetString("gridStoreChange.StyleInfo");
            this.gridStoreChange.TabIndex = 11;
            // 
            // vwStoreinBindingSource
            // 
            this.vwStoreinBindingSource.DataMember = "vw_storein";
            this.vwStoreinBindingSource.DataSource = this.cmsDB;
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 60);
            // 
            // tsbChange
            // 
            this.tsbChange.AutoSize = false;
            this.tsbChange.Image = global::CarSaleMan.Properties.Resources.btnRepair;
            this.tsbChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChange.Name = "tsbChange";
            this.tsbChange.Size = new System.Drawing.Size(60, 51);
            this.tsbChange.Text = "转库操作";
            this.tsbChange.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbChange.Click += new System.EventHandler(this.tsbChange_Click);
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
            // tblStoreinBindingSource
            // 
            this.tblStoreinBindingSource.DataMember = "tbl_storein";
            this.tblStoreinBindingSource.DataSource = this.cmsDB;
            // 
            // tblStoreinTableAdapter
            // 
            this.tblStoreinTableAdapter.ClearBeforeFill = true;
            // 
            // vwStoreinTableAdapter
            // 
            this.vwStoreinTableAdapter.ClearBeforeFill = true;
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
            // FrmStoreChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(921, 469);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.gridStoreChange);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmStoreChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "库位变化";
            this.Load += new System.EventHandler(this.FrmStoreChange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridStoreChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwStoreinBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreinBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStorechangeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbFindAll;
        private System.Windows.Forms.ToolStripButton tsbFindCondition;
        private System.Windows.Forms.ToolStripButton tsbChange;
        private System.Windows.Forms.ToolStripButton tsbHistory;
        private System.Windows.Forms.ToolStripButton tsbReturn;
        private C1.Win.C1FlexGrid.C1FlexGrid gridStoreChange;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource tblStoreinBindingSource;
        private CmsDBTableAdapters.tbl_storeinTableAdapter tblStoreinTableAdapter;
        private System.Windows.Forms.BindingSource vwStoreinBindingSource;
        private CmsDBTableAdapters.vw_storeinTableAdapter vwStoreinTableAdapter;
        private System.Windows.Forms.BindingSource tblStorechangeBindingSource;
        private CmsDBTableAdapters.tbl_storechangeTableAdapter tblStorechangeTableAdapter;
    }
}