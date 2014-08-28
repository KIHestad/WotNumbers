///
/// GenTheme
/// Original vb.Net Creator, AeonHack
/// Converted to C# by Faded
/// www.EmuDevs.com
/// 

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WinApp.Code;

[DebuggerNonUserCode]
abstract class BadThemeContainerControl : ContainerControl
{

	protected Bitmap bitmapObject;
	protected Graphics graphicObject;
	public BadThemeContainerControl()
	{
		SetStyle((ControlStyles)8198, true);
		bitmapObject = new Bitmap(1, 1);
		graphicObject = Graphics.FromImage(bitmapObject);
	}

	private Cursor _Cursor = Cursors.Default;
	public override Cursor Cursor
	{
		get 
		{ 
			return _Cursor; 
		}
		set 
		{
			//if (Cursor != Cursors.WaitCursor)
			//{
				_Cursor = value;
				Invalidate();
			//}
		}
	}

	private bool _Resizable = true;
	public bool Resizable
	{
		get { return _Resizable; }
		set { _Resizable = value; }
	}

	private int _TitleHeight = 26; 
	public int TitleHeight
	{
		get { return _TitleHeight; }
		set
		{
			_TitleHeight = value;
			Header = new Rectangle(7 + FormMargin, 7, TitleWidht, _TitleHeight - FormMargin - 7);
		}
	}

	private int _TitleWidht;
	public int TitleWidht
	{
		get 
		{
			_TitleWidht = Width - 7 - (FormMargin * 2) - 1;
			if (SystemExitImage != null) _TitleWidht -= SystemExitImage.Width;
			if (SystemMaximizeImage != null) _TitleWidht -= SystemMaximizeImage.Width;
			if (SystemMinimizeImage != null) _TitleWidht -= SystemMinimizeImage.Width;
			return _TitleWidht;
		}
	}

	private Color _FormBorderColor = ColorTheme.FormBorderBlack;
	public Color FormBorderColor
	{
		get
		{
			if (_FormBorderColor == Color.FromArgb(0, 0, 0, 0)) _FormBorderColor = ColorTheme.FormBorderBlack;
			return _FormBorderColor;
		}
		set
		{
			_FormBorderColor = value;
		}
	}

	private bool _FormFooter = false;
	public bool FormFooter
	{
		get
		{
			return _FormFooter;
		}
		set
		{
			_FormFooter = value;
		}
	}

	private int _FormFooterHeight = 26;
	public int FormFooterHeight
	{
		get
		{
			return _FormFooterHeight;
		}
		set
		{
			_FormFooterHeight = value;
		}
	}

	private int _FormMargin = 0;
	public int FormMargin
	{
		get
		{
			return _FormMargin;
		}
		set
		{
			_FormMargin = value;
		}
	}

	private int _FormInnerBorder = 3;
	public int FormInnerBorder
	{
		get
		{
			return _FormInnerBorder;
		}
		set
		{
			_FormInnerBorder = value;
		}
	}

	public int ImageWidth
	{
		get
		{
			if (_Image == null)
				return 0;
			return _Image.Width;
		}
	}

	private Image _Image;
	public Image Image
	{
		get { return _Image; }
		set
		{
			_Image = value;
			Invalidate();
		}
	}


	public Color SystemExitImageBackColor = ColorTheme.FormBackTitle;
	private Image _SystemExitImage;
	public Image SystemExitImage
	{
		get { return _SystemExitImage; }
		set
		{
			_SystemExitImage = value;
			Invalidate();
		}
	}

	public Color SystemMaximizeImageBackColor = ColorTheme.FormBackTitle;
	private Image _SystemMaximizeImage;
	public Image SystemMaximizeImage
	{
		get { return _SystemMaximizeImage; }
		set
		{
			_SystemMaximizeImage = value;
			Invalidate();
		}
	}

	public Color SystemMinimizeImageBackColor = ColorTheme.FormBackTitle;
	private Image _SystemMinimizeImage;
	public Image SystemMinimizeImage
	{
		get { return _SystemMinimizeImage; }
		set
		{
			_SystemMinimizeImage = value;
			Invalidate();
		}
	}
	
	public void SetTransparent(Color c)
	{
		if (ParentIsForm)
			ParentForm.TransparencyKey = c;
	}

	public class MainAreaClass
	{
		public int Top;
		public int Left;
		public int Right;
		public int Width;
		public int Height;
		public int Bottom;
	}
	private MainAreaClass _MainArea;
	public MainAreaClass MainArea
	{
		get { return _MainArea; }
		set { _MainArea = value; }
	}

	public void SetMainAreaSize()
	{
		MainAreaClass calcMainArea = new MainAreaClass();
		calcMainArea.Top = FormMargin + TitleHeight + 1;
		calcMainArea.Left = FormMargin + 1 + FormInnerBorder;
		calcMainArea.Right = ClientRectangle.Width - FormMargin - FormInnerBorder - 1;
		calcMainArea.Width = ClientRectangle.Width - (FormMargin * 2) - (FormInnerBorder * 2) - 2;
		calcMainArea.Height = ClientRectangle.Height - (FormMargin * 2) - FormInnerBorder - 2 - TitleHeight;
		calcMainArea.Bottom = ClientRectangle.Height - FormMargin - FormInnerBorder - 1;
		if (FormFooter) calcMainArea.Height -= FormFooterHeight;
		MainArea = calcMainArea;
	}

	public void DrawCorners(Color c, Rectangle rect)
	{
		bitmapObject.SetPixel(rect.X, rect.Y, c);
		bitmapObject.SetPixel(rect.X + (rect.Width - 1), rect.Y, c);
		bitmapObject.SetPixel(rect.X, rect.Y + (rect.Height - 1), c);
		bitmapObject.SetPixel(rect.X + (rect.Width - 1), rect.Y + (rect.Height - 1), c);
	}

	public void DrawBorder(Pen outerPen, Rectangle rect, int BorderSize)
	{
		graphicObject.DrawRectangle(outerPen, rect.X + BorderSize, rect.Y + BorderSize, rect.Width - (BorderSize * 2) - 1, rect.Height - (BorderSize * 2) - 1);
	}

	public void DrawInnerBorder(Pen outerPen, Rectangle rect, int BorderSize, int InnerBorderWidth = 2)
	{
		for (int i = 1; i <= InnerBorderWidth; i++)
		{
			graphicObject.DrawRectangle(outerPen, rect.X + BorderSize + i, rect.Y + BorderSize + i, rect.Width - (BorderSize * 2) - (i * 2) - 1, rect.Height - (i * 2) - (BorderSize * 2) - 1);
		}
	}

	private Size TextSize;
	public void DrawText(HorizontalAlignment alignment, Brush brush) // Form text
	{
		int topPadding = FormMargin + 7;
		if (string.IsNullOrEmpty(Text))
			return;
		TextSize = graphicObject.MeasureString(Text, Font).ToSize();
		int imgPaddingAndWidth = 0;
		if (_Image != null)
		{
			imgPaddingAndWidth = Image.Width + 7;
			topPadding += 2;
		}
		switch (alignment)
		{
			case HorizontalAlignment.Left:
				graphicObject.DrawString(Text, Font, brush, 8 + imgPaddingAndWidth + FormMargin, topPadding);
				break;
			case HorizontalAlignment.Right:
				graphicObject.DrawString(Text, Font, brush, Width - 5 - TextSize.Width - Image.Width - imgPaddingAndWidth - FormMargin, topPadding);
				break;
			case HorizontalAlignment.Center:
				graphicObject.DrawString(Text, Font, brush, Width / 2 - TextSize.Width / 2, topPadding);
				break;
		}
	}

