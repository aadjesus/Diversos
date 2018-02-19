using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

using Mvc.Components.Controller;
using Mvc.Components.Services;

namespace Mvc.Components.Adapter
{
	/// <summary>
	/// Provides conectivity with windows forms hosts.
	/// </summary>
	public class WindowsFormsAdapter : BaseAdapter
	{
		/// <summary>
		/// We keep the service because there's no such global context like the web HttpContext. 
		/// </summary>
		IAdapterService _service;
		/// <summary>
		/// The controller to connect with the host. We keep it because it must be connected after the form finished loading.
		/// </summary>
		BaseController _controller;

		/// <summary>
		/// Connects a controller with its hosting environment.
		/// </summary>
		public override void Connect(BaseController controller, object controlsContainer, IContainer componentsContainer)
		{
			_service = new WindowsFormsAdapterService(controlsContainer, componentsContainer);
			_controller = controller;

			//We don't connect controls to the site because they won't ask for our service for now.
//			//Connect each control to a run-time site.
//			foreach (object obj in _service.GetControls())
//			{
//				if (obj as Control != null)
//				{
//					Control control = (Control) obj;
//					control.Site = new RuntimeSite(control.Container, control, new GetServiceEventHandler(GetServiceHandler));
//				}
//			}

			//Connect components to a run-time site.
			controller.Site = new RuntimeSite(componentsContainer, controller, new GetServiceEventHandler(GetServiceHandler));
			foreach (IComponent component in controller.Components)
			{
				component.Site = new RuntimeSite(controller, component, new GetServiceEventHandler(GetServiceHandler));
			}

			//Connect lifecycle events.
			Form form = (Form) controlsContainer;
			//First connect control events.
			form.Activated += new EventHandler(OnActivated);
			//Initialize controller context next.
			form.Activated += new EventHandler(controller.Init);
			//Refresh control values at loading time.
			form.Load += new EventHandler(controller.RefreshView);
			//Persist in model at deactivation time.
			form.Deactivate += new EventHandler(controller.RefreshModels);
			//EXPLAIN: Can't connect controls at this time because there's no guarantee
			//of the order in which components are added to the form.
		}

		/// <summary>
		/// Hook controls to the controller to refresh the model.
		/// </summary>
		void OnActivated(object sender, EventArgs e)
		{
			//Just a simple implementation based on focus changes.
			//EXPLAIN: production quality may be more fine-grained.
			foreach (DictionaryEntry entry in _controller.ConfiguredViews)
			{
				Control ctl = (Control) _service.FindControl(((ViewInfo)entry.Value).ControlID);
				ctl.Leave += new EventHandler(_controller.RefreshModels);
			}
			//Refresh the form view.
			_controller.RefreshView(sender, e);
			//Attach the model changed event to the controller refresh method, 
			//so that the view is automatically updated.
			_controller.ModelChanged += new EventHandler(_controller.RefreshView);
		}
 
		/// <summary>
		/// Resolves queries for the <see cref="IAdapterService"/> at runtime.
		/// </summary>
		/// <returns>The <see cref="IAdapterService"/> instance or <c>null</c>.</returns>
		object GetServiceHandler(object sender, GetServiceEventArgs e)
		{
			if (e.ServiceType == typeof(IAdapterService))
			{
				return _service;
			}
			else
			{
				return null;
			}
		}
	}
}
