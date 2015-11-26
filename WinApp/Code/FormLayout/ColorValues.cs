using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WinApp.Code.FormLayout
{
    public class ColorValues
    {
        public static double[] RangeEFF = { 0, 305, 610, 850, 1145, 1300, 1475, 1775, 2000 };

        public static Color EffColor(double eff)
        {
            // Dynamic color by efficiency
            //  { "value": 610,  "color": ${"def.colorRating.very_bad" } },  //    0 - 609  - very bad   (20% of players)
            //  { "value": 850,  "color": ${"def.colorRating.bad"      } },  //  610 - 849  - bad        (better then 20% of players)
            //  { "value": 1145, "color": ${"def.colorRating.normal"   } },  //  850 - 1144 - normal     (better then 60% of players)
            //  { "value": 1475, "color": ${"def.colorRating.good"     } },  // 1145 - 1474 - good       (better then 90% of players)
            //  { "value": 1775, "color": ${"def.colorRating.very_good"} },  // 1475 - 1774 - very good  (better then 99% of players)
            //  { "value": 9999, "color": ${"def.colorRating.unique"   } }   // 1775 - *    - unique     (better then 99.9% of players)
            Color effRatingColor =                          ColorTheme.Rating_very_bad;
            if (eff >= RangeEFF[8]) effRatingColor =        ColorTheme.Rating_super_uniqum;
            else if (eff >= RangeEFF[7]) effRatingColor =   ColorTheme.Rating_uniqum;
            else if (eff >= RangeEFF[6]) effRatingColor =   ColorTheme.Rating_great;
            else if (eff >= RangeEFF[5]) effRatingColor =   ColorTheme.Rating_very_good;
            else if (eff >= RangeEFF[4]) effRatingColor =   ColorTheme.Rating_good;
            else if (eff >= RangeEFF[3]) effRatingColor =   ColorTheme.Rating_average;
            else if (eff >= RangeEFF[2]) effRatingColor =   ColorTheme.Rating_below_average;
            else if (eff >= RangeEFF[1]) effRatingColor =   ColorTheme.Rating_bad;
            return effRatingColor;
        }

        public static double[] RangeWN7 = { 0, 500, 700, 900, 1100, 1350, 1550, 1850, 2050 };

        public static Color WN7color(double wn7)
        {
            // http://wiki.wnefficiency.net/pages/Color_Scale
            Color wn7RatingColor =                          ColorTheme.Rating_very_bad;
            if (wn7 >= RangeWN7[8]) wn7RatingColor =        ColorTheme.Rating_super_uniqum;
            else if (wn7 >= RangeWN7[7]) wn7RatingColor =   ColorTheme.Rating_uniqum;
            else if (wn7 >= RangeWN7[6]) wn7RatingColor =   ColorTheme.Rating_great;
            else if (wn7 >= RangeWN7[5]) wn7RatingColor =   ColorTheme.Rating_very_good;
            else if (wn7 >= RangeWN7[4]) wn7RatingColor =   ColorTheme.Rating_good;
            else if (wn7 >= RangeWN7[3]) wn7RatingColor =   ColorTheme.Rating_average;
            else if (wn7 >= RangeWN7[2]) wn7RatingColor =   ColorTheme.Rating_below_average;
            else if (wn7 >= RangeWN7[1]) wn7RatingColor =   ColorTheme.Rating_bad;
            return wn7RatingColor;
        }

        public static double[] RangeWN8 = { 0, 300, 600, 900, 1250, 1600, 1900, 2350, 2900 };

        public static Color WN8color(double wn8)
        {
            // http://wiki.wnefficiency.net/pages/Color_Scale
            Color wn8RatingColor =                          ColorTheme.Rating_very_bad;
            if (wn8 >= RangeWN8[8]) wn8RatingColor =        ColorTheme.Rating_super_uniqum;
            else if (wn8 >= RangeWN8[7]) wn8RatingColor =   ColorTheme.Rating_uniqum;
            else if (wn8 >= RangeWN8[6]) wn8RatingColor =   ColorTheme.Rating_great;
            else if (wn8 >= RangeWN8[5]) wn8RatingColor =   ColorTheme.Rating_very_good;
            else if (wn8 >= RangeWN8[4]) wn8RatingColor =   ColorTheme.Rating_good;
            else if (wn8 >= RangeWN8[3]) wn8RatingColor =   ColorTheme.Rating_average;
            else if (wn8 >= RangeWN8[2]) wn8RatingColor =   ColorTheme.Rating_below_average;
            else if (wn8 >= RangeWN8[1]) wn8RatingColor =   ColorTheme.Rating_bad;
            return wn8RatingColor;
        }

        public static double[] RangeWR = { 0, 45, 47, 49, 52, 54, 56, 60, 65 };

        public static Color WinRateColor(double wr)
        {
            // http://wiki.wnefficiency.net/pages/Color_Scale
            Color wrRatingColor =                       ColorTheme.Rating_very_bad;
            if (wr >= RangeWR[8]) wrRatingColor =       ColorTheme.Rating_super_uniqum;
            else if (wr >= RangeWR[7]) wrRatingColor =  ColorTheme.Rating_uniqum;
            else if (wr >= RangeWR[6]) wrRatingColor =  ColorTheme.Rating_great;
            else if (wr >= RangeWR[5]) wrRatingColor =  ColorTheme.Rating_very_good;
            else if (wr >= RangeWR[4]) wrRatingColor =  ColorTheme.Rating_good;
            else if (wr >= RangeWR[3]) wrRatingColor =  ColorTheme.Rating_average;
            else if (wr >= RangeWR[2]) wrRatingColor =  ColorTheme.Rating_below_average;
            else if (wr >= RangeWR[1]) wrRatingColor =  ColorTheme.Rating_bad;
            return wrRatingColor;
        }

        public static Color BattleCountColor(int battleCount)
        {
            //// Dynamic color by kilo-battles
            //  { "value": 2,   "color": ${"def.colorRating.very_bad" } },   //  0 - 1
            //  { "value": 5,   "color": ${"def.colorRating.bad"      } },   //  2 - 4
            //  { "value": 9,   "color": ${"def.colorRating.normal"   } },   //  5 - 8
            //  { "value": 14,  "color": ${"def.colorRating.good"     } },   //  9 - 13
            //  { "value": 20,  "color": ${"def.colorRating.very_good"} },   // 14 - 19
            //  { "value": 999, "color": ${"def.colorRating.unique"   } }    // 20 - *
            double kBattles =   Math.Round(Convert.ToDouble(battleCount / 1000), 0);
            Color battleCountRatingColor =                      ColorTheme.Rating_very_bad;
            if (kBattles > 20) battleCountRatingColor =         ColorTheme.Rating_uniqum;
            else if (kBattles > 14) battleCountRatingColor =    ColorTheme.Rating_great;
            else if (kBattles > 9) battleCountRatingColor =     ColorTheme.Rating_good;
            else if (kBattles > 5) battleCountRatingColor =     ColorTheme.Rating_below_average;
            else if (kBattles > 2) battleCountRatingColor =     ColorTheme.Rating_bad;
            return battleCountRatingColor;
        }

        public static double[] RangeKillDeath = { 0, 0.25, 0.5, 0.75, 1, 1.2, 1.4, 1.6, 1.8, 2 };

        public static Color KillDeathColor(double value)
        {
            Color killDeathRatingColor =                                ColorTheme.Rating_very_bad;
            if (value >= RangeKillDeath[8]) killDeathRatingColor =      ColorTheme.Rating_super_uniqum;
            else if (value >= RangeKillDeath[7]) killDeathRatingColor = ColorTheme.Rating_uniqum;
            else if (value >= RangeKillDeath[6]) killDeathRatingColor = ColorTheme.Rating_great;
            else if (value >= RangeKillDeath[5]) killDeathRatingColor = ColorTheme.Rating_very_good;
            else if (value >= RangeKillDeath[4]) killDeathRatingColor = ColorTheme.Rating_good;
            else if (value >= RangeKillDeath[3]) killDeathRatingColor = ColorTheme.Rating_average;
            else if (value >= RangeKillDeath[2]) killDeathRatingColor = ColorTheme.Rating_below_average;
            else if (value >= RangeKillDeath[1]) killDeathRatingColor = ColorTheme.Rating_bad;
            return killDeathRatingColor;
        }

        public static double[] RangeDmgRank = { 0, 20, 40, 55, 65, 75, 85, 90, 95, 98 };

        public static Color DmgRankColor(double value)
        {
            Color killDeathRatingColor =                              ColorTheme.Rating_0_redDark;
            if      (value >= RangeDmgRank[9]) killDeathRatingColor = ColorTheme.Rating_9_purpleDark;
            else if (value >= RangeDmgRank[8]) killDeathRatingColor = ColorTheme.Rating_8_purple;
            else if (value >= RangeDmgRank[7]) killDeathRatingColor = ColorTheme.Rating_7_blueDark;
            else if (value >= RangeDmgRank[6]) killDeathRatingColor = ColorTheme.Rating_6_blue;
            else if (value >= RangeDmgRank[5]) killDeathRatingColor = ColorTheme.Rating_5_greenDark;
            else if (value >= RangeDmgRank[4]) killDeathRatingColor = ColorTheme.Rating_4_green;
            else if (value >= RangeDmgRank[3]) killDeathRatingColor = ColorTheme.Rating_3_yellow;
            else if (value >= RangeDmgRank[2]) killDeathRatingColor = ColorTheme.Rating_2_orange;
            else if (value >= RangeDmgRank[1]) killDeathRatingColor = ColorTheme.Rating_1_red;
            return killDeathRatingColor;
        }


    }
}
