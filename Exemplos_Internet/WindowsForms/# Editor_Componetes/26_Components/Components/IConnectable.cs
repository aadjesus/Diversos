using System;
using System.ComponentModel;

namespace Mvc.Components
{
	/// <summary>
	/// Interface to be implemented by all components that are 
	/// connected with a database through a connection string.
	/// </summary>
	/// <remarks>
	/// Models implementing this interface will get the containing <see cref="BaseController"/> 
	/// connection string property if available.
	/// </remarks>
	public interface IConnectable
	{
		string ConnectionString { get; set; }
	}
}
