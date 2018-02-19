using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Markup;

namespace XamlIoc
{
	/// <summary>Extension creating a generic type.</summary>
	/// <remarks>
	/// Largely inspired on http://blogs.msdn.com/mikehillberg/archive/2006/10/06/LimitedGenericsSupportInXaml.aspx.
	/// </remarks>
	public class GenericExtension : MarkupExtension
	{
		private Type[] typeArguments;
		private string typeName;
		private readonly Dictionary<string, object> propertyDictionary = new Dictionary<string, object>();

		/// <summary>First type arguments of <see cref="TypeArguments"/>.</summary>
		/// <remarks>Shortcut for <see cref="TypeArguments"/>, allow to bypass the creation of an array in XAML.</remarks>
		public Type FirstTypeArgument
		{
			get { return typeArguments == null || typeArguments.Length == 0 ? null : typeArguments[0]; }
			set { typeArguments = new Type[] { value }; }
		}

		/// <summary>Type arguments on the generic type.</summary>
		public Type[] TypeArguments
		{
			get { return typeArguments; }
			set { typeArguments = value; }
		}

		/// <summary>Generic type name to create.</summary>
		public string TypeName
		{
			get { return typeName; }
			set { typeName = value; }
		}

		/// <summary>Dictionary of properties.</summary>
		/// <remarks>Maps property name of the generic type to their value.</remarks>
		public Dictionary<string, object> PropertyDictionary
		{
			get { return propertyDictionary; }
		}

		/// <summary>Returns a final type instance.</summary>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			Type genericType = GetGenericType(serviceProvider);
			Type finalType = genericType.MakeGenericType(new List<Type>(typeArguments).ToArray());
			object obj = Activator.CreateInstance(finalType);

			PopulateObject(obj);

			return obj;
		}

		/// <summary>Sets some properties of the object.</summary>
		/// <remarks>Default implementation:  sets the properties defined in <see cref="PropertyDictionary"/>.</remarks>
		/// <param name="obj"></param>
		protected virtual void PopulateObject(object obj)
		{
			if (PropertyDictionary.Count != 0)
			{
				Type type = obj.GetType();

				foreach (KeyValuePair<string, object> pair in PropertyDictionary)
				{
					string propertyName = pair.Key;
					object value = pair.Value;
					PropertyInfo info = type.GetProperty(propertyName);

					if (info == null)
					{
						throw new ApplicationException(type.FullName + " doesn't contain property " + propertyName);
					}
					else if (!info.CanWrite)
					{
						throw new ApplicationException(type.FullName + " contains property " + propertyName + " but the property can be written to.");
					}
					else
					{
						info.SetValue(obj, value, null);
					}
				}
			}
		}

		/// <summary>Generic type to create.</summary>
		private Type GetGenericType(IServiceProvider serviceProvider)
		{
			IXamlTypeResolver xamlTypeResolver =
				serviceProvider.GetService(typeof(IXamlTypeResolver)) as IXamlTypeResolver;

			if (xamlTypeResolver == null)
			{
				throw new Exception("The Generic markup extension requires an IXamlTypeResolver service provider");
			}
			else
			{
				//	Get e.g. "Collection`1" type
				Type genericType =
					xamlTypeResolver.Resolve(string.Concat(TypeName, "`", TypeArguments.Length.ToString()));

				return genericType;
			}
		}
	}
}