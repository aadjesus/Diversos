using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;

namespace Galador.Utils
{
	#region class ThreadTimer

	/// <summary>
	/// A timer to work on a <see cref="TimedWorkerThread"/>. <seealso cref="TimedWorkerThread.DefaultTimer"/>
	/// </summary>
	/// <remarks>Unlike the <c>System.Threading.Timer</c> this class will not spawn plenty of (multi threaded) Tick event
	/// if a Tick event takes a long time to complete. Instead it will wait <c>Period</c> after the completion of the previous
	/// tick to perform a new tick event in the same thread.</remarks>
	public class ThreadTimer
	{
		readonly object mLock = new object();
		TimedWorkerThread mThread;
		TimeSpan mPeriod = TimeSpan.Zero;
		DateTime mNextTimerTick;
		bool mEnabled = true;

		public ThreadTimer(TimedWorkerThread wt = null)
		{
			TargetThread = wt;
		}

		public TimedWorkerThread TargetThread 
		{
			get { return mThread; }
			set 
			{
				lock (mLock)
				{
					if (value == mThread)
						return;
					mThread = value;
					NextTick = NextTick;
				}
			}
		}

		/// <summary>
		/// Define an interval of time at which the <c>Tick</c> event should be triggered.
		/// This doesn't fire the task exactly regularly, instead the interval is the time
		/// between the completion of the previous tick event and the start of the next tick event. 
		/// I.e. if the tick event takes a lot of time to complete, tick events won't accumulate.
		/// The starting time can be set (after the period has been set) by changing <c>NextTick</c>.
		/// </summary>
		public TimeSpan Period
		{
			get { return mPeriod; }
			set
			{
				lock (mLock)
				{
					if (value <= TimeSpan.Zero)
						value = TimeSpan.Zero;
					if (value == mPeriod)
						return;
					mPeriod = value;
					NextTick = DateTime.Now + Period;
				}
			}
		}

		public bool IsEnabled 
		{
			get { return mEnabled; }
			set 
			{
				lock (mLock)
				{
					if (value == mEnabled)
						return;
					mEnabled = value;
					NextTick = NextTick;
				}
			}
		}

		/// <summary>
		/// Set the next tick. It is reset when the Period change and after every <c>Tick</c> event.
		/// </summary>
		public DateTime NextTick
		{
			get { return mNextTimerTick; }
			set
			{
				lock (mLock)
				{
					mNextTimerTick = value;
					if (TargetThread == null)
						return;
					lock (TargetThread.SyncRoot)
					{
						if (!TargetThread.IsDisposed
							&& !TargetThread.IsShuttingDown
							&& Period > TimeSpan.Zero
							&& IsEnabled)
							TargetThread.QueueScheduledItem(mNextTimerTick, DoTimer);
					}
				}
			}
		}

		public event EventHandler Tick;

		void DoTimer()
		{
			lock (mLock)
			{
				if (TargetThread == null
					|| TargetThread.Thread != Thread.CurrentThread)
					return;
				if (DateTime.Now < mNextTimerTick)
					return;
			}
			try
			{
				var e = Tick;
				if (e != null)
					e(this, EventArgs.Empty);
			}
			finally { NextTick = DateTime.Now + Period; }
		}
	}

	#endregion

	/// <summary>
	/// This class encapsulate a single worker thread which could both 
	/// perform queued task on the worker's thread, fire event at regular interval 
	/// and perform scheduled task, all on the worker's thread too!
	/// </summary>
	/// <remarks>No finalizer for this disposable object. Being the root object of a thread 
	/// it will never be finalized and needs to be disposed explicitly!</remarks>
	public class TimedWorkerThread : IDisposable
	{
		#region class WorkItem, ScheduleItem

		private class WorkItem
		{
			public WorkItem(Action method)
			{
				mWaitCallback = method;
			}

			readonly Action mWaitCallback;

			public void Process()
			{
				mWaitCallback();
			}
		}

		private class ScheduleItem : IComparable, IComparable<ScheduleItem>
		{
			public ScheduleItem(DateTime time, WorkItem item)
			{
				Time = time;
				Item = item;
			}
			public DateTime Time { get; private set; }
			public WorkItem Item { get; private set; }

			public int CompareTo(object obj)
			{
				var si = obj as ScheduleItem;
				if (si == null)
					return 0;
				return CompareTo(si);
			}
			public int CompareTo(ScheduleItem other)
			{
				return Time.CompareTo(other.Time);
			}
		}

		#endregion

