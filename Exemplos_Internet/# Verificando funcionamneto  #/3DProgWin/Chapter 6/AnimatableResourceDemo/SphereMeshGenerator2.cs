//-----------------------------------------------------
// SphereMeshGenerator2.cs (c) 2007 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace Petzold.AnimatableResourceDemo
{
    [RuntimeNameProperty("Name")]
    public class SphereMeshGenerator2 : Animatable
    {
        // Constructor performs initialization.
        public SphereMeshGenerator2()
        {
            Geometry = Geometry.Clone();
            PropertyChanged(new DependencyPropertyChangedEventArgs());
        }

        // Name dependency property and CLR property.
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name",
                typeof(string),
                typeof(SphereMeshGenerator2));

        public string Name
        {
            set { SetValue(NameProperty, value); }
            get { return (string)GetValue(NameProperty); }
        }

        // Center dependency property and CLR property.
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center",
                typeof(Point3D),
                typeof(SphereMeshGenerator2),
                new PropertyMetadata(new Point3D(), PropertyChanged));

        public Point3D Center
        {
            set { SetValue(CenterProperty, value); }
            get { return (Point3D)GetValue(CenterProperty); }
        }

        // Radius dependency property and CLR property.
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius",
                typeof(double),
                typeof(SphereMeshGenerator2),
                new PropertyMetadata(1.0, PropertyChanged));

        public double Radius
        {
            set { SetValue(RadiusProperty, value); }
            get { return (double)GetValue(RadiusProperty); }
        }

        // Slices dependency property and CLR property.
        public static readonly DependencyProperty SlicesProperty =
            DependencyProperty.Register("Slices",
                typeof(int),
                typeof(SphereMeshGenerator2),
                new PropertyMetadata(32, PropertyChanged));

        public int Slices
        {
            set { SetValue(SlicesProperty, value); }
            get { return (int)GetValue(SlicesProperty); }
        }

        // Stacks dependency property and CLR property.
        public static readonly DependencyProperty StacksProperty =
            DependencyProperty.Register("Stacks",
                typeof(int),
                typeof(SphereMeshGenerator2),
                new PropertyMetadata(16, PropertyChanged));

        public int Stacks
        {
            set { SetValue(StacksProperty, value); }
            get { return (int)GetValue(StacksProperty); }
        }

        // Geometry dependency property and CLR property.
        static readonly DependencyPropertyKey GeometryKey =
            DependencyProperty.RegisterReadOnly("Geometry",
                typeof(MeshGeometry3D),
                typeof(SphereMeshGenerator2),
                new PropertyMetadata(new MeshGeometry3D()));

        public static readonly DependencyProperty GeometryProperty =
            GeometryKey.DependencyProperty;

        public MeshGeometry3D Geometry
        {
            protected set { SetValue(GeometryKey, value); }
            get { return (MeshGeometry3D)GetValue(GeometryProperty); }
        }

        // PropertyChanged methods.
        static void PropertyChanged(DependencyObject obj,
                                    DependencyPropertyChangedEventArgs args)
        {
            (obj as SphereMeshGenerator2).PropertyChanged(args);
        }

        void PropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            // Get reference to Geometry property for local ease.
            MeshGeometry3D mesh = Geometry;

            // Get references to all four collections.
            Point3DCollection vertices = mesh.Positions;
            Vector3DCollection normals = mesh.Normals;
            Int32Collection indices = mesh.TriangleIndices;
            PointCollection textures = mesh.TextureCoordinates;

            // Set the MeshGeometry3D properties to null to inhibit notifications.
            mesh.Positions = null;
            mesh.Normals = null;
            mesh.TriangleIndices = null;
            mesh.TextureCoordinates = null;

            // Clear the four collections.
            vertices.Clear();
            normals.Clear();
            indices.Clear();
            textures.Clear();

            // Fill the vertices, normals, and textures collections.
            for (int stack = 0; stack <= Stacks; stack++)
            {
                double phi = Math.PI / 2 - stack * Math.PI / Stacks;
                double y = Radius * Math.Sin(phi);
                double scale = -Radius * Math.Cos(phi);

                for (int slice = 0; slice <= Slices; slice++)
                {
                    double theta = slice * 2 * Math.PI / Slices;
                    double x = scale * Math.Sin(theta);
                    double z = scale * Math.Cos(theta);

                    Vector3D normal = new Vector3D(x, y, z);
                    normals.Add(normal);
                    vertices.Add(normal + Center);
                    textures.Add(new Point((double)slice / Slices, 
                                           (double)stack / Stacks));
                }
            }

            // Fill the indices collection.
            for (int stack = 0; stack < Stacks; stack++)
            {
                int top = (stack + 0) * (Slices + 1);
                int bot = (stack + 1) * (Slices + 1);

                for (int slice = 0; slice < Slices; slice++)
                {
                    if (stack != 0)
                    {
                        indices.Add(top + slice);
                        indices.Add(bot + slice);
                        indices.Add(top + slice + 1);
                    }

                    if (stack != Stacks - 1)
                    {
                        indices.Add(top + slice + 1);
                        indices.Add(bot + slice);
                        indices.Add(bot + slice + 1);
                    }
                }
            }

            // Set the collections back to the properties
            mesh.TextureCoordinates = textures;
            mesh.TriangleIndices = indices;
            mesh.Normals = normals;
            mesh.Positions = vertices;
        }

        // Required override of CreateInstanceCore.
        protected override Freezable CreateInstanceCore()
        {
            return new SphereMeshGenerator2();
        }
    }
}
