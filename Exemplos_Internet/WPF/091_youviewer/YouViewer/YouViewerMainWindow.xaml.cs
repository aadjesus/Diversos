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
using System.Windows.Shapes;

namespace YouViewer
{
    /// <summary>
    /// Hosts a WPF.JoshSmith.Controls.DragCanvas which
    /// in turn holds n-many YouTubeResultControls
    /// </summary>
    public partial class YouViewerMainWindow : Window
    {
        #region Data
        private Random rand = new Random(50);
        #endregion

        #region Ctor
        public YouViewerMainWindow()
        {
            InitializeComponent();
            viewer.ClosedEvent += viewer_ClosedEvent;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Raised when Viewer is closed
        /// </summary>
        private void viewer_ClosedEvent(object sender, EventArgs e)
        {
            dragCanvas.Opacity = 1.0;
        }

        /// <summary>
        /// Create a new YouTubeResultControl for each YouTubeInfo in input list
        /// </summary>
        private void PopulateCanvas(List<YouTubeInfo> infos)
        {
            dragCanvas.Children.Clear();
            for (int i = 0; i < infos.Count; i++)
            {
                YouTubeResultControl control = new YouTubeResultControl { Info = infos[i] };
                int angleMutiplier = i % 2 == 0 ? 1 : -1;
                control.RenderTransform = new RotateTransform { Angle = GetRandom(30, angleMutiplier) };
                control.SetValue(Canvas.LeftProperty, GetRandomDist(dragCanvas.ActualWidth - 150.0));
                control.SetValue(Canvas.TopProperty, GetRandomDist(dragCanvas.ActualHeight - 150.0));
                control.SelectedEvent += control_SelectedEvent;
                dragCanvas.Children.Add(control);
            }
        }

        /// <summary>
        /// Raised when a new YouTubeResultControl is selected
        /// </summary>
        private void control_SelectedEvent(object sender, YouTubeResultEventArgs e)
        {
            dragCanvas.Opacity = 0.6;
            viewer.VideoUrl = e.Info.EmbedUrl;
        }

        /// <summary>
        /// Get a new random number between 0.0 and 1.0 which is mutltiplied by
        /// limit and angleMutiplier
        /// </summary>
        private int GetRandom(double limit, int angleMutiplier)
        {
            return (int)((rand.NextDouble() * limit) * angleMutiplier);
        }

        /// <summary>
        /// Get a new random number between 0.0 and 1.0 which is mutltiplied by
        /// limit
        /// </summary>
        private double GetRandomDist(double limit)
        {
            return rand.NextDouble() * limit;
        }

        /// <summary>
        /// Get Most Popular Videos
        /// </summary>
        private void btnMostPopular_Click(object sender, RoutedEventArgs e)
        {
            List<YouTubeInfo> infos = YouTubeProvider.LoadMostPopularVideos();
            PopulateCanvas(infos);
        }

        /// <summary>
        /// Do a key word search
        /// </summary>
        private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtKeyWord.Text != string.Empty)
                {
                    List<YouTubeInfo> infos = YouTubeProvider.LoadVideosKey(txtKeyWord.Text);
                    PopulateCanvas(infos);
                }
                else
                {
                    MessageBox.Show("you need to enter a search word", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        #region Drag Methods
        /// <summary>
        /// Toggles the WPF.JoshSmith.Controls.DragCanvas.SetCanBeDragged
        /// property for all the YouTubeResultControl controls within the
        /// WPF.JoshSmith.Controls.DragCanvas
        /// </summary>
        private void OnMenuItemClick(object sender, RoutedEventArgs e)
        {

            bool canBeDragged = WPF.JoshSmith.Controls.DragCanvas.GetCanBeDragged(dragCanvas.Children[0]);
            canBeDragged = !canBeDragged;
            foreach (YouTubeResultControl child in dragCanvas.Children)
            {
                WPF.JoshSmith.Controls.DragCanvas.SetCanBeDragged(child, canBeDragged);
                child.DragMode = canBeDragged;
            }
        }
        #endregion


        #endregion
    }
}
