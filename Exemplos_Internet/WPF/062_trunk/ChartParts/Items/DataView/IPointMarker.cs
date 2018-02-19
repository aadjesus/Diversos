// <copyright file="IPointMarker.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-01-20</date>
// <summary>OpenWPFChart library. IPointMarker interface.</summary>
// <revision>$Id: IPointMarker.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// PointMarker interface.
	/// </summary>
	public interface IPointMarker
	{
		/// <summary>
		/// Gets or sets the PointMarker property.
		/// </summary>
		/// <value>The Drawing.</value>
		Drawing PointMarker { get; set; }

		/// <summary>
		/// Gets or sets the PointMarkerVisible property.
		/// </summary>
		/// <value>true to draw PointMarkers.</value>
		bool PointMarkerVisible { get; set; }
	}
}
