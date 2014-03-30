using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater
{
    public static class ImportWS2DB
    {
        
        public static void importWotStats2DB()
        {
            string WSDB = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\WOT Statistics\\Hist_" + Config.Settings.playerName + "\\LastBattle\\WOTSStore.db";

            // SQLite dbconn
            String sqliteDbConnection = "Data Source=" + WSDB;
            SQLiteConnection sqliteConn = new SQLiteConnection(sqliteDbConnection);
            sqliteConn.Open();
            SQLiteCommand sqLiteCmd = new SQLiteCommand(sqliteConn);

            // SQL Server dbconn
            SqlConnection sqlConn = new SqlConnection(Config.DatabaseConnection());
            sqlConn.Open();

            // Create temp table for import
            string sql = "create table wsRecentBattles (rbId int, rbTankId int, rbCountryId int, rbBattles int, rbKills int, rbDamageDealt int, rbDamageReceived int,"
                       + "rbSpotted int, rbCapturePoints int, rbDefencePoints int, rbSurvived int, rbVictory int, rbBattleTime int, rbShot int, rbHits int, rbBattleMode int); ";
            SqlCommand startup = new SqlCommand(sql, sqlConn);
            startup.ExecuteNonQuery();


            // Fetch WS recentBattles into datatable
            sqLiteCmd.CommandText = "SELECT rbId, rbTankId, rbCountryId, rbBattles, rbKills, rbDamageDealt, rbDamageReceived, rbSpotted, rbCapturePoints, rbDefencePoints, "
                                  + "rbSurvived, rbVictory, rbBattleTime, rbShot, rbHits, rbBattleMode FROM recentBattles";  //rbFragList
            SQLiteDataReader reader = sqLiteCmd.ExecuteReader();
            DataTable recentBattles = new DataTable();
            recentBattles.Load(reader);

            // Write recentBattles to db
            int i = 0;
            sql = "insert into wsRecentBattles (rbId, rbTankId, rbCountryId, rbBattles, rbKills, rbDamageDealt, rbDamageReceived, rbSpotted, rbCapturePoints, rbDefencePoints, "
                + "rbSurvived, rbVictory, rbBattleTime, rbShot, rbHits, rbBattleMode) values (@rbId, @rbTankId, @rbCountryId, @rbBattles, @rbKills, @rbDamageDealt, "
                + "@rbDamageReceived, @rbSpotted, @rbCapturePoints, @rbDefencePoints, @rbSurvived, @rbVictory, @rbBattleTime, @rbShot, @rbHits, @rbBattleMode)";
            SqlCommand insertRB = new SqlCommand(sql, sqlConn);
            while (i < recentBattles.Rows.Count)
            {
                insertRB.Parameters.Clear();
                insertRB.Parameters.AddWithValue("@rbId", recentBattles.Rows[i]["rbId"].ToString());
                insertRB.Parameters.AddWithValue("@rbTankId", recentBattles.Rows[i]["rbTankId"].ToString());
                insertRB.Parameters.AddWithValue("@rbCountryId", recentBattles.Rows[i]["rbCountryId"].ToString());
                insertRB.Parameters.AddWithValue("@rbBattles", recentBattles.Rows[i]["rbBattles"].ToString());
                insertRB.Parameters.AddWithValue("@rbKills", recentBattles.Rows[i]["rbKills"].ToString());
                insertRB.Parameters.AddWithValue("@rbDamageDealt", recentBattles.Rows[i]["rbDamageDealt"].ToString());
                insertRB.Parameters.AddWithValue("@rbDamageReceived", recentBattles.Rows[i]["rbDamageReceived"].ToString());
                insertRB.Parameters.AddWithValue("@rbSpotted", recentBattles.Rows[i]["rbSpotted"].ToString());
                insertRB.Parameters.AddWithValue("@rbCapturePoints", recentBattles.Rows[i]["rbCapturePoints"].ToString());
                insertRB.Parameters.AddWithValue("@rbDefencePoints", recentBattles.Rows[i]["rbDefencePoints"].ToString());
                insertRB.Parameters.AddWithValue("@rbSurvived", recentBattles.Rows[i]["rbSurvived"].ToString());
                insertRB.Parameters.AddWithValue("@rbVictory", recentBattles.Rows[i]["rbVictory"].ToString());
                insertRB.Parameters.AddWithValue("@rbBattleTime", recentBattles.Rows[i]["rbBattleTime"].ToString());
                insertRB.Parameters.AddWithValue("@rbShot", recentBattles.Rows[i]["rbShot"].ToString());
                insertRB.Parameters.AddWithValue("@rbHits", recentBattles.Rows[i]["rbHits"].ToString());
                insertRB.Parameters.AddWithValue("@rbBattleMode", recentBattles.Rows[i]["rbBattleMode"].ToString());
                insertRB.ExecuteNonQuery();
                i++;
            }

            // Insert into battles using stored proc
            SqlCommand runProc = new SqlCommand("exec importBattle", sqlConn);
            runProc.ExecuteNonQuery();

            // Remove temp tables
            SqlCommand cleanup = new SqlCommand("drop table wsRecentBattles", sqlConn);
            cleanup.ExecuteNonQuery();

            // Close db connections
            sqlConn.Close();
            sqliteConn.Close();
        }
    }
}