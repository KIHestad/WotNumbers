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
            public bool trendExists { get; set; }
            public bool trendCalc { get; set; }
            public bool trendReversePos { get; set; }
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
                "SELECT id, name, colNameSum, colNameBattleSum, colNameBattleSumCalc, colNameBattleSumTank, colNameBattleSumReversePos, description, colDataType " +
                "FROM columnSelection " +
                "WHERE colType=1 AND colNameSum IS NOT NULL " +
                "ORDER BY id";
            DataTable dtColumnSelection = DB.FetchData(sql);
            string sqlSelectValue = ""; // The SQL to fetch calculated total stats value
            string sqlSelectTrend = ""; // The SQL to fetch sum battle result for timespan to use for show in trend or use in trend calculation
            string sqlSelectTotal = ""; // The SQL to fetch total result to use in in trend calculation
            // Loop to build select and add tooltip
            foreach (DataGridDataClass item in dataGridData)
            {
                if (item.columnSelectionID != "")
                {
                    DataRow[] drGet = dtColumnSelection.Select("id = " + item.columnSelectionID);
                    if (drGet.Length > 0)
                    {
                        // build select to get value data
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
                        sqlSelectValue += dr["colNameSum"].ToString() + " AS COL" + item.columnSelectionID + ", ";
                        // build select to get trend data
                        item.trendExists = false;
                        if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total &&  dr["colNameBattleSum"] != DBNull.Value)
                        {
                            item.trendExists = true;
                            item.trendCalc = Convert.ToBoolean(dr["colNameBattleSumCalc"]);
                            item.trendReversePos = Convert.ToBoolean(dr["colNameBattleSumReversePos"]);
                            sqlSelectTrend += dr["colNameBattleSum"].ToString() + " AS COL" + item.columnSelectionID + ", ";
                            if (item.trendCalc)
                                sqlSelectTotal += dr["colNameBattleSumTank"].ToString() + " AS COL" + item.columnSelectionID + ", ";
                        }
                    }
                }
            }
            // Get total data now if any values found
            if (sqlSelectValue.Length > 2)
            {
                sqlSelectValue = sqlSelectValue.Substring(0, sqlSelectValue.Length - 2);
                if (battleMode == "")
                {
                    sql =
                        "SELECT " + sqlSelectValue + " " +
                        "FROM    playerTank INNER JOIN " +
                        "        tank ON playerTank.tankId = tank.id LEFT OUTER JOIN " +
                        "        playerTankBattleTotalsView as playerTankBattle ON playerTank.id = playerTankBattle.playerTankId " +
                        "WHERE        (playerTank.playerId = @playerId) ";
                }
                else
                {
                    sql =
                        "SELECT " + sqlSelectValue + " " +
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
                DataRow drTotalStatsTrend = null;
                DataRow drTotalStatsTank = null;
                // Get total trend now if any values found
                if (sqlSelectTrend.Length > 2)
                {
                    // SQL to get battle for timespan
                    sqlSelectTrend = sqlSelectTrend.Substring(0, sqlSelectTrend.Length - 2);
                    sql =
                        "SELECT SUM(battle.battlesCount) as battlesCount, " + sqlSelectTrend + " " +
                        "FROM    battle INNER JOIN " +
                        "        playerTank ON battle.playerTankId = playerTank.Id INNER JOIN " +
                        "        tank ON playerTank.tankId = tank.id " +
                        "WHERE   (playerTank.playerId = @playerId) ";
                    DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
                    // filter battlemode
                    if (battleMode != "")
                    {
                        sql += "AND battleMode = @battleMode ";
                        DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
                    }
                    // filter timespan
                    DateTime dateFilter = DateTimeHelper.GetTodayDateTimeStart();
                    switch (_battleTimeSpan)
                    {
                        case GadgetHelper.TimeRangeEnum.TimeWeek:
                            sql += " AND battleTime>=@battleTime ";
                            dateFilter = dateFilter.AddDays(-7);
                            break;
                        case GadgetHelper.TimeRangeEnum.TimeMonth:
                            sql += " AND battleTime>=@battleTime ";
                            dateFilter = dateFilter.AddMonths(-1);
                            break;
                        case GadgetHelper.TimeRangeEnum.TimeMonth3:
                            sql += " AND battleTime>=@battleTime ";
                            dateFilter = dateFilter.AddMonths(-3);
                            break;
                        case GadgetHelper.TimeRangeEnum.TimeToday:
                            sql += " AND battleTime>=@battleTime ";
                            break;
                    }
                    DB.AddWithValue(ref sql, "@battleTime", dateFilter, DB.SqlDataType.DateTime);
                    // Get trend data
                    DataTable dtTotalStatsTrend = DB.FetchData(sql);
                    drTotalStatsTrend = dtTotalStatsTrend.Rows[0];
                    // SQL to get total tanks stats
                    if (sqlSelectTotal.Length > 2)
                    {
                        sqlSelectTotal = sqlSelectTotal.Substring(0, sqlSelectTotal.Length - 2);
                        string sqlTotals = "";
                        if (battleMode == "")
                        {
                            sqlTotals =
                                "SELECT SUM(playerTankBattle.battles) as battlesCount, " + sqlSelectTotal + " " +
                                "FROM    playerTank INNER JOIN " +
                                "        tank ON playerTank.tankId = tank.id LEFT OUTER JOIN " +
                                "        playerTankBattleTotalsView as playerTankBattle ON playerTank.id = playerTankBattle.playerTankId " +
                                "WHERE        (playerTank.playerId = @playerId) ";
                        }
                        else
                        {
                            sqlTotals =
                                "SELECT SUM(playerTankBattle.battles) as battlesCount, " + sqlSelectTotal + " " +
                                "FROM    playerTank INNER JOIN " +
                                "        tank ON playerTank.tankId = tank.id INNER JOIN " +
                                "        tankType ON tank.tankTypeId = tankType.id LEFT OUTER JOIN " +
                                "        playerTankBattle ON playerTank.id = playerTankBattle.playerTankId  " +
                                "WHERE        (playerTank.playerId = @playerId) AND playerTankBattle.battleMode = @battleMode ";
                        }
                        DB.AddWithValue(ref sqlTotals, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
                        DB.AddWithValue(ref sqlTotals, "@battleMode", battleMode, DB.SqlDataType.VarChar);
                        // Get Totals data
                        DataTable dtTotalStatsTank = DB.FetchData(sqlTotals);
                        drTotalStatsTank = dtTotalStatsTank.Rows[0];
                    }
                }
                // Loop to add values to grid
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
                                // Add total value to grid
                                double val = Convert.ToDouble(drTotalStats[cellSQLFieldName]);
                                if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total && val > 999999)
                                {
                                    if (val > 999999999)
                                        item.cellValue.Value = (val / 1000000).ToString("# ### ###").Trim() + " M";
                                    else 
                                        item.cellValue.Value = (val / 1000000).ToString("# ### ###.##0").Trim() + " M";
                                    item.cellValue.ToolTipText = val.ToString("N0");
                                }
                                else
                                {
                                    item.cellValue.Value = val;
                                }
                                // Add trend to grid
                                if (item.trendExists)
                                {
                                    double trendValue = Convert.ToDouble(drTotalStatsTrend[cellSQLFieldName]);
                                    if (item.trendCalc)
                                    {
                                        int trendBattlesCount = Convert.ToInt32(drTotalStatsTrend["battlesCount"]);
                                        double totalValue = Convert.ToDouble(drTotalStatsTank[cellSQLFieldName]);
                                        int totalBattlesCount = Convert.ToInt32(drTotalStatsTank["battlesCount"]);
                                        if (totalBattlesCount - trendBattlesCount == 0)
                                        {
                                            trendValue = 0;
                                        }
                                        else
                                        {
                                            double prevValue = ((totalValue - trendValue) / (totalBattlesCount - trendBattlesCount));
                                            trendValue = val - prevValue;
                                        }
                                    }
                                    // Format number
                                    if (trendValue > 9999)
                                    {
                                        if (trendValue > 9999999)
                                            item.cellTrend.Value = (trendValue / 1000000).ToString("# ### ###").Trim() + " M";
                                        else
                                            item.cellTrend.Value = (trendValue / 1000).ToString("# ### ###").Trim() + " K";
                                    }
                                    else
                                    {
                                        item.cellTrend.Value = trendValue;
                                        if (trendValue - Math.Truncate(trendValue) != 0)
                                        {
                                            if (trendValue < 9)
                                                item.cellTrend.Style.Format = "N2";
                                            else if (trendValue < 99)
                                                item.cellTrend.Style.Format = "N1";
                                        }
                                    }
                                    // Trend colors and tooltip
                                    string trendToolTipText = "";
                                    if (trendValue >= 0.01)
                                    {
                                        if (!item.trendReversePos)
                                        {
                                            item.cellTrend.Style.ForeColor = ColorTheme.Rating_4_green;
                                            trendToolTipText = "Positive Trend" + Environment.NewLine;
                                        }
                                        else
                                        {
                                            item.cellTrend.Style.ForeColor = ColorTheme.Rating_1_red;
                                            trendToolTipText = "Negative Trend" + Environment.NewLine;
                                        }
                                    }
                                    else if (trendValue > 0)
                                    {
                                        if (!item.trendReversePos)
                                        {
                                            item.cellTrend.Style.ForeColor = ColorTheme.Rating_4_greenLight;
                                            item.cellTrend.Value = "pos";
                                            trendToolTipText = "Minor Positive Trend" + Environment.NewLine;
                                        }
                                        else
                                        {
                                            item.cellTrend.Style.ForeColor = ColorTheme.Rating_2_orange;
                                            item.cellTrend.Value = "neg";
                                            trendToolTipText = "Minor Negative Trend" + Environment.NewLine;
                                        }
                                    }
                                    else if (trendValue <= -0.01)
                                    {
                                        if (!item.trendReversePos)
                                        {
                                            item.cellTrend.Style.ForeColor = ColorTheme.Rating_1_red;
                                            trendToolTipText = "Negative Trend" + Environment.NewLine;
                                        }
                                        else
                                        {
                                            item.cellTrend.Style.ForeColor = ColorTheme.Rating_4_green;
                                            trendToolTipText = "Positive Trend" + Environment.NewLine;
                                        }
                                    }
                                    else if (trendValue < 0)
                                    {
                                        if (!item.trendReversePos)
                                        {
                                            item.cellTrend.Style.ForeColor = ColorTheme.Rating_2_orange;
                                            item.cellTrend.Value = "neg";
                                            trendToolTipText = "Minor Negative Trend" + Environment.NewLine;
                                        }
                                        else
                                        {
                                            item.cellTrend.Style.ForeColor = ColorTheme.Rating_4_greenLight;
                                            item.cellTrend.Value = "pos";
                                            trendToolTipText = "Minor Positive Trend" + Environment.NewLine;
                                        }
                                    }
                                    else
                                    {
                                        item.cellTrend.Style.ForeColor = ColorTheme.Rating_3_yellow;
                                        if (item.trendCalc)
                                            item.cellTrend.Value = "nul";
                                        trendToolTipText = "No change" + Environment.NewLine;
                                    }
                                    item.cellTrend.ToolTipText = trendToolTipText + trendValue.ToString();
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
            // Colors
            Brush brushBack = new SolidBrush(dataGrid.DefaultCellStyle.BackColor);
            // Border under headers
            if (e.RowIndex == -1 && e.ColumnIndex > -1 )
            {
                // Get header text
                string header = dataGrid.Columns[e.ColumnIndex].HeaderText;
                // Erase the cell.
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
            else if (e.ColumnIndex > -1)
            {
                if (e.Value != null)
                {
                    if (e.Value.ToString() == "neg")
                    {
                        // Erase the cell.
                        e.Graphics.FillRectangle(brushBack, e.CellBounds);
                        Brush b = new SolidBrush(ColorTheme.Rating_2_orange);
                        List<Point> points = new List<Point>();
                        int x = e.CellBounds.X;
                        int y = e.CellBounds.Y;
                        points.Add(new Point(x + 18, y + 6));
                        points.Add(new Point(x + 24, y + 16));
                        points.Add(new Point(x + 30, y + 6));
                        e.Graphics.FillPolygon(b, points.ToArray());
                        e.Handled = true;
                    }
                    else if (e.Value.ToString() == "pos")
                    {
                        // Erase the cell.
                        e.Graphics.FillRectangle(brushBack, e.CellBounds);
                        Brush b = new SolidBrush(ColorTheme.Rating_4_greenLight);
                        List<Point> points = new List<Point>();
                        int x = e.CellBounds.X;
                        int y = e.CellBounds.Y;
                        points.Add(new Point(x + 17, y + 16));
                        points.Add(new Point(x + 24, y + 5));
                        points.Add(new Point(x + 31, y + 16));
                        e.Graphics.FillPolygon(b, points.ToArray());
                        e.Handled = true;
                    }
                    else if (e.Value.ToString() == "nul")
                    {
                        // Erase the cell.
                        e.Graphics.FillRectangle(brushBack, e.CellBounds);
                        Brush b = new SolidBrush(ColorTheme.Rating_3_yellow);
                        int x = e.CellBounds.X;
                        int y = e.CellBounds.Y;
                        e.Graphics.FillRectangle(b, (x + 25), y + 5, 12, 12);
                        e.Handled = true;
                    }
                }
            }
        }


    }
}
