// <copyright file="LinearAxisDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-06</date>
// <summary>OpenWPFChart library. Linear Axis DataView.</summary>
// <revision>$Id: LinearAxisDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using OpenWPFChart.Parts;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// Coordinate Axis Properties dialog.
	/// </summary>
	public class LinearAxisDataView : AxisDataView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LinearAxisDataView"/> class.
		/// </summary>
		/// <param name="axis">The axis.</param>
		public LinearAxisDataView(LinearAxis axis) : base(axis)
		{
			ChartLinearScale linearScale = axis.AxisScale as ChartLinearScale;
			tickStep = linearScale.TickStep;
			longTickAnchor = linearScale.LongTickAnchor;
			longTickRate = linearScale.LongTickRate;
		}

		#region TickStep
		double tickStep;
		/// <summary>
		/// Gets or sets the TickStep property.
		/// <para>The space between two regular (not long) ticks. It should be positive regardeless 
		/// of <see cref="Start"/> and <see cref="Stop"/> values relationship.</para>
		/// </summary>
		/// <value>Any positive double value.</value>
		public double TickStep
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
		double longTickAnchor;
		/// <summary>
		/// Gets or sets the LongTickAnchor property.
		/// <para>Represents the position of any long tick.</para>
		/// </summary>
		/// <value>Any finite double value.</value>
		public double LongTickAnchor
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
			switch(columnName)
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
