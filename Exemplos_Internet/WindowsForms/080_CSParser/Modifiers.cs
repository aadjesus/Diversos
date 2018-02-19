// Modifiers.cs : Contains utilities to work with modifiers
//
// Written by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)

using System;
using System.CodeDom;
using System.Reflection;

namespace IvanZ.CSParser
{
	
	
	/// <summary>
	/// Summary description for Modifiers.
	/// </summary>
	public class Modifiers
	{
		

		public static bool CheckTypeModifiers(ModifierAttribs a, bool classScope, Types type)
		{
			bool res= true;
			ModifierAttribs mask=ModifierAttribs.Extern|
				ModifierAttribs.Override|
				ModifierAttribs.Readonly|
				ModifierAttribs.Static|
				ModifierAttribs.Virtual|
				ModifierAttribs.Volatile;
			
			if (type!= Types.Class)
				mask|=ModifierAttribs.Abstract|
					ModifierAttribs.Sealed;
			if (type==Types.Enum)
				mask|=ModifierAttribs.Unsafe;
					
			if (!classScope)
			{
				mask |= ModifierAttribs.Private|
					ModifierAttribs.Protected;
					    
			}
			if ((a & mask)!=0) res=false;
			return res;
		}

		public static bool CheckMemberModifiers(ModifierAttribs a,  Members type)
		{
			bool res= true;
			ModifierAttribs mask=0;

			switch (type)
			{
				case Members.Constant:
					mask= ModifierAttribs.Abstract|
						ModifierAttribs.Extern|
						ModifierAttribs.Sealed|
						ModifierAttribs.Override|
						ModifierAttribs.Readonly|
						ModifierAttribs.Virtual|
						ModifierAttribs.Volatile|
						ModifierAttribs.Static|
						ModifierAttribs.Unsafe;
					break;
				case Members.Constructor:
					mask= ModifierAttribs.Abstract|
						ModifierAttribs.New|
						ModifierAttribs.Const|
						ModifierAttribs.Sealed|
						ModifierAttribs.Override|
						ModifierAttribs.Readonly|
						ModifierAttribs.Virtual|
						ModifierAttribs.Volatile;
					break;
				case Members.StaticConstructor:
					mask= ModifierAttribs.Abstract|
						ModifierAttribs.Protected|
						ModifierAttribs.Private|
						ModifierAttribs.Public|
						ModifierAttribs.Internal|
						ModifierAttribs.Sealed|
						ModifierAttribs.New|
						ModifierAttribs.Const|
						ModifierAttribs.Override|
						ModifierAttribs.Readonly|
						ModifierAttribs.Virtual|
						ModifierAttribs.Volatile;
					break;
				case Members.Destructor:
					mask= ModifierAttribs.Abstract|
						ModifierAttribs.Protected|
						ModifierAttribs.Private|
						ModifierAttribs.Public|
						ModifierAttribs.Internal|
						ModifierAttribs.Sealed|
						ModifierAttribs.Static|
						ModifierAttribs.New|
						ModifierAttribs.Const|
						ModifierAttribs.Override|
						ModifierAttribs.Readonly|
						ModifierAttribs.Virtual|
						ModifierAttribs.Volatile;
					break;
				case Members.Operator:
					mask= ModifierAttribs.Abstract|
						ModifierAttribs.Protected|
						ModifierAttribs.Private|
						ModifierAttribs.Internal|
						ModifierAttribs.Sealed|
						ModifierAttribs.New|
						ModifierAttribs.Const|
						ModifierAttribs.Override|
						ModifierAttribs.Readonly|
						ModifierAttribs.Virtual|
						ModifierAttribs.Volatile;
					break;
				case Members.Field:
					mask= ModifierAttribs.Abstract|
						ModifierAttribs.Const|
						ModifierAttribs.Sealed|
						ModifierAttribs.Extern|
						ModifierAttribs.Override|
						ModifierAttribs.Virtual;
						
					break;
				case Members.Indexer:
					mask= ModifierAttribs.Readonly|
						ModifierAttribs.Volatile|
						ModifierAttribs.Static|
						ModifierAttribs.Const;
					break;
				default:
					mask= ModifierAttribs.Readonly|
						ModifierAttribs.Volatile|
						ModifierAttribs.Const;
					break;
			}
					
			
			if ((a & mask)!=0) res=false;
			return res;
		}



		public static void SetElemModifiers(CodeTypeMember dec, ModifierAttribs a, bool classScope)
		{
			MemberAttributes ma=0;
			TypeAttributes ta =0;
			
			// access
			if ((a & ModifierAttribs.Public) != 0)
			{
				ma|= MemberAttributes.Public;
				if (classScope)
					ta|= TypeAttributes.NestedPublic;
				else
					ta|= TypeAttributes.Public;
			}
			else if ((a & ModifierAttribs.Private) != 0)
			{
				ma|= MemberAttributes.Private;
				if (classScope)
					ta|= TypeAttributes.NestedPrivate;
			}
			else if (((a & ModifierAttribs.Protected) != 0) && ((a & ModifierAttribs.Internal) != 0))
			{	
				ma|= MemberAttributes.FamilyOrAssembly;
				if (classScope)
					ta|= TypeAttributes.NestedFamORAssem;
			}
			else if ((a & ModifierAttribs.Protected) != 0)
			{
				ma|= MemberAttributes.Family;
				if (classScope)
					ta|= TypeAttributes.NestedFamily;
			}
			else if ((a & ModifierAttribs.Internal) != 0)
			{
				ma|= MemberAttributes.Assembly;
				if (classScope)
					ta|= TypeAttributes.NestedAssembly;
				else
					ta|= TypeAttributes.NotPublic;
			}

			// Probably will need more closer look what can be together and what not
			// abstact/sealed

			if ((a & ModifierAttribs.Abstract) != 0)
			{
				ma|= MemberAttributes.Abstract;
				ta|= TypeAttributes.Abstract;
			}
			else if ((a & ModifierAttribs.Sealed) != 0)
			{
				ma|= MemberAttributes.Final;
				ta|= TypeAttributes.Sealed;
			}
			
			// Scope
			if ((a & ModifierAttribs.New) != 0)
				ma|= MemberAttributes.New;
			if ((a & ModifierAttribs.Const) != 0)
				ma|= MemberAttributes.Const;
			else if ((a & ModifierAttribs.Static) != 0)
				ma|= MemberAttributes.Static;
			else if ((a & ModifierAttribs.Override) != 0)
				ma|= MemberAttributes.Override;
			else if (((a & ModifierAttribs.Virtual) == 0) &&
				     ((a & ModifierAttribs.Abstract) == 0))
				ma|= MemberAttributes.Final;
			
			
			
			//Other   
			if ((a & ModifierAttribs.Const) != 0)
				ma|= MemberAttributes.Const;
			

			// Unsafe, ReadOnly, Volatile, Extern?? - how to
		
			dec.Attributes=ma;
			if (dec is CodeTypeDeclaration)
				((CodeTypeDeclaration)dec).TypeAttributes=ta;
		
		}
	}
}
