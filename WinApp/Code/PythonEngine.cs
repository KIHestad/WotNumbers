using System;
using System.Collections.Generic;
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
		}
		
		public static bool InUse = false;
	}
}
