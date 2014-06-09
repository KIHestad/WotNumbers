using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp.Code
{
	class ColorTheme
	{
		// Forms
		public static Color FormTranparency = Color.FromArgb(255, 1, 1, 1);
		public static Color FormBack = Color.FromArgb(255, 32, 32, 32);
		public static Color FormBackTitle = Color.FromArgb(255, 45, 45, 49);
		public static Color FormBackTitleFont = Color.FromArgb(255, 180, 180, 186);
		public static Color FormBackFooter = Color.FromArgb(255, 8, 8, 8);
		public static Color FormBorderBlack = Color.FromArgb(255, 0, 0, 0);
		public static Color FormBorderBlue = Color.FromArgb(255, 68, 96, 127);
		public static Color FormBorderRed = Color.FromArgb(255, 190, 0, 0);
		
		// Controls
		public static Color ControlBack = Color.FromArgb(255, 55, 55, 59);
		public static Color ControlBorder = Color.FromArgb(255, 105, 105, 109);
		public static Color ControlBackMouseOver = Color.FromArgb(255, 68, 68, 72);
		public static Color ControlBackMouseDown = Color.FromArgb(255, 82, 82, 86);
		public static Color ControlFont = Color.FromArgb(255, 200, 200, 206);
		public static Color ControlDarkFont = Color.FromArgb(255, 150, 150, 156);
		public static Color ControlDisabledFont = Color.FromArgb(255, 100, 100, 106);
		public static Color ControlDimmedFont = Color.FromArgb(255, 85, 85, 89);
		public static Color ControlSeparatorGroupBoxBorder = Color.FromArgb(255, 75, 75, 79);
		
		// ToolStrip 
		public static Color ToolGrayMainBack = Color.FromArgb(255, 22, 22, 22);
		public static Color ToolGrayMain = Color.FromArgb(255, 45, 45, 49);
		public static Color ToolGrayHover = Color.FromArgb(255, 68, 68, 72);
		public static Color ToolGrayOutline = Color.FromArgb(255, 82, 82, 86);
		public static Color ToolGrayScrollbar = Color.FromArgb(255, 102, 102, 106);
		public static Color ToolGrayScrollbarHover = Color.FromArgb(255, 126, 126, 130);
		public static Color ToolGrayCheckBack = Color.FromArgb(255, 68, 68, 72);
		public static Color ToolGrayCheckBorder = Color.FromArgb(255, 180, 180, 186);
		public static Color ToolGrayCheckHover = Color.FromArgb(255, 92, 92, 96);
		public static Color ToolGrayCheckPressed = Color.FromArgb(255, 108, 108, 112);
		public static Color ToolWhiteToolStrip = Color.FromArgb(255, 220, 220, 220);
		public static Color ToolBlue = Color.FromArgb(255, 66, 125, 215);
		public static Color ToolBlueSelectedButton = Color.FromArgb(255, 68, 96, 127);
		
		// Grid 
		public static Color GridHeaderBackLight = Color.FromArgb(255, 22, 22, 22);
		public static Color GridBorders = Color.FromArgb(255, 17, 17, 17);
		public static Color GridSelectedHeaderColor = Color.FromArgb(255, 37, 37, 37);
		public static Color GridSelectedCellColor = Color.FromArgb(255, 52, 52, 52);
		public static Color GridCellFont = Color.FromArgb(255, 200, 200, 200);
		
		// Scrollbar
		public static Color ScrollbarBack = Color.FromArgb(255, 65, 65, 69);
		public static Color ScrollbarFront = Color.FromArgb(255, 102, 102, 106);
		public static Color ScrollbarArrow = Color.FromArgb(255, 152, 152, 156);

		// Player rating colors
		// Dynamic color by various statistical parameters.
		//"colorRating": {
		//  "very_bad":     "0xFE0E00",   // very bad   / очень плохо
		//  "bad":          "0xFE7903",   // bad        / плохо
		//  "normal":       "0xF8F400",   // normal     / средне
		//  "good":         "0x60FF00",   // good       / хорошо
		//  "very_good":    "0x02C9B3",   // very good  / очень хорошо
		//  "unique":       "0xD042F3"    // unique     / уникально
		//}
		public static Color Rating_very_bad = ColorTranslator.FromHtml("#FE0E00");
		public static Color Rating_bad = ColorTranslator.FromHtml("#FE7903");
		public static Color Rating_normal = ColorTranslator.FromHtml("#F8F400");
		public static Color Rating_good = ColorTranslator.FromHtml("#60FF00");
		public static Color Rating_very_good = ColorTranslator.FromHtml("#02C9B3");
		public static Color Rating_uniqe = ColorTranslator.FromHtml("#D042F3");
		
	}
}
