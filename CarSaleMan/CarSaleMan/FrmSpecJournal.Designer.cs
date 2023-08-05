namespace CarSaleMan
{
    partial class FrmSpecJournal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSpecJournal));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbSaveValue = new System.Windows.Forms.ToolStripButton();
            this.tsbRejectValue = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbReturn = new System.Windows.Forms.ToolStripButton();
            this.gridValue = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tblLogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.tblLogTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_logTableAdapter();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblLogBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.BackgroundImage = global::CarSaleMan.Properties.Resources.bkToolBar;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSaveValue,
            this.tsbRejectValue,
            this.toolStripSeparator2,
            this.tsbReturn});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.Size = new System.Drawing.Size(587, 60);
            this.toolStrip.TabIndex = 11;
            this.toolStrip.Text = "ToolBar";
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
            this.gridValue.DataSource = this.tblLogBindingSource;
            this.gridValue.ExtendLastCol = true;
            this.gridValue.Location = new System.Drawing.Point(6, 63);
            this.gridValue.Name = "gridValue";
            this.gridValue.Rows.Count = 1;
            this.gridValue.Rows.DefaultSize = 19;
            this.gridValue.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridValue.ShowCursor = true;
            this.gridValue.Size = new System.Drawing.Size(575, 415);
            this.gridValue.StyleInfo = resources.GetString("gridValue.StyleInfo");
            this.gridValue.TabIndex = 13;
            // 
            // tblLogBindingSource
            // 
            this.tblLogBindingSource.DataMember = "tbl_log";
            this.tblLogBindingSource.DataSource = this.cmsDB;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblLogTableAdapter
            // 
            this.tblLogTableAdapter.ClearBeforeFill = true;
            // 
            // FrmSpecJournal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(587, 482);
            this.Controls.Add(this.gridValue);
            this.Controls.Add(this.toolStrip);
            this.Name = "FrmSpecJournal";
            this.Text = "特殊日记";
            this.Load += new System.EventHandler(this.FrmSpecJournal_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblLogBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbSaveValue;
        private System.Windows.Forms.ToolStripButton tsbRejectValue;
        private System.Windows.Forms.ToolStripButton tsbReturn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private C1.Win.C1FlexGrid.C1FlexGrid gridValue;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource tblLogBindingSource;
        private CmsDBTableAdapters.tbl_logTableAdapter tblLogTableAdapter;

    }
}