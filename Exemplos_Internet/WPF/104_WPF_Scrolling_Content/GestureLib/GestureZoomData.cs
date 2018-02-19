
namespace GestureLib
{
    #region Using Statements
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class GestureZoomData : GestureData
    {
        #region Members
        private TNtrGestureZoom _gZoom = new TNtrGestureZoom();
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wParam"></param>
        public GestureZoomData(IntPtr wParam)
            : base(wParam)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public TNtrGestureZoom ZoomStruct
        {
            set { this._gZoom = value; }
            get { return this._gZoom; }
        }
        #endregion
    } // end of class
} // end of namespace