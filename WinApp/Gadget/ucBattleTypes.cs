using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;
using WinApp.Code.FormLayout;

namespace WinApp.Gadget
{
	public partial class ucBattleTypes : UserControl
	{
		public ucBattleTypes()
		{
			InitializeComponent();
		}

		private object ShowDBnullIfZero(object val, int battlecount)
		{
			if (battlecount == 0)
				return DBNull.Value;
			else
				return val;
		}

		private void ucBattleTypes_Load(object sender, EventArgs e)
		{
			
		}

		public void DataBind()
		{
			GridHelper.StyleGadgetDataGrid(dataGridView1);
			// Create table structure, and get total number of used tanks to show in first row
            string colRandom = "Random";
            string colTeamUR = "Team-U";
            string colTeamR = "Team-R";
            string colSkirm = "Skirmish";
            string colStrong = "Strongh";
            string colGlbMap = "Glb-Map";
            string colTotal = "Total";
			string sql =
                "Select 'Tanks used' as Data, " +
                "  cast(0 as float) as '" + colRandom + "', " +
                "  cast(0 as float) as '" + colTeamUR + "', " +
                "  cast(0 as float) as '" + colTeamR + "', " +
                "  cast(0 as float) as '" + colSkirm + "', " +
                "  cast(0 as float) as '" + colStrong + "', " +
                "  cast(0 as float) as '" + colGlbMap + "', " +
                "  cast(count(playerTank.tankId) as float) as '" + colTotal + "' " +
				"from playerTank " +
				"where playerTank.playerId=@playerId and tankid in (" +
				"  select tankid from playerTankBattle ptb inner join playerTank pt on ptb.PlayerTankId = pt.id and pt.playerId=@playerId)";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql, Config.Settings.showDBErrors);
			// If no data quit
			if (dt.Rows.Count == 0) return;
			// 15
			sql =
				"Select count(playerTank.tankId) " +
				"from playerTank " +
				"where playerTank.playerId=@playerId and tankid in (" +
				"  select tankid from playerTankBattle ptb inner join playerTank pt on ptb.PlayerTankId = pt.id and pt.playerId=@playerId where ptb.battleMode = '15')";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			DataTable dtValue = DB.FetchData(sql, Config.Settings.showDBErrors);
			int usedRandom = 0;
			if (dtValue.Rows[0][0] != DBNull.Value) usedRandom = Convert.ToInt32(dtValue.Rows[0][0]);
            // 7
            sql =
                "Select count(playerTank.tankId) " +
                "from playerTank " +
                "where playerTank.playerId=@playerId and tankid in (" +
                "  select tankid from playerTankBattle ptb inner join playerTank pt on ptb.PlayerTankId = pt.id and pt.playerId=@playerId where ptb.battleMode = '7')";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
            dtValue = DB.FetchData(sql, Config.Settings.showDBErrors);
            int usedTeam = 0;
            if (dtValue.Rows[0][0] != DBNull.Value) usedTeam = Convert.ToInt32(dtValue.Rows[0][0]);
            // 7Ranked
            sql =
                "Select count(playerTank.tankId) " +
                "from playerTank " +
                "where playerTank.playerId=@playerId and tankid in (" +
                "  select tankid from playerTankBattle ptb inner join playerTank pt on ptb.PlayerTankId = pt.id and pt.playerId=@playerId where ptb.battleMode = '7Ranked')";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
            dtValue = DB.FetchData(sql, Config.Settings.showDBErrors);
            int usedTeamRanked = 0;
            if (dtValue.Rows[0][0] != DBNull.Value) usedTeamRanked = Convert.ToInt32(dtValue.Rows[0][0]);
			// Skirmishes
			sql =
				"Select count(playerTank.tankId) " +
				"from playerTank " +
				"where playerTank.playerId=@playerId and tankid in (" +
				"  select tankid from playerTankBattle ptb inner join playerTank pt on ptb.PlayerTankId = pt.id and pt.playerId=@playerId where ptb.battleMode = 'Skirmishes')";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			dtValue = DB.FetchData(sql, Config.Settings.showDBErrors);
			int usedSkirmishes = 0;
			if (dtValue.Rows[0][0] != DBNull.Value) usedSkirmishes = Convert.ToInt32(dtValue.Rows[0][0]);
			// Stronghold
			sql =
				"Select count(playerTank.tankId) " +
				"from playerTank " +
				"where playerTank.playerId=@playerId and tankid in (" +
				"  select tankid from playerTankBattle ptb inner join playerTank pt on ptb.PlayerTankId = pt.id and pt.playerId=@playerId where ptb.battleMode = 'Stronghold')";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			dtValue = DB.FetchData(sql, Config.Settings.showDBErrors);
			int usedStronghold = 0;
			if (dtValue.Rows[0][0] != DBNull.Value) usedStronghold = Convert.ToInt32(dtValue.Rows[0][0]);
            // Global Map
            sql =
                "Select count(playerTank.tankId) " +
                "from playerTank " +
                "where playerTank.playerId=@playerId and tankid in (" +
                "  select tankid from playerTankBattle ptb inner join playerTank pt on ptb.PlayerTankId = pt.id and pt.playerId=@playerId where ptb.battleMode = 'GlobalMap')";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
            dtValue = DB.FetchData(sql, Config.Settings.showDBErrors);
            int usedGlobalMap = 0;
            if (dtValue.Rows[0][0] != DBNull.Value) usedGlobalMap = Convert.ToInt32(dtValue.Rows[0][0]);


