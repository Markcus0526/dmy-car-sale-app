using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.C1Excel;

namespace CarSaleMan
{
    public partial class FrmImportExcel : Form
    {
        #region Fields and Properties

        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        public FrmImportExcel()
        {
            InitializeComponent();

            // init private variables
            // events
        }

        #endregion

        #region Private Methods

        private void ImportFromExcel()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.DefaultExt = "xls";
                dlg.FileName = "*.xls";
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }

                tblOnroadTableAdapter.Fill(cmsDB.tbl_onroad);
                
                xlsRoadCar.Clear();
                xlsRoadCar.Sheets.Clear();

                xlsRoadCar.Load(dlg.FileName);

                bool captionText = true;
                int rowIdx = 1;

                gridImportCar.Rows.Count = 1;

                foreach (XLSheet sheet in xlsRoadCar.Sheets)
                {
                    if (sheet.Columns.Count == 0 && sheet.Rows.Count == 0)
                        break;

                    int frows = gridImportCar.Rows.Fixed;
                    int fcols = gridImportCar.Cols.Fixed;
                    
                    
                    gridImportCar.Cols.Count = sheet.Columns.Count + fcols;

                    // load cells
                    for (int r = 0; r < sheet.Rows.Count; r++)
                    {
                        if (captionText == true)
                        {
                            for (int c = 0; c < sheet.Columns.Count; c++)
                            {
                                // get cell
                                XLCell cell = sheet.GetCell(0, c);
                                if (cell == null) continue;

                                // apply content
                                gridImportCar[0, c + fcols] = cell.Value;

                            }

                            captionText = false;
                        }
                        else
                        {
                            if (r == 0)
                                continue;

                            String vin = "";

                            if (sheet.GetCell(r, 6) != null && sheet.GetCell(r, 6).Value != null)
                                vin = sheet.GetCell(r, 6).Value.ToString();

                            String selectQuery = "vin = '" + vin + "'";
                            DataRow[] rows = cmsDB.tbl_onroad.Select(selectQuery);

                            if (rows.Length == 0)
                            {
                                gridImportCar.Rows.Count++;

                                gridImportCar[rowIdx, 0] = rowIdx;

                                for (int c = 0; c < sheet.Columns.Count; c++)
                                {
                                    if (sheet.GetCell(r, c) == null)
                                        gridImportCar[rowIdx, c + fcols] = "";
                                    else
                                    {
                                        if (sheet.GetCell(r, c).Value == null)
                                            gridImportCar[rowIdx, c + fcols] = "";
                                        else
                                            gridImportCar[rowIdx, c + fcols] = sheet.GetCell(r, c).Value.ToString();
                                    }

                                }
                                rowIdx++;
                            }
                        }
                    }
                }

                lblCount.Text = String.Format("{0:0}", gridImportCar.Rows.Count - 1);

                gridImportCar.AutoSizeCols();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void SaveCartypeTable()
        {
            try
            {
                tblCartypeTableAdapter.Fill(cmsDB.tbl_cartype);
                tblCarseriesTableAdapter.Fill(cmsDB.tbl_basedata);
                List<String> seriesList = new List<String>();
                for (int i = 0; i < tblCarseriesBindingSource.Count; i++)
                {
                    seriesList.Add(((DataRowView)(tblCarseriesBindingSource[i])).Row["value"].ToString());
                }

                for (int i = 1; i < gridImportCar.Rows.Count; i++)
                {
                    String selectQuery = "carcode = '" + gridImportCar[i, 9].ToString() + "' AND subsets = '" + gridImportCar[i, 17].ToString() + "' AND insidesetcode = '" + gridImportCar[i, 15].ToString() + "'";
                    DataRow[] rows = cmsDB.tbl_cartype.Select(selectQuery);

                    if (rows.Length == 0)
                    {
                        DataRow row = cmsDB.tbl_cartype.NewRow();

                        row["carseries"] = "";
                        String carname = gridImportCar[i, 10].ToString();
                        for (int j = 0; j < seriesList.Count; j++)
                        {
                            if (carname.Contains(seriesList[j]) == true)
                            {
                                row["carseries"] = seriesList[j];
                                break;
                            }
                        }
                            
                        row["carcode"] = gridImportCar[i, 9].ToString();
                        row["carname"] = gridImportCar[i, 10].ToString();
                        row["eop"] = "·ñ";
                        row["subsets"] = gridImportCar[i, 17].ToString();
                        row["insidesetcode"] = gridImportCar[i, 15].ToString();
                        row["insidesetname"] = gridImportCar[i, 16].ToString();
                        row["inprice"] = 0;
                        row["outprice"] = 0;
                        row["otherprice1"] = 0;
                        row["otherprice2"] = 0;
                        row["otherprice3"] = 0;
                        row["otherprice4"] = 0;
                        row["propval"] = 0;
                        row["profitval"] = 0;
                        row["outstoreprice"] = 0;
                        row["vinprefix"] = gridImportCar[i, 7].ToString();
                        row["enginenoprefix"] = gridImportCar[i, 8].ToString();
                        row["deleted"] = 0;

                        cmsDB.tbl_cartype.Rows.Add(row);
                    }
                }

                tblCartypeTableAdapter.Update(cmsDB.tbl_cartype);
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion

        #region Event Methods

        private void FrmImportExcel_Load(object sender, EventArgs e)
        {
            try
            {
                ImportFromExcel();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveCartypeTable();
            }
            catch (System.Exception ex)
            {
                CommonMisc.LogErrors(ex.ToString());
            }
        }

        #endregion
    }
}