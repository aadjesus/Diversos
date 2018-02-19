using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Data;
using System.Reflection;

namespace Mvc.Components.Design
{
	/// <summary>
	/// Contains utility methods for type loading and reflection.
	/// </summary>
	internal class DesignUtils
	{
		/// <summary>
		/// Loads a type that is not declared as public (internal or private for example.
		/// </summary>
		/// <param name="className">The qualified name of the type to load.</param>
		/// <returns>The loaded type.</returns>
		/// <exception cref="ReflectionTypeLoadException">The received type couldn't be loaded.</exception>
		/// <example>
		/// The following code loads the private type associated with properties which are extended 
		/// at design-time by <see cref="System.ComponentModel.IExtenderProvider"/> components:
		/// <code>
		///		Type prop = DesignUtils.LoadPrivateType("System.ComponentModel.ExtendedPropertyDescriptor,System");
		/// </code>
		/// </example>
		/// <remarks>
		/// Use this feature sparingly because changes in the underlying .NET Framework implementation 
		/// may turn working code into failing one!
		/// </remarks>
		internal static Type LoadPrivateType(string className)
		{
			string[] names = className.Split(',');
			if (names.Length < 2) throw new ReflectionTypeLoadException(null,null,"Invalid class name: " + className);

			//Try to load className from the referenced assemblies. If it is not referenced, throw.
			AssemblyName[] asmNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
			Assembly asm = null;
			foreach (AssemblyName name in asmNames)
			{
				if (String.CompareOrdinal(name.Name, names[1]) == 0)
				{
					asm = Assembly.Load(name);
					break;
				}
			}
			if (asm == null) throw new ReflectionTypeLoadException(null,null,names[1] + " assembly couldn't be loaded.");

			//Locate the type. Iteration is required because it's not public.
			Type type = null;
			foreach (Type t in asm.GetTypes())
			{
				if (String.CompareOrdinal(t.FullName,names[0]) == 0)
				{
					type = t;
					break;
				}
			}

			if (type == null) throw new ReflectionTypeLoadException(null,null,className + " wasn't found.");

			return type;
		}

		/// <summary>
		/// Provides access to the hidden type property that allows us to access the actual 
		/// component providing a property, in case the edition is taking place from 
		/// within the control Property Browser.
		/// </summary>
		internal static PropertyInfo ProviderProperty = 
			DesignUtils.LoadPrivateType("System.ComponentModel.ExtendedPropertyDescriptor,System").GetProperty("Provider");

	}
}
