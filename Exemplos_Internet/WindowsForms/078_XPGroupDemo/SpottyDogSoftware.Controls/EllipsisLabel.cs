using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace SpottyDogSoftware.Controls
{
	// Label control that supports ellipsis and antialias.
	public class EllipsisLabel : UserControl
	{
		// internal members
		private bool m_antiAlias = false;
		private StringFormat m_format = new StringFormat();
		private SolidBrush m_brush = new SolidBrush(SystemColors.WindowText);
		private int m_padding = 0;
		private ContentAlignment m_alignment = ContentAlignment.TopLeft;
		private IContainer components = null;
		
		public EllipsisLabel()
		{
			InitializeComponent();
			
			// default the text to the control's name
			Text = Name;
			
			// init format
			m_format.FormatFlags = StringFormatFlags.NoWrap;
			m_format.Trimming = StringTrimming.EllipsisCharacter;
			
			// turn on double buffering and transparency
			SetStyle(	ControlStyles.UserPaint | 
				ControlStyles.ResizeRedraw | 
				ControlStyles.SupportsTransparentBackColor | 
				ControlStyles.AllPaintingInWmPaint | 
				ControlStyles.DoubleBuffer, 
				true);
		}
		
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		[DebuggerStepThroughAttribute()]
		private void InitializeComponent()
		{
			Name = "EllipsisLabel";
			Size = new Size(100, 23);
		}
		
		[DefaultValueAttribute(1)]
		[DescriptionAttribute("Specifies the alignment of the text.")]
		[CategoryAttribute("Appearance")]
		public ContentAlignment Alignment
		{
			get
			{
				return m_alignment;
			}

			set
			{
				m_alignment = value;
				
				// set the horz format based on the new alignment
				m_format.Alignment = StringAlignment.Near;
				
				if (m_alignment == ContentAlignment.BottomCenter || m_alignment == ContentAlignment.MiddleCenter || m_alignment == ContentAlignment.TopCenter)
				{
					m_format.Alignment = StringAlignment.Center;
				}
				
				if (m_alignment == ContentAlignment.BottomRight || m_alignment == ContentAlignment.MiddleRight || m_alignment == ContentAlignment.TopRight)
				{
					m_format.Alignment = StringAlignment.Far;
				}
				
				// set the vert format based on the new alignment
				m_format.LineAlignment = StringAlignment.Near;
				
				if (m_alignment == ContentAlignment.BottomCenter || m_alignment == ContentAlignment.BottomCenter || m_alignment == ContentAlignment.BottomCenter)
				{
					m_format.LineAlignment = StringAlignment.Far;
				}
				
				if (m_alignment == ContentAlignment.MiddleCenter || m_alignment == ContentAlignment.MiddleLeft || m_alignment == ContentAlignment.MiddleRight)
				{
					m_format.LineAlignment = StringAlignment.Center;
				}
				
				Invalidate();
			}
		}

		[CategoryAttribute("Appearance")]
		[DescriptionAttribute("Text that is displayed in the label.")]
		[DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
		[BrowsableAttribute(true)]
		new public string Text
		{
			get
			{
				return base.Text;
			}

			set
			{
				base.Text = value;
				Invalidate();
			}
		}

		[DescriptionAttribute("Specifies how to trim characters from a string that does not completely fit into a layout shape.")]
		[DefaultValueAttribute(3)]
		[CategoryAttribute("Appearance")]
		public StringTrimming Ellipsis
		{
			get
			{
				return m_format.Trimming;
			}

			set
			{
				m_format.Trimming = value;
				Invalidate();
			}
		}

		[DescriptionAttribute("If the text should be drawn antialiased.")]
		[CategoryAttribute("Appearance")]
		[DefaultValueAttribute(false)]
		public bool AntiAlias
		{
			get
			{
				return m_antiAlias;
			}

			set
			{
				m_antiAlias = value;
				Invalidate();
			}
		}

		[CategoryAttribute("Appearance")]
		[DescriptionAttribute("Amount of space to leave around the text.")]
		[DefaultValueAttribute(0)]
		public int Padding
		{
			get
			{
				return m_padding;
			}

			set
			{
				m_padding = value;
				Invalidate();
			}
		}

		// paint the label
		protected override void OnPaint(PaintEventArgs e)
		{
			// calculate the bounding area, required when drawing ellipsis
			RectangleF rc = new RectangleF(m_padding, m_padding, (Width - m_padding * 2), (Height - m_padding * 2));
			
			// see if want to draw antialias
			if (m_antiAlias)
			{
				e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			}
			
			// draw the text
			e.Graphics.DrawString(Text, Font, m_brush, rc, m_format);
		}

		// create new brush when the fore color changes
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			m_brush.Dispose();
			m_brush = new SolidBrush(ForeColor);
		}

		// update the brush when the system colors change
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
			OnForeColorChanged(EventArgs.Empty);
		}

		// update the format if the right-to-left property changes
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			
			if (RightToLeft == RightToLeft.Yes)
			{
				m_format.FormatFlags = m_format.FormatFlags | StringFormatFlags.DirectionRightToLeft;
			}
			
			if (RightToLeft == RightToLeft.No)
			{
				m_format.FormatFlags = m_format.FormatFlags & (StringFormatFlags)(-2);
			}
			
			Invalidate();
		}
	}
}
