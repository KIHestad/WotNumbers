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
			double exp = 1;
			if (length > 0)
				exp = Math.Pow(10, length);
			return ((double)Math.Round(i / exp)) * exp;
		}

		public static double RoundDown(this double i, int length)
		{
			double exp = 1;
			if (length > 0)
				exp = Math.Pow(10, length);
			return ((double)Convert.ToInt32(i / exp)) * exp;
		}

		public static double RoundUp(this double i, int length)
		{
			double exp = 1;
			if (length > 0)
				exp = Math.Pow(10, length);
			i = i + .5;
			return ((double)Math.Round(i / exp)) * exp;
		}

	}
}
