using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WinApp.Code;
using WinApp.Code.FormView;
using WinApp.Code.Rating;

namespace WinApp.Forms
{
	public partial class BattleChart : FormCloseOnEsc
    {
		public enum CalculationType
		{
			standard = 0,
			firstInPercentageOfNext = 1,
			eff = 2,
			wn7 = 3,
			wn8 = 4,
            firstDividedOnNext = 5,
            wn9 = 6,
		}

		private class ChartTypeCols
		{
			public string playerTankValCol = "";    // Column holding current value from playerTankBattle table
            public string battleValCol = "";        // Column holding battle value
            public string battleFirstValCol = "";   // Column holding the first battle value, normally same as battleValCol - exept for some special calculations
		}

		private class ChartType
		{
			public string name = "";									// Name of Chart VAlue in dropdown 
			public CalculationType calcType = CalculationType.standard;	// Calculation Type
			public bool totals = true;									// Show totals, not actual battle values
			public List<ChartTypeCols> col;							    // What columns to be used to calculate values
            public SeriesChartType chartType = SeriesChartType.Line;    // Chart type, line = standard, spline er rounded, point = no line only dot
		}

        // string ddTankList = ""; // Replaced with tank search
		string ddChartList = "";
        string ddModeList = "( All Modes ),";
        int initPlayerTankId = 0;
		int decimals = 3;
		int numPoints = 100; // Max num of points in one chart serie, exept for battle values (ChatValues.totals = false)
		private double axisYminimum = 999999999;
		private List<ChartType> chartValues = new List<ChartType>(); // List of all available chart types 
        private List<BattleMode.Item> mainBattleModes = BattleMode.GetMainBattleModes();

