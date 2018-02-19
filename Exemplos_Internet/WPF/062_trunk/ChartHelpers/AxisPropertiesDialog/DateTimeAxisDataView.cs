// <copyright file="DateTimeAxisDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-07</date>
// <summary>OpenWPFChart library. DateTime Axis DataView.</summary>
// <revision>$Id: DateTimeAxisDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// DateTime Axis DataView.
	/// </summary>
	public class DateTimeAxisDataView : AxisDataView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeAxisDataView"/> class.
		/// </summary>
		/// <param name="axis">The axis.</param>
		public DateTimeAxisDataView(DateTimeAxis axis) : base(axis)
		{
			ChartDateTimeScale dtScale = axis.AxisScale as ChartDateTimeScale;
			tickUnits = dtScale.TickUnits;
			tickStep = dtScale.TickStep;
			longTickAnchor = dtScale.LongTickAnchor;
			longTickRate = dtScale.LongTickRate;
		}

		#region TickUnits
		DateTimeTickUnits tickUnits;
		/// <summary>
		/// Gets or sets the TickUnits property.
		/// <para>Tick values are the integer number of DataTime in TickUnits (e.g. "Days").</para>
		/// </summary>
		public DateTimeTickUnits TickUnits
		{
			get { return tickUnits; }
			set
			{
				if (tickUnits != value)
				{
					tickUnits = value;
					Notify("TickUnits");
				}
			}
		}
		#endregion TickUnits

		#region TickStep
		long tickStep;
		/// <summary>
		/// Gets or sets the TickStep property.
		/// <para>The space between two regular (not long) ticks in DateTimeTickUnits.
		/// It should be positive regardeless of <see cref="Start"/> and <see cref="Stop"/>
		/// values relationship.</para>
		/// </summary>
		/// <value>Any positive long value.</value>
		public long TickStep
		{
			get { return tickStep; }
			set
			{
				if (tickStep != value)
				{
					tickStep = value;
					Notify("TickStep");
				}
			}
		}
		#endregion TickStep

		#region LongTickAnchor
		DateTime longTickAnchor;
		/// <summary>
		/// Gets or sets the LongTickAnchor property.
		/// <para>Represents the position of any long tick.</para>
		/// </summary>
		/// <value>Any finite double value.</value>
		public DateTime LongTickAnchor
		{
			get { return longTickAnchor; }
			set
			{
				if (longTickAnchor != value)
				{
					longTickAnchor = value;
					Notify("LongTickAnchor");
				}
			}
		}
		#endregion LongTickAnchor

		#region LongTickRate
		int longTickRate;
		/// <summary>
		/// Gets or sets the LongTickRate property.
		/// <para>Represents long ticks rate; e.g. 
		/// <list>
		/// if LongTickRate == 1 then all ticks will be long.
		/// if LongTickRate == 2 then will be one short tick between two long ticks.
		/// if LongTickRate == 10 then every tenth tick will be long.
		/// </list>
		/// </para>
		/// </summary>
		/// <value>Positive integer value.</value>
		public int LongTickRate
		{
			get { return longTickRate; }
			set
			{
				if (longTickRate != value)
				{
					longTickRate = value;
					Notify("LongTickRate");
				}
			}
		}
		#endregion LongTickRate

		#region IDataErrorInfo Members
		protected override string GetDataErrorInfo(string columnName)
		{
			switch (columnName)
			{
				case "TickStep":
					if (TickStep <= 0)
						return "TickStep must be positive";
					return null;
				case "LongTickRate":
					if (LongTickRate <= 0)
						return "LongTickRate must be positive";
					return null;
			}
			return base.GetDataErrorInfo(columnName);
		}
		#endregion IDataErrorInfo Members
	}
}
