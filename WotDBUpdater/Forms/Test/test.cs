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
	public partial class test : Form
	{
		public test()
		{
			InitializeComponent();

		}

		private void badButton1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void badButton2_Click(object sender, EventArgs e)
		{
			if (badForm1.FormBorderColor == Code.Support.ColorTheme.FormBorderBlack)
			{
				badForm1.FormBorderColor = Code.Support.ColorTheme.FormBorderBlue;
				badButton2.Text = "FormBorderBlue";
			}
			else if (badForm1.FormBorderColor == Code.Support.ColorTheme.FormBorderBlue)
			{
				badForm1.FormBorderColor = Code.Support.ColorTheme.FormBorderRed;
				badButton2.Text = "FormBorderRed";
			}
			else
			{
				badForm1.FormBorderColor = Code.Support.ColorTheme.FormBorderBlack;
				badButton2.Text = "FormBorderBlack";
			}
			Refresh();
		}

		private void test_Resize(object sender, EventArgs e)
		{

		}
	}
}
