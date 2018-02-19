// -----------------------------------------------------------------------
// <copyright file="ExitCode.cs" company="ComponentOwl.com">
//     Copyright © 2010-2012 ComponentOwl.com. All rights reserved.
// </copyright>
// <author>Libor Tinka</author>
// -----------------------------------------------------------------------

namespace ComponentOwl.ToolboxIntegration
{
    /// <summary>
    ///   Exit codes for the application.
    /// </summary>
    internal enum ExitCode
    {
        /// <summary>
        ///   Application ended with success.
        /// </summary>
        Success = 0,

        /// <summary>
        ///   Cannot proceed because Visual Studio is not installed.
        /// </summary>
        VsNotInstalled = 1,

        /// <summary>
        ///   Toolbox Control Installer package no installed.
        /// </summary>
        TciNotInstalled = 2,

        /// <summary>
        ///   Failed to install/uninstall component.
        /// </summary>
        Fail = 3
    }
}