namespace CarSaleMan
{
    partial class FrmReportSaleCountTotal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportSaleCountTotal));
            this.prevPanelReport = new C1.Win.C1Preview.C1PrintPreviewControl();
            this.prntDoc = new C1.C1Preview.C1PrintDocument();
            this.storReportStoreoutcountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsDB = new CarSaleMan.CmsDB();
            this.storReportStoreoutcountTableAdapter = new CarSaleMan.CmsDBTableAdapters.stor_report_storeoutcountTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.prevPanelReport)).BeginInit();
            this.prevPanelReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.storReportStoreoutcountBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).BeginInit();
            this.SuspendLayout();
            // 
            // prevPanelReport
            // 
            this.prevPanelReport.AvailablePreviewActions = ((C1.Win.C1Preview.C1PreviewActionFlags)(((((((((((((((((((((C1.Win.C1Preview.C1PreviewActionFlags.PageSetup | C1.Win.C1Preview.C1PreviewActionFlags.Print)
                        | C1.Win.C1Preview.C1PreviewActionFlags.PageSingle)
                        | C1.Win.C1Preview.C1PreviewActionFlags.PageContinuous)
                        | C1.Win.C1Preview.C1PreviewActionFlags.PageFacing)
                        | C1.Win.C1Preview.C1PreviewActionFlags.PageFacingContinuous)
                        | C1.Win.C1Preview.C1PreviewActionFlags.GoFirst)
                        | C1.Win.C1Preview.C1PreviewActionFlags.GoPrev)
                        | C1.Win.C1Preview.C1PreviewActionFlags.GoNext)
                        | C1.Win.C1Preview.C1PreviewActionFlags.GoLast)
                        | C1.Win.C1Preview.C1PreviewActionFlags.GoPage)
                        | C1.Win.C1Preview.C1PreviewActionFlags.HistoryNext)
                        | C1.Win.C1Preview.C1PreviewActionFlags.HistoryPrev)
                        | C1.Win.C1Preview.C1PreviewActionFlags.ZoomIn)
                        | C1.Win.C1Preview.C1PreviewActionFlags.ZoomOut)
                        | C1.Win.C1Preview.C1PreviewActionFlags.ZoomFactor)
                        | C1.Win.C1Preview.C1PreviewActionFlags.ZoomInTool)
                        | C1.Win.C1Preview.C1PreviewActionFlags.ZoomOutTool)
                        | C1.Win.C1Preview.C1PreviewActionFlags.HandTool)
                        | C1.Win.C1Preview.C1PreviewActionFlags.SelectTextTool)
                        | C1.Win.C1Preview.C1PreviewActionFlags.Find)));
            this.prevPanelReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prevPanelReport.ImageSet = C1.Win.C1Preview.ImageSetEnum.XP3;
            this.prevPanelReport.Location = new System.Drawing.Point(0, 0);
            this.prevPanelReport.Name = "prevPanelReport";
            this.prevPanelReport.OutlineViewCaption = "概述";
            // 
            // prevPanelReport.OutlineView
            // 
            this.prevPanelReport.PreviewOutlineView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prevPanelReport.PreviewOutlineView.Location = new System.Drawing.Point(0, 0);
            this.prevPanelReport.PreviewOutlineView.Name = "OutlineView";
            this.prevPanelReport.PreviewOutlineView.Size = new System.Drawing.Size(165, 427);
            this.prevPanelReport.PreviewOutlineView.TabIndex = 0;
            // 
            // prevPanelReport.PreviewPane
            // 
            this.prevPanelReport.PreviewPane.Document = this.prntDoc;
            this.prevPanelReport.PreviewPane.IntegrateExternalTools = true;
            this.prevPanelReport.PreviewPane.TabIndex = 0;
            this.prevPanelReport.PreviewPane.ZoomMode = C1.Win.C1Preview.ZoomModeEnum.ActualSize;
            // 
            // prevPanelReport.PreviewTextSearchPanel
            // 
            this.prevPanelReport.PreviewTextSearchPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.prevPanelReport.PreviewTextSearchPanel.Location = new System.Drawing.Point(530, 0);
            this.prevPanelReport.PreviewTextSearchPanel.MinimumSize = new System.Drawing.Size(200, 240);
            this.prevPanelReport.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel";
            this.prevPanelReport.PreviewTextSearchPanel.Size = new System.Drawing.Size(200, 453);
            this.prevPanelReport.PreviewTextSearchPanel.TabIndex = 0;
            this.prevPanelReport.PreviewTextSearchPanel.Visible = false;
            // 
            // prevPanelReport.ThumbnailView
            // 
            this.prevPanelReport.PreviewThumbnailView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prevPanelReport.PreviewThumbnailView.Location = new System.Drawing.Point(0, 0);
            this.prevPanelReport.PreviewThumbnailView.Name = "ThumbnailView";
            this.prevPanelReport.PreviewThumbnailView.Size = new System.Drawing.Size(165, 480);
            this.prevPanelReport.PreviewThumbnailView.TabIndex = 0;
            this.prevPanelReport.PreviewThumbnailView.UseImageAsThumbnail = false;
            this.prevPanelReport.Size = new System.Drawing.Size(863, 553);
            this.prevPanelReport.TabIndex = 4;
            this.prevPanelReport.Text = "c1PrintPreviewControl1";
            this.prevPanelReport.ThumbnailViewCaption = "首页";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.File.Open.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.File.Open.Image")));
            this.prevPanelReport.ToolBars.File.Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.File.Open.Name = "btnFileOpen";
            this.prevPanelReport.ToolBars.File.Open.Size = new System.Drawing.Size(32, 22);
            this.prevPanelReport.ToolBars.File.Open.Tag = "C1PreviewActionEnum.FileOpen";
            this.prevPanelReport.ToolBars.File.Open.ToolTipText = "Open File";
            this.prevPanelReport.ToolBars.File.Open.Visible = false;
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.File.PageSetup.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.File.PageSetup.Image")));
            this.prevPanelReport.ToolBars.File.PageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.File.PageSetup.Name = "btnPageSetup";
            this.prevPanelReport.ToolBars.File.PageSetup.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.File.PageSetup.Tag = "C1PreviewActionEnum.PageSetup";
            this.prevPanelReport.ToolBars.File.PageSetup.ToolTipText = "Page Setup";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.File.Print.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.File.Print.Image")));
            this.prevPanelReport.ToolBars.File.Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.File.Print.Name = "btnPrint";
            this.prevPanelReport.ToolBars.File.Print.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.File.Print.Tag = "C1PreviewActionEnum.Print";
            this.prevPanelReport.ToolBars.File.Print.ToolTipText = "Print";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.File.Reflow.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.File.Reflow.Image")));
            this.prevPanelReport.ToolBars.File.Reflow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.File.Reflow.Name = "btnReflow";
            this.prevPanelReport.ToolBars.File.Reflow.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.File.Reflow.Tag = "C1PreviewActionEnum.Reflow";
            this.prevPanelReport.ToolBars.File.Reflow.ToolTipText = "Reflow";
            this.prevPanelReport.ToolBars.File.Reflow.Visible = false;
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.File.Save.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.File.Save.Image")));
            this.prevPanelReport.ToolBars.File.Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.File.Save.Name = "btnFileSave";
            this.prevPanelReport.ToolBars.File.Save.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.File.Save.Tag = "C1PreviewActionEnum.FileSave";
            this.prevPanelReport.ToolBars.File.Save.ToolTipText = "Save File";
            this.prevPanelReport.ToolBars.File.Save.Visible = false;
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Navigation.GoFirst.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Navigation.GoFirst.Image")));
            this.prevPanelReport.ToolBars.Navigation.GoFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Navigation.GoFirst.Name = "btnGoFirst";
            this.prevPanelReport.ToolBars.Navigation.GoFirst.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.Navigation.GoFirst.Tag = "C1PreviewActionEnum.GoFirst";
            this.prevPanelReport.ToolBars.Navigation.GoFirst.ToolTipText = "Go To First Page";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Navigation.GoLast.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Navigation.GoLast.Image")));
            this.prevPanelReport.ToolBars.Navigation.GoLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Navigation.GoLast.Name = "btnGoLast";
            this.prevPanelReport.ToolBars.Navigation.GoLast.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.Navigation.GoLast.Tag = "C1PreviewActionEnum.GoLast";
            this.prevPanelReport.ToolBars.Navigation.GoLast.ToolTipText = "Go To Last Page";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Navigation.GoNext.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Navigation.GoNext.Image")));
            this.prevPanelReport.ToolBars.Navigation.GoNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Navigation.GoNext.Name = "btnGoNext";
            this.prevPanelReport.ToolBars.Navigation.GoNext.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.Navigation.GoNext.Tag = "C1PreviewActionEnum.GoNext";
            this.prevPanelReport.ToolBars.Navigation.GoNext.ToolTipText = "Go To Next Page";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Navigation.GoPrev.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Navigation.GoPrev.Image")));
            this.prevPanelReport.ToolBars.Navigation.GoPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Navigation.GoPrev.Name = "btnGoPrev";
            this.prevPanelReport.ToolBars.Navigation.GoPrev.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.Navigation.GoPrev.Tag = "C1PreviewActionEnum.GoPrev";
            this.prevPanelReport.ToolBars.Navigation.GoPrev.ToolTipText = "Go To Previous Page";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Navigation.HistoryNext.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Navigation.HistoryNext.Image")));
            this.prevPanelReport.ToolBars.Navigation.HistoryNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Navigation.HistoryNext.Name = "btnHistoryNext";
            this.prevPanelReport.ToolBars.Navigation.HistoryNext.Size = new System.Drawing.Size(32, 22);
            this.prevPanelReport.ToolBars.Navigation.HistoryNext.Tag = "C1PreviewActionEnum.HistoryNext";
            this.prevPanelReport.ToolBars.Navigation.HistoryNext.ToolTipText = "Next View";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Navigation.HistoryPrev.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Navigation.HistoryPrev.Image")));
            this.prevPanelReport.ToolBars.Navigation.HistoryPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Navigation.HistoryPrev.Name = "btnHistoryPrev";
            this.prevPanelReport.ToolBars.Navigation.HistoryPrev.Size = new System.Drawing.Size(32, 22);
            this.prevPanelReport.ToolBars.Navigation.HistoryPrev.Tag = "C1PreviewActionEnum.HistoryPrev";
            this.prevPanelReport.ToolBars.Navigation.HistoryPrev.ToolTipText = "Previous View";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Navigation.LblOfPages.Name = "lblOfPages";
            this.prevPanelReport.ToolBars.Navigation.LblOfPages.Size = new System.Drawing.Size(30, 22);
            this.prevPanelReport.ToolBars.Navigation.LblOfPages.Tag = "C1PreviewActionEnum.GoPageCount";
            this.prevPanelReport.ToolBars.Navigation.LblOfPages.Text = "0 页";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Navigation.LblPage.Name = "lblPage";
            this.prevPanelReport.ToolBars.Navigation.LblPage.Size = new System.Drawing.Size(19, 22);
            this.prevPanelReport.ToolBars.Navigation.LblPage.Tag = "C1PreviewActionEnum.GoPageLabel";
            this.prevPanelReport.ToolBars.Navigation.LblPage.Text = "页";
            this.prevPanelReport.ToolBars.Navigation.ToolTipPageNo = null;
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Page.Continuous.Checked = true;
            this.prevPanelReport.ToolBars.Page.Continuous.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prevPanelReport.ToolBars.Page.Continuous.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Page.Continuous.Image")));
            this.prevPanelReport.ToolBars.Page.Continuous.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Page.Continuous.Name = "btnPageContinuous";
            this.prevPanelReport.ToolBars.Page.Continuous.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.Page.Continuous.Tag = "C1PreviewActionEnum.PageContinuous";
            this.prevPanelReport.ToolBars.Page.Continuous.ToolTipText = "Continuous View";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Page.Facing.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Page.Facing.Image")));
            this.prevPanelReport.ToolBars.Page.Facing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Page.Facing.Name = "btnPageFacing";
            this.prevPanelReport.ToolBars.Page.Facing.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.Page.Facing.Tag = "C1PreviewActionEnum.PageFacing";
            this.prevPanelReport.ToolBars.Page.Facing.ToolTipText = "Pages Facing View";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Page.FacingContinuous.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Page.FacingContinuous.Image")));
            this.prevPanelReport.ToolBars.Page.FacingContinuous.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Page.FacingContinuous.Name = "btnPageFacingContinuous";
            this.prevPanelReport.ToolBars.Page.FacingContinuous.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.Page.FacingContinuous.Tag = "C1PreviewActionEnum.PageFacingContinuous";
            this.prevPanelReport.ToolBars.Page.FacingContinuous.ToolTipText = "Pages Facing Continuous View";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Page.Single.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Page.Single.Image")));
            this.prevPanelReport.ToolBars.Page.Single.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Page.Single.Name = "btnPageSingle";
            this.prevPanelReport.ToolBars.Page.Single.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.Page.Single.Tag = "C1PreviewActionEnum.PageSingle";
            this.prevPanelReport.ToolBars.Page.Single.ToolTipText = "Single Page View";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Text.Find.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Text.Find.Image")));
            this.prevPanelReport.ToolBars.Text.Find.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Text.Find.Name = "btnFind";
            this.prevPanelReport.ToolBars.Text.Find.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.Text.Find.Tag = "C1PreviewActionEnum.Find";
            this.prevPanelReport.ToolBars.Text.Find.ToolTipText = "Find Text";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Text.Hand.Checked = true;
            this.prevPanelReport.ToolBars.Text.Hand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prevPanelReport.ToolBars.Text.Hand.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Text.Hand.Image")));
            this.prevPanelReport.ToolBars.Text.Hand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Text.Hand.Name = "btnHandTool";
            this.prevPanelReport.ToolBars.Text.Hand.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.Text.Hand.Tag = "C1PreviewActionEnum.HandTool";
            this.prevPanelReport.ToolBars.Text.Hand.ToolTipText = "Hand Tool";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Text.SelectText.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Text.SelectText.Image")));
            this.prevPanelReport.ToolBars.Text.SelectText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Text.SelectText.Name = "btnSelectTextTool";
            this.prevPanelReport.ToolBars.Text.SelectText.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.Text.SelectText.Tag = "C1PreviewActionEnum.SelectTextTool";
            this.prevPanelReport.ToolBars.Text.SelectText.ToolTipText = "Text Select Tool";
            this.prevPanelReport.ToolBars.Zoom.ToolTipZoomFactor = null;
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Zoom.ZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Zoom.ZoomIn.Image")));
            this.prevPanelReport.ToolBars.Zoom.ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Zoom.ZoomIn.Name = "btnZoomIn";
            this.prevPanelReport.ToolBars.Zoom.ZoomIn.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.Zoom.ZoomIn.Tag = "C1PreviewActionEnum.ZoomIn";
            this.prevPanelReport.ToolBars.Zoom.ZoomIn.ToolTipText = "Zoom In";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Zoom.ZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Zoom.ZoomOut.Image")));
            this.prevPanelReport.ToolBars.Zoom.ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Zoom.ZoomOut.Name = "btnZoomOut";
            this.prevPanelReport.ToolBars.Zoom.ZoomOut.Size = new System.Drawing.Size(23, 22);
            this.prevPanelReport.ToolBars.Zoom.ZoomOut.Tag = "C1PreviewActionEnum.ZoomOut";
            this.prevPanelReport.ToolBars.Zoom.ZoomOut.ToolTipText = "Zoom Out";
            // 
            // 
            // 
            this.prevPanelReport.ToolBars.Zoom.ZoomTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prevPanelReport.ToolBars.Zoom.ZoomInTool,
            this.prevPanelReport.ToolBars.Zoom.ZoomOutTool});
            this.prevPanelReport.ToolBars.Zoom.ZoomTool.Image = ((System.Drawing.Image)(resources.GetObject("prevPanelReport.ToolBars.Zoom.ZoomTool.Image")));
            this.prevPanelReport.ToolBars.Zoom.ZoomTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevPanelReport.ToolBars.Zoom.ZoomTool.Name = "btnZoomTool";
            this.prevPanelReport.ToolBars.Zoom.ZoomTool.Size = new System.Drawing.Size(32, 22);
            this.prevPanelReport.ToolBars.Zoom.ZoomTool.Tag = "C1PreviewActionEnum.ZoomInTool";
            this.prevPanelReport.ToolBars.Zoom.ZoomTool.ToolTipText = "Zoom In Tool";
            // 
            // prntDoc
            // 
            this.prntDoc.PageLayouts.Default.PageSettings = new C1.C1Preview.C1PageSettings(false, System.Drawing.Printing.PaperKind.A4, false, "10mm", "10mm", "10mm", "10mm", System.Drawing.Printing.PaperSourceKind.Upper, 1, null, System.Drawing.Printing.PrinterResolutionKind.Custom, 300, 300);
            this.prntDoc.TagsInputDialogClass = null;
            // 
            // storReportStoreoutcountBindingSource
            // 
            this.storReportStoreoutcountBindingSource.DataMember = "stor_report_storeoutcount";
            this.storReportStoreoutcountBindingSource.DataSource = this.cmsDB;
            // 
            // cmsDB
            // 
            this.cmsDB.DataSetName = "CmsDB";
            this.cmsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // storReportStoreoutcountTableAdapter
            // 
            this.storReportStoreoutcountTableAdapter.ClearBeforeFill = true;
            // 
            // FrmReportSaleCountTotal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 553);
            this.Controls.Add(this.prevPanelReport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmReportSaleCountTotal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "销售数量汇总报表";
            ((System.ComponentModel.ISupportInitialize)(this.prevPanelReport)).EndInit();
            this.prevPanelReport.ResumeLayout(false);
            this.prevPanelReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.storReportStoreoutcountBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmsDB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Preview.C1PrintPreviewControl prevPanelReport;
        private C1.C1Preview.C1PrintDocument prntDoc;
        private System.Windows.Forms.BindingSource storReportStoreoutcountBindingSource;
        private CmsDB cmsDB;
        private CmsDBTableAdapters.stor_report_storeoutcountTableAdapter storReportStoreoutcountTableAdapter;
    }
}