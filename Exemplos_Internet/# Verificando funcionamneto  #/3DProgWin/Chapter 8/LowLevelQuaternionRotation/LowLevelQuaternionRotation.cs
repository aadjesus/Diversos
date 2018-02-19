//-----------------------------------------------------------
// LowLevelQuaternionRotation.cs (c) 2007 by Charles Petzold
//-----------------------------------------------------------
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Petzold.LowLevelQuaternionRotation
{
    public class LowLevelQuaternionRotation : Window
    {
        const double secondsPerCycle = 3;
        static readonly Vector3D axis = new Vector3D(1, 1, 0);
        static readonly Quaternion qCenter = new Quaternion(0.5, 0.5, -2, 0);

        Stopwatch stopwatch;
        MeshGeometry3D mesh;
        Point3D[] pointsCuboid =
            {
                new Point3D(0, 1,  0), new Point3D(0, 0,  0), 
                new Point3D(1, 1,  0), new Point3D(1, 0,  0),
                new Point3D(0, 1, -4), new Point3D(0, 0, -4), 
                new Point3D(0, 1,  0), new Point3D(0, 0,  0),
                new Point3D(1, 1, -4), new Point3D(0, 1, -4), 
                new Point3D(1, 1,  0), new Point3D(0, 1,  0),
                new Point3D(1, 1,  0), new Point3D(1, 0,  0), 
                new Point3D(1, 1, -4), new Point3D(1, 0, -4),
                new Point3D(1, 0,  0), new Point3D(0, 0,  0), 
                new Point3D(1, 0, -4), new Point3D(0, 0, -4),
                new Point3D(1, 1, -4), new Point3D(1, 0, -4), 
                new Point3D(0, 1, -4), new Point3D(0, 0, -4)
            };

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new LowLevelQuaternionRotation());
        }
        public LowLevelQuaternionRotation()
        {
            Title = "Low-Level Quaternion Rotation";

            // Create Viewport3D as content of window.
            Viewport3D viewport = new Viewport3D();
            Content = viewport;

            // Create MeshGeometry3D.
            mesh = new MeshGeometry3D();
            mesh.Positions = new Point3DCollection(pointsCuboid);
            mesh.TriangleIndices = new Int32Collection(new Int32[]
                {
                    0, 1, 2, 1, 3, 2, 4, 5, 6, 5, 7, 6,
                    8, 9, 10, 9, 11, 10, 12, 13, 14, 13, 15, 14,
                    16, 17, 18, 17, 19, 18, 20, 21, 22, 21, 23, 22
                });

            // Assemble all the models together.
            Model3DGroup grp = new Model3DGroup();
            grp.Children.Add(new GeometryModel3D(mesh, 
                                    new DiffuseMaterial(Brushes.Cyan)));
            grp.Children.Add(new AmbientLight(Color.FromRgb(64, 64, 64)));
            grp.Children.Add(new DirectionalLight(Color.FromRgb(192, 192, 192),
                                                  new Vector3D(2, -3, -1)));

            // Create ModelVisual3D and camera.
            ModelVisual3D vis = new ModelVisual3D();
            vis.Content = grp;
            viewport.Children.Add(vis);
            viewport.Camera = 
                new PerspectiveCamera(new Point3D(-2, 2, 4), 
                        new Vector3D(2, -1, -4), new Vector3D(0, 1, 0), 45);

            stopwatch = new Stopwatch();
            stopwatch.Start();
            CompositionTarget.Rendering += OnRendering;
        }

        void OnRendering(object sender, EventArgs args)
        {
            // Detach collection from MeshGeometry3D.
            Point3DCollection points = mesh.Positions;
            mesh.Positions = null;
            points.Clear();

            // Calculation rotation quaternion.
            double angle = 360.0 * (stopwatch.Elapsed.TotalSeconds %
                                            secondsPerCycle) / secondsPerCycle;
            Quaternion qRotate = new Quaternion(axis, angle);
            Quaternion qConjugate = qRotate;
            qConjugate.Conjugate();

            // Apply rotation to each point.
            foreach (Point3D point in pointsCuboid)
            {
                Quaternion qPoint = new Quaternion(point.X, point.Y, point.Z, 0);
                qPoint -= qCenter;
                Quaternion qRotatedPoint = qRotate * qPoint * qConjugate;
                qRotatedPoint += qCenter;
                points.Add(new Point3D(qRotatedPoint.X, qRotatedPoint.Y, 
                                                        qRotatedPoint.Z));
            }

            // Re-attach collections to MeshGeometry3D.
            mesh.Positions = points;
        }
    }
}
