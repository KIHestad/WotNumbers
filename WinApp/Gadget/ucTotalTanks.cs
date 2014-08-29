using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Gadget
{
	public partial class ucTotalTanks : UserControl
	{
		public ucTotalTanks()
		{
			InitializeComponent();
		}

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			DataBind();
			base.OnInvalidated(e);
		}

		private void ucPlayerInfo_Load(object sender, EventArgs e)
		{
			DataBind();
		}

		private void DataBind()
		{
			GridHelper.StyleDataGrid(dataGridView1);
			string sql =
				"SELECT  tankType.shortName AS Type, COUNT(playerTank.id) AS Tanks, SUM(playerTankBattleTotalsView.battles) AS Battles, SUM(playerTankBattleTotalsView.wins) As Victory " +
				"FROM    playerTank INNER JOIN " +
				"        tank ON playerTank.tankId = tank.id INNER JOIN " +
				"        tankType ON tank.tankTypeId = tankType.id LEFT OUTER JOIN " +
				"        playerTankBattleTotalsView ON playerTank.id = playerTankBattleTotalsView.playerTankId " +
				"WHERE        (playerTank.playerId = @playerId) " +
				"GROUP BY tankType.shortName ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			// create table structure
			sql = "Select 'Tanks owned' as Data, CAST(0 AS FLOAT) as 'Light', CAST(0 AS FLOAT) as 'Medium', CAST(0 AS FLOAT) as 'Heavy', CAST(0 AS FLOAT) as 'TD', CAST(0 AS FLOAT) as 'SPG', CAST(0 AS FLOAT) as 'Total' ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtResult = DB.FetchData(sql);
			DataRow drBattles = dtResult.NewRow();
			drBattles["Data"] = "Battle Count";
			dtResult.Rows.Add(drBattles);
			DataRow drWR = dtResult.NewRow();
			drWR["Data"] = "Win Rate";
			dtResult.Rows.Add(drWR);
			dtResult.AcceptChanges();
			// Populate data into result table
			double totBattles = 0;
			double totVictory = 0;
			double totTanks = 0;
			foreach (DataRow dr in dt.Rows)
			{
				string tankType = dr["Type"].ToString();
				string resultCol = "";
				switch (tankType)
				{
					case "HT": resultCol = "Heavy"; break;
					case "LT": resultCol = "Light"; break;
					case "MT": resultCol = "Medium"; break;
					case "SPG": resultCol = "SPG"; break;
					case "TD": resultCol = "TD"; break;
				}
				double battles = 0;
				double victory = 0;
				double tanks = 0;
				if (dr["Tanks"] != DBNull.Value) tanks = Convert.ToDouble(dr["Tanks"]);
				if (dr["Battles"] != DBNull.Value) battles = Convert.ToDouble(dr["Battles"]);
				if (dr["Victory"] != DBNull.Value) victory = Convert.ToDouble(dr["Victory"]);
				dtResult.Rows[0][resultCol] = tanks;
				dtResult.Rows[1][resultCol] = battles;
				if (battles > 0)
					dtResult.Rows[2][resultCol] = victory / battles * 100;
				totBattles += battles;
				totTanks += tanks;
				totVictory += victory;
			}
			dtResult.Rows[0]["Total"] = totTanks;
			dtResult.Rows[1]["Total"] = totBattles;
			if (totBattles > 0)
				dtResult.Rows[2]["Total"] = totVictory / totBattles * 100;
			dtResult.AcceptChanges();
			dataGridView1.DataSource = dtResult;

			// Text cols
			dataGridView1.Columns["Data"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridView1.Columns["Data"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
			// No sorting
			for (int i = 0; i < dataGridView1.Columns.Count; i++)
			{
				dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
			}
			// Format
			for (int i = 1; i < dataGridView1.Columns.Count; i++)
			{
				dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
				dataGridView1.Columns[i].Resizable = DataGridViewTriState.False;
			}
			// No resize and Right align numbers
			dataGridView1.Columns[0].Resizable = DataGridViewTriState.False;
			// Finish
			dataGridView1.Columns[0].Width = 97;
			dataGridView1.Columns[1].Width = 53;
			dataGridView1.Columns[2].Width = 53;
			dataGridView1.Columns[3].Width = 53;
			dataGridView1.Columns[4].Width = 53;
			dataGridView1.Columns[5].Width = 53;
			dataGridView1.Columns[6].Width = 55;
		}

		private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			dataGridView1.ClearSelection();
		}

		private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			string col = dataGridView1.Columns[e.ColumnIndex].Name;
			DataGridViewCell cell = dataGridView1[e.ColumnIndex, e.RowIndex];
			if (e.RowIndex == 2 && e.ColumnIndex > 0)
			{
				if (cell.Value != DBNull.Value)
				{
					cell.Style.ForeColor = Rating.WinRateColor(Convert.ToDouble(cell.Value));
					cell.Style.SelectionForeColor = cell.Style.ForeColor;
					cell.Style.Format = "0.00";
				}
			}
			else if (e.RowIndex == 1 && e.ColumnIndex > 0)
			{
				cell.Style.Format = "N0";
			}
		}

		private void dataGridView1_Sorted(object sender, EventArgs e)
		{
			dataGridView1.ClearSelection();
		}
	}
}
