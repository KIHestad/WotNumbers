﻿using System;
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
    public partial class frmDBView : Form
    {
        public frmDBView()
        {
            InitializeComponent();

        }

        private void frmDBView_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Config.Settings.databaseConn);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT '( Select from list )' AS TableName UNION SELECT table_name AS TableName FROM information_schema.views ORDER BY TableName", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            con.Close();
            ddSelectView.DataSource = dt;
            ddSelectView.DisplayMember = "TableName";
            ddSelectView.ValueMember = "TableName";
        }

        

        private void frmDBView_SizeChanged(object sender, EventArgs e)
        {
            panel2.Height = frmDBView.ActiveForm.ClientSize.Height - panel1.Height;
        }

                private void RefreshDataGrid()
        {
            try
            {
                string TableName = ddSelectView.SelectedValue.ToString();
                if (TableName == "( Select from list )")
                {
                    dataGridViewShowView.DataSource = null;
                }
                else
                {
                    SqlConnection con = new SqlConnection(Config.Settings.databaseConn);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM " + TableName, con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewShowView.DataSource = dt;
                }
            }
            catch (Exception)
            {
                // nothing
            }
        }

       

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void ddSelectView_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        
     
    }
}
