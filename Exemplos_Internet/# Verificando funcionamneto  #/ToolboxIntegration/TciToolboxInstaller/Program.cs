// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="ComponentOwl.com">
//     Copyright © 2010-2012 ComponentOwl.com. All rights reserved.
// </copyright>
// <author>Libor Tinka</author>
// -----------------------------------------------------------------------

namespace ComponentOwl.ToolboxIntegration
{
    #region Usings

    using System;
    using System.IO;

    #endregion

    public class Program
    {
        #region Private Constants

        private const string ParameterInstall = "install";
        private const string ParameterUninstall = "uninstall";

        #endregion

        #region Public Methods

        [STAThread]
        public static int Main(string[] args)
        {
#if DEBUG

            Console.WriteLine("Mode: {0}", args[0]);
            Console.WriteLine("VS version: {0}", args[1]);
            Console.WriteLine("Tab name: {0}", args[2]);
            Console.WriteLine("Assembly path: {0}", args[3]);

#endif

            ExitCode exitCode;
            TciToolboxInstaller toolboxInstaller = null;

            try
            {
                // parse command line arguments
                if (args.Length != 4)
                {
                    throw (new ArgumentException(String.Format("Incorrect number of arguments ('{0}'). Should be '4'.", args.Length)));
                }

                string parameterMode = args[0].Trim();
                string parameterVersion = args[1].Trim();
                string parameterTabName = args[2].Replace("\"", "").Trim();
                string parameterAssemblyPath = args[3].Replace("\"", "").Trim();

                bool modeInstall;

                if (String.Equals(parameterMode, ParameterInstall, StringComparison.Ordinal))
                {
                    modeInstall = true;
                }
                else if (
                    String.Equals(parameterMode, ParameterUninstall, StringComparison.Ordinal))
                {
                    modeInstall = false;
                }
                else
                {
                    throw (new ArgumentException(String.Format("The mode parameter should be either '{0}' or '{1}', but was '{2}'.", ParameterInstall, ParameterUninstall, parameterMode)));
                }

                VisualStudioVersion version;

                if (String.Equals(parameterVersion, VisualStudioVersion.Vs2005.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    version = VisualStudioVersion.Vs2005;
                }
                else if (
                    String.Equals(parameterVersion, VisualStudioVersion.Vs2008.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    version = VisualStudioVersion.Vs2008;
                }
                else if (
                    String.Equals(parameterVersion, VisualStudioVersion.Vs2010.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    version = VisualStudioVersion.Vs2010;
                }
                else if (
                    String.Equals(parameterVersion, VisualStudioVersion.Vs2012.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    version = VisualStudioVersion.Vs2012;
                }
                else
                {
                    throw (new ArgumentException(String.Format("The version parameter was incorrect: '{0}'.", parameterVersion)));
                }

                string tabName;

                if (parameterTabName.Length != 0)
                {
                    tabName = parameterTabName;
                }
                else
                {
                    throw (new ArgumentException(String.Format("The tab name parameter contains invalid name: '{0}'.", parameterTabName)));
                }

                string assemblyPath;

                if (File.Exists(parameterAssemblyPath))
                {
                    assemblyPath = Path.GetFullPath(parameterAssemblyPath);
                }
                else
                {
                    throw (new ArgumentException(String.Format("The specified path '{0}' does not exist.", parameterAssemblyPath)));
                }

                // install/uninstall controls in Visual Studio Toolbox
                toolboxInstaller = new TciToolboxInstaller(assemblyPath, tabName);

                exitCode = (modeInstall
                                ? toolboxInstaller.InstallControls(version)
                                : toolboxInstaller.UninstallControls(version));
            }

#if DEBUG

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                exitCode = ExitCode.Fail;
            }

#else

            catch (Exception)
            {
                exitCode = ExitCode.Fail;
            }

#endif

#if DEBUG

            Console.WriteLine("Exit code: {0} ({1})", (int)exitCode, exitCode);

#endif

            return (int)exitCode;
        }

        #endregion
    }
}