
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

    #region Enums
    /// <summary>
    /// Direction of flick
    /// Taken out of tabflicks.h header file
    /// </summary>
    public enum FLICKDIRECTION : short
    {
        FLICKDIRECTION_MIN = 0,
        FLICKDIRECTION_RIGHT = 0,
        FLICKDIRECTION_UPRIGHT = 1,
        FLICKDIRECTION_UP = 2,
        FLICKDIRECTION_UPLEFT = 3,
        FLICKDIRECTION_LEFT = 4,
        FLICKDIRECTION_DOWNLEFT = 5,
        FLICKDIRECTION_DOWN = 6,
        FLICKDIRECTION_DOWNRIGHT = 7,
        FLICKDIRECTION_INVALID = 8
    }

    /// <summary>
    /// Mode of flick
    /// Taken out of tabflicks.h header file
    /// </summary>
    public enum FLICKMODE : short
    {
        FLICKMODE_MIN = 0,
        FLICKMODE_OFF = 0,
        FLICKMODE_ON = 1,
        FLICKMODE_LEARNING = 2,
        FLICKMODE_MAX = 2,
        FLICKMODE_DEFAULT = 2
    }

    /// <summary>
    /// Command type of flick
    /// </summary>
    public enum FLICKACTION_COMMANDCODE : short
    {
        FLICKACTION_COMMANDCODE_NULL = 0,
        FLICKACTION_COMMANDCODE_SCROLL = 1,
        FLICKACTION_COMMANDCODE_APPCOMMAND = 2,
        FLICKACTION_COMMANDCODE_CUSTOMKEY = 3,
        FLICKACTION_COMMANDCODE_KEYMODIFIER = 4
    }

    /// <summary>
    /// Direction of scroll
    /// </summary>
    public enum SCROLLDIRECTION : short
    {
        SCROLLDIRECTION_UP = 0,
        SCROLLDIRECTION_DOWN = 1
    }

    /// <summary>
    /// Key modifier code
    /// </summary>
    public enum KEYMODIFIER : short
    {
        KEYMODIFIER_CONTROL = 1,
        KEYMODIFIER_MENU = 2,
        KEYMODIFIER_SHIFT = 4,
        KEYMODIFIER_WIN = 8,
        KEYMODIFIER_ALTGR = 16,
        KEYMODIFIER_EXT = 32
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public static class Flicks
    {
        #region Constants
        public const int FLICK_WM_HANDLED_MASK = 0x1;
        public const int NUM_FLICK_DIRECTIONS = 8;

        public const int WM_TABLET_DEFBASE = 0x02C0;
        public const int WM_TABLET_FLICK = (WM_TABLET_DEFBASE + 11);
        public const int WM_APPCOMMAND= 0x0319;

        public const int SM_TABLETPC = 86;
        #endregion

        #region DllImports
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetSystemMetrics(int nIndex);
        #endregion

        #region IsTabletPC Static Method
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsTabletPC()
        {
            int nResult = GetSystemMetrics(SM_TABLETPC);
            if (nResult != 0)
                return true;

            return false;
        }
        #endregion

        #region ProcessMessage Static Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Flick ProcessMessage(Message msg)
        {
            if (msg == null)
                return null;

            Flick flick = new Flick(new FlickPoint((int)msg.LParam), new FlickData(msg.WParam));

            return flick;
        }
        #endregion

        #region ProcessMessage Static Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lParam"></param>
        /// <param name="wParam"></param>
        /// <returns></returns>
        public static Flick ProcessMessage(IntPtr lParam, IntPtr wParam)
        {
            Flick flick = new Flick(new FlickPoint((int)lParam), new FlickData(wParam));

            return flick;
        }
        #endregion

        #region GetLoWord Static Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nVal"></param>
        /// <returns></returns>
        public static short GetLoWord(Int32 nVal)
        {
            return (short)(0x0000ffff & nVal);
        }
        #endregion

        #region GetHiWord Static Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nVal"></param>
        /// <returns></returns>
        public static short GetHiWord(Int32 nVal)
        {
            return (short)(nVal >> 16);
        }
        #endregion
    } // end of class
} // end of namespace