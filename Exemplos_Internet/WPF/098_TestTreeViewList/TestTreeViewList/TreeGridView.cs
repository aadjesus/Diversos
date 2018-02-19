using System;
using System.Collections;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;

namespace TestTreeViewList
{
    public class TreeGridView : Control, ISelector
    {
        #region private fields
        private ItemsControl itemsPresenter;
        #endregion

        #region dependency properties
        /// <summary>
        /// Identifies the ItemsSource dependency property
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource",
            typeof(IEnumerable),
            typeof(TreeGridView),
            new UIPropertyMetadata(null));
        
        /// <summary>
        /// Gets or sets a collection used to generate the content of the TreeGridView. 
        /// This is a dependency property. 
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Identify the SelectedItem dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(object),
            typeof(TreeGridView),
            new UIPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the selected item or returns null if the selection is empty.
        /// This is a dependency property. 
        /// </summary>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Identify the Depth dependency property.
        /// </summary>
        public static readonly DependencyProperty DepthProperty = DependencyProperty.Register(
            "Depth",
            typeof(int),
            typeof(TreeGridView),
            new UIPropertyMetadata(3, new PropertyChangedCallback(OnDepthChanged)));

        /// <summary>
        /// Gets or sets the value which defines the depth of the datas linked to the control.
        /// This is a dependency property.
        /// </summary>
        public int Depth
        {
            get { return (int)GetValue(DepthProperty); }
            set { SetValue(DepthProperty, value); }
        }

        /// <summary>
        /// Identify the ItemTemplate dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            "ItemTemplate",
            typeof(DataTemplate),
            typeof(TreeGridView),
            new UIPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the DataTemplate which is used to render an item. 
        /// This is a dependency property.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        #endregion

        #region routed events
        /// <summary>
        /// Idenfity the SelectedItemChanged routed event.
        /// </summary>
        public static readonly RoutedEvent SelectedItemChangedEvent = EventManager.RegisterRoutedEvent(
            "SelectedItemChanged", 
            RoutingStrategy.Bubble, 
            typeof(RoutedPropertyChangedEventHandler<object>), 
            typeof(TreeGridView));

        /// <summary>
        /// Occurs when the selected item changes.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<object> SelectedItemChanged
        {
            add
            {
                AddHandler(SelectedItemChangedEvent, value);
            }
            remove
            {
                RemoveHandler(SelectedItemChangedEvent, value);
            }
        }
        #endregion

        static TreeGridView()
        {
            // register an event handler using the static EventManager class so that all
            // SelectionChangedEvent which bubbles up the visual tree from our children
            // are catched in a single point
            EventManager.RegisterClassHandler(
                typeof(TreeGridView),
                Selector.SelectionChangedEvent,
                new RoutedEventHandler(OnListBoxSelectionChanged));

            // track Focus changes in order to update selection
            EventManager.RegisterClassHandler(
                typeof(TreeGridView),
                Control.GotFocusEvent,
                new RoutedEventHandler(OnGotFocus));
        }

        public override void OnApplyTemplate()
        {
            // make sure that we're able to retrieve the needed ItemsControl
            this.itemsPresenter = this.Template.FindName("PART_ItemsPresenter", this) as ItemsControl;
            if (this.itemsPresenter == null)
            {
                throw new InvalidOperationException("PART_ItemsPresenter not found in the visual tree");
            }

            this.FillItemsPresenter(this.Depth);
        }

        private static void OnListBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            Selector source = (Selector)e.OriginalSource;

            ((TreeGridView) e.Source).UpdateSelection(source.SelectedItem);
        }

        private static void OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is ListBoxItem)
            {
                // if a ListBoxItem receives focus, update the selection
                ListBoxItem listBoxItem = (ListBoxItem)e.OriginalSource;
                ListBox listBox = (ListBox)ItemsControl.ItemsControlFromItemContainer(listBoxItem);

                TreeGridView tgv = (TreeGridView)e.Source;
                tgv.UpdateSelection(listBox.SelectedItem);                
            }
        }

        private static void OnDepthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TreeGridView) d).FillItemsPresenter((int) e.NewValue);
        }

        private void UpdateSelection(object selectedItem)
        {
            object oldValue = this.SelectedItem;

            // update the DP which contains the actual SelectedItem 
            this.SelectedItem = selectedItem;

            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Loaded,
                new Action(this.SetIsReallySelected));

            // raise the SelectedItemChanged event
            var e = new RoutedPropertyChangedEventArgs<object>(oldValue, selectedItem, SelectedItemChangedEvent);
            base.RaiseEvent(e);
        }

        private void SetIsReallySelected()
        {
            // set the IsReallySelected attached property for all our ListBoxItems
            var listBoxItems = TreeHelper.FindVisualChildren<ListBoxItem>(this);
            foreach (var listBoxItem in listBoxItems)
            {
                ListBox listBox = (ListBox)ItemsControl.ItemsControlFromItemContainer(listBoxItem);
                bool isSelectionActive = (bool)listBox.GetValue(Selector.IsSelectionActiveProperty);

                // set the property to true if the item is the SelectedItem and its parent
                // ListBox as the IsSelectionActive property set to true
                if (listBoxItem.DataContext == this.SelectedItem && isSelectionActive)
                {
                    ActiveSelectionManager.SetIsReallySelected(listBoxItem, true);
                }
                else
                {
                    ActiveSelectionManager.SetIsReallySelected(listBoxItem, false);
                }
            }
        }

        /// <summary>
        /// Setup bindings between each ListBox so that a ListBox has its ItemsSource set to the
        /// previous SelectedItem of the previous ListBox in the list.
        /// </summary>
        private void SetupBindings()
        {
            // retrieve all ListBox which have been generated
            var listboxes = TreeHelper.FindVisualChildren<ListBox>(this);

            // setup bindings properly between each ListBox
            for (int i = 0; i < listboxes.Count; i++)
            {
                var binding = new Binding();

                var expression = this.GetBindingExpression(ItemsSourceProperty);
                var path = expression.ParentBinding.Path.Path;

                if (i == 0)
                {
                    binding.Path = new PropertyPath("ItemsSource");
                    binding.Source = this;
                }
                else
                {
                    binding.Path = new PropertyPath("SelectedItem." + path);
                    binding.Source = listboxes[i - 1];
                }

                listboxes[i].SetBinding(ItemsControl.ItemsSourceProperty, binding);
            }
        }

        private void FillItemsPresenter(int depth)
        {
            this.itemsPresenter.Items.Clear();

            for (int i = 0; i < depth; i++)
            {
                // we add dummies object just to make the ItemsControl create
                // its children the number of time we want
                this.itemsPresenter.Items.Add(new object());
            }

            // setup bindings just when layout and redering is completed in order
            // to be able to go through the visual tree
            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Loaded,
                new Action(this.SetupBindings));
        }
    }
}
