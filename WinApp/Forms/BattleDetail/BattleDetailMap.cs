using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
    public partial class BattleDetailMap : FormCloseOnEsc
    {
        private int battleId = 0;
        private BattleReview battleReviewForm;

        public BattleDetailMap(int showBattleId, BattleReview battleReviewForm)
        {
            battleId = showBattleId;
            this.battleReviewForm = battleReviewForm;
            InitializeComponent();
        }

        // Paintig objects
        Bitmap bitmap; // bitmap object to hold the painting
        Graphics graphics; // graphics object to perform painting
                           // Custom cursors
        Cursor[] cursorEraser = new Cursor[3];
        Cursor[] cursorPainting = new Cursor[3];

        private async void BattleDetailMap_Load(object sender, EventArgs e)
        {
            // Paint toolstrip
            toolStripPaint.Renderer = new StripRenderer();
            // Init paint area
            bitmap = new Bitmap(300, 300, PixelFormat.Format32bppArgb);
            graphics = Graphics.FromImage(bitmap);
            // Check if painting exists
            string sql = "select painting from battleMapPaint where battleId=@battleId";
            DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
            DataTable dtBtlMapPaint = await DB.FetchData(sql);
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
            DataRow dr = (await DB.FetchData(sql)).Rows[0];
            // Get map pictures and text
            if (dr["arena_id"] != DBNull.Value)
            {
                string arena_id = dr["arena_id"].ToString();
                panelMap.BackgroundImage = ImageHelper.GetMap(arena_id);
            }
            ResizeNow();
        }

        private void BattleDetailMap_FormClosed(object sender, FormClosedEventArgs e)
        {
            battleReviewForm.PaintMap();
        }

        private void BattleDetailMap_Resize(object sender, EventArgs e)
        {
            ResizeNow();
        }

        private void ResizeNow()
        {
            toolStripPaint.Width = BattleDetailMap.ActiveForm.Width - 40;
            // Calc scale, origin = 300 pixels, get highest value pic resized height or width
            scale = Math.Max(300f / picPaint.Width, 300f / picPaint.Height);
            // Skew top/left position of drawing as pic miht be moved down or to right, so drawing must be moved to left or up in ref to cursor pos
            skewX = (picPaint.Width - (300 / scale)) / 2 * scale;
            skewY = (picPaint.Height - (300 / scale)) / 2 * scale;
        }

        #region painting

        // Parameters to control painting
        bool paintingExists = false;
        bool startPaint = false;
        bool paintMode = true;
        Point lastPos;
        int cursorSize = 1;
        Color penColor = Color.White;
        Cursor paintCursor = Cursors.Cross;
        SolidBrush sb = new SolidBrush(Color.White);
        int penSize = 2;
        Pen pen = new Pen(Color.White, 4); // default size = penSize * 2
        // Resizing picture scale and skew to perform drawing
        float scale = 0.5f;
        float skewX = 0;
        float skewY = 0;

        private void picPaint_MouseDown(object sender, MouseEventArgs e)
        {
            startPaint = true;
            paintingExists = true;
            lastPos = e.Location;
            lastPos.X = Convert.ToInt32((lastPos.X * scale) - skewX);
            lastPos.Y = Convert.ToInt32((lastPos.Y * scale) - skewY);
            picPaint.Cursor = cursorPainting[cursorSize]; // draw cursor
            PaintNow(e);
        }

        private void picPaint_MouseMove(object sender, MouseEventArgs e)
        {
            PaintNow(e);
            var newPos = e.Location;
            newPos.X = Convert.ToInt32((newPos.X * scale) - skewX);
            newPos.Y = Convert.ToInt32((newPos.Y * scale) - skewY);
            if (startPaint && paintMode)
            {
                graphics.DrawLine(pen, lastPos, newPos);
            }
            lastPos = newPos;
        }

        private void picPaint_MouseUp(object sender, MouseEventArgs e)
        {
            startPaint = false;
            picPaint.Cursor = paintCursor;
        }

        private void PaintNow(MouseEventArgs e)
        {
            if (startPaint)
            {
                if (paintMode)
                {
                    // Painting
                    graphics.FillEllipse(sb, (e.X * scale) - skewX - penSize, (e.Y * scale) - skewY - penSize, penSize * 2, penSize * 2);
                    picPaint.Image = bitmap;
                }
                else
                {
                    // Erasing
                    graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    using (var br = new SolidBrush(Color.FromArgb(0, 255, 255, 255)))
                    {
                        graphics.FillEllipse(br, (e.X * scale) - skewX - penSize * 5, (e.Y * scale) - skewY - penSize * 5, penSize * 10, penSize * 10);
                    }
                    picPaint.Image = bitmap;
                }

            }
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
            paintMode = false;
            ToolStripButton btn = (ToolStripButton)sender;
            btn.Checked = true;
            Refresh();
            if (mEraser.Checked)
            {
                paintMode = false;
                paintCursor = cursorEraser[cursorSize];
            }
            else
            {
                paintMode = true;
                paintCursor = Cursors.Cross;

            }
            penColor = ColorTranslator.FromHtml("#" + btn.Tag.ToString());
            sb = new SolidBrush(penColor);
            pen = new Pen(sb, penSize * 2);
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
            if (!paintMode)
            {
                paintCursor = cursorEraser[cursorSize];
                picPaint.Cursor = paintCursor;
            }
            pen = new Pen(sb, penSize * 2);
        }

        private void mClear_Click(object sender, EventArgs e)
        {
            bitmap = new Bitmap(300, 300, PixelFormat.Format32bppArgb);
            graphics = Graphics.FromImage(bitmap);
            picPaint.Image = bitmap;
            paintingExists = false;
        }

        private async void mSave_Click(object sender, EventArgs e)
        {
            // Delete previous painting
            string sql = "DELETE FROM battleMapPaint WHERE battleId=" + battleId;
            await DB.ExecuteNonQuery(sql);
            // Save new painting if exists
            if (paintingExists)
            {
                sql = "INSERT INTO battleMapPaint (battleId, painting) VALUES (@battleId, @painting)";
                DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
                await DB.ExecuteNonQuery(sql, imgParameter: "@painting", img: picPaint.Image);
            }
        }


        #endregion

        
    }
}
