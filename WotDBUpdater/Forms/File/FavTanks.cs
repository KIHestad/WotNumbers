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
	public partial class FavTanks : Form
	{
		public FavTanks()
		{
			InitializeComponent();
		}

		private void FavTanks_Load(object sender, EventArgs e)
		{
			// Style toolbar
			toolAllTanks.Renderer = new StripRenderer();
			toolAllTanks.BackColor = ColorTheme.FormBackTitle;
			toolAllTanks.ShowItemToolTips = false;
			toolSelectedTanks.Renderer = new StripRenderer();
			toolSelectedTanks.BackColor = ColorTheme.FormBackTitle;
			toolSelectedTanks.ShowItemToolTips = false;
			// Style datagrid
			StyleDataGrid(dataGridFavList);
			StyleDataGrid(dataGridAllTanks);
			StyleDataGrid(dataGridSelectedTanks);
			// Show content
			ShowFavList();
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

		private void ShowFavList()
		{
			DataTable dt = db.FetchData("select position as 'Pos', name as 'Name' from favList order by position, name");
			dataGridFavList.DataSource = dt;
			bool buttonsEnabled = (dt.Rows.Count > 0);
			btnFavListCancel.Enabled = buttonsEnabled;
			btnFavListSave.Enabled = buttonsEnabled;
			btnFavListDelete.Enabled = buttonsEnabled;
			SelectFavList();
		}

		private void btnFavListAdd_Click(object sender, EventArgs e)
		{
			string newFavListName = txtFavListName.Text.Trim();
			if (newFavListName.Length > 0)
			{
				// CheckBox if exists
				DataTable dt = db.FetchData("select id from favList where name='" + newFavListName + "'");
				if (dt.Rows.Count > 0)
				{
					Code.MsgBox.Show("Cannot add new favourite tank list with this name, already in use.", "Cannot create favourite tank list");
				}
				else
				{
					AddFavList();
				}
			}
		}

		private void AddFavList()
		{
			string newFavListName = txtFavListName.Text.Trim();
			string newFavListPos = popupPosition.Text;
			if (newFavListPos == "Not Visible") newFavListPos = "NULL";
			// Change position on existing
			string sql = "update favlist set position = position + 1 where position >= @newFavListPos; ";
			// Remove positions above 10
			sql += "update favlist set position = NULL where position > 10; ";
			// Add new favlist
			sql += "insert into favList (position, name) values (@newFavListPos, '@newFavListName'); ";
			// Add parameters
			sql = sql.Replace("@newFavListPos", newFavListPos);
			sql = sql.Replace("@newFavListName", newFavListName);
			// Execute now
			db.ExecuteNonQuery(sql);
			// Refresh Grid
			ShowFavList();
		}

		private void popupPosition_Click(object sender, EventArgs e)
		{
			string posList = "Not Visible,1,2,3,4,5,6,7,8,9,10";
			popupPosition.Text = Code.PopupGrid.Show("Select Position", Code.PopupGrid.PopupGridType.List, posList);
		}

		private void dataGrid_Paint(object sender, PaintEventArgs e)
		{
			DataGridView dgv = (DataGridView)sender;
			e.Graphics.DrawRectangle(new Pen(ColorTheme.ScrollbarBack), 0, 0, dgv.Width-1, dgv.Height-1);
		}

		private void btnFavListDelete_Click(object sender, EventArgs e)
		{
			Code.MsgBox.Button answer = MsgBox.Show("Are you sure you want to delete favourite tank list: " + txtFavListName.Text,
				"Confirm deletion", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{
				string sql = "delete from favList where name='" + txtFavListName.Text + "'";
				db.ExecuteNonQuery(sql);
				ShowFavList();
			}
		}

		private void dataGridFavList_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			SelectFavList();
		}

		private void SelectFavList()
		{
			int selectedRowCount = dataGridFavList.Rows.GetRowCount(DataGridViewElementStates.Selected);
			if (selectedRowCount > 0)
			{
				txtFavListName.Text = dataGridFavList.SelectedRows[0].Cells[1].Value.ToString();
				popupPosition.Text = dataGridFavList.SelectedRows[0].Cells[0].Value.ToString();
				if (popupPosition.Text == "") popupPosition.Text = "Not Visible";
			}
			else
			{
				txtFavListName.Text = "";
				popupPosition.Text = "Not Visible";
			}
		}

		private void btnFavListCancel_Click(object sender, EventArgs e)
		{
			SelectFavList();
		}

		private void btnFavListSave_Click(object sender, EventArgs e)
		{
			string oldFavListName = dataGridFavList.SelectedRows[0].Cells[1].Value.ToString();
			string message = "You are about to save favourite tank list: " +  txtFavListName.Text;
			if (txtFavListName.Text != oldFavListName)
				message = "You are about to save and rename favourite tank list: " + oldFavListName + " to new name: " + txtFavListName.Text;
			Code.MsgBox.Button answer = MsgBox.Show(message,"Save existing favourite tank list", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{
				SaveFavList();
			}
		}

		private void SaveFavList()
		{
			string oldFavListName = dataGridFavList.SelectedRows[0].Cells[1].Value.ToString();
			string newFavListName = txtFavListName.Text.Trim();
			string newFavListPos = popupPosition.Text;
			if (newFavListPos == "Not Visible") newFavListPos = "NULL";
			// Change position on existing
			string sql = "update favlist set position = position + 1 where position >= @newFavListPos; ";
			// Remove positions above 10
			sql += "update favlist set position = NULL where position > 10; ";
			// Add new favlist
			sql += "update favList set position=@newFavListPos, name='@newFavListName' where name ='@oldFavListName'; ";
			// Add parameters
			sql = sql.Replace("@newFavListPos", newFavListPos);
			sql = sql.Replace("@newFavListName", newFavListName);
			sql = sql.Replace("@oldFavListName", oldFavListName);
			// Execute now
			db.ExecuteNonQuery(sql);
			// Refresh Grid
			ShowFavList();
		}


	}
}
