// <copyright file="ChartPointVisual.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart  library. Chart Point Visual.</summary>
// <revision>$Id: ChartPointVisual.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// ChartPointVisual class is required just to distinguish Chart Points form other visual types 
	/// on Hit testing.
	/// </summary>
	public class ChartPointVisual : DrawingVisual
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ChartPointVisual"/> class.
		/// </summary>
		/// <param name="marker">The marker <see cref="Drawing"/>.</param>
		public ChartPointVisual(Drawing marker)
		{
			using (DrawingContext dc = RenderOpen())
			{
				dc.DrawDrawing(marker);
			}
		}
	}
}
