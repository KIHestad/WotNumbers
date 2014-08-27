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
		double gaugeVal = 18;
		double gaugeStep = 1;
		double wr = 0;

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
					wr = wins / battles * 100;
					// Show in center text
					aGauge1.CenterText = Math.Round(wins / battles * 100, 2).ToString() + " %";
					aGauge1.CenterTextColor = Rating.WinRateColor(wr);
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
					gaugeStep = (wr - aGauge1.ValueMin) / 50; // Define default "speed" = step per movement
					timer1.Enabled = true;
				}
			}
			
		}
		double gaugeStepCount = 0; 
		private void timer1_Tick(object sender, EventArgs e)
		{
			// Define progress as factor 0-1
			gaugeStepCount += 2;
			double gaugeProgress = 1 - (gaugeVal / (aGauge1.ValueMax - aGauge1.ValueMin));
			double reducespeed = 1; 
			gaugeVal = aGauge1.ValueMin + (gaugeStep * gaugeStepCount * reducespeed);  // * Math.Pow(gaugeStepCount, 2);
			//Debug.WriteLine (gaugeVal);
			if (gaugeVal > wr)
			{
				gaugeVal = wr;
				timer1.Enabled = false;
			}
			aGauge1.Value = (float)Math.Min(Math.Max(gaugeVal, 18), 82);
		}
	}
}
