using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;
using WinApp.Code.FormLayout;

namespace WinApp.Gadget
{
    public partial class ucTotalStats : UserControl
    {
        private object[] currentParameters = null;
        private string battleMode { get; set; }
        private GadgetHelper.TimeRangeEnum _battleTimeSpan { get; set; }
        private int gridColums { get; set; }
        private string[] gridHeaders { get; set; }
        private int fixedParams = 5; // Number of fixed parameters before grid rows params starts

        public ucTotalStats(object[] totalStatsParam)
        {
            InitializeComponent();
            currentParameters = totalStatsParam;
            // Get battle mode
            battleMode = (string)currentParameters[0];
            // Get timespan
            _battleTimeSpan = GadgetHelper.GetTimeItemFromName((string)currentParameters[1]).TimeRange;
            // Get number of grids
            gridColums = (int)currentParameters[2];
            // Get col headers
            string headerList = currentParameters[3].ToString();
            gridHeaders = headerList.Split(new string[] { ";" }, StringSplitOptions.None);
            // Get gadget header
            lblHeader.Text = (string)currentParameters[4];
        }

        private void ucTotalStats_Load(object sender, EventArgs e)
		{
            // Greate grid
            GridHelper.StyleGadgetDataGrid(dataGrid, DataGridViewSelectionMode.CellSelect);
            dataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Transparent;
            dataGrid.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 0, 0, 6);
            dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font(dataGrid.DefaultCellStyle.Font.FontFamily, 9);
            // show correct timespan button as selected
            SelectTimeRangeButton();
        }

        public void DataBind()
        {
            GetGridData();
            ReziseNow();
        }

        private class DataGridDataClass
        {
            public DataGridViewCell cellName { get; set; }
            public DataGridViewCell cellValue { get; set; }
            public DataGridViewCell cellTrend { get; set; }
            public string columnSelectionID { get; set; }
        }

        private static List<DataGridDataClass> dataGridData = null;

