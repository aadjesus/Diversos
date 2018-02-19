using System;
using System.Configuration;

namespace XamlIoc.Configuration
{
	/// <summary>IOC configuration section.</summary>
	public class IocConfigSection : ConfigurationSection
	{
		/// <summary>Returns the global instance of the application.</summary>
		public static IocConfigSection Instance
		{
			get { return (IocConfigSection)ConfigurationManager.GetSection("iocConfigSection"); }
		}

		/// <summary><c>true</c> iif <see cref="MainContainer"/> is omitted.</summary>
		public bool UseApplicationResourceDictionary
		{
			get { return string.IsNullOrEmpty(MainContainer); }
		}

		/// <summary>Main container xaml file pack URI.</summary>
		/// <remarks>If omitted, the system falls back to the application's own resource directory.</remarks>
		[ConfigurationProperty("mainContainer")]
		public string MainContainer
		{
			get { return (string)this["mainContainer"]; }
		}

		/// <summary>Collection of stand-alone xaml files.</summary>
		[ConfigurationProperty("standAloneResources", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(IocStandAloneResourceConfigCollection), AddItemName = "standAloneResource")]
		public IocStandAloneResourceConfigCollection StandAloneResources
		{
			get { return (IocStandAloneResourceConfigCollection)this["standAloneResources"]; }
		}
	}
}