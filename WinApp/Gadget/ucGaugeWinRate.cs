using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;
using System.Diagnostics;
using WinApp.Gadget;

namespace WinApp.Gadget
{
	public partial class ucGaugeWinRate : UserControl
	{
		string _battleMode = "";
		float outsideRange = 0;
		public ucGaugeWinRate(string battleMode = "")
		{
			InitializeComponent();
			_battleMode = battleMode;
		}

		private void ucGaugeWinRate_Load(object sender, EventArgs e)
		{
			SelectTimeRangeButton();
			DataBind();
		}

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			DataBind();
			base.OnInvalidated(e);
		}

		public void DataBind()
		{
			// Init Gauge
			aGauge1.ValueMin = 30;
			aGauge1.ValueMax = 70;
			if (aGauge1.Value < 29) aGauge1.Value = 29;
			aGauge1.ValueScaleLinesMajorStepValue = 5;
			outsideRange = (aGauge1.ValueMax - aGauge1.ValueMin) * 3 / 100;
			// Colors 0-8
			for (byte i = 0; i <= 8; i++)
			{
				aGauge1.Range_Idx = i;
				if (i == 0)
					aGauge1.RangesStartValue[i] = aGauge1.ValueMin;
				else
					aGauge1.RangesStartValue[i] = (float)Rating.rangeWR[i];
				if (i == 8)
					aGauge1.RangesEndValue[i] = aGauge1.ValueMax;
				else
					aGauge1.RangesEndValue[i] = (float)Rating.rangeWR[i + 1];
				aGauge1.RangeEnabled = true;
			}
			// Show battle mode
			string capText = "Total";
			switch (_battleMode)
			{
				case "15": capText = "Random/TC"; break;
				case "7": capText = "Team"; break;
				case "Historical": capText = "Historical Battles"; break;
				case "Skirmishes": capText = "Skirmishes"; break;
			}
			string sqlBattlemode = "";
			if (_battleMode != "")
			{
				sqlBattlemode = " and battleMode=@battleMode";
				DB.AddWithValue(ref sqlBattlemode, "@battleMode", _battleMode, DB.SqlDataType.VarChar);
			}
			aGauge1.CenterSubText = "Win Rate: " + capText;
			float battles = 0;
			float wins = 0;
			// Overall stats team
			if (GadgetHelper.SelectedTimeRangeWR == GadgetHelper.TimeRange.Total)
			{
				string sql =
					"select sum(ptb.battles) as battles, sum(ptb.wins) as wins " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id " +
					"where pt.playerId=@playerId " + sqlBattlemode;
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable dt = DB.FetchData(sql);
				if (dt.Rows.Count > 0)
				{
					DataRow dr = dt.Rows[0];
					if (dr["battles"] != DBNull.Value && Convert.ToInt32(dr["battles"]) > 0)
					{
						// Current Battle count
						battles = (float)Convert.ToDouble(dr["battles"]);
						// Current wins
						wins = (float)Convert.ToDouble(dr["wins"]);
					}
				}
			}
			else // Check time range
			{
				int battleRevert = 0;
				string battleTimeFilter = "";
				DateTime dateFilter = DateTimeHelper.GetTodayDateTimeStart(); 
				switch (GadgetHelper.SelectedTimeRangeWR)
				{
					case GadgetHelper.TimeRange.Num1000:
						battleRevert = 1000;
						break;
					case GadgetHelper.TimeRange.Num5000:
						battleRevert = 5000;
						break;
					case GadgetHelper.TimeRange.TimeWeek:
						battleTimeFilter = " AND battleTime>=@battleTime ";
						// Adjust time scale according to selected filter
						dateFilter = dateFilter.AddDays(-7);
						DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter, DB.SqlDataType.DateTime);
						break;
					case GadgetHelper.TimeRange.TimeMonth:
						battleTimeFilter = " AND battleTime>=@battleTime ";
						// Adjust time scale according to selected filter
						dateFilter = dateFilter.AddMonths(-1);
						DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter, DB.SqlDataType.DateTime);
						break;
					case GadgetHelper.TimeRange.TimeToday:
						battleTimeFilter = " AND battleTime>=@battleTime ";
						DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter, DB.SqlDataType.DateTime);
						break;
					default:
						break;
				}
				string sql = 
					"select battlesCount, victory " + 
					"from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id " +
					"where playerId=@playerId " + sqlBattlemode + " " + battleTimeFilter + " order by battleTime DESC";
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable dtBattles = DB.FetchData(sql);
				if (dtBattles.Rows.Count > 0)
				{
					if (battleRevert == 0) battleRevert = dtBattles.Rows.Count;
					int count = 0;
					foreach (DataRow dr in dtBattles.Rows)
					{
						battles += Convert.ToInt32(dr["battlesCount"]);
						wins += Convert.ToInt32(dr["victory"]);
						count++;
						if (count > battleRevert) break;
					}
				}
			}
			// Show in gauge
			if (battles == 0)
				end_val = 0;
			else
				end_val = wins / battles * 100; // end_val = Win Rate
			// Show in center text
			aGauge1.CenterText = Math.Round(end_val, 2).ToString() + " %";
			aGauge1.CenterTextColor = Rating.WinRateColor(end_val);
			// CALC NEEDLE MOVEMENT
			// AVG_STEP_VAL	= (END_VAL-START_VAL)/STEP_TOT
			if (end_val < aGauge1.ValueMin - outsideRange) end_val = aGauge1.ValueMin - outsideRange;
			if (end_val > aGauge1.ValueMax) end_val = aGauge1.ValueMax + outsideRange;
			avg_step_val = (end_val - aGauge1.ValueMin) / step_tot; // Define average movements per timer tick
			
			move_speed = Math.Abs(end_val - aGauge1.Value) / 30;
			if (move_speed > 1) move_speed = 1;
			step_count = 0;
			timer1.Enabled = true;
		}

		double move_speed = 1;
		double avg_step_val = 0;
		double end_val = 0;
		double step_tot = 75;
		double step_count = 0;
		bool moveNeedle = false;

		private void timer1_Tick(object sender, EventArgs e)
		{
			double gaugeVal = 0;
			if (moveNeedle)
			{
				gaugeVal = aGauge1.Value;
				if (end_val < aGauge1.Value)
				{
					gaugeVal -= move_speed;
					if (gaugeVal <= end_val || gaugeVal <= aGauge1.ValueMin - outsideRange)
					{
						gaugeVal = end_val;
						timer1.Enabled = false;
					}
				}
				else
				{
					gaugeVal += move_speed;
					if (gaugeVal >= end_val || gaugeVal >= aGauge1.ValueMax + outsideRange)
					{
						gaugeVal = end_val;
						timer1.Enabled = false;
					}
				}
				if (Math.Abs(end_val - gaugeVal) / move_speed < 19 && move_speed > 0.01)
					move_speed = move_speed * 0.95;
			}
			else 
			{
				// AVG_STEP_VAL		(END_VAL-START_VAL)/STEP_TOT
				//BASE FORMULA		START_VAL + (EXP(1-(STEP_COUNT/STEP_TOTAL)) * STEP_COUNT * AVG_STEP_VAL
				step_count++;
				gaugeVal = aGauge1.ValueMin + (Math.Exp(1 - (step_count / step_tot)) * step_count * avg_step_val);
				if (step_count >= step_tot)
				{
					gaugeVal = end_val;
					timer1.Enabled = false;
					moveNeedle = true; // use normal movment after this
				}
			}
			aGauge1.Value = (float)gaugeVal;
		}

		private void btnTime_Click(object sender, EventArgs e)
		{
			BadButton b = (BadButton)sender;
			switch (b.Name)
			{
				case "btnTotal": GadgetHelper.SelectedTimeRangeWR = GadgetHelper.TimeRange.Total; break;
				case "btn1000": GadgetHelper.SelectedTimeRangeWR = GadgetHelper.TimeRange.Num1000; break;
				case "btn5000": GadgetHelper.SelectedTimeRangeWR = GadgetHelper.TimeRange.Num5000; break;
				case "btnMonth": GadgetHelper.SelectedTimeRangeWR = GadgetHelper.TimeRange.TimeMonth; break;
				case "btnWeek": GadgetHelper.SelectedTimeRangeWR = GadgetHelper.TimeRange.TimeWeek; break;
				case "btnToday": GadgetHelper.SelectedTimeRangeWR = GadgetHelper.TimeRange.TimeToday; break;
			}
			SelectTimeRangeButton();
			DataBind();
		}

		private void SelectTimeRangeButton()
		{
			btnTotal.Checked = false;
			btn1000.Checked = false;
			btnMonth.Checked = false;
			btnWeek.Checked = false;
			btnToday.Checked = false;
			switch (GadgetHelper.SelectedTimeRangeWR)
			{
				case GadgetHelper.TimeRange.Total: btnTotal.Checked = true; break;
				case GadgetHelper.TimeRange.Num1000: btn1000.Checked = true; break;
				case GadgetHelper.TimeRange.TimeMonth: btnMonth.Checked = true; break;
				case GadgetHelper.TimeRange.TimeWeek: btnWeek.Checked = true; break;
				case GadgetHelper.TimeRange.TimeToday: btnToday.Checked = true; break;
			}
		}

		private void ucGaugeWinRate_Paint(object sender, PaintEventArgs e)
		{
			if (BackColor == ColorTheme.FormBackSelectedGadget)
				GadgetHelper.DrawBorderOnGadget(sender, e);
		}
	}
}