	public void DrawIcon(HorizontalAlignment a)
	{
		int topPadding = FormMargin + 5;
		if (_Image == null)
			return;
		switch (a)
		{
			case HorizontalAlignment.Left:
				graphicObject.DrawImage(_Image, 9 + FormMargin, topPadding);
				break;
			case HorizontalAlignment.Right:
				graphicObject.DrawImage(_Image, Width - 9 - TextSize.Width, topPadding);
				break;
			case HorizontalAlignment.Center:
				graphicObject.DrawImage(_Image, Width / 2 - TextSize.Width / 2, topPadding);
				break;
		}
	}

	public void PaintSysIcons()
	{
		if (ParentForm.WindowState != FormWindowState.Minimized)
		{
			graphicObject.Dispose();
			bitmapObject.Dispose();
			bitmapObject = new Bitmap(Width, Height);
			graphicObject = Graphics.FromImage(bitmapObject);
			Invalidate();
		}
	}

	public void AddSysIcons()
	{
		SolidBrush brush;
		// Add exit button
		int sysImgX = ClientRectangle.Width - FormMargin - 1;
		if (SystemExitImage != null)
		{
			sysImgX = sysImgX - SystemExitImage.Width;
			Rectangle rectangleSystemExit = new Rectangle(sysImgX, FormMargin + 1, SystemExitImage.Width, SystemExitImage.Height);
			brush = new SolidBrush(SystemExitImageBackColor);
			graphicObject.FillRectangle(brush, rectangleSystemExit);
			graphicObject.DrawImage(SystemExitImage, sysImgX, FormMargin + 1);

		}
		// Add max/normal button
		if (SystemMaximizeImage != null)
		{
			sysImgX = sysImgX - SystemMaximizeImage.Width;
			Rectangle rectangleSystemMaximize = new Rectangle(sysImgX, FormMargin + 1, SystemMaximizeImage.Width, SystemMaximizeImage.Height);
			brush = new SolidBrush(SystemMaximizeImageBackColor);
			graphicObject.FillRectangle(brush, rectangleSystemMaximize);
			graphicObject.DrawImage(SystemMaximizeImage, sysImgX, FormMargin + 1);
		}
		// Add min button
		if (SystemMinimizeImage != null)
		{
			sysImgX = sysImgX - SystemMinimizeImage.Width;
			Rectangle rectangleSystemMinimize = new Rectangle(sysImgX, FormMargin + 1, SystemMinimizeImage.Width, SystemMinimizeImage.Height);
			brush = new SolidBrush(SystemMinimizeImageBackColor);
			graphicObject.FillRectangle(brush, rectangleSystemMinimize);
			graphicObject.DrawImage(SystemMinimizeImage, sysImgX, FormMargin + 1);
		}
	}

	private struct Pointer
	{
		public readonly Cursor Cursor;
		public readonly byte Position;
		public Pointer(Cursor c, byte p)
		{
			Cursor = c;
			Position = p;
		}
	}

	private Pointer GetPointer()
	{
		Point PTC = PointToClient(MousePosition);
		// Border pointers
		bool FrameLeft   = PTC.X < 7;
		bool FrameRight  = PTC.X > Width - 8;
		bool FrameTop    = PTC.Y < 7;
		bool FrameBottom = PTC.Y > Height - 8;
		// Debug.WriteLine (PTC.X.ToString() + ", " + PTC.Y.ToString());
		// System Icons ponters
		bool SysIconArea = false; 
		int SysIconWidth = 0;
		if (SystemExitImage != null)
		{
			SysIconWidth += SystemExitImage.Width;
			if (SystemMaximizeImage != null) SysIconWidth += SystemMaximizeImage.Width;
			if (SystemMinimizeImage != null) SysIconWidth += SystemMinimizeImage.Width;
			SysIconArea = PTC.Y < SystemExitImage.Height + FormMargin + 1 &
							PTC.Y > 3 + FormMargin &  // Have to point 3 pixels down on system icons, if not resize
							PTC.X > Width - SysIconWidth - (FormMargin * 2) - 1 &
							PTC.X < Width - FormMargin - 4;
			// Check if pointing at system icon position
			if (SysIconWidth > 0 & SysIconArea)
			{
				int SysIconLeft = Width - FormMargin - 1;
				if (SystemExitImage != null)
				{
					SysIconLeft -= SystemExitImage.Width;
					if (PTC.X > SysIconLeft & PTC.X < SysIconLeft + SystemExitImage.Width)
						SystemExitImageBackColor = ColorTheme.ControlBackMouseOver;
					else
						SystemExitImageBackColor = ColorTheme.FormBackTitle;
				}
				if (SystemMaximizeImage != null)
				{
					SysIconLeft -= SystemMaximizeImage.Width;
					if (PTC.X > SysIconLeft & PTC.X < SysIconLeft + SystemMaximizeImage.Width)
						SystemMaximizeImageBackColor = ColorTheme.ControlBackMouseOver;
					else
						SystemMaximizeImageBackColor = ColorTheme.FormBackTitle;
				}
				if (SystemMinimizeImage != null)
				{
					SysIconLeft -= SystemMinimizeImage.Width;
					if (PTC.X > SysIconLeft & PTC.X < SysIconLeft + SystemMinimizeImage.Width)
						SystemMinimizeImageBackColor = ColorTheme.ControlBackMouseOver;
					else
						SystemMinimizeImageBackColor = ColorTheme.FormBackTitle;
				}
				PaintSysIcons();
				return new Pointer(Cursors.Default, 0);
			}
			else
			{
				// Draw default sys icon background
				SystemMinimizeImageBackColor = ColorTheme.FormBackTitle;
				SystemMaximizeImageBackColor = ColorTheme.FormBackTitle;
				SystemExitImageBackColor = ColorTheme.FormBackTitle;
				PaintSysIcons();
			}
		}
		// Check Border position for resizing
		if (_Resizable)
		{
			if (FrameLeft & FrameTop)
				return new Pointer(Cursors.SizeNWSE, 13);
			else if (FrameLeft & FrameBottom)
				return new Pointer(Cursors.SizeNESW, 16);
			else if (FrameRight & FrameTop)
				return new Pointer(Cursors.SizeNESW, 14);
			else if (FrameRight & FrameBottom)
				return new Pointer(Cursors.SizeNWSE, 17);
			else if (FrameLeft)
				return new Pointer(Cursors.SizeWE, 10);
			else if (FrameRight)
				return new Pointer(Cursors.SizeWE, 11);
			else if (FrameTop)
				return new Pointer(Cursors.SizeNS, 12);
			else if (FrameBottom)
				return new Pointer(Cursors.SizeNS, 15);
			else
				return new Pointer(Cursors.Default, 0);
		}
		return new Pointer(Cursors.Default, 0);
	}

	private Pointer Current;
	//private Pointer Pending;
	private void SetCurrent()
	{
		//Pending = GetPointer();
		//if (Current.Position == Pending.Position)
		//	return;
		Current = GetPointer();
		if (Cursor != Cursors.WaitCursor)
			Cursor = Current.Cursor;
	}

	protected override abstract void OnPaint(PaintEventArgs e);

	private bool ParentIsForm;
	protected override void OnHandleCreated(EventArgs e)
	{
		Dock = DockStyle.Fill;
		ParentIsForm = Parent is Form;
		if (ParentIsForm)
			ParentForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

		base.OnHandleCreated(e);
	}

