using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class FixBattleTable : FormCloseOnEsc
	{
		private static bool _autoRun = false;
		public FixBattleTable(bool autoRun = false)
		{
			InitializeComponent();
			_autoRun = autoRun;
		}

		private async void FixBattleTable_Shown(object sender, EventArgs e)
		{
			if (_autoRun)
				await RunNow();
		}

		private string GetProcessingString()
		{
			return "Fix battle table ";
		}
		private void UpdateProgressBar(string statusText, int count)
		{
			lblProgressStatus.Text = statusText;
			if (statusText == "")
				badProgressBar.Value = 0;
			else
				badProgressBar.Value += count;
			Refresh();
		}

		private async Task RunNow()
		{
			this.Cursor = Cursors.WaitCursor;
			FixBattleTableTheme.Cursor = Cursors.WaitCursor;
			btnStart.Enabled = false;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;
			badProgressBar.ValueMax = 2;

			UpdateProgressBar("Starting updates...", 1);

			await FixBattleTableFunc();

			// Done
			UpdateProgressBar("", 0);
			lblProgressStatus.Text = "Update finished: " + DateTime.Now.ToString();
			btnStart.Enabled = true;

			// Done
			this.Cursor = Cursors.Default;
			this.Close();
		}

		private async void btnStart_Click(object sender, EventArgs e)
		{
			await RunNow();
		}

		private static string GetUpdateString(DataRow src, DataRow dst, int deletedRows)
		{
			// string updateString = "UPDATE battle SET id=@newId, damageRating=@damageRating, damageRatingTotal=@damageRatingTotal, battleResultId=@battleResultId, draw=@draw, defeat=@defeat, orphanDat=0 WHERE id=@id; ";
			// DB.AddWithValue(ref updateString, "@newId", Convert.ToInt64(src["id"]) - deletedRows, DB.SqlDataType.Int);

			string updateString = "UPDATE battle SET damageRating=@damageRating, damageRatingTotal=@damageRatingTotal, battleResultId=@battleResultId, draw=@draw, defeat=@defeat, orphanDat=0 WHERE id=@id; ";
			DB.AddWithValue(ref updateString, "@damageRating", Convert.ToInt64(dst["damageRating"]), DB.SqlDataType.Int);
			DB.AddWithValue(ref updateString, "@damageRatingTotal", Convert.ToInt64(dst["damageRatingTotal"]), DB.SqlDataType.Int);
			DB.AddWithValue(ref updateString, "@battleResultId", Convert.ToInt64(dst["battleResultId"]), DB.SqlDataType.Int);
			DB.AddWithValue(ref updateString, "@draw", Convert.ToInt64(dst["draw"]), DB.SqlDataType.Int);
			DB.AddWithValue(ref updateString, "@defeat", Convert.ToInt64(dst["defeat"]), DB.SqlDataType.Int);
			DB.AddWithValue(ref updateString, "@id", Convert.ToInt64(src["id"]), DB.SqlDataType.Int);

			return updateString;
		}
		private static string GetDeleteString(DataRow row)
		{
			string deleteString = "DELETE FROM battle WHERE id=@id; ";
			DB.AddWithValue(ref deleteString, "@id", Convert.ToInt64(row["id"]), DB.SqlDataType.Int);

			return deleteString;
		}
		public async static Task FixBattleTableFunc()
		{
			// sql string = todas las entries dela teabla battle antes de la fecha de introducción de orphan battles
			string sql = "SELECT * FROM battle WHERE battleTime>@battleTimeFrom ORDER BY battleTime, id";
			DateTime battleTimeFrom = new DateTime(2022, 10, 1, 0, 0, 0);
			DB.AddWithValue(ref sql, "@battleTimeFrom", battleTimeFrom, DB.SqlDataType.DateTime);

			string sqlUpdateString = "";
			string sqlDeleteString = "";
			int deletedRows = 0;

			DataTable dt = await DB.FetchData(sql);
			if (dt.Rows.Count > 1)
			{
				int rowIdx = 0;

				DataRow currentRow = dt.Rows[rowIdx];

				while ((rowIdx + 1) < dt.Rows.Count)
				{
					rowIdx++;

					DataRow rowToCompareAgainst = dt.Rows[rowIdx];

					if (Dossier2db.SameBattle(currentRow, rowToCompareAgainst))
					{
						sqlUpdateString += GetUpdateString(currentRow, rowToCompareAgainst, deletedRows);
						sqlDeleteString += GetDeleteString(rowToCompareAgainst);
						deletedRows++;

						if ((rowIdx + 1) < dt.Rows.Count)
						{
							rowIdx++;
							currentRow = dt.Rows[rowIdx];
						}
					}
					else
					{
						currentRow = dt.Rows[rowIdx];
					}
				}
			}

			if (sqlDeleteString != "")
				await DB.ExecuteNonQuery(sqlDeleteString);
			if (sqlUpdateString != "")
				await DB.ExecuteNonQuery(sqlUpdateString);
		}

	}
}
