using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Clifton.Tools.Strings;
							  
using Cx.Attributes;
using Cx.Exceptions;

namespace Cx.Common
{
	public static class CxCommon
	{
		/// <summary>
		/// Returns the type that implements the specified interface.  Only public class types are inspected.
		/// </summary>
		public static Type FindImplementor(Assembly assy, string componentName, Type targetInterface)
		{
			componentName = StringHelpers.LeftOf(componentName, '`');
			Type compType = null;

			IEnumerable<Type> cxComponents = from classType in assy.GetTypes()
											 where classType.IsClass && (classType.IsPublic || classType.IsNotPublic)
											 from classInterface in classType.GetInterfaces()
											 where classInterface == targetInterface
											 select classType;

			// Find the class of the specified component name.
			foreach (Type t in cxComponents)
			{
				object[] attributes = t.GetCustomAttributes(typeof(CxComponentNameAttribute), false);

				if (attributes.Length == 1)
				{
					if (((CxComponentNameAttribute)attributes[0]).ComponentName == componentName)
					{
						compType = t;
						break;
					}
				}
			}

			// If not found, let's try finding a class with the specified name.  This is a fallback for when we don't actually
			// need to name the component class using an attribute, which helps decouple our application code from the framework.
			foreach (Type t in cxComponents)
			{
				if (t.Name == componentName)
				{
					compType = t;
					break;
				}
			}

			Verify.IsNotNull(compType, "Could not find component in " + assy.FullName + " with the component name " + componentName);

			return compType;
		}
	}
}
