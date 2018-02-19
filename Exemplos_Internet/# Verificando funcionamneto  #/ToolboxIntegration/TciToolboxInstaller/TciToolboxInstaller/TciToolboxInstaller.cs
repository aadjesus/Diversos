// -----------------------------------------------------------------------
// <copyright file="TciToolboxInstaller.cs" company="ComponentOwl.com">
//     Copyright © 2010-2012 ComponentOwl.com. All rights reserved.
// </copyright>
// <author>Libor Tinka</author>
// -----------------------------------------------------------------------

namespace ComponentOwl.ToolboxIntegration
{
    #region Usings

    using System;
    using System.EnterpriseServices.Internal;
    using System.Reflection;

    using Microsoft.Win32;

    #endregion

    /// <summary>
    ///   Adds or removes control from Visual Studio Toolbox using TCI.
    /// </summary>
    internal sealed class TciToolboxInstaller
    {
        #region Private Constants

        private const string ToolboxControlsInstallerPackageGuid = "{2c298b35-07da-45f1-96a3-be55d91c8d7a}";

        #endregion

        #region Private Fields

        private readonly string assemblyPath;
        private readonly string tabName;

        #endregion

        #region Public Constructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="TciToolboxInstaller" /> class.
        /// </summary>
        /// <param name="assemblyPath"> Path of DLL to install/uninstall. </param>
        /// <param name="tabName"> Toolbox tab name. </param>
        public TciToolboxInstaller(string assemblyPath, string tabName)
        {
            this.assemblyPath = assemblyPath;
            this.tabName = tabName;
        }

        #endregion

        #region Internal Methods

        public ExitCode InstallControls(VisualStudioVersion version)
        {
            ExitCode exitCode;

            if (IsVisualStudioInstalled(version) == false)
            {
                exitCode = ExitCode.VsNotInstalled;
            }
            else
            {
                string subKeyVisualStudioConfig = GetVisualStudioConfigSubKey(version);

                // check if TCI is present
                RegistryKey keyPackageTci = Registry.CurrentUser.OpenSubKey(
                    String.Concat(subKeyVisualStudioConfig, "Packages\\", ToolboxControlsInstallerPackageGuid),
                    false);

                if (keyPackageTci == null)
                {
                    exitCode = ExitCode.TciNotInstalled;
                }
                else
                {
                    // get the assembly
                    Assembly assembly = Assembly.LoadFrom(this.assemblyPath);

                    // register the assembly with TCI
                    string subKeyVisualStudio = GetVisualStudioSubKey(version);

                    RegistryKey keyTci = Registry.LocalMachine.CreateSubKey(String.Concat(subKeyVisualStudio, "ToolboxControlsInstaller\\"));

                    RemoveAssemblyKeys(keyTci, assembly);
                    AddAssemblyKey(keyTci, assembly);

                    keyTci.Close();

                    if (version == VisualStudioVersion.Vs2012)
                    {
                        //NOTE: for Visual Studio 2012, write also to user config registry hive
                        keyTci = Registry.CurrentUser.CreateSubKey(String.Concat(subKeyVisualStudioConfig, "ToolboxControlsInstaller\\"));

                        RemoveAssemblyKeys(keyTci, assembly);
                        AddAssemblyKey(keyTci, assembly);

                        keyTci.Close();
                    }

                    // install assembly into GAC
                    (new Publish()).GacInstall(this.assemblyPath);

                    exitCode = ExitCode.Success;
                }
            }

            return exitCode;
        }

        public ExitCode UninstallControls(VisualStudioVersion version)
        {
            ExitCode exitCode;

            if (IsVisualStudioInstalled(version) == false)
            {
                exitCode = ExitCode.VsNotInstalled;
            }
            else
            {
                // get the assembly
                Assembly assembly = Assembly.LoadFrom(this.assemblyPath);

                // register the assembly with TCI
                string subKeyVisualStudio = GetVisualStudioSubKey(version);

                RegistryKey keyTci = Registry.LocalMachine.OpenSubKey(
                    String.Concat(subKeyVisualStudio, "ToolboxControlsInstaller\\"),
                    true);

                if (keyTci != null)
                {
                    RemoveAssemblyKeys(keyTci, assembly);

                    keyTci.Close();
                }

                if (version == VisualStudioVersion.Vs2012)
                {
                    //NOTE: for Visual Studio 2012, delete key in user config registry hive as well
                    string subKeyVisualStudioConfig = GetVisualStudioConfigSubKey(version);

                    keyTci = Registry.CurrentUser.OpenSubKey(
                        String.Concat(subKeyVisualStudioConfig, "ToolboxControlsInstaller\\"),
                        true);

                    if (keyTci != null)
                    {
                        RemoveAssemblyKeys(keyTci, assembly);

                        keyTci.Close();
                    }
                }

                // uninstall assembly from GAC
                (new Publish()).GacRemove(this.assemblyPath);

                exitCode = ExitCode.Success;
            }

            return exitCode;
        }

