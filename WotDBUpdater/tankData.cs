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
        public int used15 = 0;
        public int used7 = 0;

        public void Clear()
        {
            tankName = "";
            used15 = 0;
            used7 = 0;
        }
    }
    
    public static class tankData
    {
        public static DataSet Tanks = new DataSet();

        public static void GetTanks()
        {
            using(SqlConnection conn = new SqlConnection(Config.Settings.DatabaseConn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT tankId, name FROM tank", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(Tanks);
                conn.Close();
            }
        }

        public static DataSet UserTanks = new DataSet();

        public static void GetUserTanks()
        {
            using(SqlConnection conn = new SqlConnection(Config.Settings.DatabaseConn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT userTankId, battles15, battles7 FROM userTank", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(UserTanks);
                conn.Close();
            }
        }

    }
}
