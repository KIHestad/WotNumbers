using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater
{
    public static class importTanks2DB
    {

        private static string LogText(string logtext)
        {
            return DateTime.Now + " " + logtext;
        }

        public static String fetchTanks()
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            string jsonfile = appPath + "/tanks.json";
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(jsonfile))
            {
                String line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
            }
            string json = sb.ToString();
            return json;
        }

        


        public static List<string> importTanks(bool TestRunPrevJsonFile = false)
        {

            

            List<string> logtext = new List<string>();

            
            logtext.Add(LogText("test"));
            return logtext;
        }
    }
}
