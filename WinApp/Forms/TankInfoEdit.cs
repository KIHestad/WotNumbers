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
using WinApp.Code.Rating;

namespace WinApp.Forms
{
    public partial class TankInfoEdit : Form
    {
        private int _tankId = 0;
        private TankHelper.BasicTankInfo _defaultTankDetails = new TankHelper.BasicTankInfo();
        private WN8.RatingParametersWN8 _defaultWN8 = new WN8.RatingParametersWN8();
        private WN9.RatingParametersWN9 _defaultWN9 = new WN9.RatingParametersWN9();
        private string _nations = "";
        private string _tankTypes = "";
        private bool _WN8Changed = false;
        private bool _WN9Changed = false;
        private bool _saveOnClosing = false;
        

        public TankInfoEdit(int tankID)
        {
            InitializeComponent();
            _tankId = tankID;
        }

        private async void TankInfoEdit_Load(object sender, EventArgs e)
        {
            txtID.Text = _tankId.ToString();
            DataRow dr = await TankHelper.GetTankInfo(_tankId);
            if (dr != null)
            {
                // Get default values
                _defaultTankDetails.name = dr["name"].ToString();
                _defaultTankDetails.short_name = dr["short_name"].ToString();
                _defaultTankDetails.nation = dr["countryName"].ToString();
                _defaultTankDetails.tankType = dr["tankTypeName"].ToString();
                _defaultTankDetails.tier = dr["tier"].ToString();
                _defaultTankDetails.customTankInfo = Convert.ToBoolean(dr["customTankInfo"]);
                ShowTankDetails();
                // WN8
                ShowTankWN8(dr);
                _WN8Changed = false;
                // WN9
                ShowTankWN9(dr);
                _WN9Changed = false;
                // Get dropdown values
                DataTable dt = await DB.FetchData("SELECT name FROM tankType ORDER BY id");
                foreach (DataRow dr1 in dt.Rows)
                    _tankTypes += dr1["name"].ToString() + ",";
                if (_tankTypes.Length > 0)
                    _tankTypes = _tankTypes.Substring(0, _tankTypes.Length - 1);
                dt = await DB.FetchData("SELECT name FROM country ORDER BY sortOrder");
                foreach (DataRow dr2 in dt.Rows)
                    _nations += dr2["name"].ToString() + ",";
                if (_nations.Length > 0)
                    _nations = _nations.Substring(0, _nations.Length - 1);

            }
            else
            {
                btnSave.Enabled = false;
            }
        }

        private void ShowTankDetails()
        {
            txtName.Text = _defaultTankDetails.name;
            txtShortName.Text = _defaultTankDetails.short_name;
            txtTier.Text = _defaultTankDetails.tier;
            ddNation.Text = _defaultTankDetails.nation;
            ddTankType.Text = _defaultTankDetails.tankType;
        }

        private void ShowTankWN8(DataRow dr)
        {
            txtWN8dmg.Text = dr["expDmg"].ToString();
            txtWN8wr.Text = dr["expWR"].ToString();
            txtWN8spot.Text = dr["expSpot"].ToString();
            txtWN8frags.Text = dr["expFrags"].ToString();
            txtWN8def.Text = dr["expDef"].ToString();
        }

        private void ShowTankWN9(DataRow dr)
        {
            txtWN9mmrange.Text = dr["mmrange"].ToString();
            txtWN9exp.Text = dr["wn9exp"].ToString();
            txtWN9scale.Text = dr["wn9scale"].ToString();
            txtWN9nerf.Text = dr["wn9nerf"].ToString();
        }

        private bool CheckIfEditedTankDetails()
        {
            return (
                txtName.Text != _defaultTankDetails.name ||
                txtShortName.Text != _defaultTankDetails.short_name ||
                txtTier.Text != _defaultTankDetails.tier ||
                ddNation.Text != _defaultTankDetails.nation ||
                ddTankType.Text != _defaultTankDetails.tankType
            );
        }

        private async Task<int> GetIdFromName(string tableName, string nameValue)
        {
            string sql = "SELECT id FROM " + tableName + " WHERE name=@name";
            DB.AddWithValue(ref sql, "@name", nameValue, DB.SqlDataType.VarChar);
            DataTable dt = await DB.FetchData(sql);
            int nationId = -1;
            if (dt.Rows.Count > 0)
                nationId = Convert.ToInt32(dt.Rows[0]["id"]);
            return nationId;
        }

