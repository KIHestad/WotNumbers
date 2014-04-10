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
	public partial class DBTable : Form
	{
		public DBTable()
		{
			InitializeComponent();
		}

		private void frmDBTable_Load(object sender, EventArgs e)
		{
			ddSelectTable.DataSource = db.ListTables();
			ddSelectTable.DisplayMember = "TABLE_NAME";
			ddSelectTable.ValueMember = "TABLE_NAME";
		}

		private void ddSelectTable_SelectedValueChanged(object sender, EventArgs e)
		{
			RefreshDataGrid();
		}

		private void frmDBTable_SizeChanged(object sender, EventArgs e)
		{
			panel2.Height = DBTable.ActiveForm.ClientSize.Height - panel1.Height;
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
					dataGridViewShowTable.DataSource = db.FetchData("SELECT * FROM " + TableName);
				}
			}
			catch (Exception)
			{
				// nothing
			}
		}
	 
	}
}
