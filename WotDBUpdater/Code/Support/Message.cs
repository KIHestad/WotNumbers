using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WotDBUpdater.Forms;

namespace WotDBUpdater.Code.Support
{
    class Message
    {
        public static void Show(string title, string message)
        {
            Form frm = new Forms.Message(title, message);
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.ShowDialog();
        }
    }
}
