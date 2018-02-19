
// TestProjectFiles.cs: NUNIT Tests for CS CODEDOM Parser
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
    
    
    
	public class TestProjectFiles : TestCase
	{
        
		private const string DIR=@".";
		private FileInfo[] filesToTest;
 
		public TestProjectFiles(string TestName) : 
			base(TestName)
		{
		}


        
		protected override void SetUp()
		{
			
			DirectoryInfo d=new DirectoryInfo(DIR);
			Console.WriteLine("Testing all files in directory: {0}", d.FullName);
			filesToTest=d.GetFiles("*.cs");


		}
		
		public void TestIt()
		{
			foreach(FileInfo f in filesToTest)
			{
				if (f.Name!="xxxcs-parser.cs")
				{
					Console.WriteLine("Testing File: {0}", f.FullName);
					int n=	TestFile(f);
					Assert("Parsing Errors Found on file "+ f.Name+" - (no of Errors "+n.ToString()+")",n==0);
				}
			}
		}
		
		protected int TestFile(FileInfo file)
		{
			FileStream fstream = file.Open( FileMode.Open, FileAccess.Read, FileShare.Read);
			CSharpParser p= new CSharpParser(file.Name, fstream, null);
			int errno= p.parse();
			CodeCompileUnit unit;
			unit=p.Builder.CurrCompileUnit;
			Assert("CompileUnit is not null", unit!=null);
			return errno;
			

		}

		public static ITest Suite 
		{
			get 
			{
				TestSuite s = new TestSuite();

				s.AddTest(new TestProjectFiles("TestIt"));
				return s;
			}
		}



	}
}
