using System;
using System.IO;
using Newtonsoft.Json;

namespace Common
{
	[Serializable()]
	public class ConfigData
	{
		public enum dbType
		{
			MSSQLserver = 1,
			SQLite = 2
		}

		public class PosSize
		{
			public int Top = 10;
			public int Left = 10;
			public int Width = 780;
			public int Height = 480;
		}

		public dbType  databaseType { get; set; }				// SQLite or MS SQL Server
		public string  databaseFileName { get; set; }			// SQLite Filename
		public string  databaseServer { get; set; }				// MSSQL Servername
		public bool    databaseWinAuth { get; set; }			// MSSQL Win (true) og SQL (false) authentication
		public string  databaseUid { get; set; }				// MSSQL Username (if SQL authentication)
		public string  databasePwd { get; set; }				// MSSQL Password (if SQL authentication)
		public string  databaseName { get; set; }				// MSSQL Databasename
		public int     playerId { get; set; }					// Player ID selected
		public string  playerName { get; set; }					// Player Name selected
		public string  dossierFilePath { get; set; }			// Dossier file path
		public int     dossierFileWathcherRun { get; set; }		// Dossier file listener activated
		public bool    grindParametersAutoStart { get; set; }	// Autoshow Grinding params on app startup
		public PosSize posSize { get; set; }					// Main Form Position And Size
		public int     timeZoneAdjust { get; set; }				// Adjust battle time read from dossier according to time zone
	}

	public class Config
	{
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

		public static string AppDataDBFolder
		{
			get
			{
				return AppDataBaseFolder + "Database\\";
			}
		}

		public static string AppDataAdminFolder
		{
			get
			{
				return AppDataBaseFolder + "Admin\\";
			}
		}


		private static ConfigData SetConfigDefaults()
		{
			ConfigData config = new ConfigData();
			// Insert default values as settings
			config.databaseType = ConfigData.dbType.SQLite;
			// Params for SQLite
			config.databaseFileName = "";
			// Params for MS SQL Server
			config.databaseServer = ".";
			config.databaseWinAuth = true;
			config.databaseUid = "";
			config.databasePwd = "";
			config.databaseName = "";
			// Common param
			config.playerId = 0;
			config.playerName = "";
			config.dossierFilePath = "";
			config.dossierFileWathcherRun = 0;
			config.grindParametersAutoStart = false;
			config.posSize = new ConfigData.PosSize();
			config.timeZoneAdjust = 0;
			// done
			return config;
		}

		
		public static bool SaveConfig(ConfigData config, string configfile, out string msg)
		{
			bool ok = true;
			string returnMsg = "Application settings succsessfully saved.";
			// Write new settings to Json
			try
			{
				string json = JsonConvert.SerializeObject(config);
				File.WriteAllText(Config.AppDataBaseFolder + configfile, json);
			}
			catch (Exception ex)
			{
				returnMsg = "Error occured saving application settings to config file" + Environment.NewLine + Environment.NewLine + ex.Message;
			}
			msg = returnMsg;
			return ok;
		}

		public static bool GetConfig(ref ConfigData config, string configfile, out string msg)
		{
			bool ok = true;
			string returMsg = "Application settings succsessfully read.";
			// Does config file exist?
			if (!File.Exists(Config.AppDataBaseFolder + configfile))
			{
				SetConfigDefaults();
				returMsg = "Config file is missing, please configure application settings.";
				ok = false;
			}
			else
			{
				// Read from json config file
				try
				{
					ConfigData conf = new ConfigData();
					string json = File.ReadAllText(Config.AppDataBaseFolder + configfile);
					conf = JsonConvert.DeserializeObject<ConfigData>(json);
					config = conf;
				}
				catch (Exception ex)
				{
					File.Delete(Config.AppDataBaseFolder + configfile);
					SetConfigDefaults();
					returMsg = "Error reading config file, please configure application settings." + Environment.NewLine + Environment.NewLine + ex.Message;
					ok = false;
				}
			}
			msg = returMsg;
			return ok;
		}

		
		// Create standard dbconnection string based on standard config settings
		public static string DatabaseConnection(ConfigData config) 
		{
			int connectionTimeot = 10;
			string dbcon = "";
			if (config.databaseType == ConfigData.dbType.MSSQLserver)
			{
				// Calc win/sql auth login and user/pw settings
				string integratedSecurity = "True";
				string userLogin = "";
				if (!config.databaseWinAuth)
				{
					integratedSecurity = "False";
					string uid = config.databaseUid;
					string pwd = config.databasePwd;
					userLogin = "User Id=" + uid + ";Password=" + pwd + ";";
				}
				// Create conn str now
				dbcon = "Data Source=" + config.databaseServer +
						";Initial Catalog=" + config.databaseName +
						";Integrated Security=" + integratedSecurity + ";" +
						userLogin +
						"; Connect Timeout=" + connectionTimeot.ToString();
			}
			else if (config.databaseType == ConfigData.dbType.SQLite)
			{
				dbcon = "Data Source=" + config.databaseFileName + ";Version=3;PRAGMA foreign_keys = ON;";
			}
			return dbcon;
		}

		// Create alternative dbconnection string overriding standard config settings
		public static string DatabaseConnection(ConfigData config,
												ConfigData.dbType dbType,
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
				string databaseServer = config.databaseServer;
				if (databaseServerOverride != "") databaseServer = databaseServerOverride;
				// Get databasename
				string databaseName = config.databaseName;
				if (databaseNameOverride != "") databaseName = databaseNameOverride;
				// Get authentication type
				string integratedSecurity = "True";
				bool winAuth = config.databaseWinAuth;
				if (databaseWinOrSql == "Win") winAuth = true;
				if (databaseWinOrSql == "Sql") winAuth = false;
				// Get user name and password for login when sql authentication
				string userLogin = "";
				if (!winAuth)
				{
					integratedSecurity = "False";
					string uid = config.databaseUid;
					string pwd = config.databasePwd;
					if (databaseUidOverride != "") uid = databaseUidOverride;
					if (databasePwdOverride != "") pwd = databasePwdOverride;
					userLogin = "User Id=" + uid + ";Password=" + pwd + ";";
				}
				dbcon = "Data Source=" + databaseServer + ";Initial Catalog=" + databaseName + ";Integrated Security=" + integratedSecurity + ";" + userLogin + "; Connect Timeout=" + connectionTimeot.ToString();
			}
			else if (dbType == ConfigData.dbType.SQLite)
			{
				string databaseFileName = config.databaseFileName;
				if (databaseFileOverride != "") databaseFileName = databaseFileOverride + ".db";
				dbcon = "Data Source=" + databaseFileName + ";Version=3;";
			}
			return dbcon;
		}
	}
}
