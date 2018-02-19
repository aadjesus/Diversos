using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace VEControl
{
    //NOTE: This is not a full implementation of VEShape (http://msdn.microsoft.com/en-us/library/bb412535.aspx)
    //      It is enough to build out the scenario for the demo
	public class VEShape
	{
        //BAD:  shouldn't expose the latLong directly.  This was done because of
        //      time contstraints.
        private VEShapeType type;
        private List<VELatLong> points;

		public VEShape(VEShapeType type, VELatLong latLong)
		{
            if (type == VEShapeType.Pushpin)
            {
                this.type = type;
                this.points = new List<VELatLong>();
                points.Add(latLong);
            }
            else
            {
                throw new Exception("This constructor only works for VEShapeType.Pushpin");
            }
		}

        public VEShape(VEShapeType type, List<VELatLong> points)
        {
            this.type = type;
            this.points = points;
        }

        public List<VELatLong> Points 
        { 
            
            get
            {
                return this.points;
            }
        }

        public VEShapeType Type 
        {
            get
            {
                return this.type;
            }        
        }

        //TODO: Implement a better way to associate a DataContext with a shape.
        //      This is different than how the JavaScript API works.  We want 
        //      to be able to take advantage of WPFs rich Databinding capabilities
        //      (not available in JavaScript).  Right now we will simply set this
        //      Tag property to the object we want the VE InfoBox to bind to.
        public object Tag { get; set; }
	}
}

