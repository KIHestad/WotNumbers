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
		private ConfigData.dbType SelectedDbType;
		public DatabaseNew(ConfigData.dbType dbType)
		{
			InitializeComponent();
			if (dbType == ConfigData.dbType.MSSQLserver)
				DatabaseNewTheme.Text = "Create New MS SQL Server Database";
			else if (dbType == ConfigData.dbType.SQLite)
				DatabaseNewTheme.Text = "Create New SQLite Database";
			SelectedDbType = dbType;
		}

		private void frmDatabaseNew_Load(object sender, EventArgs e)
		{
			txtPlayerName.Text = Config.Settings.playerName;
			txtFileLocation.Text = Path.GetDirectoryName(Application.ExecutablePath) + "\\Database\\";
		}

		private void UpdateProgressBar(ref int step, int maxStep)
		{
			step++; // count step 1 to maxValue
			badProgressBar.Value = step;
			Refresh();
			Application.DoEvents();
		}

		private void btnCreateDB_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			int maxStep = 12;
			badProgressBar.ValueMax = maxStep ;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;

			int step = 0;
			UpdateProgressBar(ref step, maxStep);
			Application.DoEvents();
			// Create db now
			if (db.CreateDatabase(txtDatabasename.Text, txtFileLocation.Text, SelectedDbType))
			{
				// Fill database with default data
				UpdateProgressBar(ref step, maxStep);
				// Update db by running sql scripts
				string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\Database\\";
				string sql;
				// Create Tables
				StreamReader streamReader = new StreamReader(path + "createTable.txt", Encoding.UTF8);
				sql = streamReader.ReadToEnd();
				db.ExecuteNonQuery(sql, true, SelectedDbType);
				UpdateProgressBar(ref step, maxStep);
				// Create Views
				streamReader = new StreamReader(path + "createView.txt", Encoding.UTF8);
				sql = streamReader.ReadToEnd();
				db.ExecuteNonQuery(sql, true, SelectedDbType);
				UpdateProgressBar(ref step, maxStep);
				// Insert default data
				streamReader = new StreamReader(path + "insert.txt", Encoding.UTF8);
				sql = streamReader.ReadToEnd();
				db.ExecuteNonQuery(sql, true, SelectedDbType);
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
