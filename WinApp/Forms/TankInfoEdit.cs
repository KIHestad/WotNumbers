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
                txtName.Text = dr["name"].ToString();
                txtShortName.Text = dr["short_name"].ToString();
                txtTier.Text = dr["tier"].ToString();
                ddNation.Text = dr["countryName"].ToString();
                ddTankType.Text = dr["tankTypeName"].ToString();
            }
            else
            {
                btnSave.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void ddNation_Click(object sender, EventArgs e)
        {
            Code.DropDownGrid.Show(ddNation, Code.DropDownGrid.DropDownGridType.List, "USSR,Germany,USA,China,France,UK,Japan,Czechoslovakia");
        }

        private void ddTankType_Click(object sender, EventArgs e)
        {
            Code.DropDownGrid.Show(ddTankType, Code.DropDownGrid.DropDownGridType.List, "Light tank,Medium tank,Heavy tank,Tank destroyer,Self propelled gun");
        }
    }
}
