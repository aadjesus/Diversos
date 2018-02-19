using System;
using System.Windows.Input;

namespace MVVM
{
    /// <summary>A view model that can request that the associated view be closed.</summary>
    public abstract class CloseableViewModel : ViewModel
    {
        //--------------------------------------------------------------------------------------------------------------------------
        #region Fields

        ViewModelCommand closeCommand;

        #endregion // Fields
        //--------------------------------------------------------------------------------------------------------------------------
        #region Constructor

        protected CloseableViewModel()
        {
            closeCommand = new ViewModelCommand(param => OnRequestClose());
        }

        #endregion // Constructor
        //--------------------------------------------------------------------------------------------------------------------------
        #region CloseCommand

        /// <summary>Gets a command that closed the view and this view-model.</summary>
        public ICommand CloseCommand { get {  return closeCommand; } }

        #endregion // CloseCommand
        //--------------------------------------------------------------------------------------------------------------------------
        #region Events

        /// <summary>Raised when the view and view-model should be removed from the UX.</summary>
        public event EventHandler RequestClose;

        /// <summary>Called to raise the RequestCloseEvent</summary>
        void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;

            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion
        //--------------------------------------------------------------------------------------------------------------------------
    }
}