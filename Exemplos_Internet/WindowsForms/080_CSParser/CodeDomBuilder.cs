// CodeDomBuilder.cs: Builds  CodeDom Tree
//
// Written by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)

using System;
using System.CodeDom;
using Mono.CSharp;
using System.Diagnostics;
using System.Runtime;

namespace IvanZ.CSParser
{
	/// <summary>
	/// Summary description for CodeDomBuilder.
	/// </summary>
	public class CodeDomBuilder
	{
		
		private string sourceName;
		private Tokenizer lex;
		private CommentBuilder cmtBuilder;

		private CodeObject parent;
		public virtual CodeObject CurrParent
		{
			get 
			{
				return parent;
			}
			
		}
		public virtual void Up()
		{
			parent=(CodeObject) parent.UserData["Parent"];
			Debug.Assert(parent!=null, "Parent is null!!");
			//Clear any unused comments, when exiting level
			cmtBuilder.ClearComments();
		}
		
		private CodeCompileUnit compUnit;
		public virtual CodeCompileUnit CurrCompileUnit 
		{
			get 
			{
				return compUnit;
			}
		}

		private CodeNamespace ns;
		public virtual CodeNamespace CurrNamespace
		{
			get
			{
				return ns;
			}
			set
			{
				ns = value;
			}
		}

		private CodeTypeDeclaration type;
		public virtual CodeTypeDeclaration CurrType
		{
			get 
			{
				return type;
			}
			set
			{
				type=value;
			}
		}
		
		private CodeTypeMember member;
		public virtual CodeTypeMember CurrMember
		{
			get
			{
				return member;
			}
			set
			{
				member=value;
			}
		}

		private bool StartCmtAdded=false;

		public CodeDomBuilder(string name, Tokenizer lexer, CommentBuilder cb)
		{
			compUnit= new CodeCompileUnit();
			parent = compUnit;
			this.sourceName=name;
			lex=lexer;
			cmtBuilder=cb;

			// creates default NS - root ns (without name)
			
				CurrNamespace= new CodeNamespace();
				CurrNamespace.UserData["Location"]=new CodeLinePragma(sourceName, 0);
				CurrNamespace.UserData["Parent"]=CurrCompileUnit;
				CurrCompileUnit.Namespaces.Add(CurrNamespace);
				parent = CurrNamespace;
			      
			

			
		}

		public virtual void AddNamespace(string name) 
		{
			//no nested ns
			if ( ((CodeNamespace) parent).Name!="")
			{
				Report.Warning(9002,lex.Location,
					"No nested namespaces, they are not supported in CODEDOM, we try to flatten hierarchy, but no guarantee that it works for all cases");
				name = CurrNamespace.Name+"."+name;
			}
			CurrNamespace= new CodeNamespace(name);
			CurrNamespace.UserData["Location"]=new CodeLinePragma(sourceName,lex.Line);
			CurrNamespace.UserData["Parent"]=parent;
			
			CurrCompileUnit.Namespaces.Add(CurrNamespace);
			CurrNamespace.Comments.AddRange(cmtBuilder.CurrComments);
			cmtBuilder.ClearComments();
			parent = CurrNamespace;
		}

		public virtual void AddUsingDirective(string name)
		{
			
			CodeNamespaceImport i = new CodeNamespaceImport(name);
			i.UserData["Location"] = new CodeLinePragma(sourceName, lex.Line);
			i.UserData["Parent"]= parent;
			CurrNamespace.Imports.Add(i);
			//If we are in the default ns and no starting comments were already added
			if (CurrCompileUnit.Namespaces.Count==1 && !StartCmtAdded)
			{
				CurrNamespace.Comments.AddRange(cmtBuilder.CurrComments);
				cmtBuilder.ClearComments();
				StartCmtAdded=true;
			}
			cmtBuilder.ClearComments();
		}

