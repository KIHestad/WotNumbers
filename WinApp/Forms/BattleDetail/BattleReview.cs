using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class BattleReview : UserControl
	{
		private string lastBattleComment = "";		
		private int battleId = 0;
		private int tankId = 0;
		private int mapId = 0;
		private int clanId = 0;
		private string battleMode = "";
		public BattleReview(int showBattleId)
		{
			InitializeComponent();
			battleId = showBattleId;
		}

		private void BattleMapAndComment_Load(object sender, EventArgs e)
		{
			// Fetch battle data data
			string sql = 
				"select map.*, battle.comment as battleComment, playerTank.tankId as tankId, map.id as mapId, " + 
				"  battle.enemyClanDBID as enemyClanDBID, battle.battleMode as battleMode " +
				"from battle left join " +
				"  map on map.id = battle.mapId left join " +
				"  playerTank on battle.playerTankId = playerTank.Id " +
				"where battle.id=@battleId";
			DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
			DataRow dr = DB.FetchData(sql).Rows[0];
			// Get map pictures and text
			if (dr["arena_id"] != DBNull.Value)
			{
				string arena_id = dr["arena_id"].ToString();
				picMap.Image = ImageHelper.GetMap(arena_id);
				picIllustration.Image = ImageHelper.GetMap(arena_id, true);
				lblMapDescription.Text = dr["description"].ToString();
			}
			// get battle comment
			if (dr["battleComment"] != DBNull.Value)
			{
				lastBattleComment = dr["battleComment"].ToString();
				txtComment.Text = lastBattleComment;
			}
			// get tank & battle mode
			tankId = Convert.ToInt32(dr["tankId"]);
			battleMode = dr["battleMode"].ToString();
			// get map for filter
			if (dr["mapId"] != DBNull.Value)
				mapId = Convert.ToInt32(dr["mapId"]);
			else
				chkMap.Visible = false;
			// get clan for filter
			if (dr["enemyClanDBID"] != DBNull.Value)
				clanId = Convert.ToInt32(dr["enemyClanDBID"]);
			else
				chkClan.Visible = false;
			// Other other battle reviews
			GridHelper.StyleDataGrid(dgvReviews);
			GetOtherBattleReviews();
			ResizeNow();
		}

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			base.OnInvalidated(e);
		}

		private void GetOtherBattleReviews()
		{
			string where = "";
			if (chkTank.Checked)
				where += " and playerTank.tankId=" + tankId + " ";
			if (chkBattleMode.Checked)
			{
				where += " and battle.battleMode=@battleMode ";
				DB.AddWithValue(ref where, "@battleMode", battleMode, DB.SqlDataType.VarChar);
			}
			if (chkMap.Checked && chkMap.Visible)
				where += " and battle.mapId=" + mapId + " ";
			if (chkClan.Checked && chkClan.Visible)
				where += " and battle.enemyClanDBID=" + clanId + " ";
			string sql =
				"select battleTime as 'Battle Time', comment as 'Battle Comment' " +
				"from battle inner join playerTank on battle.playerTankId = playerTank.id " +
				"where comment is not null " + where +
				"order by battleTime DESC; ";
			DataTable dt = DB.FetchData(sql);
			dgvReviews.DataSource = dt;

		}

		private void btnCommentClear_Click(object sender, EventArgs e)
		{
			txtComment.Text = "";
		}

		private void btnCommentCancel_Click(object sender, EventArgs e)
		{
			txtComment.Text = lastBattleComment;
		}

		private void btnCommentSave_Click(object sender, EventArgs e)
		{
			lastBattleComment = txtComment.Text;
			string sql = "update battle set comment=@comment where id=@id";
			DB.AddWithValue(ref sql, "@comment", lastBattleComment, DB.SqlDataType.VarChar);
			DB.AddWithValue(ref sql, "@id", battleId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);
		}

		private void BattleReview_Resize(object sender, EventArgs e)
		{
			ResizeNow();
		}

		private void ResizeNow()
		{
			int margin = 15;
			int areaWith = this.Width - panelComment.Left - margin;
			int areaHeight = this.Height - panelComment.Top - margin;
			// Get new panel height
			int gridHeight = 364; // Minimum height for grid
			int commentHeight = areaHeight - gridHeight - 10; // subtract space between grids
			if (commentHeight > 120) commentHeight = 120; // Max height
			gridHeight = areaHeight - commentHeight - margin; // Available height to other btl review
			// Set panel Height
			panelComment.Height = commentHeight;
			panelGrid.Top = panelComment.Top + commentHeight + margin;
			panelGrid.Height = gridHeight;
			// Set panel Width
			panelComment.Width = areaWith;
			panelGrid.Width = areaWith;
			// Set comment controls
			picIllustration.Height = commentHeight;
			picIllustration.Width = (Convert.ToInt32(picIllustration.Height * 2.357142857142857));
			picIllustration.Left = areaWith - picIllustration.Width;
			int xpos = picIllustration.Left - 53;
			btnCommentCancel.Left = xpos;
			btnCommentClear.Left = xpos;
			btnCommentSave.Left = xpos;
			btnCommentSave.Top = commentHeight - btnCommentSave.Height;
			btnCommentCancel.Top = btnCommentSave.Top - 23;
			btnCommentClear.Top = btnCommentCancel.Top - 23;
			txtComment.Width = xpos - 21;
			txtComment.Height = commentHeight;
			scrollComment.Left = txtComment.Left + txtComment.Width;
			scrollComment.Height = commentHeight;
			// Grid Columns
			dgvReviews.Columns[0].Width = 95;
			dgvReviews.Columns[1].Width = dgvReviews.Width - dgvReviews.Columns[0].Width - 2;
		}

		private void dataGridViewFilterChanged(object sender, EventArgs e)
		{
			GetOtherBattleReviews();
		}
	}
}
