using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp.Code
{
	class FavListHelper
	{

		public static int lastAddFavListFromPopup = 0;
		public static bool refreshGridAfterAddRemove = false;

		public async static Task<int> GetId(string FavListName)
		{
			int favListId = 0;
			string sql = "select id from favList where name=@name;";
			DB.AddWithValue(ref sql, "@name", FavListName, DB.SqlDataType.VarChar);
			DataTable dt = await DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				favListId = Convert.ToInt32(dt.Rows[0]["id"]);
			}
			return favListId;
		}

		public async static Task FavListSort()
		{
			string sql = "select * from favList where position is not null order by position;";
			DataTable dt = await DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				sql = "";
				int pos = 1;
				foreach (DataRow dr in dt.Rows)
				{
					sql += "update favList set position=@pos where id=@id; ";
					DB.AddWithValue(ref sql, "@id", Convert.ToInt32(dr["id"]), DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@pos", pos, DB.SqlDataType.Int);
					pos++;
				}
				await DB.ExecuteNonQuery(sql);
			}
		}

		public async static Task TankSort(int favListId)
		{
			string sql = "select tankId from favListTank order by sortorder";
			DB.AddWithValue(ref sql, "@tankId", favListId, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
			// Modify sort order generate sql
			sql = "";
			int pos = 1;
			foreach (DataRow row in dt.Rows)
			{
				sql += "update favListTank set sortorder=@sortorder; ";
				DB.AddWithValue(ref sql, "@sortorder", pos, DB.SqlDataType.Int);
			}
			// Update
			await DB.ExecuteNonQuery(sql);
		}

		public async static Task<bool> CheckIfAnyFavList(Form parentForm, int tankId, bool add)
		{
			bool found = true;
			string sql = "select id from favList ";
			if (add)
				sql += "where id not in (select favListId from favListTank where tankId=@tankId) ";
			else
				sql += "where id in (select favListId from favListTank where tankId=@tankId) ";
			DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
			// Check if any favList is available
			if (dt.Rows.Count == 0)
			{
				found = false;
				if (add)
					MsgBox.Show("This tank is already included in all favourite tank lists, or no tank list exists.", "Cannot add tank to favourite tank list", parentForm);
				else
					MsgBox.Show("This tank is not includeded in any favourite tank list, or no tank list exists.", "Cannot remove tank from favourite tank list", parentForm);
			}
			return found;
		}
	}
}