			// Add usage
			dt.Rows[0][colRandom] = usedRandom;
			dt.Rows[0][colTeamUR] = usedTeam;
			dt.Rows[0][colTeamR] = usedTeamRanked;
			dt.Rows[0][colSkirm] = usedSkirmishes;
            dt.Rows[0][colStrong] = usedStronghold;
            dt.Rows[0][colGlbMap] = usedGlobalMap;

			// get overall stats all battles
			double[] wr = new double[9];
			int[] battleCount = new int[9];
			sql =
				"select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
				"  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " +
				"from playerTankBattle ptb left join " +
				"  playerTank pt on ptb.playerTankId=pt.id left join " +
				"  tank t on pt.tankId = t.id " +
				"where pt.playerId=@playerId ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtStats = DB.FetchData(sql);
			if (dtStats.Rows.Count > 0 && dtStats.Rows[0]["battles"] != DBNull.Value)
			{
				// TOTALS
				DataRow stats = dtStats.Rows[0];
				// Battle count
				battleCount[0] = Convert.ToInt32(stats["battles"]);
				// win rate
				wr[0] = (Convert.ToDouble(stats["wins"]) / Convert.ToDouble(stats["battles"]) * 100);

				// Overall stats random
				sql =
					"select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
					"  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id left join " +
					"  tank t on pt.tankId = t.id " +
					"where pt.playerId=@playerId and ptb.battleMode='15'";
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				dtStats = DB.FetchData(sql);
				stats = dtStats.Rows[0];
				if (stats["battles"] != DBNull.Value && Convert.ToInt32(stats["battles"]) > 0)
				{
					// Battle count
					battleCount[1] = Convert.ToInt32(stats["battles"]);
					// win rate
					wr[1] = (Convert.ToDouble(stats["wins"]) / Convert.ToDouble(stats["battles"]) * 100);
				}

				// Overall stats team unranked
				sql =
					"select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
					"  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id left join " +
					"  tank t on pt.tankId = t.id " +
					"where pt.playerId=@playerId and ptb.battleMode='7'";
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				dtStats = DB.FetchData(sql);
				stats = dtStats.Rows[0];
				if (stats["battles"] != DBNull.Value && Convert.ToInt32(stats["battles"]) > 0)
				{
					// Battle count
					battleCount[2] = Convert.ToInt32(stats["battles"]);
					// win rate
					wr[2] = (Convert.ToDouble(stats["wins"]) / Convert.ToDouble(stats["battles"]) * 100);
				}

                // Overall stats team ranked
                sql =
                    "select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
                    "  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " +
                    "from playerTankBattle ptb left join " +
                    "  playerTank pt on ptb.playerTankId=pt.id left join " +
                    "  tank t on pt.tankId = t.id " +
                    "where pt.playerId=@playerId and ptb.battleMode='7Ranked'";
                DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
                dtStats = DB.FetchData(sql);
                stats = dtStats.Rows[0];
                if (stats["battles"] != DBNull.Value && Convert.ToInt32(stats["battles"]) > 0)
                {
                    // Battle count
                    battleCount[3] = Convert.ToInt32(stats["battles"]);
                    // win rate
                    wr[3] = (Convert.ToDouble(stats["wins"]) / Convert.ToDouble(stats["battles"]) * 100);
                }


				// Overall stats Skirmishes
				sql =
					"select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
					"  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id left join " +
					"  tank t on pt.tankId = t.id " +
					"where pt.playerId=@playerId and ptb.battleMode='Skirmishes'";
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				dtStats = DB.FetchData(sql);
				stats = dtStats.Rows[0];
				if (stats["battles"] != DBNull.Value && Convert.ToInt32(stats["battles"]) > 0)
				{
					// Battle count
					battleCount[4] = Convert.ToInt32(stats["battles"]);
					// win rate
					wr[4] = (Convert.ToDouble(stats["wins"]) / Convert.ToDouble(stats["battles"]) * 100);
				}

				// Overall stats Stronghold
				sql =
					"select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
					"  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id left join " +
					"  tank t on pt.tankId = t.id " +
					"where pt.playerId=@playerId and ptb.battleMode='Stronghold'";
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				dtStats = DB.FetchData(sql);
				stats = dtStats.Rows[0];
				if (stats["battles"] != DBNull.Value && Convert.ToInt32(stats["battles"]) > 0)
				{
					// Battle count
					battleCount[5] = Convert.ToInt32(stats["battles"]);
					// win rate
					wr[5] = (Convert.ToDouble(stats["wins"]) / Convert.ToDouble(stats["battles"]) * 100);
				}

                // Overall stats Stronghold
                sql =
                    "select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
                    "  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " +
                    "from playerTankBattle ptb left join " +
                    "  playerTank pt on ptb.playerTankId=pt.id left join " +
                    "  tank t on pt.tankId = t.id " +
                    "where pt.playerId=@playerId and ptb.battleMode='GlobalMap'";
                DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
                dtStats = DB.FetchData(sql);
                stats = dtStats.Rows[0];
                if (stats["battles"] != DBNull.Value && Convert.ToInt32(stats["battles"]) > 0)
                {
                    // Battle count
                    battleCount[6] = Convert.ToInt32(stats["battles"]);
                    // win rate
                    wr[6] = (Convert.ToDouble(stats["wins"]) / Convert.ToDouble(stats["battles"]) * 100);
                }

				// Add Data to dataTable
				DataRow dr = dt.NewRow();
				dr["Data"] = "Battle count";
				dr[colRandom] = battleCount[1];
				dr[colTeamUR] = ShowDBnullIfZero(battleCount[2], Convert.ToInt32(battleCount[2]));
				dr[colTeamR] = ShowDBnullIfZero(battleCount[3], Convert.ToInt32(battleCount[3]));
				dr[colSkirm] = ShowDBnullIfZero(battleCount[4], Convert.ToInt32(battleCount[4]));
                dr[colStrong] = ShowDBnullIfZero(battleCount[5], Convert.ToInt32(battleCount[5]));
                dr[colGlbMap] = ShowDBnullIfZero(battleCount[6], Convert.ToInt32(battleCount[6]));
                dr[colTotal] = battleCount[0].ToString();
				dt.Rows.Add(dr);

				// Add Winrate
				dr = dt.NewRow();
				dr["Data"] = "Win rate";
                dr[colRandom] = Math.Round(wr[1], 2);
                dr[colTeamUR] = ShowDBnullIfZero(Math.Round(wr[2], 2), Convert.ToInt32(battleCount[2]));
                dr[colTeamR] = ShowDBnullIfZero(Math.Round(wr[3], 2), Convert.ToInt32(battleCount[3]));
                dr[colSkirm] = ShowDBnullIfZero(Math.Round(wr[4], 2), Convert.ToInt32(battleCount[4]));
                dr[colStrong] = ShowDBnullIfZero(Math.Round(wr[5], 2), Convert.ToInt32(battleCount[5]));
                dr[colGlbMap] = ShowDBnullIfZero(Math.Round(wr[6], 2), Convert.ToInt32(battleCount[6]));
                dr[colTotal] = Math.Round(wr[0], 2);
				dt.Rows.Add(dr);
			}
			dataGridView1.DataSource = dt;

