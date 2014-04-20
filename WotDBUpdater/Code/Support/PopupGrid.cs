using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater.Code
{
	class PopupGrid
	{
		public enum PopupGridType
		{
			List = 1,
			Sql = 2
		}

		private static bool _ValueSelected;
		public static bool ValueSelected
		{
			get { return _ValueSelected; }
			set { _ValueSelected = value; }
		}	
		
		private static string _Value = "";
		public static string Value
		{
			get { return _Value; }
			set { _Value = value; }
		}	
		
		public static string Show(string Title, PopupGridType DataSourceType, string DataSource, string OverrideDbCon = "")
		{
			Value = "";
			ValueSelected = false;
			DataTable dt = new DataTable();
			if (DataSourceType == PopupGridType.List)
			{
				dt.Columns.Add("Items");
				string[] list = DataSource.Split(new string[] { "," }, StringSplitOptions.None);
				foreach (string item in list)
				{
					DataRow dr = dt.NewRow();
					dr["Items"] = item;
					dt.Rows.Add(dr);
				}
			}
			else if (DataSourceType == PopupGridType.Sql)
			{
				dt = DB.FetchData(DataSource);
			}
			if (dt.Rows.Count > 0)
			{
				Form frm = new Forms.PopupGrid(Title, dt);
				frm.SetDesktopLocation(Cursor.Position.X - (frm.Width / 2), Cursor.Position.Y + 20);
				frm.ShowDialog();
			}
			return Value;
		}
	}
}
