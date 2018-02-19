using System;

namespace Cx.Interfaces
{
	/// <summary>
	/// Base class for defining a class as a component.  Should not be
	/// directly inherited from a component class.
	/// </summary>
	public interface ICxComponentClass
	{
	}

	/// <summary>
	/// Any class inheriting this interface is a visual component.
	/// </summary>
	public interface ICxVisualComponentClass : ICxComponentClass
	{
		void Register(object form, ICxVisualComponent component);
	}

	/// <summary>
	/// Any class inheriting this interface is a business component.
	/// </summary>
	public interface ICxBusinessComponentClass : ICxComponentClass
	{
	}
}
