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
		
		private static string _SelectedValue = "";
		public static string SelectedValue
		{
			get { return _SelectedValue; }
			set { _SelectedValue = value; }
		}	
		
		public static string Show(string Title, PopupGridType DataSourceType, string DataSource, string OverrideDbCon = "")
		{
			SelectedValue = "";
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
				dt = db.FetchData(DataSource);
			}
			Form frm = new Forms.PopupGrid(Title, dt);
			frm.ShowDialog();
			return SelectedValue;
		}
	}
}
