using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class BattleMapAndComment : UserControl
	{
		private int battleId = 0;
		public BattleMapAndComment(int showBattleId)
		{
			InitializeComponent();
			battleId = showBattleId;
		}

		private void BattleMapAndComment_Load(object sender, EventArgs e)
		{
			string sql = "select map.* from map inner join battle on map.id = battle.mapId where battle.id=@battleId";
			DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				DataRow dr = dt.Rows[0];
				if (dr["arena_id"] != DBNull.Value)
				{
					string arena_id = dr["arena_id"].ToString();
					picMap.Image = ImageHelper.GetMap(arena_id);
					lblMapDescription.Text = dr["description"].ToString();
				}
			}
		}
	}
}
