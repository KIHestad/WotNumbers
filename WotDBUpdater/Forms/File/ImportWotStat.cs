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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater.Forms.File
{
	public partial class ImportWotStat : Form
	{
		public ImportWotStat()
		{
			InitializeComponent();
		}

		private void btnOpenWotStatDbFile_Click(object sender, EventArgs e)
		{
			// Select dossier file
			openFileWotStatDbFile.FileName = "*.db";
			openFileWotStatDbFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + 
				"\\WOT Statistics\\Hist_" + Config.Settings.playerName + "\\LastBattle";
			openFileWotStatDbFile.ShowDialog();
			txtWotStatDb.Text = openFileWotStatDbFile.FileName;
		}

		private void btnStartImport_Click(object sender, EventArgs e)
		{
			if (System.IO.File.Exists(txtWotStatDb.Text))
			{
				// Connect to WoT Stat DB
				String dbConnection = "Data Source=" + txtWotStatDb.Text;
				SQLiteConnection cnn = new SQLiteConnection(dbConnection);
				cnn.Open();
				SQLiteCommand mycommand = new SQLiteCommand(cnn);
				// Get count
				String sql = "SELECT COUNT(1) AS rowcount FROM RecentBattles";
				mycommand.CommandText = sql;
				SQLiteDataReader reader = mycommand.ExecuteReader();
				int rowcount = 0;
				foreach (var item in reader)
				{
					rowcount = Convert.ToInt32(reader["rowcount"]);
				}
				progressBarImport.Maximum = rowcount;
				progressBarImport.Value = 0;
				//// Loop through
				//sql = "SELECT * FROM RecentBattles";
				//mycommand = new SQLiteCommand(cnn);
				//mycommand.CommandText = sql;
				//reader = mycommand.ExecuteReader();
				//foreach (var item in reader)
				//{
				//	progressBarImport.Value++;
				//	lblResult.Text = reader["rbID"].ToString() + " " + reader["rbTankID"].ToString();
				//	Application.DoEvents();
				//}

				ImportNow();

				// Done
				lblResult.Text = "Done";
				cnn.Close();
			}
			else
			{
				Code.Support.Message.Show("Select a file u noob...", "Noob message");
			}
		}

		private void ImportNow()
		{
			string WSDB = txtWotStatDb.Text;

			// SQLite dbconn
			String sqliteDbConnection = "Data Source=" + WSDB;
			SQLiteConnection sqliteConn = new SQLiteConnection(sqliteDbConnection);
			sqliteConn.Open();
			SQLiteCommand sqLiteCmd = new SQLiteCommand(sqliteConn);

			// SQL Server dbconn
			SqlConnection sqlConn = new SqlConnection(Config.DatabaseConnection());
			sqlConn.Open();

			// Create temp table for import
			string sql = "create table wsRecentBattles (rbId int, rbTankId int, rbCountryId int, rbBattles int, rbKills int, rbDamageDealt int, rbDamageReceived int,"
					   + "rbSpotted int, rbCapturePoints int, rbDefencePoints int, rbSurvived int, rbVictory int, rbBattleTime int, rbShot int, rbHits int, rbBattleMode int); ";
			SqlCommand startup = new SqlCommand(sql, sqlConn);
			startup.ExecuteNonQuery();


			// Fetch WS recentBattles into datatable
			sqLiteCmd.CommandText = "SELECT rbId, rbTankId, rbCountryId, rbBattles, rbKills, rbDamageDealt, rbDamageReceived, rbSpotted, rbCapturePoints, rbDefencePoints, "
								  + "rbSurvived, rbVictory, rbBattleTime, rbShot, rbHits, rbBattleMode FROM recentBattles";  //rbFragList
			SQLiteDataReader reader = sqLiteCmd.ExecuteReader();
			DataTable recentBattles = new DataTable();
			recentBattles.Load(reader);

			// Write recentBattles to db
			int i = 0;
			sql = "insert into wsRecentBattles (rbId, rbTankId, rbCountryId, rbBattles, rbKills, rbDamageDealt, rbDamageReceived, rbSpotted, rbCapturePoints, rbDefencePoints, "
				+ "rbSurvived, rbVictory, rbBattleTime, rbShot, rbHits, rbBattleMode) values (@rbId, @rbTankId, @rbCountryId, @rbBattles, @rbKills, @rbDamageDealt, "
				+ "@rbDamageReceived, @rbSpotted, @rbCapturePoints, @rbDefencePoints, @rbSurvived, @rbVictory, @rbBattleTime, @rbShot, @rbHits, @rbBattleMode)";
			SqlCommand insertRB = new SqlCommand(sql, sqlConn);
			while (i < recentBattles.Rows.Count)
			{
				insertRB.Parameters.Clear();
				insertRB.Parameters.AddWithValue("@rbId", recentBattles.Rows[i]["rbId"].ToString());
				insertRB.Parameters.AddWithValue("@rbTankId", recentBattles.Rows[i]["rbTankId"].ToString());
				insertRB.Parameters.AddWithValue("@rbCountryId", recentBattles.Rows[i]["rbCountryId"].ToString());
				insertRB.Parameters.AddWithValue("@rbBattles", recentBattles.Rows[i]["rbBattles"].ToString());
				insertRB.Parameters.AddWithValue("@rbKills", recentBattles.Rows[i]["rbKills"].ToString());
				insertRB.Parameters.AddWithValue("@rbDamageDealt", recentBattles.Rows[i]["rbDamageDealt"].ToString());
				insertRB.Parameters.AddWithValue("@rbDamageReceived", recentBattles.Rows[i]["rbDamageReceived"].ToString());
				insertRB.Parameters.AddWithValue("@rbSpotted", recentBattles.Rows[i]["rbSpotted"].ToString());
				insertRB.Parameters.AddWithValue("@rbCapturePoints", recentBattles.Rows[i]["rbCapturePoints"].ToString());
				insertRB.Parameters.AddWithValue("@rbDefencePoints", recentBattles.Rows[i]["rbDefencePoints"].ToString());
				insertRB.Parameters.AddWithValue("@rbSurvived", recentBattles.Rows[i]["rbSurvived"].ToString());
				insertRB.Parameters.AddWithValue("@rbVictory", recentBattles.Rows[i]["rbVictory"].ToString());
				insertRB.Parameters.AddWithValue("@rbBattleTime", recentBattles.Rows[i]["rbBattleTime"].ToString());
				insertRB.Parameters.AddWithValue("@rbShot", recentBattles.Rows[i]["rbShot"].ToString());
				insertRB.Parameters.AddWithValue("@rbHits", recentBattles.Rows[i]["rbHits"].ToString());
				insertRB.Parameters.AddWithValue("@rbBattleMode", recentBattles.Rows[i]["rbBattleMode"].ToString());
				insertRB.ExecuteNonQuery();
				i++;
			}

			// Insert into battles using stored proc
			SqlCommand runProc = new SqlCommand("exec importBattle", sqlConn);
			runProc.ExecuteNonQuery();

			// Remove temp tables
			SqlCommand cleanup = new SqlCommand("drop table wsRecentBattles", sqlConn);
			cleanup.ExecuteNonQuery();

			// Close db connections
			sqlConn.Close();
			sqliteConn.Close();
		}
	}
}
