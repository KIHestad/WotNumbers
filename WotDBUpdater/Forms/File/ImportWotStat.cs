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
                // Loop through
                sql = "SELECT * FROM RecentBattles";
                mycommand = new SQLiteCommand(cnn);
                mycommand.CommandText = sql;
                reader = mycommand.ExecuteReader();
                foreach (var item in reader)
                {
                    progressBarImport.Value++;
                    lblResult.Text = reader["rbID"].ToString() + " " + reader["rbTankID"].ToString();
                    Application.DoEvents();
                }
                // Done
                lblResult.Text = "Done";
                cnn.Close();
            }
            else
            {
                Code.Support.Message.Show("Select a file u noob...", "Noob message");
            }
        }
    }
}
