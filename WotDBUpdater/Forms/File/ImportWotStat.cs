using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WotDBUpdater.Code;

namespace WotDBUpdater.Forms.File
{
	public partial class ImportWotStat : Form
	{
		public ImportWotStat()
		{
			InitializeComponent();
		}

		private void btnOpenWotStatDbFile_Click(object sender, EventArgs e)
		{
			// Select dossier file
			openFileWotStatDbFile.FileName = "*.db";
			openFileWotStatDbFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + 
				"\\WOT Statistics\\Hist_" + Config.Settings.playerName + "\\LastBattle";
			openFileWotStatDbFile.ShowDialog();
			txtWotStatDb.Text = openFileWotStatDbFile.FileName;
		}

		private void btnStartImport_Click(object sender, EventArgs e)
		{
			if (System.IO.File.Exists(txtWotStatDb.Text))
			{
				// Connect to WoT Stat DB
				String dbConnection = "Data Source=" + txtWotStatDb.Text;
				SQLiteConnection cnn = new SQLiteConnection(dbConnection);
				cnn.Open();
				SQLiteCommand mycommand = new SQLiteCommand(cnn);
				// Get count
				String sql = "SELECT COUNT(1) AS rowcount FROM RecentBattles";
				mycommand.CommandText = sql;
				SQLiteDataReader reader = mycommand.ExecuteReader();
				int rowcount = 0;
				foreach (var item in reader)
				{
					rowcount = Convert.ToInt32(reader["rowcount"]);
				}
				progressBarImport.ValueMax = rowcount;
				progressBarImport.Value = 0;
				ImportNow();

				// Done
				cnn.Close();
			}
			else
			{
				Code.MsgBox.Show("Select a file u noob...", "Noob message");
			}
		}

