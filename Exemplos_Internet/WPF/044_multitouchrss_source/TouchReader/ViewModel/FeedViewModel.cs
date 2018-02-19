using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Ricciolo.PaperBoy.Feeds;

namespace VirtualDreams.TouchReader.ViewModel
{
    internal class FeedViewModel : RssViewModelBase
    {
        private readonly Feed _feed;
        private ReadOnlyCollection<RssViewModelBase> _items;

        public FeedViewModel(Feed feed)
        {
            _feed = feed;
        }

        protected override string GetTitle() { return _feed.Title; }

        public bool HasUnreadItems
        {
            get { return _feed.HasUnreadItems; }
        }

        public int UnreadItemCount
        {
            get { return _feed.UnreadItemCount; }
        }

        public string TitleAndUnreadItemCount
        {
            get { return Title + " (" + UnreadItemCount + ")"; }
        }

        public ReadOnlyCollection<RssViewModelBase> Items
        {
            get
            {
                if (_items == null)
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += RefreshItems;
                    bw.RunWorkerAsync();
                }
                return _items;
            }
        }

        void RefreshItems(object sender, DoWorkEventArgs e)
        {
            _feed.Items.Refresh(true);
            LoadItems();
        }

        private void LoadItems()
        {
            var itemsList = new List<RssViewModelBase>();

            foreach (var item in _feed.Items)
                itemsList.Add(new FeedItemViewModel(item));

            _items = new ReadOnlyCollection<RssViewModelBase>(itemsList);
            RaisePropertyChanged("Items");
            RaisePropertyChanged("HasUnreadItems");
            RaisePropertyChanged("UnreadItemCountString");
            RaisePropertyChanged("TitleAndUnreadItemCount");
            RaisePropertyChanged("Favicon");
            RaisePropertyChanged("FaviconBrush");
        }
    }
}