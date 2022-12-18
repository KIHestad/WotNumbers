using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Newtonsoft.Json;
using WinApp.Code.FormLayout;

namespace WinApp.Code
{
	// Save in config.json settings file
	[Serializable()]
	public class ConfigData
	{
		public enum dbType
		{
			MSSQLserver = 1,
			SQLite = 2
		}

		public enum WoTGameStartType
		{
			None = 0,
			Launcher = 1,
			Game = 2,
			NotConfigured = 3,
		}

		public class PosSize
		{
			public int Top = 10;
			public int Left = 10;
			public int Width = 1300;
			public int Height = 690;
			public FormWindowState WindowState = FormWindowState.Normal;
		}

		public class CustomBattleTimeFilter
		{
			public DateTime? from = null;
			public DateTime? to = null;
		}

		public class Result
		{
			public bool Success { get; set; }
			public string Message { get; set; }
		}
		public enum BattleViewMode
		{
			Old = 0, // Show deduced battles / don't show orphan battles
			New = 1 // Show orphan battles / don't show deduced battles
		}

		public BattleViewMode battleViewMode { get; set; }                // SQLite or MS SQL Server
		public dbType databaseType { get; set; }                // SQLite or MS SQL Server
		public string databaseFileName { get; set; }            // SQLite Filename
		public string databaseServer { get; set; }              // MSSQL Servername
		public bool databaseWinAuth { get; set; }           // MSSQL Win (true) og SQL (false) authentication
		public string databaseUid { get; set; }             // MSSQL Username (if SQL authentication)
		public string databasePwd { get; set; }             // MSSQL Password (if SQL authentication)
		public string databaseName { get; set; }                // MSSQL Databasename
		public int playerId { get; set; }                   // Player ID selected
		public uint playerAccountId { get; set; }             // Player Account Id
		public string playerName { get; set; }                  // Player Name selected
		public string playerServer { get; set; }                // Player Server selected
		public string playerNameAndServer                       // Construct playername with servername as name used in player-table as unique playername
		{
			get
			{
				if (playerServer == "")
					return playerName;
				else
					return playerName + " (" + playerServer + ")";
			}
			set
			{
				if (value.Contains(" ("))
				{
					// Set playerIdName as substring from playerName - the part in front of " (SERVER)"
					playerName = value.Substring(0, value.IndexOf(" ("));
					// Set playerIdServer as substring from playerName - the " (SERVER)" part removinc spave and pharanthesis
					string server = value.Substring(playerName.Length + 2);
					playerServer = server.Substring(0, server.Length - 1);
				}
				else
				{
					playerName = value;
					playerServer = "";
				}
			}
		}
		public string dossierFilePath { get; set; }         // Dossier file path
		public string battleFilePath
		{
			get
			{
				if (Directory.Exists(dossierFilePath))
				{
					DirectoryInfo di = Directory.GetParent(dossierFilePath);
					if (di.Exists)
						return di.FullName + "\\battle_results\\";
					else
						return "";
				}
				else
					return "";
			}
		}
		public int dossierFileWathcherRun { get; set; }     // Dossier file listener activated
		public PosSize posSize { get; set; }                        // Main Form Position And Size
		public int timeZoneAdjust { get; set; }             // Adjust battle time read from dossier according to time zone
		public bool showDBErrors { get; set; }                  // To show all DB errors  
		public DateTime readMessage { get; set; }                   // Timestamp for last message read from Wot Numbers API
		public DateTime doneRunWotApi { get; set; }                 // done executed force run wot api triggered from Wot Numbers API
		public DateTime doneRunForceDossierFileCheck { get; set; }  // done executed force run full force dossier file check triggered from Wot Numbers API
		public int gridFontSize { get; set; }                   // Grid font size
		public bool gridBattlesTotalsTop { get; set; }          // false = totals as footer, true = totals frozen at top
		public bool homeViewNewLayout { get; set; }             // Experimental mode for home view
		public int mainGridTankRowWidht { get; set; }               // Width for row header in main grid
		public int mainGridBattleRowWidht { get; set; }             // Width for row header in main grid
		public bool useSmallMasteryBadgeIcons { get; set; }         // Flag for small MB icons
		public WoTGameStartType wotGameStartType { get; set; }      // Param for start WoT from Wot Numbers
		public string wotGameFolder { get; set; }
		public long wotGameAffinity { get; set; }
		public bool wotGameAutoStart { get; set; }
		public string wotGameRunBatchFile { get; set; }
		public bool notifyIconUse { get; set; }                     // Notify icon settings - use notify icon insted of taskbar icon
		public bool notifyIconFormExitToMinimize { get; set; }      // Override exit from form to minimize
		public CustomBattleTimeFilter customBattleTimeFilter { get; set; } // Remember last used custom battle time filter
		public bool vBAddictUploadActive { get; set; }              // Activate auto dossier and battle upload to vBAddict
		public string vBAddictPlayerToken { get; set; }             // player token
		public bool CheckForBrrOnStartup { get; set; }              // Perform check for BRR on startup
		public bool vBAddictUploadReplayActive { get; set; }        // Activate auto replay upload to vBAddict
		public bool vBAddictShowToolBarMenu { get; set; }           // Activate auto replay upload to vBAddict
		public ColorRangeScheme.RatingColorScheme RatingColors { get; set; }    // Rating Color Scheme
		public string downloadFilePath { get; set; }                // File path for downloading new versions
		public bool downloadFilePathAddSubfolder { get; set; }      // Flag for creating subfolder with version number when downloading
		public int newDayAtHour { get; set; }                       // Hour when new day starts, normally at server time reset: 07:00
		public string databaseBackupFilePath { get; set; }          // File path for database backup
		public DateTime? databaseBackupLastPerformed { get; set; }  // Last database backup runned
		public int databaseBackupPeriod { get; set; }               // Number of days between auto database backup
		public bool tankSearchMainModeAdvanced { get; set; }        // Selected tank search main mode, true=advanced, fale=simple
		public DateTime lastGrindingProgressRecalc { get; set; }    // When latest grinding progress recalc was run
		public string currentHomeView { get; set; }                 // The last used home view menu name, to show on restart
		public string currentChartFavourite { get; set; }           // The last used chart favourite, to show on restart
		public string res_mods_subfolder { get; set; }              // Current res_mods folder
	}

