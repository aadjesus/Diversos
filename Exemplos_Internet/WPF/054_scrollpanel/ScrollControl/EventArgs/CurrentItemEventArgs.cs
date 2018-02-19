using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScrollControl
{
    //the event handler delegate
    public delegate void CurrentItemEventHandler(object sender,
        CurrentItemEventArgs e);

    /// <summary>
    /// CurrentItemEventArgs : a custom event argument class
    /// which simply holds the number a single integer
    /// </summary>
    public class CurrentItemEventArgs : RoutedEventArgs
    {
        #region Data
        public int Number { get; set; }
        #endregion //Data

        #region Ctor
        /// <summary>
        /// Constructs a new CurrentItemEventArgs object
        /// using the parameters provided
        /// </summary>
        /// <param name="numberOfItems">the int value</param>
        public CurrentItemEventArgs(RoutedEvent routedEvent,
            int number)
            : base(routedEvent)
        {
            this.Number = number;
        }
        #endregion //Ctor
    }
}
