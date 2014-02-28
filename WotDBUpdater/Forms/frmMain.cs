using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using WotDBUpdater.Forms;
using System.Net;

//using IronPython.Hosting;
//using Microsoft.Scripting.Hosting;
//using IronPython.Runtime;

namespace WotDBUpdater
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Startup settings
            Config.GetConfig();
            Config.CheckDBConn();

            string result = dossier2json.updateDossierFileWatcher();
            Log(result);
            SetStartStopButton();
            SetFormTitle();
            // Init
            tankData.GetTankListFromDB();
            tankData.GetJsonUserTankFromDB();
            moduleData.GetTankListFromDB();
            moduleData.GetJsonTurretFromDB();
        }

        private void SetFormTitle()
        {
            // Check / show logged in user
            if (Config.Settings.UserName == "")
            {
                this.Text = "WotDBUpdater - NO USER SELECTED";
            }
            else
            {
                this.Text = "WotDBUpdater - " + Config.Settings.UserName;
            }
        }

        void Log(string logtext, bool addTime = false)
        {
            // log to ListBox and scroll to bottom
            string timestamp = "";
            if (addTime) timestamp = DateTime.Now.ToString() + " ";
            listBoxLog.Items.Add(timestamp + logtext);
            listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
        }

        void Log(List<string> logtext)
        {
            foreach (string s in logtext)
            {
                Log(s);
            }
        }

        private void SetStartStopButton()
        {
            // Set Start - Stop button properties
            if (Config.Settings.Run == 1)
            {
                btnStartStop.Text = "Stop";
                lblStatus.Text = "RUNNING";
                pnlStatus.BackColor = System.Drawing.Color.ForestGreen; 
            }
            else
            {
                btnStartStop.Text = "Start";
                lblStatus.Text = "STOPPED";
                pnlStatus.BackColor = System.Drawing.Color.Gray;
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            // Start - Stop button event for listening to dossier file
            bool run = !(Config.Settings.Run == 1); // toggle run
            if (run) Config.Settings.Run = 1; else Config.Settings.Run = 0; // save as 0 = false, 1=true
            Config.SaveConfig();
            string result = dossier2json.updateDossierFileWatcher();
            Log(result);
            SetStartStopButton();
        }

        private void btnManualRun_Click(object sender, EventArgs e)
        {
            // Dossier file manual handling
            List<string> result = dossier2json.manualRun();
            Log(result);
        }

        private void btnTestPrev_Click(object sender, EventArgs e)
        {
            // Test running previous dossier file
            List<string> result = dossier2json.manualRun(true);
            Log(result);
        }

        private void listBoxLog_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(listBoxLog.Items[listBoxLog.SelectedIndex].ToString(), "Log Details");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAbout();
            frm.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void selectApplicationSetting_Click(object sender, EventArgs e)
        {
            Form frm = new frmApplicationSetting();
            frm.ShowDialog();
            SetFormTitle();
        }

        private void databaseSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDatabaseSetting();
            frm.ShowDialog();
        }

        private void showTankTableInGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmCountryInGrid();
            frm.ShowDialog();
        }

        private void addCountryToTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddCountryToTable();
            frm.ShowDialog();
        }

        private void importTanksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmImportTank();
            frm.ShowDialog();
        }

        private void showDatabaseTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Reports.frmDBTable();
            frm.Show();
        }

        private void showDatabaseViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Reports.frmDBView();
            frm.Show();
        }

        private void listTanksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = tankData.listTanks();
            MessageBox.Show(s);
        }

        private void testURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string lcUrl = "https://api.worldoftanks.eu/wot/encyclopedia/tankinfo/?application_id=2a70055c41b7a6fff1e35a3ba9cadbf1&tank_id=49";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(lcUrl);

            httpRequest.Timeout = 10000;     // 10 secs
            httpRequest.UserAgent = "Code Sample Web Client";

            HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
            StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());

            string content = responseStream.ReadToEnd();
            MessageBox.Show(content);
        }

        private void btntestForce_Click(object sender, EventArgs e)
        {
            // Test running previous dossier file, force update - even if no more battles is detected
            List<string> result = dossier2json.manualRun(true, true);
            Log(result);
        }



        private void testReadModuleDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = modules2DB.importTurrets();
            MessageBox.Show(s);
        }

        private void importGunsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = modules2DB.importGuns();
            MessageBox.Show(s);
        }
    }

    

}
