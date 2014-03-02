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
    public partial class frmAddCountryToTable : Form
    {
        public frmAddCountryToTable()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Cet config data
                SqlConnection con = new SqlConnection(Config.Settings.databaseConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO country (countryid, name, shortname) VALUES (@countryid, @name, @shortname)", con);
                cmd.Parameters.AddWithValue("@countryid", txtid.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@shortname", txtShortName.Text);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
