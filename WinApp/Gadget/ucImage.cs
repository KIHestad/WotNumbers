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
		public ucImage(Image image = null, DateTime? battleTime = null, string result = "", string resultColor= "")
		{
			InitializeComponent();
			if (image != null)
				picture.Image = image;
			label1.Text = "";
			label2.Text = "";
			if (battleTime != null)
			{
				DateTime d = Convert.ToDateTime(battleTime);
				label1.Text = d.ToString("dd.MM.yy HH:mm");
			}
			if (result != "")
				label2.Text = result;
			if (resultColor != "")
				label2.ForeColor = System.Drawing.ColorTranslator.FromHtml(resultColor);
		}

		private void ucImage_Load(object sender, EventArgs e)
		{

		}


	}
}
