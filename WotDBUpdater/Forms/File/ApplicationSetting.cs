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
			// Startup settings
			txtDossierFilePath.Text = Config.Settings.dossierFilePath;
			// Database type
			if (Config.Settings.databaseType == dbType.MSSQLserver)
				cboDatabaseType.Text = "MS SQL Server";
			else if (Config.Settings.databaseType == dbType.SQLite)
				cboDatabaseType.Text = "SQLite";
			// Player
			cboSelectPlayer.Text = Config.Settings.playerName;
		}

		
		private void btmAddPlayer_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.AddPlayer();
			frm.ShowDialog();
			cboSelectPlayer.Text = Config.Settings.playerName;
		}

		private void btnRemovePlayer_Click_1(object sender, EventArgs e)
		{
			DialogResult result = MessageBoxEx.Show(this, "Are you sure you want to remove player: " + cboSelectPlayer.Text + " ?", "Remove player", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				try
				{
					SqlConnection con = new SqlConnection(Config.DatabaseConnection());
					con.Open();
					SqlCommand cmd = new SqlCommand("DELETE FROM player WHERE name=@name", con);
					cmd.Parameters.AddWithValue("@name", cboSelectPlayer.Text);
					cmd.ExecuteNonQuery();
					con.Close();
					MessageBoxEx.Show(this, "Player successfully removed.", "Player removed");
					cboSelectPlayer.Text = "";
				}
				catch (Exception ex)
				{
					MessageBoxEx.Show(this, "Cannot remove this player, probaly because data is stored for the player. Only players without any data can be removed.\n\n" + ex.Message, "Cannot remove player");
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
			// Database type
			if (cboDatabaseType.Text == "MS SQL Server")
				Config.Settings.databaseType = dbType.MSSQLserver;
			else if (cboDatabaseType.Text == "SQLite")
				Config.Settings.databaseType = dbType.SQLite;
			// Other
			Config.Settings.dossierFilePath = txtDossierFilePath.Text;
			Config.Settings.playerName = cboSelectPlayer.Text;
			string msg = "";
			bool saveOk = false;
			saveOk = Config.SaveAppConfig(out msg);
			MessageBoxEx.Show(this, msg, "Application settings saved");
			if (saveOk)
			{
				Form.ActiveForm.Close();
			}
		}

		private void btnDbSetting_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.DatabaseSetting();
			frm.ShowDialog();
		}

		private void cboSelectPlayer_Click(object sender, EventArgs e)
		{
			cboSelectPlayer.Text = Code.PopupGrid.Show("Select Player", Code.PopupDataSourceType.Sql, "SELECT name FROM player ORDER BY name");
		}


	}
}
