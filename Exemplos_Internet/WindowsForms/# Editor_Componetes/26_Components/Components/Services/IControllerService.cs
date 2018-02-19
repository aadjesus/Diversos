using System;
using System.Collections;

using Mvc.Components.Controller;
using Mvc.Components.Model;

namespace Mvc.Components.Services
{
	/// <summary>
	/// Provides design-time services related to controller mappings.
	/// </summary>
	public interface IControllerService
	{
		/// <summary>
		/// Verifies that mappings for the controller are correct. 
		/// </summary>
		/// <param name="controller">Controller to check.</param>
		void VerifyMappings(BaseController controller);

		/// <summary>
		/// Lists all the properties of the control.
		/// </summary>
		string[] GetControlProperties(object control);

		/// <summary>
		/// Lists all the properties of the model.
		/// </summary>
		string[] GetModelProperties(BaseModel model);
	}
}
