using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	public class BattleHelper
	{
		public async static Task<int> GetBattleCount(int playerTankId, string battleTimeFilter)
        {
            int count = 0;
            string sql = "SELECT sum(battlesCount) FROM battle WHERE playerTankId=@playerTankId " + battleTimeFilter;
            DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
            DataTable dt = await DB.FetchData(sql);
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                count = Convert.ToInt32(dt.Rows[0][0]);
            dt.Dispose();
            dt.Clear();
            return count;
        }

        public async static Task<int> GetBattleVictoryCount(int playerTankId, string battleTimeFilter)
        {
            int count = 0;
            string sql = "SELECT sum(victory) FROM battle WHERE playerTankId=@playerTankId " + battleTimeFilter;
            DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
            DataTable dt = await DB.FetchData(sql);
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                count = Convert.ToInt32(dt.Rows[0][0]);
            dt.Dispose();
            dt.Clear();
            return count;
        }
	}
}
