using System;
using System.Configuration;

namespace XamlIoc.Configuration
{
	/// <summary>IOC stand alone configuration file collection.</summary>
	public class IocStandAloneResourceConfigCollection : ConfigurationElementCollection
	{
		/// <summary>Returns the account of a given name.</summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public new IocStandAloneResourceConfig this[string name]
		{
			get { return (IocStandAloneResourceConfig)BaseGet(name); }
		}

		/// <summary>Collection type.</summary>
		public override ConfigurationElementCollectionType CollectionType
		{
			get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new IocStandAloneResourceConfig();
		}

		protected override ConfigurationElement CreateNewElement(string elementName)
		{
			throw new NotSupportedException();
		}

		/// <summary>Defines the key of a <see cref="IocStandAloneResourceConfig"/> as its key.</summary>
		/// <param name="element"></param>
		/// <returns></returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((IocStandAloneResourceConfig)element).Key;
		}
	}
}