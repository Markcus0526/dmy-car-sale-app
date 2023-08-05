namespace CarSaleMan
{
    partial class FrmChartSaleRegion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChartSaleRegion));
            this.chartResult = new C1.Win.C1Chart.C1Chart();
            this.storChartSaleregionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rdPie = new System.Windows.Forms.RadioButton();
            this.rdLine = new System.Windows.Forms.RadioButton();
            this.rdBar = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.pdfDoc = new C1.C1Pdf.C1PdfDocument();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.storChartSaleregionTableAdapter = new CarSaleMan.CmsDBTableAdapters.stor_chart_saleregionTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.chartResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.storChartSaleregionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartResult
            // 
            this.chartResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chartResult.DataSource = this.storChartSaleregionBindingSource;
            this.chartResult.Location = new System.Drawing.Point(0, 100);
            this.chartResult.Name = "chartResult";
            this.chartResult.PropBag = resources.GetString("chartResult.PropBag");
            this.chartResult.Size = new System.Drawing.Size(880, 337);
            this.chartResult.TabIndex = 0;
            // 
            // storChartSaleregionBindingSource
            // 
            this.storChartSaleregionBindingSource.DataMember = "stor_chart_saleregion";
            this.storChartSaleregionBindingSource.DataSource = this.cmsDB;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dtStartDate
            // 
            this.dtStartDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtStartDate.Location = new System.Drawing.Point(453, 26);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(108, 20);
            this.dtStartDate.TabIndex = 2;
            // 
            // dtEndDate
            // 
            this.dtEndDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtEndDate.Location = new System.Drawing.Point(453, 53);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(108, 20);
            this.dtEndDate.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(386, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "开始时间 :";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(386, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "结束时间 :";
            // 
            // rdPie
            // 
            this.rdPie.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rdPie.AutoSize = true;
            this.rdPie.Location = new System.Drawing.Point(24, 29);
            this.rdPie.Name = "rdPie";
            this.rdPie.Size = new System.Drawing.Size(49, 17);
            this.rdPie.TabIndex = 6;
            this.rdPie.TabStop = true;
            this.rdPie.Text = "饼图";
            this.rdPie.UseVisualStyleBackColor = true;
            this.rdPie.CheckedChanged += new System.EventHandler(this.rdPie_CheckedChanged);
            // 
            // rdLine
            // 
            this.rdLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rdLine.AutoSize = true;
            this.rdLine.Checked = true;
            this.rdLine.Location = new System.Drawing.Point(131, 29);
            this.rdLine.Name = "rdLine";
            this.rdLine.Size = new System.Drawing.Size(61, 17);
            this.rdLine.TabIndex = 7;
            this.rdLine.TabStop = true;
            this.rdLine.Text = "折线图";
            this.rdLine.UseVisualStyleBackColor = true;
            this.rdLine.CheckedChanged += new System.EventHandler(this.rdLine_CheckedChanged);
            // 
            // rdBar
            // 
            this.rdBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rdBar.AutoSize = true;
            this.rdBar.Location = new System.Drawing.Point(226, 29);
            this.rdBar.Name = "rdBar";
            this.rdBar.Size = new System.Drawing.Size(61, 17);
            this.rdBar.TabIndex = 8;
            this.rdBar.TabStop = true;
            this.rdBar.Text = "柱形图";
            this.rdBar.UseVisualStyleBackColor = true;
            this.rdBar.CheckedChanged += new System.EventHandler(this.rdBar_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.rdLine);
            this.groupBox1.Controls.Add(this.rdBar);
            this.groupBox1.Controls.Add(this.rdPie);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(32, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 63);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图表类型";
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnExportPDF.BackColor = System.Drawing.Color.Transparent;
            this.btnExportPDF.Image = global::CarSaleMan.Properties.Resources.btnPdf;
            this.btnExportPDF.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExportPDF.Location = new System.Drawing.Point(778, 15);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(72, 66);
            this.btnExportPDF.TabIndex = 31;
            this.btnExportPDF.Text = "导出 PDF";
            this.btnExportPDF.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExportPDF.UseVisualStyleBackColor = false;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // pdfDoc
            // 
            this.pdfDoc.RotateAngle = 0F;
            this.pdfDoc.UseFileCaching = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRefresh.Image = global::CarSaleMan.Properties.Resources.btnRefresh;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(577, 25);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(55, 48);
            this.btnRefresh.TabIndex = 32;
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // storChartSaleregionTableAdapter
            // 
            this.storChartSaleregionTableAdapter.ClearBeforeFill = true;
            // 
            // FrmChartSaleRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(880, 437);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnExportPDF);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtEndDate);
            this.Controls.Add(this.dtStartDate);
            this.Controls.Add(this.chartResult);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmChartSaleRegion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "车辆的销售区域统计";
            this.Load += new System.EventHandler(this.FrmChartSaleRegion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.storChartSaleregionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Chart.C1Chart chartResult;
		private System.Windows.Forms.DateTimePicker dtStartDate;
		private System.Windows.Forms.DateTimePicker dtEndDate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton rdPie;
		private System.Windows.Forms.RadioButton rdLine;
		private System.Windows.Forms.RadioButton rdBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExportPDF;
        private C1.C1Pdf.C1PdfDocument pdfDoc;
        private System.Windows.Forms.BindingSource storChartSaleregionBindingSource;
        private CmsDB cmsDB;
        private System.Windows.Forms.Button btnRefresh;
        private CmsDBTableAdapters.stor_chart_saleregionTableAdapter storChartSaleregionTableAdapter;
    }
}