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
            Color barColor = ColorTheme.ChartBarBlue; // Default chart color
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
                if (currentParameters[0] != null)
                {
                    BattleMode.Item battleMode = BattleMode.GetItemFromSqlName(currentParameters[0].ToString());
                    ddBattleMode.Text = "";
                    if (battleMode != null)
                        ddBattleMode.Text = battleMode.Name;
                }
                // Get Color
				if (currentParameters[1] != null)
					barColor = ColorTranslator.FromHtml(currentParameters[1].ToString());
				// Get Timespan
                if (currentParameters[2] != null)
                    ddTimeSpan.Text = GadgetHelper.GetTimeItemFromName(currentParameters[2].ToString()).Name;
			}
            // Set color for both new and existing
            panel1.BackColor = barColor;
		}


		private void ddBattleMode_Click(object sender, EventArgs e)
		{
			DropDownGrid.Show(ddBattleMode, DropDownGrid.DropDownGridType.List, BattleMode.GetDropDownList(true));
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			if (ddBattleMode.Text == "")
			{
				MsgBox.Show("Please select a battle mode", "Missing battle mode");
			}
            else if (ddTimeSpan.Text == "")
            {
                MsgBox.Show("Please select a time span", "Missing time span");
            }
			else
			{
                BattleMode.Item battleMolde = BattleMode.GetItemFromName(ddBattleMode.Text);
                string paramBattleMode = "";
                if (battleMolde != null)
                    paramBattleMode = battleMolde.SqlName;
                string paramTimeSpan = "";
                GadgetHelper.TimeItem ti = GadgetHelper.GetTimeItemFromName(ddTimeSpan.Text);
                if (ti != null)
                    paramTimeSpan = ti.Name;
				GadgetHelper.newParameters[0] = paramBattleMode;
				Color c = panel1.BackColor;
				GadgetHelper.newParameters[1] = System.Drawing.ColorTranslator.ToHtml(c);
                GadgetHelper.newParameters[2] = paramTimeSpan;
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

        private void ddTimeSpan_Click(object sender, EventArgs e)
        {
            DropDownGrid.Show(ddTimeSpan, DropDownGrid.DropDownGridType.List, GadgetHelper.GetTimeDropDownList());
        }


	}
}
