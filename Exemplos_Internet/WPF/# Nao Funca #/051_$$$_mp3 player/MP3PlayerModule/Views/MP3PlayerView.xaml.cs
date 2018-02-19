using System;
using System.Collections.Generic;
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
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Data;
using Framework.MP3PlayerModule.Data;


namespace Framework.MP3PlayerModule.Views
{
    [SmartPart]
    public partial class MP3PlayerView : System.Windows.Controls.UserControl, IMP3PlayerView
    {
        private MP3PlayerViewPresenter presenter;

        [CreateNew]
        public MP3PlayerViewPresenter Presenter
        {
            set
            {
                presenter = value;
                presenter.View = this;
            }
        }
        
        public MP3PlayerView()
        {
            InitializeComponent();
            FitControlToParentArea();
            CreateButtonHandlers();
        }

        public void SetTrackListBinding(DataTable table)
        {
            DataContext = table;
        }

        public void RefreshListView()
        {
            TrackListView.Items.Refresh();
        }
        
        private void CreateButtonHandlers()
        {
            Play.Click += new RoutedEventHandler(Play_Click);
            Stop.Click += new RoutedEventHandler(Stop_Click);
            Browse.Click += new RoutedEventHandler(Browse_Click);
        }

        void Browse_Click(object sender, RoutedEventArgs e)
        {
            presenter.Browse();
        }

        void Stop_Click(object sender, RoutedEventArgs e)
        {
            presenter.Stop();
        }

        void Play_Click(object sender, RoutedEventArgs e)
        {
            DataRowView view = TrackListView.SelectedItem as DataRowView;
            LibraryInformation.DataTableTracksRow row = view.Row as LibraryInformation.DataTableTracksRow;
            presenter.Play(row.Path);
        }

        private void FitControlToParentArea()
        {
            this.Width = double.NaN;
            this.Height = double.NaN;
        }

    }
}