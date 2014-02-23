using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater
{
    public partial class frmDatabaseSetting : Form
    {
        public frmDatabaseSetting()
        {
            InitializeComponent();
        }

        private void frmDatabaseSetting_Load(object sender, EventArgs e)
        {
            // Startup settings
            txtConnStr.Text = Config.Settings.DatabaseConn;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Config.Settings.DatabaseConn = txtConnStr.Text;
            if (Config.CheckDBConn()) // check db config, displays message if error
            {
                Config.SaveConfig(false, false); // save without db check
                Config.SaveConfig(false, true); // save and check user
                Form.ActiveForm.Close();
            }
        }
    }
}
