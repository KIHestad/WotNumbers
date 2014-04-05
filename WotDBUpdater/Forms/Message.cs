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
		public Message(string title, string message)
		{
			InitializeComponent();
			txtMessage.Text = message;
			txtMessage.SelectionStart = 0;
			txtMessage.SelectionLength = 0;
			badForm1.Text = title;
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
			if (lines >= 5)
			{
				if (lines > 12) lines = 12; // max size
				this.Height = txtMessage.Top + (lines * 25); // resize initial height of form to fit content
			}
			RefreshForm();
		}

		private void RefreshForm()
		{
			txtMessage.Height = this.Height - txtMessage.Top - 1;
			txtMessage.Width = this.Width - (txtMessage.Left * 2);
			Refresh();
		}


	}
}
