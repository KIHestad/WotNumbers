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
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace WinApp.Forms
{
	public partial class DatabaseNew : Form
	{
		private bool _autoSetup = false;
		public DatabaseNew(bool autoSetup = false)
		{
			InitializeComponent();
			_autoSetup = autoSetup;
		}

		private void frmDatabaseNew_Load(object sender, EventArgs e)
		{
			if (_autoSetup)
			{
				Config.Settings.databaseType = ConfigData.dbType.SQLite;
				string databaseFileName = "WotNumbers";
				string databaseFileNameSubFix = "";
				int dbNum = 0;
				while (File.Exists(Config.AppDataDBFolder + databaseFileName + databaseFileNameSubFix + ".db"))
				{
					dbNum++;
					databaseFileNameSubFix = dbNum.ToString();
				}
				txtDatabasename.Text = databaseFileName + databaseFileNameSubFix;
				btnCancel.Enabled = false;
				btnCreateDB.Enabled = false;
				btnSelectFile.Enabled = false;
			}
			if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
			{
				DatabaseNewTheme.Text = "Create New MS SQL Server Database";
				txtFileLocation.Text = GetMSSQLDefualtFileLocation();
			}
			else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
			{
				DatabaseNewTheme.Text = "Create New SQLite Database";
				txtFileLocation.Text = Config.AppDataDBFolder;
			}
		}

		private void DatabaseNew_Shown(object sender, EventArgs e)
		{
			if (_autoSetup)
			{
				CreateNewDb();
				AutoSetupHelper.AutoSetupCompleteOK = true;
				this.Close();
			}
		}

		private string GetMSSQLDefualtFileLocation()
		{
			string folder = "";
			try
			{
				string winAuth = "Sql";
				if (Config.Settings.databaseWinAuth) winAuth = "Win";
				string connectionstring = Config.DatabaseConnection(ConfigData.dbType.MSSQLserver, "", Config.Settings.databaseServer, "master",
																	winAuth, Config.Settings.databaseUid, Config.Settings.databasePwd);
				using (var connection = new SqlConnection(connectionstring))
				{
					ServerConnection serverConnection = new ServerConnection(connection);
					Server server = new Server(serverConnection);
					string defaultDataPath = string.IsNullOrEmpty(server.Settings.DefaultFile) ? server.MasterDBPath : server.Settings.DefaultFile;
					folder = defaultDataPath;
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				// throw;
			}
			return folder;
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
			CreateNewDb();
			this.Close();
		}
		
		private void CreateNewDb()
		{
			// Wait cursor
			this.Cursor = Cursors.WaitCursor;
			DatabaseNewTheme.Cursor = Cursors.WaitCursor;
			btnCancel.Enabled = false;
			btnCreateDB.Enabled = false;
			btnSelectFile.Enabled = false;
			txtDatabasename.Enabled = false;
			txtFileLocation.Enabled = false;
			// Create new db
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
				if (ok)
				{
					// Insert default data
					UpdateProgressBar("Inserting data into database");
					streamReader = new StreamReader(path + "insert.txt", Encoding.UTF8);
					sql = streamReader.ReadToEnd();
					ok = DB.ExecuteNonQuery(sql);
					if (ok)
					{
						// Upgrade to latest version
						UpdateProgressBar("Upgrading database");
						DBVersion.CheckForDbUpgrade(this);
						
						// Get tanks, remember to init tankList first
						UpdateProgressBar("Retrieves tanks from Wargaming API");
						TankData.GetTankList();
						ImportWotApi2DB.ImportTanks(this);
						// Init after getting tanks and other basic data import
						TankData.GetTankList();
						TankData.GetJson2dbMappingFromDB();

						// Get turret
						UpdateProgressBar("Retrieves tank turrets from Wargaming API");
						ImportWotApi2DB.ImportTurrets(this);

						// Get guns
						UpdateProgressBar("Retrieves tank guns from Wargaming API");
						ImportWotApi2DB.ImportGuns(this);

						// Get radios
						UpdateProgressBar("Retrieves tank radios from Wargaming API");
						ImportWotApi2DB.ImportRadios(this);

						// Get achievements
						UpdateProgressBar("Retrieves achievements from Wargaming API");
						ImportWotApi2DB.ImportAchievements(this);
						TankData.GetAchList();

						// Get WN8 ratings
						UpdateProgressBar("Retrieves WN8 expected values from API");
						ImportWN8Api2DB.UpdateWN8(this);

						// Reset player
						Config.Settings.playerName = "";
						Config.Settings.playerId = 0;

						// New Init after upgrade db
						TankData.GetAllLists();

						// Startup with default settings
						MainSettings.GridFilterTank = GridFilter.GetDefault(GridView.Views.Tank);
						MainSettings.GridFilterBattle = GridFilter.GetDefault(GridView.Views.Battle);

						// Get initial dossier 
						UpdateProgressBar("Running initial dossier file check");
						dossier2json d2j = new dossier2json();
						d2j.ManualRunInBackground("Initial dossier file check in progress, please wait...", false);
						UpdateProgressBar("");
					}
				}
			}
			// Done
			Cursor.Current = Cursors.Default;
			badProgressBar.Visible = false;
			Application.DoEvents();
			string result = "";
			if (ok)
			{
				// Save new database to config
				if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
					Config.Settings.databaseName = txtDatabasename.Text;
				else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
					Config.Settings.databaseFileName = txtFileLocation.Text + txtDatabasename.Text + ".db";
				Code.MsgBox.Show("Database created successfully, new database saved to settings.", "Created database", this);
			}
			else
			{
				// Revert to prevous settings
				Code.MsgBox.Show("Failed to create database, revert to using previous settings.", "Failed to create database", this);
				Config.Settings = Config.LastWorkingSettings;
			}
			Config.SaveConfig(out result);
		}


		private void cmdSelectFile_Click(object sender, EventArgs e)
		{
			// Select dossier file
			folderBrowserDialogDBPath.ShowNewFolderButton = false;

			if (txtFileLocation.Text == "")
			{
				if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
					folderBrowserDialogDBPath.SelectedPath = Config.AppDataDBFolder;
				else if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
					folderBrowserDialogDBPath.SelectedPath = GetMSSQLDefualtFileLocation();
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
