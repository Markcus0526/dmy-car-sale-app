namespace CarSaleMan
{
    partial class FrmLogon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogon));
            this.cbUsername = new System.Windows.Forms.ComboBox();
            this.tblUserinfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLogon = new System.Windows.Forms.Button();
            this.cbDepartCode = new System.Windows.Forms.ComboBox();
            this.vwDepartmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelCode = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelPass = new System.Windows.Forms.Label();
            this.tblUserinfoTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_userinfoTableAdapter();
            this.vwDepartmentTableAdapter = new CarSaleMan.CmsDBTableAdapters.vw_departmentTableAdapter();
            this.tblPermissionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblPermissionTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_permissionTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.tblUserinfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwDepartmentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPermissionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cbUsername
            // 
            this.cbUsername.BackColor = System.Drawing.Color.PaleGreen;
            this.cbUsername.DataSource = this.tblUserinfoBindingSource;
            this.cbUsername.DisplayMember = "username";
            this.cbUsername.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUsername.FormattingEnabled = true;
            this.cbUsername.Location = new System.Drawing.Point(110, 183);
            this.cbUsername.Name = "cbUsername";
            this.cbUsername.Size = new System.Drawing.Size(120, 21);
            this.cbUsername.TabIndex = 2;
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
            // tbPassword
            // 
            this.tbPassword.BackColor = System.Drawing.Color.PaleGreen;
            this.tbPassword.Location = new System.Drawing.Point(110, 223);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(120, 20);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPass_KeyDown);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = global::CarSaleMan.Properties.Resources.btnExit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(292, 16);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(32, 32);
            this.btnExit.TabIndex = 5;
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // btnLogon
            // 
            this.btnLogon.BackColor = System.Drawing.Color.Transparent;
            this.btnLogon.BackgroundImage = global::CarSaleMan.Properties.Resources.btnLogon;
            this.btnLogon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogon.Location = new System.Drawing.Point(251, 160);
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.Size = new System.Drawing.Size(64, 64);
            this.btnLogon.TabIndex = 4;
            this.btnLogon.UseVisualStyleBackColor = false;
            this.btnLogon.Click += new System.EventHandler(this.btnLogon_Click);
            // 
            // cbDepartCode
            // 
            this.cbDepartCode.BackColor = System.Drawing.Color.PaleGreen;
            this.cbDepartCode.DataSource = this.vwDepartmentBindingSource;
            this.cbDepartCode.DisplayMember = "departmentcode";
            this.cbDepartCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepartCode.FormattingEnabled = true;
            this.cbDepartCode.Location = new System.Drawing.Point(110, 141);
            this.cbDepartCode.Name = "cbDepartCode";
            this.cbDepartCode.Size = new System.Drawing.Size(120, 21);
            this.cbDepartCode.TabIndex = 1;
            // 
            // vwDepartmentBindingSource
            // 
            this.vwDepartmentBindingSource.DataMember = "vw_department";
            this.vwDepartmentBindingSource.DataSource = this.cmsDB;
            // 
            // labelCode
            // 
            this.labelCode.AutoSize = true;
            this.labelCode.BackColor = System.Drawing.Color.Transparent;
            this.labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCode.ForeColor = System.Drawing.Color.White;
            this.labelCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCode.Location = new System.Drawing.Point(45, 144);
            this.labelCode.Name = "labelCode";
            this.labelCode.Size = new System.Drawing.Size(53, 16);
            this.labelCode.TabIndex = 11;
            this.labelCode.Text = "±à¡¡Âë";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.ForeColor = System.Drawing.Color.White;
            this.labelName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelName.Location = new System.Drawing.Point(45, 186);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(53, 16);
            this.labelName.TabIndex = 12;
            this.labelName.Text = "ÐÕ¡¡Ãû";
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.BackColor = System.Drawing.Color.Transparent;
            this.labelPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPass.ForeColor = System.Drawing.Color.White;
            this.labelPass.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPass.Location = new System.Drawing.Point(45, 226);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(53, 16);
            this.labelPass.TabIndex = 13;
            this.labelPass.Text = "¿Ú¡¡Áî";
            // 
            // tblUserinfoTableAdapter
            // 
            this.tblUserinfoTableAdapter.ClearBeforeFill = true;
            // 
            // vwDepartmentTableAdapter
            // 
            this.vwDepartmentTableAdapter.ClearBeforeFill = true;
            // 
            // tblPermissionBindingSource
            // 
            this.tblPermissionBindingSource.DataMember = "tbl_permission";
            this.tblPermissionBindingSource.DataSource = this.cmsDB;
            // 
            // tblPermissionTableAdapter
            // 
            this.tblPermissionTableAdapter.ClearBeforeFill = true;
            // 
            // FrmLogon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CarSaleMan.Properties.Resources.bkLogon;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(336, 272);
            this.Controls.Add(this.labelPass);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelCode);
            this.Controls.Add(this.cbDepartCode);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogon);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.cbUsername);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLogon";
            this.Load += new System.EventHandler(this.FrmLogon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tblUserinfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwDepartmentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPermissionBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbUsername;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLogon;
        private System.Windows.Forms.ComboBox cbDepartCode;
        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.BindingSource tblUserinfoBindingSource;
        private CmsDB cmsDB;
        private CmsDBTableAdapters.tbl_userinfoTableAdapter tblUserinfoTableAdapter;
        private System.Windows.Forms.BindingSource vwDepartmentBindingSource;
        private CmsDBTableAdapters.vw_departmentTableAdapter vwDepartmentTableAdapter;
        private System.Windows.Forms.BindingSource tblPermissionBindingSource;
        private CmsDBTableAdapters.tbl_permissionTableAdapter tblPermissionTableAdapter;
    }
}