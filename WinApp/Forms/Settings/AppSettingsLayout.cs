using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms.Settings
{
    public partial class AppSettingsLayout : UserControl
    {
        private bool currentMasteryBadgeIcons;
        
        public AppSettingsLayout()
        {
            InitializeComponent();
        }

        private static string currentValue = "";
        private void ddFontSize_Click(object sender, EventArgs e)
        {
            currentValue = ddFontSize.Text;
            Code.DropDownGrid.Show(ddFontSize, Code.DropDownGrid.DropDownGridType.List, "6,7,8,9,10,11,12,14");
        }

        private void AppSettingsLayout_Load(object sender, EventArgs e)
        {
            DataBind();
        }

        private void DataBind()
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
            EditChangesApply(false);
        }

        private void SetTextForChkNotifyIconFormExitToMinimize()
        {
            string text = "Minimize to task bar when closing application";
            if (chkNotifyIconUse.Checked)
                text = "Minimize to sys tray when closing application";
            chkNotifyIconFormExitToMinimize.Text = text;
            Refresh();
        }

        private void EditChangesApply(bool changesApplied)
        {
            AppSettingsHelper.ChangesApplied = changesApplied;
            btnCancel.Enabled = changesApplied;
            btnSave.Enabled = changesApplied;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        public void SaveChanges()
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
            EditChangesApply(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataBind();
            EditChangesApply(false);
        }

        private void chkNotifyIconUse_Click(object sender, EventArgs e)
        {
            SetTextForChkNotifyIconFormExitToMinimize();
            EditChangesApply(true);
        }

        private void chkNotifyIconFormExitToMinimize_Click(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

        private void chkBattleTotalsPosition_Click(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

        private void chkSmallMasteryBadgeIcons_Click(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

        private void ddFontSize_TextChanged(object sender, EventArgs e)
        {
            if (currentValue != ddFontSize.Text)
                EditChangesApply(true);
        }


    }
}