			// Text cols
			dataGridView1.Columns["Data"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridView1.Columns["Data"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
			// No sorting
			for (int i = 0; i < dataGridView1.Columns.Count; i++)
			{
				dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
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
			dataGridView1.Columns[0].Width = 70;
			dataGridView1.Columns[1].Width = 56;
            dataGridView1.Columns[2].Width = 56;
            dataGridView1.Columns[3].Width = 56;
            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[6].Width = 60;
		}

		private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			dataGridView1.ClearSelection();
		}

		private void dataGridView1_Sorted(object sender, EventArgs e)
		{
			dataGridView1.ClearSelection();
		}

		private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (this.Enabled)
			{
				string col = dataGridView1.Columns[e.ColumnIndex].Name;
				DataGridViewCell cell = dataGridView1[e.ColumnIndex, e.RowIndex];
				if (e.RowIndex == 2 && e.ColumnIndex > 0)
				{
					if (cell.Value != DBNull.Value)
					{
                        cell.Style.ForeColor = ColorRangeScheme.WinRateColor(Convert.ToDouble(cell.Value));
						cell.Style.SelectionForeColor = cell.Style.ForeColor;
						cell.Style.Format = "0.00";
					}
				}
				else if (e.RowIndex == 1 && e.ColumnIndex > 0)
				{
					if (cell.Value != DBNull.Value)
					{
						cell.Style.Format = "N0";
						if (e.ColumnIndex == 1)
                            cell.Style.ForeColor = ColorRangeScheme.BattleCountColor(Convert.ToInt32(cell.Value));
						cell.Style.SelectionForeColor = cell.Style.ForeColor;

					}
				}
			}
		}

		private void ucBattleTypes_Paint(object sender, PaintEventArgs e)
		{
			if (BackColor == ColorTheme.FormBackSelectedGadget)
				GadgetHelper.DrawBorderOnGadget(sender, e);
			dataGridView1.DefaultCellStyle.BackColor = BackColor;
		}
	}
}
