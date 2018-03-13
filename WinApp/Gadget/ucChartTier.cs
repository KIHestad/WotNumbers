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
		private string _barColorHTML = "";
        private GadgetHelper.TimeRangeEnum _battleTimeSpan = GadgetHelper.TimeRangeEnum.Total;
		
		// slide chart
		private int timerStep = 0;
		private int timerMaxStep = 20;
		private double[] oldVal = new double[10];
		private double[] newVal = new double[10];
		private double[] move = new double[10];

		// label controls
		List<Control> lblControls = new List<Control>();

		public ucChartTier(string battleMode = "", string barColorHTML = "", GadgetHelper.TimeRangeEnum timeSpan = GadgetHelper.TimeRangeEnum.Total)
		{
			InitializeComponent();
			_battleMode = battleMode;
			_barColorHTML = barColorHTML;
            _battleTimeSpan = timeSpan;
		}

		private void ucChart_Load(object sender, EventArgs e)
		{
            chart1.Top = 1;
            chart1.Left = 1;
            timerMaxStep = 20;
            lblChartType.ForeColor = ColorTheme.ControlFont;
            CreateEmptyChart();
            ReziseChart();
            // show correct timespan button as selected
            switch (_battleTimeSpan)
            {
                case GadgetHelper.TimeRangeEnum.Total:
                    btnTotal.Checked = true;
                    break;
                case GadgetHelper.TimeRangeEnum.TimeMonth3:
                    btnMonth3.Checked = true;
                    break;
                case GadgetHelper.TimeRangeEnum.TimeMonth:
                    btnMonth.Checked = true;
                    break;
                case GadgetHelper.TimeRangeEnum.TimeWeek:
                    btnWeek.Checked = true;
                    break;
                case GadgetHelper.TimeRangeEnum.TimeToday:
                    btnToday.Checked = true;
                    break;
            }
		}

		public void DataBind()
		{
			DrawChart();
		}

		private void CreateEmptyChart()
		{
			// X Axis font
			Font letterType = new Font("MS Sans Serif", 10, GraphicsUnit.Pixel);
			Color defaultColor = ColorTheme.ControlFont;
			ChartArea area = chart1.ChartAreas[0];
			area.AxisY.Enabled = AxisEnabled.False;
			area.InnerPlotPosition = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(0, 0, 100, 100);
			foreach (var axis in area.Axes)
			{
				axis.LabelStyle.Font = letterType;
				axis.LabelStyle.ForeColor = Color.Transparent;
				axis.LabelAutoFitMinFontSize = (int)letterType.Size;
				axis.LabelAutoFitMaxFontSize = (int)letterType.Size;
			}
			// Get serie
			Series serie1 = chart1.Series[0];
			Color barColor = ColorTheme.ChartBarBlue;
			if (_barColorHTML != "")
			{
				barColor = ColorTranslator.FromHtml(_barColorHTML);
			}
			serie1.Color = barColor;
			serie1.IsXValueIndexed = true;
			//serie1["MaxPixelPointWidth"] = "25";
			// Add points
			for (double id = 1; id <= 10; id++)
			{
				DataPoint p = new DataPoint();
				p.YValues[0] = 0;
				p.AxisLabel = id.ToRoman();
				p.Font = new Font("MS Sans Serif", 9, GraphicsUnit.Pixel);
				p.LabelForeColor = ColorTheme.ControlFont;
				serie1.Points.Add(p);
				// Add labels as x-axis labels
				Label lbl = new Label();
				lbl.Name = "lbl" + id.ToString();
				lbl.Text = id.ToRoman();
				lbl.AutoSize = false;
				lbl.Height = 16;
				lbl.Width = 24;
				lbl.ForeColor = ColorTheme.ControlFont;
				lbl.TextAlign = ContentAlignment.MiddleCenter;
				lbl.Font = new Font("Microsoft Sans Serif", 9); 
				this.Controls.Add(lbl);
				Control[] c = this.Controls.Find(lbl.Name, false);
				c[0].BringToFront();
				lblControls.Add(c[0]); // store in image control for later resize
				// Add tooltip
				ToolTip tip = new ToolTip();
				tip.SetToolTip(c[0], "Tier " + id);
			}
		}

		private void DrawChart()
		{
			// Show battle mode
			string battleModeText = "Total";
            battleModeText = BattleMode.GetItemFromSqlName(_battleMode).Name;
			string sqlBattlemode = "";
			string sql = "";
            if (_battleTimeSpan == GadgetHelper.TimeRangeEnum.Total)
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
                    "WHERE  (playerTank.playerId = @playerId) AND tank.tier > 0 " + sqlBattlemode +
					"GROUP BY tank.tier " +
					"ORDER BY tank.tier ";
			}
			else
			{
				// Create Battle Time filer
				DateTime dateFilter = DateTimeHelper.GetTodayDateTimeStart(); 
				// Adjust time scale according to selected filter
                switch (_battleTimeSpan)
                {
                    case GadgetHelper.TimeRangeEnum.TimeWeek:
                        dateFilter = dateFilter.AddDays(-7);
                        break;
                    case GadgetHelper.TimeRangeEnum.TimeMonth:
                        dateFilter = dateFilter.AddMonths(-1);
                        break;
                    case GadgetHelper.TimeRangeEnum.TimeMonth3:
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
                    "WHERE  (battle.battleTime >= @battleTime) AND (playerTank.playerId = @playerId) AND tank.tier > 0 " + sqlBattlemode +
					"GROUP BY tank.tier " +
					"ORDER BY tank.tier ";
				DB.AddWithValue(ref sql, "@battleTime", dateFilter, DB.SqlDataType.DateTime);
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
			// Chart
			chart1.Width = this.Width - 2;
			chart1.Height = this.Height - (this.Height - lblChartType.Top + 21); // Make room for labels
			// Labels
			for (int id = 0; id < lblControls.Count; id++)
			{
				Control c = lblControls[id];
				double barWidth = chart1.Width / lblControls.Count + 0.75; 
				c.Top = this.Height - (this.Height - lblChartType.Top + 18);
				c.Left = Convert.ToInt32((barWidth / 2) - 11 + (barWidth * id));
			}
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
			btnWeek.Checked = false;
			btnToday.Checked = false;
			BadButton btn = (BadButton)sender;
			btn.Checked = true;
            _battleTimeSpan = GadgetHelper.GetTimeItemFromName(btn.Tag.ToString()).TimeRange;
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
		}
	}
}
