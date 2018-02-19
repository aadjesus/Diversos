//----------------------------------------------------
// Simple3DSceneInCode.cs (c) 2007 by Charles Petzold
//----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Petzold.Simple3DSceneInCode
{
    public class Simple3DSceneInCode : Window
    {
        PerspectiveCamera cam;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new Simple3DSceneInCode());
        }

        public Simple3DSceneInCode()
        {
            Title = "Simple 3D Scene in Code";

            // Make DockPanel content of window.
            DockPanel dock = new DockPanel();
            Content = dock;

            // Create Scrollbar for moving camera.
            ScrollBar scroll = new ScrollBar();
            scroll.Orientation = Orientation.Horizontal;
            scroll.Value = -2;
            scroll.Minimum = -2;
            scroll.Maximum = 2;
            scroll.ValueChanged += ScrollBarOnValueChanged;
            dock.Children.Add(scroll);
            DockPanel.SetDock(scroll, Dock.Bottom);

            // Create Viewport3D for 3D scene.
            Viewport3D viewport = new Viewport3D();
            dock.Children.Add(viewport);

            // Define the MeshGeometry3D.
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(new Point3D(0, 0, 0));
            mesh.Positions.Add(new Point3D(0, 1, -1));
            mesh.Positions.Add(new Point3D(0, 0, -2));
            mesh.TriangleIndices = new Int32Collection(new int[] { 0, 1, 2 });

            // Define the GeometryModel3D.
            GeometryModel3D geomod = new GeometryModel3D();
            geomod.Geometry = mesh;
            geomod.Material = new DiffuseMaterial(Brushes.Cyan);
            geomod.BackMaterial = new DiffuseMaterial(Brushes.Red);

            // Create ModelVisual3D for GeometryModel3D.
            ModelVisual3D modvis = new ModelVisual3D();
            modvis.Content = geomod;
            viewport.Children.Add(modvis);

            // Create another ModelVisual3D for light.
            modvis = new ModelVisual3D();
            modvis.Content = new AmbientLight(Colors.White);
            viewport.Children.Add(modvis);

            // Create the camera.
            cam = new PerspectiveCamera(new Point3D(-2, 0, 5), 
                            new Vector3D(0, 0, -1), new Vector3D(0, 1, 0), 45);
            viewport.Camera = cam;
        }
        void ScrollBarOnValueChanged(object sender,
                            RoutedPropertyChangedEventArgs<double> args)
        {
            cam.Position = new Point3D(args.NewValue, 0, 5);
        }
    }
}

