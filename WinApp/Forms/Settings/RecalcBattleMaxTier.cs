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
	public partial class RecalcBattleMaxTier : FormCloseOnEsc
    {
		private static bool _autoRun = false;
        public RecalcBattleMaxTier(bool autoRun = false)
		{
			InitializeComponent();
			_autoRun = autoRun; 
		}

        private async void RecalcBattleMaxTier_Shown(object sender, EventArgs e)
		{
			if (_autoRun)
                await RunNow();
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
			RecalcBattleMaxTierTheme.Cursor = Cursors.WaitCursor;
			btnStart.Enabled = false;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;
            // Get battles
			UpdateProgressBar("Getting battle count", 0);
            // Credits = total income
            // CreditsNet = total income - all cost pre calculated
            string sql =
                "SELECT battle.id as battleId, battle.battleTime as battleTime, max(tank.tier) as battleMaxTier " +
                "from battle " +  
                " inner join battlePlayer on battle.id = battlePlayer.battleId  " +
                " inner join tank on battlePlayer.tankId = tank.id " +
                "group by battle.id, battle.battleTime " +
                "ORDER BY battle.id";
				
			DataTable dt = DB.FetchData(sql);
			int tot = dt.Rows.Count;
			badProgressBar.ValueMax = tot + 2;
			sql = "";
            int loopCount = 0;
            string updateSQL = "";
            string battleTime = "";
            UpdateProgressBar("Starting updates...", 1);
			foreach (DataRow dr in dt.Rows)
			{
                // Build SQL
                updateSQL += "UPDATE battle SET maxBattleTier=" + dr["battleMaxTier"].ToString() + " WHERE id=" + dr["battleId"].ToString() + "; " + Environment.NewLine;
                loopCount++;
                if (loopCount >= 20)
                {
                    battleTime = dr["battleTime"].ToString();
                    UpdateProgressBar("Calc Max Tier " + badProgressBar.Value + "/" + tot.ToString() + " " + battleTime, loopCount);
                    await DB.ExecuteNonQueryAsync(sql, Config.Settings.showDBErrors, true);
                    loopCount = 0;
                }
			}
            if (updateSQL != "") // Update last batch of sql's
			{
                UpdateProgressBar("Calc Max Tier " + badProgressBar.Value + "/" + tot.ToString() + " " + battleTime, loopCount);
                await DB.ExecuteNonQueryAsync(sql, Config.Settings.showDBErrors, true);
			}

			// Done
			UpdateProgressBar("",0);
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
