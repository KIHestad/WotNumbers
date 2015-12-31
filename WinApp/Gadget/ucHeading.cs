using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Gadget
{
    public partial class ucHeading : UserControl
    {
        public ucHeading(string headerText)
        {
            InitializeComponent();
            label1.Text = headerText;
        }

        private void ucHeading_Paint(object sender, PaintEventArgs e)
        {
			if (BackColor == ColorTheme.FormBackSelectedGadget)
				GadgetHelper.DrawBorderOnGadget(sender, e);
        }

        
    }
}
