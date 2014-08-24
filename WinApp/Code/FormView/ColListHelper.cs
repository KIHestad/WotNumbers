using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	class ColListHelper
	{
		public static int GetColListId(string colListName)
		{
			int colListId = 0;
			string sql = "select columnList.id as id " +
						 "from columnList  " +
						 "where columnList.colType=@colType and columnList.name=@name";
			DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@name", colListName, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				colListId = Convert.ToInt32(dt.Rows[0]["id"]);
			}
			return colListId;
		}

		public static int GetColSelectionId(string colName)
		{
			int colListId = 0;
			string sql = "select columnSelection.id as id " +
						 "from columnSelection " +
						 "where colType=@colType and name=@colName";
			DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@colName", colName, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				colListId = Convert.ToInt32(dt.Rows[0]["id"]);
			}
			return colListId;
		}

		public static GridFilter.Settings GetSettingsForColList(int colListId)
		{
			GridFilter.Settings dfl = new GridFilter.Settings();
			string sql = "SELECT columnList.id AS colListId, columnList.name AS colListName, columnList.defaultFavListId AS colListDefaultFavList, " +
						 "       favList.id AS favListId, favList.name AS favListName " +
						 "FROM columnList LEFT OUTER JOIN " +
						 "     favList ON columnList.defaultFavListId = favList.id " +
						 "WHERE columnList.id=@id;";
			DB.AddWithValue(ref sql, "@id", colListId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				dfl.ColListId = colListId;
				dfl.ColListName = dt.Rows[0]["colListName"].ToString();
				int defaultFavListID = Convert.ToInt32(dt.Rows[0]["colListDefaultFavList"]);
				if (defaultFavListID > 0)
				{
					dfl.FavListShow = GridFilter.FavListShowType.FavList;
					dfl.FavListId = defaultFavListID;
					dfl.FavListName = dt.Rows[0]["favListName"].ToString();
				}
				else
				{
					dfl.FavListShow = (GridFilter.FavListShowType)defaultFavListID;
				}
			}
			return dfl;
		}

		public static void SaveColWidth(string colName, int colWidht)
		{
			int colListId = MainSettings.GetCurrentGridFilter().ColListId;
			int colSelectionId = GetColSelectionId(colName);
			string sql = "UPDATE columnListSelection SET colWidth=@colWidth WHERE columnSelectionId=@columnSelectionId AND columnListId=@columnListId ;";
			DB.AddWithValue(ref sql, "@colWidth", colWidht, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@columnSelectionId", colSelectionId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@columnListId", colListId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);
		}

		public class ColListClass
		{
			public string name = "";
			public string colName = "";
			public string colNameSelect = "";
			public string description = "";
			public int width = 0;
			public string type = "";
			public string group = "";
		}

		public static void GetSelectedColumnList(out string Select, out List<ColListClass> colList, out int img, out int smallimg, out int contourimg, bool grouping = false, bool groupingSum = false)
		{
			string sql = "SELECT columnListSelection.sortorder, columnSelection.colName, columnSelection.colNameSQLite, columnSelection.name, columnListSelection.colWidth, columnSelection.colDataType  " +
						 "FROM   columnListSelection INNER JOIN " +
						 "		 columnSelection ON columnListSelection.columnSelectionId = columnSelection.id " +
						 "WHERE        (columnListSelection.columnListId = @columnListId) " +
						 "ORDER BY columnListSelection.sortorder; ";
			DB.AddWithValue(ref sql, "@columnListId", MainSettings.GetCurrentGridFilter().ColListId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql, Config.Settings.showDBErrors);
			Select = "";
			img = -1;
			smallimg = -1;
			contourimg = -1;
			List<ColListClass> selectColList = new List<ColListClass>();
			if (dt.Rows.Count == 0)
			{
				Select = "'No columns defined in Column Selection List' As 'Error', ";
				ColListClass colListItem = new ColListClass();
				colListItem.name = "Error";
				colListItem.width = 300;
				colListItem.type = "VarChar";
				selectColList.Add(colListItem);
			}
			else
			{
				int colNum = 0;
				foreach (DataRow dr in dt.Rows)
				{
					string colName = dr["colName"].ToString(); // Get default colName
					ColListClass colListItem = new ColListClass();
					string colAlias = dr["name"].ToString();
					colListItem.name = colAlias;
					colListItem.colName = dr["colName"].ToString();
					colListItem.colNameSelect = colListItem.colName;
					colListItem.width = Convert.ToInt32(dr["colWidth"]);
					colListItem.type = dr["colDataType"].ToString();
					selectColList.Add(colListItem);
					// Check for alternative colName for SQLite
					if (Config.Settings.databaseType == ConfigData.dbType.SQLite && dr["colNameSQLite"] != DBNull.Value)
					{
						colName = dr["colNameSQLite"].ToString();
					}
					// Check for datatypes
					string colDataType = dr["colDataType"].ToString();
					if (colDataType == "Image")
					{
						// Image, get from separate datatable
						string imgColName = dr["name"].ToString();
						switch (imgColName)
						{
							case "Tank Icon": contourimg = colNum; break;
							case "Tank Image": smallimg = colNum; break;
							case "Tank Image Large": img = colNum; break;
						}
					}
					else if (colDataType == "DateTime" || colDataType == "VarChar")
					{
						if (!grouping)
							Select += colName + " as '" + colAlias + "', "; // return value
						else
						{
							if (colName == "tank.name")
								Select += colName + " as '" + colAlias + "', "; // return value
							else
							{
								if (colDataType == "DateTime")
									colListItem.colNameSelect = "NULL";
								else if (colDataType == "VarChar")
									colListItem.colNameSelect = "''";
								Select += colListItem.colNameSelect + " as '" + colAlias + "', "; 
							}
						}
					}
					else // Numbers
					{
						if (!grouping)
							Select += colName + " as '" + colAlias + "', "; // return value
						else
						{
							if (colName == "battle.battlesCount")
								colListItem.colNameSelect = "SUM(" + colName + ")"; // Get sum battle count
							else if (colName == "battle.eff" || colName == "battle.wn7" || colName == "battle.wn8")
								colListItem.colNameSelect = "AVG(" + colName + ")"; // rating only use avg values
							else
							{
								if (groupingSum)
									colListItem.colNameSelect = "SUM(" + colName + ")"; // else avg value
								else
									colListItem.colNameSelect = "AVG(" + colName + ")"; // else avg value
							}
							Select += colListItem.colNameSelect + " as '" + colAlias + "', "; 
						}
					}
					colNum++;
				}
			}
			colList = selectColList;
		}

		public static void GetAllTankDataColumn(out string select, out List<ColListClass> colList)
		{
			string sql = "SELECT * " +
						 "FROM  columnSelection " +
						 "WHERE colDataType<>'Image' AND colType=1 " +
						 "ORDER BY position; ";
			DataTable dt = DB.FetchData(sql, Config.Settings.showDBErrors);
			select = "";
			List<ColListClass> selectColList = new List<ColListClass>();
			int colNum = 0;
			foreach (DataRow dr in dt.Rows)
			{
				string colName = dr["colName"].ToString(); // Get default colName
				ColListClass colListItem = new ColListClass();
				colListItem.name = dr["name"].ToString();
				colListItem.description = dr["description"].ToString();
				colListItem.width = Convert.ToInt32(dr["colWidth"]);
				colListItem.group = dr["colGroup"].ToString();
				colListItem.type = dr["colDataType"].ToString();
				selectColList.Add(colListItem);
				// Check for alternative colName for SQLite
				if (Config.Settings.databaseType == ConfigData.dbType.SQLite && dr["colNameSQLite"] != DBNull.Value)
				{
					colName = dr["colNameSQLite"].ToString();
				}
				// Normal select from db
				select += colName + " as '" + dr["name"].ToString() + "', ";
				colNum++;
			}
			select = select.Substring(0, select.Length - 2); // Remove latest comma
			colList = selectColList;
		}

		public static string GetColName(string name, GridView.Views view)
		{
			string sql = "SELECT colName FROM columnSelection WHERE name=@name AND colType=@colType; ";
			String colName = "";
			DB.AddWithValue(ref sql, "@colType", (int)view, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@name", name, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				colName = dt.Rows[0]["colName"].ToString();
			}
			return colName;
		}

	}
}
