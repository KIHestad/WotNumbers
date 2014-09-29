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
	public partial class ucGaugeDmgCausedReceived : UserControl
	{
		string _battleMode = "";
		GadgetHelper.TimeRange SelectedTimeRange = GadgetHelper.TimeRange.Total;

		public ucGaugeDmgCausedReceived(string battleMode = "")
		{
			InitializeComponent();
			_battleMode = battleMode;
		}

		private void ucGauge_Load(object sender, EventArgs e)
		{
			SelectedTimeRange = GadgetHelper.TimeRange.Total;
			DataBind();
		}

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			DataBind();
			base.OnInvalidated(e);
		}

		public void DataBind()
		{
			// Colors 0-8
			for (byte i = 0; i <= 8; i++)
			{
				aGauge1.Range_Idx = i;
				if (i == 0)
					aGauge1.RangesStartValue[i] = aGauge1.ValueMin;
				else
					aGauge1.RangesStartValue[i] = (float)Rating.rangeKillDeath[i];
				if (i == 8)
					aGauge1.RangesEndValue[i] = aGauge1.ValueMax;
				else
					aGauge1.RangesEndValue[i] = (float)Rating.rangeKillDeath[i + 1];
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
			lblBattleMode.Text = capText;
			string sqlBattlemode = "";
			string sql = "";
			if (SelectedTimeRange == GadgetHelper.TimeRange.Total)
			{
				if (_battleMode != "")
				{
					sqlBattlemode = " AND (playerTankBattle.battleMode = @battleMode) ";
					DB.AddWithValue(ref sqlBattlemode, "@battleMode", _battleMode, DB.SqlDataType.VarChar);
				}
				sql =
					"SELECT SUM(playerTankBattle.dmg) AS dmg, SUM(dmgReceived) AS dmgReceived " +
					"FROM   playerTankBattle INNER JOIN " +
					"		playerTank ON playerTankBattle.playerTankId = playerTank.id " +
					"WHERE  (playerTank.playerId = @playerId) " + sqlBattlemode;
			}
			else
			{
				// Create Battle Time filer
				DateTime dateFilter = new DateTime();
				DateTime basedate = DateTime.Now; // current time
				if (DateTime.Now.Hour < 7) basedate = DateTime.Now.AddDays(-1); // correct date according to server reset 07:00 AM
				dateFilter = new DateTime(basedate.Year, basedate.Month, basedate.Day, 7, 0, 0); // datefilter = today
				// Adjust time scale according to selected filter
				switch (SelectedTimeRange)
				{
					case GadgetHelper.TimeRange.TimeWeek:
						dateFilter = dateFilter.AddDays(-7);
						break;
					case GadgetHelper.TimeRange.TimeMonth:
						dateFilter = dateFilter.AddMonths(-1);
						break;
					case GadgetHelper.TimeRange.TimeMonth3:
						dateFilter = dateFilter.AddMonths(-3);
						break;
				}
				if (_battleMode != "")
				{
					sqlBattlemode = " AND (battle.battleMode = @battleMode) ";
					DB.AddWithValue(ref sqlBattlemode, "@battleMode", _battleMode, DB.SqlDataType.VarChar);
				}
				sql =
					"SELECT SUM(battle.dmg) AS dmg, SUM(battle.dmgReceived) AS dmgReceived " +
					"FROM   battle INNER JOIN " +
					"       playerTank ON battle.playerTankId = playerTank.id " +
					"WHERE  (battle.battleTime >= @battleTime) AND (playerTank.playerId = @playerId) " + sqlBattlemode;
				DB.AddWithValue(ref sql, "@battleTime", dateFilter.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
			}
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			double dmg = 0;
			double dmgReceived = 0;
			gaugeVal = 0;
			if (dt.Rows.Count > 0 && dt.Rows[0]["dmg"] != DBNull.Value)
			{
				dmg = Convert.ToDouble(dt.Rows[0]["dmg"]);
				dmgReceived = Convert.ToDouble(dt.Rows[0]["dmgReceived"]);
				gaugeVal = Math.Round((dmg / dmgReceived), 2);
			}
			lblLeft.Text = Shorten(dmg);
			lblRight.Text = Shorten(dmgReceived);
			lblCenter.Text = gaugeVal.ToString();
			lblCenter.ForeColor = Rating.KillDeathColor(gaugeVal);
			speed = (gaugeVal - aGauge1.Value) / 30;
			increase = (speed > 0);
			timer1.Enabled = true;
		}

		private string Shorten(double val)
		{
			string s = "";
			if (val > 1000000)
				s = Math.Round((val / 1000000), 1).ToString() + "M";
			else if (val > 1000)
				s = Math.Round((val / 1000), 1).ToString() + "K";
			else
				s = val.ToString();
			return s;
		}
		
		private void btnTime_Click(object sender, EventArgs e)
		{
			BadButton b = (BadButton)sender;
			btn3M.Checked = false;
			btnMonth.Checked = false;
			btnToday.Checked = false;
			btnTotal.Checked = false;
			btnWeek.Checked = false;
			b.Checked = true;
			switch (b.Name)
			{
				case "btnTotal": SelectedTimeRange = GadgetHelper.TimeRange.Total; break;
				case "btn3M": SelectedTimeRange = GadgetHelper.TimeRange.TimeMonth3; break;
				case "btnMonth": SelectedTimeRange = GadgetHelper.TimeRange.TimeMonth; break;
				case "btnWeek": SelectedTimeRange = GadgetHelper.TimeRange.TimeWeek; break;
				case "btnToday": SelectedTimeRange = GadgetHelper.TimeRange.TimeToday; break;
			}
			DataBind();
		}

		private void ucGauge_Paint(object sender, PaintEventArgs e)
		{
			if (BackColor == ColorTheme.FormBackSelectedGadget)
				GadgetHelper.DrawBorderOnGadget(sender, e);
		}

		private double gaugeVal = 0;
		private double speed = 0.1;
		private bool increase = true;
		private void timer1_Tick(object sender, EventArgs e)
		{
			double newGaugeVal = aGauge1.Value + speed;
			if (increase && newGaugeVal >= gaugeVal || !increase && newGaugeVal < gaugeVal)
			{
				timer1.Enabled = false;
				newGaugeVal = gaugeVal;
			}
			else
			{
				if (Math.Abs(gaugeVal - newGaugeVal) < 0.6)
					speed = speed * 0.93; // drop speed
				if (increase && speed < 0.002)
					speed = 0.002;
				else if (!increase && speed > -0.002)
					speed = -0.002;
					
			}
			aGauge1.Value = (float)newGaugeVal;
		}
	}
}
