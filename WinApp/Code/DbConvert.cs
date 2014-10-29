using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
	class DbConvert
	{
		public static double ToDouble(object databaseField)
		{
			if (databaseField == DBNull.Value)
				return 0;
			else
				return Convert.ToDouble(databaseField);
		}

		public static int ToInt32(object databaseField)
		{
			if (databaseField == DBNull.Value)
				return 0;
			else
				return Convert.ToInt32(databaseField);
		}

		public static string ToString(object databaseField)
		{
			if (databaseField == DBNull.Value)
				return "";
			else
				return databaseField.ToString();
		}

	}
}
