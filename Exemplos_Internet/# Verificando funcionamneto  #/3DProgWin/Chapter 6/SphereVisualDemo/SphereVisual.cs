//---------------------------------------------
// SphereVisual.cs (c) 2007 by Charles Petzold
//---------------------------------------------
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Petzold.SphereVisualDemo
{
    public class SphereVisual : ModelVisual3D
    {
        // Private fields store necessary content.
        GeometryModel3D model;
        MeshGeometry3D mesh;

        // Constructor.
        public SphereVisual()
        {
            // Create objects and set the Content property.
            model = new GeometryModel3D();
            mesh = new MeshGeometry3D();
            model.Geometry = mesh;
            Content = model;

            // Initialize the MeshGeometry3D.
            PropertyChanged(new DependencyPropertyChangedEventArgs());
        }

        // Material dependency property and CLR property.
        public static readonly DependencyProperty MaterialProperty =
            GeometryModel3D.MaterialProperty.AddOwner(
                typeof(SphereVisual),
                new PropertyMetadata(MaterialPropertyChanged));

        public Material Material
        {
            set { SetValue(MaterialProperty, value); }
            get { return (Material)GetValue(MaterialProperty); }
        }

        // BackMaterial dependency property and CLR property.
        public static readonly DependencyProperty BackMaterialProperty =
            GeometryModel3D.BackMaterialProperty.AddOwner(
                typeof(SphereVisual),
                new PropertyMetadata(MaterialPropertyChanged));

        public Material BackMaterial
        {
            set { SetValue(BackMaterialProperty, value); }
            get { return (Material)GetValue(BackMaterialProperty); }
        }

        // Center dependency property and CLR property.
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center",
                typeof(Point3D),
                typeof(SphereVisual),
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
                typeof(SphereVisual),
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
                typeof(SphereVisual),
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
                typeof(SphereVisual),
                new PropertyMetadata(16, PropertyChanged));

        public int Stacks
        {
            set { SetValue(StacksProperty, value); }
            get { return (int)GetValue(StacksProperty); }
        }

        // MaterialPropertyChanged methods.
        static void MaterialPropertyChanged(DependencyObject obj,
                                    DependencyPropertyChangedEventArgs args)
        {
            (obj as SphereVisual).MaterialPropertyChanged(args);
        }

        void MaterialPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            if (args.Property == MaterialProperty)
                model.Material = args.NewValue as Material;

            else if (args.Property == BackMaterialProperty)
                model.BackMaterial = args.NewValue as Material;
        }

        // PropertyChanged methods.
        static void PropertyChanged(DependencyObject obj,
                                    DependencyPropertyChangedEventArgs args)
        {
            (obj as SphereVisual).PropertyChanged(args);
        }

        void PropertyChanged(DependencyPropertyChangedEventArgs args)
        {
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

            // Set the collections back to the properties.
            mesh.TextureCoordinates = textures;
            mesh.TriangleIndices = indices;
            mesh.Normals = normals;
            mesh.Positions = vertices;
        }
    }
}
