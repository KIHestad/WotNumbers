///
/// GenTheme
/// Original vb.Net Creator, AeonHack
/// Converted to C# by Faded
/// www.EmuDevs.com
/// 

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WotDBUpdater.Code.Support;

abstract class BadThemeContainerControl : ContainerControl
{
	protected Bitmap BitmapObject;
	protected Graphics GraphicObject;
	public BadThemeContainerControl()
	{
		SetStyle((ControlStyles)8198, true);
		BitmapObject = new Bitmap(1, 1);
		GraphicObject = Graphics.FromImage(BitmapObject);
	}

	private bool ParentIsForm;
	protected override void OnHandleCreated(EventArgs e)
	{
		Dock = DockStyle.Fill;
		ParentIsForm = Parent is Form;
		if (ParentIsForm)
			ParentForm.FormBorderStyle = FormBorderStyle.None;
		base.OnHandleCreated(e);
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
			Header = new Rectangle(7, 7, Width - 14, _TitleHeight);
		}
	}

	private IntPtr Flag;
	protected override void OnMouseDown(MouseEventArgs e)
	{
		if (!(e.Button == MouseButtons.Left))
			return;
		if (ParentIsForm)
			if (ParentForm.WindowState == FormWindowState.Maximized)
				return;

		if (Header.Contains(e.Location))
			Flag = new IntPtr(2);
		else if (Current.Position == 0 | !_Resizable)
			return;
		else
			Flag = new IntPtr(Current.Position);

		Capture = false;
		Message msg = Message.Create(Parent.Handle, 161, Flag, IntPtr.Zero);
		DefWndProc(ref msg);
		base.OnMouseDown(e);
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

	private bool F1;
	private bool F2;
	private bool F3;
	private bool F4;
	private Point PTC;
	private Pointer GetPointer()
	{
		PTC = PointToClient(MousePosition);
		F1 = PTC.X < 7;
		F2 = PTC.X > Width - 7;
		F3 = PTC.Y < 7;
		F4 = PTC.Y > Height - 7;

		if (F1 & F3)
			return new Pointer(Cursors.SizeNWSE, 13);
		if (F1 & F4)
			return new Pointer(Cursors.SizeNESW, 16);
		if (F2 & F3)
			return new Pointer(Cursors.SizeNESW, 14);
		if (F2 & F4)
			return new Pointer(Cursors.SizeNWSE, 17);
		if (F1)
			return new Pointer(Cursors.SizeWE, 10);
		if (F2)
			return new Pointer(Cursors.SizeWE, 11);
		if (F3)
			return new Pointer(Cursors.SizeNS, 12);
		if (F4)
			return new Pointer(Cursors.SizeNS, 15);
		return new Pointer(Cursors.Default, 0);
	}

	private Pointer Current;
	private Pointer Pending;
	private void SetCurrent()
	{
		Pending = GetPointer();
		if (Current.Position == Pending.Position)
			return;
		Current = GetPointer();
		Cursor = Current.Cursor;
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		if (_Resizable)
			SetCurrent();
		base.OnMouseMove(e);
	}

	protected Rectangle Header;
	protected override void OnSizeChanged(EventArgs e)
	{
		Header = new Rectangle(7, 7, Width - 14, _TitleHeight);
		GraphicObject.Dispose();
		BitmapObject.Dispose();
		BitmapObject = new Bitmap(Width, Height);
		GraphicObject = Graphics.FromImage(BitmapObject);
		Invalidate();
		base.OnSizeChanged(e);
	}

	public void SetTransparent(Color c)
	{
		if (ParentIsForm)
			ParentForm.TransparencyKey = c;
	}

	protected override abstract void OnPaint(PaintEventArgs e);

	public void DrawCorners(Color c, Rectangle rect)
	{
		BitmapObject.SetPixel(rect.X, rect.Y, c);
		BitmapObject.SetPixel(rect.X + (rect.Width - 1), rect.Y, c);
		BitmapObject.SetPixel(rect.X, rect.Y + (rect.Height - 1), c);
		BitmapObject.SetPixel(rect.X + (rect.Width - 1), rect.Y + (rect.Height - 1), c);
	}

	public void DrawBorder(Pen outerPen, Pen innerPen, Rectangle rect)
	{
		GraphicObject.DrawRectangle(outerPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
		GraphicObject.DrawRectangle(innerPen, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3);
	}

	public void DrawBorder(Pen outerPen, Rectangle rect)
	{
		GraphicObject.DrawRectangle(outerPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
	}

	private Size TextSize;
	public void DrawText(HorizontalAlignment a, Brush b, int offset = 0) // Form text
	{
		if (string.IsNullOrEmpty(Text))
			return;
		TextSize = GraphicObject.MeasureString(Text, Font).ToSize();

		switch (a)
		{
			case HorizontalAlignment.Left:
				GraphicObject.DrawString(Text, Font, b, 19 + offset, _TitleHeight / 2 - TextSize.Height / 2 + 2);
				break;
			case HorizontalAlignment.Right:
				GraphicObject.DrawString(Text, Font, b, Width - 5 - TextSize.Width - offset, _TitleHeight / 2 - TextSize.Height / 2 + 2);
				break;
			case HorizontalAlignment.Center:
				GraphicObject.DrawString(Text, Font, b, Width / 2 - TextSize.Width / 2, _TitleHeight / 2 - TextSize.Height / 2 + 2);
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

	public void DrawIcon(HorizontalAlignment a, int offset = 5)
	{
		if (_Image == null)
			return;
		switch (a)
		{
			case HorizontalAlignment.Left:
				GraphicObject.DrawImage(_Image, 7 + offset, _TitleHeight / 2 - _Image.Height / 2 + 1);
				break;
			case HorizontalAlignment.Right:
				GraphicObject.DrawImage(_Image, Width - 5 - TextSize.Width - offset, _TitleHeight / 2 - TextSize.Height / 2 + 1);
				break;
			case HorizontalAlignment.Center:
				GraphicObject.DrawImage(_Image, Width / 2 - TextSize.Width / 2, _TitleHeight / 2 - TextSize.Height / 2 + 1);
				break;
		}
	}

	private Color _FormBorderColor = Color.FromArgb(0, 0, 0, 0);
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


}

abstract class BadThemeControl : Control
{
	protected Bitmap B;
	protected Graphics G;
	public BadThemeControl()
	{
		SetStyle((ControlStyles)8198, true);
		B = new Bitmap(1, 1);
		G = Graphics.FromImage(B);
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
		G.Dispose();
		B.Dispose();
		B = new Bitmap(Width, Height);
		G = Graphics.FromImage(B);
		Invalidate();
		base.OnSizeChanged(e);
	}

	protected override abstract void OnPaint(PaintEventArgs e);

	public void DrawCorners(Color c, Rectangle rect)
	{
		B.SetPixel(rect.X, rect.Y, c);
		B.SetPixel(rect.X + (rect.Width - 1), rect.Y, c);
		B.SetPixel(rect.X, rect.Y + (rect.Height - 1), c);
		B.SetPixel(rect.X + (rect.Width - 1), rect.Y + (rect.Height - 1), c);
	}

	public void DrawBorders(Pen outerPen, Pen innerPen, Rectangle rect)
	{
		G.DrawRectangle(outerPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
		G.DrawRectangle(innerPen, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3);
	}

	public void DrawBorders(Pen outerPen, Rectangle rect)
	{
		G.DrawRectangle(outerPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
	}


	private Size TextSize;
	public void DrawText(HorizontalAlignment a, Brush b, int offset = 0) // Button text
	{
		if (string.IsNullOrEmpty(Text))
			return;
		TextSize = G.MeasureString(Text, Font).ToSize();

		switch (a)
		{
			case HorizontalAlignment.Left:
				G.DrawString(Text, Font, b, 5 + offset, Height / 2 - TextSize.Height / 2);
				break;
			case HorizontalAlignment.Right:
				G.DrawString(Text, Font, b, Width - 5 - TextSize.Width - offset, Height / 2 - TextSize.Height / 2);
				break;
			case HorizontalAlignment.Center:
				G.DrawString(Text, Font, b, Width / 2 - TextSize.Width / 2, Height / 2 - TextSize.Height / 2);
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

	public void DrawIcon(HorizontalAlignment a, int offset = 0)
	{
		if (_Image == null)
			return;
		switch (a)
		{
			case HorizontalAlignment.Left:
				G.DrawImage(_Image, Width / 10 + offset, Height / 2 - _Image.Height / 2);
				break;
			case HorizontalAlignment.Right:
				G.DrawImage(_Image, Width - (Width / 10) - TextSize.Width - offset, Height / 2 - TextSize.Height / 2);
				break;
			case HorizontalAlignment.Center:
				G.DrawImage(_Image, Width / 2 - TextSize.Width / 2, Height / 2 - TextSize.Height / 2);
				break;
		}
	}
}

class BadForm : BadThemeContainerControl
{
	public BadForm()
	{
		SetTransparent(Color.Fuchsia); // Transparency color = purple (FF00FF)
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		GraphicObject.Clear(ColorTheme.FormBorderBlack);

		Rectangle RectangleTitle = new Rectangle(1, 1, Width -2, TitleHeight);
		Rectangle RectangleMain = new Rectangle(1, TitleHeight + 1 , Width -2, Height - TitleHeight -2);

		// BackGradient = new LinearGradientBrush(RectangleTitle, ColorMain, ColorTitle, LinearGradientMode.Vertical); // Alternative gradient background
		SolidBrush brush = new SolidBrush(ColorTheme.ControlBack);
		GraphicObject.FillRectangle(brush, RectangleTitle);
		brush = new SolidBrush(ColorTheme.FormBack);
		GraphicObject.FillRectangle(brush, RectangleMain);

		// G.DrawLine(P2, 0, 28, Width, 28); // Separator line #1 below title header
		// G.DrawLine(P1, 0, 29, Width, 29); // Separator line #2 below title header

		DrawText(HorizontalAlignment.Left, new SolidBrush(ColorTheme.ControlFont), ImageWidth); // Add title text
		DrawIcon(HorizontalAlignment.Left); // Add title icon
		Pen FormBorderPenColor = new Pen(FormBorderColor);
		DrawBorder(FormBorderPenColor, ClientRectangle); // Outer Border
		//DrawCorners(Color.Fuchsia, ClientRectangle); // Corner pixel color

		e.Graphics.DrawImage(BitmapObject, 0, 0);
	}
}

class BadPanelFooter : BadThemeContainerControl
{

	public BadPanelFooter()
	{
		SetTransparent(Color.Fuchsia); // Transparency color = purple (FF00FF)
	}

	protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
	{
		GraphicObject.Clear(ColorTheme.FormBorderBlack);
		Rectangle RectangleFooter = new Rectangle(1, Height-27, Width - 2, 26);
		SolidBrush brush = new SolidBrush(ColorTheme.FormBorderBlack);
		GraphicObject.FillRectangle(brush, RectangleFooter);
		DrawText(HorizontalAlignment.Left, new SolidBrush(ColorTheme.ControlFont), ImageWidth); // Add title text
		DrawIcon(HorizontalAlignment.Left); // Add title icon
		Pen FormBorderPenColor = new Pen(FormBorderColor);
		e.Graphics.DrawImage(BitmapObject, 0, 0);
	}
}

class BadButton : BadThemeControl
{

	public BadButton()
	{
		AllowTransparent();
	}

	protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
	{
		SolidBrush brushBackColor; //= new SolidBrush(); //ColorTheme.BackControl);
		if (MouseState == State.MouseDown)
			brushBackColor = new SolidBrush(ColorTheme.ControlBackMouseDown);
		else if (MouseState == State.MouseOver)
			brushBackColor = new SolidBrush(ColorTheme.ControlBackMouseOver);
		else
			brushBackColor = new SolidBrush(ColorTheme.ControlBack);
		G.FillRectangle(brushBackColor, ClientRectangle);

		DrawText(HorizontalAlignment.Center, new SolidBrush(ColorTheme.ControlFont));
		DrawIcon(HorizontalAlignment.Left);

		//Pen outerBorderPen = new Pen(Color.Black); // Button border
		//DrawBorders(outerBorderPen, ClientRectangle);
		//DrawCorners(BackColor, ClientRectangle); // Button corners

		e.Graphics.DrawImage(B, 0, 0);
	}
}

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

	private Color _Color1 = Color.FromArgb(90, Color.Black);
	public Color Color1
	{
		get { return _Color1; }
		set
		{
			_Color1 = value;
			Invalidate();
		}
	}

	private Color _Color2 = Color.FromArgb(14, Color.White);
	public Color Color2
	{
		get { return _Color2; }
		set
		{
			_Color2 = value;
			Invalidate();
		}
	}

	// private Rectangle R1;
	// private LinearGradientBrush B1;
	// private int Rotation;

	protected override void OnPaint(PaintEventArgs e)
	{
		G.Clear(BackColor);

		if (_Direction == Orientation.Horizontal)
		{
			G.DrawLine(new Pen(_Color1), 0, Height / 2, Width, Height / 2);
			G.DrawLine(new Pen(_Color2), 0, Height / 2 + 1, Width, Height / 2 + 1);
		}
		else
		{
			G.DrawLine(new Pen(_Color1), Width / 2, 0, Width / 2, Height);
			G.DrawLine(new Pen(_Color2), Width / 2 + 1, 0, Width / 2 + 1, Height);
		}

		e.Graphics.DrawImage(B, 0, 0);
	}
}