	protected Rectangle Header;
	protected override void OnSizeChanged(EventArgs e)
	{
		if (ParentIsForm)
			if (ParentForm.WindowState == FormWindowState.Minimized)
				return;
		try
		{
			SetMainAreaSize();
			Header = new Rectangle(7 + FormMargin, 7, TitleWidht, _TitleHeight - FormMargin - 7);
			graphicObject.Dispose();
			bitmapObject.Dispose();
			bitmapObject = new Bitmap(Width, Height);
			graphicObject = Graphics.FromImage(bitmapObject);
			Invalidate();
			// Draw resize cursor if within resize area
			// Current = GetPointer();
			// Cursor = Current.Cursor;
		}
		catch (Exception ex)
		{
			Log.LogToFile(ex);
			//throw;
		}
		// Done
		base.OnSizeChanged(e);
	}

	private IntPtr Flag;
	protected override void OnMouseDown(MouseEventArgs e)
	{
		if (!(e.Button == MouseButtons.Left))
			return;
		if (e.Clicks == 2)
			return;
		if (ParentIsForm)
			if (ParentForm.WindowState == FormWindowState.Maximized)
				return;
		if (Header.Contains(e.Location))
		{
			Flag = new IntPtr(2);
		}
		else if (Current.Position == 0 | !_Resizable)
			return;
		else
			Flag = new IntPtr(Current.Position);

		Capture = false;
		Message msg = Message.Create(Parent.Handle, 161, Flag, IntPtr.Zero);
		DefWndProc(ref msg);
		base.OnMouseDown(e);
	}
		
	protected override void OnDoubleClick(EventArgs e)
	{
		//Header = new Rectangle(7 + FormMargin, 7, TitleWidht, _TitleHeight - FormMargin - 7);
		Point PTC = PointToClient(MousePosition);
		if (Resizable && Header.Contains(PTC))
		{
			if (ParentForm.WindowState == FormWindowState.Normal)
				ParentForm.WindowState = FormWindowState.Maximized;
			else
				ParentForm.WindowState = FormWindowState.Normal;
		}
		base.OnDoubleClick(e);
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		Point PTC = PointToClient(MousePosition);
		// System Icons ponters
		bool TitleArea = PTC.Y < TitleHeight + FormMargin + 1;
		int SysIconWidth = 0;
		if (SystemExitImage != null) SysIconWidth += SystemExitImage.Width;
		if (SystemMaximizeImage != null) SysIconWidth += SystemMaximizeImage.Width;
		if (SystemMinimizeImage != null) SysIconWidth += SystemMinimizeImage.Width;
		// Check System Icon position
		if (SysIconWidth > 0 & TitleArea & PTC.X > Width - SysIconWidth - (FormMargin * 2) - 1)
		{
			int SysIconLeft = Width - FormMargin - 1;
			if (SystemExitImage != null)
			{
				SysIconLeft -= SystemExitImage.Width;
				if (PTC.X > SysIconLeft & PTC.X < SysIconLeft + SystemExitImage.Width)
					ParentForm.Close();
			}
			if (SystemMaximizeImage != null)
			{
				SysIconLeft -= SystemMaximizeImage.Width;
				if (PTC.X > SysIconLeft & PTC.X < SysIconLeft + SystemMaximizeImage.Width)
					if (ParentForm.WindowState == FormWindowState.Normal)
						ParentForm.WindowState = FormWindowState.Maximized;
					else
						ParentForm.WindowState = FormWindowState.Normal;
			}
			if (SystemMinimizeImage != null)
			{
				SysIconLeft -= SystemMinimizeImage.Width;
				if (PTC.X > SysIconLeft & PTC.X < SysIconLeft + SystemMinimizeImage.Width)
					ParentForm.WindowState = FormWindowState.Minimized;
			}
		}
		// Set Main area values
		SetMainAreaSize();
		base.OnMouseUp(e);
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		// Draw default cursor when leave form focus
		if (Cursor != Cursors.WaitCursor)
			Cursor = Cursors.Default;
		// Draw default sys icon background
		SystemMinimizeImageBackColor = ColorTheme.FormBackTitle;
		SystemMaximizeImageBackColor = ColorTheme.FormBackTitle;
		SystemExitImageBackColor = ColorTheme.FormBackTitle;
		PaintSysIcons();
		// Continue
		base.OnMouseLeave(e);
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		SetCurrent();
		base.OnMouseMove(e);
	}

	
}

[DebuggerNonUserCode]
class BadForm : BadThemeContainerControl
{
	public BadForm()
	{
		SetTransparent(Color.Fuchsia); // Transparency color = purple (FF00FF)
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		graphicObject.Clear(Color.Fuchsia);
		SolidBrush brush;
		// Draw Main area
		Rectangle rectangleMain = new Rectangle(FormMargin + 1, TitleHeight + FormMargin + 1, ClientRectangle.Width - (FormMargin * 2) - 2, ClientRectangle.Height - TitleHeight - (FormMargin * 2) - 1);
		brush = new SolidBrush(ColorTheme.FormBack);
		graphicObject.FillRectangle(brush, rectangleMain);
		// Footer
		if (FormFooter)
		{
			Rectangle rectangleFooter = new Rectangle(FormMargin + 1, ClientRectangle.Height - FormFooterHeight - FormMargin - FormInnerBorder - 1, ClientRectangle.Width - (FormMargin * 2) - 2, FormFooterHeight);
			brush = new SolidBrush(ColorTheme.FormBackFooter);
			graphicObject.FillRectangle(brush, rectangleFooter);
		}
		// Add outer border
		Pen FormBorderPenColor = new Pen(FormBorderColor);
		DrawBorder(FormBorderPenColor, ClientRectangle, FormMargin); // Outer Border
		// Add inner border
		FormBorderPenColor = new Pen(ColorTheme.FormBack); // Alternative color: FormBackTitle
		DrawInnerBorder(FormBorderPenColor, ClientRectangle, FormMargin, FormInnerBorder); // Inner Border
		// Draw title 
		Rectangle rectangleTitle = new Rectangle(FormMargin + 1, FormMargin + 1, ClientRectangle.Width - (FormMargin * 2) - 2, TitleHeight);
		brush = new SolidBrush(ColorTheme.FormBackTitle);
		graphicObject.FillRectangle(brush, rectangleTitle);
		// Draw sys icons
		AddSysIcons(); // Add Sys Icons in title bar
		// Add title icon and text
		DrawText(HorizontalAlignment.Left, new SolidBrush(ColorTheme.FormBackTitleFont)); // Add title text
		DrawIcon(HorizontalAlignment.Left); // Add title icon

		// Set Main area values
		SetMainAreaSize();

		// Draw theme on form
		e.Graphics.DrawImage(bitmapObject, 0, 0);
	}

	
}

[DebuggerNonUserCode]
abstract class BadThemeControl : Control
{
	protected Bitmap bitmapObject;
	protected Graphics grapichObject;

	private Cursor _Cursor = Cursors.Default;
	public override Cursor Cursor
	{
		get 
		{ 
			Form frm = Parent.FindForm();
			if (frm.Cursor == Cursors.WaitCursor)
				return Cursors.WaitCursor;
			else
				return _Cursor;
		}
		set	
		{
			Form frm = Parent.FindForm();
			if (frm.Cursor != Cursors.WaitCursor)
			{
				_Cursor = value;
			}
			else
				_Cursor = Cursors.WaitCursor;
			Invalidate();
		}
	}
		
	public BadThemeControl()
	{
		SetStyle((ControlStyles)8198, true);
		bitmapObject = new Bitmap(1, 1);
		grapichObject = Graphics.FromImage(bitmapObject);
	}