	// Settings for session, resets on each startup
	public class ConfigSessionData
	{
		public bool tempBattleResultSave { get; set; }              // Save battle result files to %appdata%/BattleResultSaved, temp setting not in config file
		public bool tempBattleResultSaveFirstRun { get; set; }      // Force first battle fetch after tempBattleResultSave = true to fetch all battle results available
	}

	public class Config
	{
		// Session settings, reset for each startup
		public static ConfigSessionData SessionSettings = new ConfigSessionData()
		{
			tempBattleResultSave = false,
			tempBattleResultSaveFirstRun = true
		};

		// Settings to be saved to config.json
		private const string configfile = "config.json";        // File to load/save config changes
		public static ConfigData Settings = new ConfigData();               // Current configs
		private static bool _appDataFolderOK = false;

		public static string AppDataBaseFolder
		{
			get
			{
				string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // = %appdata% 
				string wotnumFolder = "\\Wot Numbers";
				if (!_appDataFolderOK)
				{
					if (!Directory.Exists(appdataFolder))
					{
						Directory.CreateDirectory(appdataFolder);
					}
					if (!Directory.Exists(appdataFolder + wotnumFolder))
					{
						Directory.CreateDirectory(appdataFolder + wotnumFolder);
					}
					if (!Directory.Exists(appdataFolder + wotnumFolder + "\\Log"))
					{
						Directory.CreateDirectory(appdataFolder + wotnumFolder + "\\Log");
					}
					if (!Directory.Exists(appdataFolder + wotnumFolder + "\\HomeView"))
					{
						Directory.CreateDirectory(appdataFolder + wotnumFolder + "\\HomeView");
					}
					if (!Directory.Exists(appdataFolder + wotnumFolder + "\\BattleResult"))
					{
						Directory.CreateDirectory(appdataFolder + wotnumFolder + "\\BattleResult");
					}
					if (!Directory.Exists(appdataFolder + wotnumFolder + "\\BattleResultSaved"))
					{
						Directory.CreateDirectory(appdataFolder + wotnumFolder + "\\BattleResultSaved");
					}
					if (!Directory.Exists(appdataFolder + wotnumFolder + "\\Download"))
					{
						Directory.CreateDirectory(appdataFolder + wotnumFolder + "\\Download");
					}
					if (!Directory.Exists(appdataFolder + wotnumFolder + "\\BattleResultToUpload"))
					{
						Directory.CreateDirectory(appdataFolder + wotnumFolder + "\\BattleResultToUpload");
					}
					_appDataFolderOK = true;
				}
				return appdataFolder + wotnumFolder + "\\";
			}
		}

