using System;
using System.Windows.Markup;

namespace XamlIoc
{
	/// <summary>Hold any type of object.</summary>
	/// <remarks>
	/// Typically used in a resource dictionary in XAML, since markup extensions are not expended in a dictionary ;
	/// the child of the container is expended though.
	/// </remarks>
	[ContentProperty("Child")]
	public class ObjectContainer
	{
		private object child;

		/// <summary>Contained child.</summary>
		public object Child
		{
			get { return child; }
			set { child = value; }
		}
	}
}