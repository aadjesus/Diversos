using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

using Cx.Attributes;
using Cx.Common;
using Cx.EventArgs;
using Cx.Exceptions;
using Cx.Interfaces;

namespace Cx.DataService
{
	[CxComponentName("CxDataService")]
	[CxExplicitEvent("ComponentsLoaded")]
	[CxExplicitEvent("WireupsLoaded")]
	[CxExplicitEvent("PropertyValuesLoaded")]
	[CxExplicitEvent("ExplicitEventsLoaded")]
	public class CxDataService : ICxBusinessComponentClass, ICxDataService
	{
		public DiagnosticDictionary<string, ICxComponent> Components { get; protected set; }
		public List<Wireup> Wireups { get; protected set; }
		public DiagnosticDictionary<string, List<PropertyValue>> PropertyValues { get; protected set; }
		public DiagnosticDictionary<string, List<DeclaredEvent>> DeclaredEvents { get; protected set; }

		protected XDocument xdoc;
		protected EventHelper componentsLoaded;
		protected EventHelper wireupsLoaded;
		protected EventHelper propertyValuesLoaded;
		protected EventHelper declaredEventsLoaded;

		public CxDataService()
		{
			componentsLoaded = EventHelpers.CreateEvent<IEnumerable>(this, "ComponentsLoaded");
			wireupsLoaded = EventHelpers.CreateEvent<IEnumerable>(this, "WireupsLoaded");
			propertyValuesLoaded = EventHelpers.CreateEvent<IEnumerable>(this, "PropertyValuesLoaded");
			declaredEventsLoaded = EventHelpers.CreateEvent<IEnumerable>(this, "DeclaredEventsLoaded");
		}
		
		[CxConsumer]
		public void OnLoadComponents(object sender, CxEventArgs<string> args)
		{
			Init();
			LoadXml(args.Data);
			LoadConfigurationMetadata();
		}

		[CxConsumer]
		public void OnSaveMetadata(object sender, CxEventArgs<CxMetadata> args)
		{
			xdoc = new XDocument(new XElement("Cx"));
			SaveComponents(xdoc.Root, args.Data.ComponentList);
			SaveWireups(xdoc.Root, args.Data.Wireups);
			SavePropertyValues(xdoc.Root, args.Data.PropertyValues);
			SaveDeclaredEvents(xdoc.Root, args.Data.DeclaredEvents);
			xdoc.Save(args.Data.Uri);
		}

		public void LoadComponents(string uri)
		{
			Init();
			LoadXml(uri);
			LoadConfigurationMetadata();
		}

		protected void SaveComponents(XElement root, List<ICxComponent> list)
		{
			XElement components = new XElement("Components");
			root.Add(components);

			// serialize visual components first.
			foreach (ICxComponent comp in list)
			{
				if (comp is ICxVisualComponent)
				{
					ICxVisualComponent visComp = (ICxVisualComponent)comp;
					XElement elComp = new XElement("VisualComponent");
					components.Add(elComp);
					elComp.Add(
						new XAttribute("Name", visComp.Name), 
						new XAttribute("ComponentName", visComp.ComponentName),
						new XAttribute("Assembly", visComp.Assembly));

					if (visComp.Location != null)
					{
						elComp.Add(new XAttribute("Location", visComp.Location));
					}

					if (visComp.Size != null)
					{
						elComp.Add(new XAttribute("Size", visComp.Size));
					}
				}
			}

			// serialize business components next.
			foreach (ICxComponent comp in list)
			{
				if (comp is ICxBusinessComponent)
				{
					ICxBusinessComponent busComp = (ICxBusinessComponent)comp;
					XElement elComp = new XElement("BusinessComponent");
					components.Add(elComp);
					elComp.Add(
						new XAttribute("Name", busComp.Name),
						new XAttribute("ComponentName", busComp.ComponentName),
						new XAttribute("Assembly", busComp.Assembly)
						);
				}
			}
		}

		protected void SaveWireups(XElement root, List<Wireup> wireups)
		{
			XElement elWireups = new XElement("WireUps");
			root.Add(elWireups);

			foreach (Wireup wireup in wireups)
			{
				XElement elWireup=new XElement("WireUp");
				elWireups.Add(elWireup);
				elWireup.Add(
					new XAttribute("Producer", wireup.Producer),
					new XAttribute("Consumer", wireup.Consumer)
					);
			}
		}

