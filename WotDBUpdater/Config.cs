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
        public int UserID { get; set; }
        public string UserName { get; set; }
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
            Config.Settings.UserID = 0;
            Config.Settings.UserName = "";
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

        public static bool SaveConfig(bool CheckDBSetting = false, bool LookupUserInDB = false)
        {
            bool DBok = true;
            if (CheckDBSetting) DBok = CheckDBConn();

            if (DBok)
            {
                if (LookupUserInDB)
                {
                    try
                    {
                        // Check if user exist in database, if not create
                        SqlConnection con = new SqlConnection(Config.Settings.DatabaseConn);
                        con.Open();
                        Config.Settings.UserName = Config.Settings.UserName.Trim();
                        // Check if user exist
                        bool createnewuser = false;
                        SqlCommand cmd = new SqlCommand("SELECT * FROM wotUser WHERE name=@name", con);
                        cmd.Parameters.AddWithValue("@name", Config.Settings.UserName);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (!reader.HasRows) createnewuser = true;
                        reader.Close();
                        if (createnewuser)
                        {
                            // create new user
                            cmd = new SqlCommand("INSERT INTO wotUser (name) VALUES (@name)", con);
                            cmd.Parameters.AddWithValue("@name", Config.Settings.UserName);
                            cmd.ExecuteNonQuery();
                        }
                        // Get User ID
                        cmd = new SqlCommand("SELECT * FROM wotUser WHERE name=@name", con);
                        cmd.Parameters.AddWithValue("@name", Config.Settings.UserName);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Config.Settings.UserID = Convert.ToInt32(reader["wotUserId"]);
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error occured looup user in database, check your Application Settings.\n\n" + ex.Message, "Config error");
                        Config.Settings.UserID = 0;
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
