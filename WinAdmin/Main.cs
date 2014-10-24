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
			
		}

		private void menuDataGetTankDataFromAPI_Click(object sender, EventArgs e)
		{
			Form frm = new GetTankDataFromAPI();
			frm.ShowDialog();
		}

		private void readMasteryBadgesFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Recreate table
			DB.DBResult result = new DB.DBResult();
			// Add masterybadge table if missing
			string sql = "SELECT name FROM sqlite_master WHERE type='table' AND name='masterybadge';";
			DataTable dt = DB.FetchData(sql, Settings.Config, out result);
			if (dt.Rows.Count == 1)
			{
				sql = "drop table masterybadge;";
				DB.ExecuteNonQuery(sql, Settings.Config, out result);
				FormHelper.ShowError(result);
			}
			sql = "create table masterybadge ( " +
				"	id integer primary key, " +
				"	img blob, imgSmall blob, imgLarge " +
				")";
			DB.ExecuteNonQuery(sql, Settings.Config, out result);
			FormHelper.ShowError(result);
			// Remove current images
			// Loop throug current images
			result = new DB.DBResult();
			string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Img\\Badges\\";
			string[] images = Directory.GetFiles(path, "*.png");
			foreach (string imageFile in images)
			{
				// read image into database
				// Normal= 124 x  31
				// Small =  50 x  24
				// Large = 160 x 199
				byte[] img = getImageFromFile(imageFile);
				byte[] imgSmall = getImageFromFile(imageFile.Replace("\\Img\\Badges\\","\\Img\\Badges\\Small\\"));
				byte[] imgLarge = getImageFromFile(imageFile.Replace("\\Img\\Badges\\", "\\Img\\Badges\\Large\\"));
				// SQL Lite binary insert
				string conString = Config.DatabaseConnection(Settings.Config);
				SQLiteConnection con = new SQLiteConnection(conString);
				SQLiteCommand cmd = con.CreateCommand();
				cmd.CommandText = "INSERT INTO masterybadge (id, img, imgSmall, imgLarge) VALUES (@id, @img, @imgSmall, @imgLarge); ";
				SQLiteParameter imgParam = new SQLiteParameter("@img", System.Data.DbType.Binary);
				SQLiteParameter imgSmallParam = new SQLiteParameter("@imgSmall", System.Data.DbType.Binary);
				SQLiteParameter imgLargeParam = new SQLiteParameter("@imgLarge", System.Data.DbType.Binary);
				SQLiteParameter idParam = new SQLiteParameter("@id", System.Data.DbType.Int32);
				imgParam.Value = img;
				imgSmallParam.Value = imgSmall;
				imgLargeParam.Value = imgLarge;
				idParam.Value = Path.GetFileNameWithoutExtension(imageFile);
				cmd.Parameters.Add(imgParam);
				cmd.Parameters.Add(imgSmallParam);
				cmd.Parameters.Add(imgLargeParam);
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
			MessageBox.Show("Images imported from file", "Done");
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

		private void readTankTypeFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Recreate table
			DB.DBResult result = new DB.DBResult();
			// Add masterybadge table if missing
			string sql = "SELECT name FROM sqlite_master WHERE type='table' AND name='tanktype';";
			DataTable dt = DB.FetchData(sql, Settings.Config, out result);
			if (dt.Rows.Count == 1)
			{
				sql = "drop table tanktype;";
				DB.ExecuteNonQuery(sql, Settings.Config, out result);
				FormHelper.ShowError(result);
			}
			sql = "create table tanktype ( " +
				"	id integer primary key, " +
				"	img blob " +
				")";
			DB.ExecuteNonQuery(sql, Settings.Config, out result);
			FormHelper.ShowError(result);
			// Remove current images
			// Loop throug current images
			result = new DB.DBResult();
			string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Img\\TankType\\";
			string[] images = Directory.GetFiles(path, "*.png");
			foreach (string imageFile in images)
			{
				// read image into database
				byte[] img = getImageFromFile(imageFile);
				// SQL Lite binary insert
				string conString = Config.DatabaseConnection(Settings.Config);
				SQLiteConnection con = new SQLiteConnection(conString);
				SQLiteCommand cmd = con.CreateCommand();
				cmd.CommandText = "INSERT INTO tanktype (id, img) VALUES (@id, @img); ";
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
			MessageBox.Show("Images imported from file", "Done");
		}

		private void readNationImagesFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Recreate table
			DB.DBResult result = new DB.DBResult();
			// Add masterybadge table if missing
			string sql = "SELECT name FROM sqlite_master WHERE type='table' AND name='nation';";
			DataTable dt = DB.FetchData(sql, Settings.Config, out result);
			if (dt.Rows.Count == 1)
			{
				sql = "drop table nation;";
				DB.ExecuteNonQuery(sql, Settings.Config, out result);
				FormHelper.ShowError(result);
			}
			sql = "create table nation ( " +
				"	id integer primary key, " +
				"	img blob " +
				")";
			DB.ExecuteNonQuery(sql, Settings.Config, out result);
			FormHelper.ShowError(result);
			// Remove current images
			// Loop throug current images
			result = new DB.DBResult();
			string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Img\\Nation\\";
			string[] images = Directory.GetFiles(path, "*.png");
			foreach (string imageFile in images)
			{
				// read image into database
				byte[] img = getImageFromFile(imageFile);
				// SQL Lite binary insert
				string conString = Config.DatabaseConnection(Settings.Config);
				SQLiteConnection con = new SQLiteConnection(conString);
				SQLiteCommand cmd = con.CreateCommand();
				cmd.CommandText = "INSERT INTO nation (id, img) VALUES (@id, @img); ";
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
			MessageBox.Show("Images imported from file", "Done");
		}

		private void readMapsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Recreate table
			DB.DBResult result = new DB.DBResult();
			// Add masterybadge table if missing
			string sql = "SELECT name FROM sqlite_master WHERE type='table' AND name='map';";
			DataTable dt = DB.FetchData(sql, Settings.Config, out result);
			if (dt.Rows.Count == 0)
			sql = "create table map ( " +
				"	id integer primary key, " +
				"   name varchar(100) not null, " +
				"   minimap blob, " + 
				"	illustration blob " +
				")";
			DB.ExecuteNonQuery(sql, Settings.Config, out result);
			FormHelper.ShowError(result);
			// Remove current images
			// Loop throug current images
			result = new DB.DBResult();
			string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Img\\Map\\";
			string[] images = Directory.GetFiles(path, "*.jpg");
			foreach (string imageFile in images)
			{
				string name = Path.GetFileNameWithoutExtension(imageFile);
				sql = "select id from map where name=@name";
				DB.AddWithValue(ref sql, "@name", name, DB.SqlDataType.VarChar, Settings.Config);
				if (DB.FetchData(sql, Settings.Config, out result).Rows.Count == 0)
					sql = "INSERT INTO map (name, minimap, illustration) VALUES (@name, @minimap, @illustration); ";
				else
					sql = "UPDATE map SET minimap=@minimap, illustration=@illustration WHERE name=@name; ";
				// read image into database
				byte[] imgMinimap = getImageFromFile(imageFile);
				byte[] imgIllustration = getImageFromFile(Path.GetDirectoryName(imageFile) + "\\Illustration\\" + Path.GetFileName(imageFile));
				// SQL Lite binary insert
				string conString = Config.DatabaseConnection(Settings.Config);
				SQLiteConnection con = new SQLiteConnection(conString);
				SQLiteCommand cmd = con.CreateCommand();
				cmd.CommandText = sql;
				SQLiteParameter imgMinimapParam = new SQLiteParameter("@minimap", System.Data.DbType.Binary);
				SQLiteParameter imgIllustrationParam = new SQLiteParameter("@illustration", System.Data.DbType.Binary);
				SQLiteParameter nameParam = new SQLiteParameter("@name", System.Data.DbType.String);
				imgMinimapParam.Value = imgMinimap;
				imgIllustrationParam.Value = imgIllustration;
				nameParam.Value = name;
				cmd.Parameters.Add(imgMinimapParam);
				cmd.Parameters.Add(imgIllustrationParam);
				cmd.Parameters.Add(nameParam);
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
			MessageBox.Show("Images imported from file", "Done");
		}
	}
}
