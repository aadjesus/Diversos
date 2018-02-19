using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Linq.Expressions;

namespace Galador.Applications
{
	#region ThreadOption, EventSubscription<TTarget, TArg>

	public enum ThreadOption
	{
		PublisherThread = 0,
		UIThread = 1,
		BackgroundThread = 2,
	}

	public abstract class EventSubscription<TArg>
	{
		public abstract bool IsAlive { get; }
		public abstract void Invoke(TArg payload);
	}

	internal class EventSubscription<TTarget, TArg> : EventSubscription<TArg>
	{
		public EventSubscription(TTarget target, Action<TTarget, TArg> action, ThreadOption option, bool keepSubscriberReferenceAlive, Func<TTarget, TArg, bool> filter, bool instanceOK = false)
		{
			if (action == null)
				throw new ArgumentNullException("action");
			if (!instanceOK && !action.Method.IsStatic)
				throw new ArgumentException("action must be a static method", "action");
			if (!instanceOK && filter != null && !filter.Method.IsStatic)
				throw new ArgumentException("filter must be a static method", "filter");

			this.mFilter = filter;
			this.mOption = option;
			this.mTarget = target != null ? new WeakReference(target) : null;
			this.mAction = action;
		}

		ThreadOption mOption;
		Func<TTarget, TArg, bool> mFilter;
		Action<TTarget, TArg> mAction;
		WeakReference mTarget;

		public override bool IsAlive
		{
			get
			{
				if (mTarget == null)
					return true;
				return mTarget.IsAlive;
			}
		}

		public override void Invoke(TArg payload)
		{
			TTarget target = default(TTarget);
			if (mTarget != null)
			{
				target = mTarget.Get<TTarget>();
				if (target == null)
					return;
			}

			if (mFilter != null && !mFilter(target, payload))
				return;

			switch (mOption)
			{
				default:
				case ThreadOption.PublisherThread:
					mAction(target, payload);
					break;
				case ThreadOption.UIThread:
					Invoker.BeginInvoke(mAction, target, payload);
					break;
				case ThreadOption.BackgroundThread:
					ThreadPool.QueueUserWorkItem(delegate(object o) { mAction(target, payload); });
					break;
			}
		}

		public override bool Equals(object obj)
		{
			var o = obj as EventSubscription<TTarget, TArg>;
			if (o == null)
				return false;
			if (mTarget == null || o.mTarget == null)
				if (mTarget != o.mTarget)
					return false;
			if (mTarget != null && !Equals(mTarget.Get<TTarget>(), o.mTarget.Get<TTarget>()))
				return false;
			if (o.mAction != mAction)
				return false;
			return true;
		}
		public override int GetHashCode() { return (mTarget != null ? mTarget.GetHashCode() : 0) ^ mAction.GetHashCode(); }
	}

	#endregion

	/// <summary>
	/// This app class help create disconnected events where the sender and target don't know each other
	/// but know about a common event arg
	/// </summary>
	public static class Notifications
	{
		static Dictionary<Type, object> mSubscriptions = new Dictionary<Type, object>();

		public static EventSubscription<TArg> Subscribe<TTarget, TArg>(TTarget target, Action<TTarget, TArg> action, ThreadOption option = ThreadOption.UIThread, bool keepSubscriberReferenceAlive = false, Func<TTarget, TArg, bool> filter = null)
		{
			var ev = new EventSubscription<TTarget, TArg>(target, action, option, keepSubscriberReferenceAlive, filter);
			lock (mSubscriptions)
				GetSubscriptions<TArg>(true).Add(ev);
			return ev;
		}

		/// <summary>
		/// Subscribe all method of the target tagged with <see cref="NotificationHandlerAttribute"/>
		/// </summary>
		public static void Register<TTarget>(TTarget target, ThreadOption option = ThreadOption.UIThread, bool keepSubscriberReferenceAlive = false)
		{
			if (target == null)
				return;

			var methods =
				from MethodInfo mi in target.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
				where mi.GetParameters().Length == 1
				from nha in mi.GetCustomAttributes(typeof(NotificationHandlerAttribute), true)
				select mi;

			lock (mSubscriptions)
				foreach (var mi in methods)
					GetSubscriber<TTarget>(mi)(target, mi, option, keepSubscriberReferenceAlive);
		}

