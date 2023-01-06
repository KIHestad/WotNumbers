using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
    public class Constants
    {
        public static bool IsDebugging()
        {
            bool debugging = false;
            WellAreWe(ref debugging);
            return debugging;
        }

        [Conditional("DEBUG")]
        private static void WellAreWe(ref bool debugging)
        {
            debugging = true;
        }

        public static string WotNumWebUrl()
        {
            return "https://wotnumbers.com";
        }
        public static string WotNumDownloadUrl()
        {
            return "https://github.com/KIHestad/WotNumbers/tree/master/LatestRelease";
        }
        public static string WotNumVersionSettingsFolderUrl()
        {
            if (IsDebugging())
                // Alternative user local web server
                return "https://raw.githubusercontent.com/D0ct0rDave/WotNumbers/master/LatestRelease";
            else
                return "https://raw.githubusercontent.com/KIHestad/WotNumbers/master/LatestRelease";
        }

        public static string WotNumVersionSettingsUrl()
        {
            return WotNumVersionSettingsFolderUrl() + "/VersionSettings.json";
        }
        public static readonly int RecalcDataBatchSize = 100;
        public static readonly int LastEntriesSize = 1000;

        public static readonly int ArenaCreationAndBattleStartTimeGap = 45;
        public static readonly int BattleEndTimeThreshold = 30;
    }
}
