using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater
{
    public static class tankData
    {
        #region DatabaseLookup

        public static DataTable TankList = new DataTable();

        public static void GetTankListFromDB()
        {
            using(SqlConnection conn = new SqlConnection(Config.Settings.DatabaseConn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT tankId, name FROM tank", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(TankList);
                conn.Close();
            }
        }

        public static DataTable GetUserTankDataFromDB(int tankId)
        {
            using (SqlConnection conn = new SqlConnection(Config.Settings.DatabaseConn))
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM userTank WHERE wotUserId = " + Config.Settings.UserID + " AND tankId=" + tankId.ToString(), conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                conn.Close();
                return dt;
            }
        }

        public static DataTable jsonUserTankTable = new DataTable();

        public static void GetJsonUserTankFromDB()
        {
            using (SqlConnection conn = new SqlConnection(Config.Settings.DatabaseConn))
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM jsonUserTank", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(jsonUserTankTable);
                conn.Close();
            }
        }

        #endregion

        #region LookupData

        // TODO: just for testing
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
            int tankID = 0;
            string expression = "name = '" + TankName + "'";
            DataRow[] foundRows = TankList.Select(expression);
            if (foundRows.Length > 0) // If tank exist in Tank table
                tankID = Convert.ToInt32(foundRows[0]["tankId"]);
            return tankID;
        }

        public static bool TankExist(int tankID)
        {
            string expression = "tankId = " + tankID.ToString();
            DataRow[] foundRows = TankList.Select(expression);
            return (foundRows.Length > 0);
        }

        #endregion

        #region Main

        public static void SaveTankDataResult(string tankName, DataRow NewUserTankRow, bool ForceUpdate = false)
        {
            // Get Tank ID
            int tankID = GetTankID(tankName);
            if (tankID > 0) // when tankid=0 the tank is not found in tank table
            {
                // Check if battle count has increased, first get existing battle count
                DataTable OldUserTankTable = tankData.GetUserTankDataFromDB(tankID); // Return Existing User Tank Data
                // Check if user has this tank
                if (OldUserTankTable.Rows.Count == 0)
                {
                    SaveNewUserTank(tankID);
                    OldUserTankTable = tankData.GetUserTankDataFromDB(tankID); // Return once more now after row is added
                }
                // Check if battle count has increased, first get existing (old) tank data
                DataRow OldUserTankRow = OldUserTankTable.Rows[0];
                // Compare with last battle result
                int NewUserTankRow_battles15 = 0;
                int NewUserTankRow_battles7 = 0;
                if (NewUserTankRow["battles15"] != DBNull.Value) NewUserTankRow_battles15 = Convert.ToInt32(NewUserTankRow["battles15"]);
                if (NewUserTankRow["battles7"] != DBNull.Value) NewUserTankRow_battles7 = Convert.ToInt32(NewUserTankRow["battles7"]);
                int battlessNew15 = NewUserTankRow_battles15 - Convert.ToInt32(OldUserTankRow["battles15"]);
                int battlessNew7 = NewUserTankRow_battles7 - Convert.ToInt32(OldUserTankRow["battles7"]);
                // Check if new battle on this tank
                if (battlessNew15 != 0 || battlessNew7 != 0 || ForceUpdate)
                {
                    // New battle detected, update tankData in DB
                    UpdateUserTank(NewUserTankRow, OldUserTankTable);
                }
            }
            
        }

        private static void SaveNewUserTank(int TankID)
        {
            // Add to database
            SqlConnection con = new SqlConnection(Config.Settings.DatabaseConn);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO userTank (tankId, wotUserId) VALUES (@tankId, @wotUserId)", con);
            cmd.Parameters.AddWithValue("@tankId", TankID);
            cmd.Parameters.AddWithValue("@wotUserId", Config.Settings.UserID);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private static void UpdateUserTank(DataRow NewUserTankRow, DataTable OldUserTankTable)
        {
            // Get fields to update
            string sqlFields = "";
            foreach (DataColumn column in OldUserTankTable.Columns)
            {
                if (column.ColumnName != "userTankId" && NewUserTankRow[column.ColumnName] != DBNull.Value) // avoid the PK
                {
                    if (sqlFields.Length > 0) sqlFields += ", "; // Add comma exept for first element
                    string colName = column.ColumnName;
                    string colType = column.DataType.Name;
                    sqlFields += colName + "=";
                    switch (colType)
	                {
		                case "String"   : sqlFields += "'" + NewUserTankRow[colName] + "'"; break;
                        case "DateTime" : sqlFields += ConvertFromUnixTimestamp(Convert.ToDouble(NewUserTankRow[colName])).ToString("yyyy-MM-dd HH:mm:ss"); break;
                        default         : sqlFields += NewUserTankRow[colName]; break;
	                }
                }
            }
            // Update database
            if (sqlFields.Length > 0)
            {
                SqlConnection con = new SqlConnection(Config.Settings.DatabaseConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE userTank SET " + sqlFields + " WHERE userTankId=@userTankId ", con);
                cmd.Parameters.AddWithValue("@userTankId", OldUserTankTable.Rows[0]["userTankId"]);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            
        }

        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }       

        #endregion

    }
}
