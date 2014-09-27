using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Gadget
{
	class GadgetHelper
	{
		public enum TimeRange
		{
			Total = 0,
			Num1000 = 1,
			TimeMonth = 2,
			TimeWeek = 3,
			TimeToday = 4,
			Num5000 = 5,
		}

		public static TimeRange SelectedTimeRangeEFF = TimeRange.Total;
		public static TimeRange SelectedTimeRangeWN7 = TimeRange.Total;
		public static TimeRange SelectedTimeRangeWN8 = TimeRange.Total;
		public static TimeRange SelectedTimeRangeWR = TimeRange.Total;

		public class GadgetItem
		{
			public Control control;
			public int id;
			public string controlName;
			public string name;
			public bool visible;
			public int sortorder;
			public int posX;
			public int posY;
			public int width;
			public int height;
			public bool resizable = false;
		}

		public static List<GadgetItem> gadgets = null;

		public static object[] newParameters = { null, null, null, null, null };
		public static bool newParametersOK = true;
		
		public static void SaveGadgetPosition(int gadgetId, int left, int top)
		{
			string sql = "update gadget set posX=@posX, posY=@posY where id=@id;";
			DB.AddWithValue(ref sql, "@posX", left, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@posY", top, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@id", gadgetId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors);
		}

		public static void SaveGadgetSize(GadgetItem gadget)
		{
			string sql = "update gadget set width=@width, height=@height where id=@id;";
			DB.AddWithValue(ref sql, "@width", gadget.width, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@height", gadget.height, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@id", gadget.id, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors);
		}


		public static void SaveGadgetParameter(GadgetItem gadget)
		{
			int paramNum = 0;
			string sql = "";
			foreach (object param in newParameters)
			{
				if (param != null)
				{
					string newParam = "update gadgetParameter set value=@value where gadgetId=@gadgetId and paramNum=@paramNum; ";
					DB.AddWithValue(ref newParam, "@value", param.ToString(), DB.SqlDataType.VarChar);
					DB.AddWithValue(ref newParam, "@gadgetId", gadget.id, DB.SqlDataType.Int);
					DB.AddWithValue(ref newParam, "@paramNum", paramNum, DB.SqlDataType.Int);
					sql += newParam;
					paramNum++;
				}
			}
			DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors, true);
		}


		public static void RemoveGadget(GadgetItem gadget)
		{
			string sql = "delete from gadgetParameter where gadgetId=@id; delete from gadget where id=@id;";
			DB.AddWithValue(ref sql, "@id", gadget.id, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors);
			gadgets.Remove(gadget);
		}

		public static void RemoveGadgetAll()
		{
			string sql = "delete from gadgetParameter ; delete from gadget ;";
			DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors);
			gadgets.Clear();
		}

		public static int InsertNewGadget(GadgetItem gadget)
		{
			int gadgetId = 0;
			string sql = 
				"insert into gadget (controlName, visible, sortorder, posX, posY, width, height) " +
				"values (@controlName, @visible, @sortorder, @posX, @posY, @width, @height);";
			DB.AddWithValue(ref sql, "@controlName", gadget.controlName, DB.SqlDataType.VarChar);
			DB.AddWithValue(ref sql, "@visible", gadget.visible, DB.SqlDataType.Boolean);
			DB.AddWithValue(ref sql, "@sortorder", gadget.sortorder, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@posX", gadget.posX, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@posY", gadget.posY, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@width", gadget.width, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@height", gadget.height, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors);
			sql = "select max(id) from gadget";
			gadgetId = Convert.ToInt32(DB.FetchData(sql).Rows[0][0]);
			int paramNum = 0;
			sql = "";
			foreach (object param in newParameters)
			{
				if (param != null)
				{
					string newParam =
						"insert into gadgetParameter (gadgetId, paramNum, dataType, value) " +
						"values (@gadgetId, @paramNum, @dataType, @value); ";
					DB.AddWithValue(ref newParam, "@gadgetId", gadgetId, DB.SqlDataType.Int);
					DB.AddWithValue(ref newParam, "@paramNum", paramNum, DB.SqlDataType.Int);
					string dataType = param.GetType().ToString();
					DB.AddWithValue(ref newParam, "@dataType", dataType, DB.SqlDataType.VarChar);
					DB.AddWithValue(ref newParam, "@value", param.ToString(), DB.SqlDataType.VarChar);
					sql += newParam;
					paramNum++;
				}
			}
			if (sql != "")
				DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors, true);
			gadgets.Insert(0,gadget);
			return gadgetId;
		}


		public static void SortGadgets()
		{
			List<GadgetItem> sortGadgets = new List<GadgetItem>();
			string sql = "select * from gadget order by sortorder;";
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 1) // Only sort if more than two items
			{
				foreach (DataRow dr in dt.Rows)
				{
					sortGadgets.Add(new GadgetItem { id = Convert.ToInt32(dr["id"]), posX = Convert.ToInt32(dr["posX"]), posY = Convert.ToInt32(dr["posY"])});
				}
				// Sorting
				List<GadgetItem> sortedGadgets = sortGadgets.OrderBy(o => o.posY).ThenBy(o => o.posX).ToList();
				// Save 
				sql = "";
				int sortOrder = 1;
				foreach (GadgetItem gadget in sortedGadgets)
				{
					sql += "update gadget set sortorder=" + sortOrder + " where id = " + gadget.id + "; ";
					sortOrder++;
				}
				DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors, true);
			}
		}

		public static bool HasGadetParameter(GadgetItem gadget)
		{
			string sql = "select count(id) from gadgetParameter where gadgetId=@gadgetId;";
			DB.AddWithValue(ref sql, "@gadgetId", gadget.id, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql, Config.Settings.showDBErrors);
			bool hasParam = false;
			if (dt.Rows.Count > 0)
			{
				DataRow dr = dt.Rows[0];
				if (dr[0] != DBNull.Value)
					hasParam = (Convert.ToInt32(dr[0]) > 0);
			}
			return hasParam;
		}

		public static void GetGadgets()
		{
			try
			{
				gadgets = new List<GadgetItem>();
				string sql =
					"select gadget.id, gadget.visible, gadget.width, gadget.height, gadget.posX, gadget.posY, " +
					"  gadget.controlName, count(gadgetParameter.id) as parameterCount " +
					"from gadget left outer join " +
					"  gadgetParameter on gadget.id = gadgetParameter.gadgetId " +
					"where visible=1 " +
					"group by gadget.id, visible, width, height, posX, posY, controlName, sortorder  " +
					"order by sortorder;";
				DataTable dt = DB.FetchData(sql);
				foreach (DataRow dr in dt.Rows)
				{
					// get parameters
					object[] param = { null, null, null, null, null };
					int gadgetId = Convert.ToInt32(dr["id"]);
					string controlName = dr["controlName"].ToString();
					if (dr["parameterCount"] != null && Convert.ToInt32(dr["parameterCount"]) > 0)
					{
						sql = "select * from gadgetParameter where gadgetId=@gadgetId order by paramNum; ";
						DB.AddWithValue(ref sql, "@gadgetId", gadgetId, DB.SqlDataType.Int);
						DataTable dtParams = DB.FetchData(sql);
						int paramCount = 0;
						foreach (DataRow drParams in dtParams.Rows)
						{
							switch (drParams["dataType"].ToString())
							{
								case "System.String": param[paramCount] = drParams["value"].ToString(); break;
								case "System.Int32": param[paramCount] = Convert.ToInt32(drParams["value"]); break;
								case "System.Double": param[paramCount] = Convert.ToDouble(drParams["value"]); break;
								case "System.Boolean": param[paramCount] = Convert.ToBoolean(drParams["value"]); break;
							}
							paramCount++;
						}
					}
					Control uc = GetGadgetControl(dr["controlName"].ToString(), param);
					uc.Name = "uc" + dr["id"].ToString();
					uc.Tag = dr["controlName"].ToString();
					uc.Top = Convert.ToInt32(dr["posY"]);
					uc.Left = Convert.ToInt32(dr["posX"]);
					uc.Height = Convert.ToInt32(dr["height"]);
					uc.Width = Convert.ToInt32(dr["width"]);
					gadgets.Add(new GadgetItem
					{
						posX = uc.Left,
						posY = uc.Top,
						width = uc.Width,
						height = uc.Height,
						control = uc,
						id = gadgetId,
						controlName = controlName,
						name = GetGadgetName(controlName),
						resizable = IsGadgetRezisable(controlName)
					});
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
			}
		}

		public static bool IsGadgetRezisable(string gadgetName)
		{
			bool resizable = false;
			if (gadgetName == "ucChartTier" || gadgetName == "ucChartNation" || gadgetName == "ucChartTankType")
				resizable = true;
			return resizable;
		}

		public static Control GetGadgetControl(string name, object[] param)
		{
			try
			{
				Control uc = null;
				switch (name)
				{
					case "ucGaugeWinRate": uc = new Gadget.ucGaugeWinRate(param[0].ToString()); break;
					case "ucGaugeWN8": uc = new Gadget.ucGaugeWN8(); break;
					case "ucGaugeWN7": uc = new Gadget.ucGaugeWN7(); break;
					case "ucGaugeEFF": uc = new Gadget.ucGaugeEFF(); break;
					case "ucTotalTanks": uc = new Gadget.ucTotalTanks(); break;
					case "ucBattleTypes": uc = new Gadget.ucBattleTypes(); break;
					case "ucBattleListLargeImages": uc = new Gadget.ucBattleListLargeImages(Convert.ToInt32(param[0]), Convert.ToInt32(param[1])); break;
					case "ucChartBattle": uc = new Gadget.ucChartBattle(); break;
					case "ucChartTier": uc = new Gadget.ucChartTier(param[0].ToString()); break;
					case "ucChartNation": uc = new Gadget.ucChartNation(param[0].ToString()); break;
					case "ucChartTankType": uc = new Gadget.ucChartTankType(param[0].ToString()); break;
				}
				return uc;
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				return null;
			}
		}

		public static string GetGadgetName(string controlName)
		{
			string name = "";
			switch (controlName)
			{
				case "ucGaugeWinRate": name = "Win Rate Gauge"; break;
				case "ucGaugeWN8": name = "WN8 Gauge"; break;
				case "ucGaugeWN7": name = "WN7 Gauge"; break;
				case "ucGaugeEFF": name = "Efficiency Gauge"; break;
				case "ucTotalTanks": name = "Total Type Stats Grid"; break;
				case "ucBattleTypes": name = "Battle Mode Stats Grid"; break;
				case "ucBattleListLargeImages": name = "Last Battles Large Images"; break;
				case "ucChartBattle": name = "Chart per Battle"; break;
				case "ucChartTier": name = "Chart per Battle"; break;
				case "ucChartNation": name = "Chart per Nation"; break;
				case "ucChartTankType": name = "Chart per Tank Type"; break;
			}
			return name;
		}

		public static GadgetItem FindGadgetFromLocation(int mouseLeft, int mouseTop)
		{
			GadgetItem foundGadgetArea = null;
			bool found = false;
			int i = 0;
			while (!found && i < gadgets.Count)
			{
				if (mouseLeft >= gadgets[i].posX &&
					mouseLeft <= gadgets[i].posX + gadgets[i].width &&
					mouseTop >= gadgets[i].posY &&
					mouseTop <= gadgets[i].posY + gadgets[i].height)
				{
					found = true;
					foundGadgetArea = gadgets[i];
				}
				else
				{
					i++;
				}
			}
			return foundGadgetArea;
		}

		public static void DefaultSetup()
		{
			string sql = "delete from gadgetParameter ; delete from gadget ;";
			DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors);
			gadgets.Clear();
			string s = "INSERT INTO gadget (controlName, visible, sortorder, posX, posY, width, height) ";
			sql = "";
			sql += s + "VALUES ('ucGaugeWinRate', 1, 1, 32, 22, 200, 170) ; ";
			sql += s + "VALUES ('ucGaugeWN8', 1, 2, 252, 22, 200, 170) ; ";
			sql += s + "VALUES ('ucGaugeWN7', 1, 3, 472, 22, 200, 170) ; ";
			sql += s + "VALUES ('ucGaugeEFF', 1, 4, 692, 22, 200, 170) ; ";
			sql += s + "VALUES ('ucBattleListLargeImages', 1, 7, 62, 362, 840, 230) ; ";
			sql += s + "VALUES ('ucTotalTanks', 1, 5, 32, 222, 420, 95) ; ";
			sql += s + "VALUES ('ucBattleTypes', 1, 6, 472, 222, 420, 95) ; ";
			DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors);
			s = "INSERT INTO gadgetParameter (gadgetId, paramNum, dataType, value) ";
			sql = "";
			int gadgetId = 0;
			gadgetId = Convert.ToInt32(DB.FetchData("select id from gadget where controlName='ucGaugeWinRate'").Rows[0][0]);
			sql += s + "VALUES (" + gadgetId.ToString() + ", 0, 'System.String', '15'); ";
			gadgetId = Convert.ToInt32(DB.FetchData("select id from gadget where controlName='ucBattleListLargeImages'").Rows[0][0]);
			sql += s + "VALUES (" + gadgetId.ToString() + ", 0, 'System.Int32', '5'); ";
			sql += s + "VALUES (" + gadgetId.ToString() + ", 1, 'System.Int32', '2'); ";
			DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors);
		}

		public static void DrawBorderOnGadget(object sender, PaintEventArgs e)
		{
			UserControl uc = (UserControl)sender;
			e.Graphics.DrawRectangle(new System.Drawing.Pen(ColorTheme.FormBorderBlue), 0, 0, uc.Width-1, uc.Height-1);
		}
	}



}
