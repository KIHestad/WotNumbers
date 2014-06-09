using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp.Forms
{
	public partial class ScrollbarTest : Form
	{
		public ScrollbarTest()
		{
			InitializeComponent();
		}

		private void ScrollbarTest_Resize(object sender, EventArgs e)
		{
			badScrollBar1.Top = ScrollbarTestTheme.MainArea.Top;
			badScrollBar1.Left = ScrollbarTestTheme.MainArea.Right - badScrollBar1.Width;
			badScrollBar1.Height = ScrollbarTestTheme.MainArea.Height - badScrollBar2.Height;
			badScrollBar2.Top = ScrollbarTestTheme.MainArea.Bottom - badScrollBar2.Height;
			badScrollBar2.Left = ScrollbarTestTheme.MainArea.Left;
			badScrollBar2.Width = ScrollbarTestTheme.MainArea.Width - badScrollBar1.Width;
		}


		private bool s1move = false;
		private void badScrollBar1_MouseMove(object sender, MouseEventArgs e)
		{
			if (s1move)
				txtY.Text = badScrollBar1.ScrollPosition.ToString();
		}
			

		private void badScrollBar1_MouseDown(object sender, MouseEventArgs e)
		{
			s1move = true;
			txtY.Text = badScrollBar1.ScrollPosition.ToString();
		}

		private void badScrollBar1_MouseUp(object sender, MouseEventArgs e)
		{
			s1move = false;
		}

		private void badScrollBar2_MouseMove(object sender, MouseEventArgs e)
		{
			txtX.Text = badScrollBar2.ScrollPosition.ToString();
		}

		private void badDropDownBox1_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(badDropDownBox1, Code.DropDownGrid.DropDownGridType.List, "Dette,er,en,test,for,scrollbar,7,8,9,10,11,12,13");
		}

		private void badDropDownBox1_TextChanged(object sender, EventArgs e)
		{
			Code.MsgBox.Show("Txt changed: " + badDropDownBox1.Text);
		}
	}
}
