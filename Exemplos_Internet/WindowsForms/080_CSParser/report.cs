//
// report.cs: report errors and warnings.
//
// Author: Miguel de Icaza (miguel@ximian.com)
//
// (C) 2001 Ximian, Inc. (http://www.ximian.com)
//

// Modified by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)
using System;
using System.Collections;
using System.Diagnostics;

namespace Mono.CSharp {

	/// <summary>
	///   This class is used to report errors and warnings t te user.
	/// </summary>
	public class Report {
		/// <summary>  
		///   Errors encountered so far
		/// </summary>
		static public int Errors;

		/// <summary>  
		///   Warnings encountered so far
		/// </summary>
		static public int Warnings;

		/// <summary>  
		///   Whether errors should be throw an exception
		/// </summary>
		static public bool Fatal;
		
		/// <summary>  
		///   Whether warnings should be considered errors
		/// </summary>
		static public bool WarningsAreErrors;

		/// <summary>  
		///   Whether to dump a stack trace on errors. 
		/// </summary>
		static public bool Stacktrace;
		
		
		static public void Clear()
		{
			Errors=0;
			Warnings=0;
		}
		
		static public void RealError (string msg)
		{
			Errors++;
			Console.WriteLine (msg);

			if (Stacktrace)
				Console.WriteLine (new StackTrace ().ToString ());
			if (Fatal)
				throw new Exception (msg);
		}

		static public void Error (int code, Location l, string text)
		{
			string msg = String.Format (
//				"{0}({1}) error CS{2:0000}: {3}", l.Name, l.Row, code, text);
				"{0}({1}) error CS{2}: {3}", l.Name, l.Row, code, text);
			
			RealError (msg);
			
		}

		static public void Warning (int code, Location l, string text)
		{
			
			
			if (WarningsAreErrors)
				Error (code, l, text);
			else {
				string row;
				
				if (Location.IsNull (l))
					row = "";
				else
					row = l.Row.ToString ();
				
				Console.WriteLine (String.Format (
//					"{0}({1}) warning CS{2:0000}: {3}",
					"{0}({1}) warning CS{2}: {3}",
					l.Name,  row, code, text));
				Warnings++;
				
			}
		}
		
		static public void Warning (int code, string text)
		{
			Warning (code, Location.Null, text);
		}

		static public void Error (int code, string text)
		{
//			string msg = String.Format ("error CS{0:0000}: {1}", code, text);
			string msg = String.Format ("error CS{0}: {1}", code, text);
			
			RealError (msg);
			
		}

		static public void Message (Message m)
		{
			if (m is ErrorMessage)
				Error (m.code, m.text);
			else
				Warning (m.code, m.text);
		}

		
		
		
	}

	public class Message {
		public int code;
		public string text;
		
		public Message (int code, string text)
		{
			this.code = code;
			this.text = text;
		}
	}

	public class WarningMessage : Message {
		public WarningMessage (int code, string text) : base (code, text)
		{
		}
	}

	public class ErrorMessage : Message {
		public ErrorMessage (int code, string text) : base (code, text)
		{
		}

		//
		// For compatibility reasons with old code.
		//
		public static void report_error (string error)
		{
			Console.Write ("ERROR: ");
			Console.WriteLine (error);
		}
	}
}


