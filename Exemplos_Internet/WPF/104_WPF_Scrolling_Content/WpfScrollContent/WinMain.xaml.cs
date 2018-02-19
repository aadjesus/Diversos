
namespace WpfScrollContent
{
    #region Using Statements
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    using FlickLib;
    using GestureLib;
    #endregion

    /// <summary>
    /// Interaction logic for WinMain.xaml
    /// </summary>
    public partial class WinMain : Window
    {
        #region Constants
        private const int ANIM_SCROLL_AMOUNT = 400;
        #endregion

        #region Members
        private List<Image> _imageList = new List<Image>();
        private double _dbMousePosition = -1;
        private ScrollAnimation _animScroll = new ScrollAnimation();
        private int NTR_WM_GESTURE = 0;
        private Gestures _myGestures = new Gestures();
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public WinMain()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(WinMain_Loaded);
            this.Closing += new CancelEventHandler(WinMain_Closing);

            // Get images from Sample Pictures directory
            // If you don't give it a directory with images in it, then there 
            // won't be anything to display
            this._imageList = GetImageList(@"C:\Users\Public\Pictures\Sample Pictures");

            // Create StackPanel and set child elements to horizontal orientation
            StackPanel imgStackPnl = new StackPanel();
            imgStackPnl.HorizontalAlignment = HorizontalAlignment.Left;
            imgStackPnl.VerticalAlignment = VerticalAlignment.Top;
            imgStackPnl.Orientation = Orientation.Horizontal;

            // Iterate through each image and add to StackPanel
            foreach (Image curImage in this._imageList)
            {
                curImage.MouseDown += new MouseButtonEventHandler(CurImage_MouseDown);
                curImage.MouseUp += new MouseButtonEventHandler(CurImage_MouseUp);
                imgStackPnl.Children.Add(curImage);
            }

            // Set content of ScrollViewer to StackPanel
            this.myScrollViewer.Content = imgStackPnl;
            this.myScrollViewer.MouseMove += new MouseEventHandler(MyScrollViewer_MouseMove);

            // Hook the update animation callback event
            this._animScroll.UpdateAnimation += new UpdateAnimationHandler(AnimScroll_UpdateAnimation);
        }
        #endregion

