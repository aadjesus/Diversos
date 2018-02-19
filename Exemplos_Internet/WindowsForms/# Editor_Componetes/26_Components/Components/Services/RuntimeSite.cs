using System;
using System.ComponentModel;

namespace Mvc.Components.Services
{
	/// <summary>
	/// Provides a means to hook to the request for a service during run-time.
	/// </summary>
	public delegate object GetServiceEventHandler(object sender, GetServiceEventArgs e);
	
	/// <summary>
	/// Arguments of a request for a service. 
	/// </summary>
	public class GetServiceEventArgs
	{
		public GetServiceEventArgs(Type serviceType)
		{
			_service = serviceType;
		}

		/// <summary>
		/// Gets the requested service.
		/// </summary>
		public Type ServiceType
		{
			get { return _service; }
		} Type _service;
	}


	/// <summary>
	/// Provides a custom site to be used at run-time, so that components can 
	/// request our custom services just as if they were at design-time.
	/// </summary>
	public class RuntimeSite : ISite
	{
		/// <summary>
		/// Constructs the site.
		/// </summary>
		/// <param name="container">Current component container.</param>
		/// <param name="component">The component to associate this site with.</param>
		/// <param name="getServiceCallback">The handler that will provide services when asked by the component.</param>
		public RuntimeSite(IContainer container, IComponent component, GetServiceEventHandler getServiceCallback)
		{
			_container = container;
			_component = component;
			_callback = getServiceCallback;
		}

		#region Implementation of ISite
	
		public IComponent Component
		{
			get {	return _component; }
		} IComponent _component;

		public IContainer Container
		{
			get { return _container; }
		} IContainer _container;

		public bool DesignMode
		{
			get { return false; }
		}

		public string Name
		{
			get { return String.Empty; }
			set { }
		}

		#endregion

		#region Implementation of IServiceProvider

		/// <summary>
		/// Passes the call for a service to the handler received in the constructor.
		/// </summary>
		public object GetService(Type serviceType)
		{
			return _callback(this, new GetServiceEventArgs(serviceType));
		} GetServiceEventHandler _callback;

		#endregion
	}
}
