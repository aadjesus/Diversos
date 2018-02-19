// TreeCreator.cs: Creates tree for TreeView control from CODEDOM tree
//
// Written by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)

namespace CodeTreeView
{
    using System;
    using IvanZ.CSParser;
    using System.CodeDom;
	using System.Windows.Forms;
    
    
    public class TreeCreator : IAnalyser
    {
        
		private TreeView tree;
		private TreeNode nsNode;
		private TreeNode typeNode;
		private TreeNode memberNode;
		
		public TreeCreator(TreeView tree)
		{
			this.tree=tree;
			
		}
		public virtual void AnalyzeCompUnit(CodeCompileUnit unit)
        {
        }
        
        public virtual void AnalyzeMember(CodeTypeMember member)
        {
			memberNode= new TreeNode("[M] "+member.Name);
			memberNode.Tag= member.UserData["Location"];
			typeNode.Nodes.Add(memberNode);
        }
        
        public virtual void AnalyzeNamespace(CodeNamespace ns)
        {
			nsNode=new TreeNode("[NS] "+ns.Name);
			nsNode.Tag= ns.UserData["Location"];
			tree.Nodes.Add(nsNode);
        }
        
        public virtual void AnalyzeParameters(CodeParameterDeclarationExpression parameter,bool isDeleg)
        {
			TreeNode parNode= new TreeNode("[P] "+parameter.Name);
			parNode.Tag= parameter.UserData["Location"];

			if (isDeleg)
				typeNode.Nodes.Add(parNode);
			else
				memberNode.Nodes.Add(parNode);
			
			
        }
        
        public virtual void AnalyzeType(CodeTypeDeclaration type)
        {
			typeNode= new TreeNode("[T] "+type.Name);
			typeNode.Tag= type.UserData["Location"];
			nsNode.Nodes.Add(typeNode);

        }
    }
}
