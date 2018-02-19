// <copyright file="BezierSampledCurve.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart  library. BezierSampledCurve element class.</summary>
// <revision>$Id: BezierSampledCurve.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Windows.Data;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// BezierSampledCurve element class.
	/// </summary>
	public class BezierSampledCurve : Item
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BezierSampledCurve"/> class.
		/// </summary>
		public BezierSampledCurve()
		{
			BezierSampledCurveVisual itemVisual = new BezierSampledCurveVisual();
			BindingOperations.SetBinding(itemVisual, ItemDataViewProperty
				, new Binding("ItemDataView") { Source = this });
			visuals.Add(itemVisual);
		}
	}
}
