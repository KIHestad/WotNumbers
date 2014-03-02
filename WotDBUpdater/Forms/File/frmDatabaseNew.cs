using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Check if database exists
            if (Config.CheckDBConn(txtDatabasename.Text, false))
            {
                MessageBox.Show("Database with this name alreade exsits, choose another database name", "Cannot create database");
            }
            else
            {
                // Create db now
            }
        }

    }
}
