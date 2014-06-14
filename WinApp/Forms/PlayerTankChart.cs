using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class PlayerTankChart : Form
	{
		int initPlayerTankId = 0;
		public PlayerTankChart(int playerTankId = 0)
		{
			InitializeComponent();
			initPlayerTankId = playerTankId;
		}


		private void TestShowImage_Load(object sender, EventArgs e)
		{
			if (initPlayerTankId != 0)
			{
				string sql = "select name from tank inner join playerTank on tank.id=playerTank.tankId where playerTank.id=@id; ";
				DB.AddWithValue(ref sql, "@id", initPlayerTankId, DB.SqlDataType.Int);
				ddTank.Text = DB.FetchData(sql).Rows[0][0].ToString();
			}
		}
		
		private void ddTank_Click(object sender, EventArgs e)
		{
			string sql = "select tank.name from tank inner join playerTank on tank.id = playerTank.tankId where playerTank.playerId=@playerId order by lastBattleTime DESC";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			Code.DropDownGrid.Show(ddTank, Code.DropDownGrid.DropDownGridType.Sql, sql);
		}

		private void ddTank_TextChanged(object sender, EventArgs e)
		{
			string tankName = ddTank.Text;
			// Check if already shown
			foreach (Series serie in ChartingBattleCount.Series)
			{
				if (serie.Name == tankName) return;
			}
			// Find playerTank
			int playerTankId = TankData.GetPlayerTankId(tankName);
			// Find current values
			string sql = "select SUM(battles) from playerTankBattle where playerTankId=@playerTankId";
			DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			int sumBattles = Convert.ToInt32(DB.FetchData(sql).Rows[0][0]);
			// Add series
			Series newSerie = new Series(tankName);
			//newSerie.AxisLabel = tankName;
			newSerie.ChartType = SeriesChartType.Line;
			newSerie.MarkerStyle = MarkerStyle.Circle;
			newSerie.XValueType = ChartValueType.DateTime;
			ChartingBattleCount.Series.Add(newSerie);
			// Find battles
			sql = "select * from battle where playerTankId=@playerTankId order by battleTime DESC ";
			DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			foreach (DataRow dr in dt.Rows)
			{
				ChartingBattleCount.Series[tankName].Points.AddXY(Convert.ToDateTime(dr["battleTime"]), sumBattles);
				sumBattles -= Convert.ToInt32(dr["battlesCount"]);
			}
		}
	}
}
