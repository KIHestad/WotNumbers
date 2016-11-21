using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;
using WinApp.Code.FormLayout;

namespace WinApp.Forms.Settings
{
    public partial class AppSettingsOptions : UserControl
    {
        private static string currentValue = "";
        
        public AppSettingsOptions()
        {
            InitializeComponent();
        }

        private void AppSettingsLayout_Load(object sender, EventArgs e)
        {
            DataBind();
        }

        private void DataBind()
        {
            chkNotifyIconUse.Checked = Config.Settings.notifyIconUse;
            chkNotifyIconFormExitToMinimize.Checked = Config.Settings.notifyIconFormExitToMinimize;
            txtBackupFilePath.Text = Config.Settings.databaseBackupFilePath;
            ddHour.Text = Config.Settings.newDayAtHour.ToString("00");
            SetTextForChkNotifyIconFormExitToMinimize();
            chkTempBattleresultSave.Checked = Config.SessionSettings.tempBattleResultSave;
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
            // Settings
            Config.Settings.notifyIconUse = chkNotifyIconUse.Checked;
            Config.Settings.notifyIconFormExitToMinimize = chkNotifyIconFormExitToMinimize.Checked;
            Config.Settings.databaseBackupFilePath = txtBackupFilePath.Text;
            Config.Settings.databaseBackupPeriod = Convert.ToInt32(ddPeriod.Text);
            Config.Settings.newDayAtHour = Convert.ToInt32(ddHour.Text);
            string msg = "";
            Config.SaveConfig(out msg);
            EditChangesApply(false);
            //Session Settings
            Config.SessionSettings.tempBattleResultSave = chkTempBattleresultSave.Checked;
            Config.SessionSettings.tempBattleResultSaveFirstRun = chkTempBattleresultSave.Checked;
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

        private void ddHour_Click(object sender, EventArgs e)
        {
            currentValue = ddHour.Text;
            Code.DropDownGrid.Show(ddHour, Code.DropDownGrid.DropDownGridType.List, "00,01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23");
        }

        private void ddHour_TextChanged(object sender, EventArgs e)
        {
            if (currentValue != ddHour.Text)
                EditChangesApply(true);
        }

        private void ddPeriod_Click(object sender, EventArgs e)
        {
            currentValue = ddPeriod.Text;
            Code.DropDownGrid.Show(ddPeriod, Code.DropDownGrid.DropDownGridType.List, "0,1,2,3,4,5,6,7");
        }

        private void ddPeriod_TextChanged(object sender, EventArgs e)
        {
            if (currentValue != ddPeriod.Text)
                EditChangesApply(true);
        }

        private void btnSelectBackupFilePath_Click(object sender, EventArgs e)
        {
            // Select dossier file
            folderBrowserDialogBackup.ShowNewFolderButton = true;

            if (txtBackupFilePath.Text != "")
            {
                folderBrowserDialogBackup.SelectedPath = txtBackupFilePath.Text;
            }
            DialogResult result = folderBrowserDialogBackup.ShowDialog();
            // If file selected save config with new values
            if (folderBrowserDialogBackup.SelectedPath != "" && result != DialogResult.Cancel)
            {
                txtBackupFilePath.Text = folderBrowserDialogBackup.SelectedPath;
            }
        }

        private void txtBackupFilePath_TextChanged(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

        private void btnRunBackup_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.DatabaseBackup();
            frm.ShowDialog(this);
        }

        private void chkTempBattleresultSave_Click(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }
    }
}
