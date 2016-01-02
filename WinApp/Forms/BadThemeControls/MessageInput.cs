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
	
	public partial class MessageInput : Form
	{
        public MessageInput(string title = "", string message = "", string defaultText = "")
		{
			InitializeComponent();
            if (message != "")
			    lblText.Text = message;
            if (title != "")
			    MessageTheme.Text = title;
            txtText.Text = defaultText;
		}

		private void Message_Load(object sender, EventArgs e)
		{
            InputBox.InputResult = new InputBox.ResultClass();
        }

        private void MessageInput_Shown(object sender, EventArgs e)
        {
            txtText.Focus();
        }

		private void btnOK_Click(object sender, EventArgs e)
		{
            InputBox.InputResult.Button = InputBox.InputButton.OK;
            InputBox.InputResult.InputText = txtText.Text;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
            InputBox.InputResult.Button = InputBox.InputButton.Cancel;
            InputBox.InputResult.InputText = txtText.Text;
            this.Close();
		}

        


	}
}
