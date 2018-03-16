using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;
using WinApp.Code.FormView;

namespace WinApp.Forms.Chart
{
    public partial class FavouriteSave : Form
    {
        private int _chartFavId = -1;
        private string _chartFavNameOld = "";

        public FavouriteSave(int chartFavId = -1, string chartFavName = "") // for creating new -> (-1, "")
        {
            InitializeComponent();
            _chartFavId = chartFavId;
            _chartFavNameOld = chartFavName;
            bool newFav = (chartFavId == -1);
            // Preselect save or update checkbox
            chkNewFav.Checked = newFav;
            chkUpdateFav.Checked = !newFav;
            // Only possible to edit existing if not new fav is to be added
            if (newFav)
            {
                chkUpdateFav.Enabled = false;
                txtUpdateFavName.Text = "";
                txtUpdateFavName.Enabled = false;
            }
            else
            {
                txtUpdateFavName.Text = chartFavName;
                txtNewFavName.Enabled = false;
            }
            // Set default result values
            BattleChartHelper.SaveFavouriteSaved = false;
            BattleChartHelper.SaveFavouriteDeleted = false;
            BattleChartHelper.SaveFavouriteNewFavName = "";
            BattleChartHelper.SaveFavouriteNewFavId = -1;
        }

        private async void cmdSave_Click(object sender, EventArgs e)
        {
            // Get chart name to save
            string chartFavNameToSave = txtUpdateFavName.Text.Trim();
            if (chkNewFav.Checked)
                chartFavNameToSave = txtNewFavName.Text.Trim();

            // Check for valid name if new to be created
            if (chartFavNameToSave == "")
            {
                MsgBox.Show("Please add a favourite name.");
                return;
            }

            // Check if chart name is valid, if new chart or if changing name to anything than existing
            if (chkNewFav.Checked || chkUpdateFav.Checked && _chartFavNameOld != chartFavNameToSave)
            {
                string sqlCheckName = "SELECT COUNT(id) FROM chartFav WHERE favouriteName = @favouriteName ";
                DB.AddWithValue(ref sqlCheckName, "@favouriteName", chartFavNameToSave, DB.SqlDataType.VarChar);
                DataTable dt = DB.FetchData(sqlCheckName);
                if (dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
                {
                    MsgBox.Show("A favourit already exists with this name, please select another");
                    return;
                }
            }

            // Update or create new
            if (chkUpdateFav.Checked)
            {
                // Update current and remove chart lines, to be updater later
                string sqlUpdate =
                    "UPDATE chartFav " +
                    "SET favouriteName=@favouriteName, battleMode=@battleMode, battleTime=@battleTime, xAxis=@xAxis, bullet=@bullet, spline=@spline " +
                    "WHERE id=@id;" +
                    "DELETE FROM chartFavLine WHERE chartFavId = @chartFavId;";
                DB.AddWithValue(ref sqlUpdate, "@favouriteName", chartFavNameToSave, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlUpdate, "@battleMode", BattleChartHelper.Settings.BattleMode, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlUpdate, "@battleTime", BattleChartHelper.Settings.BattleTime, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlUpdate, "@xAxis", BattleChartHelper.Settings.Xaxis, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlUpdate, "@bullet", BattleChartHelper.Settings.Bullet, DB.SqlDataType.Boolean);
                DB.AddWithValue(ref sqlUpdate, "@spline", BattleChartHelper.Settings.Spline, DB.SqlDataType.Boolean);
                DB.AddWithValue(ref sqlUpdate, "@chartFavId", _chartFavId, DB.SqlDataType.Int);
                DB.AddWithValue(ref sqlUpdate, "@id", _chartFavId, DB.SqlDataType.Int);
                await DB.ExecuteNonQueryAsync(sqlUpdate);
                // Done
                BattleChartHelper.SaveFavouriteNewFavId = _chartFavId;
            }
            else
            {
                // Insert new chart fav, lines updated later
                string sqlInsert =
                    "INSERT INTO chartFav (favouriteName, battleMode, battleTime, xAxis, bullet, spline ) " +
                    "VALUES (@favouriteName, @battleMode, @battleTime, @xAxis, @bullet, @spline); ";
                DB.AddWithValue(ref sqlInsert, "@favouriteName", chartFavNameToSave, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlInsert, "@battleMode", BattleChartHelper.Settings.BattleMode, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlInsert, "@battleTime", BattleChartHelper.Settings.BattleTime, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlInsert, "@xAxis", BattleChartHelper.Settings.Xaxis, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlInsert, "@bullet", BattleChartHelper.Settings.Bullet, DB.SqlDataType.Boolean);
                DB.AddWithValue(ref sqlInsert, "@spline", BattleChartHelper.Settings.Spline, DB.SqlDataType.Boolean);
                await DB.ExecuteNonQueryAsync(sqlInsert);
                // Get the new id
                sqlInsert = "SELECT Id FROM chartFav WHERE favouriteName = @favouriteName; ";
                DB.AddWithValue(ref sqlInsert, "@favouriteName", chartFavNameToSave, DB.SqlDataType.VarChar);
                _chartFavId = Convert.ToInt32(DB.FetchData(sqlInsert).Rows[0][0]);
            }
            // Now add chart lines
            string sql = "";
            foreach (BattleChartHelper.BattleChartItem item in BattleChartHelper.CurrentChartView)
            {
                string newsql =
                    "INSERT INTO chartFavLine (chartFavId, tankId, chartTypeName, use2ndYaxis) " +
                    "VALUES (@chartFavId, @tankId, @chartTypeName, @use2ndYaxis);";
                DB.AddWithValue(ref newsql, "@chartFavId", _chartFavId, DB.SqlDataType.Int);
                DB.AddWithValue(ref newsql, "@tankId", item.tankId, DB.SqlDataType.Int);
                DB.AddWithValue(ref newsql, "@chartTypeName", item.chartTypeName, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref newsql, "@use2ndYaxis", item.use2ndYaxis, DB.SqlDataType.Boolean);
                sql += newsql;
            }
            await DB.ExecuteNonQueryAsync(sql, true, true);
            // Done
            BattleChartHelper.SaveFavouriteNewFavName = chartFavNameToSave;
            BattleChartHelper.SaveFavouriteNewFavId = _chartFavId;
            BattleChartHelper.SaveFavouriteSaved = true;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkUpdateFav_Click(object sender, EventArgs e)
        {
            chkUpdateFav.Checked = true;
            chkNewFav.Checked = false;
            txtNewFavName.Enabled = false;
            txtUpdateFavName.Enabled = true;
        }

        private void chkNewFav_Click(object sender, EventArgs e)
        {
            chkNewFav.Checked = true;
            chkUpdateFav.Checked = false;
            txtNewFavName.Enabled = true;
            txtUpdateFavName.Enabled = false;
        }

             
    }
}
