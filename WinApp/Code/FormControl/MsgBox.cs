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
		OKCancel = 1
	}
	
	class MsgBox
	{
		
		public enum Button
		{
			CloseButton = 0,
			OKButton = 1,
			CancelButton = 2
		}

		private static Button _SelectedButton = Button.CloseButton;
		public static Button SelectedButton
		{
			get { return _SelectedButton; }
			set { _SelectedButton = value; }
		}

		public static Button Show(string Message, string Title = "Message", MsgBoxType MessageType = MsgBoxType.Close)
		{
			Form frm = new Forms.Message(Title, Message, MessageType);
			frm.FormBorderStyle = FormBorderStyle.None;
			frm.ShowDialog();
			return SelectedButton;
		}
	}
}
