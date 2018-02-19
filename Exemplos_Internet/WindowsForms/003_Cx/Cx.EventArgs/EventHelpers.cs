using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

using Cx.Exceptions;
using Cx.Interfaces;

namespace Cx.EventArgs
{
	/// <summary>
	/// Each component instance can have several events, so we need a composite 
	/// key for the instance and the event name.		 
	/// </summary>
	public struct ComponentKey
	{
		public object component;								   
		public string eventName;

		public ComponentKey(object component, string eventName)
		{
			this.component = component;
			this.eventName = eventName;
		}
	}

	/// <summary>
	/// Used to transform an EventHandler event with signature (object sender, EventArgs args), to a specialized
	/// Cx event.  Commonly used to automatically transform a control event (like TextChanged) to a Cx event.
	/// This is useful if there are a lot of events that we need to transform in a component, as the component
	/// does have to implement IDisposable to clean up the dictionary that this class maintains.
	/// </summary>
	public static class EventHelpers
	{
		public static bool ThrowExceptionOnEventException { get; set; }
		public static bool LogEvents { get; set; }

		/// <summary>
		/// A reference to any instance of CxApp, but usually the main application instance,
		/// which is used in the event logger to get the wireup information associated with
		/// an EventInfo instance.
		/// </summary>
		public static ICxApp App { get; set; }

		// noone but that assembly can access this dictionary.
		internal static Dictionary<ComponentKey, EventHelper> componentEventHelpers = new Dictionary<ComponentKey, EventHelper>();

		/// <summary>
		/// Attempts to acquire the implementor (our event helper) for the component-event key.
		/// </summary>
		public static bool TryGetImplementor(ComponentKey key, out EventHelper implementor)
		{
			return componentEventHelpers.TryGetValue(key, out implementor);
		}

		/// <summary>
		/// Transforms an object's event to a System.EventArgs event.  Basically used to register an event handler.
		/// </summary>
		public static EventHelper Transform(object component, object obj, string objectEventName)
		{
			Type objType = obj.GetType();
			EventInfo ei = objType.GetEvent(objectEventName);
			EventHelper helper = new StandardEventHelper(component, obj, ei, null);

			return helper;
		}

		/// <summary>
		/// Transform the object's event to a component event.
		/// </summary>
		/// <param name="component">The component instance.</param>
		/// <param name="obj">The object that is the source of the event.</param>
		/// <param name="objectEventName">The object's event name.</param>
		/// <param name="objectPropertyName">The object's property, whose value will be passed in the Cx event args.</param>
		/// <param name="eventToFire">Our event name we're wiring up to, simulating a concrete event that the component would otherwise implement.</param>
		public static EventHelper Transform(object component, object obj, string objectEventName, string objectPropertyName)
		{
			Type objType=obj.GetType();
			EventInfo ei = objType.GetEvent(objectEventName);
			PropertyInfo pi = objType.GetProperty(objectPropertyName);
			EventHelper helper = null;

			// TODO: Add more intrinsic type transformations.
			switch (pi.PropertyType.Name.ToLower())
			{
				case "boolean":
					helper = new BoolEventHelper(component, obj, ei, pi);
					break;

				case "string":
					helper = new StringEventHelper(component, obj, ei, pi);
					break;

				case "ienumerable":
					helper = new EnumerableEventHelper(component, obj, ei, pi);
					break;

				case "object":
					helper = new ObjectEventHelper(component, obj, ei, pi);
					break;

				default:
					throw new CxException("No implementation for transforming the event " + objectEventName + " to a Cx event of type " + pi.PropertyType.Name);
			}

			return helper;
		}

		/// <summary>
		/// We have to remove the component's helper from the dictionary, otherwise nothing will get GC'd when the component is
		/// destroyed.
		/// </summary>
		public static void Remove(object component)
		{
			List<ComponentKey> keysToRemove = new List<ComponentKey>(from compKey in componentEventHelpers.Keys where compKey.component == component select compKey);

			foreach (ComponentKey key in keysToRemove)
			{
				componentEventHelpers.Remove(key);
			}
		}

		/// <summary>
		/// Creates a standard (EventArgs) event.
		/// </summary>
		/// <param name="component"></param>
		/// <param name="EventName"></param>
		/// <returns></returns>
		public static EventHelper CreateEvent(object component, string eventName)
		{
			EventHelper ret = new StandardEventHelper(component);
			componentEventHelpers[new ComponentKey(component, eventName)] = ret;

			return ret;
		}

