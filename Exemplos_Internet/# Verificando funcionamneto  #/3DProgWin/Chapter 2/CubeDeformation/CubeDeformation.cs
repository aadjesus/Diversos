//------------------------------------------------
// CubeDeformation.cs (c) 2007 by Charles Petzold
//------------------------------------------------
using System;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Petzold.CubeDeformation
{
    public partial class CubeDeformation : Window
    {
        // Animation parameters.
        static readonly TimeSpan interval = TimeSpan.FromSeconds(0.1);
        const int changeDirection = 10;
        const double vectorFactor = 0.005;

        // Fields used in timer event handler.
        MeshGeometry3D mesh;
        Point3D[] vertices = new Point3D[8];
        int[,] indices = new int[8,3];
        Random rand = new Random();
        Vector3D[] vectors = new Vector3D[8];

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CubeDeformation());
        }

        // Constructor.
        public CubeDeformation()
        {
            // Initialize XAML file.
            InitializeComponent();

            // Find the MeshGeometry3D object in the XAML file.
            mesh = FindName("cube") as MeshGeometry3D;

            // Find all the unique vertices; store in 'vertices' field.
            // For each vertex, find the three indices of the Positions
            //  collection where that vertex is found; store in 'indices' field.
            int verticesFound = 0;
            int[] indicesFound = new int[8];

            for (int i = 0; i < mesh.Positions.Count; i++)
            {
                Point3D point = mesh.Positions[i];
                int j;

                for (j = 0; j < verticesFound; j++)
                    if (point == vertices[j])
                    {
                        indices[j, indicesFound[j]++] = i;
                        break;
                    }

                if (j == verticesFound)
                {
                    vertices[verticesFound] = point;
                    indices[verticesFound++, indicesFound[j]++] = i;
                }
            }

            // Set up the timer for the animation.
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = interval; 
            tmr.Tick += TimerOnTick;
            tmr.Start();
        }

        void TimerOnTick(object sender, EventArgs args)
        {
            // Detach the Positions collection from the MeshGeometry3D.
            Point3DCollection coll = mesh.Positions;
            mesh.Positions = null;

            // Look through the 8 unique vertices.
            for (int i = 0; i < 8; i++)
            {
                // For each vertex, possibly change the animation 
                //  direction stored in the 'vectors' array.
                if (rand.Next(changeDirection) == 0)
                {
                    Vector3D vector = new Vector3D(rand.NextDouble() - 0.5, 
                                                   rand.NextDouble() - 0.5, 
                                                   rand.NextDouble() - 0.5);
                    vector.Normalize();
                    vectors[i] = vector;
                }

                // Regardless, change the vertex based on the vector.
                vertices[i] += vectorFactor * vectors[i];

                // Change all three copies of the vertex in the collection.
                for (int j = 0; j < 3; j++)
                    coll[indices[i, j]] = vertices[i];
            }
            // Reattach the collection to the MeshGeometry3D.
            mesh.Positions = coll;
        }
    }
}
