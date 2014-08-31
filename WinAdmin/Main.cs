using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace WinAdmin
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			string msg = "";
			if (Config.GetConfig(ref Settings.Config, Settings.ConfigFileName, out msg))
			{
				txtAdminSQLiteDB.Text = Settings.Config.databaseFileName;
			}
			else
			{
				MessageBox.Show("Error reading config file:" + Environment.NewLine + msg, "Error");
			}
			UpgradeDB();
		}

		private void menuNewDB_Click(object sender, EventArgs e)
		{
			Form frm = new NewAdminDB();
			frm.ShowDialog();
			txtAdminSQLiteDB.Text = Settings.Config.databaseFileName;
		}

		private void menuSelectDB_Click(object sender, EventArgs e)
		{
			openFileDialogDQLiteADminDB.FileName = "*.db";
			openFileDialogDQLiteADminDB.ShowDialog();
			// After file is selected
			if (openFileDialogDQLiteADminDB.FileName != "*.db")
			{
				txtAdminSQLiteDB.Text = openFileDialogDQLiteADminDB.FileName;
				string msg = "";
				Settings.Config.databaseType = ConfigData.dbType.SQLite;
				Settings.Config.databaseFileName = txtAdminSQLiteDB.Text;
				Config.SaveConfig(Settings.Config, Settings.ConfigFileName, out msg);
			}
		}

		private void MenuExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void menuDataCreateTableStruct_Click(object sender, EventArgs e)
		{
			string sql = "";
			DB.DBResult result = new DB.DBResult();
			// Trunc first
			sql = "drop table tank;";
			DB.ExecuteNonQuery(sql, Settings.Config, out result);

			// Create tables
			sql =	"create table tank ( " +
					"	id integer primary key, " +
					"	name varchar(255), " +
					"   imgPath varchar(255), " +
					"   smallImgPath varchar(255), " +
					"   contourImgPath varchar(255), " +
					"	img blob, " +
					"	smallImg blob, " +
					"	contourImg blob " +
					")";

			DB.ExecuteNonQuery(sql, Settings.Config, out result);
			FormHelper.ShowError(result);
			if (result.Error) return;

			// Done
			MessageBox.Show("Table structure created", "Done");
		}

		private void UpgradeDB()
		{
			DB.DBResult result = new DB.DBResult();
			// Add masterybadge table if missing
			string sql = "SELECT name FROM sqlite_master WHERE type='table' AND name='masterybadge';";
			DataTable dt = DB.FetchData(sql, Settings.Config, out result);
			if (dt.Rows.Count == 0)
			{
				sql = "create table masterybadge ( " +
					"	id integer primary key, " +
					"	img blob " +
					")";
				DB.ExecuteNonQuery(sql, Settings.Config, out result);
				FormHelper.ShowError(result);
			}
		}

		private void menuDataGetTankDataFromAPI_Click(object sender, EventArgs e)
		{
			Form frm = new GetTankDataFromAPI();
			frm.ShowDialog();
		}

		private void readMasteryBadgesFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Remove current images
			DB.DBResult result = new DB.DBResult();
			string sql = "DELETE FROM masterybadge; ";
			DB.ExecuteNonQuery(sql, Settings.Config, out result);
			// Loop throug current images
			string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Img\\Badges\\";
			string[] images = Directory.GetFiles(path, "*.png");
			foreach (string imageFile in images)
			{
				// read image into database
				byte[] img = getImageFromFile(imageFile);

				// SQL Lite binary insert
				string conString = Config.DatabaseConnection(Settings.Config);
				SQLiteConnection con = new SQLiteConnection(conString);
				SQLiteCommand cmd = con.CreateCommand();
				cmd.CommandText = "INSERT INTO masterybadge (id, img) VALUES (@id, @img); ";
				SQLiteParameter imgParam = new SQLiteParameter("@img", System.Data.DbType.Binary);
				SQLiteParameter idParam = new SQLiteParameter("@id", System.Data.DbType.Int32);
				imgParam.Value = img;
				idParam.Value = Path.GetFileNameWithoutExtension(imageFile);
				cmd.Parameters.Add(imgParam);
				cmd.Parameters.Add(idParam);
				con.Open();
				try
				{
					cmd.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				con.Close();
			}
		}

		public static byte[] getImageFromFile(string fileName)
		{
			byte[] imgArray;

			//Get file information and calculate the filesize
			FileInfo info = new FileInfo(fileName);
			long fileSize = info.Length;

			//reasign the filesize to calculated filesize
			Int32 maxImageSize = (Int32)fileSize;

			//Retreave image from file and binary it to Object image
			using (FileStream stream = File.Open(fileName, FileMode.Open))
			{
				BinaryReader br = new BinaryReader(stream);
				imgArray = br.ReadBytes(maxImageSize);
			}
			
			return imgArray;
		}
	}
}
