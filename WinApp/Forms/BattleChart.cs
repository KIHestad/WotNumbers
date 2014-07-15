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
			else if (ddXaxis.Text == "Battles")
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
				case "EFF": return;
			}
			// Find playerTank current value or all tanks current value
			double currentValue = 0;
			double firstValue = 0;
			string sql = "";
			// Get Standard battle data for non-calculated values
			if (tankName != "All Tanks")
			{
				// Find playertank and current value
				int playerTankId = TankData.GetPlayerTankId(tankName);
				sql = "select SUM(" + chartValue.ptbCol + ") from playerTankBattle where playerTankId=@playerTankId";
				DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				currentValue = Convert.ToInt32(DB.FetchData(sql).Rows[0][0]);
				if (ddXaxis.Text == "Battles")
				{
					// Find first value bu sutracting sum of recorded values
					sql = "select sum(" + chartValue.bCol + ") from battle where playerTankId=@playerTankId ";
					DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
					DataTable dtSum = DB.FetchData(sql);
					if (dtSum.Rows.Count > 0)
						if (dtSum.Rows[0][0] != DBNull.Value)
							firstValue = currentValue - Convert.ToDouble(dtSum.Rows[0][0]);
				}
				// Find battles
				sql = "select * from battle where playerTankId=@playerTankId order by battleTime " + chartOrder;
				DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			}
			else
			{
				// Find current value for all tanks
				sql = "select SUM(" + chartValue.ptbCol + ") from playerTankBattle";
				currentValue = Convert.ToInt32(DB.FetchData(sql).Rows[0][0]);
				if (ddXaxis.Text == "Battles")
				{
					// Find first value bu sutracting sum of recorded values
					sql = "select sum(" + chartValue.bCol + ") from battle ";
					DataTable dtSum = DB.FetchData(sql);
					if (dtSum.Rows.Count > 0)
						firstValue = currentValue - Convert.ToDouble(dtSum.Rows[0][0]);
				}
				// Find battles
				sql = "select * from battle order by battleTime " + chartOrder;
			}
			DataTable dtChart = DB.FetchData(sql);
			if (ddXaxis.Text == "Date")
			{
				foreach (DataRow dr in dtChart.Rows)
				{
					ChartingMain.Series[chartSerie].Points.AddXY(Convert.ToDateTime(dr["battleTime"]), currentValue); // Use battle date
					currentValue -= Convert.ToDouble(dr[chartValue.bCol]); //  Move backwards
				}
			}
			else if (ddXaxis.Text == "Battles")
			{
				double battleCount = 0;
				foreach (DataRow dr in dtChart.Rows)
				{
					battleCount += Convert.ToDouble(dr["battlesCount"]); // Use battle count
					ChartingMain.Series[chartSerie].Points.AddXY(battleCount, firstValue);
					firstValue += Convert.ToDouble(dr[chartValue.bCol]); // Move forwards
				}
			}
			
		}

		private int axisYminimum = 100;
		private void DrawChartWinRate(string tankName, string chartSerie, string chartOrder)
		{
			// ChartingMain.ChartAreas[0].AxisY.Interval = 1;
			// Find playerTank current value or all tanks current value
			double currentWins = 0;
			double currentBattles = 0;
			double firstWins = 0;
			double firstBattles = 0;
			string sql = "";
			// Get Standard battle data for non-calculated values
			if (tankName != "All Tanks")
			{
				// Find playertank and current value
				int playerTankId = TankData.GetPlayerTankId(tankName);
				sql = "select SUM(wins), SUM(battles) from playerTankBattle where playerTankId=@playerTankId";
				DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				currentWins = Convert.ToInt32(DB.FetchData(sql).Rows[0][0]);
				currentBattles = Convert.ToInt32(DB.FetchData(sql).Rows[0][1]);
				if (ddXaxis.Text == "Battles")
				{
					// Find first value by sutracting sum of recorded values
					sql = "select sum(victory), sum(battlescount) from battle where playerTankId=@playerTankId ";
					DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
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
				sql = "select * from battle where playerTankId=@playerTankId order by battleTime " + chartOrder;
				DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			}
			else
			{
				// Find current value for all tanks
				sql = "select SUM(wins), SUM(battles) from playerTankBattle";
				currentWins = Convert.ToInt32(DB.FetchData(sql).Rows[0][0]);
				currentBattles = Convert.ToInt32(DB.FetchData(sql).Rows[0][1]);
				if (ddXaxis.Text == "Battles")
				{
					// Find first value by subtracting sum of recorded values
					sql = "select SUM(victory), SUM(battlescount) from battle ";
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
				sql = "select * from battle order by battleTime " + chartOrder;
			}
			DataTable dtChart = DB.FetchData(sql);
			double winRate = 0;
			if (ddXaxis.Text == "Date")
			{
				foreach (DataRow dr in dtChart.Rows)
				{
					winRate = currentWins * 100 / currentBattles;
					if (winRate < axisYminimum) axisYminimum = Convert.ToInt32(winRate);
					ChartingMain.Series[chartSerie].Points.AddXY(Convert.ToDateTime(dr["battleTime"]), winRate); // Use battle date
					currentWins -= Convert.ToDouble(dr["victory"]); //  Move backwards
					currentBattles -= Convert.ToDouble(dr["battlesCount"]);
				}
			}
			else if (ddXaxis.Text == "Battles")
			{
				double battleCount = 0;
				foreach (DataRow dr in dtChart.Rows)
				{
					winRate = firstWins * 100 / firstBattles;
					if (winRate < axisYminimum) axisYminimum = Convert.ToInt32(winRate);
					battleCount += Convert.ToDouble(dr["battlesCount"]); // Use battle count
					ChartingMain.Series[chartSerie].Points.AddXY(battleCount, winRate);
					firstWins += Convert.ToDouble(dr["victory"]); // Move forwards
					firstBattles += Convert.ToDouble(dr["battlesCount"]);
				}
			}
			ChartingMain.ChartAreas[0].AxisY.Minimum = axisYminimum;
		}

		string selectedXaxis = "";
		private void ddXaxis_Click(object sender, EventArgs e)
		{
			selectedXaxis = ddXaxis.Text;						
			Code.DropDownGrid.Show(ddXaxis, Code.DropDownGrid.DropDownGridType.List, "Date,Battles");
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
			axisYminimum = 100;
		}
		
	}
}
