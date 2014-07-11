using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	public static class MainSettings
	{
		public static GridView.Views View = GridView.Views.Overall;
		public static GridFilter.Settings GridFilterTank = GridFilter.GetDefault(GridView.Views.Tank);
		public static GridFilter.Settings GridFilterBattle = GridFilter.GetDefault(GridView.Views.Battle);

		public static GridFilter.Settings GetCurrentGridFilter()
		{
			GridFilter.Settings gf = new GridFilter.Settings();
			switch (View)
			{
				case GridView.Views.Overall:
					break;
				case GridView.Views.Tank:
					gf = GridFilterTank;
					break;
				case GridView.Views.Battle:
					gf = GridFilterBattle;
					break;
				default:
					break;
			}
			return gf;
		}

		public static void UpdateCurrentGridFilter(GridFilter.Settings GridFilter)
		{
			switch (View)
			{
				case GridView.Views.Overall:
					break;
				case GridView.Views.Tank:
					GridFilterTank = GridFilter;
					break;
				case GridView.Views.Battle:
					GridFilterBattle = GridFilter;
					break;
				default:
					break;
			}
		}
	}
}
