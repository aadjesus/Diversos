
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
    public class GestureRotateData : GestureData
    {
        #region Members
        private TNtrGestureRotate _gRotate = new TNtrGestureRotate();
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wParam"></param>
        public GestureRotateData(IntPtr wParam)
            : base(wParam)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public TNtrGestureRotate RotateStruct
        {
            set { this._gRotate = value; }
            get { return this._gRotate; }
        }
        #endregion
    } // end of class
} // end of namespace