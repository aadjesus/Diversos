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

namespace FlyingWindows
{
    /// <summary>
    /// Interaction logic for WindowsContainer.xaml
    /// </summary>
    public partial class WindowsContainer : UserControl
    {
        private List<ChildWindow> _ListWindows = new List<ChildWindow>();

        private double _ChildMargin = 0.0D;

        public double ChildMargin
        {
            get { return _ChildMargin; }
            set { _ChildMargin = value; }
        }

        public enum WindowLayoutMode
        {
            /// <summary>
            /// All windows shown proportionally
            /// </summary>
            Tiled = 0,
            /// <summary>
            /// One detailed and other tiny same sizes
            /// </summary>
            Detailed = 1,
        }

        public enum WindowLayoutPattern
        {
            EqualWeighting = 0,
            AsDesigned = 1
        }

        private WindowLayoutMode _Mode = WindowLayoutMode.Tiled;
        private WindowLayoutPattern _LayoutPattern = WindowLayoutPattern.AsDesigned;

        public WindowLayoutPattern LayoutPattern
        {
            get { return _LayoutPattern; }
            set { _LayoutPattern = value; }
        }

        public WindowsContainer()
        {
            InitializeComponent();
            InitialiseChildWindows();
        }

        private void InitialiseChildWindows()
        {
            foreach (UIElement element in LayoutRoot.Children)
            {
                if (element is ChildWindow)
                {
                    ChildWindow window = element as ChildWindow;

                    window.ModeChanged -= new ModeChangedEventHandler(window_ModeChanged);
                    window.ModeChanged += new ModeChangedEventHandler(window_ModeChanged);

                    window.Title.Caption.Text = window.Name;

                    window.StandardRect = new Rect(
                        (double)window.GetValue(Canvas.LeftProperty),
                        (double)window.GetValue(Canvas.TopProperty),
                        window.Width, 
                        window.Height);

                    _ListWindows.Add(window);
                }
            }
        }

        private ChildWindow _DetailedChildWindow = null;

        void window_ModeChanged(object sender, ModeChangedEventArgs e)
        {
            if (sender is ChildWindow)
            {
                if ((sender as ChildWindow).Mode == WindowLayoutMode.Detailed)
                {
                    foreach (ChildWindow window in _ListWindows)
                    {
                        if (sender == window)
                        {
                            continue;
                        }

                        window.Mode = WindowLayoutMode.Tiled;
                    }
                    ArrangeWindows((ChildWindow)sender, WindowLayoutMode.Detailed);
                    _DetailedChildWindow = sender as ChildWindow;
                    _Mode = WindowLayoutMode.Detailed;
                }
                else
                {
                    _DetailedChildWindow = null;
                    foreach (ChildWindow window in _ListWindows)
                    {
                        window.Mode = WindowLayoutMode.Tiled;
                    }
                    ArrangeWindows((ChildWindow)sender, WindowLayoutMode.Tiled);
                    _Mode = WindowLayoutMode.Tiled;
                }
            }
        }

