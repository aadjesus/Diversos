using System.Windows;

namespace TestTreeViewList
{
    public class ActiveSelectionManager
    {
        /// <summary>
        /// Gets the value of the IsReallySelected attached property that indicates whether an item is selected
        /// and its parent ListBox focused.
        /// </summary>
        /// <param name="obj">ListBoxItem which has the value attached</param>
        /// <returns>True if the ListBoxItem is "really" selected</returns>
        public static bool GetIsReallySelected(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsReallySelectedProperty);
        }

        /// <summary>
        /// Sets the value of the IsReallySelected attached property that indicates whether an item is selected
        /// and its parent ListBox focused.
        /// </summary>
        /// <param name="obj">ListBoxItem which has the value attached</param>
        /// <param name="value">Value of the property to set</param>
        /// <returns>True if the ListBoxItem is "really" selected</returns>
        public static void SetIsReallySelected(DependencyObject obj, bool value)
        {
            obj.SetValue(IsReallySelectedProperty, value);
        }

        /// <summary>
        /// Identifies the IsReallySelected attached property. This attached property adds a IsReallySelected
        /// boolean value to a ListBoxItem. The value is true when the ListBoxItem is selected and the parent
        /// ListBox has its IsSelectionActive value set to true (ListBox is focused). The value of the attached
        /// property is sets by the TreeGridView control.
        /// </summary>
        public static readonly DependencyProperty IsReallySelectedProperty = DependencyProperty.RegisterAttached(
            "IsReallySelected",
            typeof(bool),
            typeof(ActiveSelectionManager),
            new UIPropertyMetadata(false));
    }
}
