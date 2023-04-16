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
                TankId = 0;
                ChartTypeName = "";
                Use2ndYaxis = false;
            }

            public int TankId { get; set; }
            public string TankName { get; set; }
            public string ChartTypeName { get; set; }
            public bool Use2ndYaxis { get; set; }
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
                tierTotal = 7,
                tierInterval = 8,
                EMAiN100 = 9,
                EMAiN10 = 10,
                EMAiCombinedDmg = 11
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
            public SeriesChartType seriesStyle = SeriesChartType.Line;  // Chart type, line = standard, spline er rounded, point = no line only dot
        }

        public static List<ChartType> GetChartTypeList()
        {
            List<ChartTypeCols> chartTypeColList;
            List<ChartType> chartTypeList = new List<ChartType>();

            // Win Rate
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" },
                new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" }
            };
            chartTypeList.Add(new ChartType() { name = "Win Rate", col = chartTypeColList, calcType = CalculationType.firstInPercentageOfNext });

            // Damage Rating
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "(damageRating / 100)", battleValCol = "(damageRatingTotal / 100)" }
            };
            chartTypeList.Add(new ChartType() { name = "Dmg Rating", col = chartTypeColList, totals = false, calcType = CalculationType.standard });

            // WN9
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" },
                new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" },
                new ChartTypeCols() { playerTankValCol = "spot", battleValCol = "spotted" },
                new ChartTypeCols() { playerTankValCol = "frags", battleValCol = "frags" },
                new ChartTypeCols() { playerTankValCol = "def", battleValCol = "def" },
                new ChartTypeCols() { playerTankValCol = "cap", battleValCol = "cap" },
                new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" },
                new ChartTypeCols() { playerTankValCol = "t.tier", battleFirstValCol = "t.tier", battleValCol = "tier" }
            };
            chartTypeList.Add(new ChartType() { name = "WN9", col = chartTypeColList, calcType = CalculationType.wn9 });

            // WN9 per battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "wn9" }
            };
            chartTypeList.Add(new ChartType() { name = "WN9 per battle", col = chartTypeColList, totals = false});


            // WN8
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" },
                new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" },
                new ChartTypeCols() { playerTankValCol = "spot", battleValCol = "spotted" },
                new ChartTypeCols() { playerTankValCol = "frags", battleValCol = "frags" },
                new ChartTypeCols() { playerTankValCol = "def", battleValCol = "def" },
                new ChartTypeCols() { playerTankValCol = "cap", battleValCol = "cap" },
                new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" },
                new ChartTypeCols() { playerTankValCol = "t.tier * ptb.battles", battleFirstValCol = "t.tier * b.battlesCount", battleValCol = "tier" }
            };
            chartTypeList.Add(new ChartType() { name = "WN8", col = chartTypeColList, calcType = CalculationType.wn8 });

            // WN8 per battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "wn8" }
            };
            chartTypeList.Add(new ChartType() { name = "WN8 per battle", col = chartTypeColList, totals = false});

            // WN 7
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" },
                new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" },
                new ChartTypeCols() { playerTankValCol = "spot", battleValCol = "spotted" },
                new ChartTypeCols() { playerTankValCol = "frags", battleValCol = "frags" },
                new ChartTypeCols() { playerTankValCol = "def", battleValCol = "def" },
                new ChartTypeCols() { playerTankValCol = "cap", battleValCol = "cap" },
                new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" },
                new ChartTypeCols() { playerTankValCol = "t.tier * ptb.battles", battleFirstValCol = "t.tier * b.battlesCount", battleValCol = "tier" }
            };
            chartTypeList.Add(new ChartType() { name = "WN7", col = chartTypeColList, calcType = CalculationType.wn7 });

            // WN7 per battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "wn7" }
            };
            chartTypeList.Add(new ChartType() { name = "WN7 per battle", col = chartTypeColList, totals = false});

            // Efficiency
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" },
                new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" },
                new ChartTypeCols() { playerTankValCol = "spot", battleValCol = "spotted" },
                new ChartTypeCols() { playerTankValCol = "frags", battleValCol = "frags" },
                new ChartTypeCols() { playerTankValCol = "def", battleValCol = "def" },
                new ChartTypeCols() { playerTankValCol = "cap", battleValCol = "cap" },
                new ChartTypeCols() { playerTankValCol = "t.tier * ptb.battles", battleFirstValCol = "t.tier * b.battlesCount", battleValCol = "tier" }
            };
            chartTypeList.Add(new ChartType() { name = "EFF", col = chartTypeColList, calcType = CalculationType.eff });

            // Efficiency per battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "eff" }
            };
            chartTypeList.Add(new ChartType() { name = "EFF per battle", col = chartTypeColList, totals = false, seriesStyle = SeriesChartType.Point });

            // XP
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "xp" },
                new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" }
            };
            chartTypeList.Add(new ChartType() { name = "XP Average", col = chartTypeColList, calcType = CalculationType.firstDividedOnNext });

            // XP
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "xp" }
            };
            chartTypeList.Add(new ChartType() { name = "XP Total", col = chartTypeColList });

            // Average damage
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "dmg" },
                new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" }
            };
            chartTypeList.Add(new ChartType() { name = "Damage Average", col = chartTypeColList, calcType = CalculationType.firstDividedOnNext });

            // Damage
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "dmg" }
            };
            chartTypeList.Add(new ChartType() { name = "Damage Total", col = chartTypeColList });

            // Frag Average
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "frags" },
                new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" }
            };
            chartTypeList.Add(new ChartType() { name = "Frags Average", col = chartTypeColList, calcType = CalculationType.firstDividedOnNext });

            // Frag Count
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "frags" }
            };
            chartTypeList.Add(new ChartType() { name = "Frags Total", col = chartTypeColList });

            // Battle count
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" }
            };
            chartTypeList.Add(new ChartType() { name = "Battle Count", col = chartTypeColList });

            // Victory Count
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" }
            };
            chartTypeList.Add(new ChartType() { name = "Victory Count", col = chartTypeColList });

            // Draw Count
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "(battles - wins - losses)", battleValCol = "draw" }
            };
            chartTypeList.Add(new ChartType() { name = "Draw Count", col = chartTypeColList });

            // Defeat Count
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "losses", battleValCol = "defeat" }
            };
            chartTypeList.Add(new ChartType() { name = "Defeat Count", col = chartTypeColList });

            // Tier Total Average
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" },
                new ChartTypeCols() { playerTankValCol = "t.tier * ptb.battles", battleFirstValCol = "t.tier * b.battlesCount", battleValCol = "tier" }
            };
            chartTypeList.Add(new ChartType() { name = "Tier Total Avg", col = chartTypeColList, calcType = CalculationType.tierTotal });

            // Tier Interval Average
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "battles", battleValCol = "battlescount" },
                new ChartTypeCols() { playerTankValCol = "t.tier * ptb.battles", battleFirstValCol = "t.tier * b.battlesCount", battleValCol = "tier" },
            };
            chartTypeList.Add(new ChartType() { name = "Tier Played Avg", col = chartTypeColList, calcType = CalculationType.tierInterval });

            // Raw damage in battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" },
            };

            chartTypeList.Add(new ChartType() { name = "Dmg", col = chartTypeColList, totals = false, calcType = CalculationType.standard });

            // EMAi(100) damage in battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" },
            };

            chartTypeList.Add(new ChartType() { name = "EMAi(100) Dmg", col = chartTypeColList, totals = true, calcType = CalculationType.EMAiN100 });

            // Assisted spot in battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "assistSpot", battleValCol = "assistSpot" },
            };

            chartTypeList.Add(new ChartType() { name = "Assisted Spot", col = chartTypeColList, totals = false, calcType = CalculationType.standard });

            // EMAi(100) assisted spot in battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "assistSpot", battleValCol = "assistSpot" },
            };

            chartTypeList.Add(new ChartType() { name = "EMAi(100) Assisted Spot", col = chartTypeColList, totals = true, calcType = CalculationType.EMAiN100});

            // Assisted track in battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "assistTrack", battleValCol = "assistTrack" },
            };

            chartTypeList.Add(new ChartType() { name = "Assisted Track", col = chartTypeColList, totals = false, calcType = CalculationType.standard });

            // EMAi(100) assisted track in battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "assistTrack", battleValCol = "assistTrack" },
            };

            chartTypeList.Add(new ChartType() { name = "EMAi(100) Assisted Track", col = chartTypeColList, totals = true, calcType = CalculationType.EMAiN100 });

            // Combined damage in battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" },
                new ChartTypeCols() { playerTankValCol = "assistSpot", battleValCol = "assistSpot" },                
                new ChartTypeCols() { playerTankValCol = "assistTrack", battleValCol = "assistTrack" },            
            };

            chartTypeList.Add(new ChartType() { name = "Combined Damage", col = chartTypeColList, totals = false, calcType = CalculationType.standard });

            // EMAi(100) Combined damage track in battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "dmg", battleValCol = "dmg" },
                new ChartTypeCols() { playerTankValCol = "assistSpot", battleValCol = "assistSpot" },
                new ChartTypeCols() { playerTankValCol = "assistTrack", battleValCol = "assistTrack" },
            };

            chartTypeList.Add(new ChartType() { name = "EMAi(100) Combined Damage", col = chartTypeColList, totals = true, calcType = CalculationType.EMAiCombinedDmg });

            // Min tier in battle 
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { battleValCol = "minBattleTier" },
            };

            chartTypeList.Add(new ChartType() { name = "Minimum tier in battle", col = chartTypeColList, totals = false, calcType = CalculationType.standard });

            // EMAi(10) Min tier in battle 
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { battleValCol = "minBattleTier" },
            };

            chartTypeList.Add(new ChartType() { name = "EMAi(10) Minimum tier in battle", col = chartTypeColList, totals = true, calcType = CalculationType.EMAiN10 });

            // Max tier in battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { battleValCol = "maxBattleTier" },
            };

            chartTypeList.Add(new ChartType() { name = "Maximum tier in battle", col = chartTypeColList, totals = false, calcType = CalculationType.standard });

            // EMAi(10) Max tier in battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { battleValCol = "maxBattleTier" },
            };

            chartTypeList.Add(new ChartType() { name = "EMAi(10) Maximum tier in battle", col = chartTypeColList, totals = true, calcType = CalculationType.EMAiN10 });

            /*
            // Max - current tier difference in battle
            chartTypeColList = new List<ChartTypeCols>
            {
                // new ChartTypeCols() { battleValCol = "maxBattleTier" },
                // new ChartTypeCols() { battleValCol = "tier" },
                new ChartTypeCols() { battleValCol = "maxBattleTier-tier" },
            };

            chartTypeList.Add(new ChartType() { name = "Tier difference in battle", col = chartTypeColList, totals = true, calcType = CalculationType.standard });

            // EMAi(10) Max - current tier difference in battle
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { battleValCol = "maxBattleTier" },
                new ChartTypeCols() { battleValCol = "tier" },
            };

            chartTypeList.Add(new ChartType() { name = "EMAi(10) Tier difference in battle", col = chartTypeColList, totals = true, calcType = CalculationType.EMAiN10 });
            */

            // Victory
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "wins", battleValCol = "victory" },
            };

            chartTypeList.Add(new ChartType() { name = "Victory", col = chartTypeColList, totals = false, calcType = CalculationType.standard });

            // EMAi(10) Victory
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { battleValCol = "victory" },
            };

            chartTypeList.Add(new ChartType() { name = "EMAi(10) Victory", col = chartTypeColList, totals = true, calcType = CalculationType.EMAiN10 });

            // Survival
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { playerTankValCol = "survived", battleValCol = "survived" },
            };

            chartTypeList.Add(new ChartType() { name = "Survival", col = chartTypeColList, totals = false, calcType = CalculationType.standard });

            // EMAi(10) Survival
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { battleValCol = "survived" },
            };

            chartTypeList.Add(new ChartType() { name = "EMAi(10) Survival", col = chartTypeColList, totals = true, calcType = CalculationType.EMAiN10 });

            // Battle life time
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { battleValCol = "b.battleLifeTime" },
            };

            chartTypeList.Add(new ChartType() { name = "Life time", col = chartTypeColList, totals = false, calcType = CalculationType.standard });

            // EMAi(10) Battle life time
            chartTypeColList = new List<ChartTypeCols>
            {
                new ChartTypeCols() { battleValCol = "b.battleLifeTime" },
            };

            chartTypeList.Add(new ChartType() { name = "EMAi(10) Life time", col = chartTypeColList, totals = true, calcType = CalculationType.EMAiN10 });

            // Done
            return chartTypeList;
        }

        #endregion
    }
}