		private void ImportNow()
		{
			string WSDB = txtWotStatDb.Text;

			// SQLite dbconn
			String sqliteDbConnection = "Data Source=" + WSDB;
			SQLiteConnection sqliteConn = new SQLiteConnection(sqliteDbConnection);
			sqliteConn.Open();
			SQLiteCommand sqLiteCmd = new SQLiteCommand(sqliteConn);

			// SQL Server dbconn
			SqlConnection sqlConn = new SqlConnection(Config.DatabaseConnection());
			sqlConn.Open();
			SqlCommand cmd;

			// Fetch WS recentBattles into datatable
			sqLiteCmd.CommandText = "SELECT rbId, rbTankId, rbCountryId, rbBattles, rbKills, rbDamageDealt, rbDamageReceived, rbSpotted, rbCapturePoints, rbDefencePoints, "
								  + "rbSurvived, rbVictory, rbBattleTime, rbShot, rbHits, rbFragList, rbXPReceived, rbBattleMode FROM recentBattles";
			SQLiteDataReader reader = sqLiteCmd.ExecuteReader();
			DataTable recentBattles = new DataTable();
			recentBattles.Load(reader);

			// Write recentBattles to db
			int i = 0;
			progressBarImport.Visible = true;
			while (i < recentBattles.Rows.Count)
			{
				progressBarImport.Value++;
				//lblResult.Text = "Reading id: " + recentBattles.Rows[i]["rbId"].ToString();
				Application.DoEvents();
				Refresh();
				// Get battles first
				int battlesCount = Convert.ToInt32(recentBattles.Rows[i]["rbBattles"]) / 100;
				if (battlesCount == 1) // Only read battles from WoT Statistics for "single" battle results and when player tank is found
				{
					// Then get tankId and playerTankId
					int wstankId = Convert.ToInt32(recentBattles.Rows[i]["rbTankId"]);
					int wsCountryId = Convert.ToInt32(recentBattles.Rows[i]["rbCountryId"]);
					int tankId = TankData.ConvertWs2TankId(wstankId, wsCountryId); // Convert WS tankId + countryID to tank.TankID
					int playerTankId = TankData.GetPlayerTankId(tankId);
					if (playerTankId != 0) // If not player tank is found skip
					{
						int wsId = Convert.ToInt32(recentBattles.Rows[i]["rbId"]);
						int frags = Convert.ToInt32(recentBattles.Rows[i]["rbKills"]) / 100;
						int dmg = Convert.ToInt32(recentBattles.Rows[i]["rbDamageDealt"]) / 100;
						int dmgReceived = Convert.ToInt32(recentBattles.Rows[i]["rbDamageReceived"]) / 100;
						int spotted = Convert.ToInt32(recentBattles.Rows[i]["rbSpotted"]) / 100;
						int cap = Convert.ToInt32(recentBattles.Rows[i]["rbCapturePoints"]) / 100;
						int def = Convert.ToInt32(recentBattles.Rows[i]["rbDefencePoints"]) / 100;
						int survived = Convert.ToInt32(recentBattles.Rows[i]["rbSurvived"]);
						int killed = 1 - survived; // only one battle is imported, then this will work
						int battleSurviveId = 2; // Survived some
						if (recentBattles.Rows[i]["rbSurvived"].ToString() == "0") battleSurviveId = 3; // not survived
						if (recentBattles.Rows[i]["rbSurvived"].ToString() == "1") battleSurviveId = 1; // survived
						int victory = 0; // victory = no
						int draw = 0; // draw = no
						int defeat = 0; // defeat = no
						if (recentBattles.Rows[i]["rbVictory"].ToString() == "0") victory = 1; // defeat = yes
						int battleResultId = 4; // Several
						if (recentBattles.Rows[i]["rbVictory"].ToString() == "0")
						{
							battleResultId = 1; // victory
							victory = 1;
						}
						if (recentBattles.Rows[i]["rbVictory"].ToString() == "1")
						{
							battleResultId = 3; // def
							defeat = 1;
						}
						if (recentBattles.Rows[i]["rbVictory"].ToString() == "2")
						{
							battleResultId = 2; // draw
							draw = 1;
						}
						DateTime battleTime = Convert.ToDateTime("1970-01-01 01:00:00").AddSeconds(Convert.ToInt32(recentBattles.Rows[i]["rbBattleTime"]));
						int shots = Convert.ToInt32(recentBattles.Rows[i]["rbShot"]) / 100;
						int hits = Convert.ToInt32(recentBattles.Rows[i]["rbHits"]) / 100;
						int xp = Convert.ToInt32(recentBattles.Rows[i]["rbXPReceived"]) / 100;
						int mode15 = 0;
						if (recentBattles.Rows[i]["rbBattleMode"].ToString() == "15") mode15 = 1;
						int mode7 = 0;
						if (recentBattles.Rows[i]["rbBattleMode"].ToString() == "7") mode7 = 1;
						// Calc WN8
						int wn8 = Rating.CalculateBattleWn8(tankId, battlesCount, dmg, spotted, frags, def);
						// Calc EFF
						int eff = Rating.CalculateBattleEff(tankId, battlesCount, dmg, spotted, frags, def, cap);
						// Insert or update Battle table
						string sqlInsertBattle = "";
						int battleId = TankData.GetBattleIdForImportedWsBattleFromDB(wsId);
						if (battleId > 0)
							sqlInsertBattle =
							"update battle SET playerTankId=@playerTankId, battlesCount=@battlesCount, frags=@frags, dmg=@dmg, dmgReceived=@dmgReceived, spotted=@spotted, cap=@cap, def=@def, survived=@survived, killed=@killed, " +
							"  battleSurviveId=@battleSurviveId, victory=@victory, draw=@draw, defeat=@defeat, battleResultId=@battleResultId, battleTime=@battleTime, shots=@shots, hits=@hits, xp=@xp, mode15=@mode15, mode7=@mode7, wn8=@wn8, eff=@eff " +
							"where wsId=@wsId ";
						else
							sqlInsertBattle =
							"insert into battle (playerTankId, wsId, battlesCount, frags, dmg, dmgReceived, spotted, cap, def, survived, killed, battleSurviveId, victory, draw, defeat, battleResultId, battleTime, shots, hits, xp, mode15, mode7, wn8, eff) " +
							"values (@playerTankId, @wsId, @battlesCount, @frags, @dmg, @dmgReceived, @spotted, @cap, @def, @survived, @killed,  @battleSurviveId, @victory, @draw, @defeat, @battleResultId, @battleTime, @shots, @hits, @xp, @mode15, @mode7, @wn8, @eff)";
						cmd = new SqlCommand(sqlInsertBattle, sqlConn);
						cmd.Parameters.AddWithValue("@playerTankId", playerTankId);
						cmd.Parameters.AddWithValue("@wsId", wsId);
						cmd.Parameters.AddWithValue("@battlesCount", battlesCount);
						cmd.Parameters.AddWithValue("@frags", frags);
						cmd.Parameters.AddWithValue("@dmg", dmg);
						cmd.Parameters.AddWithValue("@dmgReceived", dmgReceived);
						cmd.Parameters.AddWithValue("@spotted", spotted);
						cmd.Parameters.AddWithValue("@cap", cap);
						cmd.Parameters.AddWithValue("@def", def);
						cmd.Parameters.AddWithValue("@survived", survived);
						cmd.Parameters.AddWithValue("@killed", killed);
						cmd.Parameters.AddWithValue("@battleSurviveId", battleSurviveId);
						cmd.Parameters.AddWithValue("@victory", victory);
						cmd.Parameters.AddWithValue("@draw", draw);
						cmd.Parameters.AddWithValue("@defeat", defeat);
						cmd.Parameters.AddWithValue("@battleResultId", battleResultId);
						cmd.Parameters.AddWithValue("@battleTime", battleTime);
						cmd.Parameters.AddWithValue("@shots", shots);
						cmd.Parameters.AddWithValue("@hits", hits);
						cmd.Parameters.AddWithValue("@xp", xp);
						cmd.Parameters.AddWithValue("@mode15", mode15);
						cmd.Parameters.AddWithValue("@mode7", mode7);
						cmd.Parameters.AddWithValue("@wn8", wn8);
						cmd.Parameters.AddWithValue("@eff", eff);
						cmd.ExecuteNonQuery();
						
						// Get the last battleId if inserted
						if (battleId == 0)
						{
							cmd = new SqlCommand("select max(id) as battleId from battle", sqlConn);
							SqlDataReader myReader = cmd.ExecuteReader();
							while (myReader.Read())
							{
								battleId = Convert.ToInt32(myReader["battleId"]);
							}
							myReader.Close();
						}
						
						// Insert Battle Frags
						string wsfrags = recentBattles.Rows[i]["rbFragList"].ToString(); // Format: 0:0_2;1:0_21;2:0_10;3:2_17;4:1_16
						if (wsfrags != "")
						{
							List<Dossier2db.FragItem> fragList = new List<Dossier2db.FragItem>();
							string[] stringSeparators = new string[] { ";" };
							string[] fragItems = wsfrags.Split(stringSeparators, StringSplitOptions.None);
							foreach (string item in fragItems)
							{
								string newitem = item.Substring(item.IndexOf(":")+1); // Format: 0:0_2 -> remove 2 first char = num sequence
								string[] stringSeparators2 = new string[] { "_" };
								string[] frag = newitem.Split(stringSeparators2, StringSplitOptions.None);
								int wsfragcountryId = Convert.ToInt32(frag[0]);
								int wsfragtankId = Convert.ToInt32(frag[1]);
								int fraggedtankId = TankData.ConvertWs2TankId(wsfragtankId, wsfragcountryId); // Convert WS tankId + countryID to tank.TankID
								bool fraggedTankExist = false;
								foreach (var fragListItem in fragList)
								{
									if (fragListItem.tankId == fraggedtankId)
									{
										fragListItem.fragCount++; // Add another kill on fragged tank
										fraggedTankExist = true;
									}
								}
								if (!fraggedTankExist)
								{
									Dossier2db.FragItem newFraggedTank = new Dossier2db.FragItem();
									newFraggedTank.tankId = fraggedtankId;
									newFraggedTank.fragCount = 1; // Kill on new fragged tank
									fragList.Add(newFraggedTank);
								}
							}
							string sqlInsertBattleFrag = "DELETE FROM battleFrag WHERE battleId = " + battleId.ToString() + Environment.NewLine;
							foreach (var battleFragItem in fragList)
							{
								sqlInsertBattleFrag += 
									"INSERT INTO battleFrag (battleId, fraggedTankId, fragCount) VALUES (" +
									battleId.ToString() + ", " + battleFragItem.tankId + ", " + battleFragItem.fragCount + ") " + Environment.NewLine;
							}
							cmd = new SqlCommand(sqlInsertBattleFrag, sqlConn);
							cmd.ExecuteNonQuery();
						}
					}
				}
				i++; // Next Battle
			}

			// Close db connections
			sqlConn.Close();
			sqliteConn.Close();

			// Done
			Code.MsgBox.Show("Imported " + i.ToString() + " battles.","Import finished");
			this.Close();
		}

		private void ImportWotStat_Load(object sender, EventArgs e)
		{
			
		}


	}
}
