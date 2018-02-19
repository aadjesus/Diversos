using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galador.Applications
{
	public class Invoker
	{
		public static void BeginInvoke(Action a)
		{
			BeginInvoke((Delegate)a);
		}

		public static void BeginInvoke(Delegate method, params object[] args)
		{
#if SILVERLIGHT
			System.Windows.Deployment.Current.Dispatcher.BeginInvoke(method, args);
#else
			System.Windows.Application.Current.Dispatcher.BeginInvoke(method, args);
#endif
		}

		public static void DelayInvoke(TimeSpan delay, Action a)
		{
			DelayInvoke(delay, (Delegate)a);
		}

		public static void DelayInvoke(TimeSpan delay, Delegate method, params object[] args)
		{
			if (delay.Ticks < 100)
			{
				Invoker.BeginInvoke(method, args);
			}
			else
			{
				new System.Threading.Thread(delegate()
				{
					System.Threading.Thread.Sleep(delay);
					Invoker.BeginInvoke(method, args);
				})
				{
					IsBackground = true,
					Name = "DelayedAction",
				}
				.Start();
			}
		}
	}
}
