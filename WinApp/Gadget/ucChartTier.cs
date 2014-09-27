using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinApp.Gadget
{
	public partial class ucChartTier : UserControl
	{
		private string _battleMode = "";
		
		// slide chart
		private int timerStep = 0;
		private int timerMaxStep = 20;
		private double[] oldVal = new double[10];
		private double[] newVal = new double[10];
		private double[] move = new double[10];

		private enum Selection
		{
			Total = 1,
			Month3 = 2,
			Month = 3,
			Week = 4,
			Today = 5,
		}

		private static Selection selection = Selection.Total;

		public ucChartTier(string battleMode = "")
		{
			InitializeComponent();
			_battleMode = battleMode;
		}

		private void ucChart_Load(object sender, EventArgs e)
		{
			chart1.Top = 1;
			chart1.Left = 1;
			timerMaxStep = 20;
			selection = Selection.Total;
			lblChartType.ForeColor = ColorTheme.ControlFont;
			ReziseChart();
			CreateEmptyChart();
			DrawChart();
		}

		private void CreateEmptyChart()
		{
			// X Axis font
			Font letterType = new Font("MS Sans Serif", 10, GraphicsUnit.Pixel);
			Color defaultColor = ColorTheme.ControlFont;
			ChartArea area = chart1.ChartAreas[0];
			area.AxisY.Enabled = AxisEnabled.False;
			foreach (var axis in area.Axes)
			{
				axis.LabelStyle.Font = letterType;
				axis.LabelAutoFitMinFontSize = (int)letterType.Size;
				axis.LabelAutoFitMaxFontSize = (int)letterType.Size;
			}
			// Get serie
			Series serie1 = chart1.Series[0];
			serie1.Color = ColorTheme.ChartBarBlue;
			serie1.IsXValueIndexed = true;
			// Add points
			for (double i = 1; i <= 10; i++)
			{
				DataPoint p = new DataPoint();
				p.YValues[0] = 0;
				p.AxisLabel = i.ToRoman();
				p.Font = new Font("MS Sans Serif", 9, GraphicsUnit.Pixel);
				p.LabelForeColor = ColorTheme.ControlFont;
				serie1.Points.Add(p);
			}
		}

		private void DrawChart()
		{
			// Show battle mode
			string battleModeText = "Total";
			switch (_battleMode)
			{
				case "15": battleModeText = "Random/TC"; break;
				case "7": battleModeText = "Team"; break;
				case "Historical": battleModeText = "Historical Battles"; break;
				case "Skirmishes": battleModeText = "Skirmishes"; break;
			}
			string sqlBattlemode = "";
			string sql = "";
			if (selection == Selection.Total)
			{
				if (_battleMode != "")
				{
					sqlBattlemode = " AND (playerTankBattle.battleMode = @battleMode) ";
					DB.AddWithValue(ref sqlBattlemode, "@battleMode", _battleMode, DB.SqlDataType.VarChar);
				}
				sql =
					"SELECT SUM(playerTankBattle.battles) AS battleCount, tank.tier AS tier " +
					"FROM   playerTankBattle INNER JOIN " +
					"		playerTank ON playerTankBattle.playerTankId = playerTank.id INNER JOIN " +
					"		tank ON playerTank.tankId = tank.id " +
					"WHERE  (playerTank.playerId = @playerId) " + sqlBattlemode +
					"GROUP BY tank.tier " +
					"ORDER BY tank.tier ";
			}
			else
			{
				// Create Battle Time filer
				DateTime dateFilter = new DateTime();
				DateTime basedate = DateTime.Now; // current time
				if (DateTime.Now.Hour < 7) basedate = DateTime.Now.AddDays(-1); // correct date according to server reset 07:00 AM
				dateFilter = new DateTime(basedate.Year, basedate.Month, basedate.Day, 7, 0, 0); // datefilter = today
				// Adjust time scale according to selected filter
				switch (selection)
				{
					case Selection.Week: 
						dateFilter = dateFilter.AddDays(-7);
						break;
					case Selection.Month:
						dateFilter = dateFilter.AddMonths(-1);
						break;
					case Selection.Month3:
						dateFilter = dateFilter.AddMonths(-3);
						break;
				}
				if (_battleMode != "")
				{
					sqlBattlemode = " AND (battle.battleMode = @battleMode) ";
					DB.AddWithValue(ref sqlBattlemode, "@battleMode", _battleMode, DB.SqlDataType.VarChar);
				}
				sql =
					"SELECT SUM(battle.battlesCount) AS battleCount, tank.tier AS tier " +
					"FROM   battle INNER JOIN " +
					"       playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
					"       tank ON playerTank.tankId = tank.id " +
					"WHERE  (battle.battleTime >= @battleTime) AND (playerTank.playerId = @playerId) " + sqlBattlemode +
					"GROUP BY tank.tier " +
					"ORDER BY tank.tier ";
				DB.AddWithValue(ref sql, "@battleTime", dateFilter.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
			}
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			Series serie1 = chart1.Series[0];
			//for (int x = 0; x < 10; x++)
			//	serie1.Points[x].YValues[0] = 0;
			double tot = 0;
			// old values
			for (int x = 0; x < 10; x++)
			{
				oldVal[x] = serie1.Points[x].YValues[0];
				newVal[x] = 0;
			}
			// new values
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					int x = Convert.ToInt32(dr["tier"]) - 1;
					double val = Convert.ToDouble(dr["battleCount"]);
					tot += val;
					newVal[x] = val; 
				}
			}
			// move per step
			for (int x = 0; x < 10; x++)
			{
				move[x] = (newVal[x] - oldVal[x]) / timerMaxStep;
			}
			// New values
			lblChartType.Text = "Battle Count - " + battleModeText + ": " + tot.ToString("N0");
			timerStep = 0;
			timer1.Interval = 10;
			timer1.Enabled = true;
		}

		private void ucChart_Resize(object sender, EventArgs e)
		{
			ReziseChart();
		}

		private void ReziseChart()
		{
			chart1.Width = this.Width - 2;
			chart1.Height = this.Height - (this.Height - lblChartType.Top);
		}

		private void ucChart_Paint(object sender, PaintEventArgs e)
		{
			if (BackColor == ColorTheme.FormBackSelectedGadget)
				GadgetHelper.DrawBorderOnGadget(sender, e);
		}

		private void btnSelection_Click(object sender, EventArgs e)
		{
			btnTotal.Checked = false;
			btnMonth3.Checked = false;
			btnMonth.Checked = false;
			btnYday.Checked = false;
			btnToday.Checked = false;
			BadButton btn = (BadButton)sender;
			btn.Checked = true;
			selection = (Selection)Enum.Parse(typeof(Selection), btn.Tag.ToString());
			DrawChart();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			double max = 0;
			Series serie1 = chart1.Series[0];
			timerStep++;
			timer1.Interval = Convert.ToInt32(timer1.Interval * 1.2);
			//if (firstrun)
			//{
			//	timerStep = timerMaxStep;
			//	firstrun = false;
			//}
			if (timerStep < timerMaxStep)
			{
				// slide chart
				timerStep++;
				for (int x = 0; x < 10; x++)
				{
					serie1.Points[x].IsValueShownAsLabel = false;
					double val = oldVal[x] + (move[x] * timerStep);
					serie1.Points[x].YValues[0] = val;
					if (val > max) max = val;
					if (newVal[x] > max) max = newVal[x];
				}
			}
			else
			{
				// Final values
				max = 0;
				// Hide values = 0
				for (int x = 0; x < 10; x++)
				{
					serie1.Points[x].YValues[0] = newVal[x];
					if (newVal[x] > max) max = newVal[x];
					serie1.Points[x].IsValueShownAsLabel = (serie1.Points[x].YValues[0] > 0);
				}
				// stop timer
				timer1.Enabled = false;
			}
			chart1.ChartAreas[0].AxisY.Maximum = (max * 1.2);
			serie1.Points.ResumeUpdates();
			Application.DoEvents();
		}
	}
}
