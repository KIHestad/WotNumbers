using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater.Code
{

	public enum PopupDataSourceType
	{
		List = 1,
		Sql = 2
	}

	class PopupGrid
	{

		private static string _SelectedValue = "";
		public static string SelectedValue
		{
			get { return _SelectedValue; }
			set { _SelectedValue = value; }
		}	
		
		public static string Show(string Title, PopupDataSourceType DataSourceType, string DataSource)
		{
			DataTable dt = new DataTable();
			if (DataSourceType == PopupDataSourceType.List)
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
			else if (DataSourceType == PopupDataSourceType.Sql)
			{
				dt = db.FetchData(DataSource);
			}
			Form frm = new Forms.PopupGrid(Title, dt);
			frm.ShowDialog();
			return SelectedValue;
		}
	}
}
