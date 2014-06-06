using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WotDBUpdater.Code.Support;

namespace WotDBUpdater.Forms.File
{
	public partial class GrindingParameter : Form
	{
		public GrindingParameter()
		{
			InitializeComponent();
		}

		private void ddFirstBattle_Click(object sender, EventArgs e)
		{
			string returval = Code.DropDownGrid.Show(ddFirstBattle, Code.DropDownGrid.DropDownGridType.List, "2X,3X,5X");
			if (returval != "")
				ddFirstBattle.Text = returval;

		}

		private void ddEveryBattle_Click(object sender, EventArgs e)
		{
			string returval = Code.DropDownGrid.Show(ddEveryBattle, Code.DropDownGrid.DropDownGridType.List, "None,2X,3X,5X");
			if (returval != "")
				ddEveryBattle.Text = returval;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			GrindingData.Settings.FirstVictoryFactor = Convert.ToInt32(ddFirstBattle.Text.Substring(0,1));
			if (ddEveryBattle.Text == "None")
				GrindingData.Settings.EveryVictoryFactor = 0;
			else
				GrindingData.Settings.EveryVictoryFactor = Convert.ToInt32(ddEveryBattle.Text.Substring(0, 1));
			GrindingData.Settings.AutoStart = chkAutoLoad.Checked;
			this.Close();
		}

		private void GrindingParameter_Load(object sender, EventArgs e)
		{
			ddFirstBattle.Text = GrindingData.Settings.FirstVictoryFactor.ToString() + "X";
			if (GrindingData.Settings.EveryVictoryFactor == 0) 
				ddEveryBattle.Text = "None";
			else
				ddEveryBattle.Text = GrindingData.Settings.EveryVictoryFactor.ToString() + "X";
			chkAutoLoad.Checked = GrindingData.Settings.AutoStart;
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
