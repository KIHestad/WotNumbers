using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
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
using WinApp.Code.FormView;

namespace WinApp.Forms
{
	public partial class DatabaseNew : FormCloseOnEsc
    {
		private bool _autoSetup = false;
		public DatabaseNew(bool autoSetup = false)
		{
			InitializeComponent();
			_autoSetup = autoSetup;
		}

		private async void frmDatabaseNew_Load(object sender, EventArgs e)
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
				txtFileLocation.Text = await GetMSSQLDefualtFileLocation();
			}
			else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
			{
				DatabaseNewTheme.Text = "Create New SQLite Database";
				txtFileLocation.Text = Config.AppDataDBFolder;
			}
		}

		private async void DatabaseNew_Shown(object sender, EventArgs e)
		{
			if (_autoSetup)
			{
                AutoSetupHelper.AutoSetupCompleteOK = await CreateNewDb();
				this.Close();
			}
		}

		private async Task<string> GetMSSQLDefualtFileLocation()
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
				await Log.LogToFile(ex);
				// throw;
			}
			return folder;
		}

		private void UpdateProgressBar(string statusText)
		{
			lblStatusText.Text = statusText;
			badProgressBar.Value++;
			Refresh();
		}

		private async void btnCreateDB_Click(object sender, EventArgs e)
		{
			bool ok = await CreateNewDb();
            if (ok) {
                this.Close();
            }
		}
		
		private async Task<bool> CreateNewDb()
		{
			// Wait cursor
			DatabaseNewTheme.Cursor = Cursors.WaitCursor;
			btnCancel.Enabled = false;
			btnCreateDB.Enabled = false;
			btnSelectFile.Enabled = false;
			txtDatabasename.Enabled = false;
			txtFileLocation.Enabled = false;
			// Create new db
			badProgressBar.ValueMax = 8;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;
			UpdateProgressBar("Creating new database");
			// Create db now
			string createDbResult = await DB.CreateDatabase(txtDatabasename.Text, txtFileLocation.Text, Config.Settings.databaseType);
            if (createDbResult != null)
            {
                // Revert to prevous settings
                MsgBox.Show($"Failed to create database. Error: {createDbResult}", "Failed to create database", this);
                return false;
            }
            // successfully created db, continue to fill database with default data
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
			bool ok = await DB.ExecuteNonQuery(sql);
			if (ok)
			{
				// Insert default data
				UpdateProgressBar("Inserting data into database");
				streamReader = new StreamReader(path + "insert_dbver488.txt", Encoding.UTF8);
				sql = streamReader.ReadToEnd();
				ok = await DB.ExecuteNonQuery(sql, RunInBatch: true);
                if (ok)
                {
                    // Upgrade to latest version
                    UpdateProgressBar("Upgrading database");
                    ok = await DBVersion.CheckForDbUpgrade(this, true);
                }
                if (ok)
                {
                    // Get tanks, remember to init tankList first
                    UpdateProgressBar("Retrieves tanks from Wargaming API");
                    // OLD METHOD, still in use because some tanks are missing from the new method
                    await TankHelper.GetTankList();
                    await ImportWotApi2DB.ImportTanksOldAPI(this);
                    // NEW METHOD
                    await TankHelper.GetTankList(); // Init after getting tanks before next tank list fetch
                    ok = (await ImportWotApi2DB.ImportTanks(this)) == null;
                    // Init after getting tanks and other basic data import
                    await TankHelper.GetTankList();
                }
                if (ok)
                {
                    await TankHelper.GetJson2dbMappingFromDB();
                }

				// Get turret
				//UpdateProgressBar("Retrieves tank turrets from Wargaming API");
				//ImportWotApi2DB.ImportTurrets(this);

				// Get guns
				//UpdateProgressBar("Retrieves tank guns from Wargaming API");
				//ImportWotApi2DB.ImportGuns(this);

				// Get radios
				//UpdateProgressBar("Retrieves tank radios from Wargaming API");
				//ImportWotApi2DB.ImportRadios(this);

				// Get achievements
				//UpdateProgressBar("Retrieves achievements from Wargaming API");
				//await ImportWotApi2DB.ImportAchievements(this);
                //await TankHelper.GetAchList();

                 if (ok)
                {
                    // Get WN8 ratings
                    UpdateProgressBar("Retrieves WN8 expected values from API");
                    var result = await ImportWN8Api2DB.UpdateWN8(this);
                    ok = result.Success;
                }
					
                if (ok)
                {
                    // Get WN8 ratings
                    UpdateProgressBar("Retrieves WN9 expected values from API");
                    var result = await ImportWN9Api2DB.UpdateWN9(this);
                    ok = result.Success;
                }
                    
                if (ok)
                {
                    // Create default col list setups
                    await ColListSystemDefault.NewSystemTankColList();
                    await ColListSystemDefault.NewSystemBattleColList();

                    // Update settings 
                    DBVersion.RunDownloadAndUpdateTanks = false;
                    DBVersion.RunDossierFileCheckWithForceUpdate = true;
                    Config.Settings.doneRunWotApi = DateTime.Now;

                    // Reset player
                    Config.Settings.playerName = "";
                    Config.Settings.playerServer = "";
                    Config.Settings.playerId = 0;

                    // New Init after upgrade db
                    await TankHelper.GetAllLists();

                    // Startup with default settings
                    MainSettings.GridFilterTank = await GridFilter.GetDefault(GridView.Views.Tank);
                    MainSettings.GridFilterBattle = await GridFilter.GetDefault(GridView.Views.Battle);

                }
			}
			// Done
			DatabaseNewTheme.Cursor = Cursors.Default;
            if (ok)
            {
                UpdateProgressBar("Database created successfully");
                // Save new database to config
                if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
                    Config.Settings.databaseName = txtDatabasename.Text;
                else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
                    Config.Settings.databaseFileName = txtFileLocation.Text + txtDatabasename.Text + ".db";
                MsgBox.Show("Database created successfully, new database saved to settings.", "Created database", this);
                await Config.SaveConfig();
            }
            else
            {
                UpdateProgressBar("Error occured during database creation");
            }
            return ok;
		}


		private async void cmdSelectFile_Click(object sender, EventArgs e)
		{
			// Select dossier file
			folderBrowserDialogDBPath.ShowNewFolderButton = false;

			if (txtFileLocation.Text == "")
			{
				if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
					folderBrowserDialogDBPath.SelectedPath = Config.AppDataDBFolder;
				else if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
					folderBrowserDialogDBPath.SelectedPath = await GetMSSQLDefualtFileLocation();
			}
			else
			{
				folderBrowserDialogDBPath.SelectedPath = txtFileLocation.Text;
			}
            DialogResult result = folderBrowserDialogDBPath.ShowDialog();
			// If file selected save config with new values
            if (result == DialogResult.OK)
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
