using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace WpfCubeControl
{
    public class CubeBuilder : ModelBuilder
    {
        public CubeBuilder(Color color)
            : base(color)
        {
        }

        public ModelVisual3D Create()
        {
            Model3DGroup cube = new Model3DGroup();

            Point3D p0 = new Point3D(-4.0, -4.0, -4.0);
            Point3D p1 = new Point3D(4.0, -4.0, -4.0);
            Point3D p2 = new Point3D(4.0, -4.0, 4.0);
            Point3D p3 = new Point3D(-4.0, -4.0, 4.0);
            Point3D p4 = new Point3D(-4.0, 4.0, -4.0);
            Point3D p5 = new Point3D(4.0, 4.0, -4.0);
            Point3D p6 = new Point3D(4.0, 4.0, 4.0);
            Point3D p7 = new Point3D(-4.0, 4.0, 4.0);

            //front
            cube.Children.Add(CreateTriangle(p3, p2, p6));
            cube.Children.Add(CreateTriangle(p3, p6, p7));

            //right
            cube.Children.Add(CreateTriangle(p2, p1, p5));
            cube.Children.Add(CreateTriangle(p2, p5, p6));

            //back
            cube.Children.Add(CreateTriangle(p1, p0, p4));
            cube.Children.Add(CreateTriangle(p1, p4, p5));

            //left
            cube.Children.Add(CreateTriangle(p0, p3, p7));
            cube.Children.Add(CreateTriangle(p0, p7, p4));

            //top
            cube.Children.Add(CreateTriangle(p7, p6, p5));
            cube.Children.Add(CreateTriangle(p7, p5, p4));

            //bottom
            cube.Children.Add(CreateTriangle(p2, p3, p0));
            cube.Children.Add(CreateTriangle(p2, p0, p1));

            ModelVisual3D model = new ModelVisual3D();
            model.Content = cube;
            return model;
        }
    }
}
