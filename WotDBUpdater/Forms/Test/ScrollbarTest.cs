using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater.Forms.Test
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

		private void badScrollBar1_MouseMove(object sender, MouseEventArgs e)
		{
			txtY.Text = badScrollBar1.ScrollPosition.ToString();
		}

		private void badScrollBar2_MouseMove(object sender, MouseEventArgs e)
		{
			txtX.Text = badScrollBar2.ScrollPosition.ToString();
		}

	}
}
