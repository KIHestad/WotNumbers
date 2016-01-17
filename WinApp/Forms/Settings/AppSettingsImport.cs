using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms.Settings
{
    public partial class AppSettingsImport : UserControl
    {
        public AppSettingsImport()
        {
            InitializeComponent();
        }

        private void btnOpenWotStatDbFile_Click(object sender, EventArgs e)
        {
            // Select dossier file
            openFileWotStatDbFile.FileName = "*.db";
            openFileWotStatDbFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\WOT Statistics\\Hist_" + Config.Settings.playerName + "\\LastBattle";
            openFileWotStatDbFile.ShowDialog();
            txtWotStatDb.Text = openFileWotStatDbFile.FileName;
        }

        private void btnStartImport_Click(object sender, EventArgs e)
        {
            DateTime toDate = DateTime.Now;
            string s = txtToDate.Text.Trim();
            if (s.Length > 0)
            {
                bool dateOK = DateTime.TryParse(txtToDate.Text, out toDate);
                if (!dateOK)
                {
                    Code.MsgBox.Show("No valid date is entered", "Error in date textbox", (Form)this.TopLevelControl);
                    return;
                }
            }
            try
            {
                if (System.IO.File.Exists(txtWotStatDb.Text))
                {
                    // Connect to WoT Stat DB
                    String dbConnection = "Data Source=" + txtWotStatDb.Text;
                    SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                    cnn.Open();
                    SQLiteCommand mycommand = new SQLiteCommand(cnn);
                    // Get count
                    String sql = "SELECT COUNT(1) AS rowcount FROM RecentBattles";
                    mycommand.CommandText = sql;
                    SQLiteDataReader reader = mycommand.ExecuteReader();
                    int rowcount = 0;
                    foreach (var item in reader)
                    {
                        rowcount = Convert.ToInt32(reader["rowcount"]);
                    }
                    progressBarImport.ValueMax = rowcount;
                    progressBarImport.Value = 0;
                    ImportNow(toDate);

                    // Done
                    cnn.Close();
                }
                else
                {
                    Code.MsgBox.Show("Please select a WOT Statistics database file", "Missing file", (Form)this.TopLevelControl);
                }

            }
            catch (Exception ex)
            {
                Code.MsgBox.Show("Error occured, are you sure you have selected a WOT Statistics database file?" + Environment.NewLine + Environment.NewLine + ex.Message, "Error accesing WOT Statistics database file", (Form)this.TopLevelControl);
                // throw;
            }
        }

        private void ImportNow(DateTime toDate)
        {
            // Prepare
            btnOpenWotStatDbFile.Enabled = false;
            btnRemove.Enabled = false;
            btnStartImport.Enabled = false;
            txtToDate.Enabled = false;
            txtWotStatDb.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            this.Cursor = Cursors.WaitCursor;

            string WSDB = txtWotStatDb.Text;

            // SQLite dbconn
            String sqliteDbConnection = "Data Source=" + WSDB;
            SQLiteConnection sqliteConn = new SQLiteConnection(sqliteDbConnection);
            sqliteConn.Open();
            SQLiteCommand sqLiteCmd = new SQLiteCommand(sqliteConn);

            // Fetch WS recentBattles into datatable
            sqLiteCmd.CommandText = "SELECT rbId, rbTankId, rbCountryId, rbBattles, rbKills, rbDamageDealt, rbDamageReceived, rbSpotted, rbCapturePoints, rbDefencePoints, "
                                  + "rbSurvived, rbVictory, rbBattleTime, rbShot, rbHits, rbFragList, rbXPReceived, rbBattleMode FROM recentBattles";
            SQLiteDataReader reader = sqLiteCmd.ExecuteReader();
            DataTable recentBattles = new DataTable();
            recentBattles.Load(reader);

            if (recentBattles.Rows.Count > 0)
            {
                // Write recentBattles to db
                int i = 0;
                progressBarImport.Visible = true;
                double avgBattleTier = Rating.GetAverageBattleTier();
                while (i < recentBattles.Rows.Count)
                {
                    progressBarImport.Value++;
                    Application.DoEvents();
                    Refresh();
                    // Get battles first
                    int battlesCount = Convert.ToInt32(recentBattles.Rows[i]["rbBattles"]) / 100;
                    if (battlesCount == 1) // Only read battles from WoT Statistics for "single" battle results and when player tank is found
                    {
                        // Then get tankId and playerTankId
                        int wstankId = Convert.ToInt32(recentBattles.Rows[i]["rbTankId"]);
                        int wsCountryId = Convert.ToInt32(recentBattles.Rows[i]["rbCountryId"]);
                        int tankId = TankHelper.ConvertWs2TankId(wstankId, wsCountryId); // Convert WS tankId + countryID to tank.TankID
                        int playerTankId = TankHelper.GetPlayerTankId(tankId);
                        if (playerTankId != 0) // If not player tank is found skip
                        {
                            DateTime battleTime = Convert.ToDateTime("1970-01-01 01:00:00").AddSeconds(Convert.ToInt32(recentBattles.Rows[i]["rbBattleTime"]));
                            if (DateTime.Compare(battleTime, toDate) >= 0)
                            {
                                lblResult.Text = "Skipping battle: " + battleTime.ToString();
                            }
                            else
                            {
                                lblResult.Text = "Reading battle: " + battleTime.ToString();
                                int wsId = Convert.ToInt32(recentBattles.Rows[i]["rbId"]);
                                Rating.RatingParameters rp = new Rating.RatingParameters();
                                rp.BATTLES = battlesCount;
                                rp.FRAGS = Convert.ToInt32(recentBattles.Rows[i]["rbKills"]) / 100;
                                rp.DAMAGE = Convert.ToInt32(recentBattles.Rows[i]["rbDamageDealt"]) / 100;
                                rp.SPOT = Convert.ToInt32(recentBattles.Rows[i]["rbSpotted"]) / 100;
                                rp.CAP = Convert.ToInt32(recentBattles.Rows[i]["rbCapturePoints"]) / 100;
                                rp.DEF = Convert.ToInt32(recentBattles.Rows[i]["rbDefencePoints"]) / 100;
                                int dmgReceived = Convert.ToInt32(recentBattles.Rows[i]["rbDamageReceived"]) / 100;
                                int survived = Convert.ToInt32(recentBattles.Rows[i]["rbSurvived"]);
                                int killed = 1 - survived; // only one battle is imported, then this will work
                                int battleSurviveId = 2; // Survived some
                                if (recentBattles.Rows[i]["rbSurvived"].ToString() == "0") battleSurviveId = 3; // not survived
                                if (recentBattles.Rows[i]["rbSurvived"].ToString() == "1") battleSurviveId = 1; // survived
                                int victory = 0; // victory = no
                                int draw = 0; // draw = no
                                int defeat = 0; // defeat = no
                                if (recentBattles.Rows[i]["rbVictory"].ToString() == "0") victory = 1; // defeat = yes
                                int battleResultId = 4; // Several
                                if (recentBattles.Rows[i]["rbVictory"].ToString() == "0")
                                {
                                    battleResultId = 1; // victory
                                    victory = 1;
                                }
                                if (recentBattles.Rows[i]["rbVictory"].ToString() == "1")
                                {
                                    battleResultId = 3; // def
                                    defeat = 1;
                                }
                                if (recentBattles.Rows[i]["rbVictory"].ToString() == "2")
                                {
                                    battleResultId = 2; // draw
                                    draw = 1;
                                }
                                rp.WINS = victory;
                                int shots = Convert.ToInt32(recentBattles.Rows[i]["rbShot"]) / 100;
                                int hits = Convert.ToInt32(recentBattles.Rows[i]["rbHits"]) / 100;
                                int xp = Convert.ToInt32(recentBattles.Rows[i]["rbXPReceived"]) / 100;
                                string battleMode = recentBattles.Rows[i]["rbBattleMode"].ToString();
                                // Calc WN8
                                int wn8 = Convert.ToInt32(Math.Round(Rating.WN8battle(tankId, rp, true), 0));
                                // Calc EFF
                                int eff = Convert.ToInt32(Math.Round(Rating.EffBattle(tankId, rp), 0));
                                // Calc WN7
                                // Special tier calc
                                rp.TIER = avgBattleTier;
                                int wn7 = Convert.ToInt32(Math.Round(Rating.WN7battle(rp, true), 0));
                                // Insert or update Battle table
                                string sqlInsertBattle = "";
                                int battleId = TankHelper.GetBattleIdForImportedWsBattle(wsId);
                                if (battleId > 0)
                                    sqlInsertBattle =
                                    "update battle SET playerTankId=@playerTankId, battlesCount=@battlesCount, frags=@frags, dmg=@dmg, dmgReceived=@dmgReceived, spotted=@spotted, cap=@cap, def=@def, survived=@survived, killed=@killed, " +
                                    "  battleSurviveId=@battleSurviveId, victory=@victory, draw=@draw, defeat=@defeat, battleResultId=@battleResultId, battleTime=@battleTime, shots=@shots, hits=@hits, xp=@xp, battleMode=@battleMode, wn8=@wn8, eff=@eff, wn7=@wn7 " +
                                    "where wsId=@wsId ";
                                else
                                    sqlInsertBattle =
                                    "insert into battle (playerTankId, wsId, battlesCount, frags, dmg, dmgReceived, spotted, cap, def, survived, killed, battleSurviveId, victory, draw, defeat, battleResultId, battleTime, shots, hits, xp, battleMode, wn8, eff, wn7) " +
                                    "values (@playerTankId, @wsId, @battlesCount, @frags, @dmg, @dmgReceived, @spotted, @cap, @def, @survived, @killed,  @battleSurviveId, @victory, @draw, @defeat, @battleResultId, @battleTime, @shots, @hits, @xp, @battleMode, @wn8, @eff, @wn7)";
                                DB.AddWithValue(ref sqlInsertBattle, "@playerTankId", playerTankId, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@wsId", wsId, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@battlesCount", battlesCount, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@frags", rp.FRAGS, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@dmg", rp.DAMAGE, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@dmgReceived", dmgReceived, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@spotted", rp.SPOT, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@cap", rp.CAP, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@def", rp.DEF, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@survived", survived, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@killed", killed, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@battleSurviveId", battleSurviveId, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@victory", victory, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@draw", draw, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@defeat", defeat, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@battleResultId", battleResultId, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@battleTime", battleTime, DB.SqlDataType.DateTime);
                                DB.AddWithValue(ref sqlInsertBattle, "@shots", shots, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@hits", hits, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@xp", xp, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@battleMode", battleMode, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@wn7", wn7, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@wn8", wn8, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sqlInsertBattle, "@eff", eff, DB.SqlDataType.Int);
                                DB.ExecuteNonQuery(sqlInsertBattle);

                                // Get the last battleId if inserted
                                if (battleId == 0)
                                {
                                    DataTable dt = DB.FetchData("select max(id) as battleId from battle");
                                    if (dt.Rows.Count > 0)
                                    {
                                        battleId = Convert.ToInt32(dt.Rows[0][0]);
                                    }
                                    else battleId = 0;
                                }

                                // Insert Battle Frags
                                string wsfrags = recentBattles.Rows[i]["rbFragList"].ToString(); // Format: 0:0_2;1:0_21;2:0_10;3:2_17;4:1_16
                                if (wsfrags != "")
                                {
                                    List<Dossier2db.FragItem> fragList = new List<Dossier2db.FragItem>();
                                    string[] stringSeparators = new string[] { ";" };
                                    string[] fragItems = wsfrags.Split(stringSeparators, StringSplitOptions.None);
                                    foreach (string item in fragItems)
                                    {
                                        string newitem = item.Substring(item.IndexOf(":") + 1); // Format: 0:0_2 -> remove 2 first char = num sequence
                                        string[] stringSeparators2 = new string[] { "_" };
                                        string[] frag = newitem.Split(stringSeparators2, StringSplitOptions.None);
                                        int wsfragcountryId = Convert.ToInt32(frag[0]);
                                        int wsfragtankId = Convert.ToInt32(frag[1]);
                                        int fraggedtankId = TankHelper.ConvertWs2TankId(wsfragtankId, wsfragcountryId); // Convert WS tankId + countryID to tank.TankID
                                        bool fraggedTankExist = false;
                                        foreach (var fragListItem in fragList)
                                        {
                                            if (fragListItem.tankId == fraggedtankId)
                                            {
                                                fragListItem.fragCount++; // Add another kill on fragged tank
                                                fraggedTankExist = true;
                                            }
                                        }
                                        if (!fraggedTankExist)
                                        {
                                            Dossier2db.FragItem newFraggedTank = new Dossier2db.FragItem();
                                            newFraggedTank.tankId = fraggedtankId;
                                            newFraggedTank.fragCount = 1; // Kill on new fragged tank
                                            fragList.Add(newFraggedTank);
                                        }
                                    }
                                    string sqlInsertBattleFrag = "DELETE FROM battleFrag WHERE battleId = " + battleId.ToString() + ";" + Environment.NewLine;
                                    foreach (var battleFragItem in fragList)
                                    {
                                        if (battleFragItem.tankId != 0)
                                        {
                                            sqlInsertBattleFrag +=
                                                "INSERT INTO battleFrag (battleId, fraggedTankId, fragCount) VALUES (" +
                                                battleId.ToString() + ", " + battleFragItem.tankId + ", " + battleFragItem.fragCount + "); " + Environment.NewLine;
                                        }
                                    }
                                    DB.ExecuteNonQuery(sqlInsertBattleFrag);
                                }
                            }
                        }
                    }
                    i++; // Next Battle
                }

                // Done
                this.Cursor = Cursors.Default;
                this.Cursor = Cursors.Default;
                lblResult.Text = "All battles read";
                Code.MsgBox.Show("Imported " + i.ToString() + " battles.", "Import completed", (Form)this.TopLevelControl);
                //this.Close();
            }
            else
            {
                // Done
                this.Cursor = Cursors.Default;
                this.Cursor = Cursors.Default;
                lblResult.Text = "No battles found";
                Code.MsgBox.Show("The selected database contained no battles.", "Import completed", (Form)this.TopLevelControl);
            }
            //this.Close();


        }

        private void ImportWotStat_Load(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Code.MsgBox.Button answer = Code.MsgBox.Show("Do you want to remove previously imported battles from WoT Statistics?", "Remove previously imported battles", MsgBox.Type.OKCancel, (Form)this.TopLevelControl);
            if (answer == MsgBox.Button.OK)
            {
                string sql =
                    "delete from battleAch where battleId IN (select id from battle where wsId is not null); " +
                    "delete from battleFrag where battleId IN (select id from battle where wsId is not null); " +
                    "delete from battle where wsId is not null; ";
                DB.ExecuteNonQuery(sql);
                Code.MsgBox.Show("Previously imported battles from WoT Statistics is now removed", "Removed successfully", (Form)this.TopLevelControl);
            }
        }


    }
}
