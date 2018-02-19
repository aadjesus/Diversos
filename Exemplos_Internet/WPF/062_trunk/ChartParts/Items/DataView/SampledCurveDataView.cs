// <copyright file="SampledCurveDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-01-19</date>
// <summary>OpenWPFChart library. SampledCurveDataView is the CurveDataView with the PointMarker.</summary>
// <revision>$Id: SampledCurveDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Windows;
using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// SampledCurveDataView is the CurveDataView with the PointMarker.
	/// </summary>
	public class SampledCurveDataView : CurveDataView, IPointMarker
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SampledCurveDataView"/> class.
		/// </summary>
		/// <remarks>
		/// This object default VisualCue is the <see cref="PolylineSampledCurve"/> Type.
		/// </remarks>
		public SampledCurveDataView()
		{
			VisualCue = typeof(PolylineSampledCurve);
		}

		#region IPointMarker implementation
		#region PointMarker
		Drawing pointMarker;
		/// <summary>
		/// Gets or sets the PointMarker property.
		/// </summary>
		/// <value>any Drawing or noll</value>
		public Drawing PointMarker
		{
			get { return pointMarker; }
			set
			{
				if (pointMarker != value)
				{
					pointMarker = value;
					NotifyPropertyChanged("PointMarker");
				}
			}
		}
		#endregion PointMarker

		#region PointMarkerVisible
		bool pointMarkerVisible = true;
		/// <summary>
		/// Gets or sets the PointMarkerVisible property.
		/// </summary>
		/// <value/>
		public bool PointMarkerVisible
		{
			get { return pointMarkerVisible; }
			set
			{
				if (pointMarkerVisible != value)
				{
					pointMarkerVisible = value;
					NotifyPropertyChanged("PointMarkerVisible");
				}
			}
		}
		#endregion PointMarkerVisible
		#endregion IPointMarker implementation
	}
}
