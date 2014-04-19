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
using WotDBUpdater.Code;

namespace WotDBUpdater.Forms.File
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
			cboSelectPlayer.Text = Config.Settings.playerName;
			PlayerPanel();
		}

		private void PlayerPanel()
		{
			bool ok = db.CheckConnection(false);
			lblPlayer.Dimmed = !ok;
			cboSelectPlayer.Enabled = ok;
			btnAddPlayer.Enabled = ok;
			btnRemovePlayer.Enabled = ok;
		}

		private void btmAddPlayer_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.AddPlayer();
			frm.ShowDialog();
			cboSelectPlayer.Text = Config.Settings.playerName;
		}

		private void btnRemovePlayer_Click_1(object sender, EventArgs e)
		{
			Code.MsgBox.Button result = Code.MsgBox.Show("Are you sure you want to remove player: " + cboSelectPlayer.Text + " ?", "Remove player", Code.MsgBoxType.OKCancel);
			if (result == Code.MsgBox.Button.OKButton)
			{
				try
				{
					SqlConnection con = new SqlConnection(Config.DatabaseConnection());
					con.Open();
					SqlCommand cmd = new SqlCommand("DELETE FROM player WHERE name=@name", con);
					cmd.Parameters.AddWithValue("@name", cboSelectPlayer.Text);
					cmd.ExecuteNonQuery();
					con.Close();
					Code.MsgBox.Show("Player successfully removed.", "Player removed");
					cboSelectPlayer.Text = "";
					Refresh();
				}
				catch (Exception ex)
				{
					Code.MsgBox.Show("Cannot remove player, are you sure a player is selected? It is not possible to remove player that has saved related data, only players without any data can be removed." + 
						Environment.NewLine + Environment.NewLine + ex.Message, "Cannot remove player");
				}
			}
		}

		private void btnSelectDossierFilePath_Click(object sender, EventArgs e)
		{
			// Select dossier file
			openFileDialogDossierFile.FileName = "*.dat";
			if (txtDossierFilePath.Text == "")
			{
				openFileDialogDossierFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\wargaming.net\\WorldOfTanks\\dossier_cache";
			}
			else
			{
				openFileDialogDossierFile.InitialDirectory = txtDossierFilePath.Text;
			}
			openFileDialogDossierFile.ShowDialog();
			// If file selected save config with new values
			if (openFileDialogDossierFile.FileName != "")
			{
				txtDossierFilePath.Text = Path.GetDirectoryName(openFileDialogDossierFile.FileName);
			}
		}

		private void btnSave_Click_1(object sender, EventArgs e)
		{
			Config.Settings.dossierFilePath = txtDossierFilePath.Text;
			Config.Settings.playerName = cboSelectPlayer.Text;
			DataTable dt = db.FetchData("SELECT id FROM player WHERE name='" + cboSelectPlayer.Text + "'");
			if (dt.Rows.Count > 0)
			{
				int playerId = 0;
				if (dt.Rows[0][0] != DBNull.Value)
					playerId = Convert.ToInt32(dt.Rows[0][0]);
				Config.Settings.playerId = playerId;
			}
			string msg = "";
			bool saveOk = false;
			saveOk = Config.SaveConfig(out msg);
			if (saveOk)
			{
				MsgBox.Show(msg, "Application settings saved");
				Form.ActiveForm.Close();
			}
			else
			{
				MsgBox.Show(msg, "Error saving application settings");
			}
		}

		private void btnDbSetting_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.DatabaseSetting();
			frm.ShowDialog();
			PlayerPanel();
		}

		private void cboSelectPlayer_Click(object sender, EventArgs e)
		{
			string newValue = Code.PopupGrid.Show("Select Player", Code.PopupGrid.PopupGridType.Sql, "SELECT name FROM player ORDER BY name");
			if (Code.PopupGrid.ValueSelected) cboSelectPlayer.Text = newValue;
		}

		private void ApplicationSetting_Activated(object sender, EventArgs e)
		{
			UpdateSettings();
		}

	}
}
