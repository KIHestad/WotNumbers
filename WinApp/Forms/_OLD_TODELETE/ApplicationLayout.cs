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
		private bool currentMasteryBadgeIcons;
		
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
			currentMasteryBadgeIcons = Config.Settings.useSmallMasteryBadgeIcons;
			chkSmallMasteryBadgeIcons.Checked = currentMasteryBadgeIcons;
			chkNotifyIconUse.Checked = Config.Settings.notifyIconUse;
			chkNotifyIconFormExitToMinimize.Checked = Config.Settings.notifyIconFormExitToMinimize;
			SetTextForChkNotifyIconFormExitToMinimize();
			//if (!chkNotifyIconUse.Checked)
			//	chkNotifyIconFormExitToMinimize.Enabled = false;
		}

		private void SetTextForChkNotifyIconFormExitToMinimize()
		{
			string text = "Minimize to task bar when closing application";
			if (chkNotifyIconUse.Checked)
				text = "Minimize to sys tray when closing application";
			chkNotifyIconFormExitToMinimize.Text = text;
			Refresh();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Config.Settings.gridBattlesTotalsTop = chkBattleTotalsPosition.Checked;
			Config.Settings.gridFontSize = Convert.ToInt32(ddFontSize.Text);
			Config.Settings.useSmallMasteryBadgeIcons = chkSmallMasteryBadgeIcons.Checked;
			Config.Settings.notifyIconUse = chkNotifyIconUse.Checked;
			Config.Settings.notifyIconFormExitToMinimize = chkNotifyIconFormExitToMinimize.Checked;
			string msg = "";
			Config.SaveConfig(out msg);
			// Load new mastery badge icons if changed
			if (currentMasteryBadgeIcons != chkSmallMasteryBadgeIcons.Checked)
				ImageHelper.CreateMasteryBageImageTable();
			// Done
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void chkNotifyIconUse_Click(object sender, EventArgs e)
		{
			SetTextForChkNotifyIconFormExitToMinimize();
			// chkNotifyIconFormExitToMinimize.Enabled = chkNotifyIconUse.Checked;
		}

		
	}
}
