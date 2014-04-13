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
	// The data class containing two properties 
	[Serializable()]
	public class ConfigData
	{
		public enum dbType
		{
			MSSQLserver = 1,
			SQLite = 2
		}

		public dbType databaseType { get; set; }
		public string databaseFileName { get; set; }
		public string databaseServer { get; set; }
		public bool databaseWinAuth { get; set; }
		public string databaseUid { get; set; }
		public string databasePwd { get; set; }
		public string databaseName { get; set; }
		public int playerId { get; set; }
		public string playerName { get; set; }
		public string dossierFilePath { get; set; }
		public int run { get; set; }
	}

	class Config
	{
		public static ConfigData Settings = new ConfigData();
		
		private const string configfile = "config.json";

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
			Config.Settings.run = 0;
		}

		public static string DatabaseConnection(string databaseServerOverride = "", 
												string databaseNameOverride = "", 
												string databaseWinOrSql = "", 
												string databaseUidOverride = "", 
												string databasePwdOverride = "", 
												int connectionTimeot = 10, 
												bool databasetypeOverride = false, 
												ConfigData.dbType databaseType = ConfigData.dbType.MSSQLserver)
		{
			string dbcon = "";
			// Get database type
			ConfigData.dbType dbType = Config.Settings.databaseType;
			if (databasetypeOverride) dbType = databaseType;
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
				if (databaseNameOverride != "") databaseFileName = databaseNameOverride + ".db";
				dbcon = "Data Source=" + databaseFileName + ";Version=3;";
			}
			return dbcon;
		}

		public static bool CheckDBConn(bool showErrorIfNotExists = true, string databaseServerOverride = "", string databaseNameOverride = "", string databaseWinOrSql = "", string databaseUidOverride = "", string databasePwdOverride = "")
		{
			bool ok = false;
			// Get database type
			if (Config.Settings.databaseType ==ConfigData.dbType.MSSQLserver)
			{
				// get databasename
				string databaseName = "";
				if (Config.Settings.databaseName != null) databaseName = Config.Settings.databaseName;
				if (databaseNameOverride != "") databaseName = databaseNameOverride;
				// Check data
				if (Config.Settings.databaseServer == null || Config.Settings.databaseServer == "" || databaseName == "")
				{
					Code.MsgBox.Show("Missing database server and/or database name, check Database Settings.", "Config error");
				}
				else
				{
					try
					{
						SqlConnection con = new SqlConnection(Config.DatabaseConnection(databaseServerOverride, databaseNameOverride, databaseWinOrSql, databaseUidOverride, databasePwdOverride));
						con.Open();
						ok = true;
						con.Close();
					}
					catch (Exception ex)
					{
						if (showErrorIfNotExists) Code.MsgBox.Show("Error connectin to database, check Database Settings.\n\n" + ex.Message, "Config error");
					}
				}
			}
			else if (Config.Settings.databaseType ==ConfigData.dbType.SQLite)
			{
				ok = File.Exists(Config.Settings.databaseFileName);
				if (!ok) Code.MsgBox.Show("No SQLite databasefile found", "Config error");
			}
			return ok;
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

	}
}
