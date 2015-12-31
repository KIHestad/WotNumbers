using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinApp.Code.FormView
{
    public class BattleChartHelper
    {
        // Stores choices for values for charts at form load, and sets default values
        public static string TankName { get; set; } 
        public static string ChartMode { get; set; } 
        public static string Value { get; set; } 
        public static string Xaxis { get; set; } 
        public static string Period { get; set; } 
        public static bool Bullet { get; set; } 
        public static bool Spline { get; set; }

        public static void SetBattleChartDefaultValues()
        {
            TankName = "( All Tanks )";
            ChartMode = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeRandom_TC).Name;
            Value = "";
            Xaxis = "Date";
            Period = "( All )";
            Bullet = false;
            Spline = false;
        }
    }
}
