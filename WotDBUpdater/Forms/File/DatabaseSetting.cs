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
	public partial class DatabaseSetting : Form
	{
		public DatabaseSetting()
		{
			InitializeComponent();
		}

		private void DatabaseSetting_Load(object sender, EventArgs e)
		{
			LoadConfig();
		}

		private void LoadConfig()
		{
			// DB Type
			if (Config.Settings.databaseType ==ConfigData.dbType.MSSQLserver)
				popupDatabaseType.Text = "MS SQL Server";
			else if (Config.Settings.databaseType ==ConfigData.dbType.SQLite)
				popupDatabaseType.Text = "SQLite";
			// Startup settings SQLite
			txtDatabaseFile.Text = Config.Settings.databaseFileName;
			// Startup settings MS SQL Server
			txtServerName.Text = Config.Settings.databaseServer;
			popupDatabase.Text = Config.Settings.databaseName;
			if (Config.Settings.databaseWinAuth)
				popupDbAuth.Text = "Windows Authentication";
			else
				popupDbAuth.Text = "SQL Server Authentication";
			txtUID.Text = Config.Settings.databaseUid;
			txtPW.Text = Config.Settings.databasePwd;
			// UpdateAuthSettings
			UpdateLogin();
		}

		private void UpdateLogin()
		{
			bool enabled = (popupDbAuth.Text == "SQL Server Authentication");
			lblUIDPW.Dimmed = !enabled;
			txtUID.Enabled = enabled;
			txtPW.Enabled = enabled;
			Refresh();
		}

		private void popupDatabase_Click(object sender, EventArgs e)
		{
			// Do not use standard conn here, supply alternate sql conn string
			string winAuth = "Win";
			if (popupDbAuth.Text == "SQL Server Authentication") winAuth = "Sql";
			string connectionstring = Config.DatabaseConnection(txtServerName.Text, "master", winAuth, txtUID.Text, txtPW.Text, 10, true, ConfigData.dbType.MSSQLserver);
			string sql = "SELECT [name] FROM master.dbo.sysdatabases WHERE dbid > 4 and [name] <> 'ReportServer' and [name] <> 'ReportServerTempDB'";
			try
			{
				SqlConnection con = new SqlConnection(connectionstring);
				con.Open();
				SqlCommand command = new SqlCommand(sql, con);
				SqlDataAdapter adapter = new SqlDataAdapter(command);
				DataTable dt = new DataTable();
				adapter.Fill(dt);
				con.Close();
				string dbList = "";
				foreach (DataRow dr in dt.Rows)
				{
					dbList += dr["name"] + ",";
				}
				if (dbList.Length > 0)
				{
					dbList = dbList.Substring(0, dbList.Length - 1);
					popupDatabase.Text = Code.PopupGrid.Show("Select Database", Code.PopupGrid.PopupGridType.List, dbList);
				}
			}
			catch (Exception ex)
			{
				MsgBox.Show("Error connecting to database, please check server name and authentication" + Environment.NewLine + Environment.NewLine + ex.Message,"Database error");
			}
			
		}
			

		private void btnSave_Click_1(object sender, EventArgs e)
		{
			// Save Db Type
			if (popupDatabaseType.Text == "MS SQL Server")
				Config.Settings.databaseType = ConfigData.dbType.MSSQLserver;
			else if (popupDatabaseType.Text == "SQLite")
				Config.Settings.databaseType = ConfigData.dbType.SQLite;
			// Save SQLite settings
			Config.Settings.databaseFileName = txtDatabaseFile.Text;
			// Save SQL Server settings
			Config.Settings.databaseServer = txtServerName.Text;
			Config.Settings.databaseWinAuth = (popupDbAuth.Text == "Windows Authentication");
			Config.Settings.databaseUid = txtUID.Text;
			Config.Settings.databasePwd = txtPW.Text;
			Config.Settings.databaseName = popupDatabase.Text;
			// Save now
			string msg = "";
			bool saveOk = false;
			saveOk = Config.SaveConfig(out msg);
			// Check if Connection to DB is OK, and get base data
			string winAuth = "Win";
			if (popupDbAuth.Text == "SQL Server Authentication") winAuth = "Sql";
			if (Config.CheckDBConn(true, txtServerName.Text, popupDatabase.Text, winAuth, txtUID.Text, txtPW.Text)) // check db config, displays message if error
			{
				// Init
				TankData.GetTankListFromDB();
				TankData.GetJson2dbMappingViewFromDB();
				TankData.GettankData2BattleMappingViewFromDB();
				Code.MsgBox.Show("Database settings successfully saved", "Saved Database Settings");
				this.Close();
			}
		}

		private void btnNewDb_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.DatabaseNew();
			frm.ShowDialog();
			LoadConfig();
		}

		private void cmdSQLiteDatabaseFile_Click(object sender, EventArgs e)
		{
			// Select dossier file
			openFileDialogSQLite.FileName = "*.db";
			openFileDialogSQLite.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath) + "\\Database\\";
			openFileDialogSQLite.ShowDialog();
			if (openFileDialogSQLite.FileName != "")
			{
				txtDatabaseFile.Text = openFileDialogSQLite.FileName;
			}
		}

		private void popupDbAuth_Click(object sender, EventArgs e)
		{
			popupDbAuth.Text =
				Code.PopupGrid.Show("Select SQL Server Authentication", 
					Code.PopupGrid.PopupGridType.List, 
					"Windows Authentication,SQL Server Authentication");
			UpdateLogin();
		}

		private void popupDatabaseType_Click(object sender, EventArgs e)
		{
			popupDatabaseType.Text = Code.PopupGrid.Show("Select Database Type", Code.PopupGrid.PopupGridType.List,"SQLite,MS SQL Server");
		}

		
	}
}
