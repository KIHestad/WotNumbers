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

        private void comboBIA_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void checkBoxVent_CheckedChanged(object sender, EventArgs e)
        {
            //int eqVent;
            //if (checkBoxVent.Checked)
            //    eqVent = 1;
            double vr = TankPerformance.CalcViewRange(0);
            labelBaseVR.Text = vr.ToString();
        }

    }
}
