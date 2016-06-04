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

        public class RatingParametersWN
        {
            public RatingParametersWN()
            {
                rp = new RatingParameters();
                expDmg = 0;
                expSpot = 0;
                expFrag = 0;
                expDef = 0;
                expWinRate = 0;
            }
            public RatingParameters rp {get; set;}
            public double expWinRate {get; set;}
            public double expDmg {get; set;}
            public double expFrag { get; set; }
            public double expSpot { get; set; }
            public double expDef {get; set;}
            
        }

        #endregion

        #region Get rating parametes

        public static RatingParameters GetParamForPlayerTankBattle(string battleMode, bool excludeIfExpDmgIsNull = false)
        {
            RatingParameters rp = new RatingParameters();
            string battleModeWhere = "";
            string excludeIfExpDmgIsNullWhere = "";
            if (battleMode != "")
            {
                battleModeWhere = " and ptb.battleMode=@battleMode ";
                DB.AddWithValue(ref battleModeWhere, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            }
            if (excludeIfExpDmgIsNull)
            {
                excludeIfExpDmgIsNullWhere = "and t.expDmg is not null ";
            }
            string sql =
                "select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
                "  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " + 
                "from playerTankBattle ptb left join " +
                "  playerTank pt on ptb.playerTankId=pt.id left join " +
                "  tank t on pt.tankId = t.id " +
                "where pt.playerId=@playerId " + battleModeWhere + excludeIfExpDmgIsNullWhere;
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

        public static RatingParametersWN GetParamForPlayerTankBattle(DataTable playerTankBattle)
        {
            RatingParametersWN rpWN = null;
            // Get player totals from datatable
            if (playerTankBattle.Rows.Count > 0)
            {
                // Get player totals
                rpWN = new WNHelper.RatingParametersWN();
                rpWN.rp.BATTLES = Convert.ToDouble(playerTankBattle.Compute("SUM([battles])", ""));
                rpWN.rp.DAMAGE = Convert.ToDouble(playerTankBattle.Compute("SUM([dmg])", ""));
                rpWN.rp.SPOT = Convert.ToDouble(playerTankBattle.Compute("SUM([spot])", ""));
                rpWN.rp.FRAGS = Convert.ToDouble(playerTankBattle.Compute("SUM([frags])", ""));
                rpWN.rp.DEF = Convert.ToDouble(playerTankBattle.Compute("SUM([def])", ""));
                rpWN.rp.WINS = Convert.ToDouble(playerTankBattle.Compute("SUM([wins])", ""));
                // Get tanks with battle count per tank and expected values from db
                foreach (DataRow ptbRow in playerTankBattle.Rows)
                {
                    // Get tanks with battle count per tank and expected values
                    int tankId = Convert.ToInt32(ptbRow["tankId"]);
                    double battlecount = Convert.ToDouble(ptbRow["battles"]);
                    DataRow expected = TankHelper.TankInfo(tankId);
                    if (battlecount > 0 && expected != null && expected["expDmg"] != DBNull.Value)
                    {
                        rpWN.expDmg += Convert.ToDouble(expected["expDmg"]) * battlecount;
                        rpWN.expSpot += Convert.ToDouble(expected["expSpot"]) * battlecount;
                        rpWN.expFrag += Convert.ToDouble(expected["expFrags"]) * battlecount;
                        rpWN.expDef += Convert.ToDouble(expected["expDef"]) * battlecount;
                        rpWN.expWinRate += Convert.ToDouble(expected["expWR"]) * battlecount;
                    }
                }
            }
            return rpWN;
        }

        public static RatingParametersWN GetParamForPlayerTotal(string battleMode)
        {
            WNHelper.RatingParametersWN rpWN = new WNHelper.RatingParametersWN();
            // Get player totals from db
            rpWN.rp = WNHelper.GetParamForPlayerTankBattle(battleMode, true);
            if (rpWN.rp == null)
                return null;
            // Get tanks with battle count per tank and expected values from db
            string battleModeWhere = "";
            if (battleMode != "")
            {
                battleModeWhere = " and ptb.battleMode=@battleMode ";
                DB.AddWithValue(ref battleModeWhere, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            }
            string sql =
                "select sum(ptb.battles) as battles, t.id, t.expDmg, t.expSpot, t.expFrags, t.expDef, t.expWR " +
                "from playerTankBattle ptb left join " +
                "  playerTank pt on ptb.playerTankId=pt.id left join " +
                "  tank t on pt.tankId = t.id " +
                "where t.expDmg is not null and pt.playerId=@playerId " + battleModeWhere +
                "group by t.id, t.expDmg, t.expSpot, t.expFrags, t.expDef, t.expWR  ";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DataTable expectedTable = DB.FetchData(sql);
            foreach (DataRow expected in expectedTable.Rows)
            {
                // Get tanks with battle count per tank and expected values
                double battlecount = Convert.ToDouble(expected["battles"]);
                if (battlecount > 0 && expected["expDmg"] != DBNull.Value)
                {
                    rpWN.expDmg += Convert.ToDouble(expected["expDmg"]) * battlecount;
                    rpWN.expSpot += Convert.ToDouble(expected["expSpot"]) * battlecount;
                    rpWN.expFrag += Convert.ToDouble(expected["expFrags"]) * battlecount;
                    rpWN.expDef += Convert.ToDouble(expected["expDef"]) * battlecount;
                    rpWN.expWinRate += Convert.ToDouble(expected["expWR"]) * battlecount;
                }
            }
            return rpWN;
        }

        #endregion

        #region get data to datatable for use to get wn params

        public static DataTable GetDataForPlayerTankBattleReverse(string battleTimeFilter, int battleCount = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            if (battleMode == "")
                battleMode = "%";
            // Create an datatable with all tanks and total stats
            string sql =
                "select tank.id as tankId, SUM(battles) as battles, SUM(dmg) as dmg, SUM(spot) as spot, SUM(frags) as frags, " +
                "  SUM(def) as def, SUM(cap) as cap, SUM(wins) as wins " +
                "from playerTankBattle ptb left join " +
                "  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId and ptb.battleMode like @battleMode left join " +
                "  tank on pt.tankId = tank.id " +
                tankJoin + " " +
                "where tank.expDmg is not null and ptb.battleMode like @battleMode " +
                "group by tank.id ";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);

            DataTable ptb = DB.FetchData(sql);
            // Get all battles and subtract from totals
            sql =
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

        
        public static DataTable GetDataForBattle(string battleTimeFilter, int maxBattles = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
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
                "where tank.expDmg is not null and ptb.battleMode like @battleMode " +
                "group by tank.id ";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);

            DataTable ptb = DB.FetchData(sql);
            // Get all battles
            sql =
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
                        ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) + Convert.ToInt32(stats["dmg"]) * btl;
                        ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) + Convert.ToInt32(stats["spot"]) * btl;
                        ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) + Convert.ToInt32(stats["frags"]) * btl;
                        ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) + Convert.ToInt32(stats["def"]) * btl;
                        ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) + Convert.ToInt32(stats["cap"]) * btl;
                        ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) + Convert.ToInt32(stats["wins"]) * btl;
                    }
                    else
                    {
                        if (Config.Settings.showDBErrors)
                            Log.LogToFile("*** Could not find playerTank for battle mode '" + battleMode + "' for tank: " + tankId + " ***");
                    }
                    countBattles++;
                    if (maxBattles > 0 && countBattles > maxBattles) break;
                }
            }
            return ptb;
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

	}
}
