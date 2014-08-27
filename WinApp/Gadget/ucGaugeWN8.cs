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

		private void ucGaugeWinRate_Load(object sender, EventArgs e)
		{
			// Wn8 - new sql to avoid battles where expexted value is missing
			string sql =
				"select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
				"  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " +
				"from playerTankBattle ptb left join " +
				"  playerTank pt on ptb.playerTankId=pt.id left join " +
				"  tank t on pt.tankId = t.id " +
				"where t.expDmg is not null and pt.playerId=@playerId and ptb.battleMode='15'";
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
			end_val = Code.Rating.CalculatePlayerTotalWN8("15");
			// Show in center text
			aGauge1.CenterText = Math.Round(end_val, 2).ToString();
			aGauge1.CenterTextColor = Rating.WN8color(end_val);
			// CALC NEEDLE MOVEMENT
			// AVG_STEP_VAL	= (END_VAL-START_VAL)/STEP_TOT
			avg_step_val = (end_val - aGauge1.ValueMin) / step_tot; // Define average movements per timer tick
			timer1.Enabled = true;
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
			aGauge1.Value = (float)Math.Min(Math.Max(gaugeVal, 0), 3000);
		}
	}
}
