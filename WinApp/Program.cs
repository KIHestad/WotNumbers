using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			//try
			//{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Form mainForm = new Forms.Main();
				// mainForm.FormBorderStyle = FormBorderStyle.None;
				//mainForm.Visible = false;
				Application.Run(mainForm);
			//}
			//catch (Exception ex)
			//{
			//	string SourceName = "Wot Numbers";
			//	if (!EventLog.SourceExists(SourceName))
			//	{
			//		EventLog.CreateEventSource(SourceName, "Application");
			//	}
			//	EventLog eventLog = new EventLog();
			//	eventLog.Source = SourceName;
			//	string message = string.Format("Exception: {0} \n\nStack: {1}", ex.Message, ex.StackTrace);
			//	eventLog.WriteEntry(message, EventLogEntryType.Error);
			//	MessageBox.Show("Wot Numbers has terminated due to an error. Check Event Viewer for details." + Environment.NewLine + Environment.NewLine + ex.Message,
			//		"Wot Numbers - Error",
			//		buttons: MessageBoxButtons.OK,
			//		icon: MessageBoxIcon.Error);
			//	// throw;
			//}
			
		}
	}
}
