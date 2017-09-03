using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json.Linq;

namespace WinApp.Code
{
	class Battle2json
	{
		private static List<string> battleResultDatFileCopied = new List<string>(); // List of dat-files copyied from wargaming battle folder, to avoid copy several times
		private static List<string> battleResultJsonFileExists = new List<string>(); // List of json-files already existing in battle folder, to avoid converting several times
		public static FileSystemWatcher battleResultFileWatcher = new FileSystemWatcher();
		
        private class ClanInfo
        {
            public int clanDBID { get; set; }
            public string clanAbbrev { get; set;  }
            public int count { get; set; }
        }

		private class BattlePlayer
		{
			public int accountDBID;
			public string clanAbbrev;
			public int clanDBID;
			public string name;
			public int platoonID;
			public int team;
			public int vehicleid;
			public int playerTeam = 0; // default value = false -> 0=false, 1=true
		}

		private class BattleValue
		{
			public string colname;
			public object value;
		}

		public static void UpdateBattleResultFileWatcher()
		{
			try
			{
				bool run = (Config.Settings.dossierFileWathcherRun == 1);
				if (Directory.Exists(Path.GetDirectoryName(Config.Settings.battleFilePath)))
				{
					battleResultFileWatcher.Path = Path.GetDirectoryName(Config.Settings.battleFilePath);
					battleResultFileWatcher.Filter = "*.dat";
					battleResultFileWatcher.IncludeSubdirectories = true;
					battleResultFileWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
					battleResultFileWatcher.Created += new FileSystemEventHandler(BattleResultFileCreated);
					battleResultFileWatcher.EnableRaisingEvents = run;
				}
				else
					battleResultFileWatcher.EnableRaisingEvents = false;
			}
			catch (Exception ex)
			{
				battleResultFileWatcher.EnableRaisingEvents = false;
				Log.LogToFile(ex, "Inncorrect dossier file path");
			}
			
		}

