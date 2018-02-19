using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Xml;

namespace ImageTest
{
	public partial class frmLoader
	{

        private ListBox lstXMLData;
        private ImageTest.RSSImageFeed RSSImages = new ImageTest.RSSImageFeed();

        public frmLoader()
		{
			this.InitializeComponent();
            this.MinWidth = this.Width;
            this.MinHeight = this.Height;
            this.pnlNextPrev.Width = this.Width;
            this.SizeChanged +=new SizeChangedEventHandler(frmLoader_SizeChanged);
            this.cmbSearchType.SelectionChanged +=new SelectionChangedEventHandler(cmbSearchType_SelectionChanged);
            this.btnSearch.Click +=new RoutedEventHandler(btnSearch_Click);
            this.btnNext.Click +=new RoutedEventHandler(btnNext_Click);
            this.btnPrev.Click +=new RoutedEventHandler(btnPrev_Click);
            this.cmbSearchType.SelectedIndex = 0;


            //create the holding ListBox which will hold the Flickr RSS Images retrieved
            lstXMLData = new ListBox();
            lstXMLData.SelectionChanged +=new SelectionChangedEventHandler(lstXMLData_SelectionChanged);
            lstXMLData.Name = "lstXMLData";
            lstXMLData.Foreground = Brushes.White;
            Style itemsStyle = ItemsGrid.TryFindResource("WrapItemTemplate") as Style;
            if (itemsStyle != null)
                lstXMLData.Style = itemsStyle;

            //make the listbox have a transparent background
            Brush bTrans = Brushes.Transparent;
            bTrans.Freeze();
            lstXMLData.Background = bTrans;
            lstXMLData.BorderBrush = bTrans;
            lstXMLData.Margin = new Thickness(30, 20, 30, 10);
            pnlContents.Background = bTrans;
            pnlContents.Children.Add(lstXMLData);

            //getData from Flickr
            getFlickrData("MOST_RECENT","");
		}


        private void ItemClicked(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            Image img = (Image)b.FindName("AssociatedImage");
            frmImage fi = new frmImage();
            fi.CurrentImageURL = img.Source;
            fi.ShowDialog();
        }


        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            RSSImages.PageIndex--;
            doFlickrSearch();
        }


        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            RSSImages.PageIndex++;
            doFlickrSearch();
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            RSSImages.PageIndex = 0;
            doFlickrSearch();
        }


        private void doFlickrSearch()
        {
            string searchType = (cmbSearchType.SelectedItem as ComboBoxItem).Content.ToString();
            string searchWord = searchType.Equals("By Search Word") ? this.txtSearchWord.Text : "";
            if (searchType.Equals("By Search Word") && string.IsNullOrEmpty(searchWord))
            {
                MessageBox.Show("You MUST enter a search word, to search by keyword");
            }
            else
            {
                getFlickrData(searchType, searchWord);
            }
        }


        private void setNextPrevStates(bool prevEnabled, bool nextEnabled)
        {
            btnPrev.IsEnabled = prevEnabled;
            btnPrev.ToolTip = btnPrev.IsEnabled ? "Click to go back a page" : "There are no more pages";
            btnNext.IsEnabled = nextEnabled;
            btnNext.ToolTip = btnNext.IsEnabled ? "Click to go forward a page" : "There are no more pages";
        }


        private void cmbSearchType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string searchType = (cmbSearchType.SelectedItem as ComboBoxItem).Content.ToString();
            pnlSearchWord.Visibility = searchType.Equals("By Search Word") ? Visibility.Visible : Visibility.Hidden;
        }


        private void lstXMLData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstXMLData.UnselectAll();
        }


        private void frmLoader_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            pnlNextPrev.Width = this.RenderSize.Width;
            pnlHeader.Width = this.RenderSize.Width;
            pnlContents.Width = this.RenderSize.Width;
        }


        private void getFlickrData(string searchType,string searchWord)
        {
            //use the XLINQ Project, to go grab the requested RSS Feed
            //and save it to a local file
            if (RSSImages.LoadPictures(searchType, searchWord))
            {
                lblPageIngex.Visibility = Visibility.Visible;
                int pIndex = RSSImages.PageIndex;
                lblPageIngex.Content = "Page " + (++pIndex);

                XmlDataProvider xmldata = new XmlDataProvider();
                xmldata.Source = new Uri(@"c:\photos.xml", UriKind.Absolute);
                Binding binding = new Binding();
                binding.Source = xmldata;
                binding.XPath = @"/photos/photo";
                BindingOperations.SetBinding(lstXMLData, ListBox.ItemsSourceProperty, binding);
                lstXMLData.Visibility = Visibility.Visible;
                setNextPrevStates(RSSImages.IsPrevAvail, RSSImages.IsNextAvail);
            }
            else
            {
                lblPageIngex.Visibility = Visibility.Hidden;
                lstXMLData.Visibility = Visibility.Hidden;
                setNextPrevStates(false, false);
            }
        }
	}
}