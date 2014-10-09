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
		
		// Gadget
		public static Color FormBackSelectedGadget = Color.FromArgb(255, 38, 38, 38);
		public static Color gadgetGrid = Color.FromArgb(255, 45, 45, 49);
		public static Color gadgetGridLight = Color.FromArgb(255, 57, 57, 61);
		public static Color gadgetOriginForMoved = Color.FromArgb(255, 75, 75, 79);

		// Controls
		public static Color ControlBack = Color.FromArgb(255, 55, 55, 59);
		public static Color ControlBackDark = Color.FromArgb(255, 45, 45, 49);
		public static Color ControlBackDarkMoving = Color.FromArgb(255, 24, 24, 24);
		public static Color ControlBorder = Color.FromArgb(255, 65, 65, 69);
		public static Color ControlBorderFocused = Color.FromArgb(255, 100, 100, 106);
		public static Color ControlBackMouseOver = Color.FromArgb(255, 68, 68, 72);
		public static Color ControlBackMouseDown = Color.FromArgb(255, 82, 82, 86);
		public static Color ControlFont = Color.FromArgb(255, 200, 200, 206);
		public static Color ControlFontHighLight = Color.FromArgb(255, 220, 220, 226);
		public static Color ControlDarkFont = Color.FromArgb(255, 130, 130, 136);
		public static Color ControlDisabledFont = Color.FromArgb(255, 100, 100, 106);
		public static Color ControlDimmedFont = Color.FromArgb(255, 85, 85, 89);
		public static Color ControlDarkRed = Color.FromArgb(255, 127, 0, 0);
		//public static Color ControlSeparatorGroupBoxBorder = Color.FromArgb(255, 75, 75, 79);
		public static Color ControlSeparatorGroupBoxBorder = Color.FromArgb(255, 55, 55, 59);
		
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
		public static Color GridColumnSeparator = Color.FromArgb(255, 22, 22, 22);
		public static Color GridColumnHeaderSeparator = Color.FromArgb(255, 17, 17, 17);
		public static Color GridTotalsRow = Color.FromArgb(255, 22, 22, 22);
		public static Color GridRowCurrentPlayerAlive = Color.FromArgb(255, 42, 42, 42);
		public static Color GridRowCurrentPlayerDead = Color.FromArgb(255, 54, 34, 34);
		public static Color GridRowPlayerDead = Color.FromArgb(255, 34, 24, 24);

		// Scrollbar
		public static Color ScrollbarBack = Color.FromArgb(255, 65, 65, 69);
		public static Color ScrollbarFront = Color.FromArgb(255, 102, 102, 106);
		public static Color ScrollbarArrow = Color.FromArgb(255, 152, 152, 156);

		// Charts
		public static Color ChartBarBlue = Color.FromArgb(255, 31, 71, 165);
		public static Color ChartBarRed = ColorTranslator.FromHtml("#A31F1F");
		public static Color ChartBarGreen = ColorTranslator.FromHtml("#1B8E30");
		public static Color ChartBarPurple = ColorTranslator.FromHtml("#761E99");
		public static Color ChartBarOcre = ColorTranslator.FromHtml("#896A1B");

		// Player rating colors - http://wiki.wnefficiency.net/pages/Color_Scale
		public static Color Rating_very_bad = ColorTranslator.FromHtml("#CE0000");		// dark red
		public static Color Rating_bad = ColorTranslator.FromHtml("#FF0000");			// red
		public static Color Rating_below_average = ColorTranslator.FromHtml("#FF8400");	// orange
		public static Color Rating_average = ColorTranslator.FromHtml("#FFFF00");		// yellow
		public static Color Rating_good = ColorTranslator.FromHtml("#4CFF00");			// green
		public static Color Rating_very_good = ColorTranslator.FromHtml("#2F9E00");		// dark green
		public static Color Rating_great = ColorTranslator.FromHtml("#30A8FF");			// blue
		public static Color Rating_uniqum = ColorTranslator.FromHtml("#CC5EFF");		// purple
		public static Color Rating_super_uniqum = ColorTranslator.FromHtml("#B200FF");	// deep purple

	}
}
