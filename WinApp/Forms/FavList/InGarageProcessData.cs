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
	public partial class InGarageProcessData : Form
	{
		string favList = "";
		List<int> tanksInGarage = new List<int>();

		public InGarageProcessData()
		{
			InitializeComponent();
			txtTanksInGarage.Text = "Please wait...";
			this.Cursor = Cursors.WaitCursor;
			Application.DoEvents();
		}

		private void InGarageProcessData_Load(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			Application.DoEvents();
			txtNickname.Text = InGarageApiResult.nickname;
			GetFavList();
			string sql = "select * from favList where name = 'In Garage';";
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
				ddFavList.Text = "In Garage";
			tanksInGarage = Code.ImportWotApi2DB.ImportPlayersInGarageVehicles(this);
			txtTanksInGarage.Text = tanksInGarage.Count().ToString();
			btnSaveTanksToFavList.Enabled = true;
			this.Cursor = Cursors.Default;
		}

		private void ddFavList_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(ddFavList, Code.DropDownGrid.DropDownGridType.List, favList);
		}

		private void GetFavList()
		{
			favList = "";
			string sql = "select * from favList order by COALESCE(position,99), name";
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					favList += dr["name"].ToString() + "," ;
				}
			}
			if (favList.Length > 0)
				favList = favList.Substring(0, favList.Length - 1);
		}

		private void btnCreateFavList_Click(object sender, EventArgs e)
		{
			string sql = "select * from favList where name = 'In Garage';";
			DataTable dt = DB.FetchData(sql);
			string newFavListName = "";
			if (dt.Rows.Count == 0)
				newFavListName = "In Garage";
			Form frm = new Forms.FavListNewEdit(0, newFavListName);
			frm.ShowDialog();
			GetFavList();
			sql = "select * from favList order by id desc";
			dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				ddFavList.Text = dt.Rows[0]["name"].ToString();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnSaveTanksToFavList_Click(object sender, EventArgs e)
		{
			InGarageApiResult.changeFavList = false;
			if (ddFavList.Text == "")
			{
				Code.MsgBox.Show("Please select a Favourite Tank List before saving, if you don't have a Favourite Tank List for 'In Garage' tanks, please create one.",
					"No selected Favourite Tank List", this);
				return;
			}
			else
			{
				// Check how many to be delted and how many added
				List<int> newTank = new List<int>();
				List<int> delTank = new List<int>();
				int favListId = FavListHelper.GetId(ddFavList.Text);
				if (favListId > 0)
				{
					// Find new tanks
					string sql = "select * from favListTank where favListId=@favListId";
					DB.AddWithValue(ref sql, "@favListId", favListId, DB.SqlDataType.Int);
					DataTable currentTanks = DB.FetchData(sql);
					foreach (int tankInGarage in tanksInGarage)
					{
						DataRow[] exists = currentTanks.Select("tankId = " + tankInGarage.ToString());
						if (exists.Length == 0)
							newTank.Add(tankInGarage);
					}
					// Find tanks to remove
					foreach (DataRow currentTank in currentTanks.Rows)
					{
						List<int> foundTank = new List<int>();
						int tankId = Convert.ToInt32(currentTank["tankId"]);
						if (!tanksInGarage.Contains(tankId))
						{
							delTank.Add(tankId);
						}
					}
				}
				int newTankCount = newTank.Count;
				int delTankCount = delTank.Count;
				if (newTankCount > 0 || delTankCount > 0)
				{
					Code.MsgBox.Button answer = Code.MsgBox.Show(
						"Found " + newTankCount.ToString() + " tanks to add." + Environment.NewLine + 
						"Found " + delTankCount.ToString() + " tanks to remove." + 
						Environment.NewLine + Environment.NewLine + 
						"Press 'OK' to update tank list: " + ddFavList.Text +
						Environment.NewLine + Environment.NewLine,
						"Save 'In Garage' tanks", 
						MsgBoxType.OKCancel, this);
					if (answer == MsgBox.Button.OKButton)
					{
						// Find latest sort order
						string sql = "select max(sortorder) from favListTank where favListId=@favListId;";
						DB.AddWithValue(ref sql, "@favListId", favListId, DB.SqlDataType.Int);
						int sortOrder = 0;
						DataTable lastSortOrder = DB.FetchData(sql);
						if (lastSortOrder.Rows.Count > 0)
							if (lastSortOrder.Rows[0][0] != DBNull.Value)
								sortOrder = Convert.ToInt32(lastSortOrder.Rows[0][0]);
						// Update now
						sql = "";
						foreach (int tankId in delTank)
						{
							string delsql = "DELETE FROM favListTank WHERE favListId=@favListId AND tankId=@tankId; ";
							DB.AddWithValue(ref delsql, "@tankId", tankId, DB.SqlDataType.Int);
							sql += delsql;
						}
						foreach (int tankId in newTank)
						{
							sortOrder++;
							string newsql = "INSERT INTO favListTank (favListId, tankId, sortorder) VALUES (@favListId, @tankId, @sortorder); ";
							DB.AddWithValue(ref newsql, "@tankId", tankId, DB.SqlDataType.Int);
							DB.AddWithValue(ref newsql, "@sortorder", sortOrder, DB.SqlDataType.Int);
							sql += newsql;
						}
						DB.AddWithValue(ref sql, "@favListId", favListId, DB.SqlDataType.Int);
						DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors, true);
						// Select this list
						GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
						gf.TankId = -1;
						gf.FavListId = favListId;
						gf.FavListName = ddFavList.Text;
						gf.FavListShow = GridFilter.FavListShowType.FavList;
						MainSettings.UpdateCurrentGridFilter(gf);
						InGarageApiResult.changeFavList = true;

						// Done, close now
						this.Close();
					}
					
				}
				else
				{
					Code.MsgBox.Show("Current 'In Garage' tank list is complete. No new tanks to add or sold tanks to remove", "No changes found", this);
					this.Close();
				}
				
			}
		}
	}
}
