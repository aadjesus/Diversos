using System.Windows;

namespace MarsaX
{

    /// <summary>
    /// This class simply provides some Attached Properties
    /// </summary>
    public static class SearchButtonProps
    {
        #region DPs

        /// <summary>
        /// This Attached DP is used to give the Search Buttons an extra property
        /// that can be used in the XAML binding to show a outer glow for the currently
        /// selected Button
        /// </summary>
        #region IsCurrentlySelected
        /// <summary>
        /// Getter
        /// </summary>
        public static bool GetIsCurrentlySelected(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCurrentlySelectedProperty);
        }

        /// <summary>
        /// Setter
        /// </summary>
        public static void SetIsCurrentlySelected(
            DependencyObject obj, bool value)
        {
            obj.SetValue(IsCurrentlySelectedProperty, value);
        }

        /// <summary>
        /// Declaration
        /// </summary>
        public static readonly DependencyProperty IsCurrentlySelectedProperty =
            DependencyProperty.RegisterAttached(
            "IsCurrentlySelected",
            typeof(bool),
            typeof(SearchButtonProps),
            new UIPropertyMetadata(false));
        #endregion

        #endregion
    }
}
