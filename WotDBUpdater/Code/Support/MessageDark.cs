using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WotDBUpdater.Forms;

namespace WotDBUpdater.Code.Support
{
    class MessageDark
    {
        public static void Show(string message, string title = "Message")
        {
            Form frm = new Forms.Message(title, message);
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.ShowDialog();
        }
    }
}
