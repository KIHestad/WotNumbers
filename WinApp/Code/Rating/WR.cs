using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code.Rating
{
    public class WR
    {
        public async static Task<double> WinrateBattle(string battleTimeFilter, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            // Calculate winrate for spesified battles
            double WR = 0;
            if (battleMode == "")
                battleMode = "%";
            string sql =
                "select battlesCount as battles, victory as wins " +
                "from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
                "  tank on playerTank.tankId = tank.id " +
                tankJoin + " " +
                "where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " " + battleModeFilter + " order by battleTime DESC";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            DataTable dtBattles = await DB.FetchData(sql);
            if (dtBattles.Rows.Count > 0)
            {
                double BATTLES = 0;
                double WINS = 0;
                foreach (DataRow stats in dtBattles.Rows)
                {
                    BATTLES += WNHelper.ConvertDbVal2Double(stats["battles"]);
                    WINS += WNHelper.ConvertDbVal2Double(stats["wins"]);
                }
                if (BATTLES > 0)
                {
                    WR = Math.Round(WINS / BATTLES * 100, 2);
                }
            }
            return WR;
        }

        public async static Task<double> WinrateTank(string battleTimeFilter, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            // calculate average winrate for all tanks included in filter
            double WR = 0;
            if (battleMode == "")
                battleMode = "%";
            string sql =
                "select battles, wins " +
                "from playerTankBattle " +
                "where playerTankId IN " +
                "  (select distinct playerTank.id " +
                "  from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
                "    tank on playerTank.tankId = tank.id " +
                "  " + tankJoin + " " +
                "  where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " " + battleModeFilter + ")";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            DataTable dtBattles = await DB.FetchData(sql);
            if (dtBattles.Rows.Count > 0)
            {
                double BATTLES = 0;
                double WINS = 0;
                foreach (DataRow stats in dtBattles.Rows)
                {
                    BATTLES += WNHelper.ConvertDbVal2Double(stats["battles"]);
                    WINS += WNHelper.ConvertDbVal2Double(stats["wins"]);
                }
                if (BATTLES > 0)
                {
                    WR = Math.Round(WINS / BATTLES * 100, 2);
                }
            }
            return WR;
        }
    }
}
