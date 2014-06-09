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
	public partial class DatabaseSetting : Form
	{
		private ConfigData.dbType selectedDbType;
		
		public DatabaseSetting()
		{
			InitializeComponent();
			this.Height = 350;
		}

		private void DatabaseSetting_Load(object sender, EventArgs e)
		{
			LoadConfig();
		}

		private void LoadConfig()
		{
			// Layout
			panelSQLite.Top = panelMSSQL.Top;
			// DB Type
			selectedDbType = Config.Settings.databaseType; // As default the selected dbtype = cfrom config
			UpateDbType();
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
		
		private void UpateDbType()
		{
			if (selectedDbType == ConfigData.dbType.MSSQLserver)
			{
				popupDatabaseType.Text = "MS SQL Server";
				panelSQLite.Visible = false;
				panelMSSQL.Visible = true;
			}

			else if (selectedDbType == ConfigData.dbType.SQLite)
			{
				popupDatabaseType.Text = "SQLite";
				panelMSSQL.Visible = false;
				panelSQLite.Visible = true;
			}
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
			string connectionstring = Config.DatabaseConnection(ConfigData.dbType.MSSQLserver, "", txtServerName.Text, "master", winAuth, txtUID.Text, txtPW.Text);
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
					Code.DropDownGrid.Show(popupDatabase, Code.DropDownGrid.DropDownGridType.List, dbList);
				}
			}
			catch (Exception ex)
			{
				MsgBox.Show("Error connecting to database, please check server name and authentication" + Environment.NewLine + Environment.NewLine + ex.Message,"Database error");
			}
			
		}
			
		private void SaveConfig()
		{
			// Get ready to save new settings to config
			string msg = "";
			bool saveOk = false;
			Config.Settings.databaseType = selectedDbType;
			// Save Db settings settings according to dbtype
			if (selectedDbType == ConfigData.dbType.SQLite)
			{
				Config.Settings.databaseFileName = txtDatabaseFile.Text;
			}
			else if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
			{
				Config.Settings.databaseServer = txtServerName.Text;
				Config.Settings.databaseWinAuth = (popupDbAuth.Text == "Windows Authentication");
				Config.Settings.databaseUid = txtUID.Text;
				Config.Settings.databasePwd = txtPW.Text;
				Config.Settings.databaseName = popupDatabase.Text;
			}
			saveOk = Config.SaveConfig(out msg);
		}

		private void btnSave_Click_1(object sender, EventArgs e)
		{
			SaveConfig();
			// Check if Connection to DB is OK, and get base data
			if (DB.CheckConnection()) // check db config, displays message if error
			{
				// Check if current plyer exists in current database, if not remove it
				DataTable dt = DB.FetchData("SELECT * FROM player WHERE name='" + Config.Settings.playerName + "'");
				if (dt.Rows.Count == 0)
				{
					Config.Settings.playerId = 0;
					Config.Settings.playerName = "";
				}
				else
				{
					int playerId = 0;
					if (dt.Rows[0]["id"] != DBNull.Value)
						playerId = Convert.ToInt32(dt.Rows[0]["id"]);
					Config.Settings.playerId = playerId;
				}
				string msg = "";
				Config.SaveConfig(out msg);
				// Init
				TankData.GetTankListFromDB();
				TankData.GetJson2dbMappingFromDB();
				// Check for upgrade
				DBVersion.CheckForDbUpgrade();
				Code.MsgBox.Show("Database settings successfully saved", "Saved Database Settings");
				this.Close();
			}
		}

		private void btnNewDb_Click(object sender, EventArgs e)
		{
			// If SQL Server, check if database settings is OK
			bool ok = true;
			if (selectedDbType == ConfigData.dbType.MSSQLserver)
			{
				// Do not use standard check / conn here, perform explicit check for SQL Server before continue creating new db
				string winAuth = "Win";
				if (popupDbAuth.Text == "SQL Server Authentication") winAuth = "Sql";
				string connectionstring = Config.DatabaseConnection(ConfigData.dbType.MSSQLserver, "", txtServerName.Text, "master", winAuth, txtUID.Text, txtPW.Text);
				string sql = "SELECT * FROM master.dbo.sysdatabases";
				try
				{
					SqlConnection con = new SqlConnection(connectionstring);
					con.Open();
					SqlCommand command = new SqlCommand(sql, con);
					SqlDataAdapter adapter = new SqlDataAdapter(command);
					DataTable dt = new DataTable();
					adapter.Fill(dt);
					con.Close();
				}
				catch (Exception ex)
				{
					MsgBox.Show("Error connecting to MS SQL Server, please check server name and authentication." + Environment.NewLine + Environment.NewLine + ex.Message, "Database error");
					ok = false;
				}
			}
			if (ok)
			{
				// Remember current settings, in case create db is not successfull, it is possible to revert
				Config.LastWorkingSettings = Config.Settings; 
				// Must save databasetype and db settings before entering form for creating new database
				// All database handling uses current config settings to access correct database
				SaveConfig();
				// Open Create new db form
				Form frm = new Forms.DatabaseNew();
				frm.ShowDialog();
				LoadConfig();
			}
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
			Code.DropDownGrid.Show(popupDbAuth, Code.DropDownGrid.DropDownGridType.List, "Windows Authentication,SQL Server Authentication");
		}

		private void popupDbAuth_TextChanged(object sender, EventArgs e)
		{
			UpdateLogin();
		}


		private void popupDatabaseType_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(popupDatabaseType, Code.DropDownGrid.DropDownGridType.List, "SQLite,MS SQL Server");
		}

		private void popupDatabaseType_TextChanged(object sender, EventArgs e)
		{
			if (popupDatabaseType.Text == "MS SQL Server")
			{
				selectedDbType = ConfigData.dbType.MSSQLserver;
			}
			else if (popupDatabaseType.Text == "SQLite")
			{
				selectedDbType = ConfigData.dbType.SQLite;
			}
			UpateDbType();
		}

		
		
	}
}
