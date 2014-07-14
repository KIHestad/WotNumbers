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

		private void PlayerTankDetails_Load(object sender, EventArgs e)
		{
			if (initPlayerTankId != 0)
			{
				// Get tank id and name
				string sql = "select tank.id, tank.name from tank inner join playerTank on tank.id=playerTank.tankId where playerTank.id=@id; ";
				DB.AddWithValue(ref sql, "@id", initPlayerTankId, DB.SqlDataType.Int);
				DataRow dr = DB.FetchData(sql).Rows[0];
				string tankName = dr["name"].ToString();
				int tankId = Convert.ToInt32(dr["id"]);
				// Show name
				PlayerTankDetailsTheme.Text += " - " + tankName;
				// Show pictures
				picLarge.Image = ImageHelper.GetTankImage(tankId,"img");
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

		
	}
}
