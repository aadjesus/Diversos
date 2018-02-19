using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

using Cx.Attributes;
using Cx.Common;
using Cx.Exceptions;
using Cx.EventArgs;
using Cx.Interfaces;

using Cx.Designer.Common;
using Cx.Designer.Interfaces;

namespace Cx.Designer
{
	public class CxProducer
	{
		public ICxComponent Component { get; protected set; }
		public string Name { get; protected set; }

		public CxProducer(ICxComponent comp, string name)
		{
			Component = comp;
			Name = name;
		}

		public override string ToString()
		{
			return Component.Name + "." + Name;
		}
	}

	public class CxConsumer
	{
		public ICxComponent Component { get; protected set; }
		public string Name { get; protected set; }

		public CxConsumer(ICxComponent comp, string name)
		{
			Component = comp;
			Name = name;
		}

		public override string ToString()
		{
			return Component.Name + "." + Name;
		}
	}

	/// <summary>
	/// Supports gathering information about components utilizing the Cx framework.
	/// For each component, gathers the names of the producer events and consumer methods.
	/// </summary>
	[CxComponentName("CxDesigner")]
	[CxExplicitEvent("ComponentListLoaded")]
	[CxExplicitEvent("ProducerListLoaded")]
	[CxExplicitEvent("ConsumerListLoaded")]
	[CxExplicitEvent("RequestLoadComponents")]		// TODO: Should be RequestLoadMetadata
	[CxExplicitEvent("ClearCheckedConsumers")]
	[CxExplicitEvent("CheckConsumer")]
	[CxExplicitEvent("AddComponent")]
	[CxExplicitEvent("EditProperties")]
	[CxExplicitEvent("OpenMetadata")]
	[CxExplicitEvent("OpenDeclaredEvents")]
	[CxExplicitEvent("SaveAs")]
	[CxExplicitEvent("FilenameSet")]
	[CxExplicitEvent("SelectedProperties")]
	[CxExplicitEvent("SelectedDeclaredEvents")]
	public class CxDesigner : ICxBusinessComponentClass
	{
		public delegate void MetadataDlgt(object sender, CxEventArgs<CxMetadata> args);
														
		[CxEvent]
		public event MetadataDlgt SaveMetadata;

		protected EventHelper componentListLoadedEvent;
		protected EventHelper producerListLoadedEvent;
		protected EventHelper consumerListLoadedEvent;
		protected EventHelper requestLoadComponents;
		protected EventHelper clearCheckedConsumers;
		protected EventHelper checkConsumer;
		protected EventHelper addComponent;
		protected EventHelper editProperties;
		protected EventHelper openMetadata;
		protected EventHelper openDeclaredEvents;
		protected EventHelper saveAs;
		protected EventHelper filenameSet;
		protected EventHelper selectedProperties;
		protected EventHelper selectedDeclaredEvents;

		protected List<ICxComponent> components;
		protected List<CxProducer> producers;
		protected List<CxConsumer> consumers;
		protected List<Wireup> wireups;
		protected DiagnosticDictionary<string, List<PropertyValue>> propertyValues;
		protected DiagnosticDictionary<string, List<DeclaredEvent>> declaredEvents;
		protected ICxDataService dataService;

		protected string metadataFilename;
		protected ICxComponent selectedComponent;

		public CxDesigner()
		{								  
			producers = new List<CxProducer>();
			consumers = new List<CxConsumer>();

			// Initialize blank collection so that we can start adding components to a blank slate.
			components = new List<ICxComponent>();
			wireups = new List<Wireup>();
			propertyValues = new DiagnosticDictionary<string, List<PropertyValue>>();
			declaredEvents = new DiagnosticDictionary<string, List<DeclaredEvent>>();

			componentListLoadedEvent = EventHelpers.CreateEvent<IEnumerable>(this, "ComponentListLoaded");
			producerListLoadedEvent = EventHelpers.CreateEvent<IEnumerable>(this, "ProducerListLoaded");
			consumerListLoadedEvent = EventHelpers.CreateEvent<IEnumerable>(this, "ConsumerListLoaded");
			requestLoadComponents = EventHelpers.CreateEvent<String>(this, "RequestLoadComponents");
			clearCheckedConsumers = EventHelpers.CreateEvent(this, "ClearCheckedConsumers");
			checkConsumer = EventHelpers.CreateEvent<object>(this, "CheckConsumer");
			addComponent = EventHelpers.CreateEvent<string>(this, "AddComponent");
			openMetadata = EventHelpers.CreateEvent<string>(this, "OpenMetadata");
			openDeclaredEvents = EventHelpers.CreateEvent<string>(this, "OpenDeclaredEvents");
			editProperties = EventHelpers.CreateEvent<string>(this, "EditProperties");
			saveAs = EventHelpers.CreateEvent<string>(this, "SaveAs");
			filenameSet = EventHelpers.CreateEvent<string>(this, "FilenameSet");
			selectedProperties = EventHelpers.CreateEvent<IEnumerable>(this, "SelectedProperties");
			selectedDeclaredEvents = EventHelpers.CreateEvent<IEnumerable>(this, "SelectedDeclaredEvents");
		}														 

