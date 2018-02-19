using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Web;
using System.Web.UI;

using Mvc.Components.Controller;
using Mvc.Components.Services;

namespace Mvc.Components.Adapter
{
	/// <summary>
	/// Provides connectivity with the hosting web forms.
	/// </summary>
	public class WebFormsAdapter : BaseAdapter
	{
		/// <summary>
		/// Connects a controller with its hosting environment.
		/// </summary>
		public override void Connect(BaseController controller, object controlsContainer, IContainer componentsContainer)
		{
			WebFormsAdapterService service;

			//EXPLAIN: This will always be called at run-time exclusively.
			if (!HttpContext.Current.Items.Contains(typeof(IAdapterService).FullName))
			{
				service = new WebFormsAdapterService(controlsContainer, componentsContainer);
				HttpContext.Current.Items.Add(typeof(IAdapterService).FullName, service);
			}	
			else
			{
				service = (WebFormsAdapterService) HttpContext.Current.Items[typeof(IAdapterService).FullName];
			}

			//We don't connect controls because they won't ask for our service for now.
//			//Connect each control to a run-time site.
//			foreach (object obj in _service.GetControls())
//			{
//				if (obj as Control != null)
//				{
//					Control control = (Control) obj;
//					control.Site = new RuntimeSite(control.Container, control, new GetServiceEventHandler(GetServiceHandler));
//				}
//			}

			//EXPLAIN: Connect components to a run-time site, to provide access to the adapter service, which is resolved by the service itself (unlike windows adapter).
			controller.Site = new RuntimeSite(componentsContainer, controller, new GetServiceEventHandler(service.GetServiceHandler));
			foreach (IComponent component in controller.Components)
			{
				component.Site = new RuntimeSite(controller, component, new GetServiceEventHandler(service.GetServiceHandler));
			}

			//Connect lifecycle events.
			Page page = (Page) controlsContainer;
			page.Init += new EventHandler(controller.Init);
			page.Load += new EventHandler(controller.RefreshModels);
			page.PreRender += new EventHandler(controller.RefreshView);
		}
	}
}
