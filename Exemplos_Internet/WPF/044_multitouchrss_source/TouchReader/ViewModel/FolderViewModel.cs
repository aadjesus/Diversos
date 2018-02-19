using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Ricciolo.PaperBoy.Feeds;

namespace VirtualDreams.TouchReader.ViewModel
{
    internal class FolderViewModel : RssViewModelBase
    {
        private readonly FeedFolder _folder;

        public FolderViewModel(FeedFolder folder)
        {
            _folder = folder;
        }

        protected override string GetTitle() { return string.IsNullOrEmpty(_folder.Name) ? "Feeds" : _folder.Name; }

        public bool IsRoot
        {
            get { return _folder.IsRoot; }
        }

        private ReadOnlyCollection<RssViewModelBase> _items;

        public ReadOnlyCollection<RssViewModelBase> Items
        {
            get
            {
                if (_items == null)
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += RefreshFoldersAndFeeds;
                    bw.RunWorkerAsync();
                }
                return _items;
            }
        }

        void RefreshFoldersAndFeeds(object sender, DoWorkEventArgs e)
        {
            _folder.FoldersAndFeeds.Refresh();
            LoadFoldersAndFeeds();
        }

        private void LoadFoldersAndFeeds()
        {
            var foldersAndFeedsList = new List<RssViewModelBase>();
            foreach (var feedBase in _folder.FoldersAndFeeds)
            {
                if (feedBase is FeedFolder)
                {
                    var subFolder = feedBase as FeedFolder;
                    if (subFolder.TotalItemCount > 0)
                        foldersAndFeedsList.Add(new FolderViewModel(subFolder));
                }
                else if (feedBase is Feed)
                {
                    var feed = feedBase as Feed;
                    if (feed.ItemCount > 0)
                        foldersAndFeedsList.Add(new FeedViewModel(feed));
                }
            }
            _items = new ReadOnlyCollection<RssViewModelBase>(foldersAndFeedsList);
            RaisePropertyChanged("Items");
        }
    }
}