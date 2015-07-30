using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class RecalcBattleKDratioCRdmg : Form
	{
		private static bool _autoRun = false;
		public RecalcBattleKDratioCRdmg(bool autoRun = true)
		{
			InitializeComponent();
			_autoRun = autoRun; 
		}

		private void RecalcBattleKDratioCRdmg_Shown(object sender, EventArgs e)
		{
			if (_autoRun)
				RunNow();
		}

		private void UpdateProgressBar(string statusText)
		{
			lblProgressStatus.Text = statusText;
			if (statusText == "")
				badProgressBar.Value = 0;
			else
				badProgressBar.Value++;
			Refresh();
			Application.DoEvents();
		}

		private void RunNow()
		{
			this.Cursor = Cursors.WaitCursor;
			UpdateFromApiTheme.Cursor = Cursors.WaitCursor;
			btnStart.Enabled = false;
			badProgressBar.ValueMax = 100;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;

			RecalcNow();

			// Done
			this.Cursor = Cursors.Default;
			this.Close();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			RunNow();
		}

		private void RecalcNow()
		{
			// Loop through all existing battles with battle players
			string sql =
				"SELECT battle.id as battleId, battle.battleTime, player.name " +
				"FROM battlePlayer INNER JOIN " +
				"  battle ON battlePlayer.battleId = battle.id INNER JOIN " +
				"  playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
				"  player ON playerTank.playerId = player.id " +
				"GROUP BY battle.id, battle.battleTime, player.name;";
			DataTable dt = DB.FetchData(sql);
			string updatesql = "";
			// Progress
			badProgressBar.ValueMax = dt.Rows.Count;
			// Loop through all battles
			foreach (DataRow dr in dt.Rows)
			{
				// Current battle to check
				string battleId = dr["battleId"].ToString();
				string playerName = PlayerHelper.GetPlayerNameFromNameAndServer(dr["name"].ToString());
				// Show progress
				UpdateProgressBar("Battle #" + battleId + " - " + Convert.ToDateTime(dr["battleTime"]));
				// Get all battlePlayers
				sql = "SELECT team, deathReason, name FROM battlePlayer WHERE battleId=" + battleId;
				DataTable battlePlayers = DB.FetchData(sql);
				// Find players team (1/2) and enemy team (1/2) for battle
				DataRow[] drTemp = battlePlayers.Select("name = '" + playerName + "'");
				int playerTeam = 1;
				if (drTemp.Length > 0)
					playerTeam = Convert.ToInt32(drTemp[0]["team"]);

				//sql = "SELECT team FROM battlePlayer WHERE battleid=" + battleId + " AND name=@playerName";
				//DB.AddWithValue(ref sql, "@playerName", playerName, DB.SqlDataType.VarChar);
				//dtTemp = DB.FetchData(sql);
				//if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0][0] != DBNull.Value)
				//	playerTeam = Convert.ToInt32(dtTemp.Rows[0][0]);
				int enemyTeam = 1;
				if (playerTeam == 1) enemyTeam = 2;
				// Find survival count for battle for player team
				int survivedTeam = battlePlayers.Select("deathReason = '-1' and team=" + playerTeam).Length;
				// Find survival count for enemy team
				int survivedEnemy = battlePlayers.Select("deathReason = '-1' and team=" + enemyTeam).Length;
				// Find frags count for battle for player team
				int fragsTeam = battlePlayers.Select("deathReason <> '-1' and team=" + enemyTeam).Length;
				// Find frags count for enemy team
				int fragsEnemy = battlePlayers.Select("deathReason <> '-1' and team=" + playerTeam).Length;
				
				// Create update sql
				updatesql +=
					"UPDATE battle " +
					"SET survivedteam=" + survivedTeam.ToString() + ", survivedenemy=" + survivedEnemy.ToString() + ", " +
					" fragsteam=" + fragsTeam.ToString() + ", fragsenemy=" + fragsEnemy.ToString() + " " +
					"WHERE id = " + dr["battleId"].ToString() + ";";
			}
			if (updatesql != "")
				DB.ExecuteNonQuery(updatesql, RunInBatch: true);
		}
	}
}
