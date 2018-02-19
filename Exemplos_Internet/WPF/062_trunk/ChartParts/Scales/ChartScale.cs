// <copyright file="ChartScale.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library.
// ChartScale - Abstract base class for all Chart scales varieties.
// </summary>
// <revision>$Id: ChartScale.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Abstract base class for all Chart scales varieties.
	/// </summary>
	/// <remarks>
	/// The class describes Chart scale on either X or Y axis.
	/// <para>Defines properties collectively used by Charts, Curves, Axes and Grids.</para>
	/// </remarks>
	public abstract class ChartScale : INotifyPropertyChanged
	{
		#region Start
		object start;
		/// <summary>
		/// Gets or sets the Start property.
		/// </summary>
		/// <remarks>
		/// <see cref="Start"/> property value is expressed in "external" measurement  units and denotes the 
		/// <see cref="ChartScale"/> opening value which usually relates to top-left Chart corner.
		/// <para><see cref="Start"/> and <see cref="Stop"/> values are unrelated.</para>
		/// </remarks>
		/// <value><see cref="ChartScale"/> opening value.</value>
		/// <event cref="PropertyChanged">Raised if the property value chnges.</event>
		public object Start
		{
			get { return start; }
			set 
			{ 
				if (start != value)
				{
					start = value;
					NotifyPropertyChanged("Start");
				}
			}
		}
		#endregion Start

		#region Stop
		object stop;
		/// <summary>
		/// Gets or sets the Stop property.
		/// </summary>
		/// <remarks>
		/// <see cref="Stop"/> property value is expressed in "external" measurement units and denotes the 
		/// <see cref="ChartScale"/> closing value which usually relates to bottom-right Chart corner.
		/// <para><see cref="Start"/> and Stop values are unrelated.</para>
		/// </remarks>
		/// <value><see cref="ChartScale"/> closing value.</value>
		/// <event cref="PropertyChanged">Raised if the property value chnges.</event>
		public object Stop
		{
			get { return stop; }
			set 
			{ 
				if (stop != value)
				{
					stop = value;
					NotifyPropertyChanged("Stop");
				}
			}
		}
		#endregion Stop

		#region Scale
		double scale = 1.0;
		/// <summary>
		/// Gets or sets the Scale property.
		/// </summary>
		/// <remarks>
		/// This property is the ratio of the property value expressed in logical
		/// pixel units and the property value expressed in "external" measurement units
		/// (like for <see cref="Start"/> and <see cref="Stop"/> properties).
		/// <see cref="Scale"/> value means "pixels per external unit" (e.g. pixels per cm).
		/// <para>Any finite positive value is acceptable.</para>
		/// </remarks>
		/// <value><see cref="ChartScale"/> scale value.</value>
		/// <event cref="PropertyChanged">Raised if the property value chnges.</event>
		public double Scale
		{
			get { return scale; }
			set 
			{ 
				if (scale != value)
				{
					if (double.IsInfinity(value) || double.IsNaN(value) || value <= 0.0)
						throw new ArgumentException("Scale must be finite and positive", "value");
					scale = value;
					NotifyPropertyChanged("Scale");
				}
			}
		}
		#endregion Scale

		/// <summary>
		/// Checks whether the <see cref="ChartScale"/> object is properly initialized.
		/// </summary>
		/// <returns><c>true</c> if this <see cref="ChartScale"/> object is properly initialized;
		/// oterwise <c>false</c>.</returns>
		public virtual bool IsConsistent
		{
			get { return (Start != null && Stop != null && Start != Stop); }
		}

		/// <summary>
		/// Determines whether this object is properly initialized and is compatible 
		/// with the base type specified.
		/// </summary>
		/// <remarks>
		/// Note that <see cref="Start"/>/<see cref="Stop"/> values may have types different from 
		/// the type of range the <see cref="ChartScale"/> represent. E.g. when set in <c>XAML</c> 
		/// the values may by of string type whereas <see cref="ChartScale"/>
		/// represent Numeric or DateTime range. 
		/// </remarks>
		/// <param name="baseType">Type of values described by the <see cref="ChartScale"/>.
		/// E.g. double, DateTime, etc.</param>
		/// <returns>
		/// 	<c>true</c> if this object is properly initialized and is compatible with 
		/// 	the base type specified; otherwise, <c>false</c>.
		/// </returns>
		public bool CompatibleWith(Type baseType)
		{
			if (!IsConsistent)
				return false;
			try
			{
				Convert.ChangeType(Start, baseType);
				Convert.ChangeType(Stop, baseType);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// Converts value expressed in "external" measurement units to value in logical pixels units.
		/// </summary>
		/// <param name="value">value in "external" measurement units</param>
		/// <returns>value in pixels</returns>
		public abstract double ToPixels(object value);

		/// <summary>
		/// Converts value expressed in logical pixels units to value in "external" measurement units.
		/// </summary>
		/// <param name="value">value in pixels</param>
		/// <returns>value in "external" measurement units</returns>
		public abstract object FromPixels(double value);

		/// <summary>
		/// Returns Scale Ticks iterator.
		/// </summary>
		/// <returns><see cref="ScaleTick"/> enumerator.</returns>
		public abstract IEnumerable<ScaleTick> Ticks();

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
