// <copyright file="ScatteredPoints.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-02-09</date>
// <summary>OpenWPFChart library. ScatteredPoints element class.</summary>
// <revision>$Id: ScatteredPoints.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Windows.Data;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// ScatteredPoints element class.
	/// </summary>
	public class ScatteredPoints : Item
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ScatteredPoints"/> class.
		/// </summary>
		/// <remarks>
		/// Initializes this object with <see cref="ScatteredPointsVisual"/>.
		/// </remarks>
		public ScatteredPoints()
		{
			ScatteredPointsVisual itemVisual = new ScatteredPointsVisual();
			BindingOperations.SetBinding(itemVisual, ItemDataViewProperty
				, new Binding("ItemDataView") { Source = this });
			visuals.Add(itemVisual);
		}
	}
}
