using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class AddPlayer : Form
	{
		public AddPlayer()
		{
			InitializeComponent();
		}

		private void btnAddNewPlayer_Click(object sender, EventArgs e)
		{
			// Add to database
			if (txtNewPlayerName.Text.Trim() == "")
			{
				Code.MsgBox.Show("Please add a player name before saving u noob... ^_^", "Cannot save nothing....");
			}
			else
			{
				string sql = "INSERT INTO player (name) VALUES (@name)";
				DB.AddWithValue(ref sql, "@name", txtNewPlayerName.Text.Trim(), DB.SqlDataType.VarChar);
				if (DB.ExecuteNonQuery(sql))
				{
					Code.MsgBox.Show("New player successfully saved.", "New player added");
					this.Close();
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
