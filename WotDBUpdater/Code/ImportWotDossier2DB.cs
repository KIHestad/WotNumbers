using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater.Code
{
    public static class ImportWotDossier2DB
    {
        public static void importWotDossierHistory()
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
            string sql = "create table wsDossier ("
                       + "cmId int, cmTankId int, cmCountryId int, cmTankTitle varchar(100), cmFileId int, fiDate datetime, bpBattleCount int, bpDefencePoints int, "
                       + "bpFrags int, bpWinAndSurvive int, bpSpotted int, bpDamageDealt int, bpShots int, bpWins int, bpDamageReceived int, bpLosses int, bpXP int, "
                       + "bpSurvivedBattles int, bpCapturePoints int, bpDamageAssistedRadio int, bpDamageAssistedTracks int); ";
            db.ExecuteNonQuery(sql);

            // Join tank/battle into datatable
            sqLiteCmd.CommandText = "SELECT t.cmId, t.cmTankId, t.cmCountryId, t.cmTankTitle, t.cmFileId, f.fiDate, t.cmCreationTime, b.bpBattleCount, b.bpDefencePoints, "
                                  + "b.bpFrags, b.bpWinAndSurvive, b.bpSpotted, b.bpDamageDealt, b.bpShots, b.bpWins, b.bpDamageReceived, b.bpLosses, b.bpXP, "
                                  + "b.bpSurvivedBattles, b.bpCapturePoints, b.bpDamageAssistedRadio, b.bpDamageAssistedTracks "
                                  + "FROM Files f "
                                  + "left join File_TankDetails t on f.fiId = t.cmFileId "
                                  + "left join File_Battles b on t.cmId = b.bpParentId";
            SQLiteDataReader r = sqLiteCmd.ExecuteReader();
            DataTable dossierHistory = new DataTable();
            dossierHistory.Load(r);

            int row = 0;
            while (row < dossierHistory.Rows.Count)
            {
                int cmId = Convert.ToInt32(dossierHistory.Rows[row]["cmId"]);
                int cmTankId = Convert.ToInt32(dossierHistory.Rows[row]["cmTankId"]);
                int cmCountryId = Convert.ToInt32(dossierHistory.Rows[row]["cmCountryId"]);
                string cmTankTitle = dossierHistory.Rows[row]["cmTankTitle"].ToString();
                int cmFileId = Convert.ToInt32(dossierHistory.Rows[row]["cmFileId"]);
                DateTime fiDate = Convert.ToDateTime("1970-01-01 01:00:00").AddSeconds(Convert.ToInt32(dossierHistory.Rows[row]["fiDate"]));
                int bpBattleCount = 0;
                if (dossierHistory.Rows[row]["bpBattleCount"] != DBNull.Value)
                {
                    bpBattleCount = Convert.ToInt32(dossierHistory.Rows[row]["bpBattleCount"]);
                }
                int bpDefencePoints = 0;
                if (dossierHistory.Rows[row]["bpDefencePoints"] != DBNull.Value)
                {
                    bpDefencePoints = Convert.ToInt32(dossierHistory.Rows[row]["bpDefencePoints"]);
                }
                int bpFrags = 0;
                if (dossierHistory.Rows[row]["bpFrags"] != DBNull.Value)
                {
                    bpFrags = Convert.ToInt32(dossierHistory.Rows[row]["bpFrags"]);
                }
                int bpWinAndSurvive = 0;
                if (dossierHistory.Rows[row]["bpWinAndSurvive"] != DBNull.Value)
                {
                    bpWinAndSurvive = Convert.ToInt32(dossierHistory.Rows[row]["bpWinAndSurvive"]);
                }
                int bpSpotted = 0;
                if (dossierHistory.Rows[row]["bpSpotted"] != DBNull.Value)
                {
                    bpSpotted = Convert.ToInt32(dossierHistory.Rows[row]["bpSpotted"]);
                }
                int bpDamageDealt = 0;
                if (dossierHistory.Rows[row]["bpDamageDealt"] != DBNull.Value)
                {
                    bpDamageDealt = Convert.ToInt32(dossierHistory.Rows[row]["bpDamageDealt"]);
                }
                int bpShots = 0;
                if (dossierHistory.Rows[row]["bpShots"] != DBNull.Value)
                {
                    bpShots = Convert.ToInt32(dossierHistory.Rows[row]["bpShots"]);
                }
                int bpWins = 0;
                if (dossierHistory.Rows[row]["bpWins"] != DBNull.Value)
                {
                    bpWins = Convert.ToInt32(dossierHistory.Rows[row]["bpWins"]);
                }
                int bpDamageReceived = 0;
                if (dossierHistory.Rows[row]["bpDamageReceived"] != DBNull.Value)
                {
                    bpDamageReceived = Convert.ToInt32(dossierHistory.Rows[row]["bpDamageReceived"]);
                }
                int bpLosses = 0;
                if (dossierHistory.Rows[row]["bpLosses"] != DBNull.Value)
                {
                    bpLosses = Convert.ToInt32(dossierHistory.Rows[row]["bpLosses"]);
                }
                int bpXP = 0;
                if (dossierHistory.Rows[row]["bpXP"] != DBNull.Value)
                {
                    bpXP = Convert.ToInt32(dossierHistory.Rows[row]["bpXP"]);
                }
                int bpSurvivedBattles = 0;
                if (dossierHistory.Rows[row]["bpSurvivedBattles"] != DBNull.Value)
                {
                    bpSurvivedBattles = Convert.ToInt32(dossierHistory.Rows[row]["bpSurvivedBattles"]);
                }
                int bpCapturePoints = 0;
                if (dossierHistory.Rows[row]["bpCapturePoints"] != DBNull.Value)
                {
                    bpCapturePoints = Convert.ToInt32(dossierHistory.Rows[row]["bpCapturePoints"]);
                }
                int bpDamageAssistedRadio = 0;
                if (dossierHistory.Rows[row]["bpDamageAssistedRadio"] != DBNull.Value)
                {
                    bpDamageAssistedRadio = Convert.ToInt32(dossierHistory.Rows[row]["bpDamageAssistedRadio"]);
                }
                int bpDamageAssistedTracks = 0;
                if (dossierHistory.Rows[row]["bpDamageAssistedTracks"] != DBNull.Value)
                {
                    bpDamageAssistedTracks = Convert.ToInt32(dossierHistory.Rows[row]["bpDamageAssistedTracks"]);
                }

                // Write row to db
                sql = "insert into wsDossier ("
                    + "cmId, cmTankId, cmCountryId, cmTankTitle, cmFileId, fiDate, bpBattleCount, bpDefencePoints, bpFrags, bpWinAndSurvive, bpSpotted, "
                    + "bpDamageDealt, bpShots, bpWins, bpDamageReceived, bpLosses, bpXP, bpSurvivedBattles, bpCapturePoints, bpDamageAssistedRadio, bpDamageAssistedTracks) "
                    + "values ("
                    + "@cmId, @cmTankId, @cmCountryId, @cmTankTitle, @cmFileId, @fiDate, @bpBattleCount, @bpDefencePoints, @bpFrags, @bpWinAndSurvive, @bpSpotted, @bpDamageDealt, "
                    + "@bpShots, @bpWins, @bpDamageReceived, @bpLosses, @bpXP, @bpSurvivedBattles, @bpCapturePoints, @bpDamageAssistedRadio, @bpDamageAssistedTracks)";

                db.AddWithValue(ref sql, "@cmId", cmId, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@cmTankId", cmTankId, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@cmCountryId", cmCountryId, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@cmTankTitle", cmTankTitle, db.SqlDataType.VarChar);
                db.AddWithValue(ref sql, "@cmFileId", cmFileId, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@fiDate", fiDate.ToString("yyyy-MM-dd HH:mm"), db.SqlDataType.DateTime);
                db.AddWithValue(ref sql, "@bpBattleCount", bpBattleCount, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpDefencePoints", bpDefencePoints, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpFrags", bpFrags, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpWinAndSurvive", bpWinAndSurvive, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpSpotted", bpSpotted, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpDamageDealt", bpDamageDealt, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpShots", bpShots, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpWins", bpWins, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpDamageReceived", bpDamageReceived, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpLosses", bpLosses, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpXP", bpXP, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpSurvivedBattles", bpSurvivedBattles, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpCapturePoints", bpCapturePoints, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpDamageAssistedRadio", bpDamageAssistedRadio, db.SqlDataType.Int);
                db.AddWithValue(ref sql, "@bpDamageAssistedTracks", bpDamageAssistedTracks, db.SqlDataType.Int);
                db.ExecuteNonQuery(sql);
                
                row++;
            }

            Code.MsgBox.Show("Imported " + row.ToString() + " records.", "Import finished");
        }
    }
}
