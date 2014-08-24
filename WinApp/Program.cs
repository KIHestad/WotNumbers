using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Form mainForm = new Forms.Main();
			// mainForm.FormBorderStyle = FormBorderStyle.None;
			//mainForm.Visible = false;
			Application.Run(mainForm);
		}
	}
}
