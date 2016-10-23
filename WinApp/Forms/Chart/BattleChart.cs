using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WinApp.Code;
using WinApp.Code.FormView;
using WinApp.Code.Rating;

namespace WinApp.Forms
{
	public partial class BattleChart : FormCloseOnEsc
    {
        #region Init

        int initTankId = 0;
		int decimals = 3;
		int numPoints = 100; // Max num of points in one chart serie, exept for battle values (ChartValues.totals = false)

        // List of all available chart types 
        private List<BattleChartHelper.ChartType> chartTypeList = BattleChartHelper.GetChartTypeList(); 
        
		public BattleChart(int tankId = 0)
		{
			InitializeComponent();
			initTankId = tankId;
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
            // Toolstrip set width
            mMain.Width = this.Width - 2;
            
            // Battle Modes, set menu
            SetBattleModeMenu();
            // Battle Time Filter, set menu
            SetBattleTimeFilterMenu();

            // Set x-axis
            mXaxisDate.Checked = (BattleChartHelper.Settings.Xaxis == mXaxisDate.Text);
            mXaxisBattle.Checked = (BattleChartHelper.Settings.Xaxis == mXaxisBattle.Text);
            
            // Set options
            mBullet.Checked = BattleChartHelper.Settings.Bullet;
            mSpline.Checked = BattleChartHelper.Settings.Spline;

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
			}
			area.AxisX.LabelStyle.Angle = 20;

            ChartingMain.ChartAreas[0].AxisY2.IsStartedFromZero = ChartingMain.ChartAreas[0].AxisY.IsStartedFromZero;
            //ChartingMain.ChartAreas[0].AxisY2.LineDashStyle = ChartDashStyle.Dash;
            //ChartingMain.ChartAreas[0].AxisY2.LineColor = Color.Black;
            //ChartingMain.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            ChartingMain.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;
            // Get favourites
            SetFavouritMenu();

        }

        private void BattleChart_Shown(object sender, EventArgs e)
        {
            if (initTankId != 0)
            {
                // Add chart item for selected tank (right click on tank from main form grid)
                BattleChartHelper.CurrentChartView = new List<BattleChartHelper.BattleChartItem>();
                OpenFormSelectChartParameters(initTankId);
            }
            else
            {
                // Get latest used favourite
                GetFavouriteChart(Config.Settings.currentChartFavourite);
                // Draw current chart view
                DrawCurrentChartView();
            }

        }

        #endregion

        #region Create Chart

