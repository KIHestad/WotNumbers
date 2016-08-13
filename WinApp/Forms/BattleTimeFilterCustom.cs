using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class BattleTimeFilterCustom : FormCloseOnEsc
    {
		public BattleTimeFilterCustom()
		{
			InitializeComponent();
		}

		private void BattleTimeFilterCustom_Load(object sender, EventArgs e)
		{
			GridFilter.BattleTimeFilterCustomApply = false;
			calendarFrom.TodayDate = DateTime.Now;
			DateTime? d = null;
			if (Config.Settings.customBattleTimeFilter.from != null)
				d = Config.Settings.customBattleTimeFilter.from;
			chkUseFrom.Checked = (d != null);
			calendarFrom.EnabledState(chkUseFrom.Checked);
			SetTimeState(lblTimeFrom, txtTimeFrom, chkUseFrom.Checked);
			if (d != null)
			{
				DateTime calDate = Convert.ToDateTime(d);
				calendarFrom.SelectionStart = calDate;
				calendarFrom.SelectionEnd = calDate;
				txtTimeFrom.Text = calDate.Hour.ToString("00") + ":" + calDate.Minute.ToString("00");
			}

			calendarTo.TodayDate = DateTime.Now;
			d = Config.Settings.customBattleTimeFilter.to;
			chkUseTo.Checked = (d != null);
			calendarTo.EnabledState(chkUseTo.Checked);
			SetTimeState(lblTimeTo, txtTimeTo, chkUseTo.Checked);
			if (d != null)
			{
				DateTime calDate = Convert.ToDateTime(d);
				calendarTo.SelectionStart = calDate;
				calendarTo.SelectionEnd = calDate;
				txtTimeTo.Text = calDate.Hour.ToString("00") + ":" + calDate.Minute.ToString("00");
			}
		}

		private int GetHour(string txt)
		{
			int hour = 7;
			int len = txt.Length;
			if (len > 0)
			{
				if (len < 3)
					Int32.TryParse(txt, out hour);
				else 
					Int32.TryParse(txt.Substring(0, 2), out hour);
			}
			if (hour < 0) hour = 0;
			if (hour > 23) hour = 23;
			return hour;
		}

		private int GetMin(string txt)
		{
			int min = 0;
			int len = txt.Length;
			if (len > 2)
			{
				if (len == 4)
					Int32.TryParse(txt.Substring(2, 2), out min);
				else
					Int32.TryParse(txt.Substring(3, 2), out min);
			}
			if (min < 0) min = 0;
			if (min > 59) min = 59;
			return min;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (chkUseFrom.Checked)
			{
				DateTime d = calendarFrom.SelectionStart;
				int time = GetHour(txtTimeFrom.Text);
				int hour = GetMin(txtTimeFrom.Text);
				Config.Settings.customBattleTimeFilter.from =
					new DateTime(d.Year, d.Month, d.Day, time, hour, 0); // datefilter = today
			}
			else
				Config.Settings.customBattleTimeFilter.from = null;

			if (chkUseTo.Checked)
			{
				DateTime d = calendarTo.SelectionStart;
				int time = GetHour(txtTimeTo.Text);
				int hour = GetMin(txtTimeTo.Text);
				Config.Settings.customBattleTimeFilter.to =
					new DateTime(d.Year, d.Month, d.Day, time, hour, 0); // datefilter = today
			}
			else
				Config.Settings.customBattleTimeFilter.to = null;
			GridFilter.BattleTimeFilterCustomApply = true;
			this.Close();		
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void BattleTimeFilterCustom_Shown(object sender, EventArgs e)
		{
			calendarFrom.Visible = true;
			calendarTo.Visible = true;
		}

		private void SetTimeState(BadLabel lbl, BadTextBox txt, bool enabled)
		{
			txt.Enabled = enabled;
			lbl.Dimmed = !enabled;
		}

		private void chkUseFrom_Click(object sender, EventArgs e)
		{
			calendarFrom.EnabledState(chkUseFrom.Checked);
			SetTimeState(lblTimeFrom, txtTimeFrom, chkUseFrom.Checked);
			btnOK.Enabled = (chkUseFrom.Checked || chkUseFrom.Checked);
		}

		private void chkUseTo_Click(object sender, EventArgs e)
		{
			calendarTo.EnabledState(chkUseTo.Checked);
			SetTimeState(lblTimeTo, txtTimeTo, chkUseTo.Checked); 
			btnOK.Enabled = (chkUseFrom.Checked || chkUseFrom.Checked);
		}

		private void btnQuickSet1_Click(object sender, EventArgs e)
		{
			// Today
			chkUseFrom.Checked = true;
			calendarFrom.EnabledState(chkUseFrom.Checked);
			SetTimeState(lblTimeFrom, txtTimeFrom, chkUseFrom.Checked);
			chkUseTo.Checked = true;
			calendarTo.EnabledState(chkUseTo.Checked);
			SetTimeState(lblTimeTo, txtTimeTo, chkUseTo.Checked); 
			DateTime todayAdjustedForServerReset = DateTime.Now.AddHours(-7);
			calendarFrom.SelectionStart = todayAdjustedForServerReset;
			calendarFrom.SelectionEnd = todayAdjustedForServerReset;
			txtTimeFrom.Text = "07:00";
			calendarTo.SelectionStart = todayAdjustedForServerReset.AddDays(1);
			calendarTo.SelectionEnd = todayAdjustedForServerReset.AddDays(1);
			txtTimeTo.Text = "07:00";
			btnOK.Enabled = true;
			txtTimeFromFocused = false;
			txtTimeToFocused = false;
		}

		private void btnQuickSet2_Click(object sender, EventArgs e)
		{
			// From Now
			chkUseFrom.Checked = true;
			calendarFrom.EnabledState(chkUseFrom.Checked);
			SetTimeState(lblTimeFrom, txtTimeFrom, chkUseFrom.Checked);
			chkUseTo.Checked = false;
			calendarTo.EnabledState(chkUseTo.Checked);
			SetTimeState(lblTimeTo, txtTimeTo, chkUseTo.Checked);
			calendarFrom.SelectionStart = DateTime.Now;
			calendarFrom.SelectionEnd = DateTime.Now;
			txtTimeFrom.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
			btnOK.Enabled = true;
			txtTimeFromFocused = false;
			txtTimeToFocused = false;
		}

		private bool txtTimeFromFocused = false;
		private void txtTimeFrom_Click(object sender, EventArgs e)
		{
			if (!txtTimeFromFocused)
			{
				txtTimeFrom.SelectAll();
				txtTimeFromFocused = true;
				txtTimeToFocused = false;
			}
		}

		private bool txtTimeToFocused = false;
		private void txtTimeTo_Click(object sender, EventArgs e)
		{
			if (!txtTimeToFocused)
			{
				txtTimeTo.SelectAll();
				txtTimeFromFocused = false;
				txtTimeToFocused = true;
			}
		}

		private void calendarTo_DateChanged(object sender, DateRangeEventArgs e)
		{
			txtTimeFromFocused = false;
			txtTimeToFocused = false;
		}

		private void calendarFrom_DateChanged(object sender, DateRangeEventArgs e)
		{
			txtTimeFromFocused = false;
			txtTimeToFocused = false;
		}

		

	}
}
