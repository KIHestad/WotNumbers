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
	public partial class ucBattleListLargeImages : UserControl
	{
		private int _cols;
		private int _rows;

		private class TankInfo
		{
			public PictureBox tankPic = new PictureBox();
			public Label battleTime = new Label();
			public Label battleResult = new Label();
		}
		private List<TankInfo> tankInfo = new List<TankInfo>();
	
		public ucBattleListLargeImages(int cols, int rows)
		{
			InitializeComponent();
			_cols = cols;
			_rows = rows;
		}

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			DataBind();
			base.OnInvalidated(e);
		}

		private void ucImage_Load(object sender, EventArgs e)
		{
			// Resize
			this.Width = _cols * 170 - 10;
			this.Height = _rows * 120 - 10;
			// Add Controls
			for (int row = 0; row < _rows; row++)
			{
				for (int col = 0; col < _cols; col++)
				{
					TankInfo newTank = new TankInfo();
					newTank.tankPic.Left = col * 170;
					newTank.tankPic.Top = row * 120;
					newTank.tankPic.Width = 160;
					newTank.tankPic.Height = 100;
					newTank.battleTime.BackColor = Color.Transparent;
					newTank.battleTime.ForeColor = ColorTheme.ControlFont;
					newTank.battleTime.Left = 0;
					newTank.battleTime.Top = 0;
					newTank.battleTime.AutoSize = true;
					newTank.battleResult.BackColor = Color.Transparent;
					newTank.battleResult.Left = 0;
					newTank.battleResult.Top = 16;
					newTank.battleResult.AutoSize = true;
					tankInfo.Add(newTank);
					this.Controls.Add(newTank.tankPic);
					this.Controls.Add(newTank.battleTime);
					this.Controls.Add(newTank.battleResult);
					newTank.battleTime.Parent = newTank.tankPic;
					newTank.battleResult.Parent = newTank.tankPic;
				}
			}
			DataBind();
		}

		private void DataBind()
		{
			// Images are 160x100
			// Get all tanks and show in imageGadget
			string sql =
				"select pt.tankId, b.battleTime, br.name, br.color " +
				"from battle b inner join " +
				"  playerTank pt on b.playerTankId = pt.Id inner join " +
				"  battleResult br on b.BattleResultId = br.id " +
				"where pt.playerId=@playerId " +
				"order by b.battleTime desc; ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable battle = DB.FetchData(sql);
			int rowCount = 0;
			for (int row = 0; row < _rows; row++)
			{
				for (int col = 0; col < _cols; col++)
				{
					// get a tank to show
					if (rowCount > battle.Rows.Count - 1)
					{
						// add default image
						Image tankImage = imageList1.Images[0];
						// Add content to controls
						tankInfo[rowCount].tankPic.Image = tankImage;
						tankInfo[rowCount].battleTime.Text = "";
						tankInfo[rowCount].battleResult.Text = "";
					}
					else
					{
						int tankId = Convert.ToInt32(battle.Rows[rowCount][0]);
						Image tankImage = ImageHelper.GetTankImage(tankId, "img");
						DateTime battleTime = Convert.ToDateTime(battle.Rows[rowCount]["battleTime"]);
						string result = battle.Rows[rowCount]["name"].ToString();
						string resultColor = battle.Rows[rowCount]["color"].ToString();
						// Add content to controls
						tankInfo[rowCount].tankPic.Image = tankImage;
						tankInfo[rowCount].battleTime.Text = battleTime.ToString("dd.MM.yy HH:mm");
						tankInfo[rowCount].battleResult.Text = result;
						tankInfo[rowCount].battleResult.ForeColor = System.Drawing.ColorTranslator.FromHtml(resultColor);
					}
					// go to next battle result
					rowCount++;
				}
			}
		}
	}
}
