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
	public partial class ucGaugeWinRate : UserControl
	{
		string _battleMode = "";

		public ucGaugeWinRate(string battleMode = "")
		{
			InitializeComponent();
			_battleMode = battleMode;
		}

		private void ucGaugeWinRate_Load(object sender, EventArgs e)
		{
			// Overall stats team
			string sqlBattlemode = "";
			if (_battleMode != "")
			{
				sqlBattlemode = " and ptb.battleMode=@battleMode";
				DB.AddWithValue(ref sqlBattlemode, "@battleMode", _battleMode, DB.SqlDataType.VarChar);
			}
			string sql =
				"select sum(ptb.battles) as battles, sum(ptb.wins) as wins " +
				"from playerTankBattle ptb left join " +
				"  playerTank pt on ptb.playerTankId=pt.id " +
				"where pt.playerId=@playerId " + sqlBattlemode;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			float battles = 0;
			float wins = 0;
			if (dt.Rows.Count > 0)
			{
				DataRow dr = dt.Rows[0];
				if (dr["battles"] != DBNull.Value && Convert.ToInt32(dr["battles"]) > 0)
				{
					// Battle count
					battles = (float)Convert.ToDouble(dr["battles"]);
					// wins
					wins = (float)Convert.ToDouble(dr["wins"]);
					// Show in gauge
					end_val = wins / battles * 100; // end_val = Win Rate
					// Show in center text
					aGauge1.CenterText = Math.Round(wins / battles * 100, 2).ToString() + " %";
					aGauge1.CenterTextColor = Rating.WinRateColor(end_val);
					// Show battle mode
					string capText = "All Battle Modes";
					switch (_battleMode)
					{
						case "15" : capText = "Random / TC"; break;
						case "7" : capText = "Team"; break;
						case "Historical" : capText = "Historical Battles"; break;
						case "Skirmishes" : capText = "Skirmishes"; break;
					}
					aGauge1.CenterSubText = capText;
					// CALC NEEDLE MOVEMENT
					// AVG_STEP_VAL	= (END_VAL-START_VAL)/STEP_TOT
					avg_step_val = (end_val - aGauge1.ValueMin) / step_tot; // Define average movements per timer tick
					timer1.Enabled = true;
				}
			}
		}
		
		double avg_step_val = 0;
		double end_val = 0;
		double step_tot = 75;
		double step_count = 0;
		private void timer1_Tick(object sender, EventArgs e)
		{
			// AVG_STEP_VAL		(END_VAL-START_VAL)/STEP_TOT
			//BASE FORMULA		START_VAL + (EXP(1-(STEP_COUNT/STEP_TOTAL)) * STEP_COUNT * AVG_STEP_VAL
			step_count++;
			double gaugeVal = aGauge1.ValueMin + (Math.Exp(1 - (step_count / step_tot)) * step_count * avg_step_val);
			if (gaugeVal >= end_val)
			{
				gaugeVal = end_val;
				timer1.Enabled = false;
			}
			aGauge1.Value = (float)Math.Min(Math.Max(gaugeVal, 18), 82);
		}
	}
}
