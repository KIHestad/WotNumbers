using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp.Code
{
	class DropDownGrid
	{
		public enum DropDownGridType
		{
			List = 1,
			Sql = 2,
            DataTable = 3,
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

		private static bool _Shown = false;
		public static bool Shown
		{
			get { return _Shown; }
			set { _Shown = value; }
		}


		public static string Show(BadDropDownBox DropDownControl, DropDownGridType DataSourceType, object DataSource, string OverrideDbCon = "")
		{
			if (Shown)
			{
				Shown = false;
			}
			else
			{
				Shown = true;
				Value = "";
				ValueSelected = false;
				DataTable dt = new DataTable();
				if (DataSourceType == DropDownGridType.List)
				{
					dt.Columns.Add("Items");
					string[] list = DataSource.ToString().Split(new string[] { "," }, StringSplitOptions.None);
					foreach (string item in list)
					{
						DataRow dr = dt.NewRow();
						dr["Items"] = item;
						dt.Rows.Add(dr);
					}
				}
				else if (DataSourceType == DropDownGridType.Sql)
				{
					dt = DB.FetchData(DataSource.ToString());
				}
                else if (DataSourceType == DropDownGridType.DataTable)
                {
                    dt = (DataTable)DataSource;
                }
				if (dt.Rows.Count > 0)
				{
					Point pos = DropDownControl.PointToScreen(new Point(0, 0));
					Form frm = new Forms.DropDownGrid(dt, DropDownControl);
					frm.SetDesktopLocation(pos.X, pos.Y + DropDownControl.Height);
					frm.Width = DropDownControl.Width;
					frm.Show();
				}
			}
			return Value;
		}
	}
}
