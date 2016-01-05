using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Gadget
{
	public partial class ucChartBattle : UserControl
	{
		public ucChartBattle()
		{
			InitializeComponent();
		}

		private void ucChartBattle_Load(object sender, EventArgs e)
		{
			chart1.Top = 1;
			chart1.Left = 1;
		}

        public void DataBind()
        {

        }

		private void ucChartBattle_Resize(object sender, EventArgs e)
		{
			chart1.Width = this.Width - 2;
			chart1.Height = this.Height - 30;
		}

		private void ucChartBattle_Paint(object sender, PaintEventArgs e)
		{
			if (BackColor == ColorTheme.FormBackSelectedGadget)
				GadgetHelper.DrawBorderOnGadget(sender, e);
		}
	}
}
