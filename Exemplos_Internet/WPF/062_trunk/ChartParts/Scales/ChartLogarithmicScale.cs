// <copyright file="ChartLogarithmicScale.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. Logarithmic ChartScale of double type and its decorations layout.</summary>
// <revision>$Id: ChartLogarithmicScale.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Generic;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Logarithmic ChartScale of the <see langword="double"/> base type and its decorations layout.
	/// <remarks>
	/// <see cref="ChartLogarithmicScale"/> requires the <see cref="ChartScale.Start"/> and <see cref="ChartScale.Stop"/>
	/// properties values are convertible to the <see langword="double"/> type.
	/// <para>This class Scale property means pixel count per logarithmic base.</para>
	/// </remarks>
	/// </summary>
	public class ChartLogarithmicScale : ChartScale
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ChartLogarithmicScale"/> class.
		/// </summary>
		/// <overloads>
		/// <summary><see cref="ChartLogarithmicScale"/> constructors.</summary>
		/// <remarks>
		/// Parametrized constructors arrange the Scale properties so that it fills itself 
		/// confortable in the screen extent propvided.
		/// </remarks>
		/// </overloads>
		public ChartLogarithmicScale() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ChartLogarithmicScale"/> class.
		/// </summary>
		/// <remarks>
		/// Arranges the Scale so that it fills itself confortable in the screen extent propvided.
		/// </remarks>
		/// <param name="start">Scale start.</param>
		/// <param name="stop">Scale stop.</param>
		/// <param name="extent">Scale visual extent in pixels.</param>
		public ChartLogarithmicScale(double start, double stop, double extent)
		{
			autoArrage(start, stop, extent);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ChartLogarithmicScale"/> class.
		/// </summary>
		/// <remarks>
		/// Arranges the Scale so that it fills itself confortable in the screen extent propvided.
		/// </remarks>
		/// <param name="start">Scale start.</param>
		/// <param name="stop">Scale stop.</param>
		/// <param name="extent">Scale visual extent in pixels.</param>
		public ChartLogarithmicScale(object start, object stop, double extent)
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
			if (start > 0 && stop > 0 && start != stop)
			{
				Start = start;
				Stop = stop;
				if (start > stop)
					Scale = extent / Math.Log10(start / stop);
				else
					Scale = extent / Math.Log10(stop / start);
			}
		}
		#endregion Constructors

		#region TickMask
		LogarithmicScaleTicks tickMask = LogarithmicScaleTicks.All;
		/// <summary>
		/// Gets or sets the TickMask property.
		/// </summary>
		/// <remarks>
		/// Ticks iterator returns masked ticks only.
		/// </remarks>
		/// <value></value>
		public LogarithmicScaleTicks TickMask
		{
			get { return tickMask; }
			set
			{
				if (tickMask != value)
				{
					tickMask = value;
					NotifyPropertyChanged("TickMask");
				}
			}
		}
		#endregion TickMask

		/// <inheritdoc />
		public override bool IsConsistent
		{
			get
			{
				if (!base.IsConsistent)
					return false;
				try
				{
					double start = Convert.ToDouble(Start);
					if (double.IsNaN(start) || double.IsInfinity(start))
						return false;
					double stop = Convert.ToDouble(Stop);
					if (double.IsNaN(stop) || double.IsInfinity(stop))
						return false;
					if (start > 0.0 && stop > 0.0 && start != stop)
						return true;
					return false;
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
			if (!IsConsistent)
				throw new InvalidOperationException("Object isn't properly initialized");
			double doubleValue = Convert.ToDouble(value), start = Convert.ToDouble(Start)
				, stop = Convert.ToDouble(Stop), scale = Scale;

			if (start < stop)
				return Math.Log10(doubleValue / start) * scale;
			else // (start > stop)
				return Math.Log10(start / doubleValue) * scale;
		}

		/// <inheritdoc />
		public override object FromPixels(double value)
		{
			if (!IsConsistent)
				throw new InvalidOperationException("Object isn't properly initialized");
			double start = Convert.ToDouble(Start), stop = Convert.ToDouble(Stop), scale = Scale;

			if (start < stop)
				return start * Math.Pow(10, value / scale);
			else // (start > stop)
				return start / Math.Pow(10, value / scale);
		}

		#region Ticks iterator
		/// <inheritdoc />
		public override IEnumerable<ScaleTick> Ticks()
		{
			if (!IsConsistent)
				yield break;

			double start = Convert.ToDouble(Start), stop = Convert.ToDouble(Stop);
			if (start <= 0.0 || stop <= 0.0 || start == stop)
				yield break;
			int mask = (int)TickMask;
			
			if (start < stop)
			{
				double tickPos = adjacentTick(start, true);
				if (tickPos > stop)
					yield break;
				double power;
				int tick = DecimateTick(tickPos, out power);

				while (tickPos <= stop)
				{
					if (tick == 1)
						yield return new ScaleTick(tickPos, true);
					else if (((1 << tick - 2) & mask) != 0)
						yield return new ScaleTick(tickPos, false);

					tick++;
					if (tick == 10)
					{
						tick = 1;
						power *= 10;
					}
					tickPos = tick * power;
				}
			}
			else // start > stop
			{
				double tickPos = adjacentTick(start, false);
				if (tickPos < stop)
					yield break;
				double power;
				int tick = DecimateTick(tickPos, out power);

				while (tickPos >= stop)
				{
					if (tick == 1)
						yield return new ScaleTick(tickPos, true);
					else if (((1 << tick - 2) & mask) != 0)
						yield return new ScaleTick(tickPos, false);

					tick--;
					if (tick == 0)
					{
						tick = 9;
						power /= 10;
					}
					tickPos = tick * power;
				}
			}
		}

		/// <summary>
		/// Presents tick value as a*10^n where n - any intger; a - integer, 1 &lt;= a &lt; 10.
		/// </summary>
		/// <param name="tick">input Tick value.</param>
		/// <param name="power">output value of 10^n.</param>
		/// <returns>truncated integer value of a</returns>
		private static int DecimateTick(double tick, out double power)
		{
			double log = Math.Log10(tick);
			int n = (int)log;
			power = Math.Pow(10, n);
			return (int)(tick / power);
		}

		/// <summary>
		/// Get the Tick adjacent (prev or next) to the value specified.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="bNext">next or previous</param>
		/// <returns></returns>
		private static double adjacentTick(double value, bool bNext)
		{
			if (value == 1.0)
				return 1.0;

			// Present value as a*10^n where n - any intger; 1.0 <= a < 10.0
			double log = Math.Log10(value);
			int n = (int)log;
			if (log < 0.0)
				n--;
			double power = Math.Pow(10, n);
			double a = value / power;
			int tick = (int)a;
			if (tick * power == value)
				return value;
			if (bNext)
				return (tick + 1) * power;
			else
				return tick * power;
		}
		#endregion Ticks iterator
	}
}
