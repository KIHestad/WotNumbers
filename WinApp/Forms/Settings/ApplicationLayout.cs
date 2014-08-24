using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

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
			Code.DropDownGrid.Show(ddFontSize, Code.DropDownGrid.DropDownGridType.List, "6,7,8,9,10,11,12,14");
		}

		private void ApplicationLayout_Load(object sender, EventArgs e)
		{
			chkBattleTotalsPosition.Checked = Config.Settings.gridBattlesTotalsTop;
			int fs = Config.Settings.gridFontSize;
			if (fs == 0) fs = 8;
			if (fs < 6) fs = 6;
			if (fs > 14) fs = 14;
			ddFontSize.Text = fs.ToString();
			chkHomeViewNewLayout.Checked = Config.Settings.homeViewNewLayout;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Config.Settings.gridBattlesTotalsTop = chkBattleTotalsPosition.Checked;
			Config.Settings.gridFontSize = Convert.ToInt32(ddFontSize.Text);
			Config.Settings.homeViewNewLayout = chkHomeViewNewLayout.Checked;
			string msg = "";
			Config.SaveConfig(out msg);
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}


	}
}
