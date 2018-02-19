using System;
using System.Collections.Generic;
using System.Text;

namespace Sysbytes.ControlLibrary
{
    static class Common
    {
        public static System.Drawing.Drawing2D.GraphicsPath RoundedRect(
            System.Drawing.Rectangle baseRect, int topLeftRadius,
            int topRightRadius, int bottomLeftRadius, int bottomRightRadius)
        {
            int topLeftDiameter = topLeftRadius * 2;
            int topRightDiameter = topRightRadius * 2;
            int bottomLeftDiameter = bottomLeftRadius * 2;
            int bottomRightDiameter = bottomRightRadius * 2;

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

            System.Drawing.Rectangle rectTopLeft = new System.Drawing.Rectangle(
                baseRect.Left, baseRect.Top, topLeftDiameter, topLeftDiameter);
            System.Drawing.Rectangle rectTopRight = new System.Drawing.Rectangle(
                baseRect.Right - topRightDiameter, baseRect.Top, topRightDiameter, topRightDiameter);
            System.Drawing.Rectangle rectBottomLeft = new System.Drawing.Rectangle(
                baseRect.Left, baseRect.Bottom - bottomLeftDiameter, bottomLeftDiameter, bottomLeftDiameter);
            System.Drawing.Rectangle rectBottomRight = new System.Drawing.Rectangle(
                baseRect.Right - bottomRightDiameter, baseRect.Bottom - bottomRightDiameter, bottomRightDiameter, bottomRightDiameter);

            gp.AddArc(rectTopLeft, 180, 90);
            gp.AddArc(rectTopRight, 270, 90);
            gp.AddArc(rectBottomRight, 0, 90);
            gp.AddArc(rectBottomLeft, 90, 90);

            gp.CloseFigure();

            return gp;
        }

        public static System.Drawing.Drawing2D.GraphicsPath RoundedRect(
            System.Drawing.Rectangle baseRect, int cornerRadius)
        {
            int diameter = cornerRadius * 2;

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

            System.Drawing.Rectangle rectTopLeft = new System.Drawing.Rectangle(
                baseRect.Left, baseRect.Top, diameter, diameter);
            System.Drawing.Rectangle rectTopRight = new System.Drawing.Rectangle(
                baseRect.Right - diameter, baseRect.Top, diameter, diameter);
            System.Drawing.Rectangle rectBottomLeft = new System.Drawing.Rectangle(
                baseRect.Left, baseRect.Bottom - diameter, diameter, diameter);
            System.Drawing.Rectangle rectBottomRight = new System.Drawing.Rectangle(
                baseRect.Right - diameter, baseRect.Bottom - diameter, diameter, diameter);

            gp.AddArc(rectTopLeft, 180, 90);
            gp.AddArc(rectTopRight, 270, 90);
            gp.AddArc(rectBottomRight, 0, 90);
            gp.AddArc(rectBottomLeft, 90, 90);

            gp.CloseFigure();

            return gp;
        }
    }
}
