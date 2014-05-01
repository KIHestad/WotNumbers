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

namespace WotDBUpdater.Code
{
	[Serializable()]
	public class ConfigData
	{
		public enum dbType
		{
			MSSQLserver = 1,
			SQLite = 2
		}

		public dbType databaseType { get; set; }			// SQLite or MS SQL Server
		public string databaseFileName { get; set; }		// SQLite Filename
		public string databaseServer { get; set; }			// MSSQL Servername
		public bool   databaseWinAuth { get; set; }			// MSSQL Win (true) og SQL (false) authentication
		public string databaseUid { get; set; }				// MSSQL Username (if SQL authentication)
		public string databasePwd { get; set; }				// MSSQL Password (if SQL authentication)
		public string databaseName { get; set; }			// MSSQL Databasename
		public int    playerId { get; set; }				// Player ID selected
		public string playerName { get; set; }				// Player Name selected
		public string dossierFilePath { get; set; }			// Dossier file path
		public int    dossierFileWathcherRun { get; set; }	// Dossier file listener activated
	}

	class Config
	{
		public static ConfigData Settings = new ConfigData();				// Current configs
		public static ConfigData LastWorkingSettings = new ConfigData();	// Used for reverting to last working settings if create db fails
		
		private const string configfile = "config.json";		// File to load/save config changes

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
			Config.Settings.playerName = "";
			Config.Settings.dossierFilePath = "";
			Config.Settings.dossierFileWathcherRun = 0;
		}

		
		public static bool SaveConfig(out string msg)
		{
			bool ok = true;
			string returnMsg = "Application settings succsessfully saved.";
			// Write new settings to Json
			try
			{
				string json = JsonConvert.SerializeObject(Config.Settings);
				File.WriteAllText(configfile, json);
			}
			catch (Exception ex)
			{
				returnMsg = "Error occured saving application settings to config file" + Environment.NewLine + Environment.NewLine + ex.Message;
			}
			msg = returnMsg;
			return ok;
		}

		public static bool GetConfig(out string msg)
		{
			bool ok = true;
			string returMsg = "Application settings succsessfully read.";
			// Does config file exist?
			if (!File.Exists(configfile))
			{
				SetConfigDefaults();
				returMsg = "Config file is missing, please configure application settings.";
				ok = false;
			}
			else
			{
				// Read from XML
				try
				{
					ConfigData conf = new ConfigData();
					string json = File.ReadAllText(configfile);
					conf = JsonConvert.DeserializeObject<ConfigData>(json);
					Config.Settings = conf;
				}
				catch (Exception ex)
				{
					File.Delete(configfile);
					SetConfigDefaults();
					returMsg = "Error reading config file, please configure application settings." + Environment.NewLine + Environment.NewLine + ex.Message;
					ok = false;
				}
			}
			msg = returMsg;
			return ok;
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
