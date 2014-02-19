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

namespace WotDBUpdater
{
    public partial class frmCountryInGrid : Form
    {
        public frmCountryInGrid()
        {
            InitializeComponent();
        }

        private void frmCountryInGrid_Load(object sender, EventArgs e)
        {
            try
            {
                // Cet config data
                ConfigData conf = new ConfigData();
                conf = Config.GetConfig();
                SqlConnection con = new SqlConnection(conf.DatabaseConn);
                SqlCommand cmd = new SqlCommand("SELECT * FROM country", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
        }
    }
}
