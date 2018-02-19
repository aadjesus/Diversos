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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;

namespace FlipTile3D
{



    /// <summary>
    /// Displays a disciples blog/and allow for collapsing/expanding
    /// </summary>
    public partial class BlogDisplayer : UserControl
    {
        #region Data
        private Int32 sectionHeight;
        #endregion

        #region Ctor
        public BlogDisplayer()
        {
            InitializeComponent();
            this.Loaded += BlogDisplayer_Loaded;
            this.SizeChanged += BlogDisplayer_SizeChanged;

        }
        #endregion

        #region Private Methods
        private void BlogDisplayer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CreatePoints();
        }

        private void BlogDisplayer_Loaded(object sender, RoutedEventArgs e)
        {

            CreatePoints();
        }

        private void CreatePoints()
        {
            sectionHeight = (Int32)this.ActualHeight / 3;
            Point chevronTopRight = new Point(25, sectionHeight);
            Point chevronTopLeft = new Point(0, sectionHeight + 10);

            Point chevronBottomLeft = new Point(0, (sectionHeight * 2) - 10);
            Point chevronBottomRight = new Point(25, sectionHeight * 2);

            controlPath.Points.Clear();

            controlPath.Points.Add(new Point(25, 0));
            controlPath.Points.Add(chevronTopRight);
            controlPath.Points.Add(chevronTopLeft);
            controlPath.Points.Add(chevronBottomLeft);
            controlPath.Points.Add(chevronBottomRight);
            controlPath.Points.Add(new Point(25, this.ActualHeight));
            controlPath.Points.Add(new Point(this.ActualWidth, this.ActualHeight));
            controlPath.Points.Add(new Point(this.ActualWidth, 0));
            controlPath.Points.Add(new Point(25, 0));
        }

        private void ExpandCollapse_Click(object sender, RoutedEventArgs e)
        {

            if ((App.Current as App).AreDisciplesOpen)
            {
                this.Collapse = !Collapse;
                DoAnimation();
            }
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            webBrowser.Visibility = Visibility.Visible;
        }
        #endregion

        #region DPs
        #region NavigateToUrl

        /// <summary>
        /// NavigateToUrl Dependency Property
        /// </summary>
        public static readonly DependencyProperty NavigateToUrlProperty =
            DependencyProperty.Register("NavigateToUrl", typeof(String), typeof(BlogDisplayer),
                new FrameworkPropertyMetadata((String)String.Empty,
                    new PropertyChangedCallback(OnNavigateToUrlChanged)));

        /// <summary>
        /// Gets or sets the NavigateToUrl property. 
        /// </summary>
        public String NavigateToUrl
        {
            get { return (String)GetValue(NavigateToUrlProperty); }
            set { SetValue(NavigateToUrlProperty, value); }
        }

        /// <summary>
        /// Handles changes to the NavigateToUrl property.
        /// </summary>
        private static void OnNavigateToUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BlogDisplayer obj =  (BlogDisplayer)d;
            obj.webBrowser.Navigate(new Uri((String)e.NewValue));

        }


        #endregion

        #region Collapse

        /// <summary>
        /// Collapse Dependency Property
        /// </summary>
        public static readonly DependencyProperty CollapseProperty =
            DependencyProperty.Register("Collapse", typeof(bool), typeof(BlogDisplayer),
                new FrameworkPropertyMetadata((bool)true,
                    new PropertyChangedCallback(OnCollapseChanged)));

        /// <summary>
        /// Gets or sets the Collapse property.  
        /// </summary>
        public bool Collapse
        {
            get { return (bool)GetValue(CollapseProperty); }
            set { SetValue(CollapseProperty, value); }
        }

        /// <summary>
        /// Handles changes to the Collapse property.
        /// </summary>
        private static void OnCollapseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BlogDisplayer obj = (BlogDisplayer)d;
            obj.btnExpandCollapse.IsChecked = !(Boolean)e.NewValue;
        }





        #endregion
        #endregion

        #region Public Methods
        public void DoAnimation()
        {
            Storyboard sb = null;
            if (Collapse)
                sb = this.TryFindResource("CollapseStory") as Storyboard;
            else
                sb = this.TryFindResource("ExpandStory") as Storyboard;

            if (sb != null)
            {
                webBrowser.Visibility = Visibility.Collapsed;
                sb.Begin();
            }
        }
        #endregion
    }
}
