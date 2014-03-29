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
            string WSDB = "C:\\Users\\Kontor\\AppData\\Roaming\\WOT Statistics\\Hist_cmdrTrinity\\LastBattle\\WOTSStore.db";

            // SQLite dbconn
            String sqliteDbConnection = "Data Source=" + WSDB;
            SQLiteConnection sqliteConn = new SQLiteConnection(sqliteDbConnection);
            sqliteConn.Open();
            SQLiteCommand sqLiteCmd = new SQLiteCommand(sqliteConn);

            // SQL Server dbconn
            SqlConnection sqlConn = new SqlConnection(Config.DatabaseConnection());
            sqlConn.Open();

            // Delete from tmp tables
            SqlCommand cleanup = new SqlCommand("delete from tankId; delete from wsRecentBattles", sqlConn);
            cleanup.ExecuteNonQuery();

            // Fetch tank id from WS into datatable
            sqLiteCmd.CommandText = "SELECT distinct cmTankId, cmCountryId, cmTankTitle FROM file_TankDetails";
            SQLiteDataReader reader = sqLiteCmd.ExecuteReader();
            DataTable wsTankId = new DataTable();
            wsTankId.Load(reader);

            // Fetch tank id from SQL server into datatable
            SqlCommand sqlCmd = new SqlCommand("select t.id tankId, t.name tankName, pt.id playerTankId from tank t inner join playerTank pt on t.id = pt.tankId", sqlConn);
            sqlCmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
            DataTable playerTankId = new DataTable();
            adapter.Fill(playerTankId);

            // Join datatables on tank name 
            var result = from ws in wsTankId.AsEnumerable()
                         join t in playerTankId.AsEnumerable()
                         on ws.Field<string>("cmTankTitle") equals t.Field<string>("tankName")
                         select new
                         {
                             tankId = t["tankId"],
                             tankName = ws["cmTankTitle"],
                             playerTankId = t["playerTankId"],
                             cmTankId = ws["cmTankId"],
                             cmCountryId = ws["cmCountryId"]
                         };

            // Write joined tankdata to db
            string sql = "";
            foreach (var rowInfo in result)
            {
                sql = sql + "insert into tankId (tankId, tankName, playerTankId, cmTankId, cmCountryId) values ("
                          + rowInfo.tankId.ToString() + ", '"
                          + rowInfo.tankName.ToString() + "', "
                          + rowInfo.playerTankId.ToString() + ", "
                          + rowInfo.cmTankId.ToString() + ", "
                          + rowInfo.cmCountryId.ToString() + "); ";
            }

            SqlCommand insert = new SqlCommand(sql, sqlConn);
            insert.ExecuteNonQuery();

            // Fetch WS recentBattles into datatable
            sqLiteCmd.CommandText = "SELECT rbId, rbTankId, rbCountryId, rbBattles, rbKills, rbDamageDealt, rbDamageReceived, rbSpotted, rbCapturePoints, rbDefencePoints, "
                                  + "rbSurvived, rbVictory, rbBattleTime, rbShot, rbHits, rbBattleMode FROM recentBattles";  //rbFragList
            reader = sqLiteCmd.ExecuteReader();
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

            // Close db connections
            sqlCmd.Dispose();
            sqlConn.Close();
            sqliteConn.Close();
        }
    }
}