        private async Task<bool> TankDetailsSave()
        {
            try
            {
                // Check for valid values
                int blankfields = 0;
                if (txtName.Text.Trim() == "") blankfields++;
                if (txtShortName.Text.Trim() == "") blankfields++;
                if (txtTier.Text.Trim() == "") blankfields++;
                if (ddNation.Text.Trim() == "") blankfields++;
                if (ddTankType.Text.Trim() == "") blankfields++;
                if (blankfields > 0 && blankfields < 5)
                {
                    MsgBox.Show("Missing values", "Saving cancelled");
                    return false;
                }
                // Update default values from form
                _defaultTankDetails.name = txtName.Text.Trim();
                _defaultTankDetails.short_name = txtShortName.Text.Trim();
                _defaultTankDetails.tier = txtTier.Text.Trim();
                _defaultTankDetails.nation = ddNation.Text;
                _defaultTankDetails.tankType = ddTankType.Text;
                // Save now
                string sql = 
                    "UPDATE tank " +
                    "SET name=@name, short_name=@short_name, tier=@tier, countryId=@countryId, tankTypeId=@tankTypeId, customTankInfo=@customTankInfo " +
                    "WHERE id=@id";
                // Tank Details
                DB.AddWithValue(ref sql, "@name", _defaultTankDetails.name, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sql, "@short_name", _defaultTankDetails.short_name, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sql, "@tier", _defaultTankDetails.tier, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@countryId", await GetIdFromName("country", _defaultTankDetails.nation), DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@tankTypeId", await GetIdFromName("tankType", _defaultTankDetails.tankType), DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@customTankInfo", _defaultTankDetails.customTankInfo, DB.SqlDataType.Boolean);
                DB.AddWithValue(ref sql, "@id", _tankId, DB.SqlDataType.Int);
                await DB.ExecuteNonQuery(sql);
                // Update tankinfo
                await TankHelper.GetTankList();
                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Show("Error saving tank details, probably caused by illegal values." + Environment.NewLine + Environment.NewLine + ex.Message, "Error saving");
                return false;
            } 
        }

        private async Task<bool> TankRatingsSave()
        {
            try
            {
                // Check for valid values
                int blankfields = 0;
                // WN8
                if (txtWN8dmg.Text.Trim() == "") blankfields++;
                if (txtWN8wr.Text.Trim() == "") blankfields++;
                if (txtWN8spot.Text.Trim() == "") blankfields++;
                if (txtWN8frags.Text.Trim() == "") blankfields++;
                if (txtWN8def.Text.Trim() == "") blankfields++;
                if (blankfields > 0 && blankfields < 5)
                {
                    MsgBox.Show("Missing WN8 values", "Saving cancelled");
                    return false;
                }
                blankfields = 0;
                // WN9
                if (txtWN9exp.Text.Trim() == "") blankfields++;
                if (txtWN9mmrange.Text.Trim() == "") blankfields++;
                if (txtWN9nerf.Text.Trim() == "") blankfields++;
                if (txtWN9scale.Text.Trim() == "") blankfields++;
                if (blankfields > 0 && blankfields < 4)
                {
                    MsgBox.Show("Missing WN9 values", "Saving cancelled");
                    return false;
                }
                // Save now
                string sql =
                "UPDATE tank " +
                "SET expDmg = @expDmg, expWR = @expWR, expSpot = @expSpot, expFrags = @expFrags, expDef = @expDef, " +
                "  mmrange=@mmrange, wn9exp=@wn9exp, wn9scale=@wn9scale, wn9nerf=@wn9nerf " +
                "WHERE id=@id";
                // WN8
                DB.AddWithValue(ref sql, "@expDmg", txtWN8dmg.Text.Trim(), DB.SqlDataType.Float);
                DB.AddWithValue(ref sql, "@expWR", txtWN8wr.Text.Trim(), DB.SqlDataType.Float);
                DB.AddWithValue(ref sql, "@expSpot", txtWN8spot.Text.Trim(), DB.SqlDataType.Float);
                DB.AddWithValue(ref sql, "@expFrags", txtWN8frags.Text.Trim(), DB.SqlDataType.Float);
                DB.AddWithValue(ref sql, "@expDef", txtWN8def.Text.Trim(), DB.SqlDataType.Float);
                // WN9
                DB.AddWithValue(ref sql, "@mmrange", txtWN9mmrange.Text.Trim(), DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@wn9exp", txtWN9exp.Text.Trim(), DB.SqlDataType.Float);
                DB.AddWithValue(ref sql, "@wn9scale", txtWN9scale.Text.Trim(), DB.SqlDataType.Float);
                DB.AddWithValue(ref sql, "@wn9nerf", txtWN9nerf.Text.Trim(), DB.SqlDataType.Float);
                // id
                DB.AddWithValue(ref sql, "@id", _tankId, DB.SqlDataType.Int);
                await DB.ExecuteNonQuery(sql);
                // Update tankinfo
                await TankHelper.GetTankList();
                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Show("Error saving expected values, probably caused by illegal values." + Environment.NewLine + Environment.NewLine + ex.Message, "Error saving");
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save when closing form
            _saveOnClosing = true;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close form
            this.Close();
        }

        private async void TankInfoEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Check if tank details are changed
            _defaultTankDetails.customTankInfo = CheckIfEditedTankDetails();
            // Check for saving if changes are made and just closing form without pressing save
            if (!_saveOnClosing && (_defaultTankDetails.customTankInfo || _WN8Changed || _WN9Changed))
            {
                MsgBox.Button answer = MsgBox.Show("Do you want to save changes?", "Save changes", MsgBox.Type.YesNo);
                if (answer == MsgBox.Button.Yes)
                    _saveOnClosing = true;
            }

            // Save now if saving is selected and any change found
            if (_saveOnClosing)
            {
                bool saveOK = true;
                if (_defaultTankDetails.customTankInfo)
                    saveOK = await TankDetailsSave();
                if (saveOK && (_WN8Changed || _WN9Changed))
                    saveOK = await TankRatingsSave();
                e.Cancel = !saveOK; // Cancel closing form if any saving has failed
            }
        }

        private async void ddNation_Click(object sender, EventArgs e)
        {
            await Code.DropDownGrid.Show(ddNation, Code.DropDownGrid.DropDownGridType.List, _nations);
        }

        private async void ddTankType_Click(object sender, EventArgs e)
        {
            await Code.DropDownGrid.Show(ddTankType, Code.DropDownGrid.DropDownGridType.List, _tankTypes);
        }
       

        private async void btnReset_Click(object sender, EventArgs e)
        {
            TankHelper.BasicTankInfo getTankInfoFromApi = await ImportWotApi2DB.ImportTanksDetails(this, _tankId);
            if (getTankInfoFromApi.message != null)
            {
                MsgBox.Show(getTankInfoFromApi.message + Environment.NewLine, "Tank not found using Wargaming API");
            }
            else
            {
                //MsgBox.Button answer = MsgBox.Show("Get default values from Wargaming API and save the tankInfo now", "Get Default values", MsgBox.Type.OKCancel);
                //if (answer == MsgBox.Button.OK)
                {
                    _defaultTankDetails = getTankInfoFromApi;
                    ShowTankDetails();
                    if (await TankDetailsSave())
                        MsgBox.Show("Default tank info retrieved from Wargaming API", "Get Default Values");
                }
            }
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            string msg =
                "When running from the settings menu: 'Download and Update Tank...' the custom tank details will not be overwritten. " +
                "Pressing the button 'Get Default Values' the tank details will be reset back to default values using Wargaming API." +
                Environment.NewLine + Environment.NewLine +
                "If editing WN8 and WN9 expected values, these will be reset to default using 'Download and Update Tank...'. " +
                "The only exception is for tanks where these values are missing (new tanks not got any official expected values yet'";
            MsgBox.Show(msg, "Edit Tank Info Help");
        }
                
        private void WN8Changed(object sender, EventArgs e)
        {
            _WN8Changed = true;
        }

        private void WN9Changed(object sender, EventArgs e)
        {
            _WN9Changed = true;
        }


        private async void btnResetWN8_Click(object sender, EventArgs e)
        {
            string result = await ImportWN8Api2DB.UpdateWN8(this, _tankId); // result is empty if any error
            if (result != "")
            {
                await TankHelper.GetTankList();
                DataRow dr = await TankHelper.GetTankInfo(_tankId);
                ShowTankWN8(dr);
                _WN8Changed = false;
                MsgBox.Show(result, "Result getting WN8 expected values");
            }
            
        }

        private async void btnResetWN9_Click(object sender, EventArgs e)
        {
            string result = await ImportWN9Api2DB.UpdateWN9(this, _tankId); // result is empty if any error
            if (result != "")
            {
                await TankHelper.GetTankList();
                DataRow dr = await TankHelper.GetTankInfo(_tankId);
                ShowTankWN9(dr);
                _WN9Changed = false;
                MsgBox.Show(result, "Result getting WN9 expected values");
            }
        }
    }
}
