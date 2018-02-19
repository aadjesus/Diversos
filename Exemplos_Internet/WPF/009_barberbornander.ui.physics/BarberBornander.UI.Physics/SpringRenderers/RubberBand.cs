using System;
using System.Windows.Media;


namespace BarberBornander.UI.Physics.SpringRenderers
{
    /// <summary>
    /// Creates a RubberBand Spring renderer
    /// Basically it's a line
    /// </summary>
    public class RubberBand : ISpringRenderer
    {
        #region Public Properties
        public Pen Pen { get; set; }
        #endregion

        #region Ctor
        public RubberBand(Pen pen)
        {
            Pen = pen;
        }
        #endregion

        #region ISpringRenderer Members

        public void Render(Spring spring, DrawingContext drawingContext)
        {
            drawingContext.DrawLine(Pen, spring.From.Position.ToPoint(), spring.To.Position.ToPoint());
        }

        #endregion
    }
}
