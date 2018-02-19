using System;
using System.Diagnostics;
using System.Windows.Input;

namespace MVVM
{
    /// <summary>A command for MVVM View Models.</summary>
    public class ViewModelCommand : ICommand
    {
        //--------------------------------------------------------------------------------------------------------------------------
        #region Fields

        readonly Action<object> executeCommand;
        readonly Predicate<object> canExecuteCommand;        

        #endregion // Fields
        //--------------------------------------------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>Constructs a new command that can always execute.</summary>
        /// <param name="execute">A delegate that executes the command.</param>
        public ViewModelCommand(Action<object> executeCommand)
            : this(executeCommand, null)
        {
        }

        /// <summary>Constructs a new command.</summary>
        /// <param name="executeCommand">A delegate that executes the command.</param>
        /// <param name="canExecuteCommand">A executeCommand that determines  if the command can execute.</param>
        public ViewModelCommand(Action<object> executeCommand, Predicate<object> canExecuteCommand)
        {
            if (executeCommand == null)
            {
                throw new ArgumentNullException("executeCommand");
            }

            this.executeCommand = executeCommand;

            this.canExecuteCommand = canExecuteCommand;           
        }

        #endregion // Constructors
        //--------------------------------------------------------------------------------------------------------------------------
        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecuteCommand == null ? true : canExecuteCommand(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            executeCommand(parameter);
        }

        #endregion // ICommand Members
        //--------------------------------------------------------------------------------------------------------------------------
    }
}