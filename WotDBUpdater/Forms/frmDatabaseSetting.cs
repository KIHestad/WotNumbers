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
            ConfigData conf = new ConfigData();
            conf = Config.GetConfig();
            txtConnStr.Text = conf.DatabaseConn;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ConfigData conf = new ConfigData();
            conf = Config.GetConfig();
            conf.DatabaseConn = txtConnStr.Text;
            Config.SaveConfig(conf);
            Form.ActiveForm.Close();
        }
    }
}
