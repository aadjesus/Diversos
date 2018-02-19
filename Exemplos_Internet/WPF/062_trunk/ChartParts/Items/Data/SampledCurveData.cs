// <copyright file="SampledCurveData.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. SampledCurveData class contains set of curve points.</summary>
// <revision>$Id: SampledCurveData.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// <see cref="SampledCurveData{TAbs, TOrd}"/> class contains set of curve points of 
	/// <see cref="DataPoint{TAbs, TOrd}"/> type.
	/// </summary>
	/// <typeparam name="TAbs">The type of the abscissa.</typeparam>
	/// <typeparam name="TOrd">The type of the ordinate.</typeparam>
	/// <remarks>
	/// The point collection should be non-strictly ordered by abscissas either 
	/// ascending or descending.
	/// </remarks>
	public class SampledCurveData<TAbs, TOrd> : ItemData
	{
		#region Points
		// TODO Change type of SampledCurveData.Points property to ObservableCollection and supply appropriate TypeConverter?
		IEnumerable<DataPoint<TAbs, TOrd>> points;
		/// <summary>
		/// Gets or sets the <see cref="Points"/> property.
		/// </summary>
		/// <value><see cref="DataPoint{TAbs, TOrd}"/> iterator.</value>
		public IEnumerable<DataPoint<TAbs, TOrd>> Points
		{
			get { return points; }
			set
			{
				if (points != value)
				{
					if (points != null)
					{
						INotifyCollectionChanged iNotifyCollectionChanged
							= points as INotifyCollectionChanged;
						if (iNotifyCollectionChanged != null)
							iNotifyCollectionChanged.CollectionChanged -= CollectionChanged;
					}

					if (value != null)
					{ // Check the value.
						// Has the collection IComparable?
						foreach (Type item in typeof(TAbs).GetInterfaces())
						{
							if (item is IComparable<TAbs>)
							{
								// Check whether the specified point collection is non-strictly ordered.
								if (!isNonStrictlyOrdered(value))
									throw new ArgumentException("Points must be ordered by abscissa", "value");
							}
						}
					}

					points = value;
					if (points != null)
					{
						INotifyCollectionChanged iNotifyCollectionChanged
							= points as INotifyCollectionChanged;
						if (iNotifyCollectionChanged != null)
							iNotifyCollectionChanged.CollectionChanged += CollectionChanged;
					}
					NotifyPropertyChanged("Points");
				}
			}
		}

		/// <summary>
		/// Point collection changed in some way.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
		private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			NotifyPropertyChanged("Points");
		}

		/// <summary>
		/// Determines whether the specified point collection is non-strictly ordered (ascending or descending).
		/// </summary>
		/// <param name="points">The points.</param>
		/// <returns>
		/// 	<c>true</c> if the specified points is ordered (non-strictly); otherwise, <c>false</c>.
		/// </returns>
		static bool isNonStrictlyOrdered(IEnumerable<DataPoint<TAbs, TOrd>> points)
		{
			bool? ordering = null;
			DataPoint<TAbs, TOrd> prevPoint = new DataPoint<TAbs,TOrd>();
			int i = 0;
			IEnumerator<DataPoint<TAbs, TOrd>> en = points.GetEnumerator();
			while (en.MoveNext())
			{
				DataPoint<TAbs, TOrd> point = en.Current;
				if (i++ > 0)
				{
					int comparison = ((IComparable<TAbs>)prevPoint.X).CompareTo(point.X);
					if (comparison != 0)
					{
						if (ordering.HasValue)
						{
							if (ordering.Value != comparison < 0)
								return false;
						}
						else
						{
							ordering = comparison < 0;
						}
					}
				}
				prevPoint = point;
			}
			return true;
		}
		#endregion Points

		/// <inheritdoc />
		public override Type TypeOfAbscissa { get { return typeof(TAbs); } }

		/// <inheritdoc />
		public override Type TypeOfOrdinate { get { return typeof(TOrd); } }

		/// <inheritdoc />
		public override bool IsAbscissasEqual(ItemData item)
		{
			SampledCurveData<TAbs, TOrd> typed = item as SampledCurveData<TAbs, TOrd>;
			if (typed == null)
				return false;

			List<DataPoint<TAbs, TOrd>> points = Points.ToList();
			List<DataPoint<TAbs, TOrd>> otherPoints = typed.Points.ToList();
			if (points.Count != otherPoints.Count)
				return false;
			for (int i = 0; i < points.Count; ++i)
			{
				if (!(points[i].X.Equals(otherPoints[i].X)))
					return false;
			}
			return true;
		}

		/// <inheritdoc />
		public override bool IsOrdinatesEqual(ItemData item)
		{
			SampledCurveData<TAbs, TOrd> typed = item as SampledCurveData<TAbs, TOrd>;
			if (typed == null)
				return false;

			List<DataPoint<TAbs, TOrd>> points = Points.ToList();
			List<DataPoint<TAbs, TOrd>> otherPoints = typed.Points.ToList();
			if (points.Count != otherPoints.Count)
				return false;
			for (int i = 0; i < points.Count; ++i)
			{
				if (!(points[i].Y.Equals(otherPoints[i].Y)))
					return false;
			}
			return true;
		}
	}
}
