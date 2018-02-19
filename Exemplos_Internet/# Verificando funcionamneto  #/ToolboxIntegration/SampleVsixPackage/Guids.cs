// -----------------------------------------------------------------------
// <copyright file="Guids.cs" company="ComponentOwl.com">
//     Copyright © 2010-2012 ComponentOwl.com. All rights reserved.
// </copyright>
// <author>Libor Tinka</author>
// -----------------------------------------------------------------------

namespace ComponentOwl.ToolboxIntegration
{
    #region Usings

    using System;

    #endregion

    /// <summary>
    ///   GUIDs used by the package.
    /// </summary>
    internal static class GuidList
    {
        /// <summary>
        ///   GUID representing the package ID.
        /// </summary>
        public const string guidSampleVsixPackagePkgString = "e3dfd099-d0ab-4b8e-b26d-639032c29ad9";

        /// <summary>
        ///   GUID representing package menu group.
        /// </summary>
        public const string guidSampleVsixPackageCmdSetString = "40863321-9665-4430-a0c2-50f1df35dbc1";

        /// <summary>
        ///   GUID representing package menu group.
        /// </summary>
        public static readonly Guid guidSampleVsixPackageCmdSet = new Guid(guidSampleVsixPackageCmdSetString);
    };
}