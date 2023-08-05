using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1Chart;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Diagnostics;

namespace CarSaleMan
{
    public partial class FrmChartSaleCount : Form
    {
        #region Fields and Properties

        public static FrmChartSaleCount frmChartSaleCount;

        #endregion

        #region Constructors

        public FrmChartSaleCount()
        {
            InitializeComponent();

            // initialize event handler
            this.Activated += new EventHandler(FrmChartSaleCount_Activated);
            this.FormClosing += new FormClosingEventHandler(FrmChartSaleCount_FormClosing);
        }

        #endregion

        #region Public Methods

        public static FrmChartSaleCount GetFrmInstance(Form parent, bool pCreate)
        {
            if (pCreate == true)
            {
                if (frmChartSaleCount == null) //if not created yet, Create an instance
                {
                    frmChartSaleCount = new FrmChartSaleCount();
                    frmChartSaleCount.MdiParent = parent;
                }
            }
            return frmChartSaleCount;  //just created or created earlier.Return it
        }

        #endregion

        #region Private Methods

        private void DrawChart()
        {
            try
            {
                DateTime startDate = dtStartDate.Value;
                DateTime endDate = dtEndDate.Value;

                if (startDate > endDate)
                {
                    return;
                }

                storChartSalecountTableAdapter.Fill(cmsDB.stor_chart_salecount, startDate, endDate);

                if (rdPie.Checked)
                {
                    chartResult.ChartGroups.Group0.ChartType = C1.Win.C1Chart.Chart2DTypeEnum.Pie;
                    chartResult.Legend.Visible = true;

                    chartResult.ChartLabels.LabelsCollection.Clear();
                    int nSeriesCount = chartResult.ChartGroups.Group0.ChartData.SeriesList.Count;
                    chartResult.ChartArea.AxisX.Visible = true;
                    chartResult.ChartArea.AxisX.TickLabels = C1.Win.C1Chart.TickLabelsEnum.NextToAxis;

                    Style s = chartResult.ChartLabels.DefaultLabelStyle;
                    s.Font = new Font("Courier New", 12);
                    s.BackColor = SystemColors.Info;
                    s.Opaque = true;
                    s.Border.BorderStyle = BorderStyleEnum.Solid;

                    for (int i = 0; i < nSeriesCount; i++)
                    {
                        C1.Win.C1Chart.ChartDataSeries series = chartResult.ChartGroups.Group0.ChartData.SeriesList[i];

                        // attach labels to each slice
                        for (int j = 0; j < series.PointData.Length; j++)
                        {
                            C1.Win.C1Chart.Label lbl = chartResult.ChartLabels.LabelsCollection.AddNewLabel();
                            lbl.Text = /*Convert.ToString(series.Label) + ":" + */Convert.ToString(series.Y[j]);
                            lbl.Compass = LabelCompassEnum.Radial;
                            lbl.Offset = 10;
                            lbl.Connected = true;
                            lbl.Visible = true;
                            lbl.AttachMethod = AttachMethodEnum.DataIndex;
                            AttachMethodData am = lbl.AttachMethodData;
                            am.GroupIndex = 0;
                            am.SeriesIndex = i;
                            am.PointIndex = j;
                        }
                    }

                    CommonMisc.DecideChartClusterWidth(ref chartResult);
                    CommonMisc.SetXLabelsVert(ref chartResult);

                    return;
                }
                else if (rdLine.Checked)
                {
                    chartResult.ChartGroups.Group0.ChartType = C1.Win.C1Chart.Chart2DTypeEnum.XYPlot;
                    chartResult.Legend.Visible = true;
                }
                else if (rdBar.Checked)
                {
                    chartResult.ChartGroups.Group0.ChartType = C1.Win.C1Chart.Chart2DTypeEnum.Bar;
                    chartResult.Legend.Visible = true;
                }

                chartResult.ChartLabels.LabelsCollection.Clear();
                int nSeriesCount1 = chartResult.ChartGroups.Group0.ChartData.SeriesList.Count;
                chartResult.ChartArea.AxisX.Visible = true;
                chartResult.ChartArea.AxisX.TickLabels = C1.Win.C1Chart.TickLabelsEnum.NextToAxis;

                for (int i = 0; i < nSeriesCount1; i++)
                {
                    C1.Win.C1Chart.ChartDataSeries series = chartResult.ChartGroups.Group0.ChartData.SeriesList[i];
                    for (int j = 0; j < series.PointData.Length; j++)
                    {
                        C1.Win.C1Chart.Label label = chartResult.ChartLabels.LabelsCollection.AddNewLabel();
                        label.Name = "labelName";
                        label.Style.BackColor = Color.Transparent;
                        if (rdPie.Checked)
                            label.Compass = C1.Win.C1Chart.LabelCompassEnum.Radial;
                        else
                            label.Compass = C1.Win.C1Chart.LabelCompassEnum.North;
                        label.Offset = 10;
                        label.Connected = false;
                        label.AttachMethod = C1.Win.C1Chart.AttachMethodEnum.DataIndex;
                        label.AttachMethodData.GroupIndex = 0;
                        label.AttachMethodData.SeriesIndex = i;
                        label.AttachMethodData.PointIndex = j;
                        label.Text = Convert.ToString(series.Y[j]);
                        label.Visible = true;
                        label.Style.Font = new Font("Courier New", 12);
                    }
                }

                CommonMisc.DecideChartClusterWidth(ref chartResult);
                CommonMisc.SetXLabelsVert(ref chartResult);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private Image GetHighResMetafile(C1.Win.C1Chart.C1Chart chart)
        {
            try
            {
                Metafile meta = null;

                // get high-res reference dc
                using (PrintDocument doc = new PrintDocument())
                using (Graphics gref = doc.PrinterSettings.CreateMeasurementGraphics())
                {
                    // create metafile
                    IntPtr hdc = gref.GetHdc();
                    meta = new Metafile(hdc, EmfType.EmfOnly, "test");

                    // draw chart into metafile
                    using (Graphics g = Graphics.FromImage(meta))
                        chart.Draw(g, new Rectangle(Point.Empty, chart.Size));
                    gref.ReleaseHdc(hdc);
                }

                // done
                return meta;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
                return null;
            }
        }

        #endregion

        #region Event Methods

        private void FrmChartSaleCount_Load(object sender, EventArgs e)
        {
            try
            {
                dtStartDate.Value = DateTime.Now.AddMonths(-1).AddDays(1);
                dtEndDate.Value = DateTime.Now;

                rdLine.Checked = true;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }
        
        private void FrmChartSaleCount_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmChartSaleCount = null;
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        void FrmChartSaleCount_Activated(object sender, EventArgs e)
        {
            try
            {
                DrawChart();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void rdPie_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdPie.Checked == true)
                    DrawChart();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void rdLine_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdLine.Checked == true)
                    DrawChart();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void rdBar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdBar.Checked == true)
                    DrawChart();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                DrawChart();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "PDF files (*.pdf)|*.pdf";
                dlg.Title = "销售顾问的销量排名统计";
                dlg.FileName = "销售顾问的销量排名统计.pdf";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                String strFilePath = dlg.FileName;

                pdfDoc.Clear();
                RectangleF rcPage = pdfDoc.PageRectangle;

                rcPage.Inflate(-72, -72);

                Font font = new Font("simsun", 16, FontStyle.Bold);
                string text = "销售顾问的销量排名统计";
                RectangleF rc = rcPage;
                rc.Y = rcPage.Y - 24;
                pdfDoc.DrawString(text, font, Brushes.DarkGray, rc);

                pdfDoc.DrawImage(GetHighResMetafile(chartResult), rcPage);
                pdfDoc.DrawRectangle(Pens.SteelBlue, rcPage);

                if (strFilePath.Length >= 0)
                {
                    pdfDoc.Save(strFilePath);
                    Process.Start(strFilePath);
                }
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.Message);
            }            
        }

        

        #endregion
    }
}
