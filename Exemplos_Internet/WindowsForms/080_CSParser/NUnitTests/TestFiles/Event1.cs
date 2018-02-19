using System;
using System.Collections;

namespace testParser
{
	#region Blabla
	using System.IO;
	/// <summary>
	/// Summary description for Class2.
	/// </summary>
	abstract public class Class2: object, System.Collections.ICollection
	{
		
		
		
		private int i;
		
		public event DlgType ev1;
		public override event DlgType ev2, ev3;

		public virtual event DlgType ev4
		{
			add { }
			remove {}
		}
		
	}

	
	
#endregion	
}

