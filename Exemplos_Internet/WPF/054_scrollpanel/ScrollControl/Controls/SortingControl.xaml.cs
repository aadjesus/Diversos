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
using System.Windows.Interop;

namespace ScrollControl
{
    /// <summary>
    /// This is a simple control that hosts a number of buttons/labels
    /// and a single instance of a <see cref="ScrollerControl">
    /// ScrollerControl</see>. The buttons call methods within the
    /// <see cref="ScrollerControl"> ScrollerControl</see>, whilst the 
    /// labels display results from the events raised by the 
    /// <see cref="ScrollerControl"> ScrollerControl</see>
    /// </summary>
    public partial class SortingControl : UserControl
    {
        #region Ctor
        /// <summary>
        /// Initialises several of the UIElements
        /// </summary>
        public SortingControl()
        {
            InitializeComponent();

            this.Loaded += delegate
            {
                this.spSortArea.Visibility = Visibility.Hidden;
                GLOBALS.footerPanelHeight = dockFooter.ActualHeight;
                this.scrollerControl.AddedItems += new CurrentItemEventHandler(scrollerControl_AddedItems);
                this.scrollerControl.NewlySelectedItem += new CurrentItemEventHandler(scrollerControl_NewlySelectedItem);
                //add a RoutedEventHandler to listen to the ItemHolder.NewlySelectedItemHolderEvent
                this.AddHandler(ItemHolder.NewlySelectedItemHolderEvent,
                    new RoutedEventHandler(ItemHolder_NewlySelectedItem), false);
             };
        }
        #endregion

        #region Private Methods

        #region Event subscriptions
        /// <summary>
        /// A new <see cref="ItemHolder">ItemHolder</see> has been
        /// selected, so show its filename within a label
        /// </summary>
        private void ItemHolder_NewlySelectedItem(object sender, RoutedEventArgs e)
        {
            ItemHolder itemHolder = e.OriginalSource as ItemHolder;
            if (itemHolder != null)
                if (itemHolder.File !=null)
                    this.lblCurrentFile.Content = "Current filename : " 
                        + itemHolder.File.FullName;
        }

        /// <summary>
        /// A new <see cref="ScrollerControl">ScrollerControl</see> item has been
        /// selected, so show its index within a label
        /// </summary>
        private void scrollerControl_NewlySelectedItem(object sender, CurrentItemEventArgs e)
        {
            this.lblItemNo.Content = e.Number;
        }

        /// <summary>
        /// The <see cref="ScrollerControl">ScrollerControl</see> items have been
        /// added, so show the current count within a label
        /// </summary>
        private void scrollerControl_AddedItems(object sender, CurrentItemEventArgs e)
        {
            this.spItemNumbers.Visibility = e.Number > 0 ?
                Visibility.Visible : Visibility.Hidden;
            this.spSortArea.Visibility = e.Number > 0 ?
                Visibility.Visible : Visibility.Hidden;
            this.lblTotal.Content = e.Number;
        }
        #endregion

        #region user initiated actions

        /// <summary>
        /// Allow user to pick a new Directory. The name of the
        /// directory is then passed to the contained
        /// <see cref="ScrollerControl"> ScrollerControl</see>s.LoadItems()
        /// method, where it is used to create individual 
        /// <see cref="ItemHolder">ItemHolder</see> items representing the
        /// actual files in the picked directory
        /// </summary>
        private void btnPickLocation_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog fbd =
                new System.Windows.Forms.FolderBrowserDialog())
            {
                fbd.RootFolder = Environment.SpecialFolder.MyComputer;
                if (fbd.ShowDialog(null) == System.Windows.Forms.DialogResult.OK)
                    if (!fbd.SelectedPath.Equals(string.Empty))
                        scrollerControl.LoadItems(fbd.SelectedPath);
            }
        }

        /// <summary>
        /// Set the contained <see cref="ScrollerControl"> ScrollerControl</see>
        /// to have no active sort applied
        /// </summary>
        private void btnNoSort_Click(object sender, RoutedEventArgs e)
        {
            this.scrollerControl.SortItems(SortingType.Normal);
        }

        /// <summary>
        /// Set the contained <see cref="ScrollerControl"> ScrollerControl</see>
        /// to have a ByDate sort applied
        /// </summary>
        private void btnByDate_Click(object sender, RoutedEventArgs e)
        {
            this.scrollerControl.SortItems(SortingType.ByDate);
        }

        /// <summary>
        /// Set the contained <see cref="ScrollerControl"> ScrollerControl</see>
        /// to have a ByExtension sort applied
        /// </summary>
        private void btnByExtension_Click(object sender, RoutedEventArgs e)
        {
            this.scrollerControl.SortItems(SortingType.ByExtension);
        }

        /// <summary>
        /// Set the contained <see cref="ScrollerControl"> ScrollerControl</see>
        /// to have a BySize sort applied
        /// </summary>
        private void btnBySize_Click(object sender, RoutedEventArgs e)
        {
            this.scrollerControl.SortItems(SortingType.BySize);
        }

        /// <summary>
        /// Minimize window
        /// </summary>
        private void btnMinimise_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Mamimise window
        /// </summary>
        private void btnMaximise_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Close application
        /// </summary>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("You are about to quit",
                "Quit confirmation", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        #endregion

        #endregion
    }
}
