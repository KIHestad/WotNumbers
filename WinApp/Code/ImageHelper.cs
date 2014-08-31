using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinApp.Code
{
	class ImageHelper
	{
		public static DataTable TankImage = new DataTable();
		public static DataTable MasteryBadgeImage = new DataTable();

		public class ImgColumns : IComparable<ImgColumns>
		{
			public string colName { get; set; }
			public int colPosition { get; set; }

			public ImgColumns(string colName, int colPosition)
			{
				this.colName = colName;
				this.colPosition = colPosition;
			}

			public int CompareTo(ImgColumns other)
			{
				return this.colPosition.CompareTo(other.colPosition);
			}
		}

		public static void CreateTankImageTable()
		{
			TankImage.Columns.Add("id", typeof(Int32));
			TankImage.PrimaryKey = new DataColumn[] { TankImage.Columns["id"] };
			TankImage.Columns.Add("img", typeof(Image));
			TankImage.Columns.Add("smallimg", typeof(Image));
			TankImage.Columns.Add("contourimg", typeof(Image));
		}

		public static void CreateMasteryBageImageTable()
		{
			MasteryBadgeImage.Columns.Add("id", typeof(Int32));
			MasteryBadgeImage.PrimaryKey = new DataColumn[] { MasteryBadgeImage.Columns["id"] };
			MasteryBadgeImage.Columns.Add("img", typeof(Image));
			LoadMasteryBadgeImages();
		}

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
			TankImage.Clear();
			foreach (DataRow dr in dt.Rows)
			{
				if (TankData.PlayerTankExists(Convert.ToInt32(dr["id"])))
				{
					DataRow tankImgNewDataRow = TankImage.NewRow();
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
					TankImage.Rows.Add(tankImgNewDataRow);
					TankImage.AcceptChanges();
				}
			}
			
		}

		public static void LoadMasteryBadgeImages()
		{
			string adminDB = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\Database\\Admin.db";
			string adminDbCon = "Data Source=" + adminDB + ";Version=3;PRAGMA foreign_keys = ON;";
			string sql = "select * from masterybadge";
			SQLiteConnection con = new SQLiteConnection(adminDbCon);
			con.Open();
			SQLiteCommand command = new SQLiteCommand(sql, con);
			SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
			DataTable dt = new DataTable();
			adapter.Fill(dt);
			con.Close();
			MasteryBadgeImage.Clear();
			foreach (DataRow dr in dt.Rows)
			{
				DataRow masterybadgeImgNewDataRow = MasteryBadgeImage.NewRow();
				// ID
				masterybadgeImgNewDataRow["id"] = dr["id"];
				// Img
				byte[] imgByte = (byte[])dr["img"];
				MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
				ms.Write(imgByte, 0, imgByte.Length);
				Image image = new Bitmap(ms);
				masterybadgeImgNewDataRow["img"] = image;
				// Add to dt
				MasteryBadgeImage.Rows.Add(masterybadgeImgNewDataRow);
				MasteryBadgeImage.AcceptChanges();
			}

		}

		public enum TankImageType
		{
			ContourImage = 0,
			SmallImage = 1,
			LargeImage = 2
		}

		public static Image GetTankImage(int tankId, string imageCol)
		{
			// contourimg
			// smallimg
			// img
			DataRow[] dr = TankImage.Select("id = " + tankId.ToString());
			if (dr.Length > 0)
				return (Image)dr[0][imageCol];
			else
			{
				Bitmap img = new Bitmap(1, 1);
				return img;
			}
				
		}

		public static Image GetMasteryBadgeImage(int id)
		{
			DataRow[] dr = MasteryBadgeImage.Select("id = " + id.ToString());
			if (dr.Length > 0)
				return (Image)dr[0]["img"];
			else
			{
				Bitmap img = new Bitmap(1, 1);
				return img;
			}

		}

		public static Image GetTankImage(int tankId, TankImageType tankImageType)
		{
			string imageCol = "";
			switch (tankImageType)
			{
				case TankImageType.ContourImage:
					imageCol = "contourimg";
					break;
				case TankImageType.SmallImage:
					imageCol = "smallimg";
					break;
				case TankImageType.LargeImage:
					imageCol = "img";
					break;
			}
			DataRow[] dr = TankImage.Select("id = " + tankId.ToString());
			if (dr.Length > 0)
				return (Image)dr[0][imageCol];
			else
			{
				Bitmap img = new Bitmap(1, 1);
				return img;
			}

		}
	}
}
