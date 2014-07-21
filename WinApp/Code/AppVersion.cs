using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WinApp.Code
{
	class AppVersion
	{
		public static string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." +
					Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString() + "." +
					Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
			}
		}

		public static string BuildVersion
		{
			get
			{
				return  "(" + Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString() + ")";
			}
		}

	}
}
