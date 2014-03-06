using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater.Forms.File
{
    public partial class DatabaseSetting : Form
    {
        private bool changedDbConfig = true;
        
        public DatabaseSetting()
        {
            InitializeComponent();
        }

        private void frmDatabaseSetting_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            // Startup settings
            txtServerName.Text = Config.Settings.databaseServer;
            rbWinAuth.Checked = Config.Settings.databaseWinAuth;
            rbSqlAuth.Checked = !Config.Settings.databaseWinAuth;
            cboDatabaseName.Text = Config.Settings.databaseName;
            txtUid.Text = Config.Settings.databaseUid;
            txtPwd.Text = Config.Settings.databasePwd;
            // UpdateAuthSettings
            UpdateLogin();
        }

        private void UpdateLogin()
        {
            bool enabled = (rbSqlAuth.Checked);
            lblUid.Enabled = enabled;
            lblPwd.Enabled = enabled;
            txtUid.Enabled = enabled;
            txtPwd.Enabled = enabled;
        }

        private void UpdateDatabaseList()
        {
            if (changedDbConfig)
            {
                changedDbConfig = false;
                cboDatabaseName.Items.Clear();
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    string winAuth = "Win";
                    if (rbSqlAuth.Checked) winAuth = "Sql";
                    using (SqlConnection con = new SqlConnection(Config.DatabaseConnection(txtServerName.Text, "master", winAuth, txtUid.Text, txtPwd.Text)))
                    {
                        con.Open();
                        string sql = "SELECT [name] FROM master.dbo.sysdatabases WHERE dbid > 4 and [name] <> 'ReportServer' and [name] <> 'ReportServerTempDB'";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cboDatabaseName.Items.Add(reader["name"]);
                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show ("Error getting databases: " + ex.Message,"Database server error") ;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Config.Settings.databaseServer = txtServerName.Text;
            Config.Settings.databaseWinAuth = rbWinAuth.Checked;
            Config.Settings.databaseUid = txtUid.Text;
            Config.Settings.databasePwd = txtPwd.Text;
            Config.Settings.databaseName = cboDatabaseName.Text;
            string winAuth = "Win";
            if (rbSqlAuth.Checked) winAuth = "Sql";
            if (Config.CheckDBConn(true, txtServerName.Text, cboDatabaseName.Text, winAuth, txtUid.Text, txtPwd.Text)) // check db config, displays message if error
            {
                string msg = "";
                bool saveOk = false;
                saveOk = Config.SaveDbConfig(out msg);
                MessageBox.Show(msg, "Save database settings");
                if (saveOk)
                {
                    // Init
                    TankData.GetTankListFromDB();
                    TankData.GetJson2dbMappingViewFromDB();
                    TankData.GettankData2BattleMappingViewFromDB();
                    //Form.ActiveForm.Close();
                }
            }
        }

        private void rbWinAuth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLogin();
            changedDbConfig = true;
        }

        private void rbSqlAuth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLogin();
            changedDbConfig = true;
        }

        private void btnNewDatabase_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.File.DatabaseNew();
            frm.ShowDialog();
            LoadConfig();
        }

        private void txtServerName_TextChanged(object sender, EventArgs e)
        {
            changedDbConfig = true;
        }

        private void cboDatabaseName_Enter(object sender, EventArgs e)
        {
            UpdateDatabaseList();
        }

        private void txtUid_TextChanged(object sender, EventArgs e)
        {
            changedDbConfig = true;
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            changedDbConfig = true;
        }

       

    }
}
