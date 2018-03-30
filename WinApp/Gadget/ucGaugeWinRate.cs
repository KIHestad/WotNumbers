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
using WinApp.Code.FormLayout;
using System.Threading.Tasks;

namespace WinApp.Gadget
{
	public partial class ucGaugeWinRate : UserControl
	{
		string _battleMode = "";
		private GadgetHelper.TimeRangeEnum _battleTimeSpan = GadgetHelper.TimeRangeEnum.Total;
        float outsideRange = 0;

        public ucGaugeWinRate(string battleMode, GadgetHelper.TimeRangeEnum timeSpan)
		{
			InitializeComponent();
			_battleMode = battleMode;
            _battleTimeSpan = timeSpan;
		}

		private void ucGaugeWinRate_Load(object sender, EventArgs e)
		{
			SelectTimeRangeButton();
		}

		public async Task DataBind()
		{
			// Init Gauge
			aGauge1.ValueMin = 30;
			aGauge1.ValueMax = 70;
			if (aGauge1.Value < 29) aGauge1.Value = 29;
			aGauge1.ValueScaleLinesMajorStepValue = 5;
			outsideRange = (aGauge1.ValueMax - aGauge1.ValueMin) * 3 / 100;
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
            // Colors
            aGauge1.SetColorRanges(ColorRangeScheme.RangeWR);
            // Show battle mode
			string capText = "Total";
            capText = BattleMode.GetItemFromSqlName(_battleMode).Name;
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
            if (_battleTimeSpan == GadgetHelper.TimeRangeEnum.Total)
			{
				string sql =
					"select sum(ptb.battles) as battles, sum(ptb.wins) as wins " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id " +
					"where pt.playerId=@playerId " + sqlBattlemode;
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable dt = await DB.FetchData(sql);
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
                switch (_battleTimeSpan)
				{
					case GadgetHelper.TimeRangeEnum.TimeWeek:
						battleTimeFilter = " AND battleTime>=@battleTime ";
						// Adjust time scale according to selected filter
						dateFilter = dateFilter.AddDays(-7);
						DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter, DB.SqlDataType.DateTime);
						break;
                    case GadgetHelper.TimeRangeEnum.TimeMonth:
                        battleTimeFilter = " AND battleTime>=@battleTime ";
                        // Adjust time scale according to selected filter
                        dateFilter = dateFilter.AddMonths(-1);
                        DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter, DB.SqlDataType.DateTime);
                        break;
                    case GadgetHelper.TimeRangeEnum.TimeMonth3:
                        battleTimeFilter = " AND battleTime>=@battleTime ";
                        // Adjust time scale according to selected filter
                        dateFilter = dateFilter.AddMonths(-3);
                        DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter, DB.SqlDataType.DateTime);
                        break;
                    case GadgetHelper.TimeRangeEnum.TimeToday:
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
				DataTable dtBattles = await DB.FetchData(sql);
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
            aGauge1.CenterTextColor = ColorRangeScheme.WinRateColor(end_val);
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

		private async void btnTime_Click(object sender, EventArgs e)
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
			await DataBind();
		}

		private void SelectTimeRangeButton()
		{
			btnTotal.Checked = false;
			btnMonth3.Checked = false;
			btnMonth.Checked = false;
			btnWeek.Checked = false;
			btnToday.Checked = false;
            switch (_battleTimeSpan)
			{
				case GadgetHelper.TimeRangeEnum.Total: btnTotal.Checked = true; break;
				case GadgetHelper.TimeRangeEnum.TimeMonth3: btnMonth3.Checked = true; break;
				case GadgetHelper.TimeRangeEnum.TimeMonth: btnMonth.Checked = true; break;
				case GadgetHelper.TimeRangeEnum.TimeWeek: btnWeek.Checked = true; break;
				case GadgetHelper.TimeRangeEnum.TimeToday: btnToday.Checked = true; break;
			}
		}

		private void ucGaugeWinRate_Paint(object sender, PaintEventArgs e)
		{
			if (BackColor == ColorTheme.FormBackSelectedGadget)
				GadgetHelper.DrawBorderOnGadget(sender, e);
		}
	}
}
