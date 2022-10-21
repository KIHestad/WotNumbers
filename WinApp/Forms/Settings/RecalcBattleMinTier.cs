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
	public partial class RecalcBattleMinTier : FormCloseOnEsc
	{
		private static bool _autoRun = false;
		public RecalcBattleMinTier(bool autoRun = false)
		{
			InitializeComponent();
			_autoRun = autoRun;
		}

		private async void RecalcBattleMinTier_Shown(object sender, EventArgs e)
		{
			if (_autoRun)
				await RunNow();
		}

		private string GetProcessingString()
		{
			return "Recalc Min Tier ";
		}
		private void UpdateProgressBar(string statusText, int count)
		{
			lblProgressStatus.Text = statusText;
			if (statusText == "")
				badProgressBar.Value = 0;
			else
				badProgressBar.Value += count;
			Refresh();
		}

		private async Task RunNow()
		{
			this.Cursor = Cursors.WaitCursor;
			RecalcBattleMinTierTheme.Cursor = Cursors.WaitCursor;
			btnStart.Enabled = false;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;
			// Get battles
			UpdateProgressBar("Getting battle count", 0);

			string sql = 
				"SELECT battle.id as battleId, battle.battleTime as battleTime, min(tank.tier) as battleMinTier " +
				"from battle " +
				" inner join battlePlayer on battle.id = battlePlayer.battleId  " +
				" inner join tank on battlePlayer.tankId = tank.id " +
				"group by battle.id, battle.battleTime " +
				"ORDER BY battle.id";
			DataTable dt = await DB.FetchData(sql);

			int tot = dt.Rows.Count;
			badProgressBar.ValueMax = tot + 2;
			sql = "";
			int loopCount = 0;
			string battleTime = "";

			UpdateProgressBar("Starting updates...", 1);
			foreach (DataRow dr in dt.Rows)
			{
				// Build SQL
				sql += "UPDATE battle SET minBattleTier=" + dr["battleMinTier"].ToString() + " WHERE id=" + dr["battleId"].ToString() + "; " + Environment.NewLine;
				loopCount++;
				if (loopCount >= Constants.RecalcDataBatchSize)
				{
					battleTime = dr["battleTime"].ToString();
					UpdateProgressBar(GetProcessingString() + badProgressBar.Value + "/" + tot.ToString() + " " + battleTime, loopCount);
					await DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors, true);

					loopCount = 0;
					sql = "";
				}
			}
			if (sql != "") // Update last batch of sql's
			{
				UpdateProgressBar(GetProcessingString() + badProgressBar.Value + "/" + tot.ToString() + " " + battleTime, loopCount);
				await DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors, true);
				sql = "";
			}

			// Done
			UpdateProgressBar("", 0);
			lblProgressStatus.Text = "Update finished: " + DateTime.Now.ToString();
			btnStart.Enabled = true;

			// Done
			this.Cursor = Cursors.Default;
			this.Close();
		}

		private async void btnStart_Click(object sender, EventArgs e)
		{
			await RunNow();
		}


	}
}
