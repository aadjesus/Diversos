using System;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
#if !WINDOWS_PHONE

#endif

namespace Galador.Applications
{
	public interface ICommand<T> : ICommand
	{
		bool CanExecute(T parameter);
		void Execute(T parameter);
	}

	#region class DelegateCommand<TParameter>

	// TODO work in progress, should be much simpler, but is not currently working...
	/// <summary>
	/// A command which will initialize by an Action and an (optional)
	/// <see cref="PropertyPath"/> to a bool property or a method to check is executable.
	/// </summary>
	public partial class DelegateCommand<TParameter> : ICommand<TParameter>, IDisposable
	{
		readonly Action<TParameter> execute;

		readonly PropertyPath canExecutePath;
#if !WINDOWS_PHONE
		readonly Func<bool> canExecuteFct;
#endif

		#region ctors()

		public DelegateCommand(Action<TParameter> execute, Expression<Func<bool>> canExecute = null)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");
			this.execute = execute;

			if (canExecute != null)
			{
#if !WINDOWS_PHONE
				canExecuteFct = canExecute.Compile();
#endif
				canExecutePath = PropertyPath.Create(canExecute);
				canExecutePath.PropertyChanged += delegate { RaiseCanExecuteChanged(); };
			}
		}

		readonly Func<TParameter, bool> canExecuteFctT;
		readonly PropertyPath canExecuteChangePath;
		readonly EventRegistration canExecuteChangeEv;

		public DelegateCommand(Action<TParameter> execute, Func<TParameter, bool> canExecute, Expression<Func<Delegate>> canExecuteChangedEvent = null)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");
			this.execute = execute;

			if (canExecute != null)
				canExecuteFctT = canExecute;

			if (canExecuteChangedEvent != null)
			{
				this.canExecuteChangeEv = EventRegistration.Create(canExecuteChangedEvent);
				this.canExecuteChangeEv.Event += delegate { RaiseCanExecuteChanged(); };

				object root;
				var epath = canExecuteChangedEvent.GetPath(out root);
				if (epath.Length < 2)
				{
					this.canExecuteChangeEv.Target = root;
				}
				else
				{
					canExecuteChangePath = new PropertyPath(epath.Take(epath.Length - 1).ToArray()) { Source = root };
					canExecuteChangePath.PropertyChanged += delegate { this.canExecuteChangeEv.Target = canExecuteChangePath.Value; };
					this.canExecuteChangeEv.Target = canExecuteChangePath.Value;
				}
			}
		}

		#endregion

		#region ICommand<T>, RaiseCanExecuteChanged()

		bool ICommand.CanExecute(object parameter)
		{
			var p = parameter is TParameter ? (TParameter)parameter : default(TParameter);
			return CanExecute(p);
		}
		void ICommand.Execute(object parameter)
		{
			var p = parameter is TParameter ? (TParameter)parameter : default(TParameter);
			Execute(p);
		}

		public bool CanExecute(TParameter parameter)
		{
#if WINDOWS_PHONE
			if (canExecutePath != null)
			{
				var can = canExecutePath.Value;
				if (can is bool && !(bool)can)
					return false;
				return true;
			}
#else
			if (canExecuteFct != null)
				return canExecuteFct();
#endif
			if (canExecuteFctT != null)
				return canExecuteFctT(parameter);
			return true;
		}

		public void Execute(TParameter parameter)
		{
			if (CanExecute(parameter))
				execute(parameter);
		}

		public event EventHandler CanExecuteChanged;

		public void RaiseCanExecuteChanged()
		{
			var e = CanExecuteChanged;
			if (e != null)
				CanExecuteChanged(this, EventArgs.Empty);
		}

		#endregion

		#region IsListeningToCommandManager
#if !SILVERLIGHT

		/// <summary>
		/// If your command doesn't have an event for CanExecuteChanged, it can be automatically refreshed
		/// with the CommandManager by setting this property to true.
		/// </summary>
		public bool IsListeningToCommandManager
		{
			get { return cmdManagerHandler != null; }
			set
			{
				if (value == IsListeningToCommandManager)
					return;
				if (value)
				{
					cmdManagerHandler = delegate { RaiseCanExecuteChanged(); };
					CommandManager.RequerySuggested += cmdManagerHandler;
				}
				else
				{
					CommandManager.RequerySuggested -= cmdManagerHandler;
					cmdManagerHandler = null;
				}
			}

		}
		EventHandler cmdManagerHandler;

#endif
		#endregion

		#region IDisposable Members

		public void Dispose()
		{
#if !WINDOWS_PHONE
			if (canExecutePath != null)
				canExecutePath.Dispose();
#endif
			if (canExecuteChangeEv != null)
				canExecuteChangeEv.Dispose();
			if (canExecuteChangePath != null)
				canExecuteChangePath.Dispose();
		}

		#endregion
	}

	#endregion

	#region class DelegateCommand

	/// <summary>
	/// A command which will initialize by an Action and an (optional)
	/// <see cref="PropertyPath"/> to a bool property.
	/// </summary>
	public partial class DelegateCommand : ICommand, IDisposable
	{
		readonly Action execute;

#if !WINDOWS_PHONE
		readonly Func<bool> canExecuteFct;
#endif
		readonly PropertyPath canExecutePath;

		#region ctor()

		public DelegateCommand(Action execute, Expression<Func<bool>> canExecute = null)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");
			this.execute = execute;

			if (canExecute != null)
			{
#if !WINDOWS_PHONE
				canExecuteFct = canExecute.Compile();
#endif
				canExecutePath = PropertyPath.Create(canExecute);
				canExecutePath.PropertyChanged += delegate { RaiseCanExecuteChanged(); };
			}
		}

		#endregion

		#region ICommand<T>, RaiseCanExecuteChanged()

		bool ICommand.CanExecute(object parameter) { return CanExecute(); }
		void ICommand.Execute(object parameter) { Execute(); }

		public bool CanExecute()
		{
#if WINDOWS_PHONE
			if (canExecutePath != null)
			{
				var can = canExecutePath.Value;
				if (can is bool && !(bool)can)
					return false;
				return true;
			}
#else
			if (canExecuteFct != null)
				return canExecuteFct();
#endif
			return true;
		}

		public void Execute()
		{
			if (CanExecute())
				execute();
		}

		public event EventHandler CanExecuteChanged;

		public void RaiseCanExecuteChanged()
		{
			var e = CanExecuteChanged;
			if (e != null)
				CanExecuteChanged(this, EventArgs.Empty);
		}

		#endregion

		#region IsListeningToCommandManager
#if !SILVERLIGHT

		/// <summary>
		/// If your command doesn't have an event for CanExecuteChanged, it can be automatically refreshed
		/// with the CommandManager by setting this property to true.
		/// </summary>
		public bool IsListeningToCommandManager
		{
			get { return cmdManagerHandler != null; }
			set
			{
				if (value == IsListeningToCommandManager)
					return;
				if (value)
				{
					cmdManagerHandler = delegate { RaiseCanExecuteChanged(); };
					CommandManager.RequerySuggested += cmdManagerHandler;
				}
				else
				{
					CommandManager.RequerySuggested -= cmdManagerHandler;
					cmdManagerHandler = null;
				}
			}

		}
		EventHandler cmdManagerHandler;

#endif
		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (canExecutePath != null)
				canExecutePath.Dispose();
		}

		#endregion
	}

	#endregion
}
