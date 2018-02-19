using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;

namespace ScrollControl
{
    /// <summary>
    /// A simple control that holds a ItemControl whos ItemsPanelTemplate
    /// is set to be a <see cref="ColumnedPanel">ColumnedPanel</see>. This 
    /// class also provides various events that indicate when items have 
    /// been addded and selected. There are also methods available for
    /// adding items, and sorting the contained items.
    /// Each item will be an instance of a 
    /// <see cref="ItemHolder">ItemHolder</see>, so when the sorting is
    /// applied to the items, it is really just using the DPs exposed by
    /// the individual <see cref="ItemHolder">ItemHolder</see> items.
    /// 
    /// This also uses a single <see cref="ScrollerControlAdorner"> 
    /// ScrollerControlAdorner</see>  which has a left/right button pair
    /// that are used to manually scroll the contained 
    /// <see cref="FrictionScrollViewer">FrictionScrollViewer</see>
    /// </summary>
    public partial class ScrollerControl : UserControl
    {
        #region Data
        private AdornerLayer adornerLayer=null;
        private ScrollerControlAdorner adorner=null;
        private ColumnedPanel mainPanel = null;
        #endregion

        #region Ctor
        public ScrollerControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Created Events

        /// <summary>
        /// Notifies subscriber new items have been added
        /// </summary>
        public static readonly RoutedEvent AddedItemsEvent =
            EventManager.RegisterRoutedEvent(
            "AddedItems", RoutingStrategy.Bubble,
            typeof(CurrentItemEventHandler),
            typeof(ScrollerControl));

        //add remove handlers
        public event CurrentItemEventHandler AddedItems
        {
            add { AddHandler(AddedItemsEvent, value); }
            remove { RemoveHandler(AddedItemsEvent, value); }
        }


        /// <summary>
        /// Notifies subscriber new item has been selected
        /// </summary>
        public static readonly RoutedEvent NewlySelectedItemEvent =
            EventManager.RegisterRoutedEvent(
            "NewlySelectedItem", RoutingStrategy.Bubble,
            typeof(CurrentItemEventHandler),
            typeof(ScrollerControl));

        //add remove handlers
        public event CurrentItemEventHandler NewlySelectedItem
        {
            add { AddHandler(NewlySelectedItemEvent, value); }
            remove { RemoveHandler(NewlySelectedItemEvent, value); }
        }



        #endregion

        #region Private Methods

        /// <summary>
        /// When the contained ColumnedPanel is loaded, grab a reference to it
        /// for later use
        /// </summary>
        private void OnPanelLoaded(object sender, RoutedEventArgs e)
        {
            // Grab a reference to the TabPanel3D when it loads.
            mainPanel = sender as ColumnedPanel;
            mainPanel.SetValue(Control.VerticalAlignmentProperty, VerticalAlignment.Center);
        }

        /// <summary>
        /// Returns a new <see cref="ItemHolder">ItemHolder</see> that is populated
        /// with information about the file
        /// </summary>
        /// <param name="file">The file to use to populate the DPs of the created 
        /// <see cref="ItemHolder">ItemHolder</see></param>
        /// <returns>A new <see cref="ItemHolder">ItemHolder</see></returns>
        private ItemHolder AddItemHolder(FileInfo file)
        {

            ItemHolder itemHolder = new ItemHolder();
            itemHolder.File = file;
            itemHolder.Margin = new Thickness(4);
            itemHolder.NewlySelectedItemHolder += 
                new RoutedEventHandler(itemHolder_NewlySelectedItemHolder);

            ControlTemplate itemHolderTemplate = 
                this.TryFindResource("ItemHolderTemplate") as ControlTemplate;
            if (itemHolderTemplate != null)
                itemHolder.Template = itemHolderTemplate;

            return itemHolder;
        }


        /// <summary>
        /// <see cref="ItemHolder">ItemHolder</see> NewlySelectedItemHolder event has been
        /// raised by the individual <see cref="ItemHolder">ItemHolder</see>. So find the index
        /// of this <see cref="ItemHolder">ItemHolder</see>, and alert the parent control (
        /// <see cref="SortingControl">SortingControl</see>), which can use the information
        /// to update a label with the number of the <see cref="ItemHolder">ItemHolder</see>
        /// selected
        /// </summary>
        private void itemHolder_NewlySelectedItemHolder(object sender, RoutedEventArgs e)
        {
            //get index of sender
            int idx = this.itemsControl.Items.IndexOf(sender);

            //raise Newly Selected event, passing sender index + 1, as ItemsControl.Items
            //is 0 Based, but we want 1 based for text representation
            CurrentItemEventArgs args =
            new CurrentItemEventArgs(NewlySelectedItemEvent, idx +1);
            RaiseEvent(args);      
        }


