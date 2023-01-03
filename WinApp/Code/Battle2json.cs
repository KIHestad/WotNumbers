using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronPython.Hosting;
using IronPython.Runtime;
using IronPython.Runtime.Exceptions;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json.Linq;

namespace WinApp.Code
{
    class Battle2json
    {
        private static List<string> battleResultDatFileCopied = new List<string>(); // List of dat-files copyied from wargaming battle folder, to avoid copy several times
        private static List<string> battleResultJsonFileExists = new List<string>(); // List of json-files already existing in battle folder, to avoid converting several times

        private class ClanInfo
        {
            public int ClanId { get; set; }
            public string ClanAbbrev { get; set; }
            public int Count { get; set; }
        }

        private class BattlePlayer
        {
            public uint accountId;
            public string clanAbbrev;
            public int clanId;
            public string name;
            public int platoonId;
            public int team;
            public int vehicleId;
            public int playerTeam = 0; // default value = false -> 0=false, 1=true
            public int killerVehicleId;
        }

        private class BattleValue
        {
            public string colname;
            public object value;
        }

        public static void GetExistingBattleFiles()
        {
            // Get existing json files
            string[] filesJson = Directory.GetFiles(Config.AppDataBattleResultFolder, "*.json");
            foreach (string file in filesJson)
            {
                battleResultJsonFileExists.Add(Path.GetFileNameWithoutExtension(file).ToString()); // Remove file extension
            }
            // Get existing dst files
            string[] filesDat = Directory.GetFiles(Config.AppDataBattleResultFolder, "*.dat");
            foreach (string file in filesDat)
            {
                battleResultDatFileCopied.Add(file); // Complete file with path
            }
        }

        public async static Task<bool> ConvertBattleFilesToJson()
        {
            bool ok = true;
            try
            {
                // Get WoT top level battle_result folder for getting dat-files
                if (Directory.Exists(Path.GetDirectoryName(Config.Settings.battleFilePath)))
                {
                    DirectoryInfo di = new DirectoryInfo(Config.Settings.battleFilePath);
                    DirectoryInfo[] folders = di.GetDirectories();
                    // testing one file
                    foreach (DirectoryInfo folder in folders)
                    {
                        string[] filesDat = Directory.GetFiles(folder.FullName, "*.dat");
                        int count = 0;
                        // Force read all battle files if first battle fetch and saving enabled
                        bool getAll = (Config.SessionSettings.tempBattleResultSave && Config.SessionSettings.tempBattleResultSaveFirstRun);
                        foreach (string file in filesDat)
                        {
                            try
                            {
                                string filenameWihoutExt = Path.GetFileNameWithoutExtension(file).ToString();
                                // Check if not copied previous (during this session), and that converted json file do not already exists (from previous sessions)
                                if ((!battleResultDatFileCopied.Exists(x => x == file) && !battleResultJsonFileExists.Exists(x => x == filenameWihoutExt)) || getAll)
                                {
                                    // Copy
                                    Log.AddToLogBuffer(" > > Start copying battle DAT-file: " + file);
                                    FileInfo fileBattleOriginal = new FileInfo(file); // the original dossier file
                                    string filename = Path.GetFileName(file);
                                    if (!WaitUntilFileReadyToRead(file, 4000)) // Since we cannot read the file, skip it rather then crash further down.
                                    {
                                        Log.AddToLogBuffer(" > > > Could not read battle DAT-file: " + file);
                                        continue;
                                    }
                                    fileBattleOriginal.CopyTo(Config.AppDataBattleResultFolder + filename, true); // copy original dossier fil and rename it for analyze
                                    // if successful copy remember it
                                    if (File.Exists(Config.AppDataBattleResultFolder + filename))
                                    {
                                        battleResultDatFileCopied.Add(file);
                                        Log.AddToLogBuffer(" > > > Copied successfully battle DAT-file: " + file);
                                    }
                                }
                                else
                                {
                                    count++;
                                }
                            }
                            catch (Exception ex)
                            {
                                await Log.LogToFile(ex, " > Error copying battle DAT-file " + file + " to json");
                                ok = false;
                            }
                        }
                        if (count > 0)
                            Log.AddToLogBuffer(" > DAT-files skipped, read previous: " + count.ToString());
                        if (filesDat.Length == 0)
                            Log.AddToLogBuffer(" > No battle DAT-files found");
                    }
                    await Log.WriteLogBuffer();
                    // Loop through all dat-files copied to local folder
                    string[] filesDatCopied = Directory.GetFiles(Config.AppDataBattleResultFolder, "*.dat");
                    int totFilesDat = filesDatCopied.Count();
                    if (totFilesDat > 0)
                    {
                        Log.AddToLogBuffer(" > > Start converting " + totFilesDat.ToString() + " battle DAT-files to json");
                        foreach (string file in filesDatCopied)
                        {
                            try
                            {
                                // Convert file to json
                                var result = await ConvertBattleUsingPython(file);
                                if (result.DeleteFile)
                                {
                                    // Success, json file is now created, clean up by delete dat file
                                    FileInfo fileBattleDatCopied = new FileInfo(file); // the original file
                                    fileBattleDatCopied.Delete(); // delete original DAT file
                                    if (result.Success)
                                        Log.AddToLogBuffer(" > > > Deleted successfully converted battle DAT-file: " + file);
                                    else
                                        Log.AddToLogBuffer(" > > > Deleted faulty battle DAT-file: " + file);
                                }
                            }
                            catch (Exception ex)
                            {
                                await Log.LogToFile(ex, " > Error converting battle file " + file + " to json");
                                ok = false;
                            }
                        }
                    }
                    else
                        Log.AddToLogBuffer(" > No battle files found");
                }
            }
            catch (Exception ex)
            {
                await Log.LogToFile(ex, " > Error converting battle files to json");
                ok = false;
            }
            return ok;
        }

