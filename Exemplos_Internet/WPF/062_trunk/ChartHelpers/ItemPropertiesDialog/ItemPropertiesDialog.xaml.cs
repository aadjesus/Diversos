// <copyright file="ItemPropertiesDialog.xaml.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-02-18</date>
// <summary>OpenWPFChart library. Dialog to view/edit Chart Item Properties.</summary>
// <revision>$Id: ItemPropertiesDialog.xaml.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Collections.ObjectModel;
using System.ComponentModel; // For PropertyChangedEventArgs
using System.Windows;
using System.Windows.Media;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// ChartScale type Verieties.
	/// </summary>
	public enum ChartScaleVerieties
	{
		/// <summary>
		/// ChartLinearScale type
		/// </summary>
		Linear,
		/// <summary>
		/// ChartLogarithmicScale type
		/// </summary>
		Logarithmic,
		/// <summary>
		/// ChartDateTimeScale type
		/// </summary>
		DateTime,
		/// <summary>
		/// ChartSeriesScale type
		/// </summary>
		Series
	}

	/// <summary>
	/// Provies the VisualCue value with the description.
	/// </summary>
	struct VisualCueItem
	{
		/// <summary>
		/// Gets or sets the value of the property that triggers the DataTemplate association.
		/// </summary>
		/// <value>The value.</value>
		public object Value { get; set; }
		/// <summary>
		/// Gets or sets the user-readable description.
		/// It could be used in UI to allow the user to select one or other template.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }
	}

	/// <summary>
	/// Dialog to view/edit Chart Item Properties.
	/// Interaction logic for ItemPropertiesDialog.xaml
	/// </summary>
	public partial class ItemPropertiesDialog : Window
	{
		// Dialog DataView.
		ItemPropertiesDataView dataView;

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemPropertiesDialog"/> class.
		/// </summary>
		/// <param name="itemDataView">ItemDataView.</param>
		/// <param name="templateSelectorItems">The template selector items.</param>
		public ItemPropertiesDialog(ItemDataView itemDataView
			, Collection<GenericDataTemplateSelectorItem> templateSelectorItems)
		{
			InitializeComponent();

			dataView = new ItemPropertiesDataView(itemDataView, templateSelectorItems);
			dataView.PropertyChanged += dataView_PropertyChanged;
			DataContext = dataView;

			hScale.ChartScale = itemDataView.HorizontalScale;
			hScale.PropertyChanged += dataView_PropertyChanged;
			vScale.ChartScale = itemDataView.VerticalScale;
			vScale.PropertyChanged += dataView_PropertyChanged;
		}

		/// <summary>
		/// Enable OK button.
		/// Handles the PropertyChanged event of the data control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void dataView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			btnOK.IsEnabled = true;
		}

		/// <summary>
		/// Handles the Click event of the btnOK control.
		/// Applyes changes and closes.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		#region Properties delegators to this dialog DataView
		/// <summary>
		/// Gets the name of the Chart Item.
		/// </summary>
		/// <value>The name of the Chart Item.</value>
		public string ItemName
		{
			get { return dataView.ItemName; }
		}

		#region HorizontalScale
		public ChartScaleVerieties HorizontalScaleVeriety
		{
			get { return hScale.Veriety; }
		}

		public object HorizontalScaleStart
		{
			get { return hScale.Start; }
		}

		public object HorizontalScaleStop
		{
			get { return hScale.Stop; }
		}

		public double HorizontalScaleScale
		{
			get { return hScale.Scale; }
		}
		#endregion HorizontalScale

		#region VerticalScale
		public ChartScaleVerieties VerticalScaleVeriety
		{
			get { return vScale.Veriety; }
		}

		public object VerticalScaleStart
		{
			get { return vScale.Start; }
		}

		public object VerticalScaleStop
		{
			get { return vScale.Stop; }
		}

		public double VerticalScaleScale
		{
			get { return vScale.Scale; }
		}
		#endregion VerticalScale

		/// <summary>
		/// Gets or sets the PointMarkerVisible property.
		/// </summary>
		/// <value>true to draw PointMarkers.</value>
		public bool PointMarkerVisible
		{
			get { return dataView.PointMarkerVisible; }
			set { dataView.PointMarkerVisible = value; }
		}

		/// <summary>
		/// Gets the color of the curve.
		/// </summary>
		/// <value>The color of the curve.</value>
		public Color CurveColor
		{
			get { return dataView.CurveColor; }
		}

		/// <summary>
		/// Gets the visual cue.
		/// </summary>
		/// <value>The visual cue.</value>
		public object VisualCue
		{
			get { return dataView.VisualCueItem.Value; }
		}

		#region Horizontal Scale
		/// <summary>
		/// Gets or sets the horizontal scale tab header.
		/// </summary>
		/// <value>The horizontal scale tab header.</value>
		public object HorizontalScaleTabHeader
		{
			get { return hScaleTab.Header; }
			set { hScaleTab.Header = value; }
		}
		/// <summary>
		/// Gets or sets a value indicating whether to show horizontal CharScale properties tab.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if show horizontal scale properties; otherwise, <c>false</c>.
		/// </value>
		public bool ShowHorizontalScaleProperties
		{
			get { return dataView.ShowHorizontalScaleProperties; }
			set { dataView.ShowHorizontalScaleProperties = value; }
		}
		/// <summary>
		/// Gets or sets a value indicating whether it's allowed to swap Horizontal Linear/Logarithmic scales.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if allowed; otherwise, <c>false</c>.
		/// </value>
		public bool AllowSwapHorizontalLinLogScale
		{
			get { return dataView.AllowSwapHorizontalLinLogScale; }
			set { dataView.AllowSwapHorizontalLinLogScale = value; }
		}
		#endregion Horizontal Scale

		#region Vertical Scale
		/// <summary>
		/// Gets or sets the vertical scale tab header.
		/// </summary>
		/// <value>The vertical scale tab header.</value>
		public object VerticalScaleTabHeader
		{
			get { return vScaleTab.Header; }
			set { vScaleTab.Header = value; }
		}
		/// <summary>
		/// Gets or sets a value indicating whether to show Vertical CharScale properties tab.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if show horizontal scale properties; otherwise, <c>false</c>.
		/// </value>
		public bool ShowVerticalScaleProperties
		{
			get { return dataView.ShowVerticalScaleProperties; }
			set { dataView.ShowVerticalScaleProperties = value; }
		}
		/// <summary>
		/// Gets or sets a value indicating whether it's allowed to swap Vertical Linear/Logarithmic scales.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if allowed; otherwise, <c>false</c>.
		/// </value>
		public bool AllowSwapVetricalLinLogScale
		{
			get { return dataView.AllowSwapVetricalLinLogScale; }
			set { dataView.AllowSwapVetricalLinLogScale = value; }
		}
		#endregion Vertical Scale
		#endregion Properties delegators to this dialog DataView
	}
}
