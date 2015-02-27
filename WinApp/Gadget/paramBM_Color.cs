using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Gadget
{
	public partial class paramBM_Color : Form
	{
		int _gadgetId = -1;

		public paramBM_Color(int gadgetId = -1)
		{
			InitializeComponent();
			_gadgetId = gadgetId;
		}

		private void param_Load(object sender, EventArgs e)
		{
			object[] currentParameters = new object[] { null, null, null, null, null };
			if (_gadgetId > -1)
			{
				// Lookup value for current gadget
				string sql = "select * from gadgetParameter where gadgetId=@gadgetId order by paramNum;";
				DB.AddWithValue(ref sql, "@gadgetId", _gadgetId, DB.SqlDataType.Int);
				DataTable dt = DB.FetchData(sql, Config.Settings.showDBErrors);
				foreach (DataRow dr in dt.Rows)
				{
		 			object paramValue = dr["value"];
					int paramNum = Convert.ToInt32(dr["paramNum"]);
					currentParameters[paramNum] = paramValue;
				}
				// Get battle mode
				switch (currentParameters[0].ToString())
				{
					case "15": ddBattleMode.Text = "Random / TC"; break;
					case "7": ddBattleMode.Text = "Team"; break;
					case "Historical": ddBattleMode.Text = "Historical"; break;
					case "Skirmishes": ddBattleMode.Text = "Skirmishes"; break;
					case "Stronghold": ddBattleMode.Text = "Stronghold"; break;
					case "": ddBattleMode.Text = "All Modes"; break;
				}
				// Get Color
				Color barColor = ColorTheme.ChartBarBlue;
				if (currentParameters[1] != null)
				{
					barColor = ColorTranslator.FromHtml(currentParameters[1].ToString());
				}
				panel1.BackColor = barColor;
			}
		}


		private void ddBattleMode_Click(object sender, EventArgs e)
		{
			DropDownGrid.Show(ddBattleMode, DropDownGrid.DropDownGridType.List, "Random / TC,Team,Historical,Skirmishes,Stronghold,All Modes");
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			if (ddBattleMode.Text == "")
			{
				MsgBox.Show("Please select a battle mode", "Missing battle mode");
			}
			else
			{
				string param = "";
				switch (ddBattleMode.Text)
				{
					case "Random / TC": param = "15"; break;
					case "Team":        param = "7"; break;
					case "Historical":  param = "Historical"; break;
					case "Skirmishes":  param = "Skirmishes"; break;
					case "Stronghold": param = "Stronghold"; break;
					case "All Modes":   param = ""; break;
				}
				GadgetHelper.newParameters[0] = param;
				Color c = panel1.BackColor;
				GadgetHelper.newParameters[1] = System.Drawing.ColorTranslator.ToHtml(c);
				GadgetHelper.newParametersOK = true;
				this.Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			GadgetHelper.newParameters = new object[] { null, null, null, null, null };
			GadgetHelper.newParametersOK = false;
			this.Close();
		}

		private void paramBattleMode_Paint(object sender, PaintEventArgs e)
		{
			if (BackColor == ColorTheme.FormBackSelectedGadget)
				GadgetHelper.DrawBorderOnGadget(sender, e);
		}

		private void btnColorPicker_Click(object sender, EventArgs e)
		{
			colorDialog1.Color = panel1.BackColor;
			colorDialog1.CustomColors = ColorTheme.DefaultChartBarColors();
			colorDialog1.ShowDialog();
			panel1.BackColor = colorDialog1.Color;
		}


	}
}
