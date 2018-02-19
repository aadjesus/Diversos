using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation; 

namespace CubeInterface
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {

        public Window1()
        {
            InitializeComponent();
            this.Width = 700;
            this.Height = 700;

            mainViewport = new Viewport3D();
            this.Content = mainViewport; 
            createCubeModel();
        }
 
        Viewport3D mainViewport;
        System.Collections.ArrayList models = new System.Collections.ArrayList();
        System.Collections.ArrayList texts = new System.Collections.ArrayList();
        MeshGeometry3D triangleMesh;
        GeometryModel3D lastHitGeo;
        GeometryModel3D hitgeo;

        DiffuseMaterial unfocusedBackground = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Colors.YellowGreen));
        DiffuseMaterial focusedBackground = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Colors.GreenYellow));
        Single objectSpacing = 2;

        public void createCubeModel()
        {

            Material material;
            GeometryModel3D triangleModel;
            triangleMesh = new MeshGeometry3D();
            //Create the points we will use to define the surfaces.
            Point3D point0 = new Point3D(1, 5, 5);
            Point3D point1 = new Point3D(1, 5, -5);
            Point3D point2 = new Point3D(-1, 5, 5);
            Point3D point3 = new Point3D(-1, 5, -5);
            Point3D point4 = new Point3D(1, -5, 5);
            Point3D point5 = new Point3D(1, -5, -5);
            Point3D point6 = new Point3D(-1, -5, 5);
            Point3D point7 = new Point3D(-1, -5, -5);

            //Add the points and map texture coordonates to them.
            triangleMesh.Positions.Add(point0);
            triangleMesh.TextureCoordinates.Add(new System.Windows.Point(0, 2000));
            triangleMesh.Positions.Add(point1);
            triangleMesh.TextureCoordinates.Add(new System.Windows.Point(2000, 2000));
            triangleMesh.Positions.Add(point2);
            triangleMesh.TextureCoordinates.Add(new System.Windows.Point(0, 0));
            triangleMesh.Positions.Add(point3);
            triangleMesh.TextureCoordinates.Add(new System.Windows.Point(2000, 0));
            //Add the rest of the points, we don't need any moe texture mapping for them.
            triangleMesh.Positions.Add(point4);
            triangleMesh.Positions.Add(point5);
            triangleMesh.Positions.Add(point6);
            triangleMesh.Positions.Add(point7);
            //Start defining the surfaces that define the cube, tringle by triangle
            triangleMesh.TriangleIndices.Add(2);
            triangleMesh.TriangleIndices.Add(0);
            triangleMesh.TriangleIndices.Add(3);

            triangleMesh.TriangleIndices.Add(3);
            triangleMesh.TriangleIndices.Add(0);
            triangleMesh.TriangleIndices.Add(1);
            
            //Back
            triangleMesh.TriangleIndices.Add(7);
            triangleMesh.TriangleIndices.Add(4);
            triangleMesh.TriangleIndices.Add(6);

            triangleMesh.TriangleIndices.Add(5);
            triangleMesh.TriangleIndices.Add(4);
            triangleMesh.TriangleIndices.Add(7);

            //Down
            triangleMesh.TriangleIndices.Add(1);
            triangleMesh.TriangleIndices.Add(0);
            triangleMesh.TriangleIndices.Add(5);

            triangleMesh.TriangleIndices.Add(5);
            triangleMesh.TriangleIndices.Add(0);
            triangleMesh.TriangleIndices.Add(4);

            //Up
            triangleMesh.TriangleIndices.Add(6);
            triangleMesh.TriangleIndices.Add(2);
            triangleMesh.TriangleIndices.Add(7);

            triangleMesh.TriangleIndices.Add(7);
            triangleMesh.TriangleIndices.Add(2);
            triangleMesh.TriangleIndices.Add(3);

            //Lateral 1
            triangleMesh.TriangleIndices.Add(1);
            triangleMesh.TriangleIndices.Add(5);
            triangleMesh.TriangleIndices.Add(7);

            triangleMesh.TriangleIndices.Add(1);
            triangleMesh.TriangleIndices.Add(7);
            triangleMesh.TriangleIndices.Add(3);
            //lateral 2
            triangleMesh.TriangleIndices.Add(4);
            triangleMesh.TriangleIndices.Add(0);
            triangleMesh.TriangleIndices.Add(6);

            triangleMesh.TriangleIndices.Add(6);
            triangleMesh.TriangleIndices.Add(0);
            triangleMesh.TriangleIndices.Add(2);

            material = new DiffuseMaterial( new SolidColorBrush(System.Windows.Media.Colors.Aquamarine)   );
            triangleModel = new GeometryModel3D(triangleMesh, material);
            ModelVisual3D model; 
            model=new ModelVisual3D();
            model.Content = triangleModel;

            InitialiseViewPort();

            //AddComplexObject();

            //Add objects to our list.
            for (int i = 0; i < 10; i++)
            {
                Label l = new Label();
                l.Content = System.DateTime.Parse("01-JAN-1970").AddMonths(i).ToString("MMMM"); 
                l.BorderBrush = Brushes.YellowGreen;
                l.Foreground = Brushes.DarkGreen;  
                l.BorderThickness = new System.Windows.Thickness(1); 
               
                Add(l);         
            }
        }

        private void AddComplexObject()
        {
            DockPanel dp = new DockPanel();
            dp.Background = Brushes.YellowGreen;

            Label y = new Label();
            y.Background = Brushes.YellowGreen;
            y.Content = "Year";
            y.Margin = new Thickness(2);
            dp.Children.Add(y);
            DockPanel.SetDock(y, Dock.Top);

            TextBox m = new TextBox();
            m.Text = "Month";
            m.Margin = new Thickness(2);
            dp.Children.Add(m);
            DockPanel.SetDock(m, Dock.Bottom);

            Add(dp);
        }

        public void InitialiseViewPort()
        {
            ModelVisual3D light = new ModelVisual3D();
            light.Content = new AmbientLight(System.Windows.Media.Colors.White);
            mainViewport.Camera = new PerspectiveCamera(new Point3D(-3, 15, -2), new Vector3D(7, -10, 2), new Vector3D(1, 0, 0), 1000);
            mainViewport.Children.Add(light);
            ModelVisual3D bulb=new ModelVisual3D();
            mainViewport.Children.Add(bulb);
            mainViewport.MouseMove += new MouseEventHandler(mainViewport_MouseMove);
            mainViewport.MouseDown += new MouseButtonEventHandler(mainViewport_MouseDown); 
        }

        public void Add(Visual  l)
        {
            //Store the Visual of the control in the ArrayList, we'll need it later
            texts.Add(l);

            //Create the model on which to draw the control's Visual
            ModelVisual3D extraCube = new ModelVisual3D();
            Model3DGroup modelGroup = new Model3DGroup();
            GeometryModel3D model3d = new GeometryModel3D();

            //use the cube as the standard geometry of the model
            model3d.Geometry = (MeshGeometry3D)triangleMesh;

            //Create the material with the texture of the Control and use it on the model
            DiffuseMaterial visualMaterial = new DiffuseMaterial(new VisualBrush(l));
            model3d.Material = visualMaterial;    

            modelGroup.Children.Add(model3d);
            extraCube.Content = modelGroup;

            //Store the model in the ArayList, we'll need it for hittesting
            models.Add(model3d);

            //Add the new cube to the ViewPort, so that we can see it
            mainViewport.Children.Add(extraCube);



            // give the cube a different (cascading) location in the scene    
            extraCube.Transform = new TranslateTransform3D(objectSpacing * (texts.Count-1), 0, 0);  
            //and play the animation
            ApplyTransform(model3d);  
        }

        public void Remove(int i)
        {
            texts.RemoveAt(i);
            Repopulate();
        }

        public void Repopulate()
        {
            models.Clear(); 
            mainViewport.Children.Clear();
            InitialiseViewPort(); 
            for (int i = 0; i < texts.Count; i++)
            {
                Visual l = (Visual)texts[i];
                ModelVisual3D extraCube = new ModelVisual3D();
                Model3DGroup modelGroup = new Model3DGroup();
                GeometryModel3D model3d = new GeometryModel3D();
                model3d.Geometry = (MeshGeometry3D)triangleMesh;
                DiffuseMaterial visualMaterial = new DiffuseMaterial(new VisualBrush(l));
                //DiffuseMaterial graySide = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Colors.Blue ) ); 
                model3d.Material = visualMaterial;    // Add mesh and identifier..    
                modelGroup.Children.Add(model3d);
                extraCube.Content = modelGroup;
                mainViewport.Children.Add(extraCube);
                models.Add(model3d);
                // give the cube a different (cascading) location in the scene    
                extraCube.Transform = new TranslateTransform3D(objectSpacing * (i), 0, 0);
                ApplyTransform(model3d); 
            }
        }

        void mainViewport_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point mouseposition = e.GetPosition(mainViewport);
            Point3D testpoint3D = new Point3D(mouseposition.X, mouseposition.Y, 0);
            Vector3D testdirection = new Vector3D(mouseposition.X, mouseposition.Y, 10);
            PointHitTestParameters pointparams = new PointHitTestParameters(mouseposition);
            RayHitTestParameters rayparams = new RayHitTestParameters(testpoint3D, testdirection);
            //test for a result in the Viewport3D   
            hitgeo = null;
            
            
            VisualTreeHelper.HitTest(mainViewport, null, HTResult, pointparams);
               
            if ((hitgeo != lastHitGeo)&&(hitgeo!=null))
            {
                //MessageBox.Show("ll"); 
                for (int i = 0; i < models.Count; i++)
                { ((GeometryModel3D)models[i]).BackMaterial = unfocusedBackground; }
                
                ApplyTransform(hitgeo);
                hitgeo.BackMaterial = focusedBackground;

 
                lastHitGeo = hitgeo;
            }


        }

        void mainViewport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point mouseposition = e.GetPosition(mainViewport);
            Point3D testpoint3D = new Point3D(mouseposition.X, mouseposition.Y, 0);
            Vector3D testdirection = new Vector3D(mouseposition.X, mouseposition.Y, 10);
            PointHitTestParameters pointparams = new PointHitTestParameters(mouseposition);
            RayHitTestParameters rayparams = new RayHitTestParameters(testpoint3D, testdirection);
            //test for a result in the Viewport3D   
            hitgeo = null;
            VisualTreeHelper.HitTest(mainViewport, null, HTResult, pointparams);

            for (int i = 0; i < models.Count; i++)
                if (models[i] == hitgeo)
                {
                    hitgeo = null;
                    Remove(i);
                }
        }

        public HitTestResultBehavior HTResult(System.Windows.Media.HitTestResult rawresult)
        {
            RayHitTestResult rayResult = rawresult as RayHitTestResult;
            if (rayResult != null)
            {

                DiffuseMaterial darkSide = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Colors.Red));
                bool gasit = false;
                for (int i=0;i<models.Count;i++ )
                    if ((GeometryModel3D)models[i] == rayResult.ModelHit) 
                    { 
                        hitgeo = (GeometryModel3D)rayResult.ModelHit;
                        gasit = true;
                    }
                if (!gasit)  {hitgeo=null;}

                //lst.Items.RemoveAt(1);
            }
            else 
            {
                
            }
            return HitTestResultBehavior.Stop;
        }

        void ApplyTransform(GeometryModel3D model)
        {
            model.BackMaterial = unfocusedBackground;
            TranslateTransform3D tt3d = new TranslateTransform3D(new Vector3D(0, 0, 0));
            DoubleAnimation da = new DoubleAnimation(-4, new Duration(TimeSpan.FromSeconds(1)));
            tt3d.BeginAnimation(TranslateTransform3D.OffsetYProperty, da);
            Transform3DGroup tgroup = new Transform3DGroup();
            tgroup.Children.Add(tt3d);
            model.Transform = tgroup;

        }
        }
    }