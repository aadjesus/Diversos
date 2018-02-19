using System;

namespace Galador.Applications
{
	static class WeakReferenceEx
	{
		public static T Get<T>(this WeakReference w) { return (T)w.Target; }
	}
}
