using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater.Code
{
	class db
	{
		public enum dbType
		{
			MSSQLserver = 1,
			SQLite = 2
		}

		public DataTable RunSQL(string sql)
		{
			DataTable dt = new DataTable();
			using (SqlConnection con = new SqlConnection(Config.DatabaseConnection()))
			{
				con.Open();
				SqlCommand command = new SqlCommand(sql, con);
				SqlDataAdapter adapter = new SqlDataAdapter(command);
				adapter.Fill(dt);
				con.Close();
			}
			return dt;
		}

	}
}
