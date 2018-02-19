using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Ink;
using System.Windows.Input.StylusPlugIns;
using System.IO;

namespace InkOverVideo
{
   
    class CustomStroke : Stroke
    {
        public CustomStroke(StylusPointCollection stylusPoints, DrawingAttributes drawingAttributes)
            : base(stylusPoints, drawingAttributes)
        {
        }
        public CustomStroke(StylusPointCollection stylusPoints)
            : base(stylusPoints)
        {
        }
        public CustomStroke(Stroke stroke)
            : base(stroke.StylusPoints, stroke.DrawingAttributes)
        {
            foreach (Guid g in stroke.GetPropertyDataIds())
            {
                this.AddPropertyData(g, stroke.GetPropertyData(g));
            }

        }
        protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
        {
            DrawingAttributes da = new DrawingAttributes();

            // draw the outline stroke
            da.Color = Colors.Green;
            da.Width = drawingAttributes.Width * 1.5d;
            da.Height = drawingAttributes.Height * 1.5d;
            base.DrawCore(drawingContext, da);

            // draw the inner stroke
            da.Color = Colors.Orange;
            da.Width = drawingAttributes.Width;
            da.Height = drawingAttributes.Height;
            base.DrawCore(drawingContext, da);

        }
    }

    class MyInkCanvas : InkCanvas
    {
        private DynamicRenderer outerDr;
        public StylusPlugInCollection PluginCollection
        {
            get { return StylusPlugIns; }
        }
        public MyInkCanvas()
            : base()
        {

            DefaultDrawingAttributes.Width = 5d;
            DefaultDrawingAttributes.Height = 5d;
            DefaultDrawingAttributes.Color = Colors.Orange;
            // dynamic renderer for the outline of the ink
            outerDr = new DynamicRenderer();
            outerDr.DrawingAttributes = this.DefaultDrawingAttributes.Clone();
            outerDr.DrawingAttributes.Width *= 1.5d;
            outerDr.DrawingAttributes.Height *= 1.5d;
            outerDr.DrawingAttributes.Color = Colors.Green;

            this.StylusPlugIns.Add(outerDr);
            //Attach the dynamic renderers in the proper order
            this.InkPresenter.DetachVisuals(this.DynamicRenderer.RootVisual);
            this.InkPresenter.AttachVisuals(outerDr.RootVisual, outerDr.DrawingAttributes);
            this.InkPresenter.AttachVisuals(this.DynamicRenderer.RootVisual, this.DynamicRenderer.DrawingAttributes);
        }

        protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
        {
            CustomStroke newStroke = new CustomStroke(e.Stroke.StylusPoints, e.Stroke.DrawingAttributes);
            this.Strokes.Remove(e.Stroke);
            this.Strokes.Add(newStroke);
            InkCanvasStrokeCollectedEventArgs eNew = new InkCanvasStrokeCollectedEventArgs(newStroke);
            base.OnStrokeCollected(eNew);
        }

        protected override void OnActiveEditingModeChanged(RoutedEventArgs e)
        {
            base.OnActiveEditingModeChanged(e);
            if (this.ActiveEditingMode == InkCanvasEditingMode.Ink ||
                this.ActiveEditingMode == InkCanvasEditingMode.InkAndGesture ||
                this.ActiveEditingMode == InkCanvasEditingMode.GestureOnly)
            {
                outerDr.Enabled = true;
            }
            else
            {
                outerDr.Enabled = false;
            }
        }
    }
}