		public static string AppDataLogFolder
		{
			get
			{
				return AppDataBaseFolder + "Log\\";
			}
		}

		public static string AppDataHomeViewFolder
		{
			get
			{
				return AppDataBaseFolder + "HomeView\\";
			}
		}

		public static string AppDataDBFolder
		{
			get
			{
				return AppDataBaseFolder + "Database\\";
			}
		}

		public static string AppDataBattleResultFolder
		{
			get
			{
				return AppDataBaseFolder + "BattleResult\\";
			}
		}

		public static string AppDataBattleResultToUpload
		{
			get
			{
				return AppDataBaseFolder + "BattleResultToUpload\\";
			}
		}

		public static string AppDataBattleResultSaved
		{
			get
			{
				return AppDataBaseFolder + "BattleResultSaved\\";
			}
		}

		public static string AppDataDownloadFolder
		{
			get
			{
				return AppDataBaseFolder + "Download\\";
			}
		}

		private static void SetConfigDefaults()
		{
			// Insert default values as settings
			Config.Settings.databaseType = ConfigData.dbType.SQLite;
			// Params for SQLite
			Config.Settings.databaseFileName = "";
			// Params for MS SQL Server
			Config.Settings.databaseServer = ".";
			Config.Settings.databaseWinAuth = true;
			Config.Settings.databaseUid = "";
			Config.Settings.databasePwd = "";
			Config.Settings.databaseName = "";
			// Common param
			Config.Settings.playerId = 0;
			Config.Settings.playerAccountId = 0;
			Config.Settings.playerName = "";
			Config.Settings.playerServer = "";
			Config.Settings.dossierFilePath = "";
			Config.Settings.dossierFileWathcherRun = 0;
			Config.Settings.showDBErrors = false;
			Config.Settings.posSize = new ConfigData.PosSize();
			Config.Settings.readMessage = new DateTime(2014, 8, 1);
			Config.Settings.doneRunForceDossierFileCheck = new DateTime(2014, 8, 1);
			Config.Settings.doneRunWotApi = new DateTime(2014, 8, 1);
			Config.Settings.gridFontSize = 8;
			Config.Settings.gridBattlesTotalsTop = false;
			Config.Settings.homeViewNewLayout = true;
			Config.Settings.mainGridBattleRowWidht = 24;
			Config.Settings.mainGridTankRowWidht = 24;
			Config.Settings.useSmallMasteryBadgeIcons = true;
			Config.Settings.wotGameAffinity = 0;
			Config.Settings.wotGameFolder = "";
			Config.Settings.wotGameStartType = ConfigData.WoTGameStartType.NotConfigured;
			Config.Settings.wotGameAutoStart = false;
			Config.Settings.wotGameRunBatchFile = "";
			Config.Settings.notifyIconUse = false;
			Config.Settings.notifyIconFormExitToMinimize = false;
			// custom battle filter
			Config.Settings.customBattleTimeFilter = new ConfigData.CustomBattleTimeFilter();
			// vBAddict
			Config.Settings.vBAddictUploadActive = false;
			Config.Settings.vBAddictUploadReplayActive = false;
			Config.Settings.vBAddictPlayerToken = "";
			Config.Settings.vBAddictUploadReplayActive = false;
			Config.Settings.vBAddictShowToolBarMenu = false;
			// Database Backup settings
			Config.Settings.databaseBackupFilePath = "";
			Config.Settings.databaseBackupLastPerformed = null;
			Config.Settings.databaseBackupPeriod = 0;
			// Others
			Config.Settings.CheckForBrrOnStartup = true;
			Config.Settings.RatingColors = ColorRangeScheme.RatingColorScheme.WN_Official_Colors;
			Config.Settings.downloadFilePath = Config.AppDataDownloadFolder;
			Config.Settings.downloadFilePathAddSubfolder = false;
			Config.Settings.newDayAtHour = 7;
			Config.Settings.tankSearchMainModeAdvanced = true;
			Config.Settings.lastGrindingProgressRecalc = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day);
			Config.Settings.currentHomeView = "Default";
			Config.Settings.currentChartFavourite = "";
			Config.Settings.res_mods_subfolder = "";
			Config.Settings.battleViewMode = ConfigData.BattleViewMode.Old;
		}