        private static bool WaitUntilFileReadyToRead(string filePath, int maxWaitTime)
        {
            // Checks file is readable
            bool fileOK = false;
            int waitInterval = 100; // time to wait in ms per read operation to check filesize
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (stopWatch.ElapsedMilliseconds < maxWaitTime && !fileOK)
            {
                try
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        fileOK = true;
                        TimeSpan ts = stopWatch.Elapsed;
                        Log.AddToLogBuffer(String.Format(" > > > Battlefile read successful (waited: {0:0000}ms)", stopWatch.ElapsedMilliseconds.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    // could not read file - do not log as error, this is normal behavior
                    Log.AddToLogBuffer(String.Format(" > > > Battlefile not ready yet (waited: {0:0000}ms) - " + ex.Message, stopWatch.ElapsedMilliseconds.ToString()));
                    System.Threading.Thread.Sleep(waitInterval);
                }
            }
            stopWatch.Stop();

            return fileOK;
        }

        private class Platoon
        {
            public int platoonID;
            public int team;
            public int platoonNum;
        }

        private static string lastFile = "";

        public async static Task<int> RunBattleResultRead()
        {
            int BattlesUpdated = 0; // return value
            bool deleteLastFileOnError = false;
            string customErrMsg = "Module: Starting";
            try
            {
                Log.AddToLogBuffer(" > Start looking for battle result");
                // Look for new files
                bool convertOK = await ConvertBattleFilesToJson();
                // Get all json files
                Log.AddToLogBuffer(" > Start looking for converted json battle files");
                string[] filesJson = Directory.GetFiles(Config.AppDataBattleResultFolder, "*.json");
                // count action
                int processed = 0;
                int added = 0;
                bool deleteFileAfterRead = true;
                List<int> battleListSuccess = new List<int>();
                foreach (string file in filesJson)
                {
                    // Save file regardless of content if set i settings options
                    if (Config.SessionSettings.tempBattleResultSave)
                    {
                        FileInfo fileBattleJson = new FileInfo(file);
                        fileBattleJson.CopyTo(Config.AppDataBattleResultSaved + fileBattleJson.Name, true); // copy jason battle file to save folder
                        Config.SessionSettings.tempBattleResultSaveFirstRun = false;
                    }
                    // Start analyzing file content
                    lastFile = file;
                    processed++;
                    // Read content
                    StreamReader sr = new StreamReader(file, Encoding.UTF8);
                    string json = sr.ReadToEnd();
                    sr.Close();
                    // Root token
                    JToken token_root = JObject.Parse(json);
                    // Parser = wotbr2j-script added info
                    JToken token_parser = token_root["parser"];
                    // Common = main battle result info
                    JToken token_common = token_root["common"];
                    string result = (string)token_parser.SelectToken("result"); // Find if ok
                    string btlResultVer = (string)token_parser.SelectToken("version"); // find version

                    // Check if ok
                    if (result == "ok")
                    {
                        Int64 arenaUniqueID = (Int64)token_root.SelectToken("arenaUniqueID"); // Find unique id
                        bool battleExists = await CheckIfBattleExists(arenaUniqueID);

                        if (!battleExists) // we could also update values instead of skipping
                        {
                            int tankId = (int)token_root.SelectToken("tankId"); // tankId
                            double arenaCreateTime = (double)token_common.SelectToken("arenaCreateTime"); // Arena create time
                            double duration = (double)token_common.SelectToken("duration"); // Arena duration
                            double battleFinishUnix = arenaCreateTime + Constants.ArenaCreationAndBattleStartTimeGap + duration; // Battle finish time
                            DateTime battleTime = DateTimeHelper.AdjustForTimeZone(DateTimeHelper.ConvertFromUnixTimestamp(battleFinishUnix));
                            DateTime battleTimeStart = DateTimeHelper.AdjustForTimeZone(DateTimeHelper.ConvertFromUnixTimestamp(arenaCreateTime));

                            bool isOrphanBattle = await IsOrphanBattle(battleTime, tankId);
                            if (isOrphanBattle)
                            {
                                await CreateOrphanBattleFromJSON(json);
                            }

                            // Private token
                            JToken token_private = token_root["private"];

                            // Now find battle created from dossier, or create now if special tank = special Event
                            // This also works for orphan battles.
                            DataTable dt;
                            string sql =
                                "select b.id as battleId, pt.id as playerTankId, pt.gGrindXP, b.arenaUniqueID, b.battleMode  " +
                                "from battle b left join playerTank pt on b.playerTankId = pt.id " +
                                "where pt.tankId=@tankId and b.battleTime>@battleTimeFrom and b.battleTime<@battleTimeTo and b.battlesCount=1;";
                            DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
                            DB.AddWithValue(ref sql, "@battleTimeFrom", battleTime.AddSeconds(-30), DB.SqlDataType.DateTime);
                            DB.AddWithValue(ref sql, "@battleTimeTo", battleTime.AddSeconds(30), DB.SqlDataType.DateTime);
                            dt = await DB.FetchData(sql);

                            // If battle found from DB add enhanced values from battle file now
                            if (dt.Rows.Count > 0)
                            {
                                customErrMsg = "Module: Player data";
                                // Check if already read, if not continue adding enhanced values
                                if (dt.Rows[0]["arenaUniqueID"] == DBNull.Value)
                                {
                                    // Battle without enchanced values found
                                    int battleId = Convert.ToInt32(dt.Rows[0]["battleId"]);
                                    int playerTankId = Convert.ToInt32(dt.Rows[0]["playerTankId"]);
                                    //string battleMode = dt.Rows[0]["battleMode"].ToString();
                                    int grindXP = Convert.ToInt32(dt.Rows[0]["gGrindXP"]);
                                    uint playerAccountId = (uint)token_private["account"].SelectToken("accountDBID");

                                    // Get values
                                    List<BattleValue> battleValues = new List<BattleValue>
                                    {
                                        // common initial valuesIx
                                        // new BattleValue() { colname = "accountId", value = playerAccountId },
                                        new BattleValue() { colname = "arenaTypeID", value = (int)token_common.SelectToken("arenaTypeID") }
                                    };

                                    int playerTeam = (int)token_private["account"].SelectToken("team");
                                    int enemyTeam = playerTeam == 1 ? 2 : 1;

                                    // Find game type
                                    BonusType bonusType = (int)token_common.SelectToken("bonusType");
                                    battleValues.Add(new BattleValue() { colname = "bonusType", value = Convert.ToInt32(bonusType) });

                                    // Get platoon
                                    bool getPlatoon = bonusType.ShouldGetPlatoon();
                                    
                                    // Get Industrial Resource
                                    bool getFortResource = bonusType.ShouldGetFortResource();

                                    // Get battle mode as text from bonus type, also set flag for get clan for spesific battle types
                                    bool getEnemyClan = bonusType.ShouldGetEnemyClan();
                                    string battleResultMode = bonusType.GetBattleResultMode();

                                    battleValues.Add(new BattleValue() { colname = "bonusTypeName", value = "'" + (string)token_common.SelectToken("bonusTypeName") + "'" });
                                    battleValues.Add(new BattleValue() { colname = "finishReasonName", value = "'" + (string)token_common.SelectToken("finishReasonName") + "'" });
                                
                                    // personal - credits
                                    battleValues.Add(new BattleValue() { colname = "originalCredits", value = (int)token_private["vehicle"].SelectToken("originalCredits") });
                                    battleValues.Add(new BattleValue() { colname = "credits", value = (int)token_private["vehicle"].SelectToken("credits") });
                                    battleValues.Add(new BattleValue() { colname = "creditsPenalty", value = (int)token_private["vehicle"].SelectToken("creditsPenalty") });
                                    battleValues.Add(new BattleValue() { colname = "creditsToDraw", value = (int)token_private["vehicle"].SelectToken("creditsToDraw") });
                                    battleValues.Add(new BattleValue() { colname = "creditsContributionIn", value = (int)token_private["vehicle"].SelectToken("creditsContributionIn") });
                                    battleValues.Add(new BattleValue() { colname = "creditsContributionOut", value = (int)token_private["vehicle"].SelectToken("creditsContributionOut") });
                                    battleValues.Add(new BattleValue() { colname = "autoRepairCost", value = (int)token_private["vehicle"].SelectToken("autoRepairCost") });
                                    battleValues.Add(new BattleValue() { colname = "eventCredits", value = (int)token_private["vehicle"].SelectToken("eventCredits") });
                                    battleValues.Add(new BattleValue() { colname = "premiumCreditsFactor10", value = (int)token_private["vehicle"].SelectToken("premiumCreditsFactor100") / 10 });
                                    battleValues.Add(new BattleValue() { colname = "achievementCredits", value = (int)token_private["vehicle"].SelectToken("achievementCredits") });
                                
                                    // personal XP
                                    battleValues.Add(new BattleValue() { colname = "real_xp", value = (int)token_private["vehicle"].SelectToken("xp") });
                                    battleValues.Add(new BattleValue() { colname = "xpPenalty", value = (int)token_private["vehicle"].SelectToken("xpPenalty") });
                                    battleValues.Add(new BattleValue() { colname = "freeXP", value = (int)token_private["vehicle"].SelectToken("freeXP") });
                                    battleValues.Add(new BattleValue() { colname = "dailyXPFactor10", value = (int)token_private["vehicle"].SelectToken("dailyXPFactor10") });
                                    battleValues.Add(new BattleValue() { colname = "premiumXPFactor10", value = (int)token_private["vehicle"].SelectToken("premiumXPFactor100") / 10 });
                                    battleValues.Add(new BattleValue() { colname = "eventFreeXP", value = (int)token_private["vehicle"].SelectToken("eventFreeXP") });
                                    battleValues.Add(new BattleValue() { colname = "achievementFreeXP", value = (int)token_private["vehicle"].SelectToken("achievementFreeXP") });
                                    battleValues.Add(new BattleValue() { colname = "achievementXP", value = (int)token_private["vehicle"].SelectToken("achievementXP") });
                                    battleValues.Add(new BattleValue() { colname = "eventXP", value = (int)token_private["vehicle"].SelectToken("eventXP") });
                                    battleValues.Add(new BattleValue() { colname = "eventTMenXP", value = (int)token_private["vehicle"].SelectToken("eventTMenXP") });
                                    // personal markOfMastery
                                    battleValues.Add(new BattleValue() { colname = "markOfMastery", value = (int)token_private["vehicle"].SelectToken("markOfMastery") });
                                    // Other
                                    battleValues.Add(new BattleValue() { colname = "vehTypeLockTime", value = (int)token_private["vehicle"].SelectToken("vehTypeLockTime") });
                                    battleValues.Add(new BattleValue() { colname = "marksOnGun", value = (int)token_private["vehicle"].SelectToken("marksOnGun") });
                                    // Rating values, more adds later
                                    Rating.WNHelper.RatingParameters rp = new Rating.WNHelper.RatingParameters
                                    {
                                        DEF = (int)token_private["vehicle"].SelectToken("droppedCapturePoints")
                                    };
                                    battleValues.Add(new BattleValue() { colname = "def", value = rp.DEF }); // override def - might be above 100
                                                                                                             // field returns null
                                    if (getFortResource)
                                        if (token_private["vehicle"].SelectToken("fortResource") != null && token_private["vehicle"].SelectToken("fortResource").HasValues)
                                            battleValues.Add(new BattleValue() { colname = "fortResource", value = (int)token_private["vehicle"].SelectToken("fortResource") });
                                    // dayly double
                                    int dailyXPFactor = (int)token_private["vehicle"].SelectToken("dailyXPFactor10") / 10;
                                    battleValues.Add(new BattleValue() { colname = "dailyXPFactorTxt", value = "'" + dailyXPFactor.ToString() + " X'" });

                                    // Special fields: death reason, convert to string
                                    int deathReasonId = (int)token_private["vehicle"].SelectToken("deathReason");
                                    string deathReason = "Unknown";
                                    switch (deathReasonId)
                                    {
                                        case -1: deathReason = "Alive"; break;
                                        case 0: deathReason = "Shot"; break;
                                        case 1: deathReason = "Burned"; break;
                                        case 2: deathReason = "Rammed"; break;
                                        case 3: deathReason = "Chrashed"; break;
                                        case 4: deathReason = "Death zone"; break;
                                        case 5: deathReason = "Drowned"; break;
                                    }
                                    battleValues.Add(new BattleValue() { colname = "deathReason", value = "'" + deathReason + "'" });
                                    // Get from array autoLoadCost
                                    JArray array_autoload = (JArray)token_private["vehicle"].SelectToken("autoLoadCost");
                                    int autoLoadCost = (int)array_autoload[0];
                                    battleValues.Add(new BattleValue() { colname = "autoLoadCost", value = autoLoadCost });
                                    // Get from array autoEquipCost
                                    // Get fro array autoEqipCost
                                    JArray array_autoequip = (JArray)token_private["vehicle"].SelectToken("autoEquipCost");
                                    int autoEquipCost = (int)array_autoequip[0];
                                    battleValues.Add(new BattleValue() { colname = "autoEquipCost", value = autoEquipCost });
                                    // Calculated net credits
                                    int creditsNet = (int)token_private["vehicle"].SelectToken("credits");
                                    creditsNet -= (int)token_private["vehicle"].SelectToken("creditsPenalty"); // fine for damage to allies
                                    creditsNet += (int)token_private["vehicle"].SelectToken("creditsToDraw"); // compensation for dmg caused by allies
                                    creditsNet -= (int)token_private["vehicle"].SelectToken("autoRepairCost"); // repear cost
                                    creditsNet -= autoLoadCost;
                                    creditsNet -= autoEquipCost;
                                    battleValues.Add(new BattleValue() { colname = "creditsNet", value = creditsNet });
                                    // map id
                                    int arenaTypeID = (int)token_common.SelectToken("arenaTypeID");
                                    int mapId = arenaTypeID & 32767;
                                    battleValues.Add(new BattleValue() { colname = "mapId", value = mapId });
                                    // game mode
                                    int gammeplayId = arenaTypeID >> 16;
                                    string gameplayName = "";
                                    switch (gammeplayId)
                                    {
                                        case 0: gameplayName = "Standard"; break;
                                        case 1: gameplayName = "Encounter"; break;
                                        case 2: gameplayName = "Assault"; break;
                                    }
                                    battleValues.Add(new BattleValue() { colname = "gameplayName", value = "'" + gameplayName + "'" });
                                    // Correct battle start time
                                    battleValues.Add(new BattleValue() { colname = "battleTimeStart", value = battleTimeStart });

                                    int battleLifeTime = (int)token_private["vehicle"].SelectToken("lifeTime");
                                    battleValues.Add(new BattleValue() { colname = "battleLifeTime", value = battleLifeTime });

                                    // insert data
                                    string fields = "";
                                    foreach (var battleValue in battleValues)
                                    {
                                        if (battleValue.value.GetType() == typeof(DateTime))
                                        {
                                            string temp = "@datetimevalue";
                                            DB.AddWithValue(ref temp, "@datetimevalue", battleValue.value, DB.SqlDataType.DateTime);
                                            fields += battleValue.colname + " = " + temp + ", ";
                                        }
                                        else
                                            fields += battleValue.colname + " = " + battleValue.value.ToString() + ", ";
                                    }
                                    sql = "update battle set " + fields + " arenaUniqueID=@arenaUniqueID where id=@battleId";
                                    DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@arenaUniqueID", arenaUniqueID, DB.SqlDataType.Float);
                                    await DB.ExecuteNonQuery(sql);

                                    // Add Battle Players *******************************
                                    customErrMsg = "Module: Battle Players";
                                    List<BattlePlayer> battlePlayers = new List<BattlePlayer>();
                                    JToken token_players = token_root["players"];
                                    // Get values to save to battle
                                    int playerFortResources = 0;
                                    int[] teamFortResources = new int[3];
                                    int[] survivedCount = new int[3];
                                    int[] fragsCount = new int[3];
                                    teamFortResources[1] = 0;
                                    teamFortResources[2] = 0;
                                    survivedCount[1] = 0;
                                    survivedCount[2] = 0;
                                    fragsCount[1] = 0;
                                    fragsCount[2] = 0;
                                    List<Platoon> platoon = new List<Platoon>();
                                    int playerPlatoonId = 0;
                                    int playerPlatoonParticipants = 0;
                                    List<ClanInfo> clanCount = new List<ClanInfo>();

                                    // Iterate over all battles players and get data
                                    foreach (JToken player in token_players)
                                    {
                                        BattlePlayer newPlayer = new BattlePlayer();
                                        JProperty playerProperty = (JProperty)player;
                                        newPlayer.accountId = Convert.ToUInt32(playerProperty.Name);
                                        JToken playerInfo = player.First;
                                        newPlayer.clanId = (int)playerInfo.SelectToken("clanDBID");
                                        newPlayer.clanAbbrev = (string)playerInfo.SelectToken("clanAbbrev");
                                        newPlayer.name = (string)playerInfo.SelectToken("name");
                                        newPlayer.platoonId = (int)playerInfo.SelectToken("prebattleID");
                                        newPlayer.team = (int)playerInfo.SelectToken("team");

                                        if (newPlayer.team == playerTeam)
                                            newPlayer.playerTeam = 1;

                                        // Get values for saving to battle
                                        if (getEnemyClan && newPlayer.clanId > 0 && newPlayer.team == enemyTeam) // Get enemy clan
                                        {
                                            // Found player with clan, add to clan count
                                            bool foundPlayerClan = false;
                                            foreach (ClanInfo item in clanCount)
                                            {
                                                if (item.ClanId == newPlayer.clanId)
                                                {
                                                    item.Count++;
                                                    foundPlayerClan = true;
                                                }
                                            }
                                            if (!foundPlayerClan)
                                            {
                                                clanCount.Add(new ClanInfo() {
                                                    ClanId = newPlayer.clanId,
                                                    ClanAbbrev = newPlayer.clanAbbrev,
                                                    Count = 1
                                                });
                                            }
                                        }
                                        if (getPlatoon && newPlayer.platoonId > 0) // Get platoon info
                                        {
                                            Platoon p = new Platoon
                                            {
                                                platoonID = newPlayer.platoonId,
                                                team = newPlayer.team,
                                                platoonNum = 0
                                            };
                                            platoon.Add(p);
                                        }
                                    
                                        // Get fields and values, init adding battle id
                                        JToken vehicleInfo = playerInfo["vehicle"];
                                        fields = "battleID";
                                        string values = battleId.ToString();
                                        // Get values from player section
                                        fields += ", accountId, clanAbbrev, clanDBID, name, platoonID, team, playerTeam";
                                        values += ", " + newPlayer.accountId.ToString();
                                        values += ", '" + newPlayer.clanAbbrev + "'";
                                        values += ", " + newPlayer.clanId.ToString();
                                        values += ", '" + newPlayer.name + "'";
                                        values += ", " + newPlayer.platoonId.ToString();
                                        values += ", " + newPlayer.team.ToString();
                                        values += ", " + newPlayer.playerTeam.ToString();
                                        // Get values from vehicles section
                                        fields += ", tankId, xp , damageDealt, credits, capturePoints, damageReceived, deathReason, directHits";
                                        // typeCompDescr = tankId, might be missing in clan wars if player not spoddet
                                        string checkPlayerTankId = vehicleInfo.SelectToken("typeCompDescr").ToString();
                                        if (checkPlayerTankId == "" || checkPlayerTankId == "0")
                                            values += ", -1";
                                        else
                                            values += ", " + checkPlayerTankId;
                                        values += ", " + vehicleInfo.SelectToken("xp");
                                        values += ", " + vehicleInfo.SelectToken("damageDealt");
                                        values += ", " + vehicleInfo.SelectToken("credits");
                                        values += ", " + vehicleInfo.SelectToken("capturePoints");
                                        values += ", " + vehicleInfo.SelectToken("damageReceived");
                                        string playerDeathReason = vehicleInfo.SelectToken("deathReason").ToString();
                                        values += ", " + playerDeathReason;
                                        values += ", " + vehicleInfo.SelectToken("directHits");
                                        fields += ", directHitsReceived, droppedCapturePoints, hits, kills, shots, shotsReceived, spotted, tkills, fortResource";
                                        values += ", " + vehicleInfo.SelectToken("directHitsReceived");
                                        values += ", " + vehicleInfo.SelectToken("droppedCapturePoints");
                                        values += ", " + vehicleInfo.SelectToken("directHits");
                                        values += ", " + vehicleInfo.SelectToken("kills");
                                        values += ", " + vehicleInfo.SelectToken("shots");
                                        values += ", " + vehicleInfo.SelectToken("directHitsReceived");
                                        values += ", " + vehicleInfo.SelectToken("spotted");
                                        values += ", " + vehicleInfo.SelectToken("tkills");
                                        // TODO: no longer in use?
                                        JValue fortResource = null;
                                        int fortResourceValue = 0;
                                        if (getFortResource)
                                        {
                                            fortResource = (JValue)vehicleInfo.SelectToken("fortResource");
                                            if (fortResource != null && fortResource.Value != null)
                                                fortResourceValue = Convert.ToInt32(fortResource.Value);
                                        }
                                        values += ", " + fortResourceValue.ToString();

                                        // Added more
                                        fields += ", potentialDamageReceived, noDamageShotsReceived, sniperDamageDealt, piercingsReceived, pierced, isTeamKiller";
                                        values += ", " + vehicleInfo.SelectToken("potentialDamageReceived");
                                        values += ", " + vehicleInfo.SelectToken("noDamageDirectHitsReceived");
                                        values += ", " + vehicleInfo.SelectToken("sniperDamageDealt");
                                        values += ", " + vehicleInfo.SelectToken("piercingsReceived");
                                        values += ", " + vehicleInfo.SelectToken("piercings");

                                        // Is Team Killer
                                        bool isTeamKiller = Convert.ToBoolean(vehicleInfo.SelectToken("isTeamKiller"));
                                        if (isTeamKiller) values += ", 1"; else values += ", 0";

                                        // Added more
                                        fields += ", mileage, lifeTime, killerID, killerName, isPrematureLeave, explosionHits, explosionHitsReceived, damageBlockedByArmor, damageAssistedTrack, damageAssistedRadio ";
                                        values += ", " + vehicleInfo.SelectToken("mileage");
                                        values += ", " + vehicleInfo.SelectToken("lifeTime");

                                        // Killed by vehicle id
                                        newPlayer.vehicleId = Convert.ToInt32(vehicleInfo.SelectToken("vehicleId"));
                                        newPlayer.killerVehicleId = Convert.ToInt32(vehicleInfo.SelectToken("killerID"));
                                        values += ", " + vehicleInfo.SelectToken("killerID");
                                        values += ", NULL";

                                        // Premature Leave
                                        bool isPrematureLeave = Convert.ToBoolean(vehicleInfo.SelectToken("isPrematureLeave"));
                                        if (isPrematureLeave) values += ", 1"; else values += ", 0";

                                        // More fields
                                        values += ", " + vehicleInfo.SelectToken("explosionHits");
                                        values += ", " + vehicleInfo.SelectToken("explosionHitsReceived");
                                        values += ", " + vehicleInfo.SelectToken("damageBlockedByArmor");
                                        values += ", " + vehicleInfo.SelectToken("damageAssistedTrack");
                                        values += ", " + vehicleInfo.SelectToken("damageAssistedRadio");

                                        // If this is current player remember for later save to battle
                                        if (newPlayer.accountId == Config.Settings.playerAccountId)
                                        {
                                            if (getFortResource && fortResource != null && fortResource.Value != null)
                                                playerFortResources = Convert.ToInt32(fortResource.Value);

                                            playerPlatoonId = newPlayer.platoonId;
                                        }

                                        // Count sum frag/survival team/enemy remember for later save to battle
                                        if (playerDeathReason == "-1") // if player survived
                                            survivedCount[newPlayer.team]++;
                                        else
                                        {
                                            int playerEnemyTeam = 1;
                                            if (newPlayer.team == 1) playerEnemyTeam = 2;
                                            fragsCount[playerEnemyTeam]++;
                                        }
                                        // Add sum for team IR
                                        teamFortResources[newPlayer.team] += fortResourceValue;

                                        // Save for use later, to find killer info
                                        battlePlayers.Add(newPlayer);

                                        // Create SQL and update db
                                        sql = "insert into battlePlayer (" + fields + ") values (" + values + ")";
                                        bool success = await DB.ExecuteNonQuery(sql, false);
                                        if (!success)
                                        {
                                            // Add tank if missing
                                            // TODO : Not sure if working
                                            if (!TankHelper.TankExists(tankId))
                                            {
                                                await TankHelper.CreateUnknownTank(tankId, "Unknown");
                                                await DB.ExecuteNonQuery(sql, false);
                                            }
                                        }
                                    }

                                    // All Player information has been gathered time to retrieve proper killers and update
                                    uint killedByAccountId = 0;
                                    string killedByPlayerName = "";
                                    string updateSql = "";

                                    foreach (BattlePlayer player in battlePlayers)
                                    {
                                        BattlePlayer killer = battlePlayers.Find(k => k.vehicleId == player.killerVehicleId);

                                        if (killer != null)
                                        {
                                            updateSql += "UPDATE battlePlayer SET killerName=@killerName, KillerId=@killerId " +
                                                        "WHERE battleId=@battleId AND accountId=@accountId;";

                                            DB.AddWithValue(ref updateSql, "@killerName", killer.name, DB.SqlDataType.VarChar);
                                            DB.AddWithValue(ref updateSql, "@killerId", killer.accountId, DB.SqlDataType.Int);
                                            DB.AddWithValue(ref updateSql, "@battleId", battleId, DB.SqlDataType.Int);
                                            DB.AddWithValue(ref updateSql, "@accountId", player.accountId, DB.SqlDataType.Int);

                                            if (player.accountId == Config.Settings.playerAccountId)
                                            {
                                                killedByAccountId = killer.accountId;
                                                killedByPlayerName = killer.name;
                                            }
                                        }
                                    }
                                
                                    if (updateSql != "")
                                    {
                                        await DB.ExecuteNonQuery(updateSql, false);
                                    }

                                    // Get player platoon participants
                                    if (getPlatoon && playerPlatoonId > 0)
                                    {
                                        playerPlatoonParticipants = platoon.FindAll(p => p.platoonID == playerPlatoonId).Count;
                                    }

                                    // Update battle with enhanced values from players veicle section
                                    sql =
                                        "update battle set " +
                                        "  enemyClanAbbrev=@enemyClanAbbrev, " +
                                        "  enemyClanDBID=@enemyClanDBID, " +
                                        "  playerFortResources=@playerFortResources, " +
                                        "  clanForResources=@clanForResources, " +
                                        "  enemyClanFortResources=@enemyClanFortResources, " +
                                        "  killedByPlayerName=@killedByPlayerName, " +
                                        "  killedByAccountId=@killedByAccountId, " +
                                        "  platoonParticipants=@platoonParticipants, " +
                                        "  battleResultMode=@battleResultMode, " +
                                        "  survivedteam=@survivedteam, " +
                                        "  survivedenemy=@survivedenemy, " +
                                        "  fragsteam=@fragsteam, " +
                                        "  fragsenemy=@fragsenemy, " +
                                        "  minBattleTier=@minBattleTier, " +
                                        "  maxBattleTier=@maxBattleTier, " +
                                        "  posByXp=@posByXp, " +
                                        "  posByDmg=@posByDmg " +
                                        "where id=@battleId;";
                                    // Clan info
                                    int maxClanCount = 0;
                                    ClanInfo foundClan = new ClanInfo();
                                    foreach (ClanInfo item in clanCount)
                                    {
                                        if (item.Count > maxClanCount)
                                        {
                                            maxClanCount = item.Count;
                                            foundClan = item;
                                        }
                                    }
                                    if (getEnemyClan && maxClanCount > 0)
                                    {
                                        DB.AddWithValue(ref sql, "@enemyClanAbbrev", foundClan.ClanAbbrev, DB.SqlDataType.VarChar);
                                        DB.AddWithValue(ref sql, "@enemyClanDBID", foundClan.ClanId, DB.SqlDataType.Int);
                                    }
                                    else
                                    {
                                        DB.AddWithValue(ref sql, "@enemyClanAbbrev", DBNull.Value, DB.SqlDataType.VarChar);
                                        DB.AddWithValue(ref sql, "@enemyClanDBID", DBNull.Value, DB.SqlDataType.Int);
                                    }
                                    // Industrial Resources
                                    if (!getFortResource)
                                    {
                                        DB.AddWithValue(ref sql, "@playerFortResources", DBNull.Value, DB.SqlDataType.Int);
                                        DB.AddWithValue(ref sql, "@clanForResources", DBNull.Value, DB.SqlDataType.Int);
                                        DB.AddWithValue(ref sql, "@enemyClanFortResources", DBNull.Value, DB.SqlDataType.Int);
                                    }
                                    else
                                    {
                                        DB.AddWithValue(ref sql, "@playerFortResources", playerFortResources, DB.SqlDataType.Int);
                                        DB.AddWithValue(ref sql, "@clanForResources", teamFortResources[playerTeam], DB.SqlDataType.Int);
                                        DB.AddWithValue(ref sql, "@enemyClanFortResources", teamFortResources[enemyTeam], DB.SqlDataType.Int);
                                    }
                                    // Killed by
                                    if (killedByAccountId == 0)
                                    {
                                        DB.AddWithValue(ref sql, "@killedByPlayerName", DBNull.Value, DB.SqlDataType.VarChar);
                                        DB.AddWithValue(ref sql, "@killedByAccountId", DBNull.Value, DB.SqlDataType.Int);
                                    }
                                    else
                                    {
                                        DB.AddWithValue(ref sql, "@killedByPlayerName", killedByPlayerName, DB.SqlDataType.VarChar);
                                        DB.AddWithValue(ref sql, "@killedByAccountId", killedByAccountId, DB.SqlDataType.Int);
                                    }
                                    // Min Battle Tier
                                    int? minBattleTier = await GetMinBattleTier(battleId);
                                    if (minBattleTier == null)
                                        DB.AddWithValue(ref sql, "@minBattleTier", DBNull.Value, DB.SqlDataType.Int);
                                    else
                                        DB.AddWithValue(ref sql, "@minBattleTier", minBattleTier, DB.SqlDataType.Int);
                                    // Max Battle Tier
                                    int? maxBattleTier = await GetMaxBattleTier(battleId);
                                    if (maxBattleTier == null)
                                        DB.AddWithValue(ref sql, "@maxBattleTier", DBNull.Value, DB.SqlDataType.Int);
                                    else
                                        DB.AddWithValue(ref sql, "@maxBattleTier", maxBattleTier, DB.SqlDataType.Int);
                                    // Platoon
                                    DB.AddWithValue(ref sql, "@platoonParticipants", playerPlatoonParticipants, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@battleResultMode", battleResultMode, DB.SqlDataType.VarChar);
                                    // Survaival team /enemy
                                    DB.AddWithValue(ref sql, "@survivedteam", survivedCount[playerTeam], DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@survivedenemy", survivedCount[enemyTeam], DB.SqlDataType.Int);
                                    // Frags team/enemy
                                    DB.AddWithValue(ref sql, "@fragsteam", fragsCount[playerTeam], DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@fragsenemy", fragsCount[enemyTeam], DB.SqlDataType.Int);
                                    // Position on battle result team leaderboard
                                    var positions = await BattleHelper.GetPlayerPositionInTeamLeaderboard(battleId);

                                    DB.AddWithValue(ref sql, "@posByXp", positions.PosByXp, DB.SqlDataType.Int);
                                    DB.AddWithValue(ref sql, "@posByDmg", positions.PosByDmg, DB.SqlDataType.Int);
                                    // Add Battle ID and run sql if any values
                                    DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
                                    await DB.ExecuteNonQuery(sql);
                                    BattlesUpdated++;
                                    customErrMsg = "Module: Update Grinding Progress";
                                    // If grinding, adjust progress
                                    if (grindXP > 0)
                                        await GrindingProgress(playerTankId, (int)token_private["vehicle"].SelectToken("xp"));
                                    // Update Tank Credits
                                    customErrMsg = "Module: Update Tank Credits";
                                    await TankCreditCalculation.RecalculateForTank(playerTankId);
                                    // Done
                                    deleteFileAfterRead = true;
                                    GridView.scheduleGridRefresh = true;
                                    Log.AddToLogBuffer(" > > Done reading into DB JSON file: " + file);
                                    added++;

                                    // Add to success read battle list for later upload battles to website
                                    battleListSuccess.Add(battleId);
                                }
                            }
                            /*
                            else
                            {
                                Log.AddToLogBuffer(" > > New battle file not read, battle do not exists for JSON file: " + file);
                                // Battle do not exists, delete if old file file
                                if (battleTime < DateTime.Now.AddHours(-3))
                                    Log.AddToLogBuffer(" > > Old battle found, schedule for delete");
                                else
                                    deleteFileAfterRead = false; // keep file for a while, dossier file might be read later and then battle can be handled
                            }
                            */
                        } // if (! battleExists)
                    }
                    else
                    {
                        Log.AddToLogBuffer(" > > Battle file returned result: '" + result + "', could not process JSON file: " + file);
                        var message = token_parser?.SelectToken("message"); // get message
                        if (message != null)
                            Log.AddToLogBuffer(" > > > Message: " + message.ToString());
                        Log.AddToLogBuffer(" > > Faulty battle file schedule for delete");
                    }
                    // Delete file unless it is OK but not found battle from dossier yet
                    if (deleteFileAfterRead)
                    {
                        // Done - get battle file
                        FileInfo fileBattleJson = new FileInfo(file);
                        // Copy to save folder?
                        if (Config.SessionSettings.tempBattleResultSave)
                        {
                            fileBattleJson.CopyTo(Config.AppDataBattleResultSaved + fileBattleJson.FullName, true); // copy json battle file to save folder
                        }
                        // Delete from battleresult folder
                        fileBattleJson.Delete();
                        Log.AddToLogBuffer(" > > Deleted read or old JSON file: " + file);
                    }
                }

                // Discontinued
                // Upload to wot num web server
                //if (battleListSuccess.Count > 0)
                //{
                //    customErrMsg = "Module: Battle Upload";
                //    string uploadResult = await new Services.AppBattleUpload().RunForBattles(battleListSuccess);
                //    await Log.LogToFile($" > > Battle upload status: {uploadResult}");
                //}

                // Result logging
                if (filesJson.Length == 0) // Any files?
                    Log.AddToLogBuffer(" > > No battle files available");
                else // files converted
                {
                    if (added == 0)
                        Log.AddToLogBuffer(" > > " + processed.ToString() + " files checked, no new battle result detected");
                    else
                        Log.AddToLogBuffer(" > > " + processed.ToString() + " files checked, " + added + " files added as battle result");
                }
                await Log.WriteLogBuffer();
            }
            catch (Exception ex)
            {
                Log.AddToLogBuffer(" > > Battle file analyze terminated for file: " + lastFile);
                await Log.LogToFile(ex, $"Battle result file analyze process terminated due to faulty file structure or content. {customErrMsg}");
                deleteLastFileOnError = true;
            }
            try
            {
                if (deleteLastFileOnError)
                {
                    FileInfo fi = new FileInfo(lastFile);
                    FileInfo fileBattleJson = new FileInfo(lastFile);
                    fileBattleJson.Delete();
                    Log.AddToLogBuffer(" > > Deleted faulty JSON file: " + lastFile);
                }
            }
            catch (Exception ex)
            {
                Log.AddToLogBuffer(" > > Could not delete faulty JSON file: " + lastFile + " - Error: " + ex.Message);
            }
            return BattlesUpdated;
        }

        private async static Task GrindingProgress(int playerTankId, int XP)
        {
            // Yes, apply grinding progress to playerTank now
            // Get grinding data
            string sql =
                "SELECT tank.name, gCurrentXP, gGrindXP, gGoalXP, gProgressXP, gBattlesDay, gComment, lastVictoryTime, " +
                "        gCompleationDate, gProgressGoal, " +
                "        SUM(playerTankBattle.battles) as battles, SUM(playerTankBattle.wins) as wins, " +
                "        MAX(playerTankBattle.maxXp) AS maxXP, SUM(playerTankBattle.xp) AS totalXP, " +
                "        SUM(playerTankBattle.xp) / SUM(playerTankBattle.battles) AS avgXP " +
                "FROM    tank INNER JOIN " +
                "        playerTank ON tank.id = playerTank.tankId INNER JOIN " +
                "        playerTankBattle ON playerTank.id = playerTankBattle.playerTankId " +
                "WHERE  (playerTank.id = @playerTankId) " +
                "GROUP BY tank.name, gCurrentXP, gGrindXP, gGoalXP, gProgressXP, gBattlesDay, gComment, lastVictoryTime, gCompleationDate, gProgressGoal ";
            DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
            DataTable dt = await DB.FetchData(sql);
            DataRow grinding = dt.Rows[0];
            // Get parameters for grinding calc
            GrindingHelper.Progress progress = new GrindingHelper.Progress
            {
                ProgressXP = Convert.ToInt32(grinding["gProgressXP"]) + XP, // Added XP to previous progress
                TargetXP = Convert.ToInt32(grinding["gGrindXP"]),
                Battles = Convert.ToInt32(grinding["battles"]),
                Wins = Convert.ToInt32(grinding["wins"]),
                TotalXP = Convert.ToInt32(grinding["totalXP"]),
                AvgXP = Convert.ToInt32(grinding["avgXP"]),
                // Set current progress
                ProgressGoal = Convert.ToInt32(grinding["gProgressGoal"]),
                CompleationDate = null
            };
            if (grinding["gCompleationDate"] != DBNull.Value)
                progress.CompleationDate = Convert.ToDateTime(grinding["gCompleationDate"]);
            progress.BtlPerDay = Convert.ToInt32(grinding["gBattlesDay"]);
            // Calc new progress
            progress = GrindingHelper.CalcProgress(progress);
            // Save to playerTank
            sql =
                "UPDATE playerTank SET gProgressXP=@ProgressXP, gRestXP=@RestXP, gProgressPercent=@ProgressPercent, " +
                "					   gRestBattles=@RestBattles, gRestDays=@RestDays, gCompleationDate=@CompleationDate, gBattlesDay=@BattlesDay " +
                "WHERE id=@id; ";
            DB.AddWithValue(ref sql, "@ProgressXP", progress.ProgressXP, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@RestXP", progress.RestXP, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@ProgressPercent", progress.ProgressPercent, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@RestBattles", progress.RestBattles, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@RestDays", progress.RestDays, DB.SqlDataType.Int);
            DateTime date = Convert.ToDateTime(progress.CompleationDate);
            DB.AddWithValue(ref sql, "@CompleationDate", new DateTime(date.Year, date.Month, date.Day), DB.SqlDataType.DateTime);
            DB.AddWithValue(ref sql, "@BattlesDay", progress.BtlPerDay, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@id", playerTankId, DB.SqlDataType.Int);
            await DB.ExecuteNonQuery(sql);
        }

        private class ConvBtlUsingPythonResult
        {
            public bool Success { get; set; }
            public bool DeleteFile { get; set; }
        }

        private static void CheckBr2JSearchModulesPath()
        {
            // Look if script path is on search module paths list
            string appPath = Path.GetDirectoryName(Application.ExecutablePath); // path to app dir
            string battle2jsonScriptFolder = appPath + "\\battle2json";  // python-script for converting dossier file

            ICollection<string> searchPaths = PythonEngine.Engine.GetSearchPaths();
            bool pathFound = false;
            foreach (string path in searchPaths)
            {
                if (path == battle2jsonScriptFolder)
                {
                    pathFound = true;
                    break;
                }
            }
            if (!pathFound)
            {
                searchPaths.Add(battle2jsonScriptFolder);
                PythonEngine.Engine.SetSearchPaths(searchPaths);
            }
        }
        private async static Task<ConvBtlUsingPythonResult> ConvertBattleUsingPython(string filename)
        {
            ConvBtlUsingPythonResult result = new ConvBtlUsingPythonResult()
            {
                Success = false,
                DeleteFile = false
            };

            // Locate Python script
            string appPath = Path.GetDirectoryName(Application.ExecutablePath); // path to app dir
            string battle2jsonScriptFolder = appPath + "\\battle2json";  // python-script for converting dossier file
            string battle2jsonScript = battle2jsonScriptFolder + "\\wotbr2j.py";  // python-script for converting dossier file

            CheckBr2JSearchModulesPath();
            if (PythonEngine.LockPython(timeout: 10))
            {
                try
                {
                    PythonEngine.ipyOutput = ""; // clear ipy output
                    try
                    {
                        Log.AddToLogBuffer(" > > Starting to converted battle DAT-file to JSON file: " + filename);
                        var argv = new List
                        {
                            battle2jsonScript, // Have to add filename to run as first arg
                            filename,
                            "-f"
                        };

                        //NORMAL
                        PythonEngine.Engine.GetSysModule().SetVariable("argv", argv);
                        ScriptScope scope = PythonEngine.Engine.ExecuteFile(battle2jsonScript); // this is your python program
                        dynamic scopeResult = scope.GetVariable("main")();

                        Log.AddToLogBuffer(" > > > Converted battle DAT-file to JSON file: " + filename);
                        result.Success = true;
                        result.DeleteFile = true;
                    }
                    catch (SystemExitException)
                    {
                        Log.AddToLogBuffer(" > > > Some one called SystemExit instead of sys.exit(): Converted battle DAT-file to JSON file: " + filename);
                        result.Success = true;
                        result.DeleteFile = true;
                    }
                    catch (Exception ex)
                    {
                        Log.AddToLogBuffer(" > > IronPython exception thrown converted battle DAT-file to JSON file: " + filename);
                        await Log.LogToFile(ex, "ConvertBattleUsingPython exception running: " + battle2jsonScript + " with args: " + filename + " -f");
                        // Cleanup
                        result.DeleteFile = true;
                    }
                    Log.AddIpyToLogBuffer(PythonEngine.ipyOutput);
                    await Log.WriteLogBuffer();
                }
                finally
                {
                    PythonEngine.UnlockPython();
                }
            }
            else
            {
                Log.AddToLogBuffer(" > > Unable to lock Python environment for Dossier DAT-file conversion");
            }
            return result;
        }

        private async static Task<int?> GetMinBattleTier(int battleId)
        {
            try
            {
                int? minBattleTier = null;
                string sql =
                    "select min(tank.tier) " +
                    "from battlePlayer left join tank on battleplayer.tankid=tank.id " +
                    "where battleid=" + battleId;
                DataTable dt = await DB.FetchData(sql);
                if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                    minBattleTier = Convert.ToInt32(dt.Rows[0][0]);
                return minBattleTier;
            }
            catch (Exception ex)
            {
                await Log.LogToFile(ex, "Error getting min tier for tanks in battle: " + battleId);
                return null;
            }

        }

        private async static Task<int?> GetMaxBattleTier(int battleId)
        {
            try
            {
                int? maxBattleTier = null;
                string sql =
                    "select max(tank.tier) " +
                    "from battlePlayer left join tank on battleplayer.tankid=tank.id " +
                    "where battleid=" + battleId;
                DataTable dt = await DB.FetchData(sql);
                if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                    maxBattleTier = Convert.ToInt32(dt.Rows[0][0]);
                return maxBattleTier;
            }
            catch (Exception ex)
            {
                await Log.LogToFile(ex, "Error getting max tier for tanks in battle: " + battleId);
                return null;
            }

        }
        private async static Task<bool> CheckIfBattleExists(Int64 arenaUniqueID)
        {
            string sql = "SELECT id FROM battle WHERE arenaUniqueId = @arenaUniqueId;";

            DB.AddWithValue(ref sql, "@arenaUniqueId", arenaUniqueID, DB.SqlDataType.Int);
            DataTable dt = await DB.FetchData(sql);

            // If battle found from DB add enhanced values from battle file now
            return dt.Rows.Count > 0;
        }

        private async static Task<int> GetPlayerIdFromAccountId(uint accountId)
        {
            string sql = "SELECT id FROM player WHERE accountId = @accountId;";

            DB.AddWithValue(ref sql, "@accountId", accountId, DB.SqlDataType.Int);
            DataTable dt = await DB.FetchData(sql);

            // If battle found from DB add enhanced values from battle file now
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            else
            {
                return -1;
            }
        }

        private async static Task<bool> IsOrphanBattle(DateTime battleTime, int tankId)
        {
         DataTable dt;
            string sql =
                "select b.id as battleId, pt.id as playerTankId " +
                "from battle b left join playerTank pt on b.playerTankId = pt.id " +
                "where pt.tankId=@tankId and b.battleTime>@battleTimeFrom and b.battleTime<@battleTimeTo and b.battlesCount=1;";

            DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleTimeFrom", battleTime.AddSeconds(-Constants.BattleEndTimeThreshold), DB.SqlDataType.DateTime);
            DB.AddWithValue(ref sql, "@battleTimeTo", battleTime.AddSeconds(Constants.BattleEndTimeThreshold), DB.SqlDataType.DateTime);

            dt = await DB.FetchData(sql);

            // If battle found from DB add enhanced values from battle file now
            return (dt.Rows.Count == 0);
        }

        private static string GetUpdateFieldsStringFromBattleValues(List<BattleValue> battleValues)
        {
            string fields = "";
            bool addComma = false;
            foreach (var battleValue in battleValues)
            {
                if (addComma)
                {
                    fields += ", ";
                }

                if (battleValue.value.GetType() == typeof(DateTime))
                {
                    string temp = "@datetimevalue";
                    DB.AddWithValue(ref temp, "@datetimevalue", battleValue.value, DB.SqlDataType.DateTime);
                    fields += battleValue.colname + " = " + temp;
                }
                else if (battleValue.value.GetType() == typeof(String))
                {
                    fields += battleValue.colname + " = " + "'" + battleValue.value.ToString() + "'";
                }
                else
                {
                    fields += battleValue.colname + " = " + battleValue.value.ToString();
                }

                addComma = true;
            }

            return fields;
        }

        class InsertPair
        {
            public string fields;
            public string values;
        };

        private static InsertPair GetInsertFieldsStringFromBattleValues(List<BattleValue> battleValues)
        {
            string fields = "";
            string values = "";
            bool addComma = false;
            foreach (var battleValue in battleValues)
            {
                if (addComma)
                {
                    values += ", ";
                    fields += ", ";
                }

                if (battleValue.value.GetType() == typeof(DateTime))
                {
                    string temp = "@datetimevalue";
                    DB.AddWithValue(ref temp, "@datetimevalue", battleValue.value, DB.SqlDataType.DateTime);
                    values += temp;

                    // DateTime dateTime = Convert.ToDateTime(battleValue.value);
                    // string temp = dateTime.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    // values += "'" + temp + "'";
                }
                else if (battleValue.value.GetType() == typeof(string))
                {
                    values += "'" + battleValue.value.ToString() + "'";
                }
                else
                {
                    values += battleValue.value.ToString(); 
                }

                fields += battleValue.colname;
                addComma = true;
            }

            InsertPair pair = new InsertPair();
            pair.fields = fields;
            pair.values = values;

            return pair;
            // return new InsertPair{ fields, values };
        }

        private async static Task CreateOrphanBattleFromJSON(string jsonString)
        {
            // This should create a battle entry exactly as if it was created from the Dossier, but
            // with the orphanDat flag enabled.

            JToken token_root = JObject.Parse(jsonString);
            JToken token_common = token_root["common"];
            JToken token_private = token_root["private"];
            JToken token_account = token_private["account"];

            uint accountId = Convert.ToUInt32(token_account.SelectToken("accountDBID"));

            // Get the player ID from the player table using the accountDBID
            int playerId = await GetPlayerIdFromAccountId(accountId);
            if (playerId == -1)
            {
                // This dat file is not from a player in our WotNumbers database.
                return;
            }

            // we can get the data from vehicle or getting the AccountDBId and looking in the 
            // player list with this id. They contain the same values.
            JToken token_vehicle = token_private["vehicle"];
            int tankId = Convert.ToInt32(token_root.SelectToken("tankId"));

            // Return Existing Player Tank Data
            int playerTankId = await TankHelper.GetPlayerTankId(tankId, playerId);

            // Check if Player has this tank
            if (playerTankId  == -1)
            {
                // New tank detected, this parts only run when new tank is detected
                await Dossier2db.SaveNewPlayerTank(tankId, playerId); // Save new tank
                // Get data into DataTable once more now after row is added
                playerTankId = await TankHelper.GetPlayerTankId(tankId, playerId); 
            }

            BonusType bonusType = Convert.ToInt32(token_common.SelectToken("bonusType"));
            BattleMode.TypeEnum battleMode = bonusType.GetBattleMode();

            // Compute fields to add.
            List<BattleValue> battleValues = new List<BattleValue>
            {
                // common initial values
                  new BattleValue() { colname = "orphanDat", value = 1 }
                // , new BattleValue() { colname = "accountId", value = accountId }
                , new BattleValue() { colname = "playerTankId", value = playerTankId }
                , new BattleValue() { colname = "battlesCount", value = 1 }
            };

            battleValues.Add(new BattleValue() { colname = "arenaTypeID", value = Convert.ToInt32(token_common.SelectToken("arenaTypeID")) });

            // Compute rating parameters
            Rating.WNHelper.RatingParameters rp = new Code.Rating.WNHelper.RatingParameters();
            rp.DAMAGE = Convert.ToDouble(token_vehicle.SelectToken("damageDealt"));
            rp.SPOT = Convert.ToDouble(token_vehicle.SelectToken("spotted"));
            rp.FRAGS = Convert.ToDouble(token_vehicle.SelectToken("kills"));
            rp.DEF = Convert.ToDouble(token_vehicle.SelectToken("numDefended"));
            rp.CAP = Convert.ToDouble(token_vehicle.SelectToken("capturePoints"));

            int winnerTeam = Convert.ToInt32(token_common.SelectToken("winnerTeam"));
            int playerTeam = Convert.ToInt32(token_vehicle.SelectToken("team"));
            rp.WINS = (winnerTeam == playerTeam) ? 1.0 : 0.0;
            rp.BATTLES = 1.0f;

            Log.AddToLogBuffer("Battle2json::CreateOrphanBattleFromJSON: RatingParameters: " +
                "dmg(" + Convert.ToString(rp.DAMAGE) + "), " +
                "spot(" + Convert.ToString(rp.SPOT) + "), " +
                "frag(" + Convert.ToString(rp.FRAGS) + "), " +
                "def(" + Convert.ToString(rp.DEF) + "), " +
                "cap(" + Convert.ToString(rp.CAP) + "), " +
                "wins(" + Convert.ToString(rp.WINS) + "), " +
                "btls(" + Convert.ToString(rp.BATTLES) + "), " +
                "tier(" + Convert.ToString(rp.TIER) + ")");

            // Calculate WN9
            battleValues.Add(new BattleValue()
            {
                colname = "wn9",
                value = Math.Round((await Rating.WN9.CalcBattle(tankId, rp)).WN9, 0)
            });

            // Calculate WN8
            battleValues.Add(new BattleValue()
            {
                colname = "wn8",
                value = Math.Round(Rating.WN8.CalcBattle(tankId, rp), 0)
            });

            // Calc Eff
            battleValues.Add(new BattleValue()
            {
                colname = "eff",
                value = Math.Round(Rating.EFF.EffBattle(tankId, rp), 0)
            });

            // Calculate WN7
            rp.TIER = await Rating.WNHelper.GetAverageTier(BattleMode.GetItemFromType(battleMode).SqlName);
            battleValues.Add(new BattleValue()
            {
                colname = "wn7",
                value = Math.Round(Rating.WN7.WN7battle(rp, true), 0)
            });

            battleValues.Add(new BattleValue() { colname = "battleMode", value = BattleMode.GetItemFromType(battleMode).SqlName });

            // Calculate battle result
            battleValues.Add(new BattleValue() { colname = "battleResultId", value = (winnerTeam == playerTeam) ? 1 : (winnerTeam == 0) ? 2 : 3 });
            battleValues.Add(new BattleValue() { colname = "victory", value = (winnerTeam == playerTeam) ? 1 : 0 });
            battleValues.Add(new BattleValue() { colname = "draw", value = (winnerTeam == 0) ? 1 : 0 });
            battleValues.Add(new BattleValue() { colname = "defeat", value = (winnerTeam != playerTeam) ? 1 : 0 });

            // Calculate battle survive
            int deathCount = Convert.ToInt32(token_vehicle.SelectToken("deathCount"));
            battleValues.Add(new BattleValue() { colname = "survived", value = (deathCount == 0) ? 1 : 0 });
            battleValues.Add(new BattleValue() { colname = "battleSurviveId", value = (deathCount == 0) ? 1 : 3 });

            // Calculate kill
            battleValues.Add(new BattleValue() { colname = "killed", value = deathCount });

            battleValues.Add(new BattleValue() { colname = "frags", value = Convert.ToInt32(token_vehicle.SelectToken("kills")) });
            battleValues.Add(new BattleValue() { colname = "dmg", value = Convert.ToInt32(token_vehicle.SelectToken("damageDealt")) });
            battleValues.Add(new BattleValue() { colname = "dmgReceived", value = Convert.ToInt32(token_vehicle.SelectToken("damageReceived")) });
            battleValues.Add(new BattleValue() { colname = "assistSpot", value = Convert.ToInt32(token_vehicle.SelectToken("damageAssistedRadio")) });
            battleValues.Add(new BattleValue() { colname = "assistTrack", value = Convert.ToInt32(token_vehicle.SelectToken("damageAssistedTrack")) });
            battleValues.Add(new BattleValue() { colname = "cap", value = Convert.ToInt32(token_vehicle.SelectToken("capturePoints")) });
            battleValues.Add(new BattleValue() { colname = "def", value = Convert.ToInt32(token_vehicle.SelectToken("numDefended")) });

            battleValues.Add(new BattleValue() { colname = "assistStun", value = Convert.ToInt32(token_vehicle.SelectToken("damageAssistedStun")) });

            battleValues.Add(new BattleValue() { colname = "shots", value = Convert.ToInt32(token_vehicle.SelectToken("shots")) });
            battleValues.Add(new BattleValue() { colname = "hits", value = Convert.ToInt32(token_vehicle.SelectToken("directHits")) });
            battleValues.Add(new BattleValue() { colname = "shotsReceived", value = Convert.ToInt32(token_vehicle.SelectToken("directHitsReceived")) });
            battleValues.Add(new BattleValue() { colname = "pierced", value = Convert.ToInt32(token_vehicle.SelectToken("piercingEnemyHits")) });
            battleValues.Add(new BattleValue() { colname = "piercedReceived", value = Convert.ToInt32(token_vehicle.SelectToken("piercingsReceived")) });

            battleValues.Add(new BattleValue() { colname = "heHitsReceived", value = Convert.ToInt32(token_vehicle.SelectToken("explosionHitsReceived")) });
            battleValues.Add(new BattleValue() { colname = "noDmgShotsReceived", value = Convert.ToInt32(token_vehicle.SelectToken("noDamageDirectHitsReceived")) });
            battleValues.Add(new BattleValue() { colname = "heHits", value = Convert.ToInt32(token_vehicle.SelectToken("explosionHits")) });

            battleValues.Add(new BattleValue() { colname = "spotted", value = Convert.ToInt32(token_vehicle.SelectToken("spotted")) });
            battleValues.Add(new BattleValue() { colname = "mileage", value = Convert.ToInt32(token_vehicle.SelectToken("mileage")) });

            // No trees cut? we are green peace activists.
            battleValues.Add(new BattleValue() { colname = "treesCut", value = 0 });

            battleValues.Add(new BattleValue() { colname = "xp", value = Convert.ToInt32(token_vehicle.SelectToken("xp")) });
            battleValues.Add(new BattleValue() { colname = "xpOriginal", value = Convert.ToInt32(token_vehicle.SelectToken("originalXP")) });

            battleValues.Add(new BattleValue() { colname = "modeClan", value = Convert.ToInt32(token_vehicle.SelectToken("modeClan")) });
            battleValues.Add(new BattleValue() { colname = "modeCompany", value = Convert.ToInt32(token_vehicle.SelectToken("modeCompany")) });

            battleValues.Add(new BattleValue() { colname = "dmgBlocked", value = Convert.ToInt32(token_vehicle.SelectToken("damageBlockedByArmor")) });
            battleValues.Add(new BattleValue() { colname = "potentialDmgReceived", value = Convert.ToInt32(token_vehicle.SelectToken("potentialDamageReceived")) });

            battleValues.Add(new BattleValue() { colname = "transferred", value = 0 });

            // Rank by avg damage and progress (delta value), only for random battles
            if (battleMode == BattleMode.TypeEnum.ModeRandom_TC)
            {
                int damageRating = Convert.ToInt32(token_vehicle.SelectToken("damageRating"));
                int damageRatingDelta = 0;//  Convert.ToInt32(token_vehicle.SelectToken("damageRating");

                battleValues.Add(new BattleValue() { colname = "damageRatingTotal", value = damageRating });
                battleValues.Add(new BattleValue() { colname = "damageRating", value = damageRatingDelta });
            }

            // Calc battle times
            double arenaCreateTime = (double)token_common.SelectToken("arenaCreateTime"); // Arena create time
            double duration = (double)token_common.SelectToken("duration"); // Arena duration
            double battleEndTimeUnix = arenaCreateTime + Constants.ArenaCreationAndBattleStartTimeGap + duration; // Battle finish time

            DateTime battleFinishTime = DateTimeHelper.AdjustForTimeZone(DateTimeHelper.ConvertFromUnixTimestamp(battleEndTimeUnix));
            battleValues.Add(new BattleValue() { colname = "battleTime", value = battleFinishTime });

            // create a string with all the fields from the field list
            string updateSql = GetUpdateFieldsStringFromBattleValues(battleValues);
            
            InsertPair fieldPair = GetInsertFieldsStringFromBattleValues(battleValues);
            string sql = "INSERT INTO battle (" + fieldPair.fields + ") VALUES(" + fieldPair.values + ")";
            await DB.ExecuteNonQuery(sql);

            /* 
            // This will be done in the "update" phase of the battle data.

            int battleId = 0;
            sql = "select max(id) as battleId from battle";
            DataTable dt = await DB.FetchData(sql);
            if (dt.Rows.Count > 0)
                battleId = Convert.ToInt32(dt.Rows[0]["battleId"]);

            await CreateBattlePlayersForOrphanBattle(battleId, jsonString);
            */
        }

        private async static Task CreateBattlePlayersForOrphanBattle(int battleId, string jsonString)
        {
            JToken token_root = JObject.Parse(jsonString);
            JToken token_players = token_root["players"];

            // Create a list of players with basic information for later use in CreateBattlePlayerForOrphanBattle
            List<BattlePlayer> battlePlayers = new List<BattlePlayer>();
            foreach (JProperty playerToken in token_players)
            {
                JToken playerInfo = playerToken.First;

                BattlePlayer player = new BattlePlayer();
                player.accountId = Convert.ToUInt32(playerToken.Name);
                player.name = Convert.ToString(playerInfo.SelectToken("name"));

                JToken token_vehicle = playerInfo["vehicle"];
                player.vehicleId = Convert.ToInt32(token_vehicle.SelectToken("vehicleId"));

                battlePlayers.Add(player);
            }
            
            // For each player in the battle create an entry in the battlePlayers table.
            foreach (JProperty playerToken in token_players)
            {
                await CreateBattlePlayerForOrphanBattle(battleId, playerToken.First, battlePlayers);
            }
        }
        private async static Task CreateBattlePlayerForOrphanBattle(int battleId, JToken playerToken, List<BattlePlayer> battlePlayers)
        {
            JToken token_vehicle = playerToken["vehicle"];

            List<BattleValue> battleValues = new List<BattleValue>
            {
                // common initial values
                new BattleValue() { colname = "battleId", value = battleId }
            };

            battleValues.Add(new BattleValue() { colname = "accountId", value = Convert.ToUInt32(token_vehicle.SelectToken("accountDBID")) });
            battleValues.Add(new BattleValue() { colname = "name", value = Convert.ToString(playerToken.SelectToken("name")) });

            int team = Convert.ToInt32(token_vehicle.SelectToken("team"));
            battleValues.Add(new BattleValue() { colname = "team", value = team });

            battleValues.Add(new BattleValue() { colname = "tankId", value = Convert.ToInt32(token_vehicle.SelectToken("typeCompDescr")) });
            battleValues.Add(new BattleValue() { colname = "clanDBID", value = Convert.ToInt32(playerToken.SelectToken("clanDBID")) });
            battleValues.Add(new BattleValue() { colname = "clanAbbrev", value = Convert.ToString(playerToken.SelectToken("clanAbbrev")) });
            battleValues.Add(new BattleValue() { colname = "platoonID", value = Convert.ToInt32(playerToken.SelectToken("prebattleID")) });
            battleValues.Add(new BattleValue() { colname = "xp", value = Convert.ToInt32(token_vehicle.SelectToken("xp")) });
            battleValues.Add(new BattleValue() { colname = "damageDealt", value = Convert.ToInt32(token_vehicle.SelectToken("damageDealt")) });
            battleValues.Add(new BattleValue() { colname = "credits", value = Convert.ToInt32(token_vehicle.SelectToken("credits")) });
            battleValues.Add(new BattleValue() { colname = "capturePoints", value = Convert.ToInt32(token_vehicle.SelectToken("capturePoints")) });
            battleValues.Add(new BattleValue() { colname = "damageReceived", value = Convert.ToInt32(token_vehicle.SelectToken("damageReceived")) });
            battleValues.Add(new BattleValue() { colname = "deathReason", value = Convert.ToInt32(token_vehicle.SelectToken("deathReason")) });
            battleValues.Add(new BattleValue() { colname = "directHits", value = Convert.ToInt32(token_vehicle.SelectToken("directHits")) });
            battleValues.Add(new BattleValue() { colname = "directHitsReceived", value = Convert.ToInt32(token_vehicle.SelectToken("directHitsReceived")) });
            battleValues.Add(new BattleValue() { colname = "droppedCapturePoints", value = Convert.ToInt32(token_vehicle.SelectToken("droppedCapturePoints")) });
            battleValues.Add(new BattleValue() { colname = "hits", value = Convert.ToInt32(token_vehicle.SelectToken("directHits")) });
            battleValues.Add(new BattleValue() { colname = "kills", value = Convert.ToInt32(token_vehicle.SelectToken("kills")) });
            battleValues.Add(new BattleValue() { colname = "shots", value = Convert.ToInt32(token_vehicle.SelectToken("shots")) });
            battleValues.Add(new BattleValue() { colname = "shotsReceived", value = Convert.ToInt32(token_vehicle.SelectToken("directHitsReceived")) });
            battleValues.Add(new BattleValue() { colname = "spotted", value = Convert.ToInt32(token_vehicle.SelectToken("spotted")) });
            battleValues.Add(new BattleValue() { colname = "tkills", value = Convert.ToInt32(token_vehicle.SelectToken("tkills")) });
            // battleValues.Add(new BattleValue() { colname = "fortResource", value = Convert.ToInt32(token_vehicle.SelectToken("fortResource")) });
            battleValues.Add(new BattleValue() { colname = "potentialDamageReceived", value = Convert.ToInt32(token_vehicle.SelectToken("potentialDamageReceived")) });
            battleValues.Add(new BattleValue() { colname = "noDamageShotsReceived", value = Convert.ToInt32(token_vehicle.SelectToken("noDamageDirectHitsReceived")) });
            battleValues.Add(new BattleValue() { colname = "sniperDamageDealt", value = Convert.ToInt32(token_vehicle.SelectToken("sniperDamageDealt")) });
            battleValues.Add(new BattleValue() { colname = "piercingsReceived", value = Convert.ToInt32(token_vehicle.SelectToken("piercingsReceived")) });
            battleValues.Add(new BattleValue() { colname = "pierced", value = Convert.ToInt32(token_vehicle.SelectToken("piercingEnemyHits")) });
            battleValues.Add(new BattleValue() { colname = "mileage", value = Convert.ToInt32(token_vehicle.SelectToken("mileage")) });
            battleValues.Add(new BattleValue() { colname = "lifeTime", value = Convert.ToInt32(token_vehicle.SelectToken("lifeTime")) });
            battleValues.Add(new BattleValue() { colname = "isPrematureLeave", value = Convert.ToInt32(token_vehicle.SelectToken("isPrematureLeave")) });
            battleValues.Add(new BattleValue() { colname = "explosionHits", value = Convert.ToInt32(token_vehicle.SelectToken("explosionHits")) });
            battleValues.Add(new BattleValue() { colname = "explosionHitsReceived", value = Convert.ToInt32(token_vehicle.SelectToken("explosionHitsReceived")) });
            battleValues.Add(new BattleValue() { colname = "damageBlockedByArmor", value = Convert.ToInt32(token_vehicle.SelectToken("damageBlockedByArmor")) });
            battleValues.Add(new BattleValue() { colname = "damageAssistedTrack", value = Convert.ToInt32(token_vehicle.SelectToken("damageAssistedTrack")) });
            battleValues.Add(new BattleValue() { colname = "damageAssistedRadio", value = Convert.ToInt32(token_vehicle.SelectToken("damageAssistedRadio")) });
            battleValues.Add(new BattleValue() { colname = "isTeamKiller", value = Convert.ToInt32(token_vehicle.SelectToken("isTeamKiller")) });

            int killerId = Convert.ToInt32(token_vehicle.SelectToken("killerID"));
            int idx = battlePlayers.FindIndex(p => p.vehicleId == killerId);
            if (idx != -1)
            {
                battleValues.Add(new BattleValue() { colname = "killerID", value = battlePlayers[idx].accountId });
                battleValues.Add(new BattleValue() { colname = "killerName", value = battlePlayers[idx].name });
            }

            int playerTeam = (team == 1) ? 0 : 1;
            battleValues.Add(new BattleValue() { colname = "playerTeam", value = playerTeam });

            InsertPair insertPair = GetInsertFieldsStringFromBattleValues(battleValues);

            string sql = "INSERT INTO battlePlayer (" + insertPair.fields + ") VALUES(" + insertPair.values + ")";
            await DB.ExecuteNonQuery(sql);
        }
    }
}