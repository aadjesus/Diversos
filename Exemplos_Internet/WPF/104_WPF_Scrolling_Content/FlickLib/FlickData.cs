
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

    #region Original Struct from tabflicks.h file
    //typedef struct FLICK_DATA
    //{
    //    FLICKACTION_COMMANDCODE iFlickActionCommandCode:5;
    //    FLICKDIRECTION iFlickDirection:3;
    //    BOOL fControlModifier:1;
    //    BOOL fMenuModifier:1;
    //    BOOL fAltGRModifier:1;
    //    BOOL fWinModifier:1;
    //    BOOL fShiftModifier:1;
    //    INT  iReserved:2;
    //    BOOL fOnInkingSurface:1;
    //    INT  iActionArgument:16;
    //}FLICK_DATA;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public sealed class FlickData
    {
        #region Constants

        #endregion

        #region Members
        private FLICKACTION_COMMANDCODE _commandCode;
        private FLICKDIRECTION _direction;
        private bool _bHasControlModifier;
        private bool _bHasMenuModifier;
        private bool _bHasAltGRModifier;
        private bool _bHasWinModifier;
        private bool _bHasShiftModifier;
        private bool _bOnInkingSurface;
        private short _iActionArgument;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FlickData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wParam"></param>
        public FlickData(IntPtr wParam)
        {
            Read(wParam);
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public FLICKDIRECTION Direction
        {
            get { return this._direction; }
        }

        /// <summary>
        /// 
        /// </summary>
        public FLICKACTION_COMMANDCODE ActionCommandCode
        {
            get { return this._commandCode; }
        }
        #endregion

        #region Read Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wParam"></param>
        public void Read(IntPtr wParam)
        {
            if (wParam == IntPtr.Zero)
                return;

            Int32 nVal = (int)wParam;

            this._iActionArgument = Flicks.GetHiWord(nVal);

            Int32 nTemp = (int)(0x0000001f & nVal);
            this._commandCode = (FLICKACTION_COMMANDCODE)(nTemp);

            nTemp = (int)(0x000000E0 & nVal);
            this._direction = (FLICKDIRECTION)(nTemp >> 5);

            nTemp = (int)(0x0000ff00 & nVal);
            nTemp = nTemp >> 8;

            this._bHasControlModifier = Convert.ToBoolean(0x00000001 & nTemp);
            nTemp = nTemp >> 1;

            this._bHasShiftModifier = Convert.ToBoolean(0x00000001 & nTemp);
            nTemp = nTemp >> 1;

            this._bHasWinModifier = Convert.ToBoolean(0x00000001 & nTemp);
            nTemp = nTemp >> 1;

            this._bHasAltGRModifier = Convert.ToBoolean(0x00000001 & nTemp);
            nTemp = nTemp >> 1;

            this._bHasMenuModifier = Convert.ToBoolean(0x00000001 & nTemp);
            nTemp = nTemp >> 1;

            this._bHasControlModifier = Convert.ToBoolean(0x00000001 & nTemp);
        }
        #endregion
    } // end of class
} // end of namespace