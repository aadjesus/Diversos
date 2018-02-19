using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Amazon = MediaMania.Amazon;


namespace MediaMania
{
    /// <summary>
    /// Interaction logic for WindowMain.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        private Amazon.AmazonSearchService Ass;
        protected const string AMAZON_ID = "Put Your Amazon Acess Key here";
        private string searchMode = "books";
        public WindowMain()
        {
            InitializeComponent();
        }

        void ChangeSearchMode(object sender, EventArgs e)
        {
            RadioButton checkedRadioButton = (RadioButton)sender;
            searchMode = checkedRadioButton.Content.ToString().ToLower();
        }

        void buttonSearch_Clicked(object sender, EventArgs e)
        {
            this.labelStatus.Content = "Initializing search...";
            Amazon.KeywordRequest keywordRequest = new Amazon.KeywordRequest();
            keywordRequest.locale = "us";
            keywordRequest.sort = "reviewrank";
            keywordRequest.mode = searchMode;
            keywordRequest.keyword = this.textBoxSearchPhrase.Text;
            keywordRequest.tag = AMAZON_ID;
            keywordRequest.devtag = AMAZON_ID;
            keywordRequest.type = "heavy";
            keywordRequest.page = "1";

            try
            {
                if(Ass == null)
                    Ass = new Amazon.AmazonSearchService();
                Ass.KeywordSearchRequestCompleted += new MediaMania.Amazon.KeywordSearchRequestCompletedEventHandler(Ass_KeywordSearchRequestCompleted);
                this.labelStatus.Content = "Searching...";
                Ass.KeywordSearchRequestAsync(keywordRequest);
                buttonCancel.IsEnabled = true;
            }
            catch
            {
            }
        }

        void buttonCancel_Clicked(object sender, EventArgs e)
        {
            buttonCancel.IsEnabled = false;
            labelStatus.Content = "Aborting...";
            Ass.Abort();
            labelStatus.Content = "Ready";
        }

        void Ass_KeywordSearchRequestCompleted(object sender, MediaMania.Amazon.KeywordSearchRequestCompletedEventArgs e)
        {
            try
            {
                buttonCancel.IsEnabled = false;
                this.listBoxMedia.Items.Clear();
                Amazon.ProductInfo Pi = e.Result;

                this.labelStatus.Content = "Populating results...";
                foreach (Amazon.Details itemDetail in Pi.Details)
                {
                    DockPanel itemPanel = new DockPanel();
                    itemPanel.Margin = new Thickness(5);

                    BitmapImage bitmapImage;
                    if (itemDetail.ImageUrlMedium == "")
                        bitmapImage = new BitmapImage(new Uri("http://g-images.amazon.com/images/G/01/x-site/icons/no-img-sm.gif"));
                    else
                        bitmapImage = new BitmapImage(new Uri(itemDetail.ImageUrlSmall));
                    Image itemImage = new Image();
                    itemImage.Width = 100;
                    itemImage.Height = 100;
                    itemImage.Margin = new Thickness(0, 0, 5, 0);
                    itemImage.Source = bitmapImage;
                    itemPanel.Children.Add(itemImage);

                    ListBoxItem lbItem = new ListBoxItem();
                    lbItem.Content = itemDetail.ProductName;
                    lbItem.Background = new SolidColorBrush(Colors.Transparent);
                    lbItem.FontSize = 14;
                    itemPanel.Children.Add(lbItem);

                    this.listBoxMedia.Items.Add(itemPanel);
                }
                this.labelStatus.Content = Pi.Details.Length.ToString() + " of " + Pi.TotalResults;
            }
            catch
            {
                this.labelStatus.Content = "Not Found";
            }
        }

        private void buttonSearch_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}