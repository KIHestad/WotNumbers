using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinApp.Gadget
{
	public partial class ucImage : UserControl
	{
		public ucImage(Image image = null)
		{
			InitializeComponent();
			if (image != null)
				picture.Image = image;
		}

		private void ucImage_Load(object sender, EventArgs e)
		{

		}


	}
}
