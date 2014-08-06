using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public static class DB
	{
		public enum SqlDataType
		{
			VarChar = 1,
			Int = 2,
			DateTime = 3,
			Image = 4,
		}
		
		public class DBResult
		{
			public bool Error = false;
			public string Title = "Database Message";
			public string Text = "Database operation done successfully.";
			public string ErrorMsg = "";
			public string lastSQL = "";
		}

		public static DataTable FetchData(string sql, ConfigData config, out DBResult result)
		{
			result = new DBResult();
			result.lastSQL = sql;
			ConfigData.dbType SelecteDbType = config.databaseType;
			DataTable dt = new DataTable();
			try
			{
				string dbcon = Config.DatabaseConnection(config);
				if (SelecteDbType == ConfigData.dbType.MSSQLserver)
				{
					SqlConnection con = new SqlConnection(dbcon);
					con.Open();
					SqlCommand command = new SqlCommand(sql, con);
					SqlDataAdapter adapter = new SqlDataAdapter(command);
					adapter.Fill(dt);
					con.Close();
				}
				else if (SelecteDbType == ConfigData.dbType.SQLite)
				{
					
					SQLiteConnection con = new SQLiteConnection(dbcon);
					con.Open();
					SQLiteCommand command = new SQLiteCommand(sql, con);
					SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
					adapter.Fill(dt);
					con.Close();
				}
			}
			catch (Exception ex)
			{
				result.Error = true;
				result.ErrorMsg = ex.Message;
				result.Title = "Database error fetching data";
				result.Text = "Error fetching data from database. Please check your database settings.";
			}
			return dt;
		}

		public static void ExecuteNonQuery(string sql, ConfigData config, out DBResult result, bool runInBatch = false)
		{
			result = new DBResult();
			string[] sqlList = sql.Split(new string[] { "GO",";" }, StringSplitOptions.RemoveEmptyEntries);
			try
			{
				if (config.databaseType == ConfigData.dbType.MSSQLserver)
				{
					SqlConnection con = new SqlConnection(Config.DatabaseConnection(config));
					con.Open();
					if (runInBatch)
					{
						result.lastSQL = sql;
						sql = "BEGIN TRANSACTION; " + sql + "COMMIT TRANSACTION; ";
						SqlCommand command = new SqlCommand(sql, con);
						command.ExecuteNonQuery();
					}
					else
					{
						foreach (string s in sqlList)
						{
							if (s.Trim().Length > 0)
							{
								result.lastSQL = sql;
								SqlCommand command = new SqlCommand(s, con);
								command.ExecuteNonQuery();
							}
						}
					}
					con.Close();
				}
				else if (config.databaseType == ConfigData.dbType.SQLite)
				{
					SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection(config));
					con.Open();
					if (runInBatch)
					{
						result.lastSQL = sql;
						sql = "BEGIN TRANSACTION; " + sql + "END TRANSACTION; ";
						SQLiteCommand command = new SQLiteCommand(sql, con);
						command.ExecuteNonQuery();
					}
					else
					{
						foreach (string s in sqlList)
						{
							if (s.Trim().Length > 0)
							{
								result.lastSQL = sql;
								SQLiteCommand command = new SQLiteCommand(s, con);
								command.ExecuteNonQuery();
							}
						}
					}
					con.Close();
				}
			}
			catch (Exception ex)
			{
				result.Error = true;
				result.ErrorMsg = ex.Message;
				result.Title = "Database error executing query";
				result.Text = "Error execute query to database. Please check your input parameters.";
			}
		}
		
		public static DataTable ListTables(ConfigData config, out DBResult result)
		{
			result = new DBResult();
			DataTable dt = new DataTable();
			try
			{
				if (config.databaseType == ConfigData.dbType.MSSQLserver)
				{
					DBResult fetchTableList = new DBResult();
					string sql = "SELECT TABLE_NAME AS TABLE_NAME FROM information_schema.tables ORDER BY TABLE_NAME";
					result.lastSQL = sql;
					dt = FetchData(sql, config, out fetchTableList);
					if (fetchTableList.Error)
					{
						result.Error = true;
						result.ErrorMsg = fetchTableList.ErrorMsg;
						result.Title = "Database error getting list of tables";
						result.Text = "Error execute query to for MSSQL for listing tables in database. Please check your database connection.";
					}
				}
				else if (config.databaseType == ConfigData.dbType.SQLite)
				{
					result.lastSQL = "con.GetSchema('tables')";
					SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection(config));
					con.Open();
					DataTable TableList = new DataTable();
					TableList = con.GetSchema("tables"); // Returns list of tables in column "TABLE_NAME"
					con.Clone();
					dt = TableList;
				}

			}
			catch (Exception ex)
			{
				result.Error = true;
				result.ErrorMsg = ex.Message;
				result.Title = "Database error getting list of tables";
				result.Text = "Error execute query to database for listing tables. Please check your database connection.";
			}
			return dt;
		}

		public static void CreateDatabase(string databaseName, string fileLocation, ConfigData.dbType dbType, ref ConfigData config, string configfile, out DBResult result)
		{
			result = new DBResult();
			// Check database file location
			bool fileLocationExsits = true;
			fileLocation = fileLocation.Trim();
			if (!Directory.Exists(fileLocation))
			{
				DirectoryInfo prevPath = Directory.GetParent(fileLocation);
				if (!prevPath.Exists)
				{
					if (!Directory.GetParent(prevPath.FullName).Exists)
					{
						fileLocationExsits = false;
						result.Error = true;
						result.ErrorMsg = "Path does not exist";
						result.Title = "Error creating database";
						result.Text = "Error creating database, file path does not exist";
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
					if (!config.databaseWinAuth) winAuth = "Sql";
					string connectionstring = Config.DatabaseConnection(config, ConfigData.dbType.MSSQLserver, "", config.databaseServer, "master", winAuth, config.databaseUid, config.databasePwd);
					using (SqlConnection con = new SqlConnection(connectionstring))
					{
						con.Open();
						string sql = "SELECT [name] FROM master.dbo.sysdatabases WHERE [name] = '" + databaseName + "'";
						SqlCommand cmd = new SqlCommand(sql, con);
						SqlDataReader reader = cmd.ExecuteReader();
						if (reader.HasRows) dbExists = true;
						con.Close();
					}
					if (dbExists)
					{
						result.Error = true;
						result.ErrorMsg = "Database with this name alreade exists";
						result.Title = "Cannot create database";
						result.Text = "Database with this name alreade exists, choose another database name.";
					}
					else
					{
						SqlConnection myConn = new SqlConnection(connectionstring);
						string sql = "CREATE DATABASE " + databaseName + " ON PRIMARY " +
									"(NAME = " + databaseName + ", " +
									"FILENAME = '" + fileLocation + databaseName + ".mdf', " +
									"SIZE = 5MB, FILEGROWTH = 10%) " +
									"LOG ON (NAME = " + databaseName + "_Log, " +
									"FILENAME = '" + fileLocation + databaseName + "_log.ldf', " +
									"SIZE = 1MB, " +
									"FILEGROWTH = 10%)";

						SqlCommand myCommand = new SqlCommand(sql, myConn);
						try
						{
							// Create db now
							myConn.Open();
							myCommand.ExecuteNonQuery();
							myConn.Close();
							// Save new db into settings
							config.databaseName = databaseName;
							string msg = "";
							Config.SaveConfig(config, configfile, out msg);
						}
						catch (System.Exception ex)
						{
							result.Error = true;
							result.ErrorMsg = ex.Message;
							result.Title = "Error creating database";
							result.Text = "Error creating database, check that valid databasename is selected.";
						}
					}
				}
				else if (dbType == ConfigData.dbType.SQLite)
				{
					// Check if database exists
					if (File.Exists(fileLocation + databaseName + ".db"))
					{
						result.Error = true;
						result.ErrorMsg = "Databasefile already exists";
						result.Title = "Error creating database";
						result.Text = "Error creating database, databasefile already exists, select another database name.";
					}
					else
					{
						// Create new db file now
						string db = fileLocation + databaseName + ".db";
						SQLiteConnection.CreateFile(db);
						// Save new db file into settings
						config.databaseFileName = fileLocation + databaseName + ".db";
						string msg = "";
						Config.SaveConfig(config, configfile, out msg);
					}
				}
			}
		}

		public static void AddWithValue(ref string Sql, string Parameter, object Value, DB.SqlDataType DataType, ConfigData config)
		{
			if (Value == DBNull.Value)
			{
				Sql = ReplaceParameterWithValue(Sql, Parameter, "NULL"); ;
			}
			else
			{
				string StringValue = Value.ToString();
				var byteArray = Value.ToString();
				if (DataType == SqlDataType.VarChar)
				{
					StringValue = StringValue.Replace("'", "''");
					Sql = ReplaceParameterWithValue(Sql, Parameter, "'" + StringValue + "'");
				}
				else if (DataType == SqlDataType.Int)
				{
					Sql = ReplaceParameterWithValue(Sql, Parameter, StringValue);
				}
				else if (DataType == SqlDataType.DateTime)
				{
					Sql = ReplaceParameterWithValue(Sql, Parameter, "'" + StringValue + "'"); // yyyy-DD-mm
				}
				else if (DataType == SqlDataType.Image)
				{
					Sql = ReplaceParameterWithValue(Sql, Parameter, StringValue); // fails on ReplaceParameterWithValue
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

		public static void CheckConnection(ConfigData config, out DBResult result)
		{
			result = new DBResult();
			DataTable dt = new DataTable();
			// Get database type
			if (config.databaseType == ConfigData.dbType.MSSQLserver)
			{
				// Check data
				if (config.databaseServer == null || config.databaseServer == "" || config.databaseName == "")
				{
					result.Error = true;
					result.ErrorMsg = "Missing database server and/or database name";
					result.Title = "Config error";
					result.Text = "Missing database server and/or database name, check Database Settings.";
				}
				else
				{
					try
					{
						SqlConnection con = new SqlConnection(Config.DatabaseConnection(config));
						con.Open();
						SqlCommand command = new SqlCommand("SELECT * FROM player", con);
						SqlDataAdapter adapter = new SqlDataAdapter(command);
						adapter.Fill(dt);
						con.Close();
					}
					catch (Exception ex)
					{
						result.Error = true;
						result.ErrorMsg = ex.Message;
						result.Title = "Config error";
						result.Text = "Error connecting to database or test on accessing table data falied. Please check Database Settings.";
					}
				}
			}
			else if (config.databaseType == ConfigData.dbType.SQLite)
			{
				// Check data
				if (config.databaseFileName == "")
				{
					result.Error = true;
					result.ErrorMsg = "Missing database server and/or database name";
					result.Title = "Config error";
					result.Text = "Missing database server and/or database name, check Database Settings.";
				}
				else
				{
					try
					{
						SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection(config));
						con.Open();
						SQLiteCommand command = new SQLiteCommand("SELECT * FROM player", con);
						SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
						adapter.Fill(dt);
						con.Close();
					}
					catch (Exception ex)
					{
						result.Error = true;
						result.ErrorMsg = ex.Message;
						result.Title = "Config error";
						result.Text = "Error connecting to database or test on accessing table data falied. Please check Database Settings.";
					}
				}
			}
		}
	}
}
