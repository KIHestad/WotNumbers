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
				name = "EFF",
				ptbCol = "battles",
				bCol = "battlesCount"
			});

			chartValues.Add(new ChartValue()
			{
				name = "WN8",
				ptbCol = "battles",
				bCol = "battlesCount"
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
				sql = "select name from tank inner join playerTank on tank.id=playerTank.tankId where playerTank.id=@id; ";
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
			sql = "select tank.name from tank inner join playerTank on tank.id = playerTank.tankId where playerTank.playerId=@playerId order by lastBattleTime DESC";
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

		private int axisYminimum = 10000;

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
				Code.MsgBox.Show("Please select a chart value, this is the data that shows up in the chart.", "Missing Chart Value");
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
			newSerie.ChartType = SeriesChartType.Line;
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
				newSerie.MarkerStyle = MarkerStyle.None;
			}
			ChartingMain.Series.Add(newSerie);
			// Special calculations for calculated columns
			switch (selectedChartValue)
			{
				case "Win Rate": DrawChartWinRate(tankName, chartSerie, chartOrder); return;
				case "WN8": return;
				case "EFF": DrawChartEFF(tankName, chartSerie, chartOrder); return; 
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
			sql = "select SUM(" + chartValue.ptbCol + ") from playerTankBattle " + where;
			currentValue = Convert.ToDouble(DB.FetchData(sql).Rows[0][0]);
			if (ddXaxis.Text == "Battle")
			{
				// Find first value bu sutracting sum of recorded values
				sql = "select sum(" + chartValue.bCol + ") from battle " + where;
				DataTable dtSum = DB.FetchData(sql);
				if (dtSum.Rows.Count > 0)
					if (dtSum.Rows[0][0] != DBNull.Value)
						firstValue = currentValue - Convert.ToDouble(dtSum.Rows[0][0]);
			}
			// Find battles
			sql = "select * from battle " + where + " order by battleTime " + chartOrder;
			DataTable dtChart = DB.FetchData(sql);
			if (ddXaxis.Text == "Date")
			{
				foreach (DataRow dr in dtChart.Rows)
				{
					ChartingMain.Series[chartSerie].Points.AddXY(Convert.ToDateTime(dr["battleTime"]), Math.Round(currentValue,2)); // Use battle date
					currentValue -= Convert.ToDouble(dr[chartValue.bCol]); //  Move backwards
					if (currentValue < axisYminimum) axisYminimum = Convert.ToInt32(currentValue);
				}
			}
			else if (ddXaxis.Text == "Battle")
			{
				double battleCount = 0;
				foreach (DataRow dr in dtChart.Rows)
				{
					battleCount += Convert.ToDouble(dr["battlesCount"]); // Use battle count
					firstValue += Convert.ToDouble(dr[chartValue.bCol]); // Move forwards
					if (currentValue < axisYminimum) axisYminimum = Convert.ToInt32(currentValue);
					ChartingMain.Series[chartSerie].Points.AddXY(battleCount, Math.Round(firstValue,2));
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
			string sql = "select SUM(wins), SUM(battles) from playerTankBattle " + where;
			double currentWins = Convert.ToInt32(DB.FetchData(sql).Rows[0][0]);
			double currentBattles = Convert.ToInt32(DB.FetchData(sql).Rows[0][1]);
			double firstWins = 0;
			double firstBattles = 0;
			if (ddXaxis.Text == "Battle")
			{
				// Find first value by sutracting sum of recorded values
				sql = "select sum(victory), sum(battlescount) from battle " + where;
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
			sql = "select * from battle " + where + " order by battleTime " + chartOrder;
			DataTable dtChart = DB.FetchData(sql);
			double winRate = 0;
			if (ddXaxis.Text == "Date")
			{
				foreach (DataRow dr in dtChart.Rows)
				{
					winRate = Math.Round(currentWins * 100 / currentBattles,2);
					if (winRate < axisYminimum) axisYminimum = Convert.ToInt32(winRate);
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
					if (winRate < axisYminimum) axisYminimum = Convert.ToInt32(winRate);
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
				"from playerTankBattle ptb left join " +
				"  playerTank pt on ptb.playerTankId=pt.id left join " +
				"  tank t on pt.tankId = t.id " +
				ptWhere;
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
					"from battle b left join " +
					"  playerTank pt on b.playerTankId=pt.id left join " +
					"  tank t on pt.tankId = t.id " +
				ptWhere;
				DataTable dtBattleSum = DB.FetchData(sql);
				if (dtBattleSum.Rows.Count > 0)
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
			sql = "select * from battle " + bWhere + " order by battleTime " + chartOrder;
			DataTable dtChart = DB.FetchData(sql);
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
					EFF = Math.Round(Code.Rating.CalculatePlayerEFFforChart(BATTLES, DAMAGE, SPOT, FRAGS, DEF, CAP, defaultTIER),2);
					if (EFF < axisYminimum) axisYminimum = Convert.ToInt32(EFF);
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
					EFF = Math.Round(Code.Rating.CalculatePlayerEFFforChart(BATTLES, DAMAGE, SPOT, FRAGS, DEF, CAP, defaultTIER),2);
					if (EFF < axisYminimum) axisYminimum = Convert.ToInt32(EFF);
					ChartingMain.Series[chartSerie].Points.AddXY(battleCount, EFF); // Use battle date
				}
			}
			ChartingMain.ChartAreas[0].AxisY.Minimum = axisYminimum;
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
					Code.MsgBox.Button answer = MsgBox.Show("Changing x-axis require chart to be cleared", "Clear Chart?", MsgBoxType.OKCancel);
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
			axisYminimum = 10000;
		}

		Point? prevPosition = null;
		ToolTip tooltip = new ToolTip();

		private void ChartingMain_MouseMove(object sender, MouseEventArgs e)
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
		
	}
}
