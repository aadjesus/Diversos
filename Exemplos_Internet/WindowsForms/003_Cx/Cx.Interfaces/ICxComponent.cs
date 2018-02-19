using System;

namespace Cx.Interfaces
{
	/// <summary>
	/// Base interface for a component instance.
	/// </summary>
	public interface ICxComponent
	{
		string Name { get; set; }
		string ComponentName { get; set; }
		string Assembly { get; set; }
		string AssemblyFilename { get; }
		Type Type { get; set; }
		ICxComponentClass Instance { get; }
	}

	/// <summary>
	/// Properties and methods specific to visual components.
	/// </summary>
	public interface ICxVisualComponent : ICxComponent
	{
		string Location { get; }
		string Size { get; }
	}

	/// <summary>
	/// Properties and methods specific to business components.
	/// </summary>
	public interface ICxBusinessComponent : ICxComponent
	{
		// None right now.
	}
}
