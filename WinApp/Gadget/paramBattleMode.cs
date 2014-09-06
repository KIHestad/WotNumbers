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
	public partial class paramBattleMode : Form
	{
		int _gadgetId = -1;
		
		public paramBattleMode(int gadgetId = -1)
		{
			InitializeComponent();
			_gadgetId = gadgetId;
		}

		private void paramBattleMode_Load(object sender, EventArgs e)
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
				switch (currentParameters[0].ToString())
				{
					case "15": ddBattleMode.Text = "Random / TC"; break;
					case "7": ddBattleMode.Text = "Team"; break;
					case "Historical": ddBattleMode.Text = "Historical"; break;
					case "Skirmishes": ddBattleMode.Text = "Skirmishes"; break;
					case "": ddBattleMode.Text = "All Modes"; break;
				}
			}
		}


		private void ddBattleMode_Click(object sender, EventArgs e)
		{
			DropDownGrid.Show(ddBattleMode, DropDownGrid.DropDownGridType.List, "Random / TC,Team,Historical,Skirmishes, All Modes");
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
					case "All Modes":   param = ""; break;
				}
				GadgetHelper.newParameters[0] = param;
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


	}
}
