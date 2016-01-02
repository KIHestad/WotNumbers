using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Forms;

namespace WinApp.Code
{
	public class InputBox
	{
        public enum InputButton
        {
            OK = 0,
            Cancel = 2
        }
        
        public class ResultClass
        {
            public InputButton Button { get; set; }
            public string InputText { get; set; }
        }

        private static ResultClass _inputResult;
        public static ResultClass InputResult
		{
            get { return _inputResult; }
            set { _inputResult = value; }
		}

        public static ResultClass Show(string message = "", string title = "", string defaultText = "", Form owner = null)
		{
            return ShowNow(message, title, defaultText, owner);
		}

        private static ResultClass ShowNow(string message, string title, string defaultText, Form owner)
		{
            try
            {
                Form frm = new Forms.MessageInput(title, message, defaultText);
                frm.FormBorderStyle = FormBorderStyle.None;
                if (owner == null)
                    frm.ShowDialog();
                else
                {
                    if (owner.WindowState == FormWindowState.Minimized)
                        owner.WindowState = Config.Settings.posSize.WindowState;
                    frm.ShowDialog(owner);
                }
                return InputResult;
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, "Error opening inputbox");
                InputResult.InputText = "";
                InputResult.Button = InputButton.Cancel;
                return InputResult;
                // throw;
            }
            
		}

	}
}
