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
    public partial class FavouriteEdit : Form
    {
        string _oldFAvName = "";
        public FavouriteEdit(string favName)
        {
            InitializeComponent();
            txtUpdateFavName.Text = favName;
            _oldFAvName = favName;
            BattleChartHelper.EditFavouriteEdited = "";
            BattleChartHelper.EditFavouriteDeleted = false;

    }

    private void cmdSave_Click(object sender, EventArgs e)
        {
            string favName = txtUpdateFavName.Text.Trim();

            // If name not changed, just ignore and close
            if (favName != _oldFAvName)
            {
                // Check for valid name if new to be created
                if (favName == "")
                {
                    MsgBox.Show("Please add a favourite name");
                    return;
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
                // Update current by deleting it, and save current favlist as new
                string sqlDelete = "UPDATE chartFavourite SET favouriteName = @newfavouriteName WHERE favouriteName=@oldfavouriteName;";
                DB.AddWithValue(ref sqlDelete, "@newfavouriteName", favName, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sqlDelete, "@oldfavouriteName", _oldFAvName, DB.SqlDataType.VarChar);
                DB.ExecuteNonQuery(sqlDelete);
                BattleChartHelper.EditFavouriteEdited = favName;
            }
            this.Close();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            // Update current by deleting it, and save current favlist as new
            MsgBox.Button answer = MsgBox.Show("Are you sure you want to delete this favourite?", "Delete Favourite", MsgBox.Type.YesNo);
            if (answer == MsgBox.Button.Yes)
            {
                string sqlDelete = "DELETE FROM chartFavourite WHERE favouriteName = @favouriteName;";
                DB.AddWithValue(ref sqlDelete, "@favouriteName", _oldFAvName, DB.SqlDataType.VarChar);
                DB.ExecuteNonQuery(sqlDelete);
                BattleChartHelper.EditFavouriteDeleted = true;
                this.Close();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
