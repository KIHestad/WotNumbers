using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
	public static class ExtensionMethods
	{
		public static double RoundOff(this double val, int length)
		{
			double exp = 1;
			if (length > 0)
				exp = Math.Pow(10, length);
			return ((double)Math.Round(val / exp)) * exp;
		}

		public static double RoundDown(this double val, int length)
		{
			double exp = 1;
			if (length > 0)
				exp = Math.Pow(10, length);
			return (Math.Truncate(val / exp) * exp);
		}

		public static double RoundUp(this double val, int length)
		{
			double exp = 1;
			if (length > 0)
				exp = Math.Pow(10, length);
			val = val + .5;
			return ((double)Math.Round(val / exp)) * exp;
		}

	}
}
