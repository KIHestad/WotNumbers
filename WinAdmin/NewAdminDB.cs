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
	public partial class NewAdminDB : Form
	{
		public NewAdminDB()
		{
			InitializeComponent();
		}

		private void btnCreate_Click(object sender, EventArgs e)
		{
			// Check if database exists
			string fileLocation = Config.AppDataAdminFolder;
			string databaseName = txtName.Text.Trim();
			if (databaseName.Length == 0)
			{
				MessageBox.Show("Missing name");
			}
			else if (File.Exists(fileLocation + databaseName + ".db"))
			{
				MessageBox.Show("Error creating database, databasefile already exists, select another database name", "Error creating database");
			}
			else
			{
				// Create dir if not exists
				if (!Directory.Exists(fileLocation))
				{
					Directory.CreateDirectory(fileLocation);
				}
				// Create new db file now
				string db = fileLocation + databaseName + ".db";
				SQLiteConnection.CreateFile(db);
				// Save new db file into settings
				Settings.Config.databaseFileName = fileLocation + databaseName + ".db";
				string msg = "";
				Config.SaveConfig(Settings.Config, Settings.ConfigFileName,	out msg);
				// Done
				this.Close();
			}
		}
	}
}