        #endregion

        #region Private Methods

        private bool IsVisualStudioInstalled(VisualStudioVersion version)
        {
            string keyName;

            switch (version)
            {
                case VisualStudioVersion.Vs2005:
                    keyName = "VisualStudio.DTE.8.0";
                    break;
                case VisualStudioVersion.Vs2008:
                    keyName = "VisualStudio.DTE.9.0";
                    break;
                case VisualStudioVersion.Vs2010:
                    keyName = "VisualStudio.DTE.10.0";
                    break;
                case VisualStudioVersion.Vs2012:
                    keyName = "VisualStudio.DTE.11.0";
                    break;
                default:
                    throw (new ApplicationException(String.Format("Unknown Visual Studio version: '{0}'.", version)));
            }

            return (Registry.ClassesRoot.OpenSubKey(keyName) != null);
        }

        private void AddAssemblyKey(RegistryKey root, Assembly assembly)
        {
            RegistryKey keyAssembly = root.CreateSubKey(assembly.ToString());

            keyAssembly.SetValue("", this.tabName, RegistryValueKind.String);
            keyAssembly.Close();
        }

        private void RemoveAssemblyKeys(RegistryKey root, Assembly assembly)
        {
            AssemblyName assemblyName = assembly.GetName();

            string strAssemblyName = assemblyName.Name;

            string strPublicKeyToken = String.Concat(
                "PublicKeyToken=",
                BitConverter.ToString(assembly.GetName().GetPublicKeyToken()).Replace("-", "").ToLowerInvariant());

            foreach (string subKeyName in root.GetSubKeyNames())
            {
                if (subKeyName.Contains(strAssemblyName) &&
                    subKeyName.Contains(strPublicKeyToken))
                {
                    root.DeleteSubKey(subKeyName);
                }
            }
        }

        private string GetVisualStudioSubKey(VisualStudioVersion version)
        {
            string subkey;

            switch (version)
            {
                case VisualStudioVersion.Vs2005:
                    subkey = @"SOFTWARE\Microsoft\VisualStudio\8.0\";
                    break;
                case VisualStudioVersion.Vs2008:
                    subkey = @"SOFTWARE\Microsoft\VisualStudio\9.0\";
                    break;
                case VisualStudioVersion.Vs2010:
                    subkey = @"SOFTWARE\Microsoft\VisualStudio\10.0\";
                    break;
                case VisualStudioVersion.Vs2012:
                    subkey = @"SOFTWARE\Microsoft\VisualStudio\11.0\";
                    break;
                default:
                    throw (new ApplicationException(String.Format("Unknown Visual Studio version: '{0}'.", version)));
            }

            return subkey;
        }

        private string GetVisualStudioConfigSubKey(VisualStudioVersion version)
        {
            string subkey;

            switch (version)
            {
                case VisualStudioVersion.Vs2005:
                    subkey = @"SOFTWARE\Microsoft\VisualStudio\8.0_Config\";
                    break;
                case VisualStudioVersion.Vs2008:
                    subkey = @"SOFTWARE\Microsoft\VisualStudio\9.0_Config\";
                    break;
                case VisualStudioVersion.Vs2010:
                    subkey = @"SOFTWARE\Microsoft\VisualStudio\10.0_Config\";
                    break;
                case VisualStudioVersion.Vs2012:
                    subkey = @"SOFTWARE\Microsoft\VisualStudio\11.0_Config\";
                    break;
                default:
                    throw (new ApplicationException(String.Format("Unknown Visual Studio version: '{0}'.", version)));
            }

            return subkey;
        }

        #endregion
    }
}