using System;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace BookWriter3D
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Public constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Represents whether the book is open or not.
        /// </summary>
        bool IsBookOpen;

        /// <summary>
        /// Event handler for the Loaded event of the MainWindow
        /// </summary>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CloseBook(0);  // Book starts closed

            // Make book fade in
            DoubleAnimation da = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(2)));
            da.DecelerationRatio = 1;
            _Main3D.BeginAnimation(UIElement.OpacityProperty, da);

        }
        
        /// <summary>
        /// Event handler for the MouseDown event of the cover, back cover, spine and edges
        /// </summary>
        private void Cover_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsBookOpen)
                CloseBook(1.5);
            else
                OpenBook(1.5);
        }

        /// <summary>
        /// Event handler for the MouseDoubleClick event of the TextBoxes
        /// </summary>
        private void Page_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Read page content out loud
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SpeakAsync(((TextBox)sender).Text);
        }

        /// <summary>
        /// Event handler for the PreviewMouseRightButtonDown event of the InkCanvas (right page)
        /// </summary>
        private void InkCanvas_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Switch InkCanvas editing mode
            InkCanvas ic = sender as InkCanvas;
            ic.EditingMode = (ic.EditingMode == InkCanvasEditingMode.Ink) ? InkCanvasEditingMode.EraseByPoint : InkCanvasEditingMode.Ink;
        }

        /// <summary>
        /// Opens the 3D book.
        /// </summary>
        /// <param name="durationSeconds">Time in seconds that the animation will take.</param>
        void OpenBook(double durationSeconds)
        {
            // Transform3D_LeftRotation
            RotateTransform3D rot = (RotateTransform3D)TryFindResource("Transform3D_LeftRotation");
            DoubleAnimation da = new DoubleAnimation(15, new Duration(TimeSpan.FromSeconds(durationSeconds)));
            da.DecelerationRatio = 1;
            rot.Rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);

            // Transform3D_RightRotation
            rot = (RotateTransform3D)TryFindResource("Transform3D_RightRotation");
            da = new DoubleAnimation(-15, new Duration(TimeSpan.FromSeconds(durationSeconds)));
            rot.Rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);

            // Transform3D_SpineRotation
            rot = (RotateTransform3D)TryFindResource("Transform3D_SpineRotation");
            da = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.8333 * durationSeconds)));
            rot.Rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);

            // Transform3D_SpineCoverTranslation
            TranslateTransform3D trans = (TranslateTransform3D)TryFindResource("Transform3D_SpineCoverTranslation");
            da = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.8333 * durationSeconds)));
            trans.BeginAnimation(TranslateTransform3D.OffsetXProperty, da);

            // _Main3D.Camera
            Point3DAnimation pa = new Point3DAnimation(new Point3D(0, -2.5, 6.5), new Duration(TimeSpan.FromSeconds(durationSeconds)));
            pa.AccelerationRatio = 0.5;
            pa.DecelerationRatio = 0.5;
            ((PerspectiveCamera)_Main3D.Camera).BeginAnimation(PerspectiveCamera.PositionProperty, pa);

            // Now the book is open.
            IsBookOpen = true;
        }

        /// <summary>
        /// Closes the 3D book.
        /// </summary>
        /// <param name="durationSeconds">Time in seconds that the animation will take.</param>
        void CloseBook(double durationSeconds)
        {
            // Transform3D_LeftRotation
            RotateTransform3D rot = (RotateTransform3D)TryFindResource("Transform3D_LeftRotation");
            DoubleAnimation da = new DoubleAnimation(180, new Duration(TimeSpan.FromSeconds(durationSeconds)));
            da.DecelerationRatio = 1;
            rot.Rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);

            // Transform3D_RightRotation
            rot = (RotateTransform3D)TryFindResource("Transform3D_RightRotation");
            da = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(durationSeconds)));
            rot.Rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);

            // Transform3D_SpineRotation
            rot = (RotateTransform3D)TryFindResource("Transform3D_SpineRotation");
            da = new DoubleAnimation(90, new Duration(TimeSpan.FromSeconds(0.8333 * durationSeconds)));
            rot.Rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);

            // Transform3D_SpineCoverTranslation
            TranslateTransform3D trans = (TranslateTransform3D)TryFindResource("Transform3D_SpineCoverTranslation");
            da = new DoubleAnimation(-0.125, new Duration(TimeSpan.FromSeconds(0.8333 * durationSeconds)));
            trans.BeginAnimation(TranslateTransform3D.OffsetXProperty, da);

            // _Main3D.Camera
            Point3DAnimation pa = new Point3DAnimation(new Point3D(0.72, -2.5, 6.5), new Duration(TimeSpan.FromSeconds(durationSeconds)));
            pa.AccelerationRatio = 0.5;
            pa.DecelerationRatio = 0.5;
            ((PerspectiveCamera)_Main3D.Camera).BeginAnimation(PerspectiveCamera.PositionProperty, pa);

            // Now the book is closed.
            IsBookOpen = false;
        }

    }
}