		public virtual void AddType(string name, ModifierAttribs atribs, Types type, 
			CodeTypeReferenceCollection bases, bool classScope, 
			CodeAttributeDeclarationCollection atrs)
		{
			CodeTypeDeclaration c= new CodeTypeDeclaration(name);
			c.UserData["Location"]=new CodeLinePragma(sourceName, lex.Line);
			c.UserData["Parent"]=parent;
			if (!Modifiers.CheckTypeModifiers(atribs, classScope, type))
			{
				Location l = lex.Location;
				Report.Error (9001, l, "Modifier(s) not allowed in this scope are used for " +type.ToString()+" "+ name );
			}
			Modifiers.SetElemModifiers(c, atribs, classScope);
			// Set  type of the type
			switch (type)
			{
				case Types.Class:
					c.IsClass=true;
					break;
				case Types.Interface:
					c.IsInterface=true;
					break;
				case Types.Struct:
					c.IsStruct=true;
					break;
				case Types.Enum:
					c.IsEnum=true;
					break;
			}

			if (bases!= null)
				c.BaseTypes.AddRange(bases);
			if (classScope)
				((CodeTypeDeclaration) parent).Members.Add(c);
			else
				CurrNamespace.Types.Add(c);
			AddAttributeToMemberMultiple(atrs, c,AttributeTargets.Type); 
			parent=c;
			CurrType=c;
			c.Comments.AddRange(cmtBuilder.CurrComments);
			cmtBuilder.ClearComments();

			

		}

		public virtual void AddDelegate(string name, ModifierAttribs atribs, 
			CodeParameterDeclarationExpressionCollection pars,
			CodeTypeReference returnType, bool classScope, 
			CodeAttributeDeclarationCollection atrs)
		{
			CodeTypeDelegate c= new CodeTypeDelegate(name);
			c.UserData["Location"]=new CodeLinePragma(sourceName, lex.Line);
			c.UserData["Parent"]=parent;
			Types type= Types.Delegate;
			if (!Modifiers.CheckTypeModifiers(atribs, classScope, type))
			{
				Location l = lex.Location;
				Report.Error (9001, l, "Modifier(s) not allowed in this scope are used for " +type.ToString()+" "+ name );
			}
			Modifiers.SetElemModifiers(c, atribs, classScope);
			

			if (returnType!= null)
				c.ReturnType=returnType;
			if (pars !=null)
				c.Parameters.AddRange(pars);
			if (classScope)
				((CodeTypeDeclaration) parent).Members.Add(c);
			else
				CurrNamespace.Types.Add(c);
			AddAttributeToMemberMultiple(atrs, c,AttributeTargets.Type|AttributeTargets.Return); 
			CurrType=c;
			c.Comments.AddRange(cmtBuilder.CurrComments);
			cmtBuilder.ClearComments();

			

		}
		
		public virtual void AddFieldMultiple(CodeTypeMemberCollection memberCollection, 
			ModifierAttribs atribs, CodeTypeReference fieldType, 
			Members type, CodeAttributeDeclarationCollection atrs)
		{
			
			
			foreach (CodeMemberField m in memberCollection)
			{
				AddField(m,atribs, fieldType, type, atrs);
			}

		}

		
		public virtual void AddField(string name, 
			ModifierAttribs atribs, CodeTypeReference fieldType, 
			Members type, CodeAttributeDeclarationCollection atrs)
		{
			CodeMemberField m= new CodeMemberField();
			m.Name=name;
			m.Comments.AddRange(cmtBuilder.CurrComments);
			cmtBuilder.ClearComments();
			AddField(m,atribs, fieldType, type, atrs);
		}
		public virtual void AddField(CodeMemberField m, 
			ModifierAttribs atribs, CodeTypeReference fieldType, 
			Members type, CodeAttributeDeclarationCollection atrs)
		{
			if (type == Members.Constant)
				atribs|=ModifierAttribs.Const;

			m.UserData["Location"] = new CodeLinePragma(sourceName, lex.Line);
			m.UserData["Parent"]= parent;
			m.Type=fieldType;

			if (!Modifiers.CheckMemberModifiers(atribs , type))
			{
				Location l = lex.Location;
				Report.Error (9001, l, "Modifier(s) not allowed in this scope are used for " +type.ToString()+" "+ m.Name );
			}
			Modifiers.SetElemModifiers(m, atribs, false);

			AddAttributeToMemberMultiple(atrs, m,AttributeTargets.Field); 
			CurrType.Members.Add(m);
			CurrMember=m;

		}

