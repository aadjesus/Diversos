// <copyright file="ScatteredPointsDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-02-09</date>
// <summary>OpenWPFChart library. ScatteredPointsDataView is the ItemDataView with the PointMarker.</summary>
// <revision>$Id: ScatteredPointsDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Windows;
using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// ScatteredPointsDataView is the ItemDataView with the PointMarker.
	/// </summary>
	public class ScatteredPointsDataView : ItemDataView, IPointMarker
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ScatteredPointsDataView"/> class.
		/// </summary>
		/// <remarks>
		/// This object default VisualCue is the <see cref="ScatteredPoints"/> Type.
		/// </remarks>
		public ScatteredPointsDataView()
		{
			VisualCue = typeof(ScatteredPoints);
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
