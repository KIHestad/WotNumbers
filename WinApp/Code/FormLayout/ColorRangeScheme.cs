using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WinApp.Code.FormLayout
{
    public class ColorRangeScheme
    {
        public enum RatingColorScheme
        {
            WN_Official_Colors = 0,
            WoT_Labs_Colors = 1,
            XVM_Colors = 2
        }
        
        public static void SetRatingColors()
        {
            switch (Config.Settings.RatingColors)
            {
                // http://wiki.wnefficiency.net/pages/Color_Scale
                case RatingColorScheme.WN_Official_Colors:
                    RangeWN7 = new double[] { 0, 500, 700, 900, 1100, 1350, 1550, 1850, 2050 };
                    RangeWN8 = new double[] { 0, 300, 600, 900, 1250, 1600, 1900, 2350, 2900 };
                    RangeWR = new double[]  { 0, 45, 47, 49, 52, 54, 56, 60, 65 };
                    break;

                // http://wotlabs.net/
                case RatingColorScheme.WoT_Labs_Colors:
                    RangeWN7 = new double[] { 0, 500, 700, 900, 1100, 1350, 1550, 1850, 2050 }; // Same as WN official colors, has not own official values
                    RangeWN8 = new double[] { 0, 300, 450, 650, 900, 1200, 1600, 2450, 2900 };
                    RangeWR = new double[]  { 0, 45, 47, 49, 52, 54, 56, 60, 65 };
                    break;

                // https://bitbucket.org/XVM/xvm/src/067589e31b0abeee26743043188b39fb05d683a1/release/configs/default/colors.xc?at=default&fileviewer=file-view-default
                case RatingColorScheme.XVM_Colors:
                    RangeWN7 = new double[] { 0, 500, 700, 900, 1100, 1350, 1550, 1850, 2050 }; // Same as WN official colors, has not own official values
                    RangeWN8 = new double[] { 0, 380, 860, 860, 1420, 2105, 2105, 2770, 2770 };
                    RangeWR = new double[]  { 0, 46.5, 48.5, 48.5, 52.5, 57.5, 57.5, 64.5, 64.5 };
                    break;
            }            
        }

        public static double[] RangeEFF = { 0, 305, 615, 870, 1175, 1300, 1525, 1850, 2000 }; // XVM Colors (january 2016)

        public static Color EffColor(double eff)
        {
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

        public static double[] RangeWN7 = { 0 }; // Set accoring to application layout settings - SetRatingColors()

        public static Color WN7color(double wn7)
        {
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

        public static double[] RangeWN9 = { 0, 100, 125, 160, 200, 240, 295, 380, 420 }; // Set accoring to http://jaj22.org.uk/wotstats.html

        public static Color WN9color(double wn9)
        {
            Color wn9RatingColor = ColorTheme.Rating_very_bad;
            if (wn9 >= RangeWN8[8]) wn9RatingColor = ColorTheme.Rating_super_uniqum;
            else if (wn9 >= RangeWN8[7]) wn9RatingColor = ColorTheme.Rating_uniqum;
            else if (wn9 >= RangeWN8[6]) wn9RatingColor = ColorTheme.Rating_great;
            else if (wn9 >= RangeWN8[5]) wn9RatingColor = ColorTheme.Rating_very_good;
            else if (wn9 >= RangeWN8[4]) wn9RatingColor = ColorTheme.Rating_good;
            else if (wn9 >= RangeWN8[3]) wn9RatingColor = ColorTheme.Rating_average;
            else if (wn9 >= RangeWN8[2]) wn9RatingColor = ColorTheme.Rating_below_average;
            else if (wn9 >= RangeWN8[1]) wn9RatingColor = ColorTheme.Rating_bad;
            return wn9RatingColor;
        }


        public static double[] RangeWN8 = { 0 }; // Set accoring to application layout settings - SetRatingColors()
        
        public static Color WN8color(double wn8)
        {
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

        public static double[] RangeWR = { 0 }; // Set accoring to application layout settings - SetRatingColors()
        
        public static Color WinRateColor(double wr)
        {
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

        public static Color BattleCountColor(int battleCount) // XVM Colors (january 2016)
        {
            double kBattles =   Math.Round(Convert.ToDouble(battleCount / 1000), 0);
            Color battleCountRatingColor =                      ColorTheme.Rating_very_bad;
            if (kBattles > 43) battleCountRatingColor =         ColorTheme.Rating_uniqum;
            else if (kBattles > 30) battleCountRatingColor =    ColorTheme.Rating_great;
            else if (kBattles > 16) battleCountRatingColor =     ColorTheme.Rating_good;
            else if (kBattles > 6) battleCountRatingColor =     ColorTheme.Rating_below_average;
            else if (kBattles > 2) battleCountRatingColor =     ColorTheme.Rating_bad;
            return battleCountRatingColor;
        }

        public static double[] RangeKillDeath = { 0, 0.25, 0.5, 0.75, 1, 1.2, 1.4, 1.6, 1.8, 2 }; // Custom for Wot Numbers

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

        public static double[] RangeDmgRank = { 0, 20, 40, 55, 65, 75, 85, 90, 95, 98 }; // Custom for Wot Numbers

        public static Color DmgRankColor(double value)
        {
            Color col =                              ColorTheme.Rating_0_redDark;
            if      (value >= RangeDmgRank[9]) col = ColorTheme.Rating_9_purpleDark;
            else if (value >= RangeDmgRank[8]) col = ColorTheme.Rating_8_purple;
            else if (value >= RangeDmgRank[7]) col = ColorTheme.Rating_7_blueDark;
            else if (value >= RangeDmgRank[6]) col = ColorTheme.Rating_6_blue;
            else if (value >= RangeDmgRank[5]) col = ColorTheme.Rating_5_greenDark;
            else if (value >= RangeDmgRank[4]) col = ColorTheme.Rating_4_green;
            else if (value >= RangeDmgRank[3]) col = ColorTheme.Rating_3_yellow;
            else if (value >= RangeDmgRank[2]) col = ColorTheme.Rating_2_orange;
            else if (value >= RangeDmgRank[1]) col = ColorTheme.Rating_1_red;
            return col;
        }

        public static double[] RangeRWR = { -20, -15, -10, -5, 0, 5, 10, 15, 20, 25 }; // Custom for Wot Numbers

        public static Color RWRcolor(double value)
        {
            Color col = ColorTheme.Rating_0_redDark;
            if (value >= RangeRWR[9]) col = ColorTheme.Rating_9_purpleDark;
            else if (value >= RangeRWR[8]) col = ColorTheme.Rating_8_purple;
            else if (value >= RangeRWR[7]) col = ColorTheme.Rating_7_blueDark;
            else if (value >= RangeRWR[6]) col = ColorTheme.Rating_6_blue;
            else if (value >= RangeRWR[5]) col = ColorTheme.Rating_5_greenDark;
            else if (value >= RangeRWR[4]) col = ColorTheme.Rating_4_green;
            else if (value >= RangeRWR[3]) col = ColorTheme.Rating_3_yellow;
            else if (value >= RangeRWR[2]) col = ColorTheme.Rating_2_orange;
            else if (value >= RangeRWR[1]) col = ColorTheme.Rating_1_red;
            return col;
        }


    }
}
