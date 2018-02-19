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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace ScrollControl
{
    public enum SortingType { Normal, ByDate, ByExtension, BySize };

    /// <summary>
    /// Is a simple control that contains numerous DPs that relate
    /// to the FileInfo that is set via the File property
    /// </summary>
    public partial class ItemHolder : UserControl
    {
        #region Data
        private AdornerLayer adornerLayer;
        private ItemHolderAdorner adorner;
        private FileInfo file;
        #endregion

        #region Ctor
        public ItemHolder()
        {
            InitializeComponent();
            this.MouseEnter += new MouseEventHandler(ItemHolder_MouseEnter);
            this.MouseLeave += new MouseEventHandler(ItemHolder_MouseLeave);
            this.MouseDoubleClick += new MouseButtonEventHandler(ItemHolder_MouseDoubleClick);
            this.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
            this.SetValue(VerticalAlignmentProperty, VerticalAlignment.Center);
        }
        #endregion

        #region Created Events
        //The actual event routing
        public static readonly RoutedEvent NewlySelectedItemHolderEvent =
            EventManager.RegisterRoutedEvent(
            "NewlySelectedItemHolder", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(ItemHolder));

        //add remove handlers
        public event RoutedEventHandler NewlySelectedItemHolder
        {
            add { AddHandler(NewlySelectedItemHolderEvent, value); }
            remove { RemoveHandler(NewlySelectedItemHolderEvent, value); }
        }

        #endregion

        #region Private Methods
        private void ItemHolder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //raise new item selected event
            RoutedEventArgs args =
                new RoutedEventArgs(NewlySelectedItemHolderEvent);
            RaiseEvent(args);
        }

        private void ItemHolder_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Opacity = 0.6;
            //remove adorner
            if (adorner != null)
            {
                adornerLayer.Remove(adorner);
                adorner = null;
            }
        }

        private void ItemHolder_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Opacity = 1.0;
            //raise new item selected event
            RoutedEventArgs args =
                new RoutedEventArgs(NewlySelectedItemHolderEvent);
            RaiseEvent(args);


            //and add an adorner which shows this element as a VisualBrush
            adornerLayer = AdornerLayer.GetAdornerLayer(this);
            if (adorner == null)
            {
                adorner = new ItemHolderAdorner(this, 
                    TranslatePoint(new Point(0, 0), 
                    this.Parent as UIElement));
                adornerLayer.Add(adorner);
            }
        }
        #endregion

        #region DPs
        /// <summary>
        /// File Date (3 letter extension) for the Template usage
        /// </summary>
        public DateTime FileDate
        {
            get { return (DateTime)GetValue(FileDateProperty); }
            set { SetValue(FileDateProperty, value); }
        }

        public static readonly DependencyProperty FileDateProperty =
            DependencyProperty.Register("FileDate", typeof(DateTime), 
            typeof(ItemHolder), new UIPropertyMetadata(DateTime.Now));


        /// <summary>
        /// File Type (3 letter extension) for the Template usage
        /// </summary>
        public string FileExtension
        {
            get { return (string)GetValue(FileExtensionProperty); }
            set { SetValue(FileExtensionProperty, value); }
        }


        public static readonly DependencyProperty FileExtensionProperty =
            DependencyProperty.Register("FileExtension", typeof(string),
            typeof(ItemHolder), new UIPropertyMetadata(string.Empty));


        /// <summary>
        /// File KiloBytes for the Template usage
        /// </summary>
        public string FileKBytes
        {
            get { return (string)GetValue(FileKBytesProperty); }
            set { SetValue(FileKBytesProperty, value); }
        }


        public static readonly DependencyProperty FileKBytesProperty =
            DependencyProperty.Register("FileKBytes", typeof(string),
            typeof(ItemHolder), new UIPropertyMetadata(string.Empty));


        /// <summary>
        /// File size (raw int value) for the Sorting ONLY
        /// </summary>
        public long FileSize
        {
            get { return (long)GetValue(FileSizeProperty); }
            set { SetValue(FileSizeProperty, value); }
        }

        public static readonly DependencyProperty FileSizeProperty =
            DependencyProperty.Register("FileSize", typeof(long),
            typeof(ItemHolder), new UIPropertyMetadata());



        /// <summary>
        /// File name for the Template usage
        /// </summary>
        public string FileNameForImage
        {
            get { return (string)GetValue(FileNameForImageProperty); }
            set { SetValue(FileNameForImageProperty, value); }
        }


        public static readonly DependencyProperty FileNameForImageProperty =
            DependencyProperty.Register("FileNameForImage",
            typeof(string), typeof(ItemHolder), new UIPropertyMetadata(string.Empty));


        
        /// <summary>
        /// Sort type for the Template usage
        /// </summary>
        public SortingType SortType
        {
            get { return (SortingType)GetValue(SortTypeProperty); }
            set { SetValue(SortTypeProperty, value); }
        }


        public static readonly DependencyProperty SortTypeProperty =
            DependencyProperty.Register("SortType",
            typeof(SortingType), typeof(ItemHolder), new UIPropertyMetadata(SortingType.Normal));


        #endregion

        #region Public Methods/Properties
        /// <summary>
        /// The current file that thi class represents
        /// </summary>
        public FileInfo File 
        {
            get { return file; }
            set
            {
                try
                {
                    file = value;

                    //set if its an image
                    if (file.Name.IsImageFile())
                    {
                        FileNameForImage = file.FullName;
                    }

                    //set other properties
                    FileDate = file.CreationTime;
                    FileExtension = file.Extension.Substring(file.Extension.LastIndexOf(".") + 1).ToUpper();
                    FileKBytes = (file.Length / 1024).ToString() + " Kb";
                    FileSize = file.Length;
                    SortType = SortingType.ByDate;

                    //set the DataContext to be itself, to allow 
                    //Binding to the DPs within this class
                    this.DataContext = this;
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine(string.Format(
                        "Problem with file {0} within ItemHolder property Setter", 
                        file.Name));
                }
            }
        }
        #endregion
    }
}
