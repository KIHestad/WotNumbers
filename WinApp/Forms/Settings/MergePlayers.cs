using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms.Settings
{
    public partial class MergePlayers : FormCloseOnEsc
    {
        // Number of battles found to merge
        private int battleCount = 0;
        private DataTable dtPlayer = null;

        public MergePlayers()
        {
            InitializeComponent();
            dtPlayer = DB.FetchData("SELECT name FROM player ORDER BY name");
        }
        
        private void ddPlayerMergeFrom_Click(object sender, EventArgs e)
        {
            Code.DropDownGrid.Show(ddPlayerMergeFrom, Code.DropDownGrid.DropDownGridType.DataTable, dtPlayer);
        }

        private void ddPlayerMergeTo_Click(object sender, EventArgs e)
        {
            Code.DropDownGrid.Show(ddPlayerMergeTo, Code.DropDownGrid.DropDownGridType.DataTable, dtPlayer);
        }

        private void ddPlayerMergeFrom_TextChanged(object sender, EventArgs e)
        {
            // prepare to get data
            lblProgressStatus.Text = "Looking for battles for player...";
            // read battles
            string sql = @"
                SELECT  COUNT(battle.id) AS battleCount
                FROM     battle LEFT OUTER JOIN
                         playerTank ON battle.playerTankId = playerTank.id LEFT OUTER JOIN
                         player ON playerTank.playerId = player.id
                WHERE    player.name = @playerNameAndServer;
            ";
            DB.AddWithValue(ref sql, "@playerNameAndServer", ddPlayerMergeFrom.Text, DB.SqlDataType.VarChar);
            DataTable dt = DB.FetchData(sql);
            // Get result
            battleCount = 0;
            if (dt != null)
            {
                battleCount = Convert.ToInt32(dt.Rows[0]["battleCount"]);
            }
            lblProgressStatus.Text = "Found " + battleCount.ToString() + " battles for player " + ddPlayerMergeFrom.Text;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (battleCount == 0)
            {
                MsgBox.Show("No battles found to merge");
                return;
            }
            if (ddPlayerMergeTo.Text == "")
            {
                MsgBox.Show("No player selected to copy battle data to");
                return;
            }
            if (ddPlayerMergeTo.Text == ddPlayerMergeFrom.Text)
            {
                MsgBox.Show(
                    "Oh dude, cannot copy to the same player as selected to copy from." + Environment.NewLine +
                    Environment.NewLine +
                    "This just dosen't make any sense..." + Environment.NewLine + Environment.NewLine);
                return;
            }
            MsgBox.Button answer = MsgBox.Show(
                "This process cannot be reversed, please ensure you have a backup of your database before starting this procedure." + Environment.NewLine +
                Environment.NewLine +
                "Are you sure you want to merge data?" + Environment.NewLine +
                Environment.NewLine,
                "Start merging data",
                MsgBox.Type.YesNo
            );
            if (answer == MsgBox.Button.Yes)
            {
                string sql = @"
                SELECT        playerFrom.name AS FROM_name, playerTankFrom.tankId AS FROM_tankId, playerTankFrom.id AS FROM_playerTankId, 
                              playerTo.name AS TO_name, playerTankTo.tankId AS TO_tankId, playerTankTo.id AS TO_playerTankId
                FROM          playerTank AS playerTankFrom INNER JOIN
                              player AS playerFrom ON playerTankFrom.playerId = playerFrom.id INNER JOIN
                              playerTank AS playerTankTo ON playerTankFrom.tankId = playerTankTo.tankId INNER JOIN
                              player AS playerTo ON playerTankTo.playerId = playerTo.id
                WHERE        (playerTo.name = @PlayerNameTo) AND (playerFrom.name = @PlayerNameFrom);
                ";
                DB.AddWithValue(ref sql, "@PlayerNameFrom", ddPlayerMergeFrom.Text, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sql, "@PlayerNameTo", ddPlayerMergeTo.Text, DB.SqlDataType.VarChar);
                DataTable dt = DB.FetchData(sql);
                if (dt != null)
                {
                    badProgressBar.ValueMax = dt.Rows.Count;
                    badProgressBar.ValueMin = 0;
                    badProgressBar.Value = 0;
                    lblProgressStatus.Text = "Prepared merging battle data for " + badProgressBar.ValueMax + " tanks";
                    int errorCount = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        sql = "UPDATE battle SET playerTankId = @playerTankIdTo WHERE playerTankId = @playerTankIdFrom;";
                        DB.AddWithValue(ref sql, "@playerTankIdFrom", dr["FROM_playerTankId"], DB.SqlDataType.Int);
                        DB.AddWithValue(ref sql, "@playerTankIdTo", dr["TO_playerTankId"], DB.SqlDataType.Int);
                        if (!await DB.ExecuteNonQueryAsync(sql,false))
                            errorCount++;
                        badProgressBar.Value++;
                        lblProgressStatus.Text = "Merging battle data for tank ID: " + dr["FROM_tankId"].ToString();
                    }
                    if (errorCount == 0)
                    {
                        MsgBox.Show("Data merged successfully");
                    }
                    else
                    {
                        MsgBox.Show("Data merge failed on " + errorCount + " out of " + badProgressBar.ValueMax + " tanks, please check the log file.");
                    }
                }
                ddPlayerMergeFrom.Text = "";
                ddPlayerMergeTo.Text = "";
            }
        }

        
    }
}
