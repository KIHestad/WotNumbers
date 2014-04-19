using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WotDBUpdater.Code;

namespace WotDBUpdater.Forms.Reports
{
	public partial class DatabaseTable : Form
	{
		public DatabaseTable()
		{
			InitializeComponent();
		}

		private void DatabaseTable_Load(object sender, EventArgs e)
		{
			dataGridViewShowTable.MouseWheel += new MouseEventHandler(dataGridViewShowTable_MouseWheel); // Add Mouse Wheel handle
			// Scroll and grid size
			scrollCorner.Left = DatabaseTableTheme.MainArea.Right - scrollCorner.Width;
			scrollCorner.Top = DatabaseTableTheme.MainArea.Bottom - scrollCorner.Height;
			scrollY.Top = DatabaseTableTheme.MainArea.Top + 45;
			scrollY.Left = DatabaseTableTheme.MainArea.Right - scrollY.Width;
			scrollY.Height = DatabaseTableTheme.MainArea.Height - 45 - scrollCorner.Height;
			scrollX.Left = DatabaseTableTheme.MainArea.Left;
			scrollX.Top = DatabaseTableTheme.MainArea.Bottom - scrollX.Height;
			scrollX.Width = DatabaseTableTheme.MainArea.Width - scrollCorner.Width;
			dataGridViewShowTable.Top = DatabaseTableTheme.MainArea.Top + 45;
			dataGridViewShowTable.Left = DatabaseTableTheme.MainArea.Left;
			dataGridViewShowTable.Width = DatabaseTableTheme.MainArea.Width - scrollY.Width;
			dataGridViewShowTable.Height = DatabaseTableTheme.MainArea.Height - 45 - scrollX.Height;
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
			// Refresh grid - when no data also hide scrollbar handles
			popupSelectTable.Text = ""; // Avoid NULL as default value
			RefreshScrollbars();
		}

		private void popupSelectTable_Click(object sender, EventArgs e)
		{
			// Show popup with available tables
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

		private void RefreshDataGrid()
		{
			// Show content in grid
			string TableName = popupSelectTable.Text.ToString();
			if (TableName != "")
			{
				dataGridViewShowTable.DataSource = db.FetchData("SELECT * FROM " + TableName);
			}
			RefreshScrollbars();			
		}

		private void RefreshScrollbars()
		{
			int XVisible = 0;
			int XTotal = 0;
			int YVisible = 0;
			int YTotal = 0;
			// Calc scroll boundarys
			if (dataGridViewShowTable.RowCount > 0)
			{
				YTotal = dataGridViewShowTable.RowCount ;
				YVisible = dataGridViewShowTable.DisplayedRowCount(false);
				XTotal = dataGridViewShowTable.ColumnCount ;
				XVisible = dataGridViewShowTable.DisplayedColumnCount(false);
			}
			// Scroll init
			scrollX.ScrollElementsVisible = XVisible;
			scrollX.ScrollElementsTotals = XTotal;
			scrollY.ScrollElementsVisible = YVisible;
			scrollY.ScrollElementsTotals = YTotal;
			//Refresh();
		}


		// Resizing
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			RefreshDataGrid();
		}

		private void DatabaseTable_Resize(object sender, EventArgs e)
		{
			ResizeNow(true);
		}

		private void DatabaseTable_ResizeEnd(object sender, EventArgs e)
		{
			ResizeNow(true);
		}

		private void ResizeNow(bool ResizeGrid = false)
		{
			// Scroll and grid size
			scrollCorner.Left = DatabaseTableTheme.MainArea.Right - scrollCorner.Width;
			scrollCorner.Top = DatabaseTableTheme.MainArea.Bottom - scrollCorner.Height;
			scrollY.Left = DatabaseTableTheme.MainArea.Right - scrollY.Width;
			scrollY.Height = DatabaseTableTheme.MainArea.Height - 45 - scrollCorner.Height;
			scrollX.Top = DatabaseTableTheme.MainArea.Bottom - scrollX.Height;
			scrollX.Width = DatabaseTableTheme.MainArea.Width - scrollCorner.Width;
			if (ResizeGrid)
			{
				dataGridViewShowTable.Width = DatabaseTableTheme.MainArea.Width - scrollY.Width;
				dataGridViewShowTable.Height = DatabaseTableTheme.MainArea.Height - 45 - scrollX.Height;
			}
			RefreshScrollbars();
		}

		// Scrolling Y
		private bool scrollingY = false;
		private void scrollY_MouseDown(object sender, MouseEventArgs e)
		{
			scrollingY = true;
			ScrollY();
		}

		private void scrollY_MouseMove(object sender, MouseEventArgs e)
		{
			if (scrollingY) ScrollY();
		}

		private void ScrollY()
		{
			int posBefore = dataGridViewShowTable.FirstDisplayedScrollingRowIndex;
			dataGridViewShowTable.FirstDisplayedScrollingRowIndex = scrollY.ScrollPosition;
			if (posBefore != dataGridViewShowTable.FirstDisplayedScrollingRowIndex) Refresh();
		}

		private void scrollY_MouseUp(object sender, MouseEventArgs e)
		{
			scrollingY = false;
		}


		// Scrolling X
		private bool scrollingX = false;
		private void scrollX_MouseDown(object sender, MouseEventArgs e)
		{
			scrollingX = true;
			ScrollX();
		}

		private void scrollX_MouseUp(object sender, MouseEventArgs e)
		{
			scrollingX = false;
		}

		private void scrollX_MouseMove(object sender, MouseEventArgs e)
		{
			if (scrollingX) ScrollX();
		}

		private void ScrollX()
		{
			int posBefore = dataGridViewShowTable.FirstDisplayedScrollingColumnIndex;
			dataGridViewShowTable.FirstDisplayedScrollingColumnIndex = scrollX.ScrollPosition;
			if (posBefore != dataGridViewShowTable.FirstDisplayedScrollingColumnIndex) Refresh();
		}

		// Move scrollbar according to grid movements

		private void dataGridViewShowTable_RegionChanged(object sender, EventArgs e)
		{
			
		}

		private void dataGridViewShowTable_SelectionChanged(object sender, EventArgs e)
		{
			MoveScrollBar();
		}

		private void MoveScrollBar()
		{
			scrollX.ScrollPosition = dataGridViewShowTable.FirstDisplayedScrollingColumnIndex;
			scrollY.ScrollPosition = dataGridViewShowTable.FirstDisplayedScrollingRowIndex;
		}

		private void dataGridViewShowTable_MouseWheel(object sender, MouseEventArgs e)
		{
			try
			{
				// scroll in grid from mouse wheel
				int currentIndex = this.dataGridViewShowTable.FirstDisplayedScrollingRowIndex;
				int scrollLines = SystemInformation.MouseWheelScrollLines;
				if (e.Delta > 0)
				{
					this.dataGridViewShowTable.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines);
				}
				else if (e.Delta < 0)
				{
					this.dataGridViewShowTable.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines;
				}
				// move scrollbar
				MoveScrollBar();
			}
			catch (Exception)
			{
				// throw;
			}

		}

	}
}
