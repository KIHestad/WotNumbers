using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp.Code
{
	class ColListHelper
	{
        public class ColListItem
        {
            public int id = 0;
            public string colType = "";
            public string colName = "";
            public string name = "";
            public string colNameSelect = ""; // colName + ' as ' + name
            public string description = "";
            public string colGroup = "";
            public int colWidth = 0;
            public string colDataType = "";
            public string colNameSort = "";
        }

        public class ColListItems
        {
            public ColListItems()
            {
                ColListItemList = new List<ColListItem>();
                Select = "";
            }
            public string Select { get; set; }
            public List<ColListItem> ColListItemList { get; set; }
			public bool Contains(string columnName)
			{
				return ColListItemList.Find(c => c.colName == columnName) != null;
			}

			public int size()
			{
				return ColListItemList.Count;
			}
		}

        public class SelectedColumnList
        {
            public SelectedColumnList()
            {
                ColListItems = new ColListItems();
        
				Img = -1;
                Smallimg = -1;
                Contourimg = -1;
                Masterybadgeimg = -1;
            }
            public ColListItems ColListItems { get; set; }
            public int Img { get; set; }
            public int Smallimg { get; set; }
            public int Contourimg { get; set; }
            public int Masterybadgeimg { get; set; }
			public bool Contains(string columnName)
			{
				return ColListItems.Contains(columnName);
			}
        }

		public async static Task<int> GetColListId(string colListName)
		{
			int colListId = 0;
			string sql = "select columnList.id as id " +
						 "from columnList  " +
						 "where columnList.colType=@colType and columnList.name=@name";
			DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@name", colListName, DB.SqlDataType.VarChar);
			DataTable dt = await DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				colListId = Convert.ToInt32(dt.Rows[0]["id"]);
			}
			return colListId;
		}

		public async static Task<string> GetColListName(int colListId)
		{
			string colListName = "";
			string sql = "select columnList.name " +
						 "from columnList  " +
						 "where columnList.id=@id";
			DB.AddWithValue(ref sql, "@id", colListId, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				colListName = dt.Rows[0]["name"].ToString();
			}
			return colListName;
		}
        
		public async static Task<ColListItem> GetColListStartup(GridView.Views view)
		{
            ColListItem item = new ColListItem();
            string sql = "select id, name " +
						 "from columnList  " +
						 "where columnList.colType=@colType and colDefault=1 ";
			DB.AddWithValue(ref sql, "@colType", (int)view, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				item.id = Convert.ToInt32(dt.Rows[0]["id"]);
				item.name = dt.Rows[0]["name"].ToString();
			}
			return item;
		}

		public async static Task<int> GetColSelectionId(string colName)
		{
			int colListId = 0;
			string sql = "select columnSelection.id as id " +
						 "from columnSelection " +
						 "where colType=@colType and name=@colName";
			DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@colName", colName, DB.SqlDataType.VarChar);
			DataTable dt = await DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				colListId = Convert.ToInt32(dt.Rows[0]["id"]);
			}
			return colListId;
		}

		public async static Task<GridFilter.Settings> GetSettingsForColList(int colListId)
		{
			GridFilter.Settings dfl = new GridFilter.Settings();
			string sql = "SELECT columnList.id AS colListId, columnList.name AS colListName, columnList.defaultFavListId AS colListDefaultFavList, " +
						 "       favList.id AS favListId, favList.name AS favListName " +
						 "FROM columnList LEFT OUTER JOIN " +
						 "     favList ON columnList.defaultFavListId = favList.id " +
						 "WHERE columnList.id=@id;";
			DB.AddWithValue(ref sql, "@id", colListId, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
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

		public async static Task SaveColWidth(string colName, int colWidht)
		{
			int colListId = MainSettings.GetCurrentGridFilter().ColListId;
			int colSelectionId = await GetColSelectionId(colName);
			string sql = "UPDATE columnListSelection SET colWidth=@colWidth WHERE columnSelectionId=@columnSelectionId AND columnListId=@columnListId ;";
			DB.AddWithValue(ref sql, "@colWidth", colWidht, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@columnSelectionId", colSelectionId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@columnListId", colListId, DB.SqlDataType.Int);
			await DB.ExecuteNonQuery(sql);
		}

		public async static Task<SelectedColumnList> GetSelectedColumnList(bool grouping = false, bool groupingSum = false)
		{
			string sql = "SELECT columnListSelection.sortorder, columnSelection.colName, columnSelection.colNameSQLite, columnSelection.name, columnListSelection.colWidth, columnSelection.colDataType, columnSelection.colNameSort " +
						 "FROM   columnListSelection INNER JOIN " +
						 "		 columnSelection ON columnListSelection.columnSelectionId = columnSelection.id " +
						 "WHERE        (columnListSelection.columnListId = @columnListId) " +
						 "ORDER BY columnListSelection.sortorder; ";
			DB.AddWithValue(ref sql, "@columnListId", MainSettings.GetCurrentGridFilter().ColListId, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql, Config.Settings.showDBErrors);
            SelectedColumnList selectedColumnList = new SelectedColumnList();
			if (dt.Rows.Count == 0)
			{
                selectedColumnList.ColListItems.Select = "'No columns defined in Column Selection List' As 'Error', ";
                ColListItem colListItem = new ColListItem
                {
                    name = "Error",
                    colWidth = 300,
                    colType = "VarChar"
                };
                selectedColumnList.ColListItems.ColListItemList.Add(colListItem);
			}
			else
			{
				int colNum = 0;
				foreach (DataRow dr in dt.Rows)
				{
					string colName = dr["colName"].ToString(); // Get default colName
					ColListItem colListItem = new ColListItem();
					string colAlias = dr["name"].ToString();
					colListItem.name = colAlias;
					colListItem.colName = dr["colName"].ToString();
					colListItem.colNameSelect = colListItem.colName;
					colListItem.colWidth = Convert.ToInt32(dr["colWidth"]);
					colListItem.colType = dr["colDataType"].ToString();
					if (dr["colNameSort"] != DBNull.Value)
						colListItem.colNameSort = dr["colnameSort"].ToString();
					else
						colListItem.colNameSort = colListItem.colName;
                    selectedColumnList.ColListItems.ColListItemList.Add(colListItem);
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
							case "Tank Icon": selectedColumnList.Contourimg = colNum; break;
							case "Tank Image": selectedColumnList.Smallimg = colNum; break;
							case "Tank Image Large": selectedColumnList.Img = colNum; break;
							case "Mastery Badge": selectedColumnList.Masterybadgeimg = colNum; break;
						}
					}
					else if (colDataType == "DateTime" || colDataType == "VarChar")
					{
						if (!grouping)
                            selectedColumnList.ColListItems.Select += colName + " as '" + colAlias + "', "; // return value
						else
						{
							if (colName == "tank.name" || colName == "tank.short_name")
                                selectedColumnList.ColListItems.Select += colName + " as '" + colAlias + "', "; // return value
                            else
							{
								if (colDataType == "DateTime")
									colListItem.colNameSelect = "NULL";
								else if (colDataType == "VarChar")
									colListItem.colNameSelect = "''";
                                selectedColumnList.ColListItems.Select += colListItem.colNameSelect + " as '" + colAlias + "', "; 
							}
						}
					}
					else // Numbers
					{
						if (!grouping)
                            selectedColumnList.ColListItems.Select += colName + " as '" + colAlias + "', "; // return value
						else
						{
							if (colName == "battle.battlesCount")
								colListItem.colNameSelect = "SUM(" + colName + ")"; // Get sum battle count
							else if (colName == "battle.eff" || colName == "battle.wn7" || colName == "battle.wn8")
								colListItem.colNameSelect = "AVG(" + colName + ")"; // rating only use avg values
							else if (colAlias == "Tier")
								colListItem.colNameSelect = "AVG(" + colName + ")"; // only average value for tier
							else if (colAlias == "Min Tier")
								colListItem.colNameSelect = "AVG(" + colName + ")"; // only average value for min tier
							else if (colAlias == "Max Tier")
								colListItem.colNameSelect = "AVG(" + colName + ")"; // only average value for max tier
							else
							{
								if (groupingSum)
									colListItem.colNameSelect = "SUM(" + colName + " * battle.battlesCount)"; // else avg value
								else
									colListItem.colNameSelect = "AVG(" + colName + ")"; // else avg value
							}
                            selectedColumnList.ColListItems.Select += colListItem.colNameSelect + " as '" + colAlias + "', "; 
						}
					}
					colNum++;
                    // Check for adding calculated column "Battles today" after column "Battles Day"
                    if (colAlias == "Battles Day")
                    {
                        colListItem = new ColListItem
                        {
                            name = "Battles Today",
                            colName = "Battles Today",
                            colNameSelect = "0",
                            colWidth = Convert.ToInt32(dr["colWidth"]), // Same as prev column = "Battles Day"
                            colType = "Int",
                            colNameSort = "Battles Today"
                        };
                        selectedColumnList.ColListItems.ColListItemList.Add(colListItem);
                        selectedColumnList.ColListItems.Select += "0 as 'Battles Today', ";
                    }
				}
			}
			return selectedColumnList;
		}


		public async static Task<ColListItems> GetAllTankDataColumn()
		{
			string sql = "SELECT * " +
						 "FROM  columnSelection " +
						 "WHERE colDataType<>'Image' AND colType=1 " +
						 "ORDER BY position; ";
			DataTable dt = await DB.FetchData(sql, Config.Settings.showDBErrors);
            ColListItems tankDataColumn = new ColListItems();
			List<ColListItem> selectColList = new List<ColListItem>();
			int colNum = 0;
			foreach (DataRow dr in dt.Rows)
			{
				string colName = dr["colName"].ToString(); // Get default colName
                ColListItem colListItem = new ColListItem
                {
                    name = dr["name"].ToString(),
                    description = dr["description"].ToString(),
                    colWidth = Convert.ToInt32(dr["colWidth"]),
                    colGroup = dr["colGroup"].ToString(),
                    colType = dr["colDataType"].ToString()
                };
                if (dr["colnameSort"] != DBNull.Value)
					colListItem.colNameSort = dr["colnameSort"].ToString();
				else
					colListItem.colNameSort = colListItem.colName;
				selectColList.Add(colListItem);
				// Check for alternative colName for SQLite
				if (Config.Settings.databaseType == ConfigData.dbType.SQLite && dr["colNameSQLite"] != DBNull.Value)
				{
					colName = dr["colNameSQLite"].ToString();
				}
                // Normal select from db
                tankDataColumn.Select += colName + " as '" + dr["name"].ToString() + "', ";
				colNum++;
			}
            tankDataColumn.Select = tankDataColumn.Select.Substring(0, tankDataColumn.Select.Length - 2); // Remove latest comma
            tankDataColumn.ColListItemList = selectColList;
            return tankDataColumn;

        }

		public async static Task<ColListItem> GetColListItem(int id)
		{
			string sql = "SELECT * FROM columnSelection WHERE id=@id; ";
			DB.AddWithValue(ref sql, "@id", id, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
			return GetColListItem(dt);			
		}

		public async static Task<ColListItem> GetColListItem(string name, GridView.Views view)
		{
			string sql = "SELECT * FROM columnSelection WHERE name=@name AND colType=@colType; ";
			DB.AddWithValue(ref sql, "@colType", (int)view, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@name", name, DB.SqlDataType.VarChar);
			DataTable dt = await DB.FetchData(sql);
			return GetColListItem(dt);
		}

		public static ColListItem GetColListItem(DataTable dt)
		{
			ColListItem clc = new ColListItem();
			if (dt.Rows.Count > 0) 
			{
				clc.colName = dt.Rows[0]["colName"].ToString();
				clc.colNameSelect = dt.Rows[0]["colName"].ToString() + " as " + dt.Rows[0]["name"].ToString();
				clc.description = dt.Rows[0]["description"].ToString();
				clc.colGroup = dt.Rows[0]["colGroup"].ToString();
				clc.name = dt.Rows[0]["name"].ToString();
				clc.colType = dt.Rows[0]["colType"].ToString();
				clc.colWidth = Convert.ToInt32(dt.Rows[0]["colWidth"]);
				clc.colDataType = dt.Rows[0]["colDataType"].ToString();
				if (dt.Rows[0]["colnameSort"] != DBNull.Value)
					clc.colNameSort = dt.Rows[0]["colnameSort"].ToString();
				else
					clc.colNameSort = clc.colName;
			}
			return clc;
		}

		public async static Task ColListSort(int colType)
		{
			string sql = "select * from columnList where position is not null and colType=" + colType.ToString() + " order by position;";
			DataTable dt = await DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				sql = "";
				int pos = 1;
				foreach (DataRow dr in dt.Rows)
				{
					sql += "update columnList set position=@pos where id=@id; ";
					DB.AddWithValue(ref sql, "@id", Convert.ToInt32(dr["id"]), DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@pos", pos, DB.SqlDataType.Int);
					pos++;
				}
				await DB.ExecuteNonQuery(sql);
			}
		}

        public async static Task<ToolStrip> SetToolStripColType(ToolStrip toolStripColType, GridView.Views view, bool forGadget = false)
        {
            // Get colGroups to show in toolbar
            string forGadgetWhere = "";
            if (forGadget)
                forGadgetWhere = " AND colDataType NOT IN ('VarChar', 'Image', 'DateTime') AND colGroup NOT IN ('Module', 'Equip/Crew') "; 
            string sql = "select colGroup from columnSelection WHERE colType=@colType AND colGroup IS NOT NULL " + forGadgetWhere + " order by position; "; // First get all sorted by position
            DB.AddWithValue(ref sql, "@colType", (int)view, DB.SqlDataType.Int);
            DataTable dt = await DB.FetchData(sql);
            // Now get unique values based
            List<string> colGroup = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                if (!colGroup.Contains(dr[0].ToString())) colGroup.Add(dr[0].ToString());
            }
            int colGroupRow = -1; // Start on -1, first element will be -1 = All tanks, should be ignored, second = 0 -> first group button -> element [0] from select
            foreach (ToolStripButton button in toolStripColType.Items)
            {
                if (colGroupRow >= 0 && colGroup.Count > colGroupRow)
                {
                    button.Visible = true;
                    button.Checked = false;
                    button.Text = colGroup[colGroupRow];
                }
                else
                {
                    if (colGroupRow >= 0) button.Visible = false;
                }
                colGroupRow++;
            }
            return toolStripColType;
        }

        public async static Task<DataTable> GetDataGridColums(ToolStrip toolStripColType, GridView.Views view, bool forGadget = false)
        {
            string forGadgetWhere = "";
            if (forGadget)
                forGadgetWhere = " AND colType=1 AND colNameSum IS NOT NULL "; 
            string sql = "SELECT name as 'Name', description as 'Description', id, colWidth FROM columnSelection WHERE colType=@colType " + forGadgetWhere + " " ;
            // Check filter
            string colGroup = "All";
            foreach (ToolStripButton button in toolStripColType.Items)
            {
                if (button.Checked) colGroup = button.Text;
            }
            if (colGroup != "All") sql += "AND colGroup=@colGroup ";
            sql += "ORDER BY position; ";
            DB.AddWithValue(ref sql, "@colType", (int)view, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@colGroup", colGroup, DB.SqlDataType.VarChar);
            return await DB.FetchData(sql);
        }

	}
}
