// <copyright file="ItemDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-01-19</date>
// <summary>OpenWPFChart library. ItemDataView is the base class of all DataView classes.</summary>
// <revision>$Id: ItemDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.ComponentModel;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// ItemDataView is the base class of all DataView classes.
	/// </summary>
	public abstract class ItemDataView : INotifyPropertyChanged
	{
		#region ItemData
		ItemData itemData;
		/// <summary>
		/// Gets or sets the ItemData property.
		/// </summary>
		/// <value>ItemData value</value>
		public ItemData ItemData
		{
			get { return itemData; }
			set
			{
				if (itemData != value)
				{
					if (value != null)
					{
						if (HorizontalScale != null && !HorizontalScale.CompatibleWith(value.TypeOfAbscissa))
							throw new ArgumentException("New HorizontalScale value type doesn't match the one of data type");
						if (VerticalScale != null && !VerticalScale.CompatibleWith(value.TypeOfOrdinate))
							throw new ArgumentException("New VerticalScale value type doesn't match the one of data type");
					}

					itemData = value;
					NotifyPropertyChanged("ItemData");
				}
			}
		}
		#endregion ItemData

		#region HorizontalScale
		ChartScale horizontalScale;
		/// <summary>
		/// Gets or sets the HorizontalScale property.
		/// </summary>
		public ChartScale HorizontalScale
		{
			get { return horizontalScale; }
			set
			{
				if (horizontalScale != value)
				{
					if (value != null)
					{
						if (!value.IsConsistent)
							throw new ArgumentException("New HorizontalScale value isn't in consistent state");
						if (ItemData != null && !value.CompatibleWith(ItemData.TypeOfAbscissa))
							throw new ArgumentException("New HorizontalScale value type doesn't match the one of data type");
					}

					if (horizontalScale != null)
						horizontalScale.PropertyChanged -= HorizontalScaleChanged;

					horizontalScale = value;
					
					if (horizontalScale != null)
						horizontalScale.PropertyChanged += HorizontalScaleChanged;
					
					NotifyPropertyChanged("HorizontalScale");
				}
			}
		}
		/// <summary>
		/// HorizontalScale PropertyChanged event handler.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void HorizontalScaleChanged(object sender, PropertyChangedEventArgs e)
		{
			NotifyPropertyChanged("HorizontalScale." + e.PropertyName);
		}
		#endregion HorizontalScale

		#region VerticalScale
		ChartScale verticalScale;
		/// <summary>
		/// Gets or sets the VerticalScale property.
		/// </summary>
		public ChartScale VerticalScale
		{
			get { return verticalScale; }
			set
			{
				if (verticalScale != value)
				{
					if (value != null)
					{
						if (!value.IsConsistent)
							throw new ArgumentException("New VerticalScale value isn't in consistent state");
						if (ItemData != null && !value.CompatibleWith(ItemData.TypeOfOrdinate))
							throw new ArgumentException("New VerticalScale value type doesn't match the one of data type");
					}

					if (verticalScale != null)
						verticalScale.PropertyChanged -= VerticalScaleChanged;

					verticalScale = value;
					
					if (verticalScale != null)
						verticalScale.PropertyChanged += VerticalScaleChanged;
					
					NotifyPropertyChanged("VerticalScale");
				}
			}
		}
		/// <summary>
		/// VerticalScale PropertyChanged event handler.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void VerticalScaleChanged(object sender, PropertyChangedEventArgs e)
		{
			NotifyPropertyChanged("VerticalScale." + e.PropertyName);
		}
		#endregion VerticalScale

		#region Orientation
		Orientation orientation = Orientation.Horizontal;
		/// <summary>
		/// Gets or sets the Orientation property.
		/// </summary>
		public Orientation Orientation
		{
			get { return orientation; }
			set
			{
				if (orientation != value)
				{
					if (value != Orientation.Horizontal && value != Orientation.Vertical)
						throw new ArgumentException("Invalid Orientation enum value", "value");
					orientation = value;
					NotifyPropertyChanged("Orientation");
				}
			}
		}
		#endregion Orientation

		#region RaiseMouseEnterLeaveItemEvents
		bool raiseMouseEnterLeaveItemEvents = true;
		/// <summary>
		/// Gets or sets the RaiseMouseEnterLeaveItemEvents property.
		/// This property controls whether MouseEnterItem/MouseLeaveItem Events raised.
		/// </summary>
		public bool RaiseMouseEnterLeaveItemEvents
		{
			get { return raiseMouseEnterLeaveItemEvents; }
			set
			{
				if (raiseMouseEnterLeaveItemEvents != value)
				{
					raiseMouseEnterLeaveItemEvents = value;
					NotifyPropertyChanged("RaiseMouseEnterLeaveItemEvents");
				}
			}
		}
		#endregion RaiseMouseEnterLeaveItemEvents

		#region VisualCue
		object visualCue;
		/// <summary>
		/// Gets or sets the VisualCue property.
		/// </summary>
		public object VisualCue
		{
			get { return visualCue; }
			set
			{
				if (visualCue != value)
				{
					visualCue = value;
					NotifyPropertyChanged("VisualCue");
				}
			}
		}
		#endregion VisualCue

		#region INotifyPropertyChanged Members
		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Notifies the property changed.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion INotifyPropertyChanged Members
	}
}
