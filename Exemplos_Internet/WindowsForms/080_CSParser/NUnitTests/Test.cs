// Test.cs: NUNIT Tests for CS CODEDOM Parser
//
// Written by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)

using System;
using System.IO;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using NUnit.Framework;
using IvanZ.CSParser;
using Mono.CSharp;
using System.Reflection;

namespace IvanZ.CSParser.NUnitTests
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Test:TestCase
	{
		const string TST_FILES_DIR = @"C:\Documents and Settings\ivan.zderadicka\My Documents\Visual Studio Projects\CSParser\NUnitTests\TestFiles";
		public Test(string n):base(n) {}

		protected int TestFile(string filename, out CodeCompileUnit unit)
		{
			FileStream fstream = File.Open( filename, FileMode.Open, FileAccess.Read, FileShare.Read);
			CSharpParser p= new CSharpParser("", fstream, null);
			int errno= p.parse();
			
			
			
			//			if (p.Builder.CurrCompileUnit!=null) 
			//			{
			//				
			//
			//				CodeDomProvider provider= new CSharpCodeProvider();
			//				StringBuilder sb = new StringBuilder();
			//				StringWriter sw = new StringWriter(sb);
			//				ICodeGenerator generator= provider.CreateGenerator();
			//				generator.GenerateCodeFromCompileUnit(p.Builder.CurrCompileUnit,sw,new CodeGeneratorOptions());
			//				Console.WriteLine("CODEDOM code: \n" + sb.ToString());
			//			}
			unit=p.Builder.CurrCompileUnit;
			return errno;
		}

		public void TestNSandClasses()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\ns1.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);
			Assert("3 namespaces", u.Namespaces.Count==3);
			Assert("2 usings in def. ns", u.Namespaces[0].Imports.Count==2);
			Assert("2 classes in 1st ns", u.Namespaces[1].Types.Count==2);
			Assert("3 base types in class Class2", u.Namespaces[1].Types[0].BaseTypes.Count==3);
			Assert("Class2 has inner class", u.Namespaces[1].Types[0].Members[0].Name=="Inner" &&
				((CodeTypeDeclaration)u.Namespaces[1].Types[0].Members[0]).IsClass);
			Assert("Class2 is abstract public",  ((u.Namespaces[1].Types[0].TypeAttributes & TypeAttributes.Abstract)>0) &&
				((u.Namespaces[1].Types[0].TypeAttributes & TypeAttributes.Public)>0));
			Assert("InnerClass is private",  (((CodeTypeDeclaration)u.Namespaces[1].Types[0].Members[0]).TypeAttributes & TypeAttributes.NestedPrivate)>0);


		}
		public void TestInnerNS()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\ns2.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);
			Assert("There are 3 NS", u.Namespaces.Count==3);
			Assert("Second ns name is testParser", u.Namespaces[1].Name=="testParser");
			Assert("2nd namespace has member Class2",
				u.Namespaces[1].Types[0].Name=="Class2");

			Assert("Third ns name is testParser.OtherNameSpace", 
				u.Namespaces[2].Name=="testParser.OtherNameSpace");
			Assert("3rd namespace has member ClasssInOtherNS",
				u.Namespaces[2].Types[0].Name=="ClasssInOtherNS");

		}

		public void TestTypes()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\types1.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);
			Assert("NS Contains 5 types",u.Namespaces[1].Types.Count==5);
			Assert("First is Class", u.Namespaces[1].Types[0].IsClass);
			Assert("Second is Interface", u.Namespaces[1].Types[1].IsInterface);
			Assert("Third is Struct", u.Namespaces[1].Types[2].IsStruct);
			Assert("Forth is Enum", u.Namespaces[1].Types[3].IsEnum);
			Assert("Fifth is Delegate", u.Namespaces[1].Types[4] is CodeTypeDelegate);

			Assert("And Class contains 4 inner types", u.Namespaces[1].Types[0].Members.Count==4);
			Assert("First nested is Interface", ((CodeTypeDeclaration) u.Namespaces[1].Types[0].Members[0]).IsInterface);
			Assert("Second  nested is Struct", ((CodeTypeDeclaration) u.Namespaces[1].Types[0].Members[1]).IsStruct);
			Assert("Third nested is Enum", ((CodeTypeDeclaration) u.Namespaces[1].Types[0].Members[2]).IsEnum);
			Assert("Forth nested  is Delegate", (u.Namespaces[1].Types[0].Members[3])is CodeTypeDelegate );

			Assert("Delegate has two parameters", ((CodeTypeDelegate)u.Namespaces[1].Types[4]).Parameters.Count==2);

			Assert("First param is i", (((CodeTypeDelegate)u.Namespaces[1].Types[4]).Parameters[0].Name=="i") );
			Assert("First param is out ",	(((CodeTypeDelegate)u.Namespaces[1].Types[4]).Parameters[0].Direction == FieldDirection.Out));
			Assert("First param is int",	(((CodeTypeDelegate)u.Namespaces[1].Types[4]).Parameters[0].Type.BaseType=="System.Int32"));

			Assert("Second param is char[] j", (((CodeTypeDelegate)u.Namespaces[1].Types[4]).Parameters[1].Name=="j") &&
				(((CodeTypeDelegate)u.Namespaces[1].Types[4]).Parameters[1].Type.ArrayRank==1) &&
				(((CodeTypeDelegate)u.Namespaces[1].Types[4]).Parameters[1].Type.ArrayElementType.BaseType=="System.Char"));
		}
		
		public void TestFields()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\fields1.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);

			Assert("First field is i", ((u.Namespaces[1].Types[0]).Members[0].Name=="i") );
			Assert("First field  is private ",	((u.Namespaces[1].Types[0]).Members[0].Attributes & MemberAttributes.Private)!=0);
			Assert("First field is int",	(((CodeMemberField)u.Namespaces[1].Types[0].Members[0]).Type.BaseType=="System.Int32"));

			Assert("Third field is s", ((u.Namespaces[1].Types[0]).Members[2].Name=="s") );
			Assert("Third field  is public ",	((u.Namespaces[1].Types[0]).Members[2].Attributes & MemberAttributes.Private)!=0);
			Assert("Third field  is static ",	((u.Namespaces[1].Types[0]).Members[2].Attributes & MemberAttributes.Static)!=0);
			Assert("Third field is string",	(((CodeMemberField)u.Namespaces[1].Types[0].Members[2]).Type.BaseType=="System.String"));

			Assert("Fifth field is array of array of char",	(((CodeMemberField)u.Namespaces[1].Types[0].Members[4]).Type.ArrayRank==1) &&
				(((CodeMemberField)u.Namespaces[1].Types[0].Members[4]).Type.ArrayElementType.ArrayRank==1) &&
				(((CodeMemberField)u.Namespaces[1].Types[0].Members[4]).Type.ArrayElementType.ArrayElementType.BaseType=="System.Char") 
				);

			Assert("Sixth field  is protected internal ",	((u.Namespaces[1].Types[0]).Members[5].Attributes & MemberAttributes.FamilyOrAssembly)!=0);

			Assert("Seventh field is array of rank 2",	(((CodeMemberField)u.Namespaces[1].Types[0].Members[6]).Type.ArrayRank==2) );

			Assert("Nineth field is x", ((u.Namespaces[1].Types[0]).Members[8].Name=="x") );
			Assert("Nineth field  is public ",	((u.Namespaces[1].Types[0]).Members[8].Attributes & MemberAttributes.Public)!=0);
			Assert("Nineth field  is const ",	((u.Namespaces[1].Types[0]).Members[8].Attributes & MemberAttributes.Const)!=0);
		}

		public void TestMethods()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\methods1.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);

			

			Assert("First method is MethodA", ((u.Namespaces[1].Types[0]).Members[1].Name=="MethodA") );
			Assert("MethodA is protected internal ",	((u.Namespaces[1].Types[0]).Members[1].Attributes & MemberAttributes.FamilyOrAssembly)!=0);
			Assert("MethodA  return type is int",	(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).ReturnType.BaseType=="System.Int32"));
			Assert("MethodA has two params", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters.Count==2) );
			Assert ("MethodA first param is int i", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters[0].Name=="i") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters[0].Type.BaseType=="System.Int32") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters[0].Direction==FieldDirection.In));
			Assert ("MethodA second param is char j", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters[1].Name=="j") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters[1].Type.BaseType=="System.Char") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters[1].Direction==FieldDirection.In));
			
			Assert("Second method is MethodB", ((u.Namespaces[1].Types[0]).Members[2].Name=="MethodB") );
			Assert("MethodB is protected internal ",	((u.Namespaces[1].Types[0]).Members[2].Attributes & MemberAttributes.Private)!=0);
			Assert("MethodB  return type is void",	(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[2]).ReturnType.BaseType=="System.Void"));
			Assert("MethodB has two params", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[2]).Parameters.Count==2) );
			Assert ("MethodB second param is object[] o", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[2]).Parameters[1].Name=="o") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[2]).Parameters[1].Type.ArrayRank==1) &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[2]).Parameters[1].Type.ArrayElementType.BaseType=="System.Object"));

			Assert("MethodC is public abstact ",	(((u.Namespaces[1].Types[0]).Members[3].Attributes & MemberAttributes.Public)!=0) &&
				(((u.Namespaces[1].Types[0]).Members[3].Attributes & MemberAttributes.Abstract)!=0));
			Assert("MethodC has zero params", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[3]).Parameters.Count==0) );

			Assert ("MethodD first param is out char c", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[4]).Parameters[0].Name=="c") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[4]).Parameters[0].Type.BaseType=="System.Char") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[4]).Parameters[0].Direction==FieldDirection.Out));

			Assert ("MethodD second param is ref string g", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[4]).Parameters[1].Name=="g") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[4]).Parameters[1].Type.BaseType=="System.String") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[4]).Parameters[1].Direction==FieldDirection.Ref));

			Assert("MethodE is public static ",	(((u.Namespaces[1].Types[0]).Members[5].Attributes & MemberAttributes.Public)!=0) &&
				(((u.Namespaces[1].Types[0]).Members[5].Attributes & MemberAttributes.Static)!=0));

			Assert("MethodF is public virtual ",	(((u.Namespaces[1].Types[0]).Members[6].Attributes & MemberAttributes.Public)!=0) &&
				(((u.Namespaces[1].Types[0]).Members[6].Attributes & MemberAttributes.Final)==0));

			Assert("MethodG is public override ",	(((u.Namespaces[1].Types[0]).Members[7].Attributes & MemberAttributes.Public)!=0) &&
				(((u.Namespaces[1].Types[0]).Members[7].Attributes & MemberAttributes.Override)!=0));

			Assert("MethodH is public new ",	(((u.Namespaces[1].Types[0]).Members[8].Attributes & MemberAttributes.Public)!=0) &&
				(((u.Namespaces[1].Types[0]).Members[8].Attributes & MemberAttributes.New)!=0));

			Assert("MethodI is public new virtual ",	(((u.Namespaces[1].Types[0]).Members[9].Attributes & MemberAttributes.Public)!=0) &&
				(((u.Namespaces[1].Types[0]).Members[9].Attributes & MemberAttributes.New)!=0) && 
				(((u.Namespaces[1].Types[0]).Members[9].Attributes & MemberAttributes.Final)==0));
		}

		public void TestProperties()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\property1.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);

			Assert("First property is PropA", ((u.Namespaces[1].Types[0]).Members[2].Name=="PropA") );
			Assert("PropA is public ",	((u.Namespaces[1].Types[0]).Members[2].Attributes & MemberAttributes.Public)!=0);
			Assert("PropA type is int",	(((CodeMemberProperty)u.Namespaces[1].Types[0].Members[2]).Type.BaseType=="System.Int32"));
			Assert("PropA has get", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[2]).HasGet) );
			Assert("PropA has set", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[2]).HasSet) );
			
			Assert("Second property is PropB", ((u.Namespaces[1].Types[0]).Members[3].Name=="PropB") );
			Assert("PropB is public ",	((u.Namespaces[1].Types[0]).Members[3].Attributes & MemberAttributes.Public)!=0);
			Assert("PropB is virtual ",	((u.Namespaces[1].Types[0]).Members[3].Attributes & MemberAttributes.Final)==0);
			Assert("PropB type is int",	(((CodeMemberProperty)u.Namespaces[1].Types[0].Members[3]).Type.BaseType=="System.String"));
			Assert("PropA has get", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[3]).HasGet) );
			Assert("PropA doesnt have set", (!((CodeMemberProperty)u.Namespaces[1].Types[0].Members[3]).HasSet) );

			Assert("Third property is PropC", ((u.Namespaces[1].Types[0]).Members[4].Name=="PropC") );
			Assert("PropC is protected ",	((u.Namespaces[1].Types[0]).Members[4].Attributes & MemberAttributes.Family)!=0);
			Assert("PropC is override ",	((u.Namespaces[1].Types[0]).Members[4].Attributes & MemberAttributes.Override)!=0);
			Assert("PropC type is object",	(((CodeMemberProperty)u.Namespaces[1].Types[0].Members[4]).Type.BaseType=="System.Object"));
			Assert("PropC doesnot have get", (!((CodeMemberProperty)u.Namespaces[1].Types[0].Members[4]).HasGet) );
			Assert("PropC has set", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[4]).HasSet) );

			Assert("Fourth property is Prop", ((u.Namespaces[1].Types[0]).Members[5].Name=="PropD") );
			Assert("PropD is public ",	((u.Namespaces[1].Types[0]).Members[5].Attributes & MemberAttributes.Public)!=0);
			Assert("PropD is static ",	((u.Namespaces[1].Types[0]).Members[5].Attributes & MemberAttributes.Static)!=0);
			Assert("PropD type is object",	(((CodeMemberProperty)u.Namespaces[1].Types[0].Members[5]).Type.BaseType=="System.Int64"));
			Assert("PropD has get", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[5]).HasGet) );
			Assert("PropD doesnot have set", (!((CodeMemberProperty)u.Namespaces[1].Types[0].Members[5]).HasSet) );

			Assert("Fifth property is an indexer", ((u.Namespaces[1].Types[0]).Members[6].Name=="Item") );
			Assert("First indexer is public  ",	((u.Namespaces[1].Types[0]).Members[6].Attributes & MemberAttributes.Public)!=0);			
			Assert("First indexer type  is object",	(((CodeMemberProperty)u.Namespaces[1].Types[0].Members[6]).Type.BaseType=="System.Object"));
			Assert("First indexer has get", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[6]).HasGet) );
			Assert("First indexer doesnot have set", (!((CodeMemberProperty)u.Namespaces[1].Types[0].Members[6]).HasSet) );
			Assert("First indexer has one param", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[6]).Parameters.Count==1) );
			Assert("First indexer param name is i", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[6]).Parameters[0].Name=="i") );
			Assert("First indexer param name is int", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[6]).Parameters[0].Type.BaseType=="System.Int32") );

			Assert("Sixth property is an indexer", ((u.Namespaces[1].Types[0]).Members[7].Name=="Item") );
			Assert("Second indexer is internal ",	((u.Namespaces[1].Types[0]).Members[7].Attributes & MemberAttributes.Assembly)!=0);			
			Assert("Second indexer type  is object",	(((CodeMemberProperty)u.Namespaces[1].Types[0].Members[7]).Type.BaseType=="System.Object"));
			Assert("Second indexer has get", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[7]).HasGet) );
			Assert("Second indexer has set", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[7]).HasSet) );
			Assert("Second indexer has one param", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[7]).Parameters.Count==1) );
			Assert("Second indexer param name is s", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[7]).Parameters[0].Name=="s") );
			Assert("Second indexer param name is string", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[7]).Parameters[0].Type.BaseType=="System.String") );

			Assert("Seventh property is an indexer", ((u.Namespaces[1].Types[0]).Members[8].Name=="Item") );
			Assert("Second indexer is public ",	((u.Namespaces[1].Types[0]).Members[8].Attributes & MemberAttributes.Public)!=0);			
			Assert("Second indexer type  is object",	(((CodeMemberProperty)u.Namespaces[1].Types[0].Members[8]).Type.BaseType=="System.Object"));
			Assert("Second indexer has get", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[8]).HasGet) );
			Assert("Second indexer doesnot have set", (!((CodeMemberProperty)u.Namespaces[1].Types[0].Members[8]).HasSet) );
			Assert("Second indexer has one param", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[8]).Parameters.Count==1) );
			Assert("Second indexer param name is s", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[8]).Parameters[0].Name=="s") );
			Assert("Second indexer param name is string", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[8]).Parameters[0].Type.BaseType=="System.String") );
		}

		public void TestEvents()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\event1.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);

			Assert("First event is ev1", ((u.Namespaces[1].Types[0]).Members[1].Name=="ev1") );
			Assert("ev1 is public ",	((u.Namespaces[1].Types[0]).Members[1].Attributes & MemberAttributes.Public)!=0);
			Assert("ev1 type is DlgType",	(((CodeMemberEvent)u.Namespaces[1].Types[0].Members[1]).Type.BaseType=="DlgType"));

			Assert("Second event is ev2", ((u.Namespaces[1].Types[0]).Members[2].Name=="ev2") );
			Assert("ev2 is public ",	((u.Namespaces[1].Types[0]).Members[2].Attributes & MemberAttributes.Public)!=0);
			Assert("ev2 is override ",	((u.Namespaces[1].Types[0]).Members[2].Attributes & MemberAttributes.Override)!=0);
			Assert("ev2 type is DlgType",	(((CodeMemberEvent)u.Namespaces[1].Types[0].Members[2]).Type.BaseType=="DlgType"));

			Assert("Third event is ev3", ((u.Namespaces[1].Types[0]).Members[3].Name=="ev3") );
			Assert("ev3 is public ",	((u.Namespaces[1].Types[0]).Members[3].Attributes & MemberAttributes.Public)!=0);
			Assert("ev3 is override ",	((u.Namespaces[1].Types[0]).Members[3].Attributes & MemberAttributes.Override)!=0);
			Assert("ev3 type is DlgType",	(((CodeMemberEvent)u.Namespaces[1].Types[0].Members[3]).Type.BaseType=="DlgType"));

			Assert("Forth event is ev4", ((u.Namespaces[1].Types[0]).Members[4].Name=="ev4") );
			Assert("ev4 is public ",	((u.Namespaces[1].Types[0]).Members[4].Attributes & MemberAttributes.Public)!=0);
			Assert("ev4 is virtual ",	((u.Namespaces[1].Types[0]).Members[4].Attributes & MemberAttributes.Final)==0);
			Assert("ev4 type is DlgType",	(((CodeMemberEvent)u.Namespaces[1].Types[0].Members[4]).Type.BaseType=="DlgType"));


		}

		public void TestConstructors()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\constructors.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);
		
			Assert("First constructor is Class2", ((u.Namespaces[1].Types[0]).Members[1].Name=="Class2") );
			Assert("First Constructor is public ",	((u.Namespaces[1].Types[0]).Members[1].Attributes & MemberAttributes.Public)!=0);
			Assert("First Constructor has zero params", (((CodeConstructor)u.Namespaces[1].Types[0].Members[1]).Parameters.Count==0) );
			
			Assert("Second constructor is Class2", ((u.Namespaces[1].Types[0]).Members[2].Name=="Class2") );
			Assert("Second Constructor is public ",	((u.Namespaces[1].Types[0]).Members[2].Attributes & MemberAttributes.Public)!=0);
			Assert("Second Constructor has one params", (((CodeConstructor)u.Namespaces[1].Types[0].Members[2]).Parameters.Count==1) );
			Assert ("Second Constructor first param is int i", (((CodeConstructor)u.Namespaces[1].Types[0].Members[2]).Parameters[0].Name=="i") &&
				(((CodeConstructor)u.Namespaces[1].Types[0].Members[2]).Parameters[0].Type.BaseType=="System.Int32") &&
				(((CodeConstructor)u.Namespaces[1].Types[0].Members[2]).Parameters[0].Direction==FieldDirection.In));
			
			Assert("Third constructor is Class2", ((u.Namespaces[1].Types[0]).Members[3].Name=="Class2") );
			Assert("Third Constructor is static constructor ",	((u.Namespaces[1].Types[0]).Members[3] is CodeTypeConstructor));
			Assert("Third Constructor has zero params", (((CodeTypeConstructor)u.Namespaces[1].Types[0].Members[3]).Parameters.Count==0) );
			
			
			Assert("Fourth constructor is Class2", ((u.Namespaces[1].Types[0]).Members[4].Name=="Class2") );
			Assert("Fourth Constructor is protected ",	((u.Namespaces[1].Types[0]).Members[4].Attributes & MemberAttributes.Family)!=0);
			Assert("Fourth Constructor has one params", (((CodeConstructor)u.Namespaces[1].Types[0].Members[4]).Parameters.Count==1) );
			Assert ("Fourth Constructor first param is string s", (((CodeConstructor)u.Namespaces[1].Types[0].Members[4]).Parameters[0].Name=="s") &&
				(((CodeConstructor)u.Namespaces[1].Types[0].Members[4]).Parameters[0].Type.BaseType=="System.String") &&
				(((CodeConstructor)u.Namespaces[1].Types[0].Members[4]).Parameters[0].Direction==FieldDirection.In));

			Assert("Fifth constructor is Class2", ((u.Namespaces[1].Types[0]).Members[5].Name=="Class2") );
			Assert("Fifth Constructor is internal ",	((u.Namespaces[1].Types[0]).Members[5].Attributes & MemberAttributes.Assembly)!=0);
			Assert("Fifth Constructor has two params", (((CodeConstructor)u.Namespaces[1].Types[0].Members[5]).Parameters.Count==2) );
			Assert ("Fifth Constructor first param is object o", (((CodeConstructor)u.Namespaces[1].Types[0].Members[5]).Parameters[0].Name=="o") &&
				(((CodeConstructor)u.Namespaces[1].Types[0].Members[5]).Parameters[0].Type.BaseType=="System.Object") &&
				(((CodeConstructor)u.Namespaces[1].Types[0].Members[5]).Parameters[0].Direction==FieldDirection.In));
			Assert ("Fifth Constructor second param is long l", (((CodeConstructor)u.Namespaces[1].Types[0].Members[5]).Parameters[1].Name=="l") &&
				(((CodeConstructor)u.Namespaces[1].Types[0].Members[5]).Parameters[1].Type.BaseType=="System.Int64") &&
				(((CodeConstructor)u.Namespaces[1].Types[0].Members[5]).Parameters[1].Direction==FieldDirection.In));
			
		}

		public void TestStruct()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\struct.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);

			Assert("First member is field  c", ((u.Namespaces[1].Types[0]).Members[0].Name=="c") );
			Assert("First member  is public ",	((u.Namespaces[1].Types[0]).Members[0].Attributes & MemberAttributes.Public)!=0);
			Assert("First member  is const ",	((u.Namespaces[1].Types[0]).Members[0].Attributes & MemberAttributes.Const)!=0);
			Assert("First member is string",	(((CodeMemberField)u.Namespaces[1].Types[0].Members[0]).Type.BaseType=="System.String"));
			
			Assert("Second memberis  field is i", ((u.Namespaces[1].Types[0]).Members[1].Name=="i") );
			Assert("Second member is private ",	((u.Namespaces[1].Types[0]).Members[1].Attributes & MemberAttributes.Private)!=0);
			Assert("First field is int",	(((CodeMemberField)u.Namespaces[1].Types[0].Members[1]).Type.BaseType=="System.Int32"));
		
			Assert("Third member is constructor  TestStruct", ((u.Namespaces[1].Types[0]).Members[2].Name=="TestStruct") );
			Assert("Third member is static constructor ",	((u.Namespaces[1].Types[0]).Members[2] is CodeTypeConstructor));
			Assert("Third member has zero params", (((CodeTypeConstructor)u.Namespaces[1].Types[0].Members[2]).Parameters.Count==0) );
		
			Assert("Forth member is constructor  TestStruct", ((u.Namespaces[1].Types[0]).Members[3].Name=="TestStruct") );
			Assert("Forth member  is public ",	((u.Namespaces[1].Types[0]).Members[3].Attributes & MemberAttributes.Public)!=0);
			Assert("Forth memeber has zero params", (((CodeConstructor)u.Namespaces[1].Types[0].Members[3]).Parameters.Count==0) );

			Assert("Fifth member is  method  methodA", ((u.Namespaces[1].Types[0]).Members[4].Name=="methodA") );
			Assert("MethodA is public ",	((u.Namespaces[1].Types[0]).Members[4].Attributes & MemberAttributes.Public)!=0);
			Assert("MethodA  return type is int",	(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[4]).ReturnType.BaseType=="System.Int32"));
			Assert("MethodA has one params", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[4]).Parameters.Count==1) );
			Assert ("MethodA first param is object o", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[4]).Parameters[0].Name=="o") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[4]).Parameters[0].Type.BaseType=="System.Object") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[4]).Parameters[0].Direction==FieldDirection.In));

			Assert("Sixth member is property  prop", ((u.Namespaces[1].Types[0]).Members[5].Name=="prop") );
			Assert("Prop is public ",	((u.Namespaces[1].Types[0]).Members[5].Attributes & MemberAttributes.Public)!=0);
			Assert("Prop type is string",	(((CodeMemberProperty)u.Namespaces[1].Types[0].Members[5]).Type.BaseType=="System.String"));
			Assert("Prop has get", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[5]).HasGet) );
			Assert("Prop has set", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[5]).HasSet) );
			
			Assert("Seventh member is  event  evt", ((u.Namespaces[1].Types[0]).Members[6].Name=="evt") );
			Assert("evt is public ",	((u.Namespaces[1].Types[0]).Members[6].Attributes & MemberAttributes.Public)!=0);
			Assert("evt type is DlgType",	(((CodeMemberEvent)u.Namespaces[1].Types[0].Members[6]).Type.BaseType=="DlgType"));

			Assert("Eightth member is an indexer", ((u.Namespaces[1].Types[0]).Members[7].Name=="Item") );
			Assert("indexer is public  ",	((u.Namespaces[1].Types[0]).Members[7].Attributes & MemberAttributes.Public)!=0);			
			Assert("indexer type  is object",	(((CodeMemberProperty)u.Namespaces[1].Types[0].Members[7]).Type.BaseType=="System.Object"));
			Assert("indexer has get", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[7]).HasGet) );
			Assert("indexer doesnot have set", (!((CodeMemberProperty)u.Namespaces[1].Types[0].Members[7]).HasSet) );
			Assert("indexer has one param", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[7]).Parameters.Count==1) );
			Assert("indexer param name is i", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[7]).Parameters[0].Name=="i") );
			Assert("indexer param name is int", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[7]).Parameters[0].Type.BaseType=="System.Int32") );
		
			Assert("Nineth member is inner class", u.Namespaces[1].Types[0].Members[8].Name=="Cls" &&
				((CodeTypeDeclaration)u.Namespaces[1].Types[0].Members[8]).IsClass);
			Assert("Class is public",  ((u.Namespaces[1].Types[0].Members[8].Attributes & MemberAttributes.Public)>0));
		}


		public void TestItf()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\interface.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);

			Assert("1st using is System", u.Namespaces[0].Imports[0].Namespace=="System");
			Assert("2nd using is System.Collections", u.Namespaces[0].Imports[1].Namespace=="System.Collections");
			Assert("3rd using is System.Xml", u.Namespaces[0].Imports[2].Namespace=="System.Xml");

			Assert("First member is  method  methodA", ((u.Namespaces[1].Types[0]).Members[0].Name=="methodA") );
			Assert("MethodA  return type is int",	(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[0]).ReturnType.BaseType=="System.Int32"));
			Assert("MethodA has one params", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[0]).Parameters.Count==1) );
			Assert ("MethodA first param is object o", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[0]).Parameters[0].Name=="o") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[0]).Parameters[0].Type.BaseType=="System.Object") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[0]).Parameters[0].Direction==FieldDirection.In));

			Assert("Second member is  method  methodB", ((u.Namespaces[1].Types[0]).Members[1].Name=="methodB") );
			Assert("methodB is new ",	((u.Namespaces[1].Types[0]).Members[1].Attributes & MemberAttributes.New)!=0);
			Assert("MethodB  return type is void",	(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).ReturnType.BaseType=="System.Void"));
			Assert("MethodA has two params", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters.Count==2) );
			Assert ("MethodA first param is string s", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters[0].Name=="s") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters[0].Type.BaseType=="System.String") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters[0].Direction==FieldDirection.In));
			Assert ("MethodA second param is out bool b", (((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters[1].Name=="b") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters[1].Type.BaseType=="System.Boolean") &&
				(((CodeMemberMethod)u.Namespaces[1].Types[0].Members[1]).Parameters[1].Direction==FieldDirection.Out));

			Assert("Third member is property  prop", ((u.Namespaces[1].Types[0]).Members[2].Name=="prop") );
			Assert("Prop type is string",	(((CodeMemberProperty)u.Namespaces[1].Types[0].Members[2]).Type.BaseType=="System.String"));
			Assert("Prop has get", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[2]).HasGet) );
			Assert("Prop has set", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[2]).HasSet) );
			
			Assert("Forth member is  event  evt", ((u.Namespaces[1].Types[0]).Members[3].Name=="evt") );
			Assert("evt type is System.Xml.XmlNodeChangedEventHandler",	(((CodeMemberEvent)u.Namespaces[1].Types[0].Members[3]).Type.BaseType=="System.Xml.XmlNodeChangedEventHandler"));

			Assert("Fifth member is an indexer", ((u.Namespaces[1].Types[0]).Members[4].Name=="Item") );
			Assert("indexer type  is object",	(((CodeMemberProperty)u.Namespaces[1].Types[0].Members[4]).Type.BaseType=="System.Object"));
			Assert("indexer has get", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[4]).HasGet) );
			Assert("indexer doesnot have set", (!((CodeMemberProperty)u.Namespaces[1].Types[0].Members[4]).HasSet) );
			Assert("indexer has one param", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[4]).Parameters.Count==1) );
			Assert("indexer param name is i", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[4]).Parameters[0].Name=="i") );
			Assert("indexer param name is int", (((CodeMemberProperty)u.Namespaces[1].Types[0].Members[4]).Parameters[0].Type.BaseType=="System.Int32") );
		
		}

		public void TestEnum()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\enum.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);

			Assert("First type is enum", u.Namespaces[1].Types[0].IsEnum);
			Assert("enum name is en1", u.Namespaces[1].Types[0].Name=="en1");
			Assert("it has 4 values", u.Namespaces[1].Types[0].Members.Count==4);
			Assert("Values are val1-4", u.Namespaces[1].Types[0].Members[0].Name=="val1" && 
				u.Namespaces[1].Types[0].Members[1].Name=="val2" && 
				u.Namespaces[1].Types[0].Members[2].Name=="val3" &&
				u.Namespaces[1].Types[0].Members[3].Name=="val4");

			

		}

		public void TestExpressionLight()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\Expr1.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);

			Assert("First type is class", u.Namespaces[1].Types[0].IsClass);
			Assert("Class name is Class2", u.Namespaces[1].Types[0].Name=="Class2");
			Assert("It has 13 members", u.Namespaces[1].Types[0].Members.Count==13);
			
			Assert("Member 3 is val1", u.Namespaces[1].Types[0].Members[2].Name=="val1");
			Assert("val1 init value is string \"bla\"", 
				(((CodeMemberField)u.Namespaces[1].Types[0].Members[2]).InitExpression is CodePrimitiveExpression) &&
				(((CodePrimitiveExpression) (((CodeMemberField)u.Namespaces[1].Types[0].Members[2]).InitExpression)).Value is string) &&
				(((string) ((CodePrimitiveExpression) (((CodeMemberField)u.Namespaces[1].Types[0].Members[2]).InitExpression)).Value)=="bla"));

			Assert("Member 4 is val2", u.Namespaces[1].Types[0].Members[3].Name=="val2");
			Assert("val2 init value is double 1.1", 
				(((CodeMemberField)u.Namespaces[1].Types[0].Members[3]).InitExpression is CodePrimitiveExpression) &&
				(((CodePrimitiveExpression) (((CodeMemberField)u.Namespaces[1].Types[0].Members[3]).InitExpression)).Value is double) &&
				(((double) ((CodePrimitiveExpression) (((CodeMemberField)u.Namespaces[1].Types[0].Members[3]).InitExpression)).Value)==1.1));

			Assert("Member 5 is val3", u.Namespaces[1].Types[0].Members[4].Name=="val3");
			Assert("val3 init value is int 101", 
				(((CodeMemberField)u.Namespaces[1].Types[0].Members[4]).InitExpression is CodePrimitiveExpression) &&
				(((CodePrimitiveExpression) (((CodeMemberField)u.Namespaces[1].Types[0].Members[4]).InitExpression)).Value is int) &&
				(((int) ((CodePrimitiveExpression) (((CodeMemberField)u.Namespaces[1].Types[0].Members[4]).InitExpression)).Value)==101));


			Assert("Member 7 is val5", u.Namespaces[1].Types[0].Members[6].Name=="val5");
			Assert("val5 init value is CodeBinaryOperator bitwise_or", 
				(((CodeMemberField)u.Namespaces[1].Types[0].Members[6]).InitExpression is CodeBinaryOperatorExpression) &&
				(((CodeBinaryOperatorExpression) (((CodeMemberField)u.Namespaces[1].Types[0].Members[6]).InitExpression)).Operator == CodeBinaryOperatorType.BitwiseOr));
            
			Assert("Member 9 is val7", u.Namespaces[1].Types[0].Members[8].Name=="val7");
			Assert("val7 init value is CodeObjectCreate", 
				(((CodeMemberField)u.Namespaces[1].Types[0].Members[8]).InitExpression is CodeObjectCreateExpression) &&
				(((CodeObjectCreateExpression) (((CodeMemberField)u.Namespaces[1].Types[0].Members[8]).InitExpression)).CreateType.BaseType == "Bla"));

			Assert("Member11 is val9", u.Namespaces[1].Types[0].Members[10].Name=="val9");
			Assert("val9 init value is CodeCastExpression", 
				(((CodeMemberField)u.Namespaces[1].Types[0].Members[10]).InitExpression is CodeCastExpression) );
			//	(((CodeCastExpression) (((CodeMemberField)u.Namespaces[1].Types[0].Members[10]).InitExpression)).TargetType.BaseType == "System.Int32"));

			Assert("Member 13 is val11", u.Namespaces[1].Types[0].Members[12].Name=="val11");
			Assert("val11 init value is CodeBinaryOperator multiply", 
				(((CodeMemberField)u.Namespaces[1].Types[0].Members[12]).InitExpression is CodeBinaryOperatorExpression) &&
				(((CodeBinaryOperatorExpression) (((CodeMemberField)u.Namespaces[1].Types[0].Members[12]).InitExpression)).Operator == CodeBinaryOperatorType.Multiply));

		}

		public void TestAttribs()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\Attribs.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);

			Assert("There are two assembly level attributes", u.AssemblyCustomAttributes.Count==2);
			Assert ("Fist assembly attibute is AssemblyTitle with val XXX",
				u.AssemblyCustomAttributes[0].Name=="AssemblyTitle" &&
				((string) ((CodePrimitiveExpression) u.AssemblyCustomAttributes[0].Arguments[0].Value).Value)=="XXX");
			Assert ("Second assembly attibute is AssemblyName with val YYYY",
				u.AssemblyCustomAttributes[1].Name=="AssemblyName" &&
				((string) ((CodePrimitiveExpression) u.AssemblyCustomAttributes[1].Arguments[0].Value).Value)=="YYYY");
			Assert("Class2 has 4 attributes", u.Namespaces[1].Types[0].CustomAttributes.Count==4);
			Assert ("Class2 4th attrib is PermissionSet with 2 args",
				u.Namespaces[1].Types[0].CustomAttributes[3].Name=="PermissionSet" &&
				u.Namespaces[1].Types[0].CustomAttributes[3].Arguments.Count==2);
			Assert ("Class2 4th attrib - first arg is SecurityAction.InheritanceDemand",
				((string) ((CodeFieldReferenceExpression)  u.Namespaces[1].Types[0].CustomAttributes[3].Arguments[0].Value).FieldName)=="InheritanceDemand" &&
				((CodeTypeReferenceExpression) ((CodeFieldReferenceExpression)  u.Namespaces[1].Types[0].CustomAttributes[3].Arguments[0].Value).TargetObject).Type.BaseType=="SecurityAction"
				);
			Assert ("Class2 4th attrib - second arg is Name=\"FullTrust\"",
				u.Namespaces[1].Types[0].CustomAttributes[3].Arguments[1].Name=="Name" &&
				((string) ((CodePrimitiveExpression)  u.Namespaces[1].Types[0].CustomAttributes[3].Arguments[1].Value).Value)=="FullTrust"
				);
			Assert("Class2 val1 has 1 attribute with 3 args", u.Namespaces[1].Types[0].Members[0].CustomAttributes.Count==1 &&
				u.Namespaces[1].Types[0].Members[0].CustomAttributes[0].Arguments.Count==3);

			Assert("Class2 constructor has 1 attribute with 2 args", u.Namespaces[1].Types[0].Members[1].CustomAttributes.Count==1 &&
				u.Namespaces[1].Types[0].Members[1].CustomAttributes[0].Arguments.Count==2);

			Assert("Class2 prop has 1 attribute with 1 args", u.Namespaces[1].Types[0].Members[2].CustomAttributes.Count==1 &&
				u.Namespaces[1].Types[0].Members[2].CustomAttributes[0].Arguments.Count==1);

			Assert("Class2 methodA has 1 attribute with 0 args", u.Namespaces[1].Types[0].Members[3].CustomAttributes.Count==1 &&
				u.Namespaces[1].Types[0].Members[3].CustomAttributes[0].Arguments.Count==0);

			Assert("Class2 methodA 1st param has 2 attributes, fist atr has 1 arg  sencond has 0 ", 
				((CodeMemberMethod)u.Namespaces[1].Types[0].Members[3]).Parameters[0].CustomAttributes.Count==2 &&
				((CodeMemberMethod)u.Namespaces[1].Types[0].Members[3]).Parameters[0].CustomAttributes[0].Arguments.Count==1 &&
				((CodeMemberMethod)u.Namespaces[1].Types[0].Members[3]).Parameters[0].CustomAttributes[1].Arguments.Count==0);

			Assert("Class2 methodA 2st param has 1 attribute - SecondPar with 1 arg  -2.1", 
				((CodeMemberMethod)u.Namespaces[1].Types[0].Members[3]).Parameters[1].CustomAttributes.Count==1 &&
				((CodeMemberMethod)u.Namespaces[1].Types[0].Members[3]).Parameters[1].CustomAttributes[0].Arguments.Count==1 &&
				((CodeMemberMethod)u.Namespaces[1].Types[0].Members[3]).Parameters[1].CustomAttributes[0].Name=="SecondPar" &&
				((double) ((CodePrimitiveExpression) ((CodeMemberMethod)u.Namespaces[1].Types[0].Members[3]).Parameters[1].CustomAttributes[0].Arguments[0].Value).Value)==2.1);

			Assert("Class2 evt has 1 attribute with 3 args", u.Namespaces[1].Types[0].Members[4].CustomAttributes.Count==1 &&
				u.Namespaces[1].Types[0].Members[4].CustomAttributes[0].Arguments.Count==3);

			
			
			Assert("Class2 indexer has 2 attributes, 1st with 0 args, 2nd with 4 args", 
				u.Namespaces[1].Types[0].Members[5].CustomAttributes.Count==2 &&
				u.Namespaces[1].Types[0].Members[5].CustomAttributes[0].Arguments.Count==0 &&
				u.Namespaces[1].Types[0].Members[5].CustomAttributes[1].Arguments.Count==4);

		}

		public void TestAttribs2()
		{
			CodeCompileUnit u; 
			int n=TestFile(TST_FILES_DIR+"\\Attribs2.cs",out u);
			Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);

			
			Assert("Class2 has 1 attribute", u.Namespaces[1].Types[0].CustomAttributes.Count==1);
			Assert ("Class2 1th attrib is PermissionSet with 2 args",
				u.Namespaces[1].Types[0].CustomAttributes[0].Name=="PermissionSet" &&
				u.Namespaces[1].Types[0].CustomAttributes[0].Arguments.Count==2);
			Assert ("Class2 1th attrib - first arg is SecurityAction.InheritanceDemand",
				((string) ((CodeFieldReferenceExpression)  u.Namespaces[1].Types[0].CustomAttributes[0].Arguments[0].Value).FieldName)=="InheritanceDemand" &&
				((CodeTypeReferenceExpression) ((CodeFieldReferenceExpression)  u.Namespaces[1].Types[0].CustomAttributes[0].Arguments[0].Value).TargetObject).Type.BaseType=="SecurityAction"
				);
			Assert ("Class2 1th attrib - second arg is Name=\"FullTrust\"",
				u.Namespaces[1].Types[0].CustomAttributes[0].Arguments[1].Name=="Name" &&
				((string) ((CodePrimitiveExpression)  u.Namespaces[1].Types[0].CustomAttributes[0].Arguments[1].Value).Value)=="FullTrust"
				);

			Assert("MyDelegate has 1 attribute", u.Namespaces[1].Types[1].CustomAttributes.Count==1);
			Assert ("MyDelegate 1th attrib is DlgAttribute with 2 args",
				u.Namespaces[1].Types[1].CustomAttributes[0].Name=="DlgAttribute" &&
				u.Namespaces[1].Types[1].CustomAttributes[0].Arguments.Count==2);
			Assert ("MyDelegate 1th attrib - second arg is \"mfbu\"",
				((string) ((CodePrimitiveExpression)  u.Namespaces[1].Types[1].CustomAttributes[0].Arguments[1].Value).Value)=="mfbu"
				);

			Assert("INothing has 1 attribute", u.Namespaces[1].Types[2].CustomAttributes.Count==1);
			Assert ("INothing 1th attrib is IsReallyNothing with 0 args",
				u.Namespaces[1].Types[2].CustomAttributes[0].Name=="IsReallyNothing" &&
				u.Namespaces[1].Types[2].CustomAttributes[0].Arguments.Count==0);

			Assert("En1 has 1 attribute", u.Namespaces[1].Types[3].CustomAttributes.Count==1);
			Assert ("En1 1th attrib is FlagsAttribute with 0 args",
				u.Namespaces[1].Types[3].CustomAttributes[0].Name=="FlagsAttribute" &&
				u.Namespaces[1].Types[3].CustomAttributes[0].Arguments.Count==0);

			Assert("Str1 has 1 attribute", u.Namespaces[1].Types[4].CustomAttributes.Count==1);
			Assert ("Str1 1th attrib is ValAttribute with 3 args",
				u.Namespaces[1].Types[4].CustomAttributes[0].Name=="ValAttribute" &&
				u.Namespaces[1].Types[4].CustomAttributes[0].Arguments.Count==3);



		}
			public void TestComments()
			{
				CodeCompileUnit u; 
				int n=TestFile(TST_FILES_DIR+"\\Comments.cs",out u);
				Assert("Parsing Errors Found - (no of Errors "+n.ToString()+")",n==0);
	
				Assert("Namespace testParser", u.Namespaces[1].Name=="testParser");
				Assert("Namespace has 2 cmts", u.Namespaces[1].Comments.Count==2);
				Assert("Namespace comment one is \"Ns coment\"",
					u.Namespaces[1].Comments[0].Comment.Text.Trim()=="Ns coment");
				Assert("Namespace comment two is \"After NS comment\"",
					u.Namespaces[1].Comments[1].Comment.Text.Trim()=="After NS comment");

				Assert("Class Class2", u.Namespaces[1].Types[0].Name=="Class2" &&
				u.Namespaces[1].Types[0].IsClass);
				Assert("Class has 4 cmts", u.Namespaces[1].Types[0].Comments.Count==4);
				Assert("Class comment 1 -3 is doc comment",
					u.Namespaces[1].Types[0].Comments[0].Comment.DocComment &&
					u.Namespaces[1].Types[0].Comments[1].Comment.DocComment &&
					u.Namespaces[1].Types[0].Comments[2].Comment.DocComment
					);
				Assert("Class  4th cmt is NOT doc cmt", 
					! u.Namespaces[1].Types[0].Comments[3].Comment.DocComment);


				Assert("Class2 - val1", 
					u.Namespaces[1].Types[0].Members[0].Name=="val1" &&
					(u.Namespaces[1].Types[0].Members[0] is CodeMemberField));
				Assert("val1 has 1 cmts", u.Namespaces[1].Types[0].Members[0].Comments.Count==1);

				Assert("Class2 - i", 
					u.Namespaces[1].Types[0].Members[1].Name=="i" &&
					(u.Namespaces[1].Types[0].Members[1] is CodeMemberField));
				Assert("i has 3 cmts", u.Namespaces[1].Types[0].Members[1].Comments.Count==3);
				

				Assert("Class2 - j", 
					u.Namespaces[1].Types[0].Members[2].Name=="j" &&
					(u.Namespaces[1].Types[0].Members[2] is CodeMemberField));
				Assert("j has 0 cmts", u.Namespaces[1].Types[0].Members[2].Comments.Count==0);

				Assert("Class2 - constructor", 	
					(u.Namespaces[1].Types[0].Members[4] is CodeConstructor));
				Assert("constructor has 3 cmts", u.Namespaces[1].Types[0].Members[4].Comments.Count==3);
				

				Assert("Class2 - prop", 
					u.Namespaces[1].Types[0].Members[5].Name=="prop" &&
					(u.Namespaces[1].Types[0].Members[5] is CodeMemberProperty));
				Assert("prop has 3 cmts", u.Namespaces[1].Types[0].Members[5].Comments.Count==3);
				Assert("prop comment one is \"Old style multiline\"",
					u.Namespaces[1].Types[0].Members[5].Comments[0].Comment.Text.Trim()=="Old style multiline");
				Assert("prop comment two is \"* something here\"",
					u.Namespaces[1].Types[0].Members[5].Comments[1].Comment.Text.Trim()=="* something here");
				Assert("prop comment three is \"\"",
					u.Namespaces[1].Types[0].Members[5].Comments[2].Comment.Text.Trim()=="");
				

				Assert("Class2 - methodA", 
					u.Namespaces[1].Types[0].Members[6].Name=="methodA" &&
					(u.Namespaces[1].Types[0].Members[6] is CodeMemberMethod));
				Assert("methodA has 6 cmts", u.Namespaces[1].Types[0].Members[6].Comments.Count==6);
				

				Assert("Class2 - evt", 
					u.Namespaces[1].Types[0].Members[7].Name=="evt" &&
					(u.Namespaces[1].Types[0].Members[7] is CodeMemberEvent));
				Assert("evt has 1 cmts", u.Namespaces[1].Types[0].Members[7].Comments.Count==1);

				Assert("Class2 - indexer", 
					u.Namespaces[1].Types[0].Members[8].Name=="Item" &&
					(u.Namespaces[1].Types[0].Members[8] is CodeMemberProperty)&&
					((CodeMemberProperty) u.Namespaces[1].Types[0].Members[8]).Parameters.Count==1);
				Assert("indexer has 3 cmts", u.Namespaces[1].Types[0].Members[8].Comments.Count==3);
				
				Assert("Delegate MyDelegate", u.Namespaces[1].Types[1].Name=="MyDelegate" &&
					u.Namespaces[1].Types[1] is CodeTypeDelegate);
				Assert("delegate has 3 cmts", u.Namespaces[1].Types[1].Comments.Count==3);

				Assert("Interface INothing", u.Namespaces[1].Types[2].Name=="INothing" &&
					u.Namespaces[1].Types[2].IsInterface);
				Assert("interface has 3 cmts", u.Namespaces[1].Types[2].Comments.Count==3);

				Assert("Enum En1", u.Namespaces[1].Types[3].Name=="En1" &&
					u.Namespaces[1].Types[3].IsEnum);
				Assert("enum has 1 cmts", u.Namespaces[1].Types[3].Comments.Count==1);
				Assert("enum 1st value has 3 cmts", u.Namespaces[1].Types[3].Members[0].Comments.Count==3);

				Assert("Struct Str1", u.Namespaces[1].Types[4].Name=="Str1" &&
					u.Namespaces[1].Types[4].IsStruct);
				Assert("struct has 3 cmts", u.Namespaces[1].Types[4].Comments.Count==3);
               

		}



		public static ITest Suite 
		{
			get 
			{
				TestSuite s = new TestSuite();

				s.AddTest(new Test("TestInnerNS"));

				s.AddTest(new Test("TestNSandClasses"));
				
				s.AddTest(new Test("TestTypes"));
				s.AddTest(new Test("TestFields"));
				s.AddTest(new Test("TestMethods"));
				s.AddTest(new Test("TestProperties"));
				s.AddTest(new Test("TestEvents"));
				s.AddTest(new Test("TestConstructors"));
				
				s.AddTest(new Test("TestStruct"));
				s.AddTest(new Test("TestItf"));
				s.AddTest(new Test("TestEnum"));
				s.AddTest(new Test("TestExpressionLight"));
				s.AddTest(new Test("TestAttribs"));
				s.AddTest(new Test("TestAttribs2"));
				s.AddTest(new Test("TestComments"));
				return s;
			}
		}

	}
}
