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
            if (IsDebugging())
                // Alternative user local web server
                return "https://github.com/KIHestad/WotNumbers/tree/master/LatestRelease";
            else
                return "https://github.com/KIHestad/WotNumbers/tree/master/LatestRelease";
        }

    }
}