		public virtual void AddMethod(string name, ModifierAttribs atribs, 
			CodeTypeReference returnType,
			CodeParameterDeclarationExpressionCollection pars,
			Members type, CodeAttributeDeclarationCollection atrs)
		{
			CodeMemberMethod m;
			
			if (name=="Main") 
				m=new CodeEntryPointMethod();
			else
				m= new CodeMemberMethod();
			m.Name=name;
			if (returnType!= null)
				m.ReturnType=returnType;
			if (pars !=null)
				m.Parameters.AddRange(pars);

			m.UserData["Location"] = new CodeLinePragma(sourceName, lex.Line);
			m.UserData["Parent"]= parent;

			if (!Modifiers.CheckMemberModifiers(atribs , type))
			{
				Location l = lex.Location;
				Report.Error (9001, l, "Modifier(s) not allowed in this scope are used for " +type.ToString()+" "+ m.Name );
			}
			Modifiers.SetElemModifiers(m, atribs, false);

			AddAttributeToMemberMultiple(atrs, m,AttributeTargets.Method|AttributeTargets.Return); 
			CurrType.Members.Add(m);
			CurrMember=m;
			parent=m;
			m.Comments.AddRange(cmtBuilder.CurrComments);
			cmtBuilder.ClearComments();

		}

		public virtual void AddProperty(string name, ModifierAttribs atribs, 
			CodeTypeReference propType,
			CodeParameterDeclarationExpressionCollection pars,
			Members type, CodeAttributeDeclarationCollection atrs)
		{
			CodeMemberProperty m= new CodeMemberProperty();
			m.Name=name;
			m.Type=propType;
			if (pars !=null)
				m.Parameters.AddRange(pars);

			m.UserData["Location"] = new CodeLinePragma(sourceName, lex.Line);
			m.UserData["Parent"]= parent;

			if (!Modifiers.CheckMemberModifiers(atribs , type))
			{
				Location l = lex.Location;
				Report.Error (9001, l, "Modifier(s) not allowed in this scope are used for " +type.ToString()+" "+ m.Name );
			}
			Modifiers.SetElemModifiers(m, atribs, false);
			AddAttributeToMemberMultiple(atrs, m,AttributeTargets.Property); 
			CurrType.Members.Add(m);
			CurrMember=m;
			parent=m;
			m.Comments.AddRange(cmtBuilder.CurrComments);
			cmtBuilder.ClearComments();

		}

		public virtual void AddEvent(CodeMemberEvent m, ModifierAttribs atribs,
			CodeTypeReference evType, Members type, CodeAttributeDeclarationCollection atrs)
		{
			
			m.Type=evType;
			

			m.UserData["Location"] = new CodeLinePragma(sourceName, lex.Line);
			m.UserData["Parent"]= parent;

			if (!Modifiers.CheckMemberModifiers(atribs , type))
			{
				Location l = lex.Location;
				Report.Error (9001, l, "Modifier(s) not allowed in this scope are used for " +type.ToString()+" "+ m.Name );
			}
			Modifiers.SetElemModifiers(m, atribs, false);
			AddAttributeToMemberMultiple(atrs, m,AttributeTargets.Event|AttributeTargets.Field|AttributeTargets.Method); 
			CurrType.Members.Add(m);
			CurrMember=m;
			parent=m;

		}

		public virtual void AddEvent(string name, ModifierAttribs atribs,
			CodeTypeReference evType, Members type, CodeAttributeDeclarationCollection atrs)
		{
			CodeMemberEvent e= new CodeMemberEvent();
			e.Name=name;
			e.Comments.AddRange(cmtBuilder.CurrComments);
			cmtBuilder.ClearComments();
			AddEvent(e,atribs, evType,type, atrs);
		}

		public virtual void AddEventMultiple (CodeTypeMemberCollection names, ModifierAttribs atribs, 
			CodeTypeReference evType,
			Members type, CodeAttributeDeclarationCollection atrs)
		{
			foreach (CodeMemberField m in names)
			{
				CodeMemberEvent e =new CodeMemberEvent();
				e.Name=m.Name;
				e.Comments.AddRange(m.Comments);
				AddEvent(e, atribs, evType, type, atrs);
			}
		}


