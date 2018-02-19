//------------------------------------------------
// BeachBallSphere.cs (c) 2007 by Charles Petzold
//------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Petzold.BeachBallSphere
{
    public class BeachBallSphere : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new BeachBallSphere());
        }

        public BeachBallSphere()
        {
            Title = "Beachball Sphere";

            // Create Viewport3D as content of window.
            Viewport3D viewport = new Viewport3D();
            Content = viewport;

            // Get the MeshGeometry3D from the GenerateSphere method.
            MeshGeometry3D mesh = 
                GenerateSphere(new Point3D(0, 0, 0), 1, 36, 18);
            mesh.Freeze();

            // Define a brush for the sphere.
            Brush[] brushes = new Brush[6] { Brushes.Red, Brushes.Blue, 
                                             Brushes.Yellow, Brushes.Orange, 
                                             Brushes.Green, Brushes.White };
            DrawingGroup drawgrp = new DrawingGroup();

            for (int i = 0; i < brushes.Length ; i++)
            {
                RectangleGeometry rectgeo = 
                    new RectangleGeometry(new Rect(10 * i, 0, 10, 60));

                GeometryDrawing geodraw = 
                    new GeometryDrawing(brushes[i], null, rectgeo);

                drawgrp.Children.Add(geodraw);
            }
            DrawingBrush drawbrsh = new DrawingBrush(drawgrp);
            drawbrsh.Freeze();

            // Define the GeometryModel3D.
            GeometryModel3D geomod = new GeometryModel3D();
            geomod.Geometry = mesh;
            geomod.Material = new DiffuseMaterial(drawbrsh);

            // Create a ModelVisual3D for the GeometryModel3D.
            ModelVisual3D modvis = new ModelVisual3D();
            modvis.Content = geomod;
            viewport.Children.Add(modvis);

            // Create another ModelVisual3D for light.
            Model3DGroup modgrp = new Model3DGroup();
            modgrp.Children.Add(new AmbientLight(Color.FromRgb(128, 128, 128)));
            modgrp.Children.Add(
                new DirectionalLight(Color.FromRgb(128, 128, 128),
                                     new Vector3D(2, -3, -1)));

            modvis = new ModelVisual3D();
            modvis.Content = modgrp;
            viewport.Children.Add(modvis);

            // Create the camera.
            PerspectiveCamera cam = new PerspectiveCamera(new Point3D(0, 0, 8), 
                            new Vector3D(0, 0, -1), new Vector3D(0, 1, 0), 45);
            viewport.Camera = cam;

            // Create a transform for the GeometryModel3D.
            AxisAngleRotation3D axisangle = 
                new AxisAngleRotation3D(new Vector3D(1, 1, 0), 0);
            RotateTransform3D rotate = new RotateTransform3D(axisangle);
            geomod.Transform = rotate;
            
            // Animate the RotateTransform3D.
            DoubleAnimation anima = 
                new DoubleAnimation(360, new Duration(TimeSpan.FromSeconds(5)));
            anima.RepeatBehavior = RepeatBehavior.Forever;
            axisangle.BeginAnimation(AxisAngleRotation3D.AngleProperty, anima);
        }
        MeshGeometry3D GenerateSphere(Point3D center, double radius,
                                      int slices, int stacks)
        {
            // Create the MeshGeometry3D.
            MeshGeometry3D mesh = new MeshGeometry3D();

            // Fill the Position, Normals, and TextureCoordinates collections.
            for (int stack = 0; stack <= stacks; stack++)
            {
                double phi = Math.PI / 2 - stack * Math.PI / stacks;
                double y = radius * Math.Sin(phi);
                double scale = -radius * Math.Cos(phi);

                for (int slice = 0; slice <= slices; slice++)
                {
                    double theta = slice * 2 * Math.PI / slices;
                    double x = scale * Math.Sin(theta);
                    double z = scale * Math.Cos(theta);

                    Vector3D normal = new Vector3D(x, y, z);
                    mesh.Normals.Add(normal);
                    mesh.Positions.Add(normal + center);
                    mesh.TextureCoordinates.Add(
                            new Point((double)slice / slices,
                                      (double)stack / stacks));
                }
            }

            // Fill the TriangleIndices collection.
            for (int stack = 0; stack < stacks; stack++)
                for (int slice = 0; slice < slices; slice++)
                {
                    int n = slices + 1; // Keep the line length down.

                    if (stack != 0)
                    {
                        mesh.TriangleIndices.Add((stack + 0) * n + slice);
                        mesh.TriangleIndices.Add((stack + 1) * n + slice);
                        mesh.TriangleIndices.Add((stack + 0) * n + slice + 1);
                    }
                    if (stack != stacks - 1)
                    {
                        mesh.TriangleIndices.Add((stack + 0) * n + slice + 1);
                        mesh.TriangleIndices.Add((stack + 1) * n + slice);
                        mesh.TriangleIndices.Add((stack + 1) * n + slice + 1);
                    }
                }
            return mesh;
        }
    }
}
