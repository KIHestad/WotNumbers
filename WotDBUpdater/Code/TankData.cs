using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater
{
    public static class TankData
    {
        #region DatabaseLookup

        public static DataTable tankList = new DataTable();

        public static void GetTankListFromDB()
        {
            using(SqlConnection conn = new SqlConnection(Config.DatabaseConnection()))
            {
                tankList.Clear();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM tank", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(tankList);
                conn.Close();
            }
        }

        // public static DataTable PlayerTankList = new DataTable();

        public static DataTable GetPlayerTankFromDB(int tankId)
        {
            using (SqlConnection conn = new SqlConnection(Config.DatabaseConnection()))
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM playerTank WHERE playerId = " + Config.Settings.playerId + " AND tankId=" + tankId.ToString(), conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                conn.Close();
                return dt;
            }
        }

        public static DataTable GetBattleFromDB(int battleId)
        {
            using (SqlConnection conn = new SqlConnection(Config.DatabaseConnection()))
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM battle WHERE id=" + battleId.ToString(), conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                conn.Close();
                return dt;
            }
        }

        public static DataTable json2dbMappingView = new DataTable();
        
        public static void GetJson2dbMappingViewFromDB()
        {
            using (SqlConnection conn = new SqlConnection(Config.DatabaseConnection()))
            {
                json2dbMappingView.Clear();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM json2dbMappingView ORDER BY jsonMainSubProperty", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(json2dbMappingView);
                conn.Close();
            }
            json2dbMappingView.PrimaryKey = new DataColumn[] {json2dbMappingView.Columns["jsonMainSubProperty"]};
        }

        public static DataTable tankData2BattleMappingView = new DataTable();

        public static void GettankData2BattleMappingViewFromDB()
        {
            using (SqlConnection conn = new SqlConnection(Config.DatabaseConnection()))
            {
                tankData2BattleMappingView.Clear();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM tankData2BattleMappingView", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(tankData2BattleMappingView);
                conn.Close();
            }
        }

        #endregion

        #region LookupData

        // TODO: just for testing
        public static string ListTanks()
        {
            string s = "";
            foreach (DataRow dr in tankList.Rows)
            {
                s += dr["id"] + ":" + dr["name"] + ", ";
            }
            return s;
        }

        public static int GetTankID(string TankName, out int TankTier)
        {
            int tankID = 0;
            TankTier = 0;
            string expression = "name = '" + TankName + "'";
            DataRow[] foundRows = tankList.Select(expression);
            if (foundRows.Length > 0) // If tank exist in Tank table 
            {
                tankID = Convert.ToInt32(foundRows[0]["id"]);
                TankTier = Convert.ToInt32(foundRows[0]["tier"]);
            }
            return tankID;
        }

        public static bool TankExist(int tankID)
        {
            string expression = "id = " + tankID.ToString();
            DataRow[] foundRows = tankList.Select(expression);
            return (foundRows.Length > 0);
        }

        public static DataRow TankInfo(int tankID)
        {
            string expression = "id = " + tankID.ToString();
            DataRow[] foundRows = tankList.Select(expression);
            if (foundRows.Length > 0)
                return foundRows[0];
            else
                return null;
        }


        #endregion

       
    }
}
