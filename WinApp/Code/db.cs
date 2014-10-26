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
		
		public static DataTable FetchData(string sql, bool ShowError = true)
		{
			ConfigData.dbType SelecteDbType = Config.Settings.databaseType;
			DataTable dt = new DataTable();
			try
			{
				string dbcon = Config.DatabaseConnection();
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
				Log.LogToFile(ex, sql);
				if (ShowError)
				{
					Code.MsgBox.Show("Error fetching data from database. Please check your database settings." +
						Environment.NewLine + Environment.NewLine + sql +
						Environment.NewLine + Environment.NewLine + ex.ToString(), "Database error");
				}
			}
			return dt;
		}

		public static bool ExecuteNonQuery(string sql, bool ShowError = true, bool RunInBatch = false)
		{
			string lastRunnedSQL = "";
			bool ok = false;
			string[] sqlList = sql.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
			try
			{
				if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
				{
					SqlConnection con = new SqlConnection(Config.DatabaseConnection());
					con.Open();
					if (RunInBatch)
					{
						lastRunnedSQL = sql;
						sql = "BEGIN TRANSACTION; " + sql + "COMMIT TRANSACTION; ";
						SqlCommand command = new SqlCommand(sql, con);
						command.ExecuteNonQuery();
						Application.DoEvents(); // TODO: testing freeze-problem running long querys
					}
					else
					{
						foreach (string s in sqlList)
						{
							if (s.Trim().Length > 0)
							{
								lastRunnedSQL = s;
								SqlCommand command = new SqlCommand(s, con);
								command.ExecuteNonQuery();
								Application.DoEvents(); // TODO: testing freeze-problem running long querys
							}
						}
					}
					con.Close();
					ok = true;
				}
				else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
				{
					SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection());
					con.Open();
					if (RunInBatch)
					{
						lastRunnedSQL = sql;
						sql = "BEGIN TRANSACTION; " + sql + "END TRANSACTION; ";
						SQLiteCommand command = new SQLiteCommand(sql, con);
						command.ExecuteNonQuery();
						Application.DoEvents(); 
					}
					else
					{
						foreach (string s in sqlList)
						{
							if (s.Trim().Length > 0)
							{
								lastRunnedSQL = s;
								SQLiteCommand command = new SQLiteCommand(s, con);
								command.ExecuteNonQuery();
								Application.DoEvents(); 
							}
						}
					}
					con.Close();
					ok = true;
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex, lastRunnedSQL);
				if (ShowError)
				{
					Code.MsgBox.Show("Error execute query to database. Please check your input parameters." +
						Environment.NewLine + Environment.NewLine + lastRunnedSQL +
						Environment.NewLine + Environment.NewLine + ex.ToString(), "Database error");
					ok = false;
				}
			}
			return ok;
		}

		public static bool ExecuteNonQuery(string sql, string imgParameter, Image img, bool ShowError = true)
		{
			string lastRunnedSQL = "";
			bool ok = false;
			string[] sqlList = sql.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
			try
			{
				if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
				{
					SqlConnection con = new SqlConnection(Config.DatabaseConnection());
					con.Open();
					foreach (string s in sqlList)
					{
						if (s.Trim().Length > 0)
						{
							lastRunnedSQL = s;
							SqlCommand cmd = new SqlCommand(s, con);
							byte[] imgByte = ImageHelper.ImageToByteArray(img, ImageFormat.Png);
							cmd.Parameters.Add(imgParameter, SqlDbType.VarBinary, imgByte.Length).Value = imgByte;
							cmd.ExecuteNonQuery();
							Application.DoEvents(); 
						}
					}
					con.Close();
					ok = true;
				}
				else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
				{
					SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection());
					con.Open();
					foreach (string s in sqlList)
					{
						if (s.Trim().Length > 0)
						{
							lastRunnedSQL = s;
							SQLiteCommand cmd = con.CreateCommand();
							cmd.CommandText = s;
							SQLiteParameter imgParam = new SQLiteParameter(imgParameter, System.Data.DbType.Binary);
							imgParam.Value = ImageHelper.ImageToByteArray(img, ImageFormat.Png);
							cmd.Parameters.Add(imgParam);
							cmd.ExecuteNonQuery();
							Application.DoEvents(); 
						}
					}
					con.Close();
					ok = true;
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex, lastRunnedSQL);
				if (ShowError)
				{
					Code.MsgBox.Show("Error execute query to database. Please check your input parameters." +
						Environment.NewLine + Environment.NewLine + lastRunnedSQL +
						Environment.NewLine + Environment.NewLine + ex.ToString(), "Database error");
					ok = false;
				}
			}
			return ok;
		}

		public static DataTable ListTables()
		{
			DataTable dt = new DataTable();
			try
			{
				if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
				{
					string sql = "SELECT TABLE_NAME AS TABLE_NAME FROM information_schema.tables ORDER BY TABLE_NAME";
					dt = FetchData(sql);
				}
				else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
				{
					SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection());
					con.Open();
					DataTable TableList = new DataTable();
					TableList = con.GetSchema("tables"); // Returns list of tables in column "TABLE_NAME"
					con.Clone();
					TableList.DefaultView.Sort = "TABLE_NAME";
					dt = TableList.DefaultView.ToTable();
				}

			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				//throw;
			}
			return dt;
		}

		public static bool CreateDatabase(string databaseName, string fileLocation, ConfigData.dbType dbType)
		{
			bool dbOk = false;
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
						Code.MsgBox.Show("Error creating database, file path does not exist", "Error creating database");
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
						con.Open();
						string sql = "SELECT [name] FROM master.dbo.sysdatabases WHERE [name] = '" + databaseName + "'";
						SqlCommand cmd = new SqlCommand(sql, con);
						SqlDataReader reader = cmd.ExecuteReader();
						if (reader.HasRows) dbExists = true;
						con.Close();
					}
					if (dbExists)
					{
						Code.MsgBox.Show("Database with this name alreade exists, choose another database name.", "Cannot create database");
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
							Config.Settings.databaseName = databaseName;
							string msg = "";
							Config.SaveConfig(out msg);
							dbOk = true;
						}
						catch (System.Exception ex)
						{
							Log.LogToFile(ex);
							dbOk = false;
							Code.MsgBox.Show("Error creating database, check that valid databasename is selected." + 
								Environment.NewLine + Environment.NewLine + ex.ToString(), "Error creating database");
						}
					}
				}
				else if (dbType == ConfigData.dbType.SQLite)
				{
					// Check if database exists
					if (File.Exists(fileLocation + databaseName + ".db"))
					{
						Code.MsgBox.Show("Error creating database, databasefile already exists, select another database name", "Error creating database");
					}
					else
					{
						// Create new db file now
						string db = fileLocation + databaseName + ".db";
						SQLiteConnection.CreateFile(db);
						// Save new db file into settings
						Config.Settings.databaseFileName = fileLocation + databaseName + ".db";
						string msg = "";
						Config.SaveConfig(out msg);
						dbOk = true;
					}
				}
			}
			return dbOk;
		}
		
		public static void AddWithValue(ref string Sql, string Parameter, object Value, DB.SqlDataType DataType)
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
				else if (DataType == SqlDataType.Float)
				{
					var d = Convert.ToDecimal(Value).ToString();
					Sql = ReplaceParameterWithValue(Sql, Parameter, d);
				}
				else if (DataType == SqlDataType.DateTime)
				{
					Sql = ReplaceParameterWithValue(Sql, Parameter, "'" + StringValue + "'"); // yyyy-DD-mm
				}
				else if (DataType == SqlDataType.Image)
				{
					// convert image to blob
					Sql = ReplaceParameterWithValue(Sql, Parameter, StringValue); // fails on ReplaceParameterWithValue
				}
				else if (DataType == SqlDataType.Boolean)
				{
					bool boolVal = Convert.ToBoolean(Value);
					StringValue = "0";
					if (boolVal) StringValue = "1";
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

		public static bool CheckConnection(bool showErrorIfNotExists = true)
		{
			bool ok = false;
			DataTable dt = new DataTable();
			// Get database type
			if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
			{
				// Check data
				if (Config.Settings.databaseServer == null || Config.Settings.databaseServer == "" || Config.Settings.databaseName == "")
				{
					if (showErrorIfNotExists) Code.MsgBox.Show("Missing database server and/or database name, check Database Settings.", "Config error");
				}
				else
				{
					try
					{
						SqlConnection con = new SqlConnection(Config.DatabaseConnection());
						con.Open();
						SqlCommand command = new SqlCommand("SELECT * FROM player", con);
						SqlDataAdapter adapter = new SqlDataAdapter(command);
						adapter.Fill(dt);
						con.Close();
						ok = true;
					}
					catch (Exception ex)
					{
						Log.LogToFile(ex);
						if (showErrorIfNotExists) Code.MsgBox.Show("Error connecting to database or test on accessing table data falied. Please check Database Settings.", "Config error");
					}
				}
			}
			else if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
			{
				// Check data
				if (Config.Settings.databaseFileName == "" )
				{
					if (showErrorIfNotExists) Code.MsgBox.Show("Missing database file name, check Database Settings.", "Config error");
				}
				else
				{
					try
					{
						SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection());
						con.Open();
						SQLiteCommand command = new SQLiteCommand("SELECT * FROM player", con);
						SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
						adapter.Fill(dt);
						con.Close();
						ok = true;
					}
					catch (Exception ex)
					{
						Log.LogToFile(ex);
						if (showErrorIfNotExists) Code.MsgBox.Show("Error connecting to database or test on accessing table data falied. Please check Database Settingse", "Config error");
					}
				}
			}
			return ok;
		}



	}
}
