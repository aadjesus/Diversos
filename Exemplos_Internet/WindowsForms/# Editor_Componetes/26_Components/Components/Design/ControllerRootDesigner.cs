using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace Mvc.Components.Design
{
	/// <summary>
	/// This root designer controls which toolbox items are available as well as filtering properties.
	/// </summary>
	[ToolboxItemFilter("Mvc.Components.Controller", ToolboxItemFilterType.Require)]
	public class ControllerRootDesigner : ComponentDocumentDesigner
	{
		/// <summary>
		/// Removes the <see cref="BaseController.ConfiguredViews"/> property when the controller is
		/// the root component, because in that case it's not applicable.
		/// </summary>
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			//DONE: Only show ConfiguredViews if the component is not the root component.
			IDesignerHost host = (IDesignerHost) GetService(typeof(IDesignerHost));
			if (host.RootComponent == this.Component)
				properties.Remove("ConfiguredViews");
		}

	}
}
