using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinApp.Forms
{
	public partial class BattleDetail : Form
	{
		private int _battleId;
		
		public BattleDetail(int battleId)
		{
			InitializeComponent();
			_battleId = battleId;
		}

		private void btnTab_Click(object sender, EventArgs e)
		{
			btnEnemyTeam.Checked = false;
			btnOurTeam.Checked = false;
			btnPersonal.Checked = false;
			btnTeams.Checked = false;
			BadButton btn = (BadButton)sender;
			btn.Checked = true;
		}
	}
}
