using System;
using System.Configuration;

namespace XamlIoc.Configuration
{
	/// <summary>IOC stand alone file configuration.</summary>
	public class IocStandAloneResourceConfig : ConfigurationElement
	{
		/// <summary>Key name of the file.</summary>
		/// <remarks>This string is going to be used when retrieving instance of this object.</remarks>
		[ConfigurationProperty("key", IsRequired = true)]
		public string Key
		{
			get { return (string)this["key"]; }
		}

		/// <summary>Uri of the file.</summary>
		[ConfigurationProperty("uri", IsRequired = true)]
		public Uri Uri
		{
			get { return (Uri)this["uri"]; }
		}

		/// <summary>Is the object represented by the XAML file to be shared or not.</summary>
		[ConfigurationProperty("isShared", IsRequired = false, DefaultValue=true)]
		public bool IsShared
		{
			get { return (bool)this["isShared"]; }
		}
	}
}