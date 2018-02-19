// <copyright file="HardcodedCurve.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart  library. HardcodedCurve element class.</summary>
// <revision>$Id: HardcodedCurve.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Windows.Data;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// HardcodedCurve element class.
	/// </summary>
	public class HardcodedCurve : Item
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HardcodedCurve"/> class.
		/// </summary>
		/// <remarks>Initializes this object with <see cref="HardcodedCurveVisual"/>.</remarks>
		public HardcodedCurve()
		{
			HardcodedCurveVisual itemVisual = new HardcodedCurveVisual();
			BindingOperations.SetBinding(itemVisual, ItemDataViewProperty
				, new Binding("ItemDataView") { Source = this });
			visuals.Add(itemVisual);
		}
	}
}
