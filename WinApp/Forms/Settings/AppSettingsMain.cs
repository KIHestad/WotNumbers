using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Scripting;
using WinApp.Code;

namespace WinApp.Forms.Settings
{
    public partial class AppSettingsMain : UserControl
    {
        public AppSettingsMain()
        {
            InitializeComponent();
        }

        private void AppSettingsMain_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        
        private void DataBind()
        {
            // Startup settings
            txtDossierFilePath.Text = Config.Settings.dossierFilePath;
            // Database type
            string databaseInfo = "";
            if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
                databaseInfo = "Databasetype: MS SQL Server\n" +
                                "Sever/database: " + Config.Settings.databaseServer + "/" + Config.Settings.databaseName;
            else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
                databaseInfo = "Database Type: SQLite\nDatabase File: " + Path.GetFileName(Config.Settings.databaseFileName);
            lblDbSettings.Text = databaseInfo;
            // Download path and settings
            txtDownloadFilePath.Text = Config.Settings.downloadFilePath;
            chkCreateDownloadSubFolders.Checked = Config.Settings.downloadFilePathAddSubfolder;
            // Player
            cboSelectPlayer.Text = Config.Settings.playerNameAndServer;
            chkShowDBError.Checked = Config.Settings.showDBErrors;
            PlayerPanel();
            EditChangesApply(false);
        }

        private void PlayerPanel()
        {
            bool ok = DB.CheckConnection(false);
            cboSelectPlayer.Enabled = ok;
        }

        private void btnSelectDossierFilePath_Click(object sender, EventArgs e)
        {
            // Select dossier file
            folderBrowserDialogDossier.ShowNewFolderButton = false;

            if (txtDossierFilePath.Text == "")
            {
                folderBrowserDialogDossier.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\wargaming.net\\WorldOfTanks\\dossier_cache";
            }
            else
            {
                folderBrowserDialogDossier.SelectedPath = txtDossierFilePath.Text;
            }
            DialogResult result = folderBrowserDialogDossier.ShowDialog();
            // If file selected save config with new values
            if (folderBrowserDialogDossier.SelectedPath != "" && result != DialogResult.Cancel)
            {
                txtDossierFilePath.Text = folderBrowserDialogDossier.SelectedPath;
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            SaveChanges();
        }

        public void SaveChanges()
        {
            // Dossier File path
            if (Directory.Exists(txtDossierFilePath.Text))
            {
                Config.Settings.dossierFilePath = txtDossierFilePath.Text;
                Config.Settings.dossierFileWathcherRun = 1;
            }
            // Show DB errors (debug mode)
            Config.Settings.showDBErrors = chkShowDBError.Checked;
            // Player
            Config.Settings.playerNameAndServer = cboSelectPlayer.Text;
            DataTable dt = DB.FetchData("SELECT id FROM player WHERE name='" + cboSelectPlayer.Text + "'", Config.Settings.showDBErrors);
            if (dt.Rows.Count > 0)
            {
                int playerId = 0;
                if (dt.Rows[0][0] != DBNull.Value)
                    playerId = Convert.ToInt32(dt.Rows[0][0]);
                Config.Settings.playerId = playerId;
            }
            // vBAddict settings
            vBAddictHelper.GetSettings();
            // Download file path and settings
            // Download path and settings
            Config.Settings.downloadFilePath = txtDownloadFilePath.Text;
            Config.Settings.downloadFilePathAddSubfolder = chkCreateDownloadSubFolders.Checked;
            // Save
            string msg = "";
            bool saveOk = false;
            saveOk = Config.SaveConfig(out msg);
            if (saveOk)
            {
                // MsgBox.Show(msg, "Application settings saved", (Form)this.TopLevelControl);
                //((Form)this.TopLevelControl).Close();
                EditChangesApply(false);
            }
            else
            {
                MsgBox.Show(msg, "Error saving application settings", (Form)this.TopLevelControl);
            }
        }

        private void btnDbSetting_Click(object sender, EventArgs e)
        {
            // Check first for valid dossier path
            if (Directory.Exists(txtDossierFilePath.Text))
            {
                Config.Settings.dossierFilePath = txtDossierFilePath.Text;
                Form frm = new Forms.DatabaseSetting();
                frm.ShowDialog();
                DataBind();
                // Refresh();
            }
            else
            {
                Code.MsgBox.Show("No valid path to dossier file is found, please select dossier file path before selecting Database Settings.", "Incorrect Dossier File Path", (Form)this.TopLevelControl);
            }
        }

        private static string currentSelectedPlayer = "";
        private void cboSelectPlayer_Click(object sender, EventArgs e)
        {
            currentSelectedPlayer = cboSelectPlayer.Text;
            Code.DropDownGrid.Show(cboSelectPlayer, Code.DropDownGrid.DropDownGridType.Sql, "SELECT name FROM player ORDER BY name");
        }

        private void EditChangesApply(bool changesApplied)
        {
            AppSettingsHelper.ChangesApplied = changesApplied;
            btnCancel.Enabled = changesApplied;
            btnSave.Enabled = changesApplied;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataBind();
            EditChangesApply(false);
        }

        private void txtDossierFilePath_TextChanged(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

        private void chkShowDBError_Click(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

        private void cboSelectPlayer_TextChanged(object sender, EventArgs e)
        {
            if (currentSelectedPlayer != cboSelectPlayer.Text)
                EditChangesApply(true);
        }

        private void btnSelectDownloadFilePath_Click(object sender, EventArgs e)
        {
            // Select download path
            folderBrowserDialogDossier.ShowNewFolderButton = false;

            if (txtDownloadFilePath.Text == "")
            {
                folderBrowserDialogDossier.SelectedPath = Config.AppDataDownloadFolder;
            }
            else
            {
                folderBrowserDialogDossier.SelectedPath = txtDownloadFilePath.Text;
            }
            DialogResult result = folderBrowserDialogDossier.ShowDialog();
            // If file selected save config with new values
            if (folderBrowserDialogDossier.SelectedPath != "" && result != DialogResult.Cancel)
            {
                txtDownloadFilePath.Text = folderBrowserDialogDossier.SelectedPath;
            }
        }

        private void txtDownloadFilePath_TextChanged(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

        private void chkCreateDownloadSubFolders_Click(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

        private void btnMergePlayers_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Settings.MergePlayers();
            frm.ShowDialog();
        }
    }
}
