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
	public partial class paramBattleMode : FormCloseOnEsc
    {
        bool _saveOk = false;
        int _gadgetId = -1;
		
		public paramBattleMode(int gadgetId = -1)
		{
			InitializeComponent();
			_gadgetId = gadgetId;
		}

		private async void paramBattleMode_Load(object sender, EventArgs e)
		{
			object[] currentParameters = new object[] { null, null, null, null, null };
			if (_gadgetId > -1)
			{
				// Lookup value for current gadget
				string sql = "select * from gadgetParameter where gadgetId=@gadgetId order by paramNum;";
				DB.AddWithValue(ref sql, "@gadgetId", _gadgetId, DB.SqlDataType.Int);
				DataTable dt = await DB.FetchData(sql, Config.Settings.showDBErrors);
				foreach (DataRow dr in dt.Rows)
				{
		 			object paramValue = dr["value"];
					int paramNum = Convert.ToInt32(dr["paramNum"]);
					currentParameters[paramNum] = paramValue;
				}
                if (currentParameters[0] != null)
                    ddBattleMode.Text = BattleMode.GetItemFromSqlName(currentParameters[0].ToString()).Name;
                if (currentParameters[1] != null)
                    ddTimeSpan.Text = GadgetHelper.GetTimeItemFromName(currentParameters[1].ToString()).Name;
			}
		}


		private async void ddBattleMode_Click(object sender, EventArgs e)
		{
            await DropDownGrid.Show(ddBattleMode, DropDownGrid.DropDownGridType.List, BattleMode.GetDropDownList(true));
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
				string paramBattleMode = "";
                BattleMode.Item battleMode = BattleMode.GetItemFromName(ddBattleMode.Text);
                if (battleMode != null)
                    paramBattleMode = battleMode.SqlName;
                string paramTimeSpan = "";
                GadgetHelper.TimeItem ti = GadgetHelper.GetTimeItemFromName(ddTimeSpan.Text);
                if (ti != null)
                    paramTimeSpan = ti.Name;
				GadgetHelper.newParameters[0] = paramBattleMode;
                GadgetHelper.newParameters[1] = paramTimeSpan;
				GadgetHelper.newParametersOK = true;
                _saveOk = true;
                this.Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void paramBattleMode_Paint(object sender, PaintEventArgs e)
		{
			if (BackColor == ColorTheme.FormBackSelectedGadget)
				GadgetHelper.DrawBorderOnGadget(sender, e);
		}

        private async void ddTimeSpan_Click(object sender, EventArgs e)
        {
            await DropDownGrid.Show(ddTimeSpan, DropDownGrid.DropDownGridType.List, GadgetHelper.GetTimeDropDownList());
        }

        private void paramBattleMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_saveOk)
            {
                // Cancel saving
                GadgetHelper.newParameters = new object[] { null, null, null, null, null };
                GadgetHelper.newParametersOK = false;
            }
        }
    }
}
