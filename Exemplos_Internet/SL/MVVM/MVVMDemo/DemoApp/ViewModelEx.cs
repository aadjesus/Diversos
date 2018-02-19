using System;
using System.ComponentModel;
using System.Diagnostics;
using Galador.Applications;

namespace DemoApp
{
	/// <summary>
	/// Base class for all ViewModel classes in the application.
	/// It provides support for property change notifications 
	/// and has a DisplayName property.  This class is abstract.
	/// </summary>
	public abstract class ViewModelEx : ViewModelBase, IDisposable
	{
		/// <summary>
		/// Returns the user-friendly name of this object.
		/// Child classes can set this property to a new value,
		/// or override it to determine the value on-demand.
		/// </summary>
		public virtual string DisplayName { get; protected set; }


		public void Dispose()
		{
			this.OnDispose(true);
		}

		protected virtual void OnDispose(bool disposing)
		{
		}

#if DEBUG
		/// <summary>
		/// Useful for ensuring that ViewModel objects are properly garbage collected.
		/// </summary>
		~ViewModelEx()
		{
			string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName, this.GetHashCode());
			System.Diagnostics.Debug.WriteLine(msg);
			OnDispose(false);
		}
#endif
	}
}