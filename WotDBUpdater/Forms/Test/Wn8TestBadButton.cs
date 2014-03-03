using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater.Forms.Test
{
    class Wn8TestBadButton
    {
        public double Wn8(int tankId, double avgDmg, double avgSpot, double avgFrag, double avgDef, double avgWinRate)
        {
            // get wn8 exp values for tank
            double expDmg = 0;
            double expSpot = 0;
            double expFrag = 0;
            double expDef = 0;
            double expWinRate = 0;
            // Step 1
            double rDAMAGE = avgDmg / expDmg;
            double rSPOT = avgSpot / expSpot;
            double rFRAG = avgFrag / expFrag;
            double rDEF = avgDef / expDef;
            double rWIN = avgWinRate / expWinRate;
            // Step 2
            double rWINc = Math.Max(0, (rWIN - 0.71) / (1 - 0.71));
            double rDAMAGEc = Math.Max(0, (rDAMAGE - 0.22) / (1 - 0.22));
            double rFRAGc = Math.Max(0, Math.Min(rDAMAGEc + 0.2, (rFRAG - 0.12) / (1 - 0.12)));
            double rSPOTc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rSPOT - 0.38) / (1 - 0.38)));
            double rDEFc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rDEF - 0.10) / (1 - 0.10)));
            // Step 3
            double WN8 = 980 * rDAMAGEc + 210 * rDAMAGEc * rFRAGc + 155 * rFRAGc * rSPOTc + 75 * rDEFc * rFRAGc + 145 * Math.Min(1.8, rWINc);
            // Return value
            return WN8;
        }
    }
}
