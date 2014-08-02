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
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class ApplicationSetting : Form
	{
		public ApplicationSetting()
		{
			InitializeComponent();
		}

		private void frmDossierFileSelect_Load(object sender, EventArgs e)
		{
			UpdateSettings();
		}

		private void UpdateSettings()
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
			// Player
			cboSelectPlayer.Text = Config.Settings.playerNameAndServer;
			chkShowDBError.Checked = Config.Settings.showDBErrors;
			PlayerPanel();
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
			folderBrowserDialogDossier.ShowDialog();
			// If file selected save config with new values
			if (folderBrowserDialogDossier.SelectedPath != "")
			{
				txtDossierFilePath.Text = folderBrowserDialogDossier.SelectedPath;
			}
		}

		private void btnSave_Click_1(object sender, EventArgs e)
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
			
			// Save
			string msg = "";
			bool saveOk = false;
			saveOk = Config.SaveConfig(out msg);
			if (saveOk)
			{
				MsgBox.Show(msg, "Application settings saved", this);
				this.Close();
			}
			else
			{
				MsgBox.Show(msg, "Error saving application settings", this);
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
				PlayerPanel();
				Refresh();
			}
			else
			{
				Code.MsgBox.Show("No valid path to dossier file is found, please select dossier file path before selecting Database Settings.", "Incorrect Dossier File Path", this);
			}
		}

		private void cboSelectPlayer_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(cboSelectPlayer, Code.DropDownGrid.DropDownGridType.Sql, "SELECT name FROM player ORDER BY name");
		}

		
		private void Cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
