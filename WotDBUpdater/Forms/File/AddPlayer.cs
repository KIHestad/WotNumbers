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
using WotDBUpdater.Code;

namespace WotDBUpdater.Forms.File
{
	public partial class AddPlayer : Form
	{
		public AddPlayer()
		{
			InitializeComponent();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			
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
				db.AddWithValue(ref sql, "@name", txtNewPlayerName.Text.Trim(), db.SqlDataType.VarChar);
				if (db.ExecuteNonQuery(sql))
				{
					Code.MsgBox.Show("New player successfully saved.", "New player added");
					this.Close();
				}
			}
		}
	}
}
