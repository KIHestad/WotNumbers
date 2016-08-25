using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinApp.Code;

namespace WinApp.Code.Rating
{
	public class WNHelper
    {
        #region parameters

        public class RatingParameters
        {
            public RatingParameters()
            {
                BATTLES = 0;
                DAMAGE = 0;
                SPOT = 0;
                FRAGS = 0;
                DEF = 0;
                CAP = 0;
                WINS = 0;
                TIER = 0;
            }

            public RatingParameters(RatingParameters clone) // To be able to clone
            {
                BATTLES = clone.BATTLES;
                DAMAGE = clone.DAMAGE;
                SPOT = clone.SPOT;
                FRAGS = clone.FRAGS;
                DEF = clone.DEF;
                CAP = clone.CAP;
                WINS = clone.WINS;
                TIER = clone.TIER;
            }
            public double BATTLES {get; set;}
            public double DAMAGE {get; set;}
            public double SPOT {get; set;}
            public double FRAGS {get; set;}
            public double DEF {get; set;}
            public double CAP {get; set;}
            public double WINS {get; set;}
            public double TIER {get; set;}
        }

        public static RatingParameters GetParamForPlayerTankBattle(string battleMode, bool excludeIfWN8ExpDmgIsNull = false, bool excludeIfWN9ExpValIsNull = false, int playerTankId = 0)
        {
            RatingParameters rp = new RatingParameters();
            string battleModeWhere = "";
            string excludeIfWN8ExpDmgIsNullWhere = "";
            string excludeIfWN9ExpValIsNullWhere = "";
            string playerTankIdWhere = "";
            if (battleMode != "")
            {
                battleModeWhere = " and ptb.battleMode=@battleMode ";
                DB.AddWithValue(ref battleModeWhere, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            }
            if (excludeIfWN8ExpDmgIsNull)
            {
                excludeIfWN8ExpDmgIsNullWhere = " and t.expDmg is not null ";
            }
            if (excludeIfWN9ExpValIsNull)
            {
                excludeIfWN9ExpValIsNullWhere = " and t.wn9exp is not null ";
            }
            if (playerTankId > 0)
            {
                playerTankIdWhere = " and pt.id = " + playerTankId.ToString() + " ";
            }
            string sql =
                "select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
                "  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " +
                "from playerTankBattle ptb left join " +
                "  playerTank pt on ptb.playerTankId=pt.id left join " +
                "  tank t on pt.tankId = t.id " +
                "where pt.playerId=@playerId " + battleModeWhere + excludeIfWN8ExpDmgIsNullWhere + excludeIfWN9ExpValIsNullWhere + playerTankIdWhere;
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DataTable playerTotalsTable = DB.FetchData(sql);
            DataTable dt = DB.FetchData(sql);
            if (dt.Rows.Count == 0)
                return null;
            DataRow stats = dt.Rows[0];
            rp.BATTLES = WNHelper.ConvertDbVal2Double(stats["battles"]);
            rp.DAMAGE = WNHelper.ConvertDbVal2Double(stats["dmg"]);
            rp.SPOT = WNHelper.ConvertDbVal2Double(stats["spot"]);
            rp.FRAGS = WNHelper.ConvertDbVal2Double(stats["frags"]);
            rp.DEF = WNHelper.ConvertDbVal2Double(stats["def"]);
            rp.CAP = WNHelper.ConvertDbVal2Double(stats["cap"]);
            rp.WINS = WNHelper.ConvertDbVal2Double(stats["wins"]);
            rp.TIER = WNHelper.ConvertDbVal2Double(stats["tier"]);
            return rp;
        }

        #endregion

        #region get data to datatable for use to get wn params

        public static DataTable GetTotalTankStatsForPlayerTank(string battleMode = "15", string tankJoin = "", int tankID = 0)
        {
            if (battleMode == "")
                battleMode = "%";
            string tankIdWhere = "";
            if (tankID > 0)
                tankIdWhere = " and tank.id = " + tankID.ToString() + " ";
            // Create an datatable with all tanks and total stats
            string sql =
                "select tank.id as tankId, SUM(battles) as battles, SUM(dmg) as dmg, SUM(spot) as spot, SUM(frags) as frags, " +
                "  SUM(def) as def, SUM(cap) as cap, SUM(wins) as wins " +
                "from playerTankBattle ptb left join " +
                "  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId and ptb.battleMode like @battleMode left join " +
                "  tank on pt.tankId = tank.id " +
                tankJoin + " " +
                "where tank.expDmg is not null and ptb.battleMode like @battleMode " + tankIdWhere +
                "group by tank.id ";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            return DB.FetchData(sql);
        }

        public static DataTable GetDataForPlayerTankBattleReverse(string battleTimeFilter, int battleCount = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            // Get datatable with all tanks and total stats
            DataTable ptb = GetTotalTankStatsForPlayerTank(battleMode, tankJoin);
            // Get all battles and subtract from totals
            string sql =
                "select battlesCount as battles, dmg, spotted as spot, frags, " +
                "  def, cap, tank.tier as tier , victory as wins, tank.id as tankId " +
                "from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
                "  tank on playerTank.tankId = tank.id " +
                "where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " " + battleModeFilter + " order by battleTime DESC";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            DataTable dtBattles = DB.FetchData(sql);
            if (dtBattles.Rows.Count > 0)
            {
                if (battleCount == 0) battleCount = dtBattles.Rows.Count;
                int count = 0;
                foreach (DataRow stats in dtBattles.Rows)
                {
                    double btl = WNHelper.ConvertDbVal2Double(stats["battles"]);
                    // add to datatable
                    string tankId = stats["tankId"].ToString();
                    DataRow[] ptbRow = ptb.Select("tankId = " + tankId);
                    if (ptbRow.Length > 0)
                    {
                        ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) - Convert.ToInt32(stats["battles"]);
                        ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) - Convert.ToInt32(stats["dmg"]) * btl;
                        ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) - Convert.ToInt32(stats["spot"]) * btl;
                        ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) - Convert.ToInt32(stats["frags"]) * btl;
                        ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) - Convert.ToInt32(stats["def"]) * btl;
                        ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) - Convert.ToInt32(stats["cap"]) * btl;
                        ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) - Convert.ToInt32(stats["wins"]);
                    }
                    else
                    {
                        if (Config.Settings.showDBErrors)
                            Log.LogToFile("*** Could not find playerTank for battle mode '" + battleMode + "' for tank: " + tankId + " ***");
                    }
                    count++;
                    if (count > battleCount) break;
                }
                // Check for null values
            }
            return ptb;
        }

        
        public static DataTable GetDataForBattleRange(string battleTimeFilter, int maxBattles = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            if (battleMode == "")
                battleMode = "%";
            // Create an empty datatable with all tanks, no values
            string sql =
                "select tank.id as tankId, 0 as battles, 0 as dmg, 0 as spot, 0 as frags, " +
                "  0 as def, 0 as cap, 0 as wins " +
                "from playerTankBattle ptb left join " +
                "  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId and ptb.battleMode like @battleMode left join " +
                "  tank on pt.tankId = tank.id " +
                tankJoin + " " +
                "where tank.expDmg is not null and ptb.battleMode like @battleMode " + // TODO: check if ok - removed this when introducing WN9: tank.expDmg is not null and 
                "group by tank.id ";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);

            DataTable ptb = DB.FetchData(sql);
            // Get all battles
            
            // OLD - had to loop through all battles, instead of per tank - also removed multiplier on battles for each stat
            //sql =
            //    "select battlesCount as battles, dmg, spotted as spot, frags, " +
            //    "  def, cap, tank.tier as tier , victory as wins, tank.id as tankId " +
            //    "from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
            //    "  tank on playerTank.tankId = tank.id " +
            //    "where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " " + battleModeFilter + " order by battleTime DESC";

            sql =
                "select SUM(battlesCount) as battles, SUM(dmg * battlesCount) as dmg, SUM(spotted * battlesCount) as spot, SUM(frags * battlesCount) as frags, " +
                "  SUM(def * battlesCount) as def, SUM(cap * battlesCount) as cap, SUM(victory * battlesCount) as wins, tank.id as tankId " +
                "from battle INNER JOIN playerTank ON battle.playerTankId = playerTank.Id left join   tank on playerTank.tankId = tank.id " +
                "where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " " + battleModeFilter + " " +
                "group by tank.id";


            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            DataTable dtBattles = DB.FetchData(sql);
            if (dtBattles.Rows.Count > 0)
            {
                int countBattles = 0;
                foreach (DataRow stats in dtBattles.Rows)
                {
                    int btl = Convert.ToInt32(stats["battles"]);
                    // add to datatable
                    string tankId = stats["tankId"].ToString();
                    DataRow[] ptbRow = ptb.Select("tankId = " + tankId);
                    if (ptbRow.Length > 0)
                    {
                        ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) + btl;
                        ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) + Convert.ToInt32(stats["dmg"]);
                        ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) + Convert.ToInt32(stats["spot"]);
                        ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) + Convert.ToInt32(stats["frags"]);
                        ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) + Convert.ToInt32(stats["def"]);
                        ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) + Convert.ToInt32(stats["cap"]);
                        ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) + Convert.ToInt32(stats["wins"]);
                    }
                    else
                    {
                        if (Config.Settings.showDBErrors)
                            Log.LogToFile("*** Could not find playerTank for battle mode '" + battleMode + "' for tank: " + tankId + " ***");
                    }
                    countBattles++;
                    if (maxBattles > 0 && countBattles > maxBattles) break;
                }
                // Return playertanks with stats
                return ptb.Select("battles > 0").CopyToDataTable();
            }
            else
            {
                // No battles returns no data
                return new DataTable();
            }
            
        }

        #endregion

		public static double GetAverageTier(string battleMode = "")
        {
            double tier = 0;
            // Get average battle tier, used for total player WN7 and battle WN7
            string battleModeWhere = "";
            if (battleMode != "")
            {
                battleModeWhere += " and ptb.battleMode=@battleMode ";
                DB.AddWithValue(ref battleModeWhere, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            }
            string sql =
                "select sum(ptb.battles) as battles, sum(t.tier * ptb.battles) as tier " +
                "from playerTankBattle ptb left join " +
                "  playerTank pt on ptb.playerTankId=pt.id left join " +
                "  tank t on pt.tankId = t.id " +
                "where pt.playerId=@playerId " + battleModeWhere;
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DataTable dt = DB.FetchData(sql);
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                double totalBattles = Convert.ToDouble(dt.Rows[0][0]);
                if (totalBattles > 0)
                {
                    tier = Convert.ToDouble(dt.Rows[0][1]) / totalBattles;
                }
            }
            return tier;
        }

        public static double ConvertDbVal2Double(object dbValue)
        {
            double value = 0;
            if (dbValue != DBNull.Value)
            {
                value = Convert.ToDouble(dbValue);
            }
            return value;
        }

        public static void RecalculateRatingForTank(int playerTankId, string battleMode)
        {
            // Update playerTankBattle
            string sqlFields = "";
            // Get rating parameters
            RatingParameters rp = GetParamForPlayerTankBattle(battleMode, playerTankId: playerTankId);
            int tankId = TankHelper.GetTankID(playerTankId);
            // Calculate WN9
            double WN9maxhist = 0;
            sqlFields += " wn9=" + Math.Round(WN9.CalcTank(tankId, rp, out WN9maxhist), 0).ToString();
            sqlFields += ", wn9maxhist=" + Math.Round(WN9maxhist, 0).ToString();
            // Calculate WN8
            sqlFields += ", wn8=" + Math.Round(WN8.CalcTank(tankId, rp), 0).ToString();
            // Calculate Eff
            sqlFields += ", eff=" + Math.Round(EFF.EffTank(tankId, rp), 0).ToString();
            // Calculate WN7 - use special tier
            rp.TIER = TankHelper.GetTankTier(tankId);
            sqlFields += ", wn7=" + Math.Round(WN7.WN7tank(rp), 0).ToString();
            // Calculate RWR
            sqlFields += ", rwr=" + RWR.RWRtank(tankId, rp);
            // Save
            string sql = "UPDATE playerTankBattle SET " + sqlFields + " WHERE PlayerTankId = " + playerTankId.ToString() + " AND battleMode = '" + battleMode + "'";
            DB.ExecuteNonQuery(sql);
        }


    }
}
