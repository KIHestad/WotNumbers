using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
			// Style datagrid
			dataGridPopup.BorderStyle = BorderStyle.None;
			dataGridPopup.BackgroundColor = Code.Support.ColorTheme.FormBack;
			dataGridPopup.GridColor = Code.Support.ColorTheme.GridBorders;
			dataGridPopup.DefaultCellStyle.BackColor = Code.Support.ColorTheme.FormBack;
			dataGridPopup.DefaultCellStyle.ForeColor = Code.Support.ColorTheme.ControlFont;
			dataGridPopup.DefaultCellStyle.SelectionForeColor = Code.Support.ColorTheme.ControlFont;
			dataGridPopup.DefaultCellStyle.SelectionBackColor = Code.Support.ColorTheme.FormBack;
			dataGridPopup.Top = PopupGridTheme.MainArea.Top;
			dataGridPopup.Left = PopupGridTheme.MainArea.Left;
			ResizeNow();
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
			dataGridPopup.Rows[e.RowIndex].DefaultCellStyle.BackColor = Code.Support.ColorTheme.FormBackTitle;
			dataGridPopup.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Code.Support.ColorTheme.FormBackTitle;
		}

		private void dataGridPopup_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			dataGridPopup.Rows[e.RowIndex].DefaultCellStyle.BackColor = Code.Support.ColorTheme.FormBack;
			dataGridPopup.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Code.Support.ColorTheme.FormBack;
		}

		private void dataGridPopup_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			Code.PopupGrid.SelectedValue = dataGridPopup.Rows[e.RowIndex].Cells[0].Value.ToString();
			this.Close();
		}
	}
}
