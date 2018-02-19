using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;

using Cx.Attributes;
using Cx.Common;
using Cx.EventArgs;
using Cx.Exceptions;
using Cx.Interfaces;

namespace Cx
{
	public struct ProducerEventInfo : IComparable
	{
		public object source;
		public object target;
		public MethodInfo methodInfo;

		public ProducerEventInfo(object source, object target, MethodInfo methodInfo)
		{
			this.source = source;
			this.target = target;
			// this.eventInfo = eventInfo;
			this.methodInfo = methodInfo;
		}

		public int CompareTo(object obj)
		{
			ProducerEventInfo pei = (ProducerEventInfo)obj;
			int ret = 1;

			if ((source == pei.source) && (target == pei.target) && (methodInfo == pei.methodInfo))
			{
				ret = 0;
			}

			return ret;
		}

		public override bool Equals(object obj)
		{
			return CompareTo(obj) == 0;
		}

		public override int GetHashCode()
		{
			return source.GetHashCode() | target.GetHashCode() | methodInfo.GetHashCode();
		}
	}

	public class ComponentReference
	{
		public CxApp Container { get; protected set; }
		public ICxBusinessComponent Component { get; protected set; }
		public int Count { get; set; }

		public ComponentReference(CxApp container, ICxBusinessComponent comp)
		{
			Container = container;
			Component = comp;
			Count = 1;
		}
	}

	public class WireupInfo : IWireupInfo
	{
		public EventInfo EventInfo { get; protected set; }
		public string Producer { get; protected set; }
		public string Consumer { get; protected set; }
		public object Source { get; protected set; }
		public object Target { get; protected set; }
		protected Delegate dlgt;

		public WireupInfo(EventInfo eventInfo, object source, object target, Delegate dlgt, string producer, string consumer)
		{
			this.EventInfo = eventInfo;
			Source = source;
			Target = target;
			this.dlgt = dlgt;
			Producer = producer;
			Consumer = consumer;
		}

		public void Remove()
		{
			EventInfo.RemoveEventHandler(Target, dlgt);
		}
	}

	public class CxApp : IDisposable, ICxApp
	{
		/// <summary>
		/// A global reference count for caching components of the same name.
		/// TODO: Should probably also include type information, so components of different types but the same name can be handled.
		/// </summary>
		protected static DiagnosticDictionary<string, ComponentReference> businessComponentCountMap = new DiagnosticDictionary<string, ComponentReference>();

		/// <summary>
		/// A global list of the wireups so that we can, given the EventInfo instance, get at the metadata associated with the wireup definition.
		/// </summary>
		protected static DiagnosticDictionary<ProducerEventInfo, WireupInfo> eventWireupMap = new DiagnosticDictionary<ProducerEventInfo, WireupInfo>();

		protected DiagnosticDictionary<string, ICxComponent> components;
		protected List<Wireup> wireups;
		protected DiagnosticDictionary<string, List<PropertyValue>> propertyValues;
		protected List<ICxVisualComponent> visCompList;
		protected List<ICxBusinessComponent> busCompList;
		protected List<WireupInfo> wireupList;
		protected EventHelper componentsInstantiatedEvent;
		protected ICxDataService dataService;
		protected bool disposed;

		public List<ICxVisualComponent> VisualComponents
		{
			get { return visCompList; }
		}

		public CxApp()
		{
			components = new DiagnosticDictionary<string, ICxComponent>();
			wireups = new List<Wireup>();
			propertyValues = new DiagnosticDictionary<string, List<PropertyValue>>();
			visCompList = new List<ICxVisualComponent>();
			busCompList = new List<ICxBusinessComponent>();
			wireupList = new List<WireupInfo>();
		}

		~CxApp()
		{
			Dispose(false);
		}

		/// <summary>
		/// Returns the WireupInfo associated with the EventInfo, using the global
		/// event wireup map.
		/// </summary>
		/// <param name="ei"></param>
		/// <returns></returns>
		public IWireupInfo GetWireupInfo(object producer, object consumer, MethodInfo mi)
		{
			WireupInfo info;
			eventWireupMap.TryGetValue(new ProducerEventInfo(producer, consumer, mi), out info);

			return info;
		}