	public void AllowTransparent()
	{
		SetStyle(ControlStyles.Opaque, false);
		SetStyle((ControlStyles)141314, true);
	}

	public enum State : byte
	{
		MouseNone = 0,
		MouseOver = 1,
		MouseDown = 2
	}

	protected State MouseState;
	protected override void OnMouseLeave(EventArgs e)
	{
		ChangeMouseState(State.MouseNone);
		base.OnMouseLeave(e);
	}
	protected override void OnMouseEnter(EventArgs e)
	{
		ChangeMouseState(State.MouseOver);
		base.OnMouseEnter(e);
	}
	protected override void OnMouseUp(MouseEventArgs e)
	{
		ChangeMouseState(State.MouseOver);
		base.OnMouseUp(e);
	}
	protected override void OnMouseDown(MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
			ChangeMouseState(State.MouseDown);
		base.OnMouseDown(e);
	}

	private void ChangeMouseState(State e)
	{
		MouseState = e;
		Invalidate();
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		try
		{
			grapichObject.Dispose();
			bitmapObject.Dispose();
			bitmapObject = new Bitmap(Width, Height);
			grapichObject = Graphics.FromImage(bitmapObject);
			Invalidate();
			base.OnSizeChanged(e);
		}
		catch (Exception ex)
		{
			Log.LogToFile(ex);
			//throw;
		}
		
	}

	protected override abstract void OnPaint(PaintEventArgs e);

	public void DrawCorners(Color c, Rectangle rect)
	{
		bitmapObject.SetPixel(rect.X, rect.Y, c);
		bitmapObject.SetPixel(rect.X + (rect.Width - 1), rect.Y, c);
		bitmapObject.SetPixel(rect.X, rect.Y + (rect.Height - 1), c);
		bitmapObject.SetPixel(rect.X + (rect.Width - 1), rect.Y + (rect.Height - 1), c);
	}

