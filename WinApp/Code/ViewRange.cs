using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
    class ViewRange
    {

        // ref: 
        // http://forum.worldoftanks.asia/index.php?/topic/26905-question-how-to-calculate-final-view-range/
        // http://www.wotinfo.net/en/camo-calculator
        // http://wiki.worldoftanks.com/Battle_Mechanics#How_tank_stats_are_calculated


        public static double CalcViewRange()
        {
            // Catch selected values from form
            double baseTankVR = 420;
            double basePrimarySkill = 100;
            double baseBIASkill = 100;
            double baseReconSkill = 100;
            double baseAwarenessSkill = 100;
            int eqBino = 1;
            int eqOptics = 1;
            int eqVent = 1;
            int premiumCons = 1;

            // Declare factors
            double BIAFactor = 0;
            double reconFactor = 0;
            double awarenessFactor = 0;
            double binoFactor = 0;
            double opticsFactor = 0;
            double ventFactor = 0;
            double premiumConsFactor = 0;
            double bonus = 0;

            // Declare final calculation variables
            double calcBaseVR = 0;
            double calcPrimarySkill = 0;
            double calcSecondarySkill = 0;
            double calcVR = 0;


            // Set BIAFactor
            if (baseBIASkill > 0)
            {
                BIAFactor = 5 * baseBIASkill / 100;
            }

            // Set ventFactor
            if (eqVent > 0)
            {
                ventFactor = 5;
            }

            // Set premiumConsFactor
            if (premiumCons > 0)
            {
                premiumConsFactor = 10;
            }

            // Set binoFactor
            if (eqBino > 0)
            {
                binoFactor = 1.25;
            }

            // Set opticsFactor
            if (eqOptics > 0)
            {
                if (eqBino > 0 && eqOptics > 0)  // Bino and optics don't stack
                {
                    opticsFactor = 1;
                }
                else
                {
                    opticsFactor = 1.1;
                }
            }

            // Calculate reconFactor after adding bonus
            if (baseReconSkill > 0)
            {
                if (eqVent > 0)
                {
                    bonus = bonus + 5;
                }
                if (baseBIASkill > 0)
                {
                    bonus = bonus + 5;
                }
                if (premiumCons > 0)
                {
                    bonus = bonus + 10;
                }
                baseReconSkill = baseReconSkill * (1 + (bonus / 100));
                reconFactor = 1 + (0.0002 * baseReconSkill);
            }

            // Calculate awarenessFactor after adding bonus
            bonus = 0;
            if (baseAwarenessSkill > 0)
            {
                if (eqVent > 0)
                {
                    bonus = bonus + 5;
                }
                if (baseBIASkill > 0)
                {
                    bonus = bonus + 5;
                }
                if (premiumCons > 0)
                {
                    bonus = bonus + 10;
                }
                baseAwarenessSkill = (baseAwarenessSkill + 10) * (1 + (bonus / 100));       // magic number 10? (from wotinfo.net formula)
                awarenessFactor = 1 + (0.0003 * baseAwarenessSkill);
            }


            // Calculate performance factors
            calcBaseVR = baseTankVR / 0.875;
            calcPrimarySkill = 0.5 + (0.00375 * (basePrimarySkill + ventFactor + BIAFactor + premiumConsFactor));
            calcSecondarySkill = awarenessFactor * reconFactor * binoFactor * opticsFactor;

            // Calculate final view range
            calcVR = calcBaseVR * calcPrimarySkill * calcSecondarySkill;


            return calcVR;
        }
    }
}
