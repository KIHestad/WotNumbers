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
	public partial class DatePopup : Form
	{
        bool autoSelectDateOK = false;
        DateTime autoSelectDate; 

        public DatePopup(DateTime? currentdate)
		{
			InitializeComponent();
            if (currentdate != null)
            {
                autoSelectDateOK = true;
                autoSelectDate = Convert.ToDateTime(currentdate);
            }
		}

		private void DatePopup_Load(object sender, EventArgs e)
		{
            if (autoSelectDateOK)
			{
                calendar.SelectionStart = autoSelectDate;
                calendar.SelectionEnd = autoSelectDate;
			}
		}

				private void btnOK_Click(object sender, EventArgs e)
		{
			DateTimeHelper.DatePopupSelectedDate = calendar.SelectionStart;
            DateTimeHelper.DatePopupSelected = true;
			this.Close();		
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
            DateTimeHelper.DatePopupSelected = false;
            this.Close();
		}

		private void DatePopup_Shown(object sender, EventArgs e)
		{
			calendar.Visible = true;
		}
        		
	}
}
