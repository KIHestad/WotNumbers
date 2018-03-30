using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Code
{
	public static class DB
	{
		public enum SqlDataType
		{
			VarChar = 1,
			Int = 2,
			DateTime = 3,
			Image = 4,
			Boolean = 5,
			Float = 6,
		}
		
		public async static Task<DataTable> FetchData(string sql, bool ShowError = true)
		{
			ConfigData.dbType SelecteDbType = Config.Settings.databaseType;
			DataTable dt = new DataTable();
			try
			{
				string dbcon = Config.DatabaseConnection();
				if (SelecteDbType == ConfigData.dbType.MSSQLserver)
				{
                    using (SqlConnection con = new SqlConnection(dbcon))
                    {
                        await con.OpenAsync();
                        using (SqlCommand command = new SqlCommand(sql, con))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            await Task.Run(() => adapter.Fill(dt));
                        }
                    }
				}
				else if (SelecteDbType == ConfigData.dbType.SQLite)
				{
                    using (SQLiteConnection con = new SQLiteConnection(dbcon))
                    {
                        await con.OpenAsync();
                        using (SQLiteCommand command = new SQLiteCommand(sql, con))
                        {
                            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                            await Task.Run(() => adapter.Fill(dt));
                        }
                    }
				}
			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex, sql);
				if (ShowError)
				{
					MsgBox.Show("Error fetching data from database. Please check your database settings." +
						Environment.NewLine + Environment.NewLine + sql +
						Environment.NewLine + Environment.NewLine + ex.ToString(), "Database error");
				}
			}
			return dt;
		}

        public async static Task<bool> HasColumn(string tableName, string columnName)
        {
            bool hasColumn = false;
            DataTable dt = new DataTable();
            int colNameIndex = 0;
            try
            {
                if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
                {
                    using (SqlConnection con = new SqlConnection(Config.DatabaseConnection()))
                    {
                        await con.OpenAsync();
                        string sql = "SELECT Name FROM sys.columns WHERE Name = N'" + columnName + "' AND Object_ID = Object_ID(N'" + tableName + "'); ";
                        using (SqlCommand command = new SqlCommand(sql, con))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            await Task.Run(() => adapter.Fill(dt));
                            colNameIndex = 0;
                        }
                    }
                }
                else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
                {
                    using (SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection()))
                    {
                        await con.OpenAsync();
                        string sql = "PRAGMA table_info(" + tableName + ");";
                        using (SQLiteCommand command = new SQLiteCommand(sql, con))
                        {
                            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                            await Task.Run(() => adapter.Fill(dt));
                            colNameIndex = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Log.LogToFile(ex, "Check if column exists in table");
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[colNameIndex].ToString().ToLower() == columnName.ToLower())
                    {
                        hasColumn = true;
                        break;
                    }
                }
            }
            return hasColumn;
        }

		public async static Task<bool> ExecuteNonQuery(string sql, bool ShowError = true, bool RunInBatch = false)
		{
			string lastRunnedSQL = "";
			bool ok = false;
			string[] sqlList = sql.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
			try
			{
				if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
				{
                    using (SqlConnection con = new SqlConnection(Config.DatabaseConnection()))
                    {
                        await con.OpenAsync();
                        if (RunInBatch)
                        {
                            lastRunnedSQL = sql;
                            sql = "BEGIN TRANSACTION; " + sql + "COMMIT TRANSACTION; ";
                            using (SqlCommand command = new SqlCommand(sql, con))
                                await command.ExecuteNonQueryAsync();
                        }
                        else
                        {
                            foreach (string s in sqlList)
                            {
                                if (s.Trim().Length > 0)
                                {
                                    lastRunnedSQL = s;
                                    using (SqlCommand command = new SqlCommand(s, con))
                                        await command.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }
                    ok = true;
				}
				else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
				{
                    using (SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection()))
                    {
                        await con.OpenAsync();
                        if (RunInBatch)
                        {
                            lastRunnedSQL = sql;
                            sql = "BEGIN TRANSACTION; " + sql + "; END TRANSACTION; ";
                            using (SQLiteCommand command = new SQLiteCommand(sql, con))
                                await command.ExecuteNonQueryAsync();
                        }
                        else
                        {
                            foreach (string s in sqlList)
                            {
                                if (s.Trim().Length > 0)
                                {
                                    lastRunnedSQL = s;
                                    using (SQLiteCommand command = new SQLiteCommand(s, con))
                                        await command.ExecuteNonQueryAsync();
                                }
                            }
                        }
                        ok = true;
                    }
				}
			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex, lastRunnedSQL);
				if (ShowError)
				{
					MsgBox.Show("Error execute query to database. Please check your input parameters." +
						Environment.NewLine + Environment.NewLine + lastRunnedSQL +
						Environment.NewLine + Environment.NewLine + ex.ToString(), "Database error");
					ok = false;
				}
			}
			return ok;
		}
        
        public async static Task<bool> ExecuteNonQuery(string sql, string imgParameter, Image img, bool ShowError = true)
		{
			string lastRunnedSQL = "";
			bool ok = false;
			string[] sqlList = sql.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
			try
			{
				if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
				{
                    using (SqlConnection con = new SqlConnection(Config.DatabaseConnection()))
                    {
                        await con.OpenAsync();
                        foreach (string s in sqlList)
                        {
                            if (s.Trim().Length > 0)
                            {
                                lastRunnedSQL = s;
                                using (SqlCommand cmd = new SqlCommand(s, con))
                                {
                                    byte[] imgByte = ImageHelper.ImageToByteArray(img, ImageFormat.Png);
                                    cmd.Parameters.Add(imgParameter, SqlDbType.VarBinary, imgByte.Length).Value = imgByte;
                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                        }
                        ok = true;
                    }
				}
				else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
				{
                    using (SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection()))
                    {
                        con.Open();
                        foreach (string s in sqlList)
                        {
                            if (s.Trim().Length > 0)
                            {
                                lastRunnedSQL = s;
                                using (SQLiteCommand cmd = con.CreateCommand())
                                {
                                    cmd.CommandText = s;
                                    SQLiteParameter imgParam = new SQLiteParameter(imgParameter, DbType.Binary);
                                    imgParam.Value = ImageHelper.ImageToByteArray(img, ImageFormat.Png);
                                    cmd.Parameters.Add(imgParam);
                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                        }
                        ok = true;
                    }
				}
			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex, lastRunnedSQL);
				if (ShowError)
				{
					MsgBox.Show("Error execute query to database. Please check your input parameters." +
						Environment.NewLine + Environment.NewLine + lastRunnedSQL +
						Environment.NewLine + Environment.NewLine + ex.ToString(), "Database error");
					ok = false;
				}
			}
			return ok;
		}

		public async static Task<DataTable> ListTables()
		{
			DataTable dt = new DataTable();
			try
			{
				if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
				{
					string sql = "SELECT TABLE_NAME AS TABLE_NAME FROM information_schema.tables ORDER BY TABLE_NAME";
					dt = await FetchData(sql);
				}
				else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
				{
                    using (SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection()))
                    {
                        await con.OpenAsync();
                        DataTable TableList = new DataTable();
                        TableList = con.GetSchema("tables"); // Returns list of tables in column "TABLE_NAME"
                        con.Clone();
                        TableList.DefaultView.Sort = "TABLE_NAME";
                        dt = TableList.DefaultView.ToTable();
                    }
				}
			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex);
			}
			return dt;
		}

		public async static Task<bool> CreateDatabase(string databaseName, string fileLocation, ConfigData.dbType dbType)
		{
			bool dbOk = false;
			// Check database file location
			bool fileLocationExsits = true;
			fileLocation = fileLocation.Trim();
			if (fileLocation.Length > 0)
			{
				string lastchar = fileLocation.Substring(fileLocation.Length - 1, 1);
				if (lastchar != "/" && lastchar != "\\")
					fileLocation += "\\";
			}
			if (!Directory.Exists(fileLocation))
			{
				DirectoryInfo prevPath = Directory.GetParent(fileLocation);
				if (!prevPath.Exists)
				{
					if (!Directory.GetParent(prevPath.FullName).Exists)
					{
						fileLocationExsits = false;
						MsgBox.Show("Error creating database, file path does not exist", "Error creating database");
					}
					else
					{
						Directory.CreateDirectory(fileLocation);
					}
				}
			}
			// Start Creating if file location exists
			if (fileLocationExsits)
			{
				if (dbType == ConfigData.dbType.MSSQLserver)
				{
					// Check if database exists
					bool dbExists = false;
					string winAuth = "Win";
					if (!Config.Settings.databaseWinAuth) winAuth = "Sql";
					string connectionstring = Config.DatabaseConnection(ConfigData.dbType.MSSQLserver, "", Config.Settings.databaseServer, "master", winAuth, Config.Settings.databaseUid, Config.Settings.databasePwd);
					using (SqlConnection con = new SqlConnection(connectionstring))
					{
						await con.OpenAsync();
						string sql = "SELECT [name] FROM master.dbo.sysdatabases WHERE [name] = '" + databaseName + "'";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            SqlDataReader reader = await cmd.ExecuteReaderAsync();
                            if (reader.HasRows) dbExists = true;
                        }
					}
					if (dbExists)
					{
						MsgBox.Show("Database with this name alreade exists, choose another database name.", "Cannot create database");
					}
					else
					{
                        using (SqlConnection myConn = new SqlConnection(connectionstring))
                        {
                            string sql = "CREATE DATABASE " + databaseName + " ON PRIMARY " +
                                        "(NAME = " + databaseName + ", " +
                                        "FILENAME = '" + fileLocation + databaseName + ".mdf', " +
                                        "SIZE = 5MB, FILEGROWTH = 10%) " +
                                        "LOG ON (NAME = " + databaseName + "_Log, " +
                                        "FILENAME = '" + fileLocation + databaseName + "_log.ldf', " +
                                        "SIZE = 1MB, " +
                                        "FILEGROWTH = 10%)";
                            using (SqlCommand myCommand = new SqlCommand(sql, myConn))
                            {
                                try
                                {
                                    // Create db now
                                    await myConn.OpenAsync();
                                    await myCommand.ExecuteNonQueryAsync();
                                    // Save new db into settings
                                    Config.Settings.databaseName = databaseName;
                                    await Config.SaveConfig();
                                    dbOk = true;
                                }
                                catch (Exception ex)
                                {
                                    await Log.LogToFile(ex);
                                    dbOk = false;
                                    MsgBox.Show("Error creating database, check that valid databasename is selected." +
                                        Environment.NewLine + Environment.NewLine + ex.ToString(), "Error creating database");
                                }
                            }
                        }
					}
				}
				else if (dbType == ConfigData.dbType.SQLite)
				{
					// Check if database exists
					if (File.Exists(fileLocation + databaseName + ".db"))
					{
						MsgBox.Show("Error creating database, databasefile already exists, select another database name", "Error creating database");
					}
					else
					{
						// Create new db file now
						string db = fileLocation + databaseName + ".db";
						SQLiteConnection.CreateFile(db);
						// Save new db file into settings
						Config.Settings.databaseFileName = fileLocation + databaseName + ".db";
                        await Config.SaveConfig();
                        dbOk = true;
					}
				}
			}
			return dbOk;
		}
		
		public static void AddWithValue(ref string Sql, string Parameter, object Value, DB.SqlDataType DataType)
		{
			if (Value == null || Value == DBNull.Value)
			{
				Sql = ReplaceParameterWithValue(Sql, Parameter, "NULL");
			}
			else
			{
                // Varchar - allow empty string value
                if (DataType == SqlDataType.VarChar)
				{
					string stringValue = Value.ToString();
					stringValue = stringValue.Replace("'", "''");
                    // stringValue = stringValue.Replace(";", ":"); makes total stats bug, since ; are used as data param separators
					Sql = ReplaceParameterWithValue(Sql, Parameter, "'" + stringValue + "'");
				}
                // Other datatypes do not allow empty valyu, change to null
                if (Value.ToString() == "")
                {
                    Sql = ReplaceParameterWithValue(Sql, Parameter, "NULL");
                }
                else if (DataType == SqlDataType.Int)
                {
                    string stringValue = Value.ToString();
                    Sql = ReplaceParameterWithValue(Sql, Parameter, stringValue);
                }
                else if (DataType == SqlDataType.Float)
                {
                    string stringValue = Convert.ToDecimal(Value).ToString();
                    stringValue = stringValue.Replace(",", ".");
                    Sql = ReplaceParameterWithValue(Sql, Parameter, stringValue);
                }
                else if (DataType == SqlDataType.DateTime)
                {
                    DateTime dateTimeValue = Convert.ToDateTime(Value);
                    Sql = ReplaceParameterWithValue(Sql, Parameter, "'" + dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + "'"); // yyyy-DD-mm
                }
                else if (DataType == SqlDataType.Image)
                {
                    // convert image to blob
                    string stringValue = Convert.ToDecimal(Value).ToString();
                    Sql = ReplaceParameterWithValue(Sql, Parameter, stringValue); // fails on ReplaceParameterWithValue
                }
                else if (DataType == SqlDataType.Boolean)
                {
                    bool boolVal = Convert.ToBoolean(Value);
                    string stringValue = "0";
                    if (boolVal) stringValue = "1";
                    Sql = ReplaceParameterWithValue(Sql, Parameter, stringValue); // fails on ReplaceParameterWithValue
                }
			}
		}

		private static string ReplaceParameterWithValue(string Sql, string Parameter, string Value)
		{
			// Search for Parameter within SQL - must be followed by valid nextchar = " " or "," or ")"
			string validchars = " ,;)";
			int pos = 0;
			while (Sql.IndexOf(Parameter, pos) >= pos)
			{
				pos = Sql.IndexOf(Parameter, pos);
				bool ok = false;
				if (pos + Parameter.Length == Sql.Length)
					ok = true; // found as last item in sql = ok
				else
				{
					string nextchar = Sql.Substring(pos + Parameter.Length, 1);
					ok = validchars.Contains(nextchar); // found valid char as next char
				}
				if (ok)
				{
					// Found valid Parameter within SQL string now
					Sql = Sql.Substring(0, pos) + Value + Sql.Substring(pos + Parameter.Length);
				}
				else
				{
					// Not valid parameter (part of other), move past to read rest of sql
					pos += Parameter.Length;
				}
			}
			return Sql;
		}

		public async static Task<bool> CheckConnection(bool showErrorIfNotExists = true)
		{
			bool ok = false;
			DataTable dt = new DataTable();
			// Get database type
			if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
			{
				// Check data
				if (Config.Settings.databaseServer == null || Config.Settings.databaseServer == "" || Config.Settings.databaseName == "")
				{
					if (showErrorIfNotExists) MsgBox.Show("Missing database server and/or database name, check Database Settings.", "Config error");
				}
				else
				{
					try
					{
                        using (SqlConnection con = new SqlConnection(Config.DatabaseConnection()))
                        {
                            await con.OpenAsync();
                            using (SqlCommand command = new SqlCommand("SELECT * FROM player", con))
                            {
                                SqlDataAdapter adapter = new SqlDataAdapter(command);
                                await Task.Run(() => adapter.Fill(dt));
                                ok = true;
                            }
                        }
					}
					catch (Exception ex)
					{
						await Log.LogToFile(ex);
						if (showErrorIfNotExists) MsgBox.Show("Error connecting to database or test on accessing table data falied. Please check Database Settings.", "Config error");
					}
				}
			}
			else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
			{
				// Check data
				if (Config.Settings.databaseFileName == "" )
				{
					if (showErrorIfNotExists) MsgBox.Show("Missing database file name, check Database Settings.", "Config error");
				}
				else
				{
					try
					{
                        using (SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection()))
                        {
                            await con.OpenAsync();
                            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM player", con))
                            {
                                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                                await Task.Run(() => adapter.Fill(dt));
                                ok = true;
                            }
                        }
					}
					catch (Exception ex)
					{
						await Log.LogToFile(ex);
						if (showErrorIfNotExists) MsgBox.Show("Error connecting to database or test on accessing table data falied. Please check Database Settingse", "Config error");
					}
				}
			}
			return ok;
		}

	}
}
