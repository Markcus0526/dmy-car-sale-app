namespace CarSaleMan
{
    partial class FrmFinanceParam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFinanceParam));
            this.btnExtend5Color = new System.Windows.Forms.Button();
            this.btnNoInterest5Color = new System.Windows.Forms.Button();
            this.btnNoInterest10Color = new System.Windows.Forms.Button();
            this.btnExtend10Color = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNoInterestRate = new System.Windows.Forms.TextBox();
            this.numNoInterestDates = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numExtendDates = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtInterestRate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtExtendRate = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmsDB = new CarSaleMan.CmsDB();
            this.tblEnvBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblEnvTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_envTableAdapter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNoInterestDates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExtendDates)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblEnvBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExtend5Color
            // 
            this.btnExtend5Color.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnExtend5Color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtend5Color.Location = new System.Drawing.Point(329, 53);
            this.btnExtend5Color.Name = "btnExtend5Color";
            this.btnExtend5Color.Size = new System.Drawing.Size(67, 23);
            this.btnExtend5Color.TabIndex = 6;
            this.btnExtend5Color.Text = "5天";
            this.btnExtend5Color.UseVisualStyleBackColor = false;
            this.btnExtend5Color.Click += new System.EventHandler(this.btnExtend5Color_Click);
            // 
            // btnNoInterest5Color
            // 
            this.btnNoInterest5Color.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnNoInterest5Color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoInterest5Color.Location = new System.Drawing.Point(330, 62);
            this.btnNoInterest5Color.Name = "btnNoInterest5Color";
            this.btnNoInterest5Color.Size = new System.Drawing.Size(67, 23);
            this.btnNoInterest5Color.TabIndex = 6;
            this.btnNoInterest5Color.Text = "5天";
            this.btnNoInterest5Color.UseVisualStyleBackColor = false;
            this.btnNoInterest5Color.Click += new System.EventHandler(this.btnNoInterest5Color_Click);
            // 
            // btnNoInterest10Color
            // 
            this.btnNoInterest10Color.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnNoInterest10Color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoInterest10Color.Location = new System.Drawing.Point(236, 62);
            this.btnNoInterest10Color.Name = "btnNoInterest10Color";
            this.btnNoInterest10Color.Size = new System.Drawing.Size(67, 23);
            this.btnNoInterest10Color.TabIndex = 6;
            this.btnNoInterest10Color.Text = "10天";
            this.btnNoInterest10Color.UseVisualStyleBackColor = false;
            this.btnNoInterest10Color.Click += new System.EventHandler(this.btnNoInterest10Color_Click);
            // 
            // btnExtend10Color
            // 
            this.btnExtend10Color.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnExtend10Color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtend10Color.Location = new System.Drawing.Point(235, 53);
            this.btnExtend10Color.Name = "btnExtend10Color";
            this.btnExtend10Color.Size = new System.Drawing.Size(67, 23);
            this.btnExtend10Color.TabIndex = 6;
            this.btnExtend10Color.Text = "10天";
            this.btnExtend10Color.UseVisualStyleBackColor = false;
            this.btnExtend10Color.Click += new System.EventHandler(this.btnExtend10Color_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(195, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "利率";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "展期天数";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNoInterest5Color);
            this.groupBox1.Controls.Add(this.btnNoInterest10Color);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtNoInterestRate);
            this.groupBox1.Controls.Add(this.numNoInterestDates);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(21, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 95);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "免息期间";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(175, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "背景颜色";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(302, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "%     ( / 年)";
            // 
            // txtNoInterestRate
            // 
            this.txtNoInterestRate.BackColor = System.Drawing.SystemColors.Menu;
            this.txtNoInterestRate.Enabled = false;
            this.txtNoInterestRate.Location = new System.Drawing.Point(235, 26);
            this.txtNoInterestRate.Name = "txtNoInterestRate";
            this.txtNoInterestRate.Size = new System.Drawing.Size(61, 20);
            this.txtNoInterestRate.TabIndex = 3;
            this.txtNoInterestRate.Text = "0";
            this.txtNoInterestRate.TextChanged += new System.EventHandler(this.txtNoInterestRate_TextChanged);
            // 
            // numNoInterestDates
            // 
            this.numNoInterestDates.Location = new System.Drawing.Point(72, 27);
            this.numNoInterestDates.Name = "numNoInterestDates";
            this.numNoInterestDates.Size = new System.Drawing.Size(83, 20);
            this.numNoInterestDates.TabIndex = 2;
            this.numNoInterestDates.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numNoInterestDates.ValueChanged += new System.EventHandler(this.numNoInterestDates_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "免息天数";
            // 
            // numExtendDates
            // 
            this.numExtendDates.Location = new System.Drawing.Point(71, 25);
            this.numExtendDates.Name = "numExtendDates";
            this.numExtendDates.Size = new System.Drawing.Size(83, 20);
            this.numExtendDates.TabIndex = 4;
            this.numExtendDates.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numExtendDates.ValueChanged += new System.EventHandler(this.numExtendDates_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(174, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "背景颜色";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(301, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "%     ( / 年)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExtend5Color);
            this.groupBox2.Controls.Add(this.btnExtend10Color);
            this.groupBox2.Controls.Add(this.numExtendDates);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtInterestRate);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(22, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 91);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "展期期间";
            // 
            // txtInterestRate
            // 
            this.txtInterestRate.Location = new System.Drawing.Point(234, 24);
            this.txtInterestRate.Name = "txtInterestRate";
            this.txtInterestRate.Size = new System.Drawing.Size(61, 20);
            this.txtInterestRate.TabIndex = 3;
            this.txtInterestRate.Text = "6.87";
            this.txtInterestRate.TextChanged += new System.EventHandler(this.txtInterestRate_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "利率";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(388, 329);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "返回";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(300, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "%     ( / 年)";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(270, 329);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtExtendRate
            // 
            this.txtExtendRate.Location = new System.Drawing.Point(233, 24);
            this.txtExtendRate.Name = "txtExtendRate";
            this.txtExtendRate.Size = new System.Drawing.Size(61, 20);
            this.txtExtendRate.TabIndex = 3;
            this.txtExtendRate.Text = "7.27";
            this.txtExtendRate.TextChanged += new System.EventHandler(this.txtExtendRate_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtExtendRate);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(24, 239);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(438, 76);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "超展期到期";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(193, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "利率";
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // FrmFinanceParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 366);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFinanceParam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "财务方面参数";
            this.Load += new System.EventHandler(this.FrmFinanceParam_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNoInterestDates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExtendDates)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblEnvBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExtend5Color;
        private System.Windows.Forms.Button btnNoInterest5Color;
        private System.Windows.Forms.Button btnNoInterest10Color;
        private System.Windows.Forms.Button btnExtend10Color;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNoInterestRate;
        private System.Windows.Forms.NumericUpDown numNoInterestDates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numExtendDates;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtInterestRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtExtendRate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource tblEnvBindingSource;
        private CmsDBTableAdapters.tbl_envTableAdapter tblEnvTableAdapter;
    }
}