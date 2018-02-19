// <copyright file="DataPoint.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-01-17</date>
// <summary>OpenWPFChart library. DataPoint structure.</summary>
// <revision>$Id: DataPoint.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// 2D Data Point.
	/// </summary>
	/// <typeparam name="TAbs">The type of the abscissa.</typeparam>
	/// <typeparam name="TOrd">The type of the ordinate.</typeparam>
	public struct DataPoint<TAbs, TOrd>
	{
		/// <summary>
		/// Initializes a new instance of the DataPoint struct.
		/// </summary>
		/// <param name="x">The abscissa.</param>
		/// <param name="y">The ordinate.</param>
		public DataPoint(TAbs x, TOrd y)
		{
			this.x = x;
			this.y = y;
		}

		private readonly TAbs x;
		/// <summary>
		/// Gets the abscissa.
		/// </summary>
		/// <value>The abscissa.</value>
		public TAbs X { get { return x; } }

		private readonly TOrd y;
		/// <summary>
		/// Gets the ordinate.
		/// </summary>
		/// <value>The ordinate.</value>
		public TOrd Y { get { return y; } }

		/// <summary>
		/// Indicates whether this instance and a specified object are equal.
		/// </summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		/// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(DataPoint<TAbs, TOrd>))
				return false;
			DataPoint<TAbs, TOrd> pt = (DataPoint<TAbs, TOrd>)obj;
			return (X.Equals(pt.X) && Y.Equals(pt.Y));
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer that is the hash code for this instance.
		/// </returns>
		public override int GetHashCode()
		{
			return (X.GetHashCode() ^ Y.GetHashCode());
		}

		/// <summary>
		/// Returns this instance string representation.
		/// </summary>
		/// <returns>this instance <see cref="T:System.String"/> representation.</returns>
		public override string ToString()
		{
			return string.Format("({0},{1})", X, Y);
		}
	}
}
