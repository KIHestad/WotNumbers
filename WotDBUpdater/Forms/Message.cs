using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater.Forms
{
	
	public partial class Message : Form
	{
		public Message(string title, string message, Code.MsgBoxType MessageType = Code.MsgBoxType.Close)
		{
			InitializeComponent();
			txtMessage.Text = message;
			txtMessage.SelectionStart = 0;
			txtMessage.SelectionLength = 0;
			MessageTheme.Text = title;
			if (MessageType == Code.MsgBoxType.OKCancel)
			{
				btnClose.Visible = false;
				btnCancel.Visible = true;
				btnOK.Visible = true;
			}
		}

		private void Message_Load(object sender, EventArgs e)
		{
			string msg = txtMessage.Text;
			int lines = Convert.ToInt32((Convert.ToDouble(msg.Length) / 45));
			int pos = 0;
			// search for to LF = add lines
			while (msg.IndexOf(Environment.NewLine, pos) > 0)
			{
				pos = msg.IndexOf(Environment.NewLine, pos) + 2;
				if (msg.Length > pos && msg.Substring(pos, 2) == Environment.NewLine)
				{
					lines++;
					pos = pos + 2;
				}
			}
			if (lines > 12) lines = 12; // max size
			this.Height = txtMessage.Top + (lines * 30) + 20; // resize initial height of form to fit content
			RefreshForm();
		}

		private void RefreshForm()
		{
			txtMessage.Height = this.Height - txtMessage.Top - 20;
			txtMessage.Width = this.Width - (txtMessage.Left * 2);
			Refresh();
		}

		private void badForm1_Resize(object sender, EventArgs e)
		{
			RefreshForm();
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			Code.MsgBox.SelectedButton = Code.MsgBox.Button.CloseButton;
			this.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Code.MsgBox.SelectedButton = Code.MsgBox.Button.OKButton;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Code.MsgBox.SelectedButton = Code.MsgBox.Button.CancelButton;
			this.Close();
		}


	}
}
