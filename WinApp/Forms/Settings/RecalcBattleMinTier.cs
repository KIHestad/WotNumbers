﻿using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class RecalcBattleMinTier : FormCloseOnEsc
	{
		private static bool _autoRun = false;
		private static bool _processOnlyLastEntries = false; 
		public RecalcBattleMinTier(bool autoRun = false, bool processOnlyLastEntries = false)
		{
			InitializeComponent();
			_autoRun = autoRun;
			_processOnlyLastEntries = processOnlyLastEntries;
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
				"where tank.tier>0 " +
				"group by battle.id, battle.battleTime " +
				"ORDER BY battle.id";

			if (_processOnlyLastEntries)
			{
				sql += " DESC " +
					   "LIMIT " + Convert.ToString(Constants.LastEntriesSize);
			}

			DataTable dt = await DB.FetchData(sql);

			int tot = dt.Rows.Count;
			badProgressBar.ValueMax = tot + 2;

			int loopCount = 0;
			string updateSQL = "";
			string battleTime = "";

			UpdateProgressBar("Starting updates...", 1);
			foreach (DataRow dr in dt.Rows)
			{
				// Build SQL
				updateSQL += "UPDATE battle SET minBattleTier=" + dr["battleMinTier"].ToString() + " WHERE id=" + dr["battleId"].ToString() + "; " + Environment.NewLine;
				loopCount++;

				if (loopCount >= Constants.RecalcDataBatchSize)
				{
					battleTime = dr["battleTime"].ToString();
					UpdateProgressBar(GetProcessingString() + badProgressBar.Value + "/" + tot.ToString() + " " + battleTime, loopCount);
					await DB.ExecuteNonQuery(updateSQL, Config.Settings.showDBErrors, true);

					loopCount = 0;
					updateSQL = "";
				}
			}

			if (updateSQL != "") // Update last batch of sql's
			{
				UpdateProgressBar(GetProcessingString() + badProgressBar.Value + "/" + tot.ToString() + " " + battleTime, loopCount);
				await DB.ExecuteNonQuery(updateSQL, Config.Settings.showDBErrors, true);
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
