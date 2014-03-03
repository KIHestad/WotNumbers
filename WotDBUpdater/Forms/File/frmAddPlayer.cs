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
    public partial class frmAddPlayer : Form
    {
        public frmAddPlayer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Add to database
            if (txtPlayer.Text.Trim() == "")
            {
                MessageBox.Show("Please add a player name before saving u noob... ^_^", "Cannot save nothing....");
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(Config.DatabaseConnection());
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO player (name) VALUES (@name)", con);
                    cmd.Parameters.AddWithValue("@name", txtPlayer.Text.Trim());
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("New player successfully saved.", "New player added");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving new player to database, check that this player name not already exists.\n\nError from database: " + ex.Message, "Error saving new player");
                }
            }
        }
    }
}
