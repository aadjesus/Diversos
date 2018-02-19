using System.Windows;

namespace TestTreeViewList
{
    public interface ISelector
    {
        object SelectedItem { get; }
        event RoutedPropertyChangedEventHandler<object> SelectedItemChanged;
    }
}