		readonly object mLock = new object();
		readonly Thread mThread;
		bool mDisposed, mShuttingDown, mIsStarted;

		// work & timer queue
		readonly ManualResetEvent mWait;
		readonly Queue<WorkItem> mPendingItems;
		readonly List<ScheduleItem> mScheduledItems;

		public TimedWorkerThread(string name = null)
		{
			mWait = new ManualResetEvent(false);
			mPendingItems = new Queue<WorkItem>();
			mScheduledItems = new List<ScheduleItem>();

			mThread = new Thread(ThreadRun) { IsBackground = true, Name = name };
			// do not start the thread right away, to enable further customization
		}

		#region ThreadRun(), CheckState(), Idle, IdleChanged, IsStarted

		/// <summary>
		/// Start the thread or returns whether it has been started or not.
		/// It is not started in the constructor so that the thread can be customized further.
		/// However it will automatically be started as soon as any an <c>Action</c> is queued or scheduled.
		/// </summary>
		public bool IsStarted
		{
			get { return mIsStarted; }
			set
			{
				lock (mLock)
				{
					if (mIsStarted || !value || mDisposed)
						return;
					mIsStarted = true;
					mThread.Start();
				}
			}
		}

		void ThreadRun()
		{
			try
			{
				while (!mDisposed)
				{
					int millis = -1;
					lock (mLock)
					{
						if (mDisposed)
							return;
						if (mScheduledItems.Count > 0)
							millis = MillisTo(mScheduledItems[0].Time);
					}

					mThread.IsBackground = true;
					try { mWait.WaitOne(millis); }
					catch (ObjectDisposedException) { return; }
					mThread.IsBackground = false;

					WorkItem wi = null;
					lock (mLock)
					{
						if (mDisposed)
							return;

						if (mPendingItems.Count > 0)
						{
							wi = mPendingItems.Dequeue();
						}
						else if (mScheduledItems.Count > 0 && mScheduledItems[0].Time <= DateTime.Now)
						{
							wi = mScheduledItems[0].Item;
							mScheduledItems.RemoveAt(0);
						}

						if (mShuttingDown && wi == null)
							return;

						if (wi == null)
							mWait.Reset();
					}

					Idle = wi == null; // outside the lock because of the IdleChanged event!
					if (wi != null) // outside of the lock for obvious reason (it's custom code!)
						try { wi.Process(); }
						catch (Exception ex)
						{
							var eh = ExceptionHandler;
							if (eh == null || !eh(ex))
								throw;
						}
				}
			}
			finally
			{
				Dispose();
			}
		}
		static int MillisTo(DateTime time)
		{
			var tsTo = time - DateTime.Now;
			// Ceiling() because we should wait until after time, casting to int will lost some less than 1ms part
			var dTo = Math.Ceiling(tsTo.TotalMilliseconds);
			if (dTo <= 0)
				return 0;
			else if (dTo < int.MaxValue)
				return (int)dTo;
			return -1;
		}

		/// <summary>
		/// Whether or not this thread is running some queued items or tick event.
		/// </summary>
		public bool Idle
		{
			get { return mIdle && !mDisposed; }
			private set
			{
				lock (mLock)
				{
					if (value == mIdle)
						return;
					mIdle = value;
				}
				var ev = mIdleChanged;
				if (ev != null)
					ev(this, EventArgs.Empty);
			}
		}
		bool mIdle = true;

		public event EventHandler IdleChanged
		{
			add { lock (mLock) mIdleChanged += value; }
			remove { lock (mLock) mIdleChanged -= value; }
		}
		EventHandler mIdleChanged;

		void CheckState()
		{
			lock (mLock)
			{
				if (mDisposed)
					throw new ObjectDisposedException(GetType().Name);
				if (mShuttingDown)
					throw new InvalidOperationException("Shutting down");
			}
		}

		#endregion

		#region QueueItem(), QueueItemWait()

		/// <summary>
		/// Queue an item to be execute on the worker's <c>Thread</c> ASAP.
		/// </summary>
		public void QueueItem(Action action)
		{
			if (action == null)
				return;

			lock (mLock)
			{
				CheckState();
				mPendingItems.Enqueue(new WorkItem(action));
				mWait.Set();
				IsStarted = true;
			}
		}

		/// <summary>
		/// Queue an item to be execute on the worker's <c>Thread</c> ASAP.
		/// </summary>
		public void QueueItem<T>(Action<T> action, T arg)
		{
			if (action == null)
				return;
			QueueItem(delegate { action(arg); });
		}

