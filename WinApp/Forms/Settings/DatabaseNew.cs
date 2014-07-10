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
using WinApp.Code;

namespace WinApp.Forms
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
			txtFileLocation.Text = Config.AppDataDBFolder;
		}

		private void UpdateProgressBar(string statusText)
		{
			lblStatusText.Text = statusText;
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
				Code.MsgBox.Show("Failed to create database, revert to using previous settings.", "Failed to create database");
				Config.Settings = Config.LastWorkingSettings;
			}
			Config.SaveConfig(out result);
			this.Close();					
		}
		
		private bool CreateNewDb()
		{
			bool ok = true;
			badProgressBar.ValueMax = 13;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;
			UpdateProgressBar("Creating new database");
			// Create db now
			ok = DB.CreateDatabase(txtDatabasename.Text, txtFileLocation.Text, Config.Settings.databaseType);
			if (ok)
			{
				// Fill database with default data
				UpdateProgressBar("Creating database tables");
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
				
				// Insert default data
				UpdateProgressBar("Inserting data into database");
				streamReader = new StreamReader(path + "insert.txt", Encoding.UTF8);
				sql = streamReader.ReadToEnd();
				ok = DB.ExecuteNonQuery(sql);
				if (!ok) return false;

				// Get tanks, remember to init tankList first
				UpdateProgressBar("Retrieves tanks from Wargaming API");
				TankData.GetTankList();
				ImportWotApi2DB.ImportTanks();
				// Init after getting tanks and other basic data import
				TankData.GetTankList();
				TankData.GetJson2dbMappingFromDB();

				// Get turret
				UpdateProgressBar("Retrieves tank turrets from Wargaming API");
				ImportWotApi2DB.ImportTurrets();

				// Get guns
				UpdateProgressBar("Retrieves tank guns from Wargaming API");
				ImportWotApi2DB.ImportGuns();

				// Get radios
				UpdateProgressBar("Retrieves tank radios from Wargaming API");
				ImportWotApi2DB.ImportRadios();

				// Get achievements
				UpdateProgressBar("Retrieves achievements from Wargaming API");
				ImportWotApi2DB.ImportAchievements();
				TankData.GetAchList();

				// Get WN8 ratings
				UpdateProgressBar("Retrieves WN8 expected values from API");
				ImportWN8Api2DB.UpdateWN8();

				// Reset player
				Config.Settings.playerName = "";
				Config.Settings.playerId = 0;
				
				// Upgrade to latest version
				UpdateProgressBar("Upgrading database");
				DBVersion.CheckForDbUpgrade();
				// New Init after upgrade db
				TankData.GetAllLists();
				
				// Get initial dossier 
				UpdateProgressBar("Running initial dossier file check, please wait...");
				dossier2json.ManualRun();
				UpdateProgressBar("");
			}
			return ok;
		}


		private void cmdSelectFIle_Click(object sender, EventArgs e)
		{
			// Select dossier file
			folderBrowserDialogDBPath.ShowNewFolderButton = false;

			if (txtFileLocation.Text == "")
			{
				folderBrowserDialogDBPath.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\wargaming.net\\WorldOfTanks\\dossier_cache";
			}
			else
			{
				folderBrowserDialogDBPath.SelectedPath = txtFileLocation.Text;
			}
			folderBrowserDialogDBPath.ShowDialog();
			// If file selected save config with new values
			if (folderBrowserDialogDBPath.SelectedPath != "")
			{
				txtFileLocation.Text = folderBrowserDialogDBPath.SelectedPath;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}


	}
}
