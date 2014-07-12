using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;


namespace WinApp.Forms
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
			if (Code.GrindingHelper.Settings.EveryVictoryFactor > 0)
				lblGrindingParameters.Text = "Every victory: " + Code.GrindingHelper.Settings.EveryVictoryFactor.ToString() + "X";
			else
				lblGrindingParameters.Text = "First victory each day: " + Code.GrindingHelper.Settings.FirstVictoryFactor.ToString() + "X";
		}

		private void GetTankData()
		{
			txtGrindComment.Focus();
			string sql = "SELECT tank.name, gCurrentXP, gGrindXP, gGoalXP, gProgressXP, gBattlesDay, gComment, lastVictoryTime, " +
						"        SUM(playerTankBattle.battles) as battles, SUM(playerTankBattle.wins) as wins, " +
						"        MAX(playerTankBattle.maxXp) AS maxXP, SUM(playerTankBattle.xp) AS totalXP, " + 
						"        SUM(playerTankBattle.xp / NULLIF(playerTankBattle.battles, 0) * playerTankBattle.battleOfTotal) AS avgXP " +
						"FROM    tank INNER JOIN " +
						"        playerTank ON tank.id = playerTank.tankId INNER JOIN " +
						"        playerTankBattle ON playerTank.id = playerTankBattle.playerTankId " +
						"WHERE  (playerTank.id = @playerTankId) " +
						"GROUP BY tank.name, gCurrentXP, gGrindXP, gGoalXP, gProgressXP, gBattlesDay, gComment, lastVictoryTime ";
			DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			DataRow tank = DB.FetchData(sql).Rows[0];
			// Static data
			GrindingSetupTheme.Text = "Tank Grinding Setup - " + tank["name"].ToString();
			txtAvgXP.Text = Convert.ToInt32(tank["avgXP"]).ToString();
			txtMaxXp.Text = tank["maxXP"].ToString();
			txtTotalXP.Text = tank["totalXP"].ToString();
			txtBattles.Text = tank["battles"].ToString();
			txtWins.Text = tank["wins"].ToString();
			if (tank["lastVictoryTime"] == DBNull.Value)
				txtLastVictoryTime.Text = "not recorded";
			else
				txtLastVictoryTime.Text = Convert.ToDateTime(tank["lastVictoryTime"]).ToString("dd.MM.yyyy HH:mm");
			// Add grinding value
			txtGrindComment.Text = tank["gComment"].ToString();
			txtGrindXP.Text = tank["gGrindXP"].ToString();
			txtProgressXP.Text = tank["gProgressXP"].ToString();
			txtRestXP.Text = tank["totalXP"].ToString();
			txtBattlesPerDay.Text = tank["gBattlesDay"].ToString();
			double winRate = Convert.ToDouble(txtWins.Text) / Convert.ToDouble(txtBattles.Text) * 100;
			txtWinRate.Text = Math.Round(winRate, 1).ToString();
			CalcProgress();
		}

		
		private void txtGrindGrindXP_TextChanged(object sender, EventArgs e)
		{
			if (txtGrindXP.HasFocus)
				CalcProgress();
		}
				
		private void btnGrindReset_Click(object sender, EventArgs e)
		{
			Code.MsgBox.Button answer = Code.MsgBox.Show("This resets all values, and ends grinding for this tank", "Reset and end grinding?", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{
				txtGrindComment.Text = "";
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
			string sql = "UPDATE playerTank SET gGrindXP=@GrindXP, gProgressXP=@ProgressXP, " +
						 "                      gBattlesDay=@BattlesDay, gComment=@Comment, gRestXP=@RestXP, gProgressPercent=@ProgressPercent, " +
						 "					    gRestBattles=@RestBattles, gRestDays=@RestDays " +
						 "WHERE id=@id";
			DB.AddWithValue(ref sql, "@GrindXP", txtGrindXP.Text, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@ProgressXP", txtProgressXP.Text, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@ProgressPercent", pbProgressPercent.Value, DB.SqlDataType.Int);
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

		}

		private void CalcProgress(bool Complete = true)
		{
			// Get parameters
			int grind = 0;
			Int32.TryParse(txtGrindXP.Text, out grind);
			int progress = 0;
			Int32.TryParse(txtProgressXP.Text, out progress);
			int btlPerDay = 0;
			Int32.TryParse(txtBattlesPerDay.Text, out btlPerDay);
			// Calc values 
			pbProgressPercent.Value = GrindingHelper.CalcProgressPercent(grind, progress);
			int restXP = GrindingHelper.CalcProgressRestXP(grind, progress);
			txtRestXP.Text = restXP.ToString();
			int realAvgXP = GrindingHelper.CalcRealAvgXP(txtBattles.Text, txtWins.Text, txtTotalXP.Text, txtAvgXP.Text, btlPerDay.ToString());
			txtRealAvgXP.Text = realAvgXP.ToString();
			int restBattles = GrindingHelper.CalcRestBattles(restXP, realAvgXP);
			txtRestBattles.Text = restBattles.ToString();
			int restDays = GrindingHelper.CalcRestDays(restXP, realAvgXP, btlPerDay);
			txtRestDays.Text = restDays.ToString();
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
			Form frm = new Forms.GrindingParameter();
			frm.ShowDialog();
			UpdateGrindParameters();
		}


		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		

	}
}
