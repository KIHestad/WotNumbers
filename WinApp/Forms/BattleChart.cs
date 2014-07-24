using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class BattleChart : Form
	{
		string ddTankList = "";
		string ddChartList = "";
		int initPlayerTankId = 0;
		int numPoints = 100; // Max num of points in chart, exept for battle values (ChatValues.totals = false)
		private double axisYminimum = 999999999;

		public BattleChart(int playerTankId = 0)
		{
			InitializeComponent();
			initPlayerTankId = playerTankId;
		}

		private class ChartValue
		{
			public string name = "";	// Name of Chart VAlue in dropdown 
			public string ptbCol = "";	// playerTankBattle column to get current value 
			public string bCol = "";	// battle column to get battle value
			public bool totals = true;		// show totals, not actual battle values
		}

		private List<ChartValue> chartValues = new List<ChartValue>();

		private void AddChartValues()
		{
			chartValues.Add(new ChartValue()
			{
				name = "Win Rate",
				ptbCol = "",
				bCol = ""
			});

			chartValues.Add(new ChartValue()
			{
				name = "Total WN8",
				ptbCol = "battles",
				bCol = "battlesCount"
			});

			chartValues.Add(new ChartValue()
			{
				name = "Battle WN8",
				ptbCol = "wn8",
				bCol = "wn8",
				totals = false
			});

			chartValues.Add(new ChartValue()
			{
				name = "Total EFF",
				ptbCol = "battles",
				bCol = "battlesCount"
			});

			chartValues.Add(new ChartValue()
			{
				name = "Battle EFF",
				ptbCol = "eff",
				bCol = "eff",
				totals = false
			});



			chartValues.Add(new ChartValue()
			{
				name = "Total XP",
				ptbCol = "xp",
				bCol = "xp"
			});

			chartValues.Add(new ChartValue()
			{
				name = "Total Damage",
				ptbCol = "dmg",
				bCol = "dmg"
			});

			chartValues.Add(new ChartValue() 
			{ 
				name = "Battle Count", 
				ptbCol = "battles", 
				bCol= "battlesCount" 
			});

			chartValues.Add(new ChartValue()
			{
				name = "Victory Count",
				ptbCol = "wins",
				bCol = "victory"
			});

			chartValues.Add(new ChartValue()
			{
				name = "Draw Count",
				ptbCol = "(battles - wins - losses)",
				bCol = "draw"
			});

			chartValues.Add(new ChartValue()
			{
				name = "Defeat Count",
				ptbCol = "losses",
				bCol = "defeat"
			});

			chartValues.Add(new ChartValue()
			{
				name = "Frag Count",
				ptbCol = "frags",
				bCol = "frags"
			});



		}

		private void BattleChart_Load(object sender, EventArgs e)
		{
			// Init showing tank or all tanks
			string sql = "";
			if (initPlayerTankId != 0)
			{
				sql = "select name from tank inner join playerTank on tank.id=playerTank.tankId where playerTank.id=@id order by tank.name; ";
				DB.AddWithValue(ref sql, "@id", initPlayerTankId, DB.SqlDataType.Int);
				ddTank.Text = DB.FetchData(sql).Rows[0][0].ToString();
			}
			else
			{
				ddTank.Text = "( All Tanks )";
			}
			// Available charts
			AddChartValues();
			// DropDown Tank List
			sql = "select tank.name from tank inner join playerTank on tank.id = playerTank.tankId where playerTank.playerId=@playerId order by tank.name";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			ddTankList = "( All Tanks )";
			foreach (DataRow dr in dt.Rows)
			{
				ddTankList += "," + dr["name"].ToString();
			}
			// DropDown Chart Values
			foreach (ChartValue c in chartValues)
			{
				ddChartList += c.name + ",";
			}
			ddChartList = ddChartList.Substring(0, ddChartList.Length - 1); // Remove last comma
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

		private double SetYaxisLowestValue(double newValue)
		{
			if (newValue < axisYminimum)
			{
				if (newValue <= 100)
					return Convert.ToDouble(Convert.ToInt32(newValue *10)) / 10;
				else
					return newValue.RoundDown(1);
			}
			else
				return axisYminimum;
		}

		private void DrawChart()
		{
			// Init
			string tankName = ddTank.Text;
			if (tankName == "( All Tanks )") tankName = "All Tanks";
			string selectedChartValue = ddValue.Text;
			string chartSerie = tankName + " - " + selectedChartValue;
			string chartOrder = "";
			// Check if value selected
			if (ddValue.Text == "")
			{
				Code.MsgBox.Show("Please select a chart value, this is the data that shows up in the chart.", "Missing Chart Value", this);
				return;
			}
			// Check if already shown
			foreach (Series serie in ChartingMain.Series)
			{
				if (serie.Name == chartSerie) 
					return;
			}
			// Get Chart Value selected
			ChartValue chartValue = chartValues.Find(c => c.name == selectedChartValue);
			// Add series
			Series newSerie = new Series(chartSerie);
			//newSerie.AxisLabel = tankName;
			if (chartValue.totals)
				newSerie.ChartType = SeriesChartType.Line; //.FastLine;
			else
				newSerie.ChartType = SeriesChartType.Point;
			if (ddXaxis.Text == "Date")
			{
				ChartingMain.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Auto;
				ChartingMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
				newSerie.XValueType = ChartValueType.DateTime;
				newSerie.MarkerStyle = MarkerStyle.Circle;
				chartOrder = "DESC";
			}
			else if (ddXaxis.Text == "Battle")
			{
				ChartingMain.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;
				ChartingMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
				newSerie.XValueType = ChartValueType.Int32;
				newSerie.MarkerStyle = MarkerStyle.Circle;
			}
			ChartingMain.Series.Add(newSerie);
			// Special calculations for calculated columns
			switch (selectedChartValue)
			{
				case "Win Rate": DrawChartWinRate(tankName, chartSerie, chartOrder); return;
				case "Total WN8": DrawChartWN8(tankName, chartSerie, chartOrder); return;
				case "Total EFF": DrawChartEFF(tankName, chartSerie, chartOrder); return; 
			}
			// Find playerTank current value or all tanks current value
			double currentValue = 0;
			double firstValue = 0;
			string sql = "";
			string where = "";
			// Get Standard battle data for non-calculated values
			if (tankName != "All Tanks")
			{
				// Find playertank and current value
				int playerTankId = TankData.GetPlayerTankId(tankName);
				where = " where playerTankId=@playerTankId ";
				DB.AddWithValue(ref where, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			}
			sql = 
				"select SUM(" + chartValue.ptbCol + ") " +
				"from playerTankBattle inner join playerTank on playerTankBattle.playerTankId=playerTank.id and playerTank.playerId=@playerId " +
				where;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			currentValue = Convert.ToDouble(DB.FetchData(sql).Rows[0][0]);
			if (ddXaxis.Text == "Battle")
			{
				// Find first value bu subtracting sum of recorded values
				sql = 
					"select sum(" + chartValue.bCol + ") " +
					"from battle inner join playerTank on battle.playerTankId=playerTank.id and playerTank.playerId=@playerId " + 
					FilterPeriod(where);
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable dtSum = DB.FetchData(sql);
				if (dtSum.Rows.Count > 0)
					if (dtSum.Rows[0][0] != DBNull.Value)
						firstValue = currentValue - Convert.ToDouble(dtSum.Rows[0][0]);
			}
			// Find battles
			sql = 
				"select * " +
				"from battle inner join playerTank on battle.playerTankId=playerTank.id and playerTank.playerId=@playerId " +
				FilterPeriod(where) + " " +
				"order by battleTime " + chartOrder;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtChart = DB.FetchData(sql);
			int step = 0;
			int stepMod = dtChart.Rows.Count / numPoints;
			if (stepMod < 1) stepMod = 1;
			if (ddXaxis.Text == "Date")
			{
				if (chartValue.totals)
				{
					foreach (DataRow dr in dtChart.Rows)
					{
						step++;
						if (step % stepMod == 0)
							ChartingMain.Series[chartSerie].Points.AddXY(Convert.ToDateTime(dr["battleTime"]), Math.Round(currentValue, 2)); // Use battle date
						currentValue -= Convert.ToDouble(dr[chartValue.bCol]); //  Move backwards
						axisYminimum = SetYaxisLowestValue(currentValue);
					}
				}
				else
				{
					foreach (DataRow dr in dtChart.Rows)
					{
						currentValue = Convert.ToDouble(dr[chartValue.bCol]); //  Move backwards
						ChartingMain.Series[chartSerie].Points.AddXY(Convert.ToDateTime(dr["battleTime"]), Math.Round(currentValue, 2)); // Use battle date
						axisYminimum = SetYaxisLowestValue(currentValue);
					}
				}
			}
			else if (ddXaxis.Text == "Battle")
			{
				double battleCount = 0;
				if (chartValue.totals)
				{
					foreach (DataRow dr in dtChart.Rows)
					{
						battleCount += Convert.ToDouble(dr["battlesCount"]); // Use battle count
						firstValue += Convert.ToDouble(dr[chartValue.bCol]); // Move forwards
						axisYminimum = SetYaxisLowestValue(firstValue);
						if (firstValue < axisYminimum) axisYminimum = Convert.ToInt32(firstValue);
						step++;
						if (step % stepMod == 0)
							ChartingMain.Series[chartSerie].Points.AddXY(battleCount, Math.Round(firstValue, 2));
					}
				}
				else
				{
					foreach (DataRow dr in dtChart.Rows)
					{
						battleCount += Convert.ToDouble(dr["battlesCount"]); // Use battle count
						firstValue = Convert.ToDouble(dr[chartValue.bCol]); // Move forwards
						axisYminimum = SetYaxisLowestValue(firstValue);
						ChartingMain.Series[chartSerie].Points.AddXY(battleCount, Math.Round(firstValue, 2));
					}
				}
			}
			ChartingMain.ChartAreas[0].AxisY.Minimum = axisYminimum;
		}

		private void DrawChartWinRate(string tankName, string chartSerie, string chartOrder)
		{
			// ChartingMain.ChartAreas[0].AxisY.Interval = 1;
			// Find playerTank current value or all tanks current value
			string where = "";
			if (tankName != "All Tanks")
			{
				// Find playertank and current value
				int playerTankId = TankData.GetPlayerTankId(tankName);
				where = " where playerTankId=@playerTankId ";
				DB.AddWithValue(ref where, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			}
			string sql = 
				"select SUM(wins), SUM(battles) " +
				"from playerTankBattle inner join playerTank on playerTankBattle.playerTankId=playerTank.id and playerTank.playerId=@playerId " +
				where;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			double currentWins = Convert.ToInt32(DB.FetchData(sql).Rows[0][0]);
			double currentBattles = Convert.ToInt32(DB.FetchData(sql).Rows[0][1]);
			double firstWins = 0;
			double firstBattles = 0;
			if (ddXaxis.Text == "Battle")
			{
				// Find first value by sutracting sum of recorded values
				sql = 
					"select sum(victory), sum(battlescount) " +
					"from battle inner join playerTank on battle.playerTankId=playerTank.id and playerTank.playerId=@playerId " + 
					FilterPeriod(where);
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable dtSum = DB.FetchData(sql);
				if (dtSum.Rows.Count > 0)
				{
					if (dtSum.Rows[0][0] != DBNull.Value)
						firstWins = currentWins - Convert.ToDouble(dtSum.Rows[0][0]);
					if (dtSum.Rows[0][1] != DBNull.Value)
						firstBattles = currentBattles - Convert.ToDouble(dtSum.Rows[0][1]);
				}
			}
			// Find battles
			sql = 
				"select * " +
				"from battle inner join playerTank on battle.playerTankId=playerTank.id and playerTank.playerId=@playerId " +
				FilterPeriod(where) + " " +
				"order by battleTime " + chartOrder;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtChart = DB.FetchData(sql);
			int step = 0;
			int stepMod = dtChart.Rows.Count / numPoints;
			if (stepMod < 1) stepMod = 1;
			double winRate = 0;
			if (ddXaxis.Text == "Date")
			{
				foreach (DataRow dr in dtChart.Rows)
				{
					winRate = Math.Round(currentWins * 100 / currentBattles,2);
					axisYminimum = SetYaxisLowestValue(winRate);
					step++;
					if (step % stepMod == 0)
						ChartingMain.Series[chartSerie].Points.AddXY(Convert.ToDateTime(dr["battleTime"]), winRate); // Use battle date
					currentWins -= Convert.ToDouble(dr["victory"]); //  Move backwards
					currentBattles -= Convert.ToDouble(dr["battlesCount"]);
				}
			}
			else if (ddXaxis.Text == "Battle")
			{
				double battleCount = 0;
				foreach (DataRow dr in dtChart.Rows)
				{
					battleCount += Convert.ToDouble(dr["battlesCount"]); // Use battle count
					firstWins += Convert.ToDouble(dr["victory"]); // Move forwards
					firstBattles += Convert.ToDouble(dr["battlesCount"]);
					winRate = Math.Round(firstWins * 100 / firstBattles,2);
					axisYminimum = SetYaxisLowestValue(winRate);
					step++;
					if (step % stepMod == 0)
						ChartingMain.Series[chartSerie].Points.AddXY(battleCount, winRate);
				}
			}
			ChartingMain.ChartAreas[0].AxisY.Minimum = axisYminimum;
		}

		private void DrawChartEFF(string tankName, string chartSerie, string chartOrder)
		{
			// Find playerTank current value or all tanks current value
			string ptWhere = "";
			string bWhere = "";
			if (tankName != "All Tanks")
			{
				int playerTankId = TankData.GetPlayerTankId(tankName);
				ptWhere = " where pt.id=@playerTankId ";
				bWhere = " where playerTankId=@playerTankId ";
				DB.AddWithValue(ref ptWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref bWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			}
			string sql = 
				"select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
				"  sum (ptb.def) as def, sum (ptb.cap) as cap, sum(t.tier * ptb.battles) as tier " +
				"from playerTankBattle ptb inner join " +
				"  playerTank pt on ptb.playerTankId=pt.id  and pt.playerId=@playerId  inner join " +
				"  tank t on pt.tankId = t.id " +
				ptWhere;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataRow ptbRow = DB.FetchData(sql).Rows[0];
			double BATTLES = Convert.ToDouble(ptbRow["battles"]);
			double DAMAGE = Convert.ToDouble(ptbRow["dmg"]);
			double SPOT = Convert.ToDouble(ptbRow["spot"]);
			double FRAGS = Convert.ToDouble(ptbRow["frags"]);
			double DEF = Convert.ToDouble(ptbRow["def"]);
			double CAP = Convert.ToDouble(ptbRow["cap"]);
			double TIER = Convert.ToDouble(ptbRow["tier"]);
			if (ddXaxis.Text == "Battle")
			{
				// Find first value by sutracting sum of recorded values
				sql = 
					"select sum(b.battlesCount) as battles, sum(b.dmg) as dmg, sum (b.spotted) as spot, sum (b.frags) as frags, " +
					"  sum (b.def) as def, sum (cap) as cap, sum(t.tier * b.battlesCount) as tier " +
					"from battle b inner join " +
					"  playerTank pt on b.playerTankId=pt.id and pt.playerId=@playerId inner join " +
					"  tank t on pt.tankId = t.id " +
					FilterPeriod(ptWhere);
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable dtBattleSum = DB.FetchData(sql);
				if (dtBattleSum.Rows.Count > 0 && dtBattleSum.Rows[0]["battles"] != DBNull.Value)
				{
					DataRow bRow = dtBattleSum.Rows[0];
					BATTLES = BATTLES - Convert.ToDouble(bRow["battles"]);
					DAMAGE = DAMAGE - Convert.ToDouble(bRow["dmg"]);
					SPOT = SPOT - Convert.ToDouble(bRow["spot"]);
					FRAGS = FRAGS - Convert.ToDouble(bRow["frags"]);
					DEF = DEF - Convert.ToDouble(bRow["def"]);
					CAP = CAP - Convert.ToDouble(bRow["cap"]);
					TIER = TIER - Convert.ToDouble(bRow["tier"]);
				}
			}
			// Find battles
			sql = 
				"select * " +
				"from battle inner join playerTank on battle.playerTankId=playerTank.id and playerTank.playerId=@playerId " +
				FilterPeriod(bWhere) + " " +
				"order by battleTime " + chartOrder;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtChart = DB.FetchData(sql);
			int step = 0;
			int stepMod = dtChart.Rows.Count / numPoints;
			if (stepMod < 1) stepMod = 1;
			double EFF = 0;
			double defaultTIER = 0;
			if (tankName == "All Tanks")
				defaultTIER = TIER / BATTLES;
			else
				defaultTIER = TankData.GetTankTier(tankName);
			if (ddXaxis.Text == "Date")
			{
				foreach (DataRow dr in dtChart.Rows)
				{
					EFF = Math.Round(Code.Rating.CalculateEFF(BATTLES, DAMAGE, SPOT, FRAGS, DEF, CAP, defaultTIER),2);
					axisYminimum = SetYaxisLowestValue(EFF);
					step++;
					if (step % stepMod == 0)
						ChartingMain.Series[chartSerie].Points.AddXY(Convert.ToDateTime(dr["battleTime"]), EFF); // Use battle date
					BATTLES -= Convert.ToDouble(dr["battlesCount"]);
					DAMAGE -= Convert.ToDouble(dr["dmg"]);
					SPOT -= Convert.ToDouble(dr["spotted"]);
					FRAGS -= Convert.ToDouble(dr["frags"]);
					DEF -= Convert.ToDouble(dr["def"]);
					CAP -= Convert.ToDouble(dr["cap"]);
				}
			}
			else if (ddXaxis.Text == "Battle")
			{
				double battleCount = 0;
				foreach (DataRow dr in dtChart.Rows)
				{
					battleCount += Convert.ToDouble(dr["battlesCount"]); // Use battle count
					BATTLES += Convert.ToDouble(dr["battlesCount"]);
					DAMAGE += Convert.ToDouble(dr["dmg"]);
					SPOT += Convert.ToDouble(dr["spotted"]);
					FRAGS += Convert.ToDouble(dr["frags"]);
					DEF += Convert.ToDouble(dr["def"]);
					CAP += Convert.ToDouble(dr["cap"]);
					EFF = Math.Round(Code.Rating.CalculateEFF(BATTLES, DAMAGE, SPOT, FRAGS, DEF, CAP, defaultTIER),2);
					axisYminimum = SetYaxisLowestValue(EFF);
					step++;
					if (step % stepMod == 0)
						ChartingMain.Series[chartSerie].Points.AddXY(battleCount, EFF); // Use battle date
				}
			}
			ChartingMain.ChartAreas[0].AxisY.Minimum = axisYminimum;
		}

		private void DrawChartWN8(string tankName, string chartSerie, string chartOrder)
		{
			Cursor = Cursors.WaitCursor;
			// Find playerTank current value or all tanks current value
			string ptWhere = "";
			string bSumWhere = "";
			string bWhere = "";
			if (tankName != "All Tanks")
			{
				int playerTankId = TankData.GetPlayerTankId(tankName);
				ptWhere = " and pt.id=@playerTankId ";
				bSumWhere = " and playerTankId=@playerTankId ";
				bWhere = " where playerTankId=@playerTankId ";
				DB.AddWithValue(ref ptWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref bSumWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref bWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			}
			string sql =
				"select t.id as tankId, sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
				"  sum (ptb.def) as def, sum (ptb.cap) as cap, sum(wins) as wins " +
				"from playerTankBattle ptb left join " +
				"  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId left join " +
				"  tank t on pt.tankId = t.id " +
				"where t.expDmg is not null " + ptWhere + " " +
				"group by t.id, t.expDmg, t.expSpot, t.expFrags, t.expDef, t.expWR  ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable ptb = DB.FetchData(sql);
			if (ddXaxis.Text == "Battle")
			{
				// Find first value by sutracting sum of recorded values
				bSumWhere = "where t.expDmg is not null" + bSumWhere;
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
			sql = 
				"select battle.*, playerTank.tankId as tankId " +
				"from battle inner join " +
				"  playerTank on battle.playerTankId = playerTank.id and playerTank.playerId=@playerId " +
				FilterPeriod(bWhere) + " " + 
				"order by battleTime " + chartOrder;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtChart = DB.FetchData(sql);
			int step = 0;
			int stepMod = dtChart.Rows.Count / numPoints;
			if (stepMod < 1) stepMod = 1;
			double WN8 = 0;
			if (ddXaxis.Text == "Date")
			{
				foreach (DataRow bRow in dtChart.Rows)
				{
					WN8 = Math.Round(Code.Rating.CalculatePlayerTankTotalWN8(ptb), 2);
					axisYminimum = SetYaxisLowestValue(WN8);
					step++;
					if (step % stepMod == 0) 
						ChartingMain.Series[chartSerie].Points.AddXY(Convert.ToDateTime(bRow["battleTime"]), WN8); // Use battle date
					string tankId = bRow["tankId"].ToString();
					DataRow[] ptbRow = ptb.Select("tankId = " + tankId);
					if (ptbRow.Length > 0)
					{
						ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) - Convert.ToInt32(bRow["battlesCount"]);
						ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) - Convert.ToInt32(bRow["dmg"]);
						ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) - Convert.ToInt32(bRow["spotted"]);
						ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) - Convert.ToInt32(bRow["frags"]);
						ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) - Convert.ToInt32(bRow["def"]);
						ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) - Convert.ToInt32(bRow["cap"]);
						ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) - Convert.ToInt32(bRow["victory"]);
					}
				}
			}
			else if (ddXaxis.Text == "Battle")
			{
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
					WN8 = Math.Round(Code.Rating.CalculatePlayerTankTotalWN8(ptb), 2);
					axisYminimum = SetYaxisLowestValue(WN8);
					step++;
					if (step % stepMod == 0)
						ChartingMain.Series[chartSerie].Points.AddXY(battleCount, WN8); // Use battle count
				}
			}
			ChartingMain.ChartAreas[0].AxisY.Minimum = axisYminimum;
			Cursor = Cursors.Default;
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
					Code.MsgBox.Button answer = MsgBox.Show("Changing x-axis require chart to be cleared", "Clear Chart?", MsgBoxType.OKCancel, this);
					if (answer == MsgBox.Button.OKButton)
					{
						ResetChart();
					}
					else
					{
						ddXaxis.Text = selectedXaxis;
					}
				}
			}
		}


		private void btnAddChart_Click(object sender, EventArgs e)
		{
			DrawChart();
		}

		private void ddTank_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(ddTank, Code.DropDownGrid.DropDownGridType.List, ddTankList);
		}

		private void ddValue_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(ddValue, Code.DropDownGrid.DropDownGridType.List, ddChartList);
		}

		private void btnClearChart_Click(object sender, EventArgs e)
		{
			ResetChart();
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
			catch (Exception)
			{
				//throw;
			}
		}
	}
}
