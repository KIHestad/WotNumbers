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
            // Set initial values
            int baseVR = 0;
            int primarySkill = 0;
            bool vent = false;
            bool bino = false;
            bool optics = false;
            bool BIA = false;
            int awareness = 0;
            int recon = 0;
            bool cons = false;

            // Validate input

            if (textBoxBaseVR.Text != "")
            {
                if (!Int32.TryParse(textBoxBaseVR.Text, out baseVR))  // reset to 0 if value is not an integer
                {
                    baseVR = 0;
                    textBoxBaseVR.Text = baseVR.ToString();
                }
                if (Int32.Parse(textBoxBaseVR.Text.ToString()) < 0)  // reset to 0 if value is negative
                {
                    baseVR = 0;
                    textBoxBaseVR.Text = baseVR.ToString();
                }
                baseVR = Int32.Parse(textBoxBaseVR.Text.ToString());
            }

            if (textBoxPrimarySkill.Text != "")
            {
                if (!Int32.TryParse(textBoxPrimarySkill.Text, out primarySkill))
                {
                    primarySkill = 0;
                    textBoxPrimarySkill.Text = primarySkill.ToString();
                }
                if (Int32.Parse(textBoxPrimarySkill.Text.ToString()) < 0 || Int32.Parse(textBoxPrimarySkill.Text.ToString()) > 100)
                {
                    primarySkill = 0;
                    textBoxPrimarySkill.Text = primarySkill.ToString();
                }
                primarySkill = Int32.Parse(textBoxPrimarySkill.Text.ToString());
            }

            if (cbVent.Checked)
                vent = true;
            
            if (cbBino.Checked)
                bino = true;
            
            if (cbOptics.Checked)
                optics = true;
            
            if (cbBIA.Checked)
                BIA = true;

            if (textBoxAwareness.Text != "")
            {
                if (!Int32.TryParse(textBoxAwareness.Text, out awareness))
                {
                    awareness = 0;
                    textBoxAwareness.Text = awareness.ToString();
                }
                if (Int32.Parse(textBoxAwareness.Text.ToString()) < 0 || Int32.Parse(textBoxAwareness.Text.ToString()) > 100)
                {
                    awareness = 0;
                    textBoxAwareness.Text = awareness.ToString();
                }
                awareness = Int32.Parse(textBoxAwareness.Text.ToString());
            }

            if (textBoxRecon.Text != "")
            {
                if (!Int32.TryParse(textBoxRecon.Text, out recon))
                {
                    recon = 0;
                    textBoxRecon.Text = recon.ToString();
                }
                if (Int32.Parse(textBoxRecon.Text.ToString()) < 0 || Int32.Parse(textBoxRecon.Text.ToString()) > 100)
                {
                    recon = 0;
                    textBoxRecon.Text = recon.ToString();
                }
                recon = Int32.Parse(textBoxRecon.Text.ToString());
            }

            if (cbCons.Checked)
                cons = true;

            double vr = TankPerformance.CalcViewRange(baseVR, primarySkill, vent, bino, optics, BIA, awareness, recon, cons);
            labelBaseVR.Text = Math.Round(vr, 2).ToString();
        }

        
        private void btnClear_Click(object sender, EventArgs e)
        {
            cbVent.Checked = false;
            cbBino.Checked = false;
            cbOptics.Checked = false;
            cbBIA.Checked = false;
            textBoxRecon.Text = "";
            textBoxAwareness.Text = "";
            cbCons.Checked = false;
        }
    }
}
