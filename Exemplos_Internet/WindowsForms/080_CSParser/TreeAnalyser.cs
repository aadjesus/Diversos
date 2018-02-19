//SupportTypes.cs: Iterates CODEDOM tree and calls some analysys (defined by object 
//implementing IAnalyser interface
//
// Written by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)
using System;
using System.CodeDom;

namespace IvanZ.CSParser
{

	public interface IAnalyser
	{
		 void AnalyzeCompUnit(CodeCompileUnit unit);
		 void AnalyzeNamespace(CodeNamespace ns);
		 void AnalyzeType(CodeTypeDeclaration type);
		 void AnalyzeMember(CodeTypeMember member);
		 void AnalyzeParameters(CodeParameterDeclarationExpression parameter, bool isDeleg);

	}
	/// <summary>
	/// Summary description for TreeAnalyser.
	/// </summary>
	public class TreeAnalyzer
	{
		IAnalyser analyser;
		CodeCompileUnit unit;
		
		public TreeAnalyzer(IAnalyser analyser, CodeCompileUnit cu)
		{
			this.analyser=analyser;
			this.unit=cu;
		}

		public void DoAnalysis()
		{
			analyser.AnalyzeCompUnit(unit);
			for(int a=0; a<unit.Namespaces.Count; a++)
			{
				CodeNamespace ns= unit.Namespaces[a];
				analyser.AnalyzeNamespace(ns);
				for( int b=0; b<ns.Types.Count; b++)
				{
					CodeTypeDeclaration type= ns.Types[b];
					analyser.AnalyzeType(type);
					if (type is CodeTypeDelegate)
					{
						CodeTypeDelegate dlg= (CodeTypeDelegate) type;
						for(int i=0; i< dlg.Parameters.Count; i++)
							analyser.AnalyzeParameters(dlg.Parameters[i], true);
					}
					for (int c=0; c<type.Members.Count; c++)
					{
						CodeTypeMember member= type.Members[c];
						analyser.AnalyzeMember(member);
						CodeParameterDeclarationExpressionCollection pars=null;
						if (member is CodeMemberMethod)
							pars=((CodeMemberMethod)member).Parameters;
						else if (member is CodeMemberProperty)
							pars=((CodeMemberProperty)member).Parameters;
						if (pars!=null)
							for (int d=0; d<pars.Count; d++)
								analyser.AnalyzeParameters(pars[d], false);
					}
				}
			}
			
		}
	}
}
