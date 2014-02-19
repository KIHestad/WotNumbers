using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WotDBUpdater
{
    // The data class containing two properties 
    [Serializable()]
    public class ConfigData
    {
        public string DossierFilePath { get; set; }
        public int Run { get; set; }
        public string DatabaseConn { get; set; }
    }
    
    class Config
    {
        private const string configfile = "WotDBUpdaterConfig.xml";

        public static void SaveConfig(ConfigData conf)
        {
            // Write to XML
            XmlSerializer writer = new XmlSerializer(typeof(ConfigData));
            using (FileStream file = File.OpenWrite(configfile))
            {
                writer.Serialize(file, conf);
            }
        }

        public static ConfigData GetConfig()
        {
            // Does config file exist?
            if (!File.Exists(configfile))
            {
               SaveDefaults();
            }
            // Read from XML
            try
            {
                return LoadConfig();
            }
            catch (Exception)
            {
                File.Delete(configfile);
                SaveDefaults();
                return LoadConfig();
            }
        }

        private static void SaveDefaults()
        {
            // Create one with default values
            ConfigData newconf = new ConfigData();
            newconf.DossierFilePath = "";
            newconf.Run = 0;
            newconf.DatabaseConn = "Data Source=.;Initial Catalog=Databasename;Integrated Security=True;";
            SaveConfig(newconf);   
        }

        private static ConfigData LoadConfig()
        {
            ConfigData conf;
            XmlSerializer reader = new XmlSerializer(typeof(ConfigData));
            using (FileStream input = File.OpenRead(configfile))
            {
                conf = reader.Deserialize(input) as ConfigData;
            }
            return conf;
        }

    }
}
