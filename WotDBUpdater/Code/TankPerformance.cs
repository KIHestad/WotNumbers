using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater.Code
{
    class TankPerformance
    {

        // ref: 
        // http://forum.worldoftanks.asia/index.php?/topic/26905-question-how-to-calculate-final-view-range/
        // http://www.wotinfo.net/en/camo-calculator
        // http://wiki.worldoftanks.com/Battle_Mechanics#How_tank_stats_are_calculated

        public static double CalcViewRange(int baseVR, int primarySkill, bool vent, bool bino, bool optics, bool BIA, int awareness, int recon, bool cons)
        {
            // Catch selected values from form
            double baseTankVR = baseVR;
            double basePrimarySkill = primarySkill;
            bool baseBIASkill = BIA;
            double baseReconSkill = recon;
            double baseAwarenessSkill = awareness;
            bool eqBino = bino;
            bool eqOptics = optics;
            bool eqVent = vent;
            bool premiumCons = cons;

            // Declare factors
            double BIAFactor = 0;
            double reconFactor = 1;
            double awarenessFactor = 1;
            double binoFactor = 1;
            double opticsFactor = 1;
            double ventFactor = 0;
            double premiumConsFactor = 0;
            double bonus = 0;

            // Declare final calculation variables
            double calcBaseVR = 0;
            double calcPrimarySkill = 0;
            double calcSecondarySkill = 0;
            double calcVR = 0;


            // Set BIAFactor
            if (baseBIASkill)
            {
                BIAFactor = 5;
            }

            // Set ventFactor
            if (eqVent)
            {
                ventFactor = 5;
            }

            // Set premiumConsFactor
            if (premiumCons)
            {
                premiumConsFactor = 10;
            }

            // Set binoFactor
            if (eqBino)
            {
                binoFactor = 1.25;
            }

            // Set opticsFactor
            if (eqOptics)
            {
                if (eqBino && eqOptics)  // Bino and optics don't stack
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
                if (eqVent)
                {
                    bonus = bonus + 5;
                }
                if (baseBIASkill)
                {
                    bonus = bonus + 5;
                }
                if (premiumCons)
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
                if (eqVent)
                {
                    bonus = bonus + 5;
                }
                if (baseBIASkill)
                {
                    bonus = bonus + 5;
                }
                if (premiumCons)
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
