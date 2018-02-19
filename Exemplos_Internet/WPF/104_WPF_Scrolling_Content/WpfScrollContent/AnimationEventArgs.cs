using System;
using System.Collections.Generic;
using System.Text;

namespace WpfScrollContent
{
    /// <summary>
    /// Custom event args class to pass back an animated value
    /// </summary>
    public class AnimationEventArgs : EventArgs
    {
        // Stores the current animation value, in this case, a double
        private double _dbValue = 0;

        // Constructor must set the value
        public AnimationEventArgs(double dbValue)
        {
            this._dbValue = dbValue;
        }

        // Can only get the value, cannot set value after constructor
        public double Value
        {
            get { return this._dbValue; }
        }
    }
}