        private string GetBattleTimeFilter(string where)
		{
			string newWhere = "";
			if (BattleChartHelper.Settings.BattleTime == "ALL")
				newWhere = where;
			else
			{
				DateTime afterDate = DateTime.Now;
				switch (BattleChartHelper.Settings.BattleTime)
				{
                    case "Y2": afterDate = DateTime.Now.AddYears(-2); break;
                    case "Y1": afterDate = DateTime.Now.AddYears(-1); break;
					case "M6": afterDate = DateTime.Now.AddMonths(-6); break;
					case "M3": afterDate = DateTime.Now.AddMonths(-3); break;
					case "M1": afterDate = DateTime.Now.AddMonths(-1); break;
					case "W2": afterDate = DateTime.Now.AddDays(-14); break;
					case "W1": afterDate = DateTime.Now.AddDays(-7); break;
				}
				newWhere = " battleTime >= '" + afterDate.ToString("yyyy-MM-dd") + "' ";
				if (where == "")
					newWhere = " where " + newWhere;
				else
					newWhere = where + " and " + newWhere;

			}
			return newWhere;
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

        private void DrawCurrentChartView()
        {
            // Default = auto x-axis
            ChartingMain.ChartAreas[0].AxisX.Minimum = Double.NaN;
            // Check if 2 Yaxis
            bool yAxix1 = false;
            bool yAxix2 = false;
            foreach (BattleChartHelper.BattleChartItem item in BattleChartHelper.CurrentChartView)
            {
                if (item.use2ndYaxis == false)
                    yAxix1 = true;
                if (item.use2ndYaxis == true)
                    yAxix2 = true;
            }
            // Clear chart and prepare for yaxis
            ClearChartArea(yAxix1 && yAxix2);
            // Add chart values as series
            foreach (BattleChartHelper.BattleChartItem item in BattleChartHelper.CurrentChartView)
            {
                if (yAxix1 && yAxix2)
                    AddChartValuesToChart(item.tankId, item.tankName, item.chartTypeName, item.use2ndYaxis); // Both Y-axis in use
                else
                    AddChartValuesToChart(item.tankId, item.tankName, item.chartTypeName, false); // Only one Y-axis
            }
        }

        private void AddChartValuesToChart(int tankId, string tankName, string chartTypeName, bool use2ndYaxis)
		{
            // Init
            string chartSerie = tankName + " - " + chartTypeName;
            if (use2ndYaxis)
                chartSerie += " *";
            string chartOrder = "";
            // Get battle mode item, set param = "" if set as "ALL" Modes
            string chartMode = BattleChartHelper.Settings.BattleMode;
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
			BattleChartHelper.ChartType chartType = chartTypeList.Find(c => c.name == chartTypeName);
			// Add series
			Series newSerie = new Series(chartSerie);
            // Check for 2nd Y-axis
            if (use2ndYaxis)
                newSerie.YAxisType = AxisType.Secondary;
            // Line  and marker type
            newSerie.ChartType = chartType.seriesStyle;
            if (chartType.seriesStyle == SeriesChartType.Point) // Point = only dot shown
            {
                // Default marker type for point type
                newSerie.MarkerStyle = MarkerStyle.Circle;
            }
            else // Other chart type = line type
            {
                // Override to spline type if checked
                if (mSpline.Checked) 
                    newSerie.ChartType = SeriesChartType.Spline;
                // Set marker type
                if (mBullet.Checked)
                    newSerie.MarkerStyle = MarkerStyle.Circle;
                else
                    newSerie.MarkerStyle = MarkerStyle.None;
            }
            if (BattleChartHelper.Settings.Xaxis == "Date")
			{
				ChartingMain.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Auto;
                ChartingMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
				newSerie.XValueType = ChartValueType.DateTime;
				chartOrder = "DESC";
			}
			else if (BattleChartHelper.Settings.Xaxis == "Battle")
			{
				ChartingMain.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;
				ChartingMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
				newSerie.XValueType = ChartValueType.Int32;
			}
            
            // Add series to chart
            ChartingMain.Series.Add(newSerie);
            
            // Special calculations for calculated columns
			switch (chartTypeName)
			{
				case "WN8":
                    DrawChartSeriesWN8(tankId, chartSerie, chartMode);
                    return;
                case "WN9":
                    if (tankId != 0)
                        DrawChartSeriesWN9PerTank(tankId, chartSerie, chartOrder, chartMode);
                    else
                        DrawChartSeriesWN9ForAccount(chartSerie, chartOrder, chartMode);
                    return;
            }
			// Draw series in chart now
			DrawChartSeries(tankId, chartSerie, chartOrder, chartType, chartMode);
		}

		private static double CalcChartSeriesPointValue(List<double> values, BattleChartHelper.CalculationType calcType, double defaultTier)
		{
            Code.Rating.WNHelper.RatingParameters rp = new Code.Rating.WNHelper.RatingParameters();
            double result = 0;
			switch (calcType)
			{
				case BattleChartHelper.CalculationType.standard:
					result = values[0];
					break;
                case BattleChartHelper.CalculationType.firstInPercentageOfNext:
                    result = values[0] * 100 / values[1];
                    break;
                case BattleChartHelper.CalculationType.firstDividedOnNext:
                    result = values[0] / values[1];
                    break;
                case BattleChartHelper.CalculationType.eff:
					rp.BATTLES = values[0];
                    rp.DAMAGE = values[1];
                    rp.SPOT = values[2];
                    rp.FRAGS = values[3];
                    rp.DEF = values[4];
                    rp.CAP = values[5];
                    rp.TIER = defaultTier; // values[6]; ???
                    result = Code.Rating.EFF.EffUseFormula(rp);
					break;
				case BattleChartHelper.CalculationType.wn7:
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
				case BattleChartHelper.CalculationType.wn8:
					break;
                case BattleChartHelper.CalculationType.wn9:
                    break;
                default:
					break;
			}
			return result;
		}

		private void DrawChartSeries(int tankId, string chartSerie, string chartOrder, BattleChartHelper.ChartType chartType, string chartMode)
		{
			// Create sql select fields and to store values
			string currentValCols = "";
			string firstValCols = "";
			string battleValCols = "";
			List<double> values = new List<double>();
			foreach (BattleChartHelper.ChartTypeCols col in chartType.col)
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
			if (tankId != 0)
			{
				// Find playertank and current value
				int playerTankId = TankHelper.GetPlayerTankId(tankId);
				ptWhere = " where pt.id=@playerTankId ";
				bWhere = " where playerTankId=@playerTankId ";
				DB.AddWithValue(ref ptWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref bWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
                if (chartMode != "ALL")
                {
                    ptWhere += "and ptb.battleMode = '" + chartMode + "' ";
                    bWhere += "and battleMode = '" + chartMode + "' ";
                }
			}
            else
            {
                if (chartMode != "ALL")
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
			if (BattleChartHelper.Settings.Xaxis == "Battle")
			{
				// Find first value by sutracting sum of recorded values
				sql =
					"select " + firstValCols +
					"from battle b inner join " +
					"  playerTank pt on b.playerTankId=pt.id and pt.playerId=@playerId inner join " +
					"  tank t on pt.tankId = t.id " +
					GetBattleTimeFilter(bWhere);
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
				GetBattleTimeFilter(bWhere) + " " +
				"order by battleTime " + chartOrder;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtChart = DB.FetchData(sql);
			double chartVal = 0;
			// Calculate values for some special charts
			double defaultTier = 0;
			switch (chartType.calcType)
			{
				case BattleChartHelper.CalculationType.eff:
					if (tankId == 0)
						// The total tier is added in column number 6, the total number of battles in col num 0
						defaultTier = Convert.ToDouble(dtCurrent.Rows[0][6]) / Convert.ToDouble(dtCurrent.Rows[0][0]);
					else
						defaultTier = TankHelper.GetTankTier(tankId);
					break;
				case BattleChartHelper.CalculationType.wn7:
					if (tankId == 0)
						// The total tier is added in column number 6, the total number of battles in col num 0
                        defaultTier = Code.Rating.WNHelper.GetAverageTier();
					else
						defaultTier = TankHelper.GetTankTier(tankId);
					break;
				case BattleChartHelper.CalculationType.wn8:
					break;
                case BattleChartHelper.CalculationType.wn9:
                    break;
            }
			// If show per date
			if (BattleChartHelper.Settings.Xaxis == "Date")
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
			else if (BattleChartHelper.Settings.Xaxis == "Battle")
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
						ChartingMain.Series[chartSerie].Points.AddXY(battleCount, chartVal);
					}
				}
			}
			dtChart.Clear();
			dtCurrent.Clear();
		}

		private void DrawChartSeriesWN8(int tankId, string chartSerie, string chartMode)
		{
			Cursor = Cursors.WaitCursor;
			// Find playerTank current value or all tanks current value
			string ptWhere = "";
			string bSumWhere = "";
			string bWhere = "";
            if (tankId != 0)
			{
				int playerTankId = TankHelper.GetPlayerTankId(tankId);
				ptWhere = " and pt.id=@playerTankId ";
				bSumWhere = " and playerTankId=@playerTankId ";
				bWhere = " where playerTankId=@playerTankId ";
				DB.AddWithValue(ref ptWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref bSumWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref bWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			}
            string battleModeWhere = "";
            if (chartMode != "ALL")
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
            
			// Find battles
            if (chartMode != "ALL")
                battleModeWhere = " and battleMode='" + chartMode + "' ";
            sql =
                "select battle.*, playerTank.tankId as tankId, battleTime as battle_time, battlesCount as battles_Count " +
                "from battle inner join " +
                "  playerTank on battle.playerTankId = playerTank.id and playerTank.playerId=@playerId " +
                battleModeWhere +
                GetBattleTimeFilter(bWhere) + " " +
                "order by battleTime DESC "; 
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtBattle = DB.FetchData(sql);
			double chartVal = 0;

			// Draw chart per date
			if (BattleChartHelper.Settings.Xaxis == "Date")
			{
				DateTime chartDate = DateTime.Now;
				double hourInterval = 24;
				if (dtBattle.Rows.Count > 0)
				{
					chartDate = Convert.ToDateTime(dtBattle.Rows[0]["battle_time"]);
					if (dtBattle.Rows.Count > 5000)
						hourInterval = 48;
					else if (dtBattle.Rows.Count > 10000)
						hourInterval = 24 * 7;
				}
				foreach (DataRow drBattle in dtBattle.Rows)
				{
					DateTime thisDate = Convert.ToDateTime(drBattle["battle_time"]);
					if (thisDate <= chartDate)
					{
                        chartVal = Math.Round(Code.Rating.WN8.CalcPlayerTankBattle(ptb), decimals);
						ChartingMain.Series[chartSerie].Points.AddXY(thisDate, chartVal); // Use battle date
						chartDate = thisDate.AddHours(-hourInterval);
					}
                    // Get prev val
                    DataRow[] ptbRow = ptb.Select("tankId = " + drBattle["tankId"].ToString());
					if (ptbRow.Length > 0)
					{
						ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) - Convert.ToInt32(drBattle["battlesCount"]);
						ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) - Convert.ToInt32(drBattle["dmg"]);
						ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) - Convert.ToInt32(drBattle["spotted"]);
						ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) - Convert.ToInt32(drBattle["frags"]);
						ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) - Convert.ToInt32(drBattle["def"]);
						ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) - Convert.ToInt32(drBattle["cap"]);
						ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) - Convert.ToInt32(drBattle["victory"]);
					}
				}
			}
			
            // Draw chart per battle
			else if (BattleChartHelper.Settings.Xaxis == "Battle")
			{
                // Get total number of battles
                int battleTotalCount = Convert.ToInt32(dtBattle.Compute("SUM(battlesCount)", "")) + 1;
                int step = 0;
				int stepMod = dtBattle.Rows.Count / numPoints;
				if (stepMod < 1) stepMod = 1;
				double battleCount = 0;
                // First point = Current val
                chartVal = Math.Round(Code.Rating.WN8.CalcPlayerTankBattle(ptb), decimals);
                ChartingMain.Series[chartSerie].Points.AddXY(battleTotalCount - battleCount, chartVal); 
                // Loop through battles and subtract values
                foreach (DataRow drBattle in dtBattle.Rows)
				{
                    battleCount += Convert.ToDouble(drBattle["battlesCount"]); // Use battle count
                    DataRow[] ptbRow = ptb.Select("tankId = " + drBattle["tankId"].ToString());
					if (ptbRow.Length > 0)
					{
						ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) - Convert.ToInt32(drBattle["battlesCount"]); ;
						ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) - Convert.ToInt32(drBattle["dmg"]);
						ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) - Convert.ToInt32(drBattle["spotted"]);
						ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) - Convert.ToInt32(drBattle["frags"]);
						ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) - Convert.ToInt32(drBattle["def"]);
						ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) - Convert.ToInt32(drBattle["cap"]);
						ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) - Convert.ToInt32(drBattle["victory"]);
					}
                    step++;
                    if (step % stepMod == 0)
                    {
                        chartVal = Math.Round(Code.Rating.WN8.CalcPlayerTankBattle(ptb), decimals);
                        ChartingMain.Series[chartSerie].Points.AddXY(battleTotalCount - battleCount, chartVal);
                    }
                }
                // Last point = First battle
                chartVal = Math.Round(Code.Rating.WN8.CalcPlayerTankBattle(ptb), decimals);
                ChartingMain.Series[chartSerie].Points.AddXY(battleTotalCount - battleCount, chartVal);
                // Force chart x-axis to start on 0
                ChartingMain.ChartAreas[0].AxisX.Minimum = 0;
            }
			Cursor = Cursors.Default;
		}
        
        private void DrawChartSeriesWN9PerTank(int tankId, string chartSerie, string chartOrder, string chartMode)
        {
            Cursor = Cursors.WaitCursor;
            // Find playerTank current value or all tanks current value
            string ptWhere = "";
            string bSumWhere = "";
            string bWhere = "";
            // Filter on tank
            int playerTankId = TankHelper.GetPlayerTankId(tankId);
            ptWhere = " and pt.id=@playerTankId ";
            bSumWhere = " and playerTankId=@playerTankId ";
            bWhere = " where playerTankId=@playerTankId ";
            DB.AddWithValue(ref ptWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
            DB.AddWithValue(ref bSumWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
            DB.AddWithValue(ref bWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
            // Filter on battlemode
            string battleModeWhere = "";
            if (chartMode != "ALL")
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
            if (BattleChartHelper.Settings.Xaxis == "Battle")
            {
                // Find first value by sutracting sum of recorded values
                if (chartMode != "ALL")
                    battleModeWhere = " and b.battleMode='" + chartMode + "' ";
                bSumWhere = "where t.expDmg is not null" + battleModeWhere + bSumWhere;
                sql =
                    "select t.id as tankId, sum(b.battlesCount) as battles, sum(b.dmg) as dmg, sum (b.spotted) as spot, sum (b.frags) as frags, " +
                    "  sum (b.def) as def, sum (cap) as cap, sum(victory) as wins " +
                    "from battle b left join " +
                    "  playerTank pt on b.playerTankId=pt.id and pt.playerId=@playerId left join " +
                    "  tank t on pt.tankId = t.id " +
                    GetBattleTimeFilter(bSumWhere) + " " +
                    "group by t.id ";
                DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
                DataTable dtBattleSum = DB.FetchData(sql);
                if (dtBattleSum.Rows.Count > 0)
                {
                    foreach (DataRow ptbRow in ptb.Rows)
                    {
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
            if (chartMode != "ALL")
                battleModeWhere = " and battleMode='" + chartMode + "' ";
            sql =
                "select battle.*, playerTank.tankId as tankId, battleTime as battle_time, battlesCount as battles_Count " +
                "from battle inner join " +
                "  playerTank on battle.playerTankId = playerTank.id and playerTank.playerId=@playerId " + battleModeWhere +
                GetBattleTimeFilter(bWhere) + " " +
                "order by battleTime " + chartOrder;
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DataTable dtChart = DB.FetchData(sql);
            double chartVal = 0;
            
            // If per date
            if (BattleChartHelper.Settings.Xaxis == "Date")
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
                        ChartingMain.Series[chartSerie].Points.AddXY(thisDate, chartVal); // Use battle date
                        chartDate = thisDate.AddHours(-hourInterval);
                    }
                    // Get prev val
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
            else if (BattleChartHelper.Settings.Xaxis == "Battle")
            {
                int step = 0;
                int stepMod = dtChart.Rows.Count / numPoints;
                if (stepMod < 1) stepMod = 1;
                double battleCount = 0;
                foreach (DataRow bRow in dtChart.Rows)
                {
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
                        ChartingMain.Series[chartSerie].Points.AddXY(battleCount, chartVal); // Use battle count
                    }
                }
            }
            Cursor = Cursors.Default;
        }

        private void DrawChartSeriesWN9ForAccount(string chartSerie, string chartOrder, string chartMode)
        {
            // Start
            Cursor = Cursors.WaitCursor;

            // Done
            Cursor = Cursors.Default;

            MsgBox.Show("Not yet implemented, try WN9 per tank", "Not implemented");
            return;
            
        }

        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();

        private void ChartingMain_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                string XLabel = BattleChartHelper.Settings.Xaxis + ": ";
                var pos = e.Location;
                if (prevPosition.HasValue && pos == prevPosition.Value)
                    return;
                tooltip.RemoveAll();
                prevPosition = pos;
                var results = ChartingMain.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
                foreach (var result in results)
                {
                    if (result.ChartElementType == ChartElementType.DataPoint)
                    {
                        var prop = result.Object as DataPoint;
                        if (prop != null)
                        {
                            var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                            var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);
                            var pointY2Pixel = result.ChartArea.AxisY2.ValueToPixelPosition(prop.YValues[0]);

                            // check if the cursor is really close to the point (2 pixels around the point)
                            if (Math.Abs(pos.X - pointXPixel) < 2 && 
                                (Math.Abs(pos.Y - pointYPixel) < 2 || Math.Abs(pos.Y - pointY2Pixel) < 2))
                            {
                                string TankName = result.Series.Name;
                                string YValue = prop.YValues[0].ToString();
                                string XValue = prop.XValue.ToString();
                                if (BattleChartHelper.Settings.Xaxis == "Date")
                                {
                                    XValue = DateTime.FromOADate((double)prop.XValue).ToString("dd.MM.yyyy");
                                }
                                tooltip.Show(
                                    TankName + Environment.NewLine +
                                    XLabel + XValue + Environment.NewLine + 
                                    "Value: " + YValue, 
                                    this.ChartingMain, pos.X + 10, pos.Y);
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

        #endregion

        #region Toolbar events

        // Select Spline
        private void mSpline_Click(object sender, EventArgs e)
        {
            mSpline.Checked = !mSpline.Checked;
            BattleChartHelper.Settings.Spline = mSpline.Checked;
            // Update chart view
            DrawCurrentChartView();
            if (mSpline.Checked)
                lblFooter.Text = "Selected Spline (curved line)";
            else
                lblFooter.Text = "Selected Line";
        }

        // Select Bullet
        private void mBullet_Click(object sender, EventArgs e)
        {
            mBullet.Checked = !mBullet.Checked;
            BattleChartHelper.Settings.Bullet = mBullet.Checked;
            // Update chart view
            DrawCurrentChartView();
            if (mBullet.Checked)
                lblFooter.Text = "Selected Bullets";
            else
                lblFooter.Text = "Removed Bullets";
        }

        // Selected X-Axis
        private void mXaxis_Click(object sender, EventArgs e)
        {
            ToolStripButton button = (ToolStripButton)sender;
            if (!button.Checked)
            {
                mXaxisBattle.Checked = false;
                mXaxisDate.Checked = false;
                button.Checked = true;
                BattleChartHelper.Settings.Xaxis = button.Text;
                // Update chart view
                DrawCurrentChartView();
                lblFooter.Text = "Selected X-Axis";
            }
        }

        private void SetBattleTimeFilterMenu()
        {
            ToolStripItemCollection battleModeMenuList = mBattleTimeFilter.DropDownItems;
            foreach (var item in battleModeMenuList)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem menuItem = (ToolStripMenuItem)item;
                    menuItem.Checked = (menuItem.Tag.ToString() == BattleChartHelper.Settings.BattleTime);
                    if (menuItem.Checked)
                        mBattleTimeFilter.Text = menuItem.Text;
                }
            }
        }
               

        private void SetBattleModeMenu()
        {
            string btlMode = BattleChartHelper.Settings.BattleMode;
            if (btlMode == "ALL") btlMode = "";
            mBattleModes.Text = BattleMode.GetItemFromSqlName(btlMode).Name;
            ToolStripItemCollection battleModeMenuList = mBattleModes.DropDownItems;
            foreach (var item in battleModeMenuList)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem menuItem = (ToolStripMenuItem)item;
                    menuItem.Checked = (menuItem.Tag.ToString() == BattleChartHelper.Settings.BattleMode);
                }
            }
        }

        // Selected Battle Time Filter
        private void mBattleTimeChanged_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            BattleChartHelper.Settings.BattleTime = menu.Tag.ToString();
            SetBattleTimeFilterMenu();
            // Update chart view
            DrawCurrentChartView();
            lblFooter.Text = "Selected Battle Time: " + menu.Text;
        }

        // Select battle mode
        private void mBattleModesChanged_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            BattleChartHelper.Settings.BattleMode = menu.Tag.ToString();
            SetBattleModeMenu();
            // Update chart view
            DrawCurrentChartView();
            lblFooter.Text = "Selected Battle Mode: " + menu.Text;
        }

        private void ClearChartArea(bool useTwoYaxis)
        {
            ChartingMain.Series.Clear();
            ChartingMain.ResetAutoValues();
            if (useTwoYaxis)
            {
                ChartingMain.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                ChartingMain.ChartAreas[0].InnerPlotPosition = new ElementPosition(4, 0, 92, 91); // not so wide to allow 2nd Yxis to right
            }
            else
            {
                ChartingMain.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                ChartingMain.ChartAreas[0].InnerPlotPosition = new ElementPosition(4, 0, 96, 91); // wide
            }
        }

        // Close form
        private void BattleChart_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormHelper.ClosedOne();
        }

        // Clear Chart
        private void mChartClear_Click(object sender, EventArgs e)
        {
            BattleChartHelper.CurrentChartView = new List<BattleChartHelper.BattleChartItem>();
            ClearChartArea(false);
            lblFooter.Text = "Chart cleared";
        }

        // Refresh
        private void mRefresh_Click(object sender, EventArgs e)
        {
            // Update chart view
            DrawCurrentChartView();
            lblFooter.Text = "Refreshed chart";
        }

        // Seleced menu button for adding new chart line
        private void mChartAdd_Click(object sender, EventArgs e)
        {
            OpenFormSelectChartParameters();
        }

        // Open form for adding new chart line
        private void OpenFormSelectChartParameters(int tankId = 0)
        {
            Form frm = new ChartLineAdd(tankId);
            frm.ShowDialog();
            if (BattleChartHelper.NewChartItem.Count > 0)
            {
                // Add new chart items to chart view
                BattleChartHelper.CurrentChartView.AddRange(BattleChartHelper.NewChartItem);
                // Add the new chart item to current chart
                DrawCurrentChartView();
                // Chage to unsaved
                mFavouriteSave.Image = imageListToolStrip.Images[1];
                mFavouriteSave.ToolTipText = "Update or Save as new Favourite (chart values unsaved)";

            }
            lblFooter.Text = "Added chart values";
        }

        private void mChartRemove_Click(object sender, EventArgs e)
        {
            if (BattleChartHelper.CurrentChartView.Count == 0)
            {
                MsgBox.Show("There are no chart values to remove");
                return;
            }
            Form frm = new Forms.ChartLineRemove();
            frm.ShowDialog();
            if (BattleChartHelper.RemovedChartValues > 0)
            {
                DrawCurrentChartView();
                lblFooter.Text = "Removed " + BattleChartHelper.RemovedChartValues + " chart value";
                if (BattleChartHelper.RemovedChartValues > 1)
                    lblFooter.Text += "s";
                // Chage to unsaved
                mFavouriteSave.Image = imageListToolStrip.Images[1];
                mFavouriteSave.ToolTipText = "Update or Save as new Favourite (chart values unsaved)";
            }
        }

        private void mCheckBox_Paint(object sender, PaintEventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            if (menu.Checked)
            {
                // Default checkbox
                e.Graphics.DrawImage(Properties.Resources.menucheck, 5, 3);
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 4, 2, 17, 17);
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 5, 3, 15, 15);
            }
        }

        #endregion

        #region Favourites


        private void SetFavouritMenu()
        {
            ResetFavouriteMenu();
            string sql = "SELECT favouriteName FROM chartFavourite GROUP BY favouriteName ORDER BY favouriteName;";
            DataTable dt = DB.FetchData(sql);
            int count = dt.Rows.Count;
            if (count > 15)
                count = 15;
            mFavourite.Visible = (count > 0);
            mFavouriteEdit.Visible = (count > 0);
            if (count == 0)
                mFavouriteSave.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            else
                mFavouriteSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            for (int i = 1; i <= count; i++)
            {
                ToolStripItem[] menu = mFavourite.DropDownItems.Find("mFavourite" + i.ToString("00"), false);
                menu[0].Text = dt.Rows[i-1][0].ToString();
                menu[0].Visible = true;
            }
        }

        private void ResetFavouriteMenu()
        {
            for (int i = 1; i <= 15; i++)
            {
                ToolStripItem[] menu = mFavourite.DropDownItems.Find("mFavourite" + i.ToString("00"), false);
                menu[0].Visible = false;
            }
        }

        private void mFavouriteSelect_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            GetFavouriteChart(menu.Text);
        }

        private void GetFavouriteChart(string favName)
        {
            // Clear current charts
            BattleChartHelper.CurrentChartView = new List<BattleChartHelper.BattleChartItem>();
            // Get favourite
            string sql = "SELECT * FROM chartFavourite WHERE favouriteName = @favouriteName ORDER BY tankId, chartTypeName;";
            DB.AddWithValue(ref sql, "@favouriteName", favName, DB.SqlDataType.VarChar);
            DataTable dt = DB.FetchData(sql);
            // Read into chart if exists
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BattleChartHelper.BattleChartItem item = new BattleChartHelper.BattleChartItem();
                    item.chartTypeName = dr["chartTypeName"].ToString();
                    item.tankId = Convert.ToInt32(dr["tankId"]);
                    item.tankName = TankHelper.GetTankName(item.tankId, true);
                    item.use2ndYaxis = Convert.ToBoolean(dr["use2ndYaxis"]);
                    BattleChartHelper.CurrentChartView.Add(item);
                }
                DrawCurrentChartView();
                mFavourite.Text = favName;
                mFavouriteEdit.Enabled = true;
            }
            else
            {
                ClearChartArea(false);
            }
            // Set menu checked
            for (int i = 1; i <= 15; i++)
            {
                ToolStripItem[] menu = mFavourite.DropDownItems.Find("mFavourite" + i.ToString("00"), false);
                ToolStripMenuItem menuItem = (ToolStripMenuItem)menu[0];
                menuItem.Checked = (menuItem.Text == favName);
            }
            // Save status and footer
            mFavouriteSave.Image = imageListToolStrip.Images[0];
            mFavouriteSave.ToolTipText = "Update or Save as new Favourite";
            lblFooter.Text = "Showing Favourite Chart: " + favName;
        }

        private void mFavouriteSave_Click(object sender, EventArgs e)
        {
            SaveFavourite();
        }

        private void SaveFavourite()
        {
            if (BattleChartHelper.CurrentChartView.Count == 0)
            {
                MsgBox.Show("You have to add least one chart value before it is possible to save it as a favourite");
                return;
            }
            string currentFavourite = mFavourite.Text;
            Form frm = new Chart.FavouriteSave(currentFavourite);
            frm.ShowDialog();
            if (BattleChartHelper.SaveFavouriteSaved)
            {
                if (BattleChartHelper.SaveFavouriteNewFavName == currentFavourite)
                {
                    lblFooter.Text = "Updated current Favourite: " + currentFavourite;
                }
                else
                {
                    lblFooter.Text = "Saved as new Favourite: " + BattleChartHelper.SaveFavouriteNewFavName;
                    SetFavouritMenu();
                }
                mFavourite.Text = BattleChartHelper.SaveFavouriteNewFavName;
                // Saved
                mFavouriteSave.Image = imageListToolStrip.Images[0];
                mFavouriteSave.ToolTipText = "Update or Save as new Favourite";

            }
        }

        private void mFavouriteEdit_Click(object sender, EventArgs e)
        {
            Form frm = new Chart.FavouriteEdit(mFavourite.Text);
            frm.ShowDialog();
            if (BattleChartHelper.EditFavouriteDeleted || BattleChartHelper.EditFavouriteEdited != "")
            {
                SetFavouritMenu();
                if (BattleChartHelper.EditFavouriteDeleted)
                {
                    mFavourite.Text = "";
                    mFavouriteEdit.Enabled = false;
                    lblFooter.Text = "Deleted favourite";
                }
                else
                {
                    mFavourite.Text = BattleChartHelper.EditFavouriteEdited;
                    lblFooter.Text = "Changed name on favourite";
                }
            }
        }

        #endregion

        private void BattleChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mFavouriteSave.ToolTipText == "Update or Save as new Favourite (chart values unsaved)" && BattleChartHelper.CurrentChartView.Count > 0)
            {
                MsgBox.Button answer = MsgBox.Show("Chart is edited but not saved as favourite, save now?", "Save Favourite", MsgBox.Type.YesNo);
                if (answer == MsgBox.Button.Yes)
                {
                    SaveFavourite();
                }
            }

            if (mFavourite.Text != Config.Settings.currentChartFavourite && mFavourite.Text != "")
            {
                Config.Settings.currentChartFavourite = mFavourite.Text;
                string msg = "";
                Config.SaveConfig(out msg);
            }

        }
    }

}
