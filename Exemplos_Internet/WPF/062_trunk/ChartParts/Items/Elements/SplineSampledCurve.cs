// <copyright file="SplineSampledCurve.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart  library. SplineSampledCurve element class.</summary>
// <revision>$Id: SplineSampledCurve.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Windows.Data;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// SplineSampledCurve element class.
	/// </summary>
	public class SplineSampledCurve : Item
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SplineSampledCurve"/> class.
		/// </summary>
		/// <remarks>
		/// Initializes this object with <see cref="SplineSampledCurveVisual"/>.
		/// </remarks>
		public SplineSampledCurve()
		{
			SplineSampledCurveVisual itemVisual = new SplineSampledCurveVisual();
			BindingOperations.SetBinding(itemVisual, ItemDataViewProperty
				, new Binding("ItemDataView") { Source = this });
			visuals.Add(itemVisual);
		}
	}
}
