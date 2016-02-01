using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Gadget
{
	public class GadgetHelper
	{
		public enum TimeRangeEnum
		{
			Total = 0,
			TimeMonth3 = 6,
			TimeMonth = 2,
			TimeWeek = 3,
			TimeToday = 4,
		}

        public class TimeItem
        {
            public TimeRangeEnum TimeRange { get; set; }
            public string Name { get; set; }
            public string ButtonName { get; set; }
        }

        public static List<TimeItem> GetTime()
        {
            List<TimeItem> timeRanges = new List<TimeItem>();
            timeRanges.Add(new TimeItem() { TimeRange = TimeRangeEnum.Total, Name = "Total", ButtonName = "Total" });
            timeRanges.Add(new TimeItem() { TimeRange = TimeRangeEnum.TimeMonth3, Name = "3 Months", ButtonName = "3 Mth" });
            timeRanges.Add(new TimeItem() { TimeRange = TimeRangeEnum.TimeMonth, Name = "Month", ButtonName = "Month" });
            timeRanges.Add(new TimeItem() { TimeRange = TimeRangeEnum.TimeWeek, Name = "Week", ButtonName = "Week" });
            timeRanges.Add(new TimeItem() { TimeRange = TimeRangeEnum.TimeToday, Name = "Today", ButtonName = "Today" });
            return timeRanges;
        }

        public static string GetTimeDropDownList()
        {
            string timeList = "";
            foreach (TimeItem ti in GetTime())
            {
                timeList += ti.Name + ",";
            }
            timeList = timeList.Substring(0, timeList.Length - 1);
            return timeList;
        }

        public static TimeItem GetTimeItemFromTimeRange(TimeRangeEnum timeRange)
        {
            TimeItem ti = null;
            List<TimeItem> tiList = GetTime();
            if (tiList.Where(b => b.TimeRange == timeRange).Count() > 0)
                ti = tiList.Where(b => b.TimeRange == timeRange).First();
            return ti;
        }

        public static TimeItem GetTimeItemFromName(string name)
        {
            TimeItem ti = null;
            List<TimeItem> tiList = GetTime();
            if (tiList.Where(b => b.Name == name).Count() > 0)
                ti = tiList.Where(b => b.Name == name).First();
            return ti;
        }

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

		public static object[] newParameters = new object[20];
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
					// Check if exists
					string sqlCheck = "select id from gadgetParameter where gadgetId=@gadgetId and paramNum=@paramNum;";
					DB.AddWithValue(ref sqlCheck, "@gadgetId", gadget.id, DB.SqlDataType.Int);
					DB.AddWithValue(ref sqlCheck, "@paramNum", paramNum, DB.SqlDataType.Int);
					DataTable dt = DB.FetchData(sqlCheck);
					string newParam = "";
					if (dt.Rows.Count == 0)
					{
						newParam = "insert into gadgetParameter (gadgetId, paramNum, value, dataType) values (@gadgetId, @paramNum, @value, @dataType);";
						DB.AddWithValue(ref newParam, "@gadgetId", gadget.id, DB.SqlDataType.Int);
						DB.AddWithValue(ref newParam, "@paramNum", paramNum, DB.SqlDataType.Int);
						DB.AddWithValue(ref newParam, "@value", param.ToString(), DB.SqlDataType.VarChar);
						string dataType = param.GetType().ToString();
						DB.AddWithValue(ref newParam, "@dataType", dataType, DB.SqlDataType.VarChar);
					}
					else
					{
						newParam = "update gadgetParameter set value=@value where gadgetId=@gadgetId and paramNum=@paramNum; ";
						DB.AddWithValue(ref newParam, "@value", param.ToString(), DB.SqlDataType.VarChar);
						DB.AddWithValue(ref newParam, "@gadgetId", gadget.id, DB.SqlDataType.Int);
						DB.AddWithValue(ref newParam, "@paramNum", paramNum, DB.SqlDataType.Int);
					}
					sql += newParam;
					paramNum++;
				}
			}
			DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors, true);
		}

        public static void DeleteGadgetParameter(int gadgetId)
        {
            string sql = "delete from gadgetParameter where gadgetId = " + gadgetId.ToString();
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
					object[] param = new object[20];
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
								case "System.Int64": param[paramCount] = Convert.ToInt32(drParams["value"]); break;
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
            if (gadgetName == "ucChartTier" || 
                gadgetName == "ucChartNation" || 
                gadgetName == "ucChartTankType" || 
                gadgetName == "ucHeading" ||
                gadgetName == "ucTotalStats")
				    resizable = true;
			return resizable;
		}

		public static Control GetGadgetControl(string name, object[] param)
		{
			try
			{
				Control uc = null;
                string param0 = "";
				string param1 = "";
                string param2 = "";
                if (param[0] != null) param0 = param[0].ToString();
                if (param[1] != null) param1 = param[1].ToString();
                if (param[2] != null) param2 = param[2].ToString();
				switch (name)
				{
					// Gauges - Rating
                    case "ucGaugeWN8":
                        if (param0 == "")
                            param0 = GadgetHelper.GetTimeItemFromTimeRange(GadgetHelper.TimeRangeEnum.Total).Name;
                        uc = new Gadget.ucGaugeWN8(GadgetHelper.GetTimeItemFromName(param0).TimeRange); 
                        break;
                    case "ucGaugeWN7":
                        if (param0 == "")
                            param0 = GadgetHelper.GetTimeItemFromTimeRange(GadgetHelper.TimeRangeEnum.Total).Name;
                        uc = new Gadget.ucGaugeWN7(GadgetHelper.GetTimeItemFromName(param0).TimeRange); 
                        break;
                    case "ucGaugeEFF":
                        if (param0 == "")
                            param0 = GadgetHelper.GetTimeItemFromTimeRange(GadgetHelper.TimeRangeEnum.Total).Name;
                        uc = new Gadget.ucGaugeEFF(GadgetHelper.GetTimeItemFromName(param0).TimeRange); 
                        break;

                    // Gauge - WR
                    case "ucGaugeWinRate":
                        if (param1 == "")
                            param1 = GadgetHelper.GetTimeItemFromTimeRange(GadgetHelper.TimeRangeEnum.Total).Name;
                        uc = new Gadget.ucGaugeWinRate(param0, GadgetHelper.GetTimeItemFromName(param1).TimeRange); 
                        break;

                    // Gauge - RWR
                    case "ucGaugeRWR":
                        if (param1 == "")
                            param1 = GadgetHelper.GetTimeItemFromTimeRange(GadgetHelper.TimeRangeEnum.Total).Name;
                        uc = new Gadget.ucGaugeRWR(param0, GadgetHelper.GetTimeItemFromName(param1).TimeRange);
                        break;

                    // Gauges - K/D & D/R
					case "ucGaugeKillDeath":
                        if (param1 == "")
                            param1 = GadgetHelper.GetTimeItemFromTimeRange(GadgetHelper.TimeRangeEnum.Total).Name;
                        uc = new Gadget.ucGaugeKillDeath(param0, GadgetHelper.GetTimeItemFromName(param1).TimeRange); 
                        break;
                    case "ucGaugeDmgCausedReceived":
                        if (param1 == "")
                            param1 = GadgetHelper.GetTimeItemFromTimeRange(GadgetHelper.TimeRangeEnum.Total).Name;
                        uc = new Gadget.ucGaugeDmgCausedReceived(param0, GadgetHelper.GetTimeItemFromName(param1).TimeRange); 
                        break;
                    
                    // Grids
                    case "ucTotalTanks":
                        uc = new Gadget.ucTotalTanks(param0); 
                        break;

					case "ucBattleTypes": 
                        uc = new Gadget.ucBattleTypes(); 
                        break;

					case "ucBattleListLargeImages": 
                        uc = new Gadget.ucBattleListLargeImages(Convert.ToInt32(param0), Convert.ToInt32(param1)); 
                        break;

                    case "ucTotalStats":
                        uc = new Gadget.ucTotalStats(param);
                        break;

                    
                    // Charts
                    case "ucChartBattle": // Not in use
                        uc = new Gadget.ucChartBattle(); 
                        break; 
					
                    case "ucChartTier":
                        if (param2 == "")
                            param2 = GadgetHelper.GetTimeItemFromTimeRange(GadgetHelper.TimeRangeEnum.Total).Name;
						uc = new Gadget.ucChartTier(param0, param1, GadgetHelper.GetTimeItemFromName(param2).TimeRange); 
                        break;
					
                    case "ucChartNation":
                        if (param2 == "")
                            param2 = GadgetHelper.GetTimeItemFromTimeRange(GadgetHelper.TimeRangeEnum.Total).Name;
						uc = new Gadget.ucChartNation(param0, param1, GadgetHelper.GetTimeItemFromName(param2).TimeRange); 
                        break;
					
                    case "ucChartTankType":
                        if (param2 == "")
                            param2 = GadgetHelper.GetTimeItemFromTimeRange(GadgetHelper.TimeRangeEnum.Total).Name;
						uc = new Gadget.ucChartTankType(param0, param1, GadgetHelper.GetTimeItemFromName(param2).TimeRange); 
                        break;

                    // Heading
                    case "ucHeading":
                        uc = new Gadget.ucHeading(param0);
                        break;

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
                case "ucGaugeRWR": name = "RWR Gauge"; break;
				case "ucGaugeWN8": name = "WN8 Gauge"; break;
				case "ucGaugeWN7": name = "WN7 Gauge"; break;
				case "ucGaugeEFF": name = "Efficiency Gauge"; break;
                case "ucTotalStats": name = "Total Stats Grid"; break;
                case "ucTotalTanks": name = "Tank Type Grid"; break;
				case "ucBattleTypes": name = "Battle Mode Grid"; break;
				case "ucBattleListLargeImages": name = "Last Battles"; break;
				case "ucChartBattle": name = "Battle Chart (demo)"; break;
				case "ucChartTier": name = "Battle Count per Tier"; break;
                case "ucChartNation": name = "Battle Count per Nation"; break;
                case "ucChartTankType": name = "Battle Count per Tank Type"; break;
				case "ucGaugeKillDeath": name = "Kill / Death Ratio Gauge"; break;
				case "ucGaugeDmgCausedReceived": name = "Damage Caused / Received Gauge"; break;
                case "ucHeading": name = "Heading"; break;
			}
			return name;
		}

        public static void ControlDataBind(Control c)
        {
            if (c.Tag != null)
            {
                switch (c.Tag.ToString())
                {
                    case "ucGaugeWinRate":
                        ucGaugeWinRate ucGaugeWinRate = (ucGaugeWinRate)c;
                        ucGaugeWinRate.DataBind();
                        break;
                    case "ucGaugeRWR":
                        ucGaugeRWR ucGaugeRWR = (ucGaugeRWR)c;
                        ucGaugeRWR.DataBind();
                        break;
                    case "ucGaugeWN8":
                        ucGaugeWN8 ucGaugeWN8 = (ucGaugeWN8)c;
                        ucGaugeWN8.DataBind();
                        break;
                    case "ucGaugeWN7":
                        ucGaugeWN7 ucGaugeWN7 = (ucGaugeWN7)c;
                        ucGaugeWN7.DataBind();
                        break;
                    case "ucGaugeEFF":
                        ucGaugeEFF ucGaugeEFF = (ucGaugeEFF)c;
                        ucGaugeEFF.DataBind();
                        break;
                    case "ucTotalStats":
                        ucTotalStats ucTotalStats = (ucTotalStats)c;
                        ucTotalStats.DataBind();
                        break;
                    case "ucTotalTanks":
                        ucTotalTanks ucTotalTanks = (ucTotalTanks)c;
                        ucTotalTanks.DataBind();
                        break;
                    case "ucBattleTypes":
                        ucBattleTypes ucBattleTypes = (ucBattleTypes)c;
                        ucBattleTypes.DataBind();
                        break;
                    case "ucBattleListLargeImages":
                        ucBattleListLargeImages ucBattleListLargeImages = (ucBattleListLargeImages)c;
                        ucBattleListLargeImages.DataBind();
                        break;
                    case "ucChartBattle":
                        ucChartBattle ucChartBattle = (ucChartBattle)c;
                        ucChartBattle.DataBind();
                        break;
                    case "ucChartTier":
                        ucChartTier ucChartTier = (ucChartTier)c;
                        ucChartTier.DataBind();
                        break;
                    case "ucChartNation":
                        ucChartNation ucChartNation = (ucChartNation)c;
                        ucChartNation.DataBind();
                        break;
                    case "ucChartTankType":
                        ucChartTankType ucChartTankType = (ucChartTankType)c;
                        ucChartTankType.DataBind();
                        break;
                    case "ucGaugeKillDeath":
                        ucGaugeKillDeath ucGaugeKillDeath = (ucGaugeKillDeath)c;
                        ucGaugeKillDeath.DataBind();
                        break;
                    case "ucGaugeDmgCausedReceived":
                        ucGaugeDmgCausedReceived ucGaugeDmgCausedReceived = (ucGaugeDmgCausedReceived)c;
                        ucGaugeDmgCausedReceived.DataBind();
                        break;
                    case "ucHeading":
                        ucHeading ucHeading = (ucHeading)c;
                        ucHeading.DataBind();
                        break;
                }
            }
            else
            {
                string s = c.Name;
            }
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
			// Remove current gadgets
            string sql = "delete from gadgetParameter ; delete from gadget ;";
			DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors);
			gadgets.Clear();
            
            // Add default gadgets
			string s = "INSERT INTO gadget (controlName, visible, sortorder, posX, posY, width, height) ";
			sql = "";
            
            // Gadgets to insert
            sql += s + "VALUES ('ucGaugeWinRate', 1, 1, 152, 32, 200, 170) ; ";
			sql += s + "VALUES ('ucGaugeWN8',     1, 2, 402, 32, 200, 170) ; ";
			sql += s + "VALUES ('ucGaugeEFF',     1, 3, 652, 32, 200, 170) ; ";

			sql += s + "VALUES ('ucGaugeKillDeath',         1, 4, 272, 232, 200, 150) ; ";
			sql += s + "VALUES ('ucGaugeDmgCausedReceived', 1, 5, 532, 232, 200, 150) ; ";
			
			sql += s + "VALUES ('ucChartTier',     1, 6, 32,  412, 370, 170) ; ";
			sql += s + "VALUES ('ucChartTankType', 1, 7, 462, 412, 200, 170) ; ";
			sql += s + "VALUES ('ucChartNation',   1, 8, 712, 412, 250, 170) ; ";
			
            // Insert now
			DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors);
			s = "INSERT INTO gadgetParameter (gadgetId, paramNum, dataType, value) ";

            // Add default parameters to gadgets
			sql = "";
			int gadgetId = 0;
            
            gadgetId = Convert.ToInt32(DB.FetchData("select id from gadget where controlName='ucGaugeWinRate'").Rows[0][0]);
			sql += s + "VALUES (" + gadgetId.ToString() + ", 0, 'System.String', '15'); ";
            sql += s + "VALUES (" + gadgetId.ToString() + ", 1, 'System.String', 'Total'); ";

            gadgetId = Convert.ToInt32(DB.FetchData("select id from gadget where controlName='ucGaugeWN8'").Rows[0][0]);
            sql += s + "VALUES (" + gadgetId.ToString() + ", 0, 'System.String', 'Total'); ";

            gadgetId = Convert.ToInt32(DB.FetchData("select id from gadget where controlName='ucGaugeEFF'").Rows[0][0]);
            sql += s + "VALUES (" + gadgetId.ToString() + ", 0, 'System.String', 'Total'); ";

            gadgetId = Convert.ToInt32(DB.FetchData("select id from gadget where controlName='ucGaugeKillDeath'").Rows[0][0]);
            sql += s + "VALUES (" + gadgetId.ToString() + ", 0, 'System.String', '15'); ";
            sql += s + "VALUES (" + gadgetId.ToString() + ", 1, 'System.String', 'Total'); ";
            
            gadgetId = Convert.ToInt32(DB.FetchData("select id from gadget where controlName='ucGaugeDmgCausedReceived'").Rows[0][0]);
			sql += s + "VALUES (" + gadgetId.ToString() + ", 0, 'System.String', '15'); ";
            sql += s + "VALUES (" + gadgetId.ToString() + ", 1, 'System.String', 'Total'); ";

			gadgetId = Convert.ToInt32(DB.FetchData("select id from gadget where controlName='ucChartTier'").Rows[0][0]);
			sql += s + "VALUES (" + gadgetId.ToString() + ", 0, 'System.String', '15'); ";
			sql += s + "VALUES (" + gadgetId.ToString() + ", 1, 'System.String', '#A31F1F'); ";
            sql += s + "VALUES (" + gadgetId.ToString() + ", 2, 'System.String', 'Total'); ";

            gadgetId = Convert.ToInt32(DB.FetchData("select id from gadget where controlName='ucChartTankType'").Rows[0][0]);
            sql += s + "VALUES (" + gadgetId.ToString() + ", 0, 'System.String', '15'); ";
            sql += s + "VALUES (" + gadgetId.ToString() + ", 1, 'System.String', '#1B8E30'); ";
            sql += s + "VALUES (" + gadgetId.ToString() + ", 2, 'System.String', 'Total'); ";
            
            gadgetId = Convert.ToInt32(DB.FetchData("select id from gadget where controlName='ucChartNation'").Rows[0][0]);
			sql += s + "VALUES (" + gadgetId.ToString() + ", 0, 'System.String', '15'); ";
			sql += s + "VALUES (" + gadgetId.ToString() + ", 1, 'System.String', '#1F47A5'); ";
            sql += s + "VALUES (" + gadgetId.ToString() + ", 2, 'System.String', 'Total'); ";
			
            // Insert now
            DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors);
		}

		public static void DrawBorderOnGadget(object sender, PaintEventArgs e)
		{
			UserControl uc = (UserControl)sender;
			e.Graphics.DrawRectangle(new System.Drawing.Pen(ColorTheme.FormBorderBlue), 0, 0, uc.Width-1, uc.Height-1);
		}

        public static void HomeViewLoadFromToFile(string fileName)
        {
            string fileString = File.ReadAllText(fileName);
            int splitPos = fileString.IndexOf("]" + Environment.NewLine + "[");
            if (splitPos > 20)
            {
                splitPos +=1;
                DataTable dtGadget = JsonConvert.DeserializeObject<DataTable>(fileString.Substring(0,splitPos));
                DataTable dtGadgetParameter = JsonConvert.DeserializeObject<DataTable>(fileString.Substring(splitPos + 1));
                // When inserting gadgets, new id's will be assigned - create conversion column for old id
                dtGadget.Columns.Add("newId", typeof(Int32));
                foreach (DataRow dr in dtGadget.Rows)
                {
                    dr["newId"] = dr["id"];
                }
                // Remove current setup
                GadgetHelper.RemoveGadgetAll();
                // Store to the database from here
                // Add gadgets
                string sqlInsert = "INSERT INTO gadget (controlName, visible, sortorder, posX, posY, width, height) VALUES (@controlName, 1, @sortorder, @posX, @posY, @width, @height); ";
                foreach (DataRow dr in dtGadget.Rows)
                {
                    string newsql = sqlInsert;
                    DB.AddWithValue(ref newsql, "@controlName", dr["controlName"].ToString(), DB.SqlDataType.VarChar);
                    DB.AddWithValue(ref newsql, "@sortorder", dr["sortorder"].ToString(), DB.SqlDataType.VarChar);
                    DB.AddWithValue(ref newsql, "@posX", dr["posX"].ToString(), DB.SqlDataType.Int);
                    DB.AddWithValue(ref newsql, "@posY", dr["posY"].ToString(), DB.SqlDataType.Int);
                    DB.AddWithValue(ref newsql, "@width", dr["width"].ToString(), DB.SqlDataType.Int);
                    DB.AddWithValue(ref newsql, "@height", dr["height"].ToString(), DB.SqlDataType.Int);
                    DB.ExecuteNonQuery(newsql, Config.Settings.showDBErrors, true);
                    // get new id from db and add to memory table
                    string newId = DB.FetchData("select max(id) as newId from gadget").Rows[0]["newId"].ToString();
                    dr["newId"] = newId;
                }
                
                // Add gadget PARAMETERS
                string sqlBatch = "";
                sqlInsert = "INSERT INTO gadgetParameter (gadgetId, paramNum, dataType, value) VALUES (@gadgetId, @paramNum, @dataType, @value); ";
                foreach (DataRow dr in dtGadgetParameter.Rows)
                {
                    string sqlNew = sqlInsert;
                    string gadgetId = dtGadget.Select("Id = " + dr["gadgetId"])[0]["newId"].ToString();
                    DB.AddWithValue(ref sqlNew, "@gadgetId", gadgetId, DB.SqlDataType.Int);
                    DB.AddWithValue(ref sqlNew, "@paramNum", dr["paramNum"].ToString(), DB.SqlDataType.Int);
                    DB.AddWithValue(ref sqlNew, "@dataType", dr["dataType"].ToString(), DB.SqlDataType.VarChar);
                    DB.AddWithValue(ref sqlNew, "@value", dr["value"].ToString(), DB.SqlDataType.VarChar);
                    sqlBatch += sqlNew + Environment.NewLine;
                }
                DB.ExecuteNonQuery(sqlBatch, false, true);
            }
            
        }

        public static void HomeViewSaveToFile(string fileName)
        {
            // Get gadgets
            DataTable dtGadget = DB.FetchData("select * from gadget order by id;");
            string jsonResult = JsonConvert.SerializeObject(dtGadget, Newtonsoft.Json.Formatting.Indented);
            jsonResult += Environment.NewLine;
            // Get gadgets parametes
            DataTable dtGadgetParameter = DB.FetchData("select * from gadgetParameter order by gadgetid,id;");
            jsonResult += JsonConvert.SerializeObject(dtGadgetParameter, Newtonsoft.Json.Formatting.Indented);
            // Save
            File.WriteAllText(fileName, jsonResult);
        }

	}



}
