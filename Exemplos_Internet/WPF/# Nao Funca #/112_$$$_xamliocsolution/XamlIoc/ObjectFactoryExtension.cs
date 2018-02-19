using System;
using System.Windows;
using System.Windows.Markup;

namespace XamlIoc
{
	/// <summary>Markup Extension generating objects using <see cref="ObjectFactory"/>.</summary>
	[ContentProperty("Key")]
	public class ObjectFactoryExtension : MarkupExtension
	{
		private string key;

		/// <summary>Default constructor.</summary>
		public ObjectFactoryExtension()
		{
		}

		/// <summary>Constructor using a key.</summary>
		public ObjectFactoryExtension(string key)
		{
			this.key = key;
		}

		/// <summary>Object key to pass to <see cref="ObjectFactory"/>.</summary>
		[ConstructorArgument("key")]
		public string Key
		{
			get { return key; }
			set { key = value; }
		}

		/// <summary>Returns an instance from <see cref="ObjectFactory"/>.</summary>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			if (Key == null)
			{
				throw new ArgumentNullException("Key");
			}

			return ObjectFactory.GetObject(Key);
		}
	}
}