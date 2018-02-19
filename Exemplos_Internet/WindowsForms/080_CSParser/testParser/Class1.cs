// Class1.cs: Test cmd line utility for CS CODEDOM Parser
//
// Written by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Mono.CSharp;
using IvanZ.CSParser;
using System.IO;
using System.Text;
using System.Collections;

namespace testParser
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length<1)
			{
				Console.WriteLine("Input file has to be commad line param!");
				return;
			}

			ArrayList l =null;
			if (args.Length>1)
			l = new ArrayList(args.Length-1);
			for (int i=1; i<args.Length;i++) l.Add(args[i]);
			string name = new FileInfo(args[0]).Name;

			FileStream fstream = File.Open( args[0], FileMode.Open, FileAccess.Read, FileShare.Read);
			CSharpParser p= new CSharpParser(name, fstream, l);
			p.yacc_verbose=false;
			int errno= p.parse();
			if (errno>0)
				Console.WriteLine("Input file has {0} syntacticals errors", errno);
			else
			if (p.Builder.CurrCompileUnit!=null) 
						{
							
			
							CodeDomProvider provider= new CSharpCodeProvider();
							StringBuilder sb = new StringBuilder();
							StringWriter sw = new StringWriter(sb);
							ICodeGenerator generator= provider.CreateGenerator();
							generator.GenerateCodeFromCompileUnit(p.Builder.CurrCompileUnit,sw,new CodeGeneratorOptions());
							Console.WriteLine("CODEDOM code: \n" + sb.ToString());
						}

			Console.WriteLine("Finished, press any key to exit");
			Console.Read();
		}
	}
}
