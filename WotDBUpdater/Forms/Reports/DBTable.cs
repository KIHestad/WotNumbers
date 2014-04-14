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
using WotDBUpdater.Code;

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
			dataGridViewShowTable.Top = DBTableTheme.MainArea.Top + 45;
			dataGridViewShowTable.Left = DBTableTheme.MainArea.Left;
			ResizeNow();
			// Style datagrid
			dataGridViewShowTable.BorderStyle = BorderStyle.None;
			dataGridViewShowTable.BackgroundColor = ColorTheme.FormBack;
			dataGridViewShowTable.GridColor = ColorTheme.GridBorders;
			dataGridViewShowTable.EnableHeadersVisualStyles = false;
			dataGridViewShowTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dataGridViewShowTable.ColumnHeadersHeight = 30;
			dataGridViewShowTable.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dataGridViewShowTable.ColumnHeadersDefaultCellStyle.BackColor = ColorTheme.GridHeaderBackLight;
			dataGridViewShowTable.ColumnHeadersDefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dataGridViewShowTable.ColumnHeadersDefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dataGridViewShowTable.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedHeaderColor;
			dataGridViewShowTable.RowHeadersWidth = 20;
			dataGridViewShowTable.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dataGridViewShowTable.RowHeadersDefaultCellStyle.BackColor = ColorTheme.GridHeaderBackLight;
			dataGridViewShowTable.RowHeadersDefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dataGridViewShowTable.RowHeadersDefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dataGridViewShowTable.RowHeadersDefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedHeaderColor;
			dataGridViewShowTable.DefaultCellStyle.BackColor = ColorTheme.FormBack;
			dataGridViewShowTable.DefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dataGridViewShowTable.DefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dataGridViewShowTable.DefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedCellColor;
		}

		private void frmDBTable_SizeChanged(object sender, EventArgs e)
		{
			ResizeNow();	
		}

		private void ResizeNow()
		{
			dataGridViewShowTable.Width = DBTableTheme.MainArea.Width;
			dataGridViewShowTable.Height = DBTableTheme.MainArea.Height - 45;
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			RefreshDataGrid();
		}

		private void RefreshDataGrid()
		{
			string TableName = popupSelectTable.Text.ToString();
			if (TableName != "")
				dataGridViewShowTable.DataSource = db.FetchData("SELECT * FROM " + TableName);
		}

		private void popupSelectTable_Click(object sender, EventArgs e)
		{
			string tableList = "";
			DataTable dt = db.ListTables();
			foreach (DataRow dr in dt.Rows)
			{
				tableList += dr["TABLE_NAME"].ToString() + ",";
			}
			if (tableList.Length > 0)
			{
				tableList = tableList.Substring(0, tableList.Length - 1);
				string newValue = Code.PopupGrid.Show("Select Table", Code.PopupGrid.PopupGridType.List, tableList);
				if (Code.PopupGrid.ValueSelected) popupSelectTable.Text = newValue;
				RefreshDataGrid();
			}
		}

		private void dataGridViewShowTable_MouseMove(object sender, MouseEventArgs e)
		{
			DBTableTheme.Cursor = Cursors.Default;
		}
	 
	}
}
