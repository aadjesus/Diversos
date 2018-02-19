// <copyright file="ScatteredPointsData.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-02-09</date>
// <summary>OpenWPFChart library. ScatteredPointsData class contains set of scattered points.</summary>
// <revision>$Id: ScatteredPointsData.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// ScatteredPointsData class contains set of scattered points.
	/// <para>The point collection should be non-strictly ordered by abscissas either 
	/// ascending or descending.</para>
	/// </summary>
	/// <typeparam name="TAbs">The type of the abscissa.</typeparam>
	/// <typeparam name="TOrd">The type of the ordinate.</typeparam>
	public class ScatteredPointsData<TAbs, TOrd> : ItemData
	{
		#region Points
		// TODO Change type of ScatteredPointsData.Points property to ObservableCollection and supply appropriate TypeConverter?
		IEnumerable<DataPoint<TAbs, TOrd>> points;
		/// <summary>
		/// Gets or sets the Points property.
		/// </summary>
		/// <value/>
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
