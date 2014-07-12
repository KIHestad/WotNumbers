using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace WinAdmin
{
	class FormHelper
	{
		public static void ShowError(DB.DBResult result)
		{
			if (result.Error)
			{
				MessageBox.Show(
					result.Text + Environment.NewLine + Environment.NewLine +
					result.ErrorMsg + Environment.NewLine + Environment.NewLine +
					result.lastSQL + Environment.NewLine + Environment.NewLine,
					result.Title,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
			}
		}

	}
}
