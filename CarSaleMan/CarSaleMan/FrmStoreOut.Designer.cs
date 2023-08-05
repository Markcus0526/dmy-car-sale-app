namespace CarSaleMan
{
    partial class FrmStoreOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStoreOut));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridStoreIn = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.vwStoreinBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbFindAll = new System.Windows.Forms.ToolStripButton();
            this.tsbFindCondition = new System.Windows.Forms.ToolStripButton();
            this.tsbHistory = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCheck = new System.Windows.Forms.ToolStripButton();
            this.tsbSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbReturn = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbFindAll2 = new System.Windows.Forms.ToolStripButton();
            this.tsbFindCondition2 = new System.Windows.Forms.ToolStripButton();
            this.tsbHistory2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAdd2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbChange2 = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete2 = new System.Windows.Forms.ToolStripButton();
            this.gridStoreOut = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.vwStoreoutBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vwStoreinTableAdapter = new CarSaleMan.CmsDBTableAdapters.vw_storeinTableAdapter();
            this.tblStoreinBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblStoreinTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storeinTableAdapter();
            this.tblStoreoutBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblStoreoutTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storeoutTableAdapter();
            this.vwStoreoutTableAdapter = new CarSaleMan.CmsDBTableAdapters.vw_storeoutTableAdapter();
            this.tblStorechangeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblStorechangeTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_storechangeTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStoreIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwStoreinBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStoreOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwStoreoutBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreinBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreoutBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStorechangeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridStoreIn);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.gridStoreOut);
            this.splitContainer1.Size = new System.Drawing.Size(856, 471);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 12;
            // 
            // gridStoreIn
            // 
            this.gridStoreIn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridStoreIn.BackColor = System.Drawing.Color.White;
            this.gridStoreIn.ColumnInfo = resources.GetString("gridStoreIn.ColumnInfo");
            this.gridStoreIn.DataSource = this.vwStoreinBindingSource;
            this.gridStoreIn.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.gridStoreIn.Location = new System.Drawing.Point(3, 63);
            this.gridStoreIn.Name = "gridStoreIn";
            this.gridStoreIn.Rows.Count = 1;
            this.gridStoreIn.Rows.DefaultSize = 19;
            this.gridStoreIn.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridStoreIn.Size = new System.Drawing.Size(848, 167);
            this.gridStoreIn.StyleInfo = resources.GetString("gridStoreIn.StyleInfo");
            this.gridStoreIn.TabIndex = 12;
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
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.BackgroundImage = global::CarSaleMan.Properties.Resources.bkToolBar;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFindAll,
            this.tsbFindCondition,
            this.tsbHistory,
            this.toolStripSeparator2,
            this.tsbCheck,
            this.tsbSelect,
            this.toolStripSeparator3,
            this.tsbReturn});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(854, 60);
            this.toolStrip.TabIndex = 11;
            this.toolStrip.Text = "ToolBar";
            // 
            // tsbFindAll
            // 
            this.tsbFindAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbFindAll.Image = global::CarSaleMan.Properties.Resources.btnAllSelect;
            this.tsbFindAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindAll.Name = "tsbFindAll";
            this.tsbFindAll.Size = new System.Drawing.Size(59, 57);
            this.tsbFindAll.Text = "浏览全部";
            this.tsbFindAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbFindAll.Click += new System.EventHandler(this.tsbFindAll_Click);
            // 
            // tsbFindCondition
            // 
            this.tsbFindCondition.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbFindCondition.Image = global::CarSaleMan.Properties.Resources.btnFind;
            this.tsbFindCondition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindCondition.Name = "tsbFindCondition";
            this.tsbFindCondition.Size = new System.Drawing.Size(59, 57);
            this.tsbFindCondition.Text = "条件查询";
            this.tsbFindCondition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbFindCondition.Click += new System.EventHandler(this.tsbFindCondition_Click);
            // 
            // tsbHistory
            // 
            this.tsbHistory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbHistory.Image = global::CarSaleMan.Properties.Resources.btnHistory;
            this.tsbHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHistory.Name = "tsbHistory";
            this.tsbHistory.Size = new System.Drawing.Size(59, 57);
            this.tsbHistory.Text = "单车流转";
            this.tsbHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbHistory.Click += new System.EventHandler(this.tsbHistory_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 60);
            // 
            // tsbCheck
            // 
            this.tsbCheck.BackColor = System.Drawing.Color.Transparent;
            this.tsbCheck.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbCheck.Image = global::CarSaleMan.Properties.Resources.btnCheck;
            this.tsbCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheck.Name = "tsbCheck";
            this.tsbCheck.Size = new System.Drawing.Size(59, 57);
            this.tsbCheck.Text = "选择查看";
            this.tsbCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbCheck.Click += new System.EventHandler(this.tsbCheck_Click);
            // 
            // tsbSelect
            // 
            this.tsbSelect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbSelect.Image = global::CarSaleMan.Properties.Resources.btnSelect;
            this.tsbSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelect.Name = "tsbSelect";
            this.tsbSelect.Size = new System.Drawing.Size(59, 57);
            this.tsbSelect.Text = "全部选择";
            this.tsbSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSelect.Visible = false;
            this.tsbSelect.Click += new System.EventHandler(this.tsbSelect_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 60);
            // 
            // tsbReturn
            // 
            this.tsbReturn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbReturn.Image = global::CarSaleMan.Properties.Resources.btnBack;
            this.tsbReturn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReturn.Name = "tsbReturn";
            this.tsbReturn.Size = new System.Drawing.Size(59, 57);
            this.tsbReturn.Text = "近回系统";
            this.tsbReturn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbReturn.Click += new System.EventHandler(this.tsbReturn_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.toolStrip1.BackgroundImage = global::CarSaleMan.Properties.Resources.bkToolBar;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFindAll2,
            this.tsbFindCondition2,
            this.tsbHistory2,
            this.toolStripSeparator1,
            this.tsbAdd2,
            this.toolStripSeparator4,
            this.tsbChange2,
            this.tsbDelete2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(854, 60);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "ToolBar";
            // 
            // tsbFindAll2
            // 
            this.tsbFindAll2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbFindAll2.Image = global::CarSaleMan.Properties.Resources.btnAllSelect;
            this.tsbFindAll2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindAll2.Name = "tsbFindAll2";
            this.tsbFindAll2.Size = new System.Drawing.Size(59, 57);
            this.tsbFindAll2.Text = "浏览全部";
            this.tsbFindAll2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbFindAll2.Click += new System.EventHandler(this.tsbFindAll2_Click);
            // 
            // tsbFindCondition2
            // 
            this.tsbFindCondition2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbFindCondition2.Image = global::CarSaleMan.Properties.Resources.btnFind;
            this.tsbFindCondition2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindCondition2.Name = "tsbFindCondition2";
            this.tsbFindCondition2.Size = new System.Drawing.Size(59, 57);
            this.tsbFindCondition2.Text = "条件查询";
            this.tsbFindCondition2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbFindCondition2.Click += new System.EventHandler(this.tsbFindCondition2_Click);
            // 
            // tsbHistory2
            // 
            this.tsbHistory2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbHistory2.Image = global::CarSaleMan.Properties.Resources.btnHistory;
            this.tsbHistory2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHistory2.Name = "tsbHistory2";
            this.tsbHistory2.Size = new System.Drawing.Size(59, 57);
            this.tsbHistory2.Text = "单车流转";
            this.tsbHistory2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbHistory2.Click += new System.EventHandler(this.tsbHistory2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 60);
            // 
            // tsbAdd2
            // 
            this.tsbAdd2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbAdd2.Image = global::CarSaleMan.Properties.Resources.btnOut;
            this.tsbAdd2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd2.Name = "tsbAdd2";
            this.tsbAdd2.Size = new System.Drawing.Size(59, 57);
            this.tsbAdd2.Text = "单车出库";
            this.tsbAdd2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbAdd2.Click += new System.EventHandler(this.tsbAdd2_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 60);
            // 
            // tsbChange2
            // 
            this.tsbChange2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbChange2.Image = global::CarSaleMan.Properties.Resources.btnRepair;
            this.tsbChange2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChange2.Name = "tsbChange2";
            this.tsbChange2.Size = new System.Drawing.Size(59, 57);
            this.tsbChange2.Text = "出库修改";
            this.tsbChange2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbChange2.ToolTipText = "入库修改";
            this.tsbChange2.Click += new System.EventHandler(this.tsbChange2_Click);
            // 
            // tsbDelete2
            // 
            this.tsbDelete2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbDelete2.Image = global::CarSaleMan.Properties.Resources.btnCancel;
            this.tsbDelete2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete2.Name = "tsbDelete2";
            this.tsbDelete2.Size = new System.Drawing.Size(59, 57);
            this.tsbDelete2.Text = "入库取消";
            this.tsbDelete2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbDelete2.ToolTipText = "入库取消";
            this.tsbDelete2.Click += new System.EventHandler(this.tsbDelete2_Click);
            // 
            // gridStoreOut
            // 
            this.gridStoreOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridStoreOut.BackColor = System.Drawing.Color.White;
            this.gridStoreOut.ColumnInfo = resources.GetString("gridStoreOut.ColumnInfo");
            this.gridStoreOut.DataSource = this.vwStoreoutBindingSource;
            this.gridStoreOut.Location = new System.Drawing.Point(3, 63);
            this.gridStoreOut.Name = "gridStoreOut";
            this.gridStoreOut.Rows.Count = 1;
            this.gridStoreOut.Rows.DefaultSize = 19;
            this.gridStoreOut.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridStoreOut.Size = new System.Drawing.Size(848, 164);
            this.gridStoreOut.StyleInfo = resources.GetString("gridStoreOut.StyleInfo");
            this.gridStoreOut.TabIndex = 13;
            // 
            // vwStoreoutBindingSource
            // 
            this.vwStoreoutBindingSource.DataMember = "vw_storeout";
            this.vwStoreoutBindingSource.DataSource = this.cmsDB;
            // 
            // vwStoreinTableAdapter
            // 
            this.vwStoreinTableAdapter.ClearBeforeFill = true;
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
            // tblStoreoutBindingSource
            // 
            this.tblStoreoutBindingSource.DataMember = "tbl_storeout";
            this.tblStoreoutBindingSource.DataSource = this.cmsDB;
            // 
            // tblStoreoutTableAdapter
            // 
            this.tblStoreoutTableAdapter.ClearBeforeFill = true;
            // 
            // vwStoreoutTableAdapter
            // 
            this.vwStoreoutTableAdapter.ClearBeforeFill = true;
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
            // FrmStoreOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(856, 471);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmStoreOut";
            this.Text = "出库处理";
            this.Load += new System.EventHandler(this.FrmStoreOut_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridStoreIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwStoreinBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStoreOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwStoreoutBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreinBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStoreoutBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblStorechangeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbFindAll;
        private System.Windows.Forms.ToolStripButton tsbFindCondition;
        private System.Windows.Forms.ToolStripButton tsbHistory;
        private System.Windows.Forms.ToolStripButton tsbReturn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private C1.Win.C1FlexGrid.C1FlexGrid gridStoreIn;
        private C1.Win.C1FlexGrid.C1FlexGrid gridStoreOut;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbFindAll2;
        private System.Windows.Forms.ToolStripButton tsbFindCondition2;
        private System.Windows.Forms.ToolStripButton tsbChange2;
        private System.Windows.Forms.ToolStripButton tsbDelete2;
        private System.Windows.Forms.ToolStripButton tsbHistory2;
        private System.Windows.Forms.ToolStripButton tsbAdd2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbCheck;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource vwStoreinBindingSource;
        private CmsDBTableAdapters.vw_storeinTableAdapter vwStoreinTableAdapter;
        private System.Windows.Forms.BindingSource tblStoreinBindingSource;
        private CmsDBTableAdapters.tbl_storeinTableAdapter tblStoreinTableAdapter;
        private System.Windows.Forms.BindingSource tblStoreoutBindingSource;
        private CmsDBTableAdapters.tbl_storeoutTableAdapter tblStoreoutTableAdapter;
        private System.Windows.Forms.BindingSource vwStoreoutBindingSource;
        private CmsDBTableAdapters.vw_storeoutTableAdapter vwStoreoutTableAdapter;
        private System.Windows.Forms.BindingSource tblStorechangeBindingSource;
        private CmsDBTableAdapters.tbl_storechangeTableAdapter tblStorechangeTableAdapter;
    }
}