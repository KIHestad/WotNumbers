using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
    public class BattleCountFilterHelper
    {
        public static DataTable Get()
        {
            string sql = "SELECT id, count FROM battleFilterCount ORDER BY count";
            return DB.FetchData(sql);
        }

        public static DataRow Get(string id)
        {
            string sql = "SELECT id, count FROM battleFilterCount WHERE id=@id ORDER BY count";
            DB.AddWithValue(ref sql, "@id", id, DB.SqlDataType.Int);
            return DB.FetchData(sql).Rows[0];
        }

        public static void Save(string id, int newCount)
        {
            string sql = "UPDATE battleFilterCount SET count = @count WHERE id = @id";
            DB.AddWithValue(ref sql, "@id", id, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@count", newCount, DB.SqlDataType.Int);
            DB.ExecuteNonQuery(sql);
        }

        public static void RecalcalculateCountTotal()
        {
            string sql = "SELECT id FROM player";
            DataTable dtPlayer = DB.FetchData(sql);
            foreach (DataRow drPlayer in dtPlayer.Rows)
            {
                sql =
                    "SELECT battle.id, battlesCount " +
                    "FROM Battle INNER JOIN playerTank ON Battle.playerTankId = playerTank.Id " +
                    "WHERE playerTank.playerId = " + drPlayer["id"].ToString() + " " +
                    "ORDER BY battle.battleTime";
                DataTable dtBattle = DB.FetchData(sql);
                string sqlUpdate = "";
                int battlesCountTotal = 0;
                foreach (DataRow drBattle in dtBattle.Rows)
                {
                    battlesCountTotal += Convert.ToInt32(drBattle["battlesCount"]);
                    sqlUpdate += "UPDATE battle SET battlesCountTotal=" + battlesCountTotal.ToString() + " WHERE id=" + drBattle["id"] + ";";
                }
                DB.ExecuteNonQuery(sqlUpdate, false, true);
            }
        }

        public static int GetBattlesCountTotal()
        {
            string sql =
               "SELECT MAX(battlescounttotal) " +
               "FROM battle INNER JOIN playerTank ON Battle.playerTankId = playerTank.Id " +
               "WHERE playerTank.playerId = " + Config.Settings.playerId;
            DataTable dt = DB.FetchData(sql);
            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0][0]);
            else
                return 0;

        }

        public static int GetBattlesCountTotalMin(string id)
        {
            DataRow dr = Get(id);
            int battlesCountToShow = Convert.ToInt32(dr["count"]);
            int battlesTotalCount = GetBattlesCountTotal();
            return battlesTotalCount - battlesCountToShow;
        }

    }
}
