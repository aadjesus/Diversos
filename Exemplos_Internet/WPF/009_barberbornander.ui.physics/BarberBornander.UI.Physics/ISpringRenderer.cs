using System;

namespace BarberBornander.UI.Physics
{
    /// <summary>
    /// A simple rendering interface
    /// </summary>
    public interface ISpringRenderer
    {
        #region Interface Methods
        /// <summary>
        /// Render the Spring visual respresentation
        /// using the drawingContext
        /// </summary>
        void Render(Spring spring, 
            System.Windows.Media.DrawingContext drawingContext);
        #endregion
    }
}