		/// <summary>
		/// Cx instances are required to call this method to remove business components and
		/// wireups.  If we don't remove the wireups, the events accumulate every time an
		/// instance of the same component is created if the component is persisted across
		/// Cx instances.  
		/// Even if we remove the business component from our cache,
		/// the wireup doesn't get disconnected, so we have to always explicitly remove
		/// the event.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Creates an instance of the Cx framework given the configuration file.
		/// Loads all components and wires them up.
		/// </summary>
		public static CxApp Initialize(string dataServiceAssemblyFilename, string configUri)
		{
			CxApp cx = new CxApp();
			cx.InitializeDataService(dataServiceAssemblyFilename, configUri);
			cx.InstantiateComponents();

			return cx;
		}

		protected void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					List<string> markedForRemoval = new List<string>();

					// Remove business component reference counts.  If 0, we can remove the entry in the dictionary.
					foreach (ICxBusinessComponent comp in busCompList)
					{
						ComponentReference compRef=null;

						if (businessComponentCountMap.TryGetValue(comp.Name, out compRef))
						{
							--compRef.Count;

							if (compRef.Count == 0)
							{
								markedForRemoval.Add(comp.Name);
							}
						}
					}

					// Remove all component references marked for removal.
					foreach (string name in markedForRemoval)
					{
						components.Remove(name);
						businessComponentCountMap.Remove(name);
					}

					RemoveWireups();
					visCompList.Clear();
					busCompList.Clear();
				}

