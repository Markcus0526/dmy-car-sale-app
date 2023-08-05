namespace CarSaleMan
{
    partial class FrmUserPermissionEdit
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
            this.group1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbName1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbDepartCode1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDepartName1 = new System.Windows.Forms.TextBox();
            this.group2 = new System.Windows.Forms.GroupBox();
            this.cbDepartCode3 = new System.Windows.Forms.ComboBox();
            this.vwDepartmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.label9 = new System.Windows.Forms.Label();
            this.tbName3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbDepartName3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDepartCode2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbDepartName2 = new System.Windows.Forms.TextBox();
            this.group3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbName4 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbDepartCode4 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbDepartName4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.vwDepartmentTableAdapter = new CarSaleMan.CmsDBTableAdapters.vw_departmentTableAdapter();
            this.tblUserinfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblUserinfoTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_userinfoTableAdapter();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vwDepartmentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            this.group3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblUserinfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // group1
            // 
            this.group1.Controls.Add(this.label7);
            this.group1.Controls.Add(this.tbName1);
            this.group1.Controls.Add(this.label6);
            this.group1.Controls.Add(this.tbDepartCode1);
            this.group1.Controls.Add(this.label1);
            this.group1.Controls.Add(this.tbDepartName1);
            this.group1.Location = new System.Drawing.Point(12, 12);
            this.group1.Name = "group1";
            this.group1.Size = new System.Drawing.Size(524, 78);
            this.group1.TabIndex = 0;
            this.group1.TabStop = false;
            this.group1.Text = "添加";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(353, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "姓名：";
            // 
            // tbName1
            // 
            this.tbName1.Location = new System.Drawing.Point(402, 33);
            this.tbName1.Name = "tbName1";
            this.tbName1.Size = new System.Drawing.Size(100, 20);
            this.tbName1.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "编码：";
            // 
            // tbDepartCode1
            // 
            this.tbDepartCode1.Location = new System.Drawing.Point(66, 33);
            this.tbDepartCode1.Name = "tbDepartCode1";
            this.tbDepartCode1.Size = new System.Drawing.Size(100, 20);
            this.tbDepartCode1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "部门：";
            // 
            // tbDepartName1
            // 
            this.tbDepartName1.Location = new System.Drawing.Point(233, 33);
            this.tbDepartName1.Name = "tbDepartName1";
            this.tbDepartName1.Size = new System.Drawing.Size(100, 20);
            this.tbDepartName1.TabIndex = 1;
            // 
            // group2
            // 
            this.group2.Controls.Add(this.cbDepartCode3);
            this.group2.Controls.Add(this.label9);
            this.group2.Controls.Add(this.tbName3);
            this.group2.Controls.Add(this.label10);
            this.group2.Controls.Add(this.label11);
            this.group2.Controls.Add(this.tbDepartName3);
            this.group2.Controls.Add(this.label2);
            this.group2.Controls.Add(this.tbName2);
            this.group2.Controls.Add(this.label3);
            this.group2.Controls.Add(this.tbDepartCode2);
            this.group2.Controls.Add(this.label8);
            this.group2.Controls.Add(this.tbDepartName2);
            this.group2.Location = new System.Drawing.Point(12, 105);
            this.group2.Name = "group2";
            this.group2.Size = new System.Drawing.Size(524, 106);
            this.group2.TabIndex = 1;
            this.group2.TabStop = false;
            this.group2.Text = "修改";
            // 
            // cbDepartCode3
            // 
            this.cbDepartCode3.DataSource = this.vwDepartmentBindingSource;
            this.cbDepartCode3.DisplayMember = "departmentcode";
            this.cbDepartCode3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepartCode3.FormattingEnabled = true;
            this.cbDepartCode3.Location = new System.Drawing.Point(66, 63);
            this.cbDepartCode3.Name = "cbDepartCode3";
            this.cbDepartCode3.Size = new System.Drawing.Size(100, 21);
            this.cbDepartCode3.TabIndex = 0;
            // 
            // vwDepartmentBindingSource
            // 
            this.vwDepartmentBindingSource.DataMember = "vw_department";
            this.vwDepartmentBindingSource.DataSource = this.cmsDB;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(353, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "姓名：";
            // 
            // tbName3
            // 
            this.tbName3.Location = new System.Drawing.Point(402, 63);
            this.tbName3.Name = "tbName3";
            this.tbName3.Size = new System.Drawing.Size(100, 20);
            this.tbName3.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "编码：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(184, 66);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "部门：";
            // 
            // tbDepartName3
            // 
            this.tbDepartName3.Location = new System.Drawing.Point(233, 63);
            this.tbDepartName3.Name = "tbDepartName3";
            this.tbDepartName3.ReadOnly = true;
            this.tbDepartName3.Size = new System.Drawing.Size(100, 20);
            this.tbDepartName3.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(353, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "姓名：";
            // 
            // tbName2
            // 
            this.tbName2.Location = new System.Drawing.Point(402, 28);
            this.tbName2.Name = "tbName2";
            this.tbName2.ReadOnly = true;
            this.tbName2.Size = new System.Drawing.Size(100, 20);
            this.tbName2.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "编码：";
            // 
            // tbDepartCode2
            // 
            this.tbDepartCode2.Location = new System.Drawing.Point(66, 28);
            this.tbDepartCode2.Name = "tbDepartCode2";
            this.tbDepartCode2.ReadOnly = true;
            this.tbDepartCode2.Size = new System.Drawing.Size(100, 20);
            this.tbDepartCode2.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(184, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "部门：";
            // 
            // tbDepartName2
            // 
            this.tbDepartName2.Location = new System.Drawing.Point(233, 28);
            this.tbDepartName2.Name = "tbDepartName2";
            this.tbDepartName2.ReadOnly = true;
            this.tbDepartName2.Size = new System.Drawing.Size(100, 20);
            this.tbDepartName2.TabIndex = 10;
            // 
            // group3
            // 
            this.group3.Controls.Add(this.label4);
            this.group3.Controls.Add(this.tbName4);
            this.group3.Controls.Add(this.label12);
            this.group3.Controls.Add(this.tbDepartCode4);
            this.group3.Controls.Add(this.label13);
            this.group3.Controls.Add(this.tbDepartName4);
            this.group3.Controls.Add(this.label5);
            this.group3.Location = new System.Drawing.Point(12, 234);
            this.group3.Name = "group3";
            this.group3.Size = new System.Drawing.Size(524, 96);
            this.group3.TabIndex = 2;
            this.group3.TabStop = false;
            this.group3.Text = "删除";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(353, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "姓名：";
            // 
            // tbName4
            // 
            this.tbName4.Location = new System.Drawing.Point(402, 28);
            this.tbName4.Name = "tbName4";
            this.tbName4.ReadOnly = true;
            this.tbName4.Size = new System.Drawing.Size(100, 20);
            this.tbName4.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "编码：";
            // 
            // tbDepartCode4
            // 
            this.tbDepartCode4.Location = new System.Drawing.Point(66, 28);
            this.tbDepartCode4.Name = "tbDepartCode4";
            this.tbDepartCode4.ReadOnly = true;
            this.tbDepartCode4.Size = new System.Drawing.Size(100, 20);
            this.tbDepartCode4.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(184, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "部门：";
            // 
            // tbDepartName4
            // 
            this.tbDepartName4.Location = new System.Drawing.Point(233, 28);
            this.tbDepartName4.Name = "tbDepartName4";
            this.tbDepartName4.ReadOnly = true;
            this.tbDepartName4.Size = new System.Drawing.Size(100, 20);
            this.tbDepartName4.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(184, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "你确定删除部门姓名？";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(136, 345);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(77, 28);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "确 定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancel.Location = new System.Drawing.Point(331, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "返  回";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // vwDepartmentTableAdapter
            // 
            this.vwDepartmentTableAdapter.ClearBeforeFill = true;
            // 
            // tblUserinfoBindingSource
            // 
            this.tblUserinfoBindingSource.DataMember = "tbl_userinfo";
            this.tblUserinfoBindingSource.DataSource = this.cmsDB;
            // 
            // tblUserinfoTableAdapter
            // 
            this.tblUserinfoTableAdapter.ClearBeforeFill = true;
            // 
            // FrmUserPermissionEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 385);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.group3);
            this.Controls.Add(this.group2);
            this.Controls.Add(this.group1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmUserPermissionEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "部门姓名编辑";
            this.Load += new System.EventHandler(this.FrmUserPermissionEdit_Load);
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vwDepartmentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblUserinfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox group1;
        private System.Windows.Forms.TextBox tbDepartName1;
        private System.Windows.Forms.GroupBox group2;
        private System.Windows.Forms.GroupBox group3;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbName1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDepartCode1;
        private System.Windows.Forms.ComboBox cbDepartCode3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbName3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbDepartName3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDepartCode2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbDepartName2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbName4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbDepartCode4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbDepartName4;
        private CmsDB cmsDB;
        private System.Windows.Forms.BindingSource vwDepartmentBindingSource;
        private CmsDBTableAdapters.vw_departmentTableAdapter vwDepartmentTableAdapter;
        private System.Windows.Forms.BindingSource tblUserinfoBindingSource;
        private CmsDBTableAdapters.tbl_userinfoTableAdapter tblUserinfoTableAdapter;
    }
}