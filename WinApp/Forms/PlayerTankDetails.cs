using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class PlayerTankDetails : Form
	{
		int initPlayerTankId = 0;
		public PlayerTankDetails(int playerTankId = 0)
		{
			InitializeComponent();
			initPlayerTankId = playerTankId;
		}

		private void StyleDataGrid(DataGridView dgv)
		{
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

		private void PlayerTankDetails_Load(object sender, EventArgs e)
		{
			// Style datagrid
			StyleDataGrid(dataGridTankDetail);
			ResizeNow();
			if (initPlayerTankId != 0)
			{
				// Get tank id and name
				string sql = 
					"SELECT tank.*, tankType.name as typeName, country.name as countryName  " +
					"FROM     tank INNER JOIN " + Environment.NewLine +
					"         playerTank ON tank.id = playerTank.tankId INNER JOIN " + Environment.NewLine +
					"         tankType ON tank.tankTypeId = tankType.id INNER JOIN " + Environment.NewLine +
					"         country ON tank.countryId = country.id INNER JOIN " + Environment.NewLine +
					"         playerTankBattleTotalsView ON playerTankBattleTotalsView.playerTankId = playerTank.id LEFT OUTER JOIN " + Environment.NewLine +
					"         modTurret ON playerTank.modTurretId = modTurret.id LEFT OUTER JOIN " + Environment.NewLine +
					"         modRadio ON modRadio.id = playerTank.modRadioId LEFT OUTER JOIN " + Environment.NewLine +
					"         modGun ON playerTank.modGunId = modGun.id " +
					"WHERE playerTank.id=@id; ";
				
				DB.AddWithValue(ref sql, "@id", initPlayerTankId, DB.SqlDataType.Int);
				DataRow dr = DB.FetchData(sql).Rows[0];
				string tankName = dr["name"].ToString();
				int tankId = Convert.ToInt32(dr["id"]);
				// Show name in title bar
				PlayerTankDetailsTheme.Text += " - " + tankName;
				// Show picture
				picLarge.Image = ImageHelper.GetTankImage(tankId,"img");
				// Show tank name and info in lables
				//lblTankName.Text = tankName;
				//lblTankInfo1.Text = "Tier: " + dr["tier"].ToString();
				//lblTankInfo2.Text = "Type: " + dr["typeName"].ToString();
				//lblTankInfo3.Text = "Nation: " + dr["countryName"].ToString();
				// Grid
				sql = "select name as'Data', NULL as 'Value' from columnSelection where colType=1 and colDataType <> 'Image' ORDER BY position";
				DataTable dtGrid = DB.FetchData(sql);
				dataGridTankDetail.DataSource = dtGrid;

			}
		}

		private static Image GetImage(int i, int tankId = 1)
		{
			DataTable dtImg = DB.FetchData("SELECT img, smallImg, contourImg FROM tank WHERE id=" + tankId);
			Image image = null;
			if (dtImg.Rows.Count > 0)
			{
				byte[] rawImg = (byte[])dtImg.Rows[0][i];
				MemoryStream ms = new MemoryStream(rawImg);
				image = Image.FromStream(ms);
				ms.Close();
			}
			else
			{
				Bitmap noPicure = new Bitmap(10, 10);
				image = noPicure;
			}
			return image;
		}

		private void ResizeNow()
		{
			dataGridTankDetail.Width = PlayerTankDetailsTheme.MainArea.Width - scrollTankDetails.Width - 1;
			dataGridTankDetail.Height = PlayerTankDetailsTheme.MainArea.Top + PlayerTankDetailsTheme.MainArea.Height - dataGridTankDetail.Top;
			scrollTankDetails.Left = PlayerTankDetailsTheme.MainArea.Right - scrollTankDetails.Width -1;
			scrollTankDetails.Height = dataGridTankDetail.Height;
		}

		private void PlayerTankDetails_Resize(object sender, EventArgs e)
		{
			ResizeNow();
		}

		private void PlayerTankDetails_ResizeEnd(object sender, EventArgs e)
		{
			ResizeNow();
		}

	}
}
