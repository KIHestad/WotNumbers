using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IronPython.Hosting;

namespace WinApp.Code
{
	class PythonEngine
	{
		public static Microsoft.Scripting.Hosting.ScriptEngine Engine; // allow to run ironpython programs
		public static bool InUse = false;
		public static TextBox consoleTextBox = new TextBox();
		
		public static void CreateEngine()
		{
			// Create Engine - Debug mode
			// Dictionary<string, object> options = new Dictionary<string, object>();
			// options["Debug"] = true;
			// Engine = Python.CreateEngine(options); 

			// Create Engine - Normal mode
			Engine = Python.CreateEngine();
			
			// Remove old ipy log file
			string ipyLogFile = Config.AppDataLogFolder + "ipy.txt";
			if (File.Exists(ipyLogFile))
				File.Delete(ipyLogFile);
			// Do not output to file any more
			/*
			System.IO.FileStream fs = new System.IO.FileStream(ipyLogFile, System.IO.FileMode.Create);
			Engine.Runtime.IO.SetOutput(fs, Encoding.UTF8); // write to file
			*/

			// Capture console to textwriter
			Console.SetOut(TextWriter.Synchronized(new ConsoleTextBoxWriter(consoleTextBox)));

			// Output to console
			Engine.Runtime.IO.RedirectToConsole();
		}
		
	}

	class ConsoleTextBoxWriter : TextWriter
	{
		private TextBox _textBox;

		public ConsoleTextBoxWriter(TextBox textbox)
		{
			_textBox = textbox;
		}


		public override void Write(char value)
		{
			base.Write(value);
			// When character data is written, append it to the text box.
			_textBox.AppendText(value.ToString());
		}

		public override System.Text.Encoding Encoding
		{
			get { return System.Text.Encoding.UTF8; }
		}
	}
}
