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


        private void ViewRange_Load(object sender, EventArgs e)
        {
            loadComboRecon();
            loadComboAwareness();
        }

        private void loadComboRecon()
        {
            comboRecon.SelectedIndex = 0;
        }

        private void loadComboAwareness()
        {
            comboAwareness.SelectedIndex = 0;
        }


        //private void PropertiesChanged(object sender, EventArgs e, bool vent = false, bool bino = false, bool optics = false, int BIA = 0, int awareness = 0, int recon = 0, bool cons = false)
        private void PropertiesChanged(object sender, EventArgs e)
        {
            bool vent = false;
            bool bino = false;
            bool optics = false;
            bool BIA = false;
            int awareness = 0;
            int recon = 0;
            bool cons = false;
            if (cbVent.Checked)
                vent = true;
            if (cbBino.Checked)
                bino = true;
            if (cbOptics.Checked)
                optics = true;
            if (cbBIA.Checked)
                BIA = true;
            //awareness = Int32.Parse(comboAwareness.SelectedItem.ToString());
            //recon = Int32.Parse(comboRecon.SelectedItem.ToString());
            if (cbCons.Checked)
                cons = true;
            double vr = TankPerformance.CalcViewRange(vent, bino, optics, BIA, awareness, recon, cons);
            labelBaseVR.Text = vr.ToString();
        }



    }
}
