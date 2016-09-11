using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace WinApp.Code
{
	[DebuggerNonUserCode]
	public partial class ToolStripEx : ToolStrip
	{
        private void SetStyle()
        {
            this.GripStyle = ToolStripGripStyle.Hidden;
            this.Dock = DockStyle.None;
            this.AutoSize = false;
            this.ShowItemToolTips = true;
            this.Renderer = new StripRenderer();
        }

        public ToolStripEx()
		{
            SetStyle();
        }

		public ToolStripEx(IContainer container)
		{
            SetStyle();
            container.Add(this);
			InitializeComponent();
        }

        protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == NativeConstants.WM_MOUSEACTIVATE && m.Result == (IntPtr)NativeConstants.MA_ACTIVATEANDEAT)
			{
				m.Result = (IntPtr)NativeConstants.MA_ACTIVATE;
			}
		}
    }

    

	[DebuggerNonUserCode]
	public class StripLayout : ProfessionalColorTable
	{
		public override Color ButtonSelectedHighlight { get { return ButtonSelectedGradientMiddle; } }
		public override Color ButtonSelectedHighlightBorder { get { return ButtonSelectedBorder; } }
		public override Color ButtonPressedHighlight { get { return ButtonPressedGradientMiddle; } }
		public override Color ButtonPressedHighlightBorder { get { return ButtonPressedBorder; } }
		public override Color ButtonCheckedHighlight { get { return ColorTheme.ToolGrayCheckBack; } }
		public override Color ButtonCheckedHighlightBorder { get { return ColorTheme.ToolGrayCheckHover; } }
		public override Color ButtonPressedBorder { get { return ColorTheme.ToolGrayCheckHover; } }
		public override Color ButtonSelectedBorder { get { return ColorTheme.ToolGrayMain; } }
		public override Color ButtonCheckedGradientBegin { get { return ColorTheme.ToolBlueSelectedButton; } }// show selected view
		public override Color ButtonCheckedGradientMiddle { get { return ColorTheme.ToolBlueSelectedButton; } }// show selected view
		public override Color ButtonCheckedGradientEnd { get { return ColorTheme.ToolBlueSelectedButton; } }// show selected view
		public override Color ButtonSelectedGradientBegin { get { return ColorTheme.ToolGrayHover; } }
		public override Color ButtonSelectedGradientMiddle { get { return ColorTheme.ToolGrayHover; } }
		public override Color ButtonSelectedGradientEnd { get { return ColorTheme.ToolGrayHover; } }
		public override Color ButtonPressedGradientBegin { get { return ColorTheme.ToolBlueHoverButton; } }
		public override Color ButtonPressedGradientMiddle { get { return ColorTheme.ToolBlueHoverButton; } }
		public override Color ButtonPressedGradientEnd { get { return ColorTheme.ToolBlueHoverButton; } }
		public override Color CheckBackground { get { return ColorTheme.ToolGrayCheckBack; } }
		public override Color CheckSelectedBackground { get { return ColorTheme.ToolGrayCheckHover; } }
		public override Color CheckPressedBackground { get { return ColorTheme.ToolGrayCheckPressed; } }
		public override Color GripDark { get { return ColorTheme.ToolGrayScrollbarHover; } }
		public override Color GripLight { get { return ColorTheme.ToolGrayScrollbarHover; } }
		public override Color ImageMarginGradientBegin { get { return ColorTheme.ToolGrayMainBack; } }
		public override Color ImageMarginGradientMiddle { get { return ColorTheme.ToolGrayMainBack; } }
		public override Color ImageMarginGradientEnd { get { return ColorTheme.ToolGrayMainBack; } }
		public override Color ImageMarginRevealedGradientBegin { get { return Color.FromName("Red"); } }
		public override Color ImageMarginRevealedGradientMiddle { get { return Color.FromName("Red"); } }
		public override Color ImageMarginRevealedGradientEnd { get { return Color.FromName("Red"); } }
		public override Color MenuStripGradientBegin { get { return ColorTheme.ToolGrayMain; } }
		public override Color MenuStripGradientEnd { get { return ColorTheme.ToolGrayMain; } }
		public override Color MenuItemSelected { get { return ColorTheme.ToolGrayMain; } }
		public override Color MenuItemBorder { get { return ColorTheme.ToolGrayMain; } }
		public override Color MenuBorder { get { return ColorTheme.ToolGrayHover; } }
		public override Color MenuItemSelectedGradientBegin { get { return ColorTheme.ToolGrayHover; } }
		public override Color MenuItemSelectedGradientEnd { get { return ColorTheme.ToolGrayHover; } }
		public override Color MenuItemPressedGradientBegin { get { return ColorTheme.ToolGrayMainBack; } }
		public override Color MenuItemPressedGradientMiddle { get { return ColorTheme.ToolGrayMainBack; } }
		public override Color MenuItemPressedGradientEnd { get { return ColorTheme.ToolGrayMainBack; } }
		public override Color RaftingContainerGradientBegin { get { return Color.FromName("White"); } }
		public override Color RaftingContainerGradientEnd { get { return Color.FromName("White"); } }
		public override Color SeparatorDark { get { return ColorTheme.ToolGrayMain; } }
		public override Color SeparatorLight { get { return ColorTheme.ToolGrayScrollbarHover; } }
		public override Color StatusStripGradientBegin { get { return ColorTheme.ToolGrayMain; } }
		public override Color StatusStripGradientEnd { get { return ColorTheme.ToolGrayMain; } }
		public override Color ToolStripBorder { get { return ColorTheme.ToolGrayMain; } }
		public override Color ToolStripDropDownBackground { get { return ColorTheme.ToolGrayMainBack; } }
		public override Color ToolStripGradientBegin { get { return ColorTheme.ToolGrayMain; } }
		public override Color ToolStripGradientMiddle { get { return ColorTheme.ToolGrayMain; } }
		public override Color ToolStripGradientEnd { get { return ColorTheme.ToolGrayMain; } }
		public override Color ToolStripContentPanelGradientBegin { get { return ColorTheme.ToolGrayMain; } }
		public override Color ToolStripContentPanelGradientEnd { get { return ColorTheme.ToolGrayMain; } }
		public override Color ToolStripPanelGradientBegin { get { return ColorTheme.ToolGrayMain; } }
		public override Color ToolStripPanelGradientEnd { get { return ColorTheme.ToolGrayMain; } }
		public override Color OverflowButtonGradientBegin { get { return ColorTheme.ToolGrayMain; } }
		public override Color OverflowButtonGradientMiddle { get { return ColorTheme.ToolGrayMain; } }
		public override Color OverflowButtonGradientEnd { get { return ColorTheme.ToolGrayMain; } }
	}

	[DebuggerNonUserCode]
	internal sealed class NativeConstants
	{
		private NativeConstants()
		{
		}

		internal const uint WM_MOUSEACTIVATE = 0x21;
		internal const uint MA_ACTIVATE = 1;
		internal const uint MA_ACTIVATEANDEAT = 2;
		internal const uint MA_NOACTIVATE = 3;
		internal const uint MA_NOACTIVATEANDEAT = 4;
	}

	// [DebuggerNonUserCode]
	public class StripRenderer : ToolStripProfessionalRenderer
	{
		public StripRenderer() : base(new StripLayout())
		{
			this.RoundedEdges = false;
		}

		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			base.OnRenderItemText(e);
            if (e.Item.ForeColor == ColorTheme.ToolLabelHeading)
            {
                e.Item.Font = new Font(e.Item.Font.FontFamily, 13, GraphicsUnit.Pixel);
            }
            else
            {
                e.Item.ForeColor = ColorTheme.ToolWhiteToolStrip;
            }
		}

    }
}
