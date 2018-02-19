using System;
using System.Reflection;

namespace MethodInvoke
{
	public class CInvoker
	{
		public CInvoker()
		{}

		public object InvokeMethod (string method, object[] args)
		{
			// create an object of the external class that
			// implemented the method.
			CExternal objExternal = new CExternal();

			// get the type and methodinfo
			Type typExternal = Type.GetType("MethodInvoke.CExternal");
			MethodInfo methodInf = typExternal.GetMethod(method);

			// invoke the method
			object ret = methodInf.Invoke(objExternal, args);

			// return object
			return ret;
		}
	}

	public class CMain
	{
		public CMain()
		{}

		public static void Main()
		{
			CInvoker ink = new CInvoker();
			string[] args = {"Jitesh" , "patil"};
			ink.InvokeMethod("Fullname", args);
		}
	}
}