		protected void SavePropertyValues(XElement root, DiagnosticDictionary<string, List<PropertyValue>> propVals)
		{
			foreach (KeyValuePair<string, List<PropertyValue>> kvp in propVals)
			{
				XElement elProps = new XElement("Properties");
				elProps.Add(new XAttribute("ComponentName", kvp.Key));
				root.Add(elProps);

				foreach (PropertyValue pv in kvp.Value)
				{
					XElement propVal = new XElement("Property");
					elProps.Add(propVal);
					propVal.Add(new XAttribute("Name", pv.Name));

					if (pv.ItemValues.Count == 0)
					{
						propVal.Add(new XAttribute("Value", pv.Value));
					}
					else
					{
						foreach(ItemValue val in pv.ItemValues)
						{
							XElement elItem = new XElement("Item");
							elItem.Add(new XAttribute("Value", val.Text));
							propVal.Add(elItem);
						}
					}
				}
			}
		}

		protected void SaveDeclaredEvents(XElement root, DiagnosticDictionary<string, List<DeclaredEvent>> declaredEvents)
		{
			foreach (KeyValuePair<string, List<DeclaredEvent>> kvp in declaredEvents)
			{
				XElement elEvents = new XElement("DeclaredEvents");
				elEvents.Add(new XAttribute("ProducerName", kvp.Key));
				root.Add(elEvents);

				foreach (DeclaredEvent pv in kvp.Value)
				{
					XElement evVal = new XElement("Event");
					elEvents.Add(evVal);
					evVal.Add(new XAttribute("Name", pv.Name));
				}
			}
		}

		protected virtual void LoadConfigurationMetadata()
		{
			LoadVisualComponents();
			LoadBusinessComponents();
			LoadWireups();
			LoadProperties();
			LoadDeclaredEvents();
			componentsLoaded.Fire(new DiagnosticDictionary<string, ICxComponent>(Components));
			wireupsLoaded.Fire(new List<Wireup>(Wireups));
			propertyValuesLoaded.Fire(new DiagnosticDictionary<string, List<PropertyValue>>(PropertyValues));
			declaredEventsLoaded.Fire(new DiagnosticDictionary<string, List<DeclaredEvent>>(DeclaredEvents));
		}

		protected virtual void Init()
		{
			Components = new DiagnosticDictionary<string, ICxComponent>();
			Wireups = new List<Wireup>();
			PropertyValues = new DiagnosticDictionary<string, List<PropertyValue>>();
			DeclaredEvents = new DiagnosticDictionary<string, List<DeclaredEvent>>();
		}

		/// <summary>
		/// Specifies the configuration XML file.
		/// </summary>
		protected void LoadXml(string filename)
		{
            filename = @"c:\Users\alessandro.augusto\Documents\Visual Studio\Exemplos_Internet\WindowsForms\003_Cx\UpgradeLog.XML";
			xdoc = XDocument.Load(filename);
		}

		/// <summary>
		/// Load the visual components, acquiring the component type.  Does not instantiate the component.
		/// </summary>
		protected void LoadVisualComponents()
		{
			Verify.IsNotNull(xdoc, "Configuration XML file must be loaded before loading visual components.");

			foreach (CxVisualComponent comp in from el in xdoc.Root.Elements("Components").Elements("VisualComponent")
											   select new CxVisualComponent()
											   {
												   Name = el.Attribute("Name").Value,
												   ComponentName = el.Attribute("ComponentName").Value,
												   Assembly = el.Attribute("Assembly").Value,
												   Location = (el.Attribute("Location")==null ? null : el.Attribute("Location").Value),
												   Size = (el.Attribute("Size") == null ? null : el.Attribute("Size").Value),
											   })
			{
				Verify.IsNotNull(comp.Name, "Name attribute is missing from the component definition.");
				Verify.IsNotNull(comp.ComponentName, "ComponentName attribute is missing from the component definition.");
				Verify.IsNotNull(comp.Assembly, "Assembly attribute is missing from the component definition.");
				// menus don't have locations, and some controls may not have a size.
				Verify.ValNotIn(Components.Keys, comp.Name, "The instance name " + comp.Name + " cannot be used twice.");

				AddComponent(comp);
			}
		}

