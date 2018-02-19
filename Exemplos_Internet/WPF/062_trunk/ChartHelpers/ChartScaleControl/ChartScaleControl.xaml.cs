// <copyright file="ChartScaleControl.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-02-18</date>
// <summary>OpenWPFChart library. ChartScaleControl to edit ChartScale's.</summary>
// <revision>$Id: ChartScaleControl.xaml.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// Interaction logic for ChartScaleControl.xaml
	/// </summary>
	public partial class ChartScaleControl : UserControl, INotifyPropertyChanged
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ChartScaleControl"/> class.
		/// </summary>
		public ChartScaleControl()
		{
			InitializeComponent();
		}

		#region ScaleDataView
		ChartScaleDataView chartScaleDataView;
		/// <summary>
		/// Gets or sets the ScaleDataView.
		/// </summary>
		/// <value>The scale data view.</value>
		public ChartScaleDataView ScaleDataView
		{
			get { return chartScaleDataView; }
			private set
			{
				if (chartScaleDataView != value)
				{
					chartScaleDataView = value;
					Notify("ScaleDataView");
				}
			}
		}
		#endregion ScaleDataView

		#region ChartScale
		/// <summary>
		/// Identifies the ChartScale dependency property.
		/// </summary>
		public static readonly DependencyProperty ChartScaleProperty
			= DependencyProperty.Register("ChartScale", typeof(ChartScale), typeof(ChartScaleControl)
				, new FrameworkPropertyMetadata(null
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
					, ScalePropertyChanged
					)
				);
		/// <summary>
		/// Gets or sets the ChartScale property.
		/// </summary>
		/// <value>ChartScaleProperty</value>
		public ChartScale ChartScale
		{
			get { return (ChartScale)GetValue(ChartScaleProperty); }
			set { SetValue(ChartScaleProperty, value); }
		}
		/// <summary>
		/// ChartScale property changed event handler.
		/// </summary>
		/// <remarks>
		/// Finds new ChartScaleDataView and sets it as the DataContext.
		/// </remarks>
		/// <param name="d">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> 
		/// instance containing the event data.</param>
		static void ScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ChartScaleControl ths = d as ChartScaleControl;
			if (ths == null)
				return;

			if (ths.ScaleDataView != null)
				ths.ScaleDataView.PropertyChanged -= ths.ScaleDataViewPropertyChanged;

			ths.ScaleDataView = ChartScaleControl.GetScaleDataView(e.NewValue as ChartScale);
			
			if (ths.ScaleDataView != null)
				ths.ScaleDataView.PropertyChanged += ths.ScaleDataViewPropertyChanged;
		}
		#endregion VerticalScale

		/// <summary>
		/// ScaleDataView property changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		void ScaleDataViewPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			Notify(e.PropertyName);
		}

		public object Start
		{
			get { return ScaleDataView.GetStart(); }
		}

		public object Stop
		{
			get { return ScaleDataView.GetStop(); }
		}

		public double Scale
		{
			get { return ScaleDataView.Scale; }
		}

		public ChartScaleVerieties Veriety
		{
			get { return ScaleDataView.GetVeriety(); }
		}

		/// <summary>
		/// Selects the ChartScaleDataView appropriate for concrete ChartScale.
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <returns></returns>
		static ChartScaleDataView GetScaleDataView(ChartScale scale)
		{
			if (scale is ChartLinearScale)
			{
				return new ChartNumericScaleDataView(scale as ChartLinearScale);
			}
			else if (scale is ChartLogarithmicScale)
			{
				return new ChartNumericScaleDataView(scale as ChartLogarithmicScale);
			}
			else if (scale is ChartDateTimeScale)
			{
				return new ChartDateTimeScaleDataView(scale as ChartDateTimeScale);
			}
			else if (scale is ChartSeriesScale)
			{
				return new ChartSeriesScaleDataView(scale as ChartSeriesScale);
			}
			return null;
		}

		#region INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;
		protected void Notify(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion INotifyPropertyChanged Members
	}
}
