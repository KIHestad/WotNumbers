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
			txtPlayerName.Text = Config.Settings.playerName;
			txtFileLocation.Text = Path.GetDirectoryName(Application.ExecutablePath) + "\\Database\\";
		}

		private void UpdateProgressBar(ref int step, int maxStep)
		{
			step++; // count step 1 to maxValue
			int maxValue = (int)((double)maxStep / ((double)step / (double)maxStep) * 100); // calculate the maxValue to be correct according to that value = 1 all time
			pbCreateDatabase.Maximum = maxValue;
			Application.DoEvents();
		}

		private void UpdateSQLiteProgressBar(ref int step, int maxStep)
		{
			step++; // count step 1 to maxValue
			int maxValue = (int)((double)maxStep / ((double)step / (double)maxStep) * 100); // calculate the maxValue to be correct according to that value = 1 all time
			pbCreateSQLiteDB.Maximum = maxValue;
			Application.DoEvents();
		}


		private bool CreateDatabase(string databaseName, string fileLocation)
		{
			bool dbOk = false;
			// Check database file location
			bool fileLocationExsits = true;
			fileLocation = fileLocation.Trim();
			if (fileLocation.Substring(fileLocation.Length-1, 1) != "\\" && fileLocation.Substring(fileLocation.Length-1, 1) != "/")
				fileLocation += "\\";
			if (!Directory.Exists(fileLocation))
			{
				DirectoryInfo prevPath = Directory.GetParent(fileLocation);
				if (!prevPath.Exists)
				{
					if (!Directory.GetParent(prevPath.FullName).Exists)
					{
						fileLocationExsits = false;
						Code.Support.MessageDark.Show("Error createing database, file parh does not exist", "Error creating database");
						pbCreateDatabase.Visible = false;
					}
					else
					{
						Directory.CreateDirectory(fileLocation);
					}
				}
			}

			if (fileLocationExsits)
			{
				SqlConnection myConn = new SqlConnection(Config.DatabaseConnection("", "master"));
				string sql = "CREATE DATABASE " + databaseName + " ON PRIMARY " +
							"(NAME = " + databaseName + ", " +
							"FILENAME = '" + fileLocation + databaseName + ".mdf', " +
							"SIZE = 5MB, FILEGROWTH = 10%) " +
							"LOG ON (NAME = " + databaseName + "_Log, " +
							"FILENAME = '" + fileLocation + databaseName + "_log.ldf', " +
							"SIZE = 1MB, " +
							"FILEGROWTH = 10%)";

				SqlCommand myCommand = new SqlCommand(sql, myConn);
				try
				{
					myConn.Open();
					myCommand.ExecuteNonQuery();
					dbOk = true;
				}
				catch (System.Exception ex)
				{
					dbOk = false;
					Code.Support.MessageDark.Show("Error creating database, check that valid databasename is selected.\n\n" + ex.ToString(), "Error creating database");
					pbCreateDatabase.Visible = false;
				}
				finally
				{
					if (dbOk)
					{
						if (myConn.State == ConnectionState.Open)
						{
							myConn.Close();
							Config.Settings.databaseName = databaseName;
							string msg = "";
							Config.SaveDbConfig(out msg);
						}
					}
				}
			}
			return dbOk;
		}

		
		private void RunSql(string sqlbatch)
		{
			string[] sql = sqlbatch.Split(new string[] {"GO"}, StringSplitOptions.RemoveEmptyEntries);
			using (SqlConnection con = new SqlConnection(Config.DatabaseConnection()))
			{
				con.Open();
				foreach (string s in sql)
				{
					SqlCommand cmd = new SqlCommand(s, con);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}


		private void RunSQLite(string sqlbatch)
		{
			/*
			string sql = File.ReadAllText("x.sql");
			string db = "Data Source=C:\\dev\\wotdb\\WotDBUpdater\\bin\\Debug\\wotdb.db";
			string dbConnection = db;
			SQLiteConnection conn = new SQLiteConnection(dbConnection);
			conn.Open();
			SQLiteCommand mycommand = new SQLiteCommand(sql, conn);
			 */
		}



		private void btnCreateDB_Click(object sender, EventArgs e)
		{
			// Check if database exists
			bool dbExists = false;
			using (SqlConnection con = new SqlConnection(Config.DatabaseConnection()))
			{
				con.Open();
				string sql = "SELECT [name] FROM master.dbo.sysdatabases WHERE [name] = '" + txtDatabasename.Text + "'";
				SqlCommand cmd = new SqlCommand(sql, con);
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows) dbExists = true;
				con.Close();
			}
			if (dbExists)
			{
				Code.Support.MessageDark.Show("Database with this name alreade exsits, choose another database name.", "Cannot create database");
			}
			else
			{
				Cursor.Current = Cursors.WaitCursor;
				int maxStep = 13;
				pbCreateDatabase.Maximum = maxStep * 100;
				pbCreateDatabase.Value = maxStep * 100;
				pbCreateDatabase.Visible = true;
				int step = 0;
				UpdateProgressBar(ref step, maxStep);
				Application.DoEvents();
				// Create db now
				if (CreateDatabase(txtDatabasename.Text, txtFileLocation.Text))
				{
					// Save new database to Config
					Config.Settings.databaseName = txtDatabasename.Text;
					string result = "";
					Config.SaveDbConfig(out result);
					// Fill database with default data
					UpdateProgressBar(ref step, maxStep);
					// Update db by running sql scripts
					string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\Database\\";
					string sql;
					// Create Tables
					StreamReader streamReader = new StreamReader(path + "createTable.txt", Encoding.UTF8);
					sql = streamReader.ReadToEnd();
					RunSql(sql);
					UpdateProgressBar(ref step, maxStep);
					// Create Views
					streamReader = new StreamReader(path + "createView.txt", Encoding.UTF8);
					sql = streamReader.ReadToEnd();
					RunSql(sql);
					UpdateProgressBar(ref step, maxStep);
					// Insert default data
					streamReader = new StreamReader(path + "insert.txt", Encoding.UTF8);
					sql = streamReader.ReadToEnd();
					RunSql(sql);
					UpdateProgressBar(ref step, maxStep);
					// Get tanks, remember to init tankList first
					TankData.GetTankListFromDB();
					Application.DoEvents();
					ImportMisc2DB.UpdateTanks();
					Application.DoEvents();
					// Init after getting tanks and other basic data import
					TankData.GetTankListFromDB();
					TankData.GetJson2dbMappingViewFromDB();
					TankData.GettankData2BattleMappingViewFromDB();
					UpdateProgressBar(ref step, maxStep);
					// Get turret
					ImportWotApi2DB.ImportTurrets();
					UpdateProgressBar(ref step, maxStep);
					// Get guns
					ImportWotApi2DB.ImportGuns();
					UpdateProgressBar(ref step, maxStep);
					// Get radios
					ImportWotApi2DB.ImportRadios();
					UpdateProgressBar(ref step, maxStep);
					// Get achievements
					ImportWotApi2DB.ImportAchievements();
					UpdateProgressBar(ref step, maxStep);
					// Get WN8 ratings
					ImportMisc2DB.UpdateWN8();
					UpdateProgressBar(ref step, maxStep);
					// Add player
					if (txtPlayerName.Text.Trim() != "")
					{
						RunSql("INSERT INTO player (name) VALUES ('" + txtPlayerName.Text.Trim() + "')");
						Config.Settings.playerName = txtPlayerName.Text.Trim();
						Config.SaveAppConfig(out result);
						UpdateProgressBar(ref step, maxStep);
					}
					UpdateProgressBar(ref step, maxStep);
					// Done creating db, save settings
					string msg = "";
					Config.SaveDbConfig(out msg);
					Config.SaveAppConfig(out msg);
					UpdateProgressBar(ref step, maxStep);
					// Done
					Cursor.Current = Cursors.Default;
					Application.DoEvents();
					Code.Support.MessageDark.Show("Database created successfully.", "Created database");
					pbCreateDatabase.Visible = false;
					Form.ActiveForm.Close();
				}
			}
		}




		private void btnCreateSQLiteDB_Click(object sender, EventArgs e)
		{
			// Check if database exists
			bool dbExists = false;
			string db = "C:\\dev\\wotdb\\WotDBUpdater\\bin\\Debug\\wotdb.db";
			SQLiteConnection.CreateFile(db);

            //string sql = File.ReadAllText("x.sql");
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + db + ";Version=3;");
            conn.Open();

            string sql = "create table testc (id int)";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            conn.Close();


			//string dbConnection = db;
			//SQLiteConnection cnn = new SQLiteConnection(dbConnection);
			//cnn.Open();
			//SQLiteCommand mycommand = new SQLiteCommand(cnn);


			/*
			{
				Cursor.Current = Cursors.WaitCursor;
				int maxStep = 14;
				pbCreateSQLiteDB.Maximum = maxStep * 100;
				pbCreateSQLiteDB.Value = maxStep * 100;
				pbCreateSQLiteDB.Visible = true;
				int step = 0;
				UpdateSQLiteProgressBar(ref step, maxStep);
				Application.DoEvents();
				
				{
					// Save new database to Config
					Config.Settings.databaseName = "wotdb.db";
					string result = "";
					//Config.SaveDbConfig(out result);
					// Fill database with default data
					UpdateProgressBar(ref step, maxStep);
					// Update db by running sql scripts
					string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\Database\\";
					string sql;
					// Create Tables
					StreamReader streamReader = new StreamReader(path + "createTable.txt", Encoding.UTF8);
					sql = streamReader.ReadToEnd();
					RunSQLite(sql);
					UpdateProgressBar(ref step, maxStep);
					// Create Views
					streamReader = new StreamReader(path + "createView.txt", Encoding.UTF8);
					sql = streamReader.ReadToEnd();
					RunSql(sql);
					UpdateProgressBar(ref step, maxStep);
					// Create Stored Procedures
					streamReader = new StreamReader(path + "createProc.txt", Encoding.UTF8);
					sql = streamReader.ReadToEnd();
					RunSql(sql);
					UpdateProgressBar(ref step, maxStep);
					// Insert default data
					streamReader = new StreamReader(path + "insert.txt", Encoding.UTF8);
					sql = streamReader.ReadToEnd();
					RunSql(sql);
					UpdateProgressBar(ref step, maxStep);
					// Get tanks, remember to init tankList first
					TankData.GetTankListFromDB();
					Application.DoEvents();
					ImportMisc2DB.UpdateTanks();
					Application.DoEvents();
					// Init after getting tanks and other basic data import
					TankData.GetTankListFromDB();
					TankData.GetJson2dbMappingViewFromDB();
					TankData.GettankData2BattleMappingViewFromDB();
					UpdateProgressBar(ref step, maxStep);
					// Get turret
					ImportWotApi2DB.ImportTurrets();
					UpdateProgressBar(ref step, maxStep);
					// Get guns
					ImportWotApi2DB.ImportGuns();
					UpdateProgressBar(ref step, maxStep);
					// Get radios
					ImportWotApi2DB.ImportRadios();
					UpdateProgressBar(ref step, maxStep);
					// Get achievements
					ImportWotApi2DB.ImportAchievements();
					UpdateProgressBar(ref step, maxStep);
					// Get WN8 ratings
					ImportMisc2DB.UpdateWN8();
					UpdateProgressBar(ref step, maxStep);
					// Add player
					if (txtPlayerName.Text.Trim() != "")
					{
						RunSql("INSERT INTO player (name) VALUES ('" + txtPlayerName.Text.Trim() + "')");
						Config.Settings.playerName = txtPlayerName.Text.Trim();
						Config.SaveAppConfig(out result);
						UpdateProgressBar(ref step, maxStep);
					}
					UpdateProgressBar(ref step, maxStep);
					// Done creating db, save settings
					string msg = "";
					Config.SaveDbConfig(out msg);
					Config.SaveAppConfig(out msg);
					UpdateProgressBar(ref step, maxStep);
					// Done
					Cursor.Current = Cursors.Default;
					Application.DoEvents();
					Code.Support.MessageDark.Show("Database created successfully.", "Created database");
					pbCreateDatabase.Visible = false;
					Form.ActiveForm.Close();
				}
			}
			 */
		}


		private void badForm1_Click(object sender, EventArgs e)
		{

		}


	}
}