		/// <summary>
		/// Load the business components, acquiring the component type.  Does not instantiate the component.
		/// </summary>
		protected void LoadBusinessComponents()
		{
			Verify.IsNotNull(xdoc, "Configuration XML file must be loaded before loading business components.");

			foreach (CxBusinessComponent comp in from el in xdoc.Root.Elements("Components").Elements("BusinessComponent")
												 select new CxBusinessComponent()
												 {
													 Name = el.Attribute("Name").Value,
													 ComponentName = el.Attribute("ComponentName").Value,
													 Assembly = el.Attribute("Assembly").Value,
												 })
			{
				Verify.IsNotNull(comp.Name, "Name attribute is missing from the component definition.");
				Verify.IsNotNull(comp.ComponentName, "ComponentName attribute is missing from the component definition.");
				Verify.IsNotNull(comp.Assembly, "Assembly attribute is missing from the component definition.");
				Verify.ValNotIn(Components.Keys, comp.Name, "The instance name " + comp.Name + " cannot be used twice.");

				AddComponent(comp);			
			}
		}

		protected virtual void AddComponent(ICxComponent comp)
		{
			Components[comp.Name] = comp;
		}

		protected void LoadWireups()
		{
			Verify.IsNotNull(xdoc, "Configuration XML file must be loaded before loading wireups.");

			Wireups.AddRange(from el in xdoc.Root.Elements("WireUps").Elements("WireUp")
							 select new Wireup
							 {
								 Producer = el.Attribute("Producer").Value,
								 Consumer = el.Attribute("Consumer").Value,
							 });
		}

		protected void LoadProperties()
		{
			Verify.IsNotNull(xdoc, "Configuration XML file must be loaded before loading properties.");

			foreach (var property in from el in xdoc.Root.Elements("Properties")
									 select new
									 {
										 ComponentName = el.Attribute("ComponentName").Value,
										 Property = el.Elements("Property")
									 })
			{
				List<PropertyValue> propValues = new List<PropertyValue>();
				propValues.AddRange(from el2 in property.Property
									select new PropertyValue
									{
										ComponentName = property.ComponentName,
										Name = el2.Attribute("Name").Value,
										Value = (el2.Attribute("Value") == null ? null : el2.Attribute("Value").Value),
										ItemValues = el2.Elements("Item") == null ? null : new List<ItemValue>(from el3 in el2.Elements("Item") select new ItemValue { Text = el3.Attribute("Value").Value }),
									});

				AddPropertyValue(property.ComponentName, propValues);
			}
		}

		protected void LoadDeclaredEvents()
		{
			Verify.IsNotNull(xdoc, "Configuration XML file must be loaded before loading properties.");

			foreach (var ev in from el in xdoc.Root.Elements("DeclaredEvents")
									 select new
									 {
										 ProducerName = el.Attribute("ProducerName").Value,
										 Event = el.Elements("Event")
									 })
			{
				List<DeclaredEvent> eventValues = new List<DeclaredEvent>();
				eventValues.AddRange(from el2 in ev.Event
									select new DeclaredEvent
									{
										Name = el2.Attribute("Name").Value,
									});

				AddDeclaredEventValue(ev.ProducerName, eventValues);
			}
		}

		protected virtual void AddPropertyValue(string compName, List<PropertyValue> propVals)
		{
			PropertyValues[compName] = propVals;
		}

		protected virtual void AddDeclaredEventValue(string producerName, List<DeclaredEvent> events)
		{
			DeclaredEvents[producerName] = events;
		}
	}

	[CxComponentName("CxDesignerDataService")]
	[CxExplicitEvent("ComponentListLoaded")]
	public class CxDesignerDataService : CxDataService
	{
		public List<ICxComponent> ComponentList { get; set; }

		protected EventHelper componentListLoaded;

		public CxDesignerDataService()
		{
			componentListLoaded = EventHelpers.CreateEvent<IEnumerable>(this, "ComponentListLoaded");
		}

		protected override void Init()
		{
			base.Init();
			ComponentList = new List<ICxComponent>();
		}

		protected override void AddComponent(ICxComponent comp)
		{
			ComponentList.Add(comp);
		}

		protected override void LoadConfigurationMetadata()
		{
			base.LoadConfigurationMetadata();
			componentListLoaded.Fire(new List<ICxComponent>(ComponentList));
		}
	}
}
