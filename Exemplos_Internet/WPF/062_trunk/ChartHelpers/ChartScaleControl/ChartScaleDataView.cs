// <copyright file="ChartScaleDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-02-18</date>
// <summary>OpenWPFChart library. ChartScaleControl DataView .</summary>
// <revision>$Id: ChartScaleDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.ComponentModel;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// ChartScaleControl Data View.
	/// </summary>
	public abstract class ChartScaleDataView : INotifyPropertyChanged, IDataErrorInfo
	{
		public ChartScaleDataView(double scale)
		{
			this.scale = scale;
		}

		public abstract ChartScaleVerieties GetVeriety();
		public abstract object GetStart();
		public abstract object GetStop();

		double scale;
		public double Scale
		{
			get { return scale; }
			set
			{
				if (scale != value)
				{
					scale = value;
					Notify("Scale");
				}
			}
		}

		#region INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;
		protected void Notify(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion INotifyPropertyChanged Members

		#region IDataErrorInfo Members
		string IDataErrorInfo.Error
		{
			get { return null; }
		}

		protected virtual string GetDataErrorInfo(string columnName)
		{
				switch(columnName)
				{
					case "Scale":
						if (Scale <= 0)
							return "Scale must be positive";
						break;
				}
				return null;
		}

		string IDataErrorInfo.this[string columnName]
		{
			get 
			{
				return GetDataErrorInfo(columnName);
			}
		}
		#endregion IDataErrorInfo Members
	}
}
