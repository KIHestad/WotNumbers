using System;
using System.Collections.Generic;
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
        public string DatabaseConn { get; set; }
        public int playerID { get; set; }
        public string playerName { get; set; }
        public string DossierFilePath { get; set; }
        public int Run { get; set; }
    }

    class Config
    {
        public static ConfigData Settings = new ConfigData();
        
        private const string configfile = "WotDBUpdaterConfig.xml";

        private static void SetConfigDefaults(string message)
        {
            // Insert default values as settings
            Config.Settings.DatabaseConn = "Data Source=.;Initial Catalog=Databasename;Integrated Security=True;";
            Config.Settings.playerID = 0;
            Config.Settings.playerName = "";
            Config.Settings.DossierFilePath = "";
            Config.Settings.Run = 0;
            // Message
            MessageBox.Show(message, "Config error");
        }

        public static bool CheckDBConn()
        {
            bool ok = false;
            try
            {
                SqlConnection con = new SqlConnection(Config.Settings.DatabaseConn);
                con.Open();
                ok = true;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connectin to database, check Database Settings.\n\n" + ex.Message, "Config error");
            }
            return ok;
        }

        public static bool SaveConfig(bool CheckDBSetting = false, bool LookupPlayerInDB = false)
        {
            bool DBok = true;
            if (CheckDBSetting) DBok = CheckDBConn();

            if (DBok)
            {
                if (LookupPlayerInDB)
                {
                    try
                    {
                        // Check if player exist in database, if not create
                        SqlConnection con = new SqlConnection(Config.Settings.DatabaseConn);
                        con.Open();
                        Config.Settings.playerName = Config.Settings.playerName.Trim();
                        // Check if player exist
                        bool createnewplayer = false;
                        SqlCommand cmd = new SqlCommand("SELECT * FROM player WHERE name=@name", con);
                        cmd.Parameters.AddWithValue("@name", Config.Settings.playerName);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (!reader.HasRows) createnewplayer = true;
                        reader.Close();
                        if (createnewplayer)
                        {
                            // create new player
                            cmd = new SqlCommand("INSERT INTO player (name) VALUES (@name)", con);
                            cmd.Parameters.AddWithValue("@name", Config.Settings.playerName);
                            cmd.ExecuteNonQuery();
                        }
                        // Get player ID
                        cmd = new SqlCommand("SELECT * FROM player WHERE name=@name", con);
                        cmd.Parameters.AddWithValue("@name", Config.Settings.playerName);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Config.Settings.playerID = Convert.ToInt32(reader["id"]);
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error occured lookup player in database, check your Application Settings.\n\n" + ex.Message, "Config error");
                        Config.Settings.playerID = 0;
                    }
                }
                // Write new settings to XML
                XmlSerializer writer = new XmlSerializer(typeof(ConfigData));
                using (FileStream file = File.OpenWrite(configfile))
                {
                    writer.Serialize(file, Config.Settings);
                }
            }
            return DBok;
        }

        public static void GetConfig()
        {
            // Does config file exist?
            if (!File.Exists(configfile))
            {
                SetConfigDefaults("Config file is missing, setting default values. Please check Database and Application settings.");
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
                    SetConfigDefaults("Error reading config file, might be corrupted. The config file is now deleted. Please check Database and Application settings.\n\n" + ex.Message);
                }
            }
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