        private void TileWindows(double left, double top, double width, double height, double margin, double childAspectRatio)
        {
            // Get the no of child windows to tile:
            int windowsCount = _ListWindows.Where(s => s.Mode == WindowLayoutMode.Tiled).Count();
            if (windowsCount == 0) throw new InvalidOperationException("No child windows to tile");

            // Now calculate the optimum size for each window:
            double parentAspectRatio = width / height;// this.Width / this.Height;

            if (childAspectRatio <= 0)
            {
                double childHeight = height / windowsCount;
                childAspectRatio = width / childHeight;
            }

            double sqrtN = Math.Sqrt((double)windowsCount);
            double sqrtAspectRatioRatio = Math.Sqrt(parentAspectRatio / childAspectRatio);
            double childWindowHeight = height * sqrtAspectRatioRatio / sqrtN;
            double childWindowWidth = childAspectRatio * childWindowHeight;
            double rowCountDouble = height / childWindowHeight;
            double columnCountDouble = width / childWindowWidth;

            int columnCount = (int)columnCountDouble;
            double colDiscrepancy = (columnCountDouble - columnCount) / columnCountDouble;
            int rowCount = (int)rowCountDouble;
            double rowDiscrepancy = (rowCountDouble - rowCount) / rowCountDouble;

            if (colDiscrepancy > rowDiscrepancy)
            {
                //shrink column width
                columnCount++;
                childWindowWidth = width / columnCount;
                childWindowHeight = childWindowWidth / childAspectRatio;

                if (columnCount * rowCount < windowsCount)
                {
                    //shrink row height
                    rowCount++;
                    childWindowHeight = height / rowCount;
                    childWindowWidth = childWindowHeight * childAspectRatio;
                }
            }
            else if (rowDiscrepancy > colDiscrepancy)
            {
                //shrink row height
                rowCount++;
                childWindowHeight = height / rowCount;
                childWindowWidth = childWindowHeight * childAspectRatio;
                if (columnCount * rowCount < windowsCount)
                {
                    //shrink column width
                    columnCount++;
                    childWindowWidth = width / columnCount;
                    childWindowHeight = childWindowWidth / childAspectRatio;
                }
            }

            int index = 0;
            int indexRow = 0;
            int indexColumn = 0;
            List<Storyboard> listStoryboard = new List<Storyboard>();
            foreach (ChildWindow window in _ListWindows)
            {
                if (window.Mode == WindowLayoutMode.Tiled)
                {
                    indexColumn = index % columnCount;
                    indexRow = (int)(index / columnCount);

                    Rect targetRect = new Rect(
                        left + indexColumn * childWindowWidth + margin,
                        top + indexRow * childWindowHeight + margin,
                        childWindowWidth - 2 * margin,
                        childWindowHeight - 2 * margin);
                    listStoryboard.Add(GetStoryboard(window, targetRect));

                    window.SetVerticalTitle(targetRect.Width <= window.Title.Height + 15);

                    index++;
                }
                else
                {
                    window.SetVerticalTitle(false);
                }

            }
            foreach (Storyboard sb in listStoryboard)
            {
                LayoutRoot.Resources.Add(sb.Name, sb);

                sb.Begin(LayoutRoot);

                LayoutRoot.Resources.Remove(sb.Name);
            }
        }

        private void ArrangeWindows(ChildWindow sender, WindowLayoutMode mode)
        {
            int windowsCount = _ListWindows.Count;

            if (mode == WindowLayoutMode.Tiled)
            {
                if (LayoutPattern == WindowLayoutPattern.EqualWeighting)
                {
                    TileWindows(0D, 0D, RenderSize.Width, RenderSize.Height, ChildMargin, 5D / 3D);
                }
                else
                {
                    List<Storyboard> listStoryboard = new List<Storyboard>();
                    foreach (ChildWindow window in _ListWindows)
                    {
                        listStoryboard.Add(GetStoryboard(window, window.StandardRect));

                        window.SetVerticalTitle(window.StandardRect.Width <= window.Title.Height + 15);
                    }
                    foreach (Storyboard sb in listStoryboard)
                    {
                        LayoutRoot.Resources.Add(sb.Name, sb);

                        sb.Begin(LayoutRoot);

                        LayoutRoot.Resources.Remove(sb.Name);
                    }
                }
            }
            else
            {
                // If the existing mode is already detailed, then just swap the two windows:
                if (_Mode == mode && _DetailedChildWindow != null && sender != null)
                {
                    SwapWindow(sender as ChildWindow, _DetailedChildWindow);
                    _DetailedChildWindow.Title.TitleButton.Mode = WindowLayoutMode.Tiled;
                }
                else
                {
                    if (sender != null)
                    {
                        Rect targetRect = new Rect(0.0D + ChildMargin, 0.0D + ChildMargin, this.RenderSize.Width - SidePanelWidth - 2 * ChildMargin, this.RenderSize.Height - 2 * ChildMargin);

                        Storyboard sb = GetStoryboard(sender, targetRect);
                        LayoutRoot.Resources.Add(sb.Name, sb);
                        sb.Begin(LayoutRoot);
                        LayoutRoot.Resources.Remove(sb.Name);
                    }

                    TileWindows(RenderSize.Width - SidePanelWidth, 0, SidePanelWidth, RenderSize.Height, ChildMargin, 0);
                }
            }
        }

        private double _SidePanelWidth = 35.00D;

        public double SidePanelWidth
        {
            get { return _SidePanelWidth; }
            set { _SidePanelWidth = value; }
        }

        private void SwapWindow(ChildWindow childWindow, ChildWindow _DetailedChildWindow)
        {
            Storyboard sb1 = GetStoryboard(childWindow, _DetailedChildWindow);
            Storyboard sb2 = GetStoryboard(_DetailedChildWindow, childWindow);

            childWindow.SetVerticalTitle(_DetailedChildWindow.Width <= _DetailedChildWindow.Title.Height + 15);
            _DetailedChildWindow.SetVerticalTitle(childWindow.Width <= childWindow.Title.Height + 15);

            sb1.Begin(LayoutRoot);
            sb2.Begin(LayoutRoot);
        }

        private Storyboard GetStoryboard(ChildWindow from, Rect rect)
        {
            return GetStoryboard(from, rect, 400);
        }

