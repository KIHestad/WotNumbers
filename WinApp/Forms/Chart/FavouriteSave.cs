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
        private bool _newFav = false;
        private int _chartFavId = -1;
        private string _chartFavNameOld = "";

        public FavouriteSave(int chartFavId = -1, string chartFavName = "") // for creating new -> (-1, "")
        {
            InitializeComponent();
            txtUpdateFavName.Text = chartFavName;
            _chartFavId = chartFavId;
            _chartFavNameOld = chartFavName;
            _newFav = (chartFavId == -1);
            ChangeView();
            // Set default result values
            BattleChartHelper.SaveFavouriteSaved = false;
            BattleChartHelper.SaveFavouriteDeleted = false;
            BattleChartHelper.SaveFavouriteNewFavName = "";
            BattleChartHelper.SaveFavouriteNewFavId = -1;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            // Get chart name to save
            string chartFavNameToSave = txtUpdateFavName.Text.Trim();
            if (_newFav)
            {
                chartFavNameToSave = txtNewFavName.Text.Trim();
            }
            
            // Check for valid name if new to be created
            if (chartFavNameToSave == "")
            {
                MsgBox.Show("Please add a favourite name.");
                return;
            }

            // Check if already existing if new chart or changed name on existing fav
            if (_newFav || (!_newFav && _chartFavNameOld != chartFavNameToSave))
            {
                string sql = "SELECT COUNT(id) FROM chartFav WHERE favouriteName = @favouriteName ";
                // Check if editing fav, changed name but already exists
                if (!_newFav)
                {
                    sql += "AND id <> @id ";
                    DB.AddWithValue(ref sql, "@id", _chartFavId, DB.SqlDataType.Int);
                }
                DB.AddWithValue(ref sql, "@favouriteName", chartFavNameToSave, DB.SqlDataType.VarChar);
                DataTable dt = DB.FetchData(sql);
                if (dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
                {
                    MsgBox.Show("A favourit already exists with this name, please select another");
                    return;
                }
            }

            // Save mode
            if (!_newFav)
            {
                // Update current 
                string sqlUpdate = 
                    "UPDATE chartFav " +
                    "SET favouriteName=@favouriteName, battleMode=@battleMode, battleTime=@battleTime, xAxis=@xAxis, bullet=@bullet, spline=@spline " +
                    "WHERE id=@id;";
                DB.AddWithValue(ref sqlUpdate, "@favouriteName", chartFavNameToSave, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlUpdate, "@battleMode", BattleChartHelper.Settings.BattleMode, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlUpdate, "@battleTime", BattleChartHelper.Settings.BattleTime, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlUpdate, "@xAxis", BattleChartHelper.Settings.Xaxis, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlUpdate, "@bullet", BattleChartHelper.Settings.Bullet, DB.SqlDataType.Boolean);
                DB.AddWithValue(ref sqlUpdate, "@spline", BattleChartHelper.Settings.Spline, DB.SqlDataType.Boolean);
                DB.AddWithValue(ref sqlUpdate, "@id", _chartFavId, DB.SqlDataType.VarChar);
                DB.ExecuteNonQuery(sqlUpdate);
                // Done
                BattleChartHelper.SaveFavouriteNewFavName = chartFavNameToSave;
                BattleChartHelper.SaveFavouriteNewFavId = _chartFavId;
                BattleChartHelper.SaveFavouriteSaved = true;
            }
            else
            {
                // Insert new
                string sqlInsert = 
                    "INSERT INTO chartFav (favouriteName, battleMode, battleTime, xAxis, bullet, spline ) " +
                    "VALUES (@favouriteName, @battleMode, @battleTime, @xAxis, @bullet, @spline); ";
                DB.AddWithValue(ref sqlInsert, "@favouriteName", chartFavNameToSave, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlInsert, "@battleMode", BattleChartHelper.Settings.BattleMode, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlInsert, "@battleTime", BattleChartHelper.Settings.BattleTime, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlInsert, "@xAxis", BattleChartHelper.Settings.Xaxis, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlInsert, "@bullet", BattleChartHelper.Settings.Bullet, DB.SqlDataType.Boolean);
                DB.AddWithValue(ref sqlInsert, "@spline", BattleChartHelper.Settings.Spline, DB.SqlDataType.Boolean);
                DB.ExecuteNonQuery(sqlInsert);
                // Get the new id
                string sql = "SELECT Id FROM chartFav WHERE favouriteName = @favouriteName; ";
                DB.AddWithValue(ref sql, "@favouriteName", chartFavNameToSave, DB.SqlDataType.VarChar);
                int chartFavIdNew = Convert.ToInt32(DB.FetchData(sql).Rows[0][0]);
                // Save as new favourite now
                sql = "";
                foreach (BattleChartHelper.BattleChartItem item in BattleChartHelper.CurrentChartView)
                {
                    string newsql =
                        "INSERT INTO chartFavLine (chartFavId, tankId, chartTypeName, use2ndYaxis) " +
                        "VALUES (@chartFavId, @tankId, @chartTypeName, @use2ndYaxis);";
                    DB.AddWithValue(ref newsql, "@chartFavId", chartFavIdNew, DB.SqlDataType.Int);
                    DB.AddWithValue(ref newsql, "@tankId", item.tankId, DB.SqlDataType.Int);
                    DB.AddWithValue(ref newsql, "@chartTypeName", item.chartTypeName, DB.SqlDataType.VarChar);
                    DB.AddWithValue(ref newsql, "@use2ndYaxis", item.use2ndYaxis, DB.SqlDataType.Boolean);
                    sql += newsql;
                }
                DB.ExecuteNonQuery(sql, true, true);
                // Done
                BattleChartHelper.SaveFavouriteNewFavName = chartFavNameToSave;
                BattleChartHelper.SaveFavouriteNewFavId = chartFavIdNew;
                BattleChartHelper.SaveFavouriteSaved = true;
            }
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangeView()
        {
            chkNewFav.Checked = _newFav;
            txtNewFavName.Enabled = _newFav;
            chkUpdateFav.Checked = !_newFav;
            txtUpdateFavName.Enabled = !_newFav;
            btnDelete.Enabled = !_newFav;
        }

        private void chkUpdateFav_Click(object sender, EventArgs e)
        {
            _newFav = false;
            ChangeView();
        }

        private void chkNewFav_Click(object sender, EventArgs e)
        {
            _newFav = true;
            ChangeView();
        }

        private void FavouriteSave_Load(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MsgBox.Button answer = MsgBox.Show("Are you sure you want to delete this favourite?", "Delete Favourite", MsgBox.Type.YesNo);
            if (answer == MsgBox.Button.Yes)
            {
                string sqlDelete = "DELETE FROM chartFavLine WHERE chartFavId = @id; DELETE FROM chartFav WHERE id = @id;";
                DB.AddWithValue(ref sqlDelete, "@id", _chartFavId, DB.SqlDataType.Int);
                DB.ExecuteNonQuery(sqlDelete);
                BattleChartHelper.SaveFavouriteDeleted = true;
                this.Close();
            }
        }
    }
}
