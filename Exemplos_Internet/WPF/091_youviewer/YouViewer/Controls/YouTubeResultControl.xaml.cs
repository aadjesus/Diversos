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

using WPF.JoshSmith.Controls;

namespace YouViewer
{
    /// <summary>
    /// Event delegate
    /// </summary>
    public delegate void SelectedEventHandler(object sender, YouTubeResultEventArgs e);


    /// <summary>
    /// Shows a single image, and when Mouse is over and the mode is
    /// not in drag mode, then show a play button, which when
    /// clicked will notify the YouViewerMainWindow to show a
    /// Viewer control
    /// </summary>
    public partial class YouTubeResultControl : UserControl
    {
        #region Data
        private bool dragMode = true;
        private YouTubeInfo info = null;
        public event SelectedEventHandler SelectedEvent;
        #endregion

        #region Ctor
        public YouTubeResultControl()
        {
            InitializeComponent();
            //Loaded
            this.Loaded += delegate
            {
                DragMode = true;
                imageMain.SetValue(DragCanvas.CanBeDraggedProperty, true);
            };
            //MouseEnter
            this.MouseEnter += delegate
            {
                if (!this.DragMode)
                {
                    Storyboard sb = this.TryFindResource("OnMouseEnter") as Storyboard;
                    if (sb != null)
                        sb.Begin(this);
                }
            };
            //MouseLeave
            this.MouseLeave += delegate
            {
                if (!this.DragMode)
                {
                    Storyboard sb = this.TryFindResource("OnMouseLeave") as Storyboard;
                    if (sb != null)
                        sb.Begin(this);
                }
            };
        }
        #endregion

        #region Events
        /// <summary>
        /// Raised when this control btnPlay is clicked
        /// </summary>
        protected virtual void OnSelectedEvent(YouTubeResultEventArgs e) 
        {
            if (SelectedEvent != null) 
            {
                //Invokes the delegates.
                SelectedEvent(this, e); 
            }
        }
        #endregion

        #region Properties

        private string ImageUrl
        {
            set
            {
                BitmapImage bmp = new BitmapImage(new Uri(value, UriKind.RelativeOrAbsolute));
                imageMain.Source = bmp;
            }
        }


        public YouTubeInfo Info
        {
            get { return info; }
            set
            {
                info = value;
                ImageUrl = info.ThumnailUrl;
            }
        }


        public bool DragMode
        {
            private get { return dragMode; }
            set
            {
                dragMode = value;
                imageMain.SetValue(DragCanvas.CanBeDraggedProperty, dragMode);
                lblDragMode.Content = dragMode ? "Drag Mode" : "Play Mode";

            }
        }
        #endregion

        #region Private Methods

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            OnSelectedEvent(new YouTubeResultEventArgs(Info));
        }
        #endregion

    }
}