	public void DrawBorders(Pen outerPen, Pen innerPen, Rectangle rect)
	{
		grapichObject.DrawRectangle(outerPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
		grapichObject.DrawRectangle(innerPen, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3);
	}

	public void DrawBorders(Pen outerPen, Rectangle rect)
	{
		grapichObject.DrawRectangle(outerPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
	}


	private Size TextSize;
	public void DrawText(HorizontalAlignment horizontalAlignment, Brush brush, int offset = 0, bool verticalAlignmentMiddle = true) 
	{
		if (string.IsNullOrEmpty(Text))
		{
			TextSize = new Size(0, 13); // Default text heught
			return;
		}
		TextSize = grapichObject.MeasureString(Text, Font).ToSize();

		int yPos = 0;
		if (verticalAlignmentMiddle)
			yPos = Height / 2 - TextSize.Height / 2 - 1;

		switch (horizontalAlignment)
		{
			case HorizontalAlignment.Left:
				grapichObject.DrawString(Text, Font, brush, 5 + offset, yPos);
				break;
			case HorizontalAlignment.Right:
				grapichObject.DrawString(Text, Font, brush, Width - 5 - TextSize.Width - offset, yPos);
				break;
			case HorizontalAlignment.Center:
				grapichObject.DrawString(Text, Font, brush, (Width / 2 - TextSize.Width / 2) + offset, yPos);
				break;
		}
	}

	public int ImageWidth
	{
		get
		{
			if (_Image == null)
				return 0;
			return _Image.Width;
		}
	}

	private Image _Image;
	public Image Image
	{
		get { return _Image; }
		set
		{
			_Image = value;
			Invalidate();
		}
	}

	public void DrawIcon(HorizontalAlignment a, int xOffset = 0, int yOffset = 0)
	{
		if (_Image == null)
			return;
		switch (a)
		{
			case HorizontalAlignment.Left:
				grapichObject.DrawImage(_Image, Width / 10 + xOffset, Height / 2 - _Image.Height / 2 + yOffset);
				break;
			case HorizontalAlignment.Right:
				grapichObject.DrawImage(_Image, Width - _Image.Width - 2, Height / 2 - TextSize.Height / 2 + yOffset);
				break;
			case HorizontalAlignment.Center:
				grapichObject.DrawImage(_Image, Width / 2 - TextSize.Width / 2, Height / 2 - TextSize.Height / 2 + yOffset);
				break;
		}
	}

	public void PaintControl()
	{
		grapichObject.Dispose();
		bitmapObject.Dispose();
		bitmapObject = new Bitmap(Width, Height);
		grapichObject = Graphics.FromImage(bitmapObject);
		Invalidate();
	}
}

[DebuggerNonUserCode]
class BadButton : BadThemeControl
{
	public ToolTip ToolTipContainer
	{
		get;
		set;
	}

	private string _ToolTipText = "";
	public string ToolTipText
	{
		get { return _ToolTipText; }
		set
		{
			_ToolTipText = value;
			//Invalidate();
		}
	}

	private bool _Checked = false;
	public bool Checked
	{
		get { return _Checked; }
		set { _Checked = value; Invalidate();  }
	}

	private bool _BlackButton = false;
	public bool BlackButton
	{
		get { return _BlackButton; }
		set { _BlackButton = value; Invalidate(); }
	}

	public BadButton()
	{
		AllowTransparent();
	}

	protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
	{
		SolidBrush brushBackColor = new SolidBrush(ColorTheme.ControlBack);
		SolidBrush fontColor = new SolidBrush(ColorTheme.ControlFont);
		if (BlackButton)
			brushBackColor = new SolidBrush(ColorTheme.GridHeaderBackLight);
		if (Checked)
		{
			if (!BlackButton)
				brushBackColor = new SolidBrush(ColorTheme.ControlBackMouseDown);
			else
				brushBackColor = new SolidBrush(ColorTheme.ControlBackDark);
			fontColor = new SolidBrush(ColorTheme.ControlFontHighLight);
		}
		if (MouseState == State.MouseDown)
			brushBackColor = new SolidBrush(ColorTheme.ControlBackMouseDown);
		else if (MouseState == State.MouseOver)
			brushBackColor = new SolidBrush(ColorTheme.ControlBackMouseOver);
		grapichObject.FillRectangle(brushBackColor, ClientRectangle);
		if (!Enabled) fontColor = new SolidBrush(ColorTheme.ControlDisabledFont);
		DrawText(HorizontalAlignment.Center, fontColor, 0);
		DrawIcon(HorizontalAlignment.Left);
		if (Focused)
		{
			grapichObject.DrawRectangle(new Pen(ColorTheme.ControlBorderFocused), 0, 0, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
		}

		//Pen outerBorderPen = new Pen(Color.Black); // Button border
		//DrawBorders(outerBorderPen, ClientRectangle);
		//DrawCorners(BackColor, ClientRectangle); // Button corners

		ToolTipContainer = new ToolTip();

		e.Graphics.DrawImage(bitmapObject, 0, 0);
	}

	protected override void OnGotFocus(EventArgs e)
	{
		Invalidate();
		base.OnGotFocus(e);
	}

	protected override void OnLostFocus(EventArgs e)
	{
		Invalidate();
		base.OnLostFocus(e);
	}

	protected override void OnTextChanged(EventArgs e)
	{
		Invalidate();
		base.OnTextChanged(e);
	}

	protected override void OnMouseHover(EventArgs e)
	{
		Point p = PointToClient(MousePosition);
		p.X += 15;
		p.Y += 10;
		ToolTipContainer.Show(ToolTipText, this, p);
		base.OnMouseHover(e);
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		ToolTipContainer.Hide(this);
		base.OnMouseLeave(e);
	}
}

[DebuggerNonUserCode]
class BadCheckBox : BadThemeControl
{
	public BadCheckBox()
	{
		AllowTransparent();
		BackColor = Color.Transparent;
	}

	private bool _Checked = false;
	public bool Checked
	{
		get { return _Checked; }
		set { _Checked = value; Invalidate(); }
	}

	protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
	{
		grapichObject.Clear(BackColor);
		// Outer Border
		SolidBrush BorderColor = new SolidBrush(ColorTheme.ControlBackMouseDown);
		Rectangle GroupBoxOuter = new Rectangle(5, 4, 14, 15);
		grapichObject.FillRectangle(BorderColor, GroupBoxOuter);
		// Inner Area
		BorderColor = new SolidBrush(ColorTheme.FormBack);
		Rectangle GroupBoxInner = new Rectangle(6, 5, 12, 13);
		grapichObject.FillRectangle(BorderColor, GroupBoxInner);
		// Text
		SolidBrush fontColor = new SolidBrush(ColorTheme.ControlFont);
		if (!Enabled) fontColor = new SolidBrush(ColorTheme.ControlDisabledFont);
		DrawText(HorizontalAlignment.Left, fontColor, 18);
		// Chk image
		if (Image != null && Checked)
			grapichObject.DrawImage(Image, 6, 5, 12, 12);
		if (Focused)
		{
			grapichObject.DrawRectangle(new Pen(ColorTheme.ControlBorderFocused), 0, 0, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
		}
		e.Graphics.DrawImage(bitmapObject, 0, 0);
	}

	protected override void OnClick(EventArgs e)
	{
		Checked = !Checked;
		base.OnClick(e);
	}

	protected override void OnGotFocus(EventArgs e)
	{
		Invalidate();
		base.OnGotFocus(e);
	}

	protected override void OnLostFocus(EventArgs e)
	{
		Invalidate();
		base.OnLostFocus(e);
	}

	protected override void OnKeyPress(KeyPressEventArgs e)
	{
		if (e.KeyChar == (char)Keys.Space)
		{
			Checked = !Checked;
			base.OnClick(e);
		}
		base.OnKeyPress(e);
	}
}

[DebuggerNonUserCode]
class BadSeperator : BadThemeControl
{

	public BadSeperator()
	{
		AllowTransparent();
		BackColor = Color.Transparent;
	}

	private Orientation _Direction;
	public Orientation Direction
	{
		get { return _Direction; }
		set
		{
			_Direction = value;
			Invalidate();
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		grapichObject.Clear(BackColor);
		Color SeparatorColor = ColorTheme.ControlSeparatorGroupBoxBorder;
		if (_Direction == Orientation.Horizontal)
		{
			int Yoffset = 0;
			grapichObject.DrawLine(new Pen(SeparatorColor), 0, Height / 2 + Yoffset, Width, Height / 2 + Yoffset);
			//grapichObject.DrawLine(new Pen(SeparatorColor), 0, Height / 2 - 1 + Yoffset, Width, Height / 2 - 1 + Yoffset);
			//grapichObject.DrawLine(new Pen(SeparatorColor), 0, Height / 2 + 1 + Yoffset, Width, Height / 2 + 1 + Yoffset);
			if (Text != "")
			{
				int Xoffset = 10;
				SolidBrush brushBackColor;
				brushBackColor = new SolidBrush(ColorTheme.FormBack);
				Size textSize = grapichObject.MeasureString(Text, Font).ToSize();
				Rectangle rectangleTextBack = new Rectangle(4 + Xoffset, 0, textSize.Width + 2, Height);
				grapichObject.FillRectangle(brushBackColor, rectangleTextBack);
				DrawText(HorizontalAlignment.Left, new SolidBrush(ColorTheme.ControlDarkFont), Xoffset);
			}
		}
		else
		{
			grapichObject.DrawLine(new Pen(SeparatorColor), Width / 2, 0, Width / 2, Height);
			//grapichObject.DrawLine(new Pen(SeparatorColor), Width / 2 -1, 0, Width / 2 -1, Height);
			//grapichObject.DrawLine(new Pen(SeparatorColor), Width / 2 +1, 0, Width / 2 +1, Height);
		}

		e.Graphics.DrawImage(bitmapObject, 0, 0);
	}
}

[DebuggerNonUserCode]
class BadProgressBar : BadThemeControl
{

	private bool _ProgressBarColorMode = false;
	public bool ProgressBarColorMode
	{
		get { return _ProgressBarColorMode; }
		set { _ProgressBarColorMode = value; Invalidate(); }
	}

	private int _ProgressBarMargins = 2;
	public int ProgressBarMargins
	{
		get { return _ProgressBarMargins; }
		set 
		{
			int i = value;
			if (i < 0) i = 0;
			if (i > 4) i = 4;
			_ProgressBarMargins = i;
			Invalidate(); 
		}
	}

	private bool _ProgressBarShowPercentage = false;
	public bool ProgressBarShowPercentage
	{
		get { return _ProgressBarShowPercentage; }
		set { _ProgressBarShowPercentage = value; Invalidate(); }
	}


	private double _ValueMax = 100;
	public double ValueMax
	{
		get { return _ValueMax; }
		set { _ValueMax = value; Invalidate(); }
	}

	private double _ValueMin = 0;
	public double ValueMin
	{
		get { return _ValueMin; }
		set { _ValueMin = value; Invalidate(); }
	}

	private double _Value = 0;
	public double Value
	{
		get { return _Value; }
		set { _Value = value; Invalidate(); }
	}
	
	public BadProgressBar()
	{
		AllowTransparent();
		BackColor = Color.Transparent;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		grapichObject.Clear(BackColor);
		// Background
		SolidBrush brushBackColor = new SolidBrush(ColorTheme.ControlBorder);
		grapichObject.FillRectangle(brushBackColor, ClientRectangle);
		// Inner
		brushBackColor = new SolidBrush(ColorTheme.FormBack);
		int borderWidth = 1;
		grapichObject.FillRectangle(brushBackColor, borderWidth, borderWidth, Width - (borderWidth*2), Height - (borderWidth*2));
		// Progress
		int progressBarOffset = borderWidth + ProgressBarMargins;
		Double progress = Value;
		if (progress > ValueMax) progress = ValueMax;
		if (progress < ValueMin) progress = ValueMin;
		if ((ValueMax - ValueMin) != 0)
			progress = (progress / (ValueMax - ValueMin)) * (Width - (progressBarOffset*2));
		int percentage = Convert.ToInt32(Value * 100 / (ValueMax - ValueMin));
		// Colormode
		Color color = ColorTheme.FormBorderBlue; // Default if not color mode is selected
		if (ProgressBarColorMode)
		{
			color = ColorTheme.Rating_very_bad;
			if (percentage >= 99) color = ColorTheme.Rating_super_uniqum;
			else if (percentage >= 95) color = ColorTheme.Rating_uniqum;
			else if (percentage >= 90) color = ColorTheme.Rating_great;
			else if (percentage >= 80) color = ColorTheme.Rating_very_good;
			else if (percentage >= 65) color = ColorTheme.Rating_good;
			else if (percentage >= 50) color = ColorTheme.Rating_average;
			else if (percentage >= 35) color = ColorTheme.Rating_below_average;
			else if (percentage >= 20) color = ColorTheme.Rating_bad;
			
		}
		brushBackColor = new SolidBrush(color);
		grapichObject.FillRectangle(brushBackColor, progressBarOffset, progressBarOffset, Convert.ToInt32(progress), Height - (progressBarOffset*2));
		// Percentage text
		if (ProgressBarShowPercentage)
		{
			Text = percentage.ToString() + "%";
			SolidBrush brushFontColor = new SolidBrush(ColorTheme.ControlFont);
			if (ProgressBarColorMode)
			{
				int offset = 0;
				if (percentage > 50) brushFontColor = new SolidBrush(Color.Black);
				if (percentage > 40 && percentage <= 50) offset = (int)((percentage - 40) * 1.5);
				if (percentage > 50 && percentage < 60) offset = (int)((percentage - 60) * 1.5);
				DrawText(HorizontalAlignment.Center, brushFontColor, offset);
			}
			else
			{
				DrawText(HorizontalAlignment.Center, brushFontColor, 0);
			}
		
		}
		// Draw
		e.Graphics.DrawImage(bitmapObject, 0, 0);
	}

}

[DebuggerNonUserCode]
class BadGroupBox : BadThemeControl
{

	public BadGroupBox()
	{
		AllowTransparent();
		BackColor = Color.Transparent;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		grapichObject.Clear(BackColor);
		int Yoffset = 7;
		// Outer Border
		SolidBrush BorderColor = new SolidBrush(ColorTheme.ControlSeparatorGroupBoxBorder); 
		Rectangle GroupBoxOuter = new Rectangle(0, Yoffset, ClientRectangle.Width, ClientRectangle.Height - Yoffset);
		grapichObject.FillRectangle(BorderColor, GroupBoxOuter);
		// Inner Area
		BorderColor = new SolidBrush(ColorTheme.FormBack);
		Rectangle GroupBoxInner = new Rectangle(1, Yoffset+1, ClientRectangle.Width-2, ClientRectangle.Height - Yoffset - 2);
		grapichObject.FillRectangle(BorderColor, GroupBoxInner);
		
		if (Text != "")
		{
			int Xoffset = 10;
			SolidBrush brushBackColor;
			brushBackColor = new SolidBrush(ColorTheme.FormBack);
			Size textSize = grapichObject.MeasureString(Text, Font).ToSize();
			Rectangle rectangleTextBack = new Rectangle(4 + Xoffset, 0, textSize.Width + 2, textSize.Height);
			grapichObject.FillRectangle(brushBackColor, rectangleTextBack);
			DrawText(HorizontalAlignment.Left, new SolidBrush(ColorTheme.ControlDarkFont), Xoffset, false); 
		}
		
		e.Graphics.DrawImage(bitmapObject, 0, 0);
	}

	protected override void OnTextChanged(EventArgs e)
	{
		Invalidate();
		base.OnTextChanged(e);
	}
}

[DebuggerNonUserCode]
class BadLabel : BadThemeControl
{
	private bool _Dimmed = false;
	public bool Dimmed
	{
		get { return _Dimmed; }
		set { _Dimmed = value; Invalidate(); }
	}


	private Color _FontColor = ColorTheme.ControlFont;
	public Color FontColor
	{
		get { return _FontColor; }
		set { _FontColor = value; Invalidate(); }
	}

	Label label = new Label();
	public BadLabel()
	{
		AllowTransparent();
		BackColor = ColorTheme.FormBack;
		label.BackColor = ColorTheme.FormBack;
		//label.ForeColor = ColorTheme.ControlFont;
		label.Top = 5;
		this.Controls.Add(label);
	}

	protected override void OnTextChanged(EventArgs e)
	{
		label.Text = Text;
		base.OnTextChanged(e);
	}
	
	protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
	{
		label.Text = Text;
		ForeColor = FontColor;
		if (Dimmed)
			label.ForeColor = ColorTheme.ControlDimmedFont;
		else
			label.ForeColor = ForeColor;
		e.Graphics.DrawImage(bitmapObject, 0, 0);
	}

	protected override void OnResize(EventArgs e)
	{
		label.Width = ClientRectangle.Width ;
		label.Height = ClientRectangle.Height ;
	}
}

[DebuggerNonUserCode]
class BadTextBox : BadThemeControl
{
	private char _PasswordChar;
	public char PasswordChar
	{
		get { return _PasswordChar; }
		set { _PasswordChar = value; }
	}

	private HorizontalAlignment _TextAlign;
	public HorizontalAlignment TextAlign
	{
		get { return _TextAlign; }
		set { _TextAlign = value; }
	}

	private bool _MultilineAllow = false;
	public bool MultilineAllow
	{
		get { return _MultilineAllow; }
		set { _MultilineAllow = value; Invalidate(); }
	}

	private bool _HasFocus;
	public bool HasFocus
	{
		get { return _HasFocus; }
		set { _HasFocus = value; }
	}

	TextBox textBox = new TextBox();
	public BadTextBox()
	{
		AllowTransparent();
		textBox.BackColor = ColorTheme.FormBack;
		textBox.ForeColor = ColorTheme.ControlFont;
		textBox.BorderStyle = BorderStyle.None;
		textBox.Top = 5;
		textBox.Left = 6;
		this.Controls.Add(textBox);
		textBox.TextChanged += new EventHandler(textBox_TextChanged);
		textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
	}

	protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
	{
		// Outer Border
		SolidBrush BorderColor = new SolidBrush(ColorTheme.ControlBorder);
		Rectangle GroupBoxOuter = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
		grapichObject.FillRectangle(BorderColor, GroupBoxOuter);
		// Inner Area
		BorderColor = new SolidBrush(ColorTheme.FormBack);
		int borderWidth = 1;
		Rectangle GroupBoxInner = new Rectangle(borderWidth, borderWidth, ClientRectangle.Width - (borderWidth * 2), ClientRectangle.Height - (borderWidth*2));
		grapichObject.FillRectangle(BorderColor, GroupBoxInner);
		//textBox.Font = new Font(textBox.Font.FontFamily, 12, GraphicsUnit.Pixel);
		textBox.Multiline = MultilineAllow;
		textBox.Height = Height - 7;
		textBox.Width = Width - 12;
		textBox.PasswordChar = PasswordChar;
		textBox.TextAlign = TextAlign;
		textBox.Text = Text;
		e.Graphics.DrawImage(bitmapObject, 0, 0);
	}

	protected override void OnResize(EventArgs e)
	{
		textBox.Width = ClientRectangle.Width -10;
		textBox.Height = ClientRectangle.Height -8;
	}

	private void textBox_TextChanged(object sender, EventArgs e)
	{
		Text = textBox.Text;
	}

	protected override void OnTextChanged(EventArgs e)
	{
		textBox.Text = Text;
		base.OnTextChanged(e);
	}

	protected override void OnEnter(EventArgs e)
	{
		HasFocus = true;
		base.OnEnter(e);
	}

	protected override void OnLeave(EventArgs e)
	{
		HasFocus = false;
		base.OnLeave(e);
	}

	protected void textBox_KeyPress(object serder, KeyPressEventArgs e)
	{
		base.OnKeyPress(e);
	}

}

[DebuggerNonUserCode]
class BadPopupBox : BadThemeControl
{

	public BadPopupBox()
	{
		AllowTransparent();
	}

	private string _Text;
	public override string Text 
	{
		get { return _Text; }
		set { _Text = value; Invalidate(); }
	}

	protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
	{
		SolidBrush brushBackColor; 
		if (MouseState == State.MouseDown)
			brushBackColor = new SolidBrush(ColorTheme.ControlBackMouseDown);
		else if (MouseState == State.MouseOver)
			brushBackColor = new SolidBrush(ColorTheme.ControlBackMouseOver);
		else
			brushBackColor = new SolidBrush(ColorTheme.ControlBack);
		grapichObject.FillRectangle(brushBackColor, ClientRectangle);
		// Text
		DrawText(HorizontalAlignment.Left, new SolidBrush(ColorTheme.ControlFont));
		// Overwrite text to right to avoid hiding dropdown icon
		if (Image != null)
		{
			if (MouseState == State.MouseDown)
				brushBackColor = new SolidBrush(ColorTheme.ControlBackMouseDown);
			else if (MouseState == State.MouseOver)
				brushBackColor = new SolidBrush(ColorTheme.ControlBackMouseOver);
			else
				brushBackColor = new SolidBrush(ColorTheme.ControlBack);
			grapichObject.FillRectangle(brushBackColor, Width - Image.Width -4, 0, Image.Width + 4, Height);
			// Dropdown icon
			DrawIcon(HorizontalAlignment.Right,0,-2);
		}
		e.Graphics.DrawImage(bitmapObject, 0, 0);
	}

}

[DebuggerNonUserCode]
class BadDropDownBox : BadThemeControl
{

	public BadDropDownBox()
	{
		AllowTransparent();
	}

	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		Invalidate();
	}

	protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
	{
		SolidBrush brushBackColor;
		if (MouseState == State.MouseDown)
			brushBackColor = new SolidBrush(ColorTheme.ControlBackMouseDown);
		else if (MouseState == State.MouseOver)
			brushBackColor = new SolidBrush(ColorTheme.ControlBackMouseOver);
		else
			brushBackColor = new SolidBrush(ColorTheme.ControlBack);
		grapichObject.FillRectangle(brushBackColor, ClientRectangle);
		// Text
		DrawText(HorizontalAlignment.Left, new SolidBrush(ColorTheme.ControlFont));
		// Overwrite text to right to avoid hiding dropdown icon
		if (MouseState == State.MouseDown)
			brushBackColor = new SolidBrush(ColorTheme.ControlBackMouseDown);
		else if (MouseState == State.MouseOver)
			brushBackColor = new SolidBrush(ColorTheme.ControlBackMouseOver);
		else
			brushBackColor = new SolidBrush(ColorTheme.ControlBack);
		// DropDows Arrow Area
		grapichObject.FillRectangle(brushBackColor, Width - 22, 0, 22, Height);
		// DropDown Arrow
		brushBackColor = new SolidBrush(ColorTheme.ScrollbarArrow);
		int ArrowY = (Height / 2) + 2;
		int ArrowX = 12;
		grapichObject.FillRectangle(brushBackColor, Width - ArrowX, ArrowY, 1, 1); // Down Arrow last pixel
		Pen penArrow = new Pen(ColorTheme.ScrollbarArrow);
		// grapichObject.FillRectangle(brushBackColor, 8, bottomArrowY, 1, 1); // Down Arrow last pixel
		for (int i = 1; i <= 4; i++)
		{
			grapichObject.DrawLine(penArrow, Width - ArrowX - i, ArrowY - i, Width - ArrowX + i, ArrowY - i); // Bottom arrow
		}
		// Dropdown icon
		DrawIcon(HorizontalAlignment.Right, 0, -2);
		if (Focused)
			grapichObject.DrawRectangle(new Pen(ColorTheme.ControlBorderFocused), 0, 0, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
		e.Graphics.DrawImage(bitmapObject, 0, 0);
	}

	protected override void OnGotFocus(EventArgs e)
	{
		Invalidate();
		base.OnGotFocus(e);
	}

	protected override void OnLostFocus(EventArgs e)
	{
		Invalidate();
		base.OnLostFocus(e);
	}

}

[DebuggerNonUserCode]
class BadScrollBar : BadThemeControl
{
	private bool _ScrollNecessary = true;
	public bool ScrollNecessary
	{
		get { return _ScrollNecessary; }
		set { _ScrollNecessary = value; }
	}

	private bool _ScrollHide = true;
	public bool ScrollHide
	{
		get { return _ScrollHide; }
		set { _ScrollHide = value; Invalidate(); }
	}

	private int _ScrollPosition = 0;
	public int ScrollPosition
	{
		get { return _ScrollPosition; }
		set { _ScrollPosition = value; Invalidate(); }
	}

	private int _ScrollElementsTotals = 100;
	public int ScrollElementsTotals
	{
		get { return _ScrollElementsTotals; }
		set 
		{ 
			_ScrollElementsTotals = value;
			try
			{
				ScrollNecessary = (ScrollElementsTotals > 0 && ScrollElementsTotals > ScrollElementsVisible); 
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				// throw;
			}
			
			Invalidate();
		}
	}

	private int _ScrollElementsVisible = 20;
	public int ScrollElementsVisible
	{
		get { return _ScrollElementsVisible; }
		set { 
				_ScrollElementsVisible = value;
				try
				{
					ScrollNecessary = (ScrollElementsTotals > 0 && ScrollElementsTotals > ScrollElementsVisible); 
				}
				catch (Exception ex)
				{
					Log.LogToFile(ex);
					// throw;
				}
				Invalidate(); 
			}
	}

	private ScrollOrientation _ScrollOrientation = ScrollOrientation.VerticalScroll;
	public ScrollOrientation ScrollOrientation
	{
		get { return _ScrollOrientation; }
		set { _ScrollOrientation = value; Invalidate(); }
	}

	public BadScrollBar()
	{
		AllowTransparent();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		grapichObject.Clear(BackColor);
		SolidBrush brushBackColor = new SolidBrush(ColorTheme.ScrollbarBack);
		try
		{
			if (!ScrollHide)
			{
				// Background
				grapichObject.FillRectangle(brushBackColor, ClientRectangle);
			}
			// If no elements og fewer elements than visible area, do not whow scrollbar
			if (ScrollElementsTotals > 0 && ScrollElementsTotals > ScrollElementsVisible)
			{
				ScrollNecessary = true;
				if (ScrollHide)
				{
					// Background
					grapichObject.FillRectangle(brushBackColor, ClientRectangle);
				}
				// Calc Scroll Handle
				DrawScrollhandle();
				// Arrows
				Pen penArrow = new Pen(ColorTheme.ScrollbarArrow);
				brushBackColor = new SolidBrush(ColorTheme.ScrollbarArrow);
				if (ScrollOrientation == System.Windows.Forms.ScrollOrientation.VerticalScroll)
				{
					grapichObject.FillRectangle(brushBackColor, 8, 6, 1, 1); // Top arrow first pixel
					int bottomArrowY = scrollArrowsArea + scrollAreaPixels + scrollHandleSizePixels + 10;
					grapichObject.FillRectangle(brushBackColor, 8, bottomArrowY, 1, 1); // Bottom Arrow last pixel
					for (int i = 1; i <= 4; i++)
					{
						grapichObject.DrawLine(penArrow, 8 - i, 6 + i, 8 + i, 6 + i); // Top arrow
						grapichObject.DrawLine(penArrow, 8 - i, bottomArrowY - i, 8 + i, bottomArrowY - i); // Bottom arrow
					}
				}
				else if (ScrollOrientation == System.Windows.Forms.ScrollOrientation.HorizontalScroll)
				{
					grapichObject.FillRectangle(brushBackColor, 6, 8, 1, 1); // Left arrow first pixel
					int rightArrowX = scrollArrowsArea + scrollAreaPixels + scrollHandleSizePixels + 10;
					grapichObject.FillRectangle(brushBackColor, rightArrowX, 8, 1, 1); // Right Arrow last pixel
					for (int i = 1; i <= 4; i++)
					{
						grapichObject.DrawLine(penArrow, 6 + i, 8 - i, 6 + i, 8 + i); // Left arrow
						grapichObject.DrawLine(penArrow, rightArrowX - i, 8 - i, rightArrowX - i, 8 + i); // Right Arrow
					}
				}
			}
			else
			{
				ScrollNecessary = false;
			}
			// Draw 
			e.Graphics.DrawImage(bitmapObject, 0, 0);
		}
		catch (Exception ex)
		{
			Log.LogToFile(ex);
			// throw;
		}
	}

	protected int scrollMarginPixels = 4;
	protected int scrollArrowsArea = 17;
	protected int scrollPositionPixel;
	protected int scrollAreaPixels = 0;
	protected int scrollMaxSizePixels = 100;
	protected int scrollWidthPixels = 100;
	protected int scrollHandleSizePixels; // Size of scroll handle
	private void DrawScrollhandle()
	{
		if (ScrollOrientation == System.Windows.Forms.ScrollOrientation.VerticalScroll)
		{
			// Vertical Scrollbar
			scrollMaxSizePixels = Height;
			scrollWidthPixels = Width - (scrollMarginPixels * 2);
		}
		else if (ScrollOrientation == System.Windows.Forms.ScrollOrientation.HorizontalScroll)
		{
			scrollMaxSizePixels = Width;
			scrollWidthPixels = Height - (scrollMarginPixels * 2);
		}
		if (!move) // Position scrollbar from property
		{
			scrollAreaPixels = scrollMaxSizePixels - (scrollArrowsArea * 2); // Total scrolling area 
			scrollHandleSizePixels = scrollAreaPixels * ScrollElementsVisible / ScrollElementsTotals; // Size relative to elements visible
			if (scrollHandleSizePixels < 20) scrollHandleSizePixels = 20; // Minimum size
			scrollAreaPixels = scrollMaxSizePixels - (scrollArrowsArea * 2) - scrollHandleSizePixels; // Actual scrolling area
			double scPos = Convert.ToDouble(ScrollPosition);
			double scElementsTot = Convert.ToDouble(ScrollElementsTotals - ScrollElementsVisible);
			if (scPos > scElementsTot) scPos = scElementsTot;
			if (scPos < 0) scPos = 0;
			// Calc Position
			scrollPositionPixel = Convert.ToInt32(scrollArrowsArea + (scrollAreaPixels * scPos / scElementsTot));
			// Draw scroll handle
			SolidBrush brushBackColor = new SolidBrush(ColorTheme.ScrollbarFront);
			if (ScrollOrientation == System.Windows.Forms.ScrollOrientation.VerticalScroll)
				grapichObject.FillRectangle(brushBackColor, scrollMarginPixels, scrollPositionPixel, scrollWidthPixels, scrollHandleSizePixels);
			else if (ScrollOrientation == System.Windows.Forms.ScrollOrientation.HorizontalScroll)
				grapichObject.FillRectangle(brushBackColor, scrollPositionPixel, scrollMarginPixels, scrollHandleSizePixels, scrollWidthPixels);
		}
		else // Position scrollbar from mouse move
		{
			// Draw scroll handle
			SolidBrush brushBackColor = new SolidBrush(ColorTheme.ScrollbarFront);
			if (ScrollOrientation == System.Windows.Forms.ScrollOrientation.VerticalScroll)
				grapichObject.FillRectangle(brushBackColor, scrollMarginPixels, NewScrollPositionPixel , scrollWidthPixels, scrollHandleSizePixels);
			else if (ScrollOrientation == System.Windows.Forms.ScrollOrientation.HorizontalScroll)
				grapichObject.FillRectangle(brushBackColor, NewScrollPositionPixel , scrollMarginPixels, scrollHandleSizePixels, scrollWidthPixels);
			ScrollPosition = (ScrollElementsTotals - ScrollElementsVisible) * (NewScrollPositionPixel - scrollArrowsArea) / scrollAreaPixels;
		}
	}

	protected bool move = false;
	protected int movePixels;
	protected int oldScrollPositionPixel;
	protected int NewScrollPositionPixel;
	protected Point startPos;
	protected Point currentPos;
	protected override void OnMouseDown(MouseEventArgs e)
	{
		if (!(e.Button == MouseButtons.Left)) return;
		startPos = e.Location; // PointToClient(MousePosition);
		oldScrollPositionPixel = scrollPositionPixel;
		NewScrollPositionPixel = scrollPositionPixel;
		movePixels = 0;
		int cursorPosition = 0;
		// Check click area
		if (ScrollOrientation == System.Windows.Forms.ScrollOrientation.VerticalScroll)
		{
			cursorPosition = startPos.Y;
		}
		else if (ScrollOrientation == System.Windows.Forms.ScrollOrientation.HorizontalScroll)
		{
			cursorPosition = startPos.X;
		}
		// Clicked above handle
		if (cursorPosition < scrollArrowsArea)
		{
			// one element up
			ScrollPosition -= 1;
			if (ScrollPosition < 0) ScrollPosition = 0;
			PaintControl();
		}
		else	if (cursorPosition < oldScrollPositionPixel)
		{
			// one page up
			ScrollPosition -= (ScrollElementsVisible - 1);
			if (ScrollPosition < 0) ScrollPosition = 0;
			PaintControl();
		}
		else if (cursorPosition > scrollAreaPixels + scrollHandleSizePixels + scrollArrowsArea)
		{
			// one element down
			ScrollPosition += 1;
			if (ScrollPosition > ScrollElementsTotals - ScrollElementsVisible) ScrollPosition = ScrollElementsTotals - ScrollElementsVisible;
			PaintControl();
		}
		else if (cursorPosition > oldScrollPositionPixel + scrollHandleSizePixels)
		{
			// one page down
			ScrollPosition += (ScrollElementsVisible - 1);
			if (ScrollPosition > ScrollElementsTotals - ScrollElementsVisible) ScrollPosition = ScrollElementsTotals - ScrollElementsVisible;
			PaintControl();
		}
		else
			
			move = true;
		base.OnMouseDown(e);
		
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		if (move)
		{
			currentPos = e.Location; // PointToClient(MousePosition);
			if (ScrollOrientation == System.Windows.Forms.ScrollOrientation.VerticalScroll)
				movePixels = currentPos.Y - startPos.Y;
			else if (ScrollOrientation == System.Windows.Forms.ScrollOrientation.HorizontalScroll)
				movePixels = currentPos.X - startPos.X;
			NewScrollPositionPixel = oldScrollPositionPixel + movePixels ;
			if (NewScrollPositionPixel > scrollAreaPixels + scrollArrowsArea) NewScrollPositionPixel = scrollAreaPixels + scrollArrowsArea;
			if (NewScrollPositionPixel < scrollArrowsArea) NewScrollPositionPixel = scrollArrowsArea;
			PaintControl();
			
		}
		base.OnMouseMove(e);
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		move = false;
		base.OnMouseUp(e);
		PaintControl();
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		move = false;
		base.OnMouseLeave(e);
		PaintControl();
	}

	//protected override void OnMouseHover(EventArgs e)
	//{
	//	Cursor = Cursors.Default;
	//	base.OnMouseHover(e);
	//}
}

[DebuggerNonUserCode]
class BadScrollBarCorner : BadThemeControl
{
	public BadScrollBarCorner()
	{
		AllowTransparent();
	}

	protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
	{
		grapichObject.Clear(BackColor);
		if (Visible)
		{
			SolidBrush brushBackColor = new SolidBrush(ColorTheme.ScrollbarBack);
			grapichObject.FillRectangle(brushBackColor, ClientRectangle);
		}
		e.Graphics.DrawImage(bitmapObject, 0, 0);
	}

}