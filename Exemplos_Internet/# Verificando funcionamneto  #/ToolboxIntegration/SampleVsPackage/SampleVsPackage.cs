// -----------------------------------------------------------------------
// <copyright file="SampleVsPackage.cs" company="ComponentOwl.com">
//     Copyright © 2010-2012 ComponentOwl.com. All rights reserved.
// </copyright>
// <author>Libor Tinka</author>
// -----------------------------------------------------------------------

namespace ComponentOwl.ToolboxIntegration
{
    #region Usings

    using System;
    using System.Drawing.Design;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;

    #endregion

    /// <summary>
    ///   Visual Studio Package for demonstration Visual Studio Toolbox integration.
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(GuidList.guidSampleVsPackagePkgString)]
    [ProvideToolboxItems(2)]
    public sealed class SampleVsPackage : Package, IVsInstalledProduct
    {
        #region Private Constants

        /// <summary>
        ///   File name of the assembly to be integrated.
        /// </summary>
        private const string ComponentFile = "SampleControl.dll";

        /// <summary>
        ///   Toolbox tab name to be created.
        /// </summary>
        private const string TabName = "Component Owl";

        #endregion

        #region Public Constructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="SampleVsPackage" /> class.
        /// </summary>
        public SampleVsPackage()
        {
            ToolboxInitialized += OnToolboxInitialized;
            ToolboxUpgraded += OnToolboxUpgraded;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///   Called when Visual Studio Toolbox is initialized.
        /// </summary>
        private void OnToolboxInitialized(object sender, EventArgs eventArgs)
        {
            RemoveToolboxItems();
            InstallToolboxItems();
        }

        /// <summary>
        ///   Called when Visual Studio Toolbox is updated.
        /// </summary>
        private void OnToolboxUpgraded(object sender, EventArgs eventArgs)
        {
            InstallToolboxItems();
        }

        /// <summary>
        ///   Install toolbox items from assembly to Toolbox.
        /// </summary>
        private void InstallToolboxItems()
        {
            IToolboxService toolboxService = (IToolboxService)GetService(typeof(IToolboxService));

            foreach (ToolboxItem item in ToolboxService.GetToolboxItems(GetAssemblyName()))
            {
                toolboxService.AddToolboxItem(item, TabName);
            }
        }

        /// <summary>
        ///   Remove toolbox items contained in assembly from Toolbox.
        /// </summary>
        private void RemoveToolboxItems()
        {
            IToolboxService toolboxService = (IToolboxService)GetService(typeof(IToolboxService));

            foreach (ToolboxItem item in ToolboxService.GetToolboxItems(GetAssemblyName()))
            {
                toolboxService.RemoveToolboxItem(item);
            }
        }

        /// <summary>
        ///   Get AssemblyName of the assembly to integrate.
        /// </summary>
        private AssemblyName GetAssemblyName()
        {
            string pathAssembly = String.Concat(
                Path.GetDirectoryName(GetType().Assembly.Location),
                Path.DirectorySeparatorChar,
                ComponentFile);

            return AssemblyName.GetAssemblyName(pathAssembly);
        }

        #endregion

        #region IVsInstalledProduct Members

        /// <summary>
        ///   No longer in use: Visual Studio 2005 no longer calls this method.
        /// </summary>
        int IVsInstalledProduct.IdBmpSplash(out uint pIdBmp)
        {
            pIdBmp = 0;
            return 0;
        }

        /// <summary>
        ///   Obtains the icon used in the splash screen and the About dialog box on the Help menu.
        /// </summary>
        int IVsInstalledProduct.IdIcoLogoForAboutbox(out uint pIdIco)
        {
            pIdIco = 400;
            return 0;
        }

        /// <summary>
        ///   Obtains a pointer to the string containing the official name of the product that is displayed in the splash screen and About dialog box on the Help menu.
        /// </summary>
        int IVsInstalledProduct.OfficialName(out string pbstrName)
        {
            pbstrName = "Component Owl SampleControl";
            return 0;
        }

        /// <summary>
        ///   Obtains a pointer to the string containing the product details that are displayed in the About dialog box on the Help menu. Not called for the splash screen.
        /// </summary>
        int IVsInstalledProduct.ProductDetails(out string pbstrProductDetails)
        {
            pbstrProductDetails = "SampleControl control.\r\nFor more information see http://www.componentowl.com";
            return 0;
        }

        /// <summary>
        ///   Obtains a pointer to the string containing the ID of the product that is displayed in the About dialog box on the Help menu. Not called for the splash screen.
        /// </summary>
        int IVsInstalledProduct.ProductID(out string pbstrPID)
        {
            pbstrPID = "1.1.0.0";
            return 0;
        }

        #endregion
    }
}