        private Storyboard GetStoryboard(ChildWindow from, Rect rect, double duration)
        {
            Storyboard sb1 = new Storyboard();
            sb1.Name = "test";
            sb1.Duration = new Duration(TimeSpan.FromMilliseconds(duration));
            DoubleAnimation da1 = new DoubleAnimation((double)from.GetValue(Canvas.LeftProperty), rect.Left, sb1.Duration);
            DoubleAnimation da2 = new DoubleAnimation((double)from.GetValue(Canvas.TopProperty), rect.Top, sb1.Duration);
            DoubleAnimation da3 = new DoubleAnimation(from.Width, rect.Width, sb1.Duration);
            DoubleAnimation da4 = new DoubleAnimation(from.Height, rect.Height, sb1.Duration);

            Storyboard.SetTarget(da1, from);
            Storyboard.SetTargetProperty(da1, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTarget(da2, from);
            Storyboard.SetTargetProperty(da2, new PropertyPath("(Canvas.Top)"));
            Storyboard.SetTarget(da3, from);
            Storyboard.SetTargetProperty(da3, new PropertyPath("(Width)"));
            Storyboard.SetTarget(da4, from);
            Storyboard.SetTargetProperty(da4, new PropertyPath("(Height)"));

            sb1.Children.Add(da1);
            sb1.Children.Add(da2);
            sb1.Children.Add(da3);
            sb1.Children.Add(da4);

            return sb1;
        }

        private Storyboard GetStoryboard(ChildWindow from, ChildWindow to)
        {
            return GetStoryboard(from, to, 400);
        }

        private Storyboard GetStoryboard(ChildWindow from, ChildWindow to, double duration)
        {
            Storyboard sb1 = new Storyboard();
            sb1.Duration = new Duration(TimeSpan.FromMilliseconds(duration));
            DoubleAnimation da1 = new DoubleAnimation((double)from.GetValue(Canvas.LeftProperty), (double)to.GetValue(Canvas.LeftProperty), sb1.Duration);
            DoubleAnimation da2 = new DoubleAnimation((double)from.GetValue(Canvas.TopProperty), (double)to.GetValue(Canvas.TopProperty), sb1.Duration);
            DoubleAnimation da3 = new DoubleAnimation(from.Width, to.Width, sb1.Duration);
            DoubleAnimation da4 = new DoubleAnimation(from.Height, to.Height, sb1.Duration);
            
            Storyboard.SetTarget(da1, from);
            Storyboard.SetTargetProperty(da1, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTarget(da2, from);
            Storyboard.SetTargetProperty(da2, new PropertyPath("(Canvas.Top)"));
            Storyboard.SetTarget(da3, from);
            Storyboard.SetTargetProperty(da3, new PropertyPath("(Width)"));
            Storyboard.SetTarget(da4, from);
            Storyboard.SetTargetProperty(da4, new PropertyPath("(Height)"));

            sb1.Children.Add(da1);
            sb1.Children.Add(da2);
            sb1.Children.Add(da3);
            sb1.Children.Add(da4);

            return sb1;
        }

        private Size _OldSize = Size.Empty;

        public void Resize(Size newSize)
        {
            if (!_OldSize.IsEmpty)
            {
                List<Storyboard> listStoryboard = new List<Storyboard>();
                
                // All child windows size will be affected depending on their standard sizes
                // calculate proportions:
                double widthRatio =  newSize.Width/ _OldSize.Width;
                double heightRatio = newSize.Height/_OldSize.Height;

                foreach (ChildWindow window in _ListWindows)
                {
                    window.StandardRect = new Rect(
                    window.StandardRect.Left * widthRatio,
                    window.StandardRect.Top * heightRatio,
                    window.StandardRect.Width * widthRatio,
                    window.StandardRect.Height * heightRatio
                    );

                    if (_Mode == WindowLayoutMode.Tiled)
                    {
                        listStoryboard.Add(GetStoryboard(window, window.StandardRect, 10));
                    }
                    else if (_Mode == WindowLayoutMode.Detailed)
                    {
                        Rect rect = new Rect(
                            (double)window.GetValue(Canvas.LeftProperty) * widthRatio,
                            (double)window.GetValue(Canvas.TopProperty) * heightRatio,
                            window.Width * widthRatio,
                            window.Height * heightRatio
                            );

                        listStoryboard.Add(GetStoryboard(window, rect, 10));
                    }
                }

                foreach (Storyboard sb in listStoryboard)
                {
                    sb.Begin(LayoutRoot);
                }
            }

            _OldSize = newSize;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size size = e.NewSize;
        }
    }
}
