using System;
using System.Threading;

namespace System
{
	public class Lazy<T>
	{
		T mValue;
		readonly LazyThreadSafetyMode eMode;
		readonly Func<T> creator;
		readonly object locker;

		public Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
		{
			eMode = mode;
			switch (mode)
			{
				case LazyThreadSafetyMode.PublicationOnly:
				case LazyThreadSafetyMode.ExecutionAndPublication:
					locker = new object();
					break;
			}
			this.creator = valueFactory != null ? valueFactory : () => Activator.CreateInstance<T>();
		}

		public Lazy()
			: this((Func<T>)null, LazyThreadSafetyMode.None)
		{
		}
		public Lazy(Func<T> create)
			: this((Func<T>)create, LazyThreadSafetyMode.None)
		{
		}
		public Lazy(Func<T> valueFactory, bool isThreadSafe)
			: this(valueFactory, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
		{
		}
		public Lazy(LazyThreadSafetyMode mode)
			: this((Func<T>)null, mode)
		{
		}
		public Lazy(bool isThreadSafe)
			: this(null, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
		{
		}

		public bool IsValueCreated { get; private set; }
		public T Value 
		{
			get
			{
				if (!IsValueCreated)
				{
					switch (eMode)
					{
						default:
						case LazyThreadSafetyMode.None:
							mValue = creator();
							IsValueCreated = true;
							break;
						case LazyThreadSafetyMode.PublicationOnly:
							var tValue = creator();
							if(!IsValueCreated)
							lock (locker)
								if (!IsValueCreated)
								{
									mValue = tValue;
									IsValueCreated = true;
								}
							break;
						case LazyThreadSafetyMode.ExecutionAndPublication:
							lock (locker)
								if (!IsValueCreated)
								{
									mValue = creator();
									IsValueCreated = true;
								}
							break;
					}
				}
				return mValue;
			}
		}
	}
}