		// We call it a Handler to avoid ambiguous match.
		[CxConsumer]
		public void InitializeDesigner(object sender, CxEventArgs<String> args)
		{
			metadataFilename = args.Data;
			requestLoadComponents.Fire(metadataFilename);
			FillInComponentTypes();
			BuildProducerConsumerList();
			FireListEvents();
			filenameSet.Fire(metadataFilename);
		}

		[CxConsumer]
		public void OnNewMetadata(object sender, System.EventArgs args)
		{
			metadataFilename = String.Empty;
			filenameSet.Fire(metadataFilename);
			ClearModel();
		}

		[CxConsumer]
		public void OnComponentsLoaded(object sender, CxEventArgs<IEnumerable> args)
		{
			components = (List<ICxComponent>)args.Data;
		}

		[CxConsumer]
		public void OnWireupsLoaded(object sender, CxEventArgs<IEnumerable> args)
		{
			wireups = (List<Wireup>)args.Data;
		}

		[CxConsumer]
		public void OnPropertyValuesLoaded(object sender, CxEventArgs<IEnumerable> args)
		{
			propertyValues = (DiagnosticDictionary<string, List<PropertyValue>>)args.Data;
		}

		[CxConsumer]
		public void OnDeclaredEventsLoaded(object sender, CxEventArgs<IEnumerable> args)
		{
			declaredEvents=(DiagnosticDictionary<string, List<DeclaredEvent>>)args.Data;
		}

		// ============= THIS OUGHT TO GO INTO A CONTROLLER =================

		protected CxProducer selectedProducer = null;

		[CxConsumer]
		public void OnProducerSelected(object sender, CxEventArgs<object> args)
		{
			CxProducer prod = (CxProducer)args.Data;
			string prodFullName = prod.Component.Name + "." + prod.Name;

			// Clear the selected producer so we can update the current list without processing check state events.
			selectedProducer = null;
			clearCheckedConsumers.Fire();

			foreach (Wireup wireup in wireups)
			{
				if (wireup.Producer == prodFullName)
				{
					foreach(CxConsumer consumer in consumers)
					{
						string consFullName=consumer.Component.Name+"."+consumer.Name;

						if (wireup.Consumer == consFullName)
						{
							checkConsumer.Fire(consumer);
							break;
						}
					}
				}
			}

			// Now we're ready to process check state events from the user.
			selectedProducer = prod;
		}

		[CxConsumer]
		public void OnItemChecked(object sender, CxEventArgs<CxObjectState> args)
		{
			if (selectedProducer != null)
			{
				CxConsumer consumer = (CxConsumer)args.Data.Object;																	    
				string producerName = selectedProducer.Component.Name + "." + selectedProducer.Name;
				string consumerName = consumer.Component.Name + "." + consumer.Name;

				if (args.Data.State)
				{
					// Consumer selected.
					wireups.Add(new Wireup(producerName, consumerName));
				}
				else
				{
					// Consumer unselected.
					foreach (Wireup wireup in wireups)
					{
						if ((wireup.Producer == producerName) && (wireup.Consumer == consumerName))
						{
							wireups.Remove(wireup);
							break;
						}
					}
				}
			}
		}

		[CxConsumer]
		public void OnSave(object sender, System.EventArgs args)
		{
			SaveModel();
		}

