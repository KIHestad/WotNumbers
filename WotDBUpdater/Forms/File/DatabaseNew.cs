using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WotDBUpdater.Code;

namespace WotDBUpdater.Forms.File
{
	public partial class DatabaseNew : Form
	{
		public DatabaseNew()
		{
			InitializeComponent();
		}

		private void frmDatabaseNew_Load(object sender, EventArgs e)
		{
			if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
				DatabaseNewTheme.Text = "Create New MS SQL Server Database";
			else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
				DatabaseNewTheme.Text = "Create New SQLite Database";
			txtPlayerName.Text = Config.Settings.playerName;
			txtFileLocation.Text = Path.GetDirectoryName(Application.ExecutablePath) + "\\Database\\";
		}

		private void UpdateProgressBar()
		{
			badProgressBar.Value++;
			Refresh();
			Application.DoEvents();
		}

		private void btnCreateDB_Click(object sender, EventArgs e)
		{
			// Wait cursor
			Cursor.Current = Cursors.WaitCursor;
			// Create new db
			bool OKcreated = CreateNewDb();
			// Done
			Cursor.Current = Cursors.Default;
			badProgressBar.Visible = false;
			Application.DoEvents();
			string result = "";
			if (OKcreated)
			{
				// Save new database to config
				if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
					Config.Settings.databaseName = txtDatabasename.Text;
				else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
					Config.Settings.databaseFileName = txtFileLocation.Text + txtDatabasename.Text + ".db";
				Code.MsgBox.Show("Database created successfully, new database saved to settings.", "Created database");
			}
			else
			{
				// Revert to prevous settings
				Code.MsgBox.Show("Failed to create database, revert to using previous database.", "Failed to create database");
				Config.Settings = Config.LastWorkingSettings;
			}
			Config.SaveConfig(out result);
			this.Close();		
		}
		
		private bool CreateNewDb()
		{
			bool ok = true;
			badProgressBar.ValueMax = 12;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;
			UpdateProgressBar();
			// Create db now
			if (DB.CreateDatabase(txtDatabasename.Text, txtFileLocation.Text, Config.Settings.databaseType))
			{
				// Fill database with default data
				UpdateProgressBar();
				// Update db by running sql scripts
				string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\Database\\";
				string sql;

				// Create Tables
				string filename = "";
				if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
					filename = "createTableMSSQL.txt";
				else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
					filename = "createTableSQLite.txt";
				StreamReader streamReader = new StreamReader(path + filename, Encoding.UTF8);
				sql = streamReader.ReadToEnd();
				ok = DB.ExecuteNonQuery(sql);
				if (!ok) return false;
				UpdateProgressBar();

				// Insert default data
				streamReader = new StreamReader(path + "insert.txt", Encoding.UTF8);
				sql = streamReader.ReadToEnd();
				ok = DB.ExecuteNonQuery(sql);
				if (!ok) return false;
				UpdateProgressBar();

				// Get tanks, remember to init tankList first
				TankData.GetTankListFromDB();
				ImportWotApi2DB.ImportTanks();
				// Init after getting tanks and other basic data import
				TankData.GetTankListFromDB();
				TankData.GetJson2dbMappingFromDB();
				UpdateProgressBar();

				// Get turret
				ImportWotApi2DB.ImportTurrets();
				UpdateProgressBar();

				// Get guns
				ImportWotApi2DB.ImportGuns();
				UpdateProgressBar();

				// Get radios
				ImportWotApi2DB.ImportRadios();
				UpdateProgressBar();

				// Get achievements
				ImportWotApi2DB.ImportAchievements();
				UpdateProgressBar();

				// Get WN8 ratings
				ImportWN8Api2DB.UpdateWN8();
				UpdateProgressBar();

				// Add player
				if (txtPlayerName.Text.Trim() != "")
				{
					ok = DB.ExecuteNonQuery("INSERT INTO player (name) VALUES ('" + txtPlayerName.Text.Trim() + "')");
					Config.Settings.playerName = txtPlayerName.Text.Trim();
					Config.Settings.playerId = 1;
				}
				else
				{
					Config.Settings.playerName = "";
					Config.Settings.playerId = 0;
				}
				UpdateProgressBar();

				// Upgrade to latest version
				DBVersion.CheckForDbUpgrade();
				UpdateProgressBar();
			}
			return ok;
		}


		private void cmdSelectFIle_Click(object sender, EventArgs e)
		{
			// Select dossier file
			openFileDialog.FileName = "*.db";
			openFileDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath) + "\\Database\\";
			openFileDialog.ShowDialog();
			if (openFileDialog.FileName != "*.db" && openFileDialog.FileName != "")
			{
				txtFileLocation.Text = openFileDialog.FileName;
			}
		}


	}
}
