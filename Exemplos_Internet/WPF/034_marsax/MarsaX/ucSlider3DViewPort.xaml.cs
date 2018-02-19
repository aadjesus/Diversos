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
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace MarsaX
{
    /// <summary>
    /// Available search types
    /// </summary>
    public enum SearchTypes { FlickrLatest, FlickrInteresting, FlickrKey };

    //the event handler delegate
    public delegate void ModelSelectedEventHandler(object sender, ModelSelectedEventArgs e);


    /// <summary>
    /// Hosts a single ContainerUIElement3D which is used to hold numerous
    /// ModelUIElement3D. Where a new ModelUIElement3D is created with an image
    /// that matches the results of a XLINQ query to a Flickr RSS feed.
    /// The user is able to pan/zoom the ContainerUIElement3D and is also able
    /// to click on one of the ModelUIElement3Ds to view the associated image
    /// in a popup window.
    /// </summary>
    public partial class ucslideImages3DViewPort : UserControl
    {
        #region Data
        public bool Focused { get; set; }
        private Dictionary<ModelUIElement3D, string> modelToImageLookUp = new Dictionary<ModelUIElement3D, string>();
        private const double MODEL_OFFSET = 1.05;
        private DispatcherTimer angleAnimationTimer = new DispatcherTimer();
        private DispatcherTimer loadTimer = new DispatcherTimer();
        private SearchTypes currentSearchtype = SearchTypes.FlickrLatest;
        #endregion

        #region Events


        //The actual event routing
        public static readonly RoutedEvent ModelSelectedEvent =
            EventManager.RegisterRoutedEvent(
            " ModelSelected", RoutingStrategy.Bubble,
            typeof(ModelSelectedEventHandler),
            typeof(ucslideImages3DViewPort));

        //add remove handlers
        public event ModelSelectedEventHandler ModelSelected
        {
            add { AddHandler(ModelSelectedEvent, value); }
            remove { RemoveHandler(ModelSelectedEvent, value); }
        }
        #endregion

        #region Ctor
        public ucslideImages3DViewPort()
        {
            InitializeComponent();
            IsSearchAreaShown = false;
            this.Loaded += ucslideImages3DViewPort_Loaded;
            loadTimer.Interval = TimeSpan.FromSeconds(30);
            loadTimer.IsEnabled = false;
            loadTimer.Tick += loadTimer_Tick;
        }
        #endregion

        #region Private Methods

        #region Search Related Methods

        /// <summary>
        /// On load simply start with a SearchTypes.FlickrLatest 
        /// <see cref="SearchTypes">SearchTypes</see>
        /// </summary>
        private void ucslideImages3DViewPort_Loaded(object sender, RoutedEventArgs e)
        {
            SearchButtonProps.SetIsCurrentlySelected(btnLatest, true);
            ConductSearch(string.Empty, SearchTypes.FlickrLatest);
        }

        /// <summary>
        /// A delay load timer, to allow some time for hrttp based images to be retrieved.
        /// After the load timer elapses, allow the imageResults control to be shown
        /// </summary>
        private void loadTimer_Tick(object sender, EventArgs e)
        {
            ucLoader.Visibility = Visibility.Hidden;
            controlsArea.Visibility = Visibility.Visible;
            IsAnimating = false;
            IsVisible = true;
            loadTimer.IsEnabled = false;
        }

        /// <summary>
        /// Stores the newSearchType search request in an internal 
        /// field and proceeds to call the GetQueryResults on a background thread
        /// </summary>
        private void ConductSearch(string keyword, SearchTypes newSearchType)
        {
            currentSearchtype = newSearchType;
            loadTimer.IsEnabled = true;
            controlsArea.Visibility = Visibility.Collapsed;
            ucLoader.Visibility = Visibility.Visible;
            IsAnimating = true;
            IsVisible = false;

            //Load the images async, but assume that the loadTimer time will
            //be enough to cover how long it will take to fetch and display
            //all the images
            ThreadPool.QueueUserWorkItem(x =>
            {
                var data = GetQueryResults(keyword);

                //Create 3D models for the images 
                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    ((Action)delegate { CreateModelsForImages(data); }));
            });
        }

        /// <summary>
        /// returns a List<see cref="PhotoInfo">PhotoInfo</see>
        /// which match the current search query based on the value
        /// of internal search request
        /// </summary>
        private List<PhotoInfo> GetQueryResults(string keyword)
        {
            switch (currentSearchtype)
            {
                case SearchTypes.FlickrLatest:
                    return FlickerProvider.LoadLatestPictures();
                case SearchTypes.FlickrInteresting:
                    return FlickerProvider.LoadInterestingPictures();
                case SearchTypes.FlickrKey:
                    return FlickerProvider.LoadPicturesKey(keyword);
                default:
                    return FlickerProvider.LoadLatestPictures();
            }
        }


        /// <summary>
        /// Conducts a search using the key word, when the user presses enter
        /// </summary>
        private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {

                foreach (Button btnChild in spSearchButtons.Children)
                {
                    SearchButtonProps.SetIsCurrentlySelected(btnChild, false);
                }

                //now set the selected one, so that the current selection gets a 
                //warm green glow applied
                SearchButtonProps.SetIsCurrentlySelected(btnKeyWord, true);


                if (txtKeyWord.Text == string.Empty)
                    MessageBox.Show("You need to enter a keyword", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    ConductSearch(txtKeyWord.Text, SearchTypes.FlickrKey);


                //Hide the search area
                HideSearchArea();
            }
        }


        /// <summary>
        /// Sets SearchButtonProps.SetIsCurrentlySelected property for current
        /// clicked button, and also calls the ConductSearch() method passing
        /// in the required parameters that match the requested search
        /// </summary>
        private void SearchAreaButton_Click(object sender, RoutedEventArgs e)
        {
            Button btnOriginal = e.OriginalSource as Button;
            if (btnOriginal != null)
            {
                foreach (Button btnChild in spSearchButtons.Children)
                {
                    SearchButtonProps.SetIsCurrentlySelected(btnChild, false);
                }
                //now set the selected one, so that the current selection gets a 
                //warm green glow applied
                SearchButtonProps.SetIsCurrentlySelected(btnOriginal, true);

                //work out what search to do
                if (btnOriginal == btnLatest)
                {
                    ConductSearch(string.Empty, SearchTypes.FlickrLatest);
                }
                if (btnOriginal == btnInteresting)
                {
                    ConductSearch(string.Empty, SearchTypes.FlickrInteresting);
                }
                if (btnOriginal == btnKeyWord)
                {
                    if (txtKeyWord.Text == string.Empty)
                        MessageBox.Show("You need to enter a keyword", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                        ConductSearch(txtKeyWord.Text, SearchTypes.FlickrKey);
                }

                //Hide the search area
                HideSearchArea();
            }
        }
        #endregion

        #region Search Area Animations

        /// <summary>
        /// If the SearchArea is not currently shown, Animates the SearchArea 
        /// to be shown on screen
        /// </summary>
        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            if (!IsSearchAreaShown)
            {
                IsSearchAreaShown = true;
                Storyboard HideSearchArea = 
                    this.TryFindResource("OnShowSearchArea") as Storyboard;
                if (HideSearchArea != null)
                    HideSearchArea.Begin(SearchArea);
            }
        }

        /// <summary>
        /// If the SearchArea is currently shown, Animates the SearchArea 
        /// to be hidden off screen
        /// </summary>
        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            if (IsSearchAreaShown)
            {
                HideSearchArea();
            }
        }

        /// <summary>
        /// Animates the SearchArea to be hidden off screen
        /// </summary>
        private void HideSearchArea()
        {
            IsSearchAreaShown = false;
            Storyboard HideSearchArea = 
                this.TryFindResource("OnHideSearchArea") as Storyboard;
            if (HideSearchArea != null)
                HideSearchArea.Begin(SearchArea);
        }

        #endregion

        #region 3D Model Creation
        /// <summary>
        /// Creates a new ModelUIElement3D for each of the <See cref="PhotoInfo">PhotoInfo</See>
        /// within the input List<See cref="PhotoInfo">PhotoInfo</See>.
        /// </summary>
        private void CreateModelsForImages(List<PhotoInfo> photos)
        {
            int photoNum = 0;
            IsVisible = false;
            container.Children.Clear();
            modelToImageLookUp.Clear();

            for (int rows = 0; rows < Constants.ROWS; rows++)
            {
                for (int col = 0; col < Constants.COLUMNS; col++)
                {
                    if (photoNum < photos.Count)
                    {
                        container.Children.Add(CreateModel(photos[photoNum].ImageUrl, rows, col));
                        photoNum++;
                    }
                }   
            }
        }



        /// <summary>
        /// Creates a new ModelUIElement3D child which is added to the 
        /// ContainerUIElement3Ds Children. The row/col parameters are used to position the
        /// ModelUIElement3D is 3D space, whilst the imageUri is used to create an Image
        /// for the new ModelUIElement3D child
        /// </summary>
        private ModelUIElement3D CreateModel(string imageUri, int row, int col)
        {
            //Get a VisualBrush for the Url
            VisualBrush vBrush = GetVisualBrush(imageUri);

            //Create the model
            ModelUIElement3D model3D = new ModelUIElement3D
            {
                Model = new GeometryModel3D
                {
                    Geometry = new MeshGeometry3D
                    {
                        TriangleIndices = new Int32Collection(
                            new int[] { 0, 1, 2, 2, 3, 0 }),
                        TextureCoordinates = new PointCollection(
                            new Point[] 
                            { 
                                new Point(0, 1), 
                                new Point(1, 1), 
                                new Point(1, 0), 
                                new Point(0, 0) 
                            }),
                        Positions = new Point3DCollection(
                            new Point3D[] 
                            { 
                                new Point3D(-0.5, -0.5, 0), 
                                new Point3D(0.5, -0.5, 0), 
                                new Point3D(0.5, 0.5, 0), 
                                new Point3D(-0.5, 0.5, 0) 
                            })
                    },
                    Material = new DiffuseMaterial
                    {
                        Brush = vBrush
                    },
                    BackMaterial = new DiffuseMaterial
                    {
                        Brush = Brushes.Black
                    },
                    Transform = CreateGroup(row, col)
                }
            };
            //hook up mouse events, and add to lookup and return the ModelUIElement3D
            model3D.MouseEnter += ModelUIElement3D_MouseEnter;
            model3D.MouseDown += model3D_MouseDown;
            modelToImageLookUp.Add(model3D, imageUri);
            return model3D;
        }


        /// <summary>
        /// Creates a Border with an Image where the Image.Source is the url
        /// input paarmeter. This is then made into a VisualBrush which is
        /// retuned for use within a ModelUIElement3D
        /// </summary>
        private VisualBrush GetVisualBrush(string url)
        {
            Border bord = new Border();
            bord.Width = 15;
            bord.Height = 15;
            bord.CornerRadius = new CornerRadius(0);
            bord.BorderThickness = new Thickness(0.5);
            bord.BorderBrush = Brushes.WhiteSmoke;
            try
            {
                Image img = new Image
                {
                    Source = new BitmapImage(new Uri(@url, UriKind.RelativeOrAbsolute)),
                    Stretch = Constants.stretchImagesFor3DModels ? Stretch.Fill : Stretch.Uniform,
                    Margin = new Thickness(0)

                };

                bord.Child = img;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message, "ERROR");
            }

            VisualBrush vBrush = new VisualBrush(bord);
            return vBrush;
        }


        /// <summary>
        /// Creates a Transform3DGroup for the current ModelUIElement3D
        /// </summary>
        private Transform3DGroup CreateGroup(int row, int col)
        {
            Transform3DGroup group = new Transform3DGroup();
            group.Children.Add(new TranslateTransform3D
            {
                OffsetX = col * MODEL_OFFSET,
                OffsetY = row * -MODEL_OFFSET,
                OffsetZ = 0.0
            });
            group.Children.Add(new RotateTransform3D
            {
                Rotation = new AxisAngleRotation3D
                {
                    Angle = 0,
                    Axis = new Vector3D(1, 0, 0)
                }
            });
            group.Children.Add(new ScaleTransform3D
            {
                ScaleX =1.0,
                ScaleY = 1.0,
                ScaleZ = 1.0
            });
            return group;
        }


        /// <summary>
        /// Sets the CurrentImage property to be the Url of the image
        /// associated with the current ModelUIElement3D that was clicked
        /// </summary>
        private void model3D_MouseDown(object sender, MouseButtonEventArgs e)
        {


            //raise our custom CustomClickWithCustomArgs event
            ModelSelectedEventArgs args = new ModelSelectedEventArgs(ModelSelectedEvent, CurrentImage);
            RaiseEvent(args);

        }


        /// <summary>
        /// Does a xisAngleRotation3D.AngleProperty rotation for the current
        /// ModelUIElement3D on MouseEnter
        /// </summary>
        private void ModelUIElement3D_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Constants.should3DModelFlipOnMouseOver)
            {
                ModelUIElement3D modelUIElement3D = sender as ModelUIElement3D;
                Transform3DGroup group = modelUIElement3D.Model.Transform as Transform3DGroup;
                TranslateTransform3D trans = group.Children[0] as TranslateTransform3D;
                RotateTransform3D rotate = group.Children[1] as RotateTransform3D;
                AxisAngleRotation3D axis = rotate.Rotation as AxisAngleRotation3D;
                double rotateValue = axis.Angle == 360 ? 0 : 360;
                DoubleAnimation daRotate = new DoubleAnimation(rotateValue, new Duration(new TimeSpan(0, 0, 1)));
                axis.BeginAnimation(AxisAngleRotation3D.AngleProperty, daRotate);
            }
            CurrentImage = modelToImageLookUp[sender as ModelUIElement3D];
        }





        #endregion

        #region Container/Camera Move/Angle Animations
        /// <summary>
        /// Use bound value of Slider to work out what column to 
        /// animate the ContainerUIElement3D to
        /// </summary>
        private void slideImages_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Animate((int)Math.Round(slideImages.Value));
        }


        /// <summary>
        /// Animates to a partilcular ModelUIElement3D position, uses the col input parameter
        /// to work out how far to move the ContainerUIElement3D which holds all
        /// the ModelUIElement3Ds
        /// </summary>
        private void Animate(int col)
        {
            double move = col * -MODEL_OFFSET;

            Storyboard storyboard = new Storyboard();
            ParallelTimeline timeline = new ParallelTimeline();
            timeline.BeginTime = TimeSpan.FromSeconds(0);
            timeline.Duration = TimeSpan.FromSeconds(2);
            //do move
            DoubleAnimation daMove = new DoubleAnimation(move, new Duration(TimeSpan.FromSeconds(2)));
            daMove.DecelerationRatio = 1.0;
            Storyboard.SetTargetName(daMove, "contTrans");
            Storyboard.SetTargetProperty(daMove, new PropertyPath(TranslateTransform3D.OffsetXProperty));

            //do angle
            double angle = col > Constants.COLUMNS / 2 ? -15 : 15;
            DoubleAnimation daAngle = new DoubleAnimation(angle, new Duration(TimeSpan.FromSeconds(0.8)));
            Storyboard.SetTargetName(daAngle, "contAngle");
            Storyboard.SetTargetProperty(daAngle, new PropertyPath(AxisAngleRotation3D.AngleProperty));

            DoubleAnimation daAngle2 = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(1)));
            daAngle2.BeginTime = daAngle.Duration.TimeSpan;
            Storyboard.SetTargetName(daAngle2, "contAngle");
            Storyboard.SetTargetProperty(daAngle2, new PropertyPath(AxisAngleRotation3D.AngleProperty));

            timeline.Children.Add(daMove);
            timeline.Children.Add(daAngle);
            timeline.Children.Add(daAngle2);

            storyboard.Children.Add(timeline);

            storyboard.Begin(this);
        }

        /// <summary>
        /// Changes the embedded ViewPort3D camera position between 4-10. Simulating a zoom
        /// </summary>
        private void slideZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CameraPosition = e.NewValue;
        }

        /// <summary>
        /// Handle the Mouse wheel to Zoom in and out
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            double value = Math.Max(0, e.Delta / 10);//divide the value by 10 so that it is more smooth
            value = Math.Min(e.Delta, Constants.COLUMNSTOSHOW);
            slideZoom.Value = value;
        }

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// The currently clicked Image
        /// </summary>
        public string CurrentImage { get; private set; }

        /// <summary>
        /// True if there is current an Animation in progress
        /// </summary>
        public bool IsAnimating { get; private set; }


        /// <summary>
        /// True if there teh search area is shown
        /// </summary>
        public bool IsSearchAreaShown { get; private set; }


        /// <summary>
        /// Shows or hides the ViewPort3D
        /// </summary>
        public new bool IsVisible
        {
            set
            {
                if (value)
                    vp.Visibility = Visibility.Visible;
                else
                    vp.Visibility = Visibility.Hidden;
            }
        }


        /// <summary>
        /// The new Camera Z position. When set aninmates the camera position to the new
        /// Z position. This is a simulated Zoom
        /// </summary>
        public double CameraPosition
        {
            set
            {
                Point3D newPosition = new Point3D(camera.Position.X, camera.Position.Y, value);
                Point3DAnimation daZoom = new Point3DAnimation(newPosition, new Duration(TimeSpan.FromSeconds(1)));
                camera.BeginAnimation(PerspectiveCamera.PositionProperty, daZoom);
            }
        }


        #endregion

    }
}
