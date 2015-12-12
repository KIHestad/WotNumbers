using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
    public partial class Replay : Form
    {
        private int _battleId { get; set; }
        private string _filename { get; set; }
        public Replay(int battleId)
        {
            InitializeComponent();
            _battleId = battleId;
        }

        private void Replay_Shown(object sender, EventArgs e)
        {
            GetvBAddictUploadInfo();
            FileInfo fi = ReplayHelper.GetReplayFile(_battleId);
            if (fi != null)
            {
                lblMessage.Text = "Replay file for the current battle is found.";
                txtPath.Text = Path.GetDirectoryName(fi.FullName);
                txtFile.Text = Path.GetFileName(fi.FullName);
                _filename = fi.FullName;
            }
            else
            {
                lblMessage.Text = "Sorry, could not find any replay file for this battle.";
                btnPlayReplay.Enabled = false;
                btnShowFolder.Enabled = false;
                btnUploadReplayTovBAddict.Enabled = false;
            }
        }

        private void btnShowFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", string.Format("/select,\"{0}\"", _filename));
        }

        private void btnPlayReplay_Click(object sender, EventArgs e)
        {
            Process[] p = Process.GetProcessesByName("WorldOfTanks");
            if (p[0].ProcessName == "WorldOfTanks")
                MsgBox.Show("It seems like World of Tanks is already running. Shut down WoT to be able to play replay", "WoT is running");
            Process.Start("explorer.exe", _filename);
        }

        private void btnUploadReplayTovBAddict_Click(object sender, EventArgs e)
        {
            btnUploadReplayTovBAddict.Enabled = false;
            btnUploadReplayTovBAddict.Text = "Uploading...";
            Application.DoEvents();
            string resultText = "";
            bool resultOK = vBAddictHelper.UploadReplay(_battleId, _filename, Config.Settings.playerName, Config.Settings.playerServer.ToLower(), Config.Settings.vBAddictPlayerToken, out resultText);
            string msg = "Upload to vBAddict was successful.";
            if (!resultOK)
            {
                msg = "Upload to vBAddict failed with message: " + resultText;
                btnUploadReplayTovBAddict.Text = "Upload to vBAddict";
                btnUploadReplayTovBAddict.Enabled = true;
                MsgBox.Show(msg, "Upload to vBAddict result", this);
            }
            else
            {
                btnUploadReplayTovBAddict.Text = "Upload done";
                GetvBAddictUploadInfo();
            }
            Application.DoEvents();
        }

        private void GetvBAddictUploadInfo()
        {
            linkvBAddictUpload.Text = vBAddictHelper.GetInfoUploadedvBAddict(_battleId);
            toolTipvBAddictLink.SetToolTip(linkvBAddictUpload, "Go to player profile at vBAddict");
        }

        private void linkvBAddictUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkvBAddictUpload.Text != "")
            {
                string serverURL = string.Format("http://www.vbaddict.net/player/{0}-{1}", Config.Settings.playerName.ToLower(), ExternalPlayerProfile.GetServer);
                System.Diagnostics.Process.Start(serverURL);
            }
        }
    }
}
