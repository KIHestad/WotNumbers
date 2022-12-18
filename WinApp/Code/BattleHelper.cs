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
        public async static Task<int> GetTotalBattleRows()
        {
            int count = 0;
            string sql = "SELECT count(id) FROM battle";
            DataTable dt = await DB.FetchData(sql);
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                count = Convert.ToInt32(dt.Rows[0][0]);
            dt.Dispose();
            dt.Clear();
            return count;
        }

        public async static Task<int> GetTankBattleCount(int playerTankId, string battleTimeFilter)
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

        public async static Task<int> GetTankBattleVictoryCount(int playerTankId, string battleTimeFilter)
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

        public class PosOnTeamLeaderboard
        {
            public int? PosByXp { get; set; }
            public int? PosByDmg { get; set; }
        }

        public async static Task<PosOnTeamLeaderboard> GetPlayerPositionInTeamLeaderboard(int battleId)
        {
            PosOnTeamLeaderboard posOnTeamLeaderboard = new PosOnTeamLeaderboard();
            uint playerAccountId = Config.Settings.playerAccountId;
            // Get players team
            int playersTeam = 0;
            string sql = @"
                SELECT team
                FROM battlePlayer 
                WHERE battlePlayer.accountId=@accountId AND battleId=@battleId 
                ORDER BY battlePlayer.xp DESC, battlePlayer.damageDealt DESC";
            
            DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int); 
            DB.AddWithValue(ref sql, "@accountId", playerAccountId, DB.SqlDataType.Int);

            DataTable dt = await DB.FetchData(sql);
            if (dt == null || dt.Rows.Count == 0)
                return posOnTeamLeaderboard;
            playersTeam = Convert.ToInt32(dt.Rows[0]["team"]);
            // Get position pr xp
            sql = @"
                SELECT battlePlayer.accountId as teamPlayerAccountId, battlePlayer.xp as teamPlayerXp, battlePlayer.damageDealt as teamPlayerDmg
                FROM battlePlayer 
                WHERE battlePlayer.team = @playersTeam AND battleId=@battleId 
                ORDER BY battlePlayer.xp DESC, battlePlayer.damageDealt DESC";
            DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@playersTeam", playersTeam, DB.SqlDataType.Int);
            dt = await DB.FetchData(sql);
            if (dt.Rows.Count > 1)
            {
                int pos = 1;
                while (dt.Rows.Count > pos && Convert.ToUInt32(dt.Rows[pos -1]["teamPlayerAccountId"]) != Config.Settings.playerAccountId)
                    pos++;
                posOnTeamLeaderboard.PosByXp = pos;
            }
            // Get position pr dmg
            sql = @"
                SELECT battlePlayer.accountId as teamPlayerAccountId, battlePlayer.xp as teamPlayerXp, battlePlayer.damageDealt as teamPlayerDmg
                FROM battlePlayer 
                WHERE battlePlayer.team = @playersTeam AND battleId=@battleId 
                ORDER BY battlePlayer.damageDealt DESC, battlePlayer.xp DESC";
            DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@playersTeam", playersTeam, DB.SqlDataType.Int);
            dt = await DB.FetchData(sql);
            if (dt.Rows.Count > 1)
            {
                int pos = 1;
                while (dt.Rows.Count > pos && Convert.ToUInt32(dt.Rows[pos - 1]["teamPlayerAccountId"]) != Config.Settings.playerAccountId)
                    pos++;
                posOnTeamLeaderboard.PosByDmg = pos;
            }
            // done
            dt.Dispose();
            dt.Clear();
            return posOnTeamLeaderboard;
        }
    }
}
