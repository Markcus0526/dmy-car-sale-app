namespace CarSaleMan
{
    partial class FrmStatisCarSaleQuarter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStatisCarSaleQuarter));
            this.gridQuarter = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbExportExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbReturn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbYearText = new System.Windows.Forms.ToolStripLabel();
            this.tsbYear = new System.Windows.Forms.ToolStripComboBox();
            this.tsbQuarterText = new System.Windows.Forms.ToolStripLabel();
            this.tsbQuarter = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTitleText = new System.Windows.Forms.ToolStripLabel();
            this.tblBasedataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.tblBasedataTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_basedataTableAdapter();
            this.tblQuarterstatsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblQuarterstatsTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_quarterstatsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gridQuarter)).BeginInit();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblBasedataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblQuarterstatsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridQuarter
            // 
            this.gridQuarter.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.gridQuarter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridQuarter.BackColor = System.Drawing.Color.White;
            this.gridQuarter.ColumnInfo = resources.GetString("gridQuarter.ColumnInfo");
            this.gridQuarter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridQuarter.Location = new System.Drawing.Point(1, 64);
            this.gridQuarter.Name = "gridQuarter";
            this.gridQuarter.Rows.Count = 9;
            this.gridQuarter.Rows.DefaultSize = 21;
            this.gridQuarter.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridQuarter.Size = new System.Drawing.Size(918, 393);
            this.gridQuarter.StyleInfo = resources.GetString("gridQuarter.StyleInfo");
            this.gridQuarter.TabIndex = 11;
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
            this.toolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbExportExcel,
            this.tsbSave,
            this.tsbReturn,
            this.toolStripSeparator2,
            this.tsbYearText,
            this.tsbYear,
            this.tsbQuarterText,
            this.tsbQuarter,
            this.toolStripSeparator1,
            this.tsbTitleText});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.Size = new System.Drawing.Size(921, 60);
            this.toolStrip.TabIndex = 10;
            this.toolStrip.Text = "ToolBar";
            // 
            // tsbExportExcel
            // 
            this.tsbExportExcel.AutoSize = false;
            this.tsbExportExcel.Image = global::CarSaleMan.Properties.Resources.btnExcel;
            this.tsbExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportExcel.Name = "tsbExportExcel";
            this.tsbExportExcel.Size = new System.Drawing.Size(60, 51);
            this.tsbExportExcel.Text = "导入EXCEL";
            this.tsbExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbExportExcel.Click += new System.EventHandler(this.tsbExportExcel_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.AutoSize = false;
            this.tsbSave.Image = global::CarSaleMan.Properties.Resources.btnSave;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(60, 51);
            this.tsbSave.Text = "保存";
            this.tsbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 60);
            // 
            // tsbYearText
            // 
            this.tsbYearText.Name = "tsbYearText";
            this.tsbYearText.Size = new System.Drawing.Size(31, 57);
            this.tsbYearText.Text = "年：";
            // 
            // tsbYear
            // 
            this.tsbYear.AutoSize = false;
            this.tsbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsbYear.DropDownWidth = 60;
            this.tsbYear.Name = "tsbYear";
            this.tsbYear.Size = new System.Drawing.Size(60, 22);
            // 
            // tsbQuarterText
            // 
            this.tsbQuarterText.Name = "tsbQuarterText";
            this.tsbQuarterText.Size = new System.Drawing.Size(59, 57);
            this.tsbQuarterText.Text = "    季度：";
            // 
            // tsbQuarter
            // 
            this.tsbQuarter.AutoSize = false;
            this.tsbQuarter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsbQuarter.Items.AddRange(new object[] {
            "第一季度",
            "第二季度",
            "第三季度",
            "第四季度"});
            this.tsbQuarter.Name = "tsbQuarter";
            this.tsbQuarter.Size = new System.Drawing.Size(80, 22);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 60);
            // 
            // tsbTitleText
            // 
            this.tsbTitleText.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbTitleText.Name = "tsbTitleText";
            this.tsbTitleText.Size = new System.Drawing.Size(0, 57);
            // 
            // tblBasedataBindingSource
            // 
            this.tblBasedataBindingSource.DataMember = "tbl_basedata";
            this.tblBasedataBindingSource.DataSource = this.cmsDB;
            this.tblBasedataBindingSource.Filter = "name = \'车系列\'";
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
            // tblQuarterstatsBindingSource
            // 
            this.tblQuarterstatsBindingSource.DataMember = "tbl_quarterstats";
            this.tblQuarterstatsBindingSource.DataSource = this.cmsDB;
            // 
            // tblQuarterstatsTableAdapter
            // 
            this.tblQuarterstatsTableAdapter.ClearBeforeFill = true;
            // 
            // FrmStatisCarSaleQuarter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(921, 469);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.gridQuarter);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmStatisCarSaleQuarter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "销售季度任务统计";
            this.Load += new System.EventHandler(this.FrmStatisCarSaleQuarter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridQuarter)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblBasedataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblQuarterstatsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbExportExcel;
        private System.Windows.Forms.ToolStripButton tsbReturn;
        private C1.Win.C1FlexGrid.C1FlexGrid gridQuarter;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripLabel tsbYearText;
        private System.Windows.Forms.ToolStripComboBox tsbYear;
        private System.Windows.Forms.ToolStripLabel tsbQuarterText;
        private System.Windows.Forms.ToolStripComboBox tsbQuarter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tsbTitleText;
        private System.Windows.Forms.BindingSource tblBasedataBindingSource;
        private CmsDB cmsDB;
        private CmsDBTableAdapters.tbl_basedataTableAdapter tblBasedataTableAdapter;
        private System.Windows.Forms.BindingSource tblQuarterstatsBindingSource;
        private CmsDBTableAdapters.tbl_quarterstatsTableAdapter tblQuarterstatsTableAdapter;
    }
}