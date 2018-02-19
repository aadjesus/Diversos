using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Galador.Utils
{
	public class PriorityThreadPool : IDisposable
	{
		public PriorityThreadPool()
		{
			IsBackground = true;
			ThreadCount = Environment.ProcessorCount;
		}

		#region public static PriorityThreadPool Shared

		public static PriorityThreadPool Shared { get { return cShared; } }
		static PriorityThreadPool cShared = new PriorityThreadPool() { ThreadCount = Environment.ProcessorCount, IsBackground = true };

		#endregion

		#region ThreadName, IsBackground, ExceptionHandler, SyncLock

		/// <summary>
		/// A name that would be used for the pool's new thread's name.
		/// </summary>
		public string ThreadName { get; set; }

		/// <summary>
		/// The value use for the pool's new thread's IsBackground property.
		/// </summary>
		public bool IsBackground { get; set; }

		/// <summary>
		/// Handle the exception. Return true if it has been handled, or false to rethrow.
		/// </summary>
		public Predicate<Exception> ExceptionHandler { get; set; }

		/// <summary>
		/// The lock use internally for managing the pool's threads.
		/// </summary>
		public object SyncLock { get { return queueLock; } }

		#endregion

		#region QueueUserWorkItem

		public void QueueUserWorkItem(
			int priority,
			WaitCallback callBack)
		{
			QueueUserWorkItem(priority, callBack, null);
		}
		public void QueueUserWorkItem(
			int priority,
			WaitCallback callBack,
			object state)
		{
			if (callBack == null)
				return;
			var q = new QueueItem
			{
				Priority = priority,
				Callback = callBack,
				State = state
			};
			Enqueue(q);
		}

		#endregion

		#region ThreadCount, IdleThreadCount, BusyThreadCount, PendingItemCount

		int threadCount = 4;
		/// <summary>
		/// Maximum number of thread to create. 0 Will stop it. This is a control parameter.
		/// The pool can have less thread if it has been stressed yet or more thread if the value has just been changed.
		/// </summary>
		public int ThreadCount
		{
			get { return threadCount; }
			set
			{
				lock (queueLock)
				{
					if (IsDisposed)
						throw new ObjectDisposedException(GetType().FullName);

					if (value == threadCount)
						return;
					if (value < 0 || value > 50)
						throw new ArgumentOutOfRangeException("value");

					threadCount = value;
					wait.Set();
				}
				UpdateIdleInfo();
			}
		}

		/// <summary>
		/// The idle thread count is the number of thread which have nothing to do for the foreseeable future.
		/// It's not <c>ThreadCount - BusyThreadCount</c> as it include information about pending task too.
		/// </summary>
		public int IdleThreadCount
		{
			get
			{
				lock (queueLock)
				{
					if (IsDisposed)
						return 0;
					int F = ThreadCount - threads.Count;
					F += (from t in threads where !t.IsRunning select t).Count();
					F -= pending.Count;
					if (F < 0)
						F = 0;
					return F;
				}
			}
		}

		/// <summary>
		/// The number of thread currently working
		/// </summary>
		public int BusyThreadCount
		{
			get
			{
				lock (queueLock)
				{
					if (IsDisposed)
						return 0;
					return (from t in threads where t.IsRunning select t).Count();
				}
			}
		}

		public int PendingItemCount
		{
			get
			{
				lock (queueLock)
				{
					if (IsDisposed)
						return 0;
					return pending.Count;
				}
			}
		}

		#endregion

		#region HasIdleThread, HasIdleThreadChanged, Idle, IdleChanged

		object idleLock = new object();

		bool hasIdleThread = true;
		public bool HasIdleThread
		{
			get { return hasIdleThread; }
			private set
			{
				lock (idleLock)
				{
					if (value == hasIdleThread)
						return;
					hasIdleThread = value;
				}
				OnHasIdleThreadChanged();
			}
		}

		public event EventHandler HasIdleThreadChanged
		{
			add
			{
				lock (idleLock)
					hasIdleThreadEvent += value;
			}
			remove
			{
				lock (idleLock)
					hasIdleThreadEvent -= value;
			}
		}
		EventHandler hasIdleThreadEvent;

		void OnHasIdleThreadChanged()
		{
			EventHandler ev;
			lock (idleLock)
				ev = hasIdleThreadEvent;
			if (ev != null)
				try
				{
					ev(this, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					var h = ExceptionHandler;
					if (h == null || !h(ex))
					{
						// remove the dead thread
						lock (queueLock)
							threads.RemoveAll(ti => ti.Thread == Thread.CurrentThread);
						throw;
					}
				}
		}

		/// <summary>
		/// Returns true if there are no pending and running action.
		/// </summary>
		public bool Idle
		{
			get { return isIdle; }
			private set
			{
				lock (idleLock)
				{
					if (value == isIdle)
						return;
					isIdle = value;
				}
				OnIdleChanged();
			}
		}
		bool isIdle;

		public event EventHandler IdleChanged
		{
			add
			{
				lock (idleLock)
					idleChanged += value;
			}
			remove
			{
				lock (idleLock)
					idleChanged -= value;
			}
		}
		EventHandler idleChanged;

		void OnIdleChanged()
		{
			EventHandler ev;
			lock (idleLock)
				ev = idleChanged;
			if (ev != null)
				try
				{
					ev(this, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					var h = ExceptionHandler;
					if (h == null || !h(ex))
					{
						// remove the dead thread
						lock (queueLock)
							threads.RemoveAll(ti => ti.Thread == Thread.CurrentThread);
						throw;
					}
				}
		}

		void UpdateIdleInfo()
		{
			bool idleThread, idle = true;
			lock (queueLock)
			{
				if (pending.Count > 0)
				{
					idle = false;
				}
				else
				{
					foreach (var item in threads)
					{
						if (item.IsRunning)
						{
							idle = false;
							break;					
						}
					}
				}

				idleThread = IdleThreadCount > 0;
			}
			HasIdleThread = idleThread;
			Idle = idle;
		}

		#endregion

		#region inner workings

		// struct are light, although, arguable, the WaitCallback is going to be heavy...
		struct QueueItem
		{
			public int Priority;
			public WaitCallback Callback;
			public object State;
		}
		// it is a class, so it is passed as reference, so it can be found in the list
		class ThreadItem
		{
			public Thread Thread;
			public QueueItem? Item;
			public bool IsRunning { get { return Item.HasValue; } }
		}

		object queueLock = new object();
		ManualResetEvent wait = new ManualResetEvent(true);
		List<ThreadItem> threads = new List<ThreadItem>();
		List<QueueItem> pending = new List<QueueItem>();
		void Enqueue(QueueItem qi)
		{
			lock (queueLock)
			{
				if (IsDisposed)
					throw new ObjectDisposedException(GetType().FullName);

				// test if a new thread needs to be created
				int nRunning = 1 + pending.Count + (from t in threads where t.IsRunning select t).Count();
				if (nRunning > threads.Count && threads.Count < ThreadCount)
				{
					var name = ThreadName;
					var item = new ThreadItem
					{
						Thread = new Thread(ThreadRun)
						{
							IsBackground = this.IsBackground,
							Name = !string.IsNullOrEmpty(name) ? (name + " - " + threads.Count) : null
						}
					};
					threads.Add(item);
					item.Thread.Start(item);
				}

				// do not reuse the thread, always queue the item
				pending.Add(qi);
				UpdateIdleInfo();

				// release threads
				wait.Set();
			}
			// give a chance to the other thread to catch up!
			Thread.Sleep(0);
		}

		void TryDequeue(ThreadItem target)
		{
			lock (queueLock)
			{
				if (pending.Count == 0)
				{
					wait.Reset();
					target.Item = null;
					return;
				}

				int imax = 0;
				int maxp = pending[0].Priority;
				for (int i = 1; i < pending.Count; i++)
				{
					if (pending[i].Priority > maxp)
					{
						imax = i;
						maxp = pending[i].Priority;
					}
				}
				target.Item = pending[imax];
				pending.RemoveAt(imax);
				UpdateIdleInfo();
			}
		}

		void ThreadRun(object state)
		{
			var self = (ThreadItem)state;
			try
			{
				while (true)
				{
					lock (queueLock)
					{
						if (threads.Count > threadCount)
							return;
						TryDequeue(self); // careful with Dequeue() it Reset() the wait handle
					}

					if (self.IsRunning)
						ClientRun(self);

					lock (queueLock)
					{
						if (IsDisposed)
							return;

						self.Item = null;
						UpdateIdleInfo();

						// ThreadCount has reduced... some thread should die...
						if (threads.Count > threadCount)
							return;

						if (pending.Count > 0)
							continue;
					}

					wait.WaitOne();
				}
			}
			catch (ObjectDisposedException) { return; }
			finally
			{
				lock (queueLock)
					threads.Remove(self);
				UpdateIdleInfo();
			}
		}
		void ClientRun(ThreadItem ti)
		{
			try
			{
				var qi = ti.Item.Value;
				qi.Callback(qi.State);
			}
			catch(Exception ex)
			{
				var h = ExceptionHandler;
				if (h == null || !h(ex))
				{
					// remove the dead thread
					lock (queueLock)
						threads.Remove(ti);
					throw;
				}
			}
		}

		#endregion

		#region IDisposable

		public bool IsDisposed { get; private set; }

		public void Dispose()
		{
			// gentle quit...
			lock (queueLock)
			{
				ThreadCount = 0;
				pending.Clear();
				IsDisposed = true;
			}
			wait.Set();
			wait.Close();
		}

		#endregion

		#region ToString() (print some information on the threads in the pool)

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			lock (queueLock)
			{
				sb.Append("PriorityThreadPool[thread (idle / total): ");
				sb.Append(IdleThreadCount);
				sb.Append("/");
				sb.Append(ThreadCount);
				sb.Append("]");
				sb.AppendLine();
				foreach (var item in threads)
				{
					if (!item.IsRunning)
						continue;
					sb.Append("\t");
					sb.Append(item.Thread.Name);
					sb.AppendLine();

					StackTrace stack;
					if (Thread.CurrentThread == item.Thread)
					{
						stack = new StackTrace(true);
					}
					else
					{
#pragma warning disable 618
						item.Thread.Suspend();
						try
						{
							stack = new StackTrace(item.Thread, true);
						}
						finally
						{
							item.Thread.Resume();
						}
#pragma warning restore 618
					}
					sb.Append(stack);
				}
			}
			return sb.ToString();
		}

		#endregion
	}

#if SILVERLIGHT
	static class PoolEx
	{
		public static void RemoveAll<T>(this List<T> list, Predicate<T> toRemove)
		{
			for (int i = list.Count - 1; i >= 0; i--)
				if (toRemove(list[i]))
					list.RemoveAt(i);
		}
		[Conditional("USELESS")]
		public static void Suspend(this Thread t) { }
		[Conditional("USELESS")]
		public static void Resume(this Thread t) { }
	}
#endif
}
