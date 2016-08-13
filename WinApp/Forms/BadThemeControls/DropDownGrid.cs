using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class DropDownGrid : FormCloseOnEsc
    {
		private Control SourceDropDown;
		public DropDownGrid(DataTable dt, Control DropDown)
		{
			InitializeComponent();
			dataGridDropDown.DataSource = dt;
			scrollY.ScrollElementsTotals = dt.Rows.Count;
			dataGridDropDown.MouseWheel += new MouseEventHandler(dataGridDropDown_MouseWheel); // Add Mouse Wheel handle	
			SourceDropDown = DropDown;
		}
				
		private void DropDown_Load(object sender, EventArgs e)
		{
			if (dataGridDropDown.RowCount > 0)
			{
				// Style datagrid
				dataGridDropDown.BorderStyle = BorderStyle.None;
				dataGridDropDown.BackgroundColor = ColorTheme.ToolGrayMainBack;
				dataGridDropDown.GridColor = ColorTheme.ToolGrayMainBack;
				dataGridDropDown.DefaultCellStyle.BackColor = ColorTheme.ToolGrayMainBack;
				dataGridDropDown.DefaultCellStyle.ForeColor = ColorTheme.ControlFont;
				dataGridDropDown.DefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
				dataGridDropDown.DefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedCellColor;
				// Form Height
				int rowHeight = dataGridDropDown.Rows[0].Height;
				int rowCount = dataGridDropDown.RowCount;
				if (rowCount > 12) rowCount = 12;
				this.Height = (rowHeight * rowCount) + 5;
				// Form Border
				System.Drawing.Graphics formGraphics = this.CreateGraphics();
				formGraphics.DrawRectangle(new Pen(ColorTheme.ScrollbarBack), 0, 0, Width - 1, Height - 1);
				// Position grid and scroll
				dataGridDropDown.Top = 2;
				dataGridDropDown.Left = 1;
				dataGridDropDown.Height = this.Height - 2;
				scrollY.ScrollElementsVisible = dataGridDropDown.DisplayedRowCount(false);
				scrollY.ScrollNecessary = (scrollY.ScrollElementsTotals > scrollY.ScrollElementsVisible);
				scrollY.Top = 2;
				scrollY.Left = this.Width - scrollY.Width - 2;
				scrollY.Height = this.Height - 4;
				if (scrollY.ScrollNecessary)
				{
					dataGridDropDown.Width = this.Width - scrollY.Width - 6;
				}
				else
				{
					dataGridDropDown.Width = this.Width - 4; 
				}
				dataGridDropDown.Columns[0].Width = dataGridDropDown.Width;
				dataGridDropDown.Focus();
			}
			else
				this.Close();
		}

		private void ScrollGrid()
		{
			dataGridDropDown.FirstDisplayedScrollingRowIndex = scrollY.ScrollPosition;
		}

		private void dataGridDropDown_MouseWheel(object sender, MouseEventArgs e)
		{
			try
			{
				// scroll in grid from mouse wheel
				int currentIndex = this.dataGridDropDown.FirstDisplayedScrollingRowIndex;
				int scrollLines = SystemInformation.MouseWheelScrollLines;
				if (e.Delta > 0)
				{
					this.dataGridDropDown.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines);
				}
				else if (e.Delta < 0)
				{
					this.dataGridDropDown.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines;
				}
				// move scrollbar
				scrollY.ScrollPosition = dataGridDropDown.FirstDisplayedScrollingRowIndex;
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				// throw;
			}

		}

		private void dataGridDropDown_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
		{
			dataGridDropDown.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.ToolGrayMain;
			dataGridDropDown.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTheme.ToolGrayMain;
		}

		private void dataGridDropDown_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			dataGridDropDown.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.ToolGrayMainBack;
			dataGridDropDown.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTheme.ToolGrayMainBack;
		}

		private void dataGridDropDown_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			Cell_Click();
		}

		private void Cell_Click()
		{
			Code.PopupGrid.Value = dataGridDropDown.CurrentCell.Value.ToString();
			Code.PopupGrid.ValueSelected = true;
			this.Close();
			SourceDropDown.Text = Code.PopupGrid.Value;
		}

		private void scrollY_MouseMove(object sender, MouseEventArgs e)
		{
			ScrollGrid();
		}

		private void scrollY_MouseDown(object sender, MouseEventArgs e)
		{
			ScrollGrid();
		}

		private void DropDownGrid_Paint(object sender, PaintEventArgs e)
		{
			Form frm = (Form)sender;
			e.Graphics.DrawRectangle(new Pen(ColorTheme.ScrollbarBack), 0, 0, frm.Width - 1, frm.Height - 1);
		}

		private void DropDownGrid_FormClosed(object sender, FormClosedEventArgs e)
		{
			Code.DropDownGrid.Shown = false;
		}

		private void DropDownGrid_Deactivate(object sender, EventArgs e)
		{
			Code.DropDownGrid.Shown = false;
			this.Close();
		}

		private void dataGridDropDown_KeyPress(object sender, KeyPressEventArgs e)
		{
			string key = Convert.ToString(e.KeyChar);
			int rownum = dataGridDropDown.SelectedCells[0].RowIndex;
			if (!FindItem(key, rownum))
			{
				rownum = -1;
				FindItem(key, rownum);
			}
		}

		private bool FindItem(string key, int rownum)
		{
			bool found = false;
			while (rownum < dataGridDropDown.RowCount - 1 && !found)
			{
				rownum++;
				found = dataGridDropDown.Rows[rownum].Cells[0].Value.ToString().Substring(0, 1).ToUpper() == key.ToUpper();
			}
			if (found)
			{
				dataGridDropDown.ClearSelection();
				dataGridDropDown.Rows[rownum].Selected = true;
				dataGridDropDown.CurrentCell = dataGridDropDown.Rows[rownum].Cells[0];
				if (rownum >= 3)
					dataGridDropDown.FirstDisplayedScrollingRowIndex = rownum - 3;
				else
					dataGridDropDown.FirstDisplayedScrollingRowIndex = 0;
			}
			return found;
		}

		private void dataGridDropDown_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				Cell_Click();
			}
		}

	}
}
