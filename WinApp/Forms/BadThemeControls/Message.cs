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
using WinApp.Code;

namespace WinApp.Forms
{
	
	public partial class Message : Form
	{
        private static MsgBox.Type _MessageType { get; set; }
        public Message(string title, string message, MsgBox.Type MessageType = MsgBox.Type.Close)
		{
			InitializeComponent();
			txtMessage.Text = message;
			txtMessage.SelectionStart = 0;
			txtMessage.SelectionLength = 0;
			MessageTheme.Text = title;
            _MessageType = MessageType;
            if (MessageType == MsgBox.Type.OKCancel || MessageType == MsgBox.Type.YesNo)
			{
				btnClose.Visible = false;
				btnCancel.Visible = true;
				btnOK.Visible = true;
                if (MessageType == MsgBox.Type.YesNo)
                {
                    btnCancel.Text = "No";
                    btnOK.Text = "Yes";
                }
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
			this.Height = txtMessage.Top + (lines * 25) + 20; // resize initial height of form to fit content
			if (this.Top == 0)
			{
				Form lastForm = null;
				Form parentForm = null; 
				foreach (Form form in Application.OpenForms)
				{
					parentForm = lastForm;
					lastForm = form;
				}
				string s = parentForm.Text;
				this.Top = parentForm.Top + 40;
				this.Left = parentForm.Left + (parentForm.Width / 2) - (this.Width / 2);
			}
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
            if (_MessageType == MsgBox.Type.YesNo)
                Code.MsgBox.SelectedButton = Code.MsgBox.Button.No;
            else
                Code.MsgBox.SelectedButton = Code.MsgBox.Button.Close;
			this.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
            if (_MessageType == MsgBox.Type.YesNo)
                Code.MsgBox.SelectedButton = Code.MsgBox.Button.Yes;
            else
                Code.MsgBox.SelectedButton = Code.MsgBox.Button.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Code.MsgBox.SelectedButton = Code.MsgBox.Button.Cancel;
			this.Close();
		}


	}
}
