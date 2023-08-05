namespace CarSaleMan
{
    partial class FrmMessage
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
            this.rdSelect2 = new System.Windows.Forms.RadioButton();
            this.rdSelect1 = new System.Windows.Forms.RadioButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblText2 = new System.Windows.Forms.Label();
            this.lblText1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rdSelect2
            // 
            this.rdSelect2.AutoSize = true;
            this.rdSelect2.BackColor = System.Drawing.Color.Transparent;
            this.rdSelect2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSelect2.ForeColor = System.Drawing.Color.White;
            this.rdSelect2.Location = new System.Drawing.Point(172, 62);
            this.rdSelect2.Name = "rdSelect2";
            this.rdSelect2.Size = new System.Drawing.Size(98, 20);
            this.rdSelect2.TabIndex = 15;
            this.rdSelect2.Text = "rdSelect2";
            this.rdSelect2.UseVisualStyleBackColor = false;
            // 
            // rdSelect1
            // 
            this.rdSelect1.AutoSize = true;
            this.rdSelect1.BackColor = System.Drawing.Color.Transparent;
            this.rdSelect1.Checked = true;
            this.rdSelect1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSelect1.ForeColor = System.Drawing.Color.White;
            this.rdSelect1.Location = new System.Drawing.Point(49, 62);
            this.rdSelect1.Name = "rdSelect1";
            this.rdSelect1.Size = new System.Drawing.Size(98, 20);
            this.rdSelect1.TabIndex = 14;
            this.rdSelect1.TabStop = true;
            this.rdSelect1.Text = "rdSelect1";
            this.rdSelect1.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Aqua;
            this.lblTitle.Location = new System.Drawing.Point(22, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(49, 13);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "label1";
            // 
            // lblText2
            // 
            this.lblText2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblText2.AutoSize = true;
            this.lblText2.BackColor = System.Drawing.Color.Transparent;
            this.lblText2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText2.ForeColor = System.Drawing.Color.White;
            this.lblText2.Location = new System.Drawing.Point(22, 76);
            this.lblText2.Name = "lblText2";
            this.lblText2.Size = new System.Drawing.Size(48, 16);
            this.lblText2.TabIndex = 12;
            this.lblText2.Text = "Text2";
            // 
            // lblText1
            // 
            this.lblText1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblText1.AutoSize = true;
            this.lblText1.BackColor = System.Drawing.Color.Transparent;
            this.lblText1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText1.ForeColor = System.Drawing.Color.White;
            this.lblText1.Location = new System.Drawing.Point(22, 55);
            this.lblText1.Name = "lblText1";
            this.lblText1.Size = new System.Drawing.Size(48, 16);
            this.lblText1.TabIndex = 11;
            this.lblText1.Text = "Text1";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightCyan;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnClose.Location = new System.Drawing.Point(128, 117);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 24);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "¹Ø±Õ";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.LightCyan;
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNo.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnNo.Location = new System.Drawing.Point(191, 117);
            this.btnNo.Margin = new System.Windows.Forms.Padding(0);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(55, 24);
            this.btnNo.TabIndex = 9;
            this.btnNo.Text = "·ñ";
            this.btnNo.UseVisualStyleBackColor = false;
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.Color.LightCyan;
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnYes.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnYes.Location = new System.Drawing.Point(74, 117);
            this.btnYes.Margin = new System.Windows.Forms.Padding(0);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(55, 24);
            this.btnYes.TabIndex = 8;
            this.btnYes.Text = "ÊÇ";
            this.btnYes.UseVisualStyleBackColor = false;
            // 
            // FrmMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CarSaleMan.Properties.Resources.bkMessage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(329, 161);
            this.Controls.Add(this.rdSelect2);
            this.Controls.Add(this.rdSelect1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblText2);
            this.Controls.Add(this.lblText1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMessage";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmMessage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdSelect2;
        private System.Windows.Forms.RadioButton rdSelect1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblText2;
        private System.Windows.Forms.Label lblText1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnYes;
    }
}