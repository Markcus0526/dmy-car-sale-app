namespace CarSaleMan
{
    partial class FrmActionHis
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbActionLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(173, 317);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "返  回";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbActionLog
            // 
            this.tbActionLog.BackColor = System.Drawing.Color.White;
            this.tbActionLog.Location = new System.Drawing.Point(12, 12);
            this.tbActionLog.Multiline = true;
            this.tbActionLog.Name = "tbActionLog";
            this.tbActionLog.ReadOnly = true;
            this.tbActionLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbActionLog.Size = new System.Drawing.Size(398, 299);
            this.tbActionLog.TabIndex = 5;
            this.tbActionLog.WordWrap = false;
            // 
            // FrmActionHis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 353);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbActionLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmActionHis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "单车流转阅览器";
            this.Load += new System.EventHandler(this.FrmActionHis_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbActionLog;
    }
}