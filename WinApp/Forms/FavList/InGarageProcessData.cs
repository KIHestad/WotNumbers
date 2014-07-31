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
			if (ddFavList.Text == "")
			{
				Code.MsgBox.Show("Please select a Favourite Tank List before saving, if you don't have a Favourite Tank List for 'In Garage' tanks, please create one.",
					"No selected Favourite Tank List", this);
				return;
			}
			else
			{
				// Check how many to be delted and how many added
			}
		}
	}
}
