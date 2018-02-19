// <copyright file="ChartDateTimeScaleDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-02-18</date>
// <summary>OpenWPFChart library. ItemPropertiesDialog ChartDateTimeScale Data View class.</summary>
// <revision>$Id: ChartDateTimeScaleDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// ItemPropertiesDialog ChartDateTimeScale Data View class.
	/// </summary>
	public class ChartDateTimeScaleDataView : ChartScaleDataView
	{
		public ChartDateTimeScaleDataView(ChartDateTimeScale scale) : base(scale.Scale) 
		{
			start = Convert.ToDateTime(scale.Start);
			stop = Convert.ToDateTime(scale.Stop);
		}

		public ChartScaleVerieties ScaleVeriety
		{
			get { return ChartScaleVerieties.DateTime; }
		}
		public override ChartScaleVerieties GetVeriety() { return ChartScaleVerieties.DateTime; }

		DateTime start;
		public DateTime Start
		{
			get { return start; }
			set
			{
				if (start != value)
				{
					start = value;
					Notify("Start");
				}
			}
		}
		public override object GetStart() { return Start; }

		DateTime stop;
		public DateTime Stop
		{
			get { return stop; }
			set
			{
				if (stop != value)
				{
					stop = value;
					Notify("Stop");
				}
			}
		}
		public override object GetStop() { return Stop; }

		protected override string GetDataErrorInfo(string columnName)
		{
			switch(columnName)
			{
				case "Start":
				case "Stop":
					if (Start == Stop)
						return "Start and Stop must not be equal";
					break;
			}
			return base.GetDataErrorInfo(columnName);
		}
	}
}
