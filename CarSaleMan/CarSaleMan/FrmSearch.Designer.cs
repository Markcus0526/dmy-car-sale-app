namespace CarSaleMan
{
    partial class FrmSearch
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
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.chkKey1 = new System.Windows.Forms.CheckBox();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbKey3 = new System.Windows.Forms.ComboBox();
            this.cbKey2 = new System.Windows.Forms.ComboBox();
            this.cbKey1 = new System.Windows.Forms.ComboBox();
            this.btnErase = new System.Windows.Forms.Button();
            this.chkKey3 = new System.Windows.Forms.CheckBox();
            this.chkKey2 = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(92, 56);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(114, 20);
            this.dtpEnd.TabIndex = 1;
            // 
            // chkKey1
            // 
            this.chkKey1.AutoCheck = false;
            this.chkKey1.AutoSize = true;
            this.chkKey1.Enabled = false;
            this.chkKey1.Location = new System.Drawing.Point(12, 17);
            this.chkKey1.Name = "chkKey1";
            this.chkKey1.Size = new System.Drawing.Size(50, 17);
            this.chkKey1.TabIndex = 0;
            this.chkKey1.Text = "字段";
            this.chkKey1.UseVisualStyleBackColor = true;
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "yyyy-MM-dd";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(92, 13);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(114, 20);
            this.dtpStart.TabIndex = 0;
            // 
            // chkDate
            // 
            this.chkDate.AutoCheck = false;
            this.chkDate.AutoSize = true;
            this.chkDate.Location = new System.Drawing.Point(12, 16);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(74, 17);
            this.chkDate.TabIndex = 0;
            this.chkDate.Text = "日期字段";
            this.chkDate.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbKey3);
            this.panel1.Controls.Add(this.cbKey2);
            this.panel1.Controls.Add(this.cbKey1);
            this.panel1.Controls.Add(this.btnErase);
            this.panel1.Controls.Add(this.chkKey3);
            this.panel1.Controls.Add(this.chkKey2);
            this.panel1.Controls.Add(this.chkKey1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 162);
            this.panel1.TabIndex = 4;
            // 
            // cbKey3
            // 
            this.cbKey3.FormattingEnabled = true;
            this.cbKey3.Location = new System.Drawing.Point(112, 84);
            this.cbKey3.Name = "cbKey3";
            this.cbKey3.Size = new System.Drawing.Size(203, 21);
            this.cbKey3.TabIndex = 2;
            // 
            // cbKey2
            // 
            this.cbKey2.FormattingEnabled = true;
            this.cbKey2.Location = new System.Drawing.Point(112, 49);
            this.cbKey2.Name = "cbKey2";
            this.cbKey2.Size = new System.Drawing.Size(203, 21);
            this.cbKey2.TabIndex = 1;
            // 
            // cbKey1
            // 
            this.cbKey1.FormattingEnabled = true;
            this.cbKey1.Location = new System.Drawing.Point(112, 15);
            this.cbKey1.Name = "cbKey1";
            this.cbKey1.Size = new System.Drawing.Size(203, 21);
            this.cbKey1.TabIndex = 0;
            // 
            // btnErase
            // 
            this.btnErase.Location = new System.Drawing.Point(240, 125);
            this.btnErase.Name = "btnErase";
            this.btnErase.Size = new System.Drawing.Size(75, 26);
            this.btnErase.TabIndex = 3;
            this.btnErase.Text = "清    除";
            this.btnErase.UseVisualStyleBackColor = true;
            this.btnErase.Click += new System.EventHandler(this.btnErase_Click);
            // 
            // chkKey3
            // 
            this.chkKey3.AutoCheck = false;
            this.chkKey3.AutoSize = true;
            this.chkKey3.Enabled = false;
            this.chkKey3.Location = new System.Drawing.Point(12, 86);
            this.chkKey3.Name = "chkKey3";
            this.chkKey3.Size = new System.Drawing.Size(50, 17);
            this.chkKey3.TabIndex = 0;
            this.chkKey3.Text = "字段";
            this.chkKey3.UseVisualStyleBackColor = true;
            // 
            // chkKey2
            // 
            this.chkKey2.AutoCheck = false;
            this.chkKey2.AutoSize = true;
            this.chkKey2.Enabled = false;
            this.chkKey2.Location = new System.Drawing.Point(12, 51);
            this.chkKey2.Name = "chkKey2";
            this.chkKey2.Size = new System.Drawing.Size(50, 17);
            this.chkKey2.TabIndex = 0;
            this.chkKey2.Text = "字段";
            this.chkKey2.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(253, 259);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 26);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "搜    素";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dtpEnd);
            this.panel2.Controls.Add(this.dtpStart);
            this.panel2.Controls.Add(this.chkDate);
            this.panel2.Location = new System.Drawing.Point(12, 190);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(222, 95);
            this.panel2.TabIndex = 5;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCount.Location = new System.Drawing.Point(75, 297);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 16);
            this.lblCount.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 297);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "总数：";
            // 
            // FrmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 322);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSearch";
            this.ShowInTaskbar = false;
            this.Text = "条件查询";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnErase;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker dtpEnd;
        public System.Windows.Forms.CheckBox chkKey1;
        public System.Windows.Forms.DateTimePicker dtpStart;
        public System.Windows.Forms.CheckBox chkDate;
        public System.Windows.Forms.ComboBox cbKey3;
        public System.Windows.Forms.ComboBox cbKey2;
        public System.Windows.Forms.ComboBox cbKey1;
        public System.Windows.Forms.CheckBox chkKey3;
        public System.Windows.Forms.CheckBox chkKey2;
        public System.Windows.Forms.Label lblCount;
    }
}