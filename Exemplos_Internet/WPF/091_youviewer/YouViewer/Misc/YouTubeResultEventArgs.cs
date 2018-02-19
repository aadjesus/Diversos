using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouViewer
{
    /// <summary>
    /// Simple event class, which holds
    /// a single YouTubeInfo
    /// </summary>
    public class YouTubeResultEventArgs
    {
        #region Data
        public YouTubeInfo Info { get; set; }
        #endregion

        #region Ctor
        public YouTubeResultEventArgs(YouTubeInfo info)
        {
            this.Info = info;
        }
        #endregion
    }
}
