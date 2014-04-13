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
			//SB.Top = ScrollbarTestTheme.MainArea.Top;
			//SB.Left = ScrollbarTestTheme.MainArea.Right - SB.Width;
			//SB.Height = ScrollbarTestTheme.MainArea.Height;
		}
	}
}
