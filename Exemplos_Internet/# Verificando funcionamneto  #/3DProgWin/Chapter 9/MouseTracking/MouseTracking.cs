//----------------------------------------------
// MouseTracking.cs (c) 2007 by Charles Petzold
//----------------------------------------------
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Petzold.Media3D;

namespace Petzold.MouseTracking
{
    public partial class MouseTracking
    {
        bool isTracking;
        Point3D pointOriginal;
        TranslateTransform3D transOriginal;
        TranslateTransform3D transTracking;

        public MouseTracking()
        {
            InitializeComponent();
        }

        // Left mouse button click initiates tracking operation.
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs args)
        {
            base.OnMouseLeftButtonDown(args);

            Point ptMouse = args.GetPosition(viewport);
            HitTestResult result = VisualTreeHelper.HitTest(viewport, ptMouse);

            // We're only interested in 3D hits.
            RayMeshGeometry3DHitTestResult result3d = 
                                result as RayMeshGeometry3DHitTestResult;
            if (result3d == null)
                return;

            // We're only interested in ModelVisual3D hits.
            ModelVisual3D vis3d = result3d.VisualHit as ModelVisual3D;
            if (vis3d == null)
                return;

            // We're only interested in visuals with translate transforms.
            transTracking = vis3d.Transform as TranslateTransform3D;
            if (transTracking == null)
                return;

            LineRange range;
            ViewportInfo.Point2DtoPoint3D(viewport, ptMouse, out range);
            pointOriginal = range.PointFromZ(transTracking.OffsetZ);
            transOriginal = transTracking.Clone();
            isTracking = true;
            CaptureMouse();
            Focus();

            args.Handled = true;
        }

        // Mouse moves occur in the Z=0 plane.
        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);

            if (!isTracking)
                return;

            // Get the mouse position and adjust the translate transform.
            Point ptMouse = args.GetPosition(viewport);
            LineRange range;
            ViewportInfo.Point2DtoPoint3D(viewport, ptMouse, out range);
            Point3D pointNew = range.PointFromZ(transTracking.OffsetZ);
            AdjustTranslateTransform(pointNew);
        }

        // Mouse wheel moves forward and backward along Z axis.
        protected override void OnMouseWheel(MouseWheelEventArgs args)
        {
            base.OnMouseWheel(args);

            if (!isTracking)
                return;

            // Get the mouse position and adjust the translate transform.
            Point ptMouse = args.GetPosition(viewport);
            LineRange range;
            ViewportInfo.Point2DtoPoint3D(viewport, ptMouse, out range);
            Point3D pointNew = range.PointFromZ(transTracking.OffsetZ);
            pointNew.Z += args.Delta / 1200.0;
            AdjustTranslateTransform(pointNew);
        }

        // Called from OnMouseMove and OnMouseWheel.
        void AdjustTranslateTransform(Point3D pointNew)
        {
            Vector3D vectMouse = pointNew - pointOriginal;
            transTracking.OffsetX = transOriginal.OffsetX + vectMouse.X;
            transTracking.OffsetY = transOriginal.OffsetY + vectMouse.Y;
            transTracking.OffsetZ = transOriginal.OffsetZ + vectMouse.Z;
        }

        // End the tracking normally.
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs args)
        {
            base.OnMouseLeftButtonUp(args);

            isTracking = false;
            ReleaseMouseCapture();
        }

        // Abort the tracking operation.
        protected override void OnPreviewTextInput(TextCompositionEventArgs args)
        {
            base.OnPreviewTextInput(args);

            if (!isTracking)
                return;

            // End mouse tracking with press of Escape key.
            if (args.Text.IndexOf('\x1B') != -1)
                ReleaseMouseCapture();
        }

        // Clean up for aborted tracking operation.
        protected override void OnLostMouseCapture(MouseEventArgs args)
        {
            base.OnLostMouseCapture(args);

            // If tracking has been aborted, return to original values.
            if (isTracking)
            {
                transTracking.OffsetX = transOriginal.OffsetX;
                transTracking.OffsetY = transOriginal.OffsetY;
                transTracking.OffsetZ = transOriginal.OffsetZ;
                isTracking = false;
            }
        }
    }
}
