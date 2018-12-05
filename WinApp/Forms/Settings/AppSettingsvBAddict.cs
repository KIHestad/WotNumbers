using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WinApp.Code;

namespace WinApp.Forms.Settings
{
    public partial class AppSettingsvBAddict : UserControl
    {
        public AppSettingsvBAddict()
        {
            InitializeComponent();
        }

        private void vBAddict_Load(object sender, EventArgs e)
        {
            DataBind();
        }

        private void DataBind()
        {
            group_vBAddict_Settings.Text = "Settings for current player: " + Config.Settings.playerNameAndServer;
            chkActivateAutoUpload.Checked = vBAddictHelper.Settings.UploadActive;
            chkActivateAutoReplayUpload.Checked = vBAddictHelper.Settings.UploadReplayActive;
            txtToken.Text = vBAddictHelper.Settings.Token;
            chkShowvbAddictIcon.Checked = Config.Settings.vBAddictShowToolBarMenu;
            toolTipShowvBAddictIcon.SetToolTip(chkShowvbAddictIcon, "Used to go to your profile on vBAddict website");
            EditChangesApply(false);
        }

        //private void TestStatus(bool testing = true)
        //{
        //    btnTestConnection.Enabled = !testing;
        //    btnUploadDossier.Enabled = !testing;
        //    if (testing)
        //        this.Cursor = Cursors.WaitCursor;
        //    else
        //        this.Cursor = Cursors.Default;
        //    Refresh();
        //}

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            MsgBox.Show("vBAddict support EOL");
            //TestStatus();
            //MsgBox.Show(await vBAddictHelper.TestConnection(), "vBAddict connection test result");
            //TestStatus(false);
        }

        private void btnUploadDossier_Click(object sender, EventArgs e)
        {
            MsgBox.Show("vBAddict support EOL");

            //TestStatus();
            //string dossierFile = Config.AppDataBaseFolder + "dossier_prev.dat";
            //string token = txtToken.Text.Trim();
            //string msg = "";
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //bool result = vBAddictHelper.UploadDossier(dossierFile, Config.Settings.playerName, Config.Settings.playerServer.ToLower(), token, out msg, false);
            //sw.Stop();
            //double timeUsed = Convert.ToDouble(sw.ElapsedMilliseconds) / 1000;
            //msg += Environment.NewLine + Environment.NewLine + "Used " + timeUsed.ToString() + " sec" + Environment.NewLine + Environment.NewLine;
            //string msgHeader = "Success uploading dossier file to vBAddict";
            //if (!result)
            //    msgHeader = "Error uploading dossier file to vBAddict";
            //MsgBox.Show(msg, msgHeader);
            //TestStatus(false);
        }


        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        public void SaveChanges()
        {
            MsgBox.Show("vBAddict support EOL");
            //vBAddictHelper.Settings.Token = txtToken.Text;
            //vBAddictHelper.Settings.UploadActive = chkActivateAutoUpload.Checked;
            //vBAddictHelper.Settings.UploadReplayActive = chkActivateAutoReplayUpload.Checked;
            //await vBAddictHelper.SaveSettings();
            //Config.Settings.vBAddictShowToolBarMenu = chkShowvbAddictIcon.Checked;
            //var result = await Config.SaveConfig();
            //if (!result.Success)
            //    MsgBox.Show("Error saving settings: " + result.Message, "Save result");
            //else
            //{
            //    btnCancel.Enabled = false;
            //    btnSaveSettings.Enabled = false;
            //    EditChangesApply(false);
            //}                
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataBind();
            EditChangesApply(false);
        }

        private void linkVbAddict_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.vbaddict.net/token.php");
        }

        private void EditChangesApply(bool changesApplied)
        {
            AppSettingsHelper.ChangesApplied = changesApplied;
            btnCancel.Enabled = changesApplied;
            btnSaveSettings.Enabled = changesApplied;
        }

        private void txtToken_TextChanged(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

        private void chkShowvbAddictIcon_Click(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

        private void chkActivateAutoReplayUpload_Click(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

        private void chkActivateAutoUpload_Click(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

		
    }
}