		/// <summary>
		/// Creates a command event that is mananged internally.  Invocation of
		/// the command event is accomplished using the FireCommand method below.
		/// </summary>
		public static void CreateCommand(object component, string eventName)
		{
			EventHelper ret = new StandardEventHelper(component);
			componentEventHelpers[new ComponentKey(component, eventName)] = ret;
		}

		/// <summary>
		/// Fires a command event associated with a component and the event name.
		/// </summary>
		public static void FireCommand(object component, string eventName)
		{
			componentEventHelpers[new ComponentKey(component, eventName)].Fire();
		}

		public static EventHelper CreateEvent<T>(object component, string eventName)
		{
			EventHelper ret = null;

			switch (typeof(T).Name.ToLower())
			{
				case "boolean":
					ret = new BoolEventHelper(component);
					break;
				case "string":
					ret = new StringEventHelper(component);
					break;
				case "cxstringpair":
					ret = new StringPairEventHelper(component);
					break;
				case "cxobjectstate":
					ret = new ObjectStateEventHelper(component);
					break;
				case "ienumerable":
					ret = new EnumerableEventHelper(component);
					break;
				case "object":
					ret = new ObjectEventHelper(component);
					break;
				default:
					throw new CxException("No implementation for creating the event "+typeof(T).Name);
			}

			componentEventHelpers[new ComponentKey(component, eventName)] = ret;

			return ret;
		}

		/// <summary>
		/// Fires a parameterless event that was auto-generated for a wire-up in which there is
		/// no concrete event declared in the component, nor an EventHelper instance managed by
		/// the component.  This simplifies, at least partially, the execution of events.
		/// </summary>
		/// <param name="eventName"></param>
		//public static void Command(string eventName)
		//{
		//}

		/// <summary>
		/// Performs a safe invoke of all methods attached to the delegate.
		/// This method is the master event fire event that allows provides additional functionality:
		/// 1. logging of events
		/// 2. synchronous or asynchronous event firing
		/// 3. safe invocation, in that exceptions are caught, allowing the event chain to continue.
		/// </summary>
		public static void Fire(Delegate del, params object[] args)
		{
			if (del == null)
			{
				return;
			}

			List<Exception> exceptions = new List<Exception>();
			Delegate[] delegates = del.GetInvocationList();

			foreach (Delegate sink in delegates)
			{
				try
				{
					if (LogEvents)
					{
						try
						{
							IWireupInfo wireupInfo = App.GetWireupInfo(args[0], del.Target, sink.Method);

							if (wireupInfo != null)
							{
								Debug.WriteLine(wireupInfo.Producer + " -> " + wireupInfo.Consumer);
							}
							else
							{
								// This event was wired up some other way.  Just display the source, target, and target method.
								// If we wanted to be really tricky, we could inspect the stack frame to get the method that is invoking the event.
								Debug.WriteLine(args[0].ToString() + " firing event to " + sink.Target.ToString() + "." + sink.Method.ToString());
							}
						}
						catch
						{
							// ignore any exceptions the logger creates!
						}
					}

					sink.DynamicInvoke(args);
				}
				catch (Exception e)
				{
					exceptions.Add(e);
				}
			}

			if (exceptions.Count > 0)
			{
				if (ThrowExceptionOnEventException)
				{
					throw new CxException(exceptions, "Event exceptions have occurred.");
				}
			}
		}
	}

	/// <summary>
	/// Manages common information about our event helpers.
	/// </summary>
	public abstract class EventHelper
	{
		protected object Component { get; set; }
		protected object Object { get; set; }
		protected EventInfo EventInfo { get; set; }
		protected PropertyInfo PropertyInfo { get; set; }

		public virtual void Fire() { }
		public virtual void Fire(object data) { }

		/// <summary>
		/// This makes the whole line more readable.  For example:
		/// EventHelpers.Transform(this, tbDisplay, "TextChanged", "Text").To("DisplayTextChanged");
		/// </summary>
		public virtual EventHelper To(string eventToFire)
		{
			EventHelpers.componentEventHelpers[new ComponentKey(Component, eventToFire)] = this;

			return this;
		}

		internal EventHelper(object component)
		{
			Component = component;
		}

