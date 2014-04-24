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

namespace WotDBUpdater.Forms.File
{
	public partial class ColumnSetup : Form
	{
		
		public enum ColumnSetupType
		{
			TankView = 1,
			BattleView = 2
		}

		private ColumnSetupType colSetupType;

		public ColumnSetup(ColumnSetupType colSelectedSetupType)
		{
			InitializeComponent();
			if (colSelectedSetupType == ColumnSetupType.TankView)
				popupColumnSetupType.Text = "Tank View";
			else if (colSelectedSetupType == ColumnSetupType.BattleView)
				popupColumnSetupType.Text = "Battle View";
			colSetupType = colSelectedSetupType;
			
		}

		#region Load and Style

		private void ColumnSetup_Load(object sender, EventArgs e)
		{
			// Style toolbar
			toolAllColumns.Renderer = new StripRenderer();
			toolAllColumns.BackColor = ColorTheme.FormBackTitle;
			toolAllColumns.ShowItemToolTips = false;
			toolSelectedColumns.Renderer = new StripRenderer();
			toolSelectedColumns.BackColor = ColorTheme.FormBackTitle;
			toolSelectedColumns.ShowItemToolTips = false;
			// Style datagrid
			StyleDataGrid(dataGridColumnSetup);
			StyleDataGrid(dataGridAllColumns);
			StyleDataGrid(dataGridSelectedColumns);
			// Show content
			ShowColumnSetupList();
		}

		class StripRenderer : ToolStripProfessionalRenderer
		{
			public StripRenderer()
				: base(new Code.StripLayout())
			{
				this.RoundedEdges = false;
			}

			protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
			{
				base.OnRenderItemText(e);
				e.Item.ForeColor = ColorTheme.ToolWhiteToolStrip;
			}
		}

		private void dataGrid_Paint(object sender, PaintEventArgs e)
		{
			DataGridView dgv = (DataGridView)sender;
			e.Graphics.DrawRectangle(new Pen(ColorTheme.ScrollbarBack), 0, 0, dgv.Width - 1, dgv.Height - 1);
		}

		private void StyleDataGrid(DataGridView dgv)
		{
			dgv.BorderStyle = BorderStyle.FixedSingle;
			dgv.BackgroundColor = ColorTheme.FormBack;
			dgv.GridColor = ColorTheme.GridBorders;
			dgv.EnableHeadersVisualStyles = false;
			dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dgv.ColumnHeadersHeight = 26;
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
		}

		private void toolItem_Checked_paint(object sender, PaintEventArgs e)
		{
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			if (menu.Checked)
			{
				if (menu.Image == null)
				{
					// Default checkbox
					e.Graphics.DrawImage(imageListToolStrip.Images[0], 5, 3);
					e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 4, 2, 17, 17);
					e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 5, 3, 15, 15);
				}
				else
				{
					// Border around picture
					e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 3, 1, 19, 19);
					e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 4, 2, 17, 17);
				}

			}
		}

		#endregion

		private void popupColumnSetupType_Click(object sender, EventArgs e)
		{
			string colSelectedSetupType = Code.PopupGrid.Show("Select Column Setup Type", Code.PopupGrid.PopupGridType.List, "Tank View,Battle View");
			if (colSelectedSetupType != "")
			{
				popupColumnSetupType.Text = colSelectedSetupType;
				if (colSelectedSetupType == "Tank View")
					colSetupType = ColumnSetupType.TankView;
				else if (colSelectedSetupType == "Battle View")
					colSetupType = ColumnSetupType.BattleView;
				ShowColumnSetupList();
			}
		}

		private void ShowColumnSetupList()
		{
			if (colSetupType == ColumnSetupType.TankView)
			{
				lblDefaultColumnSetup.Text = "This setup is used as default for Tank View.";
				lblDefaultColumnSetup.ForeColor = Color.ForestGreen;
			}
			else if (colSetupType == ColumnSetupType.BattleView)
			{
				lblDefaultColumnSetup.Text = "This setup is not used as default for Battle View.";
				lblDefaultColumnSetup.ForeColor = ColorTheme.ControlFont;
			}
		}

	}
}
