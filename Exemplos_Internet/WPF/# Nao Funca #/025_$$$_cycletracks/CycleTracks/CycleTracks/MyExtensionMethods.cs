using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VEControl;
using System.Windows;
using GarminData;

namespace CycleTracks
{
    public static class MyExtensionMethods
    {
        public static VELatLong ToVELatLong(this TrackPoint p)
        {
            return p.Position.ToVELatLong();
        }

        public static VELatLong ToVELatLong(this Point p)
        {
            return new VELatLong(p.X, p.Y);
        }
    }
}
