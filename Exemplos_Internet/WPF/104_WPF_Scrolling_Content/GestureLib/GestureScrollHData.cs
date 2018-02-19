
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
    public class GestureScrollHData : GestureData
    {
        #region Members
        private TNtrGestureScrollH _gScrollH = new TNtrGestureScrollH();
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wParam"></param>
        public GestureScrollHData(IntPtr wParam)
            : base(wParam)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public TNtrGestureScrollH ScrollHStruct
        {
            set { this._gScrollH = value; }
            get { return this._gScrollH; }
        }
        #endregion
    } // end of class
} // end of namespace