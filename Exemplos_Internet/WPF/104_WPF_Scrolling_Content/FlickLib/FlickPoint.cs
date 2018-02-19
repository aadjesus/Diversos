

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

    #region Original Struct
    /// <summary>
    /// Original location data from tablet flick
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FLICK_POINT
    {
        public Int16 x;
        public Int16 y;
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public struct FlickPoint
    {
        #region Members
        public short X;
        public short Y;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public FlickPoint(short x, short y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nVal"></param>
        public FlickPoint(Int32 nVal)
        {
            this.X = Flicks.GetLoWord(nVal);
            this.Y = Flicks.GetHiWord(nVal);
        }
        #endregion

        #region Convert Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nVal"></param>
        public void Convert(Int32 nVal)
        {
            this.X = Flicks.GetLoWord(nVal);
            this.Y = Flicks.GetHiWord(nVal);
        }
        #endregion
    } // end of class
} // end of namespace