        /// <summary>
        /// Set each <see cref="ItemHolder">ItemHolder</see> within the ItemsControl.Items
        /// collection to the sort parameter value
        /// </summary>
        /// <param name="sort">Sort type</param>
        private void SetItemsCurrentSort(SortingType sort)
        {
            foreach (ItemHolder itemHolder in this.itemsControl.Items)
                itemHolder.SortType = sort;
        }


        /// <summary>
        /// On resize, remove Adorner, and reshow it, as we need to recalculate
        /// its positions
        /// </summary>
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (adorner != null)
            {
                adornerLayer.Remove(adorner);
                adorner = new ScrollerControlAdorner(this);
                adornerLayer.Add(adorner);
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Contained <see cref="FrictionScrollViewer">FrictionScrollViewer</see>
        /// is scrolled by the delta. This delta is set via the 
        /// <see cref="ScrollerControlAdorner"> ScrollerControlAdorner</see> 
        /// associated with this control
        /// </summary>
        /// <param name="delta">The delat position</param>
        public void Scroll(Point delta)
        {
            // Scroll to the new position.
            ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.HorizontalOffset + delta.X);
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset + delta.Y);
        }

        /// <summary>
        /// Creates a new <see cref="ItemHolder">ItemHolder</see>
        /// for each file within the direcory specified by the path
        /// parameter.
        /// </summary>
        /// <param name="path"></param>
        public void LoadItems(string path)
        {
            ItemList.Instance.Clear();
            this.itemsControl.ItemsSource = ItemList.Instance;

            //Add new items, 1 file = 1 new ItemHolder
            foreach (FileInfo f in
                        (from file in new DirectoryInfo(path).GetFiles()
                         select file))
            {
                ItemList.Instance.Add(AddItemHolder(f));
            }

            //raise added item event, so subscriber can use event args
            //to see how many items there are now.
            CurrentItemEventArgs args =
                new CurrentItemEventArgs(AddedItemsEvent,
                    this.itemsControl.Items.Count);
            RaiseEvent(args);

            //Add adorner
            if (this.itemsControl.Items.Count > 0)
            {
                if (adorner == null)
                {
                    adornerLayer = AdornerLayer.GetAdornerLayer(this);
                    adorner = new ScrollerControlAdorner(this);
                    adornerLayer.Add(adorner);
                }
                else
                {
                    adornerLayer.Remove(adorner);
                    adorner = new ScrollerControlAdorner(this);
                    adornerLayer.Add(adorner);
                }
            }
        }

        /// <summary>
        /// Gets the current item count
        /// </summary>
        public int ItemCount
        {
            get { return this.itemsControl.Items.Count; }
        }

        /// <summary>
        /// Sorts the list of items, using the current sort as
        /// the SortingType to apply
        /// </summary>
        /// <param name="sort">The sort to use</param>
        public void SortItems(SortingType sort)
        {
            CollectionView defaultView =
                (CollectionView)CollectionViewSource.
                GetDefaultView(itemsControl.ItemsSource);


            switch (sort)
            {
                case SortingType.Normal:
                    defaultView.SortDescriptions.Clear();
                    SetItemsCurrentSort(sort);
                    break;
                case SortingType.ByDate:
                    defaultView.SortDescriptions.Add(new SortDescription("FileDate", 
                        ListSortDirection.Descending));
                    SetItemsCurrentSort(sort);
                    break;
                case SortingType.ByExtension:
                    defaultView.SortDescriptions.Add(new SortDescription("FileExtension", 
                        ListSortDirection.Descending));
                    SetItemsCurrentSort(sort);
                    break;
                case SortingType.BySize:
                    defaultView.SortDescriptions.Add(new SortDescription("FileSize", 
                        ListSortDirection.Descending));
                    SetItemsCurrentSort(sort);
                    break;
                default:
                    defaultView.SortDescriptions.Clear();
                    SetItemsCurrentSort(SortingType.Normal);
                    break;
            }
        }
        #endregion

    }
}