		internal EventHelper(object component, object obj, EventInfo ei, PropertyInfo pi)
		{												 
			Component = component;
			Object = obj;
			EventInfo = ei;
			PropertyInfo = pi;

			WireUpCommonHandler();
		}

		/// <summary>
		/// Wires up our event handler to the object's event.
		/// </summary>
		protected virtual void WireUpCommonHandler()
		{
			MethodInfo mi = GetType().GetMethod("CommonHandler", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance); ;
			Delegate dlgt = Delegate.CreateDelegate(EventInfo.EventHandlerType, this, mi);
			EventInfo.AddEventHandler(Object, dlgt);
		}
	}

	internal class StandardEventHelper : EventHelper
	{
		public event EventHandler Event;

		public StandardEventHelper(object component)
			: base(component)
		{
		}

		public StandardEventHelper(object component, object obj, EventInfo ei, PropertyInfo pi)
			: base(component, obj, ei, pi)
		{
		}

		public override void Fire()
		{
			EventHelpers.Fire(Event, Component, System.EventArgs.Empty);
		}

		/// <summary>
		/// The source (object's) event is handled here, and fires the System.EventArgs event with EventArgs.Empty
		/// </summary>
		protected void CommonHandler(object sender, System.EventArgs e)
		{
			EventHelpers.Fire(Event, Component, System.EventArgs.Empty);
		}
	}

	internal class ObjectEventHelper : EventHelper
	{
		/// <summary>
		/// Events have to be implemented in our own class because events cannot be fired from anywhere but our class.
		/// </summary>
		// Too bad we can't use generics here for the delegate type.
		public event CxObjectDlgt Event;

		public ObjectEventHelper(object component)
			: base(component)
		{
		}

		public ObjectEventHelper(object component, object obj, EventInfo ei, PropertyInfo pi)
			: base(component, obj, ei, pi)
		{
		}

		public override void Fire(object data)
		{
			EventHelpers.Fire(Event, Component, new CxEventArgs<object>(data));
		}

		/// <summary>
		/// The source (object's) event is handled here, and fires the Cx event, acquiring the value via
		/// reflection from the PropertyInfo instance.
		/// </summary>
		protected void CommonHandler(object sender, System.EventArgs e)
		{
			object val = PropertyInfo.GetValue(Object, null);
			EventHelpers.Fire(Event, Component, new CxEventArgs<object>(val));
		}
	}

	/// <summary>
	/// Implements the transformation from Event(object sender, EventArgs e) to Event(component, CxEventArgs&lt;string&gt; args.
	/// </summary>
	internal class StringEventHelper : EventHelper
	{
		/// <summary>
		/// Events have to be implemented in our own class because events cannot be fired from anywhere but our class.
		/// </summary>
		// Too bad we can't use generics here for the delegate type.
		public event CxStringDlgt Event;

		public StringEventHelper(object component)
			: base(component)
		{
		}

		public StringEventHelper(object component, object obj, EventInfo ei, PropertyInfo pi)
			: base(component, obj, ei, pi)
		{
		}

		public override void Fire(object data)
		{
			Verify.IsOfType<string>(data, "The data is not of type String");
			EventHelpers.Fire(Event, Component, new CxEventArgs<string>((string)data));
		}

		/// <summary>
		/// The source (object's) event is handled here, and fires the Cx event, acquiring the value via
		/// reflection from the PropertyInfo instance.
		/// </summary>
		protected void CommonHandler(object sender, System.EventArgs e)
		{
			string val = PropertyInfo.GetValue(Object, null) as string;
			EventHelpers.Fire(Event, Component, new CxEventArgs<string>((string)val));
		}
	}

	/// <summary>
	/// Implements the transformation from Event(object sender, EventArgs e) to Event(component, CxEventArgs&lt;string&gt; args.
	/// </summary>
	internal class BoolEventHelper : EventHelper
	{
		/// <summary>
		/// Events have to be implemented in our own class because events cannot be fired from anywhere but our class.
		/// </summary>
		// Too bad we can't use generics here for the delegate type.
		public event CxBoolDlgt Event;

		public BoolEventHelper(object component)
			: base(component)
		{
		}

		public BoolEventHelper(object component, object obj, EventInfo ei, PropertyInfo pi)
			: base(component, obj, ei, pi)
		{
		}

