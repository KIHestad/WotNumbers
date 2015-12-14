using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;


namespace WinApp.Forms
{
	public partial class GrindingSetup : Form
	{
		private int playerTankId;
		private bool dataChanged = false;
		private bool _init = true;

		public GrindingSetup(int selectedPlayerTankId)
		{
			InitializeComponent();
			playerTankId = selectedPlayerTankId;
		}

		private void GrindingSetup_Load(object sender, EventArgs e)
		{
			GetTankData();
			dataChanged = false;
		}

		private void GetTankData()
		{
			_init = true;
			txtGrindComment.Focus();
			string sql = "SELECT tank.name, gCurrentXP, gGrindXP, gGoalXP, gProgressXP, gBattlesDay, gComment, tank.id as tankId " +
						 "FROM    tank INNER JOIN " +
						 "        playerTank ON tank.id = playerTank.tankId " +
						 "WHERE  (playerTank.id = @playerTankId) ";
			DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				DataRow tank = dt.Rows[0];
				// Static data
				GrindingSetupTheme.Text = "Tank Grinding Setup - " + tank["name"].ToString();
				// Add grinding value
				txtGrindComment.Text = tank["gComment"].ToString();
				txtGrindXP.Text = tank["gGrindXP"].ToString();
				txtProgressXP.Text = tank["gProgressXP"].ToString();
				txtBattlesPerDay.Text = tank["gBattlesDay"].ToString();
				int tankId = Convert.ToInt32(tank["tankId"]);
				tankPic.Image = ImageHelper.GetTankImage(tankId, ImageHelper.TankImageType.LargeImage);
			}
			sql = "SELECT    SUM(playerTankBattle.battles) as battles, SUM(playerTankBattle.wins) as wins, " +
					"        MAX(playerTankBattle.maxXp) AS maxXP, SUM(playerTankBattle.xp) AS totalXP, " +
					"        SUM(playerTankBattle.xp) / SUM (playerTankBattle.battles) AS avgXP " +
					"FROM    tank INNER JOIN " +
					"        playerTank ON tank.id = playerTank.tankId INNER JOIN " +
					"        playerTankBattle ON playerTank.id = playerTankBattle.playerTankId " +
					"WHERE  (playerTank.id = @playerTankId) ";
			DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0 && dt.Rows[0]["battles"] != DBNull.Value)
			{
				DataRow tank = dt.Rows[0];
				txtAvgXP.Text = Convert.ToInt32(tank["avgXP"]).ToString();
				txtMaxXp.Text = tank["maxXP"].ToString();
				txtTotalXP.Text = tank["totalXP"].ToString();
				txtBattles.Text = tank["battles"].ToString();
				txtWins.Text = tank["wins"].ToString();
				double winRate = Convert.ToDouble(txtWins.Text) / Convert.ToDouble(txtBattles.Text) * 100;
				txtWinRate.Text = Math.Round(winRate, 1).ToString();
			}
			else
			{
				txtAvgXP.Text = "0";
				txtMaxXp.Text = "0";
				txtTotalXP.Text = "0";
				txtBattles.Text = "0";
				txtWins.Text = "0";
				txtWinRate.Text = "0";
			}
			_init = false;
			CalcProgress();
		}

		private void txtGrindGrindXP_TextChanged(object sender, EventArgs e)
		{
			if (!_init)
			{
				int i;
				bool ok = Int32.TryParse(txtGrindXP.Text, out i);
				if (((ok && i > 0) || txtProgressXP.Text != "0") && txtBattlesPerDay.Text == "0")
					txtBattlesPerDay.Text = "1";
				else if (txtProgressXP.Text == "0" && txtGrindXP.Text == "0")
					txtBattlesPerDay.Text = "0";
				CalcProgress();
				dataChanged = true;
			}
		}

		private void txtProgressXP_TextChanged(object sender, EventArgs e)
		{
			if (!_init)
			{
				int i;
				bool ok = Int32.TryParse(txtProgressXP.Text, out i);
				if (((ok && i > 0) || txtGrindXP.Text != "0") && txtBattlesPerDay.Text == "0")
					txtBattlesPerDay.Text = "1";
				else if (txtProgressXP.Text == "0" && txtGrindXP.Text == "0")
					txtBattlesPerDay.Text = "0";
				CalcProgress();
				dataChanged = true;
			}
		}

		private void txtGrindComment_TextChanged(object sender, EventArgs e)
		{
			dataChanged = true;
		}

		private void txtBattlesPerDay_TextChanged(object sender, EventArgs e)
		{
			if (!_init)
			{
				CalcProgress(false);
				dataChanged = true;
			}
		}

		private void btnGrindReset_Click(object sender, EventArgs e)
		{
            Code.MsgBox.Button answer = Code.MsgBox.Show("This resets all values, and ends grinding for this tank", "Reset and end grinding?", MsgBox.Type.OKCancel, this);
			if (answer == MsgBox.Button.OK)
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
                MsgBox.Button answer = MsgBox.Show("Do you want to cancel your changes and revert to last saved values?", "Cancel and revert data?", MsgBox.Type.OKCancel, this);
				if (answer == MsgBox.Button.OK)
				{
					GetTankData();
					dataChanged = false;
				}
			}
			
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (CheckValidData())
			{
				if (dataChanged)
				{
                    MsgBox.Button answer = MsgBox.Show("Do you want to save your changes?", "Save Data?", MsgBox.Type.OKCancel, this);
					if (answer == MsgBox.Button.OK)
					{
						SaveData();
					}
				}
			}
		}

		private bool CheckValidData()
		{
			bool ok = true;
			int grindXP = 0;
			int ProgressXP = 0;
			int btlprDay = 0;
			if (txtGrindXP.Text == "") txtGrindXP.Text = "0";
			if (txtProgressXP.Text == "") txtProgressXP.Text = "0";
			if (txtBattlesPerDay.Text == "") txtBattlesPerDay.Text = "0";
			ok = Int32.TryParse(txtGrindXP.Text, out grindXP);
			if (ok) Int32.TryParse(txtProgressXP.Text, out ProgressXP);
			if (ok) Int32.TryParse(txtBattlesPerDay.Text, out btlprDay);
			if (!ok)
				MsgBox.Show("Illegal character found, please only enter numberic values without decimals", "Illegal character in text box", this);
			else
			{
				if ((grindXP > 0 || ProgressXP > 0) && btlprDay == 0)
					txtBattlesPerDay.Text = "1";
			}
			return ok;
		}

		private void SaveData()
		{
			if (CheckValidData())
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

		private void GrindingSetup_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (dataChanged)
			{
                MsgBox.Button answer = MsgBox.Show("Data is changed, but not saved. Do you want to save your changes now?", "Save data on closing?", MsgBox.Type.OKCancel, this);
				if (answer == MsgBox.Button.OK)
				{
					if (!CheckValidData())
					{
						e.Cancel = true;
					}
					else					
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

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txtGrindXP_KeyPress(object sender, KeyPressEventArgs e)
		{
			// Check for a naughty character in the KeyDown event.
			bool validChar = System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]");
			bool backSpace = (e.KeyChar == (char)8);
			// Stop the character from being entered into the control since it is illegal.
			e.Handled = !(validChar || backSpace);
		}

		private void txtProgressXP_KeyPress(object sender, KeyPressEventArgs e)
		{
			// Check for a naughty character in the KeyDown event.
			bool validChar = System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]");
			bool backSpace = (e.KeyChar == (char)8);
			// Stop the character from being entered into the control since it is illegal.
			e.Handled = !(validChar || backSpace);
		}

		private void txtBattlesPerDay_KeyPress(object sender, KeyPressEventArgs e)
		{
			// Check for a naughty character in the KeyDown event.
			bool validChar = System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]");
			bool backSpace = (e.KeyChar == (char)8);
			// Stop the character from being entered into the control since it is illegal.
			e.Handled = !(validChar || backSpace);
		}



	}
}