		[CxConsumer]
		public void OnSaveFilename(object sender, CxEventArgs<string> args)
		{
			metadataFilename=args.Data;
			filenameSet.Fire(metadataFilename);
			SaveModel();
		}

		[CxConsumer]
		public void OnSaveAs(object sender, System.EventArgs args)
		{
			OpenSaveAsDialog();
		}

		[CxConsumer]
		public void OnAddComponentButton(object sender, System.EventArgs args)
		{
			addComponent.Fire("addComponent.xml");
		}

		[CxConsumer]
		public void OnEditPropertiesButton(object sender, System.EventArgs args)
		{
			editProperties.Fire("editProperties.xml");
		}

		[CxConsumer]
		public void OnUpdatePropertyList(object sender, CxEventArgs<IEnumerable> args)
		{
			if (selectedComponent != null)
			{
				propertyValues[selectedComponent.Name] = new List<PropertyValue>((IEnumerable<PropertyValue>)args.Data);
			}
		}

		[CxConsumer]
		public void OnUpdateDeclaredEventList(object sender, CxEventArgs<IEnumerable> args)
		{
			if (selectedComponent != null)
			{
				declaredEvents[selectedComponent.Name] = new List<DeclaredEvent>((IEnumerable<DeclaredEvent>)args.Data);
			}
		}

		[CxConsumer]
		public void OnOpenMetadata(object sender, System.EventArgs args)
		{
			openMetadata.Fire("openMetadata.xml");
		}

		[CxConsumer]
		public void OnAddComponent(object sender, CxEventArgs<ComponentPacket> args)
		{
			ICxComponent comp = null;

			if (args.Data.ComponentType.GetInterface(typeof(ICxVisualComponentClass).Name) != null)
			{
				// Visual component.
				comp = new CxVisualComponent();
				((CxVisualComponent)comp).Location = args.Data.Location;
				((CxVisualComponent)comp).Size = args.Data.Size;
			}
			else
			{
				// Business component.
				comp = new CxBusinessComponent();
			}

			comp.Name = args.Data.Name;
			comp.ComponentName = args.Data.ComponentName;
			comp.Assembly = args.Data.AssemblyName;
			comp.Type = args.Data.ComponentType;
			components.Add(comp);
			BuildProducerConsumerList();
			FireListEvents();
		}

		/// <summary>
		/// When a component is selected, we want to send off the list of properties and declared events associated with the component.
		/// </summary>
		[CxConsumer]
		public void OnComponentSelected(object sender, CxEventArgs<object> args)
		{
			selectedComponent = (ICxComponent)args.Data;
			List<PropertyValue> propVals = new List<PropertyValue>();
			List<DeclaredEvent> decEvents = new List<DeclaredEvent>();

			propertyValues.TryGetValue(selectedComponent.Name, ref propVals);
			selectedProperties.Fire(new List<PropertyValue>(propVals));

			declaredEvents.TryGetValue(selectedComponent.Name, ref decEvents);
			selectedDeclaredEvents.Fire(new List<DeclaredEvent>(decEvents));
		}

		[CxConsumer]
		public void OnGetPropertyList(object sender, System.EventArgs args)
		{
			if (selectedComponent != null)
			{
				// Create an empty list so that the we fire the event regardless.
				List<PropertyValue> propVals=new List<PropertyValue>();

				propertyValues.TryGetValue(selectedComponent.Name, ref propVals);
				selectedProperties.Fire(new List<PropertyValue>(propVals));
			}
		}

		[CxConsumer]
		public void OnGetDeclaredEventList(object sender, System.EventArgs args)
		{
			if (selectedComponent != null)
			{
				List<DeclaredEvent> decEvents = new List<DeclaredEvent>();

				declaredEvents.TryGetValue(selectedComponent.Name, ref decEvents);
				selectedDeclaredEvents.Fire(new List<DeclaredEvent>(decEvents));
			}
		}

		[CxConsumer]
		public void OnOpenDeclaredEvents(object sender, System.EventArgs args)
		{
			openDeclaredEvents.Fire("declaredEvents.xml");
		}

		// ==================================================================

		protected void SaveModel()
		{
			if (!String.IsNullOrEmpty(metadataFilename))
			{
				CxMetadata package = new CxMetadata(metadataFilename, components, wireups, propertyValues, declaredEvents);
				EventHelpers.Fire(SaveMetadata, this, new CxEventArgs<CxMetadata>(package));
			}
			else
			{
				OpenSaveAsDialog();
			}
		}

