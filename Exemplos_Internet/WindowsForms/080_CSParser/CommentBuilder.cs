// CommentBuilder.cs: CODEDOM Creates CODEDOM comments
//
// Written by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)

using System;
using System.Text;
using System.CodeDom;
using System.Collections;

namespace IvanZ.CSParser
{
	/// <summary>
	/// Summary description for CommentBuilder.
	/// </summary>
	public class CommentBuilder
	{
		private StringBuilder sb;
		private int line;
		private string srcName;
		private CodeCommentStatementCollection comments;
		public CodeCommentStatementCollection CurrComments
		{
			get
			{
				return comments;
			}
		}
		
		
		public CommentBuilder(string name)
		{
			sb=new StringBuilder();
			comments=new CodeCommentStatementCollection();
			srcName=name;
		}

		public virtual void StartComment(int line)
		{
			sb.Length=0; 
			this.line=line;
		}

		public virtual void AddChar(char c)
		{
			sb.Append(c);
		}

		public virtual void FinishComment()
		{
			string val=sb.ToString();
			bool isDoc= val.StartsWith("/");
			if (isDoc)
				val=val.Substring(1);

			string[] oneLine= val.Split('\n');
			for (int i=0; i<oneLine.Length;i++)
			{
				CodeCommentStatement cmt=new CodeCommentStatement(oneLine[i],isDoc);
				cmt.UserData["Location"]=new CodeLinePragma(srcName,line+i);
				comments.Add(cmt);	
			}	
		}

		public virtual void ClearComments()
		{
			comments.Clear();
		}

		public virtual void AssignParent(CodeObject parent) 
		{
			foreach( CodeCommentStatement c in comments)
				c.UserData["Parent"]=parent;
		}

	}
}
