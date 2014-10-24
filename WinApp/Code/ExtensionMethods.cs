using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

		public static string ToRoman(this int number)
		{
			StringBuilder result = new StringBuilder();
			int[] digitsValues = { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };
			string[] romanDigits = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
			while (number > 0)
			{
				for (int i = digitsValues.Count() - 1; i >= 0; i--)
					if (number / digitsValues[i] >= 1)
					{
						number -= digitsValues[i];
						result.Append(romanDigits[i]);
						break;
					}
			}
			return result.ToString();
		}

		public static string ToRoman(this double number)
		{
			return Convert.ToInt32(number).ToRoman();
		}

		public static string ToBinary(this long number)
		{
			string binary = string.Empty;
			while (number > 0)
			{
				// Logical AND the number and prepend it to the result string
				binary = (number & 1) + binary;
				number = number >> 1;
			}

			return binary;
		}

		public static string ReplaceWhiteSpaceWithSpace(this string text)
		{
			string value = Regex.Replace(text, "\\s", " ");
			return value;
		}

		public static string ReplaceSpaceWithWhiteSpace(this string text)
		{
			return Regex.Replace(text, " ", @"\s+");

			//string value = Regex.Replace(text, " ", "\\s");
			//return value;
		}
	}
}