        #region MyScrollViewer_MouseMove Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mouseEventArgs"></param>
        private void MyScrollViewer_MouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (this._dbMousePosition > 0)
            {
                double dbX = mouseEventArgs.GetPosition(this.myScrollViewer).X;
                double dbDelta = dbX - this._dbMousePosition;
                double dbCurOffset = this.myScrollViewer.ContentHorizontalOffset;

                double dbNew = dbCurOffset - dbDelta;
                this.myScrollViewer.ScrollToHorizontalOffset(dbNew);

                this._dbMousePosition = dbX;
            }
        }
        #endregion

        #region CurImage_MouseDown Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mouseButtonEventArgs"></param>
        private void CurImage_MouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            this._dbMousePosition = mouseButtonEventArgs.GetPosition(this.myScrollViewer).X;
        }
        #endregion

        #region CurImage_MouseUp Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mouseButtonEventArgs"></param>
        private void CurImage_MouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            this._dbMousePosition = -1;
        }
        #endregion

        #region GetImageList Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private List<Image> GetImageList(string strPath)
        {
            List<Image> imageList = new List<Image>();
            string strFilePath = "";

            if (Directory.Exists(strPath) == false)
            {
                MessageBox.Show(string.Format("{0} path could not be found.", strPath));
                return imageList;
            }

            try
            {
                // supported image files
                List<string> formatList = new List<string>();
                formatList.Add(".jpg");
                formatList.Add(".png");
                formatList.Add(".bmp");
                formatList.Add(".gif");
                formatList.Add(".tif");

                DirectoryInfo dirInfo = new DirectoryInfo(strPath);
                FileInfo[] files = dirInfo.GetFiles();

                foreach (FileInfo curFile in files)
                {
                    // only look for image files 
                    if (formatList.Contains(curFile.Extension.ToLower()) == false)
                        continue;

                    strFilePath = curFile.FullName;

                    Image curImage = new Image();
                    BitmapImage bmpImage = new BitmapImage();
                    bmpImage.BeginInit();
                    bmpImage.UriSource = new Uri(curFile.FullName, UriKind.Absolute);
                    bmpImage.EndInit();

                    curImage.Height = 140;
                    curImage.Stretch = Stretch.Fill;

                    curImage.Source = bmpImage;
                    curImage.Margin = new Thickness(10);

                    imageList.Add(curImage);
                }

                if (imageList.Count == 0)
                    MessageBox.Show(string.Format("No image files could be found in {0}", strPath));
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}-{1}", ex.Message, strFilePath));
            }

            return imageList;
        } // end of GetImageList method
        #endregion

        #region WinMain_Loaded Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void WinMain_Loaded(object sender, RoutedEventArgs routedEventArgs)
        {
            WindowInteropHelper winHelper = new WindowInteropHelper(this);
            try
            {
                HwndSource hwnd = HwndSource.FromHwnd(winHelper.Handle);
                hwnd.AddHook(new HwndSourceHook(this.MessageProc));

                bool bIsConnected = this._myGestures.Connect();
                if (bIsConnected == true)
                {
                    TNtrGestures regGestures = new TNtrGestures();
                    regGestures.ReceiveRotate = true;
                    regGestures.UseUserRotateSettings = true;
                    regGestures.ReceiveFingersDoubleTap = true;
                    regGestures.UseUserFingersDoubleTapSettings = true;
                    regGestures.ReceiveZoom = true;
                    regGestures.UseUserZoomSettings = true;
                    regGestures.ReceiveScroll = true;
                    regGestures.UseUserScrollSettings = true;

                    bool bRegister = this._myGestures.RegisterGestures(winHelper.Handle, regGestures);
                    if (bRegister == true)
                    {
                        this.NTR_WM_GESTURE = Gestures.RegisterGestureWinMessage();
                    }
                }
                else
                {
                    MessageBox.Show("Error connecting to DuoSense hardware.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region WinMain_Closing Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cancelEventArgs"></param>
        private void WinMain_Closing(object sender, CancelEventArgs cancelEventArgs)
        {
            this._myGestures.Disconnect();
        }
        #endregion

        #region MessageProc Method
        /// <summary>
        /// This method is the WPF equivalent to win32 WndProc.  The app receives all the 
        /// Windows messages.  The app can then recognize and respond to the flick or gesture.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nMsgID"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="bHandled"></param>
        /// <returns></returns>
        private IntPtr MessageProc(IntPtr hwnd, int nMsgID, IntPtr wParam, IntPtr lParam, ref bool bHandled)
        {
            if (nMsgID == Flicks.WM_TABLET_FLICK)
            {
                Flick flick = Flicks.ProcessMessage(lParam, wParam);
                if (flick != null)
                {
                    ProcessFlick(flick);
                    bHandled = true;
                }
            }

            if (nMsgID == NTR_WM_GESTURE)
            {
                GestureData gData = this._myGestures.ProcessMessage(lParam, wParam);
                if (gData != null)
                {
                    ProcessGesture(gData);
                    bHandled = true;
                }
            }

            return IntPtr.Zero;
        }
        #endregion

        #region ScrollContentLeft Method
        /// <summary>
        /// This method sets all the values on the ScrollAnimation object
        /// and starts the animation so that the content will scroll left.
        /// </summary>
        private void ScrollContentLeft()
        {
            double dbCurOffset = this.myScrollViewer.ContentHorizontalOffset;

            this._animScroll.From = dbCurOffset;
            this._animScroll.To = dbCurOffset - ANIM_SCROLL_AMOUNT;
            this._animScroll.Duration = TimeSpan.FromSeconds(1);
            this._animScroll.Start();
        }
        #endregion

        #region ScrollContentRight Method
        /// <summary>
        /// This method sets all the values on the ScrollAnimation object
        /// and starts the animation so that the content will scroll right.
        /// </summary>
        private void ScrollContentRight()
        {
            double dbCurOffset = this.myScrollViewer.ContentHorizontalOffset;

            this._animScroll.From = dbCurOffset;
            this._animScroll.To = dbCurOffset + ANIM_SCROLL_AMOUNT;
            this._animScroll.Duration = TimeSpan.FromSeconds(1);
            this._animScroll.Start();
        }
        #endregion

        #region AnimScroll_UpdateAnimation Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objSender"></param>
        /// <param name="animationArgs"></param>
        private void AnimScroll_UpdateAnimation(object objSender, AnimationEventArgs animationArgs)
        {
            if (this.Dispatcher == System.Windows.Threading.Dispatcher.CurrentDispatcher)
            {
                this.myScrollViewer.ScrollToHorizontalOffset(animationArgs.Value);
            }
            else
            {
                this.Dispatcher.BeginInvoke(new UpdateAnimationHandler(AnimScroll_UpdateAnimation), new object[] { objSender, animationArgs });
            }
        }
        #endregion

        #region ProcessFlick Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flick"></param>
        private void ProcessFlick(Flick flick)
        {
            Object objTest;
            Point ptTest;

            // Scroll content left if the left flick was on top of the scroll viewer
            if (flick.Data.Direction == FLICKDIRECTION.FLICKDIRECTION_LEFT)
            {
                ptTest = this.myScrollViewer.PointFromScreen(new Point(flick.Point.X, flick.Point.Y));
                objTest = this.myScrollViewer.InputHitTest(ptTest);
                if (objTest != null)
                    ScrollContentRight();
            }

            // Scroll content left if the right flick was on top of the scroll viewer
            if (flick.Data.Direction == FLICKDIRECTION.FLICKDIRECTION_RIGHT)
            {
                ptTest = this.myScrollViewer.PointFromScreen(new Point(flick.Point.X, flick.Point.Y));
                objTest = this.myScrollViewer.InputHitTest(ptTest);
                if (objTest != null)
                    ScrollContentLeft();
            }

            // If an up flick happened over an image, then put image on the main image control
            if (flick.Data.Direction == FLICKDIRECTION.FLICKDIRECTION_UP)
            {
                ptTest = this.myScrollViewer.PointFromScreen(new Point(flick.Point.X, flick.Point.Y));
                objTest = this.myScrollViewer.InputHitTest(ptTest);
                if (objTest != null)
                {
                    if (objTest is Image)
                    {
                        Image hitImage = (Image)objTest;
                        this.imgMain.Source = hitImage.Source;
                    }
                }
            }
        } // end of ProcessFlick method
        #endregion

        #region ProcessGesture Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gData"></param>
        private void ProcessGesture(GestureData gData)
        {
            double dbTop = this.imgMain.Margin.Top;
            switch (gData.GestureType)
            {
                case ENtrGestureType.Zoom:
                    GestureZoomData gzData = (GestureZoomData)gData;
                    if (gzData.ZoomStruct.mAmount < 0)
                    {
                        dbTop += 50;
                    }
                    else
                    {
                        dbTop -= 50;
                    }
                    this.imgMain.Margin = new Thickness(dbTop);
                    this.InvalidateVisual();
                    break;

                case ENtrGestureType.Rotate:
                    GestureRotateData rData = (GestureRotateData)gData;
                    if (rData.RotateStruct.mAmount < 0)
                    {
                    }
                    else
                    {
                    }
                    break;
            }
        }
        #endregion
    } // end of class
} // end of namespace