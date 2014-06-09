using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinApp.Code
{
	public class StripLayout : ProfessionalColorTable
	{
		public override Color ButtonSelectedHighlight				{get { return ButtonSelectedGradientMiddle; }}
		public override Color ButtonSelectedHighlightBorder			{get { return ButtonSelectedBorder; }}
		public override Color ButtonPressedHighlight				{get { return ButtonPressedGradientMiddle; }}
		public override Color ButtonPressedHighlightBorder			{get { return ButtonPressedBorder; }}
		public override Color ButtonCheckedHighlight				{get { return ColorTheme.ToolGrayCheckBack; }}
		public override Color ButtonCheckedHighlightBorder			{get { return ColorTheme.ToolGrayCheckHover; } }
		public override Color ButtonPressedBorder					{get { return ColorTheme.ToolGrayCheckHover; } }
		public override Color ButtonSelectedBorder					{get { return ColorTheme.ToolGrayMain; }}
		public override Color ButtonCheckedGradientBegin			{get { return ColorTheme.ToolBlueSelectedButton; }}// show selected view
		public override Color ButtonCheckedGradientMiddle			{get { return ColorTheme.ToolBlueSelectedButton; }}// show selected view
		public override Color ButtonCheckedGradientEnd				{get { return ColorTheme.ToolBlueSelectedButton; }}// show selected view
		public override Color ButtonSelectedGradientBegin			{get { return ColorTheme.ToolGrayHover; }}
		public override Color ButtonSelectedGradientMiddle			{get { return ColorTheme.ToolGrayHover; }}
		public override Color ButtonSelectedGradientEnd				{get { return ColorTheme.ToolGrayHover; }}
		public override Color ButtonPressedGradientBegin			{get { return ColorTheme.ToolBlue; }}
		public override Color ButtonPressedGradientMiddle			{get { return ColorTheme.ToolBlue; }}
		public override Color ButtonPressedGradientEnd				{get { return ColorTheme.ToolBlue; }}
		public override Color CheckBackground						{get { return ColorTheme.ToolGrayCheckBack; }}
		public override Color CheckSelectedBackground				{get { return ColorTheme.ToolGrayCheckHover; }}
		public override Color CheckPressedBackground				{get { return ColorTheme.ToolGrayCheckPressed; }}
		public override Color GripDark								{get { return ColorTheme.ToolGrayScrollbarHover; }}
		public override Color GripLight								{get { return ColorTheme.ToolGrayScrollbarHover; }}
		public override Color ImageMarginGradientBegin				{get { return ColorTheme.ToolGrayMainBack; }}
		public override Color ImageMarginGradientMiddle				{get { return ColorTheme.ToolGrayMainBack; }}
		public override Color ImageMarginGradientEnd				{get { return ColorTheme.ToolGrayMainBack; }}
		public override Color ImageMarginRevealedGradientBegin		{get { return Color.FromName("Red"); }}
		public override Color ImageMarginRevealedGradientMiddle		{get { return Color.FromName("Red"); }}
		public override Color ImageMarginRevealedGradientEnd		{get { return Color.FromName("Red"); }}
		public override Color MenuStripGradientBegin				{get { return ColorTheme.ToolGrayMain; }}
		public override Color MenuStripGradientEnd					{get { return ColorTheme.ToolGrayMain; }}
		public override Color MenuItemSelected						{get { return ColorTheme.ToolGrayMain; }}
		public override Color MenuItemBorder						{get { return ColorTheme.ToolGrayMain; }}
		public override Color MenuBorder							{get { return ColorTheme.ToolGrayHover; }}
		public override Color MenuItemSelectedGradientBegin			{get { return ColorTheme.ToolGrayHover; }}
		public override Color MenuItemSelectedGradientEnd			{get { return ColorTheme.ToolGrayHover; }}
		public override Color MenuItemPressedGradientBegin			{get { return ColorTheme.ToolGrayMainBack; }}
		public override Color MenuItemPressedGradientMiddle			{get { return ColorTheme.ToolGrayMainBack; }}
		public override Color MenuItemPressedGradientEnd			{get { return ColorTheme.ToolGrayMainBack; }}
		public override Color RaftingContainerGradientBegin			{get { return Color.FromName("White"); }}
		public override Color RaftingContainerGradientEnd			{get { return Color.FromName("White"); }}
		public override Color SeparatorDark							{get { return ColorTheme.ToolGrayMain; }}
		public override Color SeparatorLight						{get { return ColorTheme.ToolGrayScrollbarHover; }}
		public override Color StatusStripGradientBegin				{get { return ColorTheme.ToolGrayMain; }}
		public override Color StatusStripGradientEnd				{get { return ColorTheme.ToolGrayMain; }}
		public override Color ToolStripBorder						{get { return ColorTheme.ToolGrayMain; }}
		public override Color ToolStripDropDownBackground			{get { return ColorTheme.ToolGrayMainBack; }}
		public override Color ToolStripGradientBegin				{get { return ColorTheme.ToolGrayMain; }}
		public override Color ToolStripGradientMiddle				{get { return ColorTheme.ToolGrayMain; }}
		public override Color ToolStripGradientEnd					{get { return ColorTheme.ToolGrayMain; }}
		public override Color ToolStripContentPanelGradientBegin	{get { return ColorTheme.ToolGrayMain; }}
		public override Color ToolStripContentPanelGradientEnd		{get { return ColorTheme.ToolGrayMain; }}
		public override Color ToolStripPanelGradientBegin			{get { return ColorTheme.ToolGrayMain; }}
		public override Color ToolStripPanelGradientEnd				{get { return ColorTheme.ToolGrayMain; }}
		public override Color OverflowButtonGradientBegin			{get { return ColorTheme.ToolGrayMain; }}
		public override Color OverflowButtonGradientMiddle			{get { return ColorTheme.ToolGrayMain; }}
		public override Color OverflowButtonGradientEnd				{get { return ColorTheme.ToolGrayMain; }}
	}
}
