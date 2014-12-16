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
	public partial class RecalcBattleWN8 : Form
	{
		private static bool _autoRun = false;
		public RecalcBattleWN8(bool autoRun = false)
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
			RecalcBattleWN8Theme.Cursor = Cursors.WaitCursor;
			btnStart.Enabled = false;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;

			// Get battles
			UpdateProgressBar("Getting battle count");
			string sql = 
				"select battle.*, playerTank.tankId as tankId " +
				"from battle inner join playerTank on battle.playerTankId = playerTank.id " +
				"order by battle.battleTime";
			DataTable dt = DB.FetchData(sql);
			int tot = dt.Rows.Count;
			badProgressBar.ValueMax = tot + 1;
			sql = "";
			foreach (DataRow dr in dt.Rows)
			{
				UpdateProgressBar("Calc WN8 for battle " + badProgressBar.Value + "/" + tot.ToString() + " " + dr["battleTime"].ToString());
				int tankId = Convert.ToInt32(dr["tankId"]);
				double battleCount = Convert.ToDouble(dr["battlesCount"]);
				double dmg = Convert.ToDouble(dr["dmg"]);
				double spotted = Convert.ToDouble(dr["spotted"]);
				double frags = Convert.ToDouble(dr["frags"]);
				double def = Convert.ToDouble(dr["def"]);
				double wins = Convert.ToDouble(dr["victory"]);
				double WN8 = Math.Round(Rating.CalculateTankWN8(tankId, 1, dmg, spotted, frags, def, wins, true),0);
				string newSQL = "update battle set wn8=@wn8 where id=@id;";
				DB.AddWithValue(ref newSQL, "@wn8", WN8, DB.SqlDataType.Int);
				DB.AddWithValue(ref newSQL, "@id", Convert.ToInt32(dr["id"]), DB.SqlDataType.Int);
				sql += newSQL;
				if (sql.Length >= 4000) // Approx 100 updates
				{
					lblProgressStatus.Text = "Saving to database...";
					Application.DoEvents();
					DB.ExecuteNonQuery(sql,Config.Settings.showDBErrors,true);
					sql = "";
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
