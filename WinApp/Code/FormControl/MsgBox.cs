using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Forms;

namespace WinApp.Code
{
	public enum MsgBoxType
	{
		Close = 0,
		OKCancel = 1,
        YesNo = 2,
	}
	
	class MsgBox
	{
		
		public enum Button
		{
			Close = 0,
			OK = 1,
			Cancel = 2,
            Yes = 3,
            No = 4,
		}

		private static Button _SelectedButton = Button.Close;
		public static Button SelectedButton
		{
			get { return _SelectedButton; }
			set { _SelectedButton = value; }
		}

		public static Button Show(string Message, string Title, MsgBoxType MessageType, Form owner)
		{
			return ShowNow(Message, Title, MessageType, owner);
		}

		public static Button Show(string Message, string Title, MsgBoxType MessageType)
		{
			return ShowNow(Message, Title, MessageType, null);
		}

		public static Button Show(string Message, string Title, Form owner)
		{
			return ShowNow(Message, Title, MsgBoxType.Close, owner);
		}

		public static Button Show(string Message, string Title)
		{
			return ShowNow(Message, Title, MsgBoxType.Close, null);
		}

		private static Button ShowNow(string Message, string Title, MsgBoxType MessageType, Form owner)
		{
			Form frm = new Forms.Message(Title, Message, MessageType);
			frm.FormBorderStyle = FormBorderStyle.None;
			if (owner == null)
				frm.ShowDialog();
			else
			{
				if (owner.WindowState == FormWindowState.Minimized)
					owner.WindowState = Config.Settings.posSize.WindowState;
				frm.ShowDialog(owner);
			}
			return SelectedButton;
		}

	}
}
