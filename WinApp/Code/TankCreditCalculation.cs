using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
    class TankCreditCalculation
    {
        public class TankCreditItem
        {
            public int playerTankId { get; set; }
            public double battleCount { get; set; }
            public double creditsIncome { get; set; }
            public double creditsNet { get; set; }
            public double maxcreditsIncome { get; set; }
            public double maxcreditsNet { get; set; }
            public string battleMode { get; set; }
            public double battleLifeTime { get; set; }
        }

        public static string CreateSQL(TankCreditItem tci)
        {
            string newSQL = "";
            // Create update SQL if any battlecount
            if (tci.battleCount > 0)
            {
                newSQL =
                    "UPDATE playerTankBattle SET " +
                    "  credBtlCount=@battleCount, " +
                    "  credAvgIncome=@credAvgIncome, " +
                    "  credAvgCost=@credAvgCost, " +
                    "  credAvgResult=@credAvgResult, " +
                    "  credMaxIncome=@credMaxIncome, " +
                    "  credMaxCost=@credMaxCost, " +
                    "  credMaxResult=@credMaxResult, " +
                    "  credTotIncome=@credTotIncome, " +
                    "  credTotCost=@credTotCost, " +
                    "  credTotResult=@credTotResult, " +
                    "  credBtlLifetime=@credBtlLifetime " +
                    "WHERE playerTankId=@playerTankId and battleMode=@battleMode;";
                DB.AddWithValue(ref newSQL, "@playerTankId", tci.playerTankId, DB.SqlDataType.Int);
                DB.AddWithValue(ref newSQL, "@battleMode", tci.battleMode, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref newSQL, "@battleCount", tci.battleCount, DB.SqlDataType.Int);
                DB.AddWithValue(ref newSQL, "@credAvgIncome", Convert.ToInt32(tci.creditsIncome / tci.battleCount), DB.SqlDataType.Int);
                DB.AddWithValue(ref newSQL, "@credAvgCost", Convert.ToInt32((tci.creditsIncome - tci.creditsNet) / tci.battleCount), DB.SqlDataType.Int);
                DB.AddWithValue(ref newSQL, "@credAvgResult", Convert.ToInt32(tci.creditsNet / tci.battleCount), DB.SqlDataType.Int);
                DB.AddWithValue(ref newSQL, "@credMaxIncome", Convert.ToInt32(tci.maxcreditsIncome), DB.SqlDataType.Int);
                DB.AddWithValue(ref newSQL, "@credMaxCost", Convert.ToInt32(tci.maxcreditsIncome - tci.maxcreditsNet), DB.SqlDataType.Int);
                DB.AddWithValue(ref newSQL, "@credMaxResult", Convert.ToInt32(tci.maxcreditsNet), DB.SqlDataType.Int);
                DB.AddWithValue(ref newSQL, "@credTotIncome", Convert.ToInt64(tci.creditsIncome), DB.SqlDataType.Int);
                DB.AddWithValue(ref newSQL, "@credTotCost", Convert.ToInt64(tci.creditsIncome - tci.creditsNet), DB.SqlDataType.Int);
                DB.AddWithValue(ref newSQL, "@credTotResult", Convert.ToInt64(tci.creditsNet), DB.SqlDataType.Int);
                DB.AddWithValue(ref newSQL, "@credBtlLifetime", Convert.ToInt32(tci.battleLifeTime), DB.SqlDataType.Int);
            }
            return newSQL;
        }

        public static void RecalculateForTank(int playerTankId)
        {
            // Get battles
            // Credits = total income
            // CreditsNet = total income - all cost pre calculated
            string sql =
                "select battle.battleMode as battleMode, " +
                "  sum(battle.battlesCount) as battlesCount, " +
                "  sum(credits) as credits, " +
                "  sum(creditsNet) as creditsNet, " +
                "  max(credits) as maxcredits, " +
                "  max(creditsNet) as maxcreditsNet, " +
                "  sum(battle.battleLifeTime) as battleLifeTime " +
                "from battle inner join playerTank on battle.playerTankId = playerTank.id inner join tank on playerTank.tankID = tank.Id " +
                "where credits is not null and creditsNet is not null and battle.playerTankId=@playerTankId " +
                "group by battle.battleMode ";
            DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
            DataTable dt = DB.FetchData(sql);
            string newSQL = "";
            foreach (DataRow dr in dt.Rows)
            {
                TankCreditCalculation.TankCreditItem tci = new TankCreditCalculation.TankCreditItem();
                tci.battleCount = Convert.ToDouble(dr["battlesCount"]);
                tci.battleMode = dr["battleMode"].ToString();
                // Create SQL
                if (tci.battleCount > 0)
                {
                    tci.playerTankId = playerTankId;
                    tci.creditsIncome = Convert.ToDouble(dr["credits"]);
                    tci.creditsNet = Convert.ToDouble(dr["creditsNet"]);
                    tci.maxcreditsIncome = Convert.ToDouble(dr["maxcredits"]);
                    tci.maxcreditsNet = Convert.ToDouble(dr["maxcreditsNet"]);
                    tci.battleLifeTime = Convert.ToDouble(dr["battleLifeTime"]);
                    newSQL += TankCreditCalculation.CreateSQL(tci);
                }
            }
            DB.ExecuteNonQuery(newSQL, Config.Settings.showDBErrors, true);
        }
    }
}