        private void GetGridData()
        {
            // Clear Grid
            ClearSelectedColumnsDataGrid();
            // Prepare data fetch
            dataGridData = new List<DataGridDataClass>();
            // Check if any data rows
            if (currentParameters.Length > fixedParams)
            {
                // Loop through each row
                for (int i = fixedParams; i < currentParameters.Length; i++)
                {
                    // Check if data exists
                    if (currentParameters[i] != null)
                    {
                        // Get parameter data as splitted string array
                        string row = currentParameters[i].ToString();
                        string[] rowItems = row.Split(new string[] { ";" }, StringSplitOptions.None);
                        // Add row to datagrid
                        int rowId = dataGrid.Rows.Add();
                        DataGridViewRow dgvr = dataGrid.Rows[rowId];
                        // Add values for each columd for the new row
                        for (int sectionCol = 0; sectionCol < gridColums; sectionCol++)
                        {
                            dgvr.Cells["Data" + sectionCol].Value = rowItems[(sectionCol * 2) + 1];
                            DataGridViewCell cellTrendLookup = null;
                            if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total)
                                cellTrendLookup = dgvr.Cells["Trend" + sectionCol];
                            dataGridData.Add(new DataGridDataClass() {
                                cellName = dgvr.Cells["Data" + sectionCol],
                                cellValue = dgvr.Cells["Value" + sectionCol],
                                cellTrend = cellTrendLookup,
                                columnSelectionID = rowItems[(sectionCol * 2)] 
                            });

                            //dgvr.Cells["Value" + sectionCol].Value = rowItems[(sectionCol * 2)];
                            //if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total)
                            //    dgvr.Cells["Trend" + sectionCol].Value = 0;
                        }
                    }
                }
            }
            dataGrid.ClearSelection();
            GetData();
        }

        private void GetData() //;object sender, DoWorkEventArgs e)
        {
            // Get Columns for data lookup
            string sql =
                "SELECT id, name, colNameSum, description, colDataType " +
                "FROM columnSelection " +
                "WHERE colType=1 AND colNameSum IS NOT NULL " +
                "ORDER BY id";
            DataTable dtColumnSelection = DB.FetchData(sql);
            string sqlSelect = "";
            // Loop to build select and add tooltip
            foreach (DataGridDataClass item in dataGridData)
            {
                if (item.columnSelectionID != "")
                {
                    DataRow[] drGet = dtColumnSelection.Select("id = " + item.columnSelectionID);
                    if (drGet.Length > 0)
                    {
                        DataRow dr = drGet[0];
                        item.cellName.ToolTipText = dr["description"].ToString();
                        if (dr["colDataType"].ToString() == "Float"
                            || dr["name"].ToString() == "Tier"
                            || dr["name"].ToString() == "WN8"
                            || dr["name"].ToString() == "WN7"
                            || dr["name"].ToString() == "EFF"
                            )
                        {
                            item.cellValue.Style.Format = "N2";
                        }
                        // build select to get data
                        sqlSelect += dr["colNameSum"].ToString() + " AS COL" + item.columnSelectionID + ", ";
                    }
                }
            }
            // Get total data now if any values found
            if (sqlSelect.Length > 2)
            {
                sqlSelect = sqlSelect.Substring(0, sqlSelect.Length - 2);
                if (battleMode == "")
                {
                    sql =
                        "SELECT " + sqlSelect + " " +
                        "FROM    playerTank INNER JOIN " +
                        "        tank ON playerTank.tankId = tank.id LEFT OUTER JOIN " +
                        "        playerTankBattleTotalsView as playerTankBattle ON playerTank.id = playerTankBattle.playerTankId " +
                        "WHERE        (playerTank.playerId = @playerId) ";
                }
                else
                {
                    sql =
                        "SELECT " + sqlSelect + " " +
                        "FROM    playerTank INNER JOIN " +
                        "        tank ON playerTank.tankId = tank.id INNER JOIN " +
                        "        tankType ON tank.tankTypeId = tankType.id LEFT OUTER JOIN " +
                        "        playerTankBattle ON playerTank.id = playerTankBattle.playerTankId  " +
                        "WHERE        (playerTank.playerId = @playerId) AND playerTankBattle.battleMode = @battleMode ";
                }
                DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
                DataTable dtTotalStats = DB.FetchData(sql);
                DataRow drTotalStats = dtTotalStats.Rows[0];
                // Loop to add total values
                foreach (DataGridDataClass item in dataGridData)
                {
                    if (item.columnSelectionID != "")
                    {
                        try
                        {
                            // Get cell value from sql
                            string cellSQLFieldName = "COL" + item.columnSelectionID;
                            if (drTotalStats[cellSQLFieldName] != DBNull.Value)
                            {
                                double val = Convert.ToDouble(drTotalStats[cellSQLFieldName]);
                                if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total && val > 999999)
                                {
                                    if (val > 999999999)
                                        item.cellValue.Value = (val / 1000000).ToString("# ### ###") + " M";
                                    else 
                                        item.cellValue.Value = (val / 1000000).ToString("# ### ###.##0") + " M";
                                    item.cellValue.ToolTipText = val.ToString("N0");
                                }
                                else
                                {
                                    item.cellValue.Value = val;
                                }
                            }
                            // Get cell name for special operations
                            string cellName = item.cellName.Value.ToString().Trim();
                            switch (cellName)
                            {
                                case "Battles":
                                    item.cellValue.Style.ForeColor = ColorRangeScheme.BattleCountColor(Convert.ToInt32(item.cellValue.Value));
                                    break;
                                case "Win Rate":
                                    item.cellValue.Style.ForeColor = ColorRangeScheme.WinRateColor(Convert.ToInt32(item.cellValue.Value));
                                    break;
                                case "WN8":
                                    item.cellValue.Value = Code.Rating.CalculatePlayerTotalWN8(battleMode);
                                    item.cellValue.Style.ForeColor = ColorRangeScheme.WN8color(Convert.ToInt32(item.cellValue.Value));
                                    break;
                                case "WN7":
                                    item.cellValue.Value = Code.Rating.CalcTotalWN7(battleMode);
                                    item.cellValue.Style.ForeColor = ColorRangeScheme.WN7color(Convert.ToInt32(item.cellValue.Value));
                                    break;
                                case "EFF":
                                    item.cellValue.Value = Code.Rating.CalcTotalEFF(battleMode);
                                    item.cellValue.Style.ForeColor = ColorRangeScheme.EffColor(Convert.ToInt32(item.cellValue.Value));
                                    break;

                            }
                        }
                        catch (Exception)
                        {
                            // throw;
                        }
                        
                    }
                }
            }
        }

        private void ClearSelectedColumnsDataGrid()
        {
            // Add columns to datagrid
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();
            for (int i = 0; i < gridColums; i++)
            {
                dataGrid.Columns.Add("Data" + i.ToString(), gridHeaders[i]);
                dataGrid.Columns.Add("Value" + i.ToString(), "Value");
                if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total)
                    dataGrid.Columns.Add("Trend" + i.ToString(), "Trend");
                dataGrid.Columns.Add("Separator" + i.ToString(), "");
            }
            // No sorting and format
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                dataGrid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGrid.Columns[i].Resizable = DataGridViewTriState.False;
            }
            // Format
            for (int i = 0; i < gridColums; i++)
            {
                dataGrid.Columns["Value" + i.ToString()].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGrid.Columns["Value" + i.ToString()].DefaultCellStyle.Format = "N0";
                dataGrid.Columns["Value" + i.ToString()].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total)
                {
                    dataGrid.Columns["Trend" + i.ToString()].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGrid.Columns["Trend" + i.ToString()].DefaultCellStyle.Format = "N0";
                }
                dataGrid.Columns["Separator" + i.ToString()].HeaderCell.Style.BackColor = Color.Transparent;
            }
        }

        private void ReziseNow()
        {
            if (dataGrid.ColumnCount > 0)
            {
                int step = 3;
                int col0Width = 120; // Data
                int col1Width = 80; // Value
                int col2Width = 0; // Trend
                if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total)
                {
                    col0Width = 100; // Data
                    col1Width = 60; // Value
                    col2Width = 40; // Trend
                    step = 4;
                }
                float colUsageWidth = (col0Width + col1Width + col2Width) * gridColums;
                float restSpaceWidth = dataGrid.Width - colUsageWidth;
                int col3Width = 0;
                if (gridColums > 1) // Separator
                    col3Width = Convert.ToInt32(restSpaceWidth / (gridColums - 1)); 
                if (col3Width < 5)
                    col3Width = 5;
                for (int i = 0; i < gridColums * step; i = i + step)
                {
                    dataGrid.Columns[i + 0].Width = col0Width; // Data
                    dataGrid.Columns[i + 1].Width = col1Width; // Value
                    if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total)
                    {
                        dataGrid.Columns[i + 2].Width = col2Width; // Trend
                        dataGrid.Columns[i + 3].Width = col3Width; // Separator
                    }
                    else
                        dataGrid.Columns[i + 2].Width = col3Width; // Separator
                }
                // buttons in footer
                int middleButtonX = (panelFooter.Width / 2) - (btnMonth.Width / 2);
                int distance = btnMonth.Width + 5;
                btnTotal.Left = middleButtonX - (distance * 2);
                btnMonth3.Left = middleButtonX - distance;
                btnMonth.Left = middleButtonX;
                btnWeek.Left = middleButtonX + distance;
                btnToday.Left = middleButtonX + (distance * 2);
                // Hide labels if small
                bool showLabels = (panelFooter.Width > (distance * 5) + 150);
                lblBattleMode.Visible = showLabels;
                lblTotalStats.Visible = showLabels;
                lblTotalStats.Width = btnTotal.Left - 5;
                lblBattleMode.Left = btnToday.Left + distance - 1;
                lblBattleMode.Width = panelFooter.Width - btnToday.Right - 4;
                ShowFooterText();
            }
        }

        private void ShowFooterText()
        {
            int distance = btnMonth.Width + 5;
            if (panelFooter.Width < (distance * 5) + 250)
            {
                lblBattleMode.Text = BattleMode.GetItemFromSqlName(battleMode).Name;
                lblTotalStats.Text = "Total Statistics";
            }
            else
            {
                lblBattleMode.Text = "Battle Mode: " + BattleMode.GetItemFromSqlName(battleMode).Name;
                lblTotalStats.Text = "Total Statistics" + footerTimespanText;
            }
        }

        private void ucTotalStats_Paint(object sender, PaintEventArgs e)
        {
            if (BackColor == ColorTheme.FormBackSelectedGadget)
                GadgetHelper.DrawBorderOnGadget(sender, e);
        }

        private void ucTotalStats_Resize(object sender, EventArgs e)
        {
            ReziseNow();
        }

        private void btnTimeSpan_Click(object sender, EventArgs e)
        {
            BadButton b = (BadButton)sender;
            switch (b.Name)
            {
                case "btnTotal": _battleTimeSpan = GadgetHelper.TimeRangeEnum.Total; break;
                case "btnMonth3": _battleTimeSpan = GadgetHelper.TimeRangeEnum.TimeMonth3; break;
                case "btnMonth": _battleTimeSpan = GadgetHelper.TimeRangeEnum.TimeMonth; break;
                case "btnWeek": _battleTimeSpan = GadgetHelper.TimeRangeEnum.TimeWeek; break;
                case "btnToday": _battleTimeSpan = GadgetHelper.TimeRangeEnum.TimeToday; break;
            }
            SelectTimeRangeButton();
            DataBind();
        }

        string footerTimespanText = "";
        private void SelectTimeRangeButton()
        {
            btnTotal.Checked = false;
            btnMonth3.Checked = false;
            btnMonth.Checked = false;
            btnWeek.Checked = false;
            btnToday.Checked = false;
            switch (_battleTimeSpan)
            {
                case GadgetHelper.TimeRangeEnum.Total: 
                    btnTotal.Checked = true;
                    footerTimespanText = "";
                    break;
                case GadgetHelper.TimeRangeEnum.TimeMonth3: 
                    btnMonth3.Checked = true;
                    footerTimespanText = " - Trend 3 Months";
                    break;
                case GadgetHelper.TimeRangeEnum.TimeMonth: 
                    btnMonth.Checked = true;
                    footerTimespanText = " - Trend Last Month";
                    break;
                case GadgetHelper.TimeRangeEnum.TimeWeek: 
                    btnWeek.Checked = true;
                    footerTimespanText = " - Trend Last Week";
                    break;
                case GadgetHelper.TimeRangeEnum.TimeToday: 
                    btnToday.Checked = true;
                    footerTimespanText = " - Trend Today";
                    break;
            }
            ShowFooterText();
        }

        private void dataGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Border under headers
            if (e.RowIndex == -1 && e.ColumnIndex > -1 )
            {
                // Get header text
                string header = dataGrid.Columns[e.ColumnIndex].HeaderText;
                // Erase the cell.
                Brush brushBack = new SolidBrush(dataGrid.DefaultCellStyle.BackColor);
                e.Graphics.FillRectangle(brushBack, e.CellBounds);
                // Draw Line if not separator
                if (header != "")
                {
                    Pen p = new Pen(ColorTheme.ControlDarkFont);
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    e.Graphics.DrawLine(p, e.CellBounds.Left, e.CellBounds.Bottom - 3, e.CellBounds.Right - 1, e.CellBounds.Bottom - 3);
                }
                // Draw the text content of the cell, ignoring alignment.
                if (e.Value != null)
                {
                    // right align for some cols
                    int moveToRightAlign = 0;
                    if (header == "Value" || header == "Trend")
                    {
                        // Measure string.
                        SizeF stringSize = new SizeF();
                        stringSize = e.Graphics.MeasureString((String)e.Value, e.CellStyle.Font);
                        // Calc move pixels to right align text
                        moveToRightAlign = e.CellBounds.Width - Convert.ToInt32(stringSize.Width);
                    }
                    Brush brushFore = new SolidBrush(ColorTheme.ControlFont);
                    e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, brushFore, e.CellBounds.X + moveToRightAlign, e.CellBounds.Y + 2, StringFormat.GenericDefault);
                }
                e.Handled = true;
            }
        }


    }
}
