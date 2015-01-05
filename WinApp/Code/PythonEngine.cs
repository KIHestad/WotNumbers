using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IronPython.Hosting;

namespace WinApp.Code
{
	class PythonEngine
	{
		public static Microsoft.Scripting.Hosting.ScriptEngine Engine; // allow to run ironpython programs
		
		public static void CreateEngine()
		{
			// Debug mode
			// Dictionary<string, object> options = new Dictionary<string, object>();
			// options["Debug"] = true;
			// Engine = Python.CreateEngine(options); 

			// Normal mode
			Engine = Python.CreateEngine();
			string ipyLogFile = Config.AppDataLogFolder + "ipy.txt";
			if (File.Exists(ipyLogFile))
				File.Delete(ipyLogFile);
			System.IO.FileStream fs = new System.IO.FileStream(ipyLogFile, System.IO.FileMode.Create);
			Engine.Runtime.IO.SetOutput(fs, Encoding.UTF8);
		}
		
		public static bool InUse = false;
	}
}
