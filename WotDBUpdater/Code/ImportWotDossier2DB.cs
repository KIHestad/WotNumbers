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

        #region importWotDossierHistory

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
            DB.ExecuteNonQuery(sql);

            // Join tank/battle into datatable
            sqLiteCmd.CommandText = "SELECT t.cmId, t.cmTankId, t.cmCountryId, t.cmTankTitle, t.cmFileId, f.fiDate, b.bpBattleCount, b.bpDefencePoints, "
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

                DB.AddWithValue(ref sql, "@cmId", cmId, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@cmTankId", cmTankId, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@cmCountryId", cmCountryId, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@cmTankTitle", cmTankTitle, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sql, "@cmFileId", cmFileId, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@fiDate", fiDate.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
                DB.AddWithValue(ref sql, "@bpBattleCount", bpBattleCount, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpDefencePoints", bpDefencePoints, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpFrags", bpFrags, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpWinAndSurvive", bpWinAndSurvive, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpSpotted", bpSpotted, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpDamageDealt", bpDamageDealt, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpShots", bpShots, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpWins", bpWins, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpDamageReceived", bpDamageReceived, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpLosses", bpLosses, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpXP", bpXP, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpSurvivedBattles", bpSurvivedBattles, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpCapturePoints", bpCapturePoints, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpDamageAssistedRadio", bpDamageAssistedRadio, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@bpDamageAssistedTracks", bpDamageAssistedTracks, DB.SqlDataType.Int);
                DB.ExecuteNonQuery(sql);
                
                row++;
            }

            Code.MsgBox.Show("Imported " + row.ToString() + " records.", "Import finished");

            sqliteConn.Close();
            sqlConn.Close();
        }

        #endregion


        public static void importWotDossierHistory2Battle()
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

            // Fetch distinct tanks into datatable
            sqLiteCmd.CommandText = "select distinct t.cmCountryID, t.cmTankID "
                                  + "from File_TankDetails t "
                                  + "left join File_Battles b on t.cmID = b.bpParentID "
                                  + "order by t.cmCountryID, t.cmTankID";
            SQLiteDataReader r = sqLiteCmd.ExecuteReader();
            DataTable wsTankList = new DataTable();
            wsTankList.Load(r);

            // Build supporting datatable for tankId mapping
            DataTable wsTankId = DB.FetchData("select t.wsCountryId, t.wsTankId, t.tankId, t.tankName, pt.id as playerTankId "
                                            + "from wsTankId t "
                                            + "left join playerTank pt on t.tankId = pt.tankId "
                                            + "order by t.wsCountryId, t.wsTankId");

            // Build supporting datatable for battle modes
            DataTable battleMode = new DataTable();
            battleMode.Columns.Add("mode", typeof(int));
            battleMode.Rows.Add(15);
            battleMode.Rows.Add(7);

            // Delete previously imported battles  (temp. solution)
            DB.ExecuteNonQuery("delete from battle where wsId = 999999999");

            // Loop through each tank
            int currentTank = 0;
            int imported = 0;
            int loopedRecords = 0;
            while (currentTank < wsTankList.Rows.Count)
            {
                // Current tank
                int cmCountryId = Convert.ToInt32(wsTankList.Rows[currentTank]["cmCountryId"]);
                int cmTankId = Convert.ToInt32(wsTankList.Rows[currentTank]["cmTankId"]);

                // Loop through each battle mode (15/7)
                int battleModeRow = 0;
                while (battleModeRow < battleMode.Rows.Count)
                {

                    // Current mode (possible values: NULL, 0, 7, 15)
                    int currentMode = 15;
                    if (battleMode.Rows[battleModeRow]["mode"] != DBNull.Value)
                    {
                        currentMode = Convert.ToInt32(battleMode.Rows[battleModeRow]["mode"]);
                        if (currentMode == 7) currentMode = 7;
                    }
                    
                    
                    try
                    {
                        // Lookup playerTankId for current tank
                        string expression = "wsCountryId = '" + cmCountryId + "' AND wsTankId = '" + cmTankId + "'";
                        DataRow[] foundRows = wsTankId.Select(expression);
                        int playerTankId = Convert.ToInt32(foundRows[0]["playerTankId"]);
                
                        // Fetch data for current tank into datatable

                        if (currentMode == 15)
                        {
                            sqLiteCmd.CommandText = "SELECT "
                                              + "  t.cmId, "
                                              + "  t.cmTankId, "
                                              + "  t.cmCountryId, "
                                              + "  t.cmTankTitle, "
                                              + "  t.cmFileId, "
                                              + "  f.fiDate, "
                                              + "  b.bpBattleCount, "
                                              + "  t.cmLastBattleTime, "
                                              + "  b.bpWins, "
                                              + "  b.bpLosses, "
                                              + "  b.bpSurvivedBattles, "
                                              + "  b.bpDamageDealt, "
                                              + "  b.bpFrags, "
                                              + "  b.bpDamageReceived, "
                                              + "  b.bpCapturePoints, "
                                              + "  b.bpDefencePoints, "
                                              + "  b.bpShots, "
                                              + "  b.bpHits, "
                                              + "  b.bpShotsReceived, "
                                              + "  b.bpPierced, "
                                              + "  b.bpPiercedReceived, "
                                              + "  b.bpDamageAssistedRadio, "
                                              + "  b.bpDamageAssistedTracks, "
                                              + "  b.bpSpotted, "
                                              + "  b.bpXP, "
                                              + "  b.bpBattleMode "
                                              + "FROM Files f "
                                              + "  LEFT JOIN File_TankDetails t ON f.fiId = t.cmFileId "
                                              + "  LEFT JOIN File_Battles b ON t.cmId = b.bpParentId "
                                              + "WHERE cmCountryId = " + cmCountryId + " AND cmTankId = " + cmTankId
                                              + "  AND (b.bpBattleMode != 7 OR b.bpBattleMode is null)"
                                              + "ORDER BY t.cmFileId";
                        }

                        if (currentMode == 7)
                        {
                            sqLiteCmd.CommandText = "SELECT "
                                              + "  t.cmId, "
                                              + "  t.cmTankId, "
                                              + "  t.cmCountryId, "
                                              + "  t.cmTankTitle, "
                                              + "  t.cmFileId, "
                                              + "  f.fiDate, "
                                              + "  b.bpBattleCount, "
                                              + "  t.cmLastBattleTime, "
                                              + "  b.bpWins, "
                                              + "  b.bpLosses, "
                                              + "  b.bpSurvivedBattles, "
                                              + "  b.bpDamageDealt, "
                                              + "  b.bpFrags, "
                                              + "  b.bpDamageReceived, "
                                              + "  b.bpCapturePoints, "
                                              + "  b.bpDefencePoints, "
                                              + "  b.bpShots, "
                                              + "  b.bpHits, "
                                              + "  b.bpShotsReceived, "
                                              + "  b.bpPierced, "
                                              + "  b.bpPiercedReceived, "
                                              + "  b.bpDamageAssistedRadio, "
                                              + "  b.bpDamageAssistedTracks, "
                                              + "  b.bpSpotted, "
                                              + "  b.bpXP, "
                                              + "  b.bpBattleMode "
                                              + "FROM Files f "
                                              + "  LEFT JOIN File_TankDetails t ON f.fiId = t.cmFileId "
                                              + "  LEFT JOIN File_Battles b ON t.cmId = b.bpParentId "
                                              + "WHERE cmCountryId = " + cmCountryId + " AND cmTankId = " + cmTankId
                                              + "  AND b.bpBattleMode = 7 "
                                              + "ORDER BY t.cmFileId";
                        }
                        
                        r = sqLiteCmd.ExecuteReader();
                        DataTable dossierHistory = new DataTable();
                        dossierHistory.Load(r);

                        sqLiteCmd.CommandText = "SELECT "
                                              + "  t.cmId, "
                                              + "  t.cmTankId, "
                                              + "  t.cmCountryId, "
                                              + "  t.cmTankTitle, "
                                              + "  t.cmFileId, "
                                              + "  f.fiDate, "
                                              + "  b.bpBattleCount, "
                                              + "  t.cmLastBattleTime, "
                                              + "  b.bpWins, "
                                              + "  b.bpLosses, "
                                              + "  b.bpSurvivedBattles, "
                                              + "  b.bpDamageDealt, "
                                              + "  b.bpFrags, "
                                              + "  b.bpDamageReceived, "
                                              + "  b.bpCapturePoints, "
                                              + "  b.bpDefencePoints, "
                                              + "  b.bpShots, "
                                              + "  b.bpHits, "
                                              + "  b.bpShotsReceived, "
                                              + "  b.bpPierced, "
                                              + "  b.bpPiercedReceived, "
                                              + "  b.bpDamageAssistedRadio, "
                                              + "  b.bpDamageAssistedTracks, "
                                              + "  b.bpSpotted, "
                                              + "  b.bpXP "
                                              + "FROM Files f "
                                              + "  LEFT JOIN File_TankDetails t ON f.fiId = t.cmFileId "
                                              + "  LEFT JOIN File_Battles b ON t.cmId = b.bpParentId "
                                              + "WHERE cmCountryId = " + cmCountryId + " AND cmTankId = " + cmTankId
                                              + " ORDER BY t.cmFileId";
                        r = sqLiteCmd.ExecuteReader();
                        DataTable dossierHistory_test = new DataTable();
                        dossierHistory_test.Load(r);

                        // Init battle parameters
                        int cmId = 0;
                        int bpBattleCount = 0;
                        DateTime cmLastBattleTime = Convert.ToDateTime("1970-01-01 01:00:00");
                        int bpWins = 0;
                        int bpLosses = 0;
                        int bpSurvivedBattles = 0;
                        int bpDamageDealt = 0;
                        int bpFrags = 0;
                        int bpDamageReceived = 0;
                        int bpCapturePoints = 0;
                        int bpDefencePoints = 0;
                        int bpShots = 0;
                        int bpHits = 0;
                        int bpShotsReceived = 0;
                        int bpPierced = 0;
                        int bpPiercedReceived = 0;
                        int bpDamageAssistedRadio = 0;
                        int bpDamageAssistedTracks = 0;
                        int bpSpotted = 0;
                        int bpXP = 0;
                        int bpBattleMode = 0;

                        // Init parameters for comparison
                        int prev_cmId = 0;
                        int prev_bpBattleCount = 0;
                        DateTime prev_cmLastBattleTime = Convert.ToDateTime("1970-01-01 01:00:00");
                        int prev_bpWins = 0;
                        int prev_bpLosses = 0;
                        int prev_bpSurvivedBattles = 0;
                        int prev_bpDamageDealt = 0;
                        int prev_bpFrags = 0;
                        int prev_bpDamageReceived = 0;
                        int prev_bpCapturePoints = 0;
                        int prev_bpDefencePoints = 0;
                        int prev_bpShots = 0;
                        int prev_bpHits = 0;
                        int prev_bpShotsReceived = 0;
                        int prev_bpPierced = 0;
                        int prev_bpPiercedReceived = 0;
                        int prev_bpDamageAssistedRadio = 0;
                        int prev_bpDamageAssistedTracks = 0;
                        int prev_bpSpotted = 0;
                        int prev_bpXP = 0;

                        // Init parameters for delta values
                        int delta_bpBattleCount = 0;
                        int delta_bpWins = 0;
                        int delta_bpLosses = 0;
                        int delta_bpSurvivedBattles = 0;
                        int delta_bpDamageDealt = 0;
                        int delta_bpFrags = 0;
                        int delta_bpDamageReceived = 0;
                        int delta_bpCapturePoints = 0;
                        int delta_bpDefencePoints = 0;
                        int delta_bpShots = 0;
                        int delta_bpHits = 0;
                        int delta_bpShotsReceived = 0;
                        int delta_bpPierced = 0;
                        int delta_bpPiercedReceived = 0;
                        int delta_bpDamageAssistedRadio = 0;
                        int delta_bpDamageAssistedTracks = 0;
                        int delta_bpSpotted = 0;
                        int delta_bpXP = 0;

                        // Init aggregated data
                        int delta_agg_draw = 0;
                        int delta_agg_battleResultId = 0;
                        int delta_agg_killed = 0;
                        int delta_agg_battleSurviveId = 0;


                        // Loop through dossier history for current tank
                        int row = 0;
                        while (row < dossierHistory.Rows.Count)
                        {
                            // Skip records with no battledata
                            if (dossierHistory.Rows[row]["bpBattleCount"] != DBNull.Value)
                            {
                                bpBattleCount = Convert.ToInt32(dossierHistory.Rows[row]["bpBattleCount"]);
                        
                                // Fetch new values if battlecount is changed (else the record will be identical with previous)
                                if (bpBattleCount > prev_bpBattleCount || row == 0)
                                {

                                    cmId = Convert.ToInt32(dossierHistory.Rows[row]["cmId"]);
                                    cmLastBattleTime = Convert.ToDateTime("1970-01-01 01:00:00").AddSeconds(Convert.ToInt32(dossierHistory.Rows[row]["cmLastBattleTime"]));
                                    bpWins = Convert.ToInt32(dossierHistory.Rows[row]["bpWins"]);
                                    bpLosses = Convert.ToInt32(dossierHistory.Rows[row]["bpLosses"]);
                                    bpSurvivedBattles = Convert.ToInt32(dossierHistory.Rows[row]["bpSurvivedBattles"]);
                                    bpDamageDealt = Convert.ToInt32(dossierHistory.Rows[row]["bpDamageDealt"]);
                                    bpFrags = Convert.ToInt32(dossierHistory.Rows[row]["bpFrags"]);
                                    bpDamageReceived = Convert.ToInt32(dossierHistory.Rows[row]["bpDamageReceived"]);
                                    if (dossierHistory.Rows[row]["bpPierced"] != DBNull.Value)
                                    {
                                        bpPierced = Convert.ToInt32(dossierHistory.Rows[row]["bpPierced"]);
                                    }
                                    if (dossierHistory.Rows[row]["bpPiercedReceived"] != DBNull.Value)
                                    {
                                        bpPiercedReceived = Convert.ToInt32(dossierHistory.Rows[row]["bpPiercedReceived"]);
                                    }
                                    bpCapturePoints = Convert.ToInt32(dossierHistory.Rows[row]["bpCapturePoints"]);
                                    bpDefencePoints = Convert.ToInt32(dossierHistory.Rows[row]["bpDefencePoints"]);
                                    bpShots = Convert.ToInt32(dossierHistory.Rows[row]["bpShots"]);
                                    bpHits = Convert.ToInt32(dossierHistory.Rows[row]["bpHits"]);
                                    if (dossierHistory.Rows[row]["bpShotsReceived"] != DBNull.Value)
                                    {
                                        bpShotsReceived = Convert.ToInt32(dossierHistory.Rows[row]["bpShotsReceived"]);
                                    }
                                    if (dossierHistory.Rows[row]["bpDamageAssistedRadio"] != DBNull.Value)
                                    {
                                        bpDamageAssistedRadio = Convert.ToInt32(dossierHistory.Rows[row]["bpDamageAssistedRadio"]);
                                    }
                                    if (dossierHistory.Rows[row]["bpDamageAssistedTracks"] != DBNull.Value)
                                    {
                                        bpDamageAssistedTracks = Convert.ToInt32(dossierHistory.Rows[row]["bpDamageAssistedTracks"]);
                                    }
                                    bpSpotted = Convert.ToInt32(dossierHistory.Rows[row]["bpSpotted"]);
                                    bpXP = Convert.ToInt32(dossierHistory.Rows[row]["bpXP"]);
                                    if (dossierHistory.Rows[row]["bpBattleMode"] != DBNull.Value)
                                    {
                                        bpBattleMode = Convert.ToInt32(dossierHistory.Rows[row]["bpBattleMode"]);
                                    }


                                    // Calc delta values for current and previous record
                                    delta_bpBattleCount = bpBattleCount - prev_bpBattleCount;
                                    delta_bpWins = bpWins - prev_bpWins;
                                    delta_bpLosses = bpLosses - prev_bpLosses;
                                    delta_bpSurvivedBattles = bpSurvivedBattles - prev_bpSurvivedBattles;
                                    delta_bpDamageDealt = bpDamageDealt - prev_bpDamageDealt;
                                    delta_bpFrags = bpFrags - prev_bpFrags;
                                    delta_bpDamageReceived = bpDamageReceived - prev_bpDamageReceived;
                                    delta_bpCapturePoints = bpCapturePoints - prev_bpCapturePoints;
                                    delta_bpDefencePoints = bpDefencePoints - prev_bpDefencePoints;
                                    delta_bpShots = bpShots - prev_bpShots;
                                    delta_bpHits = bpHits - prev_bpHits;
                                    delta_bpShotsReceived = bpShotsReceived - prev_bpShotsReceived;
                                    delta_bpPierced = bpPierced - prev_bpPierced;
                                    delta_bpPiercedReceived = bpPiercedReceived - prev_bpPiercedReceived;
                                    delta_bpDamageAssistedRadio = bpDamageAssistedRadio - prev_bpDamageAssistedRadio;
                                    delta_bpDamageAssistedTracks = bpDamageAssistedTracks - prev_bpDamageAssistedTracks;
                                    delta_bpSpotted = bpSpotted - prev_bpSpotted;
                                    delta_bpXP = bpXP - prev_bpXP;
                                
                                    // Aggregated delta values
                                    delta_agg_draw = (delta_bpBattleCount - delta_bpWins - delta_bpLosses);
                                    if (delta_bpWins == delta_bpBattleCount) delta_agg_battleResultId = 1;                  // only wins
                                    else if (delta_bpLosses == delta_bpBattleCount) delta_agg_battleResultId = 3;           // only defeats
                                    else if (delta_agg_draw == delta_bpBattleCount) delta_agg_battleResultId = 2;           // only draws
                                    else delta_agg_battleResultId = 4;                                                      // mixed results
                                    delta_agg_killed = (delta_bpBattleCount - delta_bpSurvivedBattles);
                                    if (delta_bpSurvivedBattles == delta_bpBattleCount) delta_agg_battleSurviveId = 1;      // survived all battles
                                    else if (delta_agg_killed == delta_bpBattleCount) delta_agg_battleSurviveId = 3;        // killed in all battles
                                    else delta_agg_battleSurviveId = 2;                                                     // mixed results


                                    // Write row to db
                                    string sql = "insert into battle ("
                                               + "playerTankId, battlesCount, battleTime, battleResultId, victory, draw, defeat, battleSurviveId, survived, killed, "
                                               + "dmg, frags, dmgReceived, cap, def, shots, hits, shotsReceived, pierced, piercedReceived, assistSpot, assistTrack, "
                                               + "spotted, xp, mode15, mode7, wsId) "
                                               + "values ("
                                               + "@playerTankId, "
                                               + "@delta_bpBattleCount, "
                                               + "@cmLastBattleTime, "
                                               + "@delta_agg_battleResultId, "
                                               + "@delta_bpWins, "
                                               + "@delta_agg_draw, "
                                               + "@delta_bpLosses, "
                                               + "@delta_agg_battleSurviveId, "
                                               + "@delta_bpSurvivedBattles, "
                                               + "@delta_agg_killed, "
                                               + "@delta_bpDamageDealt, "
                                               + "@delta_bpFrags, "
                                               + "@delta_bpDamageReceived, "
                                               + "@delta_bpCapturePoints, "
                                               + "@delta_bpDefencePoints, "
                                               + "@delta_bpShots, "
                                               + "@delta_bpHits, "
                                               + "@delta_bpShotsReceived, "
                                               + "@delta_bpPierced, "
                                               + "@delta_bpPiercedReceived, "
                                               + "@delta_bpDamageAssistedRadio, "
                                               + "@delta_bpDamageAssistedTracks, "
                                               + "@delta_bpSpotted, "
                                               + "@delta_bpXP, "
                                               + "@mode15, "
                                               + "@mode7, "
                                               + "999999999"       // mark rows as imported
                                               + ");";
                                    
                                    DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpBattleCount", delta_bpBattleCount, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@cmLastBattleTime", cmLastBattleTime.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
                                    DB.AddWithValue(ref sql, "@delta_agg_battleResultId", delta_agg_battleResultId, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpWins", delta_bpWins, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_agg_draw", delta_agg_draw, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpLosses", delta_bpLosses, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_agg_battleSurviveId", delta_agg_battleSurviveId, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpSurvivedBattles", delta_bpSurvivedBattles, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_agg_killed", delta_agg_killed, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpDamageDealt", delta_bpDamageDealt, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpFrags", delta_bpFrags, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpDamageReceived", delta_bpDamageReceived, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpCapturePoints", delta_bpCapturePoints, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpDefencePoints", delta_bpDefencePoints, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpShots", delta_bpShots, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpHits", delta_bpHits, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpShotsReceived", delta_bpShotsReceived, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpPierced", delta_bpPierced, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpPiercedReceived", delta_bpPiercedReceived, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpDamageAssistedRadio", delta_bpDamageAssistedRadio, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpDamageAssistedTracks", delta_bpDamageAssistedTracks, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpSpotted", delta_bpSpotted, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@delta_bpXP", delta_bpXP, DB.SqlDataType.Int);
                                    if (currentMode == 15) DB.AddWithValue(ref sql, "@mode15", 1, DB.SqlDataType.Int);
                                    else DB.AddWithValue(ref sql, "@mode15", 0, DB.SqlDataType.Int);
                                    if (currentMode == 7) DB.AddWithValue(ref sql, "@mode7", 1, DB.SqlDataType.Int);
                                    else DB.AddWithValue(ref sql, "@mode7", 0, DB.SqlDataType.Int);

                                    DB.ExecuteNonQuery(sql);
                                    imported++;


                                    // Prepare for next run
                                    prev_cmId = cmId;
                                    prev_bpBattleCount = bpBattleCount;
                                    prev_cmLastBattleTime = cmLastBattleTime;
                                    prev_bpWins = bpWins;
                                    prev_bpLosses = bpLosses;
                                    prev_bpSurvivedBattles = bpSurvivedBattles;
                                    prev_bpDamageDealt = bpDamageDealt;
                                    prev_bpFrags = bpFrags;
                                    prev_bpDamageReceived = bpDamageReceived;
                                    prev_bpCapturePoints = bpCapturePoints;
                                    prev_bpDefencePoints = bpDefencePoints;
                                    prev_bpShots = bpShots;
                                    prev_bpHits = bpHits;
                                    prev_bpShotsReceived = bpShotsReceived;
                                    prev_bpPierced = bpPierced;
                                    prev_bpPiercedReceived = bpPiercedReceived;
                                    prev_bpDamageAssistedRadio = bpDamageAssistedRadio;
                                    prev_bpDamageAssistedTracks = bpDamageAssistedTracks;
                                    prev_bpSpotted = bpSpotted;
                                    prev_bpXP = bpXP;

                                }
                            }

                            row++;
                            loopedRecords++;
                    

                        } // End of looping battles on current tank
                    }

                    catch (Exception ex)
                    {
                        //Code.MsgBox.Show("Tank not found! cmCountryId: " + cmCountryId + ", cmTankId: " + cmTankId);
                        //Code.MsgBox.Show(ex.Message);
                        //return;
                    }

                    
                    battleModeRow++;

                } // End of looping battle modes

                currentTank++;

            } // End of looping tanks


            Code.MsgBox.Show("Imported " + imported.ToString() + " battles (total lines read: " + loopedRecords + ")" , "Import finished");

            sqliteConn.Close();
            sqlConn.Close();

        }
    }
}