		public override void Fire(object data)
		{
			Verify.IsOfType<bool>(data, "The data is not of type Bool");

			if (Event != null)
			{
				EventHelpers.Fire(Event, Component, new CxEventArgs<bool>((bool)data));
				// Event(Component, new CxEventArgs<bool>((bool)data));
			}
		}

		/// <summary>
		/// The source (object's) event is handled here, and fires the Cx event, acquiring the value via
		/// reflection from the PropertyInfo instance.
		/// </summary>
		protected void CommonHandler(object sender, System.EventArgs e)
		{
			bool val = (bool)PropertyInfo.GetValue(Object, null);
			EventHelpers.Fire(Event, Component, new CxEventArgs<bool>((bool)val));
		}
	}

	internal class StringPairEventHelper : EventHelper
	{
		/// <summary>
		/// Events have to be implemented in our own class because events cannot be fired from anywhere but our class.
		/// </summary>
		// Too bad we can't use generics here for the delegate type.
		public event CxStringPairDlgt Event;

		public StringPairEventHelper(object component)
			: base(component)
		{
		}

		public StringPairEventHelper(object component, object obj, EventInfo ei, PropertyInfo pi)
			: base(component, obj, ei, pi)
		{
		}

		public override void Fire(object data)
		{
			Verify.IsOfType<CxStringPair>(data, "The data is not of type CxStringPair");
			EventHelpers.Fire(Event, Component, new CxEventArgs<CxStringPair>((CxStringPair)data));
		}

		/// <summary>
		/// The source (object's) event is handled here, and fires the Cx event, acquiring the value via
		/// reflection from the PropertyInfo instance.
		/// </summary>
		protected void CommonHandler(object sender, System.EventArgs e)
		{
			CxStringPair val = PropertyInfo.GetValue(Object, null) as CxStringPair;
			EventHelpers.Fire(Event, Component, new CxEventArgs<CxStringPair>((CxStringPair)val));
		}
	}

	internal class ObjectStateEventHelper : EventHelper
	{
		/// <summary>
		/// Events have to be implemented in our own class because events cannot be fired from anywhere but our class.
		/// </summary>
		// Too bad we can't use generics here for the delegate type.
		public event CxObjectStateDlgt Event;

		public ObjectStateEventHelper(object component)
			: base(component)
		{
		}

		public ObjectStateEventHelper(object component, object obj, EventInfo ei, PropertyInfo pi)
			: base(component, obj, ei, pi)
		{
		}

		public override void Fire(object data)
		{
			Verify.IsOfType<CxObjectState>(data, "The data is not of type CxObjectState");
			EventHelpers.Fire(Event, Component, new CxEventArgs<CxObjectState>((CxObjectState)data));
		}

		/// <summary>
		/// The source (object's) event is handled here, and fires the Cx event, acquiring the value via
		/// reflection from the PropertyInfo instance.
		/// </summary>
		protected void CommonHandler(object sender, System.EventArgs e)
		{
			CxObjectState val = PropertyInfo.GetValue(Object, null) as CxObjectState;
			EventHelpers.Fire(Event, Component, new CxEventArgs<CxObjectState>((CxObjectState)val));
		}
	}

	internal class EnumerableEventHelper : EventHelper
	{
		/// <summary>
		/// Events have to be implemented in our own class because events cannot be fired from anywhere but our class.
		/// </summary>
		// Too bad we can't use generics here for the delegate type.
		public event CxEnumerableDlgt Event;

		public EnumerableEventHelper(object component)
			: base(component)
		{
		}

		public EnumerableEventHelper(object component, object obj, EventInfo ei, PropertyInfo pi)
			: base(component, obj, ei, pi)
		{
		}

		public override void Fire(object data)
		{
			Verify.IsOfType<IEnumerable>(data, "The data is not of type IEnumerable");
			EventHelpers.Fire(Event, Component, new CxEventArgs<IEnumerable>((IEnumerable)data));
		}

		/// <summary>
		/// The source (object's) event is handled here, and fires the Cx event, acquiring the value via
		/// reflection from the PropertyInfo instance.
		/// </summary>
		protected void CommonHandler(object sender, System.EventArgs e)
		{
			IEnumerable val = PropertyInfo.GetValue(Object, null) as IEnumerable;
			EventHelpers.Fire(Event, Component, new CxEventArgs<IEnumerable>((IEnumerable)val));
		}
	}
}
																  