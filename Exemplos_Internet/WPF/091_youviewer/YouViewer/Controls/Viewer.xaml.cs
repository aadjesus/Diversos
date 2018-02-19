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

namespace YouViewer
{
    /// <summary>
    /// Contains a new .NET 3.5 SP1 WebBrowser control
    /// that simply navigates to the YouTube SWF file
    /// Uri
    /// </summary>
    public partial class Viewer : UserControl
    {
        #region Data
        private string videoUrl = string.Empty;
        private static bool IsExpanded = false;
        public event EventHandler ClosedEvent;
        #endregion

        #region Ctor
        public Viewer()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Raised when the close button is clicked. This event
        /// is used by YouViewerMainWindow to set Opacity on its
        /// contained DragCanvas back to fully viewable Opacity
        /// </summary>
        protected virtual void OnClosedEvent(EventArgs e)
        {
            if (ClosedEvent != null)
            {
                //Invokes the delegates.
                ClosedEvent(this, e);
            }
        }
        #endregion

        #region Properties

        public string VideoUrl
        {
            set 
            {
                if (videoUrl != value)
                {
                    if (!IsExpanded)
                    {
                        videoUrl = value;
                        browser.Source = new Uri(videoUrl, UriKind.Absolute);
                        IsExpanded = true;
                        Storyboard sbEnter = this.TryFindResource("OnMouseEnter") as Storyboard;
                        if (sbEnter != null)
                        {
                            sbEnter.Completed += new EventHandler(sbEnter_Completed);
                            sbEnter.Begin(this);
                        }
                    }
                }
            }

        }
        #endregion

        #region Private Methods

        private void sbEnter_Completed(object sender, EventArgs e)
        {
            browser.Visibility = Visibility.Visible;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (IsExpanded)
            {
                browser.Visibility = Visibility.Collapsed;
                IsExpanded = false;
                browser.Source = null;
                Storyboard sbLeave = this.TryFindResource("OnMouseLeave") as Storyboard;
                if (sbLeave != null)
                {
                    sbLeave.Completed += sbLeave_Completed;     
                    sbLeave.Begin(this);
                }
            }
        }

        private void sbLeave_Completed(object sender, EventArgs e)
        {
            OnClosedEvent(new EventArgs());
        }
        #endregion
    }
}
