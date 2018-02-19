// SupportTypes.cs: Contains different additional classes, which agregate some for 
//CODEDOM object creation 
//
// Written by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)
using System;
using System.CodeDom;

namespace IvanZ.CSParser
{
	/// <summary>
	/// Summary description for SupportTypes.
	/// </summary>
	public class IndexerDecl
	{
		
		public string Name;
		public CodeParameterDeclarationExpressionCollection Params;
		public CodeTypeReference Type;
		public IndexerDecl(string name,
							CodeParameterDeclarationExpressionCollection pars,
							CodeTypeReference type)
		{
			Name=name;
			Params=pars;
			Type=type;
		}
	}

	public class ConstructorInit
	{
		public bool IsBase;
		public CodeExpressionCollection Params;

		public ConstructorInit(bool isBase, CodeExpressionCollection pars)
		{
			IsBase=isBase;
			Params=pars;
		}

	}

	public class ConstructorDecl
	{
		public string Name;
		public CodeParameterDeclarationExpressionCollection Params;
		public ConstructorInit Initializer;

		public ConstructorDecl(string name, 
			CodeParameterDeclarationExpressionCollection pars, 
			ConstructorInit initializer)
		{
			Name=name;
			Params=pars;
			Initializer=initializer;
		}

	}
}
