using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinApp.Code
{
	class ImageHelper
	{
		public static DataTable tankImg;
		
		public static void LoadTankImages()
		{
			string adminDB = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\Database\\Admin.db";
			string adminDbCon = "Data Source=" + adminDB + ";Version=3;PRAGMA foreign_keys = ON;";
			string sql = "select * from tank";
			SQLiteConnection con = new SQLiteConnection(adminDbCon);
			con.Open();
			SQLiteCommand command = new SQLiteCommand(sql, con);
			SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
			DataTable dt = new DataTable();
			adapter.Fill(dt);
			con.Close();
			DataTable tankImgNew = CreateTankImg();
			foreach (DataRow dr in dt.Rows)
			{
				DataRow tankImgNewDataRow = tankImgNew.NewRow();
				// ID
				tankImgNewDataRow["id"] = dr["id"];
				// Img
				byte[] imgByte = (byte[])dr["img"];
				MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
				ms.Write(imgByte, 0, imgByte.Length);
				Image image = new Bitmap(ms);
				tankImgNewDataRow["img"] = image;
				// SmallImg
				imgByte = (byte[])dr["smallImg"];
				ms = new MemoryStream(imgByte, 0, imgByte.Length);
				ms.Write(imgByte, 0, imgByte.Length);
				image = new Bitmap(ms);
				tankImgNewDataRow["smallImg"] = image;
				// ContourImg
				imgByte = (byte[])dr["contourImg"];
				ms = new MemoryStream(imgByte, 0, imgByte.Length);
				ms.Write(imgByte, 0, imgByte.Length);
				image = new Bitmap(ms);
				tankImgNewDataRow["contourImg"] = image;
				// Add to dt
				tankImgNew.Rows.Add(tankImgNewDataRow);
			}
			// Set public static tank img datatable to this
			tankImg = tankImgNew;
		}

		private static DataTable CreateTankImg()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("id", typeof(Int32));
			dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
			dt.Columns.Add("img", typeof(Image));
			dt.Columns.Add("smallimg", typeof(Image));
			dt.Columns.Add("contourimg", typeof(Image));
			return dt;
		}

	}
}
