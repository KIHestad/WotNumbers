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
		private int playerTankId;
		private bool dataChanged = false;

		public GrindingSetup(int selectedPlayerTankId)
		{
			InitializeComponent();
			playerTankId = selectedPlayerTankId;
		}

		private void GrindingSetup_Load(object sender, EventArgs e)
		{
			GetTankData();
			dataChanged = false;
			UpdateGrindParameters();
		}

		private void UpdateGrindParameters()
		{
			if (Code.Support.GrindingData.Settings.EveryVictoryFactor > 0)
				lblGrindingParameters.Text = "Every victory: " + Code.Support.GrindingData.Settings.EveryVictoryFactor.ToString() + "X";
			else
				lblGrindingParameters.Text = "First victory each day: " + Code.Support.GrindingData.Settings.FirstVictoryFactor.ToString() + "X";
		}

		private void GetTankData()
		{
			string sql = "SELECT        tank.name, gCurrentXP, gGrindXP, gGoalXP, gProgressXP, gBattlesDay, gComment, " +
						"MAX(playerTankBattle.maxXp) AS maxXP, SUM(playerTankBattle.xp) AS totalXP, SUM(playerTankBattle.xp / NULLIF (playerTankBattle.battles, 0) " +
						"						 * playerTankBattle.battleOfTotal) AS avgXP " +
						"	FROM            tank INNER JOIN " +
						"							 playerTank ON tank.id = playerTank.tankId INNER JOIN " +
						"							 playerTankBattle ON playerTank.id = playerTankBattle.playerTankId " +
						"	WHERE        (playerTank.id = @playerTankId) " +
						"	GROUP BY tank.name, gCurrentXP, gGrindXP,gGoalXP,gProgressXP,gBattlesDay,gComment ";
			DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			DataRow tank = DB.FetchData(sql).Rows[0];
			GrindingSetupTheme.Text = "Tank Grinding Setup - " + tank["name"].ToString();
			txtAvgXP.Text = Convert.ToInt32(tank["avgXP"]).ToString();
			txtMaxXp.Text = tank["maxXP"].ToString();
			txtTotalXP.Text = tank["totalXP"].ToString();
			txtGrindComment.Text = tank["gComment"].ToString();
			txtCurrentXP.Text = tank["gCurrentXP"].ToString();
			txtGoalXP.Text = tank["gGoalXP"].ToString();
			txtGrindXP.Text = tank["gGrindXP"].ToString();
			txtProgressXP.Text = tank["gProgressXP"].ToString();
			txtRestXP.Text = tank["totalXP"].ToString();
			txtBattlesPerDay.Text = tank["gBattlesDay"].ToString();
			CalcProgress();
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
			Code.MsgBox.Button answer = Code.MsgBox.Show("This resets all values, and ends grinding for this tank", "Reset and end grinding?", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{
				txtGrindComment.Text = "";
				txtCurrentXP.Text = "0";
				txtGoalXP.Text = "0";
				txtGrindXP.Text = "0";
				txtProgressXP.Text = "0";
				txtRestXP.Text = "0";
				txtBattlesPerDay.Text = "0";
				txtRestDays.Text = "0";
				txtRestBattles.Text = "0";
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (dataChanged)
			{
				MsgBox.Button answer = MsgBox.Show("Do you want to cancel your changes and revert to last saved values?", "Cancel and revert data?", MsgBoxType.OKCancel);
				if (answer == MsgBox.Button.OKButton)
				{
					GetTankData();
					dataChanged = false;
				}
			}
			
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (dataChanged)
			{
				MsgBox.Button answer = MsgBox.Show("Do you want to save your changes?", "Save Data?", MsgBoxType.OKCancel);
				if (answer == MsgBox.Button.OKButton)
				{
					SaveData();
				}
			}
		}

		private void SaveData()
		{
			string sql = "UPDATE playerTank SET gCurrentXP=@CurrentXP, gGrindXP=@GrindXP, gGoalXP=@GoalXP, gProgressXP=@ProgressXP, " +
						 "                      gBattlesDay=@BattlesDay, gComment=@Comment, gRestXP=@RestXP, gProgressPercent=@ProgressPercent, " +
						 "					    gRestBattles=@RestBattles, gRestDays=@RestDays " +
						 "WHERE id=@id";
			DB.AddWithValue(ref sql, "@CurrentXP", txtCurrentXP.Text, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@GrindXP", txtGrindXP.Text, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@GoalXP", txtGoalXP.Text, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@ProgressXP", txtProgressXP.Text, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@ProgressPercent", txtProgressPercent.Text, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@RestXP", txtRestXP.Text, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@RestBattles", txtRestBattles.Text, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@RestDays", txtRestDays.Text, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@BattlesDay", txtBattlesPerDay.Text, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@Comment", txtGrindComment.Text, DB.SqlDataType.VarChar);
			DB.AddWithValue(ref sql, "@id", playerTankId, DB.SqlDataType.Int);
			if (DB.ExecuteNonQuery(sql))
				dataChanged = false;
		}

		private void btnProgressReset_Click(object sender, EventArgs e)
		{
			Code.MsgBox.Button answer = Code.MsgBox.Show("This resets the progress XP, grinding continues based on grinding values", "Reset Progress", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{
				txtProgressXP.Text = "0";
			}
		}

		private void CalcProgress(bool Complete = true)
		{
			if (Complete)
			{
				// Complete calc
				int progress = 0;
				Int32.TryParse(txtProgressXP.Text, out progress);
				int progresspercent = 0;
				int grind = 0;
				Int32.TryParse(txtGrindXP.Text, out grind);
				if (grind > 0)
					progresspercent = (progress * 100) / grind;
				if (progresspercent > 100)
					progresspercent = 100;
				txtProgressPercent.Text = progresspercent.ToString();
				int progressrest = grind - progress;
				if (progressrest < 0)
					progressrest = 0;
				txtRestXP.Text = progressrest.ToString();
				int btlPerDay = 0;
				Int32.TryParse(txtBattlesPerDay.Text, out btlPerDay);
				if (btlPerDay == 0)
				{
					btlPerDay = 1;
				}
				txtRestBattles.Text = (progressrest / Convert.ToInt32(txtAvgXP.Text)).ToString();
				txtRestDays.Text = (progressrest / (Convert.ToInt32(txtAvgXP.Text) * btlPerDay)).ToString();
			}
			else
			{
				// Only rest days
				int btlPerDay = 0;
				Int32.TryParse(txtBattlesPerDay.Text, out btlPerDay);
				if (btlPerDay == 0)
				{
					btlPerDay = 1;
				}
				txtRestDays.Text = (Convert.ToInt32(txtRestXP.Text) / (Convert.ToInt32(txtAvgXP.Text) * btlPerDay)).ToString();
			}
			
			dataChanged = true;
		}

		private void txtProgressXP_TextChanged(object sender, EventArgs e)
		{
			CalcProgress();
		}

		private void txtGrindComment_TextChanged(object sender, EventArgs e)
		{
			dataChanged = true;
		}

		private void txtBattlesPerDay_TextChanged(object sender, EventArgs e)
		{
			CalcProgress(false); 
			dataChanged = true;
		}

		private void GrindingSetup_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (dataChanged)
			{
				MsgBox.Button answer = MsgBox.Show("Data is changed, but not saved. Do you want to save your changes now?", "Save data on closing?", MsgBoxType.OKCancel);
				if (answer == MsgBox.Button.OKButton)
				{
					SaveData();
				}
			}
		}

		private void btnSubtrDay_Click(object sender, EventArgs e)
		{
			int btlPerDay = 0;
			Int32.TryParse(txtBattlesPerDay.Text, out btlPerDay);
			btlPerDay--;
			if (btlPerDay < 1)
				btlPerDay = 1;
			txtBattlesPerDay.Text = btlPerDay.ToString();
		}

		private void btnAddDay_Click(object sender, EventArgs e)
		{
			int btlPerDay = 0;
			Int32.TryParse(txtBattlesPerDay.Text, out btlPerDay);
			btlPerDay++;
			txtBattlesPerDay.Text = btlPerDay.ToString();
		}

		private void btnGrindingParameters_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.GrindingParameter();
			frm.ShowDialog();
			UpdateGrindParameters();
		}

		

	}
}
