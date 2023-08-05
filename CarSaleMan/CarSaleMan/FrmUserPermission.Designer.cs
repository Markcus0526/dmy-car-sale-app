namespace CarSaleMan
{
    partial class FrmUserPermission
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserPermission));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbAddName = new System.Windows.Forms.ToolStripButton();
            this.tsbDeleteName = new System.Windows.Forms.ToolStripButton();
            this.tsbChangeName = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPassword = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSaveValue = new System.Windows.Forms.ToolStripButton();
            this.tsbRejectValue = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbReturn = new System.Windows.Forms.ToolStripButton();
            this.gridValue = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tblPermissionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.gridUser = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tblUserInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblUserinfoTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_userinfoTableAdapter();
            this.tblPermissionTableAdapter = new CarSaleMan.CmsDBTableAdapters.tbl_permissionTableAdapter();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPermissionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblUserInfoBindingSource)).BeginInit();
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
            this.tsbAddName,
            this.tsbDeleteName,
            this.tsbChangeName,
            this.toolStripSeparator1,
            this.tsbPassword,
            this.toolStripSeparator3,
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
            // tsbPassword
            // 
            this.tsbPassword.AutoSize = false;
            this.tsbPassword.Image = ((System.Drawing.Image)(resources.GetObject("tsbPassword.Image")));
            this.tsbPassword.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPassword.Name = "tsbPassword";
            this.tsbPassword.Size = new System.Drawing.Size(36, 57);
            this.tsbPassword.Text = "密码";
            this.tsbPassword.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbPassword.Click += new System.EventHandler(this.tsbPassword_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 60);
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
            this.gridValue.DataSource = this.tblPermissionBindingSource;
            this.gridValue.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.gridValue.Location = new System.Drawing.Point(196, 63);
            this.gridValue.Name = "gridValue";
            this.gridValue.Rows.Count = 1;
            this.gridValue.Rows.DefaultSize = 19;
            this.gridValue.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridValue.ShowCursor = true;
            this.gridValue.Size = new System.Drawing.Size(386, 415);
            this.gridValue.StyleInfo = resources.GetString("gridValue.StyleInfo");
            this.gridValue.TabIndex = 13;
            // 
            // tblPermissionBindingSource
            // 
            this.tblPermissionBindingSource.DataMember = "tbl_permission";
            this.tblPermissionBindingSource.DataSource = this.cmsDB;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridUser
            // 
            this.gridUser.AllowEditing = false;
            this.gridUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gridUser.BackColor = System.Drawing.Color.White;
            this.gridUser.ColumnInfo = resources.GetString("gridUser.ColumnInfo");
            this.gridUser.ExtendLastCol = true;
            this.gridUser.Location = new System.Drawing.Point(5, 63);
            this.gridUser.Name = "gridUser";
            this.gridUser.Rows.Count = 1;
            this.gridUser.Rows.DefaultSize = 19;
            this.gridUser.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridUser.ShowCursor = true;
            this.gridUser.Size = new System.Drawing.Size(185, 415);
            this.gridUser.StyleInfo = resources.GetString("gridUser.StyleInfo");
            this.gridUser.TabIndex = 14;
            // 
            // tblUserInfoBindingSource
            // 
            this.tblUserInfoBindingSource.DataMember = "tbl_userinfo";
            this.tblUserInfoBindingSource.DataSource = this.cmsDB;
            // 
            // tblUserinfoTableAdapter
            // 
            this.tblUserinfoTableAdapter.ClearBeforeFill = true;
            // 
            // tblPermissionTableAdapter
            // 
            this.tblPermissionTableAdapter.ClearBeforeFill = true;
            // 
            // FrmUserPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(587, 482);
            this.Controls.Add(this.gridUser);
            this.Controls.Add(this.gridValue);
            this.Controls.Add(this.toolStrip);
            this.Name = "FrmUserPermission";
            this.Text = "权限设定";
            this.Load += new System.EventHandler(this.FrmUserPermission_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPermissionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblUserInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbAddName;
        private System.Windows.Forms.ToolStripButton tsbChangeName;
        private System.Windows.Forms.ToolStripButton tsbDeleteName;
        private System.Windows.Forms.ToolStripButton tsbSaveValue;
        private System.Windows.Forms.ToolStripButton tsbRejectValue;
        private System.Windows.Forms.ToolStripButton tsbReturn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private C1.Win.C1FlexGrid.C1FlexGrid gridValue;
        private C1.Win.C1FlexGrid.C1FlexGrid gridUser;
        private System.Windows.Forms.BindingSource tblUserInfoBindingSource;
        private CmsDB cmsDB;
        private CmsDBTableAdapters.tbl_userinfoTableAdapter tblUserinfoTableAdapter;
        private System.Windows.Forms.ToolStripButton tsbPassword;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.BindingSource tblPermissionBindingSource;
        private CmsDBTableAdapters.tbl_permissionTableAdapter tblPermissionTableAdapter;

    }
}