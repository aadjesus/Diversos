
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
    public class GestureScrollVData : GestureData
    {
        #region Members
        private TNtrGestureScrollV _gScrollV = new TNtrGestureScrollV();
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wParam"></param>
        public GestureScrollVData(IntPtr wParam)
            : base(wParam)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public TNtrGestureScrollV ScrollVStruct
        {
            set { this._gScrollV = value; }
            get { return this._gScrollV; }
        }
        #endregion
    } // end of class
} // end of namespace