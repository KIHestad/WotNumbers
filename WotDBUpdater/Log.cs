using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater
{
    public class Log
    {
        private static string path = Path.GetDirectoryName(Application.ExecutablePath) + "/log.txt"; // path to app dir + logfilename

        public static void LogToFile(string logtext, bool addDateTime = false)
        {
            // Add list og Strings
            CreateFileIfNotExist();
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(logtext);
            }
        }

        public static void LogToFile(List<string> logtext, bool addDateTime = false)
        {
            // Add list og Strings
            CreateFileIfNotExist();
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("");
                foreach (var s in logtext)
                {
                    sw.WriteLine(s);
                }
            }
        }

        private static void CreateFileIfNotExist()
        {
            // This text is added only once to the file. 
            if (!File.Exists(path))
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("**************************************************");
                    sw.WriteLine("Start logging: " + DateTime.Now.ToString());
                    sw.WriteLine("**************************************************");
                    sw.WriteLine("");
                }
            }
        }

        private static string AddDateTime(string logtext, bool addDateTime)
        {
            string s = logtext;
            if (addDateTime) s = DateTime.Now.ToString() + " " + s;
            return s;
        }
    }
}
