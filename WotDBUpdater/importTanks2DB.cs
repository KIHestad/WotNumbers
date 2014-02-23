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

        public static List<string> importTanks(bool TestRunPrevJsonFile = false)
        {
            List<string> logtext = new List<string>();

            
            logtext.Add(LogText("test"));
            return logtext;
        }
    }
}
