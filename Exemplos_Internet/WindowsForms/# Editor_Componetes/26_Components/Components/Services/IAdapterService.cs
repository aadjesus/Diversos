using System;
using System.ComponentModel;

using Mvc.Components.Controller;

namespace Mvc.Components.Services
{
	/// <summary>
	/// Interface implemented by services that adapt requests according to the target "platform".
	/// </summary>
	public interface IAdapterService
	{
		/// <summary>
		/// Locates a control in the current container.
		/// </summary>
		object FindControl(string controlId);

		/// <summary>
		/// Retrieves the control ID.
		/// </summary>
		string GetControlID(object control);

		/// <summary>
		/// Retrieves all controls in the current container.
		/// </summary>
		object[] GetControls();

		/// <summary>
		/// Gets all components in the current container.
		/// </summary>
		ComponentCollection GetComponents();

		/// <summary>
		/// Forces a refresh of controls in the current view (visual container).
		/// </summary>
		/// <param name="controller">The controller whose mappings will be taking into account for the refresh.</param>
		void RefreshView(BaseController controller);

		/// <summary>
		/// Forces a refresh of the model based on the current view (controls) values.
		/// </summary>
		/// <param name="controller">The controller whose mappings will be taking into account for the refresh.</param>
		void RefreshModels(BaseController controller);
	}
}
