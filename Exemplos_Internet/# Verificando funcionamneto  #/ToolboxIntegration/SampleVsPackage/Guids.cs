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
        public const string guidSampleVsPackagePkgString = "00000000-8fdf-48b6-98f8-4ff21a3a4def";

        /// <summary>
        ///   GUID representing package menu group.
        /// </summary>
        public const string guidSampleVsPackageCmdSetString = "def6519d-5ace-4062-95d6-4ee43f4a5de9";

        /// <summary>
        ///   GUID representing package menu group.
        /// </summary>
        public static readonly Guid guidSampleVsPackageCmdSet = new Guid(guidSampleVsPackageCmdSetString);
    };
}