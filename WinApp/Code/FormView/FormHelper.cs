using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinApp.Code
{
	class FormHelper
	{
		private static int skewCount = 0;
		private static int lastX = 0;
		private static int lastY = 0;
		private static int lastW = 0;
		private const int offset = 20;

		public static void OpenForm(Form parentForm, Form openForm)
		{
			int newX = 0;
			int newY = 0;
			if (parentForm.WindowState == FormWindowState.Normal)
			{
				// Check if parent form has moved or resized width
				Point pos = parentForm.PointToScreen(new Point(0, 0));
				if (pos.X != lastX || pos.Y != lastY || parentForm.Width != lastW)
				{
					// Yes, moved or resized width- remember as last position
					skewCount = 0;
					lastX = pos.X;
					lastY = pos.Y;
					lastW = parentForm.Width;
				}
				// Find if enough space to left to open new form
				Screen screen = Screen.FromControl(parentForm);
				int parentFormX = parentForm.Location.X - screen.Bounds.Left; // x position on active screen for parent form
				int parentFormY = parentForm.Location.Y - screen.Bounds.Top; // x position on active screen for parent form
				int spaceToRight = screen.WorkingArea.Width - parentFormX - parentForm.Width; // Space to right for parent form
				
				if (spaceToRight < openForm.Width)
				{
					// Center location for new form
					newX = parentForm.Location.X + 50 + (skewCount * offset);
					newY = parentForm.Location.Y + 100 + (skewCount * offset);
				}
				else
				{
					// Right location for new form
					newX = (lastX + lastW) + (skewCount * offset);
					newY = (lastY) + (skewCount * offset);
				}
				// Show
				openForm.SetDesktopLocation(newX, newY);
				openForm.Height = parentForm.Height;
				openForm.Show();
			}
			else
			{
				// Center location for new form
				newX = parentForm.Location.X + 50 + (skewCount * offset);
				newY = parentForm.Location.Y + 100 + (skewCount * offset);
				openForm.SetDesktopLocation(newX, newY);
				openForm.Show();
			}
			// Make ready for next open form
			skewCount++;
			if (skewCount > 5)
				skewCount = 0;
		}

		/// <summary>Returns the location of the form relative to the top-left corner
		/// of the screen that contains the top-left corner of the form, or null if the
		/// top-left corner of the form is off-screen.</summary>
		private Point? GetLocationWithinScreen(Form form)
		{
			foreach (Screen screen in Screen.AllScreens)
				if (screen.Bounds.Contains(form.Location))
					return new Point(form.Location.X - screen.Bounds.Left,
									 form.Location.Y - screen.Bounds.Top);

			return null;
		}

	}
}
