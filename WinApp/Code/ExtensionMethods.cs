using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
	public static class ExtensionMethods
	{
		public static double RoundOff(this double i, int length)
		{
			double exp = Math.Pow(10, length);
			return ((double)Math.Round(i / exp)) * exp;
		}

		public static double RoundDown(this double i, int length)
		{
			double exp = Math.Pow(10, length);
			return ((double)Convert.ToInt32(i / exp)) * exp;
		}
	}
}
