using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Gadget
{
	public partial class paramColsRows : FormCloseOnEsc
    {
        bool _saveOk = false;
        int _gadgetId = -1;

		public paramColsRows(int gadgetId = -1)
		{
			InitializeComponent();
			_gadgetId = gadgetId;
		}

		private async void paramColsRows_Load(object sender, EventArgs e)
		{
			object[] currentParameters = new object[] { null, null, null, null, null };
			if (_gadgetId > -1)
			{
				// Lookup value for current gadget
				string sql = "select * from gadgetParameter where gadgetId=@gadgetId order by paramNum;";
				DB.AddWithValue(ref sql, "@gadgetId", _gadgetId, DB.SqlDataType.Int);
				DataTable dt = await DB.FetchData(sql, Config.Settings.showDBErrors);
				foreach (DataRow dr in dt.Rows)
				{
					object paramValue = dr["value"];
					int paramNum = Convert.ToInt32(dr["paramNum"]);
					currentParameters[paramNum] = paramValue;
				}
				txtCols.Text = currentParameters[0].ToString();
				txtRows.Text = currentParameters[1].ToString();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();			
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			int rows = 0;
			int cols = 0;
			bool ok = false;
			ok = Int32.TryParse(txtCols.Text, out cols);
			if (ok) ok = Int32.TryParse(txtRows.Text, out rows);
			if (!ok)
			{
				MsgBox.Show("Please select a valid number", "Invalid numbers");
			}
			else
			{
				if (rows > 20) rows = 20;
				if (cols > 20) cols = 20;
				GadgetHelper.newParameters[0] = cols;
				GadgetHelper.newParameters[1] = rows;
				GadgetHelper.newParametersOK = true;
                _saveOk = true;
				this.Close();
			}
		}

        private void paramColsRows_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_saveOk)
            {
                // Cancel saving
                GadgetHelper.newParameters = new object[] { null, null, null, null, null };
                GadgetHelper.newParametersOK = false;
            }
        }
    }
}
