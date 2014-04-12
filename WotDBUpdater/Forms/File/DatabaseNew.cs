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
			Cursor.Current = Cursors.WaitCursor;
			int maxStep = 12;
			pbCreateDatabase.Maximum = maxStep * 100;
			pbCreateDatabase.Value = maxStep * 100;
			pbCreateDatabase.Visible = true;
			int step = 0;
			UpdateProgressBar(ref step, maxStep);
			Application.DoEvents();
			// Create db now
			if (db.CreateDatabase(txtDatabasename.Text, txtFileLocation.Text))
			{
				// Fill database with default data
				UpdateProgressBar(ref step, maxStep);
				// Update db by running sql scripts
				string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\Database\\";
				string sql;
				// Create Tables
				StreamReader streamReader = new StreamReader(path + "createTable.txt", Encoding.UTF8);
				sql = streamReader.ReadToEnd();
				db.ExecuteNonQuery(sql);
				UpdateProgressBar(ref step, maxStep);
				// Create Views
				streamReader = new StreamReader(path + "createView.txt", Encoding.UTF8);
				sql = streamReader.ReadToEnd();
				db.ExecuteNonQuery(sql);
				UpdateProgressBar(ref step, maxStep);
				// Insert default data
				streamReader = new StreamReader(path + "insert.txt", Encoding.UTF8);
				sql = streamReader.ReadToEnd();
				db.ExecuteNonQuery(sql);
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
					db.ExecuteNonQuery("INSERT INTO player (name) VALUES ('" + txtPlayerName.Text.Trim() + "')");
					Config.Settings.playerName = txtPlayerName.Text.Trim();
					string result = "";
					Config.SaveConfig(out result);
				}
				UpdateProgressBar(ref step, maxStep);
				// Done
				Cursor.Current = Cursors.Default;
				Application.DoEvents();
				Code.MsgBox.Show("Database created successfully.", "Created database");
				this.Close();
			}	
		}




		private void btnCreateSQLiteDB_Click(object sender, EventArgs e)
		{
			// Check if database exists
			bool dbExists = false;
			string db = "C:\\wotdb.db";
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
					Code.MessageDark.Show("Database created successfully.", "Created database");
					pbCreateDatabase.Visible = false;
					Form.ActiveForm.Close();
				}
			}
			 */
		}


		private void badForm1_Click(object sender, EventArgs e)
		{

		}

		private void cmdSelectFIle_Click(object sender, EventArgs e)
		{
			
		}


	}
}
