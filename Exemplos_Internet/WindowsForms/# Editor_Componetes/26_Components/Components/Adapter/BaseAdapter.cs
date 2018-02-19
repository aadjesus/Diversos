using System;
using System.ComponentModel;

using Mvc.Components.Controller;

namespace Mvc.Components.Adapter
{
	/// <summary>
	/// Provides an abstract and common base for windows and web forms adapters.
	/// </summary>
	public abstract class BaseAdapter
	{
		/// <summary>
		/// Connects a controller with its hosting environment.
		/// </summary>
		public abstract void Connect(BaseController controller, object controlsContainer, IContainer componentsContainer);
	}
}
