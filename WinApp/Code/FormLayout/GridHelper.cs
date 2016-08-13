using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinApp.Code
{
	class GridHelper
	{
		public static void StyleDataGrid(DataGridView dgv, DataGridViewSelectionMode selectionMode = DataGridViewSelectionMode.FullRowSelect )
		{
			dgv.ReadOnly = true;
			dgv.AllowUserToAddRows = false;
			dgv.AllowUserToDeleteRows = false; 
			dgv.BorderStyle = BorderStyle.FixedSingle;
			dgv.BackgroundColor = ColorTheme.FormBack;
			dgv.GridColor = ColorTheme.GridBorders;
			dgv.EnableHeadersVisualStyles = false;
			dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(2,4,0,4);
			dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTheme.GridHeaderBackLight;
			dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedHeaderColor;
			dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			dgv.DefaultCellStyle.BackColor = ColorTheme.FormBack;
			dgv.DefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dgv.DefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dgv.DefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedCellColor;
			dgv.ScrollBars = ScrollBars.None;
			dgv.RowHeadersVisible = false;
			dgv.AllowUserToAddRows = false;
			dgv.AllowUserToDeleteRows = false;
			dgv.AllowUserToOrderColumns = false;
			dgv.AllowUserToResizeRows = false;
			dgv.SelectionMode = selectionMode;
			dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;

        }

		public static void StyleGadgetDataGrid(DataGridView dgv, DataGridViewSelectionMode selectionMode = DataGridViewSelectionMode.FullRowSelect)
		{
			dgv.ReadOnly = true;
			dgv.AllowUserToAddRows = false;
			dgv.AllowUserToDeleteRows = false;
			dgv.BorderStyle = BorderStyle.None;
			dgv.BackgroundColor = ColorTheme.FormBack;
			dgv.GridColor = ColorTheme.FormBack;
			dgv.EnableHeadersVisualStyles = false;
			dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(2, 4, 0, 4);
			dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTheme.GridHeaderBackLight;
			dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedHeaderColor;
			dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			dgv.DefaultCellStyle.BackColor = ColorTheme.FormBack;
			dgv.DefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dgv.DefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dgv.DefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedCellColor;
			dgv.ScrollBars = ScrollBars.None;
			dgv.RowHeadersVisible = false;
			dgv.AllowUserToAddRows = false;
			dgv.AllowUserToDeleteRows = false;
			dgv.AllowUserToOrderColumns = false;
			dgv.AllowUserToResizeRows = false;
			dgv.SelectionMode = selectionMode;
			dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;

        }

    }
}
