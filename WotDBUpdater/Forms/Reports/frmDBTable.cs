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

namespace WotDBUpdater.Forms.Reports
{
    public partial class frmDBTable : Form
    {
        public frmDBTable()
        {
            InitializeComponent();
        }

        private void frmDBTable_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Config.Settings.databaseConn);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT '( Select from list )' AS TableName UNION SELECT table_name AS TableName FROM information_schema.tables ORDER BY TableName", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt=new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            con.Close();
            ddSelectTable.DataSource = dt;
            ddSelectTable.DisplayMember = "TableName";
            ddSelectTable.ValueMember = "TableName";
        }

        private void ddSelectTable_SelectedValueChanged(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void frmDBTable_SizeChanged(object sender, EventArgs e)
        {
            panel2.Height = frmDBTable.ActiveForm.ClientSize.Height - panel1.Height;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            try
            {
                string TableName = ddSelectTable.SelectedValue.ToString();
                if (TableName == "( Select from list )")
                {
                    dataGridViewShowTable.DataSource = null;
                }
                else
                {
                    SqlConnection con = new SqlConnection(Config.Settings.databaseConn);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM " + TableName, con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewShowTable.DataSource = dt;
                }
            }
            catch (Exception)
            {
                // nothing
            }
        }
     
    }
}
