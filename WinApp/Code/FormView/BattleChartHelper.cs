using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinApp.Code.FormView
{
    public class BattleChartHelper
    {
        // Result from Favourite save
        public static string SaveFavouriteNewFavName = "";
        public static int SaveFavouriteNewFavId = -1;
        public static bool SaveFavouriteSaved = false;
        public static bool SaveFavouriteDeleted = false;

        // Rsult form Removing chart values
        public static int RemovedChartValues = 0;

        #region Settings class = current settings for displaying chart

        public static BattleChartSettings Settings { get; set; }
        
        // Stores choices for values for charts at form load, and sets default values
        public class BattleChartSettings
        {
            public BattleChartSettings()
            {
                BattleMode = "15";
                BattleTime = "ALL";
                Xaxis = "Date";
                Bullet = false;
                Spline = false;
            }
            public string BattleMode { get; set; }
            public string BattleTime { get; set; }
            public string Xaxis { get; set; }
            public bool Bullet { get; set; }
            public bool Spline { get; set; }
            
        }

        #endregion

        #region Chart items

        public static List<BattleChartItem> NewChartItem { get; set; }
        public static List<BattleChartItem> CurrentChartView { get; set; }

        public class BattleChartItem
        {
            public BattleChartItem()
            {
                tankId = 0;
                chartTypeName = "";
                use2ndYaxis = false;
            }

            public int tankId { get; set; }
            public string tankName { get; set; }
            public string chartTypeName { get; set; }
            public bool use2ndYaxis { get; set; }
        }

        #endregion

        #region Chart Types = All available chart definitions for showing in menu and for calculations

        public enum CalculationType
        {
            standard = 0,
            firstInPercentageOfNext = 1,
            firstDividedOnNext = 5,
            eff = 2,
            wn7 = 3,
            wn8 = 4,
            wn9 = 6,
        }

        public class ChartTypeCols
        {
            public string playerTankValCol = "";    // Column holding current value from playerTankBattle table
            public string battleValCol = "";        // Column holding battle value
            public string battleFirstValCol = "";   // Column holding the first battle value, normally same as battleValCol - exept for some special calculations
        }

        public class ChartType
        {
            public string name = "";                                    // Name of Chart VAlue in dropdown 
            public CalculationType calcType = CalculationType.standard; // Calculation Type
            public bool totals = true;                                  // Show totals, not actual battle values
            public List<ChartTypeCols> col;                             // What columns to be used to calculate values
            public SeriesChartType seriesStyle = SeriesChartType.Line;    // Chart type, line = standard, spline er rounded, point = no line only dot
        }

        public static List<ChartType> GetChartTypeList()
        {
            List<ChartTypeCols> chartTypeColList;
            List<ChartType> chartTypeList = new List<ChartType>();

            // Win Rate
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartTypeList.Add(new ChartType() { name = "Win Rate", col = chartTypeColList, calcType = CalculationType.firstInPercentageOfNext });

            // Damage Rating
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "(damageRating / 100)", battleValCol = "(damageRatingTotal / 100)" });
            chartTypeList.Add(new ChartType() { name = "Dmg Rating", col = chartTypeColList, totals = false, calcType = CalculationType.standard });

            // WN9
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "spot", battleValCol = "spotted" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "frags", battleValCol = "frags" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "def", battleValCol = "def" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "cap", battleValCol = "cap" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "t.tier", battleFirstValCol = "t.tier", battleValCol = "tier" });
            chartTypeList.Add(new ChartType() { name = "WN9", col = chartTypeColList, calcType = CalculationType.wn9 });

            // WN9 per battle
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "wn9" });
            chartTypeList.Add(new ChartType() { name = "WN9 per battle", col = chartTypeColList, totals = false, seriesStyle = SeriesChartType.Point });


            // WN8
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "spot", battleValCol = "spotted" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "frags", battleValCol = "frags" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "def", battleValCol = "def" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "cap", battleValCol = "cap" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "t.tier * ptb.battles", battleFirstValCol = "t.tier * b.battlesCount", battleValCol = "tier" });
            chartTypeList.Add(new ChartType() { name = "WN8", col = chartTypeColList, calcType = CalculationType.wn8 });

            // WN8 per battle
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "wn8" });
            chartTypeList.Add(new ChartType() { name = "WN8 per battle", col = chartTypeColList, totals = false, seriesStyle = SeriesChartType.Point });

            // WN 7
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "spot", battleValCol = "spotted" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "frags", battleValCol = "frags" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "def", battleValCol = "def" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "cap", battleValCol = "cap" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "t.tier * ptb.battles", battleFirstValCol = "t.tier * b.battlesCount", battleValCol = "tier" });
            chartTypeList.Add(new ChartType() { name = "WN7", col = chartTypeColList, calcType = CalculationType.wn7 });

            // WN7 per battle
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "wn7" });
            chartTypeList.Add(new ChartType() { name = "WN7 per battle", col = chartTypeColList, totals = false, seriesStyle = SeriesChartType.Point });

            // Efficiency
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "spot", battleValCol = "spotted" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "frags", battleValCol = "frags" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "def", battleValCol = "def" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "cap", battleValCol = "cap" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "t.tier * ptb.battles", battleFirstValCol = "t.tier * b.battlesCount", battleValCol = "tier" });
            chartTypeList.Add(new ChartType() { name = "EFF", col = chartTypeColList, calcType = CalculationType.eff });

            // Efficiency per battle
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "eff" });
            chartTypeList.Add(new ChartType() { name = "EFF per battle", col = chartTypeColList, totals = false, seriesStyle = SeriesChartType.Point });

            // XP
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "xp" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartTypeList.Add(new ChartType() { name = "XP Average", col = chartTypeColList, calcType = CalculationType.firstDividedOnNext });

            // XP
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "xp" });
            chartTypeList.Add(new ChartType() { name = "XP Total", col = chartTypeColList });

            // Average damage
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "dmg" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartTypeList.Add(new ChartType() { name = "Damage Average", col = chartTypeColList, calcType = CalculationType.firstDividedOnNext });

            // Damage
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "dmg" });
            chartTypeList.Add(new ChartType() { name = "Damage Total", col = chartTypeColList });

            // Frag Average
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "frags" });
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartTypeList.Add(new ChartType() { name = "Frags Average", col = chartTypeColList, calcType = CalculationType.firstDividedOnNext });

            // Frag Count
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "frags" });
            chartTypeList.Add(new ChartType() { name = "Frags Total", col = chartTypeColList });

            // Battle count
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" });
            chartTypeList.Add(new ChartType() { name = "Battle Count", col = chartTypeColList });

            // Victory Count
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" });
            chartTypeList.Add(new ChartType() { name = "Victory Count", col = chartTypeColList });

            // Draw Count
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "(battles - wins - losses)", battleValCol = "draw" });
            chartTypeList.Add(new ChartType() { name = "Draw Count", col = chartTypeColList });

            // Defeat Count
            chartTypeColList = new List<ChartTypeCols>();
            chartTypeColList.Add(new ChartTypeCols() { playerTankValCol = "losses", battleValCol = "defeat" });
            chartTypeList.Add(new ChartType() { name = "Defeat Count", col = chartTypeColList });

            // Done
            return chartTypeList;
        }

        #endregion
    }
}
