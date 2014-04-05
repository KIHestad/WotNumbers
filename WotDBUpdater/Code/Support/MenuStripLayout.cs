using System;
using System.Drawing;
using System.Windows.Forms;

namespace WotDBUpdater.Code.Support
{

	public class StripLayout : ProfessionalColorTable
	{
		// From Dark Grey
		

		public override Color ButtonSelectedHighlight
		{
			get { return ButtonSelectedGradientMiddle; }
		}
		public override Color ButtonSelectedHighlightBorder
		{
			get { return ButtonSelectedBorder; }
		}
		public override Color ButtonPressedHighlight
		{
			get { return ButtonPressedGradientMiddle; }
		}
		public override Color ButtonPressedHighlightBorder
		{
			get { return ButtonPressedBorder; }
		}
		public override Color ButtonCheckedHighlight
		{
			get { return ButtonCheckedGradientMiddle; }
		}
		public override Color ButtonCheckedHighlightBorder
		{
			get { return ButtonSelectedBorder; }
		}
		public override Color ButtonPressedBorder
		{
			get { return ButtonSelectedBorder; }
		}
		public override Color ButtonSelectedBorder
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color ButtonCheckedGradientBegin
		{
			get { return ColorTheme.colorBlueSelectedButton; } // show selected view
		}
		public override Color ButtonCheckedGradientMiddle
		{
			get { return ColorTheme.colorBlueSelectedButton; } // show selected view
		}
		public override Color ButtonCheckedGradientEnd
		{
			get { return ColorTheme.colorBlueSelectedButton; } // show selected view
		}
		public override Color ButtonSelectedGradientBegin
		{
			get { return ColorTheme.colorGrayHover; }
		}
		public override Color ButtonSelectedGradientMiddle
		{
			get { return ColorTheme.colorGrayHover; }
		}
		public override Color ButtonSelectedGradientEnd
		{
			get { return ColorTheme.colorGrayHover; }
		}
		public override Color ButtonPressedGradientBegin
		{
			get { return ColorTheme.colorBlue; }
		}
		public override Color ButtonPressedGradientMiddle
		{
			get { return ColorTheme.colorBlue; }
		}
		public override Color ButtonPressedGradientEnd
		{
			get { return ColorTheme.colorBlue; }
		}
		public override Color CheckBackground
		{
			get { return ColorTheme.colorGrayScrollbarHover; }
		}
		public override Color CheckSelectedBackground
		{
			get { return ColorTheme.colorGrayScrollbarHover; }
		}
		public override Color CheckPressedBackground
		{
			get { return ColorTheme.colorGrayCheckPressed; }
		}
		public override Color GripDark
		{
			get { return ColorTheme.colorGrayScrollbarHover; }
		}
		public override Color GripLight
		{
			get { return ColorTheme.colorGrayScrollbarHover; }
		}
		public override Color ImageMarginGradientBegin
		{
			get { return ColorTheme.colorGrayDropDownBack; }
		}
		public override Color ImageMarginGradientMiddle
		{
			get { return ColorTheme.colorGrayDropDownBack; }
		}
		public override Color ImageMarginGradientEnd
		{
			get { return ColorTheme.colorGrayDropDownBack; }
		}
		public override Color ImageMarginRevealedGradientBegin
		{
			get { return Color.FromName("Red"); }
		}
		public override Color ImageMarginRevealedGradientMiddle
		{
			get { return Color.FromName("Red"); }
		}
		public override Color ImageMarginRevealedGradientEnd
		{
			get { return Color.FromName("Red"); }
		}
		public override Color MenuStripGradientBegin
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color MenuStripGradientEnd
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color MenuItemSelected
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color MenuItemBorder
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color MenuBorder
		{
			get { return ColorTheme.colorGrayHover; }
		}
		public override Color MenuItemSelectedGradientBegin
		{
			get { return ColorTheme.colorGrayHover; }
		}
		public override Color MenuItemSelectedGradientEnd
		{
			get { return ColorTheme.colorGrayHover; }
		}
		public override Color MenuItemPressedGradientBegin
		{
			get { return ColorTheme.colorGrayDropDownBack; }
		}
		public override Color MenuItemPressedGradientMiddle
		{
			get { return ColorTheme.colorGrayDropDownBack; }
		}
		public override Color MenuItemPressedGradientEnd
		{
			get { return ColorTheme.colorGrayDropDownBack; }
		}
		public override Color RaftingContainerGradientBegin
		{
			get { return Color.FromName("White"); }
		}
		public override Color RaftingContainerGradientEnd
		{
			get { return Color.FromName("White"); }
		}
		public override Color SeparatorDark
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color SeparatorLight
		{
			get { return ColorTheme.colorGrayScrollbarHover; }
		}
		public override Color StatusStripGradientBegin
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color StatusStripGradientEnd
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color ToolStripBorder
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color ToolStripDropDownBackground
		{
			get { return ColorTheme.colorGrayDropDownBack; }
		}
		public override Color ToolStripGradientBegin
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color ToolStripGradientMiddle
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color ToolStripGradientEnd
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color ToolStripContentPanelGradientBegin
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color ToolStripContentPanelGradientEnd
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color ToolStripPanelGradientBegin
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color ToolStripPanelGradientEnd
		{
			get { return ColorTheme.colorGrayMain; }
		}
		public override Color OverflowButtonGradientBegin
		{
			get { return ColorTheme.colorGrayOutline; }
		}
		public override Color OverflowButtonGradientMiddle
		{
			get { return ColorTheme.colorGrayOutline; }
		}
		public override Color OverflowButtonGradientEnd
		{
			get { return ColorTheme.colorGrayOutline; }
		}
	}
}
