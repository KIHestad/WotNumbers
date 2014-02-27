using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater
{
    class moduleData
    {
        #region databaseLookup

        public static DataTable TankList = new DataTable();

        public static void GetTankListFromDB()
        {
            using (SqlConnection conn = new SqlConnection(Config.Settings.DatabaseConn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT tankId, name FROM tank", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(TankList);
                conn.Close();
            }
        }

        public static DataTable GetTurretDataFromDB(int tankId)
        {
            using (SqlConnection conn = new SqlConnection(Config.Settings.DatabaseConn))
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM turret WHERE tankId=" + tankId.ToString(), conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                conn.Close();
                return dt;
            }
        }

        public static DataTable jsonTurretTable = new DataTable();
        //public static DataView jsonTurretView;

        public static void GetJsonTurretFromDB()
        {
            using (SqlConnection conn = new SqlConnection(Config.Settings.DatabaseConn))
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM jsonTurret", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(jsonTurretTable);
                conn.Close();
            }
            //jsonTurretView = new DataView(jsonTurretTable);
            //jsonTurretTable.PrimaryKey = new DataColumn[] { jsonTurretTable.Columns["jsonProperty"] };
        }


        public static string listTanks()
        {
            string s = "";
            foreach (DataRow dr in TankList.Rows)
            {
                s += dr["tankId"] + " : " + dr["name"] + "\n";
            }
            return s;
        }

        public static int GetTankID(string TankName)
        {
            int tankId = 0;
            string expression = "name = '" + TankName + "'";
            DataRow[] foundRows = TankList.Select(expression);
            if (foundRows.Length > 0) // If tank exist in Tank table
                tankId = Convert.ToInt32(foundRows[0]["tankId"]);
            return tankId;
        }

        public static bool turretExists(int turretId)
        {
            string expression = "turretId = " + turretId.ToString();
            DataRow[] foundRows = TankList.Select(expression);
            return (foundRows.Length > 0);
        }

        #endregion



        #region main

        private static void saveNewTurret2DB(int turretId)
        {
            // Add to database
            SqlConnection con = new SqlConnection(Config.Settings.DatabaseConn);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO turret (turretId, tankId) VALUES (@turretId, @tankId)", con);
            cmd.Parameters.AddWithValue("@turretId", turretId);
            cmd.Parameters.AddWithValue("@tankId", turretId);      //  <---------
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public static void saveTurret(int turretId, DataRow NewTurretRow)  //, bool ForceUpdate = false
        {
            if (turretId > 0) // when turretId=0 the turret is not found in turret table
            {
                // Check if battle count has increased, first get existing battle count
                DataTable OldTurretTable = GetTurretDataFromDB(turretId); // Return Existing User Tank Data
                Array a = OldTurretTable.Select();
                int c = OldTurretTable.Rows.Count;
                // Check if turret exists
                if (OldTurretTable.Rows.Count == 0)
                {
                    saveNewTurret2DB(turretId);
                    OldTurretTable = GetTurretDataFromDB(turretId); // Return once more now after row is added
                }
                //DataRow OldTurretRow = OldTurretTable.Rows[0];

                updateTurret(NewTurretRow, OldTurretTable);
            }
        }




        private static void updateTurret(DataRow newTurretRow, DataTable oldTurretTable)
        {
            // Get fields to update
            string sqlFields = "";
            foreach (DataColumn column in oldTurretTable.Columns)
            {
                if (newTurretRow[column.ColumnName] != DBNull.Value)
                {
                    if (sqlFields.Length > 0) sqlFields += ", "; // Add comma exept for first element
                    string colName = column.ColumnName;
                    string colType = column.DataType.Name;
                    sqlFields += colName + "=";
                    switch (colType)
                    {
                        case "String": sqlFields += "'" + newTurretRow[colName] + "'"; break;
                        default: sqlFields += newTurretRow[colName]; break;
                    }
                }
            }
            // Update database
            if (sqlFields.Length > 0)
            {
                SqlConnection con = new SqlConnection(Config.Settings.DatabaseConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE turret SET " + sqlFields, con);
                cmd.Parameters.AddWithValue("@turretId", oldTurretTable.Rows[0]["turretId"]);
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        #endregion
    }
}
