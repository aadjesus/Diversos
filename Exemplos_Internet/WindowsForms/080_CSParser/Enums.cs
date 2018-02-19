// Enums.cs: Different enumerations 
//
// Written by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)
using System;

namespace IvanZ.CSParser
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public enum Types
	{
		Class,
		Interface,
		Struct,
		Enum,
		Delegate
	}

	public enum Members
	{
		Constant,
		Field,
		Method,
		Property,
		Event,
		Indexer,
		Operator,
		Constructor,
		StaticConstructor,
		Destructor,
		NestedType
	}

	[Flags]
	public enum ModifierAttribs
	{
		// Access 
		Private   = 0x0001,
		Internal  = 0x0002,
		Protected = 0x0004,
		Public    = 0x0008,
	 
		// Scope
		Abstract  = 0x0010, 
		Virtual   = 0x0020,
		Sealed    = 0x0040,
		Static    = 0x0080,
		Override  = 0x0100,
		Readonly  = 0x0200,
		Const	  = 0X0400,
		New       = 0x0800,
	 	 
		// Special 
		Extern    = 0x1000,
		Volatile  = 0x2000,
		Unsafe    = 0x4000
	}

	public enum ParamModifiers
	{
		In,
		Out,
		Ref
	}

	[Flags]
	public enum AttributeTargets
	{
		Assembly=0x0001,
		Field=0x0002,
	    Event=0x0004,
	    Method=0x0008,
	    Module=0x0010,
	    Param=0x0020,
	    Property=0x0040,
	    Return=0x0080,
	    Type=0x0100 

	}
}