		public async static Task<ConfigData.Result> SaveConfig()
		{
			ConfigData.Result result = new ConfigData.Result()
			{
				Success = true,
				Message = "Application settings successfully saved."
			};
			// Write new settings to Json
			try
			{
				string json = JsonConvert.SerializeObject(Config.Settings);
				File.WriteAllText(Config.AppDataBaseFolder + configfile, json);
			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex);
				result.Success = false;
				result.Message = "Error occured saving application settings to config file" + Environment.NewLine + Environment.NewLine + ex.Message;
			}
			return result;
		}

		public async static Task<ConfigData.Result> GetConfig()
		{
			ConfigData.Result result = new ConfigData.Result()
			{
				Success = true,
				Message = "Application settings successfully read."
			};
			// Does config file exist?
			if (!File.Exists(Config.AppDataBaseFolder + configfile))
			{
				SetConfigDefaults();
				result.Message = "Config file is missing, please configure application settings.";
				result.Success = false;
			}
			else
			{
				// Read from json config file
				try
				{
					ConfigData conf = new ConfigData();
					string json = File.ReadAllText(Config.AppDataBaseFolder + configfile);
					conf = JsonConvert.DeserializeObject<ConfigData>(json);
					Config.Settings = conf;
				}
				catch (Exception ex)
				{
					await Log.LogToFile(ex);
					File.Delete(Config.AppDataBaseFolder + configfile);
					SetConfigDefaults();
					result.Message = "Error reading config file, please configure application settings." + Environment.NewLine + Environment.NewLine + ex.Message;
					result.Success = false;
				}
			}
			return result;
		}


		// Create standard dbconnection string based on standard config settings
		public static string DatabaseConnection()
		{
			int connectionTimeot = 10;
			string dbcon = "";
			if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
			{
				// Calc win/sql auth login and user/pw settings
				string integratedSecurity = "True";
				string userLogin = "";
				if (!Config.Settings.databaseWinAuth)
				{
					integratedSecurity = "False";
					string uid = Config.Settings.databaseUid;
					string pwd = Config.Settings.databasePwd;
					userLogin = "User Id=" + uid + ";Password=" + pwd + ";";
				}
				// Create conn str now
				dbcon = "Data Source=" + Config.Settings.databaseServer +
						";Initial Catalog=" + Config.Settings.databaseName +
						";Integrated Security=" + integratedSecurity + ";" +
						userLogin +
						"; Connect Timeout=" + connectionTimeot.ToString();
			}
			else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
			{
				dbcon = "Data Source=" + Config.Settings.databaseFileName + ";Version=3;PRAGMA foreign_keys = ON;";
			}
			return dbcon;
		}

		// Create alternative dbconnection string overriding standard config settings
		public static string DatabaseConnection(ConfigData.dbType dbType,
												string databaseFileOverride = "", // SQLite databasefile override
												string databaseServerOverride = "", // MS SQL Server override
												string databaseNameOverride = "",
												string databaseWinOrSql = "",
												string databaseUidOverride = "",
												string databasePwdOverride = "",
												int connectionTimeot = 10)
		{
			string dbcon = "";
			if (dbType == ConfigData.dbType.MSSQLserver)
			{
				// Get databaseserver
				string databaseServer = Config.Settings.databaseServer;
				if (databaseServerOverride != "") databaseServer = databaseServerOverride;
				// Get databasename
				string databaseName = Config.Settings.databaseName;
				if (databaseNameOverride != "") databaseName = databaseNameOverride;
				// Get authentication type
				string integratedSecurity = "True";
				bool winAuth = Config.Settings.databaseWinAuth;
				if (databaseWinOrSql == "Win") winAuth = true;
				if (databaseWinOrSql == "Sql") winAuth = false;
				// Get user name and password for login when sql authentication
				string userLogin = "";
				if (!winAuth)
				{
					integratedSecurity = "False";
					string uid = Config.Settings.databaseUid;
					string pwd = Config.Settings.databasePwd;
					if (databaseUidOverride != "") uid = databaseUidOverride;
					if (databasePwdOverride != "") pwd = databasePwdOverride;
					userLogin = "User Id=" + uid + ";Password=" + pwd + ";";
				}
				dbcon = "Data Source=" + databaseServer + ";Initial Catalog=" + databaseName + ";Integrated Security=" + integratedSecurity + ";" + userLogin + "; Connect Timeout=" + connectionTimeot.ToString();
			}
			else if (dbType == ConfigData.dbType.SQLite)
			{
				string databaseFileName = Config.Settings.databaseFileName;
				if (databaseFileOverride != "") databaseFileName = databaseFileOverride + ".db";
				dbcon = "Data Source=" + databaseFileName + ";Version=3;";
			}
			return dbcon;
		}
	}
}
