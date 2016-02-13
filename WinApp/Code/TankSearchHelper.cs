using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinApp.Code
{
    class TankSearchHelper
    {
        // Used as return values from tank search form
        public static int SelectedTankId = -1;
        public static MsgBox.Button Result = MsgBox.Button.Cancel;
        // Remember last tank search settings
        public static string SearchText = "";
        public static bool ModeSingle = true;
        public static bool[] SelectedNations = new bool[] { false, false, false, false, false, false, false, false };

        public static void OpenTankSearch(Form parentform)
        {
            Form frm = new Forms.TankSearch();
            if (!Config.Settings.tankSearchMainModeAdvanced)
            {
                frm.Width = 400;
                frm.Height = 400;
            }
            frm.ShowDialog(parentform);
        }
    }
}