		private static void BattleResultFileCreated(object source, FileSystemEventArgs e)
		{
			if (!Dossier2db.Running)
			{
				Log.AddToLogBuffer("// New battle file detected");
				RunBattleResultRead();
			}
			else
				Log.LogToFile("// New battle file detected, reading is terminated due to dossier file process is running");
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

		public static bool ConvertBattleFilesToJson()
		{
			bool ok = true;
			try
			{
				// Upload prev unsuccsessful uploads to vBAddict 
				if (vBAddictHelper.Settings.UploadActive)
				{
					vBAddictBattleResultToUpload();
				}
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
							string filenameWihoutExt = Path.GetFileNameWithoutExtension(file).ToString();
							// Check if not copied previous (during this session), and that converted json file do not already exists (from previous sessions)
							if ((!battleResultDatFileCopied.Exists(x => x == file) && !battleResultJsonFileExists.Exists(x => x == filenameWihoutExt)) || getAll)
							{
								// Copy
								Log.AddToLogBuffer(" > > Start copying battle DAT-file: " + file);
								FileInfo fileBattleOriginal = new FileInfo(file); // the original dossier file
								string filename = Path.GetFileName(file);
								WaitUntilFileReadyToRead(file, 4000);
								fileBattleOriginal.CopyTo(Config.AppDataBattleResultFolder + filename, true); // copy original dossier fil and rename it for analyze
								Application.DoEvents();
								// if successful copy remember it
								if (File.Exists(Config.AppDataBattleResultFolder + filename))
								{
									battleResultDatFileCopied.Add(file);
									Log.AddToLogBuffer(" > > > Copied successfully battle DAT-file: " + file);
								}
							}
							else
								count++;
						}
						if (count > 0)
							Log.AddToLogBuffer(" > DAT-files skipped, read previous: " + count.ToString());
						if (filesDat.Length == 0)
							Log.AddToLogBuffer(" > No battle DAT-files found");
					}
					Log.WriteLogBuffer();
					// Loop through all dat-files copied to local folder
					string[] filesDatCopied = Directory.GetFiles(Config.AppDataBattleResultFolder, "*.dat");
					int totFilesDat = filesDatCopied.Count();
					if (totFilesDat > 0)
					{
						WaitUntilIronPythonReady(10000); // Wait until IronPython Engine is available, max 10 seconds
						Log.AddToLogBuffer(" > > Start converting " + totFilesDat.ToString() + " battle DAT-files to json");
						foreach (string file in filesDatCopied)
						{
							// Convert file to json
							bool deleteFile = false;
							bool okConvert = ConvertBattleUsingPython(file, out deleteFile);
							// Upload battle to vBAddict if OK
							if (okConvert && vBAddictHelper.Settings.UploadActive)
							{
								string msg = "";
								bool uploadOK = vBAddictHelper.UploadBattle(file, Config.Settings.playerName, Config.Settings.playerServer.ToLower(), vBAddictHelper.Settings.Token, out msg);
								if (uploadOK)
									Log.AddToLogBuffer(" > > > Uploaded battle to vBAddict successfully");
								else
								{
									Log.AddToLogBuffer(" > > > Error uploading to vBAddict, copy file for later upload");
									FileInfo fileBattleDatCopied = new FileInfo(file); // the battle file
									fileBattleDatCopied.CopyTo(Config.AppDataBattleResultToUpload + fileBattleDatCopied.Name);
									Log.AddToLogBuffer(msg);
								}
							}
							if (deleteFile)
							{
								// Success, json file is now created, clean up by delete dat file
								FileInfo fileBattleDatCopied = new FileInfo(file); // the original file
								fileBattleDatCopied.Delete(); // delete original DAT file
								if (okConvert)
									Log.AddToLogBuffer(" > > > Deleted successfully converted battle DAT-file: " + file);
								else
									Log.AddToLogBuffer(" > > > Deleted faulty battle DAT-file: " + file);
								Application.DoEvents();
							}
						}
					}
					else
						Log.AddToLogBuffer(" > No battle files found");
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex," > Error converting battle file to json");
				ok = false;
			}
			return ok;
		}

		private static void WaitUntilFileReadyToRead(string filePath, int maxWaitTime)
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
		}

		private static void vBAddictBattleResultToUpload()
		{
			try
			{
				// Loop through all dat-files copied to AppDataBattleResultToUpload folder from previous unsuccesful uploads
				string[] filesDatCopied = Directory.GetFiles(Config.AppDataBattleResultToUpload, "*.dat");
				int totFilesDat = filesDatCopied.Count();
				if (totFilesDat > 0)
				{
					Log.AddToLogBuffer(" > > Start uploading previous unsuccessful vBAddict uploads, " + totFilesDat.ToString() + " battle DAT-files found");
					foreach (string file in filesDatCopied)
					{
						string msg = "";
						bool uploadOK = vBAddictHelper.UploadBattle(file, Config.Settings.playerName, Config.Settings.playerServer.ToLower(), vBAddictHelper.Settings.Token, out msg);
						if (uploadOK)
						{
							Log.AddToLogBuffer(" > > > Uploaded to vBAddict successfully file: " + file);
							// Success, dat file is now created, clean up by delete dat file
							FileInfo fileBattleDatCopied = new FileInfo(file); // the original file
							fileBattleDatCopied.Delete();
							Log.AddToLogBuffer(" > > > File removed from upload folder successfully");
						}
						else
						{
							Log.AddToLogBuffer(" > > > Error uploading to vBAddict, keep file for later upload");
							FileInfo fileBattleDatCopied = new FileInfo(file); // the battle file
							fileBattleDatCopied.CopyTo(Config.AppDataBattleResultToUpload + fileBattleDatCopied.Name);
							Log.AddToLogBuffer(msg);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex, "Function for uploaded previous unsuccessful vBAddict uploads failed.");
			}
		}

		private class Platoon
		{
			public int platoonID;
			public int team;
			public int platoonNum;
		}

		private static string lastFile = "";

		public static void RunBattleResultRead(bool refreshGridOnFoundBattles = true, bool forceReadFiles = false)
		{
			bool deleteLastFileOnError = false;
			try
			{
				Log.AddToLogBuffer(" > Start looking for battle result");
				if (forceReadFiles)
				{
					battleResultDatFileCopied = new List<string>();
					Log.AddToLogBuffer(" > Clear history, force check all DAT-files");
				}
				bool refreshAfterUpdate = false;
				// Look for new files
				bool convertOK = ConvertBattleFilesToJson();
				// Get all json files
				Log.AddToLogBuffer(" > Start looking for converted json battle files");
				string[] filesJson = Directory.GetFiles(Config.AppDataBattleResultFolder, "*.json");
				// count action
				int processed = 0;
				int added = 0;
				bool deleteFileAfterRead = true;
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
					Application.DoEvents();
					StreamReader sr = new StreamReader(file, Encoding.UTF8);
					string json = sr.ReadToEnd();
					sr.Close();
					Application.DoEvents();
					// Root token
					JToken token_root = JObject.Parse(json);
					// Get version
					int btlResultVer = 0;
					JToken token_parser = token_root["parser"];
					JToken token_common = token_root["common"];
					string result = (string)token_parser.SelectToken("result"); // Find if ok - battleresultversion >= 15, WoT 9.8 and higher
					if (result == "ok")
						btlResultVer = (int)token_parser.SelectToken("battleResultVersion");
					else
					{
						result = (string)token_common.SelectToken("result"); // Find if ok - battleresultversion <= 14, Wot 9.7 and lower
						if (result == "ok")
							btlResultVer = (int)token_root.SelectToken("battleresultversion");
					}
					// Check if ok
					if (btlResultVer != 0)
					{
						Int64 arenaUniqueID = (Int64)token_root.SelectToken("arenaUniqueID"); // Find unique id
						double arenaCreateTime = (double)token_common.SelectToken("arenaCreateTime"); // Arena create time
						double duration = (double)token_common.SelectToken("duration"); // Arena duration
						double battlefinishUnix = arenaCreateTime + duration; // Battle finish time
						DateTime battleTime = DateTimeHelper.AdjustForTimeZone(DateTimeHelper.ConvertFromUnixTimestamp(battlefinishUnix)).AddSeconds(45);
                        DateTime battleTimeStart = DateTimeHelper.AdjustForTimeZone(DateTimeHelper.ConvertFromUnixTimestamp(arenaCreateTime));
                        // Personal token
                        JToken token_personel;
						if (btlResultVer >= 15)
							token_personel = token_root["personal"].First.First;
						else
							token_personel = token_root["personal"];
						// Get tank
						int tankId = (int)token_personel.SelectToken("typeCompDescr"); // tankId
						// Check for special tanks
						List<int> specialTanks = new List<int>(new[] { 64801, 64769, 65089 }); // Mammoth, Arctic Fox, Polar Bear
						bool specialTankFound = (specialTanks.Contains(tankId));
						// Now find battle created from dossier, or create now if special tank = special Event
						DataTable dt;
						string sql = "";
						if (specialTankFound) // Special tanks, no battle is created from dossier, check for existing battle mapping against arenaUniqueId
						{
							sql =
								"select b.id as battleId, pt.id as playerTankId, pt.gGrindXP, b.arenaUniqueID  " +
								"from battle b left join playerTank pt on b.playerTankId = pt.id " +
								"where b.arenaUniqueID=@arenaUniqueID;";
							DB.AddWithValue(ref sql, "@arenaUniqueID", arenaUniqueID, DB.SqlDataType.Int);
							dt = DB.FetchData(sql);
							// check if battle exists
							if (dt.Rows.Count == 0)
							{
								// Create battle, do not exists
								int playerTankId = TankHelper.GetPlayerTankId(tankId);
								sql =
									"insert into battle " +
                                    "(playerTankId,  battleMode, battlesCount, battleTime,  battleTimeStart, battleResultId, battleSurviveId) values " +
                                    "(@playerTankId, 'Special',  1,            @battleTime, @battleTimeStart,1,              1               );";
								DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
								DB.AddWithValue(ref sql, "@battleTime", battleTime, DB.SqlDataType.DateTime);
                                DB.AddWithValue(ref sql, "@battleTimeStart", battleTimeStart, DB.SqlDataType.DateTime);
                                DB.ExecuteNonQuery(sql);
							}
						}
						// Fetch battle
						sql =
								"select b.id as battleId, pt.id as playerTankId, pt.gGrindXP, b.arenaUniqueID, b.battleMode  " +
								"from battle b left join playerTank pt on b.playerTankId = pt.id " +
								"where pt.tankId=@tankId and b.battleTime>@battleTimeFrom and b.battleTime<@battleTimeTo and b.battlesCount=1;";
						DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
						DB.AddWithValue(ref sql, "@battleTimeFrom", battleTime.AddSeconds(-30), DB.SqlDataType.DateTime);
						DB.AddWithValue(ref sql, "@battleTimeTo", battleTime.AddSeconds(30), DB.SqlDataType.DateTime);
						dt = DB.FetchData(sql);
						// If battle found from DB add enhanced values from battle file now
						if (dt.Rows.Count > 0)
						{
							// Check if already read, if nowt continue adding enhanced values
							if (dt.Rows[0]["arenaUniqueID"] == DBNull.Value)
							{
								// Battle without enchanced values found
								int battleId = Convert.ToInt32(dt.Rows[0]["battleId"]);
								int playerTankId = Convert.ToInt32(dt.Rows[0]["playerTankId"]);
                                //string battleMode = dt.Rows[0]["battleMode"].ToString();
								int grindXP = Convert.ToInt32(dt.Rows[0]["gGrindXP"]);
								// Get values
								List<BattleValue> battleValues = new List<BattleValue>();
								// common initial values
								battleValues.Add(new BattleValue() { colname = "arenaTypeID", value = (int)token_common.SelectToken("arenaTypeID") });
								int playerTeam = (int)token_personel.SelectToken("team");
								int enemyTeam = 1;
								if (playerTeam == 1) enemyTeam = 2;
								// Find game type
								int bonusType = (int)token_common.SelectToken("bonusType");
								battleValues.Add(new BattleValue() { colname = "bonusType", value = bonusType });
								// Get platoon
								bool getPlatoon = false;
								if (bonusType == 1)
									getPlatoon = true;
								// Get Industrial Resource
								bool getFortResource = false;
								if (bonusType == 10)
									getFortResource = true;
								// Get battle mode as text from bonus type, also set flag for get clan for spesific battle types
                                string battleResultMode = "";
								bool getEnemyClan = false;
                                // TODO: Include grand battles
								switch (bonusType)
								{
									case 0: battleResultMode = "Unknown"; break;
									case 1: battleResultMode = "Random"; break;
									case 2: battleResultMode = "Trainig Room"; break;
									case 3: battleResultMode = "Tank Company"; getEnemyClan = true; break;
									case 4: battleResultMode = "Tournament"; getEnemyClan = true; break;
									case 5: battleResultMode = "Clan War"; getEnemyClan = true; break;
									case 6: battleResultMode = "Tutorial"; break;
									case 7: battleResultMode = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeTeam).Name; break;
                                    case 8: battleResultMode = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeHistorical).Name; break;
                                    case 9: battleResultMode = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeSpecial).Name; break;
                                    case 10: battleResultMode = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeSkirmishes).Name; getEnemyClan = true; break;
                                    case 11: battleResultMode = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeStronghold).Name; getEnemyClan = true; break;
                                    case 12: battleResultMode = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeTeamRanked).Name; getEnemyClan = true; break;
									case 13: battleResultMode = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeGlobalMap).Name; getEnemyClan = true; break;
								}
								battleValues.Add(new BattleValue() { colname = "bonusTypeName", value = "'" + (string)token_common.SelectToken("bonusTypeName") + "'" });
								battleValues.Add(new BattleValue() { colname = "finishReasonName", value = "'" + (string)token_common.SelectToken("finishReasonName") + "'" });
								// personal - credits
								battleValues.Add(new BattleValue() { colname = "originalCredits", value = (int)token_personel.SelectToken("originalCredits") });
								battleValues.Add(new BattleValue() { colname = "credits", value = (int)token_personel.SelectToken("credits") });
								battleValues.Add(new BattleValue() { colname = "creditsPenalty", value = (int)token_personel.SelectToken("creditsPenalty") });
								battleValues.Add(new BattleValue() { colname = "creditsToDraw", value = (int)token_personel.SelectToken("creditsToDraw") });
								battleValues.Add(new BattleValue() { colname = "creditsContributionIn", value = (int)token_personel.SelectToken("creditsContributionIn") });
								battleValues.Add(new BattleValue() { colname = "creditsContributionOut", value = (int)token_personel.SelectToken("creditsContributionOut") });
								battleValues.Add(new BattleValue() { colname = "autoRepairCost", value = (int)token_personel.SelectToken("autoRepairCost") });
								battleValues.Add(new BattleValue() { colname = "eventCredits", value = (int)token_personel.SelectToken("eventCredits") });
								battleValues.Add(new BattleValue() { colname = "premiumCreditsFactor10", value = (int)token_personel.SelectToken("premiumCreditsFactor10") });
								battleValues.Add(new BattleValue() { colname = "achievementCredits", value = (int)token_personel.SelectToken("achievementCredits") });
								// personal XP
								battleValues.Add(new BattleValue() { colname = "real_xp", value = (int)token_personel.SelectToken("xp") });
								battleValues.Add(new BattleValue() { colname = "xpPenalty", value = (int)token_personel.SelectToken("xpPenalty") });
								battleValues.Add(new BattleValue() { colname = "freeXP", value = (int)token_personel.SelectToken("freeXP") });
								battleValues.Add(new BattleValue() { colname = "dailyXPFactor10", value = (int)token_personel.SelectToken("dailyXPFactor10") });
								battleValues.Add(new BattleValue() { colname = "premiumXPFactor10", value = (int)token_personel.SelectToken("premiumXPFactor10") });
								battleValues.Add(new BattleValue() { colname = "eventFreeXP", value = (int)token_personel.SelectToken("eventFreeXP") });
								battleValues.Add(new BattleValue() { colname = "achievementFreeXP", value = (int)token_personel.SelectToken("achievementFreeXP") });
								battleValues.Add(new BattleValue() { colname = "achievementXP", value = (int)token_personel.SelectToken("achievementXP") });
								battleValues.Add(new BattleValue() { colname = "eventXP", value = (int)token_personel.SelectToken("eventXP") });
								battleValues.Add(new BattleValue() { colname = "eventTMenXP", value = (int)token_personel.SelectToken("eventTMenXP") });
								// personal markOfMastery
                                battleValues.Add(new BattleValue() { colname = "markOfMastery", value = (int)token_personel.SelectToken("markOfMastery") }); 
								// Other
                                battleValues.Add(new BattleValue() { colname = "vehTypeLockTime", value = (int)token_personel.SelectToken("vehTypeLockTime") });
								battleValues.Add(new BattleValue() { colname = "marksOnGun", value = (int)token_personel.SelectToken("marksOnGun") });
								// Rating values, more adds later
                                Rating.WNHelper.RatingParameters rp = new Rating.WNHelper.RatingParameters();
                                rp.DEF = (int)token_personel.SelectToken("droppedCapturePoints");
								battleValues.Add(new BattleValue() { colname = "def", value = rp.DEF }); // override def - might be above 100
								// field returns null
                                if (getFortResource)
								    if (token_personel.SelectToken("fortResource") != null && token_personel.SelectToken("fortResource").HasValues)
									    battleValues.Add(new BattleValue() { colname = "fortResource", value = (int)token_personel.SelectToken("fortResource") });
								// dayly double
								int dailyXPFactor = (int)token_personel.SelectToken("dailyXPFactor10") / 10;
								battleValues.Add(new BattleValue() { colname = "dailyXPFactorTxt", value = "'" + dailyXPFactor.ToString() + " X'" });
								// Special fields: death reason, convert to string
								int deathReasonId = (int)token_personel.SelectToken("deathReason");
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
								JArray array_autoload = (JArray)token_personel.SelectToken("autoLoadCost");
								int autoLoadCost = (int)array_autoload[0];
								battleValues.Add(new BattleValue() { colname = "autoLoadCost", value = autoLoadCost });
								// Get from array autoEquipCost
								// Get fro array autoEqipCost
                                JArray array_autoequip = (JArray)token_personel.SelectToken("autoEquipCost");
                                int autoEquipCost = (int)array_autoequip[0];
								battleValues.Add(new BattleValue() { colname = "autoEquipCost", value = autoEquipCost });
								// Calculated net credits
								int creditsNet = (int)token_personel.SelectToken("credits");
								creditsNet -= (int)token_personel.SelectToken("creditsPenalty"); // fine for damage to allies
								creditsNet += (int)token_personel.SelectToken("creditsToDraw"); // compensation for dmg caused by allies
								creditsNet -= (int)token_personel.SelectToken("autoRepairCost"); // repear cost
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
                                battleValues.Add(new BattleValue() { colname = "battleTimeStart", value = battleTimeStart});
                                // Special tank extra data insert, not fetched from dossier file
                                if (specialTankFound)
								{
									// battle lifetime
									battleValues.Add(new BattleValue() { colname = "battleLifeTime", value = (int)token_personel.SelectToken("lifeTime") });
									// winning team
									int winnerTeam = (int)token_common.SelectToken("winnerTeam");
									rp.WINS = 0;
									if (winnerTeam == playerTeam)
									{
										battleValues.Add(new BattleValue() { colname = "victory", value = 1 });
										battleValues.Add(new BattleValue() { colname = "battleResultId", value = 1 });
                                        rp.WINS = 1;
									}
									else if (winnerTeam == enemyTeam)
									{
										battleValues.Add(new BattleValue() { colname = "defeat", value = 1 });
										battleValues.Add(new BattleValue() { colname = "battleResultId", value = 3 });
									}
									else 
									{
										battleValues.Add(new BattleValue() { colname = "draw", value = 1 });
										battleValues.Add(new BattleValue() { colname = "battleResultId", value = 2 });
									}
									// survival
									int playerDeathReason = (int)token_personel.SelectToken("deathReason");
									if (playerDeathReason == -1)
									{
										battleValues.Add(new BattleValue() { colname = "killed", value = 1 });
										battleValues.Add(new BattleValue() { colname = "battleSurviveId", value = 1 });
									}
									else
									{
										battleValues.Add(new BattleValue() { colname = "survived", value = 1 });
										battleValues.Add(new BattleValue() { colname = "battleSurviveId", value = 3 });
									}
									// other
									rp.DAMAGE = (double)token_personel.SelectToken("damageDealt");
									battleValues.Add(new BattleValue() { colname = "dmg", value = rp.DAMAGE });
									rp.FRAGS = (double)token_personel.SelectToken("kills");
									battleValues.Add(new BattleValue() { colname = "frags", value = rp.FRAGS });
									battleValues.Add(new BattleValue() { colname = "dmgReceived", value = (int)token_personel.SelectToken("damageReceived") });
									battleValues.Add(new BattleValue() { colname = "assistSpot", value = (int)token_personel.SelectToken("damageAssistedRadio") });
									battleValues.Add(new BattleValue() { colname = "assistTrack", value = (int)token_personel.SelectToken("damageAssistedTrack") });
									rp.CAP = (double)token_personel.SelectToken("capturePoints");
									battleValues.Add(new BattleValue() { colname = "cap", value = rp.CAP });
									//battleValues.Add(new BattleValue() { colname = "def", value = (int)token_personel.SelectToken("droppedCapturePoints") });
									battleValues.Add(new BattleValue() { colname = "shots", value = (int)token_personel.SelectToken("shots") });
									battleValues.Add(new BattleValue() { colname = "hits", value = (int)token_personel.SelectToken("hits") });
									battleValues.Add(new BattleValue() { colname = "shotsReceived", value = (int)token_personel.SelectToken("shotsReceived") });
									battleValues.Add(new BattleValue() { colname = "pierced", value = (int)token_personel.SelectToken("pierced") });
									battleValues.Add(new BattleValue() { colname = "piercedReceived", value = (int)token_personel.SelectToken("piercedReceived") });
									rp.SPOT = (double)token_personel.SelectToken("spotted");
									battleValues.Add(new BattleValue() { colname = "spotted", value = rp.SPOT });
									battleValues.Add(new BattleValue() { colname = "mileage", value = (int)token_personel.SelectToken("mileage") });
									//battleValues.Add(new BattleValue() { colname = "treesCut", value = (int)token_personel.SelectToken("") });
									battleValues.Add(new BattleValue() { colname = "xp", value = (int)token_personel.SelectToken("originalXP") });
									battleValues.Add(new BattleValue() { colname = "heHitsReceived", value = (int)token_personel.SelectToken("heHitsReceived") });
									battleValues.Add(new BattleValue() { colname = "noDmgShotsReceived", value = (int)token_personel.SelectToken("noDamageShotsReceived") });
									battleValues.Add(new BattleValue() { colname = "heHits", value = (int)token_personel.SelectToken("he_hits") });
									battleValues.Add(new BattleValue() { colname = "dmgBlocked", value = (int)token_personel.SelectToken("damageBlockedByArmor") });
									battleValues.Add(new BattleValue() { colname = "potentialDmgReceived", value = (int)token_personel.SelectToken("potentialDamageReceived") });
									//Ratings
									rp.TIER = TankHelper.GetTankTier(tankId);
                                    rp.BATTLES = 1;
                                    double eff = Code.Rating.EFF.EffBattle(tankId, rp);
									battleValues.Add(new BattleValue() { colname = "EFF", value = Math.Round(eff,0) });
                                    double wn7 = Code.Rating.WN7.WN7battle(rp, true);
									battleValues.Add(new BattleValue() { colname = "WN7", value = Math.Round(wn7, 0) });
                                    double wn8 = Code.Rating.WN8.CalcBattle(tankId, rp);
									battleValues.Add(new BattleValue() { colname = "WN8", value = Math.Round(wn8, 0) });
                                    double wn9maxhist = 0; // not in used for battles
                                    double wn9 = Code.Rating.WN9.CalcBattle(tankId, rp, out wn9maxhist);
                                    battleValues.Add(new BattleValue() { colname = "WN9", value = Math.Round(wn9, 0) });
                                }
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
								DB.ExecuteNonQuery(sql);
								
								// Add Battle Players *******************************
								
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
								int killerID = 0;
								List<Platoon> platoon = new List<Platoon>();
								int playerPlatoonId = 0;
								int playerPlatoonParticipants = 0;
								int killedByAccountId = 0;
								string killedByPlayerName = "";
                                List<ClanInfo> clanCount = new List<ClanInfo>();
								foreach (JToken player in token_players)
								{
									BattlePlayer newPlayer = new BattlePlayer();
									JProperty playerProperty = (JProperty)player;
									newPlayer.accountDBID = Convert.ToInt32(playerProperty.Name);
									JToken playerInfo = player.First;
									newPlayer.clanDBID = (int)playerInfo.SelectToken("clanDBID");
									newPlayer.clanAbbrev = (string)playerInfo.SelectToken("clanAbbrev");
									newPlayer.name = (string)playerInfo.SelectToken("name");
									if (btlResultVer >= 15)
									{
										newPlayer.platoonID = (int)playerInfo.SelectToken("prebattleID");
										newPlayer.vehicleid = 0; // NEW METHOD NOT SUPPORTED FOR NEW FILE STRUCT YET ->
									}
									else
									{
										newPlayer.platoonID = (int)playerInfo.SelectToken("platoonID");
										newPlayer.vehicleid = (int)playerInfo.SelectToken("vehicleid");
									}
									newPlayer.team = (int)playerInfo.SelectToken("team");
									if (newPlayer.team == playerTeam)
										newPlayer.playerTeam = 1;
									
									battlePlayers.Add(newPlayer);
									// Get values for saving to battle
                                    if (getEnemyClan && newPlayer.clanDBID > 0 && newPlayer.team == enemyTeam) // Get enemy clan
									{
                                        // Found player with clan, add to clan count
                                        bool foundPlayerClan = false;
                                        foreach (ClanInfo item in clanCount)
                                        {
                                            if (item.clanDBID == newPlayer.clanDBID)
                                            {
                                                item.count++;
                                                foundPlayerClan = true;
                                            }
                                        }
                                        if (!foundPlayerClan)
                                        {
                                            clanCount.Add(new ClanInfo() {
                                                clanDBID = newPlayer.clanDBID,
                                                clanAbbrev = newPlayer.clanAbbrev,
                                                count = 1
                                            });
                                        }
									}
									if (getPlatoon && newPlayer.platoonID > 0) // Get platoon info
									{
										Platoon p = new Platoon();
										p.platoonID = newPlayer.platoonID;
										p.team = newPlayer.team;
										p.platoonNum = 0;
										platoon.Add(p);
									}
								}
								// Get results from vehiles section and add to db
								JToken token_vehicles = token_root["vehicles"];
								// First iteration to get vehiclesid to late be able to find player killed by
								if (btlResultVer >= 15)
								{
									foreach (JToken vechicle in token_vehicles)
									{
										JProperty vProperty = (JProperty)vechicle;
										int vid = Convert.ToInt32(vProperty.Name);
										JValue accountDBID = (JValue)vechicle.First.First.SelectToken("accountDBID");
										BattlePlayer player = battlePlayers.Find(p => p.accountDBID == Convert.ToInt32(accountDBID));
										player.vehicleid = vid;
									}
								}
								// Get battle player data
								foreach (JToken vechicle in token_vehicles)
								{
									// Get json token vith vechicle data
									JProperty vechicleProperty = (JProperty)vechicle;
									JToken vechicleInfo;
									// Find player for mapping
									BattlePlayer player = new BattlePlayer(); 
									if (btlResultVer >= 15)
									{
										vechicleInfo = vechicle.First.First;
										JValue accountDBID = (JValue)vechicleInfo.SelectToken("accountDBID");
										if (accountDBID != null)
											player = battlePlayers.Find(p => p.accountDBID == Convert.ToInt32(accountDBID));
									}
									else
									{
										vechicleInfo = vechicle.First;
										int vehicleid = Convert.ToInt32(vechicleProperty.Name);
										player = battlePlayers.Find(p => p.vehicleid == vehicleid);
									}

									if (player != null)
									{
										
										// Get fields and values, init adding battle id
										fields = "battleID";
										string values = battleId.ToString();
										string jsonField = "";
										// Get values from player section
										fields += ", accountId, clanAbbrev, clanDBID, name, platoonID, team, playerTeam";
										values += ", " + player.accountDBID.ToString();
										values += ", '" + player.clanAbbrev + "'";
										values += ", " + player.clanDBID.ToString();
										values += ", '" + player.name + "'";
										values += ", " + player.platoonID.ToString();
										values += ", " + player.team.ToString();
										values += ", " + player.playerTeam.ToString();
										// Get values from vehicles section
										fields += ", tankId, xp , damageDealt, credits, capturePoints, damageReceived, deathReason, directHits";
										// typeCompDescr = tankId, might be missing in clan wars if player not spoddet
                                        string checkPlayerTankId = vechicleInfo.SelectToken("typeCompDescr").ToString();
                                        if (checkPlayerTankId == "" || checkPlayerTankId == "0")
											values += ", -1";
										else
                                            values += ", " + checkPlayerTankId;
										values += ", " + vechicleInfo.SelectToken("xp");
										values += ", " + vechicleInfo.SelectToken("damageDealt");
										values += ", " + vechicleInfo.SelectToken("credits");
										values += ", " + vechicleInfo.SelectToken("capturePoints");
										values += ", " + vechicleInfo.SelectToken("damageReceived");
										string playerDeathReason = vechicleInfo.SelectToken("deathReason").ToString();
										values += ", " + playerDeathReason;
										values += ", " + vechicleInfo.SelectToken("directHits");
										fields += ", directHitsReceived, droppedCapturePoints, hits, kills, shots, shotsReceived, spotted, tkills, fortResource";
										values += ", " + vechicleInfo.SelectToken("directHitsReceived");
										values += ", " + vechicleInfo.SelectToken("droppedCapturePoints");
										if (btlResultVer >= 15) { jsonField = "directHits"; } else { jsonField = "hits"; } // CHANGED STRUCT FROM WOT 9.8
										values += ", " + vechicleInfo.SelectToken(jsonField);
										values += ", " + vechicleInfo.SelectToken("kills");
										values += ", " + vechicleInfo.SelectToken("shots");
										if (btlResultVer >= 15) { jsonField = "directHitsReceived"; } else { jsonField = "shotsReceived"; } // CHANGED STRUCT FROM WOT 9.8
										values += ", " + vechicleInfo.SelectToken(jsonField);
										values += ", " + vechicleInfo.SelectToken("spotted");
										values += ", " + vechicleInfo.SelectToken("tkills");
                                        // TODO: no longer in use?
                                        JValue fortResource = null;
                                        int fortResourceValue = 0;
                                        if (getFortResource)
                                        {
                                            fortResource = (JValue)vechicleInfo.SelectToken("fortResource");
                                            if (fortResource != null && fortResource.Value != null)
                                                fortResourceValue = Convert.ToInt32(fortResource.Value);
                                        }
                                        values += ", " + fortResourceValue.ToString();
                                        // Added more
                                        fields += ", potentialDamageReceived, noDamageShotsReceived, sniperDamageDealt, piercingsReceived, pierced, isTeamKiller";
										values += ", " + vechicleInfo.SelectToken("potentialDamageReceived");
										if (btlResultVer >= 15) { jsonField = "noDamageDirectHitsReceived"; } else { jsonField = "noDamageShotsReceived"; } // CHANGED STRUCT FROM WOT 9.8
										values += ", " + vechicleInfo.SelectToken(jsonField);
										values += ", " + vechicleInfo.SelectToken("sniperDamageDealt");
										values += ", " + vechicleInfo.SelectToken("piercingsReceived");
										if (btlResultVer >= 15) { jsonField = "piercings"; } else { jsonField = "pierced"; } // CHANGED STRUCT FROM WOT 9.8
										values += ", " + vechicleInfo.SelectToken(jsonField);
										
										// Is Team Killer
										bool isTeamKiller = Convert.ToBoolean(vechicleInfo.SelectToken("isTeamKiller"));
										if (isTeamKiller) values += ", 1"; else values += ", 0";
										// Added more
										fields += ", mileage, lifeTime, killerID, killerName, isPrematureLeave, explosionHits, explosionHitsReceived, damageBlockedByArmor, damageAssistedTrack, damageAssistedRadio ";
										values += ", " + vechicleInfo.SelectToken("mileage");
										values += ", " + vechicleInfo.SelectToken("lifeTime");
										// Killed by account id
										int playerKillerId = Convert.ToInt32(vechicleInfo.SelectToken("killerID"));
										BattlePlayer killer = battlePlayers.Find(k => k.vehicleid == playerKillerId);
										if (killer != null)
											values += ", " + killer.accountDBID;
										else
											values += ", 0";
										if (killer != null)
											values += ", '" + killer.name + "'";
										else
											values += ", NULL";
										// Premature Leave
										bool isPrematureLeave = Convert.ToBoolean(vechicleInfo.SelectToken("isPrematureLeave"));
										if (isPrematureLeave) values += ", 1"; else values += ", 0";
										// More fields
										values += ", " + vechicleInfo.SelectToken("explosionHits");
										values += ", " + vechicleInfo.SelectToken("explosionHitsReceived");
										values += ", " + vechicleInfo.SelectToken("damageBlockedByArmor");
										values += ", " + vechicleInfo.SelectToken("damageAssistedTrack");
										values += ", " + vechicleInfo.SelectToken("damageAssistedRadio");
										// If this is current player remember for later save to battle
										if (player.name == Config.Settings.playerName)
										{
                                            if (getFortResource && fortResource != null && fortResource.Value != null)
                                                playerFortResources = Convert.ToInt32(fortResource.Value);
											killerID = playerKillerId;
											playerPlatoonId = player.platoonID;
										}
										// Count sum frag/survival team/enemy remember for later save to battle
										if (playerDeathReason == "-1") // if player survived
											survivedCount[player.team]++;
										else
										{
											int playerEnemyTeam = 1;
											if (player.team == 1) playerEnemyTeam = 2;
											fragsCount[playerEnemyTeam]++;
										}
										// Add sum for team IR
										teamFortResources[player.team] += fortResourceValue;
										// Create SQL and update db
										sql = "insert into battlePlayer (" + fields + ") values (" + values + ")";
										bool success = DB.ExecuteNonQuery(sql,false);
                                        if (!success)
                                        {
                                            // Add tank if missing
                                            // TODO : Not sure if working
                                            if (!TankHelper.TankExists(tankId))
                                            {
                                                TankHelper.CreateUnknownTank(tankId, "Unknown");
                                                DB.ExecuteNonQuery(sql, false);
                                            }
                                        }
									}
									// Get killer info
									if (killerID > 0)
									{
										BattlePlayer killer = battlePlayers.Find(k => k.vehicleid == killerID);
										if (killer != null)
										{
											killedByAccountId = killer.accountDBID;
											killedByPlayerName = killer.name;
										}
									}
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
                                    "  maxBattleTier=@maxBattleTier " +
									"where id=@battleId;";
                                // Clan info
                                int maxClanCount = 0;
                                ClanInfo foundClan = new ClanInfo();
                                foreach (ClanInfo item in clanCount)
                                {
                                    if (item.count > maxClanCount)
                                    {
                                        maxClanCount = item.count;
                                        foundClan = item;
                                    }
                                }
								if (getEnemyClan && maxClanCount > 0)
                                {
                                    DB.AddWithValue(ref sql, "@enemyClanAbbrev", foundClan.clanAbbrev, DB.SqlDataType.VarChar);
                                    DB.AddWithValue(ref sql, "@enemyClanDBID", foundClan.clanDBID, DB.SqlDataType.Int);
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
                                // Max Battle Tier
                                int? maxBattleTier = GetMaxBattleTier(battleId);
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
								// Add Battle ID and run sql if any values
								DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
								DB.ExecuteNonQuery(sql);
								// If grinding, adjust grogress
								if (grindXP > 0)
									GrindingProgress(playerTankId, (int)token_personel.SelectToken("xp"));
                                // Update Tank Credits
                                TankCreditCalculation.RecalculateForTank(playerTankId);
								// Done
								deleteFileAfterRead = true;
								refreshAfterUpdate = true;
								GridView.scheduleGridRefresh = true;
								Log.AddToLogBuffer(" > > Done reading into DB JSON file: " + file);
								added++;
                                // Check for upload replay file to vBAddict
                                if (vBAddictHelper.Settings.UploadReplayActive)
                                {
                                    FileInfo fi = ReplayHelper.GetReplayFile(battleId);
                                    if (fi != null)
                                    {
                                        string replayFilename = fi.FullName;
                                        string msg = "";
                                        bool uploadOK = vBAddictHelper.UploadReplay(battleId, replayFilename, Config.Settings.playerName, Config.Settings.playerServer.ToLower(), vBAddictHelper.Settings.Token, out msg);
                                        if (uploadOK)
                                            Log.AddToLogBuffer(" > > Uploaded replay to vBAddict successfully: " + msg);
                                        else
                                        {
                                            Log.AddToLogBuffer(" > > Error uploading replay to vBAddict: " + msg);
                                        }
                                    }
                                }
							}
						}
						else
						{
							Log.AddToLogBuffer(" > > New battle file not read, battle do not exists for JSON file: " + file);
							// Battle do not exists, delete if old file file
							if (battleTime < DateTime.Now.AddHours(-3))
								Log.AddToLogBuffer(" > > Old battle found, schedule for delete");
							else
								deleteFileAfterRead = false; // keep file for a while, dossier file might be read later and then battle can be handled
						}
					}
					else
					{
						Log.AddToLogBuffer(" > > Battle file returned result: '" + result + "', could not process JSON file: " + file);
						var message = token_common.SelectToken("message"); // get message
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
                            fileBattleJson.CopyTo(Config.AppDataBattleResultSaved + fileBattleJson.FullName, true); // copy jason battle file to save folder
                        }
                        // Delete from battleresult folder
                        fileBattleJson.Delete();
						Log.AddToLogBuffer(" > > Deleted read or old JSON file: " + file);
					}
				}
				// Create alert file if new battle result added 
				if (refreshAfterUpdate && refreshGridOnFoundBattles)
				{
					GridView.scheduleGridRefresh = false;
					Log.BattleResultDoneLog();
				}
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
				Log.WriteLogBuffer();
			}
			catch (Exception ex)
			{
				Log.AddToLogBuffer(" > > Battle file analyze terminated for file: " + lastFile);
				Log.LogToFile(ex, "Battle result file analyze process terminated due to faulty file structure or content");
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
		}
		
		private static void GrindingProgress(int playerTankId, int XP)
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
			DataRow grinding = DB.FetchData(sql).Rows[0];
            // Get parameters for grinding calc
            GrindingHelper.Progress progress = new GrindingHelper.Progress();
            progress.ProgressXP = Convert.ToInt32(grinding["gProgressXP"]) + XP; // Added XP to previous progress
            progress.TargetXP = Convert.ToInt32(grinding["gGrindXP"]);
            progress.Battles = Convert.ToInt32(grinding["battles"]);
            progress.Wins = Convert.ToInt32(grinding["wins"]);
            progress.TotalXP = Convert.ToInt32(grinding["totalXP"]);
            progress.AvgXP = Convert.ToInt32(grinding["avgXP"]);
            // Set current progress
            progress.ProgressGoal = Convert.ToInt32(grinding["gProgressGoal"]);
            progress.CompleationDate = null;
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
			DB.ExecuteNonQuery(sql);
		}

		private static bool ConvertBattleUsingPython(string filename, out bool deleteFile)
		{
			bool ok = false;
			deleteFile = false;
			// Locate Python script
			string appPath = Path.GetDirectoryName(Application.ExecutablePath); // path to app dir
			string battle2jsonScript = appPath + "\\dossier2json\\wotbr2j.py"; // python-script for converting dossier file
			// Use IronPython
			PythonEngine.ipyOutput = ""; // clear ipy output
			try
			{
				//var ipy = Python.CreateRuntime();
				//dynamic ipyrun = ipy.UseFile(dossier2jsonScript);
				//ipyrun.main();

				if (!PythonEngine.InUse)
				{
					PythonEngine.InUse = true;
					Log.AddToLogBuffer(" > > Starting to converted battle DAT-file to JSON file: " + filename);
					var argv = new List();
					argv.Add(battle2jsonScript); // Have to add filename to run as first arg
					argv.Add(filename);
					argv.Add("-f");
					PythonEngine.Engine.GetSysModule().SetVariable("argv", argv);
					Microsoft.Scripting.Hosting.ScriptScope scope = PythonEngine.Engine.ExecuteFile(battle2jsonScript); // this is your python program
					dynamic result = scope.GetVariable("main")();
					//ScriptRuntimeSetup setup = new ScriptRuntimeSetup();
					//setup.DebugMode = true;
					//setup.LanguageSetups.Add(Python.CreateLanguageSetup(null));
					//ScriptRuntime runtime = new ScriptRuntime(setup);
					//ScriptEngine engine = runtime.GetEngineByTypeName(typeof(PythonContext).AssemblyQualifiedName);
					//ScriptSource script = engine.CreateScriptSourceFromFile(battle2jsonScript);
					//CompiledCode code = script.Compile();
					//ScriptScope scope = engine.CreateScope();
					//script.Execute(scope);
					Application.DoEvents();
					Log.AddToLogBuffer(" > > > Converted battle DAT-file to JSON file: " + filename);
					ok = true;
					deleteFile = true;
					PythonEngine.InUse = false;
				}
				else
				{
					Log.AddToLogBuffer(" > > IronPython Engine in use, not converted battle DAT-file to JSON file: " + filename);
					ok = true;
				}
			}
			catch (Exception ex)
			{
				Log.AddToLogBuffer(" > > IronPython exception thrown converted battle DAT-file to JSON file: " + filename);
				Log.LogToFile(ex, "ConvertBattleUsingPython exception running: " + battle2jsonScript + " with args: " + filename + " -f");
				// Cleanup
				deleteFile = true;
				PythonEngine.InUse = false;
			}
			Log.AddIpyToLogBuffer(PythonEngine.ipyOutput);
			Log.WriteLogBuffer();
			return ok;
		}

		private static void WaitUntilIronPythonReady(int maxWaitTime)
		{
			// Checks if IronPython is ready
			int waitInterval = 250; // time to wait in ms per read operation to check filesize
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			while (stopWatch.ElapsedMilliseconds < maxWaitTime && PythonEngine.InUse)
			{
				TimeSpan ts = stopWatch.Elapsed;
				Log.AddToLogBuffer(String.Format(" > > IronPython in use, waiting for process to finish (waited: {0:0000}ms)", stopWatch.ElapsedMilliseconds.ToString()));
				System.Threading.Thread.Sleep(waitInterval);
			}
			Log.AddToLogBuffer(String.Format(" > > IronPython ready to read battle files"));
			stopWatch.Stop();
		}

        private static int? GetMaxBattleTier(int battleId)
        {
            // TODO
            int? maxBattleTier = null;
            string sql =
                "select max(tank.tier) " +
                "from battlePlayer left join tank on battleplayer.tankid=tank.id " +
                "where battleid=" + battleId;
            DataTable dt = DB.FetchData(sql);
            if (dt.Rows.Count > 0)
                maxBattleTier = Convert.ToInt32(dt.Rows[0][0]);
            return maxBattleTier;
        }
	}
}
