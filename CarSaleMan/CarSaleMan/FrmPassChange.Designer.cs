namespace CarSaleMan
{
    partial class FrmPassChange
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.tbNewPassConfirm = new System.Windows.Forms.TextBox();
            this.label_NewPassConfirm = new System.Windows.Forms.Label();
            this.tbNewPass = new System.Windows.Forms.TextBox();
            this.label_NewPass = new System.Windows.Forms.Label();
            this.tbOldPass = new System.Windows.Forms.TextBox();
            this.label_OldPass = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tblUserinfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.tblUserinfoTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_userinfoTableAdapter();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblUserinfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.tbNewPassConfirm);
            this.groupBox.Controls.Add(this.label_NewPassConfirm);
            this.groupBox.Controls.Add(this.tbNewPass);
            this.groupBox.Controls.Add(this.label_NewPass);
            this.groupBox.Controls.Add(this.tbOldPass);
            this.groupBox.Controls.Add(this.label_OldPass);
            this.groupBox.Controls.Add(this.tbName);
            this.groupBox.Controls.Add(this.labelName);
            this.groupBox.Location = new System.Drawing.Point(12, 17);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(295, 159);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "修改密码";
            // 
            // tbNewPassConfirm
            // 
            this.tbNewPassConfirm.Location = new System.Drawing.Point(96, 124);
            this.tbNewPassConfirm.Name = "tbNewPassConfirm";
            this.tbNewPassConfirm.PasswordChar = '*';
            this.tbNewPassConfirm.Size = new System.Drawing.Size(172, 20);
            this.tbNewPassConfirm.TabIndex = 3;
            this.tbNewPassConfirm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewPassConfirm_KeyDown);
            // 
            // label_NewPassConfirm
            // 
            this.label_NewPassConfirm.AutoSize = true;
            this.label_NewPassConfirm.Location = new System.Drawing.Point(17, 127);
            this.label_NewPassConfirm.Name = "label_NewPassConfirm";
            this.label_NewPassConfirm.Size = new System.Drawing.Size(73, 13);
            this.label_NewPassConfirm.TabIndex = 7;
            this.label_NewPassConfirm.Text = "新密码确定 :";
            // 
            // tbNewPass
            // 
            this.tbNewPass.Location = new System.Drawing.Point(96, 93);
            this.tbNewPass.Name = "tbNewPass";
            this.tbNewPass.PasswordChar = '*';
            this.tbNewPass.Size = new System.Drawing.Size(172, 20);
            this.tbNewPass.TabIndex = 2;
            this.tbNewPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewPass_KeyDown);
            // 
            // label_NewPass
            // 
            this.label_NewPass.AutoSize = true;
            this.label_NewPass.Location = new System.Drawing.Point(17, 96);
            this.label_NewPass.Name = "label_NewPass";
            this.label_NewPass.Size = new System.Drawing.Size(49, 13);
            this.label_NewPass.TabIndex = 4;
            this.label_NewPass.Text = "新密码 :";
            // 
            // tbOldPass
            // 
            this.tbOldPass.Location = new System.Drawing.Point(96, 62);
            this.tbOldPass.Name = "tbOldPass";
            this.tbOldPass.PasswordChar = '*';
            this.tbOldPass.Size = new System.Drawing.Size(172, 20);
            this.tbOldPass.TabIndex = 1;
            this.tbOldPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOldPass_KeyDown);
            // 
            // label_OldPass
            // 
            this.label_OldPass.AutoSize = true;
            this.label_OldPass.Location = new System.Drawing.Point(17, 65);
            this.label_OldPass.Name = "label_OldPass";
            this.label_OldPass.Size = new System.Drawing.Size(49, 13);
            this.label_OldPass.TabIndex = 2;
            this.label_OldPass.Text = "原密码 :";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(96, 29);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(172, 20);
            this.tbName.TabIndex = 6;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(20, 32);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(49, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "姓　名 :";
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(35, 189);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(91, 23);
            this.btnChange.TabIndex = 4;
            this.btnChange.Text = "确　认";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(189, 189);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "放　弃";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tblUserinfoBindingSource
            // 
            this.tblUserinfoBindingSource.DataMember = "tbl_userinfo";
            this.tblUserinfoBindingSource.DataSource = this.cmsDB;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblUserinfoTableAdapter
            // 
            this.tblUserinfoTableAdapter.ClearBeforeFill = true;
            // 
            // FrmPassChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 231);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPassChange";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改密码";
            this.Load += new System.EventHandler(this.FrmPassChange_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblUserinfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.TextBox tbNewPass;
        private System.Windows.Forms.Label label_NewPass;
        private System.Windows.Forms.TextBox tbOldPass;
        private System.Windows.Forms.Label label_OldPass;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbNewPassConfirm;
        private System.Windows.Forms.Label label_NewPassConfirm;
        private System.Windows.Forms.BindingSource tblUserinfoBindingSource;
        private CmsDB cmsDB;
        private CmsDBTableAdapters.tbl_userinfoTableAdapter tblUserinfoTableAdapter;
    }
}