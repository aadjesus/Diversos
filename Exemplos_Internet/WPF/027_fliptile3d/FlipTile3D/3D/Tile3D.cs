using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace FlipTile3D
{
    /// <summary>
    /// Source code origin Kevin Moore, Kevin's WPF Bag-o-Tricks
    /// http://j832.com/bagotricks/
    /// 
    /// Modified by Sacha Barber for codeproject article
    /// </summary>
    public class Tile3D : WrapperElement<Viewport3D>
    {
        #region Data
        private Point lastMouse = new Point(double.NaN, double.NaN);
        private bool isFlipped;
        private readonly TileData[] tiles = new TileData[xCount * yCount];
        private readonly DiffuseMaterial backMaterial = new DiffuseMaterial();
        private readonly CompositionTargetRenderingListener listener =
            new CompositionTargetRenderingListener();
        private IList<Tuple<DiffuseMaterial, String>> materials;
        private const int xCount = 7, yCount = 7;
        #endregion

        #region Ctor
        public Tile3D() : base(new Viewport3D())
        {
            listener.Rendering += tick;

            this.Unloaded += delegate(object sender, RoutedEventArgs e)
            {
                listener.StopListening();
            };
        }
        #endregion

        #region Private methods

        private void setClickedItem(Point point)
        {
            //move point to center
            point -= (new Vector(this.ActualWidth / 2, this.ActualHeight / 2));

            //flip it
            point.Y *= -1;

            //scale it to 1x1 dimentions
            double scale = Math.Min(this.ActualHeight, this.ActualWidth) / 2;
            point = (Point)(((Vector)point) / scale);

            //set it up so that bottomLeft = 0,0 & topRight = 1,1 (positive Y is up)
            point = (Point)(((Vector)point + new Vector(1, 1)) * .5);

            //scale up so that the point coordinates align w/ the x/y scale
            point.Y *= yCount;
            point.X *= xCount;

            //now we have the indicies of the x & y of the tile clicked
            int yIndex = (int)Math.Floor(point.Y);
            int xIndex = (int)Math.Floor(point.X);

            int tileIndex = yIndex * xCount + xIndex;



            backMaterial.Brush = tiles[tileIndex].diffuseMaterial.Brush;

            TileClickedEventArgs args =
                new TileClickedEventArgs(tiles[tileIndex].disciplesBlogUri, Tile3D.TileClickedEvent);
            RaiseEvent(args);
        }

        private void setup3D()
        {

            //perf improvement. Clipping in 3D is expensive. Avoid if you can!
            WrappedElement.ClipToBounds = false;

            PerspectiveCamera camera = new PerspectiveCamera(
                new Point3D(0, 0, 3.73), //position
                new Vector3D(0, 0, -1), //lookDirection
                new Vector3D(0, 1, 0), //upDirection
                30 //FOV
                );

            WrappedElement.Camera = camera;

            Model3DGroup everything = new Model3DGroup();

            Model3DGroup lights = new Model3DGroup();
            DirectionalLight whiteLight = new DirectionalLight(Colors.White, new Vector3D(0, 0, -1));
            lights.Children.Add(whiteLight);

            everything.Children.Add(lights);

            ModelVisual3D model = new ModelVisual3D();

            double tileSizeX = 2.0 / xCount;
            double startX = -((double)xCount) / 2 * tileSizeX + tileSizeX / 2;
            double startY = -((double)yCount) / 2 * tileSizeX + tileSizeX / 2;

            int index;

            Size tileTextureSize = new Size(1.0 / xCount, 1.0 / yCount);

            //so, tiles are numbers, left-to-right (ascending x), bottom-to-top (ascending y)
            for (int y = 0; y < yCount; y++)
            {
                for (int x = 0; x < xCount; x++)
                {
                    index = y * xCount + x;

                    Rect backTextureCoordinates = new Rect(
                        x * tileTextureSize.Width,

                        // this will give you a headache. Exists since we are going 
                        // from bottom bottomLeft of 3D space (negative Y is down), 
                        // but texture coor are negative Y is up
                        1 - y * tileTextureSize.Height - tileTextureSize.Height,

                        tileTextureSize.Width, tileTextureSize.Height);

                    tiles[index] = new TileData();


                    Tuple<DiffuseMaterial, String> discipleItem = getMaterial(index);

                    tiles[index].Setup3DItem(discipleItem.BlogUrl,
                        everything,
                        discipleItem.Material,
                        new Size(tileSizeX, tileSizeX),
                        new Point(startX + x * tileSizeX, startY + y * tileSizeX),
                        backMaterial,
                        backTextureCoordinates);

                }
            }

            model.Content = everything;

            WrappedElement.Children.Add(model);

            //start the per-frame tick for the physics
            listener.StartListening();
        }

        private void tick(object sender, EventArgs e)
        {
            Vector mouseFixed = fixMouse(lastMouse, this.RenderSize);
            for (int i = 0; i < tiles.Length; i++)
            {
                //TODO: unwire Render event if nothing happens
                tiles[i].TickData(mouseFixed, isFlipped);
            }
        }

        private static Vector fixMouse(Point mouse, Size size)
        {

            double scale = Math.Max(size.Width, size.Height) / 2;

            // Translate y going down to y going up
            mouse.Y = -mouse.Y + size.Height;

            mouse.Y -= size.Height / 2;
            mouse.X -= size.Width / 2;

            Vector v = new Vector(mouse.X, mouse.Y);

            v /= scale;

            return v;
        }

        private Tuple<DiffuseMaterial, String> getMaterial(int index)
        {
            return materials[index % materials.Count];
        }

        private IList<Tuple<DiffuseMaterial, String>> CreateInfosForDisciples()
        {
            IList<Tuple<DiffuseMaterial, String>> materials = new List<Tuple<DiffuseMaterial, String>>();

            foreach (var disciple in DiscipleInfos)
            {

                BitmapImage bitmapImage = new BitmapImage(
                    new Uri(BaseUriHelper.GetBaseUri(OwnerWindow), @disciple.ImageUrl));

                ImageBrush imageBrush = new ImageBrush(bitmapImage);
                imageBrush.Stretch = Stretch.UniformToFill;
                imageBrush.ViewportUnits = BrushMappingMode.Absolute;
                imageBrush.Freeze();

                DiffuseMaterial diffuseMaterial = new DiffuseMaterial(imageBrush);
                materials.Add(new Tuple<DiffuseMaterial, String>(diffuseMaterial, disciple.BlogUrl));
            }
            return materials;
        }
        #endregion

        #region Overrides
        #region Render/layout overrides

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(Brushes.Transparent, null, new Rect(this.RenderSize));
        }

        #endregion

        #region mouse overrides
        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            lastMouse = e.GetPosition(this);
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            lastMouse = new Point(double.NaN, double.NaN);
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            isFlipped = !isFlipped;
            if (isFlipped)
            {
                setClickedItem(e.GetPosition(this));
            }
        }


        #endregion
        #endregion

        #region DPs
        #region OwnerWindow

        /// <summary>
        /// OwnerWindow Dependency Property
        /// </summary>
        public static readonly DependencyProperty OwnerWindowProperty =
            DependencyProperty.Register("OwnerWindow", typeof(DependencyObject), typeof(Tile3D),
                new FrameworkPropertyMetadata((DependencyObject)null));

        /// <summary>
        /// Gets or sets the OwnerWindow property.  
        /// </summary>
        public DependencyObject OwnerWindow
        {
            get { return (DependencyObject)GetValue(OwnerWindowProperty); }
            set { SetValue(OwnerWindowProperty, value); }
        }

        #endregion


        #region DiscipleInfos

        /// <summary>
        /// DiscipleInfos Dependency Property
        /// </summary>
        public static readonly DependencyProperty DiscipleInfosProperty =
            DependencyProperty.Register("DiscipleInfos", typeof(List<DiscipleInfo>), typeof(Tile3D),
                new FrameworkPropertyMetadata((List<DiscipleInfo>)null,
                    new PropertyChangedCallback(OnDiscipleInfosChanged)));

        /// <summary>
        /// Gets or sets the DiscipleInfos property.  
        /// </summary>
        public List<DiscipleInfo> DiscipleInfos
        {
            get { return (List<DiscipleInfo>)GetValue(DiscipleInfosProperty); }
            set { SetValue(DiscipleInfosProperty, value); }
        }

        /// <summary>
        /// Handles changes to the DiscipleInfos property.
        /// </summary>
        private static void OnDiscipleInfosChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Tile3D obj = (Tile3D)d;
            if (e.NewValue != null)
            {
                List<DiscipleInfo> items = (List<DiscipleInfo>)e.NewValue;
                if (items.Count > 0)
                {
                    obj.materials = obj.CreateInfosForDisciples();
                    obj.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                        new Action(obj.setup3D));
                }
            }
        }



        #endregion
        #endregion

        #region Events
        public static readonly RoutedEvent TileClickedEvent =
                EventManager.RegisterRoutedEvent(
                "TileClicked", RoutingStrategy.Bubble,
                typeof(TileClickedEventHandler),
                typeof(Tile3D));


        //add remove handlers
        public event TileClickedEventHandler TileClicked
        {
            add { AddHandler(TileClickedEvent, value); }
            remove { RemoveHandler(TileClickedEvent, value); }
        }
        #endregion
    }
}
