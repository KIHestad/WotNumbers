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
using WinApp.Code.FormLayout;

namespace WinApp.Gadget
{
	public partial class ucGaugeEFF : UserControl
	{
        private GadgetHelper.TimeRangeEnum _battleTimeSpan = GadgetHelper.TimeRangeEnum.Total;

        public ucGaugeEFF(GadgetHelper.TimeRangeEnum timeSpan)
		{
			InitializeComponent();
            _battleTimeSpan = timeSpan;
		}

		private void ucGauge_Load(object sender, EventArgs e)
		{
			SelectTimeRangeButton();
		}

		public void DataBind()
		{
			// Init Gauge
			aGauge1.ValueMin = 0;
			aGauge1.ValueMax = 2500;
			aGauge1.ValueScaleLinesMajorStepValue = 250;
			aGauge1.CenterSubText = "Efficiency: Random/TC";
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
            aGauge1.SetColorRanges(ColorRangeScheme.RangeEFF);
            
            
			// Overall stats team
            if (_battleTimeSpan == GadgetHelper.TimeRangeEnum.Total)
			{
                end_val = Code.Rating.EFF.EffTotal();
			}
			else // Check time range
			{
				int maxBattles = 0;
				string battleTimeFilter = "";
				DateTime dateFilter = DateTimeHelper.GetTodayDateTimeStart();
                switch (_battleTimeSpan)
				{
                    //case GadgetHelper.TimeRange.Num1000:
                    //    battleRevert = 1000;
                    //    break;
                    //case GadgetHelper.TimeRange.Num5000:
                    //    battleRevert = 5000;
                    //    break;
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
                end_val = Code.Rating.EFF.EffBattle(battleTimeFilter, maxBattles);	

			}
			// Show in gauge
			aGauge1.CenterText = Math.Round(end_val, 2).ToString();
            aGauge1.CenterTextColor = ColorRangeScheme.EffColor(end_val);
			// CALC NEEDLE MOVEMENT
			// AVG_STEP_VAL	= (END_VAL-START_VAL)/STEP_TOT
			avg_step_val = (end_val - aGauge1.ValueMin) / step_tot; // Define average movements per timer tick
			move_speed = Math.Abs(end_val - aGauge1.Value) / 30;
			if (move_speed > 40) move_speed = 40;
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
                case "btnTotal": _battleTimeSpan = GadgetHelper.TimeRangeEnum.Total; break;
                case "btnMonth3": _battleTimeSpan = GadgetHelper.TimeRangeEnum.TimeMonth3; break;
                case "btnMonth": _battleTimeSpan = GadgetHelper.TimeRangeEnum.TimeMonth; break;
                case "btnWeek": _battleTimeSpan = GadgetHelper.TimeRangeEnum.TimeWeek; break;
                case "btnToday": _battleTimeSpan = GadgetHelper.TimeRangeEnum.TimeToday; break;
			}
			SelectTimeRangeButton();
			DataBind();
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

		private void ucGaugeEFF_Paint(object sender, PaintEventArgs e)
		{
			if (BackColor == ColorTheme.FormBackSelectedGadget)
				GadgetHelper.DrawBorderOnGadget(sender, e);
		}
	}
}
