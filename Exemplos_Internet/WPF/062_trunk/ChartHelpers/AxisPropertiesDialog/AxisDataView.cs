// <copyright file="AxisDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-06</date>
// <summary>OpenWPFChart library. Coordinate Axis Data View base class.</summary>
// <revision>$Id: AxisDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// Coordinate Axis Data View base class.
	/// </summary>
	public abstract class AxisDataView : INotifyPropertyChanged, IDataErrorInfo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AxisDataView"/> class.
		/// </summary>
		/// <param name="axis">The axis.</param>
		public AxisDataView(Axis axis)
		{
			axisScale = axis.AxisScale;
			contentLayout = axis.ContentLayout;
			tickLength = axis.TickLength;
			longTickLength = axis.LongTickLength;
			labelFormat = axis.LabelFormat;
			labelMargin = axis.LabelMargin;
			penColor = (axis.Pen.Brush as SolidColorBrush).Color;
			penThickness = axis.Pen.Thickness;
			fontFamily = axis.FontFamily;
			fontSize = axis.FontSize;
			fontStretch = axis.FontStretch;
			fontStyle = axis.FontStyle;
			fontWeight = axis.FontWeight;
		}

		readonly ChartScale axisScale;
		public ChartScale AxisScale
		{
			get { return axisScale; }
		}

		#region AxisContentLayout
		AxisContentLayout contentLayout;
		public AxisContentLayout ContentLayout
		{
			get { return contentLayout; }
			set
			{
				if (contentLayout != value)
				{
					contentLayout = value;
					Notify("ContentLayout");
				}
			}
		}

		public bool IsAtLeftOrBelow
		{
			get { return (contentLayout & AxisContentLayout.AtLeftOrBelow) != 0; }
			set
			{
				AxisContentLayout cl = ContentLayout;
				if (value)
				{
					cl &= ~AxisContentLayout.AtRightOrAbove;
					cl |= AxisContentLayout.AtLeftOrBelow;
				}
				else
				{
					cl |= AxisContentLayout.AtRightOrAbove;
					cl &= ~AxisContentLayout.AtLeftOrBelow;
				}
				ContentLayout = cl;
			}
		}

		public bool IsAtRightOrAbove
		{
			get { return (contentLayout & AxisContentLayout.AtRightOrAbove) != 0; }
			set
			{
				AxisContentLayout cl = ContentLayout;
				if (value)
				{
					cl |= AxisContentLayout.AtRightOrAbove;
					cl &= ~AxisContentLayout.AtLeftOrBelow;
				}
				else
				{
					cl &= ~AxisContentLayout.AtRightOrAbove;
					cl |= AxisContentLayout.AtLeftOrBelow;
				}
				ContentLayout = cl;
			}
		}

		public bool IsTicksCentered
		{
			get { return (contentLayout & AxisContentLayout.TicksCentered) != 0; }
			set
			{
				if (value)
					ContentLayout |= AxisContentLayout.TicksCentered;
				else
					ContentLayout &= ~AxisContentLayout.TicksCentered;
			}
		}
		#endregion AxisContentLayout

		#region Ticks
		#region TickLength
		double tickLength;
		/// <summary>
		/// Gets or sets the TickLength property.
		/// </summary>
		/// <value>Any finite positive double value.</value>
		public double TickLength
		{
			get { return tickLength; }
			set
			{
				if (tickLength != value)
				{
					tickLength = value;
					Notify("TickLength");
				}
			}
		}
		#endregion TickLength

		#region LongTickLength
		double longTickLength;
		/// <summary>
		/// Gets or sets the LongTickLength property.
		/// </summary>
		/// <value>Any finite positive double value.</value>
		public double LongTickLength
		{
			get { return longTickLength; }
			set
			{
				if (longTickLength != value)
				{
					longTickLength = value;
					Notify("LongTickLength");
				}
			}
		}
		#endregion LongTickLength
		#endregion Ticks

		#region Label
		#region LabelFormat
		string labelFormat;
		/// <summary>
		/// Gets or sets the LabelFormat property.
		/// <para>Standard format string.</para>
		/// </summary>
		public string LabelFormat
		{
			get { return labelFormat; }
			set
			{
				if (labelFormat != value)
				{
					labelFormat = value;
					Notify("LabelFormat");
				}
			}
		}
		#endregion LabelFormat

		#region LabelMargin
		double labelMargin;
		/// <summary>
		/// Gets or sets the LabelMargin property.
		/// <para>This is the distance between long tich and its label.</para>
		/// </summary>
		/// <value>Any finite non-negative value.</value>
		public double LabelMargin
		{
			get { return labelMargin; }
			set
			{
				if (labelMargin != value)
				{
					labelMargin = value;
					Notify("LabelMargin");
				}
			}
		}
		#endregion LabelMargin
		#endregion Label

		#region Pen
		Color penColor;
		public Color PenColor
		{
			get { return penColor; }
			set
			{
				if (penColor != value)
				{
					penColor = value;
					Notify("PenColor");
				}
			}
		}

		double penThickness;
		public double PenThickness
		{
			get { return penThickness; }
			set
			{
				if (penThickness != value)
				{
					penThickness = value;
					Notify("PenThickness");
				}
			}
		}
		#endregion Pen

		#region Font
		#region FontFamily
		FontFamily fontFamily;
		/// <summary>
		/// Gets or sets the FontFamily property.
		/// </summary>
		public FontFamily FontFamily
		{
			get { return fontFamily; }
			set
			{
				if (fontFamily != value)
				{
					fontFamily = value;
					Notify("FontFamily");
				}
			}
		}
		#endregion FontFamily

		#region FontSize
		double fontSize;
		/// <summary>
		/// Gets or sets the FontSize property.
		/// </summary>
		public double FontSize
		{
			get { return fontSize; }
			set
			{
				if (fontSize != value)
				{
					fontSize = value;
					Notify("FontSize");
				}
			}
		}
		#endregion FontSize

		#region FontStretch
		FontStretch fontStretch;
		/// <summary>
		/// Gets or sets the FontStretch property.
		/// </summary>
		public FontStretch FontStretch
		{
			get { return fontStretch; }
			set
			{
				if (fontStretch != value)
				{
					fontStretch = value;
					Notify("FontStretch");
				}
			}
		}
		#endregion FontStretch

		#region FontStyle
		FontStyle fontStyle;
		/// <summary>
		/// Gets or sets the FontStyle property.
		/// </summary>
		public FontStyle FontStyle
		{
			get { return fontStyle; }
			set
			{
				if (fontStyle != value)
				{
					fontStyle = value;
					Notify("FontStyle");
				}
			}
		}
		#endregion FontStyle

		#region FontWeight
		FontWeight fontWeight;
		/// <summary>
		/// Gets or sets the FontWeight property.
		/// </summary>
		public FontWeight FontWeight
		{
			get { return fontWeight; }
			set
			{
				if (fontWeight != value)
				{
					fontWeight = value;
					Notify("FontWeight");
				}
			}
		}
		#endregion FontWeight
		#endregion Font

		#region INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;
		protected void Notify(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion INotifyPropertyChanged Members

		#region IDataErrorInfo Members
		string IDataErrorInfo.Error
		{
			get { return null; }
		}

		protected virtual string GetDataErrorInfo(string columnName)
		{
			switch (columnName)
			{
				case "TickLength":
					if (TickLength <= 0)
						return "TickLength must be positive";
					break;
				case "LongTickLength":
					if (LongTickLength <= 0)
						return "LongTickLength must be positive";
					break;
				case "LabelMargin":
					if (LabelMargin < 0)
						return "LabelMargin cannot be negative";
					break;
				case "PenThickness":
					if (PenThickness <= 0)
						return "PenThickness must be positive";
					break;
			}
			return null;
		}

		string IDataErrorInfo.this[string columnName]
		{
			get
			{
				return GetDataErrorInfo(columnName);
			}
		}
		#endregion IDataErrorInfo Members
	}
}
