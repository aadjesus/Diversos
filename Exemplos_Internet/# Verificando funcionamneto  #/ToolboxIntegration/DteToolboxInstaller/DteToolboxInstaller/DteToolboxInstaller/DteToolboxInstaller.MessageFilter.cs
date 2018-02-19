// -----------------------------------------------------------------------
// <copyright file="ToolboxInstaller.MessageFilter.cs" company="ComponentOwl.com">
//     Copyright © 2010-2012 ComponentOwl.com. All rights reserved.
// </copyright>
// <author>Libor Tinka</author>
// -----------------------------------------------------------------------

namespace ComponentOwl.ToolboxIntegration
{
    #region Usings

    using System;

    #endregion

    public partial class DteToolboxInstaller : IOleMessageFilter
    {
        #region Private Fields

        private IOleMessageFilter oldOleFilter;

        #endregion

        #region IOleMessageFilter Members

        /// <summary>
        ///   Handle incoming thread requests.
        /// </summary>
        int IOleMessageFilter.HandleInComingCall(int dwCallType, IntPtr hTaskCaller, int dwTickCount, IntPtr lpInterfaceInfo)
        {
            // return the flag SERVERCALL_ISHANDLED.
            return 0;
        }

        /// <summary>
        ///   Try again when thread call was rejected.
        /// </summary>
        int IOleMessageFilter.RetryRejectedCall(IntPtr hTaskCallee, int dwTickCount, int dwRejectType)
        {
            if (dwRejectType == 2) // flag = SERVERCALL_RETRYLATER
            {
                // retry the thread call immediately if return >= 0 & < 100.
                return 99;
            }

            // too busy; cancel call
            return -1;
        }

        int IOleMessageFilter.MessagePending(IntPtr hTaskCallee, int dwTickCount, int dwPendingType)
        {
            // return the flag PENDINGMSG_WAITDEFPROCESS
            return 2;
        }

        #endregion
    }
}