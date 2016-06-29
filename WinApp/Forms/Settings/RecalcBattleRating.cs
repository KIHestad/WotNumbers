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
	public partial class RecalcBattleRating : Form
	{
		private bool _autoRun = false;
        private bool _forWN9 = true;
        private bool _forWN8 = true;
        private bool _forWN7 = false;
        private bool _forEFF = false;
        private int _forBattleId = 0;

        public RecalcBattleRating(bool autoRun, bool forWN9, bool forWN8, bool forWN7, bool forEFF, int forBattleId = 0)
		{
			InitializeComponent();
			_autoRun = autoRun;
            _forWN9 = forWN9;
            _forWN8 = forWN8;
            _forWN7 = forWN7;
            _forEFF = forEFF;
            _forBattleId = forBattleId;
		}

		private void UpdateFromApi_Shown(object sender, EventArgs e)
		{
            string ratings = "";
            if (_forWN9)
                ratings += "WN9, ";
            if (_forWN8)
                ratings += "WN8, ";
            if (_forWN7)
                ratings += "WN7, ";
            if (_forEFF)
                ratings += "EFF, ";
            ratings = ratings.Substring(0, ratings.Length - 2);
            RecalcBattleWN8Theme.Text = "Recalculate battle " + ratings;
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
            string battleWhere = "";
            if (_forBattleId != 0)
                battleWhere = "WHERE battle.id = " + _forBattleId.ToString() + " ";
			string sql = 
				"select battle.*, playerTank.tankId as tankId " +
				"from battle inner join playerTank on battle.playerTankId = playerTank.id " +
                battleWhere +
				"order by battle.battleTime";
			DataTable dt = DB.FetchData(sql);
			int tot = dt.Rows.Count;
			badProgressBar.ValueMax = tot + 1;
			sql = "";
			foreach (DataRow dr in dt.Rows)
			{
                int battleId = Convert.ToInt32(dr["id"]);
                UpdateProgressBar("Calc for battle " + badProgressBar.Value + "/" + tot.ToString() + " " + dr["battleTime"].ToString());
				int tankId = Convert.ToInt32(dr["tankId"]);
                Code.Rating.WNHelper.RatingParameters rp = new Code.Rating.WNHelper.RatingParameters();
				rp.BATTLES = Convert.ToDouble(dr["battlesCount"]);
				rp.DAMAGE = Convert.ToDouble(dr["dmg"]);
				rp.SPOT = Convert.ToDouble(dr["spotted"]);
				rp.FRAGS = Convert.ToDouble(dr["frags"]);
				rp.DEF = Convert.ToDouble(dr["def"]);
				rp.WINS = Convert.ToDouble(dr["victory"]);
                // Create sql and get ratings
                double WN9 = 0;
                double WN8 = 0;
                double WN7 = 0;
                double EFF = 0;
                string newSQL = "update battle set ";
                if (_forWN9)
                {
                    WN9 = Math.Round(Code.Rating.WN9.CalcBattle(tankId, rp), 0);
                    newSQL += "wn9=@wn9, ";
                    DB.AddWithValue(ref newSQL, "@wn9", WN9, DB.SqlDataType.Int);
                }
                if (_forWN8)
                {
                    WN8 = Math.Round(Code.Rating.WN8.CalcBattle(tankId, rp), 0);
                    newSQL += "wn8=@wn8, ";
                    DB.AddWithValue(ref newSQL, "@wn8", WN8, DB.SqlDataType.Int);
                }
                if (_forEFF)
                {
                    EFF = Math.Round(Code.Rating.EFF.EffBattle(tankId, rp), 0);
                    newSQL += "eff=@eff, ";
                    DB.AddWithValue(ref newSQL, "@eff", EFF, DB.SqlDataType.Int);
                }
                if (_forWN7)
                {
                    rp.TIER = Code.Rating.WNHelper.GetAverageTier();
                    WN7 = Math.Round(Code.Rating.WN7.WN7battle(rp, true), 0); 
                    newSQL += "wn7=@wn7, ";
                    DB.AddWithValue(ref newSQL, "@wn7", WN7, DB.SqlDataType.Int);
                }
                newSQL = newSQL.Substring(0, newSQL.Length - 2);
                newSQL += " where id=@id;";
				DB.AddWithValue(ref newSQL, "@id", battleId, DB.SqlDataType.Int);
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
