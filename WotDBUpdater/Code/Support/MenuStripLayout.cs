using System;
using System.Drawing;
using System.Windows.Forms;

namespace WotDBUpdater.Code.Support
{
    public class StripLayout : ProfessionalColorTable
    {
        // From Dark Grey
        public static Color colorGrayDropDownBack = Color.FromArgb(255, 22, 22, 22);
        public static Color colorGrayMain = Color.FromArgb(255, 45, 45, 49);
        public static Color colorGrayHover = Color.FromArgb(255, 68, 68, 72);
        public static Color colorGrayOutline = Color.FromArgb(255, 82, 82, 96);
        public static Color colorGrayScrollbar = Color.FromArgb(255, 102, 102, 106);
        public static Color colorGrayScrollbarHover = Color.FromArgb(255, 126, 126, 130);
        public static Color colorGrayCheckPressed = Color.FromArgb(255, 177, 177, 180);
        // To light grey

        // White
        public static Color colorWhiteToolStrip = Color.FromArgb(255, 240, 240, 240); 
        // Blue
        public static Color colorBlue = Color.FromArgb(255, 66, 125, 215);
        public static Color colorBlueSelectedButton = Color.FromArgb(255, 68, 96, 127);
        

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
            get { return colorGrayMain; }
        }
        public override Color ButtonCheckedGradientBegin
        {
            get { return colorBlueSelectedButton; } // show selected view
        }
        public override Color ButtonCheckedGradientMiddle
        {
            get { return colorBlueSelectedButton; } // show selected view
        }
        public override Color ButtonCheckedGradientEnd
        {
            get { return colorBlueSelectedButton; } // show selected view
        }
        public override Color ButtonSelectedGradientBegin
        {
            get { return colorGrayHover; }
        }
        public override Color ButtonSelectedGradientMiddle
        {
            get { return colorGrayHover; }
        }
        public override Color ButtonSelectedGradientEnd
        {
            get { return colorGrayHover; }
        }
        public override Color ButtonPressedGradientBegin
        {
            get { return colorBlue; }
        }
        public override Color ButtonPressedGradientMiddle
        {
            get { return colorBlue; }
        }
        public override Color ButtonPressedGradientEnd
        {
            get { return colorBlue; }
        }
        public override Color CheckBackground
        {
            get { return colorGrayScrollbarHover; }
        }
        public override Color CheckSelectedBackground
        {
            get { return colorGrayScrollbarHover; }
        }
        public override Color CheckPressedBackground
        {
            get { return colorGrayCheckPressed; }
        }
        public override Color GripDark
        {
            get { return colorGrayScrollbarHover; }
        }
        public override Color GripLight
        {
            get { return colorGrayScrollbarHover; }
        }
        public override Color ImageMarginGradientBegin
        {
            get { return colorGrayDropDownBack; }
        }
        public override Color ImageMarginGradientMiddle
        {
            get { return colorGrayDropDownBack; }
        }
        public override Color ImageMarginGradientEnd
        {
            get { return colorGrayDropDownBack; }
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
            get { return colorGrayMain; }
        }
        public override Color MenuStripGradientEnd
        {
            get { return colorGrayMain; }
        }
        public override Color MenuItemSelected
        {
            get { return colorGrayMain; }
        }
        public override Color MenuItemBorder
        {
            get { return colorGrayMain; }
        }
        public override Color MenuBorder
        {
            get { return colorGrayHover; }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return colorGrayHover; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return colorGrayHover; }
        }
        public override Color MenuItemPressedGradientBegin
        {
            get { return colorGrayDropDownBack; }
        }
        public override Color MenuItemPressedGradientMiddle
        {
            get { return colorGrayDropDownBack; }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get { return colorGrayDropDownBack; }
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
            get { return colorGrayMain; }
        }
        public override Color SeparatorLight
        {
            get { return colorGrayScrollbarHover; }
        }
        public override Color StatusStripGradientBegin
        {
            get { return colorGrayMain; }
        }
        public override Color StatusStripGradientEnd
        {
            get { return colorGrayMain; }
        }
        public override Color ToolStripBorder
        {
            get { return colorGrayMain; }
        }
        public override Color ToolStripDropDownBackground
        {
            get { return colorGrayDropDownBack; }
        }
        public override Color ToolStripGradientBegin
        {
            get { return colorGrayMain; }
        }
        public override Color ToolStripGradientMiddle
        {
            get { return colorGrayMain; }
        }
        public override Color ToolStripGradientEnd
        {
            get { return colorGrayMain; }
        }
        public override Color ToolStripContentPanelGradientBegin
        {
            get { return colorGrayMain; }
        }
        public override Color ToolStripContentPanelGradientEnd
        {
            get { return colorGrayMain; }
        }
        public override Color ToolStripPanelGradientBegin
        {
            get { return colorGrayMain; }
        }
        public override Color ToolStripPanelGradientEnd
        {
            get { return colorGrayMain; }
        }
        public override Color OverflowButtonGradientBegin
        {
            get { return colorGrayOutline; }
        }
        public override Color OverflowButtonGradientMiddle
        {
            get { return colorGrayOutline; }
        }
        public override Color OverflowButtonGradientEnd
        {
            get { return colorGrayOutline; }
        }
    }
}
