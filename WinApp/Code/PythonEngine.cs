using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IronPython.Hosting;
using System.Diagnostics;
using System.Threading;
using Microsoft.Scripting.Hosting.Providers;
using IronPython.Runtime;

namespace WinApp.Code
{
    class PythonEngine
	{
		public static Microsoft.Scripting.Hosting.ScriptEngine Engine; // allow to run ironpython programs
        private static Mutex pythonLock = new Mutex();

		// Fetch output
		public static string ipyOutput;
		public static MemoryStream ipyMemoryStream = new MemoryStream(1024);
		public static EventRaisingStreamWriter outputWr;
		
		public static void CreateEngine()
		{
            // Create Engine - Debug mode
            Dictionary<string, object> options = new Dictionary<string, object>();
            options["Debug"] = true;
            Engine = Python.CreateEngine(options); 

            // Create Engine - Normal mode
            //Engine = Python.CreateEngine();

            var pc = HostingHelpers.GetLanguageContext(Engine) as PythonContext;
            var hooks = pc.SystemState.Get__dict__()["path_hooks"] as List;
            hooks.Clear();

            /*
			System.IO.FileStream fs = new System.IO.FileStream(ipyLogFile, System.IO.FileMode.Create);
			Engine.Runtime.IO.SetOutput(fs, Encoding.UTF8); // write to file
			*/

            // Create handlers for fetching python output
            outputWr = new EventRaisingStreamWriter(ipyMemoryStream);
			outputWr.StringWritten += new EventHandler<MyEvtArgs<string>>(sWr_StringWritten);
			Engine.Runtime.IO.SetOutput(ipyMemoryStream, outputWr);
		}

        public static bool LockPython(int timeout = 1)
        {
            if (pythonLock.WaitOne(TimeSpan.FromSeconds(timeout)))
            {
                return true;
            }
            Log.AddToLogBuffer("Unable to lock Python environment!");
            return false;
        }

        public static void UnlockPython()
        {
            pythonLock.ReleaseMutex();
        }

        private static void sWr_StringWritten(object sender, MyEvtArgs<string> e)
		{
			ipyOutput += e.Value;
		}

	}

	// Used for ironpython redirect output
    [DebuggerNonUserCode]
    public class MyEvtArgs<T> : EventArgs
	{
		public T Value
		{
			get;
			private set;
		}
		public MyEvtArgs(T value)
		{
			this.Value = value;
		}
	}

    [DebuggerNonUserCode]
	public class EventRaisingStreamWriter : StreamWriter
	{
		#region Event
		public event EventHandler<MyEvtArgs<string>> StringWritten;
		#endregion

		#region CTOR
		public EventRaisingStreamWriter(Stream s)
			: base(s)
		{ }
		#endregion

		#region Private Methods
		private void LaunchEvent(string txtWritten)
		{
			if (StringWritten != null)
			{
				StringWritten(this, new MyEvtArgs<string>(txtWritten));
			}
		}
		#endregion

		#region Overrides

		public override void Write(char value)
		{
			base.Write(value);
			LaunchEvent(value.ToString());
		}

		public override void Write(string value)
		{
			base.Write(value);
			LaunchEvent(value);
		}

		public override void Write(bool value)
		{
			base.Write(value);
			LaunchEvent(value.ToString());
		}
		// here override all writing methods...

		#endregion
	}
}

