using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater
{
    public class TankDataResult
    {
        public string tankName = "";
        public int battles15 = 0;
        public int battles7 = 0;

        public void Clear()
        {
            tankName = "";
            battles15 = 0;
            battles7 = 0;
        }
    }
    
    public static class tankData
    {
        #region DatabaseLookup

        public static DataTable Tank = new DataTable();

        public static void GetTanksFromDB()
        {
            using(SqlConnection conn = new SqlConnection(Config.Settings.DatabaseConn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT tankId, name FROM tank", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(Tank);
                conn.Close();
            }
        }

        public static DataTable UserTank = new DataTable();

        public static void GetUserTanksFromDB()
        {
            using(SqlConnection conn = new SqlConnection(Config.Settings.DatabaseConn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT tankId, battles15, battles7 FROM userTank WHERE wotUserId = " + Config.Settings.UserID, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(UserTank);
                conn.Close();
            }
        }

        #endregion

        #region LookupData

        // TODO: just for testing
        public static string listTanks()
        {
            string s = "";
            foreach (DataRow dr in Tank.Rows)
            {
                s += dr["tankId"] + " : " + dr["name"] + "\n";
            }
            return s;
        }

        public static string listUserTanks()
        {
            string s = "";
            foreach (DataRow dr in UserTank.Rows)
            {
                s += dr["tankId"] + "\n";
            }
            return s;
        }

        public static int GetTankID(string TankName)
        {
            int tankID = 0;
            string expression = "name = '" + TankName + "'";
            DataRow[] foundRows = Tank.Select(expression);
            if (foundRows.Length > 0) // If tank exist in Tank table
                tankID = Convert.ToInt32(foundRows[0]["tankId"]);
            
            //foreach (DataRow dr in foundRows)
            //{
            //    tankID = Convert.ToInt32(dr["tankId"]);
            //}
            
            return tankID;
        }

        public static bool HasUserTank(int tankID)
        {
            bool hasTank = false;
            string expression = "tankId = " + tankID.ToString();
            DataRow[] foundRows = UserTank.Select(expression);
            hasTank = (foundRows.Length > 0);
            return hasTank;
        }

        public static void GetUserTankBattelCount(out int battles15, out int battles7, int tankID)
        {
            string expression = "tankId = " + tankID.ToString();
            DataRow[] foundRows = UserTank.Select(expression);
            battles15 = Convert.ToInt32(foundRows[0]["battles15"]);
            battles7 = Convert.ToInt32(foundRows[0]["battles7"]);
        }

        #endregion

        #region Main

        private static void AddComma(ref string current, string add)
        {
            if (current.Length > 0) current += ", ";
            current += add;
        }

        public static void SaveTankDataResult(TankDataResult tdr)
        {
            // Get Tank ID
            int tankID = GetTankID(tdr.tankName);
            if (tankID > 0) // when tankid=0 the tank is not found in tank table
            {
                // Check if user has this tank
                if (!HasUserTank(tankID))
                {
                    SaveNewUserTank(tankID);
                }
                // Prepare SQL UPDATE
                string sqlFields = "";
                // Check if battle count has increased
                int battles15 = 0;
                int battles7 = 0;
                int battlessNew15 = 0;
                int battlessNew7 = 0;
                GetUserTankBattelCount(out battles15, out battles7, tankID);
                // 15x15 battles
                battlessNew15 = tdr.battles15 - battles15;
                if (battlessNew15 > 0)
                {
                    AddComma(ref sqlFields, "battles15 = " + tdr.battles15);
                }
                // 7x7 battles
                battlessNew7 = tdr.battles7 - battles7;
                if (battlessNew7 > 0)
                {
                    AddComma(ref sqlFields, "battles7 = " + tdr.battles7);
                }
                // Update now
                UpdateUserTank(sqlFields);
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
            // Update local variables
            GetUserTanksFromDB();
        }

        private static void UpdateUserTank(string sqlFields)
        {
            // Update database
            if (sqlFields.Length > 0 )
            {
                SqlConnection con = new SqlConnection(Config.Settings.DatabaseConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE userTank SET " + sqlFields + " WHERE wotUserId=@wotUserId", con);
                cmd.Parameters.AddWithValue("@wotUserId", Config.Settings.UserID);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        #endregion

    }
}