		#region Register() internals

		static Action<TTarget, MethodInfo, ThreadOption, bool> GetSubscriber<TTarget>(MethodInfo mi)
		{
			var t = mi.GetParameters()[0].ParameterType;
			var mt = typeof(Notifications).GetMethod("MISubscribe", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(typeof(TTarget), t);
			return (Action<TTarget, MethodInfo, ThreadOption, bool>)Delegate.CreateDelegate(typeof(Action<TTarget, MethodInfo, ThreadOption, bool>), mt);
		}
		static internal void MISubscribe<TTarget, TArg>(TTarget target, MethodInfo method, ThreadOption option, bool keepSubscriberReferenceAlive)
		{
			Action<TTarget, TArg> action = (t, args) => method.Invoke(t, new object[] { args });
			var ev = new EventSubscription<TTarget, TArg>(target, action, option, keepSubscriberReferenceAlive, null, true);
				GetSubscriptions<TArg>(true).Add(ev);
		}

		#endregion

		public static void Unsubscribe<TTarget, TArg>(TTarget target, Action<TTarget, TArg> action)
		{
			lock (mSubscriptions)
			{
				var l = GetSubscriptions<TArg>(false);
				if (l == null)
					return;

				var ev = new EventSubscription<TTarget, TArg>(target, action, ThreadOption.UIThread, false, null, true);
				for (int i = l.Count; i-- > 0; )
				{
					if (Equals(l[i], ev))
					{
						l.RemoveAt(i);
						break;
					}
				}

				Trim<TArg>();
			}
		}

		public static void Unsubscribe<TArg>(EventSubscription<TArg> evs)
		{
			lock (mSubscriptions)
			{
				var l = GetSubscriptions<TArg>(false);
				if (l == null)
					return;

				for (int i = l.Count; i-- > 0; )
				{
					if (l[i] == evs)
					{
						l.RemoveAt(i);
						break;
					}
				}

				Trim<TArg>();
			}
		}

		/// <summary>
		/// Publishes the payload data.
		/// </summary>
		/// <param name="payload">Message to pass to the subscribers.</param>
		public static void Publish<T>(T payload)
		{
			List<EventSubscription<T>> l;
			lock (mSubscriptions)
			{
				l = GetSubscriptions<T>(false);
				if (l == null)
					return;

				// make a local copy (thread safety)
				l = new List<EventSubscription<T>>(l);
			}

			foreach (var item in l)
				item.Invoke(payload);

			lock (mSubscriptions)
				Trim<T>();
		}

		#region utils: Trim<T>(), GetSubscriptions<T>()

		static void Trim<TArg>()
		{
			var l = GetSubscriptions<TArg>(false);
			if (l == null)
				return;

			for (int i = l.Count; i-- > 0; )
				if (!l[i].IsAlive)
					l.RemoveAt(i);

			if (l.Count == 0)
				mSubscriptions.Remove(typeof(TArg));
		}

		static List<EventSubscription<TArg>> GetSubscriptions<TArg>(bool create)
		{
			List<EventSubscription<TArg>> l = null;
			object o;
			if (mSubscriptions.TryGetValue(typeof(TArg), out o))
				l = (List<EventSubscription<TArg>>)o;

			if (create && l == null)
			{
				l = new List<EventSubscription<TArg>>();
				mSubscriptions[typeof(TArg)] = l;
			}

			return l;
		}

		#endregion
	}

	/// <summary>
	/// Method (with one parameter) of the target with this attribute, get <see cref="Notifications.Subscribe"/>
	/// when <see cref="Notifications.Register"/> is called on the target.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class NotificationHandlerAttribute : Attribute
	{
	}
}
