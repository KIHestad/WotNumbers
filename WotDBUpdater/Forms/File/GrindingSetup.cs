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
			if (txtCurrentXP.HasFocus)
				CalcGoalXP();
		}

		private void txtGrindGrindXP_TextChanged(object sender, EventArgs e)
		{
			if (txtGrindXP.HasFocus)	
				CalcGoalXP();
		}

		private void CalcGoalXP()
		{
			int curXP = 0;
			if (Int32.TryParse(txtCurrentXP.Text, out curXP))
			{
				int grindXP = 0;
				if (Int32.TryParse(txtGrindXP.Text, out grindXP))
				{
					txtGoalXP.Text = (curXP + grindXP).ToString();
				}
			}
			CalcProgress();
		}

		private void txtGrindGoalXP_TextChanged(object sender, EventArgs e)
		{
			if (txtGoalXP.HasFocus)
			{
				int curXP = 0;
				if (Int32.TryParse(txtCurrentXP.Text, out curXP))
				{
					int goalXP = 0;
					if (Int32.TryParse(txtGoalXP.Text, out goalXP))
					{
						txtGrindXP.Text = (goalXP - curXP).ToString();
					}
				}
			}
			CalcProgress();
		}

		private void btnGrindReset_Click(object sender, EventArgs e)
		{
			Code.MsgBox.Button answer = Code.MsgBox.Show("This resets all values, and ends grinding for this tank", "Reset and end grinding", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{
				txtGrindComment.Text = "";
				txtCurrentXP.Text = "0";
				txtGoalXP.Text = "0";
				txtGrindXP.Text = "0";
				txtGrind2XP.Text = "0";
				txtProgressXP.Text = "0";
				txtRestXP.Text = "0";
				txtBattlesPerDay.Text = "0";
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{

		}

		private void btnSave_Click(object sender, EventArgs e)
		{

		}

		private void btnProgressReset_Click(object sender, EventArgs e)
		{
			Code.MsgBox.Button answer = Code.MsgBox.Show("This resets the progress XP, grinding continues based on grinding values", "Reset Progress", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{
				txtProgressXP.Text = "0";
			}
		}

		private void CalcProgress()
		{
			txtGrind2XP.Text = txtGrindXP.Text;
			int progress = 0;
			Int32.TryParse(txtProgressXP.Text, out progress);
			int progresspercent = 0;
			int grind = 0;
			Int32.TryParse(txtGrind2XP.Text, out grind);
			if (grind > 0)
				progresspercent = progress * 100 / grind; 
			if (progresspercent > 100) 
				progresspercent = 100;
			txtProgressPercent.Text = progresspercent.ToString() + " %";
			int progressrest = grind - progress;
			if (progressrest < 0)
				progressrest = 0;
			txtRestXP.Text = progressrest.ToString();
		}

		private void txtProgressXP_TextChanged(object sender, EventArgs e)
		{
			CalcProgress();
		}

		

	}
}
