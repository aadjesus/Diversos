/*
 * bla bla bla
 * more more more more
 */
using System;
// using comment
using System.Collections;
using System.Reflection;


// Ns coment
namespace testParser //After NS comment
{
	
	
	/// <summary>
	/// test class
	/// </summary>
	[PermissionSet(SecurityAction.InheritanceDemand, Name="FullTrust")]
	 public class Class2 //After Class
	{
		 
		// Old style comment
		string val1="bla";	

		/// <summary>
		/// 3 in one statement
		/// </summary>
		int i,j,k;
		/// <summary>
		/// 
		/// </summary>
		 public Class2() {}		
		
		 /* Old style multiline
		  * something here 
		  */
		 public string prop 
		 {		
			get { return "";}
		 }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <returns></returns>
		public  int methodA( int i, int j)  // After method
		
		 {
			//Inside method
		 return 0;
			//before local declaration
			int i;
		 }

		
		//single line comment
		public event EventHandler evt;

		/// <summary>
		/// indexer - why it did not generate tag for param automatically
		/// </summary>
		public object this[int i]
		 {
			//inside indexer block 
			get {
				 
				 return null;
			 }

		 }

	}

    /// <summary>

    /// something
    /// </summary>
	public delegate int MyDelegate([PAttribute]object o);

	/*
	 * anything 
	 */

	[IsReallyNothing]
	public interface INothing
	{}

	[FlagsAttribute]
	public enum En1 // After enum
	{
		/// <summary>
		/// Value 1
		/// </summary>
		[V1Attribute("mfbu")]
		val1=1,
		[V2Attribute("mfbu")]
		val2=2
	}

	[ValAttribute(true, loc="somewhere", mode="auto")]
	/// <summary>
	/// structure
	/// </summary>
	public struct Str1 {} 

	
	

}



