using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MVVM
{
    /// <summary>Base class for all View-Models</summary>
    public abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {
        //-------------------------------------------------------------------------------------------------------------------------
        #region Construction and Disposal

        protected ViewModel()
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) {
                // Clean up managed resources 
            }

            // Clean up unmanaged resources here 
        }

        #endregion 
        //-------------------------------------------------------------------------------------------------------------------------
        #region Properties

        /// <summary>Gets the user displayable name for the view.</summary>
        public virtual string DisplayName { get; protected set; }

        #endregion 
        //-------------------------------------------------------------------------------------------------------------------------
        #region INotifyPropertyChanged Members

        /// <summary>Raised after a property has changed.</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Raises tthe PropertyChanged event.</summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion // INotifyPropertyChanged Members
        //-------------------------------------------------------------------------------------------------------------------------
        #region Debugging Support

        /// <summary>Determines if a property name is valid for this view-model</summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Check to see if the property name matches public property on this object
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string message = string.Format( "Invalid Property '{0}' used on type '{1}'.",
                    propertyName,
                    this.GetType().ToString() );

                throw new InvalidViewModelPropertyException( message );
            }
        }

        #endregion 
        //-------------------------------------------------------------------------------------------------------------------------
    }
}