		public virtual void AddConstructor(ConstructorDecl d, ModifierAttribs atribs, 
			Members type, CodeAttributeDeclarationCollection atrs)
		{

			if (d.Name!= ((CodeTypeDeclaration) parent).Name)
			{
				Location l = lex.Location;
				Report.Error (9006, l, "Metod " + d.Name + " must have return type (or  it's mistyped constructor name)" );
			}

			CodeMemberMethod m;
			if ((atribs & ModifierAttribs.Static)!=0)
			{
				if (d.Params!=null)

				{
					Location l = lex.Location;
					Report.Error (9007, l, "Static constructor must be parameterless");
				}
				m=new CodeTypeConstructor();
				type=Members.StaticConstructor;
			}
			else
			{
				m=new CodeConstructor();
			}
			
			
			m.Name=d.Name;
			if (d.Params !=null)
				m.Parameters.AddRange(d.Params);

			m.UserData["Location"] = new CodeLinePragma(sourceName, lex.Line);
			m.UserData["Parent"]= parent;

			if (!Modifiers.CheckMemberModifiers(atribs , type))
			{
				Location l = lex.Location;
				Report.Error (9001, l, "Modifier(s) not allowed in this scope are used for " +type.ToString()+" "+ m.Name );
			}
			Modifiers.SetElemModifiers(m, atribs, false);

			AddAttributeToMemberMultiple(atrs, m,AttributeTargets.Method); 
			CurrType.Members.Add(m);
			CurrMember=m;
			parent=m;
			m.Comments.AddRange(cmtBuilder.CurrComments);
			cmtBuilder.ClearComments();


		}

		public virtual CodeExpression ResolveIdentifier(string qname)
		{
			//basically it needs lookup in the and resolving based on existing names
			//now it is just based on the stupid algorithm - everything which dont have 
			// dot is variable otherwise it is resolved to typereference plus member access

			if (qname.IndexOf('.')>-1)
			{
				int i=qname.LastIndexOf('.');
				CodeTypeReferenceExpression r= new CodeTypeReferenceExpression(qname.Substring(0, i));
				return new CodeFieldReferenceExpression(r,qname.Substring(i+1));
			}
			else
			{
				return new CodeVariableReferenceExpression(qname);
			}
		}

		public virtual void AddAssemblyAttributeMultiple(CodeAttributeDeclarationCollection atrs)
		{
			if (atrs != null)
			foreach(CodeAttributeDeclaration d in atrs)
				AddAssemblyAttribute(d);
		}
		public virtual void AddAssemblyAttribute(CodeAttributeDeclaration a)
		{
			CodeAttributeDeclarationExt aExt = (CodeAttributeDeclarationExt) a;
			if (aExt.Target != AttributeTargets.Assembly && aExt.Target != AttributeTargets.Module)
			{
				Report.Error(9015, lex.Location, "Attribute target not allowed in global scope");
				return;
			}
			CurrCompileUnit.AssemblyCustomAttributes.Add(a);
			
		}

		protected virtual void AddAttributeToMember(CodeAttributeDeclaration a, 
			CodeTypeMember m, AttributeTargets allowedTargets)
		{
			CodeAttributeDeclarationExt aExt = (CodeAttributeDeclarationExt) a;
			if (aExt.Target != 0 && ((aExt.Target &  allowedTargets)==0) && 
				((aExt.Target & AttributeTargets.Assembly)==0)
				&& (aExt.Target & AttributeTargets.Module)==0)
			{
				Report.Error(9015, lex.Location, "Attribute target not allowed in this scope");
				return;
			}
			else if (((aExt.Target & AttributeTargets.Assembly)!=0)||
				 ((aExt.Target & AttributeTargets.Module)!=0))
			{
				AddAssemblyAttribute(a);
				return;
			}
			m.CustomAttributes.Add(a);

		}

		protected virtual void AddAttributeToMemberMultiple(CodeAttributeDeclarationCollection atrs, 
			CodeTypeMember m, AttributeTargets allowedTargets)
		{
			if (atrs != null)
				foreach(CodeAttributeDeclaration d in atrs)
					AddAttributeToMember(d, m, allowedTargets);

		}

		public virtual void AddAttributeToParamMultiple(CodeAttributeDeclarationCollection atrs, 
			CodeParameterDeclarationExpression m)
		{
			if (atrs != null)
				foreach(CodeAttributeDeclaration d in atrs)
					AddAttributeToParam(d, m);

		}

		public virtual void AddAttributeToParam(CodeAttributeDeclaration a, 
			CodeParameterDeclarationExpression m)
		{
			CodeAttributeDeclarationExt aExt = (CodeAttributeDeclarationExt) a;
			if (aExt.Target != 0 && ((aExt.Target &  AttributeTargets.Param)==0))
			{
				Report.Error(9015, lex.Location, "Attribute target not allowed in this scope");
				return;
			}
			
			m.CustomAttributes.Add(a);

		}

		
		
	}
}
