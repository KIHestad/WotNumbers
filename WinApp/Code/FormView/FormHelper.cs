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
		private static int openCountToRight = 0;
		private static int skewCountToRight = 0;
		private static int lastXToRight = 0;
		private static int lastYToRight = 0;
		private static int lastWToRight = 0;
		private const int offsetToRight = 20;

		public static void OpenFormToRightOfParent(Form parentForm, Form openForm)
		{
			if (openCountToRight == 0)
				skewCountToRight = 0;
			int newX = 0;
			int newY = 0;
			int newH = 0;
			if (parentForm.WindowState == FormWindowState.Normal)
			{
				// Check if parent form has moved or resized width
				Point pos = parentForm.PointToScreen(new Point(0, 0));
				if (pos.X != lastXToRight || pos.Y != lastYToRight || parentForm.Width != lastWToRight)
				{
					// Yes, moved or resized width- remember as last position
					openCountToRight = 0;
					skewCountToRight = 0;
					lastXToRight = pos.X;
					lastYToRight = pos.Y;
					lastWToRight = parentForm.Width;
				}
				// Find if enough space to left to open new form
				Screen screen = Screen.FromControl(parentForm);
				int parentFormX = parentForm.Location.X - screen.Bounds.Left; // x position on active screen for parent form
				int parentFormY = parentForm.Location.Y - screen.Bounds.Top; // y position on active screen for parent form
				int spaceToRight = screen.WorkingArea.Width - parentFormX - parentForm.Width; // Space to right for parent form
				
				if (spaceToRight < openForm.Width)
				{
					// Windowstate = normal -> Center location for new form
					newX = parentForm.Location.X + 50 + (skewCountToRight * offsetToRight);
					newY = parentForm.Location.Y + 100 + (skewCountToRight * offsetToRight);
					newH = (parentForm.Height - 50) + (skewCountToRight * offsetToRight);
				}
				else
				{
					// Windowstate = normal -> Right location for new form
					newX = (lastXToRight + lastWToRight) + (skewCountToRight * offsetToRight);
					newY = (lastYToRight) + (skewCountToRight * offsetToRight);
					newH = parentForm.Height;
				}
			}
			else
			{
				// Windowstate = maximized -> Center location for new form
				newX = parentForm.Location.X + 50 + (skewCountToRight * offsetToRight);
				newY = parentForm.Location.Y + 100 + (skewCountToRight * offsetToRight);
				newH = (parentForm.Height - 250) + (skewCountToRight * offsetToRight);
			}
			// Show
			openForm.SetDesktopLocation(newX, newY);
			openForm.Height = newH;
			openForm.Show();
			openCountToRight++;
			// Make ready for next open form
			skewCountToRight++;
			if (skewCountToRight > 5)
				skewCountToRight = 0;
		}

		private static int skewCountCenter = 1;
		private static int lastXCenter = 0;
		private static int lastYCenter = 0;
		private static int lastWCenter = 0;
		private const int offsetCenter = 20;

		public static void OpenFormCenterOfParent(Form parentForm, Form openForm)
		{
			int newX = 0;
			int newY = 0;
			// Find postiton of parent
			Point pos = parentForm.PointToScreen(new Point(0, 0));
			// Check if not same as last, then position new at center
			if (pos.X != lastXCenter || pos.Y != lastYCenter || parentForm.Width != lastWCenter)
			{
				// Yes, moved or resized width- remember as last position
				skewCountCenter = 1;
				lastXCenter = pos.X;
				lastYCenter = pos.Y;
				lastWCenter = parentForm.Width;
				if (parentForm.Name == "Main")
					lastYCenter += 80;
			}
			// Find new location center parent 
			newX = lastXCenter + (lastWCenter / 2) - (openForm.Width / 2) + (skewCountCenter * offsetCenter);
			newY = lastYCenter + (skewCountCenter * offsetCenter);
			// Check if not outside screen
			Screen screen = Screen.FromControl(parentForm);
			int parentFormX = parentForm.Location.X - screen.Bounds.Left; // x position on active screen for parent form
			int parentFormY = parentForm.Location.Y - screen.Bounds.Top; // y position on active screen for parent form
			int spaceToRight = screen.WorkingArea.Width - parentFormX - parentForm.Width; // Space to right for parent form
			int spaceToBottom = screen.WorkingArea.Height - parentFormY - parentForm.Height; // Space at bottom
			if (spaceToRight < 0)
				newX += spaceToRight;	// move within screen
			if (spaceToBottom < 0)
				newY += spaceToBottom;	// move within screen
			// Show
			openForm.SetDesktopLocation(newX, newY);
			openForm.Show();
			// Make ready for next open form
			skewCountCenter++;
			if (skewCountCenter > 5)
				skewCountCenter = 1;
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

		public static void ClosedOne()
		{
			if (openCountToRight > 0) openCountToRight--;
		}
	}
}
