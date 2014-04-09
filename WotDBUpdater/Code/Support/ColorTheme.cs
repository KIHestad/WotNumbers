using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater.Code.Support
{
	class ColorTheme
	{
		// Control container and control colors
		public static Color FormTranparency = Color.FromArgb(255, 1, 1, 1);
		public static Color FormBack = Color.FromArgb(255, 32, 32, 32);
		public static Color FormBackTitle = Color.FromArgb(255, 45, 45, 49);
		public static Color FormBackFooter = Color.FromArgb(255, 8, 8, 8);
		public static Color FormBorderBlack = Color.FromArgb(255, 0, 0, 0);
		public static Color FormBorderBlue = Color.FromArgb(255, 68, 96, 127);
		public static Color FormBorderRed = System.Drawing.Color.DarkRed;
		public static Color ControlBack = Color.FromArgb(255, 45, 45, 49);
		public static Color ControlBackMouseOver = Color.FromArgb(255, 58, 58, 62);
		public static Color ControlBackMouseDown = Color.FromArgb(255, 72, 72, 76);
		public static Color ControlFont = Color.FromArgb(255, 150, 150, 156);
		
		// Grid colors
		public static Color ToolGrayMainBack = Color.FromArgb(255, 22, 22, 22);
		public static Color ToolGrayMain = Color.FromArgb(255, 45, 45, 49);
		public static Color ToolGrayHover = Color.FromArgb(255, 68, 68, 72);
		public static Color ToolGrayOutline = Color.FromArgb(255, 82, 82, 86);
		public static Color ToolGrayScrollbar = Color.FromArgb(255, 102, 102, 106);
		public static Color ToolGrayScrollbarHover = Color.FromArgb(255, 126, 126, 130);
		public static Color ToolGrayCheckBack = Color.FromArgb(255, 88, 88, 92);
		public static Color ToolGrayCheckHover = Color.FromArgb(255, 92, 92, 96);
		public static Color ToolGrayCheckPressed = Color.FromArgb(255, 108, 108, 112);
		public static Color ToolWhiteToolStrip = Color.FromArgb(255, 220, 220, 220);
		public static Color ToolBlue = Color.FromArgb(255, 66, 125, 215);
		public static Color ToolBlueSelectedButton = Color.FromArgb(255, 68, 96, 127);
		
	}
}
