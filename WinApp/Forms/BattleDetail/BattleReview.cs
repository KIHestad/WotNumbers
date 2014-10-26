using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;
using System.Drawing.Imaging;

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
		private Form parentForm;
		public BattleReview(int showBattleId, Form selectedParentForm)
		{
			InitializeComponent();
			battleId = showBattleId;
			parentForm = selectedParentForm;
		}
		// Paintig objects
		Bitmap bitmap; // bitmap object to hold the painting
		Graphics graphics; // graphics object to perform painting
		// Custom cursors
		Cursor[] cursorEraser = new Cursor[3];
		Cursor[] cursorPainting = new Cursor[3];

		#region Main

		private void BattleMapAndComment_Load(object sender, EventArgs e)
		{
			// Paint toolstrip
			toolStripPaint.Renderer = new CustomStripRenderer();
			// Init paint area
			bitmap = new Bitmap(300, 300, PixelFormat.Format32bppArgb);
			graphics = Graphics.FromImage(bitmap);
			// Check if painting exists
			string sql = "select painting from battleMapPaint where battleId=@battleId";
			DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
			DataTable dtBtlMapPaint = DB.FetchData(sql);
			if (dtBtlMapPaint.Rows.Count > 0)
			{
				paintingExists = true;
				byte[] imgByte = (byte[])dtBtlMapPaint.Rows[0]["painting"];
				bitmap = new Bitmap(ImageHelper.ByteArrayToImage(imgByte));
				picPaint.Image = bitmap;
			}
			// Custom cursors
			cursorPainting[0] = CursorHelper.CreateCursor(imageListCursors.Images[0], 2, 2); 
			cursorPainting[1] = CursorHelper.CreateCursor(imageListCursors.Images[1], 3, 3); 
			cursorPainting[2] = CursorHelper.CreateCursor(imageListCursors.Images[2], 4, 4); 
			cursorEraser[0] = CursorHelper.CreateCursor(imageListCursors.Images[3], 4, 4);
			cursorEraser[1] = CursorHelper.CreateCursor(imageListCursors.Images[4], 9, 9);
			cursorEraser[2] = CursorHelper.CreateCursor(imageListCursors.Images[5], 19, 19);

			// Fetch battle data data
			sql = 
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
				panelMap.BackgroundImage = ImageHelper.GetMap(arena_id);
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
				"select battleTime as 'Battle Time', comment as 'Battle Comment', battle.id as battleId " +
				"from battle inner join playerTank on battle.playerTankId = playerTank.id " +
				"where comment is not null and battle.id <> @battleId " + where +
				"order by battleTime DESC; ";
			DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			dgvReviews.DataSource = dt;
			dgvReviews.Columns["battleId"].Visible = false;
		}

		private void dgvReviews_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			int battleReviewBattleId = Convert.ToInt32(dgvReviews.Rows[e.RowIndex].Cells["battleId"].Value);
			Form frm = new Forms.BattleDetail(battleReviewBattleId, parentForm);
			frm.Show(parentForm);
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

		#endregion

		#region painting

		// Parameters to control painting
		bool startPaint = false;
		bool eraseMode = false;
		Color penColor = Color.White;
		int penSize = 2;
		int cursorSize = 1;
		bool paintingExists = false;
		Cursor paintCursor = Cursors.Cross;

		private void picPaint_MouseDown(object sender, MouseEventArgs e)
		{
			startPaint = true;
		}

		private void picPaint_MouseMove(object sender, MouseEventArgs e)
		{
			if (startPaint)
			{
				if (eraseMode)
				{
					// Erasing
					graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
					using (var br = new SolidBrush(Color.FromArgb(0, 255, 255, 255)))
					{
						graphics.FillEllipse(br, e.X - penSize * 5, e.Y - penSize * 5, penSize * 10, penSize * 10);
					}
					picPaint.Image = bitmap;
				}
				else
				{
					// Hide cursor
					picPaint.Cursor = cursorPainting[cursorSize];

					// Painting
					SolidBrush sb = new SolidBrush(penColor);
					graphics.FillEllipse(sb, e.X - penSize, e.Y - penSize, penSize * 2, penSize * 2);
					picPaint.Image = bitmap;
					paintingExists = true;
				}

			}
		}

		private void picPaint_MouseUp(object sender, MouseEventArgs e)
		{
			startPaint = false;
			picPaint.Cursor = paintCursor;
		}


		private void PaintingPenColor_Click(object sender, EventArgs e)
		{
			mPenWhite.Checked = false;
			mPenBlack.Checked = false;
			mPenRed.Checked = false;
			mPenOrange.Checked = false;
			mPenYellow.Checked = false;
			mPenGreen.Checked = false;
			mPenBlue.Checked = false;
			mPenPink.Checked = false;
			mEraser.Checked = false;
			eraseMode = false;
			ToolStripButton btn = (ToolStripButton)sender;
			btn.Checked = true;
			Application.DoEvents();
			if (mEraser.Checked)
			{
				eraseMode = true;
				paintCursor = cursorEraser[cursorSize];
			}
			else
			{
				eraseMode = false;
				paintCursor = Cursors.Cross;

			}
			penColor = ColorTranslator.FromHtml("#" + btn.Tag.ToString());
			picPaint.Cursor = paintCursor;
		}

		private void PaintingPenSize_Click(object sender, EventArgs e)
		{
			mSizeLarge.Checked = false;
			mSizeMedium.Checked = false;
			mSizeSmall.Checked = false;
			ToolStripButton btn = (ToolStripButton)sender;
			btn.Checked = true;
			penSize = Convert.ToInt32(btn.Tag);
			switch (penSize)
			{
				case 1: cursorSize = 0; break;
				case 2: cursorSize = 1; break;
				case 4: cursorSize = 2; break;
			}
			if (eraseMode)
			{
				paintCursor = cursorEraser[cursorSize];
				picPaint.Cursor = paintCursor;
			}
		}

		private void mClear_Click(object sender, EventArgs e)
		{
			bitmap = new Bitmap(300, 300, PixelFormat.Format32bppArgb);
			graphics = Graphics.FromImage(bitmap);
			picPaint.Image = bitmap;
			paintingExists = false;
		}

		private void mSave_Click(object sender, EventArgs e)
		{
			// Delete previous painting
			string sql = "DELETE FROM battleMapPaint WHERE battleId=" + battleId;
			DB.ExecuteNonQuery(sql);
			// Save new painting if exists
			if (paintingExists)
			{
				sql = "INSERT INTO battleMapPaint (battleId, painting) VALUES (@battleId, @painting)";
				DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
				DB.ExecuteNonQuery(sql, imgParameter: "@painting", img: picPaint.Image);
			}
		}

		#endregion

	}
}
