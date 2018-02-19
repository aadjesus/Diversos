using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;

namespace Galador.Applications
{
	#region Foreach

	public abstract class Foreach : IDisposable
	{
		public Foreach(IEnumerable collection = null)
		{
			cHandler = HandleCollectionChanged;
			this.Collection = collection;
		}
		NotifyCollectionChangedEventHandler cHandler;
		internal List<object> copy;

		public IEnumerable Collection
		{
			get { return collection; }
			set
			{
				if (value == collection)
					return;

				if (copy != null)
					foreach (var item in copy)
						Unregister(item);
				if (notifyCollection != null)
					notifyCollection.CollectionChanged -= cHandler;

				collection = value;
				notifyCollection = value as INotifyCollectionChanged;
				copy = value != null ? new List<object>(from o in collection.Cast<object>() where o != null select o) : null;

				if (notifyCollection != null)
					notifyCollection.CollectionChanged += cHandler;
				if (copy != null)
					foreach (var item in copy)
						Register(item);
			}
		}
		IEnumerable collection;
		INotifyCollectionChanged notifyCollection;

		void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			// warning an exception might leave this object in an invalid state
			// commented code doesn't, but makes it difficult to fire collection changed event
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Reset:
					{
						for (int i = copy.Count - 1; i >= 0; i--)
						{
							var item = copy[i];
							copy.RemoveAt(i);
							Unregister(item, i);
						}
						//foreach (var item in copy)
						//    Unregister(item);
						//copy.Clear();
					}
					break;
				default:
					if (e.OldItems != null)
					{
						foreach (var oldItem in e.OldItems)
							if (oldItem != null)
							{
								int index = copy.IndexOf(oldItem);
								if (index > -1)
								{
									copy.RemoveAt(index);
									Unregister(oldItem, index);
								}
							}
						//foreach (var oldItem in e.OldItems)
						//    if (oldItem != null)
						//        copy.Remove(oldItem);
						//foreach (var oldItem in e.OldItems)
						//    if (oldItem != null)
						//        Unregister(oldItem);
					}
					if (e.NewItems != null)
					{
						foreach (var newItem in e.NewItems)
							if (newItem != null)
							{
								copy.Add(newItem);
								Register(newItem);
							}
						//foreach (var newItem in e.NewItems)
						//    if (newItem != null)
						//        copy.Add(newItem);
						//foreach (var newItem in e.NewItems)
						//    if (newItem != null)
						//        Register(newItem);
					}
					break;
			}
		}

		protected abstract void Register(object item);
		protected abstract void Unregister(object item);
		protected internal virtual void Unregister(object item, int index) { Unregister(item); }

		public IEnumerable CurrentItems { get { return copy != null ? copy.ToArray() : new object[0]; } }

		public virtual void Dispose()
		{
			Collection = null;
		}
	}

	#endregion

	#region ForeachEx

#if !WINDOWS_PHONE
	public static class ForeachEx
	{
		public static void SetCollectionSource<TSource, TItem, TCollection>(this Foreach<TItem, TCollection> fe, TSource source, Expression<Func<TSource, TCollection>> getcollection)
			where TCollection : class, INotifyCollectionChanged, IEnumerable<TItem>
			where TSource : INotifyPropertyChanged
		{
			if (source == null)
				return;
			if (getcollection == null)
				throw new ArgumentNullException("getcollection");

			var func = getcollection.Compile();
			var fname = PropertyUtils.GetName(getcollection);

			source.PropertyChanged += (o, e) =>
			{
				if (e.PropertyName == fname)
					fe.Collection = func(source);
			};
			fe.Collection = func(source);
		}

		public static void SetCollectionSource<TSource>(this Foreach fe, TSource source, Expression<Func<TSource, IEnumerable>> getcollection)
		{
			if (source == null)
				return;
			if (getcollection == null)
				throw new ArgumentNullException("getcollection");

			var func = getcollection.Compile();
			var fname = PropertyUtils.GetName(getcollection);

			if (source is INotifyPropertyChanged)
			{
				var snpc = (INotifyPropertyChanged)source;
				snpc.PropertyChanged += (o, e) =>
				{
					if (e.PropertyName == fname)
						fe.Collection = func(source);
				};
			}
			fe.Collection = func(source);
		}
	}
