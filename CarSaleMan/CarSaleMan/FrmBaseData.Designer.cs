namespace CarSaleMan
{
    partial class FrmBaseData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaseData));
            this.lstName = new C1.Win.C1List.C1List();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbAddName = new System.Windows.Forms.ToolStripButton();
            this.tsbDeleteName = new System.Windows.Forms.ToolStripButton();
            this.tsbChangeName = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSaveValue = new System.Windows.Forms.ToolStripButton();
            this.tsbRejectValue = new System.Windows.Forms.ToolStripButton();
            this.tsbImportValue = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbReturn = new System.Windows.Forms.ToolStripButton();
            this.gridValue = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tblBasedataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.tblBasedataTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_basedataTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.lstName)).BeginInit();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblBasedataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            this.SuspendLayout();
            // 
            // lstName
            // 
            this.lstName.AddItemSeparator = ';';
            this.lstName.AlternatingRows = true;
            this.lstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstName.Caption = "基础信息名称";
            this.lstName.CaptionHeight = 20;
            this.lstName.ColumnCaptionHeight = 20;
            this.lstName.ColumnFooterHeight = 20;
            this.lstName.ColumnHeaders = false;
            this.lstName.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            this.lstName.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
            this.lstName.ExtendRightColumn = true;
            this.lstName.Images.Add(((System.Drawing.Image)(resources.GetObject("lstName.Images"))));
            this.lstName.ItemHeight = 25;
            this.lstName.Location = new System.Drawing.Point(6, 63);
            this.lstName.MatchEntryTimeout = ((long)(2000));
            this.lstName.Name = "lstName";
            this.lstName.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.lstName.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.lstName.Size = new System.Drawing.Size(195, 415);
            this.lstName.TabIndex = 0;
            this.lstName.Text = "基础信息名称";
            this.lstName.PropBag = resources.GetString("lstName.PropBag");
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.BackgroundImage = global::CarSaleMan.Properties.Resources.bkToolBar;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddName,
            this.tsbDeleteName,
            this.tsbChangeName,
            this.toolStripSeparator1,
            this.tsbSaveValue,
            this.tsbRejectValue,
            this.tsbImportValue,
            this.toolStripSeparator2,
            this.tsbReturn});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.Size = new System.Drawing.Size(587, 60);
            this.toolStrip.TabIndex = 11;
            this.toolStrip.Text = "ToolBar";
            // 
            // tsbAddName
            // 
            this.tsbAddName.AutoSize = false;
            this.tsbAddName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbAddName.Image = global::CarSaleMan.Properties.Resources.btnPlus2;
            this.tsbAddName.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddName.Name = "tsbAddName";
            this.tsbAddName.Size = new System.Drawing.Size(60, 51);
            this.tsbAddName.Text = "添加";
            this.tsbAddName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbAddName.Click += new System.EventHandler(this.tsbAddName_Click);
            // 
            // tsbDeleteName
            // 
            this.tsbDeleteName.AutoSize = false;
            this.tsbDeleteName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbDeleteName.Image = global::CarSaleMan.Properties.Resources.btnMinus2;
            this.tsbDeleteName.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteName.Name = "tsbDeleteName";
            this.tsbDeleteName.Size = new System.Drawing.Size(60, 51);
            this.tsbDeleteName.Text = "删除";
            this.tsbDeleteName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbDeleteName.Click += new System.EventHandler(this.tsbDeleteName_Click);
            // 
            // tsbChangeName
            // 
            this.tsbChangeName.AutoSize = false;
            this.tsbChangeName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbChangeName.Image = global::CarSaleMan.Properties.Resources.btnRepair;
            this.tsbChangeName.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChangeName.Name = "tsbChangeName";
            this.tsbChangeName.Size = new System.Drawing.Size(60, 51);
            this.tsbChangeName.Text = "修改";
            this.tsbChangeName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbChangeName.Click += new System.EventHandler(this.tsbChangeName_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 60);
            // 
            // tsbSaveValue
            // 
            this.tsbSaveValue.AutoSize = false;
            this.tsbSaveValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbSaveValue.Image = global::CarSaleMan.Properties.Resources.btnSave;
            this.tsbSaveValue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveValue.Name = "tsbSaveValue";
            this.tsbSaveValue.Size = new System.Drawing.Size(60, 51);
            this.tsbSaveValue.Text = "保存";
            this.tsbSaveValue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSaveValue.ToolTipText = "保存";
            this.tsbSaveValue.Click += new System.EventHandler(this.tsbSaveValue_Click);
            // 
            // tsbRejectValue
            // 
            this.tsbRejectValue.AutoSize = false;
            this.tsbRejectValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbRejectValue.Image = global::CarSaleMan.Properties.Resources.btnStop;
            this.tsbRejectValue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRejectValue.Name = "tsbRejectValue";
            this.tsbRejectValue.Size = new System.Drawing.Size(60, 51);
            this.tsbRejectValue.Text = "驳回";
            this.tsbRejectValue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRejectValue.ToolTipText = "驳回";
            this.tsbRejectValue.Click += new System.EventHandler(this.tsbRejectValue_Click);
            // 
            // tsbImportValue
            // 
            this.tsbImportValue.AutoSize = false;
            this.tsbImportValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbImportValue.Image = global::CarSaleMan.Properties.Resources.btnImport;
            this.tsbImportValue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImportValue.Name = "tsbImportValue";
            this.tsbImportValue.Size = new System.Drawing.Size(60, 51);
            this.tsbImportValue.Text = "导入";
            this.tsbImportValue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbImportValue.ToolTipText = "导入";
            this.tsbImportValue.Click += new System.EventHandler(this.tsbImportValue_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 60);
            // 
            // tsbReturn
            // 
            this.tsbReturn.AutoSize = false;
            this.tsbReturn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbReturn.Image = global::CarSaleMan.Properties.Resources.btnBack;
            this.tsbReturn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReturn.Name = "tsbReturn";
            this.tsbReturn.Size = new System.Drawing.Size(60, 51);
            this.tsbReturn.Text = "近回系统";
            this.tsbReturn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbReturn.Click += new System.EventHandler(this.tsbReturn_Click);
            // 
            // gridValue
            // 
            this.gridValue.AllowAddNew = true;
            this.gridValue.AllowDelete = true;
            this.gridValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridValue.BackColor = System.Drawing.Color.White;
            this.gridValue.ColumnInfo = resources.GetString("gridValue.ColumnInfo");
            this.gridValue.DataSource = this.tblBasedataBindingSource;
            this.gridValue.Location = new System.Drawing.Point(202, 63);
            this.gridValue.Name = "gridValue";
            this.gridValue.Rows.Count = 1;
            this.gridValue.Rows.DefaultSize = 19;
            this.gridValue.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridValue.ShowCursor = true;
            this.gridValue.Size = new System.Drawing.Size(378, 415);
            this.gridValue.StyleInfo = resources.GetString("gridValue.StyleInfo");
            this.gridValue.TabIndex = 13;
            // 
            // tblBasedataBindingSource
            // 
            this.tblBasedataBindingSource.DataMember = "tbl_basedata";
            this.tblBasedataBindingSource.DataSource = this.cmsDB;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblBasedataTableAdapter
            // 
            this.tblBasedataTableAdapter.ClearBeforeFill = true;
            // 
            // FrmBaseData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(587, 482);
            this.Controls.Add(this.gridValue);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.lstName);
            this.Name = "FrmBaseData";
            this.Text = "权限设置";
            this.Load += new System.EventHandler(this.FrmBaseData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lstName)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblBasedataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1List.C1List lstName;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbAddName;
        private System.Windows.Forms.ToolStripButton tsbChangeName;
        private System.Windows.Forms.ToolStripButton tsbDeleteName;
        private System.Windows.Forms.ToolStripButton tsbSaveValue;
        private System.Windows.Forms.ToolStripButton tsbImportValue;
        private System.Windows.Forms.ToolStripButton tsbRejectValue;
        private System.Windows.Forms.ToolStripButton tsbReturn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private C1.Win.C1FlexGrid.C1FlexGrid gridValue;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource tblBasedataBindingSource;
        private CarSaleMan.CmsDBTableAdapters.tbl_basedataTableAdapter tblBasedataTableAdapter;

    }
}