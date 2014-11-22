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

namespace WinApp.Gadget
{
	public partial class ucGaugeWN7 : UserControl
	{
		public ucGaugeWN7()
		{
			InitializeComponent();
		}

		private void ucGauge_Load(object sender, EventArgs e)
		{
			SelectTimeRangeButton();
			DataBind();
		}

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			DataBind();
			base.OnInvalidated(e);
		}

		private void DataBind()
		{
			// Init Gauge
			aGauge1.ValueMin = 0;
			aGauge1.ValueMax = 2500;
			aGauge1.ValueScaleLinesMajorStepValue = 250;
			aGauge1.CenterSubText = "WN7 Rating";
			// Colors 0-8
			for (byte i = 0; i <= 8; i++)
			{
				aGauge1.Range_Idx = i;
				if (i == 0)
					aGauge1.RangesStartValue[i] = aGauge1.ValueMin;
				else
					aGauge1.RangesStartValue[i] = (float)Rating.rangeWN7[i];
				if (i == 8)
					aGauge1.RangesEndValue[i] = aGauge1.ValueMax;
				else
					aGauge1.RangesEndValue[i] = (float)Rating.rangeWN7[i + 1];
				aGauge1.RangeEnabled = true;
			}
			// Overall stats team
			if (GadgetHelper.SelectedTimeRangeWN7 == GadgetHelper.TimeRange.Total)
			{
				string sql =
					"select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
					"  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id left join " +
					"  tank t on pt.tankId = t.id " +
					"where pt.playerId=@playerId and ptb.battleMode='15'";
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable dt = DB.FetchData(sql);
				if (dt.Rows.Count == 0) return;
				DataRow stats = dt.Rows[0];
				double BATTLES = Rating.ConvertDbVal2Double(stats["battles"]);
				double DAMAGE = Rating.ConvertDbVal2Double(stats["dmg"]);
				double SPOT = Rating.ConvertDbVal2Double(stats["spot"]);
				double FRAGS = Rating.ConvertDbVal2Double(stats["frags"]);
				double DEF = Rating.ConvertDbVal2Double(stats["def"]);
				double CAP = Rating.ConvertDbVal2Double(stats["cap"]);
				double WINS = Rating.ConvertDbVal2Double(stats["wins"]);
				double TIER = 0;
				if (BATTLES > 0)
					TIER = Rating.ConvertDbVal2Double(stats["tier"]) / BATTLES;
				end_val = Code.Rating.CalculateWN7(BATTLES, DAMAGE, SPOT, FRAGS, DEF, CAP, WINS, Rating.GetAverageBattleTier("15"));
			}
			else // Check time range
			{
				int battleRevert = 0;
				string battleTimeFilter = "";
				DateTime basedate = DateTime.Now; // current time
				if (DateTime.Now.Hour < 5) basedate = DateTime.Now.AddDays(-1); // correct date according to server reset 05:00
				DateTime dateFilter = new DateTime(basedate.Year, basedate.Month, basedate.Day, 7, 0, 0); // datefilter = today
				switch (GadgetHelper.SelectedTimeRangeWN7)
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
						DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
						break;
					case GadgetHelper.TimeRange.TimeMonth:
						battleTimeFilter = " AND battleTime>=@battleTime ";
						// Adjust time scale according to selected filter
						dateFilter = dateFilter.AddMonths(-1);
						DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
						break;
					case GadgetHelper.TimeRange.TimeToday:
						battleTimeFilter = " AND battleTime>=@battleTime ";
						DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
						break;
					default:
						break;
				}
				end_val = Rating.CalcBattleWN7(battleTimeFilter, battleRevert);
				//string sql =
				//	"select battlesCount as battles, dmg, spotted as spot, frags, " +
				//	"  def, cap, t.tier as tier , victory as wins " +
				//	"from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
				//	"  tank t on playerTank.tankId = t.id " +
				//	"where playerId=@playerId and battleMode='15' " + battleTimeFilter + " order by battleTime DESC";
				//DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				//DataTable dtBattles = DB.FetchData(sql);
				//end_val = 0;
				//if (dtBattles.Rows.Count > 0)
				//{
				//	if (battleRevert == 0) battleRevert = dtBattles.Rows.Count;
				//	int count = 0;
				//	double BATTLES = 0;
				//	double DAMAGE = 0;
				//	double SPOT = 0;
				//	double FRAGS = 0;
				//	double DEF = 0;
				//	double CAP = 0;
				//	double WINS = 0;
				//	double TIER = 0;
				//	foreach (DataRow stats in dtBattles.Rows)
				//	{
				//		double btl = Rating.ConvertDbVal2Double(stats["battles"]);
				//		BATTLES += btl;
				//		DAMAGE += Rating.ConvertDbVal2Double(stats["dmg"]) * btl;
				//		SPOT += Rating.ConvertDbVal2Double(stats["spot"]) * btl;
				//		FRAGS += Rating.ConvertDbVal2Double(stats["frags"]) * btl;
				//		DEF += Rating.ConvertDbVal2Double(stats["def"]) * btl;
				//		CAP += Rating.ConvertDbVal2Double(stats["cap"]) * btl;
				//		WINS += Rating.ConvertDbVal2Double(stats["wins"]) * btl;
				//		TIER += Rating.ConvertDbVal2Double(stats["tier"]) * btl;
				//		count++;
				//		if (count > battleRevert) break;
				//	}
				//	if (BATTLES > 0)
				//		end_val = Code.Rating.CalculateWN7(BATTLES, DAMAGE, SPOT, FRAGS, DEF, CAP, WINS, (TIER / BATTLES));
				//	else
				//		end_val = 0;
				//}
			}
			// Show in center text
			aGauge1.CenterText = Math.Round(end_val, 2).ToString();
			aGauge1.CenterTextColor = Rating.WN7color(end_val);
			// CALC NEEDLE MOVEMENT
			// AVG_STEP_VAL	= (END_VAL-START_VAL)/STEP_TOT
			avg_step_val = (end_val - aGauge1.ValueMin) / step_tot; // Define average movements per timer tick
			move_speed = Math.Abs(end_val - aGauge1.Value) / 30;
			if (move_speed > 40) move_speed = 40;
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
					if (gaugeVal <= end_val || gaugeVal <= aGauge1.ValueMin)
					{
						gaugeVal = end_val;
						timer1.Enabled = false;
					}
				}
				else
				{
					gaugeVal += move_speed;
					if (gaugeVal >= end_val || gaugeVal >= aGauge1.ValueMax)
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
				case "btnTotal": GadgetHelper.SelectedTimeRangeWN7 = GadgetHelper.TimeRange.Total; break;
				case "btn1000": GadgetHelper.SelectedTimeRangeWN7 = GadgetHelper.TimeRange.Num1000; break;
				case "btn5000": GadgetHelper.SelectedTimeRangeWN7 = GadgetHelper.TimeRange.Num5000; break;
				case "btnMonth": GadgetHelper.SelectedTimeRangeWN7 = GadgetHelper.TimeRange.TimeMonth; break;
				case "btnWeek": GadgetHelper.SelectedTimeRangeWN7 = GadgetHelper.TimeRange.TimeWeek; break;
				case "btnToday": GadgetHelper.SelectedTimeRangeWN7 = GadgetHelper.TimeRange.TimeToday; break;
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
			switch (GadgetHelper.SelectedTimeRangeWN7)
			{
				case GadgetHelper.TimeRange.Total: btnTotal.Checked = true; break;
				case GadgetHelper.TimeRange.Num1000: btn1000.Checked = true; break;
				case GadgetHelper.TimeRange.TimeMonth: btnMonth.Checked = true; break;
				case GadgetHelper.TimeRange.TimeWeek: btnWeek.Checked = true; break;
				case GadgetHelper.TimeRange.TimeToday: btnToday.Checked = true; break;
			}
		}

		private void ucGaugeWN7_Paint(object sender, PaintEventArgs e)
		{
			if (BackColor == ColorTheme.FormBackSelectedGadget)
				GadgetHelper.DrawBorderOnGadget(sender, e);
		}
	}
}
