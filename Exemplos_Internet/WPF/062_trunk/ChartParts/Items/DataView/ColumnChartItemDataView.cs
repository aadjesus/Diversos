// <copyright file="ColumnChartItemDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-10</date>
// <summary>OpenWPFChart library. ColumnChartItem DataView.</summary>
// <revision>$Id: ColumnChartItemDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Windows;
using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// ColumnChartItem DataView.
	/// </summary>
	/// <remarks>
	/// This class adds columns-related properties to its SampledCurveDataView base class.
	/// </remarks>
	public class ColumnChartItemDataView : SampledCurveDataView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ColumnChartItemDataView"/> class.
		/// </summary>
		/// <remarks>
		/// This object VisualCue defines what curve (if any) is displayed with the Columns 
		/// (e.g. poliline, Bezier, etc.)
		/// Default VisualCue is the <see cref="PolylineSampledCurve"/> Type.
		/// </remarks>
		public ColumnChartItemDataView()
		{
			VisualCue = typeof(PolylineSampledCurve);
		}

		#region IsCurveVisible
		bool isCurveVisible = true;
		/// <summary>
		/// Gets or sets the <see cref="IsCurveVisible"/> property.
		/// </summary>
		/// <remarks>
		/// Defines if the curve is displayed along with the Columns.
		/// </remarks>
		/// <value/>
		public bool IsCurveVisible
		{
			get { return isCurveVisible; }
			set
			{
				if (isCurveVisible != value)
				{
					isCurveVisible = value;
					NotifyPropertyChanged("IsCurveVisible");
				}
			}
		}
		#endregion IsCurveVisible

		#region ColumnWidth
		double columnWidth = 10;
		/// <summary>
		/// Gets or sets the ColumnWidth property.
		/// </summary>
		/// <remarks>
		/// ColumnWidth in pixels; must be > 0.
		/// </remarks>
		/// <value>Width of Columns.</value>
		public double ColumnWidth
		{
			get { return columnWidth; }
			set
			{
				if (columnWidth != value)
				{
					columnWidth = value;
					NotifyPropertyChanged("ColumnWidth");
				}
			}
		}
		#endregion ColumnWidth

		#region ColumnBrush
		Brush columnBrush;
		/// <summary>
		/// Gets or sets the <see cref="ColumnBrush"/> property.
		/// </summary>
		/// <remarks>
		/// If the <see cref="ColumnBrush"/> isn't set the the <see cref="Pen"/> Brush is used instead.
		/// </remarks>
		/// <value>Brush the column is drawn with.</value>
		public Brush ColumnBrush
		{
			get { return columnBrush != null ? columnBrush : Pen.Brush; }
			set
			{
				if (columnBrush != value)
				{
					columnBrush = value;
					NotifyPropertyChanged("ColumnBrush");
				}
			}
		}
		#endregion ColumnBrush
	}
}
