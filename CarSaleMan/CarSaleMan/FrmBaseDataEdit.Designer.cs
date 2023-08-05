namespace CarSaleMan
{
    partial class FrmBaseDataEdit
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
            this.group1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdTwo = new System.Windows.Forms.RadioButton();
            this.rdOne = new System.Windows.Forms.RadioButton();
            this.tbName1 = new System.Windows.Forms.TextBox();
            this.group2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName3 = new System.Windows.Forms.TextBox();
            this.tbName2 = new System.Windows.Forms.TextBox();
            this.group3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbName4 = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.group3.SuspendLayout();
            this.SuspendLayout();
            // 
            // group1
            // 
            this.group1.Controls.Add(this.label1);
            this.group1.Controls.Add(this.rdTwo);
            this.group1.Controls.Add(this.rdOne);
            this.group1.Controls.Add(this.tbName1);
            this.group1.Location = new System.Drawing.Point(12, 12);
            this.group1.Name = "group1";
            this.group1.Size = new System.Drawing.Size(291, 96);
            this.group1.TabIndex = 0;
            this.group1.TabStop = false;
            this.group1.Text = "添加";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "基础信息名称：";
            // 
            // rdTwo
            // 
            this.rdTwo.AutoSize = true;
            this.rdTwo.Location = new System.Drawing.Point(202, 62);
            this.rdTwo.Name = "rdTwo";
            this.rdTwo.Size = new System.Drawing.Size(67, 17);
            this.rdTwo.TabIndex = 2;
            this.rdTwo.TabStop = true;
            this.rdTwo.Text = "2值数据";
            this.rdTwo.UseVisualStyleBackColor = true;
            // 
            // rdOne
            // 
            this.rdOne.AutoSize = true;
            this.rdOne.Checked = true;
            this.rdOne.Location = new System.Drawing.Point(111, 62);
            this.rdOne.Name = "rdOne";
            this.rdOne.Size = new System.Drawing.Size(67, 17);
            this.rdOne.TabIndex = 1;
            this.rdOne.TabStop = true;
            this.rdOne.Text = "1值数据";
            this.rdOne.UseVisualStyleBackColor = true;
            // 
            // tbName1
            // 
            this.tbName1.Location = new System.Drawing.Point(115, 19);
            this.tbName1.Name = "tbName1";
            this.tbName1.Size = new System.Drawing.Size(154, 20);
            this.tbName1.TabIndex = 0;
            // 
            // group2
            // 
            this.group2.Controls.Add(this.label3);
            this.group2.Controls.Add(this.label2);
            this.group2.Controls.Add(this.tbName3);
            this.group2.Controls.Add(this.tbName2);
            this.group2.Location = new System.Drawing.Point(12, 122);
            this.group2.Name = "group2";
            this.group2.Size = new System.Drawing.Size(291, 96);
            this.group2.TabIndex = 1;
            this.group2.TabStop = false;
            this.group2.Text = "修改";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "新的信息名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "现在信息名称：";
            // 
            // tbName3
            // 
            this.tbName3.Location = new System.Drawing.Point(115, 56);
            this.tbName3.Name = "tbName3";
            this.tbName3.Size = new System.Drawing.Size(154, 20);
            this.tbName3.TabIndex = 0;
            // 
            // tbName2
            // 
            this.tbName2.Location = new System.Drawing.Point(115, 19);
            this.tbName2.Name = "tbName2";
            this.tbName2.ReadOnly = true;
            this.tbName2.Size = new System.Drawing.Size(154, 20);
            this.tbName2.TabIndex = 0;
            // 
            // group3
            // 
            this.group3.Controls.Add(this.label5);
            this.group3.Controls.Add(this.label4);
            this.group3.Controls.Add(this.tbName4);
            this.group3.Location = new System.Drawing.Point(12, 234);
            this.group3.Name = "group3";
            this.group3.Size = new System.Drawing.Size(291, 96);
            this.group3.TabIndex = 2;
            this.group3.TabStop = false;
            this.group3.Text = "删除";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(71, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "你确定删除基础信息？";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "基础信息名称：";
            // 
            // tbName4
            // 
            this.tbName4.Location = new System.Drawing.Point(115, 19);
            this.tbName4.Name = "tbName4";
            this.tbName4.ReadOnly = true;
            this.tbName4.Size = new System.Drawing.Size(154, 20);
            this.tbName4.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(323, 245);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(77, 28);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确 定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(323, 302);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "返  回";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmBaseDataEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 347);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.group3);
            this.Controls.Add(this.group2);
            this.Controls.Add(this.group1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmBaseDataEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "基础信息编辑";
            this.Load += new System.EventHandler(this.FrmBaseDataEdit_Load);
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox group1;
        private System.Windows.Forms.RadioButton rdTwo;
        private System.Windows.Forms.RadioButton rdOne;
        private System.Windows.Forms.TextBox tbName1;
        private System.Windows.Forms.GroupBox group2;
        private System.Windows.Forms.TextBox tbName3;
        private System.Windows.Forms.TextBox tbName2;
        private System.Windows.Forms.GroupBox group3;
        private System.Windows.Forms.TextBox tbName4;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}