using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Galador.Applications
{
	public class EventRegistration : IDisposable
	{
		readonly EventInfo eInfo;
		readonly Delegate handler;

		public EventRegistration(EventInfo eInfo)
		{
			if (eInfo == null)
				throw new ArgumentNullException();
			this.eInfo = eInfo;
			handler = Delegate.CreateDelegate(eInfo.EventHandlerType, this, ((Action<object, object>)OnEvent).Method);

			if (eInfo.GetAddMethod() != null && eInfo.GetAddMethod().IsStatic)
				eInfo.AddEventHandler(null, handler);
		}

		public static EventRegistration Create<TSource>(Expression<Func<TSource, Delegate>> getEvent)
		{
			var epath = PropertyUtils.GetPath(getEvent);
			var tp = epath.Last();
			var ei = tp.DeclaringType.GetEvent(tp.Name);
			return new EventRegistration(ei);
		}

		public static EventRegistration Create(Expression<Func<Delegate>> getEvent)
		{
			var epath = PropertyUtils.GetPath(getEvent);
			var tp = epath.Last();
			var ei = tp.DeclaringType.GetEvent(tp.Name);
			return new EventRegistration(ei);
		}

		public object Target
		{
			get { return target.Target; }
			set
			{
				if (value != null && !eInfo.DeclaringType.IsInstanceOfType(value))
					throw new InvalidCastException();

				var prev = Target;
				if (prev == value)
					return;

				if (prev != null)
					eInfo.RemoveEventHandler(prev, handler);

				target.Target = value;

				if (value != null)
					eInfo.AddEventHandler(value, handler);
			}
		}
		WeakReference target = new WeakReference(null);

		public void OnEvent(object sender, object args)
		{
			if (Event != null)
				Event(sender, args);
		}

		public Action<object, object> Event;

		public void Dispose()
		{
			Target = null;
			if (eInfo.GetRemoveMethod() != null && eInfo.GetRemoveMethod().IsStatic)
				eInfo.RemoveEventHandler(null, handler);
		}
	}
}
