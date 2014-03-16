using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WotDBUpdater.Code.Support
{
    class FormTheme
    {
    }

    ///
    /// GenTheme
    /// Original vb.Net Creator, AeonHack
    /// Converted to C# by Faded
    /// www.EmuDevs.com
    /// 

    public class Draw
    {
        public static void Gradient(Graphics g, Color c1, Color c2, int x, int y, int width, int height)
        {
            Rectangle R = new Rectangle(x, y, width, height);
            using (LinearGradientBrush T = new LinearGradientBrush(R, c1, c2, LinearGradientMode.Vertical))
            {
                g.FillRectangle(T, R);
            }
        }
        public static void Blend(Graphics g, Color c1, Color c2, Color c3, float c, int d, int x, int y, int width, int height)
        {
            ColorBlend V = new ColorBlend(3);
            V.Colors = new Color[] { c1, c2, c3 };
            V.Positions = new float[] { 0F, c, 1F };
            Rectangle R = new Rectangle(x, y, width, height);
            using (LinearGradientBrush T = new LinearGradientBrush(R, c1, c1, (LinearGradientMode)d))
            {
                T.InterpolationColors = V;
                g.FillRectangle(T, R);
            }
        }
    }

    abstract class GenTheme : ContainerControl
    {
        protected Bitmap B;
        protected Graphics G;
        public GenTheme()
        {
            SetStyle((ControlStyles)8198, true);
            B = new Bitmap(1, 1);
            G = Graphics.FromImage(B);
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

        private int _MoveHeight = 24;
        public int MoveHeight
        {
            get { return _MoveHeight; }
            set
            {
                _MoveHeight = value;
                Header = new Rectangle(7, 7, Width - 14, _MoveHeight);
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
            Header = new Rectangle(7, 7, Width - 14, _MoveHeight);
            G.Dispose();
            B.Dispose();
            B = new Bitmap(Width, Height);
            G = Graphics.FromImage(B);
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
            B.SetPixel(rect.X, rect.Y, c);
            B.SetPixel(rect.X + (rect.Width - 1), rect.Y, c);
            B.SetPixel(rect.X, rect.Y + (rect.Height - 1), c);
            B.SetPixel(rect.X + (rect.Width - 1), rect.Y + (rect.Height - 1), c);
        }

        public void DrawBorders(Pen p1, Pen p2, Rectangle rect)
        {
            G.DrawRectangle(p1, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            G.DrawRectangle(p2, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3);
        }

        private Size TextSize;
        public void DrawText(HorizontalAlignment a, Brush b, int offset = 0)
        {
            if (string.IsNullOrEmpty(Text))
                return;
            TextSize = G.MeasureString(Text, Font).ToSize();

            switch (a)
            {
                case HorizontalAlignment.Left:
                    G.DrawString(Text, Font, b, 5 + offset, _MoveHeight / 2 - TextSize.Height / 2 + 7);
                    break;
                case HorizontalAlignment.Right:
                    G.DrawString(Text, Font, b, Width - 5 - TextSize.Width - offset, _MoveHeight / 2 - TextSize.Height / 2 + 7);
                    break;
                case HorizontalAlignment.Center:
                    G.DrawString(Text, Font, b, Width / 2 - TextSize.Width / 2, _MoveHeight / 2 - TextSize.Height / 2 + 7);
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
                    G.DrawImage(_Image, 5 + offset, _MoveHeight / 2 - _Image.Height / 2 + 7);
                    break;
                case HorizontalAlignment.Right:
                    G.DrawImage(_Image, Width - 5 - TextSize.Width - offset, _MoveHeight / 2 - TextSize.Height / 2 + 7);
                    break;
                case HorizontalAlignment.Center:
                    G.DrawImage(_Image, Width / 2 - TextSize.Width / 2, _MoveHeight / 2 - TextSize.Height / 2 + 7);
                    break;
            }
        }
    }

    abstract class GenThemeControl : Control
    {
        protected Bitmap B;
        protected Graphics G;
        public GenThemeControl()
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

        public void DrawBorders(Pen p1, Pen p2, Rectangle rect)
        {
            G.DrawRectangle(p1, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            G.DrawRectangle(p2, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3);
        }

        private Size TextSize;
        public void DrawText(HorizontalAlignment a, Brush b, int offset = 0)
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

    class GGenTheme : GenTheme
    {
        public GGenTheme()
        {
            MoveHeight = 28;
            ForeColor = Color.FromArgb(100, 100, 100);
            SetTransparent(Color.Fuchsia);

            C1 = Color.FromArgb(41, 41, 41);
            C2 = Color.FromArgb(25, 25, 25);

            P1 = new Pen(Color.FromArgb(58, 58, 58));
            P2 = new Pen(C2);
        }

        private Color C1;
        private Color C2;
        private Pen P1;
        private Pen P2;
        private LinearGradientBrush B1;

        private Rectangle R1;
        protected override void OnPaint(PaintEventArgs e)
        {
            G.Clear(C1);

            R1 = new Rectangle(0, 0, Width, 28);
            B1 = new LinearGradientBrush(R1, C2, C1, LinearGradientMode.Vertical);
            G.FillRectangle(B1, R1);

            G.DrawLine(P2, 0, 28, Width, 28);
            G.DrawLine(P1, 0, 29, Width, 29);

            DrawText(HorizontalAlignment.Left, new SolidBrush(ForeColor), ImageWidth);
            DrawIcon(HorizontalAlignment.Left);

            DrawBorders(Pens.Black, P1, ClientRectangle);
            DrawCorners(Color.Fuchsia, ClientRectangle);

            e.Graphics.DrawImage(B, 0, 0);
        }
    }

    class GButton : GenThemeControl
    {

        private Pen P1;
        private Pen P2;
        private LinearGradientBrush B1;
        private Color C1;
        private Color C2;

        private Rectangle R1;
        public GButton()
        {
            AllowTransparent();
            BackColor = Color.FromArgb(41, 41, 41);
            ForeColor = Color.FromArgb(100, 100, 100);

            P1 = new Pen(Color.FromArgb(25, 25, 25));
            P2 = new Pen(Color.FromArgb(11, Color.White));

            C1 = Color.FromArgb(41, 41, 41);
            C2 = Color.FromArgb(51, 51, 51);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (MouseState == State.MouseDown)
                B1 = new LinearGradientBrush(ClientRectangle, C1, C2, LinearGradientMode.Vertical);
            else
                B1 = new LinearGradientBrush(ClientRectangle, C2, C1, LinearGradientMode.Vertical);

            G.FillRectangle(B1, ClientRectangle);

            DrawText(HorizontalAlignment.Center, new SolidBrush(ForeColor));
            DrawIcon(HorizontalAlignment.Left);

            DrawBorders(P1, P2, ClientRectangle);
            DrawCorners(BackColor, ClientRectangle);

            e.Graphics.DrawImage(B, 0, 0);
        }
    }

    class Seperator : GenThemeControl
    {

        public Seperator()
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

        private Rectangle R1;
        private LinearGradientBrush B1;

        private int Rotation;
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
}