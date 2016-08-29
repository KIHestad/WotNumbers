using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
    public partial class TankInfoEdit : Form
    {
        private int _tankId = 0;
        private TankHelper.BasicTankInfo _defaultTankInfo = new TankHelper.BasicTankInfo();
        private string _nations = "";
        private string _tankTypes = "";

        

        public TankInfoEdit(int tankID)
        {
            InitializeComponent();
            _tankId = tankID;
        }

        private void TankInfoEdit_Load(object sender, EventArgs e)
        {
            txtID.Text = _tankId.ToString();
            DataRow dr = TankHelper.GetTankInfo(_tankId);
            if (dr != null)
            {
                // Get default values
                _defaultTankInfo.name = dr["name"].ToString();
                _defaultTankInfo.short_name = dr["short_name"].ToString();
                _defaultTankInfo.nation = dr["countryName"].ToString();
                _defaultTankInfo.tankType = dr["tankTypeName"].ToString();
                _defaultTankInfo.tier = dr["tier"].ToString();
                _defaultTankInfo.customTankInfo = Convert.ToBoolean(dr["customTankInfo"]);
                // Show values
                SetDefaultTankInfo();
                // Get dropdown values
                DataTable dt = DB.FetchData("SELECT name FROM tankType ORDER BY id");
                foreach (DataRow dr1 in dt.Rows)
                    _tankTypes += dr1["name"].ToString() + ",";
                if (_tankTypes.Length > 0)
                    _tankTypes = _tankTypes.Substring(0, _tankTypes.Length - 1);
                dt = DB.FetchData("SELECT name FROM country ORDER BY sortOrder");
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

        private void SetDefaultTankInfo()
        {
            txtName.Text = _defaultTankInfo.name;
            txtShortName.Text = _defaultTankInfo.short_name;
            txtTier.Text = _defaultTankInfo.tier;
            ddNation.Text = _defaultTankInfo.nation;
            ddTankType.Text = _defaultTankInfo.tankType;
        }

        private bool CheckIfEditedValues()
        {
            return (
                txtName.Text != _defaultTankInfo.name ||
                txtShortName.Text != _defaultTankInfo.short_name ||
                txtTier.Text != _defaultTankInfo.tier ||
                ddNation.Text != _defaultTankInfo.nation ||
                ddTankType.Text != _defaultTankInfo.tankType
            );
        }

        private int GetIdFromName(string tableName, string nameValue)
        {
            string sql = "SELECT id FROM " + tableName + " WHERE name=@name";
            DB.AddWithValue(ref sql, "@name", nameValue, DB.SqlDataType.VarChar);
            DataTable dt = DB.FetchData(sql);
            int nationId = -1;
            if (dt.Rows.Count > 0)
                nationId = Convert.ToInt32(dt.Rows[0]["id"]);
            return nationId;
        }

        private bool TankInfoSave()
        {
            try
            {
                // Update default values
                _defaultTankInfo.name = txtName.Text.Trim();
                _defaultTankInfo.short_name = txtShortName.Text.Trim();
                _defaultTankInfo.tier = txtTier.Text.Trim();
                _defaultTankInfo.nation = ddNation.Text;
                _defaultTankInfo.tankType = ddTankType.Text;
                // Save now
                string sql = "UPDATE tank SET name=@name, short_name=@short_name, tier=@tier, countryId=@countryId, tankTypeId=@tankTypeId, customTankInfo=@customTankInfo WHERE id=@id";
                DB.AddWithValue(ref sql, "@name", _defaultTankInfo.name, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sql, "@short_name", _defaultTankInfo.short_name, DB.SqlDataType.VarChar);
                DB.AddWithValue(ref sql, "@tier", _defaultTankInfo.tier, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@countryId", GetIdFromName("country", _defaultTankInfo.nation), DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@tankTypeId", GetIdFromName("tankType", _defaultTankInfo.tankType), DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@customTankInfo", _defaultTankInfo.customTankInfo, DB.SqlDataType.Boolean);
                DB.AddWithValue(ref sql, "@id", _tankId, DB.SqlDataType.Int);
                DB.ExecuteNonQuery(sql);
                // Update tankinfo
                TankHelper.GetTankList();
                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Show("Error saving changes, probably caused by illegal values.\n\n" + ex.Message, "Error saving");
                return false;
            } 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckIfEditedValues())
            {
                _defaultTankInfo.customTankInfo = true;
                if (TankInfoSave())
                {
                    this.Close(); // close form after saving if success
                }
            }
            else
            {
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ddNation_Click(object sender, EventArgs e)
        {
            Code.DropDownGrid.Show(ddNation, Code.DropDownGrid.DropDownGridType.List, _nations);
        }

        private void ddTankType_Click(object sender, EventArgs e)
        {
            Code.DropDownGrid.Show(ddTankType, Code.DropDownGrid.DropDownGridType.List, _tankTypes);
        }

        private void TankInfoEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CheckIfEditedValues())
            {
                MsgBox.Button answer = MsgBox.Show("Do you want to save changes?", "Save changes", MsgBox.Type.YesNo);
                if (answer == MsgBox.Button.Yes)
                {
                    if (!TankInfoSave())
                    {
                        e.Cancel = true; // Cancel closing form if saving fails
                    }
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string message = "";
            TankHelper.BasicTankInfo getTankInfoFromApi = ImportWotApi2DB.ImportTanksDetails(this, _tankId, out message);
            if (getTankInfoFromApi == null)
            {
                MsgBox.Show(message + Environment.NewLine, "Tank not found using Wargaming API");
            }
            else
            {
                //MsgBox.Button answer = MsgBox.Show("Get default values from Wargaming API and save the tankInfo now", "Get Default values", MsgBox.Type.OKCancel);
                //if (answer == MsgBox.Button.OK)
                {
                    _defaultTankInfo = getTankInfoFromApi;
                    SetDefaultTankInfo();
                    if (TankInfoSave())
                        MsgBox.Show("Default tank info retrieved from Wargaming API", "Get Default Values");
                }
            }
        }
    }
}
