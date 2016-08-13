using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class ManualCheckNewBattles : FormCloseOnEsc
    {
		public ManualCheckNewBattles()
		{
			InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			RunBattleCheckHelper.CurrentBattleCheckMode = RunBattleCheckHelper.RunBattleCheckMode.Cancelled;
			this.Close();
		}

		private void chkRun_Click(object sender, EventArgs e)
		{
			RunBattleCheckHelper.CurrentBattleCheckMode = RunBattleCheckHelper.RunBattleCheckMode.NormalMode;
			if (chkForceUpdateAll.Checked)
				RunBattleCheckHelper.CurrentBattleCheckMode = RunBattleCheckHelper.RunBattleCheckMode.ForceUpdateAll;
			this.Close();
		}
	}
}
