using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Gadget
{
	public partial class ucPlayerInfo : UserControl
	{
		public ucPlayerInfo()
		{
			InitializeComponent();
		}

		private void ucPlayerInfo_Load(object sender, EventArgs e)
		{
			string sql = "select name from player where id=@id; ";
			DB.AddWithValue(ref sql, "@id", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
				txtPlayerName.Text = dt.Rows[0][0].ToString();
		}
	}
}
