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
					string newValue = Code.PopupGrid.Show("Select Database", Code.PopupGrid.PopupGridType.List, dbList);
					if (Code.PopupGrid.ValueSelected) popupDatabase.Text = newValue;
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
			Config.Settings.databaseType = selectedDbType;
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
				// Check if current plyer exists in current database, if not remove it
				DataTable dt = db.FetchData("SELECT * FROM player WHERE name='" + Config.Settings.playerName + "'");
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
				Config.SaveConfig(out msg);
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
			// If SQL Server, check if database settings is OK
			bool ok = true;
			if (selectedDbType == ConfigData.dbType.MSSQLserver)
			{
				// Do not use standard conn here, supply alternate sql conn string
				string winAuth = "Win";
				if (popupDbAuth.Text == "SQL Server Authentication") winAuth = "Sql";
				string connectionstring = Config.DatabaseConnection(txtServerName.Text, "master", winAuth, txtUID.Text, txtPW.Text, 10, true, ConfigData.dbType.MSSQLserver);
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
					MsgBox.Show("Error connecting to database, please check server name and authentication" + Environment.NewLine + Environment.NewLine + ex.Message, "Database error");
					ok = false;
				}
			}
			if (ok)
			{
				Form frm = new Forms.File.DatabaseNew(selectedDbType);
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
			string newValue = Code.PopupGrid.Show("Select SQL Server Authentication", Code.PopupGrid.PopupGridType.List, "Windows Authentication,SQL Server Authentication");
			if (Code.PopupGrid.ValueSelected) popupDbAuth.Text = newValue;
			UpdateLogin();
		}

		private void popupDatabaseType_Click(object sender, EventArgs e)
		{
			string newValue = Code.PopupGrid.Show("Select Database Type", Code.PopupGrid.PopupGridType.List, "SQLite,MS SQL Server");
			if (Code.PopupGrid.ValueSelected) popupDatabaseType.Text = newValue;
			// DB Type
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
