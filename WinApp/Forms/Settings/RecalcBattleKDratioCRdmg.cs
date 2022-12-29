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
	public partial class RecalcBattleKDratioCRdmg : FormCloseOnEsc
    {
		private static bool _autoRun = false;
		public RecalcBattleKDratioCRdmg(bool autoRun = true)
		{
			InitializeComponent();
			_autoRun = autoRun; 
		}

		private async void RecalcBattleKDratioCRdmg_Shown(object sender, EventArgs e)
		{
			if (_autoRun)
                await RunNow();
		}

		private void UpdateProgressBar(string statusText)
		{
			lblProgressStatus.Text = statusText;
			if (statusText == "")
				badProgressBar.Value = 0;
			else
				badProgressBar.Value++;
			Refresh();
		}

		private async Task RunNow()
		{
			this.Cursor = Cursors.WaitCursor;
			UpdateFromApiTheme.Cursor = Cursors.WaitCursor;
			btnStart.Enabled = false;
			badProgressBar.ValueMax = 100;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;

            await RecalcNow();

			// Done
			this.Cursor = Cursors.Default;
			this.Close();
		}

		private async void btnStart_Click(object sender, EventArgs e)
		{
            await RunNow();
		}

		private async Task RecalcNow()
		{
			// Loop through all existing battles with battle players
			string sql =
				"SELECT battle.id as battleId, battle.battleTime, player.name " +
				"FROM battlePlayer INNER JOIN " +
				"  battle ON battlePlayer.battleId = battle.id INNER JOIN " +
				"  playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
				"  player ON playerTank.playerId = player.id " +
				"GROUP BY battle.id, battle.battleTime, player.name;";
			DataTable dt = await DB.FetchData(sql);

			int tot = dt.Rows.Count; 
			badProgressBar.ValueMax = tot;	// Progress
			string updateSql = "";
			int loopCount = 0;

			UpdateProgressBar("Starting updates...");
			foreach (DataRow dr in dt.Rows)
			{
				// Current battle to check
				string battleId = dr["battleId"].ToString();
				string playerName = dr["playerName"].ToString();
				// Show progress
				UpdateProgressBar("Battle #" + battleId + " - " + Convert.ToDateTime(dr["battleTime"]));
				// Get all battlePlayers
				string selectSql = "SELECT team, deathReason, name FROM battlePlayer WHERE battleId=" + battleId;
				DataTable battlePlayers = await DB.FetchData(selectSql);
				// Find players team (1/2) and enemy team (1/2) for battle
				DataRow[] drTemp = battlePlayers.Select("name = '" + playerName + "'");
				int playerTeam = 1;
				if (drTemp.Length > 0)
					playerTeam = Convert.ToInt32(drTemp[0]["team"]);

				//selectSql = "SELECT team FROM battlePlayer WHERE battleid=" + battleId + " AND name=@playerName";
				//DB.AddWithValue(ref sql, "@playerName", playerName, DB.SqlDataType.VarChar);
				//dtTemp = await DB.FetchData(selectSql);
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
				updateSql +=
					"UPDATE battle " +
					"SET survivedteam=" + survivedTeam.ToString() + ", survivedenemy=" + survivedEnemy.ToString() + ", " +
					" fragsteam=" + fragsTeam.ToString() + ", fragsenemy=" + fragsEnemy.ToString() + " " +
					"WHERE id = " + dr["battleId"].ToString() + ";";

				loopCount++;
				if (loopCount >= Constants.RecalcDataBatchSize)
				{
					await DB.ExecuteNonQuery(updateSql, RunInBatch: true);
					loopCount = 0;
					updateSql = "";
				}
			}

			if (updateSql != "")
			{
				await DB.ExecuteNonQuery(updateSql, RunInBatch: true);
			}
		}
	}
}
