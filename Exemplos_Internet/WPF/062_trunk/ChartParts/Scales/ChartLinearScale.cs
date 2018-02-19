// <copyright file="ChartLinearScale.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. Linear ChartScale of double type and its decorations layout.</summary>
// <revision>$Id: ChartLinearScale.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Linear Chart Scale of the <see langword="double"/> base type and its decorations layout.
	/// </summary>
	/// <remarks>
	/// <see cref="ChartLinearScale"/> requires the <see cref="ChartScale.Start"/> and <see cref="ChartScale.Stop"/>
	/// properties values are convertible to the <see langword="double"/> type.
	/// <para>This class Scale property means pixel count per <see langword="double"/> <c>1.0</c>.</para>
	/// </remarks>
	public class ChartLinearScale : ChartScale
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ChartLinearScale"/> class.
		/// </summary>
		/// <overloads>
		/// <summary><see cref="ChartLinearScale"/> constructors.</summary>
		/// <remarks>
		/// Parametrized constructors arrange the Scale properties so that it fills itself 
		/// confortable in the screen extent propvided.
		/// </remarks>
		/// </overloads>
		public ChartLinearScale() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ChartLinearScale"/> class.
		/// </summary>
		/// <remarks>
		/// Arranges the Scale so that it fills itself confortable in the screen extent propvided.
		/// </remarks>
		/// <param name="start">Scale start.</param>
		/// <param name="stop">Scale stop.</param>
		/// <param name="extent">Scale visual extent in pixels.</param>
		public ChartLinearScale(double start, double stop, double extent)
		{
			autoArrage(start, stop, extent);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ChartLinearScale"/> class.
		/// </summary>
		/// <remarks>
		/// Arranges the Scale so that it fills itself confortable in the screen extent propvided.
		/// </remarks>
		/// <param name="start">Scale start.</param>
		/// <param name="stop">Scale stop.</param>
		/// <param name="extent">Scale visual extent in pixels.</param>
		public ChartLinearScale(object start, object stop, double extent)
		{
			try
			{
				double dblStart = Convert.ToDouble(start), dblStop = Convert.ToDouble(stop);
				autoArrage(dblStart, dblStop, extent);
			}
			catch (InvalidCastException)
			{
			}
		}

		/// <summary>
		/// Arranges the Scale so that it fills itself confortable in the screen extent propvided.
		/// </summary>
		/// <param name="start">Scale start.</param>
		/// <param name="stop">Scale stop.</param>
		/// <param name="extent">Scale visual extent in pixels.</param>
		void autoArrage(double start, double stop, double extent)
		{
			if (start == stop)
				return;

			Start = start;
			Stop = stop;
			Scale = extent / Math.Abs(start - stop);
			TickStep = Math.Abs(start - stop) / 20;
			LongTickAnchor = start;
			LongTickRate = 5;
		}
		#endregion Constructors

		#region TickStep
		double tickStep = double.NaN;
		/// <summary>
		/// Gets or sets the TickStep property.
		/// </summary>
		/// <remarks>
		/// <para>The space between two regular (not long) adjacent ticks. It should be positive regardeless 
		/// of <see cref="ChartScale.Start"/> and <see cref="ChartScale.Stop"/> values relationship.</para>
		/// <para>Must be positive.</para>
		/// </remarks>
		/// <value>The space between two regular adjacent ticks.</value>
		public double TickStep
		{
			get { return tickStep; }
			set
			{
				if (tickStep != value)
				{
					if (double.IsInfinity(value) || double.IsNaN(value) || value <= 0.0)
						throw new ArgumentException("TickStep must be finite and positive", "value");
					tickStep = value;
					NotifyPropertyChanged("TickStep");
				}
			}
		}
		#endregion TickStep

		#region LongTickAnchor
		double longTickAnchor;
		/// <summary>
		/// Gets or sets the <see cref="LongTickAnchor"/> property.
		/// </summary>
		/// <remarks>
		/// The position of any long tick.
		/// </remarks>
		/// <value>Some long tick position.</value>
		public double LongTickAnchor
		{
			get { return longTickAnchor; }
			set
			{
				if (longTickAnchor != value)
				{
					if (double.IsInfinity(value) || double.IsNaN(value))
						throw new ArgumentException("LongTickAnchor must be finite and positive", "value");
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
				if (!base.IsConsistent || double.IsNaN(TickStep))
					return false;
				try
				{
					double start = Convert.ToDouble(Start);
					if (double.IsNaN(start) || double.IsInfinity(start))
						return false;
					double stop = Convert.ToDouble(Stop);
					if (double.IsNaN(stop) || double.IsInfinity(stop))
						return false;
					if (start == stop)
						return false;
					return true;
				}
				catch (InvalidCastException)
				{
					return false;
				}
			}
		}

		/// <inheritdoc />
		public override double ToPixels(object value)
		{
			if (!base.IsConsistent)
				throw new InvalidOperationException("Object isn't properly initialized");
			double doubleValue = Convert.ToDouble(value), start = Convert.ToDouble(Start)
				, stop = Convert.ToDouble(Stop), scale = Scale;

			if (start < stop)
				return (doubleValue - start) * scale;
			else // (start > stop)
				return (start - doubleValue) * scale;
		}

		/// <inheritdoc />
		public override object FromPixels(double value)
		{
			if (!base.IsConsistent)
				throw new InvalidOperationException("Object isn't properly initialized");
			double start = Convert.ToDouble(Start), stop = Convert.ToDouble(Stop), scale = Scale;

			if (start < stop)
				return value / scale + start;
			else // (start > stop)
				return start - value / scale;
		}

		#region Ticks iterator
		/// <inheritdoc />
		public override IEnumerable<ScaleTick> Ticks()
		{
			if (!IsConsistent)
				yield break;

			double start = Convert.ToDouble(Start), stop = Convert.ToDouble(Stop);
			if (start == stop)
				yield break;

			int longTickRate = LongTickRate;
			double tickStep = TickStep;
			double longTickAnchor = nearestLongTick();
			double tickPos = longTickAnchor;
			int ticksProcessed = 0;
			if (start < stop)
			{
				while (tickPos <= stop)
				{
					if (tickPos >= start)
						yield return new ScaleTick(tickPos, ticksProcessed % longTickRate == 0);
					tickPos = longTickAnchor + ++ticksProcessed * tickStep;
				}
			}
			else // start > stop
			{
				while (tickPos >= stop)
				{
					if (tickPos <= start)
						yield return new ScaleTick(tickPos, ticksProcessed % longTickRate == 0);
					tickPos = longTickAnchor - ++ticksProcessed * tickStep;
				}
			}
		}

		/// <summary>
		/// Find nearest Long Tick position. It is:
		/// <list type="bullet">
		/// <item>at the left of the scale range if Start &lt; Stop</item>.
		/// <item>at the right of the scale range if Start &gt; Stop</item>.
		/// <item>not defined if Start == Stop</item>.
		/// </list>
		/// </summary>
		/// <returns>Nearest Long Tick position in Start/Stop units.</returns>
		private double nearestLongTick()
		{
			double start = Convert.ToDouble(Start), stop = Convert.ToDouble(Stop);
			if (start == stop)
				return double.NaN;

			// Distance between two adjacent long Ticks.
			double longTickStep = TickStep * LongTickRate;
			Debug.Assert(longTickStep > 0.0, "longTickStep > 0.0");
			double longTickAnchor = LongTickAnchor;

			double anchor = 0.0;
			if (start < stop)
			{ // LongTickAnchor should be at the left of the scale range
				if (longTickAnchor > start)
				{
					int n = (int)((longTickAnchor - start) / longTickStep);
					anchor = longTickAnchor - (n + 1) * longTickStep;
					if (anchor + longTickStep == start)
						anchor = start;
				}
				else if (longTickAnchor < start)
				{
					int n = (int)((start - longTickAnchor) / longTickStep);
					anchor = longTickAnchor + n * longTickStep;
				}
				else
					anchor = longTickAnchor;

				Debug.Assert(anchor <= start, "anchor <= start");
				Debug.Assert(anchor + longTickStep > start, "anchor + longTickStep > start");
			}
			else // start > stop
			{ // LongTickAnchor should be at the right of the scale range
				if (longTickAnchor > start)
				{
					int n = (int)((longTickAnchor - start) / longTickStep);
					anchor = longTickAnchor - n * longTickStep;
				}
				else if (longTickAnchor < start)
				{
					int n = (int)((start - longTickAnchor) / longTickStep);
					anchor = longTickAnchor + (n + 1) * longTickStep;
					if (anchor - longTickStep == start)
						anchor = start;
				}
				else
					anchor = longTickAnchor;

				Debug.Assert(anchor >= start, "anchor >= start");
				Debug.Assert(anchor - longTickStep < start, "anchor - longTickStep < start");
			}
			return anchor;
		}
		#endregion Ticks iterator
	}
}
