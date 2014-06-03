using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WotDBUpdater.Code;


namespace WotDBUpdater.Forms.File
{
	public partial class GrindingSetup : Form
	{
		private int tankID;

		public GrindingSetup(int selectedTankID)
		{
			InitializeComponent();
			tankID = selectedTankID;
		}

		private void GrindingSetup_Load(object sender, EventArgs e)
		{
			string sql = "SELECT        tank.name, MAX(playerTankBattle.maxXp) AS maxXP, SUM(playerTankBattle.xp) AS totalXP, SUM(playerTankBattle.xp / NULLIF (playerTankBattle.battles, 0) " +
						"						 * playerTankBattle.battleOfTotal) AS avgXP " +
						"	FROM            tank INNER JOIN " +
						"							 playerTank ON tank.id = playerTank.tankId INNER JOIN " +
						"							 playerTankBattle ON playerTank.id = playerTankBattle.playerTankId " +
						"	WHERE        (tank.id = @tankId) AND (playerTank.playerId = @playerId) " +
						"	GROUP BY tank.name ";
			DB.AddWithValue(ref sql, "@tankId", tankID, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataRow tank = DB.FetchData(sql).Rows[0];
			GrindingSetupTheme.Text = "Tank Grinding Setup - " + tank["name"].ToString() + "(" + tankID  +")";
			txtAvgXP.Text = tank["avgXP"].ToString();
			txtMaxXp.Text = tank["maxXP"].ToString();
			txtTotalXP.Text = tank["totalXP"].ToString();

			//DataRow tankXP = 

			txtGrindComment.Focus();
		}

		private void txtGrindCurrentXP_TextChanged(object sender, EventArgs e)
		{
			if (txtGrindCurrentXP.HasFocus)
				CalcGoalXP();
		}

		private void txtGrindGrindXP_TextChanged(object sender, EventArgs e)
		{
			if (txtGrindGrindXP.HasFocus)	
				CalcGoalXP();
		}

		private void CalcGoalXP()
		{
			int curXP = 0;
			if (Int32.TryParse(txtGrindCurrentXP.Text, out curXP))
			{
				int grindXP = 0;
				if (Int32.TryParse(txtGrindGrindXP.Text, out grindXP))
				{
					txtGrindGoalXP.Text = (curXP + grindXP).ToString();
				}
			}
		}

		private void txtGrindGoalXP_TextChanged(object sender, EventArgs e)
		{
			if (txtGrindGoalXP.HasFocus)
			{
				int curXP = 0;
				if (Int32.TryParse(txtGrindCurrentXP.Text, out curXP))
				{
					int goalXP = 0;
					if (Int32.TryParse(txtGrindGoalXP.Text, out goalXP))
					{
						txtGrindGrindXP.Text = (goalXP - curXP).ToString();
					}
				}
			}
		}

		

		

	}
}
