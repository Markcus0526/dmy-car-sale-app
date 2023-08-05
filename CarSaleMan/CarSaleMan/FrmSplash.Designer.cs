namespace CarSaleMan
{
    partial class FrmSplash
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
            this.workerLoading = new System.ComponentModel.BackgroundWorker();
            this.progLoading = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // workerLoading
            // 
            this.workerLoading.WorkerReportsProgress = true;
            // 
            // progLoading
            // 
            this.progLoading.Location = new System.Drawing.Point(28, 235);
            this.progLoading.Name = "progLoading";
            this.progLoading.Size = new System.Drawing.Size(432, 23);
            this.progLoading.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progLoading.TabIndex = 0;
            // 
            // FrmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.progLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSplash";
            this.Load += new System.EventHandler(this.FrmSplash_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker workerLoading;
        private System.Windows.Forms.ProgressBar progLoading;

    }
}