using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cx.EventArgs
{
	public delegate void CxObjectDlgt(object sender, CxEventArgs<object> args);
	public delegate void CxCharDlgt(object sender, CxEventArgs<char> args);
	public delegate void CxBoolDlgt(object sender, CxEventArgs<bool> args);
	public delegate void CxStringDlgt(object sender, CxEventArgs<string> args);
	public delegate void CxStringPairDlgt(object sender, CxEventArgs<CxStringPair> args);
	public delegate void CxObjectStateDlgt(object sender, CxEventArgs<CxObjectState> args);
	public delegate void CxEnumerableDlgt(object sender, CxEventArgs<IEnumerable> args);

	public class CxStringPair
	{
		public string Data1 { get; set; }
		public string Data2 { get; set; }

		public CxStringPair(string s1, string s2)
		{
			Data1 = s1;
			Data2 = s2;
		}
	}

	/// <summary>
	/// Used to communicate the state of an object.  The context depends on the producer's usage of this message.
	/// </summary>
	public class CxObjectState
	{
		public object Object { get; set; }
		public bool State { get; set; }

		public CxObjectState(object obj, bool state)
		{
			Object = obj;
			State = state;
		}
	}

	public class CxEventArgs<T> : System.EventArgs
	{
		public T Data { get; set; }

		public CxEventArgs(T val)
		{
			Data = val;
		}
	}
}
