//-----------------------------------------------------
// SphereMeshGenerator1.cs (c) 2007 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Petzold.SphereResourceDemo
{
    public class SphereMeshGenerator1
    {
        // Four private initialized fields.
        int slices = 32;
        int stacks = 16;
        Point3D center = new Point3D();
        double radius = 1;

        // Four public properties allow access to private fields.
        public int Slices
        {
            set { slices = value; }
            get { return slices; }
        }

        public int Stacks
        {
            set { stacks = value; }
            get { return stacks; }
        }

        public Point3D Center
        {
            set { center = value; }
            get { return center; }
        }

        public double Radius
        {
            set { radius = value; }
            get { return radius; }
        }

        // Get-only property generates MeshGeometry3D.
        public MeshGeometry3D Geometry
        {
            get
            {
                // Create a MeshGeometry3D.
                MeshGeometry3D mesh = new MeshGeometry3D();

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
                        mesh.Normals.Add(normal);
                        mesh.Positions.Add(normal + Center);
                        mesh.TextureCoordinates.Add(
                                    new Point((double)slice / Slices, 
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
                            mesh.TriangleIndices.Add(top + slice);
                            mesh.TriangleIndices.Add(bot + slice);
                            mesh.TriangleIndices.Add(top + slice + 1);
                        }

                        if (stack != Stacks - 1)
                        {
                            mesh.TriangleIndices.Add(top + slice + 1);
                            mesh.TriangleIndices.Add(bot + slice);
                            mesh.TriangleIndices.Add(bot + slice + 1);
                        }
                    }
                }
                return mesh;
            }
        }
    }
}
