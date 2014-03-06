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

namespace WotDBUpdater.Forms.Test
{
    public partial class CountryInGrid : Form
    {
        public CountryInGrid()
        {
            InitializeComponent();
        }

        private void frmCountryInGrid_Load(object sender, EventArgs e)
        {
            try
            {
                // Cet config data
                SqlConnection con = new SqlConnection(Config.DatabaseConnection());
                SqlCommand cmd = new SqlCommand("SELECT * FROM country", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(this, ex.Message);
            }
                
        }
    }
}
