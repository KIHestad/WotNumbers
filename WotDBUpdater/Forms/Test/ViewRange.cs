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

        public event EventHandler OnPropertyChanged;


        private void comboBIA_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
