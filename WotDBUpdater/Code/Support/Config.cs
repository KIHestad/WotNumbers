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

namespace WotDBUpdater
{
	// The data class containing two properties 
	[Serializable()]
	public class ConfigData
	{
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
		
		private const string configfile = "WotDBUpdaterConfig.xml";

		private static void SetConfigDefaults()
		{
			// Insert default values as settings
			Config.Settings.databaseServer = ".";
			Config.Settings.databaseWinAuth = true;
			Config.Settings.databaseUid = "";
			Config.Settings.databasePwd = "";
			Config.Settings.databaseName = "";
			Config.Settings.playerId = 0;
			Config.Settings.playerName = "";
			Config.Settings.dossierFilePath = "";
			Config.Settings.run = 0;
		}

		public static string DatabaseConnection(string databaseServerOverride = "", string databaseNameOverride = "", string databaseWinOrSql = "", string databaseUidOverride = "", string databasePwdOverride = "", int connectionTimeot = 30)
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

			return "Data Source=" + databaseServer + ";Initial Catalog=" + databaseName + ";Integrated Security=" + integratedSecurity + ";" + userLogin + "; Connect Timeout=" + connectionTimeot.ToString();
		}

		public static bool CheckDBConn(bool showErrorIfNotExists = true, string databaseServerOverride = "", string databaseNameOverride = "", string databaseWinOrSql = "", string databaseUidOverride = "", string databasePwdOverride = "")
		{
			bool ok = false;
			// get databasename
			string databaseName = "";
			if (Config.Settings.databaseName != null) databaseName = Config.Settings.databaseName;
			if (databaseNameOverride != "") databaseName = databaseNameOverride;
			// Check data
			if (Config.Settings.databaseServer == null || Config.Settings.databaseServer == "" || databaseName == "")
			{
				Code.Support.MessageDark.Show("Missing database server and/or database name, check Database Settings.", "Config error");
			}
			else
			{
				try
				{
					SqlConnection con = new SqlConnection(Config.DatabaseConnection(databaseServerOverride, databaseNameOverride,databaseWinOrSql,databaseUidOverride,databasePwdOverride));
					con.Open();
					ok = true;
					con.Close();
				}
				catch (Exception ex)
				{
					if (showErrorIfNotExists) Code.Support.MessageDark.Show("Error connectin to database, check Database Settings.\n\n" + ex.Message, "Config error");
				}
			}
			return ok;
		}

		public static bool SaveDbConfig(out string msg)
		{
			bool dbOk = false;
			string returnMsg = "Database settings succsessfully saved. ";
			try
			{
				// Check if database and player exsists
				SqlConnection con = new SqlConnection(Config.DatabaseConnection());
				con.Open();
				dbOk = true; // if database og databaseserver not exsists exeption is thrown by now
				// Check if player exist by lookup name
				SqlCommand cmd = new SqlCommand("SELECT * FROM player WHERE name=@name", con);
				cmd.Parameters.AddWithValue("@name", Config.Settings.playerName);
				SqlDataReader reader = cmd.ExecuteReader();
				if (!reader.HasRows)
				{
					returnMsg = "Selected Player does not exist in database, please check your Application Settings.";
					Config.Settings.playerId = 0;
					Config.Settings.playerName = "";
				}
				else
				{
					// Get player ID
					while (reader.Read())
					{
						Config.Settings.playerId = Convert.ToInt32(reader["id"]);
					}
				}
				reader.Close();
				con.Close();
				
				// Write new settings to XML
				XmlSerializer writer = new XmlSerializer(typeof(ConfigData));
				using (FileStream file = File.OpenWrite(configfile))
				{
					writer.Serialize(file, Config.Settings);
				}
			}
			catch (Exception ex)
			{
				returnMsg = "Error occured saving database settings: " + ex.Message;
			}
			msg = returnMsg;
			return dbOk;
		}

		public static bool SaveAppConfig(out string Msg)
		{
			bool appOk = false;
			string returnMsg = "Application settings succsessfully saved";
			try
			{
				// Check if database and player exsists
				SqlConnection con = new SqlConnection(Config.DatabaseConnection());
				con.Open();
				appOk = true; // if database og databaseserver not exsists exeption is thrown by now
				// Check if player exist by lookup name
				SqlCommand cmd = new SqlCommand("SELECT * FROM player WHERE name=@name", con);
				cmd.Parameters.AddWithValue("@name", Config.Settings.playerName);
				SqlDataReader reader = cmd.ExecuteReader();
				if (!reader.HasRows)
				{
					// Add player to DB
					SqlConnection con2 = new SqlConnection(Config.DatabaseConnection());
					con2.Open();
					SqlCommand cmd2 = new SqlCommand("INSERT INTO player (name) VALUES (@name)", con2);
					cmd2.Parameters.AddWithValue("@name", Config.Settings.playerName);
					cmd2.ExecuteNonQuery();
					// Get the last player id
					int playerId = 0;
					cmd2 = new SqlCommand("select max(id) as playerId from player", con);
					SqlDataReader myReader = cmd.ExecuteReader();
					while (myReader.Read())
					{
						playerId = Convert.ToInt32(myReader["playerId"]);
					}
					myReader.Close();
					// Add new values to config
					Config.Settings.playerId = playerId;
					Config.Settings.playerName = Config.Settings.playerName;
					returnMsg = "New player: '" + Config.Settings.playerName + "' added to database, application settings succsessfully saved.";
					con2.Close();
				}
				else
				{
					// Get player ID
					while (reader.Read())
					{
						Config.Settings.playerId = Convert.ToInt32(reader["id"]);
					}
				}
				reader.Close();
				con.Close();
				// Write new settings to XML
				XmlSerializer writer = new XmlSerializer(typeof(ConfigData));
				using (FileStream file = File.OpenWrite(configfile))
				{
					writer.Serialize(file, Config.Settings);
				}
			}
			catch (Exception ex)
			{
				returnMsg = "Error occured saving application settings: " + ex.Message;
			}
			Msg = returnMsg;
			return appOk;
		}


		public static string GetConfig()
		{
			string msg = "";
			// Does config file exist?
			if (!File.Exists(configfile))
			{
				SetConfigDefaults();
				msg = "Config file is missing, setting default values. Please check Database and Application settings.";
			}
			else
			{
				// Read from XML
				try
				{
					Config.Settings = LoadConfig();
				}
				catch (Exception ex)
				{
					File.Delete(configfile);
					SetConfigDefaults();
					msg = "Error reading config file, might be corrupted. The config file is now deleted. Please check Database and Application settings.\n\n" + ex.Message;
				}
			}
			return msg;
		}

		private static ConfigData LoadConfig()
		{
			ConfigData conf = new ConfigData();
			XmlSerializer reader = new XmlSerializer(typeof(ConfigData));
			using (FileStream input = File.OpenRead(configfile))
			{
				conf = reader.Deserialize(input) as ConfigData;
			}
			return conf;
		}
	}
}
