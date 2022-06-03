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
                return "https://wotnumbers.com"; // Alternative user local web server
            else
                return "https://wotnumbers.com";
        }

    }
}
