// <copyright file="ChartSeriesScale.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-01-02</date>
// <summary>OpenWPFChart library. Scale based on discrete Series of values.</summary>
// <revision>$Id: ChartSeriesScale.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Linear Chart Scale based on discrete Series of values (e.g. the <see langword="string"/> collection).
	/// </summary>
	/// <remarks>
	/// This class Scale property means pixel count between two adjacent series items.
	/// </remarks>
	public class ChartSeriesScale : ChartScale
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ChartSeriesScale"/> class.
		/// </summary>
		/// <overloads>
		/// <summary><see cref="ChartSeriesScale"/> constructors.</summary>
		/// <remarks>
		/// Parametrized constructor arranges the Scale properties so that it fills itself 
		/// confortable in the screen extent propvided.
		/// </remarks>
		/// </overloads>
		public ChartSeriesScale() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ChartSeriesScale"/> class.
		/// </summary>
		/// <remarks>
		/// Arranges the Scale so that it fills itself confortable in the screen extent propvided.
		/// </remarks>
		/// <param name="series">The series.</param>
		/// <param name="start">Scale start.</param>
		/// <param name="stop">Scale stop.</param>
		/// <param name="extent">Scale visual extent in pixels.</param>
		public ChartSeriesScale(IEnumerable<object> series, object start, object stop, double extent)
		{
			if (series.Contains(start) && series.Contains(stop) && start != stop)
			{
				Series = series;
				Start = start;
				Stop = stop;
				Scale = extent / (series.Count() - 1);
				LongTickAnchor = start;
				LongTickRate = 3;
			}
		}
		#endregion Constructors

		#region Series
		IEnumerable series;
		/// <summary>
		/// Gets or sets the <see cref="Series"/> property.
		/// </summary>
		/// <remarks>
		/// The series must not contain reference to the same object.
		/// </remarks>
		/// <value><see cref="Series"/> iterator.</value>
		public IEnumerable Series
		{
			get { return series; }
			set
			{
				if (series != value)
				{
					series = value;
					NotifyPropertyChanged("Series");
				}
			}
		}
		#endregion Series

		#region LongTickAnchor
		object longTickAnchor;
		/// <summary>
		/// Gets or sets the <see cref="LongTickAnchor"/> property.
		/// </summary>
		/// <remarks>
		/// <para>Represents the position of some long tick.</para>
		/// <para>Returned value is coersed to one of the <see cref="Series"/> objects.</para>
		/// </remarks>
		/// <value>One of the <see cref="Series"/> objects.</value>
		public object LongTickAnchor
		{
			get 
			{ 
				if (Series == null)
					return longTickAnchor;
				List<object> list = Series.Cast<object>().ToList();
				if (list.Count == 0 || list.IndexOf(longTickAnchor) >= 0)
					return longTickAnchor;
				return list[0];
			}
			set
			{
				if (longTickAnchor != value)
				{
					longTickAnchor = value;
					NotifyPropertyChanged("LongTickAnchor");
				}
			}
		}
		#endregion LongTickAnchor

		#region LongTickRate
		int longTickRate = 10;
		/// <summary>
		/// Gets or sets the <see cref="LongTickRate"/> property.
		/// </summary>
		/// <remarks>
		/// <para>Represents long ticks rate; e.g. 
		/// <list>
		/// if <see cref="LongTickRate"/> == 1 then all ticks will be long.
		/// if <see cref="LongTickRate"/> == 2 then will be one short tick between two long ticks.
		/// if <see cref="LongTickRate"/> == 10 then every tenth tick will be long.
		/// </list>
		/// </para>
		/// <para>Must be positive.</para>
		/// </remarks>
		/// <value>Long ticks rate.</value>
		public int LongTickRate
		{
			get { return longTickRate; }
			set
			{
				if (longTickRate != value)
				{
					if (value <= 0)
						throw new ArgumentException("LongTickRate must be positive", "value");
					longTickRate = value;
					NotifyPropertyChanged("LongTickRate");
				}
			}
		}
		#endregion LongTickRate

		/// <inheritdoc />
		public override bool IsConsistent
		{
			get
			{
				if (!base.IsConsistent || Series == null)
					return false;

				// Check if the Start and Stop Values belonge to the Series.
				List<object> list = Series.Cast<object>().ToList();
				int start = list.IndexOf(Start), stop = list.IndexOf(Stop);
				return (start >= 0 && stop >= 0 && start != stop);
			}
		}

		/// <inheritdoc />
		public override double ToPixels(object value)
		{
			if (!IsConsistent)
				throw new InvalidOperationException("Object isn't properly initialized");

			List<object> list = Series.Cast<object>().ToList();
			int valueIndex = list.IndexOf(value);
			int start = list.IndexOf(Start), stop = list.IndexOf(Stop);
			double scale = Scale;
			if (valueIndex < 0)
				throw new ArgumentException("Object isn't in the Series", "value");
			if (start < stop)
				return (valueIndex - start) * scale;
			else // (start > stop)
				return (start - valueIndex) * scale;
		}

		/// <inheritdoc />
		public override object FromPixels(double value)
		{
			if (!IsConsistent)
				throw new InvalidOperationException("Object isn't properly initialized");

			List<object> list = Series.Cast<object>().ToList();
			int start = list.IndexOf(Start), stop = list.IndexOf(Stop);
			double scale = Scale;

			int valueIndex;
			if (start < stop)
				valueIndex = (int)(value / scale) + start;
			else // (start > stop)
				valueIndex = start - (int)(value / scale);
			if (valueIndex < 0 || valueIndex >= list.Count)
				throw new ArgumentOutOfRangeException("Object isn't in the Series", "value");
			return list.ElementAt(valueIndex);
		}

		#region Ticks iterator
		/// <inheritdoc />
		public override IEnumerable<ScaleTick> Ticks()
		{
			if (!IsConsistent)
				yield break;

			List<object> list = Series.Cast<object>().ToList();
			int longTickRate = LongTickRate;
			int longTickAnchorIndex = list.IndexOf(LongTickAnchor);
			int start = list.IndexOf(Start), stop = list.IndexOf(Stop);
			if (start <= stop)
			{
				for (int i = start; i <= stop; ++i)
				{
					if ((i - longTickAnchorIndex) % longTickRate == 0)
						yield return new ScaleTick(list[i], true);
					else
						yield return new ScaleTick(list[i], false);
				}
			}
			else // start > stop
			{
				for (int i = start; i >= stop; --i)
				{
					if ((i - longTickAnchorIndex) % longTickRate == 0)
						yield return new ScaleTick(list[i], true);
					else
						yield return new ScaleTick(list[i], false);
				}
			}
		}
		#endregion Ticks iterator
	}
}
