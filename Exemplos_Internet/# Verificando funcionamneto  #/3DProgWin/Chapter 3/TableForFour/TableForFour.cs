//---------------------------------------------
// TableForFour.cs (c) 2007 by Charles Petzold
//---------------------------------------------
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace Petzold.TableForFour
{
    public partial class TableForFour
    {
        // Constructor.
        public TableForFour()
        {
            InitializeComponent();
        }

        // OnMouseDown handler.
        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            Point pt = args.GetPosition(viewport3d);

            // Obtain the Visual3D objects under the mouse pointer.
            VisualTreeHelper.HitTest(viewport3d, null, HitTestDown,
                                     new PointHitTestParameters(pt));
        }

        // Callback for VisualTreeHelp.HitTest.
        HitTestResultBehavior HitTestDown(HitTestResult result)
        {
            // Cast result parameter to RayMeshGeometry3DHitTestResult.
            RayMeshGeometry3DHitTestResult resultMesh = 
                                    result as RayMeshGeometry3DHitTestResult;

            // This should not happen, but play it safe anyway.
            if (resultMesh == null)
                return HitTestResultBehavior.Continue;

            // Obtain clicked ModelVisual3D.
            ModelVisual3D vis = resultMesh.VisualHit as ModelVisual3D;

            // This should not happen, but play it safe anyway.
            if (vis == null)
                return HitTestResultBehavior.Continue;

            // If the user clicks the floor, set a new color.
            if (vis == (viewport3d.FindName("floor") as ModelVisual3D))
            {
                GeometryModel3D model = resultMesh.ModelHit as GeometryModel3D;
                DiffuseMaterial mat = model.Material as DiffuseMaterial;

                Random rand = new Random();
                mat.Brush = new SolidColorBrush(
                                    Color.FromRgb((byte)rand.Next(256),
                                                  (byte)rand.Next(256),
                                                  (byte)rand.Next(256)));

                return HitTestResultBehavior.Stop;
            }

            // If the user clicks the table, rotate it.
            if (vis == (viewport3d.FindName("table") as ModelVisual3D))
            {
                // Create a Storyboard for the animations.
                Storyboard storybrd = new Storyboard();

                // Define a DoubleAnimation to lift the table.
                DoubleAnimation animaDouble = new DoubleAnimation();
                animaDouble.From=0;
                animaDouble.To=3;
                Storyboard.SetTargetName(animaDouble, "translate");
                Storyboard.SetTargetProperty(animaDouble, 
                        new PropertyPath(TranslateTransform3D.OffsetYProperty));
                storybrd.Children.Add(animaDouble);

                // Another animation turns the table. 
                animaDouble = new DoubleAnimation();
                animaDouble.From=0;
                animaDouble.To=90;
                animaDouble.BeginTime=TimeSpan.FromSeconds(1);
                Storyboard.SetTargetName(animaDouble, "rotate");
                Storyboard.SetTargetProperty(animaDouble,
                        new PropertyPath(AxisAngleRotation3D.AngleProperty));
                storybrd.Children.Add(animaDouble);

                // A third animation sets the table back down.
                animaDouble = new DoubleAnimation();
                animaDouble.From=3;
                animaDouble.To=0;
                animaDouble.BeginTime=TimeSpan.FromSeconds(2);
                Storyboard.SetTargetName(animaDouble, "translate");
                Storyboard.SetTargetProperty(animaDouble, 
                        new PropertyPath(TranslateTransform3D.OffsetYProperty));
                storybrd.Children.Add(animaDouble);

                // Start the Storyboard.
                storybrd.Begin(this);

                return HitTestResultBehavior.Stop;
            }

            // Otherwise it's a chair. Get the Transform3DGroup.
            Transform3DGroup xformgrp = vis.Transform as Transform3DGroup;

            // This should not happen, but play it safe anyway.
            if (xformgrp == null)
                return HitTestResultBehavior.Stop;

            // Loop through the child tranforms.
            for (int i = 0; i < xformgrp.Children.Count; i++)
            {
                // Find the TranslateTransform3D.
                TranslateTransform3D trans = 
                                xformgrp.Children[i] as TranslateTransform3D;

                if (trans != null)
                {
                    // Define an animation for the transform.
                    DoubleAnimation anima = new DoubleAnimation();
                    DependencyProperty prop = null;

                    if (trans.OffsetZ == 0)
                    {
                        prop = TranslateTransform3D.OffsetXProperty;

                        if (Math.Abs(trans.OffsetX) < 2)
                            anima.To = 2.5 * Math.Sign(trans.OffsetX);
                        else
                            anima.To = 1.25 * Math.Sign(trans.OffsetX);
                    }
                    else
                    {
                        prop = TranslateTransform3D.OffsetZProperty;

                        if (Math.Abs(trans.OffsetZ) < 2)
                            anima.To = 2.5 * Math.Sign(trans.OffsetZ);
                        else
                            anima.To = 1.25 * Math.Sign(trans.OffsetZ);
                    }

                    // Start the animation and stop the hit-testing.
                    trans.BeginAnimation(prop, anima);
                    return HitTestResultBehavior.Stop;
                }
            }
            return HitTestResultBehavior.Continue;
        }
    }
}
