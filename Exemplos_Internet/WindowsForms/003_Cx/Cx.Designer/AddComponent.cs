using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Cx.Attributes;
using Cx.EventArgs;
using Cx.Interfaces;

using Cx.Designer.Common;

namespace Cx.Designer
{
	[CxComponentName("AddComponent")]
	[CxExplicitEvent("ComponentListLoaded")]
	[CxExplicitEvent("CloseDialog")]
	[CxExplicitEvent("IsVisualComponent")]
	public class AddComponent : ICxBusinessComponentClass
	{
		public delegate void CxComponentPacketDlgt(object sender, CxEventArgs<ComponentPacket> args); 

		[CxEvent]
		public event CxComponentPacketDlgt AddComponentEvent;

		protected string Name { get; set; }
		protected string ComponentName { get; set; }
		protected string AssemblyName { get; set; }
		protected string Location { get; set; }
		protected string Size { get; set; }
		protected Dictionary<string, Type> componentNameTypeMap;

		protected EventHelper componentListLoaded;
		protected EventHelper closeDialog;
		protected EventHelper isVisualComponent;

		public AddComponent()
		{
			componentListLoaded = EventHelpers.CreateEvent<IEnumerable>(this, "ComponentListLoaded");
			closeDialog = EventHelpers.CreateEvent(this, "CloseDialog");
			isVisualComponent = EventHelpers.CreateEvent<bool>(this, "IsVisualComponent");
		}

		[CxConsumer]
		public void OnFilenameSet(object sender, CxEventArgs<string> args)
		{
			if (!String.IsNullOrEmpty(args.Data))
			{
				AssemblyName = args.Data;
				GetComponentList(AssemblyName);
			}
		}

		[CxConsumer]
		public void OnNameSet(object sender, CxEventArgs<string> args)
		{
			Name = args.Data;
		}

		[CxConsumer]
		public void OnComponentSelected(object sender, CxEventArgs<object> args)
		{
			ComponentName = args.Data.ToString();
			bool isVisualComp = (componentNameTypeMap[ComponentName].GetInterface(typeof(ICxVisualComponentClass).Name) != null);
			isVisualComponent.Fire(isVisualComp);
		}

		[CxConsumer]
		public void OnLocationSet(object sender, CxEventArgs<string> args)
		{
			Location = args.Data;
		}

		[CxConsumer]
		public void OnSizeSet(object sender, CxEventArgs<string> args)
		{
			Size = args.Data;
		}

		[CxConsumer]
		public void OnAddComponent(object sender, System.EventArgs args)
		{
			ComponentPacket cp = new ComponentPacket(Name, ComponentName, AssemblyName, componentNameTypeMap[ComponentName], Location, Size);
			EventHelpers.Fire(AddComponentEvent, this, new CxEventArgs<ComponentPacket>(cp));
			closeDialog.Fire();
		}

		protected void GetComponentList(string assyName)
		{
			List<string> componentNames = new List<string>();
			Assembly assy = Assembly.LoadFrom(assyName);

			IEnumerable<Type> cxComponents = from classType in assy.GetTypes()
											 where classType.IsClass && (classType.IsPublic || classType.IsNotPublic)
											 from classInterface in classType.GetInterfaces()
											 where classInterface == typeof(ICxComponentClass)
											 select classType;

			componentNameTypeMap = new Dictionary<string, Type>();
			// Find the class of the specified component name.
			foreach (Type t in cxComponents)
			{
				componentNames.Add(t.Name);
				componentNameTypeMap[t.Name] = t;
			}

			componentNames.Sort();
			componentListLoaded.Fire(componentNames);
		}
	}
}
