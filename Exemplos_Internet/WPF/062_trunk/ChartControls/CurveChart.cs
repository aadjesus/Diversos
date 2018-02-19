// <copyright file="CurveChart.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. CurveChart control.
// Default CurveChart style contains two coordinate grids and two axes.
// </summary>
// <revision>$Id: CurveChart.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Specialized; // For NotifyCollectionChangedEventArgs
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Controls
{
	/// <summary>
	/// CurveChart control.
	/// </summary>
	/// <remarks>
	/// Default CurveChart style contains two coordinate grids and two axes.
	/// </remarks>
	[TemplatePart(Name = "PART_ItemsHost", Type = typeof(FrameworkElement))]
	[TemplatePart(Name = "PART_HorizontalAxisHost", Type = typeof(DependencyObject))]
	[TemplatePart(Name = "PART_VerticalAxisHost", Type = typeof(DependencyObject))]
	public class CurveChart : ListBox
	{
		/// <summary>
		/// Initializes the <see cref="CurveChart"/> class.
		/// </summary>
		static CurveChart()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CurveChart)
				, new FrameworkPropertyMetadata(typeof(CurveChart)));
		}

		#region Named Parts fields
		/// <summary>
		/// Items host.
		/// </summary>
		FrameworkElement itemsHost;
		/// <summary>
		/// Horizontal and Vertical Axes hosts.
		/// </summary>
		DependencyObject hAxisHost, vAxisHost;
		#endregion Named Parts fields

		/// <summary>
		/// When overridden in a derived class, is invoked whenever application code or 
		/// internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			// Find Items Host.
			itemsHost = Template.FindName("PART_ItemsHost", this) as FrameworkElement;
			// Find Axes Hosts.
			hAxisHost = Template.FindName("PART_HorizontalAxisHost", this) as DependencyObject;
			vAxisHost = Template.FindName("PART_VerticalAxisHost", this) as DependencyObject;
		}

		#region Dependency Properties
		#region HorizontalScale
		/// <summary>
		/// Identifies the <see cref="HorizontalScale"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalScaleProperty
			= DependencyProperty.Register("HorizontalScale", typeof(ChartScale), typeof(CurveChart)
				, new FrameworkPropertyMetadata(null
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
					, HorizontalScalePropertyChanged
					)
				, HorizontalScaleValidate);
		/// <summary>
		/// Gets or sets the <see cref="HorizontalScale"/> property.
		/// </summary>
		/// <value><see cref="OpenWPFChart.Parts.ChartScale"/> value</value>
		public ChartScale HorizontalScale
		{
			get { return (ChartScale)GetValue(HorizontalScaleProperty); }
			set { SetValue(HorizontalScaleProperty, value); }
		}
		/// <summary>
		/// Validates the suggested value.
		/// </summary>
		/// <remarks>
		/// HorizontalScale may be either null or a consistent <see cref="T:OpenWPFChart.ChartScale"/> object.
		/// </remarks>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		static bool HorizontalScaleValidate(object value)
		{
			if (value == null)
				return true;
			ChartScale scale = value as ChartScale;
			Debug.Assert(scale != null, "scale != null");
			return scale.IsConsistent;
		}
		/// <summary>
		/// HorizontalScale property changed event handler.
		/// </summary>
		/// <remarks>
		/// Applies new HorizontalScale to all Chart Items.
		/// </remarks>
		/// <param name="d">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> 
		/// instance containing the event data.</param>
		static void HorizontalScalePropertyChanged(DependencyObject d,	DependencyPropertyChangedEventArgs e)
		{
			CurveChart ths = d as CurveChart;
			if (ths == null)
				return;

			// Apply new HorizontalScale to all Items.
			foreach (var obj in ths.Items)
			{
				ItemDataView itemDataView = castToItemDataView(obj);
				if (itemDataView != null)
				{
					try
					{
						itemDataView.HorizontalScale = ths.HorizontalScale;
					}
					catch (ArgumentException)
					{
						itemDataView.HorizontalScale = null;
					}
				}
			}
		}
		#endregion HorizontalScale

		#region VerticalScale
		/// <summary>
		/// Identifies the VerticalScale dependency property.
		/// </summary>
		public static readonly DependencyProperty VerticalScaleProperty
			= DependencyProperty.Register("VerticalScale", typeof(ChartScale), typeof(CurveChart)
				, new FrameworkPropertyMetadata(null
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
					, VerticalScalePropertyChanged
					)
				, VerticalScaleValidate);
		/// <summary>
		/// Gets or sets the VerticalScale property.
		/// </summary>
		/// <value>Finite positive value</value>
		public ChartScale VerticalScale
		{
			get { return (ChartScale)GetValue(VerticalScaleProperty); }
			set { SetValue(VerticalScaleProperty, value); }
		}
		/// <summary>
		/// Validates the suggested value.
		/// </summary>
		/// <remarks>
		/// VerticalScale may be either null or a consistent <see cref="T:OpenWPFChart.ChartScale"/> object.
		/// </remarks>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		static bool VerticalScaleValidate(object value)
		{
			if (value == null)
				return true;
			ChartScale scale = value as ChartScale;
			Debug.Assert(scale != null, "scale != null");
			return scale.IsConsistent;
		}
		/// <summary>
		/// VerticalScale property changed event handler.
		/// </summary>
		/// <remarks>
		/// Applies new VerticalScale to all Chart Items.
		/// </remarks>
		/// <param name="d">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> 
		/// instance containing the event data.</param>
		static void VerticalScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			CurveChart ths = d as CurveChart;
			if (ths == null)
				return;

			// Apply new VerticalScale to all Items.
			foreach (var obj in ths.Items)
			{
				ItemDataView itemDataView = castToItemDataView(obj);
				if (itemDataView != null)
				{
					try
					{
						itemDataView.VerticalScale = ths.VerticalScale;
					}
					catch (ArgumentException)
					{
						itemDataView.VerticalScale = null;
					}
				}
			}
		}
		#endregion VerticalScale

		#region Grids
		#region HorizontalGridVisibility
		/// <summary>
		/// Identifies the HorizontalGridVisibility dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalGridVisibilityProperty
			= DependencyProperty.Register("HorizontalGridVisibility", typeof(GridVisibility), typeof(CurveChart)
				, new FrameworkPropertyMetadata(GridVisibility.AllTicks
					, FrameworkPropertyMetadataOptions.AffectsRender)
				, GridVisibility_Validate);
		/// <summary>
		/// Gets or sets the HorizontalGridVisibility property.
		/// </summary>
		/// <value><see cref="GridVisibility"/> value</value>
		public GridVisibility HorizontalGridVisibility
		{
			get { return (GridVisibility)GetValue(HorizontalGridVisibilityProperty); }
			set { SetValue(HorizontalGridVisibilityProperty, value); }
		}
		#endregion HorizontalGridVisibility

		#region VerticalGridVisibility
		/// <summary>
		/// Identifies the VerticalGridVisibility dependency property.
		/// </summary>
		public static readonly DependencyProperty VerticalGridVisibilityProperty
			= DependencyProperty.Register("VerticalGridVisibility", typeof(GridVisibility), typeof(CurveChart)
				, new FrameworkPropertyMetadata(GridVisibility.AllTicks
					, FrameworkPropertyMetadataOptions.AffectsRender)
				, GridVisibility_Validate);
		/// <summary>
		/// Gets or sets the VerticalGridVisibility property.
		/// </summary>
		/// <value><see cref="GridVisibility"/> value</value>
		public GridVisibility VerticalGridVisibility
		{
			get { return (GridVisibility)GetValue(VerticalGridVisibilityProperty); }
			set { SetValue(VerticalGridVisibilityProperty, value); }
		}
		#endregion VerticalGridVisibility

		/// <summary>
		/// Validates suggested GridVisibility property value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static bool GridVisibility_Validate(object value)
		{
			GridVisibility x = (GridVisibility)value;
			return (x == GridVisibility.AllTicks || x == GridVisibility.LongTicks || x == GridVisibility.Hidden);
		}
		#endregion Grids

		#region Axes
		#region HorizontalAxisTitle
		/// <summary>
		/// Identifies the HorizontalAxisTitle dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalAxisTitleProperty
			= DependencyProperty.Register("HorizontalAxisTitle", typeof(string), typeof(CurveChart)
				, new FrameworkPropertyMetadata(null
						, FrameworkPropertyMetadataOptions.AffectsMeasure
							| FrameworkPropertyMetadataOptions.AffectsRender
					)
				);
		/// <summary>
		/// Gets or sets the HorizontalAxisTitle property.
		/// </summary>
		/// <value>Horizontal Axis Title</value>
		public string HorizontalAxisTitle
		{
			get { return (string)GetValue(HorizontalAxisTitleProperty); }
			set { SetValue(HorizontalAxisTitleProperty, value); }
		}
		#endregion HorizontalAxisTitle

		#region HorizontalAxisLabelFormat
		/// <summary>
		/// Identifies the HorizontalAxisLabelFormat dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalAxisLabelFormatProperty
			= DependencyProperty.Register("HorizontalAxisLabelFormat", typeof(string), typeof(CurveChart)
				, new FrameworkPropertyMetadata("G"
						, FrameworkPropertyMetadataOptions.AffectsMeasure
							| FrameworkPropertyMetadataOptions.AffectsRender
					, HorizontalAxisLabelFormatPropertyChanged)
				);
		/// <summary>
		/// Gets or sets the HorizontalAxisLabelFormat property.
		/// </summary>
		/// <value>HorizontalAxisLabelFormatProperty</value>
		public string HorizontalAxisLabelFormat
		{
			get { return (string)GetValue(HorizontalAxisLabelFormatProperty); }
			set { SetValue(HorizontalAxisLabelFormatProperty, value); }
		}
		/// <summary>
		/// HorizontalAxisLabelFormat property changed event handler.
		/// </summary>
		/// <remarks>
		/// Applies new LabelFormat to the HorizontalAxis.
		/// </remarks>
		/// <param name="d">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> 
		/// instance containing the event data.</param>
		static void HorizontalAxisLabelFormatPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			CurveChart ths = d as CurveChart;
			if (ths == null)
				return;

			// Apply new LabelFormat to the HorizontalAxis.
			if (ths.hAxisHost == null)
			{
				Axis axis = findAxis(ths.hAxisHost);
				if (axis != null)
					axis.LabelFormat = e.NewValue as string;
			}
		}
		#endregion HorizontalAxisLabelFormat

		#region VerticalAxisTitle
		/// <summary>
		/// Identifies the VerticalAxisTitle dependency property.
		/// </summary>
		public static readonly DependencyProperty VerticalAxisTitleProperty
			= DependencyProperty.Register("VerticalAxisTitle", typeof(string), typeof(CurveChart)
				, new FrameworkPropertyMetadata(null
						, FrameworkPropertyMetadataOptions.AffectsMeasure
							| FrameworkPropertyMetadataOptions.AffectsRender
							, null, CoerceVerticalAxisTitle
					)
				);
		/// <summary>
		/// Gets or sets the VerticalAxisTitle property.
		/// </summary>
		/// <value>Vertical Axis Title</value>
		public string VerticalAxisTitle
		{
			get { return (string)GetValue(VerticalAxisTitleProperty); }
			set { SetValue(VerticalAxisTitleProperty, value); }
		}
		/// <summary>
		/// Coerces the VerticalAxisTitle property value.
		/// Returns either the current selected item name or local VerticalAxisTitle property value.
		/// </summary>
		/// <param name="d">The sender.</param>
		/// <param name="baseValue">The base value.</param>
		/// <returns></returns>
		private static object CoerceVerticalAxisTitle(DependencyObject d, object baseValue)
		{
			CurveChart ths = d as CurveChart;
			if (ths == null)
				return DependencyProperty.UnsetValue;

			ItemDataView itemDataView = ths.SelectedItem as ItemDataView;
			if (itemDataView == null)
			{
				Item item = ths.SelectedItem as Item;
				if (item != null)
					itemDataView = item.ItemDataView;
			}

			if (itemDataView != null)
				return itemDataView.ItemData.ItemName;
			else
			{
				object localValue = ths.ReadLocalValue(VerticalAxisTitleProperty);
				if (localValue != DependencyProperty.UnsetValue)
					return localValue;
				else
					return baseValue;
			}
		}
		#endregion VerticalAxisTitle

		#region VerticalAxisLabelFormat
		/// <summary>
		/// Identifies the VerticalAxisLabelFormat dependency property.
		/// </summary>
		public static readonly DependencyProperty VerticalAxisLabelFormatProperty
			= DependencyProperty.Register("VerticalAxisLabelFormat", typeof(string), typeof(CurveChart)
				, new FrameworkPropertyMetadata("G"
						, FrameworkPropertyMetadataOptions.AffectsMeasure
							| FrameworkPropertyMetadataOptions.AffectsRender
					, VerticalAxisLabelFormatPropertyChanged)
				);
		/// <summary>
		/// Gets or sets the VerticalAxisLabelFormat property.
		/// </summary>
		/// <value>VerticalAxisLabelFormatProperty</value>
		public string VerticalAxisLabelFormat
		{
			get { return (string)GetValue(VerticalAxisLabelFormatProperty); }
			set { SetValue(VerticalAxisLabelFormatProperty, value); }
		}
		/// <summary>
		/// VerticalAxisLabelFormat property changed event handler.
		/// </summary>
		/// <remarks>
		/// Applies new LabelFormat to the VerticalAxis.
		/// </remarks>
		/// <param name="d">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> 
		/// instance containing the event data.</param>
		static void VerticalAxisLabelFormatPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			CurveChart ths = d as CurveChart;
			if (ths == null)
				return;

			// Apply new LabelFormat to the VerticalAxis.
			if (ths.vAxisHost == null)
			{
				Axis axis = findAxis(ths.vAxisHost);
				if (axis != null)
					axis.LabelFormat = e.NewValue as string;
			}
		}
		#endregion VerticalAxisLabelFormat
		#endregion Axes
		#endregion Dependency Properties

		#region CLR Properties
		#region Axes
		/// <summary>
		/// Gets the Horizontal axis.
		/// </summary>
		public Axis HorizontalAxis
		{
			get { return hAxisHost == null ? null : findAxis(hAxisHost); }
		}

		/// <summary>
		/// Gets the Vertical axis.
		/// </summary>
		public Axis VerticalAxis
		{
			get { return vAxisHost == null ? null : findAxis(vAxisHost); }
		}
		#endregion Axes

		/// <summary>
		/// Gets the actual render size of the Chart Area.
		/// </summary>
		/// <remarks>
		/// This is the RenderSize of the control PART_ItemsHost part.
		/// </remarks>
		public Size ChartAreaRenderSize
		{
			get { return itemsHost == null ? Size.Empty : itemsHost.RenderSize; }
		}
		#endregion CLR Properties

		/// <summary>
		/// Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.Items"/> property changes.
		/// </summary>
		/// <param name="e">The event data.</param>
		/// <remarks>
		/// Adds/removes <see cref="T:OpenWPFChart.Parts.ItemDataView"/> PropertyChanged event handlers.
		/// </remarks>
		protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
		{
			base.OnItemsChanged(e);

			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (object obj in e.NewItems)
					{
						ItemDataView itemDataView = castToItemDataView(obj);
						if (itemDataView != null)
						{
							setCommonChartScales(itemDataView);

							itemDataView.PropertyChanged += item_PropertyChanged;
						}
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (object obj in e.OldItems)
					{
						ItemDataView itemDataView = castToItemDataView(obj);
						if (itemDataView != null)
							itemDataView.PropertyChanged -= item_PropertyChanged;
					}
					break;
				case NotifyCollectionChangedAction.Replace:
					foreach (object obj in e.OldItems)
					{
						ItemDataView itemDataView = castToItemDataView(obj);
						if (itemDataView != null)
							itemDataView.PropertyChanged -= item_PropertyChanged;
					}
					foreach (object obj in e.NewItems)
					{
						ItemDataView itemDataView = castToItemDataView(obj);
						if (itemDataView != null)
						{
							setCommonChartScales(itemDataView);

							itemDataView.PropertyChanged += item_PropertyChanged;
						}
					}
					break;
				case NotifyCollectionChangedAction.Reset:
					foreach (object obj in Items)
					{
						ItemDataView itemDataView = castToItemDataView(obj);
						if (itemDataView != null)
						{
							setCommonChartScales(itemDataView);

							itemDataView.PropertyChanged += item_PropertyChanged;
						}
					}
					break;
			}

			if (Items.Count == 0)
			{
				HorizontalScale = null;
				VerticalScale = null;
			}
		}

		/// <summary>
		/// Sets Common ChartScales to the Chart control and its Items.
		/// </summary>
		/// <remarks>
		/// When the control Items count is or becomes equal to zero the control scales are set 
		/// to <c>null</c> by the <see cref="M:OpenWPFChart.Chart.OnItemsChanged"/> method override.
		/// <para>If the control scale has null value it's to the corresponding 
		/// <paramref name="itemDataView"/> scale value.</para>
		/// <para>If the control scale value isn't null then the corresponding 
		/// <paramref name="itemDataView"/> scale is set to that value.</para>
		/// </remarks>
		/// <param name="itemDataView">The item data view.</param>
		void setCommonChartScales(ItemDataView itemDataView)
		{
			Debug.Assert(itemDataView != null, "itemDataView != null");
			
			if (HorizontalScale == null)
				HorizontalScale = itemDataView.HorizontalScale;
			else
				itemDataView.HorizontalScale = HorizontalScale;

			if (VerticalScale == null)
				VerticalScale = itemDataView.VerticalScale;
			else
				itemDataView.VerticalScale = VerticalScale;
		}

		/// <summary>
		/// Casts the <paramref name="obj"/> to <see cref="T:OpenWPFChart.ItemDataView"/>.
		/// </summary>
		/// <param name="obj">The object to cast.</param>
		/// <returns>ItemDataView object or null.</returns>
		static ItemDataView castToItemDataView(object obj)
		{
			ItemDataView itemDataView = obj as ItemDataView;
			if (itemDataView == null)
			{
				Item item = obj as Item;
				if (item != null)
					itemDataView = item.ItemDataView;
			}
			return itemDataView;
		}

		/// <summary>
		/// Finds an axis in its container Visual Tree.
		/// </summary>
		/// <param name="host">Axis host.</param>
		/// <returns>The axis object or null</returns>
		static Axis findAxis(DependencyObject host)
		{
			Axis axis = host as Axis;
			if (axis != null)
				return axis;
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(host); ++i)
			{
				DependencyObject item = VisualTreeHelper.GetChild(host, i);
				axis = findAxis(item);
				if (axis != null)
					return axis;
			}
			return null;
		}

		/// <summary>
		/// Handles the PropertyChanged event of the item control.
		/// </summary>
		/// <remarks>
		/// This handler is responsible for coordination this control
		/// <see cref="P:OpenWPFChart.Chart.HorizontalScale"/> and <see cref="P:OpenWPFChart.Chart.VerticalScale"/>
		/// properties with hosted items <see cref="P:OpenWPFChart.ItemDataView.HorizontalScale"/> and 
		/// <see cref="P:OpenWPFChart.ItemDataView.VerticalScale"/> properties.
		/// <para>For this control ChartScale's of the control and ChartScale's of the hosted items must
		/// be the same. So if the ChartScale of the hosted item is changed by third-partie code
		/// this change should be discarded.</para>
		/// <para>Also this handler resets the ItemTemplateSelector when 
		/// <see cref="P:OpenWPFChart.ItemDataView.VisualCue"/> property changes.</para>
		/// </remarks>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			ItemDataView itemDataView = castToItemDataView(sender);
			if (itemDataView == null)
				return;

			if (e.PropertyName.StartsWith("HorizontalScale"))
			{
				Debug.Assert(HorizontalScale != null, "HorizontalScale != null");
				if (!object.ReferenceEquals(itemDataView.HorizontalScale, HorizontalScale))
					itemDataView.HorizontalScale = HorizontalScale;
			}
			else if (e.PropertyName.StartsWith("VerticalScale"))
			{
				Debug.Assert(VerticalScale != null, "VerticalScale != null");
				if (!object.ReferenceEquals(itemDataView.VerticalScale, VerticalScale))
					itemDataView.VerticalScale = VerticalScale;
			}

			if (e.PropertyName == "VisualCue")
			{ // Reset the ItemTemplateSelector.
				DataTemplateSelector old = ItemTemplateSelector;
				ItemTemplateSelector = null;
				ItemTemplateSelector = old;
			}
		}

		/// <summary>
		/// Responds to a list box selection change by raising a 
		/// <see cref="E:System.Windows.Controls.Primitives.Selector.SelectionChanged"/> event.
		/// </summary>
		/// <remarks>
		/// Coerces VerticalAxisTitleProperty to force it show the current item Name.
		/// </remarks>
		/// <param name="e">Provides data for <see cref="T:System.Windows.Controls.SelectionChangedEventArgs"/>.</param>
		protected override void OnSelectionChanged(SelectionChangedEventArgs e)
		{
			base.OnSelectionChanged(e);
			CoerceValue(VerticalAxisTitleProperty);
		}
	}
}
