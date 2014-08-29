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
	public partial class ucGaugeWN8 : UserControl
	{
		public ucGaugeWN8()
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
			aGauge1.ValueMax = 3250;
			aGauge1.ValueScaleLinesMajorStepValue = 250;
			aGauge1.CenterSubText = "WN8 Rating";
			// Colors 0-8
			for (byte i = 0; i <= 8; i++)
			{
				aGauge1.Range_Idx = i;
				if (i == 0)
					aGauge1.RangesStartValue[i] = aGauge1.ValueMin;
				else
					aGauge1.RangesStartValue[i] = (float)Rating.rangeWN8[i];
				if (i == 8)
					aGauge1.RangesEndValue[i] = aGauge1.ValueMax;
				else
					aGauge1.RangesEndValue[i] = (float)Rating.rangeWN8[i + 1];
				aGauge1.RangeEnabled = true;
			}
			// Overall stats team
			if (GadgetHelper.SelectedTimeRangeWN8 == GadgetHelper.TimeRange.Total)
			{
				end_val = Code.Rating.CalculatePlayerTotalWN8("15");
			}
			else // Check time range
			{
				int battleRevert = 0;
				string battleTimeFilter = "";
				DateTime basedate = DateTime.Now; // current time
				if (DateTime.Now.Hour < 5) basedate = DateTime.Now.AddDays(-1); // correct date according to server reset 05:00
				DateTime dateFilter = new DateTime(basedate.Year, basedate.Month, basedate.Day, 5, 0, 0); // datefilter = today
				switch (GadgetHelper.SelectedTimeRangeWN8)
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
				// Create an empty datatable with all tanks, no values
				string sql =
					"select t.id as tankId, 0 as battles, 0 as dmg, 0 as spot, 0 as frags, " +
					"  0 as def, 0 as cap, 0 as wins " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId and ptb.battleMode='15' left join " +
					"  tank t on pt.tankId = t.id " +
					"where t.expDmg is not null and ptb.battleMode='15' " +
					"group by t.id ";
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);

				DataTable ptb = DB.FetchData(sql);
				// Get all battles
				sql =
					"select battlesCount as battles, dmg, spotted as spot, frags, " +
					"  def, cap, t.tier as tier , victory as wins, t.id as tankId " +
					"from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
					"  tank t on playerTank.tankId = t.id " +
					"where playerId=@playerId and battleMode='15' " + battleTimeFilter + " order by battleTime DESC";
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable dtBattles = DB.FetchData(sql);
				end_val = 0;
				if (dtBattles.Rows.Count > 0)
				{
					if (battleRevert == 0) battleRevert = dtBattles.Rows.Count;
					int count = 0;
					foreach (DataRow stats in dtBattles.Rows)
					{
						double btl = Rating.ConvertDbVal2Double(stats["battles"]);
						// add to datatable
						string tankId = stats["tankId"].ToString();
						DataRow[] ptbRow = ptb.Select("tankId = " + tankId);
						if (ptbRow.Length > 0)
						{
							ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) + Convert.ToInt32(stats["battles"])* btl;
							ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) + Convert.ToInt32(stats["dmg"])* btl;
							ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) + Convert.ToInt32(stats["spot"])* btl;
							ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) + Convert.ToInt32(stats["frags"])* btl;
							ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) + Convert.ToInt32(stats["def"])* btl;
							ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) + Convert.ToInt32(stats["cap"])* btl;
							ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) + Convert.ToInt32(stats["wins"])* btl;
						}
						count++;
						if (count > battleRevert) break;
					}
					end_val = Code.Rating.CalculatePlayerTankTotalWN8(ptb);
				}
			}
			// Show in center text
			aGauge1.CenterText = Math.Round(end_val, 2).ToString();
			aGauge1.CenterTextColor = Rating.WN8color(end_val);
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
			aGauge1.Value = (float)Math.Min(Math.Max(gaugeVal, 0), 3000);
		}

		private void btnTime_Click(object sender, EventArgs e)
		{
			BadButton b = (BadButton)sender;
			switch (b.Name)
			{
				case "btnTotal": GadgetHelper.SelectedTimeRangeWN8 = GadgetHelper.TimeRange.Total; break;
				case "btn1000": GadgetHelper.SelectedTimeRangeWN8 = GadgetHelper.TimeRange.Num1000; break;
				case "btn5000": GadgetHelper.SelectedTimeRangeWN8 = GadgetHelper.TimeRange.Num5000; break;
				case "btnMonth": GadgetHelper.SelectedTimeRangeWN8 = GadgetHelper.TimeRange.TimeMonth; break;
				case "btnWeek": GadgetHelper.SelectedTimeRangeWN8 = GadgetHelper.TimeRange.TimeWeek; break;
				case "btnToday": GadgetHelper.SelectedTimeRangeWN8 = GadgetHelper.TimeRange.TimeToday; break;
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
			switch (GadgetHelper.SelectedTimeRangeWN8)
			{
				case GadgetHelper.TimeRange.Total: btnTotal.Checked = true; break;
				case GadgetHelper.TimeRange.Num1000: btn1000.Checked = true; break;
				case GadgetHelper.TimeRange.TimeMonth: btnMonth.Checked = true; break;
				case GadgetHelper.TimeRange.TimeWeek: btnWeek.Checked = true; break;
				case GadgetHelper.TimeRange.TimeToday: btnToday.Checked = true; break;
			}
		}
	}
}
