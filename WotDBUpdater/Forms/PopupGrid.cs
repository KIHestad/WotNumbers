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
		}

		
		private void Popup_Resize(object sender, EventArgs e)
		{
			ResizeNow();
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
				ResizeNow();
			}
			else
				this.Close();
		}



		private void ResizeNow()
		{
			dataGridPopup.Height = PopupGridTheme.MainArea.Height;
			dataGridPopup.Width = PopupGridTheme.MainArea.Width;
			dataGridPopup.Columns[0].Width = dataGridPopup.Width - 2;
		}

		private void dataGridPopup_MouseMove(object sender, MouseEventArgs e)
		{
			PopupGridTheme.Cursor = Cursors.Default;
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
			Code.PopupGrid.SelectedValue = dataGridPopup.Rows[e.RowIndex].Cells[0].Value.ToString();
			this.Close();
		}
	}
}
