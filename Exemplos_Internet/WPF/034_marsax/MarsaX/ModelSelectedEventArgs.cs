using System;
using System.Windows;


namespace MarsaX
{
    /// <summary>
    /// CustomEventArgs : a custom event argument class
    /// which simply holds an int value representing 
    /// how many times the associated event has been fired
    /// </summary>
    public class ModelSelectedEventArgs : RoutedEventArgs
    {
        #region Instance fields
        public string ImageUrl { get; private set; }
        #endregion

        #region Ctor
        /// <summary>
        /// Constructs a new ModelSelectedEventArgs object
        /// using the parameters provided
        /// </summary>
        /// <param name="imageUrl">the image url for the 
        /// ModelUIElement3D selected</param>
        public ModelSelectedEventArgs(RoutedEvent routedEvent,string imageUrl)
            : base(routedEvent)
        {
            this.ImageUrl = imageUrl;
        }
        #endregion
    }
}
