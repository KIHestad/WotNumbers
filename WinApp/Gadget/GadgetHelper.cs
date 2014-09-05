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
			public string name;
			public int id;
			public int left;
			public int top;
			public int width;
			public int height;
		}

		public static List<GadgetItem> Gadgets = null;

		public static void SaveGadgetPosition(int gadgetId, int left, int top)
		{
			string sql = "update homeViewGadget set posX=@posX, posY=@posY where id=@id;";
			DB.AddWithValue(ref sql, "@posX", left, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@posY", top, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@id", gadgetId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);
		}

		public static void GetGadgets()
		{
			Gadgets = new List<GadgetItem>();
			string sql = 
				"select homeViewGadget.id, homeViewGadget.visible, homeViewGadget.width,homeViewGadget.height,homeViewGadget.posX,homeViewGadget.posY, " +
				"  gadget.userControlName, gadget.name, count(homeViewGadgetParameter.id) as parameterCount " +
				"from homeViewGadget inner join " +
				"  gadget on homeViewGadget.gadgetId = gadget.id left outer join " +
				"  homeViewGadgetParameter on homeViewGadget.id = homeViewGadgetParameter.homeViewGadgetId " +
				"where visible=1 " +
				"group by homeViewGadget.id, visible, homeViewGadget.width, homeViewGadget.height, posX, posY, userControlName, name, sortorder  " +
				"order by sortorder;";
			DataTable dt = DB.FetchData(sql);
			foreach (DataRow dr in dt.Rows)
			{
				// get parameters
				object[] param = {null, null, null, null, null};
				int gadgetId = Convert.ToInt32(dr["id"]);
				string gadgetName = dr["userControlName"].ToString();
				if (dr["parameterCount"] != null && Convert.ToInt32(dr["parameterCount"]) > 0)
				{
					sql = "select * from homeViewGadgetParameter where homeViewGadgetId=@homeViewGadgetId order by paramNum; ";
					DB.AddWithValue(ref sql, "@homeViewGadgetId", gadgetId, DB.SqlDataType.Int );
					DataTable dtParams = DB.FetchData(sql);
					int paramCount = 0;
					foreach (DataRow drParams in dtParams.Rows)
					{
						switch (drParams["dataType"].ToString())
						{
							case "string": param[paramCount] = drParams["value"].ToString(); break;
							case "int": param[paramCount] = Convert.ToInt32(drParams["value"]); break;
							case "double": param[paramCount] = Convert.ToDouble(drParams["value"]); break;
							case "bool": param[paramCount] = Convert.ToBoolean(drParams["value"]); break;
						}
						paramCount++;
					}
				}
				Control uc = GetGadgetControl(dr["userControlName"].ToString(), param);
				uc.Name = "uc" + dr["id"].ToString();
				uc.Tag = dr["userControlName"].ToString();
				uc.Top = Convert.ToInt32(dr["posY"]);
				uc.Left = Convert.ToInt32(dr["posX"]);
				uc.Height = Convert.ToInt32(dr["height"]);
				uc.Width = Convert.ToInt32(dr["width"]);
				Gadgets.Add(new GadgetItem { left = uc.Left, top = uc.Top, width = uc.Width, height = uc.Height, control = uc, id = gadgetId, name = gadgetName });
			}
		}

		private static Control GetGadgetControl(string name, object[] param)
		{
			Control uc = null;
			switch (name)
			{
				case "ucGaugeWinRate" : uc = new Gadget.ucGaugeWinRate(param[0].ToString()); break;
				case "ucGaugeWN8" : uc = new Gadget.ucGaugeWN8(); break;
				case "ucGaugeWN7" : uc = new Gadget.ucGaugeWN7(); break;
				case "ucGaugeEFF" : uc = new Gadget.ucGaugeEFF(); break;
				case "ucTotalTanks" : uc = new Gadget.ucTotalTanks(); break;
				case "ucBattleTypes" : uc = new Gadget.ucBattleTypes(); break;
				case "ucBattleListLargeImages" : uc = new Gadget.ucBattleListLargeImages(Convert.ToInt32(param[0]),Convert.ToInt32(param[1])) ; break;
			}
			return uc;
		}

		public static GadgetItem FindGadgetArea(int mouseLeft, int mouseTop)
		{
			GadgetItem foundGadgetArea = null;
			bool found = false;
			int i = 0;
			while (!found && i < Gadgets.Count)
			{
				if (mouseLeft >= Gadgets[i].left &&
					mouseLeft <= Gadgets[i].left + Gadgets[i].width &&
					mouseTop >= Gadgets[i].top &&
					mouseTop <= Gadgets[i].top + Gadgets[i].height)
				{
					found = true;
					foundGadgetArea = Gadgets[i];
				}
				else
				{
					i++;
				}
			}
			return foundGadgetArea;
		}
	}
}
