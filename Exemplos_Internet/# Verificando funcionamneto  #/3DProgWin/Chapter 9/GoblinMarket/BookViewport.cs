//---------------------------------------------
// BookViewport.cs (c) 2007 by Charles Petzold
//---------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using Petzold.Media3D;

namespace Petzold.BookViewer3D
{
    public partial class BookViewport : Viewport3D
    {
        // Distance between leaves.
        const double leafGap = 0.05;

        // Duration of animation.
        static readonly Duration durAnimation = 
                            new Duration(TimeSpan.FromSeconds(3));

        // Other variables needed during mouse clicks.
        int leafCount = 0;
        int leafView = 0;
        List<Billboard> lstBillboards = new List<Billboard>();
        Random rand = new Random();

        // Precreated DoubleAnimation objects.
        DoubleAnimation animaAngle = new DoubleAnimation(0, durAnimation);
        DoubleAnimation animaRadius = new DoubleAnimation(0, durAnimation);

        // Only constructor.
        public BookViewport(Visual[] visuals)
        {
            InitializeComponent();





            if (visuals == null)
                return;








            // Get the number of leaves.
            leafCount = (visuals.Length + 1) / 2;

            for (int i = 0; i < leafCount; i++)
            {
                // Create Billboard object for each leaf.
                Billboard board = new Billboard();
                board.Slices = 48;
                board.Stacks = 24;
                board.UpperLeft = new Point3D(0, 5.5, 0);
                board.UpperRight = new Point3D(0, 5.5, 8.5);
                board.LowerLeft = new Point3D(0, -5.5, 0);
                board.LowerRight = new Point3D(0, -5.5, 8.5);

                // Set the top brush to a recto.
                VisualBrush visbrush = new VisualBrush(visuals[2 * i]);
                RenderOptions.SetCachingHint(visbrush, CachingHint.Cache); 
                board.Material = new DiffuseMaterial(visbrush);

                // Set the back brush to a verso.
                visbrush = new VisualBrush(visuals[2 * i + 1]);
                visbrush.Transform = new ScaleTransform(-1, 1, 0.5, 0);
                RenderOptions.SetCachingHint(visbrush, CachingHint.Cache);
                board.BackMaterial = new DiffuseMaterial(visbrush);

                // Calculate the offset from the center for the leaf.
                double spineOffset = leafGap * (i - leafCount / 2);
                board.Transform = new TranslateTransform3D(spineOffset, 0, 0);

                // Create a PageTurner object for each leaf.
                PageTurner pgturn = new PageTurner();
                pgturn.Angle = 90;
                pgturn.Radius = (leafCount - i) * leafGap;
                board.AlgorithmicTransforms.Add(pgturn);

                // Add the board to the Viewport3D and also a collection.
                Children.Add(board);
                lstBillboards.Add(board);
            }
        }

        // Animate leaf when user clicks a page.
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs args)
        {
            Point pt = args.GetPosition(this);
            HitTestResult result = VisualTreeHelper.HitTest(this, pt);

            if (result is RayMeshGeometry3DHitTestResult)
            {
                RayMeshGeometry3DHitTestResult result3d = 
                                result as RayMeshGeometry3DHitTestResult;

                if (result3d.VisualHit is Billboard)
                {
                    // Get the clicked billboard and prepare for animations.
                    Billboard board = result3d.VisualHit as Billboard;
                    int indexBillboard = lstBillboards.IndexOf(board);
                    Billboard boardAnimate = null;

                    // Clicked a recto: forward page turn (right to left).
                    if (indexBillboard >= leafView)
                    {
                        boardAnimate = lstBillboards[leafView];
                        animaAngle.To = -90;
                        animaRadius.To = (leafView + 1) * leafGap;
                        leafView++;
                    }
                    // Clicked a verso: back page turn (left to right).
                    else
                    {
                        boardAnimate = lstBillboards[leafView - 1];
                        animaAngle.To = 90;
                        animaRadius.To = (leafCount - leafView + 1) * leafGap;
                        leafView--;
                    }
                    // Start the animations.
                    PageTurner turn = 
                            boardAnimate.AlgorithmicTransforms[0] as PageTurner;

                    turn.Random1 = rand.NextDouble() - 0.5;
                    turn.Random2 = rand.NextDouble() - 0.5;
                    turn.BeginAnimation(PageTurner.AngleProperty, animaAngle);
                    turn.BeginAnimation(PageTurner.RadiusProperty, animaRadius);
                }
            }
        }
    }
}
