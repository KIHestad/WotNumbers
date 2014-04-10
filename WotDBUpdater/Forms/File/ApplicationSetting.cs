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
			// Players
			UpdatePlayerList();
		}

		private void UpdatePlayerList()
		{
			try
			{
				cboPlayer.Items.Clear();
				using (SqlConnection con = new SqlConnection(Config.DatabaseConnection()))
				{
					con.Open();
					string sql = "SELECT * FROM player";
					SqlCommand cmd = new SqlCommand(sql, con);
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						cboPlayer.Items.Add(reader["name"]);
					}
					con.Close();
				}
				cboPlayer.SelectedIndex = cboPlayer.FindString(Config.Settings.playerName);
			}
			catch (Exception)
			{
				// none
			}
		}

		private void btnOpenDossierFile_Click(object sender, EventArgs e)
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

		private void btnSave_Click(object sender, EventArgs e)
		{
			// Database type
			if (cboDatabaseType.Text == "MS SQL Server")
				Config.Settings.databaseType = dbType.MSSQLserver;
			else if (cboDatabaseType.Text == "SQLite")
				Config.Settings.databaseType = dbType.SQLite;
			// Other
			Config.Settings.dossierFilePath = txtDossierFilePath.Text;
			Config.Settings.playerName = cboPlayer.Text;
			string msg = "";
			bool saveOk = false;
			saveOk = Config.SaveAppConfig(out msg);
			MessageBoxEx.Show(this, msg, "Application settings saved");
			if (saveOk) 
			{
				Form.ActiveForm.Close();
			}
		}

		private void btnAddPlayer_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.AddPlayer();
			frm.ShowDialog();
			UpdatePlayerList();
		}

		private void btnRemovePlayer_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBoxEx.Show(this, "Are you sure you want to remove player: " + cboPlayer.Text + " ?", "Remove player", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				try
				{
					SqlConnection con = new SqlConnection(Config.DatabaseConnection());
					con.Open();
					SqlCommand cmd = new SqlCommand("DELETE FROM player WHERE name=@name", con);
					cmd.Parameters.AddWithValue("@name", cboPlayer.Text);
					cmd.ExecuteNonQuery();
					con.Close();
					MessageBoxEx.Show(this, "Player successfully removed.", "Player removed");
					UpdatePlayerList();
				}
				catch (Exception ex)
				{
					MessageBoxEx.Show(this, "Cannot remove this player, probaly because data is stored for the player. Only players without any data can be removed.\n\n" + ex.Message, "Cannot remove player");
				}
			}
		}

		private void btnDatabaseSettings_Click(object sender, EventArgs e)
		{
			
		}


	}
}