				disposed = true;
			}
		}

		protected void InitializeDataService(string dataServiceAssemblyFilename, string configUri)
		{
			Assembly assy = Assembly.LoadFrom(dataServiceAssemblyFilename);
			Type t = CxCommon.FindImplementor(assy, "CxDataService", typeof(ICxBusinessComponentClass));
			dataService = (ICxDataService)Activator.CreateInstance(t);
			dataService.LoadComponents(configUri);
			components = dataService.Components;
			wireups = dataService.Wireups;
			propertyValues = dataService.PropertyValues;
		}

		/// <summary>
		/// Returns a component implementing a specific interface.
		/// Only use this method if there is only one component in the collection that implements this interface.
		/// </summary>
		// TODO: Might want to make this a dictionary lookup for faster lookup, or some other mechanism that doesn't
			// have to iterate through the whole collection.
		public T GetComponent<T>() where T : ICxComponentClass
		{
			T ret = default(T);	

			foreach (CxComponent comp in components.Values)
			{
				if (comp.Instance is T)
				{
					ret = (T)comp.Instance;
					break;
				}
			}

			Verify.IsNotNull(ret, "Component of type " + typeof(T).Name + " has not been instantiated.");

			return ret;
		}

		/// <summary>
		/// This method should be used internally as the main loader, as it raises
		/// the components loaded event at the end.
		/// </summary>
		protected void InstantiateComponents()
		{
			InstantiateVisualComponents();
			InstantiateBusinessComponents();
			// We defer this until the visual components have been registered, so we can generate the producer events for menu items.
			// WireUpComponents();
			FinalComponentInitialization();
		}

		/// <summary>
		/// Instantiates only the visual components specified in the configuration file.
		/// </summary>
		protected void InstantiateVisualComponents()
		{
			foreach (CxComponent comp in components.Values)
			{
				if (comp is ICxVisualComponent)
				{
					comp.Type = GetImplementorType(comp, typeof(ICxVisualComponentClass));
					ICxComponentClass compInst = InstantiateComponent(comp);
					comp.Instance = compInst;
					visCompList.Add((ICxVisualComponent)comp);
				}
			}
		}

		/// <summary>
		/// Instantiates only the business components specified in the metadata.
		/// Components of the same name, found in the global cache, are not re-instantiated.
		/// The reference is re-used, so we can easily reference components from parents
		/// in the instantiation hierarchy.
		/// </summary>
		protected void InstantiateBusinessComponents()
		{
			foreach (CxComponent comp in components.Values)
			{
				if (comp is ICxBusinessComponent)
				{
					// If we already have this component (by name) initialized in the hierarchy (a parent CxApp container),
					// then we re-use the existing instance rather than creating a new one, so we can communicate, for example,
					// with a model in a parent dialog.
					// Otherwise, create the component.
					ComponentReference compRef=null;

					if (businessComponentCountMap.TryGetValue(comp.Name, out compRef))
					{
						// Even if the count is 0, we can re-use the instance.
						comp.Instance = compRef.Component.Instance;
						comp.Type = compRef.Component.Type;
					}
					else
					{
						// This component has never been seen.
						InstantiateBusinessComponent(comp);
					}

					// List of local business components.
					busCompList.Add((ICxBusinessComponent)comp);
					// Global cache.
					IncrementBusinessComponentCountMap((ICxBusinessComponent)comp);
				}
			}
		}

		protected void IncrementBusinessComponentCountMap(ICxBusinessComponent comp)
		{
			if (businessComponentCountMap.ContainsKey(comp.Name))
			{
				++businessComponentCountMap[comp.Name].Count;
			}
			else
			{
				businessComponentCountMap[comp.Name] = new ComponentReference(this, comp);
			}
		}

		/// <summary>
		/// Wires up producer events with consumer handlers.
		/// </summary>
		public void WireUpComponents()
		{
			foreach (Wireup wireup in wireups)
			{
				WireUp(wireup.Producer, wireup.Consumer);
			}
		}

		protected void RemoveWireups()
		{
			foreach (WireupInfo wi in wireupList)
			{
				wi.Remove();
				// *** !!! ***
				// eventWireupMap.Remove(new ProducerEventInfo(wi.Source, wi.Target, wi.EventInfo));
			}

			wireupList.Clear();
		}

		/// <summary>
		/// Wires up a producer with a consumer.  All components involved in wireups must already be instantiated.
		/// </summary>
		protected void WireUp(string producer, string consumer)
		{
			object producerTarget = GetProducerTarget(producer);
			object source = producerTarget;
			object consumerTarget = GetConsumerComponent(consumer).Instance;

			EventInfo ei = GetEventInfo(producerTarget, producer);
			// We pass in the consumerTarget here, because the consumer.Type is the "open generic"--meaning that T hasn't been defined yet,
			// and we need the closed generic which is only found by getting the type of the specific consumer target.
			// Oddly, we don't have this issue with the producer, though I did modify the code to pass in the producer target as well.
			MethodInfo mi = GetMethodInfo(consumerTarget, consumer);

			if (ei == null)
			{
				// Is this an event transformed from an EventHandler to a CxEvent using the EventHelpers?
				ei = TryEventTransformation(producer, producerTarget, out producerTarget);
			}

			Verify.IsNotNull(ei, "EventInfo did not initialize for wireup of " + producer + " to " + consumer);
			Verify.IsNotNull(mi, "MethodInfo did not initialize for wireup of " + producer + " to " + consumer);

			Type eventHandlerType = ei.EventHandlerType;
			Delegate dlgt = Delegate.CreateDelegate(eventHandlerType, consumerTarget, mi);
			ei.AddEventHandler(producerTarget, dlgt);
			// AddEventMonitor(producerTarget, ei);
			WireupInfo wireupInfo=new WireupInfo(ei, producerTarget, consumerTarget, dlgt, producer, consumer);
			wireupList.Add(wireupInfo);
			eventWireupMap[new ProducerEventInfo(source, consumerTarget, mi)] = wireupInfo;
		}

		//protected void AddEventMonitor(object producerTarget, EventInfo ei)
		//{
		//    MethodInfo mi = GetMethodInfo(this, "EventMonitor");
		//    Type eventHandlerType = ei.EventHandlerType;
		//    Delegate dlgt = Delegate.CreateDelegate(eventHandlerType, this, mi);
		//    ei.AddEventHandler(producerTarget, dlgt);
		//}

		//protected void EventMonitor(object sender, System.EventArgs args)
		//{
		//}

		protected void FinalComponentInitialization()
		{
			foreach (KeyValuePair<string, List<PropertyValue>> kvp in propertyValues)
			{
				ICxComponent comp = components[kvp.Key];

				foreach (PropertyValue pv in kvp.Value)
				{
					if (!String.IsNullOrEmpty(pv.Value))
					{
						InitializeProperty(comp, pv.Name, pv.Value);
					}
					else
					{
						InitializeProperty(comp, pv.Name, pv.ItemValues);
					}
				}
			}
		}
		
		protected void InitializeProperty(ICxComponent comp, string propName, object propValue)
		{
			PropertyInfo pi = comp.Instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			Verify.IsNotNull(pi, "The component " + comp.ComponentName + " does not implement the property " + propName);

			if (pi.PropertyType != propValue.GetType())
			{
				// We need a type converter to convert from the string to the property type.
				TypeConverter tc = TypeDescriptor.GetConverter(pi.PropertyType);

				if (tc.CanConvertFrom(propValue.GetType()))
				{
					propValue=tc.ConvertFrom(propValue);
				}
			}

			try
			{
				pi.SetValue(comp.Instance, propValue, null);
			}
			catch
			{
				throw new CxException("Could not wire up property " + propName + " in component " + comp.Name + " to the value type " + propValue.GetType().AssemblyQualifiedName);
			}
		}

		/// <summary>
		/// Returns the EventInfo associated with an auto-transformed EventHandler to a Cx event delegate.
		/// Used when components want to automate the transformation of a .NET event (like a control event) to a Cx event.
		/// For example: EventHelpers.Transform(this, tbDisplay, "TextChanged", "Text", "DisplayTextChanged");
		/// transforms the TextBox.TextChanged event to a component.DisplayTextChanged Cx event, passing the handler the 
		/// Text value, where the component is the "this" in the above example.
		/// </summary>
		protected EventInfo TryEventTransformation(string producer, object producerTarget, out object implementor)
		{
			implementor = null;
			EventInfo ei = null;
			ComponentKey key = new ComponentKey(producerTarget, producer.Split('.').Last());

			// producer event name has to be used to qualify key, along with producer target.
			EventHelper helper = null;

			if (EventHelpers.TryGetImplementor(key, out helper))
			{
				implementor = helper;
				ei = helper.GetType().GetEvent("Event");
			}

			return ei;
		}

		/// <summary>
		/// Drills into the producer to acquire the object sourcing the event.
		/// </summary>
		protected object GetProducerTarget(string producer)
		{
			string[] parts = producer.Split('.');
			object obj = components[parts[0]].Instance;
			obj = DrillInto(obj, parts);

			return obj;
		}

		/// <summary>
		/// Returns the component instance implementing the event handler.
		/// </summary>
		protected ICxComponent GetConsumerComponent(string consumer)
		{
			string[] consumerParts = consumer.Split('.');

			return components[consumerParts[0]];
		}
																						 
		/// <summary>
		/// Returns the EventInfo structure associated with the producer's event.
		/// </summary>
		protected EventInfo GetEventInfo(object obj, string producer)
		{
			string[] parts = producer.Split('.');
			obj = DrillInto(obj, parts);

			EventInfo ei = obj.GetType().GetEvent(parts[parts.Length-1]);

			return ei;
		}

		/// <summary>
		/// Returns the MethodInfo structure associated with the consumer's event handler.
		/// </summary>
		protected MethodInfo GetMethodInfo(object target, string consumer)
		{
			string[] parts = consumer.Split('.');
			string methodName = parts.Length == 2 ? parts[1] : parts[0];
			MethodInfo mi = target.GetType().GetMethod(methodName, 
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

			return mi;
		}

		/// <summary>
		/// Follow the field chain until we get to the event name.
		/// Allows us to do things like [component].[property].[property].[eventname]
		/// </summary>
		protected object DrillInto(object obj, string[] parts)
		{
			int n = 1;

			while (n < parts.Length - 1)
			{
				obj = obj.GetType().GetField(parts[n], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj);
				++n;
			}

			return obj;
		}

		protected void InstantiateBusinessComponent(CxComponent comp)
		{
			comp.Type = GetImplementorType(comp, typeof(ICxBusinessComponentClass));
			ICxComponentClass compInst = InstantiateComponent(comp);
			comp.Instance = compInst;
		}

		/// <summary>
		/// Returns an instance of the component that implements the specified interface.
		/// </summary>
		protected virtual ICxComponentClass InstantiateComponent(CxComponent comp)
		{
			ICxComponentClass inst = null;

			if (comp.ComponentName.Contains('`'))
			{
				// If it's a generic, create the type this way.
				// Ex: inst = Activator.CreateInstanceFrom(@"..\..\..\Cx.Converters\bin\debug\Cx.Converters.dll", "Cx.Converters.CxBindingConverter`1[[Cx.Interfaces.ICxComponent, Cx.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]");
				ObjectHandle hObj = Activator.CreateInstanceFrom(comp.Assembly, comp.ComponentName);
				inst = (ICxComponentClass)hObj.Unwrap();
			}
			else
			{
				inst = (ICxComponentClass)Activator.CreateInstance(comp.Type);
			}

			return inst;
		}

		protected Type GetImplementorType(CxComponent comp, Type componentType)
		{
			Assembly assy = Assembly.LoadFrom(comp.Assembly);
			Type t = CxCommon.FindImplementor(assy, comp.ComponentName, componentType);

			return t;
		}
	}
}

