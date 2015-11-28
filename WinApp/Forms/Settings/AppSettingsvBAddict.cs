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
            chkActivateAutoUpload.Checked = Config.Settings.vBAddictUploadActive;
            txtToken.Text = Config.Settings.vBAddictPlayerToken;
        }

        private void TestStatus(bool testing = true)
        {
            btnTestConnection.Enabled = !testing;
            btnUploadDossier.Enabled = !testing;
            if (testing)
                this.Cursor = Cursors.WaitCursor;
            else
                this.Cursor = Cursors.Default;
            Application.DoEvents();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            TestStatus();
            MsgBox.Show(vBAddictHelper.TestConnection(), "vBAddict connection test result");
            TestStatus(false);
        }

        private void btnUploadDossier_Click(object sender, EventArgs e)
        {
            TestStatus();
            string dossierFile = Config.AppDataBaseFolder + "dossier_prev.dat";
            string token = txtToken.Text.Trim();
            string msg = "";
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool result = vBAddictHelper.UploadDossier(dossierFile, Config.Settings.playerName, Config.Settings.playerServer.ToLower(), token, out msg, false);
            sw.Stop();
            double timeUsed = Convert.ToDouble(sw.ElapsedMilliseconds) / 1000;
            msg += Environment.NewLine + Environment.NewLine + "Used " + timeUsed.ToString() + " sec" + Environment.NewLine + Environment.NewLine;
            string msgHeader = "Success uploading dossier file to vBAddict";
            if (!result)
                msgHeader = "Error uploading dossier file to vBAddict";
            MsgBox.Show(msg, msgHeader);
            TestStatus(false);
        }


        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            Config.Settings.vBAddictUploadActive = chkActivateAutoUpload.Checked;
            Config.Settings.vBAddictPlayerToken = txtToken.Text;
            string msg = "";
            if (!Config.SaveConfig(out msg))
                MsgBox.Show("Error saving settings: " + msg, "Save result");
            else
            {
                // todo
            }
                
        }

        private void linkVbAddict_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.vbaddict.net/token.php");
        }


		
    }
}
