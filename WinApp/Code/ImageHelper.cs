using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Code
{
	class ImageHelper
	{
        public static Image CheckedMenuIcon;

        public static DataTable TankImage = new DataTable();
		public static DataTable MasteryBadgeImage = new DataTable();
		public static DataTable TankTypeImage = new DataTable();
		public static DataTable NationImage = new DataTable();

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
			//TankImage.Columns.Add("img", typeof(Image));
			TankImage.Columns.Add("smallimg", typeof(Image));
			TankImage.Columns.Add("contourimg", typeof(Image));
		}

		public static void CreateMasteryBageImageTable()
		{
			MasteryBadgeImage = new DataTable();
			MasteryBadgeImage.Columns.Add("id", typeof(Int32));
			MasteryBadgeImage.PrimaryKey = new DataColumn[] { MasteryBadgeImage.Columns["id"] };
			MasteryBadgeImage.Columns.Add("img", typeof(Image));
			MasteryBadgeImage.Columns.Add("imgLarge", typeof(Image));
			LoadMasteryBadgeImages();
		}

		public static void CreateTankTypeImageTable()
		{
			TankTypeImage = new DataTable();
			TankTypeImage.Columns.Add("id", typeof(Int32));
			TankTypeImage.PrimaryKey = new DataColumn[] { TankTypeImage.Columns["id"] };
			TankTypeImage.Columns.Add("img", typeof(Image));
			LoadTankTypeImages();
		}

		public static void CreateNationImageTable()
		{
			NationImage = new DataTable();
			NationImage.Columns.Add("id", typeof(Int32));
			NationImage.PrimaryKey = new DataColumn[] { NationImage.Columns["id"] };
			NationImage.Columns.Add("img", typeof(Image));
			LoadNationImages();
		}

		public static string tankIdDebug = "0";

		public async static Task LoadTankImages()
		{
			try
			{
				if (await DB.CheckConnection(false))
				{
					string adminDB = Config.AppDataBaseFolder + "Admin.db";
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
						DataRow tankImgNewDataRow = TankImage.NewRow();
						// ID
						tankImgNewDataRow["id"] = dr["id"];
						tankIdDebug = dr["id"].ToString();
						// SmallImg
						bool imgOK = true;
						if (dr["smallImg"] == DBNull.Value)
						{
							// Missing image
							// await Log.LogToFile("Missing image for tank: " + tankIdDebug);
							imgOK = false;
						}
						if (imgOK)
						{
                            // small image (normal)
                            byte[] imgByte = (byte[])dr["smallImg"];
							MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
							ms.Write(imgByte, 0, imgByte.Length);
							Image image = new Bitmap(ms);
							tankImgNewDataRow["smallImg"] = image;
                            
                            // countor image (icon)
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
			}
			catch (Exception ex)
			{
				if (Config.Settings.showDBErrors) 
					await Log.LogToFile(ex, "Error loading tank images for tank: " + tankIdDebug);
			}
		}

		private static Image GetLargeTankImage(int tankId)
		{
            string adminDB = Config.AppDataBaseFolder + "Admin.db";
			string adminDbCon = "Data Source=" + adminDB + ";Version=3;PRAGMA foreign_keys = ON;";
			string sql = "select * from tank where id=@id";
			DB.AddWithValue(ref sql, "@id", tankId, DB.SqlDataType.Int);
			SQLiteConnection con = new SQLiteConnection(adminDbCon);
			con.Open();
			SQLiteCommand command = new SQLiteCommand(sql, con);
			SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
			DataTable dt = new DataTable();
			adapter.Fill(dt);
			con.Close();
			Image image = null;
			if (dt.Rows.Count > 0)
			{
				// SmallImg
				byte[] imgByte = (byte[])dt.Rows[0]["img"];
				MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
				ms.Write(imgByte, 0, imgByte.Length);
				image = new Bitmap(ms);
			}
			else
			{
				Bitmap img = new Bitmap(1, 1);
				image = (Image)img;
			}
			return image;
		}

        public async static Task<Image> GetMap(int mapId, bool getIllustation = false, int reSize = 0)
		{
			string sql = "select arena_id from map where id=@mapId";
			DB.AddWithValue(ref sql, "@mapId", mapId, DB.SqlDataType.Int);
			DataTable dtArenaId = await DB.FetchData(sql);
			string arena_id = "";
			if (dtArenaId.Rows.Count > 0)
			{
				arena_id = dtArenaId.Rows[0][0].ToString();
			}
			return GetMap(arena_id, getIllustation, reSize);
		}

		public static Image GetMap(string arena_id, bool getIllustation = false, int heightSize = 0)
		{
			Bitmap img = new Bitmap(1, 1);
			Image image = (Image)img;
			if (arena_id != "")
			{
                string adminDB = Config.AppDataBaseFolder + "Admin.db";
				string adminDbCon = "Data Source=" + adminDB + ";Version=3;PRAGMA foreign_keys = ON;";
				string sql = "select * from map where name=@arena_id";
				DB.AddWithValue(ref sql, "@arena_id", arena_id, DB.SqlDataType.VarChar);
				SQLiteConnection con = new SQLiteConnection(adminDbCon);
				con.Open();
				SQLiteCommand command = new SQLiteCommand(sql, con);
				SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
				DataTable dt = new DataTable();
				adapter.Fill(dt);
				con.Close();
				string field = "minimap";
				if (getIllustation) field = "illustration";
				if (dt.Rows.Count > 0)
				{
					// SmallImg
					byte[] imgByte = (byte[])dt.Rows[0][field];
					MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
					ms.Write(imgByte, 0, imgByte.Length);
					image = new Bitmap(ms);
				}
                if (heightSize > 0)
                {
                    Image newImage = new Bitmap(image.Width * heightSize / image.Height, heightSize);
                    using (Graphics graphicsHandle = Graphics.FromImage(newImage))
                    {
                        graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphicsHandle.DrawImage(image, 0, 0, image.Width * heightSize / image.Height, heightSize);
                    }
                    image = newImage;
                }
			}
			return image;
		}


		public static void LoadMasteryBadgeImages()
		{
            string adminDB = Config.AppDataBaseFolder + "Admin.db";
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
				string imgField = "img";
				if (Config.Settings.useSmallMasteryBadgeIcons)
					imgField = "imgSmall";
				byte[] imgByte = (byte[])dr[imgField];
				MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
				ms.Write(imgByte, 0, imgByte.Length);
				Image image = new Bitmap(ms);
				masterybadgeImgNewDataRow["img"] = image;
				// Large Image
				byte[] imgLargeByte = (byte[])dr["imgLarge"];
				ms = new MemoryStream(imgLargeByte, 0, imgLargeByte.Length);
				ms.Write(imgLargeByte, 0, imgLargeByte.Length);
				image = new Bitmap(ms);
				masterybadgeImgNewDataRow["imgLarge"] = image;
				// Add to dt
				MasteryBadgeImage.Rows.Add(masterybadgeImgNewDataRow);
				MasteryBadgeImage.AcceptChanges();
			}

		}

		public static void LoadTankTypeImages()
		{
			string adminDB = Config.AppDataBaseFolder + "Admin.db";
			string adminDbCon = "Data Source=" + adminDB + ";Version=3;PRAGMA foreign_keys = ON;";
			string sql = "select * from tanktype";
			SQLiteConnection con = new SQLiteConnection(adminDbCon);
			con.Open();
			SQLiteCommand command = new SQLiteCommand(sql, con);
			SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
			DataTable dt = new DataTable();
			adapter.Fill(dt);
			con.Close();
			TankTypeImage.Clear();
			foreach (DataRow dr in dt.Rows)
			{
				DataRow newDataRow = TankTypeImage.NewRow();
				// ID
				newDataRow["id"] = dr["id"];
				// Img
				string imgField = "img";
				byte[] imgByte = (byte[])dr[imgField];
				MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
				ms.Write(imgByte, 0, imgByte.Length);
				Image image = new Bitmap(ms);
				newDataRow["img"] = image;
				// Add to dt
				TankTypeImage.Rows.Add(newDataRow);
				TankTypeImage.AcceptChanges();
			}

		}

		public static void LoadNationImages()
		{
            string adminDB = Config.AppDataBaseFolder + "Admin.db";
			string adminDbCon = "Data Source=" + adminDB + ";Version=3;PRAGMA foreign_keys = ON;";
			string sql = "select * from nation";
			SQLiteConnection con = new SQLiteConnection(adminDbCon);
			con.Open();
			SQLiteCommand command = new SQLiteCommand(sql, con);
			SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
			DataTable dt = new DataTable();
			adapter.Fill(dt);
			con.Close();
			NationImage.Clear();
			foreach (DataRow dr in dt.Rows)
			{
				DataRow newDataRow = NationImage.NewRow();
				// ID
				newDataRow["id"] = dr["id"];
				// Img
				string imgField = "img";
				byte[] imgByte = (byte[])dr[imgField];
				MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
				ms.Write(imgByte, 0, imgByte.Length);
				Image image = new Bitmap(ms);
				newDataRow["img"] = image;
				// Add to dt
				NationImage.Rows.Add(newDataRow);
				NationImage.AcceptChanges();
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
            // Available types: contourimg, smallimg, img (icon, small, large)
            try
            {
                if (imageCol == "img")
                {
                    return GetLargeTankImage(tankId);
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
            catch (Exception)
            {
                Bitmap img = new Bitmap(1, 1);
                return img;
            }
		}

		public static Image GetTankImage(int tankId, TankImageType imageType)
		{
			switch (imageType)
			{
				case TankImageType.ContourImage:
					return GetTankImage(tankId, "contourimg");
				case TankImageType.SmallImage:
					return GetTankImage(tankId, "smallimg");
				case TankImageType.LargeImage:
					return GetTankImage(tankId, "img");
			}
			Bitmap img = new Bitmap(1, 1);
			return img;
		}


		public static Image GetMasteryBadgeImage(int id, bool icon = true)
		{
			DataRow[] dr = MasteryBadgeImage.Select("id = " + id.ToString());
			if (dr.Length > 0)
				if (icon)
					return (Image)dr[0]["img"];
				else
					return (Image)dr[0]["imgLarge"];
			else
			{
				Bitmap img = new Bitmap(1, 1);
				return img;
			}
		}

		public static Image GetTankTypeImage(int id)
		{
			DataRow[] dr = TankTypeImage.Select("id = " + id.ToString());
			if (dr.Length > 0)
				return (Image)dr[0]["img"];
			else
			{
				Bitmap img = new Bitmap(1, 1);
				return img;
			}
		}

		public static Image GetNationImage(int id)
		{
			DataRow[] dr = NationImage.Select("id = " + id.ToString());
			if (dr.Length > 0)
				return (Image)dr[0]["img"];
			else
			{
				Bitmap img = new Bitmap(1, 1);
				return img;
			}
		}

		public static byte[] ImageToByteArray(System.Drawing.Image imageIn, ImageFormat imageFormat)
		{
			MemoryStream ms = new MemoryStream();
			imageIn.Save(ms, imageFormat);
			return ms.ToArray();
		}

		public static Image ByteArrayToImage(byte[] byteArrayIn)
		{
			MemoryStream ms = new MemoryStream(byteArrayIn);
			Image returnImage = Image.FromStream(ms);
			return returnImage;
		}
	}
}