#endif

	#endregion

	#region ForeachCommand

	public class ForeachCommand<TItem> : ForeachCommand<TItem, ObservableCollection<TItem>>
	{
		public ForeachCommand(Func<TItem, ICommand> getCmd, ObservableCollection<TItem> collection = null)
			: base(getCmd, collection)
		{
		}
	}

	public class ForeachCommand<TItem, TCollection> : Foreach<TItem, TCollection>, ICommand
		where TCollection : class, INotifyCollectionChanged, IEnumerable<TItem>
	{
		public ForeachCommand(Func<TItem, ICommand> getCmd, TCollection collection = null)
			: base(collection)
		{
			if (getCmd == null)
				throw new ArgumentNullException("getCmd");
			this.GetCommand = getCmd;
		}

		public Func<TItem, ICommand> GetCommand { get; private set; }

		protected override void Register(TItem item)
		{
			if (item == null)
				return;
			var cmd = GetCommand(item);
			if (cmd == null)
				return;
			cmd.CanExecuteChanged += ItemCommandCanExecuteChanged;
			ItemCommandCanExecuteChanged(item, EventArgs.Empty);
		}
		protected override void Unregister(TItem item)
		{
			if (item == null)
				return;
			var cmd = GetCommand(item);
			if (cmd == null)
				return;
			cmd.CanExecuteChanged -= ItemCommandCanExecuteChanged;
			ItemCommandCanExecuteChanged(item, EventArgs.Empty);
		}

		void ItemCommandCanExecuteChanged(object sender, EventArgs e)
		{
			if (CanExecuteChanged != null)
				CanExecuteChanged(this, e);
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			var commands =
				from item in Collection
				where item != null
				let cmd = GetCommand(item)
				where cmd != null
				select cmd;

			foreach (var cmd in commands.ToList())
				if (!cmd.CanExecute(parameter))
					return false;
			return true;
		}

		public void Execute(object parameter)
		{
			var commands =
				from item in Collection
				where item != null
				let cmd = GetCommand(item)
				where cmd != null
				select cmd;

			foreach (var cmd in commands.ToList())
				cmd.Execute(parameter);
		}
	}

	#endregion

	#region ForeachProperty

	public class ForeachProperty<TItem> : ForeachProperty<TItem, ObservableCollection<TItem>>
		where TItem : INotifyPropertyChanged
	{
		public ForeachProperty(ObservableCollection<TItem> collection = null)
			: base(collection)
		{
		}
	}

	public class ForeachProperty<TItem, TCollection> : Foreach<TItem, TCollection>, INotifyPropertyChanged
		where TItem : INotifyPropertyChanged
		where TCollection : class, INotifyCollectionChanged, IEnumerable<TItem>
	{
		public ForeachProperty(TCollection collection = null)
			: base(collection)
		{
		}

		protected override void Register(TItem item)
		{
			if (item == null)
				return;
			item.PropertyChanged += OnPropertyChanged;
			OnPropertyChanged(item, new PropertyChangedEventArgs(""));
		}
		protected override void Unregister(TItem item)
		{
			if (item == null)
				return;
			item.PropertyChanged -= OnPropertyChanged;
			OnPropertyChanged(item, new PropertyChangedEventArgs(""));
		}

		public void OnPropertyChanged(object sender, PropertyChangedEventArgs arg)
		{
			if (PropertyChanged != null)
				PropertyChanged(sender, arg);
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}

	#endregion

	#region Foreach<TItem, TCollection>

	public abstract class Foreach<TItem> : Foreach<TItem, ObservableCollection<TItem>>
	{
		public Foreach(ObservableCollection<TItem> collection = null)
			: base(collection)
		{
		}
	}

	public abstract class Foreach<TItem, TCollection> : IDisposable
		where TCollection : class, INotifyCollectionChanged, IEnumerable<TItem>
	{
		NotifyCollectionChangedEventHandler cHandler;
		List<TItem> copy;

		public Foreach(TCollection collection = null)
		{
			cHandler = HandleCollectionChanged;
			Collection = collection;
		}

		public TCollection Collection
		{
			get { return collection; }
			set
			{
				if (value == collection)
					return;
				if (collection != null)
				{
					collection.CollectionChanged -= cHandler;
					foreach (var item in collection)
						Unregister(item);
				}
				collection = value;
				copy = null;
				if (collection != null)
				{
					copy = new List<TItem>(collection);
					collection.CollectionChanged += cHandler;
					foreach (TItem item in collection)
						Register(item);
				}
			}
		}
		TCollection collection;

		void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Reset:
					{
						foreach (var item in copy)
							Unregister(item);
						copy.Clear();
					}
					break;
				default:
					if (e.OldItems != null)
					{
						foreach (TItem oldItem in e.OldItems)
							if (oldItem != null)
								copy.Remove(oldItem);
						foreach (TItem oldItem in e.OldItems)
							if (oldItem != null)
								Unregister(oldItem);
					}
					if (e.NewItems != null)
					{
						foreach (TItem newItem in e.NewItems)
							if (newItem != null)
								copy.Add(newItem);
						foreach (TItem newItem in e.NewItems)
							if (newItem != null)
								Register(newItem);
					}
					break;
			}
		}

		protected abstract void Register(TItem item);
		protected abstract void Unregister(TItem item);

		public virtual void Dispose()
		{
			Collection = null;
		}
	}

	#endregion

	#region ForeachCollection : Foreach, IList, INotifyCollectionChanged

	public class ForeachCollection : Foreach, IList, INotifyCollectionChanged
	{
		List<object> objects = new List<object>();

		#region abstract override

		protected override void Register(object item)
		{
			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, Count - 1));
		}

		protected internal override void Unregister(object item, int index)
		{
			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
			Unregister(item);
		}

		protected override void Unregister(object item)
		{
		}

		#endregion

		#region INotifyCollectionChanged Members

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		#endregion

		#region IList Members

		int IList.Add(object item)
		{
			throw new NotSupportedException();
		}

		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(object value)
		{
			return copy.Contains(value);
		}

		public int IndexOf(object value)
		{
			return copy.IndexOf(value);
		}

		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		bool IList.IsFixedSize
		{
			get { return false; }
		}

		bool IList.IsReadOnly
		{
			get { return true; }
		}

		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		object IList.this[int index]
		{
			get
			{
				return copy[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}
		public object this[int index]
		{
			get
			{
				return copy[index];
			}
		}

		#endregion

		#region ICollection Members

		void ICollection.CopyTo(Array array, int index)
		{
			foreach (var item in copy)
				array.SetValue(item, index++);
		}

		public int Count
		{
			get { return copy.Count; }
		}

		bool ICollection.IsSynchronized
		{
			get { return false; }
		}

		object ICollection.SyncRoot
		{
			get { return copy; }
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return copy.GetEnumerator();
		}

		#endregion
	}

	#endregion
}