		protected void OpenSaveAsDialog()
		{
			saveAs.Fire("xml files (*.xml)|*.xml|all files (*.*)|*.*");
		}

		protected void ClearModel()
		{
			components.Clear();
			wireups.Clear();
			propertyValues.Clear();
			producers.Clear();
			consumers.Clear();
			FireListEvents();
		}
		
		protected void FireListEvents()
		{
			componentListLoadedEvent.Fire(new List<ICxComponent>(components));
			producerListLoadedEvent.Fire(producers);
			consumerListLoadedEvent.Fire(consumers);
		}

		protected void FillInComponentTypes()
		{
			foreach (CxComponent comp in components)
			{
				comp.Type = GetImplementorType(comp, typeof(ICxComponentClass));
			}
		}

		protected Type GetImplementorType(CxComponent comp, Type componentType)
		{
			Assembly assy = Assembly.LoadFrom(comp.Assembly);
			Type t = CxCommon.FindImplementor(assy, comp.ComponentName, componentType);

			return t;
		}

		/// <summary>
		/// Builds the list of producers (events) and consumers (methods) for each component.
		/// </summary>
		protected void BuildProducerConsumerList()
		{
			producers.Clear();
			consumers.Clear();

			// We know that all component types are classes.
			foreach (CxComponent comp in components)
			{
				List<CxProducer> explicitEvents = DiscoverExplicitEvents(comp);
				List<CxProducer> attributedEvents = DiscoverAttributedEvents(comp);
				List<CxProducer> declaredEvents = DiscoverDeclaredEvents(comp);
				List<CxConsumer> compConsumers = DiscoverConsumers(comp);
				producers.AddRange(explicitEvents);
				producers.AddRange(attributedEvents);
				producers.AddRange(declaredEvents);
				consumers.AddRange(compConsumers);
			}						  
		}

		/// <summary>
		/// Returns a list of explicitly defined (as attributes of the class) events that the component wants to expose.
		/// We use this for handling events of controls contained by the component and also automatic event transformations.
		/// These are enumerated by class-level CxExplicitEvent attributes.
		/// </summary>
		protected List<CxProducer> DiscoverExplicitEvents(CxComponent comp)
		{
			List<CxProducer> events = new List<CxProducer>();

			// We want to inherit events explicitly defined in base classes, therefore the "true".
			foreach(CxExplicitEventAttribute attr in comp.Type.GetCustomAttributes(typeof(CxExplicitEventAttribute), true))
			{
				events.Add(new CxProducer(comp, attr.EventName));
			}

			return events;			 
		}

		/// <summary>
		/// Returns a list of events defined in the component themselves, decorated with the CxEvent attribute.
		/// </summary>
		protected List<CxProducer> DiscoverAttributedEvents(CxComponent comp)
		{
			List<CxProducer> events = new List<CxProducer>();

			foreach (EventInfo ei in comp.Type.GetEvents())
			{
				object[] attrs = ei.GetCustomAttributes(typeof(CxEventAttribute), false);

				if (attrs.Length == 1)
				{
					events.Add(new CxProducer(comp, ei.Name));
				}
			}

			return events;
		}

		protected List<CxProducer> DiscoverDeclaredEvents(CxComponent comp)
		{
			List<CxProducer> events = new List<CxProducer>();
			List<DeclaredEvent> decEvents;

			if (declaredEvents.TryGetValue(comp.Name, out decEvents))
			{
				foreach (DeclaredEvent ev in decEvents)
				{
					events.Add(new CxProducer(comp, ev.Name));
				}
			}

			return events;
		}

		/// <summary>
		/// Returns a list of method names for component methods decorated with the CxConsumer attribute.
		/// </summary>
		protected List<CxConsumer> DiscoverConsumers(CxComponent comp)
		{
			List<CxConsumer> consumers = new List<CxConsumer>();

			foreach (MethodInfo mi in comp.Type.GetMethods())
			{
				object[] attrs = mi.GetCustomAttributes(typeof(CxConsumerAttribute), false);

				if (attrs.Length == 1)
				{
					consumers.Add(new CxConsumer(comp, mi.Name));
				}
			}

			return consumers;
		}
	}
}