		/// <summary>
		/// Queue an item to be execute on the worker's <c>Thread</c> ASAP and wait for its completion.
		/// </summary>
		public void QueueItemWait(Action action, Action<WaitHandle> waitAction = null)
		{
			CheckState();
			if (action == null)
				action = () => { };

			if (mThread == Thread.CurrentThread)
			{
				action();
				return;
			}

			using (var w = new ManualResetEvent(false))
			{
				if (waitAction == null)
					waitAction = aW => aW.WaitOne();
				var a = new Action(delegate
				{
					try { action(); }
					finally { w.Set(); }
				});
				QueueItem(a);
				waitAction(w);
			}
		}

		/// <summary>
		/// Queue an item to be execute on the worker's <c>Thread</c> ASAP and wait for its completion.
		/// </summary>
		public void QueueItemWait<T>(Action<T> action, T arg, Action<WaitHandle> waitAction = null)
		{
			if (action == null)
				action = (t) => { };
			QueueItemWait(delegate { action(arg); }, waitAction);
		}

		#endregion

		#region QueueScheduledItem()

		public void QueueScheduledItem(TimeSpan timeToWait, Action action)
		{
			QueueScheduledItem(DateTime.Now + timeToWait, action);
		}
		public void QueueScheduledItem(DateTime timeScheduled, Action action)
		{
			if (action == null)
				return;
			lock (mLock)
			{
				CheckState();
				mScheduledItems.Add(new ScheduleItem(timeScheduled, new WorkItem(action)));
				mScheduledItems.Sort();
				mWait.Set();
				IsStarted = true;
			}
		}

		public void QueueScheduledItem<T>(TimeSpan timeToWait, Action<T> action, T state)
		{
			if (action == null)
				return;
			QueueScheduledItem(DateTime.Now + timeToWait, delegate { action(state); });
		} 
		public void QueueScheduledItem<T>(DateTime timeScheduled, Action<T> action, T state)
		{
			if (action == null)
				return;
			QueueScheduledItem(timeScheduled, delegate { action(state); });
		}

		#endregion

		#region StackTrace

		/// <summary>
		/// Get this thread's current StackTrace
		/// </summary>
		public StackTrace StackTrace
		{
			get
			{
				StackTrace stack;
				if (Thread.CurrentThread == mThread)
				{
					stack = new StackTrace(true);
				}
				else
				{
#if SILVERLIGHT
					stack = new StackTrace(mThread, true);
#else
#pragma warning disable 618
					mThread.Suspend();
					try
					{
						stack = new StackTrace(mThread, true);
					}
					finally
					{
						mThread.Resume();
					}
#pragma warning restore 618
#endif
				}
				return stack;
			}
		}

		#endregion

		public bool IsDisposed { get { return mDisposed; } }
		public bool IsShuttingDown { get { return mShuttingDown || mDisposed; } }

		/// <summary>
		/// Object used for critical block by this thread internally.
		/// </summary>
		public object SyncRoot { get { return mLock; } }
		/// <summary>
		/// Thread use to run all actions. Can be use to <c>Join()</c> when waiting for the thread
		/// to <c>Shutdown()</c> / <c>Dispose()</c>.
		/// </summary>
		/// <remarks>The Thread is only started when some action are queued. Hence it can be further
		/// customized by the user before being used.</remarks>
		public Thread Thread { get { return mThread; } }
		/// <summary>
		/// Handle unhandled exception at the top of the thread and returns whether or not the exception
		/// has been handled. 
		/// <remarks>Unhandled exception will be re-thrown and destroy this thread and process.</remarks>
		/// </summary>
		public Predicate<Exception> ExceptionHandler { get; set; }

		#region Shutdown(), Dispose()

		/// <summary>
		/// This will finish execute all pending items, then Dispose()
		/// </summary>
		public void Shutdown()
		{
			lock (mLock)
			{
				if (mDisposed || mShuttingDown)
					return;
				if (!IsStarted)
				{
					Dispose();
				}
				else
				{
					mShuttingDown = true;
					mWait.Set();
				}
			}
		}

		/// <summary>
		/// This will terminate the worker's thread right after the current task
		/// </summary>
		public void Dispose()
		{
			// lock for memory barrier as well!
			lock (mLock)
			{
				if (mDisposed)
					return;
				mDisposed = true;
				mWait.Set();
			}
			mWait.Close();
			mIdleChanged = null;
			mPendingItems.Clear();
			mScheduledItems.Clear();
		}

		#endregion
	}
}
