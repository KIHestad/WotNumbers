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
        public FavouriteSave(string updateFavouriteName = "")
        {
            InitializeComponent();
            txtUpdateFavName.Text = updateFavouriteName;
            ChangeView(updateFavouriteName == "");
            // Set default result values
            BattleChartHelper.SaveFavouriteSaved = false;
            BattleChartHelper.SaveFavouriteNewFavName = "";
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            string favName = txtUpdateFavName.Text;
            if (chkUpdateFav.Checked)
            {
                // Update current by deleting it, and save current favlist as new
                string sqlDelete = "DELETE FROM chartFavourite WHERE favouriteName = @favouriteName;";
                DB.AddWithValue(ref sqlDelete, "@favouriteName", favName, DB.SqlDataType.VarChar);
                DB.ExecuteNonQuery(sqlDelete);
            }
            else
            {
                // Check for valid name if new to be created
                favName = txtNewFavName.Text.Trim();
                if (favName == "")
                {
                    MsgBox.Show("Please add a favourite name.");
                    return;
                }
            }
            // Check if already existing
            string sql = "SELECT COUNT(id) FROM chartFavourite WHERE favouriteName = @favouriteName;";
            DB.AddWithValue(ref sql, "@favouriteName", favName, DB.SqlDataType.VarChar);
            DataTable dt = DB.FetchData(sql);
            if (dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
            {
                MsgBox.Show("A favourit already exists with this name, please select another");
                return;
            }
            // Save as new favourite now
            sql = "";
            foreach (BattleChartHelper.BattleChartItem item in BattleChartHelper.CurrentChartView)
            {
                string newsql = "INSERT INTO chartFavourite (favouriteName, tankId, chartTypeName) VALUES (@favouriteName, @tankId, @chartTypeName);";
                DB.AddWithValue(ref newsql, "@favouriteName", favName, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref newsql, "@tankId", item.tankId, DB.SqlDataType.Int);
                DB.AddWithValue(ref newsql, "@chartTypeName", item.chartTypeName, DB.SqlDataType.VarChar);
                sql += newsql;
            }
            DB.ExecuteNonQuery(sql, true, true);
            // Done
            BattleChartHelper.SaveFavouriteNewFavName = favName;
            BattleChartHelper.SaveFavouriteSaved = true;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangeView(bool newFav)
        {
            chkNewFav.Checked = newFav;
            txtNewFavName.Enabled = newFav;
            chkUpdateFav.Checked = !newFav;
            txtUpdateFavName.Enabled = !newFav;
        }

        private void chkUpdateFav_Click(object sender, EventArgs e)
        {
            ChangeView(false);
        }

        private void chkNewFav_Click(object sender, EventArgs e)
        {
            ChangeView(true);
        }

        private void FavouriteSave_Load(object sender, EventArgs e)
        {

        }
    }
}
