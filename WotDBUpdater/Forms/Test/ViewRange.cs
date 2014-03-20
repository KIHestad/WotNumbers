using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater.Forms.Test
{
    public partial class ViewRange : Form
    {
        public ViewRange()
        {
            InitializeComponent();
        }



        private void PropertiesChanged(object sender, EventArgs e)
        {
            int baseVR = 0;
            int primarySkill = 0;
            bool vent = false;
            bool bino = false;
            bool optics = false;
            bool BIA = false;
            int awareness = 0;
            int recon = 0;
            bool cons = false;

            if (textBoxBaseVR.Text != "")
                baseVR = Int32.Parse(textBoxBaseVR.Text.ToString());
            if (textBoxPrimarySkill.Text != "")
                primarySkill = Int32.Parse(textBoxPrimarySkill.Text.ToString());
            if (cbVent.Checked)
                vent = true;
            if (cbBino.Checked)
                bino = true;
            if (cbOptics.Checked)
                optics = true;
            if (cbBIA.Checked)
                BIA = true;
            if (textBoxAwareness.Text != "")
                awareness = Int32.Parse(textBoxAwareness.Text.ToString());
            if (textBoxRecon.Text != "")
                recon = Int32.Parse(textBoxRecon.Text.ToString());
            if (cbCons.Checked)
                cons = true;
            double vr = TankPerformance.CalcViewRange(baseVR, primarySkill, vent, bino, optics, BIA, awareness, recon, cons);
            labelBaseVR.Text = Math.Round(vr, 2).ToString();
        }



    }
}
