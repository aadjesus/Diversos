using System.ComponentModel;
using System.Windows.Markup;
using Ricciolo.PaperBoy.Feeds;
using Ricciolo.PaperBoy.UI.HTMLConverter;
using System.Windows.Documents;

namespace VirtualDreams.TouchReader.ViewModel
{
    class FeedItemViewModel : RssViewModelBase
    {
        public FeedItemViewModel(FeedItem feedItem)
        {
            FeedItem = feedItem;
        }

        private FeedItem _feedItem;

        public FeedItem FeedItem
        {
            get { return _feedItem; }
            set
            {
                _feedItem = value;
                RaisePropertyChanged("FeedItem");
                RaisePropertyChanged("Title");
                RaisePropertyChanged("IsRead");
                RaisePropertyChanged("Content");
            }
        }

        protected override string GetTitle()
        {
            var titleString = _feedItem.Title;
            // if there's no title we try to show the date
            if (string.IsNullOrEmpty(titleString))
                titleString = "Posted at " + _feedItem.PubDate;
            return titleString;
        }

        public bool IsRead
        {
            get { return FeedItem.IsRead; }
            set
            {
                FeedItem.IsRead = value;
                RaisePropertyChanged("IsRead");
            }
        }

        private string _contentXaml;
        private const string ErrorXaml =
            "<FlowDocument xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                "<Paragraph><Run>Loading failed!</Run></Paragraph>" +
                "<Paragraph><Run>Please try another blog post.</Run></Paragraph>" +
            "</FlowDocument>";

        public FlowDocument Content
        {
            get
            {
                if (_contentXaml == null)
                {
                    LoadContent();
                    return null;
                }

                try
                {
                    return XamlReader.Parse(_contentXaml) as FlowDocument;
                }
                catch (XamlParseException)
                {
                    return XamlReader.Parse(ErrorXaml) as FlowDocument;
                }
            }
        }

        private void LoadContent()
        {
            var bw = new BackgroundWorker();
            bw.DoWork += RefreshContent;
            bw.RunWorkerCompleted += LoadCompleted;
            bw.RunWorkerAsync();
        }

        void LoadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_contentXaml == null || e.Cancelled || e.Error != null)
                _contentXaml = ErrorXaml;
            else
                IsRead = true;
            RaisePropertyChanged("Content");
        }

        void RefreshContent(object sender, DoWorkEventArgs e)
        {
            _feedItem.Refresh();
            if (string.IsNullOrEmpty(_feedItem.Description)) return;
            _contentXaml = HtmlToXamlConverter.ConvertHtmlToXaml(_feedItem.Description, true);
        }
    }
}