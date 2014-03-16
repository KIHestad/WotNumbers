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

namespace WotDBUpdater.Forms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Startup settings
            Config.GetConfig();
            if (Config.CheckDBConn())
            {
                string result = dossier2json.updateDossierFileWatcher();
                Log(result);
                SetStartStopButton();
                SetFormTitle();
                // Init
                TankData.GetTankListFromDB();
                TankData.GetJson2dbMappingViewFromDB();
                TankData.GettankData2BattleMappingViewFromDB();
            }
        }

        private void SetFormTitle()
        {
            // Check / show logged in player
            if (Config.Settings.playerName == "")
            {
                this.Text = "WotDBUpdater - NO PLAYER SELECTED";
            }
            else
            {
                this.Text = "WotDBUpdater - " + Config.Settings.playerName;
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
            if (Config.Settings.run == 1)
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
            bool run = !(Config.Settings.run == 1); // toggle run
            if (run) Config.Settings.run = 1; else Config.Settings.run = 0; // save as 0 = false, 1=true
            string msg = "";
            Config.SaveAppConfig(out msg);
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
            try
            {
                MessageBoxEx.Show(this, listBoxLog.Items[listBoxLog.SelectedIndex].ToString(), "Log Details");
            }
            catch (Exception)
            {
                
                // nothing
            }
            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Help.About();
            frm.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void selectApplicationSetting_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.File.ApplicationSetting();
            frm.ShowDialog();
            SetFormTitle();
            // Init
        }

        private void databaseSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.File.DatabaseSetting();
            frm.ShowDialog();
        }

        private void showTankTableInGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Test.CountryInGrid();
            frm.ShowDialog();
        }

        private void addCountryToTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Test.AddCountryToTable();
            frm.ShowDialog();
        }

        private void showDatabaseTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Reports.DBTable();
            frm.Show();
        }

        private void showDatabaseViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Reports.DBView();
            frm.Show();
        }

        private void listTanksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = TankData.ListTanks();
            MessageBoxEx.Show(this, s);
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
            MessageBoxEx.Show(this, content);
        }

        private void btntestForce_Click(object sender, EventArgs e)
        {
            // Test running previous dossier file, force update - even if no more battles is detected
            List<string> result = dossier2json.manualRun(true, true);
            Log(result);
        }

        private void btnTestAlt_Click(object sender, EventArgs e)
        {
            // Test running previous dossier file, force update - even if no more battles is detected
            List<string> result = dossier2json.manualRun(true, true);
            Log(result);
        }

        private void testReadModuleDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = Modules2DB.ImportTurrets();
            MessageBoxEx.Show(this, s);
        }

        private void importGunsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = Modules2DB.ImportGuns();
            MessageBoxEx.Show(this, s);
        }

        private void importRadiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = Modules2DB.ImportRadios();
            MessageBoxEx.Show(this, s);
        }

        private void testProgressBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Test.TestProgressBar();
            frm.Show();
        }

        private void importTankWn8ExpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.File.ImportTank();
            frm.ShowDialog();
        }

        private void testViewRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewRange.CalcViewRange();
        }

        
    }

    

}
