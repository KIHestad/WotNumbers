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
	public partial class RecalcBattleCreditPerTank : FormCloseOnEsc
    {
		private static bool _autoRun = false;
        public RecalcBattleCreditPerTank(bool autoRun = false)
		{
			InitializeComponent();
			_autoRun = autoRun; 
		}

		private async void UpdateFromApi_Shown(object sender, EventArgs e)
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
			RecalcBattleCreditsPerTheme.Cursor = Cursors.WaitCursor;
			btnStart.Enabled = false;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;
            // Reset all old values
            UpdateProgressBar("Reset old values");
            string resetSQL =
                "UPDATE playerTankBattle SET " +
                "  credBtlCount=0, " +
                "  credAvgIncome=null, " +
                "  credAvgCost=null, " +
                "  credAvgResult=null, " +
                "  credMaxIncome=null, " +
                "  credMaxCost=null, " +
                "  credMaxResult=null, " +
                "  credTotIncome=null, " +
                "  credTotCost=null, " +
                "  credTotResult=null, " +
                "  credBtlLifetime=null ";
            await DB.ExecuteNonQuery(resetSQL);
			// Get battles
			UpdateProgressBar("Getting battle count");
            // Credits = total income
            // CreditsNet = total income - all cost pre calculated
            string sql =
                "select playerTank.id as playerTankId, tank.name as tankName, battle.battleMode as battleMode, " +
                "  sum(battle.battlesCount) as battlesCount, " +
                "  sum(credits) as credits, " +
                "  sum(creditsNet) as creditsNet, " +
                "  max(credits) as maxcredits, " +
                "  max(creditsNet) as maxcreditsNet, " +
                "  sum(battle.battleLifeTime) as battleLifeTime " +
                "from battle inner join playerTank on battle.playerTankId = playerTank.id inner join tank on playerTank.tankID = tank.Id " +
                "where credits is not null and creditsNet is not null " +
                "group by playerTank.id, tank.name, battle.battleMode " +
                "order by tank.name, battle.battleMode";
				
			DataTable dt = await DB.FetchData(sql);
			int tot = dt.Rows.Count;
			badProgressBar.ValueMax = tot + 2;
			sql = "";
			foreach (DataRow dr in dt.Rows)
			{
                TankCreditCalculation.TankCreditItem tci = new TankCreditCalculation.TankCreditItem();
                tci.battleCount = Convert.ToDouble(dr["battlesCount"]);
                tci.battleMode = dr["battleMode"].ToString();
                UpdateProgressBar("Calc credits " + badProgressBar.Value + "/" + tot.ToString() + " " + dr["tankName"].ToString() + " - " + tci.battleMode);
				// Update
                if (tci.battleCount > 0)
                {
                    tci.playerTankId = Convert.ToInt32(dr["playerTankId"]);
				    tci.creditsIncome = Convert.ToDouble(dr["credits"]);
                    tci.creditsNet = Convert.ToDouble(dr["creditsNet"]);
                    tci.maxcreditsIncome = Convert.ToDouble(dr["maxcredits"]);
                    tci.maxcreditsNet = Convert.ToDouble(dr["maxcreditsNet"]);
                    tci.battleLifeTime = Convert.ToDouble(dr["battleLifeTime"]);
                    string newSQL = TankCreditCalculation.CreateSQL(tci);                    
                    sql += newSQL;
                    if (sql.Length >= 5000) // Approx 50 updates
                    {
                        lblProgressStatus.Text = "Saving to database...";
                        await DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors, true);
                        sql = "";
                    }
                }
			}
			if (sql != "") // Update last batch of sql's
			{
                await DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors, true);
				sql = "";
			}

			// Done
			UpdateProgressBar("");
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
