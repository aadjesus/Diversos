//-----------------------------------------------------------------------
// <copyright file="BindableObject.cs" company="Impilo Software">
//     Copyright © Impilo Software. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace OutlookCalendar.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;

    /// <summary>
    /// Implements the INotifyPropertyChanged interface and 
    /// exposes a RaisePropertyChanged method for derived 
    /// classes to raise the PropertyChange event.  The event 
    /// arguments created by this class are cached to prevent 
    /// managed heap fragmentation.
    /// </summary>
    [Serializable]
    public abstract class BindableObject : INotifyPropertyChanged
    {
        #region Data

        private static readonly Dictionary<string, PropertyChangedEventArgs> eventArgCache;
        private const string ERROR_MSG = "{0} is not a public property of {1}";
        private static readonly object syncLock = new object();

        #endregion // Data

        #region Constructors

        static BindableObject()
        {
            eventArgCache = new Dictionary<string, PropertyChangedEventArgs>();
        }

        protected BindableObject()
        {
        }

        #endregion // Constructors

        #region Public Members

        /// <summary>
        /// Raised when a public property of this object is set.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Returns an instance of PropertyChangedEventArgs for 
        /// the specified property name.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to create event args for.
        /// </param>		
        public static PropertyChangedEventArgs GetPropertyChangedEventArgs(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("propertyName cannot be null or empty.");
            }

            PropertyChangedEventArgs args;
            lock (BindableObject.syncLock)
            {
                if (!eventArgCache.TryGetValue(propertyName, out args))
                {
                    eventArgCache.Add(propertyName, args = new PropertyChangedEventArgs(propertyName));
                }
            }

            return args;
        }

        #endregion // Public Members

        #region Protected Members

        /// <summary>
        /// Derived classes can override this method to
        /// execute logic after a property is set. The 
        /// base implementation does nothing.
        /// </summary>
        /// <param name="propertyName">
        /// The property which was changed.
        /// </param>
        protected virtual void AfterPropertyChanged(string propertyName)
        {
        }

        /// <summary>
        /// Attempts to raise the PropertyChanged event, and 
        /// invokes the virtual AfterPropertyChanged method, 
        /// regardless of whether the event was raised or not.
        /// </summary>
        /// <param name="propertyName">
        /// The property which was changed.
        /// </param>
        protected void RaisePropertyChanged(string propertyName)
        {
            this.VerifyProperty(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                // Get the cached event args.
                PropertyChangedEventArgs args =
                    GetPropertyChangedEventArgs(propertyName);

                // Raise the PropertyChanged event.
                handler(this, args);
            }

            this.AfterPropertyChanged(propertyName);
        }

        #endregion // Protected Members

        #region Private Helpers

        [Conditional("DEBUG")]
        private void VerifyProperty(string propertyName)
        {
            // Thanks to Rama Krishna Vavilala for the tip to use TypeDescriptor here, instead of manual
            // reflection, so that custom properties are honored too.
            // http://www.codeproject.com/KB/WPF/podder1.aspx?msg=2381272#xx2381272xx

            bool propertyExists = TypeDescriptor.GetProperties(this).Find(propertyName, false) != null;
            if (!propertyExists)
            {
                // The property could not be found,
                // so alert the developer of the problem.

                string msg = string.Format(
                    ERROR_MSG,
                    propertyName,
                    this.GetType().FullName);

                Debug.Fail(msg);
            }
        }

        #endregion // Private Helpers
    }
}