using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;




namespace BarberBornander.UI.Physics
{
    /// <summary>
    /// Creates a Canvas with a dotted outline
    /// </summary>
    public class DashedOutlineCanvas : Canvas
    {
        #region Overrides
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            Pen penBounds = new Pen(new SolidColorBrush(Colors.WhiteSmoke), 1.0);
            penBounds.DashStyle = DashStyles.Dash;
            dc.DrawLine(penBounds, new Point(0, 0), new Point(0, this.ActualHeight));
            dc.DrawLine(penBounds, new Point(0, this.ActualHeight), new Point(this.ActualWidth, this.ActualHeight));
            dc.DrawLine(penBounds, new Point(ActualWidth, this.ActualHeight), new Point(this.ActualWidth, 0));
            dc.DrawLine(penBounds, new Point(0, 0), new Point(ActualWidth, 0));
        }
        #endregion
    }
}
