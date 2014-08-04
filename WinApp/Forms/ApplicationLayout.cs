using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinApp.Forms
{
	public partial class ApplicationLayout : Form
	{
		public ApplicationLayout()
		{
			InitializeComponent();
		}

		private void ddFontSize_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(ddFontSize, Code.DropDownGrid.DropDownGridType.List, "8,9,10,11,12,14,16");
		}

	}
}
