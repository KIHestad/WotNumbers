using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
    public class BattleCountFilterHelper
    {

        #region battle count filter menu

        public async static Task<DataTable> Get()
        {
            string sql = "SELECT id, count FROM battleFilterCount ORDER BY count";
            return await DB.FetchData(sql);
        }

        public async static Task<DataRow> Get(string id)
        {
            string sql = "SELECT id, count FROM battleFilterCount WHERE id=@id ORDER BY count";
            DB.AddWithValue(ref sql, "@id", id, DB.SqlDataType.Int);
            DataTable dt = await DB.FetchData(sql);
            return dt.Rows[0];
        }

        public async static Task<int> GetBattleLimitFromid(string id)
        {
            DataRow dr = await Get(id);
            return Convert.ToInt32(dr["count"]);
        }

        public async static Task Save(string id, int newCount)
        {
            string sql = "UPDATE battleFilterCount SET count = @count WHERE id = @id";
            DB.AddWithValue(ref sql, "@id", id, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@count", newCount, DB.SqlDataType.Int);
            await DB.ExecuteNonQuery(sql);
        }

        #endregion

        #region mark battles countable in battle count filter
        public async static Task SetBattleFilter(string from, string where, int battleCountFilter)
        {
            // reset battles
            string sql = "UPDATE battle SET battlesCountTotal = NULL; ";
            await DB.ExecuteNonQuery(sql);
            // get battles from other filters
            sql = "SELECT battle.id, battle.battlesCount " + from + where + " ORDER BY battle.battleTime DESC";
            DataTable dtBattle = await DB.FetchData(sql);
            string sqlUpdate = "";
            int rowCount = 0;
            int rowTotal = dtBattle.Rows.Count;
            int battlesCount = 0;
            while (rowCount < rowTotal && battlesCount < battleCountFilter)
            {
                battlesCount += Convert.ToInt32(dtBattle.Rows[rowCount]["battlesCount"]);
                sqlUpdate += "UPDATE battle SET battlesCountTotal=1 WHERE id=" + dtBattle.Rows[rowCount]["id"] + ";";
                rowCount++;
            }
            await DB.ExecuteNonQuery(sqlUpdate, false, true);
        }
        #endregion
        
    }
}
