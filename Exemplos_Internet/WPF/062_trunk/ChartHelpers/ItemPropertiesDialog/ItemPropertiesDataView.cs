// <copyright file="ItemPropertiesDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-02-20</date>
// <summary>OpenWPFChart library. ItemPropertiesDialog dialog Data View - private data cache.</summary>
// <revision>$Id: ItemPropertiesDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.ObjectModel;
using System.ComponentModel; // For INotifyPropertyChanged
using System.Windows.Input;
using System.Windows.Media;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// ItemPropertiesDialog dialog Data View - private data cache.
	/// </summary>
	class ItemPropertiesDataView : INotifyPropertyChanged
	{
		#region constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="ItemPropertiesDataView"/> class.
		/// </summary>
		/// <param name="itemDataView">OpenWPFChart Item data view.</param>
		/// <param name="templateSelectorItems">Generic Data Template Selector Items.</param>
		public ItemPropertiesDataView(ItemDataView itemDataView
			, Collection<GenericDataTemplateSelectorItem> templateSelectorItems)
		{
			itemName = itemDataView.ItemData.ItemName;

			// Scales.
			HorizontalScale = itemDataView.HorizontalScale;
			HorizontalScale.PropertyChanged += HorizontalScale_PropertyChanged;
			VerticalScale = itemDataView.VerticalScale;
			VerticalScale.PropertyChanged += VerticalScale_PropertyChanged;

			// Decorations.
			CurveDataView curveDataView = itemDataView as CurveDataView;
			if (curveDataView != null)
			{
				SolidColorBrush solidColorBrush = curveDataView.Pen.Brush as SolidColorBrush;
				if (solidColorBrush != null)
					this.curveColor = solidColorBrush.Color;

				IPointMarker iPointMarker = curveDataView as IPointMarker;
				if (iPointMarker != null)
					pointMarkerVisible = iPointMarker.PointMarkerVisible;
			}

			// Make VisualCue data collection.
			Collection<VisualCueItem> visualCues = new Collection<VisualCueItem>();
			if (templateSelectorItems != null)
			{
				foreach (GenericDataTemplateSelectorItem selectorItem in templateSelectorItems)
				{
					if (selectorItem.TemplatedType != null
							&& !isDerived(itemDataView.GetType(), selectorItem.TemplatedType))
						continue;
					VisualCueItem visualCueItem = new VisualCueItem() 
					{ 
						Value = selectorItem.Value, 
						Description = selectorItem.Description 
					};
					if (selectorItem.Value == itemDataView.VisualCue)
						this.visualCueItem = visualCueItem;
					visualCues.Add(visualCueItem);
				}
			}
			VisualCues = visualCues;
		}

		/// <summary>
		/// Check that the itemType is the templatedType type or is derived from it.
		/// </summary>
		/// <param name="itemType">Type of the templated item.</param>
		/// <param name="templatedType">Acceptable Type of the templated item.</param>
		/// <returns></returns>
		private static bool isDerived(Type itemType, Type templatedType)
		{
			if (itemType == templatedType)
				return true;
			if (itemType.BaseType == null)
				return false;
			return isDerived(itemType.BaseType, templatedType);
		}
		#endregion constructor

		#region ItemName
		string itemName;
		public string ItemName 
		{
			get { return itemName; }
			set
			{
				if (itemName != value)
				{
					itemName = value;
					Notify("ItemName");
				}
			}
		}
		#endregion ItemName

		#region HorizontalScale
		public ChartScale HorizontalScale
		{
			get;
			private set;
		}

		void HorizontalScale_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			Notify("HorizontalScale");
		}
		#endregion HorizontalScale

		#region VerticalScale
		public ChartScale VerticalScale
		{
			get;
			private set;
		}

		void VerticalScale_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			Notify("VerticalScale");
		}
		#endregion VerticalScale

		#region CurveColor
		Color curveColor = Colors.Black;
		public Color CurveColor
		{
			get { return curveColor; }
			set
			{
				if (curveColor != value)
				{
					curveColor = value;
					Notify("CurveColor");
				}
			}
		}

		#region SelectColorCommand
		/// <summary>
		/// Command to Select Color.
		/// </summary>
		class SelectColorCommandClass : ICommand
		{
			public delegate void ExecuteDelegate(object parameter);

			ExecuteDelegate execute;

			public SelectColorCommandClass(ExecuteDelegate executeDelegate)
			{
				execute = executeDelegate;
			}

			public void Execute(object parameter)
			{
				execute(parameter);
			}

			public bool CanExecute(object parameter)
			{
				return true;
			}

			event EventHandler ICommand.CanExecuteChanged
			{
				// By Josh Smih:
				// I intentionally left these empty because
				// this command never raises the event, and
				// not using the WeakEvent pattern here can
				// cause memory leaks. WeakEvent pattern is
				// not simple to implement, so why bother.
				add { }
				remove { }
			}
		}

		ICommand selectColorCommand;
		/// <summary>
		/// Gets the SelectColorCommand.
		/// </summary>
		/// <value/>
		public ICommand SelectColorCommand
		{
			get
			{
				if (selectColorCommand == null)
				{
					selectColorCommand = new SelectColorCommandClass(selectColor);
				}
				return selectColorCommand;
			}
		}
		/// <summary>
		/// Selects the color with System.Windows.Forms.ColorDialog.
		/// </summary>
		private void selectColor(object parameter)
		{
			Color color = CurveColor;
			System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
			dlg.Color = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
			if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				return;
			CurveColor = Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B);
		}
		#endregion SelectColorCommand
		#endregion CurveColor

		#region PointMarkerVisible
		bool pointMarkerVisible = true;
		/// <summary>
		/// Gets or sets the PointMarkerVisible property.
		/// </summary>
		/// <value>true to draw PointMarkers.</value>
		public bool PointMarkerVisible
		{
			get { return pointMarkerVisible; }
			set
			{
				if (pointMarkerVisible != value)
				{
					pointMarkerVisible = value;
					Notify("PointMarkerVisible");
				}
			}
		}
		#endregion PointMarkerVisible

		#region VisualCueItem
		/// <summary>
		/// Gets or sets the VisualCues collection.
		/// </summary>
		/// <value>The visual cues.</value>
		public Collection<VisualCueItem> VisualCues { get; private set; }

		VisualCueItem visualCueItem;
		public VisualCueItem VisualCueItem
		{
			get { return visualCueItem; }
			set
			{
				if (!visualCueItem.Equals(value))
				{
					visualCueItem = value;
					Notify("VisualCueItem");
				}
			}
		}
		#endregion VisualCueItem

		#region Visibility Control Properties
		#region Horizontal Scale
		bool showHorizontalScaleProperties = true;
		/// <summary>
		/// Gets or sets a value indicating whether to show horizontal CharScale properties tab.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if show horizontal scale properties; otherwise, <c>false</c>.
		/// </value>
		public bool ShowHorizontalScaleProperties
		{
			get { return showHorizontalScaleProperties; }
			set
			{
				if (showHorizontalScaleProperties != value)
				{
					showHorizontalScaleProperties = value;
					Notify("ShowHorizontalScaleProperties");
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether it's allowed to swap Horizontal Linear/Logarithmic scales.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if allowed; otherwise, <c>false</c>.
		/// </value>
		public bool AllowSwapHorizontalLinLogScale
		{
			get 
			{
				//ChartNumericScaleDataView hScaleDataView = HorizontalScaleDataView as ChartNumericScaleDataView;
				//if (hScaleDataView != null)
				//    return hScaleDataView.AllowSwapLinLogScale;
				return false; 
			}
			set
			{
				//ChartNumericScaleDataView hScaleDataView = HorizontalScaleDataView as ChartNumericScaleDataView;
				//if (hScaleDataView != null)
				//    hScaleDataView.AllowSwapLinLogScale = value;
			}
		}
		#endregion Horizontal Scale

		#region Vertical Scale
		public bool showVerticalScaleProperties = true;
		/// <summary>
		/// Gets or sets a value indicating whether to show vertical CharScale properties tab.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if show horizontal scale properties; otherwise, <c>false</c>.
		/// </value>
		public bool ShowVerticalScaleProperties
		{
			get { return showVerticalScaleProperties; }
			set
			{
				if (showVerticalScaleProperties != value)
				{
					showVerticalScaleProperties = value;
					Notify("ShowVerticalScaleProperties");
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether it's allowed to swap Vetrical Linear/Logarithmic scales.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if allowed; otherwise, <c>false</c>.
		/// </value>
		public bool AllowSwapVetricalLinLogScale
		{
			get
			{
				//ChartNumericScaleDataView vScaleDataView = VerticalScaleDataView as ChartNumericScaleDataView;
				//if (vScaleDataView != null)
				//    return vScaleDataView.AllowSwapLinLogScale;
				return false;
			}
			set
			{
				//ChartNumericScaleDataView vScaleDataView = VerticalScaleDataView as ChartNumericScaleDataView;
				//if (vScaleDataView != null)
				//    vScaleDataView.AllowSwapLinLogScale = value;
			}
		}
		#endregion Vertical Scale
		#endregion Visibility Control Properties

		#region INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;

		void Notify(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion INotifyPropertyChanged Members
	}
}
