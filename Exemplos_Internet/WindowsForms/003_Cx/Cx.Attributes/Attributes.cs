using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cx.Attributes
{
	/// <summary>
	/// Designate events with this attribute for auto-discovery.
	/// Also see CxExplicitEventAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Event)]
	public class CxEventAttribute : Attribute
	{
	}

	/// <summary>
	/// Designate transformed events and events from a control that
	/// you intend to expose for auto-discovery.  Also see CxEventAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
	public class CxExplicitEventAttribute : Attribute
	{
		public string EventName { get; private set; }

		public CxExplicitEventAttribute(string eventName)
		{
			EventName = eventName;
		}
	}

	/// <summary>
	/// Designate event handler methods with this attribute for auto-discovery.
	/// </summary>		 
	[AttributeUsage(AttributeTargets.Method)]
	public class CxConsumerAttribute : Attribute
	{
	}

	/// <summary>
	/// Use this attribute on any Cx component class to give it a friendly name used by the designer
	/// to select a component from an assembly.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class CxComponentNameAttribute : Attribute
	{
		public string ComponentName { get; private set; }

		/// <summary>
		/// The friendly name of the component, used to initialize an instance of this component.
		/// </summary>
		/// <param name="componentName">The component name.</param>
		public CxComponentNameAttribute(string componentName)
		{
			ComponentName = componentName;
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class CxComponentPropertyAttribute : Attribute
	{
	}
}
