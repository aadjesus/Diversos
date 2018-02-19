// -----------------------------------------------------------------------
// <copyright file="ToolboxInstaller.NativeMethods.cs" company="ComponentOwl.com">
//     Copyright © 2010-2012 ComponentOwl.com. All rights reserved.
// </copyright>
// <author>Libor Tinka</author>
// -----------------------------------------------------------------------

namespace ComponentOwl.ToolboxIntegration
{
    #region Usings

    using System.Runtime.InteropServices;

    #endregion

    public partial class DteToolboxInstaller
    {
        public static class NativeMethods
        {
            /// <summary>
            ///   Implements the IOleMessageFilter interface.
            /// </summary>
            [DllImport("Ole32.dll")]
            public static extern int CoRegisterMessageFilter(IOleMessageFilter newFilter, out IOleMessageFilter oldFilter);
        }
    }
}