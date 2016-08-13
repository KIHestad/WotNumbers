using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinApp
{
    public class FormCloseOnEsc : Form
    {
        public FormCloseOnEsc()
        {
            // Add close form on pressing ESC
            this.KeyPreview = true;
            this.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Escape) this.Close();
            };
        }
    }
}
