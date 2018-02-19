using System;
using System.Collections.Generic;
using System.Windows;

using XamlIoc.Configuration;

namespace XamlIoc
{
	/// <summary>
	/// Object factory.
	/// Pulling objects from the resource dictionary defined by configuration.
	/// </summary>
	public static class ObjectFactory
	{
		private static readonly ResourceDictionary resourceDictionary;
		private static readonly IDictionary<string, StandAloneResource> standAloneMap;

		/// <summary>Static constructor:  initializing static fields.</summary>
		static ObjectFactory()
		{
			resourceDictionary = GetConfiguredResourceDictionary();
			standAloneMap = GetStandAloneMap();
		}

		/// <summary>Returns an object of type <paramref name="T"/> from the resource dictionary.</summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public static T GetObject<T>(string key)
		{
			return (T)GetObject(key);
		}

		/// <summary>Returns an object from the resource dictionary.</summary>
		/// <remarks>Stand alone resources are tried after the resource dictionary.</remarks>
		/// <param name="key"></param>
		/// <returns></returns>
		public static object GetObject(string key)
		{
			object obj = resourceDictionary[key];

			if (obj == null)
			{
				if (!standAloneMap.ContainsKey(key))
				{
					throw new ApplicationException("Couldn't find the key " + key + " in neither the resource dictionary or stand alone resources.");
				}
				else
				{
					return standAloneMap[key].GetObject();
				}
			}

			ObjectContainer container = obj as ObjectContainer;

			if (container != null)
			{
				return container.Child;
			}
			else
			{
				return obj;
			}
		}

		/// <summary>Fetch the <see cref="ResourceDictionary"/> defined in configuration file.</summary>
		/// <returns></returns>
		private static ResourceDictionary GetConfiguredResourceDictionary()
		{
			if (IocConfigSection.Instance.UseApplicationResourceDictionary)
			{
				if (Application.Current == null)
				{
					throw new ApplicationException("No mainContainer was specified.  Object factory falls back to the application resource directory, but no application has been instanciated.");
				}
				else
				{
					return Application.Current.Resources;
				}
			}
			else
			{
				string resourceDictionaryPath = IocConfigSection.Instance.MainContainer;
				Uri resourceLocater = new System.Uri(resourceDictionaryPath, UriKind.Relative);
				ResourceDictionary resourceDictionary =
					Application.LoadComponent(resourceLocater) as ResourceDictionary;

				if (resourceDictionary == null)
				{
					throw new ApplicationException(resourceDictionaryPath + " isn't a ResourceDictionary.");
				}
				else
				{
					return resourceDictionary;
				}
			}
		}

		/// <summary>Prepares the stand alone map.</summary>
		/// <returns></returns>
		private static IDictionary<string, StandAloneResource> GetStandAloneMap()
		{
			IDictionary<string, StandAloneResource> standAloneMap = new Dictionary<string, StandAloneResource>();

			foreach (IocStandAloneResourceConfig config in IocConfigSection.Instance.StandAloneResources)
			{
				standAloneMap.Add(config.Key, new StandAloneResource(config.Uri, config.IsShared));
			}

			return standAloneMap;
		}
	}
}