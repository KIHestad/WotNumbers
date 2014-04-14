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

namespace WotDBUpdater.Forms
{
	public partial class PopupGrid : Form
	{
		
		public PopupGrid(string title, DataTable dt)
		{
			InitializeComponent();
			PopupGridTheme.Text = title;
			dataGridPopup.DataSource = dt;
			scrollGrid.ScrollElementsTotals = dt.Rows.Count;
			dataGridPopup.MouseWheel += new MouseEventHandler(dataGridPopup_MouseWheel); // Add Mouse Wheel handle
		}

		private void Popup_Load(object sender, EventArgs e)
		{
			if (dataGridPopup.RowCount > 0)
			{
				// Style datagrid
				dataGridPopup.BorderStyle = BorderStyle.None;
				dataGridPopup.BackgroundColor = ColorTheme.FormBack;
				dataGridPopup.GridColor = ColorTheme.GridBorders;
				dataGridPopup.DefaultCellStyle.BackColor = ColorTheme.FormBack;
				dataGridPopup.DefaultCellStyle.ForeColor = ColorTheme.ControlFont;
				dataGridPopup.DefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
				dataGridPopup.DefaultCellStyle.SelectionBackColor = ColorTheme.FormBack;
				dataGridPopup.Top = PopupGridTheme.MainArea.Top;
				dataGridPopup.Left = PopupGridTheme.MainArea.Left;
				scrollGrid.Top = PopupGridTheme.MainArea.Top;
				scrollGrid.ScrollNecessary = (scrollGrid.ScrollElementsTotals > scrollGrid.ScrollElementsVisible);
				ResizeNow();
			}
			else
				this.Close();
		}

		private void PopupGrid_Shown(object sender, EventArgs e)
		{
			scrollGrid.ScrollElementsVisible = dataGridPopup.DisplayedRowCount(false);
			scrollGrid.ScrollNecessary = (scrollGrid.ScrollElementsTotals > scrollGrid.ScrollElementsVisible);
			ResizeNow();
		}

		private void Popup_Resize(object sender, EventArgs e)
		{
			ResizeNow();
		}


		private void ResizeNow()
		{
			scrollGrid.ScrollElementsVisible = dataGridPopup.DisplayedRowCount(false);
			dataGridPopup.Height = PopupGridTheme.MainArea.Height;
			scrollGrid.Left = PopupGridTheme.MainArea.Right - scrollGrid.Width;
			scrollGrid.Height = PopupGridTheme.MainArea.Height;
			if (scrollGrid.ScrollNecessary)
			{
				dataGridPopup.Width = PopupGridTheme.MainArea.Width - scrollGrid.Width;	
			}
			else
			{
				dataGridPopup.Width = PopupGridTheme.MainArea.Width - 1; // Have to show 1 pixel of scrollbar so it can be redrawn
			}
			dataGridPopup.Columns[0].Width = dataGridPopup.Width - 2;
			
		}

		private void scrollGrid_MouseDown(object sender, MouseEventArgs e)
		{
			ScrollGrid();
		}

		private void scrollGrid_MouseMove(object sender, MouseEventArgs e)
		{
			ScrollGrid();
		}

		private void ScrollGrid()
		{
			dataGridPopup.FirstDisplayedScrollingRowIndex = scrollGrid.ScrollPosition;
		}

		private void dataGridPopup_MouseWheel(object sender, MouseEventArgs e)
		{
			try
			{
				// scroll in grid from mouse wheel
				int currentIndex = this.dataGridPopup.FirstDisplayedScrollingRowIndex;
				int scrollLines = SystemInformation.MouseWheelScrollLines;
				if (e.Delta > 0)
				{
					this.dataGridPopup.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines);
				}
				else if (e.Delta < 0)
				{
					this.dataGridPopup.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines;
				}
				// move scrollbar
				scrollGrid.ScrollPosition = dataGridPopup.FirstDisplayedScrollingRowIndex;
			}
			catch (Exception)
			{
				// throw;
			}

		}

		private void dataGridPopup_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
		{
			dataGridPopup.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.FormBackTitle;
			dataGridPopup.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTheme.FormBackTitle;
		}

		private void dataGridPopup_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			dataGridPopup.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.FormBack;
			dataGridPopup.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTheme.FormBack;
		}

		private void dataGridPopup_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			Code.PopupGrid.Value = dataGridPopup.Rows[e.RowIndex].Cells[0].Value.ToString();
			Code.PopupGrid.ValueSelected = true;
			this.Close();
		}

		
	}
}