		public BattleChart(int playerTankId = 0)
		{
			InitializeComponent();
			initPlayerTankId = playerTankId;
            // Add close form on pressing ESC
            this.KeyPreview = true;
            this.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Escape) this.Close();
            };
        }

		// To be able to minimize from task bar
		const int WS_MINIMIZEBOX = 0x20000;
		const int CS_DBLCLKS = 0x8;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.Style |= WS_MINIMIZEBOX;
				cp.ClassStyle |= CS_DBLCLKS;
				return cp;
			}
		}

		private void BattleChart_Load(object sender, EventArgs e)
		{
			// Init showing selected tank from grid or last tank or (All Tanks) if non selected yet
			string sql = "";
			if (initPlayerTankId != 0)
			{
				sql = "select name from tank inner join playerTank on tank.id=playerTank.tankId where playerTank.id=@id order by tank.name; ";
				DB.AddWithValue(ref sql, "@id", initPlayerTankId, DB.SqlDataType.Int);
				ddTank.Text = DB.FetchData(sql).Rows[0][0].ToString();
			}
			else
			{
                if (BattleChartHelper.TankName != "")
                    ddTank.Text = BattleChartHelper.TankName;
                else
                    ddTank.Text = "( All Tanks )";
			}
			// Available charts
			AddChartValues();
			
            // DropDown Tank List replaced with tank search
            //sql = "select tank.name from tank inner join playerTank on tank.id = playerTank.tankId where playerTank.playerId=@playerId order by tank.name";
            //DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            //DataTable dt = DB.FetchData(sql);
            //ddTankList = "( All Tanks )";
            //foreach (DataRow dr in dt.Rows)
            //{
            //    ddTankList += "," + dr["name"].ToString();
            //}
			
            // DropDown Chart Values
			foreach (ChartType c in chartValues)
			{
				ddChartList += c.name + ",";
			}
			ddChartList = ddChartList.Substring(0, ddChartList.Length - 1); // Remove last comma
            // DropDown Modes
            foreach (BattleMode.Item mode in mainBattleModes)
            {
                ddModeList += mode.Name + ",";
            }
            ddModeList = ddModeList.Substring(0, ddModeList.Length - 1); // Remove last comma
            // Add latest values or default values
            ddMode.Text = BattleChartHelper.ChartMode;
            ddValue.Text = BattleChartHelper.Value;
            ddXaxis.Text = BattleChartHelper.Xaxis;
            ddPeriod.Text = BattleChartHelper.Period;
            chkBullet.Checked = BattleChartHelper.Bullet;
            chkSpline.Checked = BattleChartHelper.Spline;

			// Chart layout
			Font letterType = new Font("MS Sans Serif", 10, GraphicsUnit.Pixel);
			Color defaultColor = ColorTheme.ControlFont;
			ChartArea area = ChartingMain.ChartAreas[0];
			foreach (var axis in area.Axes)
			{
				axis.TitleForeColor = defaultColor;
				axis.LineColor = defaultColor;
				axis.InterlacedColor = defaultColor;
				axis.LabelStyle.Font = letterType;
				axis.LabelAutoFitMinFontSize = (int)letterType.Size;
				axis.LabelAutoFitMaxFontSize = (int)letterType.Size;
				//axis.MajorTickMark.Enabled = false;
			}
			area.AxisX.LabelStyle.Angle = 20;
			area.InnerPlotPosition = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(6, 0, 96, 91);
		}

        private void BattleChart_Shown(object sender, EventArgs e)
        {
            // Show chart now if all values are selected
            if (ddValue.Text != "")
                DrawChart();
        }


		private void ddPeriod_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(ddPeriod, Code.DropDownGrid.DropDownGridType.List, "( All ),1 Year,6 Month,3 Month,1 Month,2 Weeks,1 Week");
		}

		private string FilterPeriod(string where)
		{
			string newWhere = "";
			string period = ddPeriod.Text;
			if (period == "( All )")
				newWhere = where;
			else
			{
				DateTime afterDate = DateTime.Now;
				switch (period)
				{
					case "1 Year": afterDate = DateTime.Now.AddYears(-1); break;
					case "6 Month": afterDate = DateTime.Now.AddMonths(-6); break;
					case "3 Month": afterDate = DateTime.Now.AddMonths(-3); break;
					case "1 Month": afterDate = DateTime.Now.AddMonths(-1); break;
					case "2 Weeks": afterDate = DateTime.Now.AddDays(-14); break;
					case "1 Week": afterDate = DateTime.Now.AddDays(-7); break;
				}
				newWhere = " battleTime >= '" + afterDate.ToString("yyyy-MM-dd") + "' ";
				if (where == "")
					newWhere = " where " + newWhere;
				else
					newWhere = where + " and " + newWhere;

			}
			return newWhere;
		}

		private double SetYaxisLowestValue(double val)
		{
			if (val < axisYminimum)
				return val;
			else
				return axisYminimum;
		}

		private double RoundOff(double min)
		{
			if (min <= 100)
				return Convert.ToDouble(Convert.ToInt32(min * 10)) / 10;
			else if (min <= 9999)
				return Convert.ToDouble(Math.Truncate(min));
			else
			{
				return min.RoundDown(1);
			}
				
		}

		private void DrawChart()
		{
            // Remember choice for auto value at form load next time
            BattleChartHelper.TankName = ddTank.Text;
            BattleChartHelper.ChartMode = ddMode.Text;
            BattleChartHelper.Value = ddValue.Text;
            BattleChartHelper.Xaxis = ddXaxis.Text;
            BattleChartHelper.Period = ddPeriod.Text;
            BattleChartHelper.Bullet = chkBullet.Checked;
            BattleChartHelper.Spline = chkSpline.Checked;
            // Init
			string tankName = ddTank.Text;
            if (tankName == "( All Tanks )") tankName = "All Tanks";
			string selectedChartValue = ddValue.Text;
            string chartSerie = GetChartSeriesName();
			string chartOrder = "";
            // Get battle mode, set "" if unknown = "All Modes"
            BattleMode.Item mainBattleModeItem = BattleMode.GetItemFromName(ddMode.Text);
            string chartMode = "";
            if (mainBattleModeItem != null)
                chartMode = BattleMode.GetItemFromType(mainBattleModeItem.Type).SqlName;
            // Check if value selected
			if (ddValue.Text == "")
			{
				Code.MsgBox.Show("Please select a chart value, this is the data that shows up in the chart.", "Missing Chart Value", this);
				return;
			}
			// Update toolbar
			string footerTxt = "Refreshed ";
			if (btnAddChart.Text == "Add")
				footerTxt = "Added ";
			footerTxt += "chart line: " + chartSerie;
			lblFooter.Text = footerTxt;
			// Change to refresh mode
			btnAddChart.Text = "Refresh";
			// Check if already shown
			Series removeSerie = null;
			foreach (Series serie in ChartingMain.Series)
			{
				if (serie.Name == chartSerie)
					removeSerie = serie;
			}
			// Remove if shown, to add / refresh
			if (removeSerie != null)
				ChartingMain.Series.Remove(removeSerie);
			// Get Chart Value selected
			ChartType chartValue = chartValues.Find(c => c.name == selectedChartValue);
			// Add series
			Series newSerie = new Series(chartSerie);
            // Line  and marker type
            newSerie.ChartType = chartValue.chartType;
            if (chartValue.chartType == SeriesChartType.Point) // Point = only dot shown
            {
                // Default marker type for point type
                newSerie.MarkerStyle = MarkerStyle.Circle;
            }
            else // Other chart type = line type
            {
                // Override to spline type if checked
                if (chkSpline.Checked) 
                    newSerie.ChartType = SeriesChartType.Spline;
                // Set marker type
                if (chkBullet.Checked)
                    newSerie.MarkerStyle = MarkerStyle.Circle;
                else
                    newSerie.MarkerStyle = MarkerStyle.None;
            }
            if (ddXaxis.Text == "Date")
			{
				ChartingMain.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Auto;
				ChartingMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
				newSerie.XValueType = ChartValueType.DateTime;
				chartOrder = "DESC";
			}
			else if (ddXaxis.Text == "Battle")
			{
				ChartingMain.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;
				ChartingMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
				newSerie.XValueType = ChartValueType.Int32;
			}
            // Add to chart
            ChartingMain.Series.Add(newSerie);
			// Special calculations for calculated columns
			switch (selectedChartValue)
			{
				case "WN8": DrawChartWN8(tankName, chartSerie, chartOrder, chartMode); return;
                case "WN9": DrawChartWN9(tankName, chartSerie, chartOrder, chartMode); return;
            }
			// Draw series in chart now
			DrawChartSerie(tankName, chartSerie, chartOrder, chartValue, chartMode);
		}

		private static double CalcChartSeriesPointValue(List<double> values, CalculationType calcType, double defaultTier)
		{
            Code.Rating.WNHelper.RatingParameters rp = new Code.Rating.WNHelper.RatingParameters();
            double result = 0;
			switch (calcType)
			{
				case CalculationType.standard:
					result = values[0];
					break;
                case CalculationType.firstInPercentageOfNext:
                    result = values[0] * 100 / values[1];
                    break;
                case CalculationType.firstDividedOnNext:
                    result = values[0] / values[1];
                    break;
                case CalculationType.eff:
					rp.BATTLES = values[0];
                    rp.DAMAGE = values[1];
                    rp.SPOT = values[2];
                    rp.FRAGS = values[3];
                    rp.DEF = values[4];
                    rp.CAP = values[5];
                    rp.TIER = defaultTier; // values[6]; ???
                    result = Code.Rating.EFF.EffUseFormula(rp);
					break;
				case CalculationType.wn7:
					rp.BATTLES = values[0];
                    rp.DAMAGE = values[1];
                    rp.SPOT = values[2];
                    rp.FRAGS = values[3];
                    rp.DEF = values[4];
                    rp.CAP = values[5];
                    rp.WINS = values[6];
                    rp.TIER = defaultTier; // values[6]; ???
                    result = Code.Rating.WN7.WN7useFormula(rp);
					break;
				case CalculationType.wn8:
					break;
                case CalculationType.wn9:
                    break;
                default:
					break;
			}
			return result;
		}

		private void DrawChartSerie(string tankName, string chartSerie, string chartOrder, ChartType chartType, string chartMode)
		{
			// Create sql select fields and to store values
			string currentValCols = "";
			string firstValCols = "";
			string battleValCols = "";
			List<double> values = new List<double>();
			foreach (ChartTypeCols col in chartType.col)
			{
                if (col.battleValCol == "") col.battleValCol = col.playerTankValCol; // If no value added it is the same col on all sql's
                if (col.battleFirstValCol == "") col.battleFirstValCol = col.battleValCol; // If no value added use same as battle value col
				currentValCols += "SUM(" + col.playerTankValCol + "),";
                firstValCols += "SUM(" + col.battleFirstValCol + "),"; 
				battleValCols += col.battleValCol + ",";
				values.Add(0);
			}
			currentValCols = currentValCols.Substring(0, currentValCols.Length - 1) + " "; // Remove latest comma
			firstValCols = firstValCols.Substring(0, firstValCols.Length - 1) + " "; // Remove latest comma
			battleValCols = battleValCols.Substring(0, battleValCols.Length -1) + " "; // Remove latest comma
			// Find playerTank current value or all tanks current value
			string ptWhere = "";
			string bWhere = "";
			if (tankName != "All Tanks")
			{
				// Find playertank and current value
				int playerTankId = TankHelper.GetPlayerTankId(tankName);
				ptWhere = " where pt.id=@playerTankId ";
				bWhere = " where playerTankId=@playerTankId ";
				DB.AddWithValue(ref ptWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref bWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
                if (chartMode != "")
                {
                    ptWhere += "and ptb.battleMode = '" + chartMode + "' ";
                    bWhere += "and battleMode = '" + chartMode + "' ";
                }
			}
            else
            {
                if (chartMode != "")
                {
                    ptWhere += "where ptb.battleMode = '" + chartMode + "' ";
                    bWhere += "where battleMode = '" + chartMode + "' ";
                }
            }
			// Get current values	
			string sql =
				"select " + currentValCols +
				"from playerTankBattle ptb inner join " +
				"  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId inner join " +
				"  tank t on pt.tankId = t.id " +
				ptWhere;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtCurrent = DB.FetchData(sql);
			if (dtCurrent.Rows.Count > 0)
			{
				for (int i = 0; i < values.Count; i++)
				{
                    if (dtCurrent.Rows[0][i] != DBNull.Value)
                        values[i] = Convert.ToDouble(dtCurrent.Rows[0][i]);
				}
			}
			// If chart series per battle, loop back to find first values
			if (ddXaxis.Text == "Battle")
			{
				// Find first value by sutracting sum of recorded values
				sql =
					"select " + firstValCols +
					"from battle b inner join " +
					"  playerTank pt on b.playerTankId=pt.id and pt.playerId=@playerId inner join " +
					"  tank t on pt.tankId = t.id " +
					FilterPeriod(bWhere);
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable dtFirst = DB.FetchData(sql);
				if (dtFirst.Rows.Count > 0)
				{
					for (int i = 0; i < values.Count; i++)
					{
						if (dtFirst.Rows[0][i] != DBNull.Value)
							values[i] -= Convert.ToDouble(dtFirst.Rows[0][i]);
					}
				}
				dtFirst.Clear();
			}
			// Find battles
			sql =
				"select " + battleValCols + ", battleTime as battle_time, battlesCount as battles_Count " + 
				"from battle b inner join " +
				"  playerTank pt on b.playerTankId=pt.id and pt.playerId=@playerId inner join " +
				"  tank t on pt.tankId = t.id " +
				FilterPeriod(bWhere) + " " +
				"order by battleTime " + chartOrder;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtChart = DB.FetchData(sql);
			double chartVal = 0;
			// Calculate values for some special charts
			double defaultTier = 0;
			switch (chartType.calcType)
			{
				case CalculationType.eff:
					if (tankName == "All Tanks")
						// The total tier is added in column number 6, the total number of battles in col num 0
						defaultTier = Convert.ToDouble(dtCurrent.Rows[0][6]) / Convert.ToDouble(dtCurrent.Rows[0][0]);
					else
						defaultTier = TankHelper.GetTankTier(tankName);
					break;
				case CalculationType.wn7:
					if (tankName == "All Tanks")
						// The total tier is added in column number 6, the total number of battles in col num 0
                        defaultTier = Code.Rating.WNHelper.GetAverageTier();
					else
						defaultTier = TankHelper.GetTankTier(tankName);
					break;
				case CalculationType.wn8:
					break;
                case CalculationType.wn9:
                    break;
            }
			// If show per date
			if (ddXaxis.Text == "Date")
			{
				DateTime chartDate = DateTime.Now;
				double hourInterval = 24;
				if (dtChart.Rows.Count > 0)
				{
					chartDate = Convert.ToDateTime(dtChart.Rows[0]["battle_time"]);
					if (dtChart.Rows.Count > 5000)
						hourInterval = 48;
					else if (dtChart.Rows.Count > 10000)
						hourInterval = 24 * 7;
				}
				foreach (DataRow dr in dtChart.Rows)
				{
					DateTime thisDate = Convert.ToDateTime(dr["battle_time"]);
					if (thisDate <= chartDate || !chartType.totals)
					{
						if (chartType.totals)
							chartVal = Math.Round(CalcChartSeriesPointValue(values, chartType.calcType, defaultTier), decimals);
						else
							chartVal = Convert.ToDouble(dr[0]);
						axisYminimum = SetYaxisLowestValue(chartVal);
						ChartingMain.Series[chartSerie].Points.AddXY(thisDate, chartVal); // Use battle date
						chartDate = thisDate.AddHours(-hourInterval);
					}
					// Get previous value if not showing actual battle value
					for (int i = 0; i < values.Count; i++)
					{
						values[i] -= Convert.ToDouble(dr[i]);
					}
				}
			}
			// if show per battle (count)
			else if (ddXaxis.Text == "Battle")
			{
				int step = 0;
				int stepMod = dtChart.Rows.Count / numPoints;
				if (stepMod < 1) stepMod = 1;
				double battleCount = 0;
				foreach (DataRow dr in dtChart.Rows)
				{
					battleCount += Convert.ToDouble(dr["battles_Count"]); // Use battle count
					// Get next value
					for (int i = 0; i < values.Count; i++)
					{
						values[i] += Convert.ToDouble(dr[i]);
					}
					step++;
					if (step % stepMod == 0 || step == 0 || !chartType.totals)
					{
						if (chartType.totals)
							chartVal = Math.Round(CalcChartSeriesPointValue(values, chartType.calcType, defaultTier), decimals); 
						else
                            chartVal = Convert.ToDouble(dr[0]);
                        //axisYminimum = SetYaxisLowestValue(chartVal);
						ChartingMain.Series[chartSerie].Points.AddXY(battleCount, chartVal);
					}
				}
			}
			dtChart.Clear();
			dtCurrent.Clear();

            //ChartingMain.ChartAreas[0].AxisY.Minimum = RoundOff(axisYminimum);
		}

		private void DrawChartWN8(string tankName, string chartSerie, string chartOrder, string chartMode)
		{
			Cursor = Cursors.WaitCursor;
			// Find playerTank current value or all tanks current value
			string ptWhere = "";
			string bSumWhere = "";
			string bWhere = "";
            if (tankName != "All Tanks")
			{
				int playerTankId = TankHelper.GetPlayerTankId(tankName);
				ptWhere = " and pt.id=@playerTankId ";
				bSumWhere = " and playerTankId=@playerTankId ";
				bWhere = " where playerTankId=@playerTankId ";
				DB.AddWithValue(ref ptWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref bSumWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref bWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			}
            string battleModeWhere = "";
            if (chartMode != "")
                battleModeWhere = " and ptb.battleMode='" + chartMode + "' ";
			string sql =
				"select t.id as tankId, sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
				"  sum (ptb.def) as def, sum (ptb.cap) as cap, sum(wins) as wins " +
				"from playerTankBattle ptb left join " +
                "  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId " + battleModeWhere + " left join " +
				"  tank t on pt.tankId = t.id " +
				"where t.expDmg is not null " + ptWhere + " " + battleModeWhere +
				"group by t.id ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable ptb = DB.FetchData(sql); // ptb holds all parameters needed to calc WN8

            // If per battle, find all battles for reversing to first recorded
			if (ddXaxis.Text == "Battle")
			{
				// Find first value by sutracting sum of recorded values
                if (chartMode != "")
                    battleModeWhere = " and b.battleMode='" + chartMode + "' ";
				bSumWhere = "where t.expDmg is not null" + battleModeWhere + bSumWhere;
				sql =
					"select t.id as tankId, sum(b.battlesCount) as battles, sum(b.dmg) as dmg, sum (b.spotted) as spot, sum (b.frags) as frags, " +
					"  sum (b.def) as def, sum (cap) as cap, sum(victory) as wins " +
					"from battle b left join " +
					"  playerTank pt on b.playerTankId=pt.id and pt.playerId=@playerId left join " +
					"  tank t on pt.tankId = t.id " +
					FilterPeriod(bSumWhere) + " " +
					"group by t.id ";
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable dtBattleSum = DB.FetchData(sql);
				if (dtBattleSum.Rows.Count > 0)
				{
					foreach (DataRow ptbRow in ptb.Rows)
					{
						int tankId = Convert.ToInt32(ptbRow["tankId"]);
						DataRow[] bRow = dtBattleSum.Select("tankId = " + tankId);
						if (bRow.Length > 0)
						{
							ptbRow["battles"] = Convert.ToInt32(ptbRow["battles"]) - Convert.ToInt32(bRow[0]["battles"]);
							ptbRow["dmg"] = Convert.ToInt32(ptbRow["dmg"]) - Convert.ToInt32(bRow[0]["dmg"]);
							ptbRow["spot"] = Convert.ToInt32(ptbRow["spot"]) - Convert.ToInt32(bRow[0]["spot"]);
							ptbRow["frags"] = Convert.ToInt32(ptbRow["frags"]) - Convert.ToInt32(bRow[0]["frags"]);
							ptbRow["def"] = Convert.ToInt32(ptbRow["def"]) - Convert.ToInt32(bRow[0]["def"]);
							ptbRow["cap"] = Convert.ToInt32(ptbRow["cap"]) - Convert.ToInt32(bRow[0]["cap"]);
							ptbRow["wins"] = Convert.ToInt32(ptbRow["wins"]) - Convert.ToInt32(bRow[0]["wins"]);
						}
					}
				}
			}

			// Find battles
            if (chartMode != "")
                battleModeWhere = " and battleMode='" + chartMode + "' ";
			sql = 
				"select battle.*, playerTank.tankId as tankId, battleTime as battle_time, battlesCount as battles_Count " + 
				"from battle inner join " +
                "  playerTank on battle.playerTankId = playerTank.id and playerTank.playerId=@playerId " + battleModeWhere + 
				FilterPeriod(bWhere) + " " + 
				"order by battleTime " + chartOrder;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtChart = DB.FetchData(sql);
			double chartVal = 0;

			// Draw chart per date
			if (ddXaxis.Text == "Date")
			{
				DateTime chartDate = DateTime.Now;
				double hourInterval = 24;
				if (dtChart.Rows.Count > 0)
				{
					chartDate = Convert.ToDateTime(dtChart.Rows[0]["battle_time"]);
					if (dtChart.Rows.Count > 5000)
						hourInterval = 48;
					else if (dtChart.Rows.Count > 10000)
						hourInterval = 24 * 7;
				}
				foreach (DataRow dr in dtChart.Rows)
				{
					DateTime thisDate = Convert.ToDateTime(dr["battle_time"]);
					if (thisDate <= chartDate)
					{
                        chartVal = Math.Round(Code.Rating.WN8.CalcPlayerTankBattle(ptb), decimals);
						axisYminimum = SetYaxisLowestValue(chartVal);
						ChartingMain.Series[chartSerie].Points.AddXY(thisDate, chartVal); // Use battle date
						chartDate = thisDate.AddHours(-hourInterval);
					}
					// Get prev val
					string tankId = dr["tankId"].ToString();
					DataRow[] ptbRow = ptb.Select("tankId = " + tankId);
					if (ptbRow.Length > 0)
					{
						ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) - Convert.ToInt32(dr["battlesCount"]);
						ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) - Convert.ToInt32(dr["dmg"]);
						ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) - Convert.ToInt32(dr["spotted"]);
						ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) - Convert.ToInt32(dr["frags"]);
						ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) - Convert.ToInt32(dr["def"]);
						ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) - Convert.ToInt32(dr["cap"]);
						ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) - Convert.ToInt32(dr["victory"]);
					}
				}
			}
			// Draw chart per battle
			else if (ddXaxis.Text == "Battle")
			{
				int step = 0;
				int stepMod = dtChart.Rows.Count / numPoints;
				if (stepMod < 1) stepMod = 1;
				double battleCount = 0;
				foreach (DataRow bRow in dtChart.Rows)
				{
					string tankId = bRow["tankId"].ToString();
					DataRow[] ptbRow = ptb.Select("tankId = " + tankId);
					battleCount += Convert.ToDouble(bRow["battlesCount"]); // Use battle count
					if (ptbRow.Length > 0)
					{
						ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) + Convert.ToInt32(bRow["battlesCount"]); ;
						ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) + Convert.ToInt32(bRow["dmg"]);
						ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) + Convert.ToInt32(bRow["spotted"]);
						ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) + Convert.ToInt32(bRow["frags"]);
						ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) + Convert.ToInt32(bRow["def"]);
						ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) + Convert.ToInt32(bRow["cap"]);
						ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) + Convert.ToInt32(bRow["victory"]);
					}
					step++;
					if (step % stepMod == 0 || step == 0)
					{
                        chartVal = Math.Round(Code.Rating.WN8.CalcPlayerTankBattle(ptb), decimals);
						ChartingMain.Series[chartSerie].Points.AddXY(battleCount, chartVal); // Use battle count
					}
				}
			}
			Cursor = Cursors.Default;
		}

        private void DrawChartWN9(string tankName, string chartSerie, string chartOrder, string chartMode)
        {
            if (tankName != "All Tanks")
                DrawChartWN9PerTank(tankName, chartSerie, chartOrder, chartMode);
            else
                DrawChartWN9ForAccount(chartSerie, chartOrder, chartMode);
        }
        
        private void DrawChartWN9PerTank(string tankName, string chartSerie, string chartOrder, string chartMode)
        {
            Cursor = Cursors.WaitCursor;
            // Find playerTank current value or all tanks current value
            string ptWhere = "";
            string bSumWhere = "";
            string bWhere = "";
            // Filter on tank
            int playerTankId = TankHelper.GetPlayerTankId(tankName);
            ptWhere = " and pt.id=@playerTankId ";
            bSumWhere = " and playerTankId=@playerTankId ";
            bWhere = " where playerTankId=@playerTankId ";
            DB.AddWithValue(ref ptWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
            DB.AddWithValue(ref bSumWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
            DB.AddWithValue(ref bWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
            // Filter on battlemode
            string battleModeWhere = "";
            if (chartMode != "")
                battleModeWhere = " and ptb.battleMode='" + chartMode + "' ";
            // Get data
            string sql =
                "select t.id as tankId, sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
                "  sum (ptb.def) as def, sum (ptb.cap) as cap, sum(wins) as wins " +
                "from playerTankBattle ptb left join " +
                "  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId " + battleModeWhere + " left join " +
                "  tank t on pt.tankId = t.id " +
                "where t.wn9exp is not null " + ptWhere + " " + battleModeWhere +
                "group by t.id ";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DataTable ptb = DB.FetchData(sql);

            // if per battle, start from left - find initial value
            if (ddXaxis.Text == "Battle")
            {
                // Find first value by sutracting sum of recorded values
                if (chartMode != "")
                    battleModeWhere = " and b.battleMode='" + chartMode + "' ";
                bSumWhere = "where t.expDmg is not null" + battleModeWhere + bSumWhere;
                sql =
                    "select t.id as tankId, sum(b.battlesCount) as battles, sum(b.dmg) as dmg, sum (b.spotted) as spot, sum (b.frags) as frags, " +
                    "  sum (b.def) as def, sum (cap) as cap, sum(victory) as wins " +
                    "from battle b left join " +
                    "  playerTank pt on b.playerTankId=pt.id and pt.playerId=@playerId left join " +
                    "  tank t on pt.tankId = t.id " +
                    FilterPeriod(bSumWhere) + " " +
                    "group by t.id ";
                DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
                DataTable dtBattleSum = DB.FetchData(sql);
                if (dtBattleSum.Rows.Count > 0)
                {
                    foreach (DataRow ptbRow in ptb.Rows)
                    {
                        int tankId = Convert.ToInt32(ptbRow["tankId"]);
                        DataRow[] bRow = dtBattleSum.Select("tankId = " + tankId);
                        if (bRow.Length > 0)
                        {
                            ptbRow["battles"] = Convert.ToInt32(ptbRow["battles"]) - Convert.ToInt32(bRow[0]["battles"]);
                            ptbRow["dmg"] = Convert.ToInt32(ptbRow["dmg"]) - Convert.ToInt32(bRow[0]["dmg"]);
                            ptbRow["spot"] = Convert.ToInt32(ptbRow["spot"]) - Convert.ToInt32(bRow[0]["spot"]);
                            ptbRow["frags"] = Convert.ToInt32(ptbRow["frags"]) - Convert.ToInt32(bRow[0]["frags"]);
                            ptbRow["def"] = Convert.ToInt32(ptbRow["def"]) - Convert.ToInt32(bRow[0]["def"]);
                            ptbRow["cap"] = Convert.ToInt32(ptbRow["cap"]) - Convert.ToInt32(bRow[0]["cap"]);
                            ptbRow["wins"] = Convert.ToInt32(ptbRow["wins"]) - Convert.ToInt32(bRow[0]["wins"]);
                        }
                    }
                }
            }
            
            // Find battles
            if (chartMode != "")
                battleModeWhere = " and battleMode='" + chartMode + "' ";
            sql =
                "select battle.*, playerTank.tankId as tankId, battleTime as battle_time, battlesCount as battles_Count " +
                "from battle inner join " +
                "  playerTank on battle.playerTankId = playerTank.id and playerTank.playerId=@playerId " + battleModeWhere +
                FilterPeriod(bWhere) + " " +
                "order by battleTime " + chartOrder;
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DataTable dtChart = DB.FetchData(sql);
            double chartVal = 0;
            
            // If per date
            if (ddXaxis.Text == "Date")
            {
                DateTime chartDate = DateTime.Now;
                double hourInterval = 24;
                if (dtChart.Rows.Count > 0)
                {
                    chartDate = Convert.ToDateTime(dtChart.Rows[0]["battle_time"]);
                    if (dtChart.Rows.Count > 5000)
                        hourInterval = 48;
                    else if (dtChart.Rows.Count > 10000)
                        hourInterval = 24 * 7;
                }
                foreach (DataRow dr in dtChart.Rows)
                {
                    DateTime thisDate = Convert.ToDateTime(dr["battle_time"]);
                    if (thisDate <= chartDate)
                    {
                        double wn9maxhist = 0;
                        chartVal = Math.Round(WN9.CalcPlayerTankBattle(ptb, out wn9maxhist), decimals);
                        axisYminimum = SetYaxisLowestValue(chartVal);
                        ChartingMain.Series[chartSerie].Points.AddXY(thisDate, chartVal); // Use battle date
                        chartDate = thisDate.AddHours(-hourInterval);
                    }
                    // Get prev val
                    string tankId = dr["tankId"].ToString();
                    DataRow[] ptbRow = ptb.Select("tankId = " + tankId);
                    if (ptbRow.Length > 0)
                    {
                        ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) - Convert.ToInt32(dr["battlesCount"]);
                        ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) - Convert.ToInt32(dr["dmg"]);
                        ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) - Convert.ToInt32(dr["spotted"]);
                        ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) - Convert.ToInt32(dr["frags"]);
                        ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) - Convert.ToInt32(dr["def"]);
                        ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) - Convert.ToInt32(dr["cap"]);
                        ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) - Convert.ToInt32(dr["victory"]);
                    }
                }
            }
            // If per battle
            else if (ddXaxis.Text == "Battle")
            {
                int step = 0;
                int stepMod = dtChart.Rows.Count / numPoints;
                if (stepMod < 1) stepMod = 1;
                double battleCount = 0;
                foreach (DataRow bRow in dtChart.Rows)
                {
                    string tankId = bRow["tankId"].ToString();
                    DataRow[] ptbRow = ptb.Select("tankId = " + tankId);
                    battleCount += Convert.ToDouble(bRow["battlesCount"]); // Use battle count
                    if (ptbRow.Length > 0)
                    {
                        ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) + Convert.ToInt32(bRow["battlesCount"]); ;
                        ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) + Convert.ToInt32(bRow["dmg"]);
                        ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) + Convert.ToInt32(bRow["spotted"]);
                        ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) + Convert.ToInt32(bRow["frags"]);
                        ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) + Convert.ToInt32(bRow["def"]);
                        ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) + Convert.ToInt32(bRow["cap"]);
                        ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) + Convert.ToInt32(bRow["victory"]);
                    }
                    step++;
                    if (step % stepMod == 0 || step == 0)
                    {
                        double wn9maxhist = 0;
                        chartVal = Math.Round(Code.Rating.WN9.CalcPlayerTankBattle(ptb, out wn9maxhist), decimals);
                        //axisYminimum = SetYaxisLowestValue(chartVal);
                        ChartingMain.Series[chartSerie].Points.AddXY(battleCount, chartVal); // Use battle count
                    }
                }
            }
            //ChartingMain.ChartAreas[0].AxisY.Minimum = RoundOff(axisYminimum);
            Cursor = Cursors.Default;
        }

        private void DrawChartWN9ForAccount(string chartSerie, string chartOrder, string chartMode)
        {
            // Start
            Cursor = Cursors.WaitCursor;

            // Done
            Cursor = Cursors.Default;

            MsgBox.Show("Not yet implemented, try WN9 per tank", "Not implemented");
            return;
            
            //// Get battle result for battle range to reverse
            //DataTable ptbWN9params = WN9.GetPlayerTotalWN9TankStats(chartMode);

            //// Find first battle in range
            //string battleModeWhere = "";
            //if (chartMode != "")
            //    battleModeWhere = " and battleMode='" + chartMode + "' ";
            //string sql =
            //    "select min(battle.id) as battleId, min(battle.battleTime) as battle_time, sum(battle.battlesCount) as battle_count " +
            //    "from battle inner join " +
            //    "  playerTank on battle.playerTankId = playerTank.id and playerTank.playerId=@playerId " + battleModeWhere;
            //DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            //DataTable dtChart = DB.FetchData(sql);
            //double chartVal = 0;

            //// Draw chart per date
            //if (ddXaxis.Text == "Date")
            //{
            //    DateTime thisDate = DateTime.Now;
            //    DateTime chartDateMin = DateTime.Now;
            //    int battleCount = 0;
            //    double hourInterval = 24;
            //    if (dtChart.Rows.Count > 0)
            //    {
            //        chartDateMin = Convert.ToDateTime(dtChart.Rows[0]["battle_time"]);
            //        battleCount = Convert.ToInt32(dtChart.Rows[0]["battle_count"]);
            //        if (battleCount > 1000)
            //            hourInterval = 48;
            //        else if (battleCount > 5000)
            //            hourInterval = 24 * 7;
            //    }

            //    // Draw current wn9 on chart
            //    chartVal = Math.Round(WN9.CalcPlayerTotal(chartMode, ptbWN9params), decimals);
            //    axisYminimum = SetYaxisLowestValue(chartVal);
            //    ChartingMain.Series[chartSerie].Points.AddXY(thisDate, chartVal); // Use battle date

            //    // Loop to prev date 
            //    while (DateTime.Compare(thisDate, chartDateMin) > 0)
            //    {
            //        thisDate = thisDate.AddHours(-hourInterval);
            //        string battleTimeFilter = " AND battleTime<@battleTime ";
            //        DB.AddWithValue(ref battleTimeFilter, "@battleTime", thisDate, DB.SqlDataType.DateTime);
            //        chartVal = Math.Round(WN9.CalcBattleRangeReverse(battleTimeFilter, 0, chartMode), decimals);
            //        axisYminimum = SetYaxisLowestValue(chartVal);
            //        ChartingMain.Series[chartSerie].Points.AddXY(thisDate, chartVal); // Use battle date
            //    }
            //}

            
        }

        string selectedXaxis = "";
		private void ddXaxis_Click(object sender, EventArgs e)
		{
			selectedXaxis = ddXaxis.Text;						
			Code.DropDownGrid.Show(ddXaxis, Code.DropDownGrid.DropDownGridType.List, "Date,Battle");
		}

		private void ddXaxis_TextChanged(object sender, EventArgs e)
		{
			if (ChartingMain.Series.Count > 0)
			{
				if (selectedXaxis != ddXaxis.Text)
				{
					// Disabled message, just redraw chart if changed
                    // Code.MsgBox.Button answer = MsgBox.Show("Changing x-axis require chart to be cleared", "Clear Chart?", MsgBoxType.OKCancel, this);
                    //if (answer == MsgBox.Button.OK)
                    //{
                    //    ResetChart();
                    //    btnAddChart.Text = "Add";
                    //}
                    //else
                    //{
                    //    ddXaxis.Text = selectedXaxis;
                    //}
                    ResetChart();
                    if (ddValue.Text != "")
                        DrawChart();
				}
			}
		}


		private void btnAddChart_Click(object sender, EventArgs e)
		{
			DrawChart();
		}

        private void ddTank_Click(object sender, EventArgs e)
        {
            Code.DropDownGrid.Show(ddTank, Code.DropDownGrid.DropDownGridType.List, "( All Tanks ),Search for Tank...");
        }

		private void ddValue_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(ddValue, Code.DropDownGrid.DropDownGridType.List, ddChartList);
		}

        public string GetChartSeriesName()
        {
            string tankName = ddTank.Text;
            if (tankName == "( All Tanks )") tankName = "All Tanks";
            string selectedChartValue = ddValue.Text;
            string selectedModeValue = ddMode.Text;
            if (selectedModeValue == "( All Modes )") selectedModeValue = "All Modes";
            string chartSerie = tankName + " - " + selectedChartValue + " - " + selectedModeValue;
            return chartSerie;
        }

		private void CheckAddButton()
		{
            string chartSerie = GetChartSeriesName();
            // Check if already shown
			string buttonText = "Add";
			foreach (Series serie in ChartingMain.Series)
			{
				if (serie.Name == chartSerie)
				{
					buttonText = "Refresh";
				}
			}
			if (btnAddChart.Text != buttonText)
				btnAddChart.Text = buttonText;
		}

		private void btnClearChart_Click(object sender, EventArgs e)
		{
			ResetChart();
			btnAddChart.Text = "Add";
			lblFooter.Text = "Chart cleared. No chart selected, please add a chart line";
		}

		private void ResetChart()
		{
			ChartingMain.Series.Clear();
			ChartingMain.ResetAutoValues();
			axisYminimum = 999999999;
		}

		Point? prevPosition = null;
		ToolTip tooltip = new ToolTip();

		private void ChartingMain_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				string XLabel = ddXaxis.Text + ": ";
				var pos = e.Location;
				if (prevPosition.HasValue && pos == prevPosition.Value)
					return;
				tooltip.RemoveAll();
				prevPosition = pos;
				var results = ChartingMain.HitTest(pos.X, pos.Y, false,
												ChartElementType.DataPoint);
				foreach (var result in results)
				{
					if (result.ChartElementType == ChartElementType.DataPoint)
					{
						var prop = result.Object as DataPoint;
						if (prop != null)
						{
							var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
							var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

							// check if the cursor is really close to the point (2 pixels around the point)
							if (Math.Abs(pos.X - pointXPixel) < 2 &&
								Math.Abs(pos.Y - pointYPixel) < 2)
							{
								string YValue = prop.YValues[0].ToString();
								string XValue = prop.XValue.ToString();
								if (ddXaxis.Text == "Date")
									XValue = DateTime.FromOADate((double)prop.XValue).ToString("dd.MM.yyyy");
								tooltip.Show(XLabel + XValue + Environment.NewLine + "Value: " + YValue, this.ChartingMain, pos.X + 10, pos.Y);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				//throw;
			}
		}

        private void ddTank_TextChanged(object sender, EventArgs e)
        {
            // Check if search is selected
            if (ddTank.Text == "Search for Tank...")
            {
                TankSearchHelper.OpenTankSearch(this);
                if (TankSearchHelper.Result == MsgBox.Button.OK && TankSearchHelper.SelectedTankId > 0)
                {
                    ddTank.Text = TankHelper.GetTankName(TankSearchHelper.SelectedTankId);
                }
            }
            CheckAddButton();
        }

		private void ddValue_TextChanged(object sender, EventArgs e)
		{
			CheckAddButton();
		}

		private void BattleChart_FormClosed(object sender, FormClosedEventArgs e)
		{
			FormHelper.ClosedOne();
		}

		private void AddChartValues()
		{
			List<ChartTypeCols> chartCol;

			// Win Rate
			chartCol = new List<ChartTypeCols>();
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartValues.Add(new ChartType() { name = "Win Rate", col = chartCol, calcType = CalculationType.firstInPercentageOfNext });

            // Damage Rating
            chartCol = new List<ChartTypeCols>();
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "(damageRating / 100)", battleValCol = "(damageRating / 100)" });
            chartValues.Add(new ChartType() { name = "Dmg Rating", col = chartCol, calcType = CalculationType.standard });

            // WN9
            chartCol = new List<ChartTypeCols>();
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "spot", battleValCol = "spotted" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "frags", battleValCol = "frags" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "def", battleValCol = "def" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "cap", battleValCol = "cap" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "t.tier", battleFirstValCol = "t.tier", battleValCol = "tier" });
            chartValues.Add(new ChartType() { name = "WN9", col = chartCol, calcType = CalculationType.wn9 });

            // WN9 per battle
            chartCol = new List<ChartTypeCols>();
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "wn9" });
            chartValues.Add(new ChartType() { name = "WN9 per battle", col = chartCol, totals = false, chartType = SeriesChartType.Point });


            // WN8
            chartCol = new List<ChartTypeCols>();
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "spot", battleValCol = "spotted" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "frags", battleValCol = "frags" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "def", battleValCol = "def" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "cap", battleValCol = "cap" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "t.tier * ptb.battles", battleFirstValCol = "t.tier * b.battlesCount", battleValCol = "tier" });
			chartValues.Add(new ChartType() { name = "WN8", col = chartCol, calcType = CalculationType.wn8 });
			
			// WN8 per battle
			chartCol = new List<ChartTypeCols>();
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "wn8" });
			chartValues.Add(new ChartType() { name = "WN8 per battle", col = chartCol, totals = false, chartType = SeriesChartType.Point });

			// WN 7
			chartCol = new List<ChartTypeCols>();
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "spot", battleValCol = "spotted" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "frags", battleValCol = "frags" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "def", battleValCol = "def" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "cap", battleValCol = "cap" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "t.tier * ptb.battles", battleFirstValCol = "t.tier * b.battlesCount", battleValCol = "tier" });
			chartValues.Add(new ChartType() { name = "WN7", col = chartCol, calcType = CalculationType.wn7 });

			// WN7 per battle
			chartCol = new List<ChartTypeCols>();
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "wn7" });
			chartValues.Add(new ChartType() { name = "WN7 per battle", col = chartCol, totals=false, chartType = SeriesChartType.Point });

            // Efficiency
            chartCol = new List<ChartTypeCols>();
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "spot", battleValCol = "spotted" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "frags", battleValCol = "frags" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "def", battleValCol = "def" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "cap", battleValCol = "cap" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "t.tier * ptb.battles", battleFirstValCol = "t.tier * b.battlesCount", battleValCol = "tier" });
            chartValues.Add(new ChartType() { name = "EFF", col = chartCol, calcType = CalculationType.eff });

            // Efficiency per battle
            chartCol = new List<ChartTypeCols>();
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "eff" });
            chartValues.Add(new ChartType() { name = "EFF per battle", col = chartCol, totals = false, chartType = SeriesChartType.Point });

            // XP
            chartCol = new List<ChartTypeCols>();
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "xp" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartValues.Add(new ChartType() { name = "XP Average", col = chartCol, calcType = CalculationType.firstDividedOnNext });
			
            // XP
			chartCol = new List<ChartTypeCols>();
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "xp" });
			chartValues.Add(new ChartType() { name = "XP Total", col = chartCol });
			
			// Average damage
			chartCol = new List<ChartTypeCols>();
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "dmg" });
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
			chartValues.Add(new ChartType() { name = "Damage Average", col = chartCol, calcType = CalculationType.firstDividedOnNext });

            // Damage
            chartCol = new List<ChartTypeCols>();
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "dmg" });
            chartValues.Add(new ChartType() { name = "Damage Total", col = chartCol });

            // Frag Average
            chartCol = new List<ChartTypeCols>();
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "frags" });
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartValues.Add(new ChartType() { name = "Frags Average", col = chartCol, calcType = CalculationType.firstDividedOnNext });

            // Frag Count
            chartCol = new List<ChartTypeCols>();
            chartCol.Add(new ChartTypeCols() { playerTankValCol = "frags" });
            chartValues.Add(new ChartType() { name = "Frags Total", col = chartCol });

			// Battle count
			chartCol = new List<ChartTypeCols>();
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
			chartValues.Add(new ChartType() { name = "Battle Count", col = chartCol });

			// Victory Count
			chartCol = new List<ChartTypeCols>();
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" });
			chartValues.Add(new ChartType() { name = "Victory Count", col = chartCol });

			// Draw Count
			chartCol = new List<ChartTypeCols>();
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "(battles - wins - losses)", battleValCol = "draw" });
			chartValues.Add(new ChartType() { name = "Draw Count", col = chartCol });

			// Defeat Count
			chartCol = new List<ChartTypeCols>();
			chartCol.Add(new ChartTypeCols() { playerTankValCol = "losses", battleValCol = "defeat" });
			chartValues.Add(new ChartType() { name = "Defeat Count", col = chartCol });


		}

        private void ddMode_Click(object sender, EventArgs e)
        {
            Code.DropDownGrid.Show(ddMode, Code.DropDownGrid.DropDownGridType.List, ddModeList);
        }

        private void ddMode_TextChanged(object sender, EventArgs e)
        {
            CheckAddButton();
        }

        private void chkBullet_Click(object sender, EventArgs e)
        {
            if (btnAddChart.Text == "Refresh")
                DrawChart();
        }

        private void chkSpline_Click(object sender, EventArgs e)
        {
            if (btnAddChart.Text == "Refresh")
                DrawChart();
        }

        private void ddPeriod_TextChanged(object sender, EventArgs e)
        {
            if (btnAddChart.Text == "Refresh")
                DrawChart();
        }

	}
}
