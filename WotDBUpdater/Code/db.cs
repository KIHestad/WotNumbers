using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater
{
	class db
	{
		

		public static DataTable FetchData(string sql)
		{
			DataTable dt = new DataTable();
			if (Config.Settings.databaseType == dbType.MSSQLserver)
			{
				SqlConnection con = new SqlConnection(Config.DatabaseConnection());
				con.Open();
				SqlCommand command = new SqlCommand(sql, con);
				SqlDataAdapter adapter = new SqlDataAdapter(command);
				adapter.Fill(dt);
				con.Close();
			}
			else if (Config.Settings.databaseType == dbType.SQLite)
			{
				SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection());
				con.Open();
				SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con);
				adapter.Fill(dt);
				con.Close();
			}
			return dt;
		}

		public static bool ExecuteNonQuery(string sql)
		{
			bool ok = false;
			if (Config.Settings.databaseType == dbType.MSSQLserver)
			{
				SqlConnection con = new SqlConnection(Config.DatabaseConnection());
				con.Open();
				SqlCommand command = new SqlCommand(sql, con);
				command.ExecuteNonQuery();
				con.Close();
				ok = true;
			}
			else if (Config.Settings.databaseType == dbType.SQLite)
			{
				SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection());
				con.Open();
				SQLiteCommand command = new SQLiteCommand(sql, con);
				command.ExecuteNonQuery();
				con.Close();
				ok = true;
			}
			return ok;
		}

		public static DataTable ListTables()
		{
			DataTable dt = new DataTable();
			if (Config.Settings.databaseType == dbType.MSSQLserver)
			{
				string sql = "SELECT '( Select from list )' AS TABLE_NAME UNION SELECT table_name AS TableName FROM information_schema.tables ORDER BY TableName";
				dt = FetchData(sql);
			}
			else if (Config.Settings.databaseType == dbType.SQLite)
			{
				SQLiteConnection con = new SQLiteConnection(Config.DatabaseConnection());
				con.Open();
				dt = con.GetSchema("tables"); // Returns list of tables in column "TABLE_NAME"
			}
			return dt;
		}



	}
}
