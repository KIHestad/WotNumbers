using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

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
					aGauge1.Value = wins / battles * 100;
					// Show in center text
					aGauge1.CenterText = Math.Round(wins / battles * 100, 2).ToString() + " %";
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
				}
			}
			
		}
	}
}
