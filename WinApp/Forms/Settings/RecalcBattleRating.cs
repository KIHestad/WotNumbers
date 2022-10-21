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
using static WinApp.Code.BattleHelper;

namespace WinApp.Forms
{
    public partial class RecalcBattleRating : FormCloseOnEsc
    {
        private bool _autoRun = false;
        private bool _forWN9 = true;
        private bool _forWN8 = true;
        private bool _forWN7 = false;
        private bool _forEFF = false;
        private bool _forPlayerPosition = false;
        private int _forBattleId = 0;

        public RecalcBattleRating(bool autoRun, bool forWN9, bool forWN8, bool forWN7, bool forEFF, bool forPlayerPosition, int forBattleId = 0)
        {
            InitializeComponent();
            _autoRun = autoRun;
            _forWN9 = forWN9;
            _forWN8 = forWN8;
            _forWN7 = forWN7;
            _forEFF = forEFF;
            _forPlayerPosition = forPlayerPosition;
            _forBattleId = forBattleId;
            chkLimitToolTip.SetToolTip(chkLimit,
                "By default the system only recalculates latest 1000 battles. " +
                "Check the checkbox to recalculate all battles, might take a long time.");
        }

        private async void UpdateFromApi_Shown(object sender, EventArgs e)
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
            if (_forPlayerPosition)
                ratings += "Player position, ";
            ratings = ratings.Substring(0, ratings.Length - 2);
            RecalcBattleWN8Theme.Text = "Recalculate battle " + ratings;
            // auto run, or show checkboix for limiting number of battles to process
            if (_autoRun)
                await RunNow();
            else if (_forBattleId == 0)
                chkLimit.Visible = (await BattleHelper.GetTotalBattleRows()) > 1000;

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
            RecalcBattleWN8Theme.Cursor = Cursors.WaitCursor;
            btnStart.Enabled = false;
            badProgressBar.Value = 0;
            badProgressBar.Visible = true;
            // Get battles
            UpdateProgressBar("Getting battle count");
            string battleWhere = "";
            string top = ""; // Sql server
            string limit = ""; // SQLite
            string records = "";
            if (_forBattleId != 0)
                battleWhere = "WHERE battle.id = " + _forBattleId.ToString() + " ";
            else
            {
                if (_autoRun)
                    records = _forPlayerPosition ? "100" : "1000";
                else if (!chkLimit.Checked)
                    records = "1000";
            }
            if (records != "")
            {
                if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
                    top = $"TOP {records}";
                else
                    limit = $"LIMIT {records}";
            }
            string sql =
                $"SELECT {top} battle.*, playerTank.tankId as tankId " +
                "FROM battle inner join playerTank on battle.playerTankId = playerTank.id " +
                battleWhere +
                "ORDER BY battle.id DESC " +
                limit;
            DataTable dt = await DB.FetchData(sql);

            int tot = dt.Rows.Count;
            badProgressBar.ValueMax = tot + 1;
            sql = "";
            int loopCount = 0;

            UpdateProgressBar("Starting updates...");
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
                string newSQL = "update battle set ";
                if (_forWN9)
                {
                    var GetWn9 = await Code.Rating.WN9.CalcBattle(tankId, rp);
                    double WN9 = Math.Round(GetWn9.WN9, 0);
                    newSQL += "wn9=@wn9, ";
                    DB.AddWithValue(ref newSQL, "@wn9", WN9, DB.SqlDataType.Int);
                }
                if (_forWN8)
                {
                    double WN8 = Math.Round(Code.Rating.WN8.CalcBattle(tankId, rp), 0);
                    newSQL += "wn8=@wn8, ";
                    DB.AddWithValue(ref newSQL, "@wn8", WN8, DB.SqlDataType.Int);
                }
                if (_forEFF)
                {
                    double EFF = Math.Round(Code.Rating.EFF.EffBattle(tankId, rp), 0);
                    newSQL += "eff=@eff, ";
                    DB.AddWithValue(ref newSQL, "@eff", EFF, DB.SqlDataType.Int);
                }
                if (_forWN7)
                {
                    rp.TIER = await Code.Rating.WNHelper.GetAverageTier();
                    double WN7 = Math.Round(Code.Rating.WN7.WN7battle(rp, true), 0);
                    newSQL += "wn7=@wn7, ";
                    DB.AddWithValue(ref newSQL, "@wn7", WN7, DB.SqlDataType.Int);
                }
                if (_forPlayerPosition)
                {
                    var positions = await BattleHelper.GetPlayerPositionInTeamLeaderboard(battleId); // Recalc position
                    newSQL += "posByXp=@posByXp, posByDmg=@posByDmg, ";
                    DB.AddWithValue(ref newSQL, "@posByXp", positions.PosByXp, DB.SqlDataType.Int);
                    DB.AddWithValue(ref newSQL, "@posByDmg", positions.PosByDmg, DB.SqlDataType.Int);
                }
                newSQL = newSQL.Substring(0, newSQL.Length - 2);
                newSQL += " where id=@id;";
                DB.AddWithValue(ref newSQL, "@id", battleId, DB.SqlDataType.Int);
                sql += newSQL;

                loopCount++;
                if (loopCount >= Constants.RecalcDataBatchSize)
                {
                    lblProgressStatus.Text = "Saving to database...";
                    await DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors, true);
                    
                    loopCount = 0;
                    sql = "";
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
