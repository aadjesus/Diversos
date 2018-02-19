
namespace FlickLib
{
    #region Using Statements
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public sealed class Flick
    {
        #region Members
        private FlickPoint _flkPoint = new FlickPoint(-1, -1);
        private FlickData _flkData = null;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Flick()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="data"></param>
        public Flick(FlickPoint point, FlickData data)
        {
            this._flkPoint = point;
            this._flkData = data;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public FlickPoint Point
        {
            set { this._flkPoint = value; }
            get { return this._flkPoint; }
        }

        /// <summary>
        /// 
        /// </summary>
        public FlickData Data
        {
            set { this._flkData = value; }
            get { return this._flkData; }
        }
        #endregion
    } // end of class
} // end of namespace