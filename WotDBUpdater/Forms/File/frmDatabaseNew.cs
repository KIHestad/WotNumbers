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

namespace WotDBUpdater.Forms.File
{
    public partial class frmDatabaseNew : Form
    {
        public frmDatabaseNew()
        {
            InitializeComponent();
        }

        private void frmDatabaseNew_Load(object sender, EventArgs e)
        {
            txtPlayerName.Text = Config.Settings.playerName;
            txtFileLocation.Text = Path.GetDirectoryName(Application.ExecutablePath) + "\\Database";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Check if database exists
            if (Config.CheckDBConn(false, txtDatabasename.Text))
            {
                MessageBox.Show("Database with this name alreade exsits, choose another database name", "Cannot create database");
            }
            else
            {
                // Create db now
                if (CreateDatabase(txtDatabasename.Text, txtFileLocation.Text))
                {
                    // Fill database with default data
                    FillDatabase(txtDatabasename.Text);
                    // Done
                    Form.ActiveForm.Close();
                }

            }
        }

        private bool CreateDatabase(string databaseName, string fileLocation)
        {
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            bool dbOk = false;
            // Check database file location
            bool fileLocationExsits = true;
            if (Directory.Exists(fileLocation))
            {
                DirectoryInfo prevPath = Directory.GetParent(fileLocation);
                if (prevPath.Exists)
                {
                    Directory.CreateDirectory(fileLocation);
                }
                else
                {
                    fileLocationExsits = false;
                    MessageBox.Show("Error createing database, file parh does not exist", "Error creating database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Error createing database: " + ex.ToString(), "Error creating database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            Cursor.Current = Cursors.Default;
            return dbOk;
        }

        private void FillDatabase(string databaseName)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\Database\\";
            string sql;
            StreamReader streamReader = new StreamReader(path + "createTable.txt", Encoding.UTF8);
            sql = streamReader.ReadToEnd();
            RunSql(sql);
            streamReader = new StreamReader(path + "createView.txt", Encoding.UTF8);
            sql = streamReader.ReadToEnd();
            RunSql(sql);
            streamReader = new StreamReader(path + "insert.txt", Encoding.UTF8);
            sql = streamReader.ReadToEnd();
            RunSql(sql);
            importTanks2DB.fetchTanks();
        }

        private void RunSql(string sql)
        {
            using (SqlConnection con = new SqlConnection(Config.DatabaseConnection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
