﻿using System;
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
	public partial class RecalcPlayerAccountId : FormCloseOnEsc
	{
		private static bool _autoRun = false;
		public RecalcPlayerAccountId(bool autoRun = false)
		{
			InitializeComponent();
			_autoRun = autoRun;
		}

		private async void RecalcPlayerAccountId_Shown(object sender, EventArgs e)
		{
			if (_autoRun)
				await RunNow();
		}

		private string GetProcessingString()
		{
			return "Recalc Player Account Id ";
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
			RecalcPlayerAccountIdTheme.Cursor = Cursors.WaitCursor;
			btnStart.Enabled = false;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;

			string sql = "select id, name, accountId from player";
			DataTable dt = await DB.FetchData(sql);

			int tot = dt.Rows.Count;
			badProgressBar.ValueMax = tot + 2;
			sql = "";
			int loopCount = 0;

			UpdateProgressBar("Starting updates...", 1);
			foreach (DataRow dr in dt.Rows)
			{
				string playerNameAndServer = Convert.ToString(dr["name"]);
				int playerAccountId = await ImportWotApi2DB.ImportPlayerAccountId(this, playerNameAndServer);

				if (playerAccountId != -1)
				{
					int playerId = Convert.ToInt32(dr["id"]);

					// Build SQL
					sql = "UPDATE player SET accountId = " + playerAccountId.ToString() + " WHERE id = " + playerId.ToString() +  " ;";
					loopCount++;

					UpdateProgressBar(GetProcessingString() + badProgressBar.Value, loopCount);
					await DB.ExecuteNonQuery(sql, Config.Settings.showDBErrors, true);
				}
			}

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


	}
}
