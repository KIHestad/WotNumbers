using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Gadget
{
	public partial class paramTotalStats : Form
	{
		int _gadgetId = -1;

        public paramTotalStats(int gadgetId = -1)
		{
			InitializeComponent();
			_gadgetId = gadgetId;
		}

        private void paramTotalStats_Load(object sender, EventArgs e)
		{
			object[] currentParameters = new object[] { null, null, null, null, null };
			if (_gadgetId > -1)
			{
				// Lookup value for current gadget
				string sql = "select * from gadgetParameter where gadgetId=@gadgetId order by paramNum;";
				DB.AddWithValue(ref sql, "@gadgetId", _gadgetId, DB.SqlDataType.Int);
				DataTable dt = DB.FetchData(sql, Config.Settings.showDBErrors);
				foreach (DataRow dr in dt.Rows)
				{
		 			object paramValue = dr["value"];
					int paramNum = Convert.ToInt32(dr["paramNum"]);
					currentParameters[paramNum] = paramValue;
				}
                // Settings values
                if (currentParameters[0] != null)
                    ddBattleMode.Text = BattleMode.GetItemFromSqlName(currentParameters[0].ToString()).Name;
                if (currentParameters[1] != null)
                    ddTimeSpan.Text = GadgetHelper.GetTimeItemFromName(currentParameters[1].ToString()).Name;
                if (currentParameters[2] != null)
                    ddGridCount.Text = currentParameters[2].ToString();
                
                // All colums section
                // Style toolbar and set buttons
                toolAllColumns.Renderer = new StripRenderer();
                toolAllColumns = ColListHelper.SetToolStripColType(toolAllColumns, GridView.Views.Tank);
                toolAvailableCol_All.Checked = true;
                // Style datagrid
                GridHelper.StyleDataGrid(dataGridAllColumns);
                // Get all columns data source
                SetAllColumnsDataGrid();

                // Selected columns section
                // Style toolbar 
                toolSelectedColumns.Renderer = new StripRenderer();
                // Style datagrid
                GridHelper.StyleDataGrid(dataGridSelectedColumns, DataGridViewSelectionMode.CellSelect);
                // Get all columns data source
                SetSelectedColumnsDataGrid();

			}
		}

        private void SetAllColumnsDataGrid()
        {
            dataGridAllColumns.DataSource = ColListHelper.GetDataGridColums(toolAllColumns, GridView.Views.Tank);
            dataGridAllColumns.Columns["Description"].Width = 300;
            dataGridAllColumns.Columns["id"].Visible = false;
            dataGridAllColumns.Columns["colWidth"].Visible = false;
            // Connect to scrollbar
            scrollAllColumns.ScrollElementsTotals = dataGridAllColumns.RowCount;
            scrollAllColumns.ScrollElementsVisible = dataGridAllColumns.DisplayedRowCount(false);
        }

        private void SetSelectedColumnsDataGrid()
        {
            string sql = "";
            int cols = Convert.ToInt32(ddGridCount.Text);
            for (int i = 1; i < (cols+1); i++)
			{
			    sql += "CAST (null AS INT) as col" + i.ToString() +"_ID, CAST (null AS VARCHAR) as 'Column " + i.ToString() +"', ";
			}
            sql = sql.Substring(0, sql.Length-2);
            sql = "select " + sql;
            dataGridSelectedColumns.DataSource = DB.FetchData(sql, false);
            int colCount = 0;
            foreach (DataGridViewColumn col in dataGridSelectedColumns.Columns)
            {
                colCount ++;
                if (colCount == 1)
                    col.Visible = false;
                else
                    colCount = 0;

            }
        }


        private void toolAvaliableCol_Group_Click(object sender, EventArgs e)
        {
            foreach (ToolStripButton button in toolAllColumns.Items)
			{
				button.Checked = false;
			}
            ToolStripButton selectedButton = (ToolStripButton)sender;
            selectedButton.Checked = true;
            SetAllColumnsDataGrid();
        }

		private void ddBattleMode_Click(object sender, EventArgs e)
		{
			DropDownGrid.Show(ddBattleMode, DropDownGrid.DropDownGridType.List, BattleMode.GetDropDownList(true));
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			if (ddBattleMode.Text == "")
			{
				MsgBox.Show("Please select a battle mode", "Missing battle mode");
			}
            else if (ddTimeSpan.Text == "")
            {
                MsgBox.Show("Please select a time span", "Missing time span");
            }
			else
			{
				string paramBattleMode = "";
                BattleMode.Item battleMode = BattleMode.GetItemFromName(ddBattleMode.Text);
                if (battleMode != null)
                    paramBattleMode = battleMode.SqlName;
                string paramTimeSpan = "";
                GadgetHelper.TimeItem ti = GadgetHelper.GetTimeItemFromName(ddTimeSpan.Text);
                if (ti != null)
                    paramTimeSpan = ti.Name;
				GadgetHelper.newParameters[0] = paramBattleMode;
                GadgetHelper.newParameters[1] = paramTimeSpan;
                GadgetHelper.newParameters[2] = Convert.ToInt32(ddGridCount.Text);
				GadgetHelper.newParametersOK = true;
				this.Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			GadgetHelper.newParameters = new object[] { null, null, null, null, null };
			GadgetHelper.newParametersOK = false;
			this.Close();
		}

		private void paramBattleMode_Paint(object sender, PaintEventArgs e)
		{
			if (BackColor == ColorTheme.FormBackSelectedGadget)
				GadgetHelper.DrawBorderOnGadget(sender, e);
		}

        private void ddTimeSpan_Click(object sender, EventArgs e)
        {
            DropDownGrid.Show(ddTimeSpan, DropDownGrid.DropDownGridType.List, GadgetHelper.GetTimeDropDownList());
        }

        private void ddGridCount_Click(object sender, EventArgs e)
        {
            DropDownGrid.Show(ddGridCount, DropDownGrid.DropDownGridType.List, "1,2,3,4,5");
        }

        private void badForm1_Resize(object sender, EventArgs e)
        {
            // Size dataGridAllColumns
            int totalWidth = groupRows.Width - 60;
            int dataGridAllColumnsWidht = Convert.ToInt32(totalWidth * 0.30);
            toolAllColumns.Width = dataGridAllColumnsWidht;
            dataGridAllColumns.Width = dataGridAllColumnsWidht - scrollAllColumns.Width;
            scrollAllColumns.Left = dataGridAllColumns.Left + dataGridAllColumns.Width;
            // Size and position dataGridSelectedColumns
            int pos = toolAllColumns.Left + toolAllColumns.Width + 20;
            int dataGridSelectedColumnsWidht = Convert.ToInt32(totalWidth * 0.70);
            lblSelectedColumns.Left = pos;
            toolSelectedColumns.Left = pos;
            dataGridSelectedColumns.Left = pos;
            toolSelectedColumns.Width = dataGridSelectedColumnsWidht;
            dataGridSelectedColumns.Width = dataGridSelectedColumnsWidht;

        }

        private void ddGridCount_TextChanged(object sender, EventArgs e)
        {
            SetSelectedColumnsDataGrid();
        }


	}
}
