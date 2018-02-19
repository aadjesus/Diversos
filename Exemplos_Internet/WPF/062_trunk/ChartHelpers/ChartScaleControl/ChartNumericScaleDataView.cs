// <copyright file="ChartNumericScaleDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-02-18</date>
// <summary>OpenWPFChart library. ItemPropertiesDialog ChartLinearScale and ChartLogarithmicScale Data View class.</summary>
// <revision>$Id: ChartNumericScaleDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// ItemPropertiesDialog ChartLinearScale and ChartLogarithmicScale Data View class.
	/// </summary>
	public class ChartNumericScaleDataView : ChartScaleDataView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ChartNumericScaleDataView"/> class.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="stop">The stop.</param>
		/// <param name="scale">The scale.</param>
		ChartNumericScaleDataView(object start, object stop, double scale) : base(scale)
		{
			this.start = Convert.ToDouble(start);
			this.stop = Convert.ToDouble(stop);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ChartNumericScaleDataView"/> class
		/// for ChartLinearScale.
		/// </summary>
		/// <param name="scale">The scale.</param>
		public ChartNumericScaleDataView(ChartLinearScale scale) 
			: this(scale.Start, scale.Stop, scale.Scale)
		{
			if (Start > 0 && Stop > 0 && AllowSwapLinLogScale)
			{
				scaleVerieties = new ChartScaleVerieties[]
				{ 
					ChartScaleVerieties.Linear, 
					ChartScaleVerieties.Logarithmic 
				};
			}
			else
			{
				scaleVerieties = new ChartScaleVerieties[]
				{ 
					ChartScaleVerieties.Linear, 
				};
			}
			scaleVeriety = scaleVerieties[0];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ChartNumericScaleDataView"/> class
		/// for ChartLogarithmicScale.
		/// </summary>
		/// <param name="scale">The scale.</param>
		public ChartNumericScaleDataView(ChartLogarithmicScale scale) 
			: this(scale.Start, scale.Stop, scale.Scale)
		{
			if (AllowSwapLinLogScale)
			{
				scaleVerieties = new ChartScaleVerieties[]
				{ 
					ChartScaleVerieties.Linear, 
					ChartScaleVerieties.Logarithmic 
				};
			}
			else
			{
				scaleVerieties = new ChartScaleVerieties[]
				{ 
					ChartScaleVerieties.Linear, 
				};
			}
			scaleVeriety = scaleVerieties[1];
		}

		ChartScaleVerieties[] scaleVerieties;
		public ChartScaleVerieties[] ScaleVerieties
		{
			get
			{
				return scaleVerieties; 
			}
			private set
			{
				if (scaleVerieties != value)
				{
					scaleVerieties = value;
					Notify("ScaleVerieties");
				}
			}
		}

		ChartScaleVerieties scaleVeriety;
		public ChartScaleVerieties ScaleVeriety
		{
			get
			{ 
				return scaleVeriety; 
			}
			set
			{
				if (scaleVeriety != value)
				{
					scaleVeriety = value;
					Notify("ScaleVeriety");
				}
			}
		}
		public override ChartScaleVerieties GetVeriety()
		{ 
			return ScaleVeriety; 
		}

		double start;
		public double Start
		{
			get { return start; }
			set 
			{
				if (start != value)
				{
					start = value;
					if (start <= 0)
					{
						if (ScaleVerieties.Length == 2)
						{
							ScaleVerieties = new ChartScaleVerieties[]
							{ 
								ChartScaleVerieties.Linear, 
							};
							ScaleVeriety = ScaleVerieties[0];
						}
					}
					else if (stop > 0 && AllowSwapLinLogScale)
					{
						ScaleVerieties = new ChartScaleVerieties[]
						{ 
							ChartScaleVerieties.Linear, 
							ChartScaleVerieties.Logarithmic 
						};
					}
					Notify("Start");
				}
			}
		}
		public override object GetStart() { return Start; }

		double stop;
		public double Stop
		{
			get { return stop; }
			set
			{
				if (stop != value)
				{
					stop = value;
					if (stop <= 0)
					{
						if (ScaleVerieties.Length == 2)
						{
							ScaleVerieties = new ChartScaleVerieties[]
							{ 
								ChartScaleVerieties.Linear, 
							};
							ScaleVeriety = ScaleVerieties[0];
						}
					}
					else if (start > 0 && AllowSwapLinLogScale)
					{
						ScaleVerieties = new ChartScaleVerieties[]
						{ 
							ChartScaleVerieties.Linear, 
							ChartScaleVerieties.Logarithmic 
						};
					}
					Notify("Stop");
				}
			}
		}
		public override object GetStop() { return Stop; }

		bool allowSwapLinLogScale = true;
		/// <summary>
		/// Gets or sets a value indicating whether it's allowed to swap Linear/Logarithmic scales.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if allowed; otherwise, <c>false</c>.
		/// </value>
		public bool AllowSwapLinLogScale
		{
			get { return allowSwapLinLogScale; }
			set
			{
				if (allowSwapLinLogScale != value)
				{
					allowSwapLinLogScale = value;
					if (allowSwapLinLogScale && start > 0 && stop > 0)
					{
						if (ScaleVerieties.Length == 1)
						{
							ScaleVerieties = new ChartScaleVerieties[]
							{ 
								ChartScaleVerieties.Linear, 
								ChartScaleVerieties.Logarithmic 
							};
						}
					}
					else
					{
						if (ScaleVerieties.Length == 2)
						{
							ScaleVerieties = new ChartScaleVerieties[]
							{ 
								ChartScaleVerieties.Linear, 
							};
							ScaleVeriety = ScaleVerieties[0];
						}
					}
					Notify("AllowChangeInterpolator");
				}
			}
		}

		protected override string GetDataErrorInfo(string columnName)
		{
			switch(columnName)
			{
				case "Start":
				case "Stop":
					if (Start == Stop)
						return "Start and Stop must not be equal";
					if (ScaleVeriety == ChartScaleVerieties.Logarithmic)
					{
						if (Start <= 0)
							return "Start must be positive";
						if (Stop <= 0)
							return "Stop must be positive";
					}
					break;
			}
			return base.GetDataErrorInfo(columnName);
		}
	}
}
