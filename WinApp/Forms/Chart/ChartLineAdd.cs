using System;
using System.Windows.Forms;
using WinApp.Code.FormView;
using WinApp.Code;
using System.Collections.Generic;

namespace WinApp.Forms
{
    public partial class ChartLineAdd : Form
    {
        int _initTankId = 0;
        string ddChartList = "";

        public ChartLineAdd(int tankId = 0)
        {
            _initTankId = tankId;
            BattleChartHelper.AddChartLine = new BattleChartHelper.BattleChartAddChartLine();
            InitializeComponent();
        }

        private void ChartLineAdd_Load(object sender, EventArgs e)
        {
            // Get chart type list, add to drop down
            List<BattleChartHelper.ChartType> chartTypeList = BattleChartHelper.GetChartTypeList();
            foreach (BattleChartHelper.ChartType c in chartTypeList)
            {
                ddChartList += c.name + ",";
            }
            ddChartList = ddChartList.Substring(0, ddChartList.Length - 1); // Remove last comma
            // Set latest selected chart type or default value
            ddChartType.Text = BattleChartHelper.Settings.ChartTypeName;
            // Set default as cancel
            BattleChartHelper.AddChartLine.tankId = -1;

            // Showing selected tank from grid or last tank or (All Tanks) if non selected yet
            string sql = "";
            if (_initTankId != 0)
            {
                sql = "select name from tank inner join playerTank on tank.id=playerTank.tankId where playerTank.id=@id order by tank.name; ";
                DB.AddWithValue(ref sql, "@id", _initTankId, DB.SqlDataType.Int);
                ddTank.Text = DB.FetchData(sql).Rows[0][0].ToString();
            }
            else
            {
                if (BattleChartHelper.Settings.TankName != "")
                    ddTank.Text = BattleChartHelper.Settings.TankName;
                else
                    ddTank.Text = "( All Tanks )";
            }
            
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSelect_Click(object sender, EventArgs e)
        {
            // Check if value selected
            if (ddChartType.Text == "")
            {
                MsgBox.Show("Please select a chart type", "Missing Chart Value", this);
            }
            else
            {
                BattleChartHelper.AddChartLine.tankId = _initTankId;
                BattleChartHelper.AddChartLine.tankName = ddTank.Text;
                BattleChartHelper.AddChartLine.chartTypeName = ddChartType.Text;
                this.Close();
            }
        }

        private void ddTank_Click(object sender, EventArgs e)
        {
            Code.DropDownGrid.Show(ddTank, Code.DropDownGrid.DropDownGridType.List, "( All Tanks ),Search for Tank...");
        }

        private void ddChartType_Click(object sender, EventArgs e)
        {
            Code.DropDownGrid.Show(ddChartType, Code.DropDownGrid.DropDownGridType.List, ddChartList);
        }

        private void ddTank_TextChanged(object sender, EventArgs e)
        {
            // Check if search is selected
            if (ddTank.Text == "Search for Tank...")
            {
                TankSearchHelper.OpenTankSearch(this);
                if (TankSearchHelper.Result == MsgBox.Button.OK && TankSearchHelper.SelectedTankId > 0)
                {
                    _initTankId = TankSearchHelper.SelectedTankId;
                    ddTank.Text = TankHelper.GetTankName(TankSearchHelper.SelectedTankId);
                }
            }
        }
    }
}
