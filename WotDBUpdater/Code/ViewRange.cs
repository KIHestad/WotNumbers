using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater
{
    class ViewRange
    {

        // ref: 
        // http://forum.worldoftanks.asia/index.php?/topic/26905-question-how-to-calculate-final-view-range/
        // http://www.wotinfo.net/en/camo-calculator
        // http://wiki.worldoftanks.com/Battle_Mechanics#How_tank_stats_are_calculated


        public static double CalcViewRange()
        {
            // Example values for T18
            double baseTankVR = 240;
            double basePrimarySkill = 100;
            double baseSecondarySkill = 100;

            double eqBino = 25;                // 25
            double eqOptics = 10;              // 10
            double skillAwareness = 0;         // <3.3  (calculated from wotinfo.net, in-game values are rounded to nearest integer)
            double skillRecon = 0;             // <2

            double skillBIA = 5;               // 5
            double eqVent = 5;                 // 5
            double premiumCons = 0;            // 10

            double calcBaseVR = 0;
            double calcPrimarySkill = 0;
            double calcSecondarySkill = 0;
            double calcVR = 0;

            // Bino and optics do not stack
            if (eqBino > 0 && eqOptics > 0)
            {
                eqOptics = 0;
            }

            // Ventilation = 5% extra crew skill
            if (eqVent > 0)
            {
                skillAwareness = skillAwareness * 1.05;
                skillRecon = skillRecon * 1.05;
            };

            // BIA = 5% extra crew skill
            if (skillBIA > 0)
            {
                skillAwareness = skillAwareness * 1.05;
                skillRecon = skillRecon * 1.05;
            }

            // Premium Consumables = 10% extra crew skill
            if (premiumCons > 0)
            {
                skillAwareness = skillAwareness * 1.1;
                skillRecon = skillRecon * 1.1;
            }

            calcBaseVR = baseTankVR / 0.875;

            calcPrimarySkill = 0.5 + (0.00375 * (basePrimarySkill + eqVent + skillBIA + premiumCons));

            calcSecondarySkill = 1 + ((skillAwareness / 100) * (baseSecondarySkill / 100))
                                   + ((skillRecon / 100) * (baseSecondarySkill / 100))
                                   + (eqBino / 100)
                                   + (eqOptics / 100);

            calcVR = calcBaseVR * calcPrimarySkill * calcSecondarySkill;

            return calcVR;
        }
    }
}
