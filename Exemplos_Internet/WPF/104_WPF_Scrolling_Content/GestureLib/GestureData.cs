
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
    public abstract class GestureData
    {
        #region Members
        private ENtrGestureType _type = ENtrGestureType.DoubleTap;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public GestureData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wParam"></param>
        public GestureData(IntPtr wParam)
        {
            this._type = (ENtrGestureType)wParam;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public ENtrGestureType GestureType
        {
            get { return this._type; }
        }
        #endregion
    } // end of class
} // end of namespace