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
            lblTitle.Text = title;
        }

        private void Message_Load(object sender, EventArgs e)
        {
            panelTop.Top = 1;
            panelTop.Left = 1;
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
            panelTop.Width = this.Width - 2;
            picClose.Left = panelTop.Width - picClose.Width;
            picResize.Left = this.Width - picResize.Width - 1;
            picResize.Top = this.Height - picResize.Height - 1;
            txtMessage.Height = this.Height - txtMessage.Top - 1;
            txtMessage.Width = this.Width - (txtMessage.Left * 2);
            Refresh();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_MouseHover(object sender, EventArgs e)
        {
            picClose.BackColor = Code.Support.StripLayout.colorGrayHover;
        }

        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            picClose.BackColor = Code.Support.StripLayout.colorGrayMain;
        }

        private void MessageBox_Paint(object sender, PaintEventArgs e)
        {
            Color border = Color.Black;
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, border, ButtonBorderStyle.Solid);
        }

        private bool moving = false;
        private Point moveFromPoint;
        private int formX;
        private int formY;

        private void picResize_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            moveFromPoint = Cursor.Position;
            formX = Main.ActiveForm.Width;
            formY = Main.ActiveForm.Height;
        }

        private void picResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(moveFromPoint));
                if (formX + dif.X > 300) Main.ActiveForm.Width = formX + dif.X;
                if (formY + dif.Y > 150) Main.ActiveForm.Height = formY + dif.Y;
                RefreshForm();
            }
        }

        private void picResize_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
        }

        private void picResize_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.SizeNWSE;
        }

        private void picResize_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panelTop_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

    }
}
