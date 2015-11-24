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
	public partial class RecalcBattleCreditPerTank : Form
	{
		private static bool _autoRun = false;
        public RecalcBattleCreditPerTank(bool autoRun = false)
		{
			InitializeComponent();
			_autoRun = autoRun; 
		}

		private void UpdateFromApi_Shown(object sender, EventArgs e)
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
            DB.ExecuteNonQuery(resetSQL);
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
				
			DataTable dt = DB.FetchData(sql);
			int tot = dt.Rows.Count;
			badProgressBar.ValueMax = tot + 2;
			sql = "";
			foreach (DataRow dr in dt.Rows)
			{
                int playerTankId = Convert.ToInt32(dr["playerTankId"]);
				double battleCount = Convert.ToDouble(dr["battlesCount"]);
                double creditsIncome = Convert.ToDouble(dr["credits"]);
                double creditsNet = Convert.ToDouble(dr["creditsNet"]);
                double creditsCost = creditsIncome - creditsNet;
                double maxcreditsIncome = Convert.ToDouble(dr["maxcredits"]);
                double maxcreditsNet = Convert.ToDouble(dr["maxcreditsNet"]);
                double maxcreditsCost = maxcreditsIncome - maxcreditsNet;
                string battleMode = dr["battleMode"].ToString();
                double battleLifeTime = Convert.ToDouble(dr["battleLifeTime"]);
                UpdateProgressBar("Calc credits " + badProgressBar.Value + "/" + tot.ToString() + " " + dr["tankName"].ToString() + " - " + battleMode);
				// Update
                if (battleCount > 0)
                {
                    string newSQL =
                        "UPDATE playerTankBattle SET " +
                        "  credBtlCount=@battleCount, " +
                        "  credAvgIncome=@credAvgIncome, " +
                        "  credAvgCost=@credAvgCost, " +
                        "  credAvgResult=@credAvgResult, " +
                        "  credMaxIncome=@credMaxIncome, " +
                        "  credMaxCost=@credMaxCost, " +
                        "  credMaxResult=@credMaxResult, " +
                        "  credTotIncome=@credTotIncome, " +
                        "  credTotCost=@credTotCost, " +
                        "  credTotResult=@credTotResult, " +
                        "  credBtlLifetime=@credBtlLifetime " +
                        "WHERE playerTankId=@playerTankId and battleMode=@battleMode;";
                    DB.AddWithValue(ref newSQL, "@playerTankId", playerTankId, DB.SqlDataType.Int);
                    DB.AddWithValue(ref newSQL, "@battleMode", battleMode, DB.SqlDataType.VarChar);
                    DB.AddWithValue(ref newSQL, "@battleCount", battleCount, DB.SqlDataType.Int);
                    DB.AddWithValue(ref newSQL, "@credAvgIncome", Convert.ToInt32(creditsIncome / battleCount), DB.SqlDataType.Int);
                    DB.AddWithValue(ref newSQL, "@credAvgCost", Convert.ToInt32(creditsCost / battleCount), DB.SqlDataType.Int);
                    DB.AddWithValue(ref newSQL, "@credAvgResult", Convert.ToInt32(creditsNet / battleCount), DB.SqlDataType.Int);
                    DB.AddWithValue(ref newSQL, "@credMaxIncome", Convert.ToInt32(maxcreditsIncome), DB.SqlDataType.Int);
                    DB.AddWithValue(ref newSQL, "@credMaxCost", Convert.ToInt32(maxcreditsCost), DB.SqlDataType.Int);
                    DB.AddWithValue(ref newSQL, "@credMaxResult", Convert.ToInt32(maxcreditsNet), DB.SqlDataType.Int);
                    DB.AddWithValue(ref newSQL, "@credTotIncome", Convert.ToInt64(creditsIncome), DB.SqlDataType.Int);
                    DB.AddWithValue(ref newSQL, "@credTotCost", Convert.ToInt64(creditsCost), DB.SqlDataType.Int);
                    DB.AddWithValue(ref newSQL, "@credTotResult", Convert.ToInt64(creditsNet), DB.SqlDataType.Int);
                    DB.AddWithValue(ref newSQL, "@credBtlLifetime", Convert.ToInt32(battleLifeTime), DB.SqlDataType.Int);
                    sql += newSQL;
                    if (sql.Length >= 5000) // Approx 50 updates
                    {
                        lblProgressStatus.Text = "Saving to database...";
                        Application.DoEvents();
                        DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors, true);
                        sql = "";
                    }
                }
			}
			if (sql != "") // Update last batch of sql's
			{
				DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors, true);
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

		private void btnStart_Click(object sender, EventArgs e)
		{
			RunNow();
		}

		
	}
}
