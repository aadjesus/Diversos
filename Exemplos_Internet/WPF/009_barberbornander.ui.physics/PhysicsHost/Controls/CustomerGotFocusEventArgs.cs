using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace PhysicsHost
{
    /// <summary>
    /// CustomerGotFocusEventArgs : a custom event argument class
    /// which simply holds an string value representing 
    /// the current CustomerID for the container CustomerUserControl
    /// </summary>
    public class CustomerGotFocusEventArgs : RoutedEventArgs
    {
        #region Instance fields
        public string CustomerID { get; private set; }
        #endregion

        #region Ctor
        /// <summary>
        /// Constructs a new CustomerGotFocusEventArgs object
        /// using the parameters provided
        /// </summary>
        /// <param name="someNumber">the value for the events args</param>
        public CustomerGotFocusEventArgs(RoutedEvent routedEvent,
            string customerID)
            : base(routedEvent)
        {
            this.CustomerID = customerID;
        }
        #endregion
    }

}
