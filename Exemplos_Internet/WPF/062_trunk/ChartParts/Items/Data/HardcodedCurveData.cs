// <copyright file="HardcodedCurveData.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart  library. HardcodedCurveData class contains delegate to get hardcoded function values.</summary>
// <revision>$Id: HardcodedCurveData.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// <see cref="HardcodedCurveData{TAbs, TOrd}"/> class contains <see langword="delegate"/>
	///	to get hardcoded function values.
	/// </summary>
	/// <typeparam name="TAbs">The type of the abscissas.</typeparam>
	/// <typeparam name="TOrd">The type of the ordinate.</typeparam>
	public class HardcodedCurveData<TAbs, TOrd> : ItemData
	{
		#region Curve
		/// <summary>
		/// Hardcoded function to draw.
		/// </summary>
		public delegate TOrd CurveDelegate(TAbs x);

		CurveDelegate curve;
		/// <summary>
		/// Gets or sets the Curve property.
		/// </summary>
		/// <value>The Curve delegate.</value>
		public CurveDelegate Curve
		{
			get { return curve; }
			set
			{
				if (curve != value)
				{
					curve = value;
					NotifyPropertyChanged("Curve");
				}
			}
		}
		#endregion Curve

		/// <inheritdoc />
		public override Type TypeOfAbscissa { get { return typeof(TAbs); } }

		/// <inheritdoc />
		public override Type TypeOfOrdinate { get { return typeof(TOrd); } }

		/// <inheritdoc />
		public override bool IsAbscissasEqual(ItemData item)
		{
			return TypeOfAbscissa == item.TypeOfAbscissa;
		}

		/// <inheritdoc />
		public override bool IsOrdinatesEqual(ItemData item)
		{
			return TypeOfOrdinate == item.TypeOfOrdinate;
		}
	}
}
