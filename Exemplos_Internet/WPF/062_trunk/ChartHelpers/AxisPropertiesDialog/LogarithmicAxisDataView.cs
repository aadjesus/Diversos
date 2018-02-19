// <copyright file="LogarithmicAxisDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-07</date>
// <summary>OpenWPFChart library. Logarithmic Axis Data View.</summary>
// <revision>$Id: LogarithmicAxisDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using OpenWPFChart.Parts;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// Logarithmic Axis Data View.
	/// </summary>
	public class LogarithmicAxisDataView : AxisDataView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LogarithmicAxisDataView"/> class.
		/// </summary>
		/// <param name="axis">The axis.</param>
		public LogarithmicAxisDataView(LogarithmicAxis axis) : base(axis)
		{
			ChartLogarithmicScale logScale = axis.AxisScale as ChartLogarithmicScale;
			tickMask = logScale.TickMask;
		}

		#region TickMask
		LogarithmicScaleTicks tickMask;
		/// <summary>
		/// Gets or sets the TickMask property.
		/// <para>TickMask is applied to Ticks iterator so that only masked ticks are returned.
		/// Applied to the small ticks only.</para>
		/// </summary>
		/// <value></value>
		public LogarithmicScaleTicks TickMask
		{
			get { return tickMask; }
			set
			{
				if (tickMask != value)
				{
					tickMask = value;
					Notify("TickMask");
				}
			}
		}

		public bool IsTickMaskSecondChecked
		{
			get { return (tickMask & LogarithmicScaleTicks.Two) != 0; }
			set
			{
				LogarithmicScaleTicks tm = TickMask;
				if (value)
					tm |= LogarithmicScaleTicks.Two;
				else
					tm &= ~LogarithmicScaleTicks.Two;
				TickMask = tm;
			}
		}

		public bool IsTickMaskThirdChecked
		{
			get { return (tickMask & LogarithmicScaleTicks.Three) != 0; }
			set
			{
				LogarithmicScaleTicks tm = TickMask;
				if (value)
					tm |= LogarithmicScaleTicks.Three;
				else
					tm &= ~LogarithmicScaleTicks.Three;
				TickMask = tm;
			}
		}

		public bool IsTickMaskFourthChecked
		{
			get { return (tickMask & LogarithmicScaleTicks.Four) != 0; }
			set
			{
				LogarithmicScaleTicks tm = TickMask;
				if (value)
					tm |= LogarithmicScaleTicks.Four;
				else
					tm &= ~LogarithmicScaleTicks.Four;
				TickMask = tm;
			}
		}

		public bool IsTickMaskFifthChecked
		{
			get { return (tickMask & LogarithmicScaleTicks.Five) != 0; }
			set
			{
				LogarithmicScaleTicks tm = TickMask;
				if (value)
					tm |= LogarithmicScaleTicks.Five;
				else
					tm &= ~LogarithmicScaleTicks.Five;
				TickMask = tm;
			}
		}

		public bool IsTickMaskSixthChecked
		{
			get { return (tickMask & LogarithmicScaleTicks.Six) != 0; }
			set
			{
				LogarithmicScaleTicks tm = TickMask;
				if (value)
					tm |= LogarithmicScaleTicks.Six;
				else
					tm &= ~LogarithmicScaleTicks.Six;
				TickMask = tm;
			}
		}

		public bool IsTickMaskSeventhChecked
		{
			get { return (tickMask & LogarithmicScaleTicks.Seven) != 0; }
			set
			{
				LogarithmicScaleTicks tm = TickMask;
				if (value)
					tm |= LogarithmicScaleTicks.Seven;
				else
					tm &= ~LogarithmicScaleTicks.Seven;
				TickMask = tm;
			}
		}

		public bool IsTickMaskEighthChecked
		{
			get { return (tickMask & LogarithmicScaleTicks.Eight) != 0; }
			set
			{
				LogarithmicScaleTicks tm = TickMask;
				if (value)
					tm |= LogarithmicScaleTicks.Eight;
				else
					tm &= ~LogarithmicScaleTicks.Eight;
				TickMask = tm;
			}
		}

		public bool IsTickMaskNinthChecked
		{
			get { return (tickMask & LogarithmicScaleTicks.Nine) != 0; }
			set
			{
				LogarithmicScaleTicks tm = TickMask;
				if (value)
					tm |= LogarithmicScaleTicks.Nine;
				else
					tm &= ~LogarithmicScaleTicks.Nine;
				TickMask = tm;
			}
		}
		#endregion TickMask
	}
}
