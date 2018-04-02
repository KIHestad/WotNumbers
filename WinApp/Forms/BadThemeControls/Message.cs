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
	
	public partial class Message : FormCloseOnEsc
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
            switch (MessageType)
            {
                case MsgBox.Type.Close:
                    MsgBox.SelectedButton = MsgBox.Button.Close;
                    break;
                case MsgBox.Type.OKCancel:
                    btn2.Text = "OK";
                    btn1.Text = "Cancel";
                    btn2.Visible = true;
                    MsgBox.SelectedButton = MsgBox.Button.Cancel;
                    break;
                case MsgBox.Type.YesNo:
                    btn2.Text = "Yes";
                    btn1.Text = "No";
                    MsgBox.SelectedButton = MsgBox.Button.No;
                    btn2.Visible = true;
                    break;
                case MsgBox.Type.OK:
                    btn1.Text = "OK";
                    MsgBox.SelectedButton = MsgBox.Button.OK;
                    break;
                default:
                    break;
            }
		}

		private void Message_Load(object sender, EventArgs e)
		{
            using (Graphics g = CreateGraphics())
            {
                SizeF size = g.MeasureString(txtMessage.Text, txtMessage.Font, txtMessage.Width);
                var h = txtMessage.Top + (int)Math.Ceiling(size.Height) + 90;
                if (h > 300)
                {
                    this.Width = 550;
                    size = g.MeasureString(txtMessage.Text, txtMessage.Font, txtMessage.Width);
                    h = txtMessage.Top + (int)Math.Ceiling(size.Height) + 90;
                }
                if (h > 300)
                {
                    h = 300;
                }
                this.Height = h;
            }
                                   
			if (this.Top == 0)
			{
				Form lastForm = null;
				Form parentForm = null; 
				foreach (Form form in Application.OpenForms)
				{
					parentForm = lastForm;
					lastForm = form;
				}
                if (parentForm != null)
                {
                    string s = parentForm.Text;
                    this.Top = parentForm.Top + 40;
                    this.Left = parentForm.Left + (parentForm.Width / 2) - (this.Width / 2);
                }
			}
            ResizeNow();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            switch (_MessageType)
            {
                case MsgBox.Type.Close:
                    MsgBox.SelectedButton = MsgBox.Button.Close;
                    break;
                case MsgBox.Type.OKCancel:
                    MsgBox.SelectedButton = MsgBox.Button.Cancel;
                    break;
                case MsgBox.Type.YesNo:
                    MsgBox.SelectedButton = MsgBox.Button.No;
                    break;
                case MsgBox.Type.OK:
                    MsgBox.SelectedButton = MsgBox.Button.OK;
                    break;
            }
            this.Close();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            switch (_MessageType)
            {
                case MsgBox.Type.OKCancel:
                    MsgBox.SelectedButton = MsgBox.Button.OK;
                    break;
                case MsgBox.Type.YesNo:
                    MsgBox.SelectedButton = MsgBox.Button.Yes;
                    break;
            }
            this.Close();
        }

        private void btnCopyText_Click(object sender, EventArgs e)
        {
            
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(MessageTheme.Text + Environment.NewLine + Environment.NewLine + txtMessage.Text);
            lblStatus.Text = "Text copied to clipboard";
        }

        private void MessageTheme_Resize(object sender, EventArgs e)
        {
            ResizeNow();
        }

        private void ResizeNow()
        {
            using (Graphics g = CreateGraphics())
            {
                SizeF size = g.MeasureString(txtMessage.Text, txtMessage.Font, txtMessage.Width);
                var height = Math.Ceiling(size.Height);
                if (txtMessage.Height < height)
                    lblStatusRowCount.Text = "Form needs resizing";
                else
                    lblStatusRowCount.Text = "";
            }
        }
    }
}
