using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

		private void menuDataGetTankDataFromAPI_Click(object sender, EventArgs e)
		{
			Form frm = new GetTankDataFromAPI();
			frm.ShowDialog();
		}

		
	}
}
