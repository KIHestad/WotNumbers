using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinApp.Code
{
	public class BadGridView
	{
		/// <summary>
		/// Allows to add both text and image in the same column
		/// </summary>
		public class TextAndImageColumn : DataGridViewTextBoxColumn
		{
			private Image imageValue;
			private Size imageSize;
			public TextAndImageColumn()
			{
				this.CellTemplate = new TextAndImageCell();
			}
			public override object Clone()
			{
				TextAndImageColumn c = base.Clone() as TextAndImageColumn;
				c.imageValue = this.imageValue;
				c.imageSize = this.imageSize;
				return c;
			}
			public Image Image
			{
				get { return this.imageValue; }
				set
				{
					if (this.Image != value)
					{
						this.imageValue = value;
						this.imageSize = value.Size;
						if (this.InheritedStyle != null)
						{
							Padding inheritedPadding = this.InheritedStyle.Padding;
							this.DefaultCellStyle.Padding = new Padding(inheritedPadding.Left,
							inheritedPadding.Top, imageSize.Width,
							inheritedPadding.Bottom);
						}
					}
				}
			}
			private TextAndImageCell TextAndImageCellTemplate
			{
				get { return this.CellTemplate as TextAndImageCell; }
			}
			internal Size ImageSize
			{
				get { return imageSize; }
			}
		}
		/// <summary>
		/// Class which will allow to access individual cells
		/// </summary>
		public class TextAndImageCell : DataGridViewTextBoxCell
		{
			private Image imageValue;
			private Size imageSize;
			private int m_XCoordinate = 0;
			private int m_YCoordinate = 0;
			public override object Clone()
			{
				TextAndImageCell c = base.Clone() as TextAndImageCell;
				c.imageValue = this.imageValue;
				c.imageSize = this.imageSize;
				return c;
			}
			public int XCoordinate
			{
				get { return m_XCoordinate; }
				set { m_XCoordinate = value; }
			}
			public int YCoordinate
			{
				get { return m_YCoordinate; }
				set { m_YCoordinate = value; }
			}
			public Image Image
			{
				get
				{
					if (this.OwningColumn == null ||
					this.OwningTextAndImageColumn == null)
					{
						return imageValue;
					}
					else if (this.imageValue != null)
					{
						return this.imageValue;
					}
					else
					{
						return this.OwningTextAndImageColumn.Image;
					}
				}
				set
				{
					if (this.imageValue != value)
					{
						this.imageValue = value;
						this.imageSize = value.Size;
						Padding inheritedPadding = this.InheritedStyle.Padding;
						this.Style.Padding = new Padding(inheritedPadding.Left,
						inheritedPadding.Top, imageSize.Width,
						inheritedPadding.Bottom);
					}
				}
			}
			/// <summary>
			/// Overriding the Paint method to paint the image
			/// </summary>
			/// <param name="graphics"></param>
			/// <param name="clipBounds"></param>
			/// <param name="cellBounds"></param>
			/// <param name="rowIndex"></param>
			/// <param name="cellState"></param>
			/// <param name="value"></param>
			/// <param name="formattedValue"></param>
			/// <param name="errorText"></param>
			/// <param name="cellStyle"></param>
			/// <param name="advancedBorderStyle"></param>
			/// <param name="paintParts"></param>
			protected override void Paint(Graphics graphics, System.Drawing.Rectangle clipBounds,
			System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
			object value, object formattedValue, string errorText,
			DataGridViewCellStyle cellStyle,
			DataGridViewAdvancedBorderStyle advancedBorderStyle,
			DataGridViewPaintParts paintParts)
			{
				try
				{
					// Paint the base content
					base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
					value, formattedValue, errorText, cellStyle,
					advancedBorderStyle, paintParts);
					if (this.Image != null)
					{
						// Draw the image clipped to the cell.
						System.Drawing.Drawing2D.GraphicsContainer container =
						graphics.BeginContainer();
						graphics.SetClip(cellBounds);
						System.Drawing.Point objPoint = new System.Drawing.Point((cellBounds.Location.X + XCoordinate), (cellBounds.Location.Y + YCoordinate));
						graphics.DrawImageUnscaled(this.Image, objPoint);
						graphics.EndContainer(container);
					}
				}
				catch
				{
				}
				finally
				{
				}
			}
			private TextAndImageColumn OwningTextAndImageColumn
			{
				get { return this.OwningColumn as TextAndImageColumn; }
			}
		}
 
	}
}
