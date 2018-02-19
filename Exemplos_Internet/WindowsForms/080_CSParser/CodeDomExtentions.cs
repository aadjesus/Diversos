// CodeDomExtentions.cs: Contains extentions to CODEDOM to handle some advanced issues
//
// Written by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)

using System;
using System.CodeDom;
using System.Collections.Specialized;


namespace IvanZ.CSParser
{
	/// <summary>
	/// Summary description for CodeDomExtentions.
	/// </summary>
	public class CodeAttributeDeclarationExt: CodeAttributeDeclaration
	{
		public CodeAttributeDeclarationExt():base()
		{
			udata=new ListDictionary();
		}
		public CodeAttributeDeclarationExt(string name):base(name)
		{
			udata=new ListDictionary();
		}
		public CodeAttributeDeclarationExt(string name, params CodeAttributeArgument[] arguments):base(name, arguments)
		{
			udata=new ListDictionary();
		}

		private AttributeTargets targ=0;
		public AttributeTargets Target 
		{
			get 
			{
				return targ;
			}
			set
			{
				targ=value;
			}
		}

		private ListDictionary udata;
		public ListDictionary UserData
		{
			get 
			{
				return udata;
			}
		}


	}
}
