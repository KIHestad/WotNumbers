using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
    public class AppSettingsHelper
    {
        public enum Tabs
        {
            NotSelected = 0,
            Main = 1,
            Layout = 2,
            WoTGameClient = 3,
            vBAddict = 4,
            Import = 5,
            Replay = 6,
        }

        public static bool ChangesApplied { get; set; }
